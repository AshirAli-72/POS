using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.inventoryAuditReports;
using Spices_pos.ProductsInfo.reports;

namespace CounterSales_info.CustomerSalesReport
{
    public partial class formInventoryAuditReport : Form
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

        public formInventoryAuditReport()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public static int user_id = 0;

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                audit_ds report = new audit_ds();

                GetSetData.query = @"select date, prod_name, barcode, quantity, old_quantity, 
                                    pur_price, old_cost_price, sale_price, old_sale_price, tax, old_tax, 
                                    reason from pos_inventory_audit 
                                    where (date between '" + FromDate.Text +"' and '" + ToDate.Text +"') order by id asc";


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

                GetSetData.query = @"SELECT sum(pur_price * quantity) FROM pos_inventory_audit;";
                string total_cost_price = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_cost_price == "" || total_cost_price == "NULL")
                {
                    total_cost_price = "0";
                }

                //*******************************************************************************************

                GetSetData.query = @"SELECT sum(sale_price * quantity) FROM pos_inventory_audit;";
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

                ReportParameter pFromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(pFromDate);


                ReportParameter pToDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(pToDate);


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

        private void formInventoryAuditReport_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
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

        private void formInventoryAuditReport_KeyDown(object sender, KeyEventArgs e)
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
