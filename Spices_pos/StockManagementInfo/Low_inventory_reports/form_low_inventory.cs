using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.StockManagementInfo.Low_inventory_reports;

namespace Stock_management.Low_inventory_reports
{
    public partial class form_low_inventory : Form
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
                    handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
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

        public form_low_inventory()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void Low_inventory()
        {
            try
            {
                stocks_ds report = new stocks_ds();
                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_stock_details.quantity, pos_stock_details.full_pak, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.pkg, 
                                            pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, pos_stock_details.alert_status, 
                                            pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_products.manufacture_date, pos_products.prod_state, pos_products.unit, pos_products.size, pos_products.item_type, pos_products.image_path, pos_products.status
                                            FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                            pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                            where (pos_stock_details.quantity <= pos_stock_details.qty_alert) and (pos_category.title != 'MISCELLANEOUS');";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, data.conn_);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", report.Tables[0]);
                this.viewer_low_inventory.LocalReport.DataSources.Clear();
                this.viewer_low_inventory.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_low_inventory.LocalReport.DataSources.Add(rds);
                this.viewer_low_inventory.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_low_inventory);
                this.viewer_low_inventory.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Expired_items()
        {
            try
            {
                //FromDate.Text = DateTime.Now.ToLongDateString();
                stocks_ds report = new stocks_ds();
                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_manufacture as manufacture_date, pos_stock_details.date_of_expiry as expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_stock_details.whole_sale_price as size, pos_products.image_path, pos_products.status, pos_expired_items.date, pos_expired_items.quantity, pos_expired_items.pkg, 
                                    pos_expired_items.full_pak, pos_expired_items.pur_price, pos_expired_items.sale_price, pos_expired_items.total_pur_price, pos_expired_items.total_sale_price
                                    FROM  pos_expired_items INNER JOIN pos_products ON pos_products.product_id = pos_expired_items.prod_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id INNER JOIN pos_brand ON pos_brand.brand_id = pos_products.brand_id 
                                    INNER JOIN pos_stock_details ON pos_stock_details.stock_id = pos_expired_items.stock_id
                                    where (pos_expired_items.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title != 'MISCELLANEOUS') order by pos_expired_items.date asc;";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, data.conn_);
                da.Fill(report, report.Tables["expired_items"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", report.Tables["expired_items"]);
                this.viewer_expiry.LocalReport.DataSources.Clear();
                this.viewer_expiry.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_expiry.LocalReport.DataSources.Add(rds);
                this.viewer_expiry.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_expiry);
                this.viewer_expiry.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Before_Expired()
        {
            try
            {
                //FromDate.Text = DateTime.Now.ToLongDateString();
                stocks_ds report = new stocks_ds();

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_manufacture as manufacture_date, pos_stock_details.date_of_expiry as expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_stock_details.whole_sale_price as size, pos_products.status, pos_products.remarks, pos_stock_details.quantity, pos_stock_details.pkg, pos_stock_details.tab_pieces, pos_stock_details.full_pak, 
                                    pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_stock_details.date_of_expiry between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title != 'MISCELLANEOUS') order by pos_stock_details.date_of_expiry asc;";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, data.conn_);
                da.Fill(report, report.Tables["beforeExpiry"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", report.Tables["beforeExpiry"]);
                this.ViewerBeforeExpiry.LocalReport.DataSources.Clear();
                this.ViewerBeforeExpiry.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.ViewerBeforeExpiry.LocalReport.DataSources.Add(rds);
                this.ViewerBeforeExpiry.LocalReport.Refresh();
                DisplayReportInReportViewer(this.ViewerBeforeExpiry);
                this.ViewerBeforeExpiry.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
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

            // Retrive Report Settings from db *******************************************************************************************
            GetSetData.query = @"SELECT title FROM pos_report_settings";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

            ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
            viewer.LocalReport.SetParameters(title);

            GetSetData.query = @"SELECT address FROM pos_report_settings";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

            ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
            viewer.LocalReport.SetParameters(address);

            GetSetData.query = @"SELECT phone_no FROM pos_report_settings";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

            ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
            viewer.LocalReport.SetParameters(phone);

            GetSetData.query = @"SELECT note FROM pos_report_settings";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

            ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
            viewer.LocalReport.SetParameters(note);

            GetSetData.query = @"SELECT copyrights FROM pos_report_settings";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

            ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
            viewer.LocalReport.SetParameters(copyrights);
        }
        
        private void refresh()
        {
            lblReportTitle.Text = "Low Inventory Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            btn_low_inventory.Checked = true;
            btn_expiry.Checked = false;
            btnBefore_expiry.Checked = false;

            viewer_low_inventory.Visible = true;
            viewer_expiry.Visible = false;
            ViewerBeforeExpiry.Visible = false;

            this.viewer_low_inventory.Clear();

            this.viewer_low_inventory.Dock = DockStyle.Fill;
            Low_inventory();

        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_low_inventory_Load(object sender, EventArgs e)
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

        private void btn_low_inventory_CheckedChanged(object sender, EventArgs e)
        {
            lblReportTitle.Text = "Low Inventory Report";
            reportType = 0;

            viewer_low_inventory.Visible = true;

            this.viewer_low_inventory.Dock = DockStyle.Fill; 
            
            viewer_expiry.Visible = false;
            ViewerBeforeExpiry.Visible = false;
        }

        private void btn_expiry_CheckedChanged(object sender, EventArgs e)
        {
            lblReportTitle.Text = "Expired Inventory Report";
            reportType = 1;

            viewer_expiry.Visible = true;
            
            this.viewer_expiry.Dock = DockStyle.Fill;

            viewer_low_inventory.Visible = false;
            ViewerBeforeExpiry.Visible = false;
        }

        private void btnBefore_expiry_CheckedChanged(object sender, EventArgs e)
        {
            lblReportTitle.Text = "Before Expiry Report";
            reportType = 2;

            ViewerBeforeExpiry.Visible = true;

            this.ViewerBeforeExpiry.Dock = DockStyle.Fill;

            viewer_low_inventory.Visible = false;
            viewer_expiry.Visible = false;
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            if (reportType == 0)
            {
                Low_inventory();
            }
            else if (reportType == 1)
            {
                Expired_items();
            }
            else if (reportType == 2)
            {
                Before_Expired();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }
    }
}
