using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard.Forms;
using Settings_info.forms;
using System.Data.SqlClient;
using WebConfig;
using Datalayer;
using Message_box_info.forms;
using Settings_info.controllers;
using Login_info.controllers;
using Login_info.forms;

namespace Settings_info.controllers
{
    public class buttonControls
    {
        public static void mainMenu_buttons()
        {
            Menus main = new Menus();
            main.Show();
        }

        public static void registration_buttons()
        {
            Registration_form registration = new Registration_form();
            registration.ShowDialog();
        }

        public static string fun_title_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_title_db_db = "select title from tbl_Reports;";
                string title_db = data.select_scl_details_db(quer_get_title_db_db);

                return title_db;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);

                return "";
            }
        }

        public static string fun_sub_title_db()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_sub_title_db_db = "select sub_title from tbl_Reports;";
                string sub_title_db = data.select_scl_details_db(quer_get_sub_title_db_db);

                return sub_title_db;
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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_sub_title_db_db = "select message from tbl_Reports;";
                string sub_title_db = data.select_scl_details_db(quer_get_sub_title_db_db);

                return sub_title_db;
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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_sub_title_db_db = "select copyrights from tbl_Reports;";
                string sub_title_db = data.select_scl_details_db(quer_get_sub_title_db_db);

                return sub_title_db;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);

                return "";
            }
        }

        public static void insert_business_title_tables()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_title_db = "select business_id from businessTitles where title = '" + TextData.business_type.ToString() +"';";
                int title_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_title_db);

                if (title_db == 0)
                {
                    if (TextData.business_type != "")
                    {
                        string update_business_title_db = @"insert into businessTitles (title) values ('" + TextData.business_type.ToString() + "');";
                        data.insertUpdateCreateOrDelete(update_business_title_db);

                        done.DoneMessage("Saved!");
                        done.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage(TextData.business_type.ToString() + " already exist!");
                    error.ShowDialog();
                }
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void update_Configuration_details()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                if (TextData.comments == "")
                {
                    TextData.comments = "nill!";
                }

                string quer_get_configure_id_db = "select config_id from configuration where config_id = '1';";
                int configure_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_configure_id_db);

                if (configure_id_db == 0)
                {
                    string update_configurations_db = @"insert into configuration (shop_name, owner_name, city, address, business_nature, branch, shop_no, phone_1, phone_2, logo_path, comments) values ('" + TextData.shop_name.ToString() + "' , '" + TextData.owner_name.ToString() + "' , '" + TextData.city.ToString() + "' , '" + TextData.address.ToString() + "' , '" + TextData.business_type.ToString() + "' , '" + TextData.branch.ToString() + "' , '" + TextData.shop_no.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.phone2.ToString() + "' , '" + TextData.logo_path.ToString() + "' , '" + TextData.comments.ToString() + "');";
                    data.insertUpdateCreateOrDelete(update_configurations_db);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    string update_configurations_db = @"update configuration set shop_name = '" + TextData.shop_name.ToString() + "' , owner_name = '" + TextData.owner_name.ToString() + "' , city = '" + TextData.city.ToString() + "' , address = '" + TextData.address.ToString() + "' , business_nature = '" + TextData.business_type.ToString() + "' , branch = '" + TextData.branch.ToString() + "' , shop_no = '" + TextData.shop_no.ToString() + "' , phone_1 = '" + TextData.phone1.ToString() + "' , phone_2 = '" + TextData.phone2.ToString() + "' , logo_path = '" + TextData.logo_path.ToString() + "' , comments = '" + TextData.comments.ToString() + "' where config_id = '1';";
                    data.insertUpdateCreateOrDelete(update_configurations_db);

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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_report_id_db = "select report_id from tbl_Reports where report_id = '1';";
                int report_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_report_id_db);
                
                if(TextData.title != "")
                {
                    if (TextData.sub_title != "")
                    {
                        if (TextData.message != "")
                        {
                            if(TextData.copyrights != "")
                            {
                                if (report_id_db == 0)
                                {
                                    string quer_insert_data_into_reports_db = @"insert into tbl_Reports (title, sub_title, message, copyrights) values ('" + TextData.title.ToString() + "' , '" + TextData.sub_title.ToString() + "' , '" + TextData.message.ToString() + "' , '" + TextData.copyrights.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(quer_insert_data_into_reports_db);

                                    done.DoneMessage("Successfully Saved!");
                                    done.ShowDialog();
                                }
                                else
                                {
                                    string quer_insert_data_into_Reports_db = @"update tbl_Reports set title = '" + TextData.title.ToString() + "' , sub_title = '" + TextData.sub_title.ToString() + "' , message = '" + TextData.message.ToString() + "' , copyrights = '" + TextData.copyrights.ToString() + "' where report_id = '1';";
                                    data.insertUpdateCreateOrDelete(quer_insert_data_into_Reports_db);

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
                            error.errorMessage("Message field is empty!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Sub Title field is empty!");
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

        public static void Registration_info()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string UserName_db = "";

                string quer_get_Username_db = @"select username from Role;";
                data.Connect();

                data.cmd_ = new SqlCommand(quer_get_Username_db, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                if (data.reader_.Read())
                {
                    UserName_db = data.reader_[0].ToString();
                    data.reader_.Close();
                }
                else if (!data.reader_.Read())
                {
                    UserName_db = "";
                    data.reader_.Close();
                    data.Disconnect();
                }

                if (TextData.username != UserName_db)
                {
                    data.Connect();

                    if (TextData.person_name != "" && TextData.role_title != "")
                    {
                        if (TextData.password == TextData.confirm_password)
                        {
                            string quer_insert_data_into_registration_db = @"insert into registration (name, roleTitle) values ('" + TextData.person_name.ToString() + "' , '" + TextData.role_title.ToString() + "');";
                            data.insertUpdateCreateOrDelete(quer_insert_data_into_registration_db);


                            string quer_get_reg_id_db = @"select reg_id from registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
                            int reg_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_reg_id_db);

                            string quer_insert_data_into_db = @"insert into Role (username , password, reg_id) values ('" + TextData.username.ToString() + "' , '" + TextData.password.ToString() + "' , '" + reg_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(quer_insert_data_into_db);
                        }
                        else
                        {
                            error.errorMessage("The password is not matching!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please Fill all the Fields!");
                        error.ShowDialog();
                    }

                }
                else
                {
                    error.errorMessage("Password is not Matching!");
                    error.ShowDialog();
                }

            }
            catch (SqlException es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
            finally
            {
                data.Disconnect();
            }
        }

        public static void insert_dashboard_button_controls()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_reg_id_db = @"select reg_id from registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() +"';";
                int reg_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_reg_id_db);

                string quer_get_role_id_db = @"select role_id from Role where reg_id = '" + reg_id_db.ToString() +"';";
                int role_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_role_id_db);

                if (role_id_db != 0)
                {
                    string quer_insert_data_into_tbl_authorities_dashboard_db = @"insert into tbl_authorities_dashboard (loader, loader_return, loyal_customer, loyal_cus_return, products, purchases, expenses, company_payments, paybook, recoveries, customers, suppliers, customer_dues, pos, logout, backups,  s, settings, notifications, about, role_id) values ('" + TextData.loader.ToString() + "' , '" + TextData.loader_returns.ToString() + "' , '" + TextData.loyal_customer.ToString() + "' , '" + TextData.loyal_returns.ToString() + "' , '" + TextData.products.ToString() + "' , '" + TextData.purchases.ToString() + "' , '" + TextData.expenses.ToString() + "' , '" + TextData.company_reg.ToString() + "' , '" + TextData.paybook.ToString() + "' , '" + TextData.recoveries.ToString() + "' , '" + TextData.customers.ToString() + "' , '" + TextData.suppliers.ToString() + "' , '" + TextData.customer_dues.ToString() + "' , '" + TextData.pos.ToString() + "' , '" + TextData.logOut.ToString() + "' , '" + TextData.backup.ToString() + "'  , '" + TextData.restore.ToString() + "' , '" + TextData.settings.ToString() + "' , '" + TextData.notifications.ToString() + "' , '" + TextData.about.ToString() + "' , '" + role_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(quer_insert_data_into_tbl_authorities_dashboard_db);
                }
                else
                {
                    error.errorMessage(TextData.person_name.ToString() + " does not exist!");
                    error.ShowDialog();
                }

            }
            catch (SqlException es)
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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_reg_id_db = @"select reg_id from registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
                int reg_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_reg_id_db);

                string quer_get_role_id_db = @"select role_id from Role where reg_id = '" + reg_id_db.ToString() + "';";
                int role_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_role_id_db);

                if (role_id_db != 0)
                {
                    string quer_insert_data_into_tbl_authorities_reports_db = @"insert into tbl_authorities_reports (customer_ledger, customer_sales, customer_return, loader_reports, day_book, stock, recoveries, company_payments, receivables, payables, balance_in_out, income_statement, company_ledger, role_id) values ('" + TextData.cus_ledger.ToString() + "' , '" + TextData.cus_sales.ToString() + "' , '" + TextData.cus_returns.ToString() + "' , '" + TextData.loader_reports.ToString() + "' , '" + TextData.day_book.ToString() + "' , '" + TextData.stock.ToString() + "' , '" + TextData.cus_recoveries.ToString() + "' , '" + TextData.company_payments.ToString() + "' , '" + TextData.receivables.ToString() + "' , '" + TextData.payables.ToString() + "' , '" + TextData.balance_in_out.ToString() + "' , '" + TextData.income_statement.ToString() + "' , '" + TextData.company_ledger.ToString() + "' , '" + role_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(quer_insert_data_into_tbl_authorities_reports_db);

                    done.DoneMessage("Inserted Successfully!");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage(TextData.person_name.ToString() + " does not exist!");
                    error.ShowDialog();
                }

            }
            catch (SqlException es)
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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_reg_id_db = @"select reg_id from registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
                int reg_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_reg_id_db);

                string quer_get_role_id_db = @"select role_id from Role where reg_id = '" + reg_id_db.ToString() + "';";
                int role_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_role_id_db);

                if (role_id_db != 0)
                {
                    string quer_insert_data_into_tbl_authorities_button_controls1_db = @"insert into tbl_authorities_button_controls1 (company_reg_save, company_reg_update, company_reg_print, company_reg_new, company_reg_refresh, corporate_business_save, corporate_business_save_print, corporate_business_exit, customers_save, customers_update, customers_print, customers_new, customers_refresh, expenses_save, expenses_update, expenses_print, expenses_new, expenses_refresh, products_save, products_update, products_print, products_new, products_refresh, purchases_save, purchases_update, purchases_print, purchases_new, purchases_refresh, recoveries_save, recoveries_save_print, recoveries_exit, customer_dues_print, customer_dues_refresh, customer_dues_exit , role_id) values ('" + TextData.company_reg_save.ToString() + "' , '" + TextData.company_reg_update.ToString() + "' , '" + TextData.company_reg_print.ToString() + "' , '" + TextData.company_reg_new.ToString() + "' , '" + TextData.company_reg_refresh.ToString() + "' , '" + TextData.corporate_business_save.ToString() + "' , '" + TextData.corporate_business_save_print.ToString() + "' , '" + TextData.corporate_business_exit.ToString() + "' , '" + TextData.customers_save.ToString() + "' , '" + TextData.customers_update.ToString() + "' , '" + TextData.customers_print.ToString() + "' , '" + TextData.customers_new.ToString() + "' , '" + TextData.customers_refresh.ToString() + "' , '" + TextData.expenses_save.ToString() + "' , '" + TextData.expenses_update.ToString() + "' , '" + TextData.expenses_print.ToString() + "' , '" + TextData.expenses_new.ToString() + "' , '" + TextData.expenses_refresh.ToString() + "' , '" + TextData.products_save.ToString() + "' , '" + TextData.products_update.ToString() + "' , '" + TextData.products_print.ToString() + "' , '" + TextData.products_new.ToString() + "' , '" + TextData.products_refresh.ToString() + "' , '" + TextData.purchases_save.ToString() + "' , '" + TextData.purchases_update.ToString() + "' , '" + TextData.purchases_print.ToString() + "' , '" + TextData.purchases_new.ToString() + "' , '" + TextData.purchases_refresh.ToString() + "' , '" + TextData.recoveries_save.ToString() + "' , '" + TextData.recoveries_save_print.ToString() + "' , '" + TextData.recoveries_exit.ToString() + "' , '" + TextData.customer_dues_print.ToString() + "' , '" + TextData.customer_dues_refresh.ToString() + "' , '" + TextData.customer_dues_exit.ToString() + "' , '" + role_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(quer_insert_data_into_tbl_authorities_button_controls1_db);

                    done.DoneMessage("Inserted Successfully!");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage(TextData.person_name.ToString() + " does not exist!");
                    error.ShowDialog();
                }

            }
            catch (SqlException es)
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
            datalayer data = new datalayer(webConfig.con_string);

            try
            {
                string quer_get_reg_id_db = @"select reg_id from registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
                int reg_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_reg_id_db);

                string quer_get_role_id_db = @"select role_id from Role where reg_id = '" + reg_id_db.ToString() + "';";
                int role_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(quer_get_role_id_db);

                if (role_id_db != 0)
                {
                    string quer_insert_data_into_tbl_authorities_button_controls2_db = @"insert into tbl_authorities_button_controls2 (pos_save, pos_update, pos_save_print, pos_exit, loyal_cus_sales_save, loyal_cus_sales_update, loyal_cus_sales_save_print, loyal_cus_sales_exit, loader_prod_return_save, loader_prod_return_save_print, loader_prod_return_exit, loader_cus_sales_save, loader_cus_sales_save_print, loader_cus_sales_exit, loader_prod_save, loader_prod_save_print, loader_prod_exit, settings_reg, settings_config, settings_reports, settings_authority, whole_low_stock_print, whole_low_stock_refresh, whole_low_stock_exit, suppliers_save, suppliers_update, suppliers_print, suppliers_new, suppliers_refresh , role_id) values ('" + TextData.pos_save.ToString() + "' , '" + TextData.pos_update.ToString() + "' , '" + TextData.pos_save_print.ToString() + "' , '" + TextData.pos_exit.ToString() + "' , '" + TextData.loyal_cus_sales_save.ToString() + "' , '" + TextData.loyal_cus_sales_update.ToString() + "' , '" + TextData.loyal_cus_sales_save_print.ToString() + "' , '" + TextData.loyal_cus_sales_exit.ToString() + "' , '" + TextData.loader_prod_return_save.ToString() + "' , '" + TextData.loader_prod_return_save_print.ToString() + "' , '" + TextData.loader_prod_return_exit.ToString() + "' , '" + TextData.loader_cus_sales_save.ToString() + "' , '" + TextData.loader_cus_sales_save_print.ToString() + "' , '" + TextData.loader_cus_sales_exit.ToString() + "' , '" + TextData.loader_prod_save.ToString() + "' , '" + TextData.loader_prod_save_print.ToString() + "' , '" + TextData.loader_prod_exit.ToString() + "' , '" + TextData.settings_reg.ToString() + "' , '" + TextData.settings_config.ToString() + "' , '" + TextData.settings_reports.ToString() + "' , '" + TextData.settings_authority.ToString() + "' , '" + TextData.whole_low_stock_print.ToString() + "' , '" + TextData.whole_low_stock_refresh.ToString() + "' , '" + TextData.whole_low_stock_exit.ToString() + "' , '" + TextData.suppliers_save.ToString() + "' , '" + TextData.suppliers_update.ToString() + "' , '" + TextData.suppliers_print.ToString() + "' , '" + TextData.suppliers_new.ToString() + "' , '" + TextData.suppliers_refresh.ToString() + "' , '" + role_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(quer_insert_data_into_tbl_authorities_button_controls2_db);

                    done.DoneMessage("Inserted Successfully!");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage(TextData.person_name.ToString() + " does not exist!");
                    error.ShowDialog();
                }

            }
            catch (SqlException es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }
    }
}
