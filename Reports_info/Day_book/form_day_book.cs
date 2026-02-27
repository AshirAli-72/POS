using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Reports_info.controllers;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Day_book
{
    public partial class form_day_book : Form
    {
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;
                CreateParams handleParam = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                {
                    handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                }
                else
                {
                    handleParam.ExStyle = originalExStyle;
                }
                return handleParam;
            }
        }

        public void TrunOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MinimizeBox = true;
            this.WindowState = FormWindowState.Minimized;
        }

        public form_day_book()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public int reportType = 0;

        private void refresh()
        {
            lblReportTitle.Text = "Daybook Summary Report";
            reportType = 0;
            FromDate.Format = DateTimePickerFormat.Custom;
            FromDate.CustomFormat = "yyyy-MM-dd";
            ToDate.Format = DateTimePickerFormat.Custom;
            ToDate.CustomFormat = "yyyy-MM-dd";
            pnl_summary.Visible = true;
            pnl_detailed.Visible = false;
            pnl_summary.Dock = DockStyle.Fill;
            Viewer_Summary.Clear();
            viewer_detailed.Clear();
        }

        private void form_day_book_Load(object sender, EventArgs e)
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

        private void bill_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Daybook Summary Report";
                reportType = 0;
                pnl_summary.Dock = DockStyle.Fill;
                pnl_detailed.Visible = false;
                pnl_summary.Visible = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_detailed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Daybook Detailed Report";
                reportType = 1;
                pnl_detailed.Dock = DockStyle.Fill;
                pnl_summary.Visible = false;
                pnl_detailed.Visible = true;

                viewer_detailed.SetDisplayMode(DisplayMode.PrintLayout);
                viewer_detailed.ZoomMode = ZoomMode.Percent;
                viewer_detailed.ZoomPercent = 100;
                viewer_detailed.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        // FIXED DATA METHODS — NO CLEAR(), ADD TO CURRENT VIEWER

        private void Customer_Sales()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items,
                                pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits,
                                pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_sales_details.quantity, pos_sales_details.pkg,
                                pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_products.prod_name, pos_products.barcode
                                FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
                                INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
                                INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id 
                                INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id 
                                INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
                                WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_sales_accounts.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Customers_Sales");

                ReportDataSource rds = new ReportDataSource("Customer_Sales", ds.Tables["Customers_Sales"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Customer_Returns()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_products.prod_name,
                                pos_products.barcode, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_return_accounts.billNo, pos_return_accounts.date,
                                pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid,
                                pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks
                                FROM pos_customers INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id 
                                INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
                                INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id 
                                INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id 
                                INNER JOIN pos_products ON pos_returns_details.prod_id = pos_products.product_id
                                WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_return_accounts.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "CustomerReturns");

                ReportDataSource rds = new ReportDataSource("customer_returns", ds.Tables["CustomerReturns"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Customer_Recoveries()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_recovery_details.date,
                                pos_recovery_details.time, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recoveries.amount, pos_recoveries.credits
                                FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id 
                                INNER JOIN pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id
                                INNER JOIN pos_employees ON pos_recovery_details.employee_id = pos_employees.employee_id 
                                INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
                                WHERE (pos_recovery_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_recovery_details.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Recoveries");

                ReportDataSource rds = new ReportDataSource("recoveries", ds.Tables["Recoveries"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Purchases()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no,
                                pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.pCredits,
                                pos_purchase.freight, pos_purchase.remarks, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price, pos_purchased_items.sale_price,
                                pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_suppliers.full_name, pos_suppliers.code,
                                pos_supplier_payables.previous_payables, pos_supplier_payables.due_days 
                                FROM pos_products INNER JOIN pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id 
                                INNER JOIN pos_purchase ON pos_purchased_items.purchase_id = pos_purchase.purchase_id 
                                INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id 
                                INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Purchases");

                ReportDataSource rds = new ReportDataSource("purchases", ds.Tables["Purchases"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Purchases_return()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_pur_return_items.quantity, pos_pur_return_items.pkg, pos_pur_return_items.full_pak, pos_pur_return_items.pur_price, pos_pur_return_items.sale_price,
                                pos_pur_return_items.trade_off, pos_pur_return_items.carry_exp, pos_pur_return_items.total_pur_price, pos_pur_return_items.total_sale_price, pos_products.prod_state, pos_products.unit, pos_purchase_return.date,
                                pos_purchase_return.bill_no, pos_purchase_return.invoice_no, pos_purchase_return.no_of_items, pos_purchase_return.total_quantity, pos_purchase_return.net_trade_off, pos_purchase_return.net_carry_exp,
                                pos_purchase_return.net_total, pos_purchase_return.paid, pos_purchase_return.credits, pos_purchase_return.pCredits, pos_purchase_return.freight, pos_purchase_return.remarks, pos_suppliers.full_name,
                                pos_suppliers.code, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days
                                FROM pos_products INNER JOIN pos_pur_return_items ON pos_products.product_id = pos_pur_return_items.prod_id 
                                INNER JOIN pos_purchase_return ON pos_pur_return_items.purchase_id = pos_purchase_return.pur_return_id 
                                INNER JOIN pos_suppliers ON pos_purchase_return.supplier_id = pos_suppliers.supplier_id 
                                INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                WHERE (pos_purchase_return.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase_return.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Purchase_return");

                ReportDataSource rds = new ReportDataSource("purchases_return", ds.Tables["Purchase_return"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Expenses()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_expense_details.date, pos_expense_details.time, pos_expense_details.net_amount, pos_expense_items.amount, pos_expense_items.remarks, pos_expenses.title
                                FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id 
                                INNER JOIN pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
                                WHERE (pos_expense_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_expense_details.date ASC;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Expenses");

                ReportDataSource rds = new ReportDataSource("expenses", ds.Tables["Expenses"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Inventory()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_color.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.full_pak, pos_stock_details.pur_price,
                                pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert,
                                pos_stock_details.alert_status, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type,
                                pos_products.size, pos_products.image_path, pos_products.status, pos_products.remarks
                                FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id 
                                INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                INNER JOIN pos_color ON pos_products.color_id = pos_color.color_id 
                                INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id 
                                INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                WHERE pos_stock_details.quantity > 0;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Inventory");

                ReportDataSource rds = new ReportDataSource("inventory", ds.Tables["Inventory"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void Low_Inventory()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_color.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.full_pak, pos_stock_details.pur_price,
                                pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert,
                                pos_stock_details.alert_status, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type,
                                pos_products.size, pos_products.image_path, pos_products.status, pos_products.remarks
                                FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id 
                                INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                INNER JOIN pos_color ON pos_products.color_id = pos_color.color_id 
                                INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id 
                                INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                WHERE pos_stock_details.quantity >= 0 AND pos_stock_details.quantity <= pos_stock_details.qty_alert;";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "Inventory");

                ReportDataSource rds = new ReportDataSource("low_inventory", ds.Tables["Inventory"]);
                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }

        private void expired_items()
        {
            try
            {
                day_book_ds ds = new day_book_ds();
                string query = @"SELECT pos_brand.brand_title, pos_category.title,
                         pos_subcategory.title AS Expr1, pos_products.prod_name,
                         pos_products.barcode, pos_products.manufacture_date,
                         pos_products.expiry_date, pos_products.prod_state,
                         pos_products.unit, pos_products.item_type,
                         pos_products.size, pos_products.image_path,
                         pos_products.status, pos_products.remarks,
                         pos_expired_items.date, pos_expired_items.quantity,
                         pos_expired_items.pkg, pos_expired_items.full_pak,
                         pos_expired_items.pur_price, pos_expired_items.sale_price,
                         pos_expired_items.total_pur_price,
                         pos_expired_items.total_sale_price
                         FROM pos_brand
                         INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id
                         INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id
                         INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                         INNER JOIN pos_expired_items ON pos_products.product_id = pos_expired_items.prod_id
                         WHERE pos_expired_items.date BETWEEN '" + FromDate.Text + @"' AND '" + ToDate.Text + @"'
                         ORDER BY pos_expired_items.date ASC";

                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                new SqlDataAdapter(query, conn).Fill(ds, "expired_items");

                ReportDataSource rds = new ReportDataSource("expired_items", ds.Tables["expired_items"]);

                ReportViewer viewer = btn_summary.Checked ? Viewer_Summary : viewer_detailed;
                viewer.LocalReport.DataSources.Add(rds);
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
        }
        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            viewer.ZoomMode = ZoomMode.Percent;
            viewer.ZoomPercent = 100;
            viewer.LocalReport.EnableExternalImages = true;

            GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
            GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");

            if (!string.IsNullOrEmpty(GetSetData.query) && GetSetData.query != "nill")
            {
                string fullPath = GetSetData.Data + GetSetData.query;
                ReportParameter logo = new ReportParameter("pLogo", new Uri(fullPath).AbsoluteUri);
                viewer.LocalReport.SetParameters(logo);
            }
            else
            {
                ReportParameter logo = new ReportParameter("pLogo", "");
                viewer.LocalReport.SetParameters(logo);
            }

            viewer.LocalReport.SetParameters(new ReportParameter[]
            {
                new ReportParameter("pTitle", data.UserPermissions("title", "pos_report_settings")),
                new ReportParameter("pAddress", data.UserPermissions("address", "pos_report_settings")),
                new ReportParameter("pPhone", data.UserPermissions("phone_no", "pos_report_settings")),
                new ReportParameter("pCopyrights", data.UserPermissions("copyrights", "pos_report_settings")),
                new ReportParameter("pFromDate", FromDate.Text),
                new ReportParameter("pToDate", ToDate.Text)
            });
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewer viewer = (reportType == 1) ? viewer_detailed : Viewer_Summary;

                // viewer.Reset(); // DO NOT USE RESET, it clears RDLC path and settings
                viewer.LocalReport.DataSources.Clear();

          

                // Load all data
                Customer_Sales();
                Customer_Returns();
                Customer_Recoveries();
                Purchases();
                Purchases_return();
                Expenses();
                Inventory();
                Low_Inventory();
                expired_items();

                DisplayReportInReportViewer(viewer);

                viewer.SetDisplayMode(DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage("Report Error: " + es.Message + "\n\nStack: " + es.StackTrace);
                error.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrunOffFormLevelDoubleBuffering();
        }

        private void btn_summary_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Daybook Summary Report";
                reportType = 0;
                pnl_summary.Dock = DockStyle.Fill;
                pnl_detailed.Visible = false;
                pnl_summary.Visible = true;

                Viewer_Summary.SetDisplayMode(DisplayMode.PrintLayout);
                Viewer_Summary.ZoomMode = ZoomMode.Percent;
                Viewer_Summary.ZoomPercent = 100;
                Viewer_Summary.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                ReportViewer viewer = (reportType == 1) ? viewer_detailed : Viewer_Summary;
                viewer.PrintDialog();
            }
            catch (Exception es)
            {
                error.errorMessage("Print Error: " + es.Message);
                error.ShowDialog();
            }
        }

        private void form_day_book_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                btn_print_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}