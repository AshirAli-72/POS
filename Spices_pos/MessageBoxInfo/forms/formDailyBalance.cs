using System;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using Message_box_info.DailyBalancesReports;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms
{
    public partial class formDailyBalance : Form
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

        public formDailyBalance()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;

        private void setFormColorsDynamically()
        {
            try
            {
                int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
                int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
                int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

                int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

                int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

                //****************************************************************

                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Daily Balance Form", "Exit...", role_id);
            this.Close();
        }

        private void formDailyBalance_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void GetInvestorsProfit()
        {
            try
            {
                // Sales Details *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_SalePrice == "" || Sale_SalePrice == "NULL")
                {
                    Sale_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_WholeSale == "" || Sale_WholeSale == "NULL")
                {
                    Sale_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_PurchasePrice == "" || Sale_PurchasePrice == "NULL")
                {
                    Sale_PurchasePrice = "0";
                }

                // return Details*******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_SalePrice == "" || Return_SalePrice == "NULL")
                {
                    Return_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_wholeSale) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_WholeSale == "" || Return_WholeSale == "NULL")
                {
                    Return_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_PurchasePrice == "" || Return_PurchasePrice == "NULL")
                {
                    Return_PurchasePrice = "0";
                }
                // *******************************************************************************************

                string checkInvestorProfit_db = data.UserPermissions("investorProfit", "pos_general_settings");

                if (checkInvestorProfit_db == "W_Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }
                else if (checkInvestorProfit_db == "Sale - W_Sale")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale));
                }
                else if (checkInvestorProfit_db == "Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }

                txtInvestorProfit.Text = Math.Round(GetSetData.numericValue, 4).ToString();
                //********************************************************************
                string lessAmount_db = data.UserPermissions("lessAmount", "pos_general_settings");
                string employeeSalary_db = data.UserPermissions("employeeSalary", "pos_general_settings");
                string profitDivideBy_db = data.UserPermissions("profitDivide", "pos_general_settings");

                double lessAmount = 0;
                lessAmount = (GetSetData.numericValue * double.Parse(lessAmount_db)) / 100;
                GetSetData.numericValue = GetSetData.numericValue - lessAmount;
                GetSetData.numericValue = GetSetData.numericValue - double.Parse(employeeSalary_db);
                GetSetData.numericValue = GetSetData.numericValue / double.Parse(profitDivideBy_db);

                if (GetSetData.numericValue >= 0)
                {
                    txtCurrentInvestorProfit.Text = Math.Round(GetSetData.numericValue, 4).ToString();
                }
                else
                {
                    txtCurrentInvestorProfit.Text = "0";
                }
                //***********************************************************

                if (lessAmount >= 0)
                {
                    txtCharityAmount.Text = Math.Round(lessAmount, 4).ToString();
                }
                else
                {
                    txtCharityAmount.Text = "0";
                }

                txtTotalSalaries.Text = Math.Round(double.Parse(employeeSalary_db), 4).ToString();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void GetCompanyProfit()
        {
            try
            {
                // Sales Details *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_SalePrice == "" || Sale_SalePrice == "NULL")
                {
                    Sale_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_WholeSale == "" || Sale_WholeSale == "NULL")
                {
                    Sale_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Sale_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_PurchasePrice == "" || Sale_PurchasePrice == "NULL")
                {
                    Sale_PurchasePrice = "0";
                }

                // return Details*******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_SalePrice == "" || Return_SalePrice == "NULL")
                {
                    Return_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_wholeSale) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_WholeSale == "" || Return_WholeSale == "NULL")
                {
                    Return_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string Return_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_PurchasePrice == "" || Return_PurchasePrice == "NULL")
                {
                    Return_PurchasePrice = "0";
                }
                // *******************************************************************************************

                string checkInvestorProfit_db = data.UserPermissions("companyProfit", "pos_general_settings");

                if (checkInvestorProfit_db == "W_Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }
                else if (checkInvestorProfit_db == "Sale - W_Sale")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale));
                }
                else if (checkInvestorProfit_db == "Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }

                //********************************************************************
                if (GetSetData.numericValue >= 0)
                {
                    txtCompanyProfit.Text = Math.Round(GetSetData.numericValue, 4).ToString();
                }
                else
                {
                    txtCompanyProfit.Text = "0";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void GetInstallmentProfit()
        {
            try
            {
                GetSetData.query = @"SELECT sum(pos_installment_accounts.total_interest)
                                    FROM pos_sales_accounts INNER JOIN pos_installment_accounts ON pos_sales_accounts.sales_acc_id = pos_installment_accounts.sales_acc_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "');";

                string totalInterest = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalInterest == "" || totalInterest == "NULL")
                {
                    totalInterest = "0";
                }
                //********************************************************************

                txtInstallmentProfit.Text = Math.Round(double.Parse(totalInterest), 4).ToString();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void GetLoanGivenAmount()
        {
            try
            {
                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_loanDetails where (pos_loanDetails.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (status = 'Payment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "NULL" || GetSetData.Data == "")
                {
                    GetSetData.Data = "0";
                }

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_loanDetails where (pos_loanDetails.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (status = 'Received');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Permission == "NULL" || GetSetData.Permission == "")
                {
                    GetSetData.Permission = "0";
                }

                GetSetData.Data = (double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission)).ToString();

                if (double.Parse(GetSetData.Data) >= 0)
                {
                    txtLoanGiven.Text = Math.Round(double.Parse(GetSetData.Data), 4).ToString();
                }
                else
                {
                    txtLoanGiven.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DateWiseBalanceDetails()
        {
            try
            {
                double getFromSales = 0;
                double getFromReturns = 0;
                double getFromInstallments = 0;

                //**************************************************************************************
                GetSetData.query = "select round(sum(paid), 4) from pos_sales_accounts where (pos_sales_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (pos_sales_accounts.status = 'Sale');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_return_accounts where (pos_return_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_sales_accounts where (pos_sales_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (pos_sales_accounts.status != 'Sale');";
                GetSetData.query = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                    txtTotalSales.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalSales.Text = "0.00";
                }

                if (GetSetData.query != "NULL" && GetSetData.query != "")
                {
                    getFromInstallments = double.Parse(GetSetData.query);
                    txtTotalInstallments.Text = getFromInstallments.ToString();
                }
                else
                {
                    txtTotalInstallments.Text = "0.00";
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }
                //*******************************************************************************************

                getFromSales = (getFromSales + getFromInstallments) - getFromReturns;

                if (getFromSales >= 0)
                {
                    txtCashInHand.Text = getFromSales.ToString();
                }
                else
                {
                    txtCashInHand.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(credits), 4) from pos_sales_accounts where (pos_sales_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "')";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(credits), 4) from pos_return_accounts where (pos_return_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;
                getFromReturns = 0;

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }

                getFromSales -= getFromReturns;

                if (getFromSales >= 0)
                {
                    txtTotalCredits.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalCredits.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(total_capital), 4) from pos_capital;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalCapital.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalCapital.Text = "0.00";
                }

                //**************************************************************************************
                GetSetData.query = "select round(sum(investment), 4) from pos_investors where (pos_investors.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                
                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtInvestorsInvestment.Text = GetSetData.Data;
                }
                else
                {
                    txtInvestorsInvestment.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(amount), 4) from pos_capital_history where (pos_capital_history.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtOwnerInvestments.Text = GetSetData.Data;
                }
                else
                {
                    txtOwnerInvestments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = @"select round(sum(amount), 4) from pos_transaction_status inner join pos_banking_details on pos_transaction_status.status_id = pos_banking_details.status_id
                                    where (pos_banking_details.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (pos_transaction_status.status_title = 'Deposite');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select round(sum(amount), 4) from pos_transaction_status inner join pos_banking_details on pos_transaction_status.status_id = pos_banking_details.status_id
                                    where (pos_banking_details.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (pos_transaction_status.status_title = 'Withdraw');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;

                if (GetSetData.Data == "NULL" || GetSetData.Data == "")
                {
                    GetSetData.Data = "0";
                }

                if (GetSetData.Permission == "NULL" || GetSetData.Permission == "")
                {
                    GetSetData.Permission = "0";
                }

                getFromSales = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                if (getFromSales >= 0)
                {
                    txtBalanceInBanks.Text = getFromSales.ToString();
                }
                else
                {
                    txtBalanceInBanks.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(amount), 4) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id where (pos_recovery_details.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtRecoveries.Text = GetSetData.Data;
                }
                else
                {
                    txtRecoveries.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(payment), 4) from pos_investorPaybook where (pos_investorPaybook.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtInvestorsPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtInvestorsPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(paid), 4) from pos_purchase where (pos_purchase.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_purchase_return where (pos_purchase_return.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;
                getFromReturns = 0;

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                }
 
                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }

                getFromSales -= getFromReturns;

                if (getFromSales >= 0)
                {
                    txtTotalpurchasing.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalpurchasing.Text = "0.00";
                }

                //**************************************************************************************

                GetSetData.query = "select round(sum(balance), 4) from pos_salariesPaybook where (pos_salariesPaybook.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalSalaryPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalSalaryPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id where (pos_expense_details.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalExpenses.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalExpenses.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(totalAmount), 4) FROM pos_bankLoansDetails where (pos_bankLoansDetails.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalBankLoan.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalBankLoan.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_bankLoanPaybook where (pos_bankLoanPaybook.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalLoanPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalLoanPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(net_amount), 4) FROM pos_demand_list where (pos_demand_list.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalDemands.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalDemands.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(payment), 4) FROM pos_supplier_paybook where (pos_supplier_paybook.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtSupplierPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtSupplierPayments.Text = "0.00";
                }
                
                //**************************************************************************************
                GetSetData.query = "SELECT round(sum(lastCredits), 4) FROM pos_customer_lastCredits;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "NULL" || GetSetData.Data == "")
                {
                    GetSetData.Data = "0";
                }
               
                GetSetData.query = "SELECT round(sum(dues), 4) FROM pos_installment_plan where (status = 'Incomplete');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);
                

                if (GetSetData.Permission == "NULL" || GetSetData.Permission == "")
                {
                    GetSetData.Permission = "0";
                }

                getFromSales = 0;
                getFromSales = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                if (getFromSales >= 0)
                {
                    txtReceivables.Text = getFromSales.ToString();
                }
                else
                {
                    txtReceivables.Text = "0.00";
                }
                
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(previous_payables), 4) FROM pos_supplier_payables;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtPayables.Text = GetSetData.Data;
                }
                else
                {
                    txtPayables.Text = "0.00";
                }

                //**************************************************************************************
                GetInvestorsProfit();
                GetCompanyProfit(); 
                GetInstallmentProfit();
                GetLoanGivenAmount();
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_charityDetails where (pos_charityDetails.paymentDate between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtCharityPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtCharityPayments.Text = "0.00";
                }
                //**************************************************************************************

                double getFromTotalCash = double.Parse(txtCashInHand.Text);

                getFromTotalCash = (double.Parse(txtCashInHand.Text)) + (double.Parse(txtRecoveries.Text) + double.Parse(txtTotalBankLoan.Text) + double.Parse(txtOwnerInvestments.Text) + double.Parse(txtInvestorsInvestment.Text));
                txtCashInHand.Text = getFromTotalCash.ToString();
                
                //**************************************************************************************
                getFromTotalCash = (getFromTotalCash) - (double.Parse(txtBalanceInBanks.Text) + double.Parse(txtInvestorsPayments.Text) + double.Parse(txtTotalpurchasing.Text) + double.Parse(txtTotalSalaryPayments.Text) + double.Parse(txtTotalExpenses.Text) + double.Parse(txtTotalLoanPayments.Text) + double.Parse(txtSupplierPayments.Text) + double.Parse(txtCharityPayments.Text));

                if (getFromTotalCash >= 0)
                {
                    txtCashInHandBalance.Text = getFromTotalCash.ToString();
                }
                else
                {
                    txtCashInHandBalance.Text = "0.00";
                }
                //**************************************************************************************

                double currentCharityAmount = 0;
                currentCharityAmount = double.Parse(txtCharityAmount.Text) - double.Parse(txtCharityPayments.Text);

                if (currentCharityAmount >= 0)
                {
                    txtCurrentCharityAmount.Text = currentCharityAmount.ToString();
                }
                else
                {
                    txtCurrentCharityAmount.Text = "0.00";
                }
                //**************************************************************************************

                txtFromDate.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateWiseBalanceDetails();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DateWiseBalanceDetails();
        }

        private void refresh()
        {
            try
            {
                txtFromDate.Text = DateTime.Now.ToLongDateString();
                txtToDate.Text = DateTime.Now.ToLongDateString();
                txt_date.Text = DateTime.Now.ToLongDateString();
                double getFromSales = 0;
                double getFromReturns = 0;
                double getFromInstallments = 0;

                //**************************************************************************************
                GetSetData.query = "select round(sum(paid), 4) from pos_sales_accounts where (pos_sales_accounts.date = '" + txt_date.Text + "') and (pos_sales_accounts.status = 'Sale');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_return_accounts where (pos_return_accounts.date = '" + txt_date.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_sales_accounts where (pos_sales_accounts.date = '" + txt_date.Text + "') and (pos_sales_accounts.status != 'Sale');";
                GetSetData.query = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                    txtTotalSales.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalSales.Text = "0.00";
                }

                if (GetSetData.query != "NULL" && GetSetData.query != "")
                {
                    getFromInstallments = double.Parse(GetSetData.query);
                    txtTotalInstallments.Text = getFromInstallments.ToString();
                }
                else
                {
                    txtTotalInstallments.Text = "0.00";
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }
                //*******************************************************************************************

                getFromSales = (getFromSales + getFromInstallments) - getFromReturns;

                if (getFromSales >= 0)
                {
                    txtCashInHand.Text = getFromSales.ToString();
                }
                else
                {
                    txtCashInHand.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(credits), 4) from pos_sales_accounts where (pos_sales_accounts.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(credits), 4) from pos_return_accounts where (pos_return_accounts.date = '" + txt_date.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;
                getFromReturns = 0;

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }

                getFromSales -= getFromReturns;

                if (getFromSales >= 0)
                {
                    txtTotalCredits.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalCredits.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(total_capital), 4) from pos_capital;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalCapital.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalCapital.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(investment), 4) from pos_investors where (pos_investors.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtInvestorsInvestment.Text = GetSetData.Data;
                }
                else
                {
                    txtInvestorsInvestment.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(amount), 4) from pos_capital_history where (pos_capital_history.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtOwnerInvestments.Text = GetSetData.Data;
                }
                else
                {
                    txtOwnerInvestments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = @"select round(sum(amount), 4) from pos_transaction_status inner join pos_banking_details on pos_transaction_status.status_id = pos_banking_details.status_id
                                    where (pos_banking_details.date = '" + txt_date.Text + "') and (pos_transaction_status.status_title = 'Deposite');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select round(sum(amount), 4) from pos_transaction_status inner join pos_banking_details on pos_transaction_status.status_id = pos_banking_details.status_id
                                    where (pos_banking_details.date = '" + txt_date.Text + "') and (pos_transaction_status.status_title = 'Withdraw');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;
                getFromReturns = 0;

                if (GetSetData.Data == "NULL" || GetSetData.Data == "")
                {
                    GetSetData.Data = "0";
                }

                if (GetSetData.Permission == "NULL" || GetSetData.Permission == "")
                {
                    GetSetData.Permission = "0";
                }

                getFromSales = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                if (getFromSales >= 0)
                {
                    txtBalanceInBanks.Text = getFromSales.ToString();
                }
                else
                {
                    txtBalanceInBanks.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(amount), 4) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id where (pos_recovery_details.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtRecoveries.Text = GetSetData.Data;
                }
                else
                {
                    txtRecoveries.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(payment), 4) from pos_investorPaybook where (pos_investorPaybook.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtInvestorsPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtInvestorsPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "select round(sum(paid), 4) from pos_purchase where (pos_purchase.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select round(sum(paid), 4) from pos_purchase_return where (pos_purchase_return.date = '" + txt_date.Text + "');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                getFromSales = 0;
                getFromReturns = 0;

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    getFromSales = double.Parse(GetSetData.Data);
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    getFromReturns = double.Parse(GetSetData.Permission);
                }

                getFromSales -= getFromReturns;

                if (getFromSales >= 0)
                {
                    txtTotalpurchasing.Text = getFromSales.ToString();
                }
                else
                {
                    txtTotalpurchasing.Text = "0.00";
                }

                //**************************************************************************************

                GetSetData.query = "select round(sum(balance), 4) from pos_salariesPaybook where (pos_salariesPaybook.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalSalaryPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalSalaryPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id where (pos_expense_details.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalExpenses.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalExpenses.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(totalAmount), 4) FROM pos_bankLoansDetails where (pos_bankLoansDetails.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalBankLoan.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalBankLoan.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_bankLoanPaybook where (pos_bankLoanPaybook.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalLoanPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalLoanPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(net_amount), 4) FROM pos_demand_list where (pos_demand_list.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtTotalDemands.Text = GetSetData.Data;
                }
                else
                {
                    txtTotalDemands.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(payment), 4) FROM pos_supplier_paybook where (pos_supplier_paybook.date = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtSupplierPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtSupplierPayments.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(lastCredits), 4) FROM pos_customer_lastCredits;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "NULL" || GetSetData.Data == "")
                {
                    GetSetData.Data = "0";
                }

                GetSetData.query = "SELECT round(sum(dues), 4) FROM pos_installment_plan where (status = 'Incomplete');";
                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);


                if (GetSetData.Permission == "NULL" || GetSetData.Permission == "")
                {
                    GetSetData.Permission = "0";
                }

                getFromSales = 0;
                getFromSales = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                if (getFromSales >= 0)
                {
                    txtReceivables.Text = getFromSales.ToString();
                }
                else
                {
                    txtReceivables.Text = "0.00";
                }
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(previous_payables), 4) FROM pos_supplier_payables;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtPayables.Text = GetSetData.Data;
                }
                else
                {
                    txtPayables.Text = "0.00";
                }

                //**************************************************************************************
                GetInvestorsProfit();
                GetCompanyProfit();
                GetInstallmentProfit();
                GetLoanGivenAmount();
                //**************************************************************************************

                GetSetData.query = "SELECT round(sum(amount), 4) FROM pos_charityDetails where (pos_charityDetails.paymentDate = '" + txt_date.Text + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    txtCharityPayments.Text = GetSetData.Data;
                }
                else
                {
                    txtCharityPayments.Text = "0.00";
                }
                //**************************************************************************************

                double getFromTotalCash = double.Parse(txtCashInHand.Text);

                getFromTotalCash = (double.Parse(txtCashInHand.Text)) + (double.Parse(txtRecoveries.Text) + double.Parse(txtTotalBankLoan.Text) + double.Parse(txtOwnerInvestments.Text) + double.Parse(txtInvestorsInvestment.Text));
                txtCashInHand.Text = getFromTotalCash.ToString();
                //**************************************************************************************

                getFromTotalCash = (getFromTotalCash) - (double.Parse(txtBalanceInBanks.Text) + double.Parse(txtInvestorsPayments.Text) + double.Parse(txtTotalpurchasing.Text) + double.Parse(txtTotalSalaryPayments.Text) + double.Parse(txtTotalExpenses.Text) + double.Parse(txtTotalLoanPayments.Text) + double.Parse(txtSupplierPayments.Text) + double.Parse(txtCharityPayments.Text));

                if (getFromTotalCash >= 0)
                {
                    txtCashInHandBalance.Text = getFromTotalCash.ToString();
                }
                else
                {
                    txtCashInHandBalance.Text = "0.00";
                }
                //**************************************************************************************

                double currentCharityAmount = 0;
                currentCharityAmount = double.Parse(txtCharityAmount.Text) - double.Parse(txtCharityPayments.Text);

                if (currentCharityAmount >= 0)
                {
                    txtCurrentCharityAmount.Text = currentCharityAmount.ToString();
                }
                else
                {
                    txtCurrentCharityAmount.Text = "0.00";
                }
                //**************************************************************************************

                txtFromDate.Select();
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

        private void printDailyBalanceDetails()
        {
            try
            {
                TextData.fromDate = txtFromDate.Text;
                TextData.toDate = txtToDate.Text;
                TextData.totalCredits = txtTotalCredits.Text;
                TextData.totalCash = txtCashInHand.Text;
                TextData.totalBankBalance = txtBalanceInBanks.Text;
                TextData.ownerInvestments = txtOwnerInvestments.Text;
                TextData.InvestorsInvestment = txtInvestorsInvestment.Text;
                TextData.investorsPayments = txtInvestorsPayments.Text;
                TextData.totalPurchasing = txtTotalpurchasing.Text;
                TextData.totalRecoveries = txtRecoveries.Text;
                TextData.totalCapital = txtTotalCapital.Text;
                TextData.currentCash = txtCashInHandBalance.Text;
                TextData.totalSalaryPayments = txtTotalSalaryPayments.Text;
                TextData.totalExpenses = txtTotalExpenses.Text;
                TextData.totalBankLoan = txtTotalBankLoan.Text;
                TextData.totalLoanPayments = txtTotalLoanPayments.Text;
                TextData.totalInstallments = txtTotalInstallments.Text;
                TextData.totalDemands = txtTotalDemands.Text;
                TextData.totalSales = txtTotalSales.Text;
                TextData.totalSupplierPayments = txtSupplierPayments.Text;
                TextData.totalCharityPayments = txtCharityPayments.Text;
                TextData.totalReceivables = txtReceivables.Text;
                TextData.totalPayables = txtPayables.Text;
                TextData.totalInvestorProfit = txtInvestorProfit.Text;
                TextData.companyProfit = txtCompanyProfit.Text;
                TextData.installmentProfit = txtInstallmentProfit.Text;
                TextData.totalCharityAmount = txtCharityAmount.Text;
                TextData.currentCharity = txtCurrentCharityAmount.Text;
                TextData.totalSalaries = txtTotalSalaries.Text;
                TextData.currentInvestorProfit = txtCurrentInvestorProfit.Text;
                TextData.totalLoanGiven = txtLoanGiven.Text;

                GetSetData.SaveLogHistoryDetails("Daily Balance Form", "Printing [" + txtFromDate.Text + "  " + txtToDate.Text + "] balance details", role_id);
                formDailyBalanceReport report = new formDailyBalanceReport();
                report.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDailyBalanceDetails();
        }

        private void formDailyBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                printDailyBalanceDetails();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                DateWiseBalanceDetails();
            }
        }
    }
}
