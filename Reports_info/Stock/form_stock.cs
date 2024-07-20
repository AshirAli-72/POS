using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Stock
{
    public partial class form_stock : Form
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

        public form_stock()
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
            lblReportTitle.Text = "Sold Items Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            FromDate.Visible = true;
            ToDate.Visible = true;
            txt_company.Visible = false;
            txt_brand.Visible = false;

            lbl_from_date.Visible = true;
            lbl_to_date.Visible = true;
            lbl_brand.Visible = false;
            lbl_company.Visible = false;

            pnl_sold.Visible = true;
            pnl_low_items.Visible = false;
            pnl_purchased.Visible = false;
            pnl_returned.Visible = false;
            pnl_expiry.Visible = false;
            pnl_company_wise.Visible = false;
            pnl_brand_wise.Visible = false;
            pnl_summary.Visible = false;
            pnl_purchase_return.Visible = false;
            
            this.pnl_sold.Dock = DockStyle.Fill;
                

            chk_brand_wise.Visible = false;
            chk_company_wise.Visible = false;
            chk_over_all.Visible = false;

            this.viewer_sold.Clear();
            this.viewer_returned.Clear();
            this.viewer_low_items.Clear();
            this.viewer_purchased.Clear();
            this.viewer_expiry.Clear();
            this.viewer_company_wise.Clear();
            this.viewer_brand_wise.Clear();
            this.viewer_summary.Clear();
            this.viewer_purchase_return.Clear();

            this.txt_company.Items.Clear();
            this.txt_brand.Items.Clear();
            GetSetData.FillComboBoxWithValues("select * from pos_category;", "title", txt_company);
            GetSetData.FillComboBoxWithValues("select * from pos_brand;", "brand_title", txt_brand);
        }

        private void form_stock_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //Sold_items_report();
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

        private void btn_sold_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Sold Items Report";
                reportType = 0;

                pnl_sold.Visible = true;

                this.pnl_sold.Dock = DockStyle.Fill;
                    

                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_purchase_return.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_returned_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Returned Items Report";
                reportType = 1;

                pnl_returned.Visible = true;
                
                this.pnl_returned.Dock = DockStyle.Fill;
                   

                pnl_sold.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_purchase_return.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_low_items_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Low Inventory Report";
                reportType = 2;

                pnl_low_items.Visible = true;

                 this.pnl_low_items.Dock = DockStyle.Fill;
                    

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_purchase_return.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_purchased_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Purchased Items Report";
                reportType = 3;

                pnl_purchased.Visible = true;

               this.pnl_purchased.Dock = DockStyle.Fill;
                    

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_purchase_return.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_expiry_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Expired Items";
                reportType = 4;

                pnl_expiry.Visible = true;

                this.pnl_expiry.Dock = DockStyle.Fill;

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_purchase_return.Visible = false;

                txt_company.Visible = false;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lbl_company.Visible = false;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                chk_brand_wise.Visible = true;
                chk_company_wise.Visible = true;
                chk_over_all.Visible = true;

                chk_over_all.Checked = true;
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
                lblReportTitle.Text = "Stock Summary";
                reportType = 5;

                FromDate.Visible = true;
                ToDate.Visible = true;
                txt_brand.Visible = false;
                txt_company.Visible = false;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_brand.Visible = false;
                lbl_company.Visible = false;

                pnl_summary.Visible = true;

                this.pnl_summary.Dock = DockStyle.Fill;
                   

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_expiry.Visible = false;
                pnl_purchase_return.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
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

            GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
            ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
            viewer.LocalReport.SetParameters(copyrights);

            if (condition != "overall")
            {
                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);
            }
        }

        private void Sold_items_report()
        {
            try
            {
                stock_ds Customer_retuned_report = new stock_ds();
                SqlDataAdapter customer_da = null;
                ReportDataSource Customer_rds = null;

                // ***************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_sales_details.quantity, pos_sales_details.pkg, 
                                    pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_products.prod_name, pos_products.barcode
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN 
                                    pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_sales_accounts.date asc;";


                customer_da = new SqlDataAdapter(GetSetData.query, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["Customers_Sales"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Customer_Sales", Customer_retuned_report.Tables["Customers_Sales"]);
                this.viewer_sold.LocalReport.DataSources.Clear();
                this.viewer_sold.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_sold.LocalReport.DataSources.Add(Customer_rds);
                this.viewer_sold.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_sold, "nill");
                this.viewer_sold.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Returned_items_report()
        {
            try
            {
                stock_ds Customer_retuned_report = new stock_ds(); 
                SqlDataAdapter customer_da =  null;
                ReportDataSource Customer_rds = null;

                // ********************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_products.prod_name, 
                                    pos_products.barcode, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_return_accounts.billNo, pos_return_accounts.date, 
                                    pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, 
                                    pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status
                                    FROM pos_customers INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id INNER JOIN
                                    pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON pos_returns_details.prod_id = pos_products.product_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_return_accounts.date asc;";


                customer_da = new SqlDataAdapter(GetSetData.query, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["CustomerReturns"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_retuned_report.Tables["CustomerReturns"]);
                this.viewer_returned.LocalReport.DataSources.Clear();
                this.viewer_returned.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_returned.LocalReport.DataSources.Add(Customer_rds);
                this.viewer_returned.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_returned, "nill");
                this.viewer_returned.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Low_inventory()
        {
            try
            {
                stock_ds low_inventory_report = new stock_ds();
                ReportDataSource low_inventory_rds = null;
                SqlDataAdapter low_inventory_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_color.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.full_pak, pos_stock_details.pur_price, 
                                    pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, 
                                    pos_stock_details.alert_status, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type, 
                                    pos_products.size, pos_products.image_path, pos_products.status
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    where pos_stock_details.quantity >= 0 and pos_stock_details.quantity <= pos_stock_details.qty_alert;";


                // Script for Low Inventory ********************************
                low_inventory_da = new SqlDataAdapter(GetSetData.query, conn);
                low_inventory_da.Fill(low_inventory_report, low_inventory_report.Tables["Inventory"].TableName);

                low_inventory_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", low_inventory_report.Tables["Inventory"]);
                this.viewer_low_items.LocalReport.DataSources.Clear();
                this.viewer_low_items.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_low_items.LocalReport.DataSources.Add(low_inventory_rds);
                this.viewer_low_items.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_low_items, "nill");
                this.viewer_low_items.RefreshReport();
            }
            catch(Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void purchased()
        {
            try
            {
                stock_ds report = new stock_ds();
                GetSetData.query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, 
                                    pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.pCredits, 
                                    pos_purchase.freight, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price, pos_purchased_items.sale_price, 
                                    pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_suppliers.full_name, pos_suppliers.code, 
                                    pos_supplier_payables.previous_payables, pos_supplier_payables.due_days FROM pos_products INNER JOIN pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN
                                    pos_purchase ON pos_purchased_items.purchase_id = pos_purchase.purchase_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                    WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase.date asc;";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", report.Tables["Purchases"]);
                this.viewer_purchased.LocalReport.DataSources.Clear();
                this.viewer_purchased.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_purchased.LocalReport.DataSources.Add(rds);
                this.viewer_purchased.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_purchased, "nill");
                this.viewer_purchased.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewerExpiry(ReportViewer viewer, string condition)
        {
            try
            {
                stock_ds expired_report = new stock_ds();
                ReportDataSource expired_rds = null;
                SqlDataAdapter expired_da = null;
           
                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_products.size, pos_products.image_path, pos_products.status, pos_expired_items.date, pos_expired_items.quantity, pos_expired_items.pkg, 
                                    pos_expired_items.full_pak, pos_expired_items.pur_price, pos_expired_items.sale_price, pos_expired_items.total_pur_price, pos_expired_items.total_sale_price
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id INNER JOIN pos_expired_items ON pos_products.product_id = pos_expired_items.prod_id";

                if (condition == "ExpiryBrandWise")
                {
                    GetSetData.query += " where pos_expired_items.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' and pos_brand.brand_title = '" + txt_brand.Text + "' ORDER BY pos_expired_items.date asc;";
                }
                else if (condition == "ExpiryCategoryWise")
                {
                    GetSetData.query += " where pos_expired_items.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' and pos_category.title = '" + txt_company.Text + "' ORDER BY pos_expired_items.date asc;";
                }
                else
                {
                    GetSetData.query += " where pos_expired_items.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ORDER BY pos_expired_items.date asc;";
                }

                // Script for Low Inventory ********************************
                expired_da = new SqlDataAdapter(GetSetData.query, conn);
                expired_da.Fill(expired_report, expired_report.Tables["expired_items"].TableName);

                expired_rds = new Microsoft.Reporting.WinForms.ReportDataSource("expired_items", expired_report.Tables["expired_items"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(expired_rds);
                viewer.LocalReport.Refresh();
                DisplayReportInReportViewer(viewer, "overall");
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void stock_summary()
        {
            try
            {
                stock_ds loyal_cus_sales_report = new stock_ds();
                SqlDataAdapter loyal_cus_sales_da = null;
                ReportDataSource loyal_cus_rds = null;
                
                //Loyal_Customer_Sales ***************************************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_sales_details.quantity, pos_sales_details.pkg, 
                                    pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_products.prod_name, pos_products.barcode
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN 
                                    pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_sales_accounts.date asc;";


                loyal_cus_sales_da = new SqlDataAdapter(GetSetData.query, conn);
                loyal_cus_sales_da.Fill(loyal_cus_sales_report, loyal_cus_sales_report.Tables["Customers_Sales"].TableName);

                loyal_cus_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Customer_Sales", loyal_cus_sales_report.Tables["Customers_Sales"]);
                this.viewer_summary.LocalReport.DataSources.Clear();
                this.viewer_summary.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_summary.LocalReport.DataSources.Add(loyal_cus_rds);

                
                //Loader_Products_Returns ***************************************************************************************************************
                stock_ds Customer_retuned_report = new stock_ds();         
                SqlDataAdapter customer_da =  null;
                ReportDataSource Customer_rds = null;

                //Loyal Customer Products Returns ********************************************************************************************
                GetSetData.query = @"SELECT pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_products.prod_name, 
                                    pos_products.barcode, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_return_accounts.billNo, pos_return_accounts.date, 
                                    pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, 
                                    pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status
                                    FROM pos_customers INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id INNER JOIN
                                    pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON pos_returns_details.prod_id = pos_products.product_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_return_accounts.date asc;";


                customer_da = new SqlDataAdapter(GetSetData.query, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["CustomerReturns"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_retuned_report.Tables["CustomerReturns"]);
                this.viewer_summary.LocalReport.DataSources.Add(Customer_rds);


                //Low Inventory ***********************************************************************************************************
                stock_ds low_inventory_report = new stock_ds();
                ReportDataSource low_inventory_rds = null;
                SqlDataAdapter low_inventory_da = null;

                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_color.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.full_pak, pos_stock_details.pur_price, 
                                    pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, 
                                    pos_stock_details.alert_status, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type, 
                                    pos_products.size, pos_products.image_path, pos_products.status
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    where pos_stock_details.quantity >= 0 and pos_stock_details.quantity <= pos_stock_details.qty_alert;";


                low_inventory_da = new SqlDataAdapter(GetSetData.query, conn);
                low_inventory_da.Fill(low_inventory_report, low_inventory_report.Tables["Inventory"].TableName);

                low_inventory_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", low_inventory_report.Tables["Inventory"]);
                this.viewer_summary.LocalReport.DataSources.Add(low_inventory_rds);


                // Script for purchases ********************************************************************************
                stock_ds report = new stock_ds();

                GetSetData.query = @"SELECT pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, pos_products.unit, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, 
                                    pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.pCredits, 
                                    pos_purchase.freight, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price, pos_purchased_items.sale_price, 
                                    pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_suppliers.full_name, pos_suppliers.code, 
                                    pos_supplier_payables.previous_payables, pos_supplier_payables.due_days FROM pos_products INNER JOIN pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN
                                    pos_purchase ON pos_purchased_items.purchase_id = pos_purchase.purchase_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
                                    WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase.date asc;";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", report.Tables["Purchases"]);
                this.viewer_summary.LocalReport.DataSources.Add(rds);


                // Script for Expired Items************************************************************************************************************************************************
                stock_ds expired_over_all_report = new stock_ds();
                ReportDataSource expired_over_all_rds = null;
                SqlDataAdapter expired_over_all_da = null;

                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_expired_items.quantity, pos_expired_items.pkg, pos_expired_items.full_pak, pos_expired_items.pur_price, pos_expired_items.sale_price, 
                                    pos_expired_items.total_pur_price, pos_expired_items.total_sale_price, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_products.size, pos_products.image_path, pos_products.status
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_expired_items ON pos_products.product_id = pos_expired_items.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    where pos_products.expiry_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ORDER BY pos_products.expiry_date asc;";


                // Script for Low Inventory ********************************
                expired_over_all_da = new SqlDataAdapter(GetSetData.query, conn);
                expired_over_all_da.Fill(expired_over_all_report, expired_over_all_report.Tables["Expired_items"].TableName);

                expired_over_all_rds = new Microsoft.Reporting.WinForms.ReportDataSource("expired_items", expired_over_all_report.Tables["Expired_items"]);
                this.viewer_summary.LocalReport.DataSources.Add(expired_over_all_rds);
                this.viewer_summary.LocalReport.Refresh();
                this.viewer_summary.RefreshReport();
                DisplayReportInReportViewer(this.viewer_summary, "nill");
                this.viewer_summary.RefreshReport();

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
                    Sold_items_report();
                }
                else if (reportType == 1)
                {
                    Returned_items_report();
                }
                else if (reportType == 2)
                {
                    Low_inventory();
                }
                else if (reportType == 3)
                {
                    purchased();
                }
                else if (reportType == 4)
                {
                    if (chk_company_wise.Checked == true)
                    {
                        DisplayReportInReportViewerExpiry(this.viewer_company_wise, "ExpiryCategoryWise");
                    }
                    else if (chk_brand_wise.Checked == true)
                    {
                        DisplayReportInReportViewerExpiry(this.viewer_brand_wise, "ExpiryBrandWise");
                    }
                    else if (chk_over_all.Checked == true)
                    {
                        DisplayReportInReportViewerExpiry(this.viewer_expiry, "nill");
                    }
                }
                else if (reportType == 5)
                {
                    stock_summary();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chk_company_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_company_wise.Checked == true)
                {
                    lblReportTitle.Text = "Category Wise Expired Items";

                    chk_brand_wise.Checked = false;
                    chk_over_all.Checked = false;

                    txt_company.Visible = true;
                    txt_brand.Visible = false;

                    lbl_company.Visible = true;
                    lbl_brand.Visible = false;


                    this.pnl_company_wise.Dock = DockStyle.Fill;
                    this.viewer_company_wise.Dock = DockStyle.Fill;


                    pnl_brand_wise.Visible = false;
                    pnl_company_wise.Visible = true;
                    pnl_expiry.Visible = false;

                    viewer_brand_wise.Visible = false;
                    viewer_company_wise.Visible = true;
                    viewer_expiry.Visible = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void chk_brand_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_brand_wise.Checked == true)
                {
                    lblReportTitle.Text = "Brand Wise Expired Items";

                    chk_company_wise.Checked = false;
                    chk_over_all.Checked = false;

                    txt_company.Visible = false;
                    txt_brand.Visible = true;

                    lbl_company.Visible = false;
                    lbl_brand.Visible = true;


                    this.pnl_brand_wise.Dock = DockStyle.Fill;
                    this.viewer_brand_wise.Dock = DockStyle.Fill;

                    pnl_brand_wise.Visible = true;
                    pnl_company_wise.Visible = false;
                    pnl_expiry.Visible = false;

                    viewer_brand_wise.Visible = true;
                    viewer_company_wise.Visible = false;
                    viewer_expiry.Visible = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void chk_over_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_over_all.Checked == true)
                {
                    lblReportTitle.Text = "Overall Expired Items";

                    chk_company_wise.Checked = false;
                    chk_brand_wise.Checked = false;

                    txt_company.Visible = false;
                    txt_brand.Visible = false;

                    lbl_company.Visible = false;
                    lbl_brand.Visible = false;


                    this.pnl_expiry.Dock = DockStyle.Fill;
                    this.viewer_expiry.Dock = DockStyle.Fill;

                    pnl_brand_wise.Visible = false;
                    pnl_company_wise.Visible = false;
                    pnl_expiry.Visible = true;

                    viewer_brand_wise.Visible = false;
                    viewer_company_wise.Visible = false;
                    viewer_expiry.Visible = true;
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
