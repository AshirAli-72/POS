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

namespace Reports_info.Customer_sales_reports.Customer_Returns_reports
{
    public partial class form_customer_returns : Form
    {
        //int originalExStyle = -1;
        //bool enableFormLevelDoubleBuffering = true;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        if (originalExStyle == -1)
        //            originalExStyle = base.CreateParams.ExStyle;

        //        CreateParams handleParam = base.CreateParams;

        //        if (enableFormLevelDoubleBuffering)
        //        {
        //            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
        //        }
        //        else
        //        {
        //            handleParam.ExStyle = originalExStyle;
        //        }

        //        return handleParam;
        //    }
        //}

        //public void TrunOffFormLevelDoubleBuffering()
        //{
        //    enableFormLevelDoubleBuffering = false;
        //    this.MinimizeBox = true;
        //    this.WindowState = FormWindowState.Minimized;
        //}

        public form_customer_returns()
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

        private void FillComboBoxEmployeeName()
        {
            txt_saleman_name.Text = data.UserPermissions("full_name", "pos_employees", "emp_code", txt_saleman_code.Text);
        }

        private void FillComboBoxEmployeeCodes()
        {
            txt_saleman_code.Text = data.UserPermissions("emp_code", "pos_employees", "full_name", txt_saleman_name.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            customer_name_text.Text = null;
            customer_code_text.Text = null;
            txt_saleman_name.Text = null;
            txt_saleman_code.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();
            txt_saleman_name.Items.Clear();
            txt_saleman_code.Items.Clear();

            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", customer_name_text);
            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", customer_code_text);
            GetSetData.FillComboBoxWithValues("select * from pos_employees;", "full_name", txt_saleman_name);
            GetSetData.FillComboBoxWithValues("select * from pos_employees;", "emp_code", txt_saleman_code);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            lbl_saleman_name.Visible = false;
            lbl_saleman_code.Visible = false;
            lbl_billNo.Visible = false;

            customer_name_text.Visible = false;
            customer_code_text.Visible = false;
            txt_saleman_name.Visible = false;
            txt_saleman_code.Visible = false;
            txt_billNo.Visible = false;


            pnl_date_wise.Visible = true;
            pnl_all_transaction.Visible = false;
            pnl_bill_wise.Visible = false;
            pnl_saleman.Visible = false;

            
            this.pnl_date_wise.Dock = DockStyle.Fill;

            this.Viewer_dateWise.Clear();
            this.viewer_bill_wise.Clear();
            this.viewer_all_transaction.Clear();
            this.viewer_saleman.Clear();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
        {
            try
            {
                Customer_returns_ds report = new Customer_returns_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_returns_details.note
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id";

                if (condition == "datewise")
                {
                    GetSetData.query += " where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "' order by pos_return_accounts.date;";
                }
                else if (condition == "billwise")
                {
                    GetSetData.query += " where pos_return_accounts.billNo = '" + txt_billNo.Text + "';";
                }
                else if (condition == "customerwise")
                {
                    GetSetData.query += " where pos_customers.full_name = '" + customer_name_text.Text + "' and pos_customers.cus_code = '" + customer_code_text.Text + "';";
                }
                else if (condition == "salesmanwise")
                {
                    GetSetData.query += " where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "' and pos_employees.full_name = '" + txt_saleman_name.Text + "' and pos_employees.emp_code = '" + txt_saleman_code.Text + "' order by pos_return_accounts.date;";
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

                if (condition == "datewise" || condition == "salesmanwise")
                {
                    ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                    viewer.LocalReport.SetParameters(fromDate);

                    ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                    viewer.LocalReport.SetParameters(toDate);
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
                // *******************************************************************************************

                GetSetData.Data = "";
                GetSetData.Data = data.UserPermissions("showNoteInReport", "pos_general_settings");
                ReportParameter showNote = new ReportParameter("showNote", GetSetData.Data);
                viewer.LocalReport.SetParameters(showNote);

                viewer.RefreshReport();
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

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void form_customer_returns_Load(object sender, EventArgs e)
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

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                this.pnl_date_wise.Dock = DockStyle.Fill;
                

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = true;
                pnl_saleman.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;

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
                lblReportTitle.Text = "Bill Wise Report";
                reportType = 1;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                lbl_billNo.Visible = true;
                txt_billNo.Visible = true;

                pnl_bill_wise.Visible = true;

                this.pnl_bill_wise.Dock = DockStyle.Fill;
       

                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_saleman.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void all_transactions_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Customer Wise Report";
                reportType = 2;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();


                this.pnl_all_transaction.Dock = DockStyle.Fill;
              

                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = true;
                pnl_bill_wise.Visible = false;
                pnl_saleman.Visible = false;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = true;
                customer_code_text.Visible = true;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_salesman_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Salesman Wise Report";
                reportType = 3;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                txt_saleman_name.Text = null;
                txt_saleman_code.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                this.pnl_saleman.Dock = DockStyle.Fill;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = true;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = true;
                lbl_saleman_code.Visible = true;

                txt_saleman_name.Visible = true;
                txt_saleman_code.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
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
                    DisplayReportInReportViewer(this.viewer_bill_wise, "billwise");
                }
                else if (reportType == 2)
                {
                    DisplayReportInReportViewer(this.viewer_all_transaction, "customerwise");
                }
                else if (reportType == 3)
                {
                    DisplayReportInReportViewer(this.viewer_saleman, "salesmanwise");
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_saleman_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxEmployeeCodes();
        }

        private void txt_saleman_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxEmployeeName();
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
