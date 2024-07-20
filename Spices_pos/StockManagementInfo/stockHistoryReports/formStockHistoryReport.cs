using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Stock_management.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.StockManagementInfo.stockHistoryReports;

namespace CounterSales_info.CustomerSalesReport
{
    public partial class formStockHistoryReport : Form
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

        public formStockHistoryReport()
        {
            InitializeComponent();
        }


        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void Closebutton_Click(object sender, EventArgs e)
        {
            form_inventory_history _obj = new form_inventory_history();
            _obj.Show();
            this.Dispose();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string query)
        {
            try
            {
                stockHistory_ds report = new stockHistory_ds();


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
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

        private void formStockHistoryReport_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                resetForm();
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
                if (reportType == 0)
                {

                    GetSetData.query = @"SELECT 
                                        dbo.pos_products.product_id,
                                        LatestStockHistory.id, 
                                        dbo.pos_products.prod_name, 
                                        dbo.pos_stock_details.item_barcode, 
                                        LatestStockHistory.date, 
                                        LatestStockHistory.old_quantity, 
                                        LatestStockHistory.new_quantity, 
                                        LatestStockHistory.old_cost_price, 
                                        LatestStockHistory.new_cost_price, 
                                        LatestStockHistory.old_sale_price, 
                                        LatestStockHistory.new_sale_price, 
                                        LatestStockHistory.details, 
                                        dbo.pos_employees.full_name
                                    FROM 
                                        dbo.pos_products 
                                    INNER JOIN 
                                        dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id 
                                    INNER JOIN 
                                        (
                                            SELECT 
                                                product_id, 
                                                MAX(id) AS max_id
                                            FROM 
                                                dbo.pos_stock_history
                                            GROUP BY 
                                                product_id
                                        ) AS LatestID ON dbo.pos_stock_details.prod_id = LatestID.product_id 
                                    INNER JOIN 
                                        dbo.pos_stock_history AS LatestStockHistory ON LatestID.max_id = LatestStockHistory.id 
                                    INNER JOIN 
                                        dbo.pos_users ON LatestStockHistory.user_id = dbo.pos_users.user_id 
                                    INNER JOIN 
                                        dbo.pos_employees ON dbo.pos_users.emp_id = dbo.pos_employees.employee_id
                                    WHERE 
                                        LatestStockHistory.date = '" + FromDate.Text + "';";

                    DisplayReportInReportViewer(this.viewerDateWise, GetSetData.query);
                }
                else
                {

                    GetSetData.query = @"SELECT dbo.pos_stock_history.id, dbo.pos_products.prod_name, dbo.pos_stock_details.item_barcode, dbo.pos_stock_history.date, dbo.pos_stock_history.old_quantity, 
                                    dbo.pos_stock_history.new_quantity, dbo.pos_stock_history.old_cost_price, dbo.pos_stock_history.new_cost_price, 
                                    dbo.pos_stock_history.old_sale_price, dbo.pos_stock_history.new_sale_price, dbo.pos_stock_history.details, dbo.pos_employees.full_name
                                    FROM dbo.pos_stock_history INNER JOIN dbo.pos_products ON dbo.pos_products.product_id = dbo.pos_stock_history.product_id INNER JOIN
                                    dbo.pos_stock_details ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
                                    dbo.pos_users ON dbo.pos_users.user_id = dbo.pos_stock_history.user_id INNER JOIN
                                    dbo.pos_employees ON dbo.pos_employees.employee_id = dbo.pos_users.emp_id
                                    WHERE (pos_stock_history.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.prod_name = '" + txtTitle.Text + "') order by pos_stock_history.id";

                    DisplayReportInReportViewer(this.viewerProductWise, GetSetData.query);
                }
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void formStockHistoryReport_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.P)
            //{
            //    viewerDateWise.PrintDialog();
            //}
            //else if (e.KeyCode == Keys.Space)
            //{
            //    viewerDateWise.PrintDialog();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    viewerDateWise.PrintDialog();
            //}

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

       private void resetForm()
        {
            try
            {
                lblReportTitle.Text = "Date Wise Inventory History";
                reportType = 0;

                txtTitle.Text = null;

                this.viewerDateWise.Dock = DockStyle.Fill;

                viewerDateWise.Visible = true;
                viewerProductWise.Visible = false;

                lblFromDate.Text = "Date:";
                lblToDate.Visible = false;
                ToDate.Visible = false;
                lblProductName.Visible = false;
                txtTitle.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btn_bill_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Product Wise Inventory History";
                reportType = 1;

                txtTitle.Text = null;

                this.viewerProductWise.Dock = DockStyle.Fill;

                viewerDateWise.Visible = false;
                viewerProductWise.Visible = true;

                lblFromDate.Text = "From Date:";
                lblToDate.Visible = true;
                ToDate.Visible = true;
                lblProductName.Visible = true;
                txtTitle.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select prod_name from pos_products;", "prod_name", txtTitle);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
