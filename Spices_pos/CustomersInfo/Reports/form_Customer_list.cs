using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.CustomersInfo.Reports;

namespace Customers_info.Reports
{
    public partial class form_Customer_list : Form
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

        public form_Customer_list()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void refresh()
        {
            lblReportTitle.Text = "Overall Customers Report";
            reportType = 3;

            lblTitle.Visible = false;
            txtTitle.Visible = false;

            pnl_over_all.Visible = true;
            pnl_area.Visible = false;
            pnl_supplier_wise.Visible = false;
            pnl_status.Visible = false;

            this.pnl_over_all.Dock = DockStyle.Fill;

            this.viewer_area.Clear();
            this.viewer_over_all.Clear();
            this.viewer_supplier_wise.Clear();
            this.viewer_status.Clear();
        }

        private void form_Customer_list_Load(object sender, EventArgs e)
        {
            refresh();
            //DisplayReportInReportViewer(this.viewer_over_all, "nill", "nill");
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_area_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Country Wise Customers Report";
                reportType = 0;

                pnl_area.Visible = true;

                this.pnl_area.Dock = DockStyle.Fill;

                pnl_area.Visible = true;
                pnl_over_all.Visible = false;
                pnl_supplier_wise.Visible = false;
                pnl_status.Visible = false;

                txtTitle.Visible = true;
                lblTitle.Visible = true;
                lblTitle.Text = "Country:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_country;", "title", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_over_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Customers Report";
                reportType = 3;

                txtTitle.Text = null;
                pnl_over_all.Visible = true;

                this.pnl_over_all.Dock = DockStyle.Fill;


                pnl_area.Visible = false;
                pnl_supplier_wise.Visible = false;
                pnl_status.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_supplier_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Province Wise Customers Report";
                reportType = 1;

                pnl_supplier_wise.Visible = true;

                this.pnl_supplier_wise.Dock = DockStyle.Fill;
                   

                pnl_area.Visible = false;
                pnl_over_all.Visible = false;
                pnl_status.Visible = false;

                lblTitle.Visible = true;
                lblTitle.Text = "Province:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_city;", "title", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_status_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Country Wise Customers Report";
                reportType = 2;

                pnl_status.Visible = true;

                this.pnl_status.Dock = DockStyle.Fill;
                  

                pnl_area.Visible = false;
                pnl_over_all.Visible = false;
                pnl_supplier_wise.Visible = false;

                lblTitle.Text = "Status:";
                lblTitle.Visible = true;
                txtTitle.Visible = true;

                txtTitle.Text = "Active";
                txtTitle.Items.Clear();
                txtTitle.Items.Add("Active");
                txtTitle.Items.Add("Deactive");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition, string value)
        {
            try
            {
                Customer_list_ds report = new Customer_list_ds();
                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_customers.date, pos_customers.full_name, pos_customers.cus_code, pos_customers.post_code, pos_customers.zip_code, 
                                    pos_customers.cnic, pos_customers.house_no, pos_customers.telephone_no, pos_customers.mobile_no, pos_customers.address1, pos_customers.address2, pos_customers.email, pos_customers.bank_name, 
                                    pos_customers.account_no, pos_customers.image_path, pos_customers.discount, pos_customers.credit_limit, pos_customers.opening_balance, pos_customers.status, pos_city.title, pos_country.title AS Expr1, pos_customers.fatherName
                                    FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN
                                    pos_city ON pos_customers.city_id = pos_city.city_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id";

                if (condition != "nill" && value != "nill")
                {
                    GetSetData.query += " where " + condition + " = '" + value + "'";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Customer_List"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", report.Tables["Customer_List"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);

                // **********************************************************************************************************
                Customer_list_ds report_setting = new Customer_list_ds();

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

        private void view_button_Click(object sender, EventArgs e)
        {
            if (reportType == 0)
            {
                DisplayReportInReportViewer(this.viewer_area, "pos_country.title", txtTitle.Text);
            }
            else if (reportType == 1)
            {
                DisplayReportInReportViewer(this.viewer_supplier_wise, "pos_city.title", txtTitle.Text);
            }
            else if (reportType == 2)
            {
                DisplayReportInReportViewer(this.viewer_status, "pos_customers.status", txtTitle.Text);
            }
            else if (reportType == 3)
            {
                DisplayReportInReportViewer(this.viewer_over_all, "nill", "nill");
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
