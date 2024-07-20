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

namespace Reports_info.Company_Statement
{
    public partial class form_company_statement : Form
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

        public form_company_statement()
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
            lblReportTitle.Text = "Date Wise Supplier Statement";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "full_name", customer_name_text);
            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "code", customer_code_text);

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

        private void date_wise_sales()
        {
            try
            {
                company_statement_ds report = new company_statement_ds();
                GetSetData.query = @"SELECT date, bill_no, invoice_no, no_of_items, total_quantity, net_trade_off, net_carry_exp, net_total, paid, credits, pCredits, freight, status
                                     FROM pos_company_transactions WHERE pos_company_transactions.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' ORDER BY pos_company_transactions.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables["Purchases"]);
                this.Viewer_dateWise.LocalReport.DataSources.Clear();
                this.Viewer_dateWise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.Viewer_dateWise.LocalReport.DataSources.Add(rds);
                this.Viewer_dateWise.LocalReport.Refresh();
                DisplayReportInReportViewer(this.Viewer_dateWise);
                this.Viewer_dateWise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void supplier_wise_sales()
        {
            try
            {
                company_statement_ds report = new company_statement_ds();
                GetSetData.query = @"SELECT pos_company_transactions.date, pos_company_transactions.bill_no, pos_company_transactions.invoice_no, pos_company_transactions.no_of_items, pos_company_transactions.total_quantity, 
                                    pos_company_transactions.net_trade_off, pos_company_transactions.net_carry_exp, pos_company_transactions.net_total, pos_company_transactions.paid, pos_company_transactions.credits, 
                                    pos_company_transactions.pCredits, pos_company_transactions.freight, pos_company_transactions.status, pos_suppliers.full_name, pos_suppliers.code, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days
                                    FROM pos_supplier_payables INNER JOIN pos_suppliers ON pos_supplier_payables.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_company_transactions ON pos_suppliers.supplier_id = pos_company_transactions.supplier_id
                                    WHERE pos_company_transactions.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' and pos_suppliers.full_name = '" + customer_name_text.Text + "' and pos_suppliers.code = '" + customer_code_text.Text +"' ORDER BY pos_company_transactions.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["supplier_wise"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables["supplier_wise"]);
                this.viewer_bill_wise.LocalReport.DataSources.Clear();
                this.viewer_bill_wise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_bill_wise.LocalReport.DataSources.Add(rds);
                this.viewer_bill_wise.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_bill_wise);
                this.viewer_bill_wise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            viewer.ZoomMode = ZoomMode.Percent;
            viewer.ZoomPercent = 100;
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
            // *******************************************************************************************

            ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
            viewer.LocalReport.SetParameters(fromDate);

            ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
            viewer.LocalReport.SetParameters(toDate);
        }

        private void form_company_statement_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //date_wise_sales();
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Supplier Statement";
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
                lblReportTitle.Text = "Supplier Wise Statement";
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
                    date_wise_sales();
                }
                else if (reportType == 1)
                {
                    supplier_wise_sales();
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
