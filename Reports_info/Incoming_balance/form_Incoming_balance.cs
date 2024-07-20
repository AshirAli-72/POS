using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Incoming_balance
{
    public partial class form_Incoming_balance : Form
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

        public form_Incoming_balance()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman


        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void refresh()
        {
            lblReportTitle.Text = "Incoming Balance Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
          
            pnl_incoming.Visible = true;
            pnl_outgoing.Visible = false;
            pnl_summary.Visible = false;

            this.pnl_incoming.Dock = DockStyle.Fill;
               

            this.viewer_incomings.Clear();
            this.viewer_outgoing.Clear();
            this.viewer_summary.Clear();
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

            //GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
            //ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
            //viewer.LocalReport.SetParameters(note);

            GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
            ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
            viewer.LocalReport.SetParameters(copyrights);

            ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
            viewer.LocalReport.SetParameters(fromDate);

            ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
            viewer.LocalReport.SetParameters(toDate);
        }

        private void Customer_receivables()
        {
            try
            {
                incoming_balance_ds Customer_sales_report = new incoming_balance_ds();
                ReportDataSource Customer_Sales_rds = null;
                SqlDataAdapter Customer_Sales_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_sales_accounts.remarks, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "') order by pos_sales_accounts.date asc;";
                                                   

                // Script for Customer Sales ********************************
                Customer_Sales_da = new SqlDataAdapter(GetSetData.query, conn);
                Customer_Sales_da.Fill(Customer_sales_report, Customer_sales_report.Tables["Customers_Sales"].TableName);

                Customer_Sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_sales", Customer_sales_report.Tables["Customers_Sales"]);
                this.viewer_incomings.LocalReport.DataSources.Clear();
                this.viewer_incomings.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_incomings.LocalReport.DataSources.Add(Customer_Sales_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Purchases_return()
        {
            try
            {
                incoming_balance_ds purcahses_return_report = new incoming_balance_ds();
                ReportDataSource purchases_return_rds = null;
                SqlDataAdapter purchases_return_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_pur_return_items.quantity, pos_pur_return_items.pkg, pos_pur_return_items.full_pak, pos_pur_return_items.pur_price, pos_pur_return_items.sale_price, 
                                    pos_pur_return_items.trade_off, pos_pur_return_items.carry_exp, pos_pur_return_items.total_pur_price, pos_pur_return_items.total_sale_price, pos_products.prod_state, pos_products.unit, pos_purchase_return.date, 
                                    pos_purchase_return.bill_no, pos_purchase_return.invoice_no, pos_purchase_return.no_of_items, pos_purchase_return.total_quantity, pos_purchase_return.net_trade_off, pos_purchase_return.net_carry_exp, 
                                    pos_purchase_return.net_total, pos_purchase_return.paid, pos_purchase_return.credits, pos_purchase_return.pCredits, pos_purchase_return.freight, pos_purchase_return.remarks, pos_suppliers.full_name, 
                                    pos_suppliers.code, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days
                                    FROM pos_products INNER JOIN pos_pur_return_items ON pos_products.product_id = pos_pur_return_items.prod_id INNER JOIN pos_purchase_return ON pos_pur_return_items.purchase_id = pos_purchase_return.pur_return_id INNER JOIN
                                    pos_suppliers ON pos_purchase_return.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                    WHERE (pos_purchase_return.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase_return.date asc;";


                // Script for Purchases ********************************
                purchases_return_da = new SqlDataAdapter(GetSetData.query, conn);

                purchases_return_da.Fill(purcahses_return_report, purcahses_return_report.Tables["Purchase_return"].TableName);
                purchases_return_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases_return", purcahses_return_report.Tables["Purchase_return"]);
                this.viewer_incomings.LocalReport.DataSources.Add(purchases_return_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Customer_Recoveries()
        {
            try
            {
                incoming_balance_ds recoveries_report = new incoming_balance_ds();
                ReportDataSource recoveries_rds = null;
                SqlDataAdapter recoveries_da = null;


                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_recoveries.amount, pos_recoveries.credits, pos_recovery_details.date, pos_recovery_details.reference, pos_recovery_details.time, pos_recovery_details.remarks
                                    FROM pos_customers INNER JOIN pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
                                    where pos_recovery_details.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_recovery_details.date asc;";


                // Script for Recoveries ********************************
                recoveries_da = new SqlDataAdapter(GetSetData.query, conn);
                recoveries_da.Fill(recoveries_report, recoveries_report.Tables["Recoveries"].TableName);

                recoveries_rds = new Microsoft.Reporting.WinForms.ReportDataSource("recoveries", recoveries_report.Tables["Recoveries"]);
                this.viewer_incomings.LocalReport.DataSources.Add(recoveries_rds);
                this.viewer_incomings.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_incomings);

                //*******************************************************************************************
                GetSetData.query = @"SELECT sum(paid) as paidAmount FROM pos_sales_accounts where (pos_sales_accounts.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                string paid_amount = data.SearchStringValuesFromDb(GetSetData.query);

                if (paid_amount == "" || paid_amount == "NULL")
                {
                    paid_amount = "0";
                }
                //*******************************************************************************************

                ReportParameter pTotalSales = new ReportParameter("pTotalPaidAmount", paid_amount);
                this.viewer_incomings.LocalReport.SetParameters(pTotalSales);

                this.viewer_incomings.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        // Out_going items details 
        private void Purchases()
        {
            try
            {
                incoming_balance_ds purcahses_report = new incoming_balance_ds();
                ReportDataSource purchases_rds = null;
                SqlDataAdapter purchases_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_suppliers.full_name, pos_suppliers.code, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, 
                                    pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.pCredits, pos_purchase.freight, pos_purchase.remarks, pos_products.prod_name, pos_products.barcode, 
                                    pos_products.expiry_date, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price AS Expr1, pos_purchased_items.sale_price AS Expr2, 
                                    pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_purchased_items.purchase_id, pos_purchased_items.prod_id
                                    FROM pos_products INNER JOIN pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_suppliers INNER JOIN
                                    pos_purchase ON pos_suppliers.supplier_id = pos_purchase.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
                                    where pos_purchase.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_purchase.date asc;";


                // Script for Purchases ********************************
                purchases_da = new SqlDataAdapter(GetSetData.query, conn);
                purchases_da.Fill(purcahses_report, purcahses_report.Tables["Purchases_tbl"].TableName);

                purchases_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", purcahses_report.Tables["Purchases_tbl"]);
                this.viewer_outgoing.LocalReport.DataSources.Clear();
                this.viewer_outgoing.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_outgoing.LocalReport.DataSources.Add(purchases_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Customer_Returns()
        {
            try
            {
                incoming_balance_ds Customer_returns_report = new incoming_balance_ds();
                ReportDataSource Customer_returns_rds = null;
                SqlDataAdapter Customer_returns_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_products.prod_name, 
                                    pos_products.barcode, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_return_accounts.billNo, pos_return_accounts.date, 
                                    pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, 
                                    pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks
                                    FROM pos_customers INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id INNER JOIN
                                    pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON pos_returns_details.prod_id = pos_products.product_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_return_accounts.date asc;";


                // Script for Customer Returns ********************************
                Customer_returns_da = new SqlDataAdapter(GetSetData.query, conn);

                Customer_returns_da.Fill(Customer_returns_report, Customer_returns_report.Tables["CustomerReturns"].TableName);
                Customer_returns_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_returns_report.Tables["CustomerReturns"]);
                this.viewer_outgoing.LocalReport.DataSources.Add(Customer_returns_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Expenses()
        {
            try
            {
                incoming_balance_ds expenses_report = new incoming_balance_ds();
                ReportDataSource expenses_rds = null;
                SqlDataAdapter expenses_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_expenses.title, pos_expense_items.amount, pos_expense_items.remarks, pos_expense_details.date, pos_expense_details.time, pos_expense_details.net_amount
                                    FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN  pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
                                    where pos_expense_details.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_expense_details.date asc;";

                // Script for Expenses ********************************
                expenses_da = new SqlDataAdapter(GetSetData.query, conn);
                expenses_da.Fill(expenses_report, expenses_report.Tables["Expenses"].TableName);
                expenses_rds = new Microsoft.Reporting.WinForms.ReportDataSource("expenses", expenses_report.Tables["Expenses"]);
                this.viewer_outgoing.LocalReport.DataSources.Add(expenses_rds);
                this.viewer_outgoing.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_outgoing);

                //*******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_purchase.paid) FROM pos_purchase where (pos_purchase.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                string paid_amount = data.SearchStringValuesFromDb(GetSetData.query);

                if (paid_amount == "" || paid_amount == "NULL")
                {
                    paid_amount = "0";
                }
                //*******************************************************************************************

                ReportParameter pTotalSales = new ReportParameter("pTotalPaidAmount", paid_amount);
                this.viewer_outgoing.LocalReport.SetParameters(pTotalSales);

                this.viewer_outgoing.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void In_Out_Summary()
        {
            try
            {
                // Loyal_Customer_receivables ************************************************************************************************************************************************
                incoming_balance_ds Customer_sales_report = new incoming_balance_ds();
                ReportDataSource Customer_Sales_rds = null;
                SqlDataAdapter Customer_Sales_da = null;


                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_sales_accounts.remarks, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id
                                    where pos_sales_accounts.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_sales_accounts.date asc;";


                // Script for Customer Sales ********************************
                Customer_Sales_da = new SqlDataAdapter(GetSetData.query, conn);
                Customer_Sales_da.Fill(Customer_sales_report, Customer_sales_report.Tables["Customers_Sales"].TableName);

                Customer_Sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_sales", Customer_sales_report.Tables["Customers_Sales"]);
                this.viewer_summary.LocalReport.DataSources.Clear();
                this.viewer_summary.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_summary.LocalReport.DataSources.Add(Customer_Sales_rds);


                incoming_balance_ds purcahses_return_report = new incoming_balance_ds();
                ReportDataSource purchases_return_rds = null;
                SqlDataAdapter purchases_return_da = null;


                // script for purchase returns ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_pur_return_items.quantity, pos_pur_return_items.pkg, pos_pur_return_items.full_pak, pos_pur_return_items.pur_price, pos_pur_return_items.sale_price, 
                                    pos_pur_return_items.trade_off, pos_pur_return_items.carry_exp, pos_pur_return_items.total_pur_price, pos_pur_return_items.total_sale_price, pos_products.prod_state, pos_products.unit, pos_purchase_return.date, 
                                    pos_purchase_return.bill_no, pos_purchase_return.invoice_no, pos_purchase_return.no_of_items, pos_purchase_return.total_quantity, pos_purchase_return.net_trade_off, pos_purchase_return.net_carry_exp, 
                                    pos_purchase_return.net_total, pos_purchase_return.paid, pos_purchase_return.credits, pos_purchase_return.pCredits, pos_purchase_return.freight, pos_purchase_return.remarks, pos_suppliers.full_name, 
                                    pos_suppliers.code, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days
                                    FROM pos_products INNER JOIN pos_pur_return_items ON pos_products.product_id = pos_pur_return_items.prod_id INNER JOIN pos_purchase_return ON pos_pur_return_items.purchase_id = pos_purchase_return.pur_return_id INNER JOIN
                                    pos_suppliers ON pos_purchase_return.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                    WHERE (pos_purchase_return.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase_return.date asc;";


                // Script for Purchases ********************************
                purchases_return_da = new SqlDataAdapter(GetSetData.query, conn);

                purchases_return_da.Fill(purcahses_return_report, purcahses_return_report.Tables["Purchase_return"].TableName);
                purchases_return_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases_return", purcahses_return_report.Tables["Purchase_return"]);
                this.viewer_summary.LocalReport.DataSources.Add(purchases_return_rds);


                //Customer_Recoveries ************************************************************************************************************************************************
                incoming_balance_ds recoveries_report = new incoming_balance_ds();
                ReportDataSource recoveries_rds = null;
                SqlDataAdapter recoveries_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_recoveries.amount, pos_recoveries.credits, pos_recovery_details.date, pos_recovery_details.reference, pos_recovery_details.time, pos_recovery_details.remarks
                                    FROM pos_customers INNER JOIN pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
                                    where pos_recovery_details.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_recovery_details.date asc;";


                // Script for Recoveries ********************************
                recoveries_da = new SqlDataAdapter(GetSetData.query, conn);
                recoveries_da.Fill(recoveries_report, recoveries_report.Tables["Recoveries"].TableName);

                recoveries_rds = new Microsoft.Reporting.WinForms.ReportDataSource("recoveries", recoveries_report.Tables["Recoveries"]);
                this.viewer_summary.LocalReport.DataSources.Add(recoveries_rds);


                // purcahses_report ************************************************************************************************************************************************
                incoming_balance_ds purcahses_report = new incoming_balance_ds();
                ReportDataSource purchases_rds = null;
                SqlDataAdapter purchases_da = null;

                GetSetData.query = @"SELECT pos_suppliers.full_name, pos_suppliers.code, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, 
                                    pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.pCredits, pos_purchase.freight, pos_purchase.remarks, pos_products.prod_name, pos_products.barcode, 
                                    pos_products.expiry_date, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price AS Expr1, pos_purchased_items.sale_price AS Expr2, 
                                    pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_purchased_items.purchase_id, pos_purchased_items.prod_id
                                    FROM pos_products INNER JOIN pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_suppliers INNER JOIN
                                    pos_purchase ON pos_suppliers.supplier_id = pos_purchase.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
                                    where pos_purchase.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_purchase.date asc;;";


                // Script for Purchases ********************************
                purchases_da = new SqlDataAdapter(GetSetData.query, conn);
                purchases_da.Fill(purcahses_report, purcahses_report.Tables["Purchases_tbl"].TableName);

                purchases_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", purcahses_report.Tables["Purchases_tbl"]);
                this.viewer_summary.LocalReport.DataSources.Add(purchases_rds);


                incoming_balance_ds Customer_returns_report = new incoming_balance_ds();
                ReportDataSource Customer_returns_rds = null;
                SqlDataAdapter Customer_returns_da = null;


                // Script for customer sales returns ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_products.prod_name, 
                                    pos_products.barcode, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_return_accounts.billNo, pos_return_accounts.date, 
                                    pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, 
                                    pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks
                                    FROM pos_customers INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id INNER JOIN
                                    pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON pos_returns_details.prod_id = pos_products.product_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_return_accounts.date asc;";


                // Script for Customer Returns ********************************
                Customer_returns_da = new SqlDataAdapter(GetSetData.query, conn);

                Customer_returns_da.Fill(Customer_returns_report, Customer_returns_report.Tables["CustomerReturns"].TableName);
                Customer_returns_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_returns_report.Tables["CustomerReturns"]);
                this.viewer_summary.LocalReport.DataSources.Add(Customer_returns_rds);


                //expenses_report ************************************************************************************************************************************************
                incoming_balance_ds expenses_report = new incoming_balance_ds();
                ReportDataSource expenses_rds = null;
                SqlDataAdapter expenses_da = null;

                GetSetData.query = @"SELECT pos_expenses.title, pos_expense_items.amount, pos_expense_items.remarks, pos_expense_details.date, pos_expense_details.time, pos_expense_details.net_amount
                                    FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN  pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
                                    where pos_expense_details.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_expense_details.date asc;;";


                // Script for Expenses ********************************
                expenses_da = new SqlDataAdapter(GetSetData.query, conn);
                expenses_da.Fill(expenses_report, expenses_report.Tables["Expenses"].TableName);
                expenses_rds = new Microsoft.Reporting.WinForms.ReportDataSource("expenses", expenses_report.Tables["Expenses"]);
                this.viewer_summary.LocalReport.DataSources.Add(expenses_rds);
                this.viewer_summary.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_summary);
                this.viewer_summary.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_Incoming_balance_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                Customer_receivables();
                Purchases_return();
                Customer_Recoveries();
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

        private void btn_incoming_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Incoming Balance Report";
                reportType = 0;

                pnl_incoming.Visible = true;

                this.pnl_incoming.Dock = DockStyle.Fill;

                pnl_outgoing.Visible = false;
                pnl_summary.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_outgoing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Outgoing Balance Report";
                reportType = 1;

                pnl_outgoing.Visible = true;

                this.pnl_outgoing.Dock = DockStyle.Fill;

                pnl_incoming.Visible = false;
                pnl_summary.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_summary_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "In/Out Balance Summary Report";
                reportType = 2;

                pnl_summary.Visible = true;

                this.pnl_summary.Dock = DockStyle.Fill;

                pnl_incoming.Visible = false;
                pnl_outgoing.Visible = false;
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
                if (reportType == 0)
                {
                    Customer_receivables();
                    Purchases_return();
                    Customer_Recoveries();
                }
                else if (reportType == 1)
                {
                    Purchases();
                    Customer_Returns();
                    Expenses();
                }
                else if (reportType == 2)
                {
                    In_Out_Summary();
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
