using System;
using System.Windows.Forms;
using RefereningMaterial;
using Datalayer;
using Message_box_info.forms;
using Banking_info.Reports;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Banking_info.forms
{
    public partial class form_bank_details : Form
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

        public form_bank_details()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
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
                form_add_transaction.role_id = role_id;
                pnl_print.Visible = bool.Parse(data.UserPermissions("banking_details_print", "pos_tbl_authorities_button_controls2", role_id));
                pnl_delete.Visible = bool.Parse(data.UserPermissions("banking_details_delete", "pos_tbl_authorities_button_controls2", role_id));
                pnl_add_new.Visible = bool.Parse(data.UserPermissions("banking_details_new", "pos_tbl_authorities_button_controls2", role_id));
                pnl_modify.Visible = bool.Parse(data.UserPermissions("banking_details_modify", "pos_tbl_authorities_button_controls2", role_id));
                //GetSetData.addFormCopyrights(lblCopyrights);

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
                GetSetData.query = "select * from ViewBankDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Date] like '" + search_box.Text + "%' or [T.Status] like '" + search_box.Text + "%' or [Bank Title] like '" + search_box.Text + "%' or [Branch] like '" + search_box.Text + "%' or [Account Title] like '" + search_box.Text + "%' or [Account #] like '" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_bank_details_Load(object sender, EventArgs e)
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
            //GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Exit...", role_id);
            settings.role_id = role_id;
            settings.user_id = user_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void addNewDetails()
        {
            using (form_add_transaction add_customer = new form_add_transaction())
            {
                form_add_transaction.saveEnable = false;
                GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Add new transaction button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            this.Opacity = .850;
            fun_update_details();
            this.Opacity = .999;
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private bool fun_update_details()
        {
            try
            {
                GetSetData.query = "select banking_details_modify from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "True")
                {
                    form_add_transaction.saveEnable = true;
                    TextData.date = TextData.dateKey= ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.time = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.employee = ProductsDetailGridView.SelectedRows[0].Cells["Employee"].Value.ToString();
                    TextData.status = ProductsDetailGridView.SelectedRows[0].Cells["T.Status"].Value.ToString();
                    TextData.transation_type = ProductsDetailGridView.SelectedRows[0].Cells["T.Type"].Value.ToString();
                    TextData.bank_title = ProductsDetailGridView.SelectedRows[0].Cells["Bank Title"].Value.ToString();
                    TextData.branch_title = ProductsDetailGridView.SelectedRows[0].Cells["Branch"].Value.ToString();
                    TextData.account = ProductsDetailGridView.SelectedRows[0].Cells["Account Title"].Value.ToString();
                    TextData.account_no = ProductsDetailGridView.SelectedRows[0].Cells["Account #"].Value.ToString();
                    TextData.amount = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString());

                    using (form_add_transaction add_customer = new form_add_transaction())
                    {
                        GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Modify transaction details button click...", role_id);
                        add_customer.ShowDialog();
                    }
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                return false;
            }
        }

        private void ProductsDetailGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.date = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.time = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();

                GetSetData.query = @"select banking_id from pos_banking_details where date = '" + TextData.date.ToString() + "' and time = '" + TextData.time.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_banking_details where banking_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================
                GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Deleting [" + TextData.date + "  " + TextData.time + "] details...", role_id);

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
            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete this record!");
                sure.ShowDialog();
                this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    search_box.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("This record cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Print bank transaction details button click...", role_id);
            bank_detail_reports list = new bank_detail_reports();
            list.ShowDialog();
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
            
        }

        private void form_bank_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Print bank transaction details button click...", role_id);
                bank_detail_reports list = new bank_detail_reports();
                list.ShowDialog();
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
