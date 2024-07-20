using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Banks_Loan_info.BankLoanReports
{
    public partial class formBankLoanReports : Form
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

        public formBankLoanReports()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman


        private void FillComboBoxCustomerName()
        {
            txtEmployeeName.Text = data.UserPermissions("bank_name", "pos_bankLoansDetails", "code", txtEmployeeCode.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            txtEmployeeCode.Text = data.UserPermissions("code", "pos_bankLoansDetails", "bank_name", txtEmployeeName.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Bank Loan Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            txtEmployeeName.Text = null;
            txtEmployeeCode.Text = null;

            txtEmployeeName.Items.Clear();
            txtEmployeeCode.Items.Clear();

            GetSetData.FillComboBoxWithValues("select * from pos_bankLoansDetails;", "bank_name", txtEmployeeName);
            GetSetData.FillComboBoxWithValues("select * from pos_bankLoansDetails;", "code", txtEmployeeCode);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            txtEmployeeName.Visible = false;
            txtEmployeeCode.Visible = false;

          
            pnlDateWise.Visible = true;
            pnlEmployeeWise.Visible = false;

            this.pnlDateWise.Dock = DockStyle.Fill;
               

            this.viewerDateWise.Clear();
            this.viewerEmployeeWise.Clear();
        }

        private void formBankLoanReports_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //date_wise_sales();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomeCodes();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomerName();
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

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Bank Loan Report";
                reportType = 0;

                txtEmployeeCode.Text = null;
                txtEmployeeName.Text = null;

                this.pnlDateWise.Dock = DockStyle.Fill;
                   

                pnlEmployeeWise.Visible = false;
                pnlDateWise.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;

                txtEmployeeName.Visible = false;
                txtEmployeeCode.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_bill_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Bank Wise Loan Report";
                reportType = 1;

                txtEmployeeCode.Text = null;
                txtEmployeeName.Text = null;

                pnlEmployeeWise.Visible = true;

                this.pnlEmployeeWise.Dock = DockStyle.Fill;
                    

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnlDateWise.Visible = false;
                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;

                txtEmployeeName.Visible = true;
                txtEmployeeCode.Visible = true;
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
            // *******************************************************************************************

            ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
            viewer.LocalReport.SetParameters(fromDate);

            ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
            viewer.LocalReport.SetParameters(toDate);
        }

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureDateWiseLoanDetailsTableAdapter.Fill(this.bankLoans_ds.ReportProcedureDateWiseLoanDetails, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.bankLoans_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                DisplayReportInReportViewer(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayStatusWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureBankWiseLoanDetailsTableAdapter.Fill(this.bankLoans_ds.ReportProcedureBankWiseLoanDetails, FromDate.Text, ToDate.Text, txtEmployeeName.Text, txtEmployeeCode.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.bankLoans_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                DisplayReportInReportViewer(viewer);
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
                    DisplayDateWiseReportInReportViewer(this.viewerDateWise);
                }
                else if (reportType == 1)
                {
                    DisplayStatusWiseReportInReportViewer(this.viewerEmployeeWise);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }
    }
}
