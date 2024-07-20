using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DashboardInfo.Forms;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.controllers;
using Spices_pos.DashboardInfo.CustomControls;

namespace Purchase_info.forms
{
    public partial class form_supplier_details : Form
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


        public form_supplier_details()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        public static int count = 0;

        private void setFormColorsDynamically()
        {
            //try
            //{
            //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
            //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
            //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

            //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
            //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
            //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

            //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
            //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
            //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

            //    //****************************************************************

            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_show_all);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void system_user_permissions()
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_print", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_delete", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_new", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_modify", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_select", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_select.Visible = bool.Parse(GetSetData.Data);

                if (count == 1)
                {
                    sidePanel.Visible = true;
                    setSideBarInSidePanel();
                }
                else
                {
                    sidePanel.Visible = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void setSideBarInSidePanel()
        {
            try
            {
                sidebarUserControl sidebar = new sidebarUserControl();

                sidebar.role_id = role_id;
                sidebar.user_id = user_id;

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
                sidebar.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clearDataGridViewItems()
        {
            this.CustomerDetailGridView.DataSource = null;
            this.CustomerDetailGridView.Refresh();
            CustomerDetailGridView.Rows.Clear();
            CustomerDetailGridView.Columns.Clear();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewSupplierDetails";

                if (condition == "notNull")
                {
                    GetSetData.query = GetSetData.query + " where status = 'Active';";
                }
                else if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where [Date] like ('"+ search_box.Text +"%') or [Supplier Name] like ('"+ search_box.Text +"%') or [Code] like ('"+ search_box.Text +"%')  or [Mobile No] like ('"+ search_box.Text +"%')";
                }

                clearDataGridViewItems();
                GetSetData.FillDataGridViewUsingPagination(CustomerDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);

                if (count == 0)
                {
                    createSelectButtonInGridView();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void createSelectButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Select";
            btn.Name = "Select";
            btn.Text = "Select";
            btn.Width = 60;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            CustomerDetailGridView.Columns.Add(btn);
        }


        private void fun_exit_form()
        {
            //GetSetData.SaveLogHistoryDetails("Company Details Form", "Exit...", role_id);

            switch (count)
            {
                case 1:
                    Menus.login_checked = "";
                    Menus main = new Menus();
                    main.Show();
                    this.Dispose();
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

        private void addNewDetails()
        {
            using (form_purchase_from add_customer = new form_purchase_from())
            {
                //GetSetData.SaveLogHistoryDetails("Company Details Form", "Add new company button click...", role_id);
                form_purchase_from.role_id = role_id;
                form_purchase_from.saveEnable = false;
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void form_supplier_details_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                FillGridViewUsingPagination("notNull");
                search_box.Text = "";
                system_user_permissions();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            search_box.Text = "";
            FillGridViewUsingPagination("notNull");
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
            //GetSetData.FillDataGridView(CustomerDetailGridView, "ProcedureSearchSupplierDetails", search_box.Text);
        }

        private bool fun_update_details()
        {
            try
            {
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplier_details_modify", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    form_purchase_from.saveEnable = true;
                    TextData.full_name = TextData.full_name_key = CustomerDetailGridView.SelectedRows[0].Cells["Supplier Name"].Value.ToString();
                    TextData.cus_code = TextData.cus_codekey = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.country = TextData.countrykey = CustomerDetailGridView.SelectedRows[0].Cells["Country"].Value.ToString();
                    TextData.city = TextData.provincekey = CustomerDetailGridView.SelectedRows[0].Cells["City"].Value.ToString();
                    TextData.phone1 = CustomerDetailGridView.SelectedRows[0].Cells["Mobile No"].Value.ToString();
                    TextData.phone2 = CustomerDetailGridView.SelectedRows[0].Cells["Telephone No"].Value.ToString();
                    TextData.contact_person = CustomerDetailGridView.SelectedRows[0].Cells["Contact Person"].Value.ToString();
                    TextData.address = CustomerDetailGridView.SelectedRows[0].Cells["Address"].Value.ToString();
                    TextData.bank_name = CustomerDetailGridView.SelectedRows[0].Cells["Bank"].Value.ToString();
                    TextData.bank_account = CustomerDetailGridView.SelectedRows[0].Cells["Account #"].Value.ToString();
                    TextData.status = CustomerDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();
                    //string limit = CustomerDetailGridView.SelectedRows[0].Cells["Limit"].Value.ToString();
                    //TextData.last_balance = double.Parse(limit);

                    GetSetData.SaveLogHistoryDetails("Company Details Form", "Updating company [" + TextData.full_name + "  " + TextData.cus_code + "] details (Modify button click...)", role_id);

                    using (form_purchase_from add_customer = new form_purchase_from())
                    {
                        form_purchase_from.role_id = role_id;
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

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private void update_customer_datails(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo); 
            FillGridViewUsingPagination("");
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();

                GetSetData.query = @"select supplier_id from pos_suppliers where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_supplier_payables where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_suppliers where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================
                
                GetSetData.SaveLogHistoryDetails("Company Details Form", "Deleting company [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);

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
            TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier Name"].Value.ToString();

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
                    FillGridViewUsingPagination("notNull");
                    search_box.Text = "";
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

        private void btn_print_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Company Details Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo); 
            Button_controls.Print_Supplier_list_buttons();
        }

        private void fun_select_values_gridView()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                GetSetData.SaveLogHistoryDetails("Company Details Form", "Seleting company [" + TextData.full_name + "  " + TextData.cus_code + "]", role_id);
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            fun_select_values_gridView();
            fun_exit_form();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void form_supplier_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Company Details Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                Button_controls.Print_Supplier_list_buttons();
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
            else if (e.Control && e.KeyCode == Keys.S)
            {
                fun_select_values_gridView();
                fun_exit_form();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }

        private void CustomerDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (count != 1)
            {
                if (CustomerDetailGridView.Columns[e.ColumnIndex].Name == "Select")
                {
                    fun_select_values_gridView();
                    fun_exit_form();
                }
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
