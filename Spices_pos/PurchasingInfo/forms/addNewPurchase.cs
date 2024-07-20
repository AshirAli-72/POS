using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using Purchase_info.NewPurchaseReprots;
using Purchase_info.Purchase_return_report;
using System.Drawing.Printing;
using Supplier_Chain_info.forms;
using RefereningMaterial;
using Products_info.forms;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.controllers;

namespace Purchase_info.forms
{
    public partial class addNewPurchase : Form
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

        public addNewPurchase()
        {
            InitializeComponent();
            //setFormColorsDynamically();
            TextData.no_of_items = 0;
        }


        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        string discountValue = "";
        public static bool saveEnable;
        public static bool isReturn;

        //private void setFormColorsDynamically()
        //{
        //    //try
        //    //{
        //    //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
        //    //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
        //    //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

        //    //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
        //    //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
        //    //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

        //    //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
        //    //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
        //    //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

        //    //    //****************************************************************

        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

        //    //    //****************************************************************

        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
        //    //}
        //    //catch (Exception es)
        //    //{
        //    //    MessageBox.Show(es.Message);
        //    //}
        //}

        private void system_user_permissions()
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchases_save", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_save.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchases_print", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_save_print.Visible = bool.Parse(GetSetData.Data);

                //if (bool.Parse(customers_save_db) == false && bool.Parse(customers_update_db) == false)
                //{
                //    pnl_add_update.Visible = false;
                //}

                //***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("purchases_exit", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                //pnl_exit.Visible = bool.Parse(GetSetData.Data);


                discountValue = data.UserPermissions("discountType", "pos_general_settings");

                if (discountValue == "Yes")
                {
                    lbl_percentage.Visible = true;
                    lbl_percentage1.Visible = true;
                }
                //***************************************************************************************************

                GetSetData.Data = data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", role_id.ToString());

                if (GetSetData.Data == "TURE" || GetSetData.Data == "true" || GetSetData.Data == "True")
                {
                    lblCapital.Visible = true;
                    lblCapitalAmount.Visible = true;
                    string capitalAmount = data.UserPermissions("round(total_capital, 3)", "pos_capital");
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
            try
            {
                billNo_text.Text = TextData.send_billNo;
                invoice_no_text.Text = TextData.invoiceNo;
                txt_date.Text = TextData.dates;
                txt_supplier_name.Text = TextData.supplier;

                
                GetSetData.query = "select purchase_id from pos_purchase where (bill_no = '" + TextData.send_billNo + "') and (invoice_no = '" + TextData.invoiceNoKey.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());

                GetSetData.numericValue = data.NumericValues("no_of_items", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_no_of_items.Text = GetSetData.numericValue.ToString();

                txt_employee.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.fks.ToString());

                
                GetSetData.query = "select total_quantity from pos_purchase where purchase_id = '" + GetSetData.Ids.ToString() + "';";
                GetSetData.numericValue = data.SearchNumericValuesDb(GetSetData.query);
                txt_total_qty.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_trade_off", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_total_trade_off.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_carry_exp", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_total_carry_exp.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_total", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_sub_total.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("paid", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_paid_amount.Text = GetSetData.numericValue.ToString();

                GetSetData.numericValue = data.NumericValues("discount_percentage", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txtDiscountPercentage.Text = GetSetData.numericValue.ToString();


                GetSetData.numericValue = data.NumericValues("discount_amount", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txtDiscountAmount.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("freight", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_freight.Text = GetSetData.numericValue.ToString();
                txtFreight.Text = GetSetData.numericValue.ToString();
                

                GetSetData.numericValue = data.NumericValues("fee_amount", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txtFee.Text = GetSetData.numericValue.ToString();

                GetSetData.numericValue = data.NumericValues("credits", "pos_purchase", "purchase_id", GetSetData.Ids.ToString());
                txt_credits.Text = GetSetData.numericValue.ToString();

                FillLastCreditsTextBox();

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        private void fillPurchaseReturnsFormTextBoxes()
        {
            try
            {
                billNo_text.Text = TextData.send_billNo;
                invoice_no_text.Text = TextData.invoiceNo;
                txt_date.Text = TextData.dates;
                txt_supplier_name.Text = TextData.supplier;

                GetSetData.query = "select pur_return_id from pos_purchase_return where (bill_no = '" + TextData.send_billNo + "') and (invoice_no = '" + TextData.invoiceNoKey + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());

                GetSetData.numericValue = data.NumericValues("no_of_items", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_no_of_items.Text = GetSetData.numericValue.ToString();

                txt_employee.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.fks.ToString());

                
                GetSetData.query = "select total_quantity from pos_purchase_return where pur_return_id = '" + GetSetData.Ids.ToString() + "';";
                GetSetData.numericValue = data.SearchNumericValuesDb(GetSetData.query);
                txt_total_qty.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_trade_off", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_total_trade_off.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_carry_exp", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_total_carry_exp.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("net_total", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_sub_total.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("paid", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_paid_amount.Text = GetSetData.numericValue.ToString();

                GetSetData.numericValue = data.NumericValues("discount_percentage", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txtDiscountPercentage.Text = GetSetData.numericValue.ToString();


                GetSetData.numericValue = data.NumericValues("discount_amount", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txtDiscountAmount.Text = GetSetData.numericValue.ToString();

                
                GetSetData.numericValue = data.NumericValues("freight", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_freight.Text = GetSetData.numericValue.ToString();
                txtFreight.Text = GetSetData.numericValue.ToString();
                

                GetSetData.numericValue = data.NumericValues("fee_amount", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txtFee.Text = GetSetData.numericValue.ToString();

                GetSetData.numericValue = data.NumericValues("credits", "pos_purchase_return", "pur_return_id", GetSetData.Ids.ToString());
                txt_credits.Text = GetSetData.numericValue.ToString();

                FillLastCreditsTextBox();
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

        private void createAddButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Add";
            btn.Name = "add";
            btn.Text = "Add";
            btn.Width = 40;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 7.5F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 115, 155);
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            salesAndReturnsGridView.Columns.Add(btn);
        }

        private void createEditButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Edit";
            btn.Name = "edit";
            btn.Text = "Edit";
            btn.Width = 40;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 7.5F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
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
            if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                return_button.Visible = true;
                FormNamelabel.Text = "Create New Receiving Items";
                search_box.Visible = false;
                LoginEmployee();
                refresh();

                panel3.Size = new Size(1028, 216);
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;

                if (isReturn)
                {
                    lblStatus.Visible = false;
                    return_button.Visible = true;
                    return_button.Checked = true;

                    FormNamelabel.Text = "Update Return Received Items";

                    fillPurchaseReturnsFormTextBoxes();

                    clearDataGridViewItems();
                    GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdatePurchasingReturnItems", TextData.send_billNo);
                    createAddButtonInGridView();
                    createCheckBoxInGridView();
                }
                else
                {
                    lblStatus.Visible = false;
                    return_button.Visible = false;
                    return_button.Checked = false;

                    FormNamelabel.Text = "Update Receiving Items";
                   
                    fillAddProductsFormTextBoxes();
                    clearDataGridViewItems();
                    GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdatePurchasingItems", TextData.send_billNo);
                    createEditButtonInGridView();
                    createAddButtonInGridView();
                    createCheckBoxInGridView();
                }

                calculateTotals();
                panel3.Size = new Size(1028, 254);
            }
        }

        private void salesAndReturnsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            deleteRowFromGridView();
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

                    prod_name_text.Text = choose_product.selectedProductName;
                    txtPID.Text = choose_product.selectedProductID;
                    TextData.stock_id = choose_product.selectedStockID;
                }
                else
                {
                    txtPID.Text = data.UserPermissions("prod_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                    TextData.stock_id = data.UserPermissions("stock_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                    prod_name_text.Text = data.UserPermissions("prod_name", "pos_products", "product_id", txtPID.Text);
                }
            }
        }

        private void FillBarcodeTextBox()
        {
            if (prod_name_text.Text != "")
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
                    choose_product.providedValue = prod_name_text.Text;

                    product.ShowDialog();
                }

                txt_barcode.Text = choose_product.selectedProductBarcode;
                txtPID.Text = choose_product.selectedProductID;
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

        private void FillCategoryTextBox()
        {
            if (prod_name_text.Text != "" && txt_barcode.Text != "")
            {
                GetSetData.query = @"select category_id from pos_products where (product_id = '" + txtPID.Text + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids != 0)
                {
                    category_text.Text = data.UserPermissions("title", "pos_category", "category_id", GetSetData.Ids.ToString());
                }
                else
                {
                    refresh();
                }
            }
        }

        private void FillProductPricesTextBox()
        {
            try
            {
                if (prod_name_text.Text != "" && txt_barcode.Text != "")
                {
                    GetSetData.query = "select product_id from pos_products where (product_id = '" + txtPID.Text + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.Ids != 0)
                    {
                        string purchase_price_db = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_pur_price.Text = purchase_price_db;

                        string sale_price_db = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_sale_price.Text = sale_price_db;

                        txt_market_value.Text = data.UserPermissions("market_value", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_stock.Text = data.UserPermissions("quantity", "pos_stock_details", "stock_id", TextData.stock_id);

                        if (txt_stock.Text != "" && txt_pur_price.Text != "")
                        {
                            txt_stock_price.Text = (double.Parse(txt_stock.Text) * double.Parse(txt_pur_price.Text)).ToString();
                        }

                        string paking_db = data.UserPermissions("pkg", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_pkg.Text = paking_db;

                        //******************************************************************************************
                        string discount_db = data.UserPermissions("discount", "pos_stock_details", "stock_id", TextData.stock_id);
                        string disLimit_db = data.UserPermissions("discount_limit", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_exp_date.Text = data.UserPermissions("date_of_expiry", "pos_stock_details", "stock_id", TextData.stock_id);

                        if (discountValue == "Yes")
                        {
                            if (double.Parse(sale_price_db) > 0)
                            {
                                TextData.discount = (double.Parse(discount_db) / double.Parse(sale_price_db)) * 100;
                                TextData.disLimit = (double.Parse(disLimit_db) / double.Parse(sale_price_db)) * 100;
                            }
                            else
                            {
                                TextData.discount = (double.Parse(discount_db) / 1) * 100;
                                TextData.disLimit = (double.Parse(disLimit_db) / 1) * 100;
                            }


                            txtDiscount.Text = TextData.discount.ToString();
                            txtDiscountLimit.Text = TextData.disLimit.ToString();
                        }
                        else
                        {
                            txtDiscount.Text = discount_db;
                            txtDiscountLimit.Text = disLimit_db;
                        }
                        //******************************************************************************************

                        string tab_pieces_db = data.UserPermissions("tab_pieces", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.size = data.UserPermissions("whole_sale_price", "pos_stock_details", "stock_id", TextData.stock_id);
                        txt_prod_state.Text = data.UserPermissions("prod_state", "pos_products", "product_id", txtPID.Text);

                        GetSetData.fks = data.UserPermissionsIds("color_id", "pos_products", "product_id", txtPID.Text);
                        txt_color.Text = data.UserPermissions("title", "pos_color", "color_id", GetSetData.fks.ToString());

                        if (txt_prod_state.Text == "carton" || txt_prod_state.Text == "bag" || txt_prod_state.Text == "Tablets")
                        {
                            double total_pieces = double.Parse(paking_db) * double.Parse(tab_pieces_db);
                            double per_pack_pur_price = double.Parse(purchase_price_db) * total_pieces;
                            double per_pack_sale_price = double.Parse(sale_price_db) * total_pieces;
                            double per_pack_wholeSale = double.Parse(TextData.size) * total_pieces;

                            txt_pur_price.Text = per_pack_pur_price.ToString();
                            txt_sale_price.Text = per_pack_sale_price.ToString();
                            txt_wholeSale.Text = per_pack_wholeSale.ToString();
                        }
                        else
                        {
                            txt_pur_price.Text = purchase_price_db;
                            txt_sale_price.Text = sale_price_db;
                            txt_wholeSale.Text = TextData.size;
                        }
                    }
                }
                else
                {
                    refresh();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillLastCreditsTextBox()
        {
            if (txt_supplier_name.Text != "")
            {
                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", txt_supplier_name.Text);
                txt_last_credits.Text = data.UserPermissions("previous_payables", "pos_supplier_payables", "supplier_id", GetSetData.Ids.ToString());
            }
        }

        private void RefreshFieldProductName()
        {
            GetSetData.FillComboBoxUsingProcedures(prod_name_text, "fillComboBoxProductNames", "prod_name");
        }

        private void RefreshFieldColor()
        {

            GetSetData.FillComboBoxUsingProcedures(txt_color, "fillComboBoxColors", "title");
        }

        private void RefreshFieldCategory()
        {
            GetSetData.FillComboBoxUsingProcedures(category_text, "fillComboBoxCategory", "title");
        }

        private void RefreshFieldEmployee()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_employee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void RefreshFieldSupplier()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_supplier_name, "fillComboBoxSupplierNames", "full_name");
        }

        private void clearGridView()
        {
            this.salesAndReturnsGridView.DataSource = null;
            this.salesAndReturnsGridView.Refresh();
            salesAndReturnsGridView.Rows.Clear();
            salesAndReturnsGridView.Columns.Clear();
        }

        private void clearDataGridView()
        {
            int a = salesAndReturnsGridView.Rows.Count;

            // Refresh Button Event is Generated:
            for (int i = 0; i < a; i++)
            {
                foreach (DataGridViewRow row in salesAndReturnsGridView.SelectedRows)
                {
                    salesAndReturnsGridView.Rows.Remove(row);
                }
            }

            txt_exp_date.Text = DateTime.Now.ToLongDateString();
            txt_supplier_name.Text = "";
            txt_date.Text = DateTime.Now.ToLongDateString();
            invoice_no_text.Text = "";
            txt_remarks.Text = "";
            txt_no_of_items.Text = "0";
            TextData.no_of_items = 0;
            txt_total_trade_off.Text = "0";
            txt_total_carry_exp.Text = "0";
            txt_total_qty.Text = "0";
            txt_last_credits.Text = "0";
            txt_credits.Text = "0";
            txt_sub_total.Text = "0";
            txt_paid_amount.Text = "0";
            txt_freight.Text = "0";

            system_user_permissions();
            invoice_no_text.TabIndex = 0;
            invoice_no_text.Focus();

            salesAndReturnsGridView.DataSource = null;

            if (saveEnable == false)
            {
                if (return_button.Checked == false)
                {
                    fun_show_billno("purchase", "PUR");
                }
                else
                {
                    fun_show_billno("returns", "PR");
                }
            }

            enableSaveButton();
        }

        private void refresh()
        {
            prod_name_text.Text = null;
            txtPID.Text = "";
            txt_barcode.Text = "";
            category_text.Text = "-Select-";
            txt_wholeSale.Text = "0";
            txt_color.Text = "-Select-";
            txt_qty.Text = "1";
            txt_qty_price.Text = "0";
            txt_pkg.Text = "0";
            txt_stock.Text = "0";
            txt_stock_price.Text = "0";
            txt_market_value.Text = "0";
            txt_pur_price.Text = "";
            txt_sale_price.Text = "";
            txt_trade_off.Text = "0";
            txt_carry_exp.Text = "0";
            txt_prod_state.Text = "";
            txtDiscount.Text = "0";
            txtDiscountLimit.Text = "0";
        }

        private string auto_generate_code(string table_name, string value)
        {
            TextData.billNo = "";

            try
            {
                if (table_name == "purchase")
                {
                    GetSetData.query = @"SELECT top 1 purchaseCodes FROM pos_AllCodes order by id desc;";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids++;

                    GetSetData.query = @"update pos_AllCodes set purchaseCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else if (table_name == "returns")
                {
                    GetSetData.query = @"SELECT top 1 purchaseReturnCodes FROM pos_AllCodes order by id desc;";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids++;

                    GetSetData.query = @"update pos_AllCodes set purchaseReturnCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //**********************************************************************************************

                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.Data = value + "100000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.Data = value + "10000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.Data = value + "1000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.Data = value + "100000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.Data = value + "10000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.Data = value + "1000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "100";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "10";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "1";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                return TextData.billNo;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.billNo;
            }
        }

        private void fun_show_billno(string table_name, string value)
        {
            try
            {
                TextData.billNo = "";

                if (table_name == "purchase")
                {
                    GetSetData.query = @"SELECT top 1 purchaseCodes FROM pos_AllCodes order by id desc;";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids++;
                }
                else if (table_name == "returns")
                {
                    GetSetData.query = @"SELECT top 1 purchaseReturnCodes FROM pos_AllCodes order by id desc;";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids++;
                }
                //**********************************************************************************************

                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.Data = value + "100000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.Data = value + "10000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.Data = value + "1000000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.Data = value + "100000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.Data = value + "10000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.Data = value + "1000";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "100";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "10";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = value + "1";
                    TextData.billNo = GetSetData.Data + GetSetData.Ids.ToString();
                }

                billNo_text.Text = TextData.billNo;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void deleteRowFromGridView()
        {
            try
            {
                if (saveEnable == true)
                {
                    string purchase_price = salesAndReturnsGridView.SelectedRows[0].Cells[18].Value.ToString();
                    string quantity = salesAndReturnsGridView.SelectedRows[0].Cells[6].Value.ToString();
                    string trade_offer = salesAndReturnsGridView.SelectedRows[0].Cells[13].Value.ToString();
                    string carry_exp = salesAndReturnsGridView.SelectedRows[0].Cells[14].Value.ToString();
                    string newCostPrice = salesAndReturnsGridView.SelectedRows[0].Cells[19].Value.ToString();
                    TextData.prod_name = salesAndReturnsGridView.SelectedRows[0].Cells[0].Value.ToString();
                    TextData.barcode = salesAndReturnsGridView.SelectedRows[0].Cells[1].Value.ToString();
                    string product_id = salesAndReturnsGridView.SelectedRows[0].Cells[17].Value.ToString();
                    TextData.billNo = billNo_text.Text;
                    TextData.invoiceNo = invoice_no_text.Text;


                    double totalInventory = data.NumericValues("quantity", "pos_stock_details", "prod_id", product_id);
                    double costPrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", product_id);
                    double salePrice = data.NumericValues("sale_price", "pos_stock_details", "prod_id", product_id);

                    totalInventory -= double.Parse(quantity.ToString());

                    costPrice = (costPrice * 2) - double.Parse(newCostPrice);

                    TextData.total_pur_price = costPrice * totalInventory;
                    TextData.total_sale_price = salePrice * totalInventory;


                    GetSetData.query = @"update pos_stock_details set quantity = '" + totalInventory.ToString() + "' , pur_price = '" + costPrice.ToString() +"' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (prod_id = '" + product_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    // calculating net totals

                    GetSetData.Ids = salesAndReturnsGridView.CurrentCell.RowIndex;
                    salesAndReturnsGridView.Rows.RemoveAt(GetSetData.Ids);


                    GetSetData.query = "select purchase_id from pos_purchase where bill_no = '" + TextData.billNo + "' and invoice_no = '" + TextData.invoiceNo + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select items_id from pos_purchased_items where prod_id = '" + GetSetData.fks.ToString() + "' and purchase_id = '" + GetSetData.Ids.ToString() + "';";
                    int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (pur_item_id_db != 0)
                    {
                        GetSetData.query = "delete from pos_purchased_items where prod_id = '" + GetSetData.fks.ToString() + "' and purchase_id = '" + GetSetData.Ids.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    GetSetData.query = @"delete from pos_purchase_imei where (invoiceNo = '" + TextData.invoiceNo + "') and (prod_id = '" + GetSetData.fks.ToString() + "') and (isSold = 'False');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    // calculating net totals

                    GetSetData.Ids = salesAndReturnsGridView.CurrentCell.RowIndex;
                    salesAndReturnsGridView.Rows.RemoveAt(GetSetData.Ids);
                }
                //***************************************

                calculateTotals();
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }           
        }

        private void delete_IMEI_FromDB()
        {
            try
            {
                TextData.invoiceNo = invoice_no_text.Text;

                if (TextData.invoiceNo != "")
                {
                    GetSetData.query = @"select invoiceNo from pos_purchase_imei where (invoiceNo = '" + TextData.invoiceNo + "');";
                    string ImeiTableInvoiceNo_db = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"select invoice_no from pos_purchase where (invoice_no = '" + TextData.invoiceNo + "');";
                    string PurchasingTableInvoiceNo_db = data.SearchStringValuesFromDb(GetSetData.query);

                    if (ImeiTableInvoiceNo_db != PurchasingTableInvoiceNo_db)
                    {
                        GetSetData.query = @"delete from pos_purchase_imei where (invoiceNo = '" + TextData.invoiceNo + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
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
            //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Exit...", role_id);
            delete_IMEI_FromDB();
            clearGridView();
            Button_controls.purchase_detail_buttons();
            this.Close();
        }

        private void addNewPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                refresh();
                clearDataGridView();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void invoice_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void freight_keypress(object sender, KeyPressEventArgs e)
        {       
            data.NumericValuesOnly(txt_freight.Text, e);
        }

        private void AddNoteInGridView()
        {
            try
            {
                using (formAdd_imei itemNote = new formAdd_imei())
                {
                    TextData.invoiceNo = invoice_no_text.Text;

                    if (TextData.invoiceNo != "")
                    {
                        TextData.prod_name = salesAndReturnsGridView.SelectedRows[0].Cells[0].Value.ToString();
                        TextData.barcode = salesAndReturnsGridView.SelectedRows[0].Cells[1].Value.ToString();
                        TextData.quantity = double.Parse(salesAndReturnsGridView.SelectedRows[0].Cells[6].Value.ToString());
                        formAdd_imei.saveEnable = saveEnable;
                        itemNote.ShowDialog();
                    }
                    else
                    {
                        error.errorMessage("Please enter the invoice no!");
                        error.ShowDialog();
                    }
                }
            }
            catch (Exception es)
            {
            //    error.errorMessage("Please select the row first!");
            //    error.ShowDialog();
                MessageBox.Show(es.Message);
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
                else if (salesAndReturnsGridView.Columns[e.ColumnIndex].Name == "add")
                {
                    AddNoteInGridView();
                }
                else if (salesAndReturnsGridView.Columns[e.ColumnIndex].Name == "edit")
                {
                    editRowFromGridView();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void editRowFromGridView()
        {
            try
            {
                if (saveEnable == true)
                {
                    string purchase_price = salesAndReturnsGridView.SelectedRows[0].Cells[10].Value.ToString();
                    string salePrice = salesAndReturnsGridView.SelectedRows[0].Cells[11].Value.ToString();
                    string quantity = salesAndReturnsGridView.SelectedRows[0].Cells[6].Value.ToString();
                    string newCostPrice = salesAndReturnsGridView.SelectedRows[0].Cells[19].Value.ToString();
                    string taxation = salesAndReturnsGridView.SelectedRows[0].Cells[12].Value.ToString();
                    string tradeOff = salesAndReturnsGridView.SelectedRows[0].Cells[15].Value.ToString();
                    string shippingExpense = salesAndReturnsGridView.SelectedRows[0].Cells[16].Value.ToString();
                    TextData.prod_name = salesAndReturnsGridView.SelectedRows[0].Cells[0].Value.ToString();
                    TextData.barcode = salesAndReturnsGridView.SelectedRows[0].Cells[1].Value.ToString();
                    string product_id = salesAndReturnsGridView.SelectedRows[0].Cells[17].Value.ToString();
                    TextData.billNo = billNo_text.Text;
                    TextData.invoiceNo = invoice_no_text.Text;

                    GetSetData.query = "select purchase_id from pos_purchase where (bill_no = '" + TextData.billNo + "') and (invoice_no = '" + TextData.invoiceNo + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select items_id from pos_purchased_items where (prod_id = '" + GetSetData.fks.ToString() + "') and (purchase_id = '" + GetSetData.Ids.ToString() + "');";
                    int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    // *********************************************************

                    if (pur_item_id_db != 0)
                    {
                        double previousPurchasedInventory = data.NumericValues("quantity", "pos_purchased_items", "items_id", pur_item_id_db.ToString());

                        double totalInventory = data.NumericValues("quantity", "pos_stock_details", "prod_id", product_id);
                        double costPrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", product_id);

                        totalInventory = (totalInventory + double.Parse(quantity)) - previousPurchasedInventory;

                        costPrice = (costPrice * 2) - double.Parse(newCostPrice);

                        TextData.total_pur_price = costPrice * totalInventory;
                        TextData.total_sale_price = double.Parse(salePrice) * totalInventory;

                        // *********************************************************

                        GetSetData.query = @"update pos_stock_details set quantity = '" + totalInventory.ToString() + "' , pur_price = '" + costPrice.ToString() + "' , sale_price = '" + salePrice.ToString() + "' , market_value = '" + taxation + "' , trade_off = '" + tradeOff + "'  , carry_exp = '" + shippingExpense + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (prod_id = '" + product_id + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);


                        GetSetData.query = "update pos_purchased_items set quantity = '" + quantity + "', pur_price = '" + purchase_price + "', sale_price = '" + salePrice + "'  , trade_off = '" + tradeOff + "'  , carry_exp = '" + shippingExpense + "' , total_pur_price = '" + (Math.Round(double.Parse(newCostPrice) * double.Parse(quantity), 2)) + "', total_sale_price = '" + (Math.Round(double.Parse(salePrice) * double.Parse(quantity), 2)) + "', new_purchase_price = '" + newCostPrice +"' where (items_id = '" + pur_item_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        // *********************************************************

                        clearDataGridViewItems();
                        GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdatePurchasingItems", TextData.send_billNo);
                        createEditButtonInGridView();
                        createAddButtonInGridView();
                        createCheckBoxInGridView();

                        calculateTotals();

                        // *********************************************************

                        if (txt_paid_amount.Text != "")
                        {
                            if (txtDiscountPercentage.Text != "")
                            {
                                if (txtDiscountAmount.Text != "")
                                {
                                    if (txtFreight.Text != "")
                                    {
                                        if (txtFee.Text != "")
                                        {
                                            GetSetData.query = @"update pos_purchase set no_of_items = '" + txt_no_of_items.Text + "' , total_quantity = '" + txt_total_qty.Text + "' , net_total = '" + txt_sub_total.Text + "' , paid = '" + txt_paid_amount.Text + "' , credits = '" + txt_credits.Text + "' , freight = '" + txtFreight.Text + "' , discount_percentage = '" + txtDiscountPercentage.Text + "', discount_amount = '" + txtDiscountAmount.Text + "', fee_amount = '" + txtFee.Text + "' , net_trade_off = '" + txt_total_trade_off.Text + "' , net_carry_exp = '" + txt_total_carry_exp.Text +"' where (purchase_id = '" + GetSetData.Ids.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);

                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter the fee amount.");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the freight amount.");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the discount amount.");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the discount in percentage.");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the paid amount.");
                            error.ShowDialog();
                        }

                        done.DoneMessage("Successfully updated.");
                        done.ShowDialog();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        private void salesAndReturnsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 6 || e.ColumnIndex == 10)
            //    {
            //        TextData.sub_total = 0;
            //        double trade_off = 0;
            //        double carry_exp = 0;
            //        double quantity = 0;
            //        //TextData.no_of_items = 0;

            //        for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
            //        {
            //            MessageBox.Show(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());

            //            if (salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() != "" && salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() != "")
            //            {
            //                TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
            //                TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());

            //                TextData.prod_price = TextData.quantity * TextData.prod_price;
            //                TextData.sub_total += TextData.prod_price;

            //                //trade_off += double.Parse(salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString());
            //                //carry_exp += double.Parse(salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString());
            //                quantity += double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
            //                //TextData.no_of_items = i;

            //                txt_sub_total.Text = TextData.sub_total.ToString();
            //                txt_total_qty.Text = quantity.ToString();
            //                //txt_trade_off.Text = trade_off.ToString();
            //                //txt_carry_exp.Text = carry_exp.ToString();
            //                //txt_no_of_items.Text = TextData.no_of_items.ToString();
            //            }
            //        }
            //    }
            //}
            //catch (Exception es)
            //{
            //    //error.errorMessage(es.Message);
            //    //error.ShowDialog();
            //}
        }

        private void calculateTotals()
        {
            try
            {
                decimal totalQuantity = 0;
                decimal totalTradeOffer = 0;
                decimal totalCarryExpense = 0;
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in salesAndReturnsGridView.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // Check if the cell value is a valid decimal
                        if (decimal.TryParse(row.Cells[6].Value.ToString(), out decimal quantity))
                        {
                            totalQuantity += quantity;
                        }

                        // Check if the cell value is a valid decimal
                        if (decimal.TryParse(row.Cells[15].Value.ToString(), out decimal tradeOffer))
                        {
                            totalTradeOffer += tradeOffer;
                        }

                        // Check if the cell value is a valid decimal
                        if (decimal.TryParse(row.Cells[16].Value.ToString(), out decimal carryExpense))
                        {
                            totalCarryExpense += carryExpense;
                        }

                        // Check if the cell value is a valid decimal
                        if (decimal.TryParse(row.Cells[18].Value.ToString(), out decimal amount))
                        {
                            totalAmount += amount;
                        }
                    }
                }

                txt_total_qty.Text = Math.Round(totalQuantity, 3).ToString();
                txt_total_trade_off.Text = Math.Round(totalTradeOffer, 3).ToString();
                txt_total_carry_exp.Text = Math.Round(totalCarryExpense, 3).ToString();
                txt_sub_total.Text = Math.Round(totalAmount, 3).ToString();


                int totalRows = salesAndReturnsGridView.RowCount;

                // Display the total number of rows in the TextBox
                txt_no_of_items.Text = totalRows.ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool add_records_grid_view()
        {
            //Add New Customers Details and SaleItems list)
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.billNo = billNo_text.Text;
                TextData.invoiceNo = invoice_no_text.Text;
                TextData.supplier = txt_supplier_name.Text;
                TextData.employee = txt_employee.Text;
                TextData.prod_name = prod_name_text.Text;
                TextData.barcode = txt_barcode.Text;
                TextData.category = category_text.Text;
                TextData.exp_date = txt_exp_date.Text;
                TextData.color = txt_color.Text;
                TextData.size = txt_wholeSale.Text;
                TextData.quantity = double.Parse(txt_qty.Text);
                TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                TextData.pkg = double.Parse(txt_pkg.Text);
                TextData.market_value = double.Parse(txt_market_value.Text);
                TextData.trade_off = double.Parse(txt_trade_off.Text);
                TextData.carry_exp = double.Parse(txt_carry_exp.Text);
                TextData.discount = double.Parse(txtDiscount.Text);
                TextData.disLimit = double.Parse(txtDiscountLimit.Text);
                TextData.remarks = txt_remarks.Text;
                TextData.product_id = txtPID.Text;
                string gird_pur_price = txt_pur_price.Text;

                if (txt_pur_price.Text != "")
                {
                    TextData.prod_price = double.Parse(txt_pur_price.Text);
                }
                else
                {
                    TextData.prod_price = 0;
                }
                
                
                if (txt_sale_price.Text != "")
                {
                    TextData.sale_price = double.Parse(txt_sale_price.Text);
                }
                else
                {
                    TextData.sale_price = 0;
                }


                // ******************************************************************

                TextData.tab_pieces = data.UserPermissionsIds("tab_pieces", "pos_stock_details", "prod_id", TextData.product_id);
                int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", TextData.category);
                int color_id_db = data.UserPermissionsIds("color_id", "pos_color", "title", TextData.color);

                // ******************************************************************

                if (TextData.category == "-Select" || TextData.category == "")
                {
                    TextData.category = data.UserPermissions("title", "pos_category", "category_id", category_id_db.ToString());
                }

                if (TextData.color == "-Select" || TextData.color == "")
                {
                    TextData.color = "none";
                }

                if (TextData.employee == "-Select-" || TextData.employee == "")
                {
                    TextData.employee = "nill";
                }

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }

                if (TextData.pkg == 0)
                {
                    TextData.pkg = 1;
                }

                if (TextData.tab_pieces == 0)
                {
                    TextData.tab_pieces = 1;
                }

                //if (TextData.discount == 0)
                //{
                //    TextData.discount = 1;
                //}

                //if (TextData.disLimit == 0)
                //{
                //    TextData.disLimit = 1;
                //}


                if (discountValue == "Yes")
                {
                    TextData.discount = (TextData.sale_price * TextData.discount) / 100;
                    TextData.disLimit = (TextData.sale_price * TextData.disLimit) / 100;
                }


                string temp_name = "";
                string temp_barcode = "";
                string temp_expiry = "";
                string temp_purchase_price = "0";
                string temp_sale_price = "0";

                //for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                //{
                //    GetSetData.Data = salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString();

                //    if (TextData.prod_name == GetSetData.Data)
                //    {
                //        GetSetData.Data = TextData.prod_name;
                //        break;
                //    }
                //}

                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                {
                    temp_name = salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString();
                    temp_barcode = salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString();
                    temp_expiry = salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString();
                    temp_purchase_price = salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString();
                    temp_sale_price = salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString();

                    if ( TextData.prod_name == temp_name && TextData.barcode == temp_barcode && TextData.exp_date == temp_expiry && TextData.prod_price == double.Parse(temp_purchase_price) && TextData.sale_price == double.Parse(temp_sale_price))
                    {
                        temp_name = TextData.prod_name;
                        temp_barcode = TextData.barcode;
                        temp_expiry = TextData.exp_date;
                        temp_purchase_price = TextData.prod_price.ToString();
                        temp_sale_price = TextData.sale_price.ToString();

                        break;
                    }
                }


                if (TextData.supplier != "")
                {
                    if (TextData.invoiceNo != "")
                    {
                        if (TextData.prod_name != "")
                        {
                            if (txt_pur_price.Text != "")
                            {
                                if (txt_sale_price.Text != "")
                                {
                                    if (txtDiscount.Text != "")
                                    {
                                        if (txt_prod_state.Text == "carton" || txt_prod_state.Text == "bag" || txt_prod_state.Text == "Tablets")
                                        {
                                            //Calculating the total quantity **********************************************************
                                            TextData.full_pak = (TextData.quantity * TextData.pkg) * TextData.tab_pieces; // calculating total quantity
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity; // calculating the total purchase price
                                            TextData.trade_off = (TextData.total_pur_price * TextData.trade_off) / 100;
                                            TextData.total_pur_price = ((TextData.total_pur_price + TextData.carry_exp) - (TextData.trade_off)); // bill total purchase price
                                            TextData.prod_price = TextData.total_pur_price / TextData.full_pak; // calculating the purchase price of per piece
                                            gird_pur_price = TextData.prod_price.ToString();
                                            // **********************************************************

                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                            TextData.sale_price = TextData.total_sale_price / TextData.full_pak; // calculating the sale price of per piece

                                            TextData.totalWholeSale = double.Parse(TextData.size) * TextData.quantity;
                                            TextData.totalWholeSale = TextData.totalWholeSale / TextData.full_pak; // calculating the sale price of per piece
                                            TextData.size = TextData.totalWholeSale.ToString();
                                            TextData.discount = TextData.discount * TextData.quantity;
                                            TextData.discount = TextData.discount / TextData.full_pak;
                                            TextData.disLimit = TextData.disLimit * TextData.quantity;
                                            TextData.disLimit = TextData.disLimit / TextData.full_pak;
                                            TextData.quantity = TextData.full_pak;
                                            TextData.full_pak = double.Parse(txt_qty.Text);
                                        }
                                        else
                                        {
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity; // bill total purchase price

                                            // purchase price along with the trade offers on every items
                                            TextData.trade_off = (TextData.total_pur_price * TextData.trade_off) / 100; // calculating the trade offer of enter quantity purchase price
                                            //TextData.carry_exp = TextData.prod_price + TextData.carry_exp; // calculating the carry expenses of the products

                                            TextData.total_pur_price = ((TextData.total_pur_price + TextData.carry_exp) - (TextData.trade_off)); // bill total purchase price
                                            TextData.prod_price = TextData.total_pur_price / TextData.quantity;// purchase price per piece

                                            TextData.pkg = 1;
                                            TextData.full_pak = 0;
                                            TextData.tab_pieces = 1;
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                            //TextData.prod_price = double.Parse(txt_pur_price.Text);
                                        }


                                        //Inserting Data in the Columns **************************************************************

                                        double totalExtendedPrice = 0;

                                        totalExtendedPrice = double.Parse(gird_pur_price) * TextData.quantity;

                                        //**************************************************************

                                        if ((temp_name != TextData.prod_name) || (temp_barcode != TextData.barcode) || (temp_expiry != TextData.exp_date) || (temp_purchase_price != TextData.prod_price.ToString()) || (temp_sale_price != TextData.sale_price.ToString()))
                                        {
                                            if (saveEnable == false)
                                            {
                                                int n = salesAndReturnsGridView.Rows.Add();
                                                salesAndReturnsGridView.Rows[n].Cells[0].Value = TextData.prod_name;
                                                salesAndReturnsGridView.Rows[n].Cells[1].Value = TextData.barcode;
                                                salesAndReturnsGridView.Rows[n].Cells[2].Value = TextData.category;
                                                salesAndReturnsGridView.Rows[n].Cells[3].Value = TextData.exp_date;
                                                salesAndReturnsGridView.Rows[n].Cells[4].Value = TextData.color;
                                                salesAndReturnsGridView.Rows[n].Cells[5].Value = TextData.size;
                                                salesAndReturnsGridView.Rows[n].Cells[6].Value = TextData.quantity.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[7].Value = txt_pkg.Text;
                                                salesAndReturnsGridView.Rows[n].Cells[8].Value = TextData.tab_pieces.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[9].Value = TextData.full_pak.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[10].Value = Math.Round(double.Parse(gird_pur_price), 3);
                                                salesAndReturnsGridView.Rows[n].Cells[11].Value = Math.Round(TextData.sale_price, 3).ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[12].Value = TextData.market_value.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[13].Value = TextData.discount.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[14].Value = TextData.disLimit.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[15].Value = Math.Round(TextData.trade_off, 3).ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[16].Value = Math.Round(TextData.carry_exp, 3).ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[17].Value = TextData.product_id;
                                                salesAndReturnsGridView.Rows[n].Cells[18].Value = Math.Round(totalExtendedPrice, 3).ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[19].Value = Math.Round(double.Parse(gird_pur_price), 3).ToString();
                                            }
                                            else if (saveEnable == true)
                                            {
                                                DataTable dt = salesAndReturnsGridView.DataSource as DataTable;
                                                DataRow row = dt.NewRow();

                                                //Populate the row with data
                                                row[0] = TextData.prod_name;
                                                row[1] = TextData.barcode;
                                                row[2] = TextData.category;
                                                row[3] = TextData.exp_date;
                                                row[4] = TextData.color;
                                                row[5] = TextData.size;
                                                row[6] = TextData.quantity.ToString();
                                                row[7] = txt_pkg.Text;
                                                row[8] = TextData.tab_pieces.ToString();
                                                row[9] = TextData.full_pak.ToString();
                                                row[10] = Math.Round(double.Parse(gird_pur_price), 3).ToString();
                                                row[11] = Math.Round(TextData.sale_price, 3).ToString();
                                                row[12] = Math.Round(TextData.market_value, 3).ToString();
                                                row[13] = TextData.discount.ToString();
                                                row[14] = TextData.disLimit.ToString();
                                                row[15] = Math.Round(TextData.trade_off, 3).ToString();
                                                row[16] = Math.Round(TextData.carry_exp, 3).ToString();
                                                row[17] = TextData.product_id;
                                                row[18] = totalExtendedPrice.ToString();
                                                row[19] = Math.Round(double.Parse(gird_pur_price), 3).ToString();
                                                dt.Rows.Add(row);

                                                //TextData.sub_total = double.Parse(txt_sub_total.Text) + TextData.total_pur_price;
                                                //txt_sub_total.Text = TextData.sub_total.ToString();
                                            
                                                //TextData.quantity = double.Parse(txt_total_qty.Text) + TextData.quantity;
                                                //txt_total_qty.Text = TextData.quantity.ToString();
                                            }



                                            calculateTotals();
                                            //TextData.trade_off = double.Parse(txt_total_trade_off.Text) + TextData.trade_off;
                                            //txt_total_trade_off.Text = TextData.trade_off.ToString();

                                            //TextData.carry_exp = double.Parse(txt_total_carry_exp.Text) + TextData.carry_exp;
                                            //txt_total_carry_exp.Text = TextData.carry_exp.ToString();

                                            //TextData.no_of_items++;
                                        }

                                        // calculating net totals
                                        //txt_no_of_items.Text = TextData.no_of_items.ToString();
                                        //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Adding items [" + TextData.prod_name + "  " + TextData.barcode + "] in cart", role_id);
                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter Discount!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter sale price!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter purchase price!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter product name!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter invoice No!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter company name!");
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
                txt_barcode.Select();
            }
        }

        private void prod_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FillBarcodeTextBox();
                    FillCategoryTextBox();
                    FillProductPricesTextBox();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    FillProductNameTextBox();
                    FillCategoryTextBox();
                    FillProductPricesTextBox();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            try
            {
                clearDataGridView();
                refresh();
                return_button.Checked = false;
                invoice_no_text.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_records_into_db()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.billNo = billNo_text.Text;
                TextData.invoiceNo = invoice_no_text.Text;
                TextData.dates = txt_date.Text;
                TextData.supplier = txt_supplier_name.Text;
                TextData.employee = txt_employee.Text;
                TextData.quantity = double.Parse(txt_total_qty.Text);
                TextData.trade_off = double.Parse(txt_total_trade_off.Text);
                TextData.carry_exp = double.Parse(txt_total_carry_exp.Text);
                TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                TextData.sub_total = double.Parse(txt_sub_total.Text);
                TextData.lastCredits = double.Parse(txt_last_credits.Text);
                TextData.credits = double.Parse(txt_credits.Text);
               

                if (TextData.employee == "-Select-" || TextData.employee == "")
                {
                    TextData.employee = "nill";
                }

                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                GetSetData.Permission = data.UserPermissions("bill_no", "pos_purchase", "bill_no", TextData.billNo);
                GetSetData.Data = data.UserPermissions("invoice_no", "pos_purchase", "invoice_no", TextData.invoiceNo.ToString());

                string capital = data.UserPermissions("round(total_capital, 3)", "pos_capital");

                if (capital == "" || capital == "NULL")
                {
                    capital = "0";
                }

                if (txtAmountDue.Text != "" && double.Parse(txtAmountDue.Text) > 0)
                {
                    if (GetSetData.Permission == "" && GetSetData.Permission != TextData.billNo)
                    {
                        if (GetSetData.Data == "" && GetSetData.Data != TextData.invoiceNo)
                        {
                            if (TextData.supplier != "")
                            {
                                if (txt_paid_amount.Text != "")
                                {
                                    if (txtDiscountPercentage.Text != "")
                                    {
                                        if (txtDiscountAmount.Text != "")
                                        {
                                            if (txt_freight.Text != "")
                                            {
                                                if (txtFee.Text != "")
                                                {
                                                    TextData.paid = double.Parse(txt_paid_amount.Text);
                                                    TextData.freight = double.Parse(txtFreight.Text);
                                                    TextData.discountPercentage = double.Parse(txtDiscountPercentage.Text);
                                                    TextData.discount = double.Parse(txtDiscountAmount.Text);
                                                    TextData.fee = double.Parse(txtFee.Text);


                                                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                                    if (TextData.paid <= double.Parse(capital) || GetSetData.Data == "No")
                                                    {
                                                        GetSetData.query = "insert into pos_purchase values ('" + TextData.dates.ToString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "' , '" + TextData.discountPercentage.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.fee.ToString() + "', '0');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================

                                                        GetSetData.query = "select purchase_id from pos_purchase where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                                                        int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                        //========================================================================================================

                                                        for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                                        {
                                                            string product_id_db = salesAndReturnsGridView.Rows[i].Cells[17].Value.ToString();
                                                            //GetSetData.query = "select product_id from pos_products where prod_name = '" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "' and barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "';";
                                                            //int product_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                            //========================================================================================================
                                                            int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                                                            //========================================================================================================

                                                            // Calculating total purchase and sale price for purchased items
                                                            TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                            TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString());
                                                            TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());

                                                            //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                            TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                            //========================================================================================================
                                                            if (purchase_id_db != 0)
                                                            {
                                                                //Calculating Average Cost of product*********************************************************

                                                                string allowAverageCost = data.UserPermissions("allowAverageCost", "pos_general_settings");

                                                                double averageCost = 0;


                                                                if (allowAverageCost == "Yes")
                                                                {
                                                                    double previousPurchasePrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", TextData.product_id);

                                                                    if (previousPurchasePrice > 0)
                                                                    {
                                                                        averageCost = (previousPurchasePrice + double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString())) / 2;
                                                                    }
                                                                    else
                                                                    {
                                                                        averageCost = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                                    }
                                                                }

                                                                //*********************************************************

                                                                GetSetData.query = "insert into pos_purchased_items values ('" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + averageCost.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + product_id_db.ToString() + "' , '" + purchase_id_db.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "');";
                                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                //========================================================================================================

                                                                GetSetData.query = "select items_id from pos_purchased_items where purchase_id = '" + purchase_id_db.ToString() + "' and (prod_id = '" + product_id_db.ToString() + "');";
                                                                int purchase_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                //========================================================================================================

                                                                if (purchase_item_id_db != 0)
                                                                {
                                                                    GetSetData.query = "update pos_products set size = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , expiry_date = '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , category_id = '" + category_id_db.ToString() + "' where (product_id = '" + product_id_db + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                    //========================================================================================================


                                                                    GetSetData.query = @"select strictly_check_expiry from pos_general_settings;";
                                                                    string strictly_check_expiry_db = data.SearchStringValuesFromDb(GetSetData.query);

                                                                    GetSetData.query = @"select split_old_and_new_stock from pos_general_settings;";
                                                                    string split_old_and_new_stock_db = data.SearchStringValuesFromDb(GetSetData.query);

                                                                    int is_stock_exists = 0;
                                                                    int is_stock_exists_with_zero_quantity = 0;

                                                                    if (split_old_and_new_stock_db == "Yes")
                                                                    {
                                                                        if (strictly_check_expiry_db == "Yes")
                                                                        {
                                                                            GetSetData.query = "select stock_id from pos_stock_details where (item_barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "') and (date_of_expiry = '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "') and (pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "') and (sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                                            is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                        }
                                                                        else
                                                                        {
                                                                            GetSetData.query = "select stock_id from pos_stock_details where (item_barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "') and (pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "') and (sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                                            is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                        }

                                                                        GetSetData.query = "select top 1 stock_id from pos_stock_details where (prod_id = '" + product_id_db.ToString() + "') and ((quantity <= '0'));";
                                                                        is_stock_exists_with_zero_quantity = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                    }
                                                                    else
                                                                    {
                                                                        GetSetData.query = "select stock_id from pos_stock_details where (prod_id = '" + product_id_db.ToString() + "');";
                                                                        is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                    }



                                                                    if (is_stock_exists != 0)
                                                                    {
                                                                        TextData.quantity = data.NumericValues("quantity", "pos_stock_details", "stock_id", is_stock_exists.ToString());

                                                                        TextData.quantity = TextData.quantity + double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                                        TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                        TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                                        //========================================================================================================  

                                                                        //**************************************************

                                                                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'New Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                        //**************************************************

                                                                        GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + TextData.full_pak.ToString() + "' , pur_price = '" + averageCost.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , whole_sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , market_value = '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "',  discount = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "', discount_limit = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "', trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (stock_id = '" + is_stock_exists.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                    }
                                                                    else if (is_stock_exists_with_zero_quantity != 0)
                                                                    {
                                                                        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                                        TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                        TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;

                                                                        //========================================================================================================  

                                                                        //**************************************************

                                                                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'New Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                        //**************************************************


                                                                        GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + TextData.full_pak.ToString() + "' , pur_price = '" + averageCost.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , whole_sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , market_value = '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "',  discount = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "', discount_limit = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "', trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (stock_id = '" + is_stock_exists_with_zero_quantity.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                    }
                                                                    else
                                                                    {
                                                                        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                                        TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                        TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                                        //========================================================================================================  
                                                                        string quantity_alert = data.UserPermissions("qty_alert", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string quantity_alert_status = data.UserPermissions("alert_status", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        //========================================================================================================  


                                                                        //**************************************************

                                                                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                        string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'New Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                        //**************************************************


                                                                        // Insert Data From GridView to pos_stock in Database:
                                                                        GetSetData.query = @"insert into pos_stock_details values ('" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "', '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + quantity_alert + "' , '" + quantity_alert_status + "' , '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                    }

                                                                    //========================================================================================================                              
                                                                }
                                                            }

                                                            //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Saving purchasing items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "] details", role_id);
                                                        }

                                                        GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                                                        GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                                        //**********************************************************************
                                                        TextData.lastCredits = 0;
                                                        GetSetData.query = @"select previous_payables from pos_supplier_payables where (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                                                        TextData.lastCredits = data.SearchNumericValuesDb(GetSetData.query);

                                                        TextData.credits = double.Parse(txt_credits.Text);
                                                        TextData.lastCredits = TextData.lastCredits + TextData.credits;

                                                        GetSetData.query = @"Update pos_supplier_payables set previous_payables = '" + TextData.lastCredits.ToString() + "' , due_days = '" + TextData.dates.ToString() + "' where (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================   


                                                        // Calculating Debits and Credits
                                                        TextData.credits = double.Parse(txt_sub_total.Text) - TextData.paid;

                                                        if (TextData.credits < 0)
                                                        {
                                                            TextData.credits = TextData.credits * (-1);
                                                        }

                                                        GetSetData.query = "insert into pos_company_transactions values ('" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , 'Purchased' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================  

                                                        // *****************************************************************************************
                                                        if (GetSetData.Data == "Yes")
                                                        {
                                                            if (capital != "NULL" && capital != "")
                                                            {
                                                                TextData.paid = double.Parse(capital) - TextData.paid;
                                                                capital = TextData.paid.ToString();
                                                            }

                                                            if (double.Parse(capital) >= 0)
                                                            {
                                                                GetSetData.query = "update pos_capital set total_capital = '" + capital.ToString() + "';";
                                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                                            }
                                                        }
                                                        // *****************************************************************************************

                                                        // Generating BillNo 
                                                        auto_generate_code("purchase", "PUR");

                                                        TextData.return_value = true;
                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        error.errorMessage("Available Capital is '" + capital.ToString() + "'!");
                                                        error.ShowDialog();
                                                    }
                                                }
                                                else
                                                {
                                                    error.errorMessage("Please enter the fee amount!");
                                                    error.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                error.errorMessage("Please enter the Freight Amount!");
                                                error.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter the discount amount!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the discount percentage!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the Paid Amount!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please select the Supplier!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("'" + GetSetData.Data.ToString() + "' is already exist!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("'" + GetSetData.Permission.ToString() + "' is already exist!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("No items added in cart yet. Please make sure to add items in cart before saving the invoice!");
                    error.ShowDialog();
                }

                return false;
            }

            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                TextData.return_value = false;
                return false;
            }
        }

        private bool insert_returned_items_into_db()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.billNo = billNo_text.Text;
                TextData.invoiceNo = invoice_no_text.Text;
                TextData.dates = txt_date.Text;
                TextData.supplier = txt_supplier_name.Text;
                TextData.employee = txt_employee.Text;
                TextData.quantity = double.Parse(txt_total_qty.Text);
                TextData.trade_off = double.Parse(txt_total_trade_off.Text);
                TextData.carry_exp = double.Parse(txt_total_carry_exp.Text);
                TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                TextData.sub_total = double.Parse(txt_sub_total.Text);
                TextData.lastCredits = double.Parse(txt_last_credits.Text);
                TextData.credits = double.Parse(txt_credits.Text);


                if (TextData.employee == "-Select-" || TextData.employee == "")
                {
                    TextData.employee = "nill";
                }

                // **************************************************************
                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                GetSetData.Permission = data.UserPermissions("bill_no", "pos_purchase_return", "bill_no", TextData.billNo);
                GetSetData.Data = data.UserPermissions("invoice_no", "pos_purchase_return", "invoice_no", TextData.invoiceNo);
                // **************************************************************
                string capital = data.UserPermissions("round(total_capital, 3)", "pos_capital");

                if (txtAmountDue.Text != "" && double.Parse(txtAmountDue.Text) > 0)
                {
                    if (GetSetData.Permission == "")
                    {
                        if (GetSetData.Data == "")
                        {
                            if (TextData.supplier != "")
                            {
                                if (txt_paid_amount.Text != "")
                                {
                                    if (txtDiscountPercentage.Text != "")
                                    {
                                        if (txtDiscountAmount.Text != "")
                                        {
                                            if (txt_freight.Text != "")
                                            {
                                                if (txtFee.Text != "")
                                                {
                                                    TextData.paid = double.Parse(txt_paid_amount.Text);
                                                    TextData.freight = double.Parse(txtFreight.Text);
                                                    TextData.discountPercentage = double.Parse(txtDiscountPercentage.Text);
                                                    TextData.discount = double.Parse(txtDiscountAmount.Text);
                                                    TextData.fee = double.Parse(txtFee.Text);


                                                    GetSetData.query = @"insert into pos_purchase_return values ('" + TextData.dates.ToString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "' , '" + TextData.discountPercentage.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.fee.ToString() + "', '0');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    //========================================================================================================

                                                    GetSetData.query = @"select pur_return_id from pos_purchase_return where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                                                    int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                    //========================================================================================================

                                                    for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                                    {
                                                        string product_id_db = salesAndReturnsGridView.Rows[i].Cells[17].Value.ToString();
                                                        //========================================================================================================
                                                        int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());
                                                        //========================================================================================================

                                                        // Calculating total purchase and sale price for purchased items
                                                        TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                        TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString());
                                                        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());

                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                        //========================================================================================================

                                                        if (purchase_id_db != 0)
                                                        {
                                                            //Calculating Average Cost of product*********************************************************

                                                            string allowAverageCost = data.UserPermissions("allowAverageCost", "pos_general_settings");

                                                            double averageCost = 0;


                                                            if (allowAverageCost == "Yes")
                                                            {
                                                                double previousPurchasePrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", TextData.product_id);

                                                                if (previousPurchasePrice > 0)
                                                                {
                                                                    averageCost = (previousPurchasePrice + double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString())) / 2;
                                                                }
                                                                else
                                                                {
                                                                    averageCost = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                                }
                                                            }

                                                            //*********************************************************
                                                            GetSetData.query = @"insert into pos_pur_return_items values ('" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + averageCost.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + product_id_db.ToString() + "' , '" + purchase_id_db.ToString() + "', '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "');";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                            //========================================================================================================

                                                            TextData.quantity = data.NumericValues("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                            //========================================================================================================


                                                            TextData.quantity = TextData.quantity - double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                            TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                            TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                            //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                            TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                            //========================================================================================================

                                                            GetSetData.query = "select items_id from pos_pur_return_items where purchase_id = '" + purchase_id_db.ToString() + "' and prod_id = '" + product_id_db.ToString() + "';";
                                                            int purchase_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                            //========================================================================================================

                                                            if (purchase_item_id_db != 0)
                                                            {
                                                                string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + TextData.quantity.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'Return Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                data.insertUpdateCreateOrDelete(GetSetData.query);


                                                                GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , full_pak = '" + TextData.full_pak.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "', discount = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , discount_limit = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' where prod_id = '" + product_id_db.ToString() + "';";
                                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                //========================================================================================================
                                                            }
                                                        }

                                                        TextData.lastCredits = data.NumericValues("previous_payables", "pos_supplier_payables", "supplier_id", GetSetData.Ids.ToString());
                                                        //========================================================================================================

                                                        GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                                                        GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                                        TextData.lastCredits = TextData.lastCredits - TextData.credits;

                                                        GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.credits.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================
                                                    }

                                                    // Calculating Debits and Credits
                                                    TextData.credits = double.Parse(txt_sub_total.Text) - TextData.paid;

                                                    if (TextData.credits < 0)
                                                    {
                                                        TextData.credits = (TextData.credits * (-1));
                                                    }


                                                    GetSetData.query = @"insert into pos_company_transactions values ('" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '0' , '" + TextData.paid.ToString() + "' , '0' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , '" + "Purchase Returned" + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    //========================================================================================================

                                                    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                                    if (GetSetData.Data == "Yes")
                                                    {
                                                        if (capital != "NULL" && capital != "")
                                                        {
                                                            TextData.paid = double.Parse(capital) + TextData.paid;

                                                            if (TextData.paid >= 0)
                                                            {
                                                                capital = TextData.paid.ToString();
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

                                                    // Generating BillNo ======================================================
                                                    auto_generate_code("returns", "PR");
                                                    TextData.return_value = true;
                                                    return true;

                                                }
                                                else
                                                {
                                                    error.errorMessage("Please enter the fee amount!");
                                                    error.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                error.errorMessage("Please enter the Freight Amount!");
                                                error.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter the discount amount!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the discount percentage!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the Paid Amount!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please select the Supplier!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("'" + GetSetData.Data.ToString() + "' is already exist!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("'" + GetSetData.Permission.ToString() + "' is already exist!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("No items added in cart yet. Please make sure to add items in cart before returning the invoice!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                TextData.return_value = false;
                return false;
            }
        }

        private bool update_records_db()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.send_billNo = billNo_text.Text;
                TextData.invoiceNo = invoice_no_text.Text;
                TextData.dates = txt_date.Text;
                TextData.supplier = txt_supplier_name.Text;
                TextData.employee = txt_employee.Text;
                TextData.quantity = double.Parse(txt_total_qty.Text);
                TextData.trade_off = double.Parse(txt_total_trade_off.Text);
                TextData.carry_exp = double.Parse(txt_total_carry_exp.Text);
                TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                TextData.sub_total = double.Parse(txt_sub_total.Text);
                TextData.lastCredits = double.Parse(txt_last_credits.Text);
                TextData.credits = double.Parse(txt_credits.Text);


                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                
                GetSetData.query = @"select purchase_id from pos_purchase where bill_no = '" + TextData.send_billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNoKey.ToString() + "';";
                int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================================================================


                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                double previousPaidAmount = data.NumericValues("paid", "pos_purchase", "purchase_id", purchase_id_db.ToString());
                string capital = data.UserPermissions("round(total_capital, 3)", "pos_capital");

                if (capital == "" || capital == "NULL")
                {
                    capital = "0";
                }

                if (txtAmountDue.Text != "" && double.Parse(txtAmountDue.Text) > 0)
                {
                    if (invoice_no_text.Text != "")
                    {
                        if (TextData.supplier != "")
                        {
                            if (txt_paid_amount.Text != "")
                            {
                                if (txtDiscountPercentage.Text != "")
                                {
                                    if (txtDiscountAmount.Text != "")
                                    {
                                        if (txt_freight.Text != "")
                                        {
                                            if (txtFee.Text != "")
                                            {
                                                TextData.paid = double.Parse(txt_paid_amount.Text);
                                                TextData.freight = double.Parse(txtFreight.Text);
                                                TextData.discountPercentage = double.Parse(txtDiscountPercentage.Text);
                                                TextData.discount = double.Parse(txtDiscountAmount.Text);
                                                TextData.fee = double.Parse(txtFee.Text);

                                                if (TextData.paid <= (double.Parse(capital) + previousPaidAmount) || GetSetData.Data == "No")
                                                {
                                                    GetSetData.query = @"select credits from pos_purchase where (bill_no = '" + TextData.billNo.ToString() + "') and (invoice_no = '" + TextData.invoiceNoKey.ToString() + "');";
                                                    double previousCredits = data.SearchNumericValuesDb(GetSetData.query);


                                                    GetSetData.query = @"update pos_purchase set date = '" + TextData.dates.ToString() + "' ,  invoice_no = '" + TextData.invoiceNo.ToString() + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_quantity = '" + TextData.quantity.ToString() + "' ,  net_trade_off = '" + TextData.trade_off.ToString() + "', net_carry_exp = '" + TextData.carry_exp.ToString() + "', net_total = '" + TextData.sub_total.ToString() + "' , paid = '" + txt_paid_amount.Text + "' , credits = '" + TextData.credits.ToString() + "' , freight = '" + TextData.freight.ToString() + "' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "', discount_percentage = '" + TextData.discountPercentage.ToString() + "', discount_amount = '" + TextData.discount.ToString() + "', fee_amount = '" + TextData.fee.ToString() + "' where (bill_no = '" + TextData.send_billNo + "');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    
                                                
                                                    //========================================================================================================

                                                    for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                                    {
                                                        double pur_quantity_db = 0;

                                                        string product_id_db = salesAndReturnsGridView.Rows[i].Cells[17].Value.ToString();


                                                        //*********************************************************

                                                        GetSetData.query = @"select items_id from pos_purchased_items where (prod_id = '" + product_id_db.ToString() + "') and (purchase_id = '" + purchase_id_db.ToString() + "');";
                                                        int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                        //========================================================================================================

                                                        GetSetData.query = @"select quantity from pos_purchased_items where (purchase_id = '" + purchase_id_db.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                        pur_quantity_db = data.SearchNumericValuesDb(GetSetData.query);
                                                        //========================================================================================================

                                                     
                                                        TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                        TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                        //========================================================================================================

                                                        //if (pur_quantity_db != double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString()))
                                                        //{   //if (chkUpdationOption.Checked == true)
                                                            //{
                                                                GetSetData.query = @"select strictly_check_expiry from pos_general_settings;";
                                                                string strictly_check_expiry_db = data.SearchStringValuesFromDb(GetSetData.query);

                                                                GetSetData.query = @"select split_old_and_new_stock from pos_general_settings;";
                                                                string split_old_and_new_stock_db = data.SearchStringValuesFromDb(GetSetData.query);


                                                                int is_stock_exists = 0;
                                                                int is_stock_exists_with_zero_quantity = 0;

                                                                if (split_old_and_new_stock_db == "Yes")
                                                                {
                                                                    if (strictly_check_expiry_db == "Yes")
                                                                    {
                                                                        GetSetData.query = "select stock_id from pos_stock_details where (item_barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "') and (date_of_expiry = '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "') and (pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "') and (sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                                        is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                    }
                                                                    else
                                                                    {
                                                                        GetSetData.query = "select stock_id from pos_stock_details where (item_barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "') and (pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "') and (sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                                        is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                    }


                                                                    GetSetData.query = "select top 1 stock_id from pos_stock_details where (prod_id = '" + product_id_db.ToString() + "') and ((quantity <= '0'));";
                                                                    is_stock_exists_with_zero_quantity = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                }
                                                                else
                                                                {
                                                                    GetSetData.query = "select stock_id from pos_stock_details where (prod_id = '" + product_id_db.ToString() + "');";
                                                                    is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                }


                                                            //Calculating Average Cost of product*********************************************************

                                                            string allowAverageCost = data.UserPermissions("allowAverageCost", "pos_general_settings");

                                                            double averageCost = 0;


                                                        if (allowAverageCost == "Yes")
                                                        {
                                                            double previousPurchasePrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", product_id_db);

                                                            GetSetData.query = "select pur_price from pos_purchased_items where (prod_id = '" + product_id_db.ToString() + "') and (purchase_id = '" + purchase_id_db.ToString() + "');";
                                                            double previousCostPrice = data.SearchNumericValuesDb(GetSetData.query);

                                                            if (previousPurchasePrice > 0 && previousCostPrice != double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString()))
                                                            {
                                                                averageCost = (previousPurchasePrice + double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString())) / 2;
                                                            }
                                                            else
                                                            {
                                                                averageCost = previousPurchasePrice;
                                                            }
                                                        }

                                                            if (is_stock_exists != 0)
                                                                {
                                                                    GetSetData.query = "select quantity from pos_purchased_items where (prod_id = '" + product_id_db.ToString() + "') and (purchase_id = '" + purchase_id_db.ToString() + "');";
                                                                    int previousPurchasedQuantity = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                                    
                                                                    TextData.quantity = data.NumericValues("quantity", "pos_stock_details", "stock_id", is_stock_exists.ToString());

                                                                    TextData.quantity = (TextData.quantity + double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString())) - previousPurchasedQuantity;
                                                                    TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                    TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                    //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                    TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                                    //========================================================================================================  

                                                                    //*********************************************************

                                                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'Update Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                    //*********************************************************


                                                                    GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + TextData.full_pak.ToString() + "' , pur_price = '" + averageCost.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , whole_sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , market_value = '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "',  discount = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "', discount_limit = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "', trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (stock_id = '" + is_stock_exists.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                }
                                                                else if (is_stock_exists_with_zero_quantity != 0)
                                                                {
                                                                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                                    TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                    TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                    //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                    TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                                    //========================================================================================================  


                                                                    //*********************************************************

                                                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'Update Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                    //*********************************************************


                                                                    GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + TextData.full_pak.ToString() + "' , pur_price = '" + averageCost.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , whole_sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , market_value = '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "',  discount = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "', discount_limit = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "', trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (stock_id = '" + is_stock_exists_with_zero_quantity.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                }
                                                                else
                                                                {
                                                                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                                    TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                                    TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                                    //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                                    TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                                    //========================================================================================================  
                                                                    string quantity_alert = data.UserPermissions("qty_alert", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string quantity_alert_status = data.UserPermissions("alert_status", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    //========================================================================================================  

                                                                    //*********************************************************

                                                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'Update Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                                                    //*********************************************************


                                                                    // Insert Data From GridView to pos_stock in Database:
                                                                    GetSetData.query = @"insert into pos_stock_details values ('" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "', '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[12].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[15].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[16].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + quantity_alert + "' , '" + quantity_alert_status + "' , '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                                }
                                                            //}
                                                        //}
                                                        // *********************************************************************                             

                                                        int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", salesAndReturnsGridView.Rows[i].Cells[2].Value.ToString());

                                                        //========================================================================================================

                                                        GetSetData.query = @"update pos_products set size = '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , expiry_date = '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , category_id = '" + category_id_db.ToString() + "' where (product_id = '" + product_id_db.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================

                                                        // Calculating total purchase and sale price for purchased items
                                                        TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                        TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString());
                                                        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());

                                                        //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                        TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                        //========================================================================================================

                                                        if (pur_item_id_db == 0)
                                                        {
                                                            GetSetData.query = @"insert into pos_purchased_items values ('" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + product_id_db.ToString() + "' , '" + purchase_id_db.ToString() + "', '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "');";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        }
                                                        else
                                                        {
                                                            GetSetData.query = @"update pos_purchased_items set quantity = '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "', new_purchase_price = '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "' where prod_id = '" + product_id_db.ToString() + "' and purchase_id = '" + purchase_id_db.ToString() + "';";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        }
                                                        //========================================================================================================



                                                        //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Updating purchasing items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "]", role_id);
                                                    }

                                                    GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                                                    GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                                    // Calculating Debits and Credits
                                                    TextData.credits = (double.Parse(txt_sub_total.Text) + double.Parse(txt_last_credits.Text)) - TextData.paid;

                                                    if (TextData.credits < 0)
                                                    {
                                                        TextData.credits = TextData.credits * (-1);
                                                    }

                                                    GetSetData.query = @"select id from pos_company_transactions where (bill_no = '" + TextData.send_billNo.ToString() + "') and (invoice_no = '" + TextData.invoiceNo.ToString() + "');";
                                                    int transaction_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                                    if (transaction_id_db == 0)
                                                    {
                                                        GetSetData.query = @"insert into pos_company_transactions values ('" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , 'Purchased' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //========================================================================================================  
                                                    }
                                                    else
                                                    {
                                                        GetSetData.query = @"update pos_company_transactions set date = '" + TextData.dates.ToString() + "' , bill_no = '" + TextData.billNo.ToString() + "' , invoice_no = '" + TextData.invoiceNo.ToString() + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_quantity = '" + TextData.quantity.ToString() + "' , net_trade_off = '" + TextData.trade_off.ToString() + "' , net_carry_exp = '" + TextData.carry_exp.ToString() + "' , net_total = '" + TextData.sub_total.ToString() + "' , paid = '" + TextData.credits.ToString() + "' , credits = '" + TextData.paid.ToString() + "' , pCredits = '" + TextData.lastCredits.ToString() + "' , freight = '" + TextData.freight.ToString() + "' , status = 'Purchased' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where id = '" + transaction_id_db.ToString() + "';";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        //======================================================================================================== 
                                                    }

                                                    TextData.lastCredits = 0;
                                                    GetSetData.query = @"select previous_payables from pos_supplier_payables where (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                                                    TextData.lastCredits = data.SearchNumericValuesDb(GetSetData.query);

                                                    TextData.credits = double.Parse(txt_credits.Text);
                                                    TextData.lastCredits = (TextData.lastCredits + TextData.credits) - previousCredits;

                                                    if (TextData.lastCredits < 0)
                                                    {
                                                        TextData.lastCredits = 0;
                                                    }

                                                    GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.lastCredits.ToString() + "' , due_days = '" + TextData.dates.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    //}
                                                    //========================================================================================================   

                                                    if (GetSetData.Data == "Yes")
                                                    {
                                                        if (capital != "NULL" && capital != "")
                                                        {
                                                            TextData.paid = ((double.Parse(capital) + previousPaidAmount) - TextData.paid);
                                                        }

                                                        if (TextData.paid >= 0)
                                                        {
                                                            GetSetData.query = "update pos_capital set total_capital = '" + TextData.paid.ToString() + "';";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        }
                                                    }
                                                    // *****************************************************************************************

                                                    TextData.return_value = true;
                                                    return true;
                                                }
                                                else
                                                {
                                                    error.errorMessage("Available Capital is '" + capital.ToString() + "'!");
                                                    error.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                error.errorMessage("Please enter the fee amount!");
                                                error.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter the Freight Amount!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the discount amount!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the discount percentage!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the Paid Amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the Supplier!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the Invoice number!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("No items added in cart yet. Please make sure to add items in cart before updating the invoice!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
                TextData.return_value = false;
            }
        }
        private bool update_purchase_returns_records_db()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.send_billNo = billNo_text.Text;
                TextData.invoiceNo = invoice_no_text.Text;
                TextData.dates = txt_date.Text;
                TextData.supplier = txt_supplier_name.Text;
                TextData.employee = txt_employee.Text;
                TextData.quantity = double.Parse(txt_total_qty.Text);
                TextData.trade_off = double.Parse(txt_total_trade_off.Text);
                TextData.carry_exp = double.Parse(txt_total_carry_exp.Text);
                TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                TextData.sub_total = double.Parse(txt_sub_total.Text);
                TextData.lastCredits = double.Parse(txt_last_credits.Text);
                TextData.credits = double.Parse(txt_credits.Text);


                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                GetSetData.query = @"select pur_return_id from pos_purchase_return where (bill_no = '" + TextData.send_billNo.ToString() + "') and (invoice_no = '" + TextData.invoiceNoKey.ToString() + "');";
                int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================================================================
               

                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                double previousPaidAmount = data.NumericValues("paid", "pos_purchase_return", "pur_return_id", purchase_id_db.ToString());
                string capital = data.UserPermissions("round(total_capital, 3)", "pos_capital");

                if (capital == "" || capital == "NULL")
                {
                    capital = "0";
                }

                if (txtAmountDue.Text != "" && double.Parse(txtAmountDue.Text) > 0)
                {
                    if (invoice_no_text.Text != "")
                    {
                        if (TextData.supplier != "")
                        {
                            if (txt_paid_amount.Text != "")
                            {
                                if (txtDiscountPercentage.Text != "")
                                {
                                    if (txtDiscountAmount.Text != "")
                                    {
                                        if (txt_freight.Text != "")
                                        {
                                            if (txtFee.Text != "")
                                            {
                                                TextData.paid = double.Parse(txt_paid_amount.Text);
                                                TextData.freight = double.Parse(txtFreight.Text);
                                                TextData.discountPercentage = double.Parse(txtDiscountPercentage.Text);
                                                TextData.discount = double.Parse(txtDiscountAmount.Text);
                                                TextData.fee = double.Parse(txtFee.Text);


                                                GetSetData.query = @"select credits from pos_purchase_return where (bill_no = '" + TextData.send_billNo.ToString() + "');";
                                                double previousCredits = data.SearchNumericValuesDb(GetSetData.query);


                                                GetSetData.query = @"update pos_purchase_return set date = '" + TextData.dates.ToString() + "' ,  invoice_no = '" + TextData.invoiceNo.ToString() + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_quantity = '" + TextData.quantity.ToString() + "' ,  net_trade_off = '" + TextData.trade_off.ToString() + "', net_carry_exp = '" + TextData.carry_exp.ToString() + "', net_total = '" + TextData.sub_total.ToString() + "' , paid = '" + TextData.paid.ToString() + "' , credits = '" + TextData.credits.ToString() + "' , freight = '" + TextData.freight.ToString() + "' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "', discount_percentage = '" + TextData.discountPercentage.ToString() + "', discount_amount = '" + TextData.discount.ToString() + "', fee_amount = '" + TextData.fee.ToString() + "' where (bill_no = '" + TextData.send_billNo.ToString() + "');";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                                //========================================================================================================

                                                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                                {
                                                    double pur_quantity_db = 0;

                                                    string product_id_db = salesAndReturnsGridView.Rows[i].Cells[17].Value.ToString();

                                                    //*********************************************************

                                                    GetSetData.query = @"select items_id from pos_pur_return_items where (prod_id = '" + product_id_db.ToString() + "') and (purchase_id = '" + purchase_id_db.ToString() + "');";
                                                    int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                                    //========================================================================================================
                                                    

                                                    GetSetData.query = @"select quantity from pos_pur_return_items where (purchase_id = '" + purchase_id_db.ToString() + "') and (prod_id = '" + product_id_db.ToString() + "');";
                                                    pur_quantity_db = data.SearchNumericValuesDb(GetSetData.query);
                                                    //========================================================================================================


                                                    TextData.quantity = data.NumericValues("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                    double quantity_test = TextData.quantity;
                                                    //========================================================================================================


                                                    TextData.quantity = TextData.quantity + double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                    TextData.pkg = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                                    TextData.full_pak = double.Parse(salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString());
                                                    TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                    //========================================================================================================


                                                    if (pur_quantity_db != double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString()))
                                                    {
                                                        GetSetData.query = "select stock_id from pos_stock_details where (prod_id = '" + product_id_db.ToString() + "');";
                                                        int is_stock_exists = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                                        if (is_stock_exists != 0)
                                                        {
                                                            //*********************************************************

                                                            TextData.quantity = (TextData.quantity + pur_quantity_db) - double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());
                                                            TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;

                                                            //*********************************************************


                                                            string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                            string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", product_id_db.ToString());
                                                            string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", product_id_db.ToString());


                                                            GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + TextData.quantity.ToString() + "' , '" + oldQuantityDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + oldCostPriceDB + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + oldSalePriceDB + "' , 'Update Return Receiveing Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);

                                                           
                                                            //========================================================================================================  

                                                            GetSetData.query = @"update pos_stock_details set quantity = '" + TextData.quantity.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (stock_id = '" + is_stock_exists.ToString() + "');";
                                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                                        }

                                                    }
                                                    // *********************************************************************                             


                                                    // Calculating total purchase and sale price for purchased items
                                                    TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString());
                                                    TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString());
                                                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString());

                                                    //TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                    TextData.total_pur_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[18].Value.ToString());
                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                    //========================================================================================================

                                                    if (pur_item_id_db == 0)
                                                    {
                                                        GetSetData.query = @"insert into pos_pur_return_items values ('" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + TextData.prod_price.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + product_id_db.ToString() + "' , '" + purchase_id_db.ToString() + "', '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "');";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    }
                                                    else
                                                    {
                                                        GetSetData.query = @"update pos_pur_return_items set quantity = '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , full_pak = '" + salesAndReturnsGridView.Rows[i].Cells[9].Value.ToString() + "' , pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[10].Value.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[11].Value.ToString() + "' , trade_off = '" + salesAndReturnsGridView.Rows[i].Cells[13].Value.ToString() + "' , carry_exp = '" + salesAndReturnsGridView.Rows[i].Cells[14].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "', new_purchase_price = '" + salesAndReturnsGridView.Rows[i].Cells[19].Value.ToString() + "' where prod_id = '" + product_id_db.ToString() + "' and purchase_id = '" + purchase_id_db.ToString() + "';";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    }
                                                    //========================================================================================================



                                                    //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Updating purchasing items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "]", role_id);
                                                }

                                                GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                                                GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                                // Calculating Debits and Credits

                                                if (TextData.credits < 0)
                                                {
                                                    TextData.credits = TextData.credits * (-1);
                                                }


                                                GetSetData.query = @"select id from pos_company_transactions where (bill_no = '" + TextData.send_billNo.ToString() + "');";
                                                int transaction_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                                if (transaction_id_db == 0)
                                                {
                                                    GetSetData.query = @"insert into pos_company_transactions values ('" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.invoiceNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.trade_off.ToString() + "' , '" + TextData.carry_exp.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.lastCredits.ToString() + "' , '" + TextData.freight.ToString() + "' , 'Purchase Return' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    //========================================================================================================  
                                                }
                                                else
                                                {
                                                    GetSetData.query = @"update pos_company_transactions set date = '" + TextData.dates.ToString() + "' , bill_no = '" + TextData.billNo.ToString() + "' , invoice_no = '" + TextData.invoiceNo.ToString() + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_quantity = '" + TextData.quantity.ToString() + "' , net_trade_off = '" + TextData.trade_off.ToString() + "' , net_carry_exp = '" + TextData.carry_exp.ToString() + "' , net_total = '" + TextData.sub_total.ToString() + "' , paid = '" + TextData.credits.ToString() + "' , credits = '" + TextData.paid.ToString() + "' , pCredits = '" + TextData.lastCredits.ToString() + "' , freight = '" + TextData.freight.ToString() + "' , status = 'Purchase Return' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (id = '" + transaction_id_db.ToString() + "');";
                                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    //======================================================================================================== 
                                                }


                                                TextData.lastCredits = 0;
                                                GetSetData.query = @"select previous_payables from pos_supplier_payables where (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                                                TextData.lastCredits = data.SearchNumericValuesDb(GetSetData.query);


                                                TextData.credits = double.Parse(txt_credits.Text);
                                                TextData.lastCredits = (TextData.lastCredits + previousCredits) - TextData.credits;


                                                if (TextData.lastCredits < 0)
                                                {
                                                    TextData.lastCredits = 0;
                                                }

                                                GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.lastCredits.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                                //========================================================================================================   

                                                if (GetSetData.Data == "Yes")
                                                {
                                                    if (capital != "NULL" && capital != "")
                                                    {
                                                        TextData.paid = ((double.Parse(capital) + TextData.paid) - previousPaidAmount);
                                                    }

                                                    if (TextData.paid >= 0)
                                                    {
                                                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.paid.ToString() + "';";
                                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                                    }
                                                }
                                                // *****************************************************************************************

                                                TextData.return_value = true;
                                                return true;
                                            }
                                            else
                                            {
                                                error.errorMessage("Available Capital is '" + capital.ToString() + "'!");
                                                error.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter the fee amount!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the Freight Amount!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the discount amount!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the discount percentage!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the Paid Amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the Supplier!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the Invoice number!");
                    error.ShowDialog();
                }


                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
                TextData.return_value = false;
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            sure.Message_choose("Please check all the records before saving!");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                if (return_button.Checked == true)
                {
                    if (insert_returned_items_into_db())
                    {
                        done.DoneMessage("Successfully Saved!");
                        done.ShowDialog();
                        refresh();
                        clearDataGridView();
                        invoice_no_text.Text = "";
                    }
                }
                else
                {
                    if (insert_records_into_db())
                    {
                        done.DoneMessage("Successfully Saved!");
                        done.ShowDialog();
                        refresh();
                        clearDataGridView();
                        invoice_no_text.Text = "";
                    }
                }
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                sure.Message_choose("Please check all the records before saving!");
                sure.ShowDialog();


                if (form_sure_message.sure == true)
                {
                    if (return_button.Checked == true)
                    {
                        if (update_purchase_returns_records_db())
                        {
                            done.DoneMessage("Successfully Updated!");
                            done.ShowDialog();
                        }
                    }
                    else
                    {
                        if (update_records_db())
                        {
                            done.DoneMessage("Successfully Updated!");
                            done.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void printInvoice(bool isPaperSizeA4)
        {
            try
            {
                TextData.send_billNo = billNo_text.Text;
                TextData.send_invoiceNo = invoice_no_text.Text;

                sure.Message_choose("Please check all the records before saving!");
                sure.ShowDialog();


                if (saveEnable == false)
                {
                    if (form_sure_message.sure == true)
                    {
                        if (return_button.Checked == true)
                        {
                            if (insert_returned_items_into_db())
                            {
                                if (TextData.return_value == true)
                                {
                                    form_purchase_return report = new form_purchase_return();
                                    report.ShowDialog();
                                }

                                refresh();
                                clearDataGridView();
                                invoice_no_text.Text = "";
                            }
                        }
                        else
                        {
                            if (insert_records_into_db())
                            {
                                if (TextData.return_value == true)
                                {
                                    if (isPaperSizeA4)
                                    {
                                        new_purchase_report report = new new_purchase_report();
                                        report.ShowDialog();
                                    }
                                    else
                                    {
                                        new_purchase_a6_report report = new new_purchase_a6_report();
                                        report.ShowDialog();
                                    }
                                   
                                }

                                refresh();
                                clearDataGridView();
                                invoice_no_text.Text = "";
                            }
                        }
                    }
                }
                else if (saveEnable == true)
                {
                    if (form_sure_message.sure == true)
                    {
                        if (form_sure_message.sure == true)
                        {
                            if (return_button.Checked == true)
                            {
                                if (update_purchase_returns_records_db())
                                {
                                    if (TextData.return_value == true)
                                    {
                                        form_purchase_return report = new form_purchase_return();
                                        report.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                if (update_records_db())
                                {
                                    if (TextData.return_value == true)
                                    {
                                        if (isPaperSizeA4)
                                        {
                                            new_purchase_report report = new new_purchase_report();
                                            report.ShowDialog();
                                        }
                                        else
                                        {
                                            new_purchase_a6_report report = new new_purchase_a6_report();
                                            report.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void Printbutton_Click(object sender, EventArgs e)
        {
            printInvoice(true);
        }

        private void btnPrintA6_Click(object sender, EventArgs e)
        {
            printInvoice(false);
        }

        private void paid_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_paid_amount.Text, e);
        }

        private void credit_amount_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_credits.Text, e);
        }

        private void txtDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscountPercentage.Text, e);
        }

        private void txtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscountAmount.Text, e);
        }

        private void txtFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtFreight.Text, e);
        }

        private void txtFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtFee.Text, e);
        }

        private void btn_purchase_from_Click(object sender, EventArgs e)
        {
            try
            {
                using (form_supplier_details add_customer = new form_supplier_details())
                {
                    form_supplier_details.count = 0;
                    form_supplier_details.role_id = role_id;
                    form_supplier_details.user_id = user_id;
                    add_customer.ShowDialog();
                    //add_customer.ShowDialog();
                    txt_supplier_name.Text = TextData.full_name;
                    RefreshFieldSupplier();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                //add_records_grid_view();
            }
        }

        private void btn_add_products_Click(object sender, EventArgs e)
        {
            try
            {
                using (add_new_product add_customer = new add_new_product())
                {
                    add_new_product.isProductCreateFromPurchase = true;
                    add_new_product.user_id = user_id;
                    add_new_product.role_id = role_id;
                    add_new_product.saveEnable = false;
                    add_customer.ShowDialog();
                    RefreshFieldProductName();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_carry_exp_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_carry_exp.Text, e);
        }

        private void txt_qty_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_qty.Text, e);
        }

        private void pieces_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_pkg.Text, e);
        }

        private void pur_price_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_pur_price.Text, e);
        }

        private void sale_price_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_sale_price.Text, e);
        }

        private void trade_off_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_trade_off.Text, e);
        }

        private void market_value_keypress(object sender, KeyPressEventArgs e)
        {  
            data.NumericValuesOnly(txt_market_value.Text, e);
        }

        private void btn_category_Click(object sender, EventArgs e)
        {
            using (add_category add_customer = new add_category())
            {
                add_category.role_id = role_id;
                add_customer.ShowDialog();
                RefreshFieldCategory();
            }
        }

        private void btn_add_color_Click(object sender, EventArgs e)
        {
            using (form_color add_customer = new form_color())
            {
                form_color.role_id = role_id;
                add_customer.ShowDialog();
                RefreshFieldColor();
            }
        }

        private string total_quantity_price()
        {
            try
            {
                TextData.quantity = 0;
                TextData.prod_price = 0;
                TextData.total_pur_price = 0;

                if (txt_qty.Text != "")
                {
                    TextData.quantity = double.Parse(txt_qty.Text);
                }

                if (txt_pur_price.Text != "")
                {
                    TextData.prod_price = double.Parse(txt_pur_price.Text);
                }

                TextData.total_pur_price = TextData.quantity * TextData.prod_price;
                return TextData.total_pur_price.ToString();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.total_pur_price.ToString();
            } 
        }

        private string total_available_stock_price()
        {
            try
            {
                TextData.quantity = 0;
                TextData.full_pak = 0;
                TextData.pkg = double.Parse(txt_pkg.Text);
                TextData.prod_price = 0;
                TextData.total_pur_price = 0;
                TextData.prod_name  = prod_name_text.Text;
                TextData.barcode = txt_barcode.Text;

                // getting the Bill No  from the LoyalCusSaleAccounts table 
                GetSetData.query = @"select product_id from pos_products where prod_name = '" + TextData.prod_name.ToString() + "' and barcode = '" + TextData.barcode.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                TextData.tab_pieces = data.NumericValues("tab_pieces", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());

                if (txt_stock.Text != "")
                {
                    TextData.quantity = double.Parse(txt_stock.Text);
                }

                if (TextData.quantity == 0)
                {
                    TextData.quantity = 1;
                }

                if (txt_pur_price.Text != "")
                {
                    TextData.prod_price = double.Parse(txt_pur_price.Text);
                }

                if (txt_pkg.Text == "" || TextData.pkg == 0)
                {
                    TextData.pkg = 1;   
                }

                if (TextData.tab_pieces == 0)
                {
                    TextData.tab_pieces = 1;
                }

                if (txt_prod_state.Text == "carton" || txt_prod_state.Text == "bag" || txt_prod_state.Text == "Tablets")
                {
                    TextData.full_pak = (TextData.quantity / TextData.pkg) / TextData.tab_pieces;
                    TextData.prod_price = TextData.prod_price * TextData.full_pak;
                    TextData.prod_price = TextData.prod_price / TextData.quantity;
                    TextData.quantity = double.Parse(txt_stock.Text);
                    TextData.total_pur_price = TextData.quantity * TextData.prod_price;
                }
                else
                {
                    TextData.quantity = double.Parse(txt_stock.Text);
                    TextData.total_pur_price = TextData.quantity * TextData.prod_price;
                }

                return TextData.total_pur_price.ToString();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.total_pur_price.ToString();
            }
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            txt_qty_price.Text = total_quantity_price();
        }

        private void txt_pur_price_TextChanged(object sender, EventArgs e)
        {
            txt_qty_price.Text = total_quantity_price();
            txt_stock_price.Text = total_available_stock_price();
        }

        private void generate_barcode()
        {
            try
            {
                TextData.barcode = "";

                TextData.barcode = txt_barcode.Text;

                if (TextData.barcode != "")
                {
                    if (txt_sale_price.Text != "")
                    {
                        Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                        img_barcode.Image = brcode.Draw(TextData.barcode, 60);
                    }
                    else
                    {
                        error.errorMessage("Please enter the product sale price!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Barcode field is empty!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_print_barcode()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();

            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            PointF point = new PointF(23f, 60f);
            Font font = new System.Drawing.Font("Verdana", 6, FontStyle.Bold);
            SolidBrush black = new SolidBrush(Color.Black);
            //SolidBrush White = new SolidBrush(Color.White);
            Bitmap bm = new Bitmap(img_barcode.Width, img_barcode.Height);
            img_barcode.DrawToBitmap(bm, new Rectangle(0, 0, img_barcode.Width, img_barcode.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            e.Graphics.DrawString(txt_barcode.Text + "   RS: " + txt_sale_price.Text, font, black, point);
            bm.Dispose();
        }

        private void printBarcodeLabel()
        {
            if (txt_barcode.Text != "")
            {
                if (txt_sale_price.Text != "")
                {
                    generate_barcode();
                    fun_print_barcode();
                }
                else
                {
                    error.errorMessage("Please enter the product sale price!");
                    error.ShowDialog();
                }
            }
            else
            {
                error.errorMessage("Barcode field is empty!");
                error.ShowDialog();
            }
        }

        private void btn_print_barcode_Click(object sender, EventArgs e)
        {
            printBarcodeLabel();
        }

        private void fun_show_credits_text ()
        {
            try
            {
                TextData.paid = 0;
                TextData.freight = 0;
                TextData.sub_total = double.Parse(txt_sub_total.Text);
                TextData.lastCredits = double.Parse(txt_last_credits.Text);
                TextData.net_total = 0;
                TextData.credits = 0;
                TextData.discount = 0;
                TextData.fee = 0;
                TextData.net_total = 0;

                if (txt_paid_amount.Text != "")
                {
                    TextData.paid = double.Parse(txt_paid_amount.Text);
                } 
                
                if (txtDiscountAmount.Text != "")
                {
                    TextData.discount = double.Parse(txtDiscountAmount.Text);
                }

                if (txtFreight.Text != "")
                {
                    TextData.freight = double.Parse(txtFreight.Text);
                }   
                
                if (txtFee.Text != "")
                {
                    TextData.fee = double.Parse(txtFee.Text);
                }

                //********************************

                double amountDue = (TextData.sub_total + TextData.freight + TextData.fee) - TextData.discount;

                txtAmountDue.Text = amountDue.ToString();

                //********************************

                TextData.net_total = TextData.sub_total + TextData.lastCredits;

               
                if (TextData.paid <= TextData.net_total && TextData.freight <= TextData.net_total && TextData.credits >= 0 && TextData.credits <= TextData.net_total && TextData.paid + TextData.freight <= TextData.net_total)
                {
                    if (return_button.Checked == false)
                    {
                        TextData.credits = amountDue - TextData.paid;
                    }
                    else
                    {
                        TextData.credits = amountDue - TextData.paid; // this is for return values
                        //TextData.credits = TextData.lastCredits - TextData.credits;
                    }
                  
                    if (TextData.credits >= 0)
                    {
                        txt_credits.Text = TextData.credits.ToString();
                    }
                }
                else
                {
                    txt_credits.Text = "0";
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            } 
        }

        private void txt_paid_amount_TextChanged(object sender, EventArgs e)
        {
            fun_show_credits_text();
        }

        private void txt_freight_TextChanged(object sender, EventArgs e)
        {
            fun_show_credits_text();
        }

        private void btn_sale_person_Click(object sender, EventArgs e)
        {
            try
            {
                using (Add_supplier add_customer = new Add_supplier())
                {
                    Add_supplier.role_id = role_id;
                    add_customer.ShowDialog();
                    RefreshFieldEmployee();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void return_button_CheckedChanged(object sender, EventArgs e)
        {
            if (return_button.Checked == true)
            {
                fun_show_billno("returns", "PR");
                FormNamelabel.Text = "Return Purchasing";
            }
            else
            {
                fun_show_billno("purchase", "PUR");
                FormNamelabel.Text = "Create New Purchasing";
            }

            //GetSetData.SaveLogHistoryDetails("Add New Purchases Details Form", "Click return purchasing items button...", role_id);
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            clearDataGridViewItems();

            if (search_box.Text == "")
            {
                GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdatePurchasingItems", TextData.billNo);
            }
            else
            {
                GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureSearchingUpdatePurchasingItems", TextData.billNo, search_box.Text);
            }

            createAddButtonInGridView();
            createCheckBoxInGridView();
        }

        private void txt_wholeSale_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_wholeSale.Text, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscount.Text, e);
        }

        private void txtDiscountLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscountLimit.Text, e);
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPercentage.Text, e);
        }

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            if (txt_sale_price.Text != "")
            {
                if (txtPercentage.Text != "")
                {
                    TextData.total_sale_price = ((double.Parse(txt_sale_price.Text) * double.Parse(txtPercentage.Text)) / 100);
                    TextData.prod_price = double.Parse(txt_sale_price.Text) - TextData.total_sale_price;
                    txt_pur_price.Text = TextData.prod_price.ToString();
                }
            }
        }

        private void txt_sale_price_TextChanged(object sender, EventArgs e)
        {
            //if (txt_sale_price.Text != "")
            //{
            //    if (txtPercentage.Text != "")
            //    {
            //        TextData.total_sale_price = ((double.Parse(txt_sale_price.Text) * double.Parse(txtPercentage.Text)) / 100);
            //        TextData.prod_price = double.Parse(txt_sale_price.Text) - TextData.total_sale_price;
            //        txt_pur_price.Text = TextData.prod_price.ToString();
            //    }
            //}
        }

        private void txt_supplier_name_Enter(object sender, EventArgs e)
        {
            RefreshFieldSupplier();
        }

        private void prod_name_text_Enter(object sender, EventArgs e)
        {
            RefreshFieldProductName();
        }

        private void category_text_Enter(object sender, EventArgs e)
        {
            RefreshFieldCategory();
        }

        private void txt_employee_Enter(object sender, EventArgs e)
        {
            RefreshFieldEmployee();
        }

        private void txt_color_Enter(object sender, EventArgs e)
        {
            RefreshFieldColor();
        }

        private void txt_supplier_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLastCreditsTextBox();
        }

        private void txt_sub_total_TextChanged(object sender, EventArgs e)
        {
            fun_show_credits_text();
        }

        private void addNewPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                printBarcodeLabel();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }

        private void prod_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            
        }

        private void txtDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            TextData.paid = 0;
            TextData.discount = 0;
            TextData.discountPercentage = 0;


            if (txt_paid_amount.Text != "")
            {
                TextData.paid = double.Parse(txt_paid_amount.Text);
            }

            if (txtDiscountPercentage.Text != "")
            {
                TextData.discountPercentage = (double.Parse(txtDiscountPercentage.Text) * double.Parse(txt_sub_total.Text) / 100);

                txtDiscountAmount.Text = TextData.discountPercentage.ToString();
            }
            else
            {
                txtDiscountAmount.Text = "0";
            }
        }

        private void txtFreight_TextChanged(object sender, EventArgs e)
        {
            TextData.freight = 0;
            TextData.fee = 0;
            TextData.net_total = 0;
            TextData.sub_total = 0;
            TextData.discount = 0;


            if (txt_sub_total.Text != "")
            {
                TextData.net_total = double.Parse(txt_sub_total.Text);
            }
            
            
            if (txtDiscountAmount.Text != "")
            {
                TextData.discount = double.Parse(txtDiscountAmount.Text);
            }

            if (txtFreight.Text != "")
            {
                TextData.freight = double.Parse(txtFreight.Text);
            }

            if (txtFee.Text != "")
            {
                TextData.fee = double.Parse(txtFee.Text);
            }


            TextData.sub_total = (TextData.net_total + TextData.freight + TextData.fee) - TextData.discount;

            txtAmountDue.Text = TextData.sub_total.ToString();

        }

        private void txt_qty_price_TextChanged(object sender, EventArgs e)
        {
            double quantity = 1;
            double totalQuantityPrice = 0;
            double perQuantityCost = 0;

            if (txt_qty.Text != "")
            {
                quantity = double.Parse(txt_qty.Text);
            }

            if (txt_qty_price.Text != "")
            {
                totalQuantityPrice = double.Parse(txt_qty_price.Text);

                perQuantityCost = totalQuantityPrice / quantity;

                txtPerQuantityPrice.Text = perQuantityCost.ToString();
            }
        }

        private void invoice_no_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
