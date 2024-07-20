using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebConfig;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using Login_info.controllers;
using Reports_info.Stock;

namespace Reports_info.Stock
{
    public partial class form_stock : Form
    {
        public form_stock()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public void FillComboBoxCompany()
        {
            string query = @"select * from Category;";
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string titles_db = reader["title"].ToString();
                    txt_company.Items.Add(titles_db);
                }
                reader.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        public void FillComboBoxBrand()
        {
            string query = @"select * from Brand;";
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string titles_db = reader["brand_title"].ToString();
                    txt_brand.Items.Add(titles_db);
                }
                reader.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        public void refresh()
        {
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


            btn_low_items.Checked = false;
            btn_purchased.Checked = false;
            btn_returned.Checked = false;
            btn_summary.Checked = false;
            btn_expiry.Checked = false;
            btn_sold.Checked = true;

            pnl_sold.Visible = true;
            pnl_low_items.Visible = false;
            pnl_purchased.Visible = false;
            pnl_returned.Visible = false;
            pnl_expiry.Visible = false;
            pnl_company_wise.Visible = false;
            pnl_brand_wise.Visible = false;
            pnl_summary.Visible = false;
            
            if (btn_sold.Checked == true)
            {
                this.pnl_sold.Dock = DockStyle.Fill;
                this.viewer_sold.Dock = DockStyle.Fill;
            }
            else if (btn_returned.Checked == true)
            {
                this.pnl_returned.Dock = DockStyle.Fill;
                this.viewer_returned.Dock = DockStyle.Fill;
            }
            else if (btn_low_items.Checked == true)
            {
                this.pnl_low_items.Dock = DockStyle.Fill;
                this.viewer_low_items.Dock = DockStyle.Fill;
            }
            else if (btn_purchased.Checked == true)
            {
                this.pnl_purchased.Dock = DockStyle.Fill;
                this.viewer_purchased.Dock = DockStyle.Fill;
            }
            else if (btn_summary.Checked == true)
            {
                this.pnl_summary.Dock = DockStyle.Fill;
                this.viewer_summary.Dock = DockStyle.Fill;
            }

            chk_brand_wise.Visible = false;
            chk_company_wise.Visible = false;
            chk_over_all.Visible = false;

            chk_over_all.Checked = true;

            this.viewer_sold.Clear();
            this.viewer_returned.Clear();
            this.viewer_low_items.Clear();
            this.viewer_purchased.Clear();
            this.viewer_expiry.Clear();
            this.viewer_company_wise.Clear();
            this.viewer_brand_wise.Clear();
            this.viewer_summary.Clear();

            this.txt_company.Items.Clear();
            this.txt_brand.Items.Clear();
            FillComboBoxCompany();
            FillComboBoxBrand();
        }

        private void form_stock_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_sold_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_sold.Visible = true;
                viewer_sold.Visible = true;

                if (btn_sold.Checked == true)
                {
                    this.pnl_sold.Dock = DockStyle.Fill;
                    this.viewer_sold.Dock = DockStyle.Fill;
                }

                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_company_wise.Visible = false;

                viewer_expiry.Visible = false;
                viewer_returned.Visible = false;
                viewer_low_items.Visible = false;
                viewer_purchased.Visible = false;
                viewer_brand_wise.Visible = false;
                viewer_company_wise.Visible = false;
                viewer_summary.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;

                chk_over_all.Checked = true;
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
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_returned.Visible = true;
                viewer_returned.Visible = true;

                if (btn_returned.Checked == true)
                {
                    this.pnl_returned.Dock = DockStyle.Fill;
                    this.viewer_returned.Dock = DockStyle.Fill;
                }

                pnl_sold.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;

                viewer_brand_wise.Visible = false;
                viewer_company_wise.Visible = false;
                viewer_expiry.Visible = false;
                viewer_sold.Visible = false;
                viewer_low_items.Visible = false;
                viewer_purchased.Visible = false;
                viewer_summary.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;

                chk_over_all.Checked = true;
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
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_low_items.Visible = true;
                viewer_low_items.Visible = true;

                if (btn_low_items.Checked == true)
                {
                    this.pnl_low_items.Dock = DockStyle.Fill;
                    this.viewer_low_items.Dock = DockStyle.Fill;
                }

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;

                viewer_brand_wise.Visible = false;
                viewer_company_wise.Visible = false;
                viewer_expiry.Visible = false;
                viewer_sold.Visible = false;
                viewer_returned.Visible = false;
                viewer_purchased.Visible = false;
                viewer_summary.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;

                chk_over_all.Checked = true;
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
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_purchased.Visible = true;
                viewer_purchased.Visible = true;

                if (btn_purchased.Checked == true)
                {
                    this.pnl_purchased.Dock = DockStyle.Fill;
                    this.viewer_purchased.Dock = DockStyle.Fill;
                }

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_summary.Visible = false;
                pnl_expiry.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;

                viewer_brand_wise.Visible = false;
                viewer_company_wise.Visible = false;
                viewer_expiry.Visible = false;
                viewer_sold.Visible = false;
                viewer_returned.Visible = false;
                viewer_low_items.Visible = false;
                viewer_summary.Visible = false;

                txt_company.Visible = false;
                lbl_company.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;

                chk_over_all.Checked = true;
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
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();

                pnl_expiry.Visible = true;
                viewer_expiry.Visible = true;

                if (btn_expiry.Checked == true)
                {
                    this.pnl_expiry.Dock = DockStyle.Fill;
                    this.viewer_expiry.Dock = DockStyle.Fill;
                }

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_summary.Visible = false;

                viewer_summary.Visible = false;
                viewer_sold.Visible = false;
                viewer_returned.Visible = false;
                viewer_low_items.Visible = false;
                viewer_purchased.Visible = false;

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
                //FromDate.Text = DateTime.Now.ToLongDateString();
                //ToDate.Text = DateTime.Now.ToLongDateString();
                FromDate.Visible = true;
                ToDate.Visible = true;
                txt_brand.Visible = false;
                txt_company.Visible = false;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_brand.Visible = false;
                lbl_company.Visible = false;

                pnl_summary.Visible = true;
                viewer_summary.Visible = true;

                if (btn_summary.Checked == true)
                {
                    this.pnl_summary.Dock = DockStyle.Fill;
                    this.viewer_summary.Dock = DockStyle.Fill;
                }

                pnl_sold.Visible = false;
                pnl_returned.Visible = false;
                pnl_low_items.Visible = false;
                pnl_purchased.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_expiry.Visible = false;

                viewer_expiry.Visible = false;
                viewer_brand_wise.Visible = false;
                viewer_company_wise.Visible = false;
                viewer_sold.Visible = false;
                viewer_returned.Visible = false;
                viewer_low_items.Visible = false;
                viewer_purchased.Visible = false;

                chk_brand_wise.Visible = false;
                chk_company_wise.Visible = false;
                chk_over_all.Visible = false;

                chk_over_all.Checked = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Sold_items_report()
        {
            try
            {
                stock_ds loader_retuned_report = new stock_ds();
                stock_ds Customer_retuned_report = new stock_ds();

                SqlDataAdapter loader_da = null;
                SqlDataAdapter customer_da = null;


                ReportDataSource loader_rds = null;
                ReportDataSource Customer_rds = null;


                string quer_get_loader_sales_db = @"SELECT Products.prod_name, Products.prod_code, Products.prod_state, loaderRelateCusAccounts.billNo AS Expr1, loaderRelateCusAccounts.dates AS Expr2, loaderRelateCusAccounts.cash, loaderRelateCusAccounts.discount, 
                                                    loaderRelateCusAccounts.credits, loaderRelateCusAccounts.net_total, loaderRelateCusAccounts.totalAmount, loaderRelateCusAccounts.suplier_id, loaderRelatedCusSales.carton_sales, loaderRelatedCusSales.carton_pieces, 
                                                    loaderRelatedCusSales.quantity, loaderRelatedCusSales.unit, loaderRelatedCusSales.trade_price, loaderRelatedCusSales.prod_state, loaderRelatedCusSales.prod_id, loaderRelatedCusSales.cus_id, 
                                                    loaderRelatedCusSales.supplr_id, loaderRelatedCusSales.loadr_cus_acc_id, Customers.name, Customers.cus_code, Suppliers.name AS Expr3, Suppliers.code, loaderRelateCusAccounts.cus_id AS Expr4
                                                    FROM loaderRelatedCusSales INNER JOIN loaderRelateCusAccounts ON loaderRelatedCusSales.loadr_cus_acc_id = loaderRelateCusAccounts.loader_cus_acc_id INNER JOIN
                                                    Products ON loaderRelatedCusSales.prod_id = Products.product_id INNER JOIN
                                                    Suppliers ON loaderRelatedCusSales.supplr_id = Suppliers.supplier_id AND loaderRelateCusAccounts.suplier_id = Suppliers.supplier_id INNER JOIN
                                                    Customers ON loaderRelatedCusSales.cus_id = Customers.customer_id AND loaderRelateCusAccounts.cus_id = Customers.customer_id 
                                                    WHERE (loaderRelateCusAccounts.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY loaderRelateCusAccounts.dates asc;";


                loader_da = new SqlDataAdapter(quer_get_loader_sales_db, conn);
                loader_da.Fill(loader_retuned_report, loader_retuned_report.Tables["Loader_Sales"].TableName);

                loader_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Loader_Sales", loader_retuned_report.Tables["Loader_Sales"]);
                this.viewer_sold.LocalReport.DataSources.Clear();
                this.viewer_sold.LocalReport.DataSources.Add(loader_rds);


                // ***************************************************************************************************************
                string quer_get_loyal_cus_sales_db = @"SELECT Customers.name, Customers.cus_code, LoyalCustomerSales.dates AS Expr7, LoyalCustomerSales.cartons, LoyalCustomerSales.carton_pieces AS Expr8, LoyalCustomerSales.quantity AS Expr9, 
                                                       LoyalCustomerSales.Total_price, LoyalCusSaleAccounts.billNo AS Expr10, LoyalCusSaleAccounts.dates AS Expr11, LoyalCusSaleAccounts.cash AS Expr12, LoyalCusSaleAccounts.discount AS Expr13, 
                                                       LoyalCusSaleAccounts.credits AS Expr14, LoyalCusSaleAccounts.net_total AS Expr15, LoyalCusSaleAccounts.total_amount AS Expr16, Products.prod_name, Products.prod_code, Products.prod_state, Products.sale_price
                                                       FROM Customers INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id INNER JOIN
                                                       LoyalCustomerSales ON LoyalCusSaleAccounts.loyal_cus_sal_acc_id = LoyalCustomerSales.loyalCusAcc_id INNER JOIN
                                                       Products ON LoyalCustomerSales.prod_id = Products.product_id
                                                       WHERE (LoyalCustomerSales.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoyalCustomerSales.dates asc;";


                customer_da = new SqlDataAdapter(quer_get_loyal_cus_sales_db, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["Customers_Sales"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customers_sales", Customer_retuned_report.Tables["Customers_Sales"]);
                this.viewer_sold.LocalReport.DataSources.Add(Customer_rds);
                this.viewer_sold.LocalReport.Refresh();


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_sold.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_sold.LocalReport.SetParameters(toDate);

                this.viewer_sold.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Returned_items_report()
        {
            try
            {
                stock_ds loader_retuned_report = new stock_ds();
                stock_ds Customer_retuned_report = new stock_ds();
                
                SqlDataAdapter loader_da =  null;
                SqlDataAdapter customer_da =  null;


                ReportDataSource loader_rds = null;
                ReportDataSource Customer_rds = null;

                string quer_get_loader_cus_return_items_db = @"SELECT loaderAccounts.billNo, loaderAccounts.dates, loaderAccounts.return_amount, LoaderProductsReturn.dates AS Expr1, LoaderProductsReturn.times, LoaderProductsReturn.carton_qty, LoaderProductsReturn.carton_pieces, 
                                                               LoaderProductsReturn.unit, LoaderProductsReturn.quantity, Suppliers.code, Suppliers.name AS Expr2, Products.prod_name, Products.prod_code, Products.prod_state, Products.sale_price, Products.pur_price
                                                               FROM LoaderProductsReturn INNER JOIN
                                                               loaderAccounts ON LoaderProductsReturn.loadr_acc_id = loaderAccounts.loader_acc_id INNER JOIN
                                                               Products ON LoaderProductsReturn.prod_id = Products.product_id INNER JOIN
                                                               Suppliers ON LoaderProductsReturn.supplr_id = Suppliers.supplier_id AND loaderAccounts.supplr_id = Suppliers.supplier_id
                                                               WHERE (LoaderProductsReturn.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoaderProductsReturn.dates asc;";

                loader_da = new SqlDataAdapter(quer_get_loader_cus_return_items_db, conn);
                loader_da.Fill(loader_retuned_report, loader_retuned_report.Tables["LoaderReturns"].TableName);

                loader_rds = new Microsoft.Reporting.WinForms.ReportDataSource("loader_returns", loader_retuned_report.Tables["LoaderReturns"]);
                this.viewer_returned.LocalReport.DataSources.Clear();
                this.viewer_returned.LocalReport.DataSources.Add(loader_rds);


                // ********************************************************************************************
                string quer_get_Customer_return_items_db = @"SELECT Customers.name, Customers.cus_code, LoyalCusSaleAccounts.billNo, LoyalCusSaleAccounts.dates, LoyalCusSaleAccounts.cash, LoyalCusSaleAccounts.discount, LoyalCusSaleAccounts.credits, 
                                                             LoyalCusSaleAccounts.net_total, LoyalCusSaleAccounts.total_amount, Products.prod_name, Products.prod_code, Products.prod_state, LoyalCustomerSalesReturns.dates AS Expr1, LoyalCustomerSalesReturns.cartons, 
                                                             LoyalCustomerSalesReturns.quantity, LoyalCustomerSalesReturns.Total_price, Products.carton_qty, Products.pur_price, Products.sale_price
                                                             FROM Customers INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id INNER JOIN
                                                             LoyalCustomerSalesReturns ON LoyalCusSaleAccounts.loyal_cus_sal_acc_id = LoyalCustomerSalesReturns.loyalCusAcc_id INNER JOIN Products ON LoyalCustomerSalesReturns.prod_id = Products.product_id
                                                             WHERE (LoyalCustomerSalesReturns.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoyalCustomerSalesReturns.dates asc;";


                customer_da  = new SqlDataAdapter(quer_get_Customer_return_items_db, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["CustomerReturns"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_retuned_report.Tables["CustomerReturns"]);
                this.viewer_returned.LocalReport.DataSources.Add(Customer_rds);
                this.viewer_returned.LocalReport.Refresh();


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_returned.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_returned.LocalReport.SetParameters(toDate);


                this.viewer_returned.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Low_inventory()
        {
            try
            {
                stock_ds low_inventory_report = new stock_ds();
                ReportDataSource low_inventory_rds = null;
                SqlDataAdapter low_inventory_da = null;

                // ************************************************************************************************************************************************
                string quer_get_low_inventory_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                     Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                     FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id AND inventoryLevel.minimum >= Products.quantity INNER JOIN
                                                     Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                     WHERE (Products.quantity >= 0);";


                // Script for Low Inventory ********************************
                low_inventory_da = new SqlDataAdapter(quer_get_low_inventory_db, conn);
                low_inventory_da.Fill(low_inventory_report, low_inventory_report.Tables["Low_inventory"].TableName);

                low_inventory_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", low_inventory_report.Tables["low_inventory"]);
                this.viewer_low_items.LocalReport.DataSources.Clear();
                this.viewer_low_items.LocalReport.DataSources.Add(low_inventory_rds);
                this.viewer_low_items.LocalReport.Refresh();


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_low_items.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_low_items.LocalReport.SetParameters(toDate);

                this.viewer_low_items.RefreshReport();
            }
            catch(Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void purchased()
        {
            try
            {
                stock_ds report = new stock_ds();
                string quer_get_data_db = @"SELECT Products.prod_name, Products.prod_code, Products.quantity, Products.carton_qty, Products.unit, Products.prod_state, Products.pur_price, Products.sale_price, Purchase.billNo, Purchase.invoice_no, Purchase.receiveDate, 
                                            Purchase.billDate, Purchase.paid_amount, Purchase.credits, Purchase.billAmount, Purchase.freight, Purchase.trade_offer, Sub_category.title, Category.title AS Expr1, Brand.brand_title, Products.comments, tbl_purchase_from.title AS Expr2
                                            FROM Purchase_items INNER JOIN Products ON Purchase_items.prod_id = Products.product_id INNER JOIN Purchase ON Purchase_items.pur_id = Purchase.purchase_id INNER JOIN
                                            Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id INNER JOIN
                                            tbl_purchase_from ON Purchase.pur_from_id = tbl_purchase_from.pur_from_id
                                            WHERE (Purchase.billDate BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY Purchase.billDate asc;";

                SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
                da.Fill(report, report.Tables["Purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", report.Tables["Purchases"]);
                this.viewer_purchased.LocalReport.DataSources.Clear();
                this.viewer_purchased.LocalReport.DataSources.Add(rds);
                this.viewer_purchased.LocalReport.Refresh();


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_purchased.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_purchased.LocalReport.SetParameters(toDate);

                this.viewer_purchased.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Expired_Items_overAll_report()
        {
            try
            {
                stock_ds expired_report = new stock_ds();
                ReportDataSource expired_rds = null;
                SqlDataAdapter expired_da = null;
                //string date = "18/January/2020";
                // ************************************************************************************************************************************************
                string quer_get_low_inventory_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                     Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                     FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                     Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                     WHERE Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_da = new SqlDataAdapter(quer_get_low_inventory_db, conn);
                expired_da.Fill(expired_report, expired_report.Tables["low_inventory"].TableName);

                expired_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", expired_report.Tables["low_inventory"]);
                this.viewer_expiry.LocalReport.DataSources.Clear();
                this.viewer_expiry.LocalReport.DataSources.Add(expired_rds);
                this.viewer_expiry.LocalReport.Refresh();

                this.viewer_expiry.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Expired_Items_company_wise_report()
        {
            try
            {
                stock_ds expired_report = new stock_ds();
                ReportDataSource expired_rds = null;
                SqlDataAdapter expired_da = null;
                //string date = "18/January/2020";
                // ************************************************************************************************************************************************
                string quer_get_low_inventory_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                     Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                     FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                     Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                     WHERE Category.title = '" + txt_company.Text + "' and Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_da = new SqlDataAdapter(quer_get_low_inventory_db, conn);
                expired_da.Fill(expired_report, expired_report.Tables["low_inventory"].TableName);

                expired_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", expired_report.Tables["low_inventory"]);
                this.viewer_company_wise.LocalReport.DataSources.Clear();
                this.viewer_company_wise.LocalReport.DataSources.Add(expired_rds);
                this.viewer_company_wise.LocalReport.Refresh();

                this.viewer_company_wise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Expired_Items_brand_wise_report()
        {
            try
            {
                stock_ds expired_report = new stock_ds();
                ReportDataSource expired_rds = null;
                SqlDataAdapter expired_da = null;
                //string date = "20/January/2020";
                // ************************************************************************************************************************************************
                string quer_get_low_inventory_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                     Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                     FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                     Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                     WHERE Brand.brand_title = '" + txt_brand.Text + "' and Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_da = new SqlDataAdapter(quer_get_low_inventory_db, conn);
                expired_da.Fill(expired_report, expired_report.Tables["low_inventory"].TableName);

                expired_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", expired_report.Tables["low_inventory"]);
                this.viewer_brand_wise.LocalReport.DataSources.Clear();
                this.viewer_brand_wise.LocalReport.DataSources.Add(expired_rds);
                this.viewer_brand_wise.LocalReport.Refresh();

                this.viewer_brand_wise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void stock_summary()
        {
            try
            {
                stock_ds loader_sales_report = new stock_ds();
                stock_ds loyal_cus_sales_report = new stock_ds();

                SqlDataAdapter loader_sales_da = null;
                SqlDataAdapter loyal_cus_sales_da = null;

                ReportDataSource loader_rds = null;
                ReportDataSource loyal_cus_rds = null;

                //Loader_Customer_Sales ***************************************************************************************************************
                string quer_get_loader_sales_db = @"SELECT Products.prod_name, Products.prod_code, Products.prod_state, loaderRelateCusAccounts.billNo AS Expr1, loaderRelateCusAccounts.dates AS Expr2, loaderRelateCusAccounts.cash, loaderRelateCusAccounts.discount, 
                                                    loaderRelateCusAccounts.credits, loaderRelateCusAccounts.net_total, loaderRelateCusAccounts.totalAmount, loaderRelateCusAccounts.suplier_id, loaderRelatedCusSales.carton_sales, loaderRelatedCusSales.carton_pieces, 
                                                    loaderRelatedCusSales.quantity, loaderRelatedCusSales.unit, loaderRelatedCusSales.trade_price, loaderRelatedCusSales.prod_state, loaderRelatedCusSales.prod_id, loaderRelatedCusSales.cus_id, 
                                                    loaderRelatedCusSales.supplr_id, loaderRelatedCusSales.loadr_cus_acc_id, Customers.name, Customers.cus_code, Suppliers.name AS Expr3, Suppliers.code, loaderRelateCusAccounts.cus_id AS Expr4
                                                    FROM loaderRelatedCusSales INNER JOIN loaderRelateCusAccounts ON loaderRelatedCusSales.loadr_cus_acc_id = loaderRelateCusAccounts.loader_cus_acc_id INNER JOIN
                                                    Products ON loaderRelatedCusSales.prod_id = Products.product_id INNER JOIN
                                                    Suppliers ON loaderRelatedCusSales.supplr_id = Suppliers.supplier_id AND loaderRelateCusAccounts.suplier_id = Suppliers.supplier_id INNER JOIN
                                                    Customers ON loaderRelatedCusSales.cus_id = Customers.customer_id AND loaderRelateCusAccounts.cus_id = Customers.customer_id 
                                                    WHERE (loaderRelateCusAccounts.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY loaderRelateCusAccounts.dates asc;";


                loader_sales_da = new SqlDataAdapter(quer_get_loader_sales_db, conn);
                loader_sales_da.Fill(loader_sales_report, loader_sales_report.Tables["Loader_Sales"].TableName);

                loader_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Loader_Sales", loader_sales_report.Tables["Loader_Sales"]);
                this.viewer_summary.LocalReport.DataSources.Clear();
                this.viewer_summary.LocalReport.DataSources.Add(loader_rds);

                
                //Loyal_Customer_Sales ***************************************************************************************************************
                string quer_get_loyal_cus_sales_db = @"SELECT Customers.name, Customers.cus_code, LoyalCustomerSales.dates AS Expr7, LoyalCustomerSales.cartons, LoyalCustomerSales.carton_pieces AS Expr8, LoyalCustomerSales.quantity AS Expr9, 
                                                       LoyalCustomerSales.Total_price, LoyalCusSaleAccounts.billNo AS Expr10, LoyalCusSaleAccounts.dates AS Expr11, LoyalCusSaleAccounts.cash AS Expr12, LoyalCusSaleAccounts.discount AS Expr13, 
                                                       LoyalCusSaleAccounts.credits AS Expr14, LoyalCusSaleAccounts.net_total AS Expr15, LoyalCusSaleAccounts.total_amount AS Expr16, Products.prod_name, Products.prod_code, Products.prod_state, Products.sale_price
                                                       FROM Customers INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id INNER JOIN
                                                       LoyalCustomerSales ON LoyalCusSaleAccounts.loyal_cus_sal_acc_id = LoyalCustomerSales.loyalCusAcc_id INNER JOIN
                                                       Products ON LoyalCustomerSales.prod_id = Products.product_id
                                                       WHERE (LoyalCustomerSales.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoyalCustomerSales.dates asc;";


                loyal_cus_sales_da = new SqlDataAdapter(quer_get_loyal_cus_sales_db, conn);
                loyal_cus_sales_da.Fill(loyal_cus_sales_report, loyal_cus_sales_report.Tables["Customers_Sales"].TableName);

                loyal_cus_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customers_sales", loyal_cus_sales_report.Tables["Customers_Sales"]);
                this.viewer_summary.LocalReport.DataSources.Add(loyal_cus_rds);

                
                //Loader_Products_Returns ***************************************************************************************************************
                stock_ds loader_retuned_report = new stock_ds();
                stock_ds Customer_retuned_report = new stock_ds();
                
                SqlDataAdapter loader_da =  null;
                SqlDataAdapter customer_da =  null;


                ReportDataSource loader_returns_rds = null;
                ReportDataSource Customer_rds = null;

                string quer_get_loader_cus_return_items_db = @"SELECT loaderAccounts.billNo, loaderAccounts.dates, loaderAccounts.return_amount, LoaderProductsReturn.dates AS Expr1, LoaderProductsReturn.times, LoaderProductsReturn.carton_qty, LoaderProductsReturn.carton_pieces, 
                                                               LoaderProductsReturn.unit, LoaderProductsReturn.quantity, Suppliers.code, Suppliers.name AS Expr2, Products.prod_name, Products.prod_code, Products.prod_state, Products.sale_price, Products.pur_price
                                                               FROM LoaderProductsReturn INNER JOIN
                                                               loaderAccounts ON LoaderProductsReturn.loadr_acc_id = loaderAccounts.loader_acc_id INNER JOIN
                                                               Products ON LoaderProductsReturn.prod_id = Products.product_id INNER JOIN
                                                               Suppliers ON LoaderProductsReturn.supplr_id = Suppliers.supplier_id AND loaderAccounts.supplr_id = Suppliers.supplier_id
                                                               WHERE (LoaderProductsReturn.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoaderProductsReturn.dates asc;";

                loader_da = new SqlDataAdapter(quer_get_loader_cus_return_items_db, conn);
                loader_da.Fill(loader_retuned_report, loader_retuned_report.Tables["LoaderReturns"].TableName);

                loader_returns_rds = new Microsoft.Reporting.WinForms.ReportDataSource("loader_returns", loader_retuned_report.Tables["LoaderReturns"]);
                this.viewer_summary.LocalReport.DataSources.Add(loader_returns_rds);


                //Loyal Customer Products Returns ********************************************************************************************
                string quer_get_Customer_return_items_db = @"SELECT Customers.name, Customers.cus_code, LoyalCusSaleAccounts.billNo, LoyalCusSaleAccounts.dates, LoyalCusSaleAccounts.cash, LoyalCusSaleAccounts.discount, LoyalCusSaleAccounts.credits, 
                                                             LoyalCusSaleAccounts.net_total, LoyalCusSaleAccounts.total_amount, Products.prod_name, Products.prod_code, Products.prod_state, LoyalCustomerSalesReturns.dates AS Expr1, LoyalCustomerSalesReturns.cartons, 
                                                             LoyalCustomerSalesReturns.quantity, LoyalCustomerSalesReturns.Total_price, Products.carton_qty, Products.pur_price, Products.sale_price
                                                             FROM Customers INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id INNER JOIN
                                                             LoyalCustomerSalesReturns ON LoyalCusSaleAccounts.loyal_cus_sal_acc_id = LoyalCustomerSalesReturns.loyalCusAcc_id INNER JOIN Products ON LoyalCustomerSalesReturns.prod_id = Products.product_id
                                                             WHERE (LoyalCustomerSalesReturns.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoyalCustomerSalesReturns.dates asc;";


                customer_da  = new SqlDataAdapter(quer_get_Customer_return_items_db, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables["CustomerReturns"].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_returns", Customer_retuned_report.Tables["CustomerReturns"]);
                this.viewer_summary.LocalReport.DataSources.Add(Customer_rds);


                //Low Inventory ***********************************************************************************************************
                stock_ds low_inventory_report = new stock_ds();
                ReportDataSource low_inventory_rds = null;
                SqlDataAdapter low_inventory_da = null;

                string quer_get_low_inventory_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.quantity, Products.carton_qty, Products.unit, 
                                                     Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                     FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id AND inventoryLevel.minimum >= Products.quantity INNER JOIN
                                                     Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                     WHERE (Products.quantity >= 0);";


                low_inventory_da = new SqlDataAdapter(quer_get_low_inventory_db, conn);
                low_inventory_da.Fill(low_inventory_report, low_inventory_report.Tables["low_inventory"].TableName);

                low_inventory_rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", low_inventory_report.Tables["low_inventory"]);
                this.viewer_summary.LocalReport.DataSources.Add(low_inventory_rds);


                // Script for purchases ********************************************************************************
                stock_ds report = new stock_ds();

                string quer_get_data_db = @"SELECT Products.prod_name, Products.prod_code, Products.quantity, Products.carton_qty, Products.unit, Products.prod_state, Products.pur_price, Products.sale_price, Purchase.billNo, Purchase.invoice_no, Purchase.receiveDate, 
                                            Purchase.billDate, Purchase.paid_amount, Purchase.credits, Purchase.billAmount, Purchase.freight, Purchase.trade_offer, Sub_category.title, Category.title AS Expr1, Brand.brand_title, Products.comments, tbl_purchase_from.title AS Expr2
                                            FROM Purchase_items INNER JOIN Products ON Purchase_items.prod_id = Products.product_id INNER JOIN Purchase ON Purchase_items.pur_id = Purchase.purchase_id INNER JOIN
                                            Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id INNER JOIN
                                            tbl_purchase_from ON Purchase.pur_from_id = tbl_purchase_from.pur_from_id
                                            WHERE (Purchase.billDate BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY Purchase.billDate asc;";

                SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
                da.Fill(report, report.Tables["Purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", report.Tables["Purchases"]);
                this.viewer_summary.LocalReport.DataSources.Add(rds);


                // ************************************************************************************************************************************************
                stock_ds expired_over_all_report = new stock_ds();
                ReportDataSource expired_over_all_rds = null;
                SqlDataAdapter expired_over_all_da = null;
                //string date = "18/January/2020";
               
                string quer_get_exp_over_all_items_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                         Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                         FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                         Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                         WHERE Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_over_all_da = new SqlDataAdapter(quer_get_exp_over_all_items_db, conn);
                expired_over_all_da.Fill(expired_over_all_report, expired_over_all_report.Tables["exp_over_all"].TableName);

                expired_over_all_rds = new Microsoft.Reporting.WinForms.ReportDataSource("exp_over_all", expired_over_all_report.Tables["exp_over_all"]);
                this.viewer_summary.LocalReport.DataSources.Add(expired_over_all_rds);


                // ************************************************************************************************************************************************
                stock_ds expired_company_wise_report = new stock_ds();
                ReportDataSource expired_company_wise_rds = null;
                SqlDataAdapter expired_company_wise_da = null;
                //string date = "18/January/2020";
                
                string quer_get_expire_company_wise_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                            Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                            FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                            Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                            WHERE Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_company_wise_da = new SqlDataAdapter(quer_get_expire_company_wise_db, conn);
                expired_company_wise_da.Fill(expired_company_wise_report, expired_company_wise_report.Tables["exp_company_wise"].TableName);

                expired_company_wise_rds = new Microsoft.Reporting.WinForms.ReportDataSource("exp_company_wise", expired_company_wise_report.Tables["exp_company_wise"]);
                this.viewer_summary.LocalReport.DataSources.Add(expired_company_wise_rds);


                // ************************************************************************************************************************************************
                stock_ds expired_brand_wise_report = new stock_ds();
                ReportDataSource expired_brand_wise_rds = null;
                SqlDataAdapter expired_brand_wise_da = null;
                //string date = "18/January/2020";
                
                string quer_get_brand_wise_db = @"SELECT Sub_category.title, Category.title AS Expr1, Brand.brand_title, inventoryLevel.minimum, inventoryLevel.maximum, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, 
                                                Products.prod_state, Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price
                                                FROM inventoryLevel INNER JOIN Products ON inventoryLevel.prod_id = Products.product_id INNER JOIN
                                                Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id
                                                WHERE Products.expire_date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by Products.expire_date;";


                // Script for Low Inventory ********************************
                expired_brand_wise_da = new SqlDataAdapter(quer_get_brand_wise_db, conn);
                expired_brand_wise_da.Fill(expired_brand_wise_report, expired_brand_wise_report.Tables["exp_brand_wise"].TableName);

                expired_brand_wise_rds = new Microsoft.Reporting.WinForms.ReportDataSource("exp_brand_wise", expired_brand_wise_report.Tables["exp_brand_wise"]);
                this.viewer_summary.LocalReport.DataSources.Add(expired_brand_wise_rds);
                this.viewer_summary.LocalReport.Refresh();

                this.viewer_summary.RefreshReport();

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_summary.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_summary.LocalReport.SetParameters(toDate);

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
                if (btn_sold.Checked == true && btn_returned.Checked == false && btn_purchased.Checked == false && btn_low_items.Checked == false && btn_summary.Checked == false)
                {
                    Sold_items_report();
                }
                else if (btn_returned.Checked == true && btn_sold.Checked == false && btn_purchased.Checked == false && btn_low_items.Checked == false && btn_summary.Checked == false)
                {
                    Returned_items_report();
                }
                else if (btn_low_items.Checked == true && btn_sold.Checked == false && btn_purchased.Checked == false && btn_returned.Checked == false && btn_summary.Checked == false)
                {
                    Low_inventory();
                }
                else if (btn_purchased.Checked == true && btn_sold.Checked == false && btn_low_items.Checked == false && btn_returned.Checked == false && btn_summary.Checked == false)
                {
                    purchased();
                }
                else if (btn_expiry.Checked == true && btn_sold.Checked == false && btn_low_items.Checked == false && btn_returned.Checked == false && btn_purchased.Checked == false)
                {
                    if(chk_company_wise.Checked == true)
                    {
                        Expired_Items_company_wise_report();
                    }
                    else if (chk_brand_wise.Checked == true)
                    {
                        Expired_Items_brand_wise_report();
                    }
                    else if (chk_over_all.Checked == true)
                    {
                        Expired_Items_overAll_report();
                    }
                    

                }
                else if (btn_summary.Checked == true && btn_sold.Checked == false && btn_low_items.Checked == false && btn_returned.Checked == false && btn_purchased.Checked == false)
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
    }
}
