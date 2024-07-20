using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using Login_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Company_ledger
{
    public partial class form_company_ledger : Form
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

        public form_company_ledger()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_suppliers", "code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("code", "pos_suppliers", "full_name", customer_name_text.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Supplier Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "full_name", customer_name_text);
            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "code", customer_code_text);

            lbl_cus_name.Visible = true;
            lbl_cus_code.Visible = true;
            customer_name_text.Visible = true;
            customer_code_text.Visible = true;

           
            pnl_over_all.Visible = false;
            pnl_supplier_wise.Visible = true;

            this.pnl_supplier_wise.Dock = DockStyle.Fill;
              

            this.Viewer_over_all.Clear();
            this.viewer_supplier_wise.Clear(); 
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
        {
            try
            {
                company_ledger_ds report = new company_ledger_ds();
                GetSetData.query = @"SELECT pos_company_transactions.date, pos_company_transactions.bill_no, pos_company_transactions.invoice_no, pos_company_transactions.status, pos_company_transactions.net_total, pos_company_transactions.paid, pos_company_transactions.credits, pos_company_transactions.pCredits,
                                    ABS(pos_suppliers.last_balance + SUM(ISNULL(net_total, 0) - ISNULL(credits, 0)) OVER (ORDER BY pos_company_transactions.date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW)) as Balances, pos_suppliers.full_name, pos_suppliers.code, pos_suppliers.last_balance, pos_supplier_payables.previous_payables
                                    FROM pos_supplier_payables INNER JOIN pos_suppliers ON pos_supplier_payables.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_company_transactions ON pos_suppliers.supplier_id = pos_company_transactions.supplier_id";

                if (condition == "Datewise")
                {
                    GetSetData.query += " WHERE pos_company_transactions.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' and pos_suppliers.full_name = '" + customer_name_text.Text + "' and pos_suppliers.code = '" + customer_code_text.Text + "' ORDER BY pos_company_transactions.date asc;";
                }
                else if (condition == "customerwise")
                {
                    GetSetData.query += " WHERE pos_suppliers.full_name = '" + customer_name_text.Text + "' and pos_suppliers.code = '" + customer_code_text.Text + "' ORDER BY pos_company_transactions.date asc;";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["supplier_wise"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables["supplier_wise"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;

                if (condition == "Datewise")
                {
                    ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                    viewer.LocalReport.SetParameters(fromDate);

                    ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                    viewer.LocalReport.SetParameters(toDate);
                }
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
                // *****************************************************************************************
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_company_ledger_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomeCodes();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomerName();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Supplier Statement";
                reportType = 1;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                this.pnl_over_all.Dock = DockStyle.Fill;
                  
                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_supplier_wise.Visible = false;
                pnl_over_all.Visible = true;

                FromDate.Visible = false;
                ToDate.Visible = false;
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
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

        private void btn_bill_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Supplier Wise Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                pnl_supplier_wise.Visible = true;

                
                this.pnl_supplier_wise.Dock = DockStyle.Fill;
                    

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_over_all.Visible = false;
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
                    DisplayReportInReportViewer(this.viewer_supplier_wise, "Datewise");
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.Viewer_over_all, "customerwise");
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

    }
}
