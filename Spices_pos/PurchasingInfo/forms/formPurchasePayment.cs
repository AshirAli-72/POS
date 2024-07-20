using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Purchase_info.forms
{
    public partial class formPurchasePayment : Form
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

        public formPurchasePayment()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCapital);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, txtCapitalAmount);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

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
                txtCapitalAmount.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newSupplierPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.query = data.UserPermissions("newSupplierPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnUpdate.Visible = bool.Parse(GetSetData.query);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.query) == false)
                {
                    pnl_save.Visible = false;
                }
                //***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("new_investorPayment_savePrint", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //pnl_save_and_print.Visible = bool.Parse(GetSetData.Data);

                //***************************************************************************************************
                GetSetData.Data = data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", role_id.ToString());

                if (GetSetData.Data == "TURE" || GetSetData.Data == "true" || GetSetData.Data == "True")
                {
                    lblCapital.Visible = true;
                    txtCapitalAmount.Visible = true;
                }
                else
                {
                    lblCapital.Visible = false;
                    txtCapitalAmount.Visible = false;
                }
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
                FormNamelabel.Text = "Create New Vendor Ledger";
                LoginEmployee();
                txtDate.Select();
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                //salesAndReturnsGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Vendor Ledger";
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
                txtSupplier.Text = TextData.full_name;
                txtSupplierCode.Text = TextData.cus_code;
                txtEmployeeName.Text = TextData.employee;
                references_text.Text = TextData.reference;
                comments_text.Text = TextData.remarks;
                txtAmount.Text = TextData.paid.ToString();

                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomeDetails()
        {
            GetSetData.query = @"select mobile_no from pos_suppliers where full_name = '" + txtSupplier.Text + "' and code = '" + txtSupplierCode.Text + "';";
            txtContactPerson.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select contact_person from pos_suppliers where full_name = '" + txtSupplier.Text + "' and code = '" + txtSupplierCode.Text + "';";
            txtContactNo.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select address from pos_suppliers where full_name = '" + txtSupplier.Text + "' and code = '" + txtSupplierCode.Text + "';";
            txt_address.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select picture_path from pos_general_settings;";
            TextData.image_path = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select image_path from pos_suppliers where full_name = '" + txtSupplier.Text + "' and code = '" + txtSupplierCode.Text + "';";
            TextData.image = data.SearchStringValuesFromDb(GetSetData.query);

            if (TextData.image != "nill" && TextData.image != "")
            {
                img_pic_box.Image = Image.FromFile(TextData.image_path + TextData.image);
            }
        }

        private void FillComboBoxCusLastCredits()
        {
            try
            {
                GetSetData.query = @"select supplier_id from pos_suppliers where (full_name = '" + txtSupplier.Text + "') and (code = '" + txtSupplierCode.Text + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.query = @"select previous_payables from pos_supplier_payables where (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                txtPreviousPayables.Text = data.SearchStringValuesFromDb(GetSetData.query);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void FillComboBoxCustomerName()
        {
            txtSupplier.Text = data.UserPermissions("full_name", "pos_suppliers", "code", txtSupplierCode.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            txtSupplierCode.Text = data.UserPermissions("code", "pos_suppliers", "full_name", txtSupplier.Text);
        }

        private void FillGridViewUsingPagination()
        {
            try
            {
                GetSetData.query = "select * from ViewSupplierPaybookDetails where ([Supplier] = '" + txtSupplier.Text + "') and ([Code] = '" + txtSupplierCode.Text + "');";
                GetSetData.FillDataGridViewUsingPagination(purchaseDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refreshEveryFields()
        {
            txtEmployeeName.Text = "";
        }

        private void refresh()
        {
            txtDate.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToLongTimeString();
            txtSupplier.Text = "";
            txtSupplierCode.Text = "";
            txtContactPerson.Text = "";
            references_text.Text = "";
            comments_text.Text = "";
            txtBalance.Text = "0";
            txtPreviousPayables.Text = "0.00";
            txt_address.Text = "";
            txtContactNo.Text = "";
            img_pic_box.Image = null;

            if (data.UserPermissions("round(total_capital, 2)", "pos_capital") == "" || data.UserPermissions("round(total_capital, 2)", "pos_capital") == "NULL")
            {
                txtCapitalAmount.Text = "0";
            }
            else
            {
                txtCapitalAmount.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
            }
        }

        private void funFillCusEmpNames()
        {
            try
            {
                //txtEmployeeName.Text = get_employee;
                txtSupplier.Text = get_customer;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void formPurchasePayment_Load(object sender, EventArgs e)
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToLongTimeString();

                funFillCusEmpNames();
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
            GetSetData.SaveLogHistoryDetails("Add New Supplier Payment Form", "Exit...", role_id);
            this.Close();
            //sendSmsToCustomers();
        }

        private void btn_sale_person_Click(object sender, EventArgs e)
        {
            using (form_purchase_from customer = new form_purchase_from())
            {
                form_purchase_from.role_id = role_id;
                customer.ShowDialog();
            }
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            using (Add_supplier add_customer = new Add_supplier())
            {
                Add_supplier.role_id = role_id;
                add_customer.ShowDialog();
            }
        }

        private void txtSalaries_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAmount.Text, e);
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
            refreshEveryFields();
        }

        private void FillVariablesWithValues()
        {
            try
            {
                TextData.dates = txtDate.Text;
                TextData.times = time_text.Text;
                TextData.full_name = txtSupplier.Text;
                TextData.cus_code = txtSupplierCode.Text;
                TextData.employee = txtEmployeeName.Text;
                TextData.phone1 = txtContactPerson.Text;
                TextData.reference = references_text.Text;
                TextData.remarks = comments_text.Text;
                TextData.paid = 0;
                TextData.last_balance = double.Parse(txtPreviousPayables.Text);
                TextData.credits = double.Parse(txtBalance.Text);

                if (txtAmount.Text != "")
                {
                    TextData.paid = double.Parse(txtAmount.Text);
                }

                if (comments_text.Text == "")
                {
                    TextData.remarks = "nill";
                }

                if (references_text.Text == "")
                {
                    TextData.reference = "nill";
                }

                if (txtEmployeeName.Text == "")
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
                TextData.totalCapital = data.UserPermissions("round(total_capital, 2)", "pos_capital");

                if (TextData.totalCapital == "" || TextData.totalCapital == "NULL")
                {
                    TextData.totalCapital = "0";
                }

                if (txtEmployeeName.Text != "")
                {
                    if (txtSupplier.Text != "")
                    {
                        if (txtAmount.Text != "")
                        {
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                            if (TextData.paid <= double.Parse(TextData.totalCapital) || GetSetData.Data == "No")
                            {
                                GetSetData.query = @"select supplier_id from pos_suppliers where (full_name = '" + TextData.full_name.ToString() + "') and (code = '" + TextData.cus_code.ToString() + "');";
                                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                // *******************************************************************************
                                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                // *******************************************************************************
                                GetSetData.query = @"insert into pos_supplier_paybook values ('" + TextData.dates.ToString() + "' , '" + TextData.times.ToString() + "' , '" + TextData.reference.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.last_balance.ToString() + "', '" + TextData.credits.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                // *******************************************************************************

                                GetSetData.query = @"select payment_id from pos_supplier_paybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                                int payment_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                TextData.lastCredits = data.NumericValues("previous_payables", "pos_supplier_payables", "supplier_id", GetSetData.Ids.ToString());

                                if (TextData.lastCredits == -1)
                                {
                                    GetSetData.query = @"insert into pos_supplier_payables values ('" + TextData.credits.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                else if (TextData.lastCredits != -1 && TextData.lastCredits >= 0)
                                {
                                    GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.credits.ToString() + "' , due_days = '" + TextData.dates.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                //  Customer Transactions *******************************************************************************
                                GetSetData.query = @"insert into pos_company_transactions values ('" + TextData.dates + "' , '" + TextData.times + "' , 'Nill', 'Nill', '0', '0', '0', '0', '0', '0', '" + TextData.paid.ToString() + "', '" + TextData.last_balance.ToString() + "', '0' , 'Payment' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                // *****************************************************************************************

                                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                                if (GetSetData.Data == "Yes")
                                {
                                    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                                    {
                                        TextData.paid = double.Parse(TextData.totalCapital) - TextData.paid;
                                    }

                                    GetSetData.query = @"update pos_capital set total_capital = '" + TextData.paid.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                // *****************************************************************************************
                                GetSetData.SaveLogHistoryDetails("Add New Supplier Payment Form", "Saving Supplier payment [" + TextData.dates + "  " + TextData.times + "] details", role_id);

                                return true;
                            }
                            else
                            {
                                error.errorMessage("Available Capital is '" + TextData.totalCapital + "'!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the salaries!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the investor!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the casher!");
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

            //if (message_choose == true)
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
                TextData.totalCapital = data.UserPermissions("round(total_capital, 2)", "pos_capital");

                if (TextData.totalCapital == "" || TextData.totalCapital == "NULL")
                {
                    TextData.totalCapital = "0";
                }

                if (txtEmployeeName.Text != "")
                {
                    if (txtSupplier.Text != "")
                    {
                        if (txtAmount.Text != "")
                        {
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                            if (TextData.paid <= double.Parse(TextData.totalCapital) || GetSetData.Data == "No")
                            {
                                GetSetData.query = @"select supplier_id from pos_suppliers where (full_name = '" + TextData.full_name + "') and (code = '" + TextData.cus_code + "');";
                                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                // *******************************************************************************
                                GetSetData.query = @"select payment_id from pos_supplier_paybook where (date = '" + txtDate.Text + "') and (time = '" + time_text.Text + "');";
                                int payment_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                double previousPaid = data.NumericValues("payment", "pos_supplier_paybook", "payment_id", payment_id_db.ToString());

                                // *******************************************************************************
                                GetSetData.query = @"update pos_supplier_paybook set reference = '" + TextData.reference + "' , remarks = '" + TextData.remarks + "' , payment = '" + TextData.paid.ToString() + "', previous_payables = '" + TextData.last_balance.ToString() + "', balance = '" + TextData.credits.ToString() + "' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (date = '" + txtDate.Text + "') and (time = '" + time_text.Text + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                // *****************************************************************************************

                                TextData.credits = (TextData.last_balance + previousPaid) - TextData.paid;

                                if (TextData.credits < 0)
                                {
                                    TextData.credits = 0;
                                }

                                if (TextData.credits >= 0)
                                {
                                    GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.credits.ToString() + "' , due_days = '" + TextData.dates.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                // *****************************************************************************************

                                //  Customer Transactions *******************************************************************************
                                GetSetData.query = @"update pos_company_transactions set credits = '" + TextData.paid.ToString() + "' ,  pCredits = '" + TextData.credits.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.dates + "') and (time = '" + TextData.times + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                if (GetSetData.Data == "Yes")
                                {
                                    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                                    {
                                        TextData.paid = (double.Parse(TextData.totalCapital) + previousPaid) - TextData.paid;
                                    }

                                    if (TextData.paid < 0)
                                    {
                                        TextData.paid = 0;
                                    }

                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.paid.ToString() + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                // *****************************************************************************************

                                GetSetData.SaveLogHistoryDetails("Add New Supplier Payment Form", "Updating Supplier payment [" + TextData.dates + "  " + TextData.times + "] details", role_id);

                                return true;
                            }
                            else
                            {
                                error.errorMessage("Available Capital is '" + TextData.totalCapital + "'!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the salaries!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the investor!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the casher!");
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

        private void Printbutton_Click(object sender, EventArgs e)
        {
            //TextData.dates = txtDate.Text;
            //TextData.times = time_text.Text;

            //if (saveEnable == false)
            //{
            //    if (insert_into_db())
            //    {
            //        //formInvestorPaybookReceipt reports = new formInvestorPaybookReceipt();
            //        //reports.ShowDialog();
            //        //refresh();
            //    }
            //}
            //else if (saveEnable == true)
            //{
            //    if (UpdateRecoveryDetailsdb())
            //    {
            //        GetSetData.SaveLogHistoryDetails("Add New Supplier Payment Form", "Print Supplier payment invoice button click...", role_id);

            //        //formInvestorPaybookReceipt reports = new formInvestorPaybookReceipt();
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

        private void txt_sale_person_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtSupplier, "fillComboBoxSupplierNames", "full_name");
        }

        private void customer_code_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtSupplierCode, "fillComboBoxSupplierNames", "code");
        }

        private void customer_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxCustomeCodes();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void customer_code_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxCustomerName();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void txtSalaries_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.paid = 0;

                if (txtAmount.Text != "")
                {
                    TextData.paid = double.Parse(txtAmount.Text);
                    TextData.paid = double.Parse(txtPreviousPayables.Text) - TextData.paid;

                    if (TextData.paid >= 0)
                    {
                        txtBalance.Text = TextData.paid.ToString();
                    }
                    else
                    {
                        txtBalance.Text = "0";
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
            FillComboBoxCustomeDetails();
            FillComboBoxCusLastCredits();
            FillGridViewUsingPagination();
        }
    }
}
