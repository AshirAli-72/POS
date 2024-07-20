using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Settings_info.controllers;

namespace Investors_info.forms
{
    public partial class formInvestorDetails : Form
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
        public formInvestorDetails()
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
        public static int user_id = 0;
        public static string selected_customer = "";

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
                GetSetData.Data = data.UserPermissions("investor_details_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_details_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_details_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                sidePanel.Visible = true;
                setSideBarInSidePanel();
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

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewInvestorDetails where [Investor Name] != 'nill'";

                if (condition == "notNull")
                {
                    GetSetData.query = GetSetData.query + " and ([Status] = 'Active');";
                }
                else if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " and ([Date] like '" + search_box.Text + "%' or [Investor Name] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or  [CNIC] like '" + search_box.Text + "%' or [Mobile No] like '" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(CustomerDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void fun_exit_form()
        {
            settings.user_id = user_id;
            settings.role_id = role_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            fun_exit_form();   
        }

        private void formInvestorDetails_Load(object sender, EventArgs e)
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
            using (formNewInvestor add_customer = new formNewInvestor())
            {
                GetSetData.SaveLogHistoryDetails("Investors Details Form", "Add new investors button click...", role_id);
                formNewInvestor.role_id = role_id;
                formNewInvestor.saveEnable = false;
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
                GetSetData.Data = data.UserPermissions("investor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    formNewInvestor.saveEnable = true;
                    TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Investor Name"].Value.ToString();
                    TextData.full_name_key = TextData.full_name;
                    TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.cus_codekey = TextData.cus_code;
                    TextData.cnic = CustomerDetailGridView.SelectedRows[0].Cells["CNIC"].Value.ToString();
                    TextData.country = CustomerDetailGridView.SelectedRows[0].Cells["Country"].Value.ToString();
                    TextData.province = CustomerDetailGridView.SelectedRows[0].Cells["Province"].Value.ToString();
                    TextData.countrykey = TextData.country;
                    TextData.provincekey = TextData.province;
                    TextData.phone1 = CustomerDetailGridView.SelectedRows[0].Cells["Mobile No"].Value.ToString();
                    TextData.phone2 = CustomerDetailGridView.SelectedRows[0].Cells["Telephone No"].Value.ToString();
                    TextData.address1 = CustomerDetailGridView.SelectedRows[0].Cells["Address"].Value.ToString();

                    GetSetData.SaveLogHistoryDetails("Investors Details Form", "Updating investor [" + TextData.full_name + "  " + TextData.cus_code + "] details (Modify button click...)", role_id);

                    using (formNewInvestor add_customer = new formNewInvestor())
                    {
                        formNewInvestor.role_id = role_id;
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
            //GetSetData.SaveLogHistoryDetails("Investors Details Form", "Print button click...", role_id);
            buttonControls.PrintInvestorDetails();
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
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Investor Name"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();

                GetSetData.Ids = data.UserPermissionsIds("investor_id", "pos_investors", "full_name", TextData.full_name);

                GetSetData.query = @"delete from pos_investors where investor_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================
                GetSetData.SaveLogHistoryDetails("Investors Details Form", "Deleting investor [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);

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
            TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Investor Name"].Value.ToString();

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

        private void fun_select_values_gridView()
        {
            try
            {
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Investor Name"].Value.ToString();
                selected_customer = TextData.full_name;
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

        private void formInvestorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Investors Details Form", "Print button click...", role_id);
                buttonControls.PrintInvestorDetails();
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
