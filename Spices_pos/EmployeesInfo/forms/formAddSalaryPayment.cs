using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Supplier_Chain_info.SalaryReceipt;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Supplier_Chain_info.forms
{
    public partial class formAddSalaryPayment : Form
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

        public formAddSalaryPayment()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static int user_id = 0;
        public static bool saveEnable;

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel9, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel10, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
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
                formAddSalaryPayment.role_id = role_id;
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_salaryPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                GetSetData.Permission = data.UserPermissions("new_salaryPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnUpdate.Visible = bool.Parse(GetSetData.Permission);
                // ***************************************************************************************************

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.Permission) == false)
                {
                    pnl_save.Visible = false;
                }

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_salaryPayment_savePrint", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_save_and_print.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void LoginEmployee()
        {
            try
            {
                GetSetData.Ids = data.UserPermissionsIds("emp_id", "pos_users", "user_id", user_id.ToString());

                txtCasher.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.Ids.ToString());
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void enableSaveButton()
        {
            if (saveEnable == false)
            {
                btnUpdate.Visible = false;
                savebutton.Visible = true;
                FormNamelabel.Text = "Create New Payroll";
                LoginEmployee();
                txtPaymentDate.Select();
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                //salesAndReturnsGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Payroll";
                fillAddProductsFormTextBoxes();
                FormNamelabel.Select();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtDate.Text = TextData.dates;
                time_text.Text = TextData.time;
                txtPaymentDate.Text = TextData.paymentDate;
                txtEmployeeName.Text = TextData.full_name;
                txtEmployeeCode.Text = TextData.cus_code;
                txtFromDate.Text = TextData.fromDate;
                txtToDate.Text = TextData.toDate;
                txtTotalDuration.Text = TextData.totalDuration;
                txtHourlyWages.Text = TextData.hourlyWages;
                txtDateWiseCommission.Text = TextData.commissionAmount;
                txtCommissionPaid.Text = TextData.commissionPayment.ToString();
                txtPreviousPaidCommission.Text = TextData.lastPaidCommission;
                txtCommissionBalance.Text = TextData.commissionBalance;
                txtPayment.Text = TextData.paymentAmount;
                txtPreviousPaidSalary.Text = TextData.lastPaidSalary;
                txtSalaryBalance.Text = TextData.balance.ToString();
                txtSalary.Text = TextData.totalSalary;

                txtCredits.Text = "0";
                txtReferences.Text = "nill";

                txtRemarks.Text = data.UserPermissions("remarks", "pos_salariesPaybook", "salary_id", TextData.salaryId);

                FillEmployeeDetails();
                FillGridViewUsingPagination();
                
                string cashier_id = data.UserPermissions("casher_id", "pos_salariesPaybook", "salary_id", TextData.salaryId);
                txtCasher.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", cashier_id);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillEmployeeDetails()
        {
            try
            {
                GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + txtEmployeeName.Text + "') and (emp_code = '" + txtEmployeeCode.Text + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select mobile_no from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                txtContactNo.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select cnic from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                txtCnic.Text = data.SearchStringValuesFromDb(GetSetData.query);

                //GetSetData.query = @"select salary from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                //txtSalary.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select daily_wages from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                txtHourlyWages.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select commission from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                txtComission.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select picture_path from pos_general_settings;";
                TextData.saved_image_path = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select image_path from pos_employees where (employee_id = '" + GetSetData.Ids.ToString() + "');";
                TextData.image_path = data.SearchStringValuesFromDb(GetSetData.query);

                if (TextData.image_path != "nill" && TextData.image_path != "")
                {
                    img_pic_box.Image = Image.FromFile(TextData.saved_image_path + TextData.image_path);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void FillComboBoxEmployeeName()
        {
            txtEmployeeName.Text = data.UserPermissions("full_name", "pos_employees", "emp_code", txtEmployeeCode.Text);
        }

        private void FillComboBoxEmployeeCodes()
        {
            txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "full_name", txtEmployeeName.Text);
        }

        private void FillGridViewUsingPagination()
        {
            try
            {
                GetSetData.query = @"SELECT format(date, 'dd/MMMM/yyyy') AS Date, time as [Time], format(paymentDate, 'dd/MMMM/yyyy') as [Payment Date], 
                                    full_name as [Employee Name], emp_code as [Employee Code], mobile_no as [Mobile No],
                                    salary as [Salary], amount  as [Payment], credits  as [Credits], balance as [Balance]
                                    from ViewEmployeeSalariesDetails
                                    where (full_name = '" + txtEmployeeName.Text + "') and (emp_code = '" + txtEmployeeCode.Text + "');";

                GetSetData.FillDataGridViewUsingPagination(purchaseDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refresh()
        {
            txtPaymentDate.Text = DateTime.Now.ToLongDateString();
            txtDate.Text = DateTime.Now.ToLongDateString();
            txtFromDate.Text = DateTime.Now.ToLongDateString();
            txtToDate.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToShortTimeString();
            //txtCasher.Text = "";
            txtEmployeeName.Text = "";
            txtEmployeeCode.Text = "";
            txtContactNo.Text = "";
            txtCnic.Text = "";
            txtHourlyWages.Text = "0";
            txtSalary.Text = "0";
            txtComission.Text = "0.00";
            txtReferences.Text = "";
            txtRemarks.Text = "";
            txtPayment.Text = "";
            txtCommissionPaid.Text = "";
            txtCredits.Text = "0.00";
            txtSalaryBalance.Text = "0.00";
            txtTotalDuration.Text = "0";
            img_pic_box.Image = null;
        }

        private void formAddSalaryPayment_Load(object sender, EventArgs e)
        {
            try
            {
                txtPaymentDate.Text = DateTime.Now.ToLongDateString();
                txtDate.Text = DateTime.Now.ToLongDateString();
                txtFromDate.Text = DateTime.Now.ToLongDateString();
                txtToDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToShortTimeString();
            

                system_user_permissions();
                enableSaveButton();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Employee Salary Payment Form", "Exit...", role_id);
            this.Close();
        }

        private void add_spplier_Click(object sender, EventArgs e)
        {
            using (Add_supplier add_customer = new Add_supplier())
            {
                Add_supplier.role_id = role_id;
                add_customer.ShowDialog();
            }
        }

        private void cash_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPayment.Text, e);
            data.NumericValuesOnly(txtCredits.Text, e);
        }

        private void credits_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtCredits.Text, e);
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void FillVariablesWithValues()
        {
            try
            {
                TextData.time = time_text.Text;
                TextData.paymentDate = txtPaymentDate.Text;
                TextData.fromDate = txtFromDate.Text;
                TextData.toDate = txtToDate.Text;
                TextData.casher = txtCasher.Text;
                TextData.full_name = txtEmployeeName.Text;
                TextData.cus_code = txtEmployeeCode.Text;
                TextData.reference = txtReferences.Text;
                TextData.totalDuration = txtTotalDuration.Text;
                TextData.remarks = txtRemarks.Text;
                TextData.credits = 0;
                TextData.balance = 0;
                TextData.cash = 0;
                TextData.commissionPayment = 0;

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }

                if (TextData.reference == "")
                {
                    TextData.reference = "nill";
                }

                if (TextData.casher == "")
                {
                    TextData.casher = "nill";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_into_db()
        {
            try
            {
                FillVariablesWithValues();

                if (txtCasher.Text != "")
                {
                    if (txtEmployeeName.Text != "" && txtEmployeeCode.Text != "")
                    {
                        if (txtPayment.Text != "")
                        {
                            if (txtCommissionPaid.Text != "")
                            {
                                if (txtCredits.Text != "")
                                {
                                    TextData.cash = double.Parse(txtPayment.Text);
                                    TextData.commissionPayment = double.Parse(txtCommissionPaid.Text);
                                    TextData.credits = double.Parse(txtCredits.Text);
                                    TextData.balance = double.Parse(txtSalaryBalance.Text);

                                    GetSetData.Ids = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.casher);

                                    GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + TextData.full_name + "') and (emp_code = '" + TextData.cus_code + "');";
                                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    // *******************************************************************************
                                    GetSetData.query = @"insert into pos_salariesPaybook values ('" + TextData.paymentDate + "' , '" + TextData.time + "' , '" + TextData.paymentDate + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + txtSalaryBalance.Text + "' , '" + TextData.reference + "' , '" + TextData.remarks + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "' , '" + TextData.fromDate + "' , '" + TextData.toDate + "' , '" + TextData.commissionPayment.ToString() + "' , '" + TextData.totalDuration + "' , '" + txtSalary.Text + "' , '" + txtDateWiseCommission.Text + "' , '" + txtHourlyWages.Text + "' , '" +txtPreviousPaidSalary.Text + "' , '" + txtPreviousPaidCommission.Text + "', '" + txtCommissionBalance.Text + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *****************************************************************************************

                                    double new_commission = double.Parse(txtComission.Text) - TextData.commissionPayment;

                                    if (new_commission < 0)
                                    {
                                        new_commission = 0;
                                    }

                                    GetSetData.query = @"update pos_employees set commission = '" + new_commission.ToString() + "' where (employee_id = '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *****************************************************************************************

                                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                    string capital = data.UserPermissions("total_capital", "pos_capital");

                                    if (GetSetData.Data == "Yes")
                                    {
                                        if (capital != "NULL" && capital != "")
                                        {
                                            TextData.cash = double.Parse(capital) - TextData.cash;
                                        }

                                        if (TextData.cash >= 0)
                                        {

                                            GetSetData.query = @"update pos_capital set total_capital = '" + TextData.cash.ToString() + "';";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                    }
                                    // *****************************************************************************************

                                    //GetSetData.SaveLogHistoryDetails("Add New Employee Salary Payment Form", "Saving employee salary [" + TextData.dates + "  " + TextData.time + "] details", role_id);
                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the credits!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the commission payment amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the payment amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee name and code!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the Casher!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if (insert_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
            }
        }

        private bool UpdateRecoveryDetailsdb()
        {
            try
            {
                FillVariablesWithValues();

                if (txtCasher.Text != "")
                {
                    if (txtEmployeeName.Text != "" && txtEmployeeCode.Text != "")
                    {
                        if (txtPayment.Text != "")
                        {
                            if (txtCommissionPaid.Text != "")
                            {
                                if (txtCredits.Text != "")
                                {
                                    TextData.cash = double.Parse(txtPayment.Text);
                                    TextData.commissionPayment = double.Parse(txtCommissionPaid.Text);
                                    TextData.credits = double.Parse(txtCredits.Text);
                                    TextData.balance = double.Parse(txtSalaryBalance.Text);

                                    GetSetData.Ids = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.casher);

                                    GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + TextData.full_name + "') and (emp_code = '" + TextData.cus_code + "');";
                                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                    //**************************************************************************************

                                    double previousPaid = data.NumericValues("amount", "pos_salariesPaybook", "salary_id", TextData.salaryId);
                                    double commission_db = data.NumericValues("commission", "pos_employees", "employee_id", GetSetData.fks.ToString());
                                    double commission_payment_db = data.NumericValues("commission_payment", "pos_salariesPaybook", "salary_id", TextData.salaryId);

                                    // *******************************************************************************

                                    GetSetData.query = @"update pos_salariesPaybook set date = '" + TextData.paymentDate + "', time = '" + TextData.time +"', paymentDate = '" + TextData.paymentDate + "' , amount = '" + TextData.cash.ToString() + "' , credits = '" + TextData.credits.ToString() + "' , balance = '" + txtSalaryBalance.Text + "' , reference = '" + TextData.reference.ToString() + "' , remarks = '" + TextData.remarks.ToString() + "' , casher_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' , form_date = '" + TextData.fromDate + "' , to_date = '" + TextData.toDate + "' , commission_payment = '" + txtCommissionPaid.Text + "' , total_duration = '" + TextData.totalDuration + "' , previous_paid_salary = '" + txtPreviousPaidSalary.Text + "' , previous_paid_commission = '" + txtPreviousPaidCommission.Text + "' , commission_balance = '" + txtCommissionBalance.Text + "' where (salary_id = '" + TextData.salaryId +"');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *****************************************************************************************

                                    double new_commission = (commission_db + commission_payment_db) - TextData.commissionPayment;

                                    if (new_commission < 0)
                                    {
                                        new_commission = 0;
                                    }

                                    GetSetData.query = @"update pos_employees set commission = '" + new_commission.ToString() + "' where (employee_id = '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *****************************************************************************************
                                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                    string capital = data.UserPermissions("total_capital", "pos_capital");

                                    if (GetSetData.Data == "Yes")
                                    {
                                        if (capital != "NULL" && capital != "")
                                        {
                                            TextData.cash = (double.Parse(capital) + previousPaid) - TextData.cash;
                                        }

                                        if (TextData.cash < 0)
                                        {
                                            TextData.cash = 0;
                                        }

                                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.cash.ToString() + "';";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    // *****************************************************************************************

                                    //GetSetData.SaveLogHistoryDetails("Add New Employee Salary Payment Form", "Updating employee salary [" + TextData.dates + "  " + TextData.time + "] details", role_id);
                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the credits!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the commission payment amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the payment amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee name and code!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the Casher!");
                    error.ShowDialog();
                }
                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateRecoveryDetailsdb())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();
            }
        }

        private void calculateSalaryBalanceAmount()
        {
            try
            {
                TextData.cash = 0;
                TextData.credits = 0;
                TextData.salary = 0;
                double previousPaid = 0;


                if (txtPayment.Text != "")
                {
                    TextData.cash = double.Parse(txtPayment.Text);
                }

                if (txtCredits.Text != "")
                {
                    TextData.credits = double.Parse(txtCredits.Text);
                }

                if (txtPreviousPaidSalary.Text != "")
                {
                    previousPaid = double.Parse(txtPreviousPaidSalary.Text);
                }

                if (txtSalary.Text != "")
                {
                    TextData.salary = double.Parse(txtSalary.Text);
                }

                TextData.balance = TextData.salary - ((TextData.cash + previousPaid) - TextData.credits);
              
                if (TextData.balance >= 0)
                {
                    txtSalaryBalance.Text = Math.Round(TextData.balance, 3).ToString();
                }
                else
                {                  
                    txtSalaryBalance.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void cash_text_TextChanged(object sender, EventArgs e)
        {
            calculateSalaryBalanceAmount();
        }

        private void txtCredits_TextChanged(object sender, EventArgs e)
        {
            calculateSalaryBalanceAmount();
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            TextData.dates = txtPaymentDate.Text;
            TextData.time = time_text.Text;
            TextData.full_name = txtEmployeeName.Text;
            TextData.cus_code = txtEmployeeCode.Text;


            if (saveEnable == false)
            {
                if (insert_into_db())
                {
                    formEmployeeSalaryRecipt reports = new formEmployeeSalaryRecipt();
                    reports.ShowDialog();
                    refresh();
                }
            }
            else if (saveEnable == true)
            {
                if (UpdateRecoveryDetailsdb())
                {
                    formEmployeeSalaryRecipt reports = new formEmployeeSalaryRecipt();
                    reports.ShowDialog();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void txt_sale_person_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");
        }

        private void txtEmployeeCode_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeCode, "fillComboBoxEmployeeNames", "emp_code");
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtCasher, "fillComboBoxEmployeeNames", "full_name");
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeName();
                FillEmployeeDetails();
                FillGridViewUsingPagination();
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeCodes();
                FillEmployeeDetails();
                FillGridViewUsingPagination();
            }
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            FillComboBoxEmployeeCodes();
            FillEmployeeDetails();
            FillGridViewUsingPagination();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxEmployeeCodes();
                FillEmployeeDetails();
                FillGridViewUsingPagination();

                decimal hourlyWages = 0;

                if (txtHourlyWages.Text != "")
                {
                    hourlyWages = decimal.Parse(txtHourlyWages.Text);
                }


                GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + txtEmployeeName.Text + "');";
                string employeeId = data.SearchStringValuesFromDb(GetSetData.query);

                if (employeeId != "")
                {
                    GetSetData.query = @"select sum(per_item_commission) from pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                         where (pos_sales_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (employee_id = '" + employeeId + "');";
                    string salesCommission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (salesCommission == "NULL" || salesCommission == "")
                    {
                        salesCommission = "0";
                    }
                    
                    GetSetData.query = @"select sum(per_item_commission) from pos_return_accounts inner join pos_returns_details on pos_returns_details.return_acc_id  = pos_return_accounts.return_acc_id
                                         where (pos_return_accounts.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (employee_id = '" + employeeId + "');";
                    string returnsCommission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (returnsCommission == "NULL" || returnsCommission == "")
                    {
                        returnsCommission = "0";
                    }

                    txtDateWiseCommission.Text = (Math.Round((double.Parse(salesCommission) - double.Parse(returnsCommission)), 2)).ToString();

                    //**************************************************************************************

                    GetSetData.query = @"select sum(amount) from pos_salariesPaybook
                                         where (paymentDate between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (employee_id = '" + employeeId + "');";
                    string previousPaidSalary = data.SearchStringValuesFromDb(GetSetData.query);

                    if (previousPaidSalary == "NULL" || previousPaidSalary == "")
                    {
                        previousPaidSalary = "0";
                    }

                    txtPreviousPaidSalary.Text = previousPaidSalary;
                    calculateSalaryBalanceAmount();


                    GetSetData.query = @"select sum(commission_payment) from pos_salariesPaybook
                                         where (paymentDate between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (employee_id = '" + employeeId + "');";
                    string previousPaidCommission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (previousPaidCommission == "NULL" || previousPaidCommission == "")
                    {
                        previousPaidCommission = "0";
                    }

                    txtPreviousPaidCommission.Text = previousPaidCommission;

                    calculateCommissionBalanceAmount();
                }
                else
                {
                    txtDateWiseCommission.Text = "0";
                }
                //**************************************************************************************


                txtTotalDuration.Text = GetSetData.ProcedureGetEmployeeDuration(txtFromDate.Text, txtToDate.Text, txtEmployeeName.Text);

                txtSalary.Text = Math.Round(double.Parse(GetSetData.ProcedureGetEmployeeTotalSalary(txtFromDate.Text, txtToDate.Text, txtEmployeeName.Text, hourlyWages)), 2).ToString();
            }
            catch (Exception es)
            {
                error.errorMessage("No Record Found!");
                error.ShowDialog();
            }
        }
        private void calculateCommissionBalanceAmount()
        {
            try
            {
                TextData.cash = 0;
                TextData.commission = 0;
                double previousPaid = 0;

                if (txtPreviousPaidCommission.Text != "")
                {
                    previousPaid = double.Parse(txtPreviousPaidCommission.Text);
                }

                if (txtCommissionPaid.Text != "")
                {
                    TextData.cash = double.Parse(txtCommissionPaid.Text);
                }

                if (txtDateWiseCommission.Text != "")
                {
                    TextData.commission = double.Parse(txtDateWiseCommission.Text);
                }
                
                TextData.balance = TextData.commission - (TextData.cash + previousPaid);

                if (TextData.balance >= 0)
                {
                    txtCommissionBalance.Text = Math.Round(TextData.balance, 3).ToString();
                }
                else
                {
                    txtCommissionBalance.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        private void txtCommissionPaid_TextChanged(object sender, EventArgs e)
        {
            calculateCommissionBalanceAmount();
        }
    }
}
