using System;
using System.Data.SqlClient;
using Datalayer;
using Spices_pos.LoginInfo.forms;
using Message_box_info.forms;
using RefereningMaterial;
using CounterSales_info.forms;
using System.Threading;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DashboardInfo.Forms;

namespace Spices_pos.LoginInfo.controllers
{
    public class Button_controls
    {
        public static void MenuScreen()
        {
            Menus main = new Menus();
            main.Show();
        }

        public static void CapitalHistoryDetails()
        {
            formCapitalHistoryDetails expenses_detail = new formCapitalHistoryDetails();
            expenses_detail.Show();
        }

        public static void makeSaleScreen()
        {
            form_counter_sales main = new form_counter_sales();
            main.Show();
        }

        public static void Login_form_open()
        {
            login_form log = new login_form();
            log.Show();
        }

        public static void CheckLoginDetails(Guna.UI2.WinForms.Guna2TextBox username, Guna.UI2.WinForms.Guna2TextBox password, Guna.UI2.WinForms.Guna2TextBox barcode)
        {
            username.Text = "";
            password.Text = "";
            barcode.Text = "";
            username.Select();
            barcode.Select();
        }

        public static bool login_button(Guna.UI2.WinForms.Guna2TextBox username, Guna.UI2.WinForms.Guna2TextBox password, Guna.UI2.WinForms.Guna2TextBox barcode, bool isSwitchUser)
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            //Datalayers data = new Datalayers(webConfig.con_string);
            
            try
            {
                if (username.Text != "" && password.Text != "")
                {
                    int role_id_db = 0;

                    GetSetData.query = "select user_id from pos_users where (username = '" + username.Text + "') and (password = '" + password.Text + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                    if (GetSetData.Ids != 0)
                    {
                        role_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "role_id", "pos_users", "user_id", GetSetData.Ids.ToString());
                        GetSetData.fks = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "emp_id", "pos_users", "user_id", GetSetData.Ids.ToString());
                        TextData.role_title = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.fks.ToString());
                        //TextData.role_title = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "full_name", "pos_employees", "employee_id", GetSetData.fks.ToString());

                        //TextData.person_name = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");


                        Save_login_details_button(GetSetData.Ids.ToString());

                        //*****************************************************************************

                        if (isSwitchUser)
                        {
                            form_counter_sales.user_id = GetSetData.Ids;
                            form_counter_sales.role_id = role_id_db;

                            Auth.user_id = GetSetData.Ids;
                            Auth.role_id = role_id_db;
                            Auth.user_name = TextData.role_title;
                            makeSaleScreen();
                        }
                        else
                        {
                            Menus.login_checked = "checked";
                            Menus.registrations_id = GetSetData.fks;
                            //Menus.user_id = GetSetData.Ids;
                            //Menus.role_id = role_id_db;
                            //Menus.authorized_person = TextData.role_title;
                            Auth.user_id = GetSetData.Ids;
                            Auth.role_id = role_id_db;
                            Auth.user_name = TextData.role_title;
                            MenuScreen();

                            if (Screen.AllScreens.Length > 1)
                            {
                                if (!IsFormOpen(typeof(fromSecondScreen)))
                                {
                                    Thread thread = new Thread(() =>
                                    {
                                        fromSecondScreen secondaryForm = new fromSecondScreen();
                                        Screen secondaryScreen = Screen.AllScreens[1];
                                        secondaryForm.StartPosition = FormStartPosition.Manual;
                                        secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                                        secondaryForm.WindowState = FormWindowState.Maximized;
                                        //secondaryForm.TopMost = true;
                                        Application.Run(secondaryForm);
                                    });

                                    thread.SetApartmentState(ApartmentState.STA);
                                    thread.Start();
                                }
                            }
                        }

                        // Play Sound *****************************************************
                        //TextData.person_name = @"" + TextData.person_name + "welcome.wav";
                        //SoundPlayer player = new SoundPlayer(TextData.person_name);
                        //player.Play();
                        GetSetData.runBackgroundServices();

                        return true;

                    }
                    else
                    {
                        error.errorMessage("Invalid username or password!");
                        error.ShowDialog();
                        CheckLoginDetails(username, password, barcode);
                    }
                }
                else
                {
                    error.errorMessage("Please enter username and password!");
                    error.ShowDialog();
                    CheckLoginDetails(username, password, barcode);
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private static bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                {
                    return true;
                }
            }

            return false;
        }

        public static void forgot_pass()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.person_name = "";

                GetSetData.query = "select username from pos_role where username = '" + TextData.username.ToString() + "';";

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);

                data.Connect();
                data.reader_ = data.cmd_.ExecuteReader();

                if (data.reader_.Read())
                {
                    TextData.person_name = data.reader_[0].ToString();
                    data.reader_.Close();
                }

                if (TextData.username == TextData.person_name)
                {
                    if (TextData.confirm_password == TextData.password)
                    {
                        GetSetData.query = "update pos_role set password = '" + TextData.password.ToString() + "' where username = '" + TextData.person_name.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        done.DoneMessage("Successfully Changed Password!");
                        done.ShowDialog();
                    }
                    else
                    {
                        error.errorMessage("Password is not Matching!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Username is Incorrect!");
                    error.ShowDialog();
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

        public static void Registration_info()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.person_name = "";

                GetSetData.query = @"select username from pos_role;";
                data.Connect();

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                if (data.reader_.Read())
                {
                    TextData.person_name = data.reader_[0].ToString();
                    data.reader_.Close();
                }
                else if (!data.reader_.Read())
                {
                    TextData.person_name = "";
                    data.reader_.Close();
                    data.Disconnect();
                }

                if (TextData.username != TextData.person_name)
                {
                        data.Connect();                 

                    if (TextData.person_name != "" && TextData.role_title != "")
                    {
                        if (TextData.password == TextData.confirm_password)
                        {
                            GetSetData.query = @"insert into pos_registration (name, roleTitle) values ('" + TextData.person_name.ToString() + "' , '" + TextData.role_title.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            GetSetData.query = @"select reg_id from pos_registration where name = '" + TextData.person_name.ToString() + "' and roleTitle = '" + TextData.role_title.ToString() + "';";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            GetSetData.query = @"insert into pos_role (username , password, reg_id) values ('" + TextData.username.ToString() + "' , '" + TextData.password.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            done.DoneMessage("Inserted Successfully!");
                            done.ShowDialog();
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

        public static void user_login_info()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.person_name = "";

                GetSetData.query = @"select username from pos_role;";
                data.Connect();

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                if (data.reader_.Read())
                {
                    TextData.person_name = data.reader_[0].ToString();
                    data.reader_.Close();
                }
                else if (!data.reader_.Read())
                {
                    TextData.person_name = "";
                    data.reader_.Close();
                    data.Disconnect();
                }

                if (TextData.username != TextData.person_name)
                {
                    if (TextData.confirm_password == TextData.password)
                    {
                        data.Connect();

                        if (TextData.address == "")
                        {
                            TextData.address = "nill";
                        }

                        if (TextData.title != "" && TextData.principal != "" && TextData.city != "" && TextData.address != "" && TextData.telphone != "" && TextData.website != "" && TextData.email != "" && TextData.logo != "")
                        {
                            GetSetData.query = @"insert into School_details (title, principal, city, address, telphone_no, website, email, logo_path) values ('" + TextData.title.ToString() + "' , '" + TextData.principal.ToString() + "' , '" + TextData.city.ToString() + "' , '" + TextData.address.ToString() + "' , '" + TextData.telphone.ToString() + "' , '" + TextData.website.ToString() + "' , '" + TextData.email.ToString() + "' , '" + TextData.logo.ToString() + "' );";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            done.DoneMessage("Inserted Successfully!");
                            done.ShowDialog();

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
                else
                {
                    error.errorMessage("Username is already exist!");
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

        private static void Save_login_details_button(string userId)
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.address = GetSetData.GetSystemMacAddress();

                if (TextData.address != "")
                {
                    GetSetData.query = @"insert into pos_login_details values ('" + TextData.address + "' , '" + DateTime.Now.ToShortDateString() + "' , '" + DateTime.Now.ToLongTimeString() + "', 'Login' , '" + userId + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


    }
}
