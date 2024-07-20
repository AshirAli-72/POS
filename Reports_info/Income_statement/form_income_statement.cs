using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Income_statement
{
    public partial class form_income_statement : Form
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

        public form_income_statement()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void incomeStatement()
        {
            try
            {
                GetSetData.query = @"select sum(amount_due) from pos_sales_accounts inner join pos_clock_in on pos_clock_in.id = pos_sales_accounts.clock_in_id
                                     where (pos_clock_in.date between '"+ FromDate.Text + "' and '"+ ToDate.Text + "');";

                string sales_amount_due = data.SearchStringValuesFromDb(GetSetData.query);

                if (sales_amount_due == "" || sales_amount_due == "NULL")
                {
                    sales_amount_due = "0";
                }
                
                // *******************************************************************************************
                
                GetSetData.query = @"select sum(amount_due) from pos_return_accounts inner join pos_clock_in on pos_clock_in.id = pos_return_accounts.clock_in_id
                                     where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string returns_amount_due = data.SearchStringValuesFromDb(GetSetData.query);

                if (returns_amount_due == "" || returns_amount_due == "NULL")
                {
                    returns_amount_due = "0";
                }
                
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase) FROM pos_sales_accounts inner join pos_clock_in ON pos_clock_in.id = pos_sales_accounts.clock_in_id
                                    INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id 
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string sales_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (sales_amount_purchase == "" || sales_amount_purchase == "NULL")
                {
                    sales_amount_purchase = "0";
                }
                
                //********************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_return_accounts inner join pos_clock_in ON pos_clock_in.id = pos_return_accounts.clock_in_id
                                    INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id 
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                
                //********************************************************************

                GetSetData.query = @"select sum(pos_expense_items.amount) from pos_expense_details inner join pos_expense_items on pos_expense_details.expense_id = pos_expense_items.expense_id
                                    inner join pos_clock_in on pos_clock_in.id = pos_expense_details.clock_in_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string total_expenses = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_expenses == "" || total_expenses == "NULL")
                {
                    total_expenses = "0";
                }
                
                
                //********************************************************************

                GetSetData.query = @"select sum(amount) from pos_salariesPaybook where (paymentDate between '"+ FromDate.Text + "' and '"+ ToDate.Text + "');";

                string salary_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (salary_payment == "" || salary_payment == "NULL")
                {
                    salary_payment = "0";
                }
                

                //********************************************************************

                GetSetData.query = @"select sum(commission_payment) from pos_salariesPaybook where (paymentDate between '"+ FromDate.Text + "' and '"+ ToDate.Text + "');";

                string commission_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (commission_payment == "" || commission_payment == "NULL")
                {
                    commission_payment = "0";
                }


                //********************************************************************

                GetSetData.query = @"select sum(amount) from pos_bankLoanPaybook where (date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string bank_loan_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (bank_loan_payment == "" || bank_loan_payment == "NULL")
                {
                    bank_loan_payment = "0";
                }
                

                //********************************************************************

                GetSetData.query = @"select sum(payment) from pos_investorPaybook where (date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string investor_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (investor_payment == "" || investor_payment == "NULL")
                {
                    investor_payment = "0";
                }


                //********************************************************************

                GetSetData.query = @"select sum(paid) from pos_purchase where (date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string purchasing_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (purchasing_payment == "" || purchasing_payment == "NULL")
                {
                    purchasing_payment = "0";
                }
                

                //********************************************************************

                GetSetData.query = @"select sum(payment) from pos_supplier_paybook where (date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string supplier_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (supplier_payment == "" || supplier_payment == "NULL")
                {
                    supplier_payment = "0";
                }

                // *******************************************************************************************

                this.viewer_income_statement.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_income_statement.LocalReport.Refresh();
                this.viewer_income_statement.LocalReport.EnableExternalImages = true;


                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.viewer_income_statement.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.viewer_income_statement.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                this.viewer_income_statement.LocalReport.SetParameters(title);


                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                this.viewer_income_statement.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                this.viewer_income_statement.LocalReport.SetParameters(phone);


                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                this.viewer_income_statement.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************


                ReportParameter pSalesAmountDue = new ReportParameter("pSalesAmountDue", sales_amount_due);
                this.viewer_income_statement.LocalReport.SetParameters(pSalesAmountDue);


                ReportParameter pReturnAmountDue = new ReportParameter("pReturnAmountDue", returns_amount_due);
                this.viewer_income_statement.LocalReport.SetParameters(pReturnAmountDue);


                ReportParameter pTotalExpenses = new ReportParameter("pTotalExpenses", total_expenses);
                this.viewer_income_statement.LocalReport.SetParameters(pTotalExpenses);


                ReportParameter pSalaryPayment = new ReportParameter("pSalaryPayment", salary_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pSalaryPayment);


                ReportParameter pCommissionPayment = new ReportParameter("pCommissionPayment", commission_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pCommissionPayment);


                ReportParameter pBankLoanPayment = new ReportParameter("pBankLoanPayment", bank_loan_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pBankLoanPayment);


                ReportParameter pInvestorPayment = new ReportParameter("pInvestorPayment", investor_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pInvestorPayment);


                ReportParameter pPurchasePayment = new ReportParameter("pPurchasePayment", purchasing_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pPurchasePayment);


                ReportParameter pSupplierPayment = new ReportParameter("pSupplierPayment", supplier_payment);
                this.viewer_income_statement.LocalReport.SetParameters(pSupplierPayment);

                //********************************************************************

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_income_statement.LocalReport.SetParameters(fromDate);


                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_income_statement.LocalReport.SetParameters(toDate);


                ReportParameter sales_amountsPur1 = new ReportParameter("pTotalSalesPur", sales_amount_purchase);
                this.viewer_income_statement.LocalReport.SetParameters(sales_amountsPur1);


                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_income_statement.LocalReport.SetParameters(return_amountsPur1);

                this.viewer_income_statement.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_income_statement_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                incomeStatement();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                incomeStatement();
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
