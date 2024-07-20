using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Investors_info.PaymentsReports;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;

namespace Investors_info.forms
{
    public partial class formInvestorsPaybookDetails : Form
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

        public formInvestorsPaybookDetails()
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
                GetSetData.Data = data.UserPermissions("investor_paybookDetails_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_paybookDetails_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_paybookDetails_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("investor_paybookDetails_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
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
                GetSetData.query = "select * from ViewInvestorPaymentsDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where [Date] like '" + search_box.Text + "%' or [Investor] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or [Mobile No] like '" + search_box.Text + "%' or [CNIC] like '" + search_box.Text + "%';";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formInvestorsPaybookDetails_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.user_id = user_id;
            settings.role_id = role_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void addNewDetails()
        {
            using (formInvestorsPayments add_customer = new formInvestorsPayments())
            {
                formInvestorsPayments.role_id = role_id;
                formInvestorsPayments.saveEnable = false;
                GetSetData.SaveLogHistoryDetails("Investors Paybook Details Form", "Add new investor payment button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Investors Paybook Details Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            formInvestorsPaymentsReport report = new formInvestorsPaymentsReport();
            report.ShowDialog();
        }

        private bool fun_delete_recovery_details()
        {
            try
            {
                TextData.dates = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.times = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.cash = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Payment"].Value.ToString());

                string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (GetSetData.Data == "Yes")
                {
                    if (capital != "NULL" && capital != "")
                    {
                        TextData.cash = double.Parse(capital) + TextData.cash;
                    }

                    if (TextData.cash >= 0)
                    {
                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.cash.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                // *****************************************************************************************

                GetSetData.query = "delete from pos_investorPaybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() +"');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================
                GetSetData.SaveLogHistoryDetails("Investors Paybook Details Form", "Deleting investor payment [" + TextData.dates + "  " + TextData.times + "] details", role_id);

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                // MessageBox.Show(es.Message);
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            TextData.full_name = ProductsDetailGridView.SelectedRows[0].Cells["Investor"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.full_name.ToString() + "' payments details!");
                sure.ShowDialog();
                this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_recovery_details();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
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

        private void ModifyPurchasingDetails()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("investor_paybookDetails_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    formInvestorsPayments.saveEnable = true;
                    TextData.full_name = ProductsDetailGridView.SelectedRows[0].Cells["Investor"].Value.ToString();
                    TextData.cus_code = ProductsDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.dates = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.times = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.employee = ProductsDetailGridView.SelectedRows[0].Cells["Casher"].Value.ToString();
                    TextData.sharePercentage = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Share %"].Value.ToString());
                    TextData.investment = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Investment"].Value.ToString());
                    TextData.cash = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Payment"].Value.ToString());
                    TextData.reference = ProductsDetailGridView.SelectedRows[0].Cells["References"].Value.ToString();
                    TextData.remarks = ProductsDetailGridView.SelectedRows[0].Cells["Description"].Value.ToString();

                    GetSetData.SaveLogHistoryDetails("Investors Paybook Details Form", "Updating investor payment [" + TextData.dates + "  " + TextData.times + "] details (Modify button click...)", role_id);

                    using (formInvestorsPayments add_customer = new formInvestorsPayments())
                    {
                        formInvestorsPayments.role_id = role_id;
                        add_customer.ShowDialog();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void updates_purchase_details(object sender, DataGridViewCellEventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void formInvestorsPaybookDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Investors Paybook Details Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                formInvestorsPaymentsReport report = new formInvestorsPaymentsReport();
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
                ModifyPurchasingDetails();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }

    }
}
