using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Recoveries
{
    public partial class formSupplierPaymentReport : Form
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

        public void TrunOffFormLevelDoubleBuffering()
        {
            //enableFormLevelDoubleBuffering = false;
            //this.MinimizeBox = true;
            //this.WindowState = FormWindowState.Minimized;
        }

        public formSupplierPaymentReport()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_suppliers", "code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("code", "pos_suppliers", "full_name", customer_name_text.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Supplier Payment Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
           

            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "full_name", customer_name_text);
            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "code", customer_code_text);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            customer_name_text.Visible = false;
            customer_code_text.Visible = false;


            pnl_date_wise.Visible = true;
            pnl_name_wise.Visible = false;

            this.pnl_date_wise.Dock = DockStyle.Fill;
                

            this.Viewer_date_wise.Clear();
            this.viewer_name_wise.Clear();
        }

        private void form_recoveries_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supplierPayments_ds.ReportProcedureReportsTitles' table. You can move, or remove it, as needed.
            this.reportProcedureReportsTitlesTableAdapter.Fill(this.supplierPayments_ds.ReportProcedureReportsTitles);
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

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_date_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Supplier Payment Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                pnl_date_wise.Visible = true;

                this.pnl_date_wise.Dock = DockStyle.Fill;
                   

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;

                pnl_name_wise.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_name_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Supplier Payment Report";
                reportType = 1;
                
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                pnl_name_wise.Visible = true;

                this.pnl_name_wise.Dock = DockStyle.Fill;
                   

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;
                customer_name_text.Visible = true;
                customer_code_text.Visible = true;
                
                pnl_date_wise.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void reportParamenters(ReportViewer viewer)
        {
            try
            {
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

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);
            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureDateWisePaymentDetailsTableAdapter.Fill(this.supplierPayments_ds.ReportProcedureDateWisePaymentDetails, FromDate.Text, ToDate.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.supplierPayments_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplaySupplierWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureSupplierWisePaymentDetailsTableAdapter.Fill(this.supplierPayments_ds.ReportProcedureSupplierWisePaymentDetails, FromDate.Text, ToDate.Text, customer_name_text.Text, customer_code_text.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.supplierPayments_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    DisplayDateWiseReportInReportViewer(this.Viewer_date_wise);
                }
                else if (reportType == 1)
                {
                    DisplaySupplierWiseReportInReportViewer(this.viewer_name_wise);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrunOffFormLevelDoubleBuffering();
        }
    }
}
