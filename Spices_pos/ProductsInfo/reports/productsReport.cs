using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.reports;

namespace Products_info.reports
{
    public partial class productsReport : Form
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

        public productsReport()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0;

        private void productsReport_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //DisplayReportInReportViewer(this.viewer_over_all, "nill", "nill");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh()
        {
            lblReportTitle.Text = "Over All Inventory Report";
            reportType = 0;

            txtDate.Text = DateTime.Now.ToLongDateString();

            lblTitle.Visible = true;
            txtTitle.Visible = false;
            txtStatus.Visible = true;
            lblTitle.Text = "Status:";

            pnl_over_all.Visible = true;
            pnl_sale_price.Visible = false;
            pnl_brand_wise.Visible = false;
            pnl_company_wise.Visible = false;
            pnl_code_list.Visible = false;
            pnl_shelf_title.Visible = false;
            pnl_shelf_items.Visible = false;
            pnlDateWise.Visible = false;
            pnlTaxWise.Visible = false;

            lblDate.Visible = false;
            txtDate.Visible = false;

            this.pnl_over_all.Dock = DockStyle.Fill;
          

            this.viewer_over_all.Clear();
            this.Viewer_company_wise.Clear();
            this.viewer_brand_wise.Clear();
            this.viewer_code_list.Clear();
            this.viewer_shelf_title.Clear();
            this.viewer_shelf_items.Clear();
        }

        //private void CategoryWiseReport()
        //{
        //    try
        //    {
        //        //ProductsList_ds report = new ProductsList_ds();
        //        //Viewer_company_wise.Reset();
        //        //Viewer_company_wise.ProcessingMode = ProcessingMode.Local;
        //        //LocalReport localReport = Viewer_company_wise.LocalReport;
        //        //localReport.ReportPath = @"C:\Users\AFAQ ALI\Desktop\POS_Soft Code Optamization\Products_info\reports\Company_wise\company_wise_report.rdlc";
        //        //this.company_wiseTableAdapter.Fill(this.ProductsList_ds.company_wise, txt_company.Text);
        //        //this.Viewer_company_wise.LocalReport.DataSources.Clear();
        //        //ReportDataSource rds = new ReportDataSource("productList", report.Tables[0]);
        //        //this.pos_report_settingsBindingSource.DataSource = rds;

        //        this.company_wiseTableAdapter.Fill(this.ProductsList_ds.company_wise, txt_company.Text);
        //        this.pos_report_settingsTableAdapter.Fill(this.ProductsList_ds.pos_report_settings);
        //        this.Viewer_company_wise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        this.Viewer_company_wise.ZoomMode = ZoomMode.Percent;
        //        this.Viewer_company_wise.ZoomPercent = 100;
        //        this.Viewer_company_wise.LocalReport.Refresh();
        //        this.Viewer_company_wise.RefreshReport();
        //    }
        //    catch (Exception e)
        //    {
        //        error.errorMessage(e.Message);
        //        error.ShowDialog();
        //    }
        //}

        //private void CategoryWiseReport(ReportViewer viewer)
        //{
        //    ProductsList_ds report = new ProductsList_ds();
        //    DataSet ds = GetSetData.GetDataSet("abc", txt_company.Text);
        //    GetSetData.adptr_.Fill(ds, ds.Tables["company_wise"].TableName);

        //    //viewer.LocalReport.ReportPath = @"C:\Users\AFAQ ALI\Desktop\POS_Soft Code Optamization\Products_info\reports\Company_wise\company_wise_report.rdlc";
        //    ReportDataSource rds = new ReportDataSource("productList", ds.Tables["company_wise"]);
        //    viewer.LocalReport.DataSources.Clear();
        //    viewer.LocalReport.DataSources.Add(rds);
        //    //bindingsource.DataSource = rds;


        //    //ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("productList", ds.Tables["company_wise"]);
        //    //viewer.LocalReport.DataSources.Clear();
        //    //viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //    //viewer.LocalReport.DataSources.Add(rds);

        //    // **********************************************************************************************************
        //    ProductsList_ds report_setting = new ProductsList_ds();
        //    DataSet ds1 = GetSetData.GetDataSet("ProcedureReportDetails", "none");
        //    GetSetData.adptr_.Fill(ds1, ds1.Tables["pos_report_settings"].TableName);

        //    ReportDataSource report_rds = new ReportDataSource("report_setting_ds", ds1.Tables["pos_report_settings"]);
        //    viewer.LocalReport.DataSources.Clear();
        //    viewer.LocalReport.DataSources.Add(report_rds);
        //    //bindingsource.DataSource = report_rds;
        //    //GetSetData.adptr_.Fill(ds1, ds1.Tables["pos_report_settings"].TableName);
        //    //ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", ds1.Tables["pos_report_settings"]);
        //    //viewer.LocalReport.DataSources.Add(report_rds);

        //    viewer.LocalReport.Refresh();
        //    viewer.RefreshReport();

        //    //reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
        //    //ReportDataSource rds = new ReportDataSource("ProductsDataSet", ds.Tables[0]);
        //    //this.reportViewer1.LocalReport.DataSources.Clear();
        //    //this.reportViewer1.LocalReport.DataSources.Add(rds);
        //    //this.bindingSource1.DataSource = rds;
        //    //this.reportViewer1.RefreshReport();
        //}

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition, string value)
        {
            try
            {
                ProductsList_ds report = new ProductsList_ds();

                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_color.title AS Expr1, pos_subcategory.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.tab_pieces, pos_stock_details.pur_price, pos_stock_details.full_pak, 
                                    pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, 
                                    pos_stock_details.alert_status, pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_manufacture as manufacture_date, pos_stock_details.date_of_expiry as expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type, 
                                    pos_stock_details.whole_sale_price as size, pos_products.image_path, pos_products.status, pos_products.remarks
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id";

                if (condition != "nill" && value != "nill")
                {
                    GetSetData.query += " where " + condition + " = '" + value + "'";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataSet"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("productList", report.Tables["DataSet"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);

                // **********************************************************************************************************
                ProductsList_ds report_setting = new ProductsList_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

                viewer.LocalReport.Refresh();

                viewer.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(pur_price * quantity) FROM pos_stock_details;";
                string total_cost_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_cost_price == "" || total_cost_price == "NULL")
                {
                    total_cost_price = "0";
                }

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(sale_price * quantity) FROM pos_stock_details;";
                string total_sale_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_sale_price == "" || total_sale_price == "NULL")
                {
                    total_sale_price = "0";
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

                ReportParameter pTotalCostPrice = new ReportParameter("pTotalCostPrice", total_cost_price);
                viewer.LocalReport.SetParameters(pTotalCostPrice);


                ReportParameter pTotalSalePrice = new ReportParameter("pTotalSalePrice", total_sale_price);
                viewer.LocalReport.SetParameters(pTotalSalePrice);



                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewerShelfWise(ReportViewer viewer, string condition, string value)
        {
            try
            {
                ProductsList_ds report = new ProductsList_ds();

                GetSetData.query = @"SELECT pos_shelfItems.title, pos_category.title AS Expr1, pos_brand.brand_title, pos_color.title AS Expr2, pos_subcategory.title AS Expr3, pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_expiry as expiry_date, 
                                    pos_products.prod_state, pos_products.unit, pos_products.item_type, pos_stock_details.whole_sale_price as size, pos_products.status, pos_products.manufacture_date, pos_products.image_path, pos_stock_details.quantity, pos_stock_details.pkg, 
                                    pos_stock_details.tab_pieces, pos_stock_details.full_pak, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, 
                                    pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, pos_stock_details.alert_status
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_shelf_grouping ON pos_products.product_id = pos_shelf_grouping.prod_id INNER JOIN
                                    pos_shelfItems ON pos_shelf_grouping.shelf_id = pos_shelfItems.shelf_id INNER JOIN  pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN
                                    pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id";

                if (condition != "nill" && value != "nill")
                {
                    GetSetData.query += " where " + condition + " = '" + value + "'";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Shelf_items"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("productList", report.Tables["Shelf_items"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);

                // **********************************************************************************************************
                ProductsList_ds report_setting = new ProductsList_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

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

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void dateWiseInventoryHistoryReport()
        {
            try
            {
                ProductsList_ds report = new ProductsList_ds();

                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_color.title AS Expr1, pos_subcategory.title AS Expr2, pos_stock_backup.date, pos_stock_backup.quantity, pos_stock_backup.pkg, pos_stock_backup.tab_pieces, pos_stock_backup.pur_price, pos_stock_backup.full_pak, 
                                     pos_stock_backup.sale_price, pos_stock_backup.market_value, pos_stock_backup.trade_off, pos_stock_backup.carry_exp, pos_stock_backup.total_pur_price, pos_stock_backup.total_sale_price, pos_stock_backup.qty_alert, 
                                     pos_stock_backup.alert_status, pos_products.prod_name, pos_stock_backup.item_barcode as barcode, pos_stock_backup.date_of_manufacture as manufacture_date, pos_stock_backup.date_of_expiry as expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type, 
                                     pos_stock_backup.whole_sale_price as size, pos_products.image_path, pos_products.status, pos_products.remarks
                                     FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                     pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_stock_backup ON pos_products.product_id = pos_stock_backup.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                     where (pos_stock_backup.date = '" + txtDate.Text +"')";


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["InventoryHistory"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("productList", report.Tables["InventoryHistory"]);
                viewerDateWise.LocalReport.DataSources.Clear();
                viewerDateWise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewerDateWise.ZoomMode = ZoomMode.Percent;
                viewerDateWise.ZoomPercent = 100;
                viewerDateWise.LocalReport.DataSources.Add(rds);

                // **********************************************************************************************************
                ProductsList_ds report_setting = new ProductsList_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewerDateWise.LocalReport.DataSources.Add(report_rds);

                viewerDateWise.LocalReport.Refresh();

                viewerDateWise.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(pur_price * quantity) FROM pos_stock_backup where (date = '" + txtDate.Text + "');";
                string total_cost_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_cost_price == "" || total_cost_price == "NULL")
                {
                    total_cost_price = "0";
                }

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(sale_price * quantity) FROM pos_stock_backup where (date = '" + txtDate.Text + "');";
                string total_sale_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_sale_price == "" || total_sale_price == "NULL")
                {
                    total_sale_price = "0";
                }


                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    viewerDateWise.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    viewerDateWise.LocalReport.SetParameters(logo);
                }


                ReportParameter pTotalCostPrice = new ReportParameter("pTotalCostPrice", total_cost_price);
                viewerDateWise.LocalReport.SetParameters(pTotalCostPrice);


                ReportParameter pTotalSalePrice = new ReportParameter("pTotalSalePrice", total_sale_price);
                viewerDateWise.LocalReport.SetParameters(pTotalSalePrice);


                viewerDateWise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void TaxStatusWiseReport(ReportViewer viewer)
        {
            try
            {
                ProductsList_ds report = new ProductsList_ds();

                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_color.title AS Expr1, pos_subcategory.title AS Expr2, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.tab_pieces, pos_stock_details.pur_price, pos_stock_details.full_pak, 
                                    pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, 
                                    pos_stock_details.alert_status, pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_manufacture as manufacture_date, pos_stock_details.date_of_expiry as expiry_date, pos_products.prod_state, pos_products.unit, pos_products.item_type, 
                                    pos_stock_details.whole_sale_price as size, pos_products.image_path, pos_products.status, pos_products.remarks
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_color ON pos_products.color_id = pos_color.color_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id";

                if (txtTitle.SelectedIndex == 0)
                {
                    GetSetData.query += " where (pos_stock_details.market_value > 0)";
                }
                else
                {
                    GetSetData.query += " where (pos_stock_details.market_value <= 0)";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataSet"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("productList", report.Tables["DataSet"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);

                // **********************************************************************************************************
                ProductsList_ds report_setting = new ProductsList_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

                viewer.LocalReport.Refresh();

                viewer.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(pur_price * quantity) FROM pos_stock_details;";
                string total_cost_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_cost_price == "" || total_cost_price == "NULL")
                {
                    total_cost_price = "0";
                }

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(sale_price * quantity) FROM pos_stock_details;";
                string total_sale_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_sale_price == "" || total_sale_price == "NULL")
                {
                    total_sale_price = "0";
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

                ReportParameter pTotalCostPrice = new ReportParameter("pTotalCostPrice", total_cost_price);
                viewer.LocalReport.SetParameters(pTotalCostPrice);


                ReportParameter pTotalSalePrice = new ReportParameter("pTotalSalePrice", total_sale_price);
                viewer.LocalReport.SetParameters(pTotalSalePrice);



                viewer.RefreshReport();
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
                    if (txtStatus.Text == "Purchase Price")
                    {
                        DisplayReportInReportViewer(this.viewer_over_all, "nill", "nill");
                    }
                    else if (txtStatus.Text == "Sale Price")
                    {
                        DisplayReportInReportViewer(this.viewer_sale_price, "nill", "nill");
                    }
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.Viewer_company_wise, "pos_category.title", txtTitle.Text);
                }
                else if (reportType == 2)
                {
                    DisplayReportInReportViewer(this.viewer_brand_wise, "pos_brand.brand_title", txtTitle.Text);
                }
                else if (reportType == 3)
                {
                    DisplayReportInReportViewerShelfWise(this.viewer_shelf_title, "pos_shelfItems.title", txtTitle.Text);
                }
                else if (reportType == 4)
                {
                    DisplayReportInReportViewerShelfWise(this.viewer_shelf_items, "nill", "nill");
                }
                else if (reportType == 5)
                {
                    DisplayReportInReportViewer(this.viewer_code_list, "nill", "nill");
                }
                else if (reportType == 6)
                {
                    dateWiseInventoryHistoryReport();
                }
                else if (reportType == 7)
                {
                    TaxStatusWiseReport(this.viewerTaxWise);
                }
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_company_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Category Wise Inventory Report";
                reportType = 1;

                pnl_company_wise.Visible = true;

                this.pnl_company_wise.Dock = DockStyle.Fill;
           

                pnl_sale_price.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_code_list.Visible = false;
                pnl_shelf_title.Visible = false;
                pnl_shelf_items.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Category:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_category;", "title", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_brand_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Brand Wise Inventory Report";
                reportType = 2;

                pnl_brand_wise.Visible = true;

                this.pnl_brand_wise.Dock = DockStyle.Fill;
   

                pnl_sale_price.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_code_list.Visible = false;
                pnl_shelf_title.Visible = false;
                pnl_shelf_items.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Brand:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_brand;", "brand_title", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnShelfTitle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Shelf Name Wise Inventory Report";
                reportType = 3;

                pnl_shelf_title.Visible = true;

                this.pnl_shelf_title.Dock = DockStyle.Fill;

                pnl_brand_wise.Visible = false;
                pnl_shelf_items.Visible = false;
                pnl_sale_price.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_code_list.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Shelf No:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_shelfItems;", "title", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnShelfItems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Shelf Item Wise Inventory Report";
                reportType = 4;

                pnl_shelf_items.Visible = true;
                
                this.pnl_shelf_items.Dock = DockStyle.Fill;

                pnl_shelf_title.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_sale_price.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_code_list.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Brand:";
                txtTitle.Text = null;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_select_overall_sales_option()
        {
            try
            {
                lblReportTitle.Text = "Over All Inventory Report";
                reportType = 0;

                pnl_company_wise.Visible = false;
                pnl_code_list.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_shelf_title.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_shelf_items.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;


                lblTitle.Visible = true;
                txtTitle.Visible = false;
                txtStatus.Visible = true;
                lblTitle.Text = "Status:";

                lblDate.Visible = false;
                txtDate.Visible = false;

                if (txtStatus.Text == "Purchase Price")
                {
                    pnl_sale_price.Visible = false;
                    pnl_over_all.Visible = true;

                    this.pnl_over_all.Dock = DockStyle.Fill;
                }
                else if (txtStatus.Text == "Sale Price")
                {
                    pnl_over_all.Visible = false;
                    pnl_sale_price.Visible = true;

                    this.pnl_sale_price.Dock = DockStyle.Fill;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_over_all_CheckedChanged(object sender, EventArgs e)
        {
            fun_select_overall_sales_option();
        }

        private void btn_code_list_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Products Code List Report";
                reportType = 5;

                pnl_code_list.Visible = true;

                this.pnl_code_list.Dock = DockStyle.Fill;

                pnl_shelf_title.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_sale_price.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_shelf_items.Visible = false;
                pnlDateWise.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Brand:";
                txtTitle.Text = null;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnDateWise_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Inventory Report";
                reportType = 6;

                pnlDateWise.Visible = true;

                this.pnlDateWise.Dock = DockStyle.Fill;

                pnl_shelf_title.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_sale_price.Visible = false;
                pnl_company_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_shelf_items.Visible = false;
                pnl_code_list.Visible = false;
                pnlTaxWise.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                txtStatus.Visible = false;
                lblDate.Visible = true;
                txtDate.Visible = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnTaxWise_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Tax Status Wise Inventory Report";
                reportType = 7;

                pnlTaxWise.Visible = true;

                this.pnlTaxWise.Dock = DockStyle.Fill;


                pnl_sale_price.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_over_all.Visible = false;
                pnl_code_list.Visible = false;
                pnl_shelf_title.Visible = false;
                pnl_shelf_items.Visible = false;
                pnlDateWise.Visible = false;
                pnl_company_wise.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                txtStatus.Visible = false;
                lblDate.Visible = false;
                txtDate.Visible = false;

                lblTitle.Text = "Status:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                txtTitle.Items.Insert(0, "Tax");
                txtTitle.Items.Insert(1, "NonTax");
                txtTitle.SelectedIndex = 0;
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

        private void txtTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            fun_select_overall_sales_option();
        }

       
    }
}
