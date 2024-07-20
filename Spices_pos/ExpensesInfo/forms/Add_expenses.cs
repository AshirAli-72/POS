using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.controllers;
using Settings_info.controllers;

namespace Expenses_info.forms
{
    public partial class Add_expenses : Form
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
        public Add_expenses()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCapital);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCapitalAmount);

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
                GetSetData.addFormCopyrights(lblCopyrights);
                add_new_expense.role_id = role_id;
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_save", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_update", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("expenses_exit", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_close.Visible = bool.Parse(GetSetData.Data);

                GetSetData.Data = data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", role_id.ToString());

                if (GetSetData.Data == "TURE" || GetSetData.Data == "true" || GetSetData.Data == "True")
                {
                    lblCapital.Visible = true;
                    lblCapitalAmount.Visible = true;
                    string capitalAmount = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                    lblCapitalAmount.Text = capitalAmount;
                }
                else
                {
                    lblCapital.Visible = false;
                    lblCapitalAmount.Visible = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            date_text.Text = TextData.dates;
            time_text.Text = TextData.times;
            expense_text.Text = TextData.expense_title;
            amount_text.Text = TextData.edit_amount.ToString();
            comments_text.Text = TextData.comments;
            //net_total_text.Text = TextData.net_total.ToString();
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                add_button.Enabled = false;
                productDetailGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Expense";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Create New Expense";
            }
        }

        private void FillRefreshComboBoxValues()
        {
            GetSetData.FillComboBoxUsingProcedures(expense_text, "fillComboBoxExpenseTitle", "title");
        }

        private void refresh()
        {
            amount_text.Text = "";
            comments_text.Text = "";
            date_text.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToLongTimeString();

            system_user_permissions();
            enableSaveButton(); 
        }

        private void clearGridView()
        {
            net_total_text.Text = "0.00";
            date_text.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToLongTimeString();

            int a = productDetailGridView.Rows.Count;

            // Refresh Button Event is Generated:
            for (int i = 0; i < a; i++)
            {
                foreach (DataGridViewRow row in productDetailGridView.SelectedRows)
                {

                    productDetailGridView.Rows.Remove(row);
                }
            }
            productDetailGridView.DataSource = null;
        }

        private void Add_expenses_Load(object sender, EventArgs e)
        {
            try
            {
                //clearGridView();
                refresh();
                productDetailGridView.MouseClick += new MouseEventHandler(productListGridView_MouseClick);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void productListGridView_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                GetSetData.Ids = productDetailGridView.HitTest(e.X, e.Y).RowIndex;

                if (GetSetData.Ids >= 0)
                {
                    my_menu.Items.Add("Delete").Name = "Delete";
                }

                my_menu.Show(productDetailGridView, new Point(e.X, e.Y));

                my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_EditClicked);
            }
        }

        private void my_menu_EditClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //throw new NotImplementedException();
            deleteRowFromGridView();
        }

        private void deleteRowFromGridView()
        {
            try
            {
                GetSetData.Data = productDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString();

                TextData.net_total = double.Parse(net_total_text.Text) - double.Parse(GetSetData.Data);
                net_total_text.Text = TextData.net_total.ToString();

                GetSetData.Ids = productDetailGridView.CurrentCell.RowIndex;
                productDetailGridView.Rows.RemoveAt(GetSetData.Ids);
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "Delete")
            {
                deleteRowFromGridView();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Expenses", "Exit...", role_id);
            buttonControls.expenses_detail_buttons();
            this.Close();
        }

        private void add_category_Click(object sender, EventArgs e)
        {
            using (add_new_expense add_customer = new add_new_expense())
            {
                add_customer.ShowDialog();

                add_new_expense.title = expense_text.Text;
                TextData.expense_title = "";
                TextData.expense_title = expense_text.Text;

                FillRefreshComboBoxValues();
            }
        }

        private void refresh_fields_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
            clearGridView();
        }

        private void add_records_dataGridView()
        {
            try
            {
                TextData.dates = date_text.Text;
                TextData.times = time_text.Text;
                TextData.expense_title = expense_text.Text;
                TextData.amount = double.Parse(amount_text.Text);
                TextData.comments = comments_text.Text;
                TextData.net_total = double.Parse(net_total_text.Text);


                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                GetSetData.query = "";

                for (int i = 0; i < productDetailGridView.Rows.Count; i++)
                {
                    GetSetData.query = productDetailGridView.Rows[i].Cells[0].Value.ToString();

                    if (TextData.expense_title == GetSetData.query)
                    {
                        GetSetData.query = TextData.expense_title;
                        break;
                    }
                }

                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (TextData.expense_title != "" && TextData.expense_title != "--Select--")
                {
                    if (TextData.amount.ToString() != "")
                    {

                        string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                        double total_amount = 0;

                        TextData.net_total += TextData.amount;
                        total_amount = double.Parse(capital) - TextData.net_total;

                        if (total_amount >= 0 || GetSetData.Data == "No")
                        {
                            if (GetSetData.query != TextData.expense_title)
                            {
                                int n = productDetailGridView.Rows.Add();
                                productDetailGridView.Rows[n].Cells[0].Value = TextData.dates;
                                productDetailGridView.Rows[n].Cells[1].Value = TextData.times;
                                productDetailGridView.Rows[n].Cells[2].Value = TextData.expense_title;
                                productDetailGridView.Rows[n].Cells[3].Value = TextData.amount.ToString();
                                productDetailGridView.Rows[n].Cells[4].Value = TextData.comments;
                            }
                            else
                            {
                                GetSetData.numericValue = TextData.amount;
                                //fun_add_qty_in_seleted_cell();
                                GetSetData.Data = productDetailGridView.SelectedRows[0].Cells["Amount"].Value.ToString();

                                GetSetData.numericValue += double.Parse(GetSetData.Data);
                                productDetailGridView.SelectedRows[0].Cells["Amount"].Value = GetSetData.numericValue.ToString();
                            }

                            net_total_text.Text = TextData.net_total.ToString();
                        }
                        else
                        {
                            error.errorMessage("Sorry you do not have enough capital amount: " + capital);
                            error.ShowDialog();
                        }

                        //GetSetData.SaveLogHistoryDetails("Add New Expenses", "Adding Items in cart...", role_id);
                    }
                    else
                    {
                        error.errorMessage("Please enter amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter expense title!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            add_records_dataGridView();
            refresh();
        }

        private bool insert_record_db()
        {
            try
            {
                TextData.dates = date_text.Text;
                TextData.times = time_text.Text;
                TextData.net_total = double.Parse(net_total_text.Text);

                for (int i = 0; i < productDetailGridView.Rows.Count; i++)
                {
                    GetSetData.Ids = data.UserPermissionsIds("exp_id", "pos_expenses", "title", productDetailGridView.Rows[i].Cells[2].Value.ToString());

                    GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0') ORDER BY id DESC;";
                    string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);
                    //*************************************************


                    GetSetData.query = @"insert into pos_expense_details values ('" + productDetailGridView.Rows[i].Cells[0].Value.ToString() + "' , '" + productDetailGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + clock_in_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    GetSetData.query = @"select expense_id from pos_expense_details where date = '" + productDetailGridView.Rows[i].Cells[0].Value.ToString() + "' and time = '" + productDetailGridView.Rows[i].Cells[1].Value.ToString() + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"insert into pos_expense_items values ('" + productDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + productDetailGridView.Rows[i].Cells[4].Value.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //****************************************************************************

                    TextData.amount = double.Parse(productDetailGridView.Rows[i].Cells[3].Value.ToString());
                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                    string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                    
                    if (GetSetData.Data == "Yes")
                    {
                        if (capital != "NULL" && capital != "")
                        {
                            TextData.amount = double.Parse(capital) - TextData.amount;

                            if (TextData.amount >= 0)
                            {
                                capital = TextData.amount.ToString();
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

                    GetSetData.SaveLogHistoryDetails("Add New Expenses", "Saving [" + productDetailGridView.Rows[i].Cells[0].Value.ToString() + "  " + productDetailGridView.Rows[i].Cells[1].Value.ToString() + "] expense details", role_id);
                }

                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                return true;
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
            insert_record_db();
            refresh();
            clearGridView();
        }

        private bool update_records_db()
        {
            try
            {
                //TextData.dates = date_text.Text;
                //TextData.times = time_text.Text;
                TextData.expense_title = expense_text.Text;
                TextData.amount = double.Parse(amount_text.Text);
                TextData.comments = comments_text.Text;
                TextData.net_total = double.Parse(net_total_text.Text);


                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                GetSetData.numericValue = 0;

                if (TextData.amount < TextData.edit_amount)
                {
                    GetSetData.numericValue = TextData.net_total - TextData.amount;
                    net_total_text.Text = GetSetData.numericValue.ToString();
                }
                else if (TextData.amount > TextData.edit_amount)
                {
                    GetSetData.numericValue = TextData.net_total + TextData.amount;
                    net_total_text.Text = GetSetData.numericValue.ToString();
                }


                if (TextData.expense_title != "" && TextData.expense_title != "--Select--")
                {
                    if (TextData.amount.ToString() != "" && amount_text.Text != "")
                    {
                        GetSetData.Ids = data.UserPermissionsIds("exp_id", "pos_expenses", "title", TextData.expense_title_key);
                        GetSetData.fks = data.UserPermissionsIds("exp_id", "pos_expenses", "title", TextData.expense_title);

                        GetSetData.query = @"select expense_id from pos_expense_details where date = '" + TextData.dates.ToString() + "' and time = '" + TextData.times.ToString() + "';";
                        int expense_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        double previousPaidAmount = data.NumericValues("amount", "pos_expense_items", "expense_id", expense_acc_id_db.ToString());
                        //********************************************************************************
                        GetSetData.query = @"update pos_expense_details set date = '" + date_text.Text + "' where expense_id = '" + expense_acc_id_db.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        GetSetData.query = @"update pos_expense_items set amount = '" + TextData.amount.ToString() + "' , remarks = '" + TextData.comments.ToString() + "' where expense_id = '" + expense_acc_id_db.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //********************************************************************************
                        string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                        GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                        if (GetSetData.Data == "Yes")
                        {
                            if (capital != "NULL" && capital != "")
                            {
                                TextData.amount = ((double.Parse(capital) + previousPaidAmount) - TextData.amount);
                            }

                            if (TextData.amount >= 0)
                            {
                                GetSetData.query = "update pos_capital set total_capital = '" + TextData.amount.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                        }
                        // *****************************************************************************************

                        GetSetData.SaveLogHistoryDetails("Add New Expenses", "Updating [" + TextData.dates + "  " + TextData.times + "] details" , role_id);

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
                    error.errorMessage("Please enter expense title!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            update_records_db();
        }

        private void amount_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(amount_text.Text, e);
        }

        private void expense_text_Enter(object sender, EventArgs e)
        {
            FillRefreshComboBoxValues();
        }

        private void amount_text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.amount = 0;
                TextData.net_total = 0;
                double previousPaidAmount = 0;
                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (GetSetData.Data == "Yes")
                {
                    string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");

                    if (saveEnable == true)
                    {
                        GetSetData.query = @"select expense_id from pos_expense_details where date = '" + TextData.dates.ToString() + "' and time = '" + TextData.times.ToString() + "';";
                        int expense_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        previousPaidAmount = data.NumericValues("amount", "pos_expense_items", "expense_id", expense_acc_id_db.ToString());
                    }

                    if (amount_text.Text != "")
                    {
                        TextData.amount = double.Parse(amount_text.Text);
                    }

                    TextData.net_total = (double.Parse(capital) + previousPaidAmount) - TextData.amount;

                    if (TextData.net_total >= 0)
                    {
                        lblCapitalAmount.Text = TextData.net_total.ToString();
                    }
                    else
                    {
                        lblCapitalAmount.Text = capital;
                        //amount_text.Text = "";
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
