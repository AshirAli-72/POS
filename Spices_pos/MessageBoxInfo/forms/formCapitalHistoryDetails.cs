using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using RefereningMaterial;
using Message_box_info.CapitalHistoryReports;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.LoginInfo.controllers;

namespace Message_box_info.forms
{
    public partial class formCapitalHistoryDetails : Form
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

        public formCapitalHistoryDetails()
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
            //try
            //{
            GetSetData.addFormCopyrights(lblCopyrights);
            //    formCapitalAmount.role_id = role_id;

            //    // ***************************************************************************************************
            //    GetSetData.Data = data.UserPermissions("expenses_details_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
            //    pnl_print.Visible = bool.Parse(GetSetData.Data);

            //    // ***************************************************************************************************
            //    GetSetData.Data = data.UserPermissions("expenses_details_delete", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
            //    pnl_delete.Visible = bool.Parse(GetSetData.Data);

            //    // ***************************************************************************************************
            //    GetSetData.Data = data.UserPermissions("expenses_details_new", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
            //    pnl_add_new.Visible = bool.Parse(GetSetData.Data);

            //    // ***************************************************************************************************
            //    GetSetData.Data = data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
            //    pnl_modify.Visible = bool.Parse(GetSetData.Data);

            //    // ***************************************************************************************************
            //    GetSetData.Data = data.UserPermissions("expenses_details_refresh", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
            //    pnl_refresh.Visible = bool.Parse(GetSetData.Data);

            //    GetSetData.addFormCopyrights(lblCopyrights);
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void totalCapitalAndInvestments()
        {
            try
            {
                //txtTotalCapital.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                txtAvailableBalance.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                //txtInvestment.Text = data.UserPermissions("round(total_investments, 2)", "pos_capital");
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
                GetSetData.query = "select * from ViewCapitalHistoryDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Date] like '" + SearchByBox.Text + "%' or [Status] like '" + SearchByBox.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formCapitalHistoryDetails_Load(object sender, EventArgs e)
        {
            try
            {
                originalExStyle = -1;
                enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                SearchByBox.Text = "";
                totalCapitalAndInvestments();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.MenuScreen();
            this.Close();
        }

        private void addNewDetails()
        {
            using (formCapitalAmount add_customer = new formCapitalAmount())
            {
                formCapitalAmount.saveEnable = false;
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrunOffFormLevelDoubleBuffering();
            originalExStyle = -1;
            enableFormLevelDoubleBuffering = true;
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            SearchByBox.Text = "";
            FillGridViewUsingPagination("");
            totalCapitalAndInvestments();
        }

        private void SearchByBox_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private bool fun_update_details()
        {
            try
            {
                //GetSetData.Data = data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());

                //if (GetSetData.Data == "True")
                //{
                formCapitalAmount.saveEnable = true;
                    TextData.date = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.time = productDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.cashAmount = productDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString();
                    TextData.remarks = productDetailGridView.SelectedRows[0].Cells["Note"].Value.ToString();
                    TextData.status = productDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();
                    TextData.totalCapital = productDetailGridView.SelectedRows[0].Cells["Total Capital"].Value.ToString();
                    TextData.NetInvestment = productDetailGridView.SelectedRows[0].Cells["Total Investment"].Value.ToString();


                    using (formCapitalAmount add_customer = new formCapitalAmount())
                    {
                        GetSetData.SaveLogHistoryDetails("Capital History Details Form", "Updating capital History [" + TextData.date + "  " + TextData.time + "] details (Modify button click...)", role_id);
                        add_customer.ShowDialog();
                    }
                    
                //}

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                return false;
            }
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private void updates_expense_details(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.date = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.time = productDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();


                GetSetData.query = @"delete from pos_capital_history where (date = '" + TextData.date + "') and (time = '" + TextData.time + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Capital History Details Form", "Deleting capital history [" + TextData.date + "  " + TextData.time + "] details", role_id);
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
            TextData.date = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
            TextData.time = productDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.date + "'  '" + TextData.time + " '");
                sure.ShowDialog();
                this.Opacity = .999;

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
                error.errorMessage("'" + TextData.date + "  " + TextData.time + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            formCapitalHistoryReport report = new formCapitalHistoryReport();
            report.ShowDialog();
        }

        private void formCapitalHistoryDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                formCapitalHistoryReport report = new formCapitalHistoryReport();
                report.ShowDialog();
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
    }
}
