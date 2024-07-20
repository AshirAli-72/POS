using System;
using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using CounterSales_info.CustomerSalesReport;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.EmployeesInfo.controllers;

namespace Products_info.forms.RecipeDetails
{
    public partial class commission_details : Form
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
        public commission_details()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel9, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
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
                GetSetData.addFormCopyrights(lblCopyrights);
                //pnl_print.Visible = bool.Parse(data.UserPermissions("products_details_print", "pos_tbl_authorities_button_controls2", role_id));
                //pnl_delete.Visible = bool.Parse(data.UserPermissions("products_details_delete", "pos_tbl_authorities_button_controls2", role_id));
                //pnl_add_new.Visible = bool.Parse(data.UserPermissions("products_details_new", "pos_tbl_authorities_button_controls2", role_id));
                //pnl_modify.Visible = bool.Parse(data.UserPermissions("products_details_modify", "pos_tbl_authorities_button_controls2", role_id));


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
            this.productDetailGridView.DataSource = null;
            this.productDetailGridView.Refresh();
            productDetailGridView.Rows.Clear();
            productDetailGridView.Columns.Clear();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = @"select * from ViewEmployeeCommission";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([CID] like '" + SearchByBox.Text + "%' or [Employee Name] like '" + SearchByBox.Text + "%' or [Status] like '" + SearchByBox.Text + "%')";
                }

                clearDataGridViewItems();
                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
                createSelectButtonInGridView();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void createSelectButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Add Items";
            btn.Name = "Add Items";
            btn.Text = "Add Items";
            btn.Width = 70;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView.Columns.Add(btn);
        }


        private void product_details_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                SearchByBox.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void addNewDetails()
        {
            using (create_commission add_customer = new create_commission())
            {
                create_commission.role_id = role_id;
                create_commission.saveEnable = false;
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.mainMenu_buttons();
            this.Dispose();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            SearchByBox.Text = "";
            FillGridViewUsingPagination("");
        }

        private void search_box_TextChanged_1(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            employeeCommission.user_id = user_id;
            employeeCommission.isSuperAdmin = 1;
            employeeCommission report = new employeeCommission();
            report.Show();
            this.Dispose();
        }

        private bool fun_update_details()
        {
            try
            {
                TextData.commissionId = productDetailGridView.SelectedRows[0].Cells["CID"].Value.ToString();
                TextData.commissionTitle = productDetailGridView.SelectedRows[0].Cells["Commission Title"].Value.ToString();
                TextData.employee = productDetailGridView.SelectedRows[0].Cells["Employee Name"].Value.ToString();
                TextData.startDate = productDetailGridView.SelectedRows[0].Cells["Start Date"].Value.ToString();
                TextData.endDate = productDetailGridView.SelectedRows[0].Cells["End Date"].Value.ToString();
                TextData.startTime = productDetailGridView.SelectedRows[0].Cells["Start Time"].Value.ToString();
                TextData.endTime = productDetailGridView.SelectedRows[0].Cells["End Time"].Value.ToString();
                TextData.commissionAmount = productDetailGridView.SelectedRows[0].Cells["Commission Amount"].Value.ToString();
                TextData.commissionPercentage = productDetailGridView.SelectedRows[0].Cells["Commission %"].Value.ToString();
                TextData.status = productDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();


                create_commission.role_id = role_id;
                create_commission.saveEnable = true;
                create_commission add_customer = new create_commission();
                add_customer.ShowDialog();

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                return false;
            }
        }

        private void updates_purchase_details(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.commissionId = productDetailGridView.SelectedRows[0].Cells["CID"].Value.ToString();
                //*****************************************************************************************

                GetSetData.query = @"delete from pos_employee_commission_detail where (commission_id = '" + TextData.commissionId + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //*****************************************************************************************

                GetSetData.query = @"delete from pos_employee_commission where (commission_id = '" + TextData.commissionId + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //*****************************************************************************************

                //GetSetData.SaveLogHistoryDetails("Deal Deals Form", "Deleting Deal Deals Id[" + TextData.dealId.ToString() + "] details", role_id);
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
            TextData.commissionId = productDetailGridView.SelectedRows[0].Cells["CID"].Value.ToString();

            try
            {
                sure.Message_choose("Are you sure you want to delete '" + TextData.commissionId + "'");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    SearchByBox.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("'" + TextData.commissionId + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void product_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.ResetPageNumbers(lblPageNo);
                //deals_reports report = new deals_reports();
                //report.ShowDialog();
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
                SearchByBox.Select();
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextData.commissionId = productDetailGridView.SelectedRows[0].Cells["CID"].Value.ToString();

                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureEmployeeCommissionDetails", TextData.commissionId);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void productDetailGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "Add Items")
            {
                setCommissionItems.commissionID = productDetailGridView.SelectedRows[0].Cells["CID"].Value.ToString();
                setCommissionItems _obj = new setCommissionItems();
                _obj.ShowDialog();
            }
        }

        private void btnResetCommission_Click(object sender, EventArgs e)
        {
            formResetEmployeeCommission _obj = new formResetEmployeeCommission();
            _obj.ShowDialog();
        }
    }
}
