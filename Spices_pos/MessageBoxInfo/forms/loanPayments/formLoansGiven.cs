using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using RefereningMaterial;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.loanPayments
{
    public partial class formLoansGiven : Form
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

               public formLoansGiven()
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
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                //***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnExit.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void fillAddProductsFormTextBoxes()
        {
            txtPaymentDate.Text = TextData.date;
            txtTime.Text = TextData.time;
            txtFatherName.Text = TextData.fatherName;
            txtContactNo.Text = TextData.mobileNo;
            txtAmount.Text = TextData.editAmount;
            txtReference.Text = TextData.references;
            txtRemarks.Text = TextData.remarks;
            txt_status.Text = TextData.status;

            GetSetData.query = @"select IsCustomer from pos_loanDetails where (date = '" + TextData.date + "') and (time = '" + TextData.time + "')";
            TextData.IsCustomer = data.SearchStringValuesFromDb(GetSetData.query);
            isCustomerChecked.Checked = bool.Parse(TextData.IsCustomer);
            txtFullName.Text = TextData.fullName;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "UPDATE LOAN DETAILS";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "ADD NEW LOAN";
            }
        }

        private void formLoansGiven_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void fillVariableValues()
        {
            try
            {
                TextData.fullName = txtFullName.Text;
                TextData.fatherName = txtFatherName.Text;
                TextData.mobileNo = txtContactNo.Text;
                TextData.references = txtReference.Text;
                TextData.remarks = txtRemarks.Text;
                TextData.cashAmount = txtAmount.Text;
                TextData.status = txt_status.Text;

                if (txtFatherName.Text == "")
                {
                    TextData.fatherName = "nill";
                }

                if (txtContactNo.Text == "")
                {
                    TextData.mobileNo = "nill";
                }

                if (txtReference.Text == "")
                {
                    TextData.references = "nill";
                }

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool insert_records()
        {
            try
            {
                fillVariableValues();
                TextData.date = txtPaymentDate.Text;
                TextData.time = txtTime.Text;

                if (txtFullName.Text != "")
                {
                    if (txtAmount.Text != "")
                    {
                        if (txt_status.Text != "" && txt_status.Text != "-- Select --")
                        {
                            string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                            string general_option = data.UserPermissions("useCapital", "pos_general_settings");

                            if ((double.Parse(txtAmount.Text) < double.Parse(capital)) || general_option == "No")
                            {
                                if (isCustomerChecked.Checked == true)
                                {
                                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.fullName + "') and (cus_code = '" + TextData.customerCode + "');";
                                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                    GetSetData.Ids = data.UserPermissionsIds("loanHolder_id", "pos_LoanHolders", "full_name", "others");
                                    int employeeId_db = data.UserPermissionsIds("emp_id", "pos_role", "role_id", role_id.ToString());
                                    double creditLimit_db = data.NumericValues("credit_limit", "pos_customers", "customer_id", GetSetData.fks.ToString());

                                    if (double.Parse(TextData.cashAmount) <= creditLimit_db)
                                    {
                                        GetSetData.query = @"insert into pos_loanDetails values ('" + TextData.date + "', '" + TextData.time + "', '" + TextData.fullName + "', '" + TextData.fatherName + "', '" + TextData.mobileNo + "', '" + TextData.cashAmount + "', '" + TextData.references + "', '" + TextData.remarks + "', '" + TextData.status + "', 'True', '" + GetSetData.Ids.ToString() + "', '" + GetSetData.fks.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                        double lastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", GetSetData.fks.ToString());

                                        if (txt_status.Text == "Payment")
                                        {
                                            lastCredits_db += double.Parse(TextData.cashAmount);

                                            //  Customer Transactions *******************************************************************************
                                            GetSetData.query = @"insert into pos_customer_transactions values ('Nill' , '" + TextData.date + "' , '" + TextData.time + "' , '" + TextData.cashAmount + "' , '0', '0', '" + lastCredits_db.ToString() + "' , 'Loan Paid By Company' , '" + GetSetData.fks.ToString() + "' , '" + employeeId_db.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                        else if (txt_status.Text == "Received")
                                        {
                                            lastCredits_db -= double.Parse(TextData.cashAmount);

                                            //  Customer Transactions *******************************************************************************
                                            GetSetData.query = @"insert into pos_customer_transactions values ('Nill' , '" + TextData.date + "' , '" + TextData.time + "' , '0' , '" + TextData.cashAmount + "' , '0' , '" + lastCredits_db.ToString() + "' , 'Loan Received By Company' , '" + GetSetData.fks.ToString() + "' , '" + employeeId_db.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }

                                        if (lastCredits_db <= 0)
                                        {
                                            lastCredits_db = 0;
                                        }

                                        GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + lastCredits_db.ToString() + "' where (customer_id = '" + GetSetData.fks.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    else
                                    {
                                        error.errorMessage("Amount is exceeded from customer credit limit!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    GetSetData.Ids = data.UserPermissionsIds("loanHolder_id", "pos_LoanHolders", "full_name", TextData.fullName);
                                    GetSetData.fks = data.UserPermissionsIds("customer_id", "pos_customers", "full_name", "nill");

                                    GetSetData.query = @"insert into pos_loanDetails values ('" + TextData.date + "', '" + TextData.time + "', '" + TextData.fullName + "', '" + TextData.fatherName + "', '" + TextData.mobileNo + "', '" + TextData.cashAmount + "', '" + TextData.references + "', '" + TextData.remarks + "', '" + TextData.status + "', 'False', '" + GetSetData.Ids.ToString() + "', '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }


                                //*****************************************************************************************
                                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                TextData.cashAmount = txtAmount.Text;

                                if (GetSetData.Data == "Yes")
                                {
                                    if (capital != "NULL" && capital != "")
                                    {
                                        if (txt_status.Text == "Payment")
                                        {
                                            TextData.cashAmount = (double.Parse(capital) - double.Parse(TextData.cashAmount)).ToString();
                                        }
                                        else if (txt_status.Text == "Received")
                                        {
                                            TextData.cashAmount = (double.Parse(capital) + double.Parse(TextData.cashAmount)).ToString();
                                        }
                                    }

                                    if (double.Parse(capital) <= 0)
                                    {
                                        TextData.cashAmount = "0";
                                    }

                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.cashAmount + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                GetSetData.SaveLogHistoryDetails("Add New Loan Form", "Saving loan [" + TextData.date + "  " + TextData.time + "  " + TextData.status + "] details", role_id);
                                return true;
                            }
                            else
                            {
                                error.errorMessage("Amount is exceeded from company capital!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the status!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter full name!");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (insert_records())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
            }
        }

        private bool Update_records()
        {
            try
            {
                fillVariableValues();

                if (txtFullName.Text != "")
                {
                    if (txtAmount.Text != "")
                    {
                        if (txt_status.Text != "" && txt_status.Text != "-- Select --")
                        {
                            string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                            string general_option = data.UserPermissions("useCapital", "pos_general_settings");

                            if ((double.Parse(txtAmount.Text) < double.Parse(capital)) || general_option == "No")
                            {
                                GetSetData.query = @"select loan_id from pos_loanDetails where (date = '" + TextData.date + "') and (time = '" + TextData.time + "')";
                                int loanID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                //*********************************************************************

                                double amount_db = data.NumericValues("amount", "pos_loanDetails", "loan_id", loanID_db.ToString());
                                //*********************************************************************

                                if (isCustomerChecked.Checked == true)
                                {
                                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.fullName + "') and (cus_code = '" + TextData.customerCode + "');";
                                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                    GetSetData.Ids = data.UserPermissionsIds("loanHolder_id", "pos_LoanHolders", "full_name", "others");
                                    int employeeId_db = data.UserPermissionsIds("emp_id", "pos_role", "role_id", role_id.ToString());
                                    double creditLimit_db = data.NumericValues("credit_limit", "pos_customers", "customer_id", GetSetData.fks.ToString());

                                    if (double.Parse(TextData.cashAmount) <= creditLimit_db)
                                    {
                                        

                                        GetSetData.query = @"Update pos_loanDetails set date = '" + txtPaymentDate.Text + "', time = '" + txtTime.Text + "', fullName = '" + TextData.fullName + "', fatherName = '" + TextData.fatherName + "', contactNo = '" + TextData.mobileNo + "', amount = '" + TextData.cashAmount + "', reference = '" + TextData.references + "', remarks = '" + TextData.remarks + "', status = '" + TextData.status + "', IsCustomer = 'True', loanHolder_id = '" + GetSetData.Ids.ToString() + "', customer_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.date + "') and (time = '" + TextData.time +"');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                        double lastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", GetSetData.fks.ToString());

                                        if (txt_status.Text == "Payment")
                                        {
                                            lastCredits_db = (lastCredits_db + double.Parse(TextData.cashAmount)) - amount_db;

                                            //  Customer Transactions *******************************************************************************
                                            GetSetData.query = @"Update pos_customer_transactions set  date = '" + txtPaymentDate.Text + "' , time = '" + txtTime.Text + "',  net_amount = '" + TextData.cashAmount + "', pCredits = '" + lastCredits_db.ToString() + "', customer_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.date + "') and (time = '" + TextData.time +"');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                        else if (txt_status.Text == "Received")
                                        {
                                            lastCredits_db = (lastCredits_db + amount_db) - double.Parse(TextData.cashAmount);

                                            //  Customer Transactions *******************************************************************************
                                            GetSetData.query = @"Update pos_customer_transactions set date = '" + txtPaymentDate.Text + "' , time = '" + txtTime.Text + "' , debit = '" + TextData.cashAmount + "', pCredits = '" + lastCredits_db.ToString() + "', customer_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.date + "') and (time = '" + TextData.time + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }

                                        if (lastCredits_db <= 0)
                                        {
                                            lastCredits_db = 0;
                                        }

                                        GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + lastCredits_db.ToString() + "' where (customer_id = '" + GetSetData.fks.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    else
                                    {
                                        error.errorMessage("Amount is exceeded from customer credit limit!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    GetSetData.Ids = data.UserPermissionsIds("loanHolder_id", "pos_LoanHolders", "full_name", TextData.fullName);
                                    GetSetData.fks = data.UserPermissionsIds("customer_id", "pos_customers", "full_name", "nill");

                                    GetSetData.query = @"update pos_loanDetails set date = '" + txtPaymentDate.Text + "', time = '" + txtTime.Text + "', fullName = '" + TextData.fullName + "', fatherName = '" + TextData.fatherName + "', contactNo = '" + TextData.mobileNo + "', amount = '" + TextData.cashAmount + "', reference = '" + TextData.references + "', remarks = '" + TextData.remarks + "', status = '" + TextData.status + "', IsCustomer = 'False', loanHolder_id = '" + GetSetData.Ids.ToString() + "', customer_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.date + "') and (time = '" + TextData.time +"');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }


                                //*****************************************************************************************
                                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                TextData.cashAmount = txtAmount.Text;

                                if (GetSetData.Data == "Yes")
                                {
                                    if (capital != "NULL" && capital != "")
                                    {
                                        if (txt_status.Text == "Payment")
                                        {
                                            TextData.cashAmount = ((double.Parse(capital) + amount_db) - double.Parse(TextData.cashAmount)).ToString();
                                        }
                                        else if (txt_status.Text == "Received")
                                        {
                                            TextData.cashAmount = ((double.Parse(capital) + double.Parse(TextData.cashAmount)) - amount_db).ToString();
                                        }
                                    }

                                    if (double.Parse(capital) <= 0)
                                    {
                                        TextData.cashAmount = "0";
                                    }

                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.cashAmount + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                GetSetData.SaveLogHistoryDetails("Add New Loan Form", "Saving loan [" + TextData.date + "  " + TextData.time + "  " + TextData.status + "] details", role_id);
                                return true;
                            }
                            else
                            {
                                error.errorMessage("Amount is exceeded from company capital!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the status!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter full name!");
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
            if (Update_records())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();
            }
        }

        private void formCharityPayment_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void refresh()
        {
            try
            {
                txtPaymentDate.Text = DateTime.Now.ToLongDateString();
                txtTime.Text = DateTime.Now.ToLongTimeString();
                txtFullName.Text = "";
                txtFatherName.Text = "";
                txtContactNo.Text = "";
                txtAmount.Text = "0";
                txtReference.Text = "";
                txtRemarks.Text = "";

                FillComboBoxCustomeDetails();
                system_user_permissions();
                enableSaveButton();
                txtPaymentDate.Focus();
            }
            catch (Exception es)
            {
                 error.errorMessage(es.Message);
                 error.ShowDialog();
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAmount.Text, e);
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (isCustomerChecked.Checked == true)
                {
                    using (add_customer customer = new add_customer())
                    {
                        add_customer.role_id = role_id;
                        customer.ShowDialog();
                    }
                }
                else
                {
                    using (formLoanHolders loan = new formLoanHolders())
                    {
                        loan.ShowDialog();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void isCustomerChecked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isCustomerChecked.Checked == true)
                {
                    GetSetData.FillComboBoxUsingProcedures(txtFullName, "fillComboBoxCustomerNames", "full_name");
                }
                else
                {
                    GetSetData.FillComboBoxUsingProcedures(txtFullName, "fillComboBoxLoanHolders", "full_name");
                }
                txtFullName.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomeDetails()
        {
            try
            {
                Customer_details.selected_customer = txtFullName.Text;

                using (Customer_details customer_detail = new Customer_details())
                {
                    Customer_details.role_id = role_id;
                    customer_detail.ShowDialog();
                    TextData.fullName = Customer_details.selected_customer;
                    TextData.customerCode = Customer_details.selected_customerCode;

                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.fullName + "') and (cus_code = '" + TextData.customerCode + "');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    txtFatherName.Text = data.UserPermissions("fatherName", "pos_customers", "customer_id", GetSetData.fks.ToString());
                    txtContactNo.Text = data.UserPermissions("mobile_no", "pos_customers", "customer_id", GetSetData.fks.ToString());   
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void FillComboBoxLoanHolderDetails()
        {
            try
            {
                GetSetData.fks = data.UserPermissionsIds("loanHolder_id", "pos_LoanHolders", "full_name", txtFullName.Text);
                txtFatherName.Text = data.UserPermissions("father_name", "pos_LoanHolders", "loanHolder_id", GetSetData.fks.ToString());
                txtContactNo.Text = data.UserPermissions("contact_no", "pos_LoanHolders", "loanHolder_id", GetSetData.fks.ToString());
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (isCustomerChecked.Checked == true)
                    {
                        FillComboBoxCustomeDetails();
                    }
                    else
                    {
                        FillComboBoxLoanHolderDetails();
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
