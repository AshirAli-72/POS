using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Banking_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Banks_Loan_info.forms
{
    public partial class formAddNewLoanPayment : Form
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

        public formAddNewLoanPayment()
         {
             InitializeComponent();
            setFormColorsDynamically();
         }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
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
                //GetSetData.addFormCopyrights(lblCopyrights);
                Add_supplier.role_id = role_id;
                form_status.role_id = role_id;
                formAddNewLoan.role_id = role_id;
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_bankLoanPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Permission = data.UserPermissions("new_bankLoanPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnUpdate.Visible = bool.Parse(GetSetData.Permission);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.Permission) == false)
                {
                    pnl_save.Visible = false;
                }

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_bankLoanPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_exit.Visible = bool.Parse(GetSetData.Data);
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
                GetSetData.Ids = data.UserPermissionsIds("emp_id", "pos_role", "role_id", role_id.ToString());
                txtEmployeeName.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "employee_id", GetSetData.Ids.ToString());
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
                FormNamelabel.Text = "Create New Loan Ledger";
                LoginEmployee();
                txtPaymentDate.Focus();
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                //salesAndReturnsGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Loan Ledger";
                fillAddProductsFormTextBoxes();
                FormNamelabel.Select();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtDate.Text = TextData.date;
                time_text.Text = TextData.time;
                txtPaymentDate.Text = TextData.paymentDate;
                txtBankName.Text = TextData.bank_title;
                txtBankCode.Text = TextData.bankCode;
                txtEmployeeName.Text = TextData.employee;
                txtCash.Text = TextData.CashAmount.ToString();
                txtBalance.Text = TextData.credits.ToString();
                txtReferences.Text = TextData.reference;
                txtRemarks.Text = TextData.remarks;
                txtStatus.Text = TextData.status;

                FillComboBoxBankDetails();
                FillGridViewUsingPagination();

                GetSetData.query = "select loanPaybook_id from pos_bankLoanPaybook where (date = '" + TextData.date.ToString() + "') and (time = '" + TextData.time.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                // *********************************************************************

                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_bankLoanPaybook", "loanPaybook_id", GetSetData.Ids.ToString());
                txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "employee_id", GetSetData.fks.ToString());
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxBankDetails()
        {
            // Getting the Customer last credits from the database to add the credit amount with it
            GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (code = '" + txtBankCode.Text + "') and (bank_name = '" + txtBankName.Text + "');";
            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
            //******************************************************************************************

            GetSetData.query = @"select principle from pos_bankLoansDetails where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
            txtPrincipleAmount.Text = GetSetData.Data;
            //******************************************************************************************

            GetSetData.query = @"select interest from pos_bankLoansDetails where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
            txtInterest.Text = GetSetData.Data;
            //******************************************************************************************

            GetSetData.query = @"select totalAmount from pos_bankLoansDetails where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
            txtTotalAmount.Text = GetSetData.Data;
            //******************************************************************************************

            GetSetData.query = @"select last_balance from pos_bankLoanPayables where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
            GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
            txtPreviousPayables.Text = GetSetData.Data;
        }

        private void FillComboBoxBankName()
        {
            txtBankName.Text = data.UserPermissions("bank_name", "pos_bankLoansDetails", "code", txtBankCode.Text);
        }

        private void FillComboBoxBankCode()
        {
            txtBankCode.Text = data.UserPermissions("code", "pos_bankLoansDetails", "bank_name", txtBankName.Text);
        }

        private void FillComboBoxBankCodesCodes()
        {
            txtBankCode.Text = data.UserPermissions("code", "pos_bankLoansDetails", "bank_name", txtBankName.Text);
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
                                    full_name as [Employee], bank_name as [Bank Title], code as [Code], amount as [Payment], 
                                    previous_payables as [Last Balance], balance as [Payables], reference as [References], remarks as [Note], status_title as [Status]
                                    FROM ViewBankLoanPaybookDetails where (bank_name = '" + txtBankName.Text + "') and (code = '" + txtBankCode.Text + "');";

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
            time_text.Text = DateTime.Now.ToLongTimeString();
            txtStatus.Text = "";
            txtBankName.Text = "";
            txtBankCode.Text = "";
            txtPrincipleAmount.Text = "0";
            txtInterest.Text = "0";
            txtTotalAmount.Text = "0.00";
            txtReferences.Text = "";
            txtRemarks.Text = "";
            txtCash.Text = "";
            txtBalance.Text = "0.00";
            txtPreviousPayables.Text = "0.00";
        }

        private void formAddNewLoanPayment_Load(object sender, EventArgs e)
        {
            try
            {
                txtPaymentDate.Text = DateTime.Now.ToLongDateString();
                txtDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToLongTimeString();

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
            GetSetData.SaveLogHistoryDetails("Add Bank Loan Payments Form", "Exit...", role_id);
            this.Close();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            using (form_status add_customer = new form_status())
            {
                add_customer.ShowDialog();
                //Button_controls.status_button();
            }
        }

        private void add_sub_category_Click(object sender, EventArgs e)
        {
            using (formAddNewLoan add_customer = new formAddNewLoan())
            {
                formAddNewLoan.role_id = role_id;
                add_customer.ShowDialog();
            }
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
            data.NumericValuesOnly(txtCash.Text, e);
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxBankName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
            txtEmployeeName.Text = "";
            txtEmployeeCode.Text = "";
        }

        private void FillVariablesWithValues()
        {
            try
            {
                TextData.paymentDate = txtPaymentDate.Text;
                TextData.status = txtStatus.Text;
                TextData.bank_title = txtBankName.Text;
                TextData.bankCode = txtBankCode.Text;
                TextData.employee = txtEmployeeName.Text;
                TextData.employeeCode = txtEmployeeCode.Text;
                TextData.reference = txtReferences.Text;
                TextData.remarks = txtRemarks.Text;
                TextData.credits = 0;
                TextData.lastCredits = 0;
                TextData.CashAmount = 0;

                if (txtBalance.Text != "")
                {
                    TextData.credits = double.Parse(txtBalance.Text);
                }

                if (txtPreviousPayables.Text != "")
                {
                    TextData.lastCredits = double.Parse(txtPreviousPayables.Text);
                }

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }

                if (TextData.reference == "")
                {
                    TextData.reference = "nill";
                }

                if (TextData.employee == "")
                {
                    TextData.employee = "nill";
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

                if (txtEmployeeName.Text != "")
                {
                    if (txtBankName.Text != "" && txtBankCode.Text != "")
                    {
                        if (txtCash.Text != "")
                        {
                            txtDate.Text = DateTime.Now.ToLongDateString();
                            TextData.date = txtDate.Text;
                            TextData.time = time_text.Text;
                            TextData.CashAmount = double.Parse(txtCash.Text);

                            GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (bank_name = '" + TextData.bank_title + "') and (code = '" + TextData.bankCode + "');";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            // *******************************************************************************
                            GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + TextData.employee + "');";
                            GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            int status_id = data.UserPermissionsIds("status_id", "pos_transaction_status", "status_title", TextData.status);
                            // *******************************************************************************
                         
                            GetSetData.query = @"insert into pos_bankLoanPaybook values ('" + TextData.date + "' , '" + TextData.time + "' , '" + TextData.paymentDate + "' , '" + TextData.reference + "' , '" + TextData.remarks + "' , '" + TextData.CashAmount.ToString() + "'  , '" + TextData.lastCredits.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + status_id.ToString() +"', '" + GetSetData.fks.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            // *******************************************************************************
                            TextData.lastCredits = data.NumericValues("last_balance", "pos_bankLoanPayables", "BankLoan_id", GetSetData.Ids.ToString());

                            if (TextData.lastCredits == -1)
                            {
                                GetSetData.query = @"insert into pos_bankLoanPayables values ('" + TextData.credits.ToString() + "' , '" + TextData.date.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            else if (TextData.lastCredits != -1 && TextData.lastCredits >= 0)
                            {
                                GetSetData.query = @"update pos_bankLoanPayables set last_balance = '" + TextData.credits.ToString() + "' , last_payment = '" + TextData.date.ToString() + "' where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }


                            //========================================================================================================
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                            string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");

                            if (GetSetData.Data == "Yes")
                            {
                                if (capital != "NULL" && capital != "")
                                {
                                    TextData.totalAmount = double.Parse(capital) - TextData.CashAmount;

                                    if (TextData.totalAmount >= 0)
                                    {
                                        capital = TextData.totalAmount.ToString();
                                    }
                                    else
                                    {
                                        capital = "0";
                                    }
                                }

                                GetSetData.query = "update pos_capital set total_capital = '" + capital.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            // *****************************************************************************************

                            GetSetData.SaveLogHistoryDetails("Add Bank Loan Payments Form", "Saving bank loan payment[" + TextData.date + "  " + TextData.time + "] details", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please enter the payable amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the bank title and code!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the employee!");
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
                //this.Opacity = .850;
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                //this.Opacity = .999;
                refresh();
            }
        }

        private bool UpdateRecoveryDetailsdb()
        {
            try
            {
                FillVariablesWithValues();

                if (txtEmployeeName.Text != "")
                {
                    if (txtBankName.Text != "" && txtBankCode.Text != "")
                    {
                        if (txtCash.Text != "")
                        {
                            TextData.CashAmount = double.Parse(txtCash.Text);

                            GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (bank_name = '" + TextData.bank_title + "') and (code = '" + TextData.bankCode + "');";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            // *******************************************************************************
                            GetSetData.query = @"select employee_id from pos_employees where (full_name = '" + TextData.employee + "');";
                            GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                             int status_id = data.UserPermissionsIds("status_id", "pos_transaction_status", "status_title", TextData.status);
                            // *******************************************************************************
                            GetSetData.query = @"select loanPaybook_id from pos_bankLoanPaybook where (date = '" + TextData.date.ToString() + "') and (time = '" + TextData.time.ToString() + "') and (BankLoan_id = '" + GetSetData.Ids.ToString() + "');";
                            int loanPayment_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            // *******************************************************************************
                            double previousPaid = data.NumericValues("amount", "pos_bankLoanPaybook", "loanPaybook_id", loanPayment_id.ToString());

                            GetSetData.query = @"update pos_bankLoanPaybook set paymentDate = '" + TextData.paymentDate + "' , reference = '" + TextData.reference + "' , remarks = '" + TextData.remarks + "' , amount = '" + TextData.CashAmount.ToString() + "'  , previous_payables = '" + TextData.lastCredits.ToString() + "' , balance = '" + TextData.credits.ToString() + "' , status_id = '" + status_id.ToString() + "',  BankLoan_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (loanPaybook_id = '" + loanPayment_id.ToString() +"');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            // *******************************************************************************
                            TextData.lastCredits = data.NumericValues("last_balance", "pos_bankLoanPayables", "BankLoan_id", GetSetData.Ids.ToString());

                            TextData.credits = (TextData.lastCredits + previousPaid) - TextData.CashAmount;

                            if (TextData.credits < 0)
                            {
                                TextData.credits = 0;
                            }

                            if (TextData.lastCredits != -1 && TextData.credits >= 0)
                            {
                                GetSetData.query = @"update pos_bankLoanPayables set last_balance = '" + TextData.credits.ToString() + "' , last_payment = '" + TextData.date.ToString() + "' where BankLoan_id = '" + GetSetData.Ids.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }

                            string capital = data.UserPermissions("total_capital", "pos_capital");
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                            if (GetSetData.Data == "Yes")
                            {
                                if (capital != "NULL" && capital != "")
                                {
                                    TextData.totalAmount = ((double.Parse(capital) + previousPaid) - TextData.CashAmount);
                                }

                                if (TextData.totalAmount >= 0)
                                {
                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                            }
                            // *****************************************************************************************

                            GetSetData.SaveLogHistoryDetails("Add Bank Loan Payments Form", "Updating bank payment [" + TextData.date + "  " + TextData.time + "] details", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please enter the payable amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the bank title and code!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the employee!");
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
                //this.Opacity = .850;
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();
                //this.Opacity = .999;
            }
        }

        private void cash_text_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                TextData.CashAmount = 0;
                TextData.lastCredits = double.Parse(txtPreviousPayables.Text);

                if (txtCash.Text != "")
                {
                    TextData.CashAmount = double.Parse(txtCash.Text);
                }

                if (TextData.CashAmount <= TextData.lastCredits)
                {
                    TextData.credits = TextData.lastCredits - TextData.CashAmount;
                    txtBalance.Text = TextData.credits.ToString();
                }
                else
                {
                    //cash_text.Text = "";
                    txtBalance.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            //TextData.date = txtPaymentDate.Text;
            //TextData.time = time_text.Text;
            //TextData.bank_title = txtBankName.Text;
            //TextData.bankCode = txtBankCode.Text;
            //TextData.lastCredits = 0;

            //if (txtPreviousPayables.Text != "")
            //{
            //    TextData.lastCredits = double.Parse(txtPreviousPayables.Text);
            //}


            //if (saveEnable == false)
            //{
            //    if (insert_into_db())
            //    {
            //        //Recovery_reports reports = new Recovery_reports();
            //        //reports.ShowDialog();
            //        //refresh();
            //    }
            //}
            //else if (saveEnable == true)
            //{
            //    if (UpdateRecoveryDetailsdb())
            //    {
            //        //Recovery_reports reports = new Recovery_reports();
            //        //reports.ShowDialog();
            //    }
            //}
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void txtStatus_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtStatus, "fillComboBoxTransactionStatus", "status_title");
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
            GetSetData.FillComboBoxUsingProcedures(txtBankName, "fillComboBoxLoanBankTitles", "bank_name");
        }

        private void customer_code_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtBankCode, "fillComboBoxLoanBankTitles", "code");
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeName();
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeCodes();
            }
        }

        private void customer_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxBankCode();
                FillComboBoxBankDetails();
                FillGridViewUsingPagination();
            }
        }

        private void customer_code_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxBankName();
                FillComboBoxBankDetails();
                FillGridViewUsingPagination();
            }
        }
    }
}
