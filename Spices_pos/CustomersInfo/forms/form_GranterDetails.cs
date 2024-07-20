using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.CustomersInfo.controllers;

namespace Customers_info.forms
{
    public partial class form_GranterDetails : Form
    {
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams handleParam = base.CreateParams;

                if (enableFormLevelDoubleBuffering)
                {
                    handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
                }
                else
                {
                    handleParam.ExStyle = originalExStyle;
                }

                return handleParam;
            }
        }

        public void TrunOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MinimizeBox = true;
            this.WindowState = FormWindowState.Minimized;
        }

        public form_GranterDetails()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static int count = 0;
        public static string billNo = "";
        public static string customerName = "";
        public static string customerCode = "";
        public static string employeeName = "";

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

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_show_all);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void system_user_permissions()
        {
            try
            {
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("guarantor_details_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("guarantor_details_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("guarantor_details_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("guarantor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                if (count == 1)
                {
                    pnl_buttomButtons.Visible = false;
                }
                else
                {
                    pnl_buttomButtons.Visible = true;
                }

                GetSetData.addFormCopyrights(lblCopyrights);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewGranterDetails where [Guarantor Name] != 'nill'";

                if (condition == "notNull")
                {
                    GetSetData.query = GetSetData.query + " and ([Status] = 'Active');";
                }
                else if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " and ([Date] like '" + search_box.Text + "%' or [Guarantor Name] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or  [CNIC] like '" + search_box.Text + "%' or [Mobile No] like '" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(CustomerDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void clearGridView()
        {
            this.CustomerDetailGridView.DataSource = null;
            this.CustomerDetailGridView.Refresh();
            CustomerDetailGridView.Rows.Clear();
            CustomerDetailGridView.Columns.Clear();
        }

        private void createCheckBoxInGridView()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Select";
            chk.Name = "select";
            chk.Width = 15;
            CustomerDetailGridView.Columns.Add(chk);
        }

        private void fun_exit_form()
        {
            GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Exit...", role_id);

            switch (count)
            {
                case 1:
                    Button_controls.mainMenu_buttons();
                    this.Close();
                    break;

                default:
                    this.Close();
                    break;
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            fun_exit_form();   
        }

        private void form_GranterDetails_Load(object sender, EventArgs e)
        {
            try
            {
                originalExStyle = -1;
                enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                clearGridView();
                FillGridViewUsingPagination("notNull");
                search_box.Text = "";
                createCheckBoxInGridView();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void addNewDetails()
        {
            using (formNewGrantors add_customer = new formNewGrantors())
            {
                formNewGrantors.role_id = role_id;
                formNewGrantors.saveEnable = false;
                GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Add New Guarantor button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();
            FillGridViewUsingPagination("search");
            createCheckBoxInGridView();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();
            FillGridViewUsingPagination("notNull");
            search_box.Text = "";
            createCheckBoxInGridView();
        }

        private bool fun_update_details()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("guarantor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    formNewGrantors.saveEnable = true;
                    TextData.full_name = TextData.full_name_key = CustomerDetailGridView.SelectedRows[0].Cells["Guarantor Name"].Value.ToString();
                    TextData.cus_code = TextData.cus_codekey = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.cnic = CustomerDetailGridView.SelectedRows[0].Cells["CNIC"].Value.ToString();
                    TextData.country = TextData.countrykey = CustomerDetailGridView.SelectedRows[0].Cells["Country"].Value.ToString();
                    TextData.province = TextData.provincekey = CustomerDetailGridView.SelectedRows[0].Cells["Province"].Value.ToString();
                    TextData.phone1 = CustomerDetailGridView.SelectedRows[0].Cells["Mobile No"].Value.ToString();
                    TextData.phone2 = CustomerDetailGridView.SelectedRows[0].Cells["Telephone No"].Value.ToString();
                    TextData.address1 = CustomerDetailGridView.SelectedRows[0].Cells["Address"].Value.ToString();

                    GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Updating Guarantor [" + TextData.full_name + "  " + TextData.cus_code + "] details (Modify button click...)", role_id);

                    using (formNewGrantors add_customer = new formNewGrantors())
                    {
                        formNewGrantors.role_id = role_id;
                        add_customer.ShowDialog();
                    }
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message); // "Please select the row first!"
                error.ShowDialog();
                return false;
            }
        }

        private void update_customer_datails(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Print button click...", role_id);
            Button_controls.Customers_list_Report_buttons();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();
            FillGridViewUsingPagination("");
            search_box.Text = "";
            createCheckBoxInGridView();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Guarantor Name"].Value.ToString();
                TextData.fatherName = CustomerDetailGridView.SelectedRows[0].Cells["Father Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();

                GetSetData.Ids = data.UserPermissionsIds("granter_id", "pos_granters", "full_name", TextData.full_name);

                GetSetData.query = @"delete from pos_granters where granter_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Deleting Guarantor [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Guarantor Name"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.full_name.ToString() + "'");
                sure.ShowDialog();
                this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    clearGridView();
                    FillGridViewUsingPagination("notNull");
                    search_box.Text = "";
                    createCheckBoxInGridView();
                }
            }
            catch (Exception es)
            {
                error.errorMessage("'" + TextData.full_name.ToString() + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            clearGridView();
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
            createCheckBoxInGridView();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            clearGridView();
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
            createCheckBoxInGridView();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrunOffFormLevelDoubleBuffering();
            originalExStyle = -1;
            enableFormLevelDoubleBuffering = true;
        }

        private void saved_Grantors_db()
        {
            try
            {
                data.Connect();

                foreach (DataGridViewRow item in CustomerDetailGridView.Rows)
                {
                    GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                    if (GetSetData.checkBoxSelected)
                    {
                        TextData.full_name = item.Cells[1].Value.ToString();
                        TextData.cus_code = item.Cells[3].Value.ToString();

                        GetSetData.query = "select granter_id from pos_granters where (full_name = '" + TextData.full_name.ToString() + "') and (code = '" + TextData.cus_code.ToString() + "');";
                        int grantor_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + customerName + "') and (cus_code = '" + customerCode +"');";
                        int customer_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select employee_id from pos_employees where full_name = '" + employeeName + "';";
                        int employee_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select payment_id from pos_payment_grantors where (billNo = '" + billNo + "') and (customer_id = '" + customer_id.ToString() + "') and  (granter_id = '" + grantor_id.ToString() + "') and (employee_id = '" + employee_id.ToString() + "');";
                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (GetSetData.Ids == 0)
                        {
                            GetSetData.query = "insert into pos_payment_grantors values ('" + billNo + "', '" + customer_id.ToString() + "', '" + grantor_id.ToString() + "', '" + employee_id.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }

                this.Opacity = .850;
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                this.Opacity = .999;
                //return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //return false;
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saved_Grantors_db();
        }

        private bool Remove_Grantors_db()
        {
            try
            {
                data.Connect();
                foreach (DataGridViewRow item in CustomerDetailGridView.Rows)
                {
                    GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                    if (GetSetData.checkBoxSelected)
                    {
                        TextData.full_name = item.Cells[1].Value.ToString();
                        TextData.cus_code = item.Cells[3].Value.ToString();

                        GetSetData.query = "select granter_id from pos_granters where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                        int grantor_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + customerName + "') and (cus_code = '" + customerCode + "');";
                        int customer_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select employee_id from pos_employees where full_name = '" + employeeName + "';";
                        int employee_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "delete from pos_payment_grantors where billNo = '" + billNo + "' and customer_id = '" + customer_id.ToString() + "' and  granter_id = '" + grantor_id.ToString() + "' and employee_id = '" + employee_id.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                this.Opacity = .850;
                done.DoneMessage("Successfully Removed!");
                done.ShowDialog();
                this.Opacity = .999;
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            Remove_Grantors_db();
        }

        private void form_GranterDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Guarantors Details Form", "Print button click...", role_id);
                Button_controls.Customers_list_Report_buttons();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                addNewDetails();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                fun_update_details();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
