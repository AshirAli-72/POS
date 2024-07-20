using System;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using Message_box_info.forms;
using Banking_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Banks_Loan_info.forms
{
    public partial class formAddNewLoan : Form
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

        public formAddNewLoan()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static bool saveEnable = false;


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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCapitalAmount);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);

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
                form_branch_title.role_id = role_id;
                form_transaction_type.role_id = role_id;

                savebutton.Visible = bool.Parse(data.UserPermissions("new_bankLoan_save", "pos_tbl_authorities_button_controls3", role_id));
                update_button.Visible = bool.Parse(data.UserPermissions("new_bankLoan_update", "pos_tbl_authorities_button_controls3", role_id));
                close_button.Visible = bool.Parse(data.UserPermissions("new_bankLoan_exit", "pos_tbl_authorities_button_controls3", role_id));
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txt_date.Text = TextData.date;
                txt_time.Text = TextData.time;
                txtBankName.Text = TextData.bank_title;
                txtBankCode.Text = TextData.bankCode;
                txt_branch.Text = TextData.branch_title;
                txt_type.Text = TextData.transation_type;
                txt_remarks.Text = TextData.remarks;
                txtStatus.Text = TextData.status;
                //txtPrinciple.Text = TextData.principle.ToString();
                //txtInterest.Text = TextData.interest.ToString();
                //txtTotalAmount.Text = TextData.totalAmount.ToString();

                GetSetData.query = "select BankLoan_id from pos_bankLoansDetails where (bank_name = '" + TextData.bank_title.ToString() + "') and (code = '" + TextData.bankCode.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                txtPrinciple.Text = data.UserPermissions("principle", "pos_bankLoansDetails", "BankLoan_id", GetSetData.Ids.ToString());
                txtInterest.Text = data.UserPermissions("interest", "pos_bankLoansDetails", "BankLoan_id", GetSetData.Ids.ToString());
                txtTotalAmount.Text = data.UserPermissions("totalAmount", "pos_bankLoansDetails", "BankLoan_id", GetSetData.Ids.ToString());
                txt_type.Text = TextData.transation_type;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Bank Loan";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                txtBankCode.Text = auto_generate_code("show");
                FormNamelabel.Text = "Create New Bank Loan";
            }
        }

        private void RefreshFieldBranch()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_branch, "fillComboBoxBranchkTitle", "branch_title");
        }

        private void RefreshFieldTransactionType()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_type, "fillComboBoxTransactionType", "transaction_type");
        }

        private void refresh()
        {
            txt_date.Text = DateTime.Now.ToLongDateString();
            txt_time.Text = DateTime.Now.ToLongTimeString();
            txt_type.Text = "-- Select --";
            txtBankName.Text = "";
            txt_branch.Text = "-- Select --";
            txtPrinciple.Text = "0";
            txtInterest.Text = "0";
            txtTotalAmount.Text = "0";
            txt_remarks.Text = "";
            txtStatus.Text = "Active";
            system_user_permissions();
            enableSaveButton();
            txt_date.Select();
        }

        private void formAddNewLoan_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Bank Loan Form", "Exit...", role_id);
            //buttonControllers.bank_detail_buttons();
            this.Close();
        }

        private void btnRepayType_Click(object sender, EventArgs e)
        {
            using (form_transaction_type add_customer = new form_transaction_type())
            {
                add_customer.ShowDialog();
                //Button_controls.Transaction_type_buttons();
                RefreshFieldTransactionType();
            }
        }

        private void btnBranch_Click(object sender, EventArgs e)
        {
            using (form_branch_title add_customer = new form_branch_title())
            {
                add_customer.ShowDialog();
                //Button_controls.branch_buttons();
                RefreshFieldBranch();
            }
        }

        private string auto_generate_code(string condition)
        {
            TextData.bankCode = "";

            try
            {
                GetSetData.query = @"SELECT top 1 bankLoanCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set bankLoanCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //**********************************************************************************************

                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.Data = "100000000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.Data = "10000000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.Data = "1000000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.Data = "100000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.Data = "10000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.Data = "1000";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "100";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "10";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "1";
                    TextData.bankCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                return TextData.bankCode;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.bankCode;
            }
        }

        private void fillValuesInVariables()
        {
            try
            {
                TextData.date = txt_date.Text;
                TextData.time = txt_time.Text;
                TextData.transation_type = txt_type.Text;
                TextData.bank_title = txtBankName.Text;
                TextData.branch_title = txt_branch.Text;
                TextData.principle = 0;
                TextData.interest = 0;
                TextData.totalAmount = 0;
                TextData.remarks = txt_remarks.Text;
                TextData.status = txtStatus.Text;

                if (txt_type.Text == "" || txt_type.Text == "-- Select --")
                {
                    TextData.transation_type = "others";
                }

                if (txt_branch.Text == "" || txt_branch.Text == "-- Select --")
                {
                    TextData.branch_title = "others";
                }

                if (txt_remarks.Text == "")
                {
                    TextData.remarks = "nill";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private bool add_records_Grid_view()
        //{
        //    //Add New Customers Details and SaleItems list)
        //    try
        //    {
        //        fillValuesInVariables();
        //        TextData.date = txt_date.Text;
        //        TextData.time = txt_time.Text;

        //        if (txtBankName.Text != "" || txtBankName.Text != "-- Select --")
        //        {
        //            if (txt_type.Text != "" || txt_type.Text != "-- Select --")
        //            {
        //                if (txtTotalAmount.Text != "")
        //                {
        //                    if (txtPrinciple.Text != "")
        //                    {
        //                        if (txtInterest.Text != "")
        //                        {
        //                            TextData.bankCode = auto_generate_code("");
        //                            TextData.principle = double.Parse(txtPrinciple.Text);
        //                            TextData.interest = double.Parse(txtInterest.Text);
        //                            TextData.totalAmount = double.Parse(txtTotalAmount.Text);

        //                            int n = productListGridView.Rows.Add();
        //                            productListGridView.Rows[n].Cells[1].Value = TextData.date.ToString();
        //                            productListGridView.Rows[n].Cells[2].Value = TextData.time.ToString();
        //                            productListGridView.Rows[n].Cells[3].Value = TextData.transation_type.ToString();
        //                            productListGridView.Rows[n].Cells[4].Value = TextData.bank_title.ToString();
        //                            productListGridView.Rows[n].Cells[5].Value = TextData.bankCode.ToString();
        //                            productListGridView.Rows[n].Cells[6].Value = TextData.branch_title.ToString();
        //                            productListGridView.Rows[n].Cells[7].Value = TextData.principle.ToString();
        //                            productListGridView.Rows[n].Cells[8].Value = TextData.interest.ToString();
        //                            productListGridView.Rows[n].Cells[9].Value = TextData.totalAmount.ToString();
        //                            productListGridView.Rows[n].Cells[10].Value = TextData.remarks.ToString();

        //                            GetSetData.SaveLogHistoryDetails("Add New Bank Loan Form", "Add items in Cart...", role_id);
        //                            refresh();
        //                        }
        //                        else
        //                        {
        //                            error.errorMessage("Please enter markup amount!");
        //                            error.ShowDialog();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        error.errorMessage("Please enter principle amount!");
        //                        error.ShowDialog();
        //                    }
        //                }
        //                else
        //                {
        //                    error.errorMessage("Please enter total amount!");
        //                    error.ShowDialog();
        //                }
        //            }
        //            else
        //            {
        //                error.errorMessage("Please select repay type!");
        //                error.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage("Please select the bank!");
        //            error.ShowDialog();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        error.errorMessage(ex.Message);
        //        error.ShowDialog();
        //        return false;
        //    }
        //}

        private bool insert_records_into_db()
        {
            try
            {
                fillValuesInVariables();

                int type_id_db = data.UserPermissionsIds("transaction_id", "pos_transaction_type", "transaction_type", TextData.transation_type);
                //========================================================================================================
                //int bank_id_db = data.UserPermissionsIds("bank_id", "pos_bank", "bank_title", productListGridView.Rows[i].Cells[4].Value.ToString());
                //========================================================================================================
                int branch_id_db = data.UserPermissionsIds("branch_id", "pos_bank_branch", "branch_title", TextData.branch_title);
                //========================================================================================================
                if (txtBankName.Text != "")
                {
                    if (txt_type.Text != "" || txt_type.Text != "-- Select --")
                    {
                        if (txtPrinciple.Text != "")
                        {
                            if (txtInterest.Text != "")
                            {
                                TextData.bankCode = auto_generate_code("");
                                TextData.principle = double.Parse(txtPrinciple.Text);
                                TextData.interest = double.Parse(txtInterest.Text);
                                TextData.totalAmount = double.Parse(txtTotalAmount.Text);

                                // Insert Data From GridView to SaleItems in Database:  ** (prod_name, barcode, manufacture_date, expiry_date, prod_state, unit, item_type, size, image_path, status, remarks, category_id, brand_id, sub_cate_id, color_id) **
                                GetSetData.query = @"insert into pos_bankLoansDetails values ('" + TextData.date + "' , '" + TextData.time + "' , '" + TextData.bank_title + "' , '" + TextData.bankCode + "' , '" + TextData.principle.ToString() + "' , '" + TextData.interest.ToString() + "' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.remarks + "' , '" + TextData.status +"' , '" + type_id_db.ToString() + "' , '" + branch_id_db.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                //========================================================================================================

                                GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (bank_name = '" + TextData.bank_title + "') and (code = '" + TextData.bankCode + "');";
                                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                GetSetData.query = @"insert into pos_bankLoanPayables values ('" + TextData.totalAmount.ToString() + "' , '" + TextData.date + "' , '" + GetSetData.Ids.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                //========================================================================================================
                                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                                string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");

                                if (GetSetData.Data == "Yes")
                                {
                                    if (capital != "NULL" && capital != "")
                                    {
                                        TextData.totalAmount = double.Parse(capital) + TextData.totalAmount;

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
                                TextData.date = "Saving cart items in database... " + "[" + TextData.bank_title + "  " + TextData.bankCode + "]";
                                GetSetData.SaveLogHistoryDetails("Add New Bank Loan Form", TextData.date, role_id);

                                return true;
                            }
                            else
                            {
                                error.errorMessage("Please enter markup amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter principle amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select repay type!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the bank name!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if(insert_records_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
            }
        }

        private bool update_records_db()
        {
            try
            {
                fillValuesInVariables();
                TextData.bankCode = txtBankCode.Text;

                if (txt_type.Text != "" || txt_type.Text != "-- Select --")
                {
                    if (txtBankName.Text != "")
                    {
                        if (txt_branch.Text != "" || txt_branch.Text != "-- Select --")
                        {
                            if (txtPrinciple.Text != "")
                            {
                                if (txtInterest.Text != "")
                                {
                                    if (txtTotalAmount.Text != "")
                                    {
                                        TextData.principle = double.Parse(txtPrinciple.Text);
                                        TextData.interest = double.Parse(txtInterest.Text);
                                        TextData.totalAmount = double.Parse(txtTotalAmount.Text);

                                        //========================================================================================================
                                        int type_id_db = data.UserPermissionsIds("transaction_id", "pos_transaction_type", "transaction_type", TextData.transation_type);
                                        //========================================================================================================
                                        //int bank_id_db = data.UserPermissionsIds("bank_id", "pos_bank", "bank_title", TextData.bank_title);
                                        //========================================================================================================
                                        int branch_id_db = data.UserPermissionsIds("branch_id", "pos_bank_branch", "branch_title", TextData.branch_title);
                                        //========================================================================================================

                                        GetSetData.query = @"select BankLoan_id from pos_bankLoansDetails where (code = '" + TextData.bankCode + "');";
                                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                        double previousTotalAmount = data.UserPermissionsIds("totalAmount", "pos_bankLoansDetails", "BankLoan_id", GetSetData.Ids.ToString());
                                        //========================================================================================================

                                        GetSetData.query = @"update pos_bankLoansDetails set date = '" + txt_date.Text + "', bank_name = '" + txtBankName.Text + "', principle = '" + TextData.principle.ToString() + "', interest = '" + TextData.interest.ToString() + "', totalAmount = '" + TextData.totalAmount.ToString() + "', status = '" + TextData.status + "' , remarks = '" + TextData.remarks + "' , t_type_id = '" + type_id_db.ToString() + "', branch_id  = '" + branch_id_db.ToString() + "' where (BankLoan_id = '" + GetSetData.Ids.ToString() +"');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                        //========================================================================================================

                                        GetSetData.query = @"update pos_bankLoanPayables set last_balance = '" + TextData.totalAmount.ToString() + "' , last_payment = '" + TextData.date + "' where  (BankLoan_id = '" + GetSetData.Ids.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                        //========================================================================================================

                                        string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                                        GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                        if (GetSetData.Data == "Yes")
                                        {
                                            if (capital != "NULL" && capital != "")
                                            {
                                                TextData.totalAmount = ((double.Parse(capital) + previousTotalAmount) - TextData.totalAmount);
                                            }

                                            if (TextData.totalAmount >= 0)
                                            {
                                                GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                            }
                                        }
                                        // *****************************************************************************************

                                        TextData.date = "Updating Bank Loan Details " + "[" + (TextData.date + "  " + TextData.time) + "]";
                                        GetSetData.SaveLogHistoryDetails("Add New Bank Loan Form", TextData.date, role_id);

                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter amount!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter account number!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter account title!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter branch title!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter bank title!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter Repay type!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                return false;
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if(update_records_db())
            {
                done.DoneMessage("Successfully Updated!");
                done.ShowDialog();
            }
        }

        private void calculateTotalAmount()
        {
            try
            {
                TextData.principle = 0;
                TextData.interest = 0;
                TextData.totalAmount = 0;

                if (txtPrinciple.Text != "")
                {
                    TextData.principle = double.Parse(txtPrinciple.Text);
                }

                if (txtInterest.Text != "")
                {
                    TextData.interest = double.Parse(txtInterest.Text);
                }

                TextData.interest = (TextData.principle * TextData.interest) / 100;
                TextData.totalAmount = TextData.principle + TextData.interest;
                txtTotalAmount.Text = TextData.totalAmount.ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtPrinciple_TextChanged(object sender, EventArgs e)
        {
            calculateTotalAmount();
        }

        private void txtInterest_TextChanged(object sender, EventArgs e)
        {
            calculateTotalAmount();
        }

        private void txt_bank_title_Enter(object sender, EventArgs e)
        {
            //RefreshFieldBankTitle();
        }

        private void txt_type_Enter(object sender, EventArgs e)
        {
            RefreshFieldTransactionType();
        }

        private void txt_branch_Enter(object sender, EventArgs e)
        {
            RefreshFieldBranch();
        }

        private void txtPrinciple_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPrinciple.Text, e);
        }

        private void txtInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtInterest.Text, e);
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
