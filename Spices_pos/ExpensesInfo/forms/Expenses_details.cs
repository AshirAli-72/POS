using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using Expenses_info.ExpensesListReport;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Settings_info.controllers;

namespace Expenses_info.forms
{
    public partial class Expenses_details : Form
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

        public Expenses_details()
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
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_details_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_details_delete", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_details_new", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("expenses_details_refresh", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnl_refresh.Visible = bool.Parse(GetSetData.Data);

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
                GetSetData.query = "select * from ViewExpensesDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Expense Title] like '" + SearchByBox.Text + "%' or [Date] like '" + SearchByBox.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Expenses_details_Load(object sender, EventArgs e)
        {
            try
            {

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
            using (Add_expenses add_customer = new Add_expenses())
            {
                Add_expenses.user_id = user_id;
                Add_expenses.role_id = role_id;
                Add_expenses.saveEnable = false;
                //GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Add new expenses button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Exit...", role_id);
            buttonControls.mainMenu_buttons();

            this.Dispose();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            SearchByBox.Text = "";
            FillGridViewUsingPagination("");
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Print button click...", role_id); 
            GetSetData.ResetPageNumbers(lblPageNo);
            ExpensesList report = new ExpensesList();
            report.ShowDialog();
        }

        private bool fun_update_details()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    Add_expenses.saveEnable = true;
                    TextData.dates = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.times = productDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.expense_title = productDetailGridView.SelectedRows[0].Cells["Expense Title"].Value.ToString();
                    TextData.expense_title_key = TextData.expense_title;
                    TextData.amount = double.Parse(productDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString());
                    TextData.edit_amount = TextData.amount;
                    //TextData.net_total = double.Parse(productDetailGridView.SelectedRows[0].Cells["Net Total"].Value.ToString());
                    TextData.comments = productDetailGridView.SelectedRows[0].Cells["Note"].Value.ToString();

                 
                    using (Add_expenses add_customer = new Add_expenses())
                    {
                        Add_expenses.user_id = user_id;
                        Add_expenses.role_id = role_id;
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

        private void updates_expense_details(object sender, DataGridViewCellEventArgs e)
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
                TextData.dates = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.times = productDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.expense_title = productDetailGridView.SelectedRows[0].Cells["Expense Title"].Value.ToString();
                TextData.amount = double.Parse(productDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString());

                GetSetData.query = @"select expense_id from pos_expense_details where date = '" + TextData.dates.ToString() + "' and time = '" + TextData.times.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_expense_items where expense_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_expense_details where expense_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (GetSetData.Data == "Yes")
                {
                    if (capital != "NULL" && capital != "")
                    {
                        TextData.amount = double.Parse(capital) + TextData.amount;
                    }

                    if (TextData.amount >= 0)
                    {
                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.amount.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                // *****************************************************************************************

                GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Deleting expense [" + TextData.dates + "  " + TextData.times + "] details", role_id); 
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
            TextData.expense_title = productDetailGridView.SelectedRows[0].Cells["Expense Title"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.expense_title.ToString() + "'");
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
                error.errorMessage("'" + TextData.expense_title.ToString() + "' cannot be deleted!");
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

        private void Expenses_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                ExpensesList report = new ExpensesList();
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
