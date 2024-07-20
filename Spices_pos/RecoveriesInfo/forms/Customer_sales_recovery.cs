using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using Recoverier_info.recovery_report;
using RefereningMaterial;
using Customers_info.forms;
using Supplier_Chain_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Settings_info.controllers;

namespace Recoverier_info.forms
{
    public partial class Customer_sales_recovery : Form
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

        public Customer_sales_recovery()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int user_id = 0;
        public static int role_id = 0;
        public static bool message_choose = false;
        public static string get_customer = "";
        public static string get_customerCode = "";
        //public static string get_employee = "";
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnGenerateInvoices);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnCustomerInvoices);
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
                GetSetData.Data = data.UserPermissions("recoveries_save", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_save.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recoveries_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_save_and_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recoveries_exit", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_exit.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("recovery_details_Invoices", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //btnCustomerInvoices.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("generateInvoices", "pos_tbl_authorities_reports", "role_id", role_id.ToString());
                //btnGenerateInvoices.Visible = bool.Parse(GetSetData.Data);
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
                FormNamelabel.Text = "Create New Recovery";
                LoginEmployee();
                txtInvoiceNo.Focus();
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                //salesAndReturnsGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Recovery";
                fillAddProductsFormTextBoxes();
                FormNamelabel.Select();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtDate.Text = TextData.dates;
                time_text.Text = TextData.times;
                txtInstallmentDate.Text = TextData.installmentDate;
                customer_text.Text = TextData.cus_name;
                customer_code_text.Text = TextData.cus_code;
                txtEmployeeName.Text = TextData.employee;
                cash_text.Text = TextData.cash.ToString();
                credit_text.Text = TextData.credits.ToString();
                references_text.Text = TextData.reference;
                comments_text.Text = TextData.description;

                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();

                GetSetData.query = "select recovery_id from pos_recovery_details where (date = '" + TextData.installmentDate.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                // *********************************************************************
                txtInvoiceNo.Text = data.UserPermissions("invoiceNo", "pos_recovery_details", "recovery_id", GetSetData.Ids.ToString());

                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_recovery_details", "recovery_id", GetSetData.Ids.ToString());
                txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "employee_id", GetSetData.fks.ToString());

                GetSetData.query = @"select installmentNo from pos_recoveries where (recovery_id = '" + GetSetData.Ids.ToString() + "') and (installmentDate = '" + TextData.installmentDate + "');";
                txtInstallmentNo.Text = data.SearchStringValuesFromDb(GetSetData.query);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomeDetails()
        {
            GetSetData.query = @"select mobile_no from pos_customers where full_name = '" + customer_text.Text + "' and cus_code = '" + customer_code_text.Text + "';";
            cellno_text.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select cnic from pos_customers where full_name = '" + customer_text.Text + "' and cus_code = '" + customer_code_text.Text + "';";
            txt_cnic.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select address1 from pos_customers where full_name = '" + customer_text.Text + "' and cus_code = '" + customer_code_text.Text + "';";
            txt_address.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select picture_path from pos_general_settings;";
            TextData.image_path_db = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select image_path from pos_customers where full_name = '" + customer_text.Text + "' and cus_code = '" + customer_code_text.Text + "';";
            TextData.image = data.SearchStringValuesFromDb(GetSetData.query);

            if (TextData.image != "nill" && TextData.image != "")
            {
                img_pic_box.Image = Image.FromFile(TextData.image_path_db + TextData.image);
            }
        }

        private void FillComboBoxCusLastCredits()
        {
            try
            {
                if (customer_text.Text != "")
                {
                    if (txtInvoiceNo.Text == "")
                    {
                        // Getting the Customer last credits from the database to add the crzakkiedit amount with it
                        GetSetData.query = @"select customer_id from pos_customers where (cus_code = '" + customer_code_text.Text + "') and (full_name = '" + customer_text.Text + "');";
                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = @"select lastCredits from pos_customer_lastCredits where customer_id = '" + GetSetData.Ids.ToString() + "';";
                        GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                        last_credit_text.Text = GetSetData.Data;
                    }
                    else
                    {
                        getInvoiceLastCredits();
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void getInvoiceLastCredits()
        {
            try
            {
                GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                TextData.lastCredits = 0;
                TextData.cash = 0;
                TextData.credits = 0;

                GetSetData.query = @"select sub_total from pos_sales_accounts where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "" && GetSetData.Data != "NULL")
                {
                    TextData.lastCredits = double.Parse(GetSetData.Data);
                }
                //*********************************************************************************

                GetSetData.query = @"select (paid + discount) from pos_sales_accounts where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "" && GetSetData.Data != "NULL")
                {
                    TextData.cash = double.Parse(GetSetData.Data);
                }
                //*********************************************************************************

                GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                        where invoiceNo = '" + txtInvoiceNo.Text + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "" && GetSetData.Data != "NULL")
                {
                    TextData.credits = double.Parse(GetSetData.Data);
                }
                
                // Before Changing.......
                //TextData.lastCredits = TextData.lastCredits - (TextData.cash + TextData.credits);

                //if (TextData.lastCredits >= 0)
                //{
                //    last_credit_text.Text = TextData.lastCredits.ToString();
                //}
                //else
                //{
                //    last_credit_text.Text = "0.00";
                //}

                // Getting the Customer last credits from the database to add the credit amount with it
                GetSetData.query = @"select customer_id from pos_customers where cus_code = '" + customer_code_text.Text + "' and full_name = '" + customer_text.Text + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select lastCredits from pos_customer_lastCredits where customer_id = '" + GetSetData.Ids.ToString() + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                last_credit_text.Text = GetSetData.Data;

                GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                int sales_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + sales_acc_id_db.ToString() + "';";
                int installmentAccID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //**********************************************
                double nowLastCredits = 0;

                GetSetData.query = @"select sum(dues) from  pos_installment_plan inner join pos_installment_accounts on 
                                    pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                    pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                    where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";
                double total_dues_db = data.SearchNumericValuesDb(GetSetData.query);
                nowLastCredits = (double.Parse(GetSetData.Data) - total_dues_db);

                if (nowLastCredits >= 0)
                {
                    txtTotalLastCredits.Text = nowLastCredits.ToString();
                }
                else
                {
                    txtTotalLastCredits.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void RefreshInsatllmentNo()
        {
            try
            {
                txtInstallmentNo.Text = null;
                txtInstallmentNo.Items.Clear();

                GetSetData.query = @"select installmentNo from  pos_installment_plan inner join pos_installment_accounts on 
                                    pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                    pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                    where (pos_sales_accounts.billNo = '" + txtInvoiceNo.Text + "') and (pos_installment_plan.status = 'Incomplete');";
                
                GetSetData.FillComboBoxWithValues(GetSetData.query, "installmentNo", txtInstallmentNo);
                txtInstallmentNo.Text = null;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void LoadIntallmentDetails()
        {
            try
            {
                RefreshInsatllmentNo();

                GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                int installmentAccID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select customer_id from pos_sales_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                // **************************************************************

                customer_text.Text = data.UserPermissions("full_name", "pos_customers", "customer_id", GetSetData.fks.ToString());
                customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "customer_id", GetSetData.fks.ToString());

                GetSetData.query = @"select amount from pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + txtInstallmentNo.Text +"');";
                txtInstallment.Text = data.SearchStringValuesFromDb (GetSetData.query);

                GetSetData.query = @"select interest from pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + txtInstallmentNo.Text + "');";
                txtInterest.Text = data.SearchStringValuesFromDb(GetSetData.query);

                if ((txtInstallment.Text != "" && txtInstallment.Text != "NULL")  && (txtInterest.Text != "" && txtInterest.Text != "NULL"))
                {
                    TextData.cash = double.Parse(txtInstallment.Text) + double.Parse(txtInterest.Text);
                    txtTotalAmount.Text = TextData.cash.ToString();
                }
                else
                {
                    txtTotalAmount.Text = "0";
                }
                
                cash_text.Text = txtTotalAmount.Text;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomerName()
        {
            customer_text.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_text.Text);
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
                GetSetData.query = "select * from ViewCustomerWiseRecovery where ([Customer] = '" + customer_text.Text + "') and ([Code] = '" + customer_code_text.Text + "');";
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
            txtInstallmentDate.Text = DateTime.Now.ToLongDateString();
            txtDate.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToLongTimeString();
            txtInstallmentNo.Text = "";
            txtInstallmentNo.Items.Clear();
            txtInvoiceNo.Text = "";
            customer_text.Text = "";
            customer_code_text.Text = "";
            txtInstallment.Text = "0";
            txtInterest.Text = "0";
            txtDues.Text = "0";
            txtTotalAmount.Text = "0.00";
            cellno_text.Text = "";
            references_text.Text = "";
            comments_text.Text = "";
            cash_text.Text = "";
            credit_text.Text = "0.00";
            last_credit_text.Text = "0.00";
            txt_address.Text = "";
            txt_cnic.Text = "";
            img_pic_box.Image = null;

            customer_text.Select();
        }

        private void funFillCusEmpNames()
        {
            try
            {
                //txtEmployeeName.Text = get_employee;
                customer_text.Text = get_customer;
                customer_code_text.Text = get_customerCode;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Customer_sales_recovery_Load(object sender, EventArgs e)
        {
            try
            {
                txtInstallmentDate.Text = DateTime.Now.ToLongDateString();
                txtDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToLongTimeString();

                customer_text.Select();

                system_user_permissions();
                funFillCusEmpNames();
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
            GetSetData.SaveLogHistoryDetails("Add New Customer Recovery Form", "Exit...", role_id);
            this.Close();
        }

        private void add_sub_category_Click(object sender, EventArgs e)
        {
            using (add_customer main = new add_customer())
            {
                add_customer.role_id = role_id;
                add_customer.saveEnable = false;
                main.ShowDialog();
            }
        }

        private void add_spplier_Click(object sender, EventArgs e)
        {
            using (Add_supplier main = new Add_supplier())
            {
                Add_supplier.role_id = role_id;
                Add_supplier.saveEnable = false;
                main.ShowDialog();
            }
        }

        private void cash_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(cash_text.Text, e);
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
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
                txtDate.Text = DateTime.Now.ToLongDateString();
                TextData.dates = txtDate.Text;
                TextData.times = time_text.Text;
                TextData.installmentDate = txtInstallmentDate.Text;
                TextData.invoiceNo = txtInvoiceNo.Text;
                TextData.cus_name = customer_text.Text;
                TextData.cus_code = customer_code_text.Text;
                TextData.employee = txtEmployeeName.Text;
                TextData.emp_code = txtEmployeeCode.Text;
                TextData.cell_no = cellno_text.Text;
                TextData.reference = references_text.Text;
                TextData.description = comments_text.Text;
                TextData.credits = 0;
                TextData.lastCredits = 0;
                TextData.installmentNo = 0;
                TextData.cash = 0;
                TextData.monthlyInstallment = txtInstallment.Text;
                TextData.monthlyInterest = txtInterest.Text;
                TextData.monthlyDues = txtDues.Text;
                TextData.monthlyTotal = txtTotalAmount.Text;

                if (credit_text.Text != "")
                {
                    TextData.credits = double.Parse(credit_text.Text);
                }

                if (last_credit_text.Text != "")
                {
                    TextData.lastCredits = double.Parse(last_credit_text.Text);
                }

                if (txtInstallmentNo.Text != "")
                {
                    TextData.installmentNo = Convert.ToInt32(txtInstallmentNo.Text);
                }

                if (cash_text.Text != "")
                {
                    TextData.cash = double.Parse(cash_text.Text);
                }

                if (TextData.description == "")
                {
                    TextData.description = "nill";
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

                if ((txtInvoiceNo.Text != "" && txtInstallmentNo.Text != "") || (txtInvoiceNo.Text == "" && txtInstallmentNo.Text == ""))
                {
                    if (txtEmployeeName.Text != "")
                    {
                        if (customer_text.Text != "")
                        {
                            if (cash_text.Text != "")
                            {
                                if (double.Parse(cash_text.Text) > 0)
                                {
                                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.cus_name.ToString() + "') and (cus_code = '" + TextData.cus_code.ToString() + "');";
                                    int customerId_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    // *******************************************************************************
                                    GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                    // *******************************************************************************
                                    GetSetData.query = @"insert into pos_recovery_details values ('" + TextData.installmentDate.ToString() + "' , '" + TextData.times.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.reference.ToString() + "' , '" + TextData.description.ToString() + "' , '" + customerId_db.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *******************************************************************************
                                    GetSetData.query = @"select recovery_id from pos_recovery_details where (date = '" + TextData.installmentDate.ToString() + "') and (time = '" + TextData.times.ToString() + "') and (customer_id = '" + customerId_db.ToString() + "');";
                                    int recovery_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    // *******************************************************************************
                                    GetSetData.query = @"insert into pos_recoveries values ('" + TextData.installmentDate.ToString() + "' , '" + TextData.installmentNo.ToString() + "' , '" + TextData.monthlyInstallment.ToString() + "' , '" + TextData.monthlyInterest.ToString() + "' , '" + TextData.monthlyDues.ToString() + "' ,  '" + TextData.monthlyTotal.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + recovery_id_db.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    // *******************************************************************************
                                    TextData.lastCredits = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customerId_db.ToString());

                                    if (TextData.lastCredits == -1)
                                    {
                                        GetSetData.query = @"insert into pos_customer_lastCredits values ('" + TextData.credits.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + customerId_db.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    else if (TextData.lastCredits != -1 && TextData.lastCredits >= 0)
                                    {
                                        GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.credits.ToString() + "' , due_days = '" + TextData.dates.ToString() + "' where (customer_id = '" + customerId_db.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }

                                    //  Customer Transactions *******************************************************************************
                                    GetSetData.query = @"insert into pos_customer_transactions values ('Nill' , '" + TextData.installmentDate.ToString() + "' , '" + TextData.times.ToString() + "' , '0' , '" + TextData.cash.ToString() + "' , '0' , '" + TextData.lastCredits.ToString() + "' , 'Customer Recovery' , '" + customerId_db.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    if (TextData.invoiceNo != "" && TextData.invoiceNo != "nill" && TextData.installmentNo.ToString() != "" && TextData.installmentNo != 0)
                                    {
                                        GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                        GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                                        int installmentAccID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                        // *****************************************************************************************

                                        GetSetData.query = @"select date from pos_customerChequeDetails where (billNo = '" + TextData.invoiceNo + "') and (status = 'Incomplete') and (date = '" + txtInstallmentDate.Text + "');";
                                        string installmentAdvanceChequeDate_db = data.SearchStringValuesFromDb(GetSetData.query);

                                        if (installmentAdvanceChequeDate_db != "" && installmentAdvanceChequeDate_db != "NULL")
                                        {
                                            txtDate.Text = installmentAdvanceChequeDate_db;
                                        }

                                        if (txtInstallmentDate.Text == txtDate.Text)
                                        {
                                            GetSetData.query = @"update pos_customerChequeDetails set status = 'Completed' where (billNo = '" + TextData.invoiceNo + "') and (status = 'Incomplete') and (date = '" + txtInstallmentDate.Text + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                        // *****************************************************************************************

                                        GetSetData.query = @"select max(installmentNo) from  pos_installment_plan inner join pos_installment_accounts on 
                                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                        where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                        TextData.monthlyInstallment = data.SearchStringValuesFromDb(GetSetData.query);
                                        //Working******************************************************************************************

                                        GetSetData.query = @"select amount from  pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "');";
                                        double installment_db = data.SearchNumericValuesDb(GetSetData.query);
                                        //********************************************************************************

                                        GetSetData.query = @"select interest from  pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "');";
                                        double interest_db = data.SearchNumericValuesDb(GetSetData.query);
                                        //********************************************************************************

                                        double totalInstallmentAmount_db = double.Parse(txtInstallment.Text) + double.Parse(txtInterest.Text) + double.Parse(txtDues.Text);
                                        double PerInstallmentAmount_db = installment_db + interest_db;


                                        if (txtInstallmentNo.Text == TextData.monthlyInstallment)
                                        {
                                            GetSetData.query = @"update pos_installment_plan set dues = '0', total_amount = '" + PerInstallmentAmount_db.ToString() + "',  status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (pos_installment_plan.status = 'Incomplete');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);


                                            GetSetData.query = @"update pos_installment_plan set dues = '" + txtDues.Text + "', total_amount = '" + totalInstallmentAmount_db.ToString() + "',  status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + TextData.monthlyInstallment + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                            //******************************************************************************************


                                            GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                                             where invoiceNo ='" + TextData.invoiceNo + "';";
                                            TextData.TotalRecovery = data.SearchNumericValuesDb(GetSetData.query);
                                            //******************************************************************************************


                                            GetSetData.query = @"select sum(total_amount) from  pos_installment_plan inner join pos_installment_accounts on 
                                                            pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                            pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                            where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                            TextData.TotalInstallmentAmount = data.SearchNumericValuesDb(GetSetData.query);
                                            //******************************************************************************************


                                            if (TextData.TotalRecovery < TextData.TotalInstallmentAmount)
                                            {
                                                GetSetData.query = @"update pos_installment_plan set status = 'Incomplete' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + TextData.monthlyInstallment + "');";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                            }

                                            GetSetData.query = @"update pos_customerChequeDetails set status = 'Completed' where (billNo = '" + TextData.invoiceNo + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                        else
                                        {
                                            //********************************************************************************
                                            TextData.cash = 0;
                                            TextData.credits = 0;

                                            if (txtInstallmentNo.Text != "")
                                            {
                                                for (int i = 1; i <= Convert.ToInt32(txtInstallmentNo.Text); i++)
                                                {
                                                    if (i == Convert.ToInt32(TextData.monthlyInstallment))
                                                    {

                                                        GetSetData.query = @"update pos_installment_plan set dues = '" + txtDues.Text + "', total_amount = '" + totalInstallmentAmount_db.ToString() + "', status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);


                                                        //******************************************************************************************
                                                        GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                                                         where invoiceNo ='" + TextData.invoiceNo + "';";
                                                        TextData.TotalRecovery = data.SearchNumericValuesDb(GetSetData.query);
                                                        //******************************************************************************************


                                                        GetSetData.query = @"select sum(total_amount) from  pos_installment_plan inner join pos_installment_accounts on 
                                                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                                        where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                                        TextData.TotalInstallmentAmount = data.SearchNumericValuesDb(GetSetData.query);
                                                        //******************************************************************************************

                                                        if (TextData.TotalRecovery < TextData.TotalInstallmentAmount)
                                                        {
                                                            GetSetData.query = @"update pos_installment_plan set status = 'Incomplete' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        GetSetData.query = @"update pos_installment_plan set dues = '0', total_amount = '" + PerInstallmentAmount_db.ToString() + "', status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "') and (status = 'Incomplete');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // *****************************************************************************************
                                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                    TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                                    if (GetSetData.Data == "Yes")
                                    {
                                        if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                                        {
                                            TextData.cash += double.Parse(TextData.totalCapital);
                                        }

                                        GetSetData.query = @"update pos_capital set total_capital = '" + TextData.cash.ToString() + "';";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                        // *****************************************************************************************
                                    }

                                    //GetSetData.SaveLogHistoryDetails("Add New Customer Recovery Form", "Saving customer recovery [" + TextData.dates + "  " + TextData.times + "] details", role_id);
                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the receiving amount!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the receiving amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the customer!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the Invoice No and Installment No!");
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
            //form_sure_message choose = new form_sure_message();

            //choose.Message_choose("Please confirm all the records before saving!");
            //choose.ShowDialog();

            //if(message_choose == true)
            //{
            if (insert_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
            }
            //}
        }

        private bool UpdateRecoveryDetailsdb()
        {
            try
            {
                FillVariablesWithValues();

                if ((txtInvoiceNo.Text == "" && txtInstallmentNo.Text == "") || (txtInvoiceNo.Text != "" && txtInstallmentNo.Text != ""))
                {
                    if (txtEmployeeName.Text != "")
                    {
                        if (customer_text.Text != "")
                        {
                            if (cash_text.Text != "")
                            {
                                GetSetData.query = @"select customer_id from pos_customers where full_name = '" + TextData.cus_name.ToString() + "' and cus_code = '" + TextData.cus_code.ToString() + "';";
                                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                // *******************************************************************************
                                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                if (TextData.invoiceNo != "" && TextData.invoiceNo != "nill" && TextData.installmentNo.ToString() != "" && TextData.installmentNo != 0)
                                {
                                    GetSetData.query = @"update pos_recovery_details set date = '" + TextData.installmentDate.ToString() + "', reference = '" + TextData.reference.ToString() + "' , remarks = '" + TextData.description.ToString() + "' where (date = '" + TextData.installmentDate.ToString() + "') and (time = '" + TextData.times.ToString() + "'));";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                else
                                {
                                    // *******************************************************************************
                                    GetSetData.query = @"update pos_recovery_details set date = '" + TextData.installmentDate.ToString() + "', reference = '" + TextData.reference.ToString() + "' , remarks = '" + TextData.description.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.installmentDate.ToString() + "') and (time = '" + TextData.times.ToString() + "'));";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                // *******************************************************************************
                                GetSetData.query = @"select recovery_id from pos_recovery_details where date = '" + TextData.installmentDate.ToString() + "' and time = '" + TextData.times.ToString() + "' and customer_id = '" + GetSetData.Ids.ToString() + "';";
                                int recovery_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                // *******************************************************************************
                                TextData.lastCredits = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", GetSetData.Ids.ToString());
                                double previousPaid = data.NumericValues("amount", "pos_recoveries", "recovery_id", recovery_id_db.ToString());

                                TextData.credits = (TextData.lastCredits + previousPaid) - TextData.cash;

                                if (TextData.credits < 0)
                                {
                                    TextData.credits = 0;
                                }

                                if (TextData.lastCredits != -1 && TextData.credits >= 0)
                                {
                                    GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.credits.ToString() + "' , due_days = '" + TextData.installmentDate.ToString() + "' where customer_id = '" + GetSetData.Ids.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                
                                // *******************************************************************************
                                GetSetData.query = @"update pos_recoveries set amount = '" + TextData.cash.ToString() + "' , credits = '" + TextData.credits.ToString() + "' where recovery_id = '" + recovery_id_db.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                //  Customer Transactions *******************************************************************************
                                GetSetData.query = @"update pos_customer_transactions set debit = '" + TextData.cash.ToString() + "' ,  pCredits = '" + TextData.credits.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.installmentDate + "') and (time = '" + TextData.times + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                // *****************************************************************************************
                                TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");
                                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                if (GetSetData.Data == "Yes")
                                {
                                    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                                    {
                                        TextData.cash = (double.Parse(TextData.totalCapital) + TextData.cash) - previousPaid;
                                    }

                                    if (TextData.cash < 0)
                                    {
                                        TextData.cash = 0;
                                    }

                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.cash.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                if (TextData.invoiceNo != "" && TextData.invoiceNo != "nill" && TextData.installmentNo.ToString() != "" && TextData.installmentNo != 0)
                                {
                                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                                    int installmentAccID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                   

                                    //Working*****************************************************************************************
                                    GetSetData.query = @"select max(installmentNo) from  pos_installment_plan inner join pos_installment_accounts on 
                                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                        where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                    TextData.monthlyInstallment = data.SearchStringValuesFromDb(GetSetData.query);
                                    //Working******************************************************************************************

                                    GetSetData.query = @"select amount from  pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "');";
                                    double installment_db = data.SearchNumericValuesDb(GetSetData.query);
                                    //********************************************************************************

                                    GetSetData.query = @"select interest from  pos_installment_plan where (installment_acc_id = '" + installmentAccID_db.ToString() + "');";
                                    double interest_db = data.SearchNumericValuesDb(GetSetData.query);
                                    //********************************************************************************

                                    double totalInstallmentAmount_db = double.Parse(txtInstallment.Text) + double.Parse(txtInterest.Text) + double.Parse(txtDues.Text);
                                    double PerInstallmentAmount_db = installment_db + interest_db;


                                    if (txtInstallmentNo.Text == TextData.monthlyInstallment)
                                    {
                                        GetSetData.query = @"update pos_installment_plan set dues = '0', total_amount = '" + PerInstallmentAmount_db.ToString() + "',  status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (pos_installment_plan.status = 'Incomplete');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);


                                        GetSetData.query = @"update pos_installment_plan set dues = '" + txtDues.Text + "', total_amount = '" + totalInstallmentAmount_db.ToString() + "',  status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + TextData.monthlyInstallment + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                        //******************************************************************************************


                                        GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                                             where invoiceNo ='" + TextData.invoiceNo + "';";
                                        TextData.TotalRecovery = data.SearchNumericValuesDb(GetSetData.query);
                                        //******************************************************************************************


                                        GetSetData.query = @"select sum(total_amount) from  pos_installment_plan inner join pos_installment_accounts on 
                                                            pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                            pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                            where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                        TextData.TotalInstallmentAmount = data.SearchNumericValuesDb(GetSetData.query);
                                        //******************************************************************************************


                                        if (TextData.TotalRecovery < TextData.TotalInstallmentAmount)
                                        {
                                            GetSetData.query = @"update pos_installment_plan set status = 'Incomplete' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + TextData.monthlyInstallment + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }

                                        GetSetData.query = @"update pos_customerChequeDetails set status = 'Completed' where (billNo = '" + TextData.invoiceNo + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    else
                                    {
                                        //********************************************************************************
                                        TextData.cash = 0;
                                        TextData.credits = 0;

                                        if (txtInstallmentNo.Text != "")
                                        {
                                            for (int i = 1; i <= Convert.ToInt32(txtInstallmentNo.Text); i++)
                                            {
                                                if (i == Convert.ToInt32(TextData.monthlyInstallment))
                                                {

                                                    GetSetData.query = @"update pos_installment_plan set dues = '" + txtDues.Text + "', total_amount = '" + totalInstallmentAmount_db.ToString() + "', status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);


                                                    //******************************************************************************************
                                                    GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                                                         where invoiceNo ='" + TextData.invoiceNo + "';";
                                                    TextData.TotalRecovery = data.SearchNumericValuesDb(GetSetData.query);
                                                    //******************************************************************************************


                                                    GetSetData.query = @"select sum(total_amount) from  pos_installment_plan inner join pos_installment_accounts on 
                                                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                                                        where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";

                                                    TextData.TotalInstallmentAmount = data.SearchNumericValuesDb(GetSetData.query);
                                                    //******************************************************************************************

                                                    if (TextData.TotalRecovery < TextData.TotalInstallmentAmount)
                                                    {
                                                        GetSetData.query = @"update pos_installment_plan set status = 'Incomplete' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    }
                                                }
                                                else
                                                {
                                                    GetSetData.query = @"update pos_installment_plan set dues = '0', total_amount = '" + PerInstallmentAmount_db.ToString() + "', status = 'Completed' where (installment_acc_id = '" + installmentAccID_db.ToString() + "') and (installmentNo = '" + i.ToString() + "') and (status = 'Incomplete');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                }
                                            }
                                        }
                                    }
                                }

                                GetSetData.SaveLogHistoryDetails("Add New Customer Recovery Form", "Updating customer recovery [" + TextData.dates + "  " + TextData.times + "] details", role_id);
                                return true;
                            }
                            else
                            {
                                error.errorMessage("Please enter the recover amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the customer!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the Invoice No and Installment No!");
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
            //form_sure_message choose = new form_sure_message();

            //choose.Message_choose("Please confirm all the records before modifing!");
            //choose.ShowDialog();

            //if (message_choose == true)
            //{
            if (UpdateRecoveryDetailsdb())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();
            }
            //}
        }

        private void cash_text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.cash = 0;
                TextData.lastCredits = double.Parse(last_credit_text.Text);

                if (cash_text.Text != "")
                {
                    TextData.cash = double.Parse(cash_text.Text);
                }

                if (TextData.cash <= TextData.lastCredits)
                {
                    TextData.credits = TextData.lastCredits - TextData.cash;
                    credit_text.Text = TextData.credits.ToString();
                }
                else
                {
                    //cash_text.Text = "";
                    credit_text.Text = "0.00";
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
            TextData.dates = txtInstallmentDate.Text;
            TextData.times = time_text.Text;
            TextData.cus_name = customer_text.Text;
            TextData.cus_code = customer_code_text.Text;
            TextData.lastCredits = 0;

            if (last_credit_text.Text != "")
            {
                TextData.lastCredits = double.Parse(last_credit_text.Text);
            }
            

            if (saveEnable == false)
            {
                if (insert_into_db())
                {
                    Recovery_reports reports = new Recovery_reports();
                    reports.ShowDialog();
                    refresh();
                }
            }
            else if (saveEnable == true)
            {
                if (UpdateRecoveryDetailsdb())
                {
                    Recovery_reports reports = new Recovery_reports();
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
            //GetSetData.FillComboBoxUsingProcedures(customer_text, "fillComboBoxCustomerNames", "full_name");  
        }

        private void customer_code_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(customer_code_text, "fillComboBoxCustomerNames", "cus_code");  
        }

        private void txtInvoiceNo_Enter(object sender, EventArgs e)
        {
            try
            {
                GetSetData.FillComboBoxUsingProcedures(txtInvoiceNo, "fillComboBoxInvoicesNumbers", "billNo");
                txtInvoiceNo.Text = null;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
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
                //int counter = 0;

                //GetSetData.query = @"select count(full_name) from  pos_customers where (full_name = '" + customer_text.Text + "');";
                //counter = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //if (counter > 1)
                //{
                    if (customer_text.Text.Length > 2)
                    {
                        Customer_details.isDropDown = true;
                        Customer_details.role_id = role_id;
                        Customer_details.selected_customer = customer_text.Text;
                        buttonControls.CustomerDetailsbuttons();
                        customer_text.Text = Customer_details.selected_customer;
                        customer_code_text.Text = Customer_details.selected_customerCode;
                    }
                    else
                    {
                        error.errorMessage("Please enter minimum 3 characters.");
                        error.ShowDialog();
                    }

                    Customer_details.selected_customer = customer_text.Text;
                    Customer_details.role_id = role_id;
                    buttonControls.CustomerDetailsbuttons();
                    customer_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                //}
                //else
                //{
                //    customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_text.Text);
                //}

                //FillComboBoxCustomeCodes();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void customer_code_text_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FillComboBoxCustomerName();
                    FillComboBoxCustomeDetails();
                    FillComboBoxCusLastCredits();
                    FillGridViewUsingPagination();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtDues_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDues.Text, e);
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadIntallmentDetails();
                    FillComboBoxCustomeDetails();
                    FillComboBoxCusLastCredits();
                    FillGridViewUsingPagination();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtInstallmentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                // **************************************************************

                GetSetData.query = @"select installmentDate from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + txtInstallmentNo.Text + "');";
                txtInstallmentDate.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select max(installmentNo) from  pos_installment_plan inner join pos_installment_accounts on 
                                    pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                    pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                    where pos_installment_plan.installment_acc_id = '" + GetSetData.fks.ToString() + "';";

                TextData.monthlyInstallment = data.SearchStringValuesFromDb(GetSetData.query);

                if (txtInstallmentNo.Text == TextData.monthlyInstallment)
                {
                    GetSetData.query = @"select sum(amount) from  pos_installment_plan inner join pos_installment_accounts on 
                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                        where (pos_installment_plan.installment_acc_id = '" + GetSetData.fks.ToString() + "') and (pos_installment_plan.status = 'Incomplete');";
                    txtInstallment.Text = data.SearchStringValuesFromDb(GetSetData.query);
                    // *************************************************************************************

                    GetSetData.query = @"select sum(interest) from  pos_installment_plan inner join pos_installment_accounts on 
                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                        where (pos_installment_plan.installment_acc_id = '" + GetSetData.fks.ToString() + "') and (pos_installment_plan.status = 'Incomplete');";
                    txtInterest.Text = data.SearchStringValuesFromDb(GetSetData.query);
                    // *************************************************************************************

                    GetSetData.query = @"select sum(dues) from  pos_installment_plan inner join pos_installment_accounts on 
                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                        where (pos_installment_plan.installment_acc_id = '" + GetSetData.fks.ToString() + "') and (pos_installment_plan.status = 'Incomplete');";
                    txtDues.Text = data.SearchStringValuesFromDb(GetSetData.query);
                    // ************************************************************************************* 
                }
                else
                {
                    TextData.cash = 0;
                    TextData.lastCredits = 0;
                    TextData.credits = 0;

                    if (txtInstallmentNo.Text != "")
                    {
                        for (int i = 1; i <= Convert.ToInt32(txtInstallmentNo.Text); i++)
                        {
                            GetSetData.query = @"select amount from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "') and (status = 'Incomplete');";
                            string amount_db = data.SearchStringValuesFromDb(GetSetData.query);
                            
                            if (amount_db != "" && amount_db != "NULL")
                            {
                                TextData.cash += double.Parse(amount_db);   
                            }

                            //***************************************************************
                            GetSetData.query = @"select interest from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "') and (status = 'Incomplete');";
                            string interest_db = data.SearchStringValuesFromDb(GetSetData.query);
                            
                            if (interest_db != "" && interest_db != "NULL")
                            {
                                TextData.lastCredits += double.Parse(interest_db);
                            }

                            //***************************************************************
                            GetSetData.query = @"select dues from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "')  and (status = 'Incomplete');";
                            string dues_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (dues_db != "" && dues_db != "NULL")
                            {
                                TextData.credits += double.Parse(dues_db);
                            }
                        }

                        txtInstallment.Text = TextData.cash.ToString();
                        txtInterest.Text = TextData.lastCredits.ToString();
                        txtDues.Text = TextData.credits.ToString();

                        FillComboBoxCusLastCredits();
                    }
                }

                if ((txtInstallment.Text != "" && txtInstallment.Text != "NULL") && (txtInterest.Text != "" && txtInterest.Text != "NULL"))
                {
                    TextData.cash = double.Parse(txtInstallment.Text) + double.Parse(txtInterest.Text) + double.Parse(txtDues.Text);
                    txtTotalAmount.Text = TextData.cash.ToString();
                }
                else
                {
                    txtTotalAmount.Text = "0";
                }
                cash_text.Text = txtTotalAmount.Text;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void dueAmountTxtChanged()
        {
            try
            {
                double DueAmount = 0;
                double dueAmount_db = 0;
                double lastBalance = 0;

                if (txtInterest.Text != "" && txtInstallment.Text != "")
                {
                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    int installmentAccID_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + customer_text.Text + "') and (cus_code = '" + customer_code_text.Text + "');";
                    int customerId_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //************************************************************************************

                    GetSetData.query = @"select dues from  pos_installment_plan inner join pos_installment_accounts on 
                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                        where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";
                    TextData.dues_calculate = data.SearchNumericValuesDb(GetSetData.query);
                    //************************************************************************************

                    double nowLastCredits = 0;
                    TextData.lastCredits = 0;
                    TextData.lastCredits = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customerId_db.ToString());
                    lastBalance = TextData.lastCredits;

                    //Calculating Dues Amount According to the installment number**************************************************
                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where billNo = '" + txtInvoiceNo.Text + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select max(installmentNo) from  pos_installment_plan inner join pos_installment_accounts on 
                                        pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                        pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                        where pos_installment_plan.installment_acc_id = '" + GetSetData.fks.ToString() + "';";

                    TextData.monthlyInstallment = data.SearchStringValuesFromDb(GetSetData.query);
                    // *************************************************************************************

                    dueAmount_db = 0;

                    for (int i = 1; i <= Convert.ToInt32(txtInstallmentNo.Text); i++)
                    {
                        GetSetData.query = @"select dues from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "')  and (status = 'Incomplete');";
                        string dues_db = data.SearchStringValuesFromDb(GetSetData.query);

                        if (dues_db != "" && dues_db != "NULL")
                        {
                            dueAmount_db += double.Parse(dues_db);
                        }
                    }

                    DueAmount = dueAmount_db;

                    if (txtUpdatedDueAmount.Text != "")
                    {
                        TextData.credits = TextData.dues_calculate - double.Parse(txtUpdatedDueAmount.Text);
                        nowLastCredits = TextData.lastCredits - TextData.credits;
                        nowLastCredits = 0;

                        //************************************************************************************
                        GetSetData.query = @"select sum(dues) from  pos_installment_plan inner join pos_installment_accounts on 
                                            pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
                                            pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
                                            where pos_installment_plan.installment_acc_id = '" + installmentAccID_db.ToString() + "';";
                        double total_dues_db = data.SearchNumericValuesDb(GetSetData.query);
                        nowLastCredits = TextData.lastCredits - total_dues_db;

                        if (nowLastCredits >= 0)
                        {
                            txtTotalLastCredits.Text = nowLastCredits.ToString();
                        }
                        else
                        {
                            txtTotalLastCredits.Text = TextData.lastCredits.ToString();
                        }

                        //************************************************************************************
                        dueAmount_db -= double.Parse(txtUpdatedDueAmount.Text);
                        nowLastCredits = TextData.lastCredits - dueAmount_db;

                        if (nowLastCredits >= 0)
                        {
                            last_credit_text.Text = nowLastCredits.ToString();
                        }
                        else
                        {
                            last_credit_text.Text = TextData.lastCredits.ToString();
                        }

                        //**************************************************
                        TextData.lastCredits = 0;
                        TextData.cash = (double.Parse(txtInstallment.Text) + double.Parse(txtInterest.Text) + double.Parse(txtUpdatedDueAmount.Text));
                        txtTotalAmount.Text = TextData.cash.ToString();
                        cash_text.Text = TextData.cash.ToString();
                    }
                }
                //else
                //{
                //    txtUpdatedDueAmount.Text = "0";
                //}


                if (txtUpdatedDueAmount.Text != "")
                {
                    txtDues.Text = txtUpdatedDueAmount.Text;
                }
                else
                {
                    txtDues.Text = DueAmount.ToString();
                    last_credit_text.Text = lastBalance.ToString();
                    TextData.cash = double.Parse(txtInstallment.Text) + double.Parse(txtDues.Text);
                }
                //************************************************
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
            }
        }

        private void txtDues_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dueAmountTxtChanged();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnCustomerInvoices_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Customer Recovery Form", "Customer invoices button click...", role_id);
            formCustomerWiseBillsDetails.role_id = role_id;
            buttonControls.customersInvoicesButton();
        }

        private void btnGenerateInvoices_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Customer Recovery Form", "Generate Invoices button click...", role_id);
            //buttonControls.generateInvoicesButton();
        }

        private void txtUpdatedDueAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtUpdatedDueAmount.Text, e);
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            if (customer_text.Text.Length > 2)
            {
                int counter = 0;

                GetSetData.query = @"select count(full_name) from  pos_customers where (full_name = '" + customer_text.Text + "');";
                counter = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (counter > 1)
                {
                    Customer_details.isDropDown = true;
                    Customer_details.role_id = role_id;
                    Customer_details.selected_customer = customer_text.Text;
                    Customer_details.role_id = role_id;
                    buttonControls.CustomerDetailsbuttons();
                    customer_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                }
                else
                {
                    customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_text.Text);
                }

                //FillComboBoxCustomeCodes();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
            else
            {
                error.errorMessage("Please enter minimum 3 characters.");
                error.ShowDialog();
            }
        }
    }
}
