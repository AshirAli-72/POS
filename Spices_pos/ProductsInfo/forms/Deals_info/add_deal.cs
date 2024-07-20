using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms.RecipeDetails
{
    public partial class add_deal : Form
    {
        //int originalExStyle = -1;
        //bool enableFormLevelDoubleBuffering = true;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        if (originalExStyle == -1)
        //            originalExStyle = base.CreateParams.ExStyle;

        //        CreateParams handleParam = base.CreateParams;

        //        if (enableFormLevelDoubleBuffering)
        //        {
        //            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
        //        }
        //        else
        //        {
        //            handleParam.ExStyle = originalExStyle;
        //        }

        //        return handleParam;
        //    }
        //}

        public add_deal()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

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
                //**********************************************************************************************
                savebutton.Visible = bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id));
                update_button.Visible = bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id));

                if (bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id)) == false && bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id)) == false)
                {
                    pnl_exit.Visible = false;
                }

                pnl_save.Visible = bool.Parse(data.UserPermissions("products_exit", "pos_tbl_authorities_button_controls2", role_id));
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
                txtProductName.Text = TextData.prod_name;
                txtDealTitle.Text = TextData.dealTitle;
                txtQuantity.Text = TextData.quantity.ToString();
                txtStatus.Text = TextData.status;
                txtDescription.Text = TextData.remarks;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void createCheckBoxInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Delete";
            btn.Name = "Delete";
            btn.Text = "x";
            btn.Width = 40;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Verdana", 10F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.Red;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            salesAndReturnsGridView.Columns.Add(btn);
        }

        private void clearDataGridViewItems()
        {
            this.salesAndReturnsGridView.DataSource = null;
            this.salesAndReturnsGridView.Refresh();
            salesAndReturnsGridView.Rows.Clear();
            salesAndReturnsGridView.Columns.Clear();
        }

        private void enableSaveButton()
        {
            if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                FormNamelabel.Text = "Create New Promotion";
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Promotion";

                clearDataGridViewItems();
                GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdateDealItems", TextData.dealId.ToString());
                createCheckBoxInGridView();
                refresh();

                fillAddProductsFormTextBoxes();
            }
        }

        private void salesAndReturnsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            deleteRowFromGridView();
        }

        private void clearGridView()
        {
            //this.salesAndReturnsGridView.DataSource = null;
            //this.salesAndReturnsGridView.Refresh();
            //salesAndReturnsGridView.Rows.Clear();
            //salesAndReturnsGridView.Columns.Clear();

            try
            {
                int a = salesAndReturnsGridView.Rows.Count;

                for (int i = 0; i < a; i++)
                {
                    foreach (DataGridViewRow row in salesAndReturnsGridView.SelectedRows)
                    {

                        salesAndReturnsGridView.Rows.Remove(row);
                    }
                }

                salesAndReturnsGridView.DataSource = null;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clearDataGridView()
        {
            txtDealTitle.Text = "";
            txtDescription.Text = "";

            system_user_permissions();
            enableSaveButton();
            txtDealTitle.Select();
        }

        private void refresh()
        {
            txtProductName.Text = null;
            txt_barcode.Text = "";
            txtQuantity.Text = "0";
            txtPrice.Text = "0";
            txtStatus.SelectedIndex = 0;
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            clearGridView();
            refresh();
            clearDataGridView();
        }

        private void add_new_connection_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                clearDataGridView();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void salesAndReturnsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (salesAndReturnsGridView.Columns[e.ColumnIndex].Name == "Delete")
                {
                    deleteRowFromGridView();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            clearGridView();
            this.Close();
        }

        private void txt_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtQuantity.Text, e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPrice.Text, e);
        }

        private void fillValuesInVariablesForUse()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.prod_name = txtProductName.Text;
                TextData.dealTitle = txtDealTitle.Text;
                TextData.remarks = txtDescription.Text;
                TextData.status = txtStatus.Text;
                // ******************************************************************

                if (TextData.status == "")
                {
                    TextData.status = "nill";
                }

                if (TextData.remarks == "")
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

        private bool add_records_grid_view()
        {
            //Add New Customers Details and SaleItems list)
            try
            {
                fillValuesInVariablesForUse();
                GetSetData.Data = "";

                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                {
                    GetSetData.Data = salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString();

                    if (TextData.prod_name == GetSetData.Data)
                    {
                        GetSetData.Data = TextData.prod_name;
                        break;
                    }
                }

                if (txtProductName.Text != "")
                {
                    if (txt_barcode.Text != "")
                    {
                        if (txtDealTitle.Text != "")
                        {
                            if (txtQuantity.Text != "")
                            {
                                if (txtPrice.Text != "")
                                {
                                    TextData.quantity = double.Parse(txtQuantity.Text);
                                    TextData.dealPrice = double.Parse(txtPrice.Text);

                                    // Inserting Data in the Columns:
                                    if (GetSetData.Data != TextData.prod_name)
                                    {
                                        if (saveEnable == false)
                                        {
                                            int n = salesAndReturnsGridView.Rows.Add();
                                            salesAndReturnsGridView.Rows[n].Cells[0].Value = TextData.prod_name;
                                            salesAndReturnsGridView.Rows[n].Cells[1].Value = TextData.quantity.ToString();
                                            salesAndReturnsGridView.Rows[n].Cells[2].Value = TextData.dealPrice.ToString();
                                            salesAndReturnsGridView.Rows[n].Cells[3].Value = TextData.productId_db;
                                            salesAndReturnsGridView.Rows[n].Cells[4].Value = TextData.stock_id;
                                            //salesAndReturnsGridView.Rows[n].Cells[2].Value = TextData.remarks;
                                            //salesAndReturnsGridView.Rows[n].Cells[3].Value = TextData.status;
                                        }
                                        else if (saveEnable == true)
                                        {
                                            DataTable dt = salesAndReturnsGridView.DataSource as DataTable;
                                            DataRow row = dt.NewRow();

                                            //Populate the row with data
                                            row[0] = TextData.prod_name;
                                            row[1] = TextData.quantity.ToString();
                                            row[2] = TextData.dealPrice.ToString();
                                            row[3] = TextData.productId_db;
                                            row[4] = TextData.stock_id;
                                            //row[2] = TextData.remarks;
                                            //row[3] = TextData.status;
                                            dt.Rows.Add(row);
                                        }
                                    }

                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the item price!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the quantity!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the deal title!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter barcode!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the product name!");
                    error.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
            return false;
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            if (add_records_grid_view())
            {
                refresh();
                txtProductName.Select();
            }
        }

        private bool insert_records_into_db()
        {
            try
            {
                fillValuesInVariablesForUse();
                //========================================================================================================

                GetSetData.query = "insert into pos_deals values ('" + TextData.dealTitle + "' , '" + TextData.remarks + "' , '" + TextData.status + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================================================================

                GetSetData.query = "select deal_id from pos_deals where (deal_title = '" + TextData.dealTitle + "');";
                int dealId_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================================================================

                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                {
                    string product_id = salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString();
                    string stock_id = salesAndReturnsGridView.Rows[i].Cells[4].Value.ToString();
            
                    //========================================================================================================

                    TextData.quantity = 0;
                    TextData.dealPrice = 0;
                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString());
                    TextData.dealPrice = double.Parse(salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                    //========================================================================================================

                    if (dealId_db != 0)
                    {
                        GetSetData.query = "insert into pos_deal_items values ('" + TextData.quantity.ToString() + "' , '" + TextData.dealPrice.ToString() + "' , '" + dealId_db.ToString() + "' , '" + product_id + "', '" + stock_id + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    //GetSetData.SaveLogHistoryDetails("Add New Deal Details Form", "Saving deal items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "] details", role_id);
                }
                // *****************************************************************************************
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

            if (insert_records_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                clearGridView();
                refresh();
                clearDataGridView();
            }
        }

        private bool update_records_db()
        {
            try
            {
                fillValuesInVariablesForUse();
                //========================================================================================================

                GetSetData.query = "update pos_deals set deal_title = '" + TextData.dealTitle + "' , note = '" + TextData.remarks + "' , status = '" + TextData.status + "' where (deal_id = '" + TextData.dealId.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================================================================

                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                {
                    string product_id = salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString();
                    string stock_id = salesAndReturnsGridView.Rows[i].Cells[4].Value.ToString();

                    //========================================================================================================

                    GetSetData.query = "select prod_id from pos_deal_items where (deal_id = '" + TextData.dealId.ToString() + "') and (prod_id = '" + product_id +"');";
                    int dealItemId_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //========================================================================================================

                    TextData.quantity = 0;
                    TextData.dealPrice = 0;
                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString());
                    TextData.dealPrice = double.Parse(salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                    //========================================================================================================

                    if (dealItemId_db == 0)
                    {
                        GetSetData.query = "insert into pos_deal_items values ('" + TextData.quantity.ToString() + "' , '" + TextData.dealPrice.ToString() + "' , '" + TextData.dealId.ToString() + "' , '" + product_id + "' , '" + stock_id + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        GetSetData.query = "update pos_deal_items set  quantity = '" + TextData.quantity.ToString() + "', price = '" + TextData.dealPrice.ToString() +"'  where (deal_item_id = '" + dealItemId_db.ToString() +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    //GetSetData.SaveLogHistoryDetails("Update New Deal Details Form", "Updating deal items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "] details", role_id);
                }
                // *****************************************************************************************
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
            try
            {
                if (update_records_db())
                {
                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtProductName, "fillComboBoxProductNames", "prod_name");
        }

        private void btn_add_employee_Click(object sender, EventArgs e)
        {
            using (add_new_product main = new add_new_product())
            {
                add_new_product.role_id = role_id;
                add_new_product.saveEnable = false;
                main.ShowDialog();
            }
        }

        private void deleteRowFromGridView()
        {
            try
            {
                if (txtDealTitle.Text != "")
                {   
                    TextData.prod_name = salesAndReturnsGridView.SelectedRows[0].Cells[0].Value.ToString();
                    int product_id_db = data.UserPermissionsIds("product_id", "pos_products", "prod_name", TextData.prod_name);
                    //***********************************************************
                   
                    GetSetData.query = "select deal_id from pos_deals where (deal_title = '" + txtDealTitle.Text + "');";
                    int dealId_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids = salesAndReturnsGridView.CurrentCell.RowIndex;
                    salesAndReturnsGridView.Rows.RemoveAt(GetSetData.Ids);
                    //***********************************************************

                    if (GetSetData.Ids != 0)
                    {
                        GetSetData.query = "delete from pos_deal_items where (deal_id = '" + dealId_db.ToString() + "') and (product_id = '" + product_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                else
                {
                    error.errorMessage("Please enter the deal title!!!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                //error.errorMessage("Please select the row first!");
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void salesAndReturnsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                //{
                //    TextData.sub_total = 0;
                //    TextData.amount_due = 0;
                //    TextData.discount = 0;
                //    double quantity = 0;

                //    for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                //    {
                //        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                //        TextData.price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString());

                //        TextData.price = TextData.quantity * TextData.price;
                //        TextData.sub_total += TextData.price;
                //        salesAndReturnsGridView.Rows[i].Cells[4].Value = TextData.price.ToString();

                //        quantity += double.Parse(salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                //    }

                //    if (txt_discount.Text != "")
                //    {
                //        TextData.discount = double.Parse(txt_discount.Text);
                //    }

                //    TextData.sub_total += double.Parse(txt_connection_charges.Text);
                //    txt_sub_total.Text = TextData.sub_total.ToString();
                //    txtTotalAmount.Text = TextData.sub_total.ToString();
                //    TextData.amount_due = TextData.sub_total - TextData.discount;
                //    txt_amount_due.Text = TextData.amount_due.ToString();
                //    txt_total_quantity.Text = quantity.ToString();
                //}
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FillBarcodeTextBox();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void FillProductNameTextBox()
        {
            if (txt_barcode.Text != "")
            {
                int counter = 0;

                GetSetData.query = @"select count(prod_id) from  pos_stock_details where (item_barcode = '" + txt_barcode.Text + "');";
                counter = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (counter > 1)
                {
                    using (choose_product product = new choose_product())
                    {
                        choose_product.role_id = role_id;
                        choose_product.selectedProductName = "";
                        choose_product.selectedProductBarcode = "";
                        choose_product.selectedProductID = "";
                        choose_product.providedValueType = "barcode";
                        choose_product.providedValue = txt_barcode.Text;

                        product.ShowDialog();
                    }

                    txtProductName.Text = choose_product.selectedProductName;
                    TextData.productId_db = choose_product.selectedProductID;
                    TextData.stock_id = choose_product.selectedStockID;
                }
                else
                {
                    TextData.productId_db = data.UserPermissions("prod_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                    TextData.stock_id = data.UserPermissions("stock_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                    txtProductName.Text = data.UserPermissions("prod_name", "pos_products", "product_id", TextData.productId_db);
                }
            }
        }
        private void FillBarcodeTextBox()
        {
            if (txtProductName.Text != "")
            {
                //int counter = 0;

                //GetSetData.query = @"select count(prod_id) from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id where (prod_name = '" + prod_name_text.Text + "');";
                //counter = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //if (counter > 1)
                //{
                using (choose_product product = new choose_product())
                {
                    choose_product.role_id = role_id;
                    choose_product.selectedProductName = "";
                    choose_product.selectedProductBarcode = "";
                    choose_product.selectedProductID = "";
                    choose_product.providedValueType = "product name";
                    choose_product.providedValue = txtProductName.Text;

                    product.ShowDialog();
                }

                txt_barcode.Text = choose_product.selectedProductBarcode;
                TextData.productId_db = choose_product.selectedProductID;
                TextData.stock_id = choose_product.selectedStockID;

                //}
                //else
                //{
                //    txtPID.Text = data.UserPermissions("prod_id", "pos_products", "prod_name", prod_name_text.Text);
                //    prod_name_text.Text = data.UserPermissions("prod_name", "pos_products", "product_id", txtPID.Text);
                //TextData.stock_id = data.UserPermissions("stock_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                //}
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    FillProductNameTextBox();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
