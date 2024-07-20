using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Reports_info.Stock;

namespace Stock_management.whole_stock_reports
{
    public partial class form_whole_stock : Form
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

        public form_whole_stock()
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
            this.Close();
        }        

        private void refresh()
        {
            lblReportTitle.Text = "Overall Inventory Report";
            reportType = 0;

            txtTitle.Visible = false;
            lblTitle.Visible = false;
            

            pnl_whole_inventory.Visible = true;
            pnl_low_inventory.Visible = false;
            pnl_company.Visible = false;
            pnl_brand_wise.Visible = false;
            pnl_summary.Visible = false;

            this.pnl_whole_inventory.Dock = DockStyle.Fill;
           

            this.viewer_whole_inventory.Clear();
            this.viewer_low_inventory.Clear();
            this.viewer_company_wise.Clear();
            this.viewer_brand_wise.Clear();
            this.viewer_summary.Clear();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string value)
        {
            try
            {
                stock_ds report = new stock_ds();
                GetSetData.query = @"SELECT pos_brand.brand_title, pos_category.title, pos_subcategory.title AS Expr1, pos_stock_details.quantity, pos_stock_details.full_pak, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.pkg, 
                                    pos_stock_details.market_value, pos_stock_details.trade_off, pos_stock_details.carry_exp, pos_stock_details.total_pur_price, pos_stock_details.total_sale_price, pos_stock_details.qty_alert, pos_stock_details.alert_status, 
                                    pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_stock_details.date_of_expiry as expiry_date, pos_stock_details.date_of_manufacture as manufacture_date, pos_products.prod_state, pos_products.unit, pos_stock_details.whole_sale_price as size, pos_products.item_type, pos_products.image_path, pos_products.status
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id";

               switch(value)
               {
                   case "WholeInventory":
                       GetSetData.query += " where pos_stock_details.quantity > 0;";
                       break;

                   case "LowInventory":
                       GetSetData.query += " where (pos_stock_details.quantity >= 0) and (pos_stock_details.quantity <= pos_stock_details.qty_alert);";
                       break;

                   case "CategoryWise":
                       //GetSetData.query += " where (pos_category.title = '" + txtTitle.Text + "') and (pos_stock_details.quantity > 0);";
                       GetSetData.query += " where (pos_category.title = '" + txtTitle.Text + "') and (pos_stock_details.quantity >= 0) and (pos_stock_details.quantity <= pos_stock_details.qty_alert);";
                       break;

                   case "BrandWise":
                       //GetSetData.query += " where (pos_brand.brand_title = '" + txtTitle.Text + "') and (pos_stock_details.quantity > 0);";
                       GetSetData.query += " where (pos_brand.brand_title = '" + txtTitle.Text + "') and (pos_stock_details.quantity >= 0) and (pos_stock_details.quantity <= pos_stock_details.qty_alert);";
                       break;
               }

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("whole_inventory", report.Tables[0]);
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
                // *************************************************************************************

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_whole_stock_Load(object sender, EventArgs e)
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_company_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Category Wise Inventory Report";
                reportType = 2;

                pnl_company.Visible = true;

               
                    this.pnl_company.Dock = DockStyle.Fill;
                  

                pnl_low_inventory.Visible = false;
                pnl_whole_inventory.Visible = false;
                pnl_summary.Visible = false;
                pnl_brand_wise.Visible = false;

                txtTitle.Visible = true;
                lblTitle.Visible = true;
                lblTitle.Text = "Category:";
                txtTitle.Text = null;
                this.txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_category;", "title", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_brand_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Brand Wise Inventory Report";
                reportType = 3;

                pnl_brand_wise.Visible = true;
             
                    this.pnl_brand_wise.Dock = DockStyle.Fill;

                pnl_low_inventory.Visible = false;
                pnl_whole_inventory.Visible = false;
                pnl_summary.Visible = false;
                pnl_company.Visible = false;

                txtTitle.Visible = true;
                lblTitle.Visible = true;
                lblTitle.Text = "Brand:";
                txtTitle.Text = null;
                this.txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_brand;", "brand_title", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_whole_inventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Inventory Report";
                reportType = 0;

                pnl_whole_inventory.Visible = true;

                    this.pnl_whole_inventory.Dock = DockStyle.Fill;
                   

                pnl_low_inventory.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_summary.Visible = false;
                pnl_company.Visible = false;

                txtTitle.Visible = false;
                lblTitle.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_low_inventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Low Inventory Report";
                reportType = 1;

                pnl_low_inventory.Visible = true;

                    this.pnl_low_inventory.Dock = DockStyle.Fill;

                pnl_whole_inventory.Visible = false;
                pnl_brand_wise.Visible = false;
                pnl_summary.Visible = false;
                pnl_company.Visible = false;

                txtTitle.Visible = false;
                lblTitle.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_summary_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //FromDate.Text = DateTime.Now.ToLongDateString();
            //    //ToDate.Text = DateTime.Now.ToLongDateString();
            //    pnl_summary.Visible = true;
            //    viewer_summary.Visible = true;

            //    if (btn_summary.Checked == true)
            //    {
            //        this.pnl_summary.Dock = DockStyle.Fill;
            //        this.viewer_summary.Dock = DockStyle.Fill;
            //    }

            //    pnl_whole_inventory.Visible = false;
            //    pnl_brand_wise.Visible = false;
            //    pnl_whole_inventory.Visible = false;
            //    pnl_company.Visible = false;

            //    viewer_brand_wise.Visible = false;
            //    viewer_whole_inventory.Visible = false;
            //    viewer_company_wise.Visible = false;
            //    viewer_low_inventory.Visible = false;

            //    txt_company.Visible = false;
            //    lbl_company.Visible = false;

            //    txt_brand.Visible = false;
            //    lbl_brand.Visible = false;
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    DisplayReportInReportViewer(this.viewer_whole_inventory, "WholeInventory");
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.viewer_low_inventory, "LowInventory");
                }
                else if (reportType == 2)
                {
                    DisplayReportInReportViewer(this.viewer_company_wise, "CategoryWise");
                }
                else if (reportType == 3)
                {
                    DisplayReportInReportViewer(this.viewer_brand_wise, "BrandWise");
                }
            } 
            catch(Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
