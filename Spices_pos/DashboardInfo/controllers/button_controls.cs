using System;
using System.Drawing;
using Spices_pos.LoginInfo.forms;
using Settings_info.forms;
using Customers_info.forms;
using Supplier_Chain_info.forms;
using Products_info.forms;
using Purchase_info.forms;
using Expenses_info.forms;
using Stock_management.forms;
using CounterSales_info.forms;
using Recoverier_info.forms;
using Reports_info.Customer_sales_reports.Customer_Returns_reports;
using Reports_info.Customer_sales_reports.loyal_customer_sales_reports;
using Reports_info.Company_ledger;
using Reports_info.Company_Statement;
using Reports_info.Day_book;
using Reports_info.Stock;
using Reports_info.Receivables;
using Reports_info.Payables;
using Reports_info.Incoming_balance;
using Reports_info.Income_statement;
using Reports_info.Recoveries;
using Reports_info.DashboardChequeNotify;
using Stock_management.Low_inventory_reports;
using Datalayer;
using Banking_info.forms;
using RefereningMaterial;
using Demands_info.forms;
using Investors_info.forms;
using Message_box_info.forms;
using Recoverier_info.Customer_Dues_Report;
using Banks_Loan_info.forms;
using Message_box_info.forms.Clock_In;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Spices_pos.DashboardInfo.controllers
{
    class button_controls
    {
        public static void loginform()
        {
            login_form login = new login_form();
            login.Show();
        }

        public static void SettingsButton()
        {
            settings sett = new settings();
            sett.Show();
        }

        public static void SupplierPaybookButton()
        {
            formPurchasePaybookDetails sett = new formPurchasePaybookDetails();
            sett.Show();
        }

        public static void CustomerButton()
        {
            Customer_details.count = 1;
            Customer_details.selected_customer = "";
            Customer_details customer = new Customer_details();
            customer.Show();
        }

        public static void ShowInvesterDetails()
        {
            formInvestorDetails customer = new formInvestorDetails();
            customer.Show();
        }
        
        public static void suppliersButton()
        {
            Supliers_details.count = 1;
            Supliers_details supplier = new Supliers_details();
            supplier.Show();
        }

        public static void InvestorsPaymentDetailsButton()
        {
            formInvestorsPaybookDetails supplier = new formInvestorsPaybookDetails();
            supplier.Show();
        }

        public static void customerDuesButton()
        {
            CustomerDues dues = new CustomerDues();
            dues.Show();
        }

        public static void DemandsListButton()
        {
            formDemandList dues = new formDemandList();
            dues.Show();
        }

        public static void recoveriesButton()
        {
            form_recovery_list recover = new form_recovery_list();
            recover.Show();
        }

        public static void productsButton()
        {
            product_details.count = 1;
            product_details products = new product_details();
            products.Show();
        }

        public static void purchaseButton()
        {
            Purchase_details purchase = new Purchase_details();
            purchase.Show();
        }

        public static void expensesButton()
        {
            Expenses_details expenses = new Expenses_details();
            expenses.Show();
        }

        public static void stockButton()
        {
            Hole_inventory stock = new Hole_inventory();
            stock.Show();
        }
       

        public static void Customer_Returns_reports_btn(int role_id)
        {
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            GetSetData.query = @"select page_size from pos_general_settings;";
            TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

            //if (TextData.general_options == "A4")
            //{
                form_customer_returns.role_id = role_id;
                form_customer_returns reports = new form_customer_returns();
                reports.ShowDialog();
            //}
            //else
            //{
            //    form_returns_small_report.role_id = role_id;
            //    form_returns_small_report reports = new form_returns_small_report();
            //    reports.ShowDialog();
            //}
        }

        public static bool CounterCashButton(int role_id, int user_id)
        {
            try
            {
                Datalayers data = new Datalayers(webConfig.con_string);
                error_form error = new error_form();
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

                GetSetData.query = @"select pos_security from pos_general_settings;";
                TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
                string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


                if (singleAuthorityClosing == "No")
                {
                    GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                    string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);
                    
                    if (TextData.general_options == "Enabled")
                    {
                        if (clock_in_id != "")
                        {
                            formSetClockIn.user_id = user_id;
                            formSetClockIn.role_id = role_id;
                            formSetClockIn.saveEnable = false;
                            formSetClockIn add_customer = new formSetClockIn();

                            add_customer.ShowDialog();

                            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                            string is_not_set_yet = data.SearchStringValuesFromDb(GetSetData.query);

                            if (is_not_set_yet == "")
                            {
                                TextData.isClockedIn = true;

                                form_counter_sales.user_id = user_id;
                                form_counter_sales.role_id = role_id;
                                form_counter_sales pos = new form_counter_sales();
                                pos.Show();

                                return true;
                            }
                            else
                            {
                                TextData.isClockedIn = false;
                                return false;
                            }
                        }
                        else
                        {
                            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0') ORDER BY id DESC;";
                            string is_clocked_in = data.SearchStringValuesFromDb(GetSetData.query);

                            if (is_clocked_in == "")
                            {
                                //error.errorMessage("Please clock in first!");
                                //error.ShowDialog();

                                using (formAddClockIn add_customer = new formAddClockIn())
                                {
                                    formAddClockIn.user_id = user_id;
                                    formAddClockIn.role_id = role_id;
                                    formAddClockIn.saveEnable = false;
                                    add_customer.ShowDialog();


                                    GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0') ORDER BY id DESC;";
                                    string is_employee_clocked_in = data.SearchStringValuesFromDb(GetSetData.query);

                                    if (is_employee_clocked_in == "")
                                    {
                                        TextData.isClockedIn = false;
                                        return false;
                                    }
                                    else
                                    {
                                        TextData.isClockedIn = true;

                                        form_counter_sales.user_id = user_id;
                                        form_counter_sales.role_id = role_id;
                                        form_counter_sales pos = new form_counter_sales();
                                        pos.Show();

                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                TextData.isClockedIn = true;

                                form_counter_sales.user_id = user_id;
                                form_counter_sales.role_id = role_id;
                                form_counter_sales pos = new form_counter_sales();
                                pos.Show();

                                return true;
                            }


                            TextData.isClockedIn = false;
                            return false;
                        }

                    }
                    else
                    {
                        TextData.isClockedIn = true;

                        form_counter_sales.user_id = user_id;
                        form_counter_sales.role_id = role_id;
                        form_counter_sales pos = new form_counter_sales();
                        pos.Show();

                        return true;
                    }
                }
                else
                {
                    GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "') and (status = '-2') ORDER BY id DESC;";
                    string isClockedOutExist = data.SearchStringValuesFromDb(GetSetData.query);


                    if (isClockedOutExist != "")
                    {
                        form_sure_message sure = new form_sure_message();
                        sure.Message_choose("Sorry your shift is closed. Would you like to re-active your shift again!");

                        Screen primaryScreen = Screen.PrimaryScreen;
                        sure.Location = primaryScreen.WorkingArea.Location;
                        sure.WindowState = FormWindowState.Normal;
                        sure.ShowDialog();


                        if (form_sure_message.sure == true)
                        {
                            GetSetData.query = @"update pos_clock_in set status  = '-1' where (id = '" + isClockedOutExist + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.query = @"delete from pos_clock_out where (clock_in_id = '" + isClockedOutExist + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            //******************************************

                            TextData.isClockedIn = true;

                            form_counter_sales.user_id = user_id;
                            form_counter_sales.role_id = role_id;
                            form_counter_sales pos = new form_counter_sales();
                            pos.Show();

                            return true;
                        }
                        else
                        {
                            TextData.isClockedIn = false;
                            return false;
                        }
                    }
                    else
                    {
                        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                        string is_not_set_yet = data.SearchStringValuesFromDb(GetSetData.query);

                        if (is_not_set_yet == "")
                        {
                            error.errorMessage("Sorry you are not clock-In yet. Please clock-In first before make a sale.");
                            error.ShowDialog();


                            TextData.isClockedIn = true;

                            formClockInDetails.user_id = user_id;
                            formClockInDetails.role_id = role_id;
                            formClockInDetails _clock_in_obj = new formClockInDetails();
                            _clock_in_obj.Show();

                            return true;
                        }
                        else
                        {
                            TextData.isClockedIn = true;

                            form_counter_sales.user_id = user_id;
                            form_counter_sales.role_id = role_id;
                            form_counter_sales pos = new form_counter_sales();
                            pos.Show();

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);

                return false;
            }
        }

        public static void Day_book_btn()
        {
            form_day_book reports = new form_day_book();
            reports.Show();
        }

        public static void stock_report_btn()
        {
            form_stock reports = new form_stock();
            reports.Show();
        }

        public static void Receivables_report_btn()
        {
            form_receivables reports = new form_receivables();
            reports.Show();
        }

        public static void Payables_report_btn()
        {
            form_payables reports = new form_payables();
            reports.Show();
        }

        public static void incoming_balance_report_btn()
        {
            form_Incoming_balance reports = new form_Incoming_balance();
            reports.Show();
        }

        public static void profit_loss_report_btn()
        {
            form_income_statement reports = new form_income_statement();
            reports.Show();
        }

        public static void Recoveries_report_btn()
        {
            formSupplierPaymentReport reports = new formSupplierPaymentReport();
            reports.Show();
        }

        //public static void Database_backup_btn()
        //{
        //    form_backup_db reports = new form_backup_db();
        //    reports.ShowDialog();
        //}

        //public static void Database_Restore_btn()
        //{
        //    form_restore_db reports = new form_restore_db();
        //    reports.ShowDialog();
        //}

        public static void Low_inventory_notificatoin_btn()
        {
            form_low_inventory reports = new form_low_inventory();
            reports.ShowDialog();
        }

        public static void GurantorsButton()
        {
            form_GranterDetails.count = 1;
            form_GranterDetails supplier = new form_GranterDetails();
            supplier.Show();
        }

        public static void CompanyDetailsButton()
        {
            form_supplier_details.count = 1;
            form_supplier_details supplier = new form_supplier_details();
            supplier.Show();
        }

        //public static void buttonDailyBalance()
        //{
        //    formDailyBalance supplier = new formDailyBalance();
        //    supplier.ShowDialog();
        //}

        public static void chequeNotifications()
        {
            formChequeNotify supplier = new formChequeNotify();
            supplier.ShowDialog();
        }

        public static void DefaultersNotifications()
        {
            form_cus_dues supplier = new form_cus_dues();
            supplier.ShowDialog();
        }

        //public static void AboutSoftwareLicense()
        //{
        //    formLoginForConfigureLicense supplier = new formLoginForConfigureLicense();
        //    supplier.ShowDialog();
        //}

        //public static void customer_ledger_button()
        //{
        //    Customer_legers_form reports = new Customer_legers_form();
        //    reports.ShowDialog();
        //}

        //public static void customer_statement_button()
        //{
        //    form_customer_statement reports = new form_customer_statement();
        //    reports.ShowDialog();
        //}

        public static void customer_sales_button(int role_id)
        {
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            //GetSetData.query = @"select page_size from pos_general_settings;";
            //TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

            //if (TextData.general_options == "A4")
            //{
                form_loyal_cus_sales.role_id = role_id;
                form_loyal_cus_sales reports = new form_loyal_cus_sales();
                reports.Show();
            //}
            //else
            //{
            //    form_sales_small_report.role_id = role_id;
            //    form_sales_small_report reports = new form_sales_small_report();
            //    reports.Show();
            //}
        }

        public static void company_ledger_button()
        {
            form_company_ledger reports = new form_company_ledger();
            reports.ShowDialog();
        }

        public static void company_statement_button()
        {
            form_company_statement reports = new form_company_statement();
            reports.ShowDialog();
        }

        public static void ShowCapitalForm()
        {
            formCapitalHistoryDetails reports = new formCapitalHistoryDetails();
            reports.Show();
        }

        //public static void GenerateInvoicesReportButton()
        //{
        //    GenerateCustomerInvoices reports = new GenerateCustomerInvoices();
        //    reports.ShowDialog();
        //}

        public static void bank_details_button()
        {
            form_bank_details reports = new form_bank_details();
            reports.Show();
        }

        public static void bankLoanDetailsbutton()
        {
            formBanksLoansDetails reports = new formBanksLoansDetails();
            reports.Show();
        }

        public static void BankLoanPaybookbutton()
        {
            formBankLoanPaybookDetails reports = new formBankLoanPaybookDetails();
            reports.Show();
        }

        public static void EmployeeSalariesbutton()
        {
            formSalaryPaymentDetails reports = new formSalaryPaymentDetails();
            reports.Show();
        }

        // Button Hover and Leave Code Snipit
        public static Color ButtonHoversBackColor()
        {
            Color bcol = Color.FromArgb(244, 247, 252);
            return bcol;
        }

        public static Color ButtonHoversForeColor()
        {
            Color fcol = Color.FromArgb(5, 100, 146);
            return fcol;
        }

        public static Color ButtonHoversLeavesBackColor()
        {
            Color bcol = Color.FromArgb(30, 116, 163);
            return bcol;
        }

        public static Color ButtonHoversLeavesForeColor()
        {
            Color fcol = Color.FromArgb(244, 247, 252);
            return fcol;
        }
    }
}
