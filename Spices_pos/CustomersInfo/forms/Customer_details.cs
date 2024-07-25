using System;
using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.DashboardInfo.Forms;
using RefereningMaterial;
using Spices_pos.CustomersInfo.controllers;
using System.Diagnostics;
using CounterSales_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Customers_info.forms
{
    public partial class Customer_details : Form
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


        public Customer_details()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        public static int count = 0;
        public static string selected_customer = "";
        public static string selected_customerCode = "";
        public static bool isDropDown = false;


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
            {   // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customer_details_print", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customer_details_print", role_id.ToString()));
                pnl_delete.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customer_details_delete", role_id.ToString()));
                pnl_add_new.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customer_details_new", role_id.ToString()));
                pnl_modify.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customer_details_modify", role_id.ToString()));
                pnl_select.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customer_details_select", role_id.ToString()));

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
                if (isDropDown)
                {
                    if (condition == "search")
                    {
                        GetSetData.query = @"SELECT  top 200 pos_customers.customer_id as [CID], pos_customers.date as [Date], pos_customers.full_name AS [Customer Name], pos_customers.cus_code AS Code, pos_customers.mobile_no AS [Cell No 1], pos_customers.telephone_no AS [Cell No 2], 
                                            pos_customers.address1 AS Address, pos_customers.points as [Points], pos_customers.discount as [Discount %], pos_customers.credit_limit AS [C.Limit], pos_customers.status as [Status] FROM pos_customers 
                                            where (full_name like '" + search_box.Text + "%' or date like '" + search_box.Text + "%' or  cus_code like '" + search_box.Text + "%' or mobile_no like '" + search_box.Text + "%' or telephone_no like '" + search_box.Text + "%');";
                    }
                    else
                    {
                        GetSetData.query = @"SELECT  top 200 pos_customers.customer_id as [CID], pos_customers.date as [Date], pos_customers.full_name AS [Customer Name], pos_customers.cus_code AS Code, pos_customers.mobile_no AS [Cell No 1], pos_customers.telephone_no AS [Cell No 2], 
                                            pos_customers.address1 AS Address, pos_customers.points as [Points], pos_customers.discount as [Discount %], pos_customers.credit_limit AS [C.Limit], pos_customers.status as [Status] FROM pos_customers 
                                            where (pos_customers.full_name LIKE '" + selected_customer + "%')";
                    }
                }
                else
                {
                    if (selected_customer != "")
                    {
                        GetSetData.query = @"SELECT  top 200 pos_customers.customer_id as [CID], pos_customers.date as [Date], pos_customers.full_name AS [Customer Name], pos_customers.cus_code AS Code, pos_customers.mobile_no AS [Cell No 1], pos_customers.telephone_no AS [Cell No 2], 
                                            pos_customers.address1 AS Address, pos_customers.points as [Points], pos_customers.discount as [Discount %], pos_customers.credit_limit AS [C.Limit], pos_customers.status as [Status] FROM pos_customers 
                                            where (full_name != 'nill') and (full_name = '" + selected_customer + "')";
                    }
                    else
                    {
                        GetSetData.query = @"SELECT  top 200 pos_customers.customer_id as [CID], pos_customers.date as [Date], pos_customers.full_name AS [Customer Name], pos_customers.cus_code AS Code, pos_customers.mobile_no AS [Cell No 1], pos_customers.telephone_no AS [Cell No 2], 
                                            pos_customers.address1 AS Address, pos_customers.points as [Points], pos_customers.discount as [Discount %], pos_customers.credit_limit AS [C.Limit], pos_customers.status as [Status] FROM pos_customers 
                                            where (pos_customers.full_name != 'nill')";
                    }


                    if (condition == "notNull")
                    {
                        GetSetData.query = GetSetData.query + " and ([Status] = 'Active') order by customer_id desc;";
                    }
                    else if (condition == "search")
                    {
                        GetSetData.query = GetSetData.query + " and (date like '" + search_box.Text + "%' or full_name like '" + search_box.Text + "%' or cus_code like '" + search_box.Text + "%' or mobile_no like '" + search_box.Text + "%' or telephone_no like '" + search_box.Text + "%') order by customer_id desc;";
                    }
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

        private void fun_exit_form()
        {
            //GetSetData.SaveLogHistoryDetails("Customer Details Form", "Exit...", role_id);

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

        private bool IsFormOpen(Type formType)
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


        private void Closebutton_Click(object sender, EventArgs e)
        {
            fun_exit_form();   
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
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64,64,64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            CustomerDetailGridView.Columns.Add(btn);
        }


        private void Customer_details_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();

                FillGridViewUsingPagination("notNull");
                
                search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void addNewDetails()
        {
            add_customer.saveEnable = false;
            add_customer.role_id = role_id;

            using (add_customer customer = new add_customer())
            {
                customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            search_box.Text = "";
            FillGridViewUsingPagination("notNull");
        }

        private bool fun_update_details()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("customer_details_modify", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    add_customer.saveEnable = true;
                    //TextData.batchNo = CustomerDetailGridView.SelectedRows[0].Cells["Batch #"].Value.ToString();
                    TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Customer Name"].Value.ToString();
                    TextData.full_name_key = TextData.full_name;
                    //TextData.fatherName = CustomerDetailGridView.SelectedRows[0].Cells["Father Name"].Value.ToString();
                    TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.cus_codekey = TextData.cus_code;
                    //TextData.cnic = CustomerDetailGridView.SelectedRows[0].Cells["CNIC"].Value.ToString();
                    //TextData.country = CustomerDetailGridView.SelectedRows[0].Cells["Country"].Value.ToString();
                    //TextData.province = CustomerDetailGridView.SelectedRows[0].Cells["Province"].Value.ToString();
                    TextData.countrykey = TextData.country;
                    TextData.provincekey = TextData.province;
                    TextData.phone1 = CustomerDetailGridView.SelectedRows[0].Cells["Cell No 1"].Value.ToString();
                    TextData.phone2 = CustomerDetailGridView.SelectedRows[0].Cells["Cell No 2"].Value.ToString();
                    TextData.address1 = CustomerDetailGridView.SelectedRows[0].Cells["Address"].Value.ToString();
                    TextData.points = CustomerDetailGridView.SelectedRows[0].Cells["Points"].Value.ToString();
                    TextData.discount = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Discount %"].Value.ToString());
                    //TextData.bank_name = CustomerDetailGridView.SelectedRows[0].Cells["Bank"].Value.ToString();
                    //TextData.bank_account = CustomerDetailGridView.SelectedRows[0].Cells["Account #"].Value.ToString();
                    TextData.status = CustomerDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();
                    string limit = CustomerDetailGridView.SelectedRows[0].Cells["C.Limit"].Value.ToString();
                    TextData.credit_limit = double.Parse(limit);

                    //GetSetData.SaveLogHistoryDetails("Customer Details Form", "Updating customer [" + TextData.full_name + "  " + TextData.cus_code + "] details (Modify button Click)", role_id);
                    add_customer.role_id = role_id;

                    using (add_customer customer = new add_customer())
                    {
                        customer.ShowDialog();
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
            GetSetData.SaveLogHistoryDetails("Customer Details Form", "Print button click....", role_id);
            Button_controls.CustomersDetailsButton();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            search_box.Text = "";
            FillGridViewUsingPagination("");
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Customer Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();

                GetSetData.Ids = data.UserPermissionsIds("customer_id", "pos_customers", "full_name", TextData.full_name);

                GetSetData.query = @"delete from pos_customer_lastCredits where customer_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_customers where customer_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                //GetSetData.SaveLogHistoryDetails("Customer Details Form", "Deleting customer [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);

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
            TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Customer Name"].Value.ToString();

            try
            {
                //this.Opacity = .850;
                //sure.Message_choose("Are you sure you want to delete '" + TextData.full_name.ToString() + "'");
                sure.ShowDialog();
                //this.Opacity = .999;

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

       //private void generalizedSourceToSendCustomerDetails()

        private void fun_select_values_gridView()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Customer Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                selected_customer = TextData.full_name;
                selected_customerCode = TextData.cus_code;

                if (generalSettings.ReadField("salesmanTips") == "Yes")
                {
                    if (Screen.AllScreens.Length > 1)
                    {
                        if (!IsFormOpen(typeof(form_salesman_tips)))
                        {
                            form_salesman_tips secondaryForm = new form_salesman_tips();

                            Screen secondaryScreen = Screen.AllScreens[1];
                            secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                            secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                            secondaryForm.WindowState = FormWindowState.Maximized;
                            secondaryForm.Show();
                        }
                    }
                }
                //GetSetData.SaveLogHistoryDetails("Customer Details Form", "Selecting customer [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);
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

        private void CustomerDetailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            clearDataGridViewItems();
           
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);

            if (count == 0)
            {
                createSelectButtonInGridView();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            clearDataGridViewItems();

            GetSetData.GunaButtonPreviousItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);

            if (count == 0)
            {
                createSelectButtonInGridView();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void Customer_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Customer Details Form", "Print button click....", role_id);
                Button_controls.CustomersDetailsButton();
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

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
