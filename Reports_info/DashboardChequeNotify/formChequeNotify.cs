using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.DashboardChequeNotify
{
    public partial class formChequeNotify : Form
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

        public formChequeNotify()
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
            this.Close();
        }

        private void formChequeNotify_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            try
            {
                lblReportTitle.Text = "Cheque Wise Report";
                reportType = 0;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                viewerCheques.Visible = true;
                viewerBounced.Visible = false;
                viewerCustomer.Visible = false;
                viewerInvoice.Visible = false;
                viewerBank.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = false;
                lblCode.Visible = false;
                txtTitle.Visible = false;
                txtCode.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                this.viewerCheques.Clear();
                this.viewerBounced.Clear();
                this.viewerCustomer.Clear();
                this.viewerInvoice.Clear();
                this.viewerBank.Clear();
                this.viewerStatus.Clear();

                this.viewerCheques.Dock = DockStyle.Fill;
                    
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_low_inventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Cheque Wise Report";
                reportType = 0;

                viewerCheques.Visible = true;

                this.viewerCheques.Dock = DockStyle.Fill;
                
                viewerBounced.Visible = false;
                viewerCustomer.Visible = false;
                viewerInvoice.Visible = false;
                viewerBank.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = false;
                lblCode.Visible = false;
                txtTitle.Visible = false;
                txtCode.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_expiry_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Bounced Cheques Report";
                reportType = 1;

                viewerBounced.Visible = true;

                this.viewerBounced.Dock = DockStyle.Fill;
                

                viewerCheques.Visible = false;
                viewerCustomer.Visible = false;
                viewerInvoice.Visible = false;
                viewerBank.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = false;
                lblCode.Visible = false;
                txtTitle.Visible = false;
                txtCode.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnCustomer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Customer Wise Cheque Report";
                reportType = 2;

                viewerCustomer.Visible = true;

                this.viewerCustomer.Dock = DockStyle.Fill;
                

                viewerCheques.Visible = false;
                viewerBounced.Visible = false;
                viewerInvoice.Visible = false;
                viewerBank.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = true;
                lblCode.Visible = true;
                txtTitle.Visible = true;
                txtCode.Visible = true;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblTitle.Text = "Name:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                txtCode.Text = null;
                txtCode.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", txtTitle);
                GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", txtCode);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnInvoice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Invoice No Wise Cheque Report";
                reportType = 3;

                viewerInvoice.Visible = true;

                this.viewerInvoice.Dock = DockStyle.Fill;
                

                viewerCheques.Visible = false;
                viewerBounced.Visible = false;
                viewerCustomer.Visible = false;
                viewerBank.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = true;
                lblCode.Visible = false;
                txtTitle.Visible = true;
                txtCode.Visible = false;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                lblTitle.Text = "Invoice #";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select distinct(billNo) from pos_customerChequeDetails;", "billNo", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnBank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Bank Wise Cheque Report";
                reportType = 4;

                viewerBank.Visible = true;

                this.viewerBank.Dock = DockStyle.Fill;
               

                viewerCheques.Visible = false;
                viewerBounced.Visible = false;
                viewerCustomer.Visible = false;
                viewerInvoice.Visible = false;
                viewerStatus.Visible = false;

                lblTitle.Visible = true;
                lblCode.Visible = false;
                txtTitle.Visible = true;
                txtCode.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblTitle.Text = "Bank:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_bank;", "bank_title", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnStatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Status Wise Cheque Report";
                reportType = 5;

                viewerStatus.Visible = true;

                this.viewerStatus.Dock = DockStyle.Fill;
                

                viewerCheques.Visible = false;
                viewerBounced.Visible = false;
                viewerCustomer.Visible = false;
                viewerInvoice.Visible = false;
                viewerBank.Visible = false;

                lblTitle.Visible = true;
                lblCode.Visible = false;
                txtTitle.Visible = true;
                txtCode.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                lblTitle.Text = "Status:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select distinct(status) from pos_customerChequeDetails;", "status", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
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
            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void DisplayChequeWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureChequeNotificationsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureChequeNotifications, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayBouncedChequesReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureBouncedChequeNotificationsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBouncedChequeNotifications, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayCustomerWiseChequesReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCustomerWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureCustomerWiseChequeDetails, FromDate.Text, ToDate.Text,txtTitle.Text, txtCode.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayBillWiseChequesReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureBillWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBillWiseChequeDetails, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayBankWiseChequesReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureBankWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBankWiseChequeDetails, FromDate.Text, ToDate.Text, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayStatusWiseChequesReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureStatusWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureStatusWiseChequeDetails, FromDate.Text, ToDate.Text, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
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
            if (reportType == 0)
            {
                DisplayChequeWiseReportInReportViewer(this.viewerCheques);
            }
            else if (reportType == 1)
            {
                DisplayBouncedChequesReportInReportViewer(this.viewerBounced);
            }
            else if (reportType == 2)
            {
                DisplayCustomerWiseChequesReportInReportViewer(this.viewerCustomer);
            }
            else if (reportType == 3)
            {
                DisplayBillWiseChequesReportInReportViewer(this.viewerInvoice);
            }
            else if (reportType == 4)
            {
                DisplayBankWiseChequesReportInReportViewer(this.viewerBank);
            }
            else if (reportType == 5)
            {
                DisplayStatusWiseChequesReportInReportViewer(this.viewerStatus);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void FillComboBoxCustomerName()
        {
            txtTitle.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", txtCode.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            txtCode.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", txtTitle.Text);
        }

        private void txtTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void txtCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnCustomer.Checked == true)
                {
                    Customer_details.selected_customer = txtTitle.Text;

                    Customer_details customer_detail = new Customer_details();
                    customer_detail.ShowDialog();

                    txtTitle.Text = Customer_details.selected_customer;
                    txtCode.Text = Customer_details.selected_customerCode;
                }
            }
        }
    }
}
