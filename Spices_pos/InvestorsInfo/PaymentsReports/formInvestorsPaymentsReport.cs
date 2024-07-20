using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Investors_info.PaymentsReports
{
    public partial class formInvestorsPaymentsReport : Form
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
        public formInvestorsPaymentsReport()
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
            try
            {
                lblReportTitle.Text = "Date Wise Investor Payment Report";
                reportType = 0;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                customer_code_text.Visible = false;
                customer_name_text.Visible = false;

                viewerDateWise.Visible = true;
                viewerOverAll.Visible = false;
                viewerInvestor.Visible = false;

                this.viewerDateWise.Dock = DockStyle.Fill;
                

                this.viewerInvestor.Clear();
                this.viewerDateWise.Clear();
                this.viewerOverAll.Clear();

                customer_name_text.Text = null;
                customer_code_text.Text = null;

                customer_name_text.Items.Clear();
                customer_code_text.Items.Clear();

                GetSetData.FillComboBoxWithValues("select * from pos_investors;", "full_name", customer_name_text);
                GetSetData.FillComboBoxWithValues("select * from pos_investors;", "code", customer_code_text);
                DisplayDateWiseReportInReportViewer(this.viewerDateWise);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void formInvestorsPaymentsReport_Load(object sender, EventArgs e)
        {
           refresh();
        }

        private void DateWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Investor Payment Report";
                reportType = 0;

                viewerDateWise.Visible = true;

                this.viewerDateWise.Dock = DockStyle.Fill;
              

                viewerInvestor.Visible = false;
                viewerOverAll.Visible = false;


                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                customer_code_text.Visible = false;
                customer_name_text.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void OverAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Investor Payment Report";
                reportType = 2;

                viewerOverAll.Visible = true;

                this.viewerOverAll.Dock = DockStyle.Fill;
               

                viewerInvestor.Visible = false;
                viewerDateWise.Visible = false;


                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;

                FromDate.Visible = false;
                ToDate.Visible = false;
                customer_code_text.Visible = false;
                customer_name_text.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void CustomerWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Investor Wise Payment Report";
                reportType = 1;

                viewerInvestor.Visible = true;

                this.viewerInvestor.Dock = DockStyle.Fill;
                

                viewerOverAll.Visible = false;
                viewerDateWise.Visible = false;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                customer_code_text.Visible = true;
                customer_name_text.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_investors", "code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("code", "pos_investors", "full_name", customer_name_text.Text);
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

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureDateWiseInvestorsPaymentsTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureDateWiseInvestorsPayments, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                //************************************************************************

                // *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalRetailSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalRetailSale == "" || totalRetailSale == "NULL")
                {
                    totalRetailSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalPurchaseSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalPurchaseSale == "" || totalPurchaseSale == "NULL")
                {
                    totalPurchaseSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOINpos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalRetailReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalRetailReturn == "" || totalRetailReturn == "NULL")
                {
                    totalRetailReturn = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOINpos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalPurchaseReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalPurchaseReturn == "" || totalPurchaseReturn == "NULL")
                {
                    totalPurchaseReturn = "0";
                }
                TextData.cash = double.Parse(totalRetailSale) - double.Parse(totalRetailReturn);
                TextData.salaries = double.Parse(totalPurchaseSale) - double.Parse(totalPurchaseReturn);
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.lessAmount))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netLessAmount_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netLessAmount_db == "" || netLessAmount_db == "NULL")
                {
                    netLessAmount_db = "0";
                }
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.salaries))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netSalaries_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netSalaries_db == "" || netSalaries_db == "NULL")
                {
                    netSalaries_db = "0";
                }
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.profit))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netProfitBalance_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netProfitBalance_db == "" || netProfitBalance_db == "NULL")
                {
                    netProfitBalance_db = "0";
                }
                //************************************************************************

                reportParamenters(viewer);
                // *******************************************************************************
                ReportParameter totalSalePrice = new ReportParameter("pTotalSalePrice", TextData.cash.ToString());
                viewer.LocalReport.SetParameters(totalSalePrice);

                ReportParameter totalWholeSalePrice = new ReportParameter("pTotalWholeSalePrice", TextData.salaries.ToString());
                viewer.LocalReport.SetParameters(totalWholeSalePrice);

                ReportParameter pLessAmount = new ReportParameter("pLessAmount", netLessAmount_db);
                viewer.LocalReport.SetParameters(pLessAmount);

                ReportParameter pSalaries = new ReportParameter("pSalaries", netSalaries_db);
                viewer.LocalReport.SetParameters(pSalaries);

                ReportParameter pProfitBalance = new ReportParameter("pProfitBalance", netProfitBalance_db);
                viewer.LocalReport.SetParameters(pProfitBalance);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayInvestorWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureInvestorWisePaymentsTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureInvestorWisePayments, FromDate.Text, ToDate.Text, customer_name_text.Text, customer_code_text.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                // *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalRetailSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalRetailSale == "" || totalRetailSale == "NULL")
                {
                    totalRetailSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalPurchaseSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalPurchaseSale == "" || totalPurchaseSale == "NULL")
                {
                    totalPurchaseSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOINpos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalRetailReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalRetailReturn == "" || totalRetailReturn == "NULL")
                {
                    totalRetailReturn = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOINpos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string totalPurchaseReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalPurchaseReturn == "" || totalPurchaseReturn == "NULL")
                {
                    totalPurchaseReturn = "0";
                }
                TextData.cash = double.Parse(totalRetailSale) - double.Parse(totalRetailReturn);
                TextData.salaries = double.Parse(totalPurchaseSale) - double.Parse(totalPurchaseReturn);
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.lessAmount))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netLessAmount_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netLessAmount_db == "" || netLessAmount_db == "NULL")
                {
                    netLessAmount_db = "0";
                }
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.salaries))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netSalaries_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netSalaries_db == "" || netSalaries_db == "NULL")
                {
                    netSalaries_db = "0";
                }
                //************************************************************************

                GetSetData.query = @"SELECT  sum(distinct(pos_investorPaybook.profit))
                                    FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
                                    pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
                                    pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
                                    where (pos_investorPaybook.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string netProfitBalance_db = data.SearchStringValuesFromDb(GetSetData.query);

                if (netProfitBalance_db == "" || netProfitBalance_db == "NULL")
                {
                    netProfitBalance_db = "0";
                }
                //************************************************************************

                reportParamenters(viewer);
                // *******************************************************************************

                ReportParameter totalSalePrice = new ReportParameter("pTotalSalePrice", TextData.cash.ToString());
                viewer.LocalReport.SetParameters(totalSalePrice);

                ReportParameter totalWholeSalePrice = new ReportParameter("pTotalWholeSalePrice", TextData.salaries.ToString());
                viewer.LocalReport.SetParameters(totalWholeSalePrice);

                ReportParameter pLessAmount = new ReportParameter("pLessAmount", netLessAmount_db);
                viewer.LocalReport.SetParameters(pLessAmount);

                ReportParameter pSalaries = new ReportParameter("pSalaries", netSalaries_db);
                viewer.LocalReport.SetParameters(pSalaries);

                ReportParameter pProfitBalance = new ReportParameter("pProfitBalance", netProfitBalance_db);
                viewer.LocalReport.SetParameters(pProfitBalance);
                //************************************************************************
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayOverAllReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureOverAllInvestorPaymentsTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureOverAllInvestorPayments);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsPayments_ds.ReportProcedureReportsTitles);
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
                DisplayDateWiseReportInReportViewer(this.viewerDateWise);
            }
            else if (reportType == 1)
            {
                DisplayInvestorWiseReportInReportViewer(this.viewerInvestor);
            }
            else if (reportType == 2)
            {
                DisplayOverAllReportInReportViewer(this.viewerOverAll);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
