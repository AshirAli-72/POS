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

namespace Reports_info.Customer_sales_reports.Customers_return_small_reports
{
    public partial class form_returns_small_report : Form
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

        public form_returns_small_report()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

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
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            all_transactions_button.Checked = false;

            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", customer_name_text);
            GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", customer_code_text);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            lbl_billNo.Visible = false;


            customer_name_text.Visible = false;
            customer_code_text.Visible = false;
            txt_billNo.Visible = false;

            date_wise_button.Checked = true;

            pnl_date_wise.Visible = true;
            pnl_bill_wise.Visible = false;
            pnl_all_transaction.Visible = false;

            if (date_wise_button.Checked == true)
            {
                this.pnl_date_wise.Dock = DockStyle.Fill;
                this.Viewer_dateWise.Dock = DockStyle.Fill;
            }

            this.Viewer_dateWise.Clear();
            this.viewer_bill_wise.Clear();
            this.viewer_all_transaction.Clear();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
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
        }

        private void date_wise_report()
        {
            try
            {
                return_small_report_ds report = new return_small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "' order by pos_return_accounts.date;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
                this.Viewer_dateWise.LocalReport.DataSources.Clear();
                this.Viewer_dateWise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.Viewer_dateWise.LocalReport.DataSources.Add(rds);
                this.Viewer_dateWise.LocalReport.Refresh();

                // Retrive Report Settings from db *******************************************************************************************
                DisplayReportInReportViewer(this.Viewer_dateWise);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.Viewer_dateWise.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.Viewer_dateWise.LocalReport.SetParameters(toDate);

                this.Viewer_dateWise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void bill_wise_report()
        {
            try
            {
                return_small_report_ds report = new return_small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_return_accounts.billNo = '" + txt_billNo.Text + "';";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
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

        private void customer_wise_report()
        {
            try
            {
                return_small_report_ds report = new return_small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_customers.full_name = '" + customer_name_text.Text + "' and pos_customers.cus_code = '" + customer_code_text.Text + "';";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
                this.viewer_all_transaction.LocalReport.DataSources.Clear();
                this.viewer_all_transaction.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_all_transaction.LocalReport.DataSources.Add(rds);
                this.viewer_all_transaction.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_all_transaction);
                this.viewer_all_transaction.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_returns_small_report_Load(object sender, EventArgs e)
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

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                if (date_wise_button.Checked == true)
                {
                    this.pnl_date_wise.Dock = DockStyle.Fill;
                    this.Viewer_dateWise.Dock = DockStyle.Fill;
                }

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = true;
                pnl_bill_wise.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_billNo.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
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
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_bill_wise.Visible = true;
                viewer_bill_wise.Visible = true;

                if (btn_bill_wise.Checked == true)
                {
                    this.pnl_bill_wise.Dock = DockStyle.Fill;
                    this.viewer_bill_wise.Dock = DockStyle.Fill;
                }

                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_billNo.Visible = true;
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
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();


                if (all_transactions_button.Checked == true)
                {
                    this.pnl_all_transaction.Dock = DockStyle.Fill;
                    this.viewer_all_transaction.Dock = DockStyle.Fill;
                }

                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = true;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;
                lbl_billNo.Visible = false;
                customer_name_text.Visible = true;
                customer_code_text.Visible = true;
                txt_billNo.Visible = false;
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
                if (date_wise_button.Checked == true && all_transactions_button.Checked == false && btn_bill_wise.Checked == false)
                {
                    date_wise_report();
                }
                else if (btn_bill_wise.Checked == true && all_transactions_button.Checked == false && date_wise_button.Checked == false)
                {
                    bill_wise_report();
                }
                else if (all_transactions_button.Checked == true && date_wise_button.Checked == false && btn_bill_wise.Checked == false)
                {
                    customer_wise_report();
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
                if (all_transactions_button.Checked == true)
                {
                    Customer_details.role_id = role_id;
                    Customer_details.selected_customer = customer_name_text.Text;
                    button_controls.CustomerDetailsbuttons();
                    customer_name_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                }
            }
        }
    }
}
