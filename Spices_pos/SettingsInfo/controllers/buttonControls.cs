using System;
using Spices_pos.DashboardInfo.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.LoginInfo.forms;
using Demands_info.forms;
using Expenses_info.forms;
using Investors_info.forms;
using Investors_info.reports;
using Purchase_info.forms;
using Recoverier_info.forms;
using Customers_info.forms;
using Stock_management.forms;

namespace Settings_info.controllers
{
    public class buttonControls
    {
        public static void mainMenu_buttons()
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
        }

        public static void LowInventory_buttons()
        {
            Low_inventory lowInventory = new Low_inventory();
            lowInventory.Show();
        }

        public static void WholeInventory_buttons()
        {
            Hole_inventory wholeInventory = new Hole_inventory();
            wholeInventory.Show();
        }

        public static void CustomerDetailsbuttons()
        {
            Customer_details customer_detail = new Customer_details();
            customer_detail.ShowDialog();
        }

        public static void recovery_list_buttons()
        {
            form_recovery_list recover = new form_recovery_list();
            recover.Show();
        }

        public static void customersInvoicesButton()
        {
            formCustomerWiseBillsDetails recover = new formCustomerWiseBillsDetails();
            recover.ShowDialog();
        }

        public static void Investor_detail_buttons()
        {
            formInvestorDetails customer_detail = new formInvestorDetails();
            customer_detail.Show();
        }

        public static void NewInvestorsPaymentsDetails()
        {
            formInvestorsPaybookDetails add_customer = new formInvestorsPaybookDetails();
            add_customer.Show();
        }

        public static void PrintInvestorDetails()
        {
            formInvestorsList add_customer = new formInvestorsList();
            add_customer.ShowDialog();
        }

        public static void expenses_detail_buttons()
        {
            Expenses_details expenses_detail = new Expenses_details();
            expenses_detail.Show();
        }

        public static void Add_company_buttons()
        {
            form_purchase_from purchase_detail = new form_purchase_from();
            purchase_detail.ShowDialog();
        }

        public static void AddNewDemand()
        {
            formAddNewDemand add_purchase = new formAddNewDemand();
            add_purchase.Show();
        }

        public static void DemandListbuttons()
        {
            formDemandList purchase_detail = new formDemandList();
            purchase_detail.Show();
        }


        public static void registration_buttons()
        {
            Registration_form registration = new Registration_form();
            registration.ShowDialog();
        }

        public static string fun_title_db()
        {
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);
            error_form error = new error_form();
            done_form done = new done_form();

            try
            {
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                return GetSetData.Data;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }

        public static void employeeButton()
        {
            Add_supplier supplier = new Add_supplier();
            supplier.ShowDialog();
        }

        public static string fun_sub_title_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                return GetSetData.Data;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }

        public static string fun_phone_no_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                return GetSetData.Data;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }

        public static string fun_message_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                return GetSetData.Data;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);

                return "";
            }
        }

        public static string fun_copyrights_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                return GetSetData.Data;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);

                return "";
            }
        }

        public static void update_Configuration_details()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                if (TextData.comments == "")
                {
                    TextData.comments = "nill!";
                }

                GetSetData.Ids = data.UserPermissionsIds("config_id", "pos_configurations");

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_configurations values ('" + TextData.shop_name.ToString() + "' , '" + TextData.owner_name.ToString() + "' , '" + TextData.city.ToString() + "' , '" + TextData.address.ToString() + "' , '" + TextData.business_type.ToString() + "' , '" + TextData.branch.ToString() + "' , '" + TextData.shop_no.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.phone2.ToString() + "' , '" + TextData.saved_image_path.ToString() + "' , '" + TextData.comments.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    GetSetData.query = @"update pos_configurations set shop_name = '" + TextData.shop_name.ToString() + "' , owner_name = '" + TextData.owner_name.ToString() + "' , city = '" + TextData.city.ToString() + "' , address = '" + TextData.address.ToString() + "' , business_nature = '" + TextData.business_type.ToString() + "' , branch = '" + TextData.branch.ToString() + "' , shop_no = '" + TextData.shop_no.ToString() + "' , phone_1 = '" + TextData.phone1.ToString() + "' , phone_2 = '" + TextData.phone2.ToString() + "' , logo_path = '" + TextData.saved_image_path.ToString() + "' , comments = '" + TextData.comments.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }

        public static void insert_update_reports_tables()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Ids = data.UserPermissionsIds("report_id", "pos_report_settings", "report_id", "1");
                
                if(TextData.title != "")
                {
                    if (TextData.sub_title != "")
                    {
                        if (TextData.message != "")
                        {
                            if(TextData.copyrights != "")
                            {
                                if (GetSetData.Ids == 0)
                                {
                                    GetSetData.query = @"insert into pos_report_settings values ('" + TextData.title.ToString() + "' , '" + TextData.sub_title.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.message.ToString() + "' , '" + TextData.copyrights.ToString() + "' , '" + TextData.saved_image_path.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    done.DoneMessage("Successfully Saved!");
                                    done.ShowDialog();
                                }
                                else
                                {
                                    GetSetData.query = @"update pos_report_settings set title = '" + TextData.title.ToString() + "' , address = '" + TextData.sub_title.ToString() + "' , phone_no = '" + TextData.phone1.ToString() + "' , note = '" + TextData.message.ToString() + "' , copyrights = '" + TextData.copyrights.ToString() + "' , logo_path = '" + TextData.saved_image_path.ToString() + "' where (report_id = '1');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    done.DoneMessage("Successfully Updated!");
                                    done.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Copyrights field is empty!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Note field is empty!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Address field is empty!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Title field is empty!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        
        //public static void Registration_info()
        //{
        //    error_form error = new error_form();
        //    done_form done = new done_form();
        //    ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        //    Datalayers data = new Datalayers(webConfig.con_string);

        //    try
        //    {
        //        GetSetData.Data = "";

        //        GetSetData.Ids = data.UserPermissionsIds("reg_id", "pos_registration", "name", TextData.person_name);

        //        GetSetData.query = @"select username from pos_role;";
        //        data.Connect();

        //        data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
        //        data.reader_ = data.cmd_.ExecuteReader();

        //        if (data.reader_.Read())
        //        {
        //            GetSetData.Data = data.reader_[0].ToString();
        //            data.reader_.Close();
        //        }
        //        else if (!data.reader_.Read())
        //        {
        //            GetSetData.Data = "";
        //            data.reader_.Close();
        //            data.Disconnect();
        //        }

        //        if (GetSetData.Ids == 0)
        //        {
        //            if (TextData.username != GetSetData.Data)
        //            {
        //                data.Connect();

        //                if (TextData.person_name != "" && TextData.role_title != "")
        //                {
        //                    if (TextData.password == TextData.confirm_password)
        //                    {
        //                        if (TextData.loginEmployee != "")
        //                        {

        //                            GetSetData.query = @"insert into pos_registration values ('" + TextData.person_name.ToString() + "' , '" + TextData.role_title.ToString() + "');";
        //                            data.insertUpdateCreateOrDelete(GetSetData.query);


        //                            GetSetData.query = @"select reg_id from pos_registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
        //                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //                            GetSetData.query = @"select employee_id from pos_employees where full_name = '" + TextData.loginEmployee.ToString() + "';";
        //                            GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //                            GetSetData.query = @"insert into pos_role values ('" + TextData.username.ToString() + "' , '" + TextData.password.ToString() + "' , '" + GetSetData.Ids.ToString() + "', '" + GetSetData.fks.ToString() +"');";
        //                            data.insertUpdateCreateOrDelete(GetSetData.query);
        //                        }
        //                        else
        //                        {
        //                            error.errorMessage("Please Select Employee Name!");
        //                            error.ShowDialog();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        error.errorMessage("The password is not matching!");
        //                        error.ShowDialog();
        //                    }
        //                }
        //                else
        //                {
        //                    error.errorMessage("Please Fill all the Fields!");
        //                    error.ShowDialog();
        //                }
        //            }
        //            else
        //            {
        //                error.errorMessage("Password is not Matching!");
        //                error.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage(TextData.person_name.ToString() + " is already exist!");
        //            error.ShowDialog();
        //        }

        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        //MessageBox.Show(es.Message);
        //    }
        //    finally
        //    {
        //        data.Disconnect();
        //    }
        //}

        public static void insert_dashboard_button_controls()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = '" + TextData.role_title.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select dashboard_id from pos_tbl_authorities_dashboard where (role_id = '" + GetSetData.Ids.ToString() + "');";
                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_tbl_authorities_dashboard values ('" + TextData.pos.ToString() + "' , '" + TextData.purchases.ToString() + "' , '" + TextData.products.ToString() + "' , '" + TextData.recoveries.ToString() + "' , '" + TextData.expenses.ToString() + "' , '" + TextData.suppliers.ToString() + "' , '" + TextData.isEmployee.ToString() + "' , '" + TextData.customers.ToString() + "' , '" + TextData.IsStock.ToString() + "' , '" + TextData.reports.ToString() + "' , '" + TextData.customer_dues.ToString() + "' , '" + TextData.settings.ToString() + "' , '" + TextData.notifications.ToString() + "' , '" + TextData.backup.ToString() + "' , '" + TextData.restore.ToString() + "' , '" + TextData.about.ToString() + "' , '" + TextData.logOut.ToString() + "'  , '" + TextData.capital.ToString() + "'   , '" + TextData.dailyBalance.ToString() + "'   , '" + TextData.Investors.ToString() + "'   , '" + TextData.InvestorsPaybook.ToString() + "'   , '" + TextData.guarantors.ToString() + "' , '" + TextData.aboutLicense.ToString() + "' , '" + TextData.EmployeeSalaries.ToString() + "' , '" + TextData.bankLoan.ToString() + "' , '" + TextData.bankLoanPaybook.ToString() + "' ,  '" + TextData.supplierPaybook.ToString() + "' ,  '" + TextData.charity.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_tbl_authorities_dashboard set pos = '" + TextData.pos.ToString() + "' , purchases = '" + TextData.purchases.ToString() + "' , products = '" + TextData.products.ToString() + "' , recoveries = '" + TextData.recoveries.ToString() + "' , expenses = '" + TextData.expenses.ToString() + "' , suppliers = '" + TextData.suppliers.ToString() + "' , employee = '" + TextData.isEmployee.ToString() + "' , customers = '" + TextData.customers.ToString() + "' , stock = '" + TextData.IsStock.ToString() + "' , reports = '" + TextData.reports.ToString() + "' , customer_dues = '" + TextData.customer_dues.ToString() + "' , settings = '" + TextData.settings.ToString() + "' , notifications = '" + TextData.notifications.ToString() + "' , backups = '" + TextData.backup.ToString() + "' , restores = '" + TextData.restore.ToString() + "' , about = '" + TextData.about.ToString() + "' , logout = '" + TextData.logOut.ToString() + "'  , capital = '" + TextData.capital.ToString() + "'  , dailyBalance = '" + TextData.dailyBalance.ToString() + "'  , investors = '" + TextData.Investors.ToString() + "'   , investorPaybook = '" + TextData.InvestorsPaybook.ToString() + "'   , guarantors = '" + TextData.guarantors.ToString() + "' , aboutLicense = '" + TextData.aboutLicense.ToString() + "' , EmployeeSalaries = '" + TextData.EmployeeSalaries.ToString() + "' , bankLoan = '" + TextData.bankLoan.ToString() + "' , bankLoanPaybook = '" + TextData.bankLoanPaybook.ToString() + "' ,  supplierPaybook = '" + TextData.supplierPaybook.ToString() + "' ,  charity = '" + TextData.charity.ToString() + "' where (role_id = '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        public static void insert_reports_button_controls()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = '" + TextData.role_title.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select reports_id from pos_tbl_authorities_reports where (role_id = '" + GetSetData.Ids.ToString() + "');";
                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_tbl_authorities_reports values ('" + TextData.company_ledger.ToString() + "' , '" + TextData.company_statement.ToString() + "' , '" + TextData.cus_ledger.ToString() + "' , '" + TextData.customer_statement.ToString() + "' , '" + TextData.day_book.ToString() + "' , '" + TextData.stock.ToString() + "' , '" + TextData.cus_sales.ToString() + "' , '" + TextData.cus_returns.ToString() + "' , '" + TextData.receivables.ToString() + "' , '" + TextData.payables.ToString() + "' , '" + TextData.cus_recoveries.ToString() + "' , '" + TextData.balance_in_out.ToString() + "' , '" + TextData.income_statement.ToString() + "'  , '" + TextData.generateInvoices.ToString() + "'  , '" + TextData.chequeDetails.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_tbl_authorities_reports set  company_ledger = '" + TextData.company_ledger.ToString() + "' , company_statement = '" + TextData.company_statement.ToString() + "' , customer_ledger = '" + TextData.cus_ledger.ToString() + "' , customer_statement = '" + TextData.customer_statement.ToString() + "' , day_book = '" + TextData.day_book.ToString() + "' , stock = '" + TextData.IsStock.ToString() + "' , sales_report = '" + TextData.cus_sales.ToString() + "' , returns_report = '" + TextData.cus_returns.ToString() + "' , receivables = '" + TextData.receivables.ToString() + "' , payables = '" + TextData.payables.ToString() + "' , recoveries = '" + TextData.cus_recoveries.ToString() + "' , balance_in_out = '" + TextData.balance_in_out.ToString() + "' , income_statement = '" + TextData.income_statement.ToString() + "'  , generateInvoices = '" + TextData.generateInvoices.ToString() + "'  , chequeDetails = '" + TextData.chequeDetails.ToString() + "' where (role_id = '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        public static void insert_forms_button_controls1()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = '" + TextData.role_title.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls1 where (role_id = '" + GetSetData.Ids.ToString() + "');";
                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_tbl_authorities_button_controls1 values ('" + TextData.supplier_detials_print.ToString() + "' , '" + TextData.supplier_detials_delete.ToString() + "' , '" + TextData.supplier_detials_new.ToString() + "' , '" + TextData.supplier_detials_modify.ToString() + "' , '" + TextData.supplier_detials_select.ToString() + "' , '" + TextData.suppliers_save.ToString() + "' , '" + TextData.suppliers_update.ToString() + "' , '" + TextData.suppliers_exit.ToString() + "' , '" + TextData.customer_detials_print.ToString() + "' , '" + TextData.customer_detials_delete.ToString() + "' , '" + TextData.customer_detials_new.ToString() + "' , '" + TextData.customer_detials_modify.ToString() + "' , '" + TextData.customer_detials_select.ToString() + "' , '" + TextData.customer_save.ToString() + "' , '" + TextData.customer_update.ToString() + "' , '" + TextData.customer_exit.ToString() + "' , '" + TextData.employee_detials_print.ToString() + "' , '" + TextData.employee_detials_delete.ToString() + "' , '" + TextData.employee_detials_new.ToString() + "' , '" + TextData.employee_detials_modify.ToString() + "' , '" + TextData.employee_detials_select.ToString() + "' , '" + TextData.employee_save.ToString() + "' , '" + TextData.employee_update.ToString() + "' , '" + TextData.employee_exit.ToString() + "' , '" + TextData.purchase_detials_print.ToString() + "' , '" + TextData.purchase_detials_delete.ToString() + "' , '" + TextData.purchase_detials_new.ToString() + "' , '" + TextData.purchase_detials_returns.ToString() + "' , '" + TextData.purchase_detials_refresh.ToString() + "' , '" + TextData.purchase_save.ToString() + "' , '" + TextData.purchase_print.ToString() + "' , '" + TextData.purchase_exit.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_tbl_authorities_button_controls1 set supplier_details_print =  '" + TextData.supplier_detials_print.ToString() + "' , supplier_details_delete = '" + TextData.supplier_detials_delete.ToString() + "' , supplier_details_new = '" + TextData.supplier_detials_new.ToString() + "' , supplier_details_modify = '" + TextData.supplier_detials_modify.ToString() + "' , supplier_details_select = '" + TextData.supplier_detials_select.ToString() + "' , suppliers_new = '" + TextData.suppliers_save.ToString() + "' , suppliers_update = '" + TextData.suppliers_update.ToString() + "' , suppliers_exit = '" + TextData.suppliers_exit.ToString() + "' , customer_details_print = '" + TextData.customer_detials_print.ToString() + "' , customer_details_delete = '" + TextData.customer_detials_delete.ToString() + "' , customer_details_new = '" + TextData.customer_detials_new.ToString() + "' , customer_details_modify = '" + TextData.customer_detials_modify.ToString() + "' , customer_details_select = '" + TextData.customer_detials_select.ToString() + "' , customers_save = '" + TextData.customer_save.ToString() + "' , customers_update = '" + TextData.customer_update.ToString() + "' , customers_exit = '" + TextData.customer_exit.ToString() + "' , employee_details_print = '" + TextData.employee_detials_print.ToString() + "' , employee_details_delete = '" + TextData.employee_detials_delete.ToString() + "' , employee_details_new = '" + TextData.employee_detials_new.ToString() + "' , employee_details_modify = '" + TextData.employee_detials_modify.ToString() + "' , employee_details_select = '" + TextData.employee_detials_select.ToString() + "' , employees_save = '" + TextData.employee_save.ToString() + "' , employees_update = '" + TextData.employee_update.ToString() + "' , employees_exit = '" + TextData.employee_exit.ToString() + "' , purchase_details_print = '" + TextData.purchase_detials_print.ToString() + "' , purchase_details_delete = '" + TextData.purchase_detials_delete.ToString() + "' , purchase_details_new = '" + TextData.purchase_detials_new.ToString() + "' , purchase_details_returns = '" + TextData.purchase_detials_returns.ToString() + "' , purchase_details_refresh = '" + TextData.purchase_detials_refresh.ToString() + "' , purchases_save = '" + TextData.purchase_save.ToString() + "' , purchases_print = '" + TextData.purchase_print.ToString() + "' , purchases_exit = '" + TextData.purchase_exit.ToString() + "'  where (role_id = '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        public static void insert_forms_button_controls2()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = '" + TextData.role_title.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls2 where (role_id = '" + GetSetData.Ids.ToString() + "');";
                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_tbl_authorities_button_controls2 values ('" + TextData.products_detials_print.ToString() + "' , '" + TextData.products_detials_delete.ToString() + "' , '" + TextData.products_detials_new.ToString() + "' , '" + TextData.products_detials_modify.ToString() + "' , '" + TextData.products_detials_regular.ToString() + "' , '" + TextData.products_detials_expired.ToString() + "' , '" + TextData.products_save.ToString() + "' , '" + TextData.products_update.ToString() + "' , '" + TextData.products_exit.ToString() + "' , '" + TextData.recovery_detials_print.ToString() + "' , '" + TextData.recovery_detials_new.ToString() + "' , '" + TextData.recovery_detials_delete.ToString() + "' , '" + TextData.recovery_detials_modify.ToString() + "' , '" + TextData.recovery_detials_Invoices.ToString() + "' , '" + TextData.recovery_save.ToString() + "' , '" + TextData.recovery_print.ToString() + "' , '" + TextData.recovery_exit.ToString() + "' , '" + TextData.expense_detials_print.ToString() + "' , '" + TextData.expense_detials_delete.ToString() + "' , '" + TextData.expense_detials_new.ToString() + "' , '" + TextData.expense_detials_modify.ToString() + "' , '" + TextData.expense_detials_refresh.ToString() + "' , '" + TextData.expense_save.ToString() + "' , '" + TextData.expense_update.ToString() + "' , '" + TextData.expense_exit.ToString() + "' , '" + TextData.dues_print.ToString() + "' , '" + TextData.dues_refresh.ToString() + "' , '" + TextData.dues_exit.ToString() + "' , '" + TextData.stock_whole.ToString() + "' , '" + TextData.stock_low.ToString() + "' , '" + TextData.stock_print.ToString() + "' , '" + TextData.stock_refresh.ToString() + "' , '" + TextData.stock_exit.ToString() + "' , '" + TextData.settings_registration.ToString() + "' , '" + TextData.settings_configuration.ToString() + "' , '" + TextData.settings_reports.ToString() + "' , '" + TextData.settings_login_details.ToString() + "' , '" + TextData.settings_general_options.ToString() + "' , '" + TextData.banking_details_print.ToString() + "' , '" + TextData.banking_details_delete.ToString() + "' , '" + TextData.banking_details_new.ToString() + "' , '" + TextData.banking_details_modify.ToString() + "' , '" + TextData.new_transaction_save.ToString() + "' , '" + TextData.new_transaction_update.ToString() + "' , '" + TextData.new_transaction_savePrint.ToString() + "' , '" + TextData.new_transaction_exit.ToString() + "' , '" + TextData.demands_list_print.ToString() + "' , '" + TextData.demands_list_delete.ToString() + "' , '" + TextData.demands_list_new.ToString() + "' , '" + TextData.demands_list_modify.ToString() + "' , '" + TextData.new_demand_save.ToString() + "' , '" + TextData.new_demand_update.ToString() + "' , '" + TextData.new_demand_savePrint.ToString() + "' , '" + TextData.new_demand_exit.ToString() + "', '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_tbl_authorities_button_controls2 set products_details_print =  '" + TextData.products_detials_print.ToString() + "' , products_details_delete = '" + TextData.products_detials_delete.ToString() + "' , products_details_new = '" + TextData.products_detials_new.ToString() + "' , products_details_modify = '" + TextData.products_detials_modify.ToString() + "' , products_details_regular = '" + TextData.products_detials_regular.ToString() + "' , products_expired = '" + TextData.products_detials_expired.ToString() + "' , products_save = '" + TextData.products_save.ToString() + "' , products_update = '" + TextData.products_update.ToString() + "' , products_exit = '" + TextData.products_exit.ToString() + "' , recovery_details_print = '" + TextData.recovery_detials_print.ToString() + "' , recovery_details_new = '" + TextData.recovery_detials_new.ToString() + "' , recovery_details_delete = '" + TextData.recovery_detials_delete.ToString() + "' , recovery_details_modify = '" + TextData.recovery_detials_modify.ToString() + "' , recovery_details_Invoices = '" + TextData.recovery_detials_Invoices.ToString() + "' , recoveries_save = '" + TextData.recovery_save.ToString() + "' , recoveries_print = '" + TextData.recovery_print.ToString() + "' , recoveries_exit = '" + TextData.recovery_exit.ToString() + "' , expenses_details_print = '" + TextData.expense_detials_print.ToString() + "' , expenses_details_delete = '" + TextData.expense_detials_delete.ToString() + "' , expenses_details_new = '" + TextData.expense_detials_new.ToString() + "' , expenses_details_modify = '" + TextData.expense_detials_modify.ToString() + "' , expenses_details_refresh = '" + TextData.expense_detials_refresh.ToString() + "' , expenses_save = '" + TextData.expense_save.ToString() + "' , expenses_update = '" + TextData.expense_update.ToString() + "' , expenses_exit = '" + TextData.expense_exit.ToString() + "' , dues_print = '" + TextData.dues_print.ToString() + "' , dues_refresh = '" + TextData.dues_refresh.ToString() + "' , dues_exit = '" + TextData.dues_exit.ToString() + "' , stock_whole = '" + TextData.stock_whole.ToString() + "' , stock_low = '" + TextData.stock_low.ToString() + "' , stock_print = '" + TextData.stock_print.ToString() + "' , stock_refresh = '" + TextData.stock_refresh.ToString() + "' , stock_exit = '" + TextData.stock_exit.ToString() + "' , settings_reg = '" + TextData.settings_registration.ToString() + "' , settings_config = '" + TextData.settings_configuration.ToString() + "' , settings_reports = '" + TextData.settings_reports.ToString() + "' , settings_login_details = '" + TextData.settings_login_details.ToString() + "' , settings_general = '" + TextData.settings_general_options.ToString() + "' , banking_details_print = '" + TextData.banking_details_print.ToString() + "' , banking_details_delete = '" + TextData.banking_details_delete.ToString() + "' , banking_details_new = '" + TextData.banking_details_new.ToString() + "' , banking_details_modify = '" + TextData.banking_details_modify.ToString() + "' , new_transaction_save = '" + TextData.new_transaction_save.ToString() + "' , new_transaction_update = '" + TextData.new_transaction_update.ToString() + "' , new_transaction_savePrint = '" + TextData.new_transaction_savePrint.ToString() + "' , new_transaction_exit = '" + TextData.new_transaction_exit.ToString() + "' , demand_list_print = '" + TextData.demands_list_print.ToString() + "' , demand_list_delete = '" + TextData.demands_list_delete.ToString() + "' , demand_list_new = '" + TextData.demands_list_new.ToString() + "' , demand_list_modify = '" + TextData.demands_list_modify.ToString() + "' , new_demand_save = '" + TextData.new_demand_save.ToString() + "' , new_demand_update = '" + TextData.new_demand_update.ToString() + "' , new_demand_savePrint = '" + TextData.new_demand_savePrint.ToString() + "' , new_demand_exit = '" + TextData.new_demand_exit.ToString() + "' where (role_id = '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        public static void insert_forms_button_controls3()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = '" + TextData.role_title.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls3 where (role_id = '" + GetSetData.Ids.ToString() + "');";
                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_tbl_authorities_button_controls3 values ('" + TextData.investor_details_print.ToString() + "' , '" + TextData.investor_details_delete.ToString() + "' , '" + TextData.investor_details_new.ToString() + "' , '" + TextData.investor_details_modify.ToString() + "' , '" + TextData.newInvestor_save.ToString() + "' , '" + TextData.newInvestor_update.ToString() + "' , '" + TextData.newInvestor_barcode.ToString() + "' , '" + TextData.investorPaybook_details_print.ToString() + "' , '" + TextData.investorPaybook_details_delete.ToString() + "' , '" + TextData.investorPaybook_details_new.ToString() + "' , '" + TextData.investorPaybook_details_modify.ToString() + "' , '" + TextData.newInvestorPayment_save.ToString() + "' , '" + TextData.newInvestorPayment_update.ToString() + "' , '" + TextData.newInvestorPayment_savePrint.ToString() + "' , '" + TextData.guarantor_details_print.ToString() + "' , '" + TextData.guarantor_details_delete.ToString() + "' , '" + TextData.guarantor_details_new.ToString() + "' , '" + TextData.guarantor_details_modify.ToString() + "' , '" + TextData.newGuarantor_save.ToString() + "' , '" + TextData.newGuarantor_update.ToString() + "' , '" + TextData.newGuarantor_barcode.ToString() + "' , '" + TextData.customerOrders_print.ToString() + "' , '" + TextData.customerOrders_delete.ToString() + "' , '" + TextData.customerOrders_modify.ToString() + "' , '" + TextData.customerOrders_allSales.ToString() + "' , '" + TextData.customerOrders_allReturns.ToString() + "' , '" + TextData.customerOrders_search.ToString() + "' , '" + TextData.customerOrders_contractForm.ToString() + "' , '" + TextData.salariesPaybook_details_print.ToString() + "' , '" + TextData.salariesPaybook_details_delete.ToString() + "' , '" + TextData.salariesPaybook_details_new.ToString() + "' , '" + TextData.salariesPaybook_details_modify.ToString() + "' , '" + TextData.newSalaryPayment_save.ToString() + "' , '" + TextData.newSalaryPayment_update.ToString() + "' , '" + TextData.newSalaryPayment_savePrint.ToString() + "' , '" + TextData.bankLoan_details_print.ToString() + "' , '" + TextData.bankLoan_details_delete.ToString() + "' , '" + TextData.bankLoan_details_new.ToString() + "' , '" + TextData.bankLoan_details_modify.ToString() + "' , '" + TextData.newBankLoan_save.ToString() + "' , '" + TextData.newBankLoan_update.ToString() + "' , '" + TextData.newBankLoan_exit.ToString() + "' , '" + TextData.bankLoanPaybook_details_print.ToString() + "' , '" + TextData.bankLoanPaybook_details_delete.ToString() + "' , '" + TextData.bankLoanPaybook_details_new.ToString() + "' , '" + TextData.bankLoanPaybook_details_modify.ToString() + "' , '" + TextData.newBankLoanPayment_save.ToString() + "' , '" + TextData.newBankLoanPayment_update.ToString() + "' , '" + TextData.newBankLoanPayment_exit.ToString() + "' ,  '" + TextData.SupplierPaybook_details_print.ToString() + "' ,  '" + TextData.SupplierPaybook_details_delete.ToString() + "' , '" + TextData.SupplierPaybook_details_new.ToString() + "' , '" + TextData.SupplierPaybook_details_modify.ToString() + "' , '" + TextData.newSupplierPayment_save.ToString() + "' , '" + TextData.newSupplierPayment_update.ToString() + "' ,  '" + TextData.newSupplierPayment_exit.ToString() + "' , '" + TextData.CharityPaybook_details_print.ToString() + "' , '" + TextData.CharityPaybook_details_delete.ToString() + "' , '" + TextData.CharityPaybook_details_new.ToString() + "' , '" + TextData.CharityPaybook_details_modify.ToString() + "' , '" + TextData.newCharityPayment_save.ToString() + "' , '" + TextData.newCharityPayment_update.ToString() + "' , '" + TextData.newCharityPayment_exit.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    GetSetData.query = @"update pos_tbl_authorities_button_controls3 set investor_details_print = '" + TextData.investor_details_print.ToString() + "' , investor_details_delete = '" + TextData.investor_details_delete.ToString() + "' , investor_details_new = '" + TextData.investor_details_new.ToString() + "' , investor_details_modify = '" + TextData.investor_details_modify.ToString() + "' , new_investor_save = '" + TextData.newInvestor_save.ToString() + "' , new_investor_update = '" + TextData.newInvestor_update.ToString() + "' , new_investor_barcode = '" + TextData.newInvestor_barcode.ToString() + "' , investor_paybookDetails_print = '" + TextData.investorPaybook_details_print.ToString() + "' , investor_paybookDetails_delete = '" + TextData.investorPaybook_details_delete.ToString() + "' , investor_paybookDetails_new = '" + TextData.investorPaybook_details_new.ToString() + "' , investor_paybookDetails_modify = '" + TextData.investorPaybook_details_modify.ToString() + "' , new_investorPayment_save = '" + TextData.newInvestorPayment_save.ToString() + "' , new_investorPayment_update = '" + TextData.newInvestorPayment_update.ToString() + "' , new_investorPayment_savePrint = '" + TextData.newInvestorPayment_savePrint.ToString() + "' , guarantor_details_print = '" + TextData.guarantor_details_print.ToString() + "' , guarantor_details_delete = '" + TextData.guarantor_details_delete.ToString() + "' , guarantor_details_new = '" + TextData.guarantor_details_new.ToString() + "' , guarantor_details_modify = '" + TextData.guarantor_details_modify.ToString() + "' , new_guarantor_save = '" + TextData.newGuarantor_save.ToString() + "' , new_guarantor_update = '" + TextData.newGuarantor_update.ToString() + "' , new_guarantor_barcode = '" + TextData.newGuarantor_barcode.ToString() + "' , customerOrders_print = '" + TextData.customerOrders_print.ToString() + "' , customerOrders_delete = '" + TextData.customerOrders_delete.ToString() + "' , customerOrders_modify = '" + TextData.customerOrders_modify.ToString() + "' , customerOrders_allSales = '" + TextData.customerOrders_allSales.ToString() + "' , customerOrders_allReturns = '" + TextData.customerOrders_allReturns.ToString() + "' ,  customerOrders_search = '" + TextData.customerOrders_search.ToString() + "' , customerOrders_contractForm = '" + TextData.customerOrders_contractForm.ToString() + "' , salariesPaybook_details_print = '" + TextData.salariesPaybook_details_print.ToString() + "' , salariesPaybook_details_delete = '" + TextData.salariesPaybook_details_delete.ToString() + "' , salariesPaybook_details_new = '" + TextData.salariesPaybook_details_new.ToString() + "' , salariesPaybook_details_modify = '" + TextData.salariesPaybook_details_modify.ToString() + "' , new_salaryPayment_save = '" + TextData.newSalaryPayment_save.ToString() + "' , new_salaryPayment_update = '" + TextData.newSalaryPayment_update.ToString() + "' , new_salaryPayment_savePrint = '" + TextData.newSalaryPayment_savePrint.ToString() + "' , bankLoan_details_print = '" + TextData.bankLoan_details_print.ToString() + "' , bankLoan_details_delete = '" + TextData.bankLoan_details_delete.ToString() + "' , bankLoan_details_new = '" + TextData.bankLoan_details_new.ToString() + "' , bankLoan_details_modify = '" + TextData.bankLoan_details_modify.ToString() + "' , new_bankLoan_save = '" + TextData.newBankLoan_save.ToString() + "' , new_bankLoan_update = '" + TextData.newBankLoan_update.ToString() + "' , new_bankLoan_exit = '" + TextData.newBankLoan_exit.ToString() + "' , bankLoanPaybook_details_print = '" + TextData.bankLoanPaybook_details_print.ToString() + "' , bankLoanPaybook_details_delete = '" + TextData.bankLoanPaybook_details_delete.ToString() + "' , bankLoanPaybook_details_new = '" + TextData.bankLoanPaybook_details_new.ToString() + "' , bankLoanPaybook_details_modify = '" + TextData.bankLoanPaybook_details_modify.ToString() + "' , new_bankLoanPayment_save = '" + TextData.newBankLoanPayment_save.ToString() + "' , new_bankLoanPayment_update = '" + TextData.newBankLoanPayment_update.ToString() + "' , new_bankLoanPayment_exit = '" + TextData.newBankLoanPayment_exit.ToString() + "' ,  supplierPaybook_print = '" + TextData.SupplierPaybook_details_print.ToString() + "' ,  supplierPaybook_delete = '" + TextData.SupplierPaybook_details_delete.ToString() + "' , supplierPaybook_new = '" + TextData.SupplierPaybook_details_new.ToString() + "' , supplierPaybook_modify = '" + TextData.SupplierPaybook_details_modify.ToString() + "' , newSupplierPayment_save = '" + TextData.newSupplierPayment_save.ToString() + "' , newSupplierPayment_update = '" + TextData.newSupplierPayment_update.ToString() + "' ,  newSupplierPayment_exit = '" + TextData.newSupplierPayment_exit.ToString() + "' , charityPaybook_print = '" + TextData.CharityPaybook_details_print.ToString() + "' , charityPaybook_delete = '" + TextData.CharityPaybook_details_delete.ToString() + "' , charityPaybook_new = '" + TextData.CharityPaybook_details_new.ToString() + "' , charityPaybook_modify = '" + TextData.CharityPaybook_details_modify.ToString() + "' , newCharityPayment_save = '" + TextData.newCharityPayment_save.ToString() + "' , newCharityPayment_update = '" + TextData.newCharityPayment_update.ToString() + "' , newCharityPayment_exit = '" + TextData.newCharityPayment_exit.ToString() + "' where (role_id = '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        public static void insert_general_settings()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Ids = data.UserPermissionsIds("id", "pos_general_settings");

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_general_settings values ('" + TextData.backup_path.ToString() + "' , '" + TextData.pic_path.ToString() + "' , '" + TextData.auto_backup.ToString() + "' , '" + TextData.show_graph.ToString() + "' , '" + TextData.page_size.ToString() + "' , '" + TextData.pos_security.ToString() + "' , '" + TextData.auto_expiry.ToString() + "' , '" + TextData.box_notifications.ToString() + "' , '" + TextData.show_discount.ToString() + "' , '" + TextData.box_discount.ToString() + "' , '" + TextData.show_price.ToString() + "' , '" + TextData.show_tax.ToString() + "' , '" + TextData.show_hold.ToString() + "' , '" + TextData.show_unhold.ToString() + "' , '" + TextData.show_order.ToString() + "' , '" + TextData.show_last_order.ToString() + "' , '" + TextData.show_remarks.ToString() + "' , '" + TextData.show_recovery.ToString() + "' , '" + TextData.show_print_bill.ToString() + "' , '" + TextData.directly_print.ToString() + "' , '" + TextData.sale_df_option.ToString() + "', '" + TextData.DiscountPercentage + "', '" + TextData.autoPenalties + "', '" + TextData.useCapitalAmount + "', '" + TextData.show_guarantors + "', '" + TextData.show_installmentPlan + "', '" + TextData.showNoteInReport + "', '" + TextData.investorProfit + "', '" + TextData.lessInvestorAmount + "', '" + TextData.profitDivide + "', '" + TextData.employeeSalary + "', '" + TextData.CompanyProfit + "', '" + TextData.currency + "', '" + TextData.taxation + "', '" + TextData.default_printer + "', '" + TextData.preview_receipt + "', '" + TextData.printerModel + "', '" + TextData.searchBox + "', '" + TextData.autoOpenCashDrawer + "' , '" + TextData.strictly_check_expiry + "' , '" + TextData.autoSetPoints + "' , '" + TextData.autoRedeemPoints + "' , '" + TextData.addPointPerAmount + "' , '" + TextData.pointsRedeemLimit + "' , '" + TextData.autoPromotions + "' , '" + TextData.promotionOnItems + "' , '" + TextData.promotionDiscount + "' , '" + TextData.auto_clear_cart + "' , '" + TextData.split_old_and_new_stock + "' , '" + TextData.pointsRedeemDiscount + "', '" + TextData.pointsDiscountInPercentage.ToString() + "', '" + TextData.allowAverageCost + "', '" + TextData.autoZeroCustomerDiscount + "', '" + TextData.singleAuthorityClosing + "', '" + TextData.batchOpeningAmount + "', '" + TextData.setStockLimitToZero + "', '" + TextData.isCreditCardConnected + "', '" + TextData.notificationSound + "', '" + TextData.changeAmountPopUp + "', '" + TextData.customerAgeLimit + "', '" + TextData.salesmanTips + "', '" + TextData.useSurcharges + "', '" + TextData.surchargePercentage + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_general_settings set backup_path = '" + TextData.backup_path.ToString() + "' , picture_path = '" + TextData.pic_path.ToString() + "' , auto_backup = '" + TextData.auto_backup.ToString() + "' , show_graphs = '" + TextData.show_graph.ToString() + "' , page_size = '" + TextData.page_size.ToString() + "' , pos_security = '" + TextData.pos_security.ToString() + "' , auto_expiry = '" + TextData.auto_expiry.ToString() + "' , show_notifications = '" + TextData.box_notifications.ToString() + "' , show_discount = '" + TextData.show_discount.ToString() + "' , discount_box = '" + TextData.box_discount.ToString() + "' , price_box = '" + TextData.show_price.ToString() + "' , tax_box = '" + TextData.show_tax.ToString() + "' , show_hold = '" + TextData.show_hold.ToString() + "' , show_unhold = '" + TextData.show_unhold.ToString() + "' , show_order = '" + TextData.show_order.ToString() + "' , show_last_order = '" + TextData.show_last_order.ToString() + "' , show_remarks = '" + TextData.show_remarks.ToString() + "' , show_recovery = '" + TextData.show_recovery.ToString() + "' , show_print_receipt = '" + TextData.show_print_bill.ToString() + "' , directly_print_receipt = '" + TextData.directly_print.ToString() + "' , sale_default_option = '" + TextData.sale_df_option.ToString() + "', discountType = '" + TextData.DiscountPercentage + "', autoPenalties = '" + TextData.autoPenalties + "', useCapital = '" + TextData.useCapitalAmount + "', show_guarantors = '" + TextData.show_guarantors + "', show_installmentPlan = '" + TextData.show_installmentPlan + "', showNoteInReport = '" + TextData.showNoteInReport + "' , investorProfit = '" + TextData.investorProfit + "' , lessAmount = '" + TextData.lessInvestorAmount + "' , profitDivide = '" + TextData.profitDivide + "' , employeeSalary = '" + TextData.employeeSalary + "' , companyProfit = '" + TextData.CompanyProfit + "', currency = '" + TextData.currency + "', taxation = '" + TextData.taxation + "', default_printer = '" + TextData.default_printer + "', preview_receipt = '" + TextData.preview_receipt + "', printer_model = '" + TextData.printerModel + "', search_box = '" + TextData.searchBox + "', auto_open_cash_drawer = '" + TextData.autoOpenCashDrawer + "' , strictly_check_expiry = '" + TextData.strictly_check_expiry + "' , autoSetPoints = '" + TextData.autoSetPoints + "' , autoRedeemPoints  = '" + TextData.autoRedeemPoints + "' , addPointPerAmount = '" + TextData.addPointPerAmount + "' , pointsRedeemLimit = '" + TextData.pointsRedeemLimit + "' , autoPromotions = '" + TextData.autoPromotions + "' , promotionOnItems = '" + TextData.promotionOnItems + "' , promotionDiscount  = '" + TextData.promotionDiscount + "' , auto_clear_cart  = '" + TextData.auto_clear_cart + "' , split_old_and_new_stock = '" + TextData.split_old_and_new_stock + "' , pointsRedeemDiscount = '" + TextData.pointsRedeemDiscount + "', pointsDiscountInPercentage = '" + TextData.pointsDiscountInPercentage.ToString() + "', allowAverageCost = '" + TextData.allowAverageCost + "', autoZeroCustomerDiscount = '" + TextData.autoZeroCustomerDiscount + "', singleAuthorityClosing = '" + TextData.singleAuthorityClosing + "', batchOpeningAmount = '" + TextData.batchOpeningAmount + "', setStockLimitToZero = '" + TextData.setStockLimitToZero + "', isCreditCardConnected = '" + TextData.isCreditCardConnected + "', notificationSound = '" + TextData.notificationSound + "', changeAmountPopUp = '" + TextData.changeAmountPopUp + "', customerAgeLimit = '" + TextData.customerAgeLimit + "', salesmanTips = '" + TextData.salesmanTips + "', useSurcharges = '" + TextData.useSurcharges + "', surchargePercentage = '" + TextData.surchargePercentage + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }

        public static void insert_contractPolicies_settings()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.Ids = data.UserPermissionsIds("id", "pos_contractPolicies");

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_contractPolicies values ('" + TextData.backup_path.ToString() + "' , '" + TextData.pic_path.ToString() + "' , '" + TextData.auto_backup.ToString() + "' , '" + TextData.show_graph.ToString() + "' , '" + TextData.page_size.ToString() + "' , '" + TextData.pos_security.ToString() + "' , '" + TextData.auto_expiry.ToString() + "' , '" + TextData.box_notifications.ToString() + "' , '" + TextData.show_discount.ToString() + "' , '" + TextData.box_discount.ToString() + "' , '" + TextData.show_price.ToString() + "' , '" + TextData.show_tax.ToString() + "' , '" + TextData.show_hold.ToString() + "' , '" + TextData.show_unhold.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_contractPolicies set condition1 = '" + TextData.backup_path.ToString() + "' , condition2 = '" + TextData.pic_path.ToString() + "' , condition3 = '" + TextData.auto_backup.ToString() + "' , condition4 = '" + TextData.show_graph.ToString() + "' , condition5 = '" + TextData.page_size.ToString() + "' , condition6 = '" + TextData.pos_security.ToString() + "' , condition7 = '" + TextData.auto_expiry.ToString() + "' , condition8 = '" + TextData.box_notifications.ToString() + "' , condition9 = '" + TextData.show_discount.ToString() + "' , condition10 = '" + TextData.box_discount.ToString() + "' , condition11 = '" + TextData.show_price.ToString() + "' , condition12 = '" + TextData.show_tax.ToString() + "' , condition13 = '" + TextData.show_hold.ToString() + "' , condition14 = '" + TextData.show_unhold.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }
    }
}
