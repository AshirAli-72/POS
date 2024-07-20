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
using Customers_info.forms;

namespace Reports_info.Customer_sales_reports.Customer_sales_small_report
{
    public partial class form_sales_small_report : Form
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

        public form_sales_small_report()
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
            pnl_all_transaction.Visible = false;
            pnl_bill_wise.Visible = false;

            if (date_wise_button.Checked == true)
            {
                this.pnl_date_wise.Dock = DockStyle.Fill;
                this.Viewer_datewise.Dock = DockStyle.Fill;
            }
            else if (all_transactions_button.Checked == true)
            {
                this.pnl_all_transaction.Dock = DockStyle.Fill;
                this.viewer_all_transaction.Dock = DockStyle.Fill;
            }
            else if (all_transactions_button.Checked == true)
            {
                this.pnl_bill_wise.Dock = DockStyle.Fill;
                this.viewer_bill_wise.Dock = DockStyle.Fill;
            }

            this.Viewer_datewise.Clear();
            this.viewer_bill_wise.Clear();
            this.viewer_all_transaction.Clear();
        }

        private void form_sales_small_report_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                date_wise_sales();
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

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;

                if (date_wise_button.Checked == true)
                {
                    this.pnl_date_wise.Dock = DockStyle.Fill;
                    this.Viewer_datewise.Dock = DockStyle.Fill;
                }

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;

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
                lbl_billNo.Visible = true;
                txt_billNo.Visible = true;

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
                customer_code_text.Text = null;
                customer_name_text.Text = null;

                if (all_transactions_button.Checked == true)
                {
                    this.pnl_all_transaction.Dock = DockStyle.Fill;
                    this.viewer_all_transaction.Dock = DockStyle.Fill;
                }

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = true;
                pnl_bill_wise.Visible = false;

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

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
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

        private void date_wise_sales()
        {
            try
            {
                small_report_ds report = new small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price,
                                    pos_sales_details.total_purchase, pos_sales_details.discount as perItemDiscount
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
                this.Viewer_datewise.LocalReport.DataSources.Clear();
                this.Viewer_datewise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.Viewer_datewise.LocalReport.DataSources.Add(rds);
                this.Viewer_datewise.LocalReport.Refresh();


                //Return Items List****************************************************************
                small_report_ds sales_return_report = new small_report_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new Microsoft.Reporting.WinForms.ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.Viewer_datewise.LocalReport.DataSources.Add(sales_return_rds);


                //*******************************************************************************************
                DisplayReportInReportViewer(this.Viewer_datewise);
                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "';";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "';";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = "select sum(paid) from pos_sales_accounts where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = "select sum(paid) from pos_return_accounts where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "';";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = "select sum(discount) from pos_sales_accounts where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment');";
                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = "select sum(distinct(credits)) from pos_sales_accounts where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                
                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }


                GetSetData.query = "select sum(distinct(credits)) from pos_return_accounts where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "';";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.tax)  FROM pos_sales_accounts WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.tax)  FROM pos_return_accounts WHERE pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "';";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }
                // *********************************************************************************

                GetSetData.query = "select sum(discount) from pos_return_accounts where pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "';";
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.Viewer_datewise.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.Viewer_datewise.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.Viewer_datewise.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.Viewer_datewise.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.Viewer_datewise.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.Viewer_datewise.LocalReport.SetParameters(total_marketPrice);

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.Viewer_datewise.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_paid_return1 = new ReportParameter("pTotalPaidReturn", total_paid_return);
                this.Viewer_datewise.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.Viewer_datewise.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.Viewer_datewise.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.Viewer_datewise.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.Viewer_datewise.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.Viewer_datewise.LocalReport.SetParameters(return_perItem_discount1);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.Viewer_datewise.LocalReport.SetParameters(fromDate);
   
                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.Viewer_datewise.LocalReport.SetParameters(toDate);

                this.Viewer_datewise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void bill_wise_sales()
        {
            try
            {
                small_report_ds report = new small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.billNo = '" + txt_billNo.Text + "');";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
                this.viewer_bill_wise.LocalReport.DataSources.Clear();
                this.viewer_bill_wise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_bill_wise.LocalReport.DataSources.Add(rds);
                this.viewer_bill_wise.LocalReport.Refresh();

                DisplayReportInReportViewer(this.viewer_bill_wise);
                //*************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.billNo = '" + txt_billNo.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_bill_wise.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                this.viewer_bill_wise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_wise_sales()
        {
            try
            {
                small_report_ds report = new small_report_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price,
                                    pos_sales_details.total_purchase, pos_sales_details.discount as perItemDiscount
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", report.Tables[0]);
                this.viewer_all_transaction.LocalReport.DataSources.Clear();
                this.viewer_all_transaction.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_all_transaction.LocalReport.DataSources.Add(rds);
                this.viewer_all_transaction.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_all_transaction);


                //Return Items List****************************************************************
                small_report_ds sales_return_report = new small_report_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') and (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new Microsoft.Reporting.WinForms.ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.viewer_all_transaction.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.paid) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_return_accounts.paid) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.credits)) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id
                                    WHERE (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string CustomerLastCredits = data.SearchStringValuesFromDb(GetSetData.query);

                if (CustomerLastCredits == "" || CustomerLastCredits == "NULL")
                {
                    CustomerLastCredits = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.tax)  FROM pos_sales_accounts inner join pos_customers on pos_sales_accounts.customer_id = pos_customers.customer_id  WHERE 
                                    (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                
                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.tax)  FROM pos_return_accounts inner join pos_customers on pos_return_accounts.customer_id = pos_customers.customer_id  WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_all_transaction.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_all_transaction.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_paid_aomunt1);
                
                ReportParameter total_paid_return1 = new ReportParameter("pTotalPaidReturn", total_paid_return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter customerLastCredits1 = new ReportParameter("pLastCredits", CustomerLastCredits);
                this.viewer_all_transaction.LocalReport.SetParameters(customerLastCredits1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_all_transaction.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_all_transaction.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_all_transaction.LocalReport.SetParameters(return_perItem_discount1);

                this.viewer_all_transaction.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (date_wise_button.Checked == true && all_transactions_button.Checked == false && btn_bill_wise.Checked == false)
                {
                    date_wise_sales();
                }
                else if (btn_bill_wise.Checked == true && all_transactions_button.Checked == false && date_wise_button.Checked == false)
                {
                    bill_wise_sales();
                }
                else if (all_transactions_button.Checked == true && date_wise_button.Checked == false && btn_bill_wise.Checked == false)
                {
                    customer_wise_sales();
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
