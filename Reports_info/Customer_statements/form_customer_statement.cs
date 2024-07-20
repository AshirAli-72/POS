 using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using Login_info.controllers;
using RefereningMaterial;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Customer_statements
{
    public partial class form_customer_statement : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
        public form_customer_statement()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_name_text.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", customer_name_text);
            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", customer_code_text);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            customer_name_text.Visible = false;
            customer_code_text.Visible = false;

            
            pnl_date_wise.Visible = true;
            pnl_bill_wise.Visible = false;

            this.pnl_date_wise.Dock = DockStyle.Fill;
                

            this.Viewer_dateWise.Clear();
            this.viewer_bill_wise.Clear();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
        {
            try
            {
                customer_statement_ds report = new customer_statement_ds();
                GetSetData.query = @"SELECT pos_customer_transactions.date, pos_customer_transactions.billNo, pos_customer_transactions.status, pos_customer_transactions.net_amount, pos_customer_transactions.debit, pos_customer_transactions.credits, pos_customer_transactions.pCredits,
                                    pos_customers.full_name, pos_customers.cus_code, pos_customers.opening_balance, pos_customer_lastCredits.lastCredits 
                                    FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN pos_customer_transactions ON pos_customers.customer_id = pos_customer_transactions.customer_id";

                if (condition == "datewise")
                {
                    GetSetData.query += " WHERE (pos_customer_transactions.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_customer_transactions.date asc;";
                }
                else
                {
                    GetSetData.query += " WHERE (pos_customer_transactions.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "' and pos_customers.cus_code = '" + customer_code_text.Text + "') ORDER BY pos_customer_transactions.date asc;";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables[0]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    viewer.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    viewer.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                viewer.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                viewer.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                viewer.LocalReport.SetParameters(phone);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                viewer.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                viewer.LocalReport.SetParameters(copyrights);

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                // *******************************************************************************************
                
                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_customer_statement_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //DisplayReportInReportViewer(this.Viewer_dateWise, "datewise");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                
                this.pnl_date_wise.Dock = DockStyle.Fill;
                    

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_date_wise.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;

                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_bill_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "All Transactions Report";
                reportType = 1;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                pnl_bill_wise.Visible = true;

                this.pnl_bill_wise.Dock = DockStyle.Fill;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;

                customer_name_text.Visible = true;
                customer_code_text.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    DisplayReportInReportViewer(this.Viewer_dateWise, "datewise");
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.viewer_bill_wise, "none");
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }

        private void customer_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (customer_name_text.Text.Length > 2)
                {
                    Customer_details.isDropDown = true;
                    Customer_details.role_id = role_id;
                    Customer_details.selected_customer = customer_name_text.Text;
                    button_controls.CustomerDetailsbuttons();
                    customer_name_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                }
                else
                {
                    error.errorMessage("Please enter minimum 3 characters.");
                    error.ShowDialog();
                }
            }
        }
    }
}
