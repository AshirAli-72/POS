using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Message_box_info.DailyBalancesReports
{
    public partial class formDailyBalanceReport : Form
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

        public formDailyBalanceReport()
        {
            InitializeComponent();
        } 

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.dailyBalance_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
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

                ReportParameter fromDate = new ReportParameter("pFromDate", TextData.fromDate);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", TextData.toDate);
                viewer.LocalReport.SetParameters(toDate);

                ReportParameter currentCash = new ReportParameter("pCurrentCashInHand", TextData.currentCash);
                viewer.LocalReport.SetParameters(currentCash);

                ReportParameter totalCredits = new ReportParameter("pTotalCredits", TextData.totalCredits);
                viewer.LocalReport.SetParameters(totalCredits);

                ReportParameter totalCash = new ReportParameter("pTotalCash", TextData.totalCash);
                viewer.LocalReport.SetParameters(totalCash);

                ReportParameter totalBankBalance = new ReportParameter("pBalanceInBanks", TextData.totalBankBalance);
                viewer.LocalReport.SetParameters(totalBankBalance);

                ReportParameter ownerInvestment = new ReportParameter("pOwnerInvestments", TextData.ownerInvestments);
                viewer.LocalReport.SetParameters(ownerInvestment);

                ReportParameter totalCapital = new ReportParameter("pTotalCapital", TextData.totalCapital);
                viewer.LocalReport.SetParameters(totalCapital);

                ReportParameter investorsInvestment = new ReportParameter("pInvestorsInvestments", TextData.InvestorsInvestment);
                viewer.LocalReport.SetParameters(investorsInvestment);

                ReportParameter InvestorsPayments = new ReportParameter("pInvestorsPayments", TextData.investorsPayments);
                viewer.LocalReport.SetParameters(InvestorsPayments);

                ReportParameter totalPurchasing = new ReportParameter("pTotalPurchasing", TextData.totalPurchasing);
                viewer.LocalReport.SetParameters(totalPurchasing);

                ReportParameter totalRecoveries = new ReportParameter("pTotalRecoveries", TextData.totalRecoveries);
                viewer.LocalReport.SetParameters(totalRecoveries);

                ReportParameter totalSalaryPayments = new ReportParameter("pTotalSalaryPayments", TextData.totalSalaryPayments);
                viewer.LocalReport.SetParameters(totalSalaryPayments);

                ReportParameter totalExpenses = new ReportParameter("pTotalExpenses", TextData.totalExpenses);
                viewer.LocalReport.SetParameters(totalExpenses);

                ReportParameter totalBankLoan = new ReportParameter("pTotalBankLoan", TextData.totalBankLoan);
                viewer.LocalReport.SetParameters(totalBankLoan);

                ReportParameter totalLoanPayments = new ReportParameter("pTotalLoanPayments", TextData.totalLoanPayments);
                viewer.LocalReport.SetParameters(totalLoanPayments);

                ReportParameter totalInstallments = new ReportParameter("pTotalInstallments", TextData.totalInstallments);
                viewer.LocalReport.SetParameters(totalInstallments);

                ReportParameter totalDemands = new ReportParameter("pTotalDemands", TextData.totalDemands);
                viewer.LocalReport.SetParameters(totalDemands);

                ReportParameter totalSales = new ReportParameter("pTotalSales", TextData.totalSales);
                viewer.LocalReport.SetParameters(totalSales);

                ReportParameter totalSupplierPayments = new ReportParameter("pSupplierPayment", TextData.totalSupplierPayments);
                viewer.LocalReport.SetParameters(totalSupplierPayments);

                ReportParameter pCharityPayments = new ReportParameter("pCharityPayments", TextData.totalCharityPayments);
                viewer.LocalReport.SetParameters(pCharityPayments);

                ReportParameter pTotalReceivables = new ReportParameter("pTotalReceivables", TextData.totalReceivables);
                viewer.LocalReport.SetParameters(pTotalReceivables);

                ReportParameter pTotalPayables = new ReportParameter("pTotalPayables", TextData.totalPayables);
                viewer.LocalReport.SetParameters(pTotalPayables);

                ReportParameter pInvestorsProfit = new ReportParameter("pInvestorsProfit", TextData.totalInvestorProfit);
                viewer.LocalReport.SetParameters(pInvestorsProfit);

                ReportParameter pCompanyProfit = new ReportParameter("pCompanyProfit", TextData.companyProfit);
                viewer.LocalReport.SetParameters(pCompanyProfit);

                ReportParameter pInstallmentProfit = new ReportParameter("pInstallmentProfit", TextData.installmentProfit);
                viewer.LocalReport.SetParameters(pInstallmentProfit);

                ReportParameter pTotalCharity = new ReportParameter("pTotalCharity", TextData.totalCharityAmount);
                viewer.LocalReport.SetParameters(pTotalCharity);

                ReportParameter pCurrentCharity = new ReportParameter("pCurrentCharity", TextData.currentCharity);
                viewer.LocalReport.SetParameters(pCurrentCharity);

                ReportParameter pTotalSalaries = new ReportParameter("pTotalSalaries", TextData.totalSalaries);
                viewer.LocalReport.SetParameters(pTotalSalaries);

                ReportParameter pCurrentInvestorsProfit = new ReportParameter("pCurrentInvestorsProfit", TextData.currentInvestorProfit);
                viewer.LocalReport.SetParameters(pCurrentInvestorsProfit);

                ReportParameter pTotalLoanGiven = new ReportParameter("pTotalLoanGiven", TextData.totalLoanGiven);
                viewer.LocalReport.SetParameters(pTotalLoanGiven);

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formDailyBalanceReport_Load(object sender, EventArgs e)
        {
            DisplayReportInReportViewer(this.reportViewer1);
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
