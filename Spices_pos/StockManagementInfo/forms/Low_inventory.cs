using System;
using System.Windows.Forms;
using Login_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Stock_management.Low_inventory_reports;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Stock_management.forms
{
    public partial class Low_inventory : Form
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

        public Low_inventory()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
     
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
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
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
                GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.query = "select stock_print from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.query = "select stock_refresh from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                pnl_refresh.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.query = "select stock_exit from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                pnl_exit.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination()
        {
            try
            {
                GetSetData.query = "select * from ViewLowInventory";
                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Low_inventory_Load(object sender, EventArgs e)
        {
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;

            FillGridViewUsingPagination();
            search_box.Text = "";
            system_user_permissions();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Low Inventory Form", "Exit...", role_id);
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination();
            search_box.Text = "";
        }

        private void search_box_TextChanged_1(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            GetSetData.FillDataGridView(ProductsDetailGridView, "ProcedureSearchingLowInventory", search_box.Text);
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Low Inventory Form", "Print button click...", role_id);
            form_low_inventory report = new form_low_inventory();
            report.ShowDialog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void Low_inventory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Low Inventory Form", "Print button click...", role_id);
                form_low_inventory report = new form_low_inventory();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
