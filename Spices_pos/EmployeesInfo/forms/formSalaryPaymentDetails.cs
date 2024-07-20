using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.EmployeesInfo.controllers;

namespace Supplier_Chain_info.forms
{
    public partial class formSalaryPaymentDetails : Form
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

        public formSalaryPaymentDetails()
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
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("salariesPaybook_details_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("salariesPaybook_details_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("salariesPaybook_details_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("salariesPaybook_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
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
                GetSetData.query = @"SELECT salary_id as [SID], format(date, 'dd/MMMM/yyyy') AS Date, time as [Time], format(paymentDate, 'dd/MMMM/yyyy') as [Payment Date], 
                                    full_name as [Employee Name], emp_code as [Employee Code], mobile_no as [Mobile No], form_date as [From Date], to_date as [To Date], hourly_wages as [Hourly Wages], total_duration as [Duration],
                                    salary as [Total Salary], amount  as [Paid Salary], previous_paid_salary as [Last Paid Salary], balance as [Balance],  commission as [Total Commission], commission_payment as [Paid Commission], previous_paid_commission as [Last Paid Commission], commission_balance as [Com.Balance]
                                    from ViewEmployeeSalariesDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where (full_name like '" + search_box.Text + "%' or emp_code like '" + search_box.Text + "%' or cnic like '" + search_box.Text + "%'  or mobile_no like '" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(CustomerDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formSalaryPaymentDetails_Load(object sender, EventArgs e)
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
            using (formAddSalaryPayment add_customer = new formAddSalaryPayment())
            {
                //GetSetData.SaveLogHistoryDetails("Employee Salary Payment Details Form", "Add new salary payment button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                formAddSalaryPayment.user_id = user_id;
                formAddSalaryPayment.role_id = role_id;
                formAddSalaryPayment.saveEnable = false;
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
            search_box.Text = "";
            FillGridViewUsingPagination("");
        }

        private bool fun_update_details()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("salariesPaybook_details_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());

                if (bool.Parse(GetSetData.Data) == true)
                {
                    formAddSalaryPayment.saveEnable = true;
                    TextData.salaryId = CustomerDetailGridView.SelectedRows[0].Cells["SID"].Value.ToString();
                    TextData.dates = CustomerDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.time = CustomerDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                    TextData.paymentDate = CustomerDetailGridView.SelectedRows[0].Cells["Payment Date"].Value.ToString();
                    TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Employee Name"].Value.ToString();
                    TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Employee Code"].Value.ToString();
                    TextData.fromDate = CustomerDetailGridView.SelectedRows[0].Cells["From Date"].Value.ToString();
                    TextData.toDate = CustomerDetailGridView.SelectedRows[0].Cells["To Date"].Value.ToString();
                    TextData.totalDuration = CustomerDetailGridView.SelectedRows[0].Cells["Duration"].Value.ToString();
                    TextData.hourlyWages = CustomerDetailGridView.SelectedRows[0].Cells["Hourly Wages"].Value.ToString();
                    TextData.totalSalary = CustomerDetailGridView.SelectedRows[0].Cells["Total Salary"].Value.ToString();
                    TextData.commissionAmount = CustomerDetailGridView.SelectedRows[0].Cells["Total Commission"].Value.ToString();
                    TextData.commissionPayment= double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Paid Commission"].Value.ToString());
                    TextData.lastPaidCommission = CustomerDetailGridView.SelectedRows[0].Cells["Last Paid Commission"].Value.ToString();
                    TextData.commissionBalance = CustomerDetailGridView.SelectedRows[0].Cells["Com.Balance"].Value.ToString();
                    TextData.paymentAmount = CustomerDetailGridView.SelectedRows[0].Cells["Paid Salary"].Value.ToString();
                    TextData.lastPaidSalary = CustomerDetailGridView.SelectedRows[0].Cells["Last Paid Salary"].Value.ToString();
                    TextData.balance = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Balance"].Value.ToString());

                    //GetSetData.SaveLogHistoryDetails("Employee Salary Payment Details Form", "Updating employee [" + TextData.full_name + "   " + TextData.cus_code + "] salary payment details (Modify button click...)", role_id);

                    using (formAddSalaryPayment add_customer = new formAddSalaryPayment())
                    {
                        formAddSalaryPayment.user_id = user_id;
                        formAddSalaryPayment.role_id = role_id;
                        add_customer.ShowDialog();
                    }

                    return true;
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message); // "Please select the row first!"
                error.ShowDialog();
                return false;
            }
        }

        private void update_supplier_details(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings.user_id = user_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Employee Salary Payment Details Form", "Print employee salary payment list...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            Button_controls.PrintSalariesPaymentsListbuttons();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.salaryId = CustomerDetailGridView.SelectedRows[0].Cells["SID"].Value.ToString();
                TextData.cash = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Payment Amount"].Value.ToString());
                double commissionPayment = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Paid Commission"].Value.ToString());

                //*****************************************************************************************

                string employee_id = data.UserPermissions("employee_id", "pos_salariesPaybook", "salary_id", TextData.salaryId);
                double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id);

                employeeCommissionDb += commissionPayment;

                GetSetData.query = @"update pos_employees set commission = '" + employeeCommissionDb.ToString() + "' where (employee_id = '" + employee_id + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //*****************************************************************************************


                GetSetData.query = @"delete from pos_salariesPaybook where salary_id = '" + TextData.salaryId + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //*****************************************************************************************

                string capital = data.UserPermissions("total_capital", "pos_capital");
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
                //*****************************************************************************************  

                //GetSetData.SaveLogHistoryDetails("Employee Salary Payment Details Form", "Deleting employee [" + TextData.dates + "   " + TextData.time + "] salary payment details", role_id);

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
                sure.Message_choose("Are you sure you want to delete this record");
                sure.ShowDialog();

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

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void formSalaryPaymentDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.SaveLogHistoryDetails("Employee Salary Payment Details Form", "Print employee salary payment list...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                Button_controls.PrintSalariesPaymentsListbuttons();
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
