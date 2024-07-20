using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using WebConfig;
using Login_info.controllers;
using System.Data.SqlClient;
using Login_info.forms;

namespace Settings_info.forms
{
    public partial class settings : Form
    {
        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        int count = 1;
        public static int role_id = 0;
        string logo_path_db = "";
        string path_of_pic = "";

        public void system_user_permissions()
        {
            try
            {
                // ***************************************************************************************************
                string quer_get_expenses_print_db = "select settings_reg from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string settings_reg_db = data.select_scl_details_db(quer_get_expenses_print_db);

                pnl_loader_btn.Visible = bool.Parse(settings_reg_db);
                pnl_regisration_bt.Visible = bool.Parse(settings_reg_db);

                // ***************************************************************************************************
                string quer_get_expenses_new_db = "select settings_config from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string settings_config_db = data.select_scl_details_db(quer_get_expenses_new_db);

                pnl_configuration_btn.Visible = bool.Parse(settings_config_db);
                pnl_configuration_bt.Visible = bool.Parse(settings_config_db);

                // ***************************************************************************************************
                string quer_get_settings_reports_db = "select settings_reports from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string settings_reports_db = data.select_scl_details_db(quer_get_settings_reports_db);

                pnl_Reports_btn.Visible = bool.Parse(settings_reports_db);
                pnl_reports_bt.Visible = bool.Parse(settings_reports_db);

                // ***************************************************************************************************
                string quer_get_pos_exit_db = "select settings_authority from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string settings_authority_db = data.select_scl_details_db(quer_get_pos_exit_db);

                pnl_authorities_btn.Visible = bool.Parse(settings_authority_db);
                pnl_authorities_bt.Visible = bool.Parse(settings_authority_db);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public settings()
        {
            InitializeComponent();

            string quer_get_logo_path_db = @"select logo_path from configuration where config_id = '1';";
            logo_path_db = data.select_scl_details_db(quer_get_logo_path_db);

            path_of_pic = logo_path_db;
        }

        
        public void refresh()
        {
            TextData.shop_name = shop_name_text.Text = "";
            TextData.owner_name = owner_text.Text = "";
            TextData.city = city_text.Text = "";
            TextData.address = address_text.Text = "";
            TextData.business_type = business_text.Text = "";
            business_nature_text.Text = "";
            TextData.branch = branch_text.Text = "";
            TextData.shop_no = shop_no_text.Text = "";
            TextData.phone1 = mobile_text.Text = "";
            TextData.phone2 = telphone_text.Text = "";
            TextData.comments = comments_text.Text = "";

            img_pic_box = null;
        }

        public  void Registration_btn()
        {
            try
            {
                pnl_registration.Visible = true;
                pnl_configurations.Visible = false;
                pnl_reports.Visible = false;
                pnl_authorization.Visible = false;
                pnl_all_forms1.Visible = true;
                pnl_all_forms2.Visible = false;
                pnl_all_forms3.Visible = false;

                pnl_registration.Dock = DockStyle.Fill;
                pnl_all_forms1.Dock = DockStyle.Fill;

                btn_next1.Enabled = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Configuraions_btn()
        {
            try
            {
                pnl_registration.Visible = false;
                pnl_configurations.Visible = true;
                pnl_reports.Visible = false;
                pnl_authorization.Visible = false;

                pnl_configurations.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Reports_btn()
        {
            try
            {
                pnl_registration.Visible = false;
                pnl_configurations.Visible = false;
                pnl_reports.Visible = true;
                pnl_authorization.Visible = false;

                pnl_reports.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Authorization_btn()
        {
            try
            {
                pnl_registration.Visible = false;
                pnl_configurations.Visible = false;
                pnl_reports.Visible = false;
                pnl_authorization.Visible = true;

                pnl_authorization.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void fill_textboxes()
        {
            try
            {
                string quer_get_shop_name_db = @"select shop_name from configuration where config_id = '1';";
                string shop_name_db = data.select_scl_details_db(quer_get_shop_name_db);

                string quer_get_owner_db = @"select owner_name from configuration where config_id = '1';";
                string owner_db = data.select_scl_details_db(quer_get_owner_db);

                string quer_get_city_db = @"select city from configuration where config_id = '1';";
                string city_db = data.select_scl_details_db(quer_get_city_db);

                string quer_get_address_db = @"select address from configuration where config_id = '1';";
                string address_db = data.select_scl_details_db(quer_get_address_db);

                string quer_get_business_type_db = @"select business_nature from configuration where config_id = '1';";
                string business_nature_db = data.select_scl_details_db(quer_get_business_type_db);

                string quer_get_branch_db = @"select branch from configuration where config_id = '1';";
                string branch_db = data.select_scl_details_db(quer_get_branch_db);

                string quer_get_shop_no_db = @"select shop_no from configuration where config_id = '1';";
                string shop_no_db = data.select_scl_details_db(quer_get_shop_no_db);

                string quer_get_phone_1_db = @"select phone_1 from configuration where config_id = '1';";
                string phone_1_db = data.select_scl_details_db(quer_get_phone_1_db);

                string quer_get_phone_2_db = @"select phone_2 from configuration where config_id = '1';";
                string phone_2_db = data.select_scl_details_db(quer_get_phone_2_db);

                string quer_get_path_db = @"select logo_path from configuration where config_id = '1';";
                string path_db = data.select_scl_details_db(quer_get_path_db);

                //string quer_get_email_db = @"select email from configuration where config_id = '1';";
                //string email_db = data.select_scl_details_db(quer_get_email_db);

                string quer_get_comments_db = @"select comments from configuration where config_id = '1';";
                string comments_db = data.select_scl_details_db(quer_get_comments_db);

                shop_name_text.Text = shop_name_db;
                owner_text.Text = owner_db;
                city_text.Text = city_db;
                address_text.Text = address_db;
                business_nature_text.Text = business_nature_db;
                branch_text.Text = branch_db;
                shop_no_text.Text = shop_no_db;
                mobile_text.Text = phone_1_db;
                telphone_text.Text = phone_2_db;

                if (path_db != "Attach Logo!" && path_db != "")
                {
                    img_pic_box.Image = Bitmap.FromFile(path_db);
                }

                comments_text.Text = comments_db;
                
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            refresh();
            buttonControls.mainMenu_buttons();
            this.Close();
        }

        public void Configuration_tab_indexes()
        {
            shop_name_text.TabIndex = 0;
            owner_text.TabIndex = 1;
            city_text.TabIndex = 2;
            address_text.TabIndex = 3;
            business_nature_text.TabIndex = 4;
            business_text.TabIndex = 5;
            branch_text.TabIndex = 6;
            shop_no_text.TabIndex = 7;
            mobile_text.TabIndex = 8;
            telphone_text.TabIndex = 9;
            comments_text.TabIndex = 10;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {

                TextData.shop_name = shop_name_text.Text;
                TextData.owner_name = owner_text.Text;
                TextData.city = city_text.Text;
                TextData.address = address_text.Text;
                TextData.business_type = business_nature_text.Text;
                TextData.branch = branch_text.Text;
                TextData.shop_no = shop_no_text.Text;
                TextData.phone1 = mobile_text.Text;
                TextData.phone2 = telphone_text.Text;
                TextData.logo_path = path_of_pic;
                TextData.comments = comments_text.Text;

                

                if (path_of_pic == "" && logo_path_db == "")
                {
                    TextData.logo_path = "Attach Logo!";
                }
                else if (logo_path_db != path_of_pic && logo_path_db != "Attach Logo!")
                {
                    TextData.logo_path = path_of_pic;
                }
                
                buttonControls.update_Configuration_details();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void settings_Load(object sender, EventArgs e)
        {
            pnl_authorization.Visible = false;
            pnl_reports.Visible = false;
            pnl_registration.Visible = false;
            pnl_configurations.Visible = false;

            fill_textboxes();
            system_user_permissions();
        }

        private void close_Click(object sender, EventArgs e)
        {
            refresh();
            pnl_configurations.Visible = false;
        }

        private void btn_registration_Click(object sender, EventArgs e)
        {
            //buttonControls.registration_buttons();
            //this.Dispose();
            name_text.TabIndex = 0;
            role_text.TabIndex = 1;
            username_text.TabIndex = 2;
            pass_text.TabIndex = 3;
            txt_confirm_pass.TabIndex = 4;


            Registration_btn();

        }

        private void bt_registration_Click(object sender, EventArgs e)
        {
            //buttonControls.registration_buttons();
            //this.Dispose();
            Registration_btn();
        }

        private void btn_refresh_configure_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_configuration_Click(object sender, EventArgs e)
        {
            Configuraions_btn();
            Configuration_tab_indexes();
        }

        private void bt_configuration_Click(object sender, EventArgs e)
        {
            Configuraions_btn();
            Configuration_tab_indexes();
        }

        public void Reports_tab_indexes()
        {
            txt_title.TabIndex = 0;
            txt_sub_title.TabIndex = 1;
            txt_message.TabIndex = 2;
            txt_copyrights.TabIndex = 3;
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            Reports_btn();
            txt_title.Text = buttonControls.fun_title_db();
            txt_sub_title.Text = buttonControls.fun_sub_title_db();
            txt_message.Text = buttonControls.fun_message_db();
            txt_copyrights.Text = buttonControls.fun_copyrights_db();

            Reports_tab_indexes();
        }

        private void bt_reports_Click(object sender, EventArgs e)
        {
            Reports_btn();
            txt_title.Text = buttonControls.fun_title_db();
            txt_sub_title.Text = buttonControls.fun_sub_title_db();
            txt_message.Text = buttonControls.fun_message_db();
            txt_copyrights.Text = buttonControls.fun_copyrights_db();

            Reports_tab_indexes();
        }

        private void btn_authorities_Click(object sender, EventArgs e)
        {
            Authorization_btn();
        }

        private void bt_authorities_Click(object sender, EventArgs e)
        {
            Authorization_btn();
        }

        private void btn_save_reports_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.title = txt_title.Text;
                TextData.sub_title = txt_sub_title.Text;
                TextData.message = txt_message.Text;
                TextData.copyrights = txt_copyrights.Text;

                buttonControls.insert_update_reports_tables();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_close_report_Click(object sender, EventArgs e)
        {
            pnl_reports.Visible = false;
        }

        private void upload_logo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Images only. |*.jpg; *.jpeg; *.png; *.gif;";
                DialogResult dr = dialog.ShowDialog();
                img_pic_box.Image = Image.FromFile(dialog.FileName);

                path_of_pic = dialog.FileName;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void add_sub_category_Click(object sender, EventArgs e)
        {
            TextData.business_type = business_text.Text;

            buttonControls.insert_business_title_tables();
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                TextData.business_type = business_text.Text;

                buttonControls.insert_business_title_tables();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (count > 0)
            //{
                count++;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(count > 0)
            {
                count--;
            }

        }

        private void btn_exit_form1_Click(object sender, EventArgs e)
        {
            pnl_registration.Visible = false;
        }

        public void All_form_visibility()
        {
            try
            {
                switch (count)
                {
                    case 1:
                        pnl_all_forms1.Visible = true;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = false;

                        btn_next1.Enabled = true;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        pnl_all_forms1.Dock = DockStyle.Fill;

                        break;

                    case 2:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = true;
                        pnl_all_forms3.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = true;
                        btn_previous3.Enabled = false;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = true;
                        btn_next3.Enabled = false;
                        pnl_all_forms2.Dock = DockStyle.Fill;
                        break;

                    case 3:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = true;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = true;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        pnl_all_forms3.Dock = DockStyle.Fill;
                        break;

                    case 4:
                        count = 1;
                        btn_previous3.Enabled = true;
                        btn_next3.Enabled = false;
                        break;

                    default:
                        count = 1;
                        break;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_next1_Click(object sender, EventArgs e)
        {
            try
            {
                count++;
                All_form_visibility();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous2_Click(object sender, EventArgs e)
        {
            try
            {
                count--;
                All_form_visibility();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_next2_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 2)
                //{
                    count++;
                    All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_exit_form2_Click(object sender, EventArgs e)
        {
            count--;
            All_form_visibility();
        }

        private void btn_previous1_Click(object sender, EventArgs e)
        {

        }

        private void btn_next3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                    All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                    count--;
                    All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_exit_form3_Click(object sender, EventArgs e)
        {
            count--;
            All_form_visibility();
        }

        public void checked_values_for_tbl_dashboard_authorities()
        {
            if(chk_dashboard_loader.Checked == true)
            {
                TextData.loader = true;
            }
            else
            {
                TextData.loader = false;
            }

            if (chk_dashboard_loader_returns.Checked == true)
            {
                TextData.loader_returns = true;
            }
            else
            {
                TextData.loader_returns = false;
            }

            if (chk_dashboard_loader.Checked == true)
            {
                TextData.loader = true;
            }
            else
            {
                TextData.loader = false;
            }

            if (chk_dashboard_loyal_customer.Checked == true)
            {
                TextData.loyal_customer = true;
            }
            else
            {
                TextData.loyal_customer = false;
            }

            if (chk_dashboard_loyal_cus_returns.Checked == true)
            {
                TextData.loyal_returns = true;
            }
            else
            {
                TextData.loyal_returns = false;
            }

            if (chk_dashboard_products.Checked == true)
            {
                TextData.products = true;
            }
            else
            {
                TextData.products = false;
            }

            if (chk_dashboard_purchases.Checked == true)
            {
                TextData.purchases = true;
            }
            else
            {
                TextData.purchases = false;
            }

            if (chk_dashboard_expenses.Checked == true)
            {
                TextData.expenses = true;
            }
            else
            {
                TextData.expenses = false;
            }

            if (chk_dashboard_company_payments.Checked == true)
            {
                TextData.company_reg = true;
            }
            else
            {
                TextData.company_reg = false;
            }
            
            if (chk_dashboard_company_paybook.Checked == true)
            {
                TextData.paybook = true;
            }
            else
            {
                TextData.paybook = false;
            }
            
            if (chk_dashboard_recoveries.Checked == true)
            {
                TextData.recoveries = true;
            }
            else
            {
                TextData.recoveries = false;
            }

            if (chk_dashboard_customers.Checked == true)
            {
                TextData.customers = true;
            }
            else
            {
                TextData.customers = false;
            }

            if (chk_dashboard_suppliers.Checked == true)
            {
                TextData.suppliers = true;
            }
            else
            {
                TextData.suppliers = false;
            }

            if (chk_dashboard_customers_dues.Checked == true)
            {
                TextData.customer_dues = true;
            }
            else
            {
                TextData.customer_dues = false;
            }

            if (chk_dashboard_pos.Checked == true)
            {
                TextData.pos = true;
            }
            else
            {
                TextData.pos = false;
            }

            if (chk_dashboard_logout.Checked == true)
            {
                TextData.logOut = true;
            }
            else
            {
                TextData.logOut = false;
            }

            if (chk_dashboard_backup.Checked == true)
            {
                TextData.backup = true;
            }
            else
            {
                TextData.backup = false;
            }

            if (chk_dashboard_restore.Checked == true)
            {
                TextData.restore = true;
            }
            else
            {
                TextData.restore = false;
            }

            if (chk_dashboard_settings.Checked == true)
            {
                TextData.settings = true;
            }
            else
            {
                TextData.settings = false;
            }

            if (chk_dashboard_notifications.Checked == true)
            {
                TextData.notifications = true;
            }
            else
            {
                TextData.notifications = false;
            }

            if (chk_dashboard_about.Checked == true)
            {
                TextData.about = true;
            }
            else
            {
                TextData.about = false;
            }

            buttonControls.insert_dashboard_button_controls();
        }

        public void checked_values_for_tbl_Reports_authorities()
        {
            if (chk_reports_customer_ledger.Checked == true)
            {
                TextData.cus_ledger = true;
            }
            else
            {
                TextData.cus_ledger = false;
            }

            if (chk_reports_customer_sales.Checked == true)
            {
                TextData.cus_sales = true;
            }
            else
            {
                TextData.cus_sales = false;
            }

            if (chk_reports_cus_returns.Checked == true)
            {
                TextData.cus_returns = true;
            }
            else
            {
                TextData.cus_returns = false;
            }

            if (chk_reports_loader_reports.Checked == true)
            {
                TextData.loader_reports = true;
            }
            else
            {
                TextData.loader_reports = false;
            }

            if (chk_reports_daybook.Checked == true)
            {
                TextData.day_book = true;
            }
            else
            {
                TextData.day_book = false;
            }

            if (chk_reports_stock.Checked == true)
            {
                TextData.stock = true;
            }
            else
            {
                TextData.stock = false;
            }

            if (chk_reports_recoveries.Checked == true)
            {
                TextData.cus_recoveries = true;
            }
            else
            {
                TextData.cus_recoveries = false;
            }

            if (chk_reports_company_payments.Checked == true)
            {
                TextData.company_payments = true;
            }
            else
            {
                TextData.company_payments = false;
            }

            if (chk_reports_receivables.Checked == true)
            {
                TextData.receivables = true;
            }
            else
            {
                TextData.receivables = false;
            }

            if (chk_reports_payables.Checked == true)
            {
                TextData.payables = true;
            }
            else
            {
                TextData.payables = false;
            }

            if (chk_reports_balance_in_out.Checked == true)
            {
                TextData.balance_in_out = true;
            }
            else
            {
                TextData.balance_in_out = false;
            }

            if (chk_reports_income_statement.Checked == true)
            {
                TextData.income_statement = true;
            }
            else
            {
                TextData.income_statement = false;
            }

            if (chk_reports_company_ledger.Checked == true)
            {
                TextData.company_ledger = true;
            }
            else
            {
                TextData.company_ledger = false;
            }

            buttonControls.insert_reports_button_controls();
        }

        private void btn_save_form1_Click(object sender, EventArgs e)
        {
            TextData.person_name = name_text.Text;
            TextData.role_title = role_text.Text;
            TextData.username = username_text.Text;
            TextData.password = pass_text.Text;
            TextData.confirm_password = txt_confirm_pass.Text;

            buttonControls.Registration_info();
            checked_values_for_tbl_dashboard_authorities();
            checked_values_for_tbl_Reports_authorities();
        }

        public void checked_values_for_tbl_authorities_button_controls1()
        {
            TextData.company_reg_save = false;
            TextData.company_reg_update = false;
            TextData.company_reg_print = false;
            TextData.company_reg_new = false;
            TextData.company_reg_refresh = false;
            TextData.corporate_business_save = false;
            TextData.corporate_business_save_print = false;
            TextData.corporate_business_exit = false;
            TextData.customers_save = false;
            TextData.customers_update = false;
            TextData.customers_print = false;
            TextData.customers_new = false;
            TextData.customers_refresh = false;
            TextData.expenses_save = false;
            TextData.expenses_update = false;
            TextData.expenses_print = false;
            TextData.expenses_new = false;
            TextData.expenses_refresh = false;
            TextData.products_save = false;
            TextData.products_update = false;
            TextData.products_print = false;
            TextData.products_new = false;
            TextData.products_refresh = false;
            TextData.purchases_save = false;
            TextData.purchases_update = false;
            TextData.purchases_print = false;
            TextData.purchases_new = false;
            TextData.purchases_refresh = false;
            TextData.recoveries_save = false;
            TextData.recoveries_save_print = false;
            TextData.recoveries_exit = false;
            TextData.customer_dues_print = false;
            TextData.customer_dues_refresh = false;
            TextData.customer_dues_exit = false;


            if (chk_compnay_reg_save.Checked == true)
            {
                TextData.company_reg_save = true;
            }

            if (chk_compnay_reg_update.Checked == true)
            {
                TextData.company_reg_update = true;
            }

            if (chk_compnay_reg_print.Checked == true)
            {
                TextData.company_reg_print = true;
            }

            if (chk_compnay_reg_new.Checked == true)
            {
                TextData.company_reg_new = true;
            }

            if (chk_compnay_reg_refresh.Checked == true)
            {
                TextData.company_reg_refresh = true;
            }

            if (chk_corporate_save.Checked == true)
            {
                TextData.corporate_business_save = true;
            }

            if (chk_corporate_save_print.Checked == true)
            {
                TextData.corporate_business_save_print = true;
            }

            if (chk_corporate_exit.Checked == true)
            {
                TextData.corporate_business_exit = true;
            }

            if (chk_customers_save.Checked == true)
            {
                TextData.customers_save = true;
            }

            if (chk_customers_update.Checked == true)
            {
                TextData.customers_update = true;
            }

            if (chk_customers_print.Checked == true)
            {
                TextData.customers_print = true;
            }

            if (chk_customers_new.Checked == true)
            {
                TextData.customers_new = true;
            }

            if (chk_customers_refresh.Checked == true)
            {
                TextData.customers_refresh = true;
            }

            if (chk_expenses_save.Checked == true)
            {
                TextData.expenses_save = true;
            }

            if (chk_expenses_update.Checked == true)
            {
                TextData.expenses_update = true;
            }

            if (chk_expenses_print.Checked == true)
            {
                TextData.expenses_print = true;
            }

            if (chk_expenses_new.Checked == true)
            {
                TextData.expenses_new = true;
            }

            if (chk_expenses_refresh.Checked == true)
            {
                TextData.expenses_refresh = true;
            }

            if (chk_products_save.Checked == true)
            {
                TextData.products_save = true;
            }

            if (chk_products_update.Checked == true)
            {
                TextData.products_update = true;
            }

            if (chk_products_print.Checked == true)
            {
                TextData.products_print = true;
            }

            if (chk_products_new.Checked == true)
            {
                TextData.products_new = true;
            }

            if (chk_products_refresh.Checked == true)
            {
                TextData.products_refresh = true;
            }

            if (chk_purchases_save.Checked == true)
            {
                TextData.purchases_save = true;
            }

            if (chk_purchases_update.Checked == true)
            {
                TextData.purchases_update = true;
            }

            if (chk_purchases_print.Checked == true)
            {
                TextData.purchases_print = true;
            }

            if (chk_purchases_new.Checked == true)
            {
                TextData.purchases_new = true;
            }

            if (chk_purchases_refresh.Checked == true)
            {
                TextData.purchases_refresh = true;
            }

            if (chk_recoveries_save.Checked == true)
            {
                TextData.recoveries_save = true;
            }

            if (chk_recoveries_save_print.Checked == true)
            {
                TextData.recoveries_save_print = true;
            }

            if (chk_recoveries_exit.Checked == true)
            {
                TextData.recoveries_exit = true;
            }

            if (chk_cus_dues_print.Checked == true)
            {
                TextData.customer_dues_print = true;
            }

            if (chk_cus_dues_refresh.Checked == true)
            {
                TextData.customer_dues_refresh = true;
            }

            if (chk_cus_dues_exit.Checked == true)
            {
                TextData.customer_dues_exit = true;
            }

            buttonControls.insert_forms_button_controls1();
        }

        private void btn_save_form2_Click(object sender, EventArgs e)
        {
            TextData.person_name = name_text.Text;
            TextData.role_title = role_text.Text;
            TextData.username = username_text.Text;
            TextData.password = pass_text.Text;
            TextData.confirm_password = txt_confirm_pass.Text;

            checked_values_for_tbl_authorities_button_controls1();
        }

        public void checked_values_for_tbl_authorities_button_controls2()
        {
            TextData.pos_save = false;
            TextData.pos_update = false;
            TextData.pos_save_print = false;
            TextData.pos_exit = false;
            TextData.loyal_cus_sales_save = false;
            TextData.loyal_cus_sales_update = false;
            TextData.loyal_cus_sales_save_print = false;
            TextData.loyal_cus_sales_exit = false;
            TextData.loader_prod_return_save = false;
            TextData.loader_prod_return_save_print = false;
            TextData.loader_prod_return_exit = false;
            TextData.loader_cus_sales_save = false;
            TextData.loader_cus_sales_save_print = false;
            TextData.loader_cus_sales_exit = false;
            TextData.loader_prod_save = false;
            TextData.loader_prod_save_print = false;
            TextData.loader_prod_exit = false;
            TextData.settings_reg = false;
            TextData.settings_config = false;
            TextData.settings_reports = false;
            TextData.settings_authority = false;
            TextData.whole_low_stock_print = false;
            TextData.whole_low_stock_refresh = false;
            TextData.whole_low_stock_exit = false;
            TextData.suppliers_save = false;
            TextData.suppliers_update = false;
            TextData.suppliers_print = false;
            TextData.suppliers_new = false;
            TextData.suppliers_refresh = false;


            if (chk_Pos_save.Checked == true)
            {
                TextData.pos_save = true;
            }

            if (chk_pos_update.Checked == true)
            {
                TextData.pos_update = true;
            }

            if (chk_pos_save_print.Checked == true)
            {
                TextData.pos_save_print = true;
            }

            if (chk_pos_exit.Checked == true)
            {
                TextData.pos_exit = true;
            }

            if (chk_loyal_save.Checked == true)
            {
                TextData.loyal_cus_sales_save = true;
            }

            if (chk_loyal_update.Checked == true)
            {
                TextData.loyal_cus_sales_update = true;
            }

            if (chk_loyal_save_print.Checked == true)
            {
                TextData.loyal_cus_sales_save_print = true;
            }

            if (chk_loyal_exit.Checked == true)
            {
                TextData.loyal_cus_sales_exit = true;
            }

            if (chk_loader_return_save.Checked == true)
            {
                TextData.loader_prod_return_save = true;
            }

            if (chk_loader_return_save_print.Checked == true)
            {
                TextData.loader_prod_return_save_print = true;
            }

            if (chk_loader_return_exit.Checked == true)
            {
                TextData.loader_prod_return_exit = true;
            }

            if (chk_loader_cus_save.Checked == true)
            {
                TextData.loader_cus_sales_save = true;
            }

            if (chk_loader_cus_save_print.Checked == true)
            {
                TextData.loader_cus_sales_save_print = true;
            }

            if (chk_loader_cus_exit.Checked == true)
            {
                TextData.loader_cus_sales_exit = true;
            }

            if (chk_loader_prod_save.Checked == true)
            {
                TextData.loader_prod_save = true;
            }

            if (chk_loader_prod_save_print.Checked == true)
            {
                TextData.loader_prod_save_print = true;
            }

            if (chk_loader_prod_exit.Checked == true)
            {
                TextData.loader_prod_exit = true;
            }

            if (chk_settings_reg.Checked == true)
            {
                TextData.settings_reg = true;
            }

            if (chk_settings_config.Checked == true)
            {
                TextData.settings_config = true;
            }

            if (chk_settings_reports.Checked == true)
            {
                TextData.settings_reports = true;
            }

            if (chk_settings_authority.Checked == true)
            {
                TextData.settings_authority = true;
            }

            if (chk_whole_low_stock_print.Checked == true)
            {
                TextData.whole_low_stock_print = true;
            }

            if (chk_whole_low_stock_refresh.Checked == true)
            {
                TextData.whole_low_stock_refresh = true;
            }

            if (chk_whole_low_stock_exit.Checked == true)
            {
                TextData.whole_low_stock_exit = true;
            }

            if (chk_supplier_save.Checked == true)
            {
                TextData.suppliers_save = true;
            }

            if (chk_supplier_update.Checked == true)
            {
                TextData.suppliers_update = true;
            }

            if (chk_supplier_print.Checked == true)
            {
                TextData.suppliers_print = true;
            }

            if (chk_supplier_new.Checked == true)
            {
                TextData.suppliers_new = true;
            }

            if (chk_supplier_refresh.Checked == true)
            {
                TextData.suppliers_refresh = true;
            }

            buttonControls.insert_forms_button_controls2();
        }

        private void btn_save_form3_Click(object sender, EventArgs e)
        {
            TextData.person_name = name_text.Text;
            TextData.role_title = role_text.Text;
            TextData.username = username_text.Text;
            TextData.password = pass_text.Text;
            TextData.confirm_password = txt_confirm_pass.Text;

            checked_values_for_tbl_authorities_button_controls2();
        }
    }
}
