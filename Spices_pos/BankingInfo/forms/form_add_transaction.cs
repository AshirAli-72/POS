using System;
using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using Supplier_Chain_info.forms;

namespace Banking_info.forms
{
    public partial class form_add_transaction : Form
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

        public form_add_transaction()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
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
                form_account_number.role_id = role_id;
                form_account_title.role_id = role_id;
                form_bank_title.role_id = role_id;
                form_branch_title.role_id = role_id;
                form_status.role_id = role_id;
                form_transaction_type.role_id = role_id;

                savebutton.Visible = bool.Parse(data.UserPermissions("new_transaction_save", "pos_tbl_authorities_button_controls2", role_id));
                update_button.Visible = bool.Parse(data.UserPermissions("new_transaction_update", "pos_tbl_authorities_button_controls2", role_id));

                if (bool.Parse(data.UserPermissions("new_transaction_save", "pos_tbl_authorities_button_controls2", role_id)) == false && bool.Parse(data.UserPermissions("new_transaction_update", "pos_tbl_authorities_button_controls2", role_id)) == false)
                {
                    pnl_exit.Visible = false;
                }

                pnl_save.Visible = bool.Parse(data.UserPermissions("new_transaction_exit", "pos_tbl_authorities_button_controls2", role_id));
                //GetSetData.addFormCopyrights(lblCopyrights);
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
                txt_employee.Text = TextData.employee;
                txt_status.Text = TextData.status;
                txt_type.Text = TextData.transation_type;
                txt_bank_title.Text = TextData.bank_title;
                txt_branch.Text = TextData.branch_title;
                txt_account_title.Text = TextData.account;
                txt_account_no.Text = TextData.account_no;
                txt_amount.Text = TextData.amount.ToString();


                GetSetData.query = "select banking_id from pos_banking_details where date = '" + TextData.dateKey.ToString() + "' and time = '" + TextData.time.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                txt_reference.Text = data.UserPermissions("reference", "pos_banking_details", "banking_id", GetSetData.Ids.ToString());
                txt_remarks.Text = data.UserPermissions("remarks", "pos_banking_details", "banking_id", GetSetData.Ids.ToString());
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
                txt_employee.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                //txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "employee_id", GetSetData.Ids.ToString());
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
                add_button.Enabled = false;
                update_button.Visible = true;
                productListGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Transaction";

                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                LoginEmployee();
                FormNamelabel.Text = "Create New Transaction";
            }
        }

        private void RefreshFieldEmployee()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_employee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void RefreshFieldBankTitle()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_bank_title, "fillComboBoxBankTitle", "bank_title");
        }

        private void RefreshFieldBranch()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_branch, "fillComboBoxBranchkTitle", "branch_title");
        }

        private void RefreshFieldAccountNo()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_account_no, "fillComboBoxBankAccountNo", "account_no");
        }

        private void RefreshFieldAccountTitle()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_account_title, "fillComboBoxAccountTitle", "account_title");
        }

        private void RefreshFieldTransactionStatus()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_status, "fillComboBoxTransactionStatus", "status_title");
        }

        private void RefreshFieldTransactionType()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_type, "fillComboBoxTransactionType", "transaction_type");
        }

        private void clearGridView()
        {
            txt_status.Text = "-- Select --";
            txt_employee.Text = "-- Select --";
            int a = productListGridView.Rows.Count;

            // Refresh Button Event is Generated:
            for (int i = 0; i < a; i++)
            {
                foreach (DataGridViewRow row in productListGridView.SelectedRows)
                {
                    productListGridView.Rows.Remove(row);
                }
            }
            productListGridView.DataSource = null;
        }

        private void refresh()
        {
            txt_date.Text = DateTime.Now.ToLongDateString();
            txt_time.Text = DateTime.Now.ToLongTimeString();
            //txt_type.Text = "-- Select --";
            //txt_bank_title.Text = "-- Select --";
            //txt_branch.Text = "-- Select --";
            //txt_account_title.Text = "-- Select --";
            //txt_account_no.Text = "-- Select --";
            txt_amount.Text = "0";
            txt_reference.Text = "";
            txt_remarks.Text = "";
            system_user_permissions();
            enableSaveButton();
            txt_date.TabIndex = 0;
            txt_date.Focus();
        }

        private void form_add_transaction_Load(object sender, EventArgs e)
        {
            try
            {
                clearGridView();
                refresh();
                productListGridView.MouseClick += new MouseEventHandler(productListGridView_MouseClick);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void productListGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                GetSetData.Ids = productListGridView.HitTest(e.X, e.Y).RowIndex;

                if (GetSetData.Ids >= 0)
                {
                    my_menu.Items.Add("Delete").Name = "Delete";
                }

                my_menu.Show(productListGridView, new Point(e.X, e.Y));

                my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_EditClicked);
            }
        }

        private void my_menu_EditClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            deleteRowFromGridView();
        }

        private void deleteRowFromGridView()
        {
            try
            {
                GetSetData.Ids = productListGridView.CurrentCell.RowIndex;
                productListGridView.Rows.RemoveAt(GetSetData.Ids);
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
            }
        }

        private void productListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productListGridView.Columns[e.ColumnIndex].Name == "Delete")
            {
                deleteRowFromGridView();
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Bank Transaction Form", "Exit...", role_id);
            //Button_controls.bank_detail_buttons();
            this.Close();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
            clearGridView();
        }

        private void amount_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_amount.Text, e);
        }

        private void btn_status_Click(object sender, EventArgs e)
        {
            using (form_status add_customer = new form_status())
            {
                form_status.title = txt_status.Text;
                add_customer.ShowDialog();
                RefreshFieldTransactionStatus();
            }
        }

        private void btn_bank_Click(object sender, EventArgs e)
        {
            using (form_bank_title add_customer = new form_bank_title())
            {
                form_bank_title.title = txt_bank_title.Text;
                add_customer.ShowDialog();
                RefreshFieldBankTitle();
            }
            //Button_controls.bank_buttons();
        }

        private void btn_branch_Click(object sender, EventArgs e)
        {
            using (form_branch_title add_customer = new form_branch_title())
            {
                form_branch_title.title = txt_branch.Text;
                add_customer.ShowDialog();
                RefreshFieldBranch();
            }
            //Button_controls.branch_buttons();
        }

        private void btn_account_no_Click(object sender, EventArgs e)
        {
            using (form_account_number add_customer = new form_account_number())
            {
                form_account_number.title = btn_account_no.Text;
                add_customer.ShowDialog();
                RefreshFieldAccountNo();
            }
            //Button_controls.AccountNo_buttons();
        }

        private void btn_account_Click(object sender, EventArgs e)
        {
            using (form_account_title add_customer = new form_account_title())
            {
                form_account_title.title = txt_account_title.Text;
                add_customer.ShowDialog();
                RefreshFieldAccountTitle();
            }
            //Button_controls.Account_buttons();
        }

        private void btn_type_Click(object sender, EventArgs e)
        {
            using (form_transaction_type add_customer = new form_transaction_type())
            {
                form_transaction_type.title = txt_type.Text;
                add_customer.ShowDialog();
                RefreshFieldTransactionType();
            }
            //Button_controls.Transaction_type_buttons();
        }

        private void btn_employee_Click(object sender, EventArgs e)
        {
            using (Add_supplier add_customer = new Add_supplier())
            {
                Add_supplier.role_id = role_id;
                add_customer.ShowDialog();
                RefreshFieldEmployee();
            }
            //Button_controls.employee_buttons();
        }

        private bool add_records_Grid_view()
        {
            //Add New Customers Details and SaleItems list)
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.employee = txt_employee.Text;
                TextData.status = txt_status.Text;
                TextData.transation_type = txt_type.Text;
                TextData.date = txt_date.Text;
                TextData.time = txt_time.Text;
                TextData.bank_title = txt_bank_title.Text;
                TextData.branch_title = txt_branch.Text;
                TextData.account = txt_account_title.Text;
                TextData.account_no = txt_account_no.Text;
                TextData.amount = double.Parse(txt_amount.Text);
                TextData.reference = txt_reference.Text;
                TextData.remarks = txt_remarks.Text;

                if (txt_employee.Text == "" || txt_employee.Text == "-- Select --")
                {
                    TextData.employee = "nill";
                }

                if (txt_status.Text == "" || txt_status.Text == "-- Select --")
                {
                    TextData.status = "others";
                }

                if (txt_type.Text == "" || txt_type.Text == "-- Select --")
                {
                    TextData.transation_type = "others";
                }

                if (txt_branch.Text == "" || txt_branch.Text == "-- Select --")
                {
                    TextData.branch_title = "others";
                }

                if (txt_account_title.Text == "" || txt_account_title.Text == "-- Select --")
                {
                    TextData.account = "others";
                }

                if (txt_reference.Text == "")
                {
                    TextData.reference = "nill";
                }

                if (txt_remarks.Text == "")
                {
                    TextData.remarks = "nill";
                }


                if (txt_bank_title.Text != "" || txt_bank_title.Text != "-- Select --")
                {
                    if (txt_account_no.Text != "" || txt_account_no.Text != "-- Select --")
                    {
                        if (txt_amount.Text != "")
                        {

                            int n = productListGridView.Rows.Add();
                            productListGridView.Rows[n].Cells[1].Value = TextData.date.ToString();
                            productListGridView.Rows[n].Cells[2].Value = TextData.time.ToString();
                            productListGridView.Rows[n].Cells[3].Value = TextData.employee.ToString();
                            productListGridView.Rows[n].Cells[4].Value = TextData.status.ToString();
                            productListGridView.Rows[n].Cells[5].Value = TextData.transation_type.ToString();
                            productListGridView.Rows[n].Cells[6].Value = TextData.bank_title.ToString();
                            productListGridView.Rows[n].Cells[7].Value = TextData.branch_title.ToString();
                            productListGridView.Rows[n].Cells[8].Value = TextData.account.ToString();
                            productListGridView.Rows[n].Cells[9].Value = TextData.account_no.ToString();
                            productListGridView.Rows[n].Cells[10].Value = TextData.reference.ToString();
                            productListGridView.Rows[n].Cells[11].Value = TextData.remarks.ToString();
                            productListGridView.Rows[n].Cells[12].Value = TextData.amount.ToString();

                            GetSetData.SaveLogHistoryDetails("Add New Bank Transaction Form", "Add items in Cart...", role_id);
                            refresh();
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
                    error.errorMessage("Please select the bank!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                error.errorMessage(ex.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            add_records_Grid_view();
        }

        private bool insert_records_into_db()
        {
            try
            {
                for (int i = 0; i < productListGridView.Rows.Count; i++)
                {
                    int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", productListGridView.Rows[i].Cells[3].Value.ToString());
                    //========================================================================================================
                    int status_id_db = data.UserPermissionsIds("status_id", "pos_transaction_status", "status_title", productListGridView.Rows[i].Cells[4].Value.ToString());
                    //========================================================================================================
                    int type_id_db = data.UserPermissionsIds("transaction_id", "pos_transaction_type", "transaction_type", productListGridView.Rows[i].Cells[5].Value.ToString());
                    //========================================================================================================
                    int bank_id_db = data.UserPermissionsIds("bank_id", "pos_bank", "bank_title", productListGridView.Rows[i].Cells[6].Value.ToString());
                    //========================================================================================================
                    int branch_id_db = data.UserPermissionsIds("branch_id", "pos_bank_branch", "branch_title", productListGridView.Rows[i].Cells[7].Value.ToString());
                    //========================================================================================================
                    int account_id_db = data.UserPermissionsIds("account_id", "pos_bank_account", "account_title", productListGridView.Rows[i].Cells[8].Value.ToString());
                    //========================================================================================================
                    int accountNo_id_db = data.UserPermissionsIds("account_no_id", "pos_account_no", "account_no", productListGridView.Rows[i].Cells[9].Value.ToString());
                    //========================================================================================================


                    // Insert Data From GridView to SaleItems in Database:  ** (prod_name, barcode, manufacture_date, expiry_date, prod_state, unit, item_type, size, image_path, status, remarks, category_id, brand_id, sub_cate_id, color_id) **
                    GetSetData.query = @"insert into pos_banking_details values ('" + productListGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[2].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[12].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + status_id_db.ToString() + "' , '" + type_id_db.ToString() + "' , '" + bank_id_db.ToString() + "' , '" + branch_id_db.ToString() + "' , '" + account_id_db.ToString() + "' , '" + accountNo_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    TextData.date = "Saving cart items in database... " + "[" + (productListGridView.Rows[i].Cells[1].Value.ToString() + "  " +  productListGridView.Rows[i].Cells[2].Value.ToString()) + "]";
                    GetSetData.SaveLogHistoryDetails("Add New Bank Transaction Form", TextData.date , role_id);
                }

                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                refresh();
                clearGridView();

                return true;
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
            insert_records_into_db();
        }

        private bool update_records_db()
        {
            try
            {
                TextData.employee = txt_employee.Text;
                TextData.status = txt_status.Text;
                TextData.transation_type = txt_type.Text;
                TextData.date = txt_date.Text;
                TextData.time = txt_time.Text;
                TextData.bank_title = txt_bank_title.Text;
                TextData.branch_title = txt_branch.Text;
                TextData.account = txt_account_title.Text;
                TextData.account_no = txt_account_no.Text;
                TextData.amount = double.Parse(txt_amount.Text);
                TextData.reference = txt_reference.Text;
                TextData.remarks = txt_remarks.Text;

                if (txt_reference.Text == "")
                {
                    TextData.reference = "nill";
                }

                if (txt_remarks.Text == "")
                {
                    TextData.remarks = "nill";
                }

                if (txt_status.Text != "" || txt_status.Text != "-- Select --")
                {
                    if (txt_type.Text != "" || txt_type.Text != "-- Select --")
                    {
                        if (txt_employee.Text != "" || txt_employee.Text != "-- Select --")
                        {
                            if (txt_bank_title.Text != "" || txt_bank_title.Text != "-- Select --")
                            {
                                if (txt_branch.Text != "" || txt_branch.Text != "-- Select --")
                                {
                                    if (txt_account_title.Text != "" || txt_account_title.Text != "-- Select --")
                                    {
                                        if (txt_account_no.Text != "" || txt_account_no.Text != "-- Select --")
                                        {
                                            if (txt_amount.Text != "")
                                            {
                                                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                                                //========================================================================================================
                                                int status_id_db = data.UserPermissionsIds("status_id", "pos_transaction_status", "status_title", TextData.status);
                                                //========================================================================================================
                                                int type_id_db = data.UserPermissionsIds("transaction_id", "pos_transaction_type", "transaction_type", TextData.transation_type);
                                                //========================================================================================================
                                                int bank_id_db = data.UserPermissionsIds("bank_id", "pos_bank", "bank_title", TextData.bank_title);
                                                //========================================================================================================
                                                int branch_id_db = data.UserPermissionsIds("branch_id", "pos_bank_branch", "branch_title", TextData.branch_title);
                                                //========================================================================================================
                                                int account_id_db = data.UserPermissionsIds("account_id", "pos_bank_account", "account_title", TextData.account);
                                                //========================================================================================================
                                                int accountNo_id_db = data.UserPermissionsIds("account_no_id", "pos_account_no", "account_no", TextData.account_no);
                                                //========================================================================================================

                                                GetSetData.query = @"update pos_banking_details set date = '" + TextData.date.ToString() + "', amount = '" + TextData.amount.ToString() + "', reference = '" + TextData.reference.ToString() + "', remarks = '" + TextData.remarks.ToString() + "', status_id = '" + status_id_db.ToString() + "', t_type_id = '" + type_id_db.ToString() + "', bank_id = '" + bank_id_db.ToString() + "', branch_id  = '" + branch_id_db.ToString() + "', account_id = '" + account_id_db.ToString() + "', account_no_id = '" + accountNo_id_db.ToString() + "', employee_id = '" + employee_id_db.ToString() + "' where date = '" + TextData.dateKey.ToString() + "' and time = '" + TextData.time.ToString() +"';";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                                TextData.date = "Updating Transaction Details " + "[" + (TextData.date + "  " + TextData.time) + "]";
                                                GetSetData.SaveLogHistoryDetails("Add New Bank Transaction Form", TextData.date, role_id);

                                                done.DoneMessage("Successfully Updated!");
                                                done.ShowDialog();
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
                            error.errorMessage("Please enter employee Name!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter transaction type!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter transaction status!");
                    error.ShowDialog();
                }

                return true;
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
            update_records_db();
        }

        private void txt_employee_Enter(object sender, EventArgs e)
        {
            RefreshFieldEmployee();
        }

        private void txt_status_Enter(object sender, EventArgs e)
        {
            RefreshFieldTransactionStatus();
        }

        private void txt_type_Enter(object sender, EventArgs e)
        {
            RefreshFieldTransactionType();            

        }

        private void txt_bank_title_Enter(object sender, EventArgs e)
        {
            RefreshFieldBankTitle();
        }

        private void txt_branch_Enter(object sender, EventArgs e)
        {
            RefreshFieldBranch();
        }

        private void txt_account_title_Enter(object sender, EventArgs e)
        {
            RefreshFieldAccountTitle();
        }

        private void txt_account_no_Enter(object sender, EventArgs e)
        {
            RefreshFieldAccountNo();
        }
    }
}
