using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Reports_info.Customer_sales_reports.loyal_customer_sales_reports;
using System.Data.SqlClient;

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
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                this.viewerCheques.Clear();

                this.viewerCheques.Dock = DockStyle.Fill;
                    
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
                chequeDetails_ds report = new chequeDetails_ds();
                GetSetData.query = @"SELECT pos_bank.bank_title, pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
                                    pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
                                    FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id
                                    where (pos_customerChequeDetails.date between  '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customerChequeDetails.status != 'Complete')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new ReportDataSource("low_inventory", report.Tables[0]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(DisplayMode.PrintLayout);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
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


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private void DisplayBouncedChequesReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.reportProcedureBouncedChequeNotificationsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBouncedChequeNotifications, FromDate.Text, ToDate.Text);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        viewer.ZoomMode = ZoomMode.Percent;
        //        viewer.ZoomPercent = 100;
        //        reportParamenters(viewer);
        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private void DisplayCustomerWiseChequesReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.ReportProcedureCustomerWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureCustomerWiseChequeDetails, FromDate.Text, ToDate.Text,txtTitle.Text, txtCode.Text);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        viewer.ZoomMode = ZoomMode.Percent;
        //        viewer.ZoomPercent = 100;
        //        reportParamenters(viewer);
        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private void DisplayBillWiseChequesReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.ReportProcedureBillWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBillWiseChequeDetails, txtTitle.Text);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        viewer.ZoomMode = ZoomMode.Percent;
        //        viewer.ZoomPercent = 100;
        //        reportParamenters(viewer);
        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private void DisplayBankWiseChequesReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.ReportProcedureBankWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureBankWiseChequeDetails, FromDate.Text, ToDate.Text, txtTitle.Text);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        viewer.ZoomMode = ZoomMode.Percent;
        //        viewer.ZoomPercent = 100;
        //        reportParamenters(viewer);
        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private void DisplayStatusWiseChequesReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.ReportProcedureStatusWiseChequeDetailsTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureStatusWiseChequeDetails, FromDate.Text, ToDate.Text, txtTitle.Text);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.chequeDetails_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        viewer.ZoomMode = ZoomMode.Percent;
        //        viewer.ZoomPercent = 100;
        //        reportParamenters(viewer);
        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private void view_button_Click(object sender, EventArgs e)
        {
            //if (reportType == 0)
            //{
                DisplayChequeWiseReportInReportViewer(this.viewerCheques);
            //}
            //else if (reportType == 1)
            //{
            //    DisplayBouncedChequesReportInReportViewer(this.viewerBounced);
            //}
            //else if (reportType == 2)
            //{
            //    DisplayCustomerWiseChequesReportInReportViewer(this.viewerCustomer);
            //}
            //else if (reportType == 3)
            //{
            //    DisplayBillWiseChequesReportInReportViewer(this.viewerInvoice);
            //}
            //else if (reportType == 4)
            //{
            //    DisplayBankWiseChequesReportInReportViewer(this.viewerBank);
            //}
            //else if (reportType == 5)
            //{
            //    DisplayStatusWiseChequesReportInReportViewer(this.viewerStatus);
            //}
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
    }
}
