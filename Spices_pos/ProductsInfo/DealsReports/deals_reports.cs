using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Products_info.forms.RecipeDetails;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.DealsReports;

namespace CounterSales_info.CustomerSalesReport
{
    public partial class deals_reports : Form
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

        public deals_reports()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();


        private void Closebutton_Click(object sender, EventArgs e)
        {
            deal_details _obj = new deal_details();
            _obj.Show();
            this.Dispose();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                deals_ds report = new deals_ds();

                GetSetData.query = @"SELECT pos_deals.deal_title, pos_deals.status, pos_products.prod_name, pos_stock_details.item_barcode as barcode, 
                                    pos_stock_details.whole_sale_price as size, pos_products.item_type, 
                                    pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value,  pos_deal_items.quantity
                                    FROM pos_stock_details INNER JOIN
                                    pos_products ON pos_stock_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_deal_items ON pos_products.product_id = pos_deal_items.prod_id INNER JOIN
                                    pos_deals ON pos_deal_items.deal_id = pos_deals.deal_id
                                    WHERE (pos_deals.deal_title = '" + txtTitle.Text +"') order by pos_deal_items.deal_id";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables["DataTable1"]);
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

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void deals_reports_Load(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select deal_title from pos_deals;", "deal_title", txtTitle);
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }


        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayReportInReportViewer(this.reportViewer1);
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void deals_reports_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Space)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                reportViewer1.PrintDialog();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
