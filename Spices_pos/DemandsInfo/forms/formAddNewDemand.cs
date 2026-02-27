    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
    using Message_box_info.forms;
    using Datalayer;
    using System.Drawing.Printing;
    using RefereningMaterial;
    using Supplier_Chain_info.forms;
    using Demands_info.PrintBill;
    using Spices_pos.DatabaseInfo.WebConfig;
    using Spices_pos.LoginInfo.controllers;
    using Products_info.forms;
    using Purchase_info.forms;

    namespace Demands_info.forms
    {
        public partial class formAddNewDemand : Form
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

            public formAddNewDemand()
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
            //DataTable table = new DataTable();

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

                //    //****************************************************************

                //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
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
                    Add_supplier.role_id = role_id;
                    add_category.role_id = role_id;
                    form_color.role_id = role_id;
                    // ***************************************************************************************************
                    GetSetData.Data = data.UserPermissions("new_demand_save", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                    pnl_save.Visible = bool.Parse(GetSetData.Data);

                    // ***************************************************************************************************
                    GetSetData.Data = data.UserPermissions("new_demand_savePrint", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                    pnl_save_print.Visible = bool.Parse(GetSetData.Data);

                    //if (bool.Parse(customers_save_db) == false && bool.Parse(customers_update_db) == false)
                    //{
                    //    pnl_add_update.Visible = false;
                    //}

                    // ***************************************************************************************************
                    GetSetData.Data = data.UserPermissions("new_demand_exit", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                    pnl_exit.Visible = bool.Parse(GetSetData.Data);

                    //GetSetData.addFormCopyrights(lblCopyrights);
                }
                catch (Exception es)
                {
                    error.errorMessage(es.Message);
                    error.ShowDialog();
                }
            }

            public static bool saveEnable;

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

            private void fillAddProductsFormTextBoxes()
            {
                try
                {
                    billNo_text.Text = TextData.billNo;
                    txt_date.Text = TextData.dates;
                    txt_supplier_name.Text = TextData.supplier;

                    GetSetData.query = "select demand_id from pos_demand_list where bill_no = '" + TextData.billNo.ToString() + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());

                    GetSetData.numericValue = data.NumericValues("no_of_items", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txt_no_of_items.Text = GetSetData.numericValue.ToString();

                    txt_employee.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.fks.ToString());

                    GetSetData.query = "select total_qty from pos_demand_list where purchase_id = '" + GetSetData.Ids.ToString() + "';";
                    GetSetData.numericValue = data.SearchNumericValuesDb(GetSetData.query);
                    txt_total_qty.Text = GetSetData.numericValue.ToString();

                    GetSetData.numericValue = data.NumericValues("net_amount", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txt_sub_total.Text = GetSetData.numericValue.ToString();

                    GetSetData.numericValue = data.NumericValues("paid", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txt_paid_amount.Text = GetSetData.numericValue.ToString();

                    GetSetData.numericValue = data.NumericValues("credits", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txtCredits.Text = GetSetData.numericValue.ToString();

                    GetSetData.numericValue = data.NumericValues("discount", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txt_discounts.Text = GetSetData.numericValue.ToString();

                    GetSetData.Data = data.UserPermissions("remarks", "pos_demand_list", "demand_id", GetSetData.Ids.ToString());
                    txt_remarks.Text = GetSetData.Data;
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
                btn.Width = 50;
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
                    FormNamelabel.Text = "Create New Purchase Order";
                    billNo_text.Text = auto_generate_code("show", "DMD");
                    search_box.Visible = false;
                    LoginEmployee();
                }
                else if (saveEnable == true)
                {
                    savebutton.Visible = false;
                    update_button.Visible = true;
                    //salesAndReturnsGridView.ReadOnly = true;
                    FormNamelabel.Text = "Update Purchase Order";
                    fillAddProductsFormTextBoxes();

                    clearDataGridViewItems();
                    GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdateDemandItems", TextData.billNo);
                    createCheckBoxInGridView();
                    refresh();
                }
            }

            private void salesAndReturnsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                deleteRowFromGridView();
            }

            private void FillProductNameTextBox()
            {
                prod_name_text.Text = data.UserPermissions("prod_name", "pos_products", "barcode", txt_barcode.Text);
            }

            private void FillBarcodeTextBox()
            {
                txt_barcode.Text = data.UserPermissions("barcode", "pos_products", "prod_name", prod_name_text.Text);
            }

            private void FillCategoryTextBox()
            {
                GetSetData.query = @"select category_id from pos_products where  prod_name = '" + prod_name_text.Text + "' and barcode = '" + txt_barcode.Text + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                category_text.Text = data.UserPermissions("title", "pos_category", "category_id", GetSetData.Ids.ToString());
            }

        private void FillProductPricesTextBox()
        {
            try
            {
                // Get product ID
                GetSetData.query = $"SELECT product_id FROM pos_products WHERE prod_name = '{prod_name_text.Text}' OR barcode = '{txt_barcode.Text}';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    txt_pur_price.Text = "0";
                    txt_sale_price.Text = "0";
                    txt_stock.Text = "0";
                    return;
                }

                // 1️⃣ Correctly Get Stock from 'quantity' column
                string stock_str = data.SearchStringValuesFromDb(
                    $"SELECT ISNULL(quantity, 0) FROM pos_stock_details WHERE prod_id = {GetSetData.Ids}"
                ) ?? "0";
                txt_stock.Text = stock_str;

                // 2️⃣ Get purchase & sale price
                string pur_price_str = data.UserPermissions("pur_price", "pos_stock_details", "prod_id", GetSetData.Ids.ToString()) ?? "0";
                string sale_price_str = data.UserPermissions("sale_price", "pos_stock_details", "prod_id", GetSetData.Ids.ToString()) ?? "0";
                txt_pur_price.Text = pur_price_str;
                txt_sale_price.Text = sale_price_str;

                // 3️⃣ Other fields
                string pkg_str = data.UserPermissions("pkg", "pos_stock_details", "prod_id", GetSetData.Ids.ToString()) ?? "1";
                string tab_pieces_str = data.UserPermissions("tab_pieces", "pos_stock_details", "prod_id", GetSetData.Ids.ToString()) ?? "1";
                string prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString()) ?? "";
                txt_prod_state.Text = prod_state;

                GetSetData.fks = data.UserPermissionsIds("color_id", "pos_products", "product_id", GetSetData.Ids.ToString());
                txt_color.Text = data.UserPermissions("title", "pos_color", "color_id", GetSetData.fks.ToString()) ?? "";

                // 4️⃣ Adjust prices if carton/bag/tablets
                double pur_price = double.TryParse(pur_price_str, out pur_price) ? pur_price : 0;
                double sale_price = double.TryParse(sale_price_str, out sale_price) ? sale_price : 0;
                double pkg = double.TryParse(pkg_str, out pkg) ? pkg : 1;
                double tab_pieces = double.TryParse(tab_pieces_str, out tab_pieces) ? tab_pieces : 1;

                if (prod_state == "carton" || prod_state == "bag" || prod_state == "Tablets")
                {
                    double total_pieces = pkg * tab_pieces;
                    txt_pur_price.Text = (pur_price * total_pieces).ToString();
                    txt_sale_price.Text = (sale_price * total_pieces).ToString();
                }
            }
            catch (Exception ex)
            {
                txt_stock.Text = "0";
                txt_pur_price.Text = "0";
                txt_sale_price.Text = "0";
                txt_color.Text = "";
                txt_prod_state.Text = "";
                error.errorMessage(ex.Message);
                error.ShowDialog();
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

                txt_date.Text = DateTime.Now.ToLongDateString();
                txt_remarks.Text = "";
                txt_no_of_items.Text = "0";
                TextData.no_of_items = 0;
                txt_total_qty.Text = "0";
                txtCredits.Text = "0";
                txt_sub_total.Text = "0";
                txt_paid_amount.Text = "0";
                txt_discounts.Text = "0";

                salesAndReturnsGridView.DataSource = null;
                enableSaveButton();
                //return_button.Checked = false;
            }

            private void refresh()
            {
                prod_name_text.Text = null;
                txt_barcode.Text = "";
                category_text.Text = "-Select-";
                txt_color.Text = "-Select-";
                txt_qty.Text = "1";
                txt_qty_price.Text = "0";
                txt_stock.Text = "0";
                txt_pur_price.Text = "";
                txt_sale_price.Text = "";
                txt_prod_state.Text = "";
            }

            private void salesAndReturnsGridView_MouseClick(object sender, MouseEventArgs e)
            {
                //throw new NotImplementedException();

                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();
                    GetSetData.Ids = salesAndReturnsGridView.HitTest(e.X, e.Y).RowIndex;

                    if (GetSetData.Ids >= 0)
                    {
                        my_menu.Items.Add("Delete").Name = "Delete";
                    }

                    my_menu.Show(salesAndReturnsGridView, new Point(e.X, e.Y));

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
                    string purchase_price = salesAndReturnsGridView.SelectedRows[0].Cells[7].Value.ToString();
                    string quantity = salesAndReturnsGridView.SelectedRows[0].Cells[3].Value.ToString();
                    TextData.prod_name = salesAndReturnsGridView.SelectedRows[0].Cells[0].Value.ToString();
                    TextData.barcode = salesAndReturnsGridView.SelectedRows[0].Cells[1].Value.ToString();
                    TextData.billNo = billNo_text.Text;



                    // calculating net totals
                    TextData.total_pur_price = double.Parse(purchase_price) * double.Parse(quantity);
                    TextData.sub_total = double.Parse(txt_sub_total.Text) - TextData.total_pur_price;
                    txt_sub_total.Text = TextData.sub_total.ToString();

                    TextData.quantity = double.Parse(txt_total_qty.Text) - double.Parse(quantity);
                    txt_total_qty.Text = TextData.quantity.ToString();

                    TextData.no_of_items--;
                    txt_no_of_items.Text = TextData.no_of_items.ToString();

                    GetSetData.Ids = salesAndReturnsGridView.CurrentCell.RowIndex;
                    salesAndReturnsGridView.Rows.RemoveAt(GetSetData.Ids);

                    GetSetData.query = "select purchase_id from pos_demand_list where bill_no = '" + TextData.billNo + "';";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select product_id from pos_products where prod_name = '" + TextData.prod_name + "' and barcode = '" + TextData.barcode + "';";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = "select items_id from pos_demand_listd_items where prod_id = '" + GetSetData.fks.ToString() + "' and purchase_id = '" + GetSetData.Ids.ToString() + "';";
                    int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (pur_item_id_db != 0)
                    {
                        GetSetData.query = "delete from pos_demand_listd_items where prod_id = '" + GetSetData.fks.ToString() + "' and purchase_id = '" + GetSetData.Ids.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                catch (Exception es)
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                    //MessageBox.Show(es.Message);
                }
            }

            private void Closebutton_Click(object sender, EventArgs e)
            {
                //clearDataGridView();
                clearGridView();
                formDemandList.role_id = role_id;
                //formDemandList.user_id = user_id;
                formDemandList _obj = new formDemandList();
                _obj.Show();
                this.Dispose();
            }

            private void formAddNewDemand_Load(object sender, EventArgs e)
            {
                try
                {
                    //originalExStyle = -1;
                    //enableFormLevelDoubleBuffering = true;
                    system_user_permissions();

                    refresh();
                    clearDataGridView();

                    salesAndReturnsGridView.MouseClick += new MouseEventHandler(salesAndReturnsGridView_MouseClick);
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

            private bool add_records_grid_view()
            {
                //Add New Customers Details and SaleItems list)
                try
                {
                    //Store Data from Textboxes to textdata properties:
                    TextData.billNo = billNo_text.Text;
                    TextData.supplier = txt_supplier_name.Text;
                    TextData.employee = txt_employee.Text;
                    TextData.prod_name = prod_name_text.Text;
                    TextData.barcode = txt_barcode.Text;
                    TextData.category = category_text.Text;
                    TextData.color = txt_color.Text;
                    TextData.quantity = double.Parse(txt_qty.Text);
                    TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                    TextData.remarks = txt_remarks.Text;
                    string gird_pur_price = txt_pur_price.Text;

                    // ******************************************************************
                    GetSetData.query = @"select product_id from pos_products where prod_name = '" + TextData.prod_name.ToString() + "' and barcode = '" + TextData.barcode.ToString() + "';";
                    int prod_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    TextData.tab_pieces = data.UserPermissionsIds("tab_pieces", "pos_stock_details", "prod_id", prod_id_db.ToString());
                    TextData.pkg = data.UserPermissionsIds("pkg", "pos_stock_details", "prod_id", prod_id_db.ToString());
                // Correct: category_id aur color_id pos_products table se lein
                int category_id_db = data.UserPermissionsIds("category_id", "pos_products", "product_id", prod_id_db.ToString());
                int color_id_db = data.UserPermissionsIds("color_id", "pos_products", "product_id", prod_id_db.ToString());

                // Ab category title pos_category table se lein
                string categoryTitleDb = "";
                if (category_id_db != 0)
                {
                    categoryTitleDb = data.UserPermissions("title", "pos_category", "category_id", category_id_db.ToString());
                }

                // Fallback: agar category nahi mili to "N/A" ya blank
                TextData.category = string.IsNullOrEmpty(categoryTitleDb) ? "N/A" : categoryTitleDb;

                // Same for color (optional improvement)
                string colorTitleDb = "";
                if (color_id_db != 0)
                {
                    colorTitleDb = data.UserPermissions("title", "pos_color", "color_id", color_id_db.ToString());
                }
                TextData.color = string.IsNullOrEmpty(colorTitleDb) ? "" : colorTitleDb;



                if (TextData.color == "-Select" || TextData.color == "")
                    {
                        TextData.color = data.UserPermissions("title", "pos_color", "color_id", color_id_db.ToString());
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

                    if (TextData.supplier != "")
                    {
                        if (TextData.prod_name != "")
                        {
                            if (txt_pur_price.Text != "")
                            {
                                if (txt_sale_price.Text != "")
                                {
                                    if (txt_discounts.Text != "")
                                    {
                                        TextData.prod_price = double.Parse(txt_pur_price.Text);
                                        TextData.sale_price = double.Parse(txt_sale_price.Text);
                                        TextData.freight = double.Parse(txt_sale_price.Text);

                                        if (txt_prod_state.Text == "carton" || txt_prod_state.Text == "bag" || txt_prod_state.Text == "Tablets")
                                        {
                                            //Calculating the total quantity **********************************************************
                                            TextData.full_pak = (TextData.quantity * TextData.pkg) * TextData.tab_pieces; // calculating total quantity
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity; // calculating the total purchase price
                                            TextData.prod_price = TextData.total_pur_price / TextData.full_pak; // calculating the purchase price of per piece
                                            gird_pur_price = TextData.prod_price.ToString();
                                            // **********************************************************

                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                            TextData.sale_price = TextData.total_sale_price / TextData.full_pak; // calculating the sale price of per piece

                                            TextData.quantity = TextData.full_pak;
                                            TextData.full_pak = double.Parse(txt_qty.Text);
                                        }
                                        else
                                        {
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity; // bill total purchase price
                                            TextData.prod_price = TextData.total_pur_price / TextData.quantity;// purchase price per piece

                                            TextData.pkg = 1;
                                            TextData.full_pak = 0;
                                            TextData.tab_pieces = 1;
                                            TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                            TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                            //TextData.prod_price = double.Parse(txt_pur_price.Text);
                                        }

                                        // Inserting Data in the Columns:
                                        if (GetSetData.Data != TextData.prod_name)
                                        {
                                            if (saveEnable == false)
                                            {
                                                int n = salesAndReturnsGridView.Rows.Add();
                                                salesAndReturnsGridView.Rows[n].Cells[0].Value = TextData.prod_name;
                                                salesAndReturnsGridView.Rows[n].Cells[1].Value = TextData.barcode;
                                                salesAndReturnsGridView.Rows[n].Cells[2].Value = TextData.category;
                                                salesAndReturnsGridView.Rows[n].Cells[3].Value = TextData.quantity.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[4].Value = TextData.pkg.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[5].Value = TextData.tab_pieces.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[6].Value = TextData.full_pak.ToString();
                                                salesAndReturnsGridView.Rows[n].Cells[7].Value = gird_pur_price;
                                                salesAndReturnsGridView.Rows[n].Cells[8].Value = TextData.sale_price.ToString();
                                            }
                                            else if (saveEnable == true)
                                            {
                                                DataTable dt = salesAndReturnsGridView.DataSource as DataTable;
                                                DataRow row = dt.NewRow();

                                                //Populate the row with data
                                                row[0] = TextData.prod_name;
                                                row[1] = TextData.barcode;
                                                row[2] = TextData.category;
                                                row[3] = TextData.quantity.ToString();
                                                row[4] = TextData.pkg.ToString();
                                                row[5] = TextData.tab_pieces.ToString();
                                                row[6] = TextData.full_pak.ToString();
                                                row[7] = gird_pur_price;
                                                row[8] = TextData.sale_price.ToString();
                                                dt.Rows.Add(row);

                                            }

                                            TextData.sub_total = double.Parse(txt_sub_total.Text) + TextData.total_pur_price;
                                            txt_sub_total.Text = TextData.sub_total.ToString();
                                            TextData.quantity = double.Parse(txt_total_qty.Text) + TextData.quantity;
                                            txt_total_qty.Text = TextData.quantity.ToString();
                                            TextData.no_of_items++;

                                            GetSetData.SaveLogHistoryDetails("Add New Demands Form", "Adding items in cart...", role_id);
                                        }

                                        // calculating net totals
                                        txt_no_of_items.Text = TextData.no_of_items.ToString();
                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter discount!");
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
                        error.errorMessage("Please enter company name!");
                        error.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //error.errorMessage(ex.Message);
                    //error.ShowDialog();
                }
                return false;
            }

            private void add_button_Click(object sender, EventArgs e)
            {
                if (add_records_grid_view())
                {
                    refresh();
                }
            }

            private void prod_name_text_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FillBarcodeTextBox();
                    FillCategoryTextBox();
                    FillProductPricesTextBox();
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
                refresh();
                clearDataGridView();
            }

            private string auto_generate_code(string condition, string value)
            {
                TextData.billNo = "";

                try
                {
                    GetSetData.query = @"SELECT top 1 demandCodes FROM pos_AllCodes order by id desc;";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.Ids++;

                    if (condition != "show")
                    {
                        GetSetData.query = @"update pos_AllCodes set demandCodes = '" + GetSetData.Ids.ToString() + "';";
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

            private bool insert_records_into_db()
            {
                try
                {
                    //Store Data from Textboxes to textdata properties:
                    TextData.billNo = billNo_text.Text;
                    TextData.dates = txt_date.Text;
                    TextData.supplier = txt_supplier_name.Text;
                    TextData.employee = txt_employee.Text;
                    TextData.quantity = double.Parse(txt_total_qty.Text);
                    TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                    TextData.sub_total = double.Parse(txt_sub_total.Text);
                    TextData.credits = double.Parse(txtCredits.Text);
                    TextData.billNo = auto_generate_code("", "DMD");

                    if (TextData.employee == "-Select-" || TextData.employee == "")
                    {
                        TextData.employee = "nill";
                    }

                    GetSetData.fks = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                    GetSetData.Ids = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                    GetSetData.Permission = data.UserPermissions("bill_no", "pos_demand_list", "bill_no", TextData.billNo);

                    if (GetSetData.Permission == "" && GetSetData.Permission != TextData.billNo)
                    {
                        if (TextData.supplier != "")
                        {
                            if (txt_paid_amount.Text != "")
                            {
                                if (txt_discounts.Text != "")
                                {
                                    TextData.paid = double.Parse(txt_paid_amount.Text);
                                    TextData.freight = double.Parse(txt_discounts.Text);

                                    GetSetData.query = "insert into pos_demand_list values ('" + TextData.dates.ToString() + "' , '" + TextData.billNo.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.sub_total.ToString() + "' , '" + TextData.paid.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.freight.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                    //========================================================================================================

                                    GetSetData.query = "select demand_id from pos_demand_list where bill_no = '" + TextData.billNo.ToString() + "';";
                                    int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                    //========================================================================================================

                                    for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                    {
                                        GetSetData.query = "select product_id from pos_products where prod_name = '" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "' and barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "';";
                                        int product_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                        //========================================================================================================

                                        // Calculating total purchase and sale price for purchased items
                                        TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                        TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString());
                                        TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString());

                                        TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                        //========================================================================================================
                                        if (purchase_id_db != 0)
                                        {
                                            GetSetData.query = "insert into pos_demand_items values ('" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[4].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + purchase_id_db.ToString() + "' , '" + product_id_db.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }

                                        //GetSetData.SaveLogHistoryDetails("Add New Demands Form", "Saving items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "]", role_id);
                                        // Generating BillNo 
                                    }

                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the Discount Amount!");
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
                        error.errorMessage("'" + GetSetData.Permission.ToString() + "' is already exist!");
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

            private bool update_records_db()
            {
                try
                {
                    //Store Data from Textboxes to textdata properties:
                    TextData.billNo = billNo_text.Text;
                    TextData.dates = txt_date.Text;
                    TextData.supplier = txt_supplier_name.Text;
                    TextData.employee = txt_employee.Text;
                    TextData.quantity = double.Parse(txt_total_qty.Text);
                    TextData.no_of_items = double.Parse(txt_no_of_items.Text);
                    TextData.sub_total = double.Parse(txt_sub_total.Text);
                    TextData.credits = double.Parse(txtCredits.Text);


                    GetSetData.Ids = data.UserPermissionsIds("supplier_id", "pos_suppliers", "full_name", TextData.supplier);
                    GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                    if (TextData.supplier != "")
                    {
                        if (txt_paid_amount.Text != "")
                        {
                            if (txt_discounts.Text != "")
                            {
                                TextData.paid = double.Parse(txt_paid_amount.Text);
                                TextData.freight = double.Parse(txt_discounts.Text);

                                GetSetData.query = @"update pos_demand_list set date = '" + TextData.dates.ToString() + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_qty = '" + TextData.quantity.ToString() + "' , net_amount = '" + TextData.sub_total.ToString() + "' , paid = '" + TextData.paid.ToString() + "' , credits = '" + TextData.credits.ToString() + "' , discount = '" + TextData.freight.ToString() + "' , remarks = '" + TextData.remarks + "' , supplier_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where bill_no = '" + TextData.billNo.ToString() + "';";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                //========================================================================================================

                                GetSetData.query = @"select demand_id from pos_demand_list where bill_no = '" + TextData.billNo.ToString() + "';";
                                int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                //========================================================================================================

                                for (int i = 0; i < salesAndReturnsGridView.Rows.Count; i++)
                                {
                                    double pur_quantity_db = 0;
                                    GetSetData.query = @"select product_id from pos_products where prod_name = '" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "' and barcode = '" + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "';";
                                    int product_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    GetSetData.query = @"select items_id from pos_demand_items where prod_id = '" + product_id_db.ToString() + "' and demand_id = '" + purchase_id_db.ToString() + "';";
                                    int pur_item_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                    //======================================================================================================== 

                                    // Calculating total purchase and sale price for purchased items
                                    TextData.prod_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString());
                                    TextData.sale_price = double.Parse(salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString());
                                    TextData.quantity = double.Parse(salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString());

                                    TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                    //========================================================================================================

                                    if (pur_item_id_db == 0)
                                    {
                                        GetSetData.query = @"insert into pos_demand_items values ('" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[4].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[5].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString() + "' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + purchase_id_db.ToString() + "' , '" + product_id_db.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }
                                    else
                                    {
                                        GetSetData.query = @"update pos_demand_items set quantity = '" + salesAndReturnsGridView.Rows[i].Cells[3].Value.ToString() + "' , pkg = '" + salesAndReturnsGridView.Rows[i].Cells[4].Value.ToString() + "' , full_pak = '" + salesAndReturnsGridView.Rows[i].Cells[6].Value.ToString() + "' , pur_price = '" + salesAndReturnsGridView.Rows[i].Cells[7].Value.ToString() + "' , sale_price = '" + salesAndReturnsGridView.Rows[i].Cells[8].Value.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where prod_id = '" + product_id_db.ToString() + "' and purchase_id = '" + purchase_id_db.ToString() + "';";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }

                                    GetSetData.SaveLogHistoryDetails("Add New Demands Form", "Updating items [" + salesAndReturnsGridView.Rows[i].Cells[0].Value.ToString() + "  " + salesAndReturnsGridView.Rows[i].Cells[1].Value.ToString() + "] details", role_id);
                                }

                                return true;
                            }
                            else
                            {
                                error.errorMessage("Please enter the Discount Amount!");
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
                if (insert_records_into_db())
                {
                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                    refresh();
                    clearDataGridView();
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

            private void Printbutton_Click(object sender, EventArgs e)
            {
                TextData.send_billNo = billNo_text.Text;

                if (saveEnable == false)
                {
                        if (insert_records_into_db())
                        {
                            form_demand_receipt report = new form_demand_receipt();
                            report.ShowDialog();

                            refresh();
                            clearDataGridView();
                        }
                }
                else if (saveEnable == true)
                {
                    if (update_records_db())
                    {
                        GetSetData.SaveLogHistoryDetails("Add New Demands Form", "Print demand Invoice...", role_id);
                        form_demand_receipt report = new form_demand_receipt();
                        report.ShowDialog();
                    }
                }  
            }

            private void txt_paid_amount_KeyPress(object sender, KeyPressEventArgs e)
            {
                data.NumericValuesOnly(txt_paid_amount.Text, e);
            }

            private void txt_discounts_KeyPress(object sender, KeyPressEventArgs e)
            {
                data.NumericValuesOnly(txt_discounts.Text, e);
            }

            private void btn_select_supplier_Click(object sender, EventArgs e)
            {
                try
                {
                    using (form_supplier_details add_customer = new form_supplier_details())
                    {
                        form_supplier_details.role_id = role_id;
                        add_customer.ShowDialog();
                        //add_customer.ShowDialog();
                        //txt_supplier_name.Text = TextData.full_name;
                        RefreshFieldSupplier();
                    }
                }
                catch (Exception es)
                {
                    error.errorMessage(es.Message);
                    error.ShowDialog();
                }
            }

            private void btn_add_products_Click(object sender, EventArgs e)
            {
                try
                {
                    using (add_new_product add_customer = new add_new_product())
                    {
                        add_new_product.role_id = role_id;
                        add_new_product.saveEnable = false;
                        add_customer.ShowDialog();
                        //txt_supplier_name.Text = TextData.full_name;
                        RefreshFieldProductName();
                    }
                }
                catch (Exception es)
                {
                    error.errorMessage(es.Message);
                    error.ShowDialog();
                }
            }

            private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
            {
                data.NumericValuesOnly(txt_qty.Text, e);
            }

            private void txt_pur_price_KeyPress(object sender, KeyPressEventArgs e)
            {
                data.NumericValuesOnly(txt_pur_price.Text, e);
            }

            private void txt_sale_price_KeyPress(object sender, KeyPressEventArgs e)
            {
                data.NumericValuesOnly(txt_sale_price.Text, e);
            }

            private void btn_category_Click(object sender, EventArgs e)
            {
                using (add_category add_customer = new add_category())
                {
                    add_customer.ShowDialog();
                    RefreshFieldCategory();
                } 
            }

            private void btn_add_color_Click(object sender, EventArgs e)
            {
                using (form_color add_customer = new form_color())
                {
                    add_customer.ShowDialog();
                    RefreshFieldColor();
                }
            }

            private void btn_sale_person_Click(object sender, EventArgs e)
            {
                try
                {
                    using (Add_supplier add_customer = new Add_supplier())
                    {
                        Add_supplier.saveEnable = false;
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
                    //TextData.pkg = double.Parse(txt_pkg.Text);
                    TextData.prod_price = 0;
                    TextData.total_pur_price = 0;
                    TextData.prod_name = prod_name_text.Text;
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

                    //if (txt_pkg.Text == "" || TextData.pkg == 0)
                    //{
                    //    TextData.pkg = 1;
                    //}

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
                //txt_stock_price.Text = total_available_stock_price();
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

            private void btn_print_barcode_Click(object sender, EventArgs e)
            {
                this.Opacity = .850;
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
                this.Opacity = .999;
            }

            private void fun_show_credits_text()
            {
                try
                {
                    TextData.paid = 0;
                    TextData.freight = 0;
                    TextData.sub_total = double.Parse(txt_sub_total.Text);
                    TextData.net_total = double.Parse(txt_sub_total.Text);
                    TextData.credits = 0;

                    if (txt_paid_amount.Text != "")
                    {
                        TextData.paid = double.Parse(txt_paid_amount.Text);
                    }

                    if (txt_discounts.Text != "")
                    {
                        TextData.freight = double.Parse(txt_discounts.Text);
                    }

                    if (TextData.paid <= TextData.net_total && TextData.freight <= TextData.net_total && TextData.credits >= 0 && TextData.credits <= TextData.net_total && TextData.paid + TextData.freight <= TextData.net_total)
                    {
                        TextData.credits = TextData.net_total - (TextData.paid + TextData.freight);

                        if (TextData.credits >= 0)
                        {
                            txtCredits.Text = TextData.credits.ToString();
                        }
                    }
                    else
                    {
                        txtCredits.Text = "0";
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

            private void txt_discounts_TextChanged(object sender, EventArgs e)
            {
                fun_show_credits_text();
            }

            private void search_box_TextChanged(object sender, EventArgs e)
            {
                clearDataGridViewItems();

                if (search_box.Text == "")
                {
                    GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureUpdateDemandItems", TextData.billNo);
                }
                else
                {
                    GetSetData.FillDataGridView(salesAndReturnsGridView, "ProcedureSearchingUpdateDemandItems", TextData.billNo, search_box.Text);
                }

                createCheckBoxInGridView();
            }

            private void button9_Click(object sender, EventArgs e)
            {
                //TrunOffFormLevelDoubleBuffering();
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;
            }

            private void prod_name_text_Enter(object sender, EventArgs e)
            {
                RefreshFieldProductName();
            }

            private void txt_supplier_name_Enter(object sender, EventArgs e)
            {
                RefreshFieldSupplier();
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
        }
    }
