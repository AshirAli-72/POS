using System;
using System.Windows.Forms;
using Login_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Recoverier_info.Customer_Dues_Report;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Recoverier_info.forms
{
    public partial class CustomerDues : Form
    {
        //int originalExStyle = -1;
        //bool enableFormLevelDoubleBuffering = true;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        if (originalExStyle == -1)
        //            originalExStyle = base.CreateParams.ExStyle;

        //        CreateParams handleParam = base.CreateParams;

        //        if (enableFormLevelDoubleBuffering)
        //        {
        //            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
        //        }
        //        else
        //        {
        //            handleParam.ExStyle = originalExStyle;
        //        }

        //        return handleParam;
        //    }
        //}

        //public void TrunOffFormLevelDoubleBuffering()
        //{
        //    enableFormLevelDoubleBuffering = false;
        //    this.MinimizeBox = true;
        //    this.WindowState = FormWindowState.Minimized;
        //}

        public CustomerDues()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

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
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
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
                //GetSetData.Data = data.UserPermissions("dues_exit", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnl_exit.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("dues_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("dues_refresh", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnl_refresh.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Dues Detail Form", "Exit...", role_id);
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewCustomerDuesDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where [Customer Name] like '" + SearchByBox.Text + "%' or [Code]  like '" + SearchByBox.Text + "%' or [CNIC] like '" + SearchByBox.Text + "%' or [Mobile No] like '" + SearchByBox.Text + "%' or [Telephone No] like '" + SearchByBox.Text + "%' and ([Credits] != 0) and ([Credits] > 0);";
                }

                GetSetData.FillDataGridViewUsingPagination(CustomerDuesDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void CustomerDues_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                FillGridViewUsingPagination("");
                SearchByBox.Text = "";
                system_user_permissions();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo); 
            FillGridViewUsingPagination("");
            SearchByBox.Text = "";
        }

        private void SearchByBox_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo); 
            FillGridViewUsingPagination("search");
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Customer Dues Detail Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo); 
            form_cus_dues report = new form_cus_dues();
            report.ShowDialog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(CustomerDuesDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(CustomerDuesDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void CustomerDues_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Customer Dues Detail Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                form_cus_dues report = new form_cus_dues();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                SearchByBox.Select();
            }
        }
    }
}
