using System;
using System.Windows.Forms;
using RefereningMaterial;
using Datalayer;
using Message_box_info.forms;
using Banks_Loan_info.BankLoanReports;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Banks_Loan_info.forms
{
    public partial class formBanksLoansDetails : Form
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

        public formBanksLoansDetails()
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
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
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
                formAddNewLoan.role_id = role_id;
                //GetSetData.addFormCopyrights(lblCopyrights);
                pnl_print.Visible = bool.Parse(data.UserPermissions("bankLoan_details_print", "pos_tbl_authorities_button_controls3", role_id));
                pnl_delete.Visible = bool.Parse(data.UserPermissions("bankLoan_details_delete", "pos_tbl_authorities_button_controls3", role_id));
                pnl_add_new.Visible = bool.Parse(data.UserPermissions("bankLoan_details_new", "pos_tbl_authorities_button_controls3", role_id));
                pnl_modify.Visible = bool.Parse(data.UserPermissions("bankLoan_details_modify", "pos_tbl_authorities_button_controls3", role_id));

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
                GetSetData.query = "select * from ViewBankLoanDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Date] like '" + search_box.Text + "%' or [Bank Title] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or [Branch Title] like '" + search_box.Text + "%' or [Repay Type] like '" + search_box.Text + "%' or [Status] like '" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formBanksLoansDetails_Load(object sender, EventArgs e)
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

        private void addNewDetails()
        {
            using (formAddNewLoan add_customer = new formAddNewLoan())
            {
                formAddNewLoan.role_id = role_id;
                formAddNewLoan.saveEnable = false;
                add_customer.ShowDialog();
                //GetSetData.SaveLogHistoryDetails("Bank  Loan Details Form", "Add new bank Loan button click...", role_id);
                //button_controls.AddNewLoanButtons();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.user_id = user_id;
            settings.role_id = role_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
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
                //GetSetData.query = "select bankLoan_details_modify from pos_tbl_authorities_button_controls3 where role_id = '" + role_id.ToString() + "';";
                //GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                //if (bool.Parse(GetSetData.Data) == true)
                //{
                    formAddNewLoan.saveEnable = true;
                    TextData.date = TextData.dateKey = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.time = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.transation_type = ProductsDetailGridView.SelectedRows[0].Cells["Repay Type"].Value.ToString();
                    TextData.bank_title =  ProductsDetailGridView.SelectedRows[0].Cells["Bank Title"].Value.ToString();
                    TextData.bankCode = ProductsDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                    TextData.branch_title = ProductsDetailGridView.SelectedRows[0].Cells["Branch Title"].Value.ToString();
                    TextData.principle = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Principle"].Value.ToString());
                    TextData.interest = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Interest"].Value.ToString());
                    TextData.totalAmount = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Total Amount"].Value.ToString());
                    TextData.remarks = ProductsDetailGridView.SelectedRows[0].Cells["Note"].Value.ToString();
                    TextData.status = ProductsDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();

                    using (formAddNewLoan add_customer = new formAddNewLoan())
                    {
                        formAddNewLoan.role_id = role_id;
                        add_customer.ShowDialog();
                        GetSetData.SaveLogHistoryDetails("Bank  Loan Details Form", "Modify Loan details [" + TextData.bank_title + " " + TextData.bankCode + "] ....", role_id);
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

        private void ProductsDetailGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.bank_title = ProductsDetailGridView.SelectedRows[0].Cells["Bank Title"].Value.ToString();
                TextData.bankCode = ProductsDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                TextData.totalAmount = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Total Amount"].Value.ToString());

                GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (bank_name = '" + TextData.bank_title + "') and (code = '" + TextData.bankCode + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_bankLoanPayables where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //*****************************************************************************************

                GetSetData.query = @"delete from pos_bankLoansDetails where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //*****************************************************************************************

                string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (GetSetData.Data == "Yes")
                {
                    if (capital != "NULL" && capital != "")
                    {
                        TextData.totalAmount = double.Parse(capital) - TextData.totalAmount;
                    }

                    if (TextData.totalAmount >= 0)
                    {
                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                //*****************************************************************************************               

                GetSetData.SaveLogHistoryDetails("Bank  Loan Details Form", "Deleting [" + TextData.bank_title + "  " + TextData.bankCode + "] details...", role_id);

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
            //GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Print bank loan details button click...", role_id);
            formBankLoanReports list = new formBankLoanReports();
            list.ShowDialog();
        }

        private void formBanksLoansDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.SaveLogHistoryDetails("Bank Transaction Details Form", "Print bank loan details button click...", role_id);
                formBankLoanReports list = new formBankLoanReports();
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
