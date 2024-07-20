using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Settings_info.forms;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Expenses_info.forms
{
    public partial class UserDetails : Form
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
        public UserDetails()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;


        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = @"select pos_users.user_id as [User ID], full_name as [Name], roleTitle as [Role], username as [Username]   
                                     from pos_users inner join pos_roles on pos_users.role_id = pos_roles.role_id
                                     inner join pos_employees on pos_users.emp_id = pos_employees.employee_id";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where (full_name like '" + SearchByBox.Text + "%' or roleTitle like '" + SearchByBox.Text + "%' or username like '" + SearchByBox.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

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
            using (AddUser add_customer = new AddUser())
            {
                AddUser.role_id = role_id;
                AddUser.saveEnable = false;
            
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings main = new settings();
            main.Show();

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
            //GetSetData.ResetPageNumbers(lblPageNo);
            //ExpensesList report = new ExpensesList();
            //report.ShowDialog();
        }

        private bool fun_update_details()
        {
            try
            {
                    AddUser.saveEnable = true;

                    AddUser.user_id = productDetailGridView.SelectedRows[0].Cells["User ID"].Value.ToString();
                    AddUser.employeeName = productDetailGridView.SelectedRows[0].Cells["Name"].Value.ToString();
                    AddUser.roleTitle = productDetailGridView.SelectedRows[0].Cells["Role"].Value.ToString();
                    AddUser.username = productDetailGridView.SelectedRows[0].Cells["Username"].Value.ToString();
                    AddUser.password = data.UserPermissions("password", "pos_users", "user_id", productDetailGridView.SelectedRows[0].Cells["User ID"].Value.ToString());

                    using (AddUser add_customer = new AddUser())
                    {
                        AddUser.role_id = role_id;
                        add_customer.ShowDialog();
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
                string user_id_db = productDetailGridView.SelectedRows[0].Cells["User ID"].Value.ToString();

                GetSetData.query = @"delete from pos_users where (user_id = '" + user_id_db + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

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
            string user_id_db = productDetailGridView.SelectedRows[0].Cells["User ID"].Value.ToString();

            try
            {
                sure.Message_choose("Are you sure you want to delete '" + user_id_db + "'");
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
                error.errorMessage("'" + user_id_db + "' cannot be deleted!");
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

        private void UserDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.SaveLogHistoryDetails("Expenses Details Form", "Print button click...", role_id);
                //GetSetData.ResetPageNumbers(lblPageNo);
                //ExpensesList report = new ExpensesList();
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

        private void SearchByBox_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
