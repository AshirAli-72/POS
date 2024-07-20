using System;
using System.Windows.Forms;
using Login_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Stock_management.whole_stock_reports;
using RefereningMaterial;
using CounterSales_info.CustomerSalesReport;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;

namespace Stock_management.forms
{
    public partial class form_inventory_history : Form
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

        public form_inventory_history()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int user_id = 0;
        public static int role_id = 0;
        private bool showAllInventory;

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
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                //GetSetData.query = "select stock_print from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                //pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.query = "select stock_refresh from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                //pnl_refresh.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.query = "select stock_exit from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                //pnl_exit.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.query = "select stock_whole from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                //btn_wform_inventory_history.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.query = "select stock_low from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                //btn_low_inventory.Visible = bool.Parse(GetSetData.Data);


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
        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "SELECT top 500 * from ViewInventoryHistory";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Product Name] like '" + search_box.Text + "%' or [Barcode]  like '" + search_box.Text + "%' or [ID] like '" + search_box.Text + "%' or [Employee] like '" + search_box.Text + "%' or [Detail] like '%" + search_box.Text + "%')";
                }

                GetSetData.query = GetSetData.query + " order by [ID] desc";

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void manage_stock_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                showAllInventory = true;
                FillGridViewUsingPagination("");
                search_box.Text = "";
                system_user_permissions();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);

          
            FillGridViewUsingPagination("search");
          
        }

        private void show_all_Click_1(object sender, EventArgs e)
        {
            showAllInventory = true;

            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void Closebutton_Click_1(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Whole Inventory Form", "Exit...", role_id);
            Button_controls.mainMenu_buttons();
            this.Dispose();
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            formStockHistoryReport _obj = new formStockHistoryReport();
            _obj.Show();
            this.Dispose();
        }

        private void btn_wform_inventory_history_CheckedChanged(object sender, EventArgs e)
        {
            showAllInventory = true;
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
        }

        private void btn_low_inventory_CheckedChanged(object sender, EventArgs e)
        {
            showAllInventory = false;
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void form_inventory_history_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.SaveLogHistoryDetails("Whole Inventory Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                form_whole_stock report = new form_whole_stock();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.W)
            {
                btn_wform_inventory_history.Checked = true;
                btn_low_inventory.Checked = false;
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                btn_wform_inventory_history.Checked = false;
                btn_low_inventory.Checked = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
