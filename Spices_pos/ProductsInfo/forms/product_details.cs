using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using Products_info.reports;
using RefereningMaterial;
using System.Diagnostics;
using CounterSales_info.CustomerSalesReport;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.controllers;
using Spices_pos.DashboardInfo.CustomControls;

namespace Products_info.forms
{
    public partial class product_details : Form
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

        public product_details()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        public static int count = 0;
        string discountValue = "";
        public static string selectedProduct = "";
        public static string selectedProductID = "";
        public static string providedValue = "";

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
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnShelfItems);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_expired_items);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_show_all);
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
                pnl_print.Visible = bool.Parse(data.UserPermissions("products_details_print", "pos_tbl_authorities_button_controls2", role_id));
                pnl_delete.Visible = bool.Parse(data.UserPermissions("products_details_delete", "pos_tbl_authorities_button_controls2", role_id));
                pnl_add_new.Visible = bool.Parse(data.UserPermissions("products_details_new", "pos_tbl_authorities_button_controls2", role_id));
                //pnl_modify.Visible = bool.Parse(data.UserPermissions("products_details_modify", "pos_tbl_authorities_button_controls2", role_id));
                pnl_regular_items.Visible = bool.Parse(data.UserPermissions("products_details_regular", "pos_tbl_authorities_button_controls2", role_id));
                btn_expired_items.Visible = bool.Parse(data.UserPermissions("products_expired", "pos_tbl_authorities_button_controls2", role_id));
                discountValue = data.UserPermissions("discountType", "pos_general_settings");

                
                if (count == 1)
                {
                    sidePanel.Visible = true;
                    setSideBarInSidePanel();
                }
                else
                {
                    sidePanel.Visible = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void setSideBarInSidePanel()
        {
            try
            {
                sidebarUserControl sidebar = new sidebarUserControl();

                sidebar.user_id = user_id;
                sidebar.role_id = role_id;

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
                sidebar.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clearGridView()
        {
            this.productDetailGridView.DataSource = null;
            this.productDetailGridView.Refresh();
            productDetailGridView.Rows.Clear();
            productDetailGridView.Columns.Clear();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select top 500 [PID], [Product Name], [Category], [Brand], [Description] from ViewProducts";

                if (condition == "notNull")
                {
                    //GetSetData.query = GetSetData.query + " where status = 'Enabled'";
                }
                else if (condition == "search")
                {
                    int product_id = data.UserPermissionsIds("prod_id", "pos_stock_details", "item_barcode", search_box.Text);

                    if (product_id != 0)
                    {
                        GetSetData.query = GetSetData.query + " where ([PID] = '" + product_id.ToString() + "') or ([Product Name] like '%" + search_box.Text + "%' or [Category] like '%" + search_box.Text + "%' or [Brand] like '%" + search_box.Text + "%')";
                    }
                    else
                    {
                        GetSetData.query = GetSetData.query + " where ([PID] like '%" + search_box.Text + "') or ([Product Name] like '%" + search_box.Text + "%' or [Category] like '%" + search_box.Text + "%' or [Brand] like '%" + search_box.Text + "%')";
                    }
                }


                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void search_box_TextChanged_1(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void fun_exit_form()
        {
            switch (count)
            {
                case 0:
                    Button_controls.CounterSalesButtons();
                    break;

                case 1:
                    Button_controls.mainMenu_buttons();
                    break; 

                case 2:
                    Button_controls.addPurchase_buttons();
                    break;

                default:
                    this.Close();
                    break;
            }

            this.Close();
        }
        private void Closebutton_Click(object sender, EventArgs e)
        {
            fun_exit_form();
        }

        private void product_details_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();

                if (providedValue == "")
                {
                    FillGridViewUsingPagination("notNull");
                }
                else
                {
                    FillGridViewUsingPagination("selectedProduct");
                }

                search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private bool fun_update_details()
        //{
        //    try
        //    {
        //        GetSetData.query = "select products_details_modify from pos_tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
        //        GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (GetSetData.Data == "True")
        //        {
        //            add_new_product.saveEnable = true;
        //            TextData.prod_name = TextData.prod_name_key = productDetailGridView.SelectedRows[0].Cells["Product Name"].Value.ToString();
        //            TextData.barcode = TextData.barcode_key = productDetailGridView.SelectedRows[0].Cells["Barcode"].Value.ToString();
        //            TextData.exp_date = productDetailGridView.SelectedRows[0].Cells["Expiry Date"].Value.ToString();

        //            GetSetData.query = "select product_id from pos_products where prod_name = '" + TextData.prod_name_key.ToString() + "' and barcode = '" + TextData.barcode_key.ToString() + "';";
        //            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //            TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString());
        //            TextData.size = data.UserPermissions("size", "pos_products", "product_id", GetSetData.Ids.ToString());
        //            TextData.remarks = TextData.size;

        //            TextData.discount = data.NumericValues("discount", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
        //            TextData.disLimit = data.NumericValues("discount_limit", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());

        //            string marketPrice = data.UserPermissions("market_value", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
        //            TextData.market_value = double.Parse(marketPrice);
        //            // *********************************************************************************

        //            GetSetData.Data = productDetailGridView.SelectedRows[0].Cells["Qty"].Value.ToString();
        //            TextData.quantity = double.Parse(GetSetData.Data);
        //            string pkg = productDetailGridView.SelectedRows[0].Cells["PKG"].Value.ToString();
        //            TextData.pkg = double.Parse(pkg);
        //            string full_pag = productDetailGridView.SelectedRows[0].Cells["Tab PCS"].Value.ToString();
        //            TextData.tab_pieces = double.Parse(full_pag);
        //            TextData.status = productDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString();
        //            TextData.category = productDetailGridView.SelectedRows[0].Cells["Category"].Value.ToString();
        //            TextData.sub_category = productDetailGridView.SelectedRows[0].Cells["Sub Category"].Value.ToString();
        //            TextData.brand = productDetailGridView.SelectedRows[0].Cells["Brand"].Value.ToString();
        //            string pur_price = productDetailGridView.SelectedRows[0].Cells["Pur Price"].Value.ToString();
        //            TextData.prod_price = double.Parse(pur_price);
        //            string sale_price = productDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString();
        //            TextData.sale_price = double.Parse(sale_price);

        //            if (discountValue == "Yes")
        //            {
        //                TextData.discount = (TextData.discount / TextData.sale_price) * 100;
        //                TextData.disLimit = (TextData.disLimit / TextData.sale_price) * 100;
        //            }

        //            // *********************************************************************************

        //            if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets")
        //            {
        //                if (TextData.pkg == 0)
        //                {
        //                    TextData.pkg = 1;
        //                }

        //                if (TextData.tab_pieces == 0)
        //                {
        //                    TextData.tab_pieces = 1;
        //                }

        //                TextData.quantity = (TextData.quantity / TextData.pkg) / TextData.tab_pieces;

        //                if (TextData.quantity == 0)
        //                {
        //                    TextData.quantity = 1;
        //                }

        //                TextData.prod_price = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.prod_price;
        //                TextData.sale_price = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.sale_price;
        //                //TextData.market_value = (double.Parse(GetSetData.Data) / TextData.quantity) * double.Parse(marketPrice);
        //                TextData.totalWholeSale = (double.Parse(GetSetData.Data) / TextData.quantity) * double.Parse(TextData.size);
        //                TextData.size = TextData.totalWholeSale.ToString();

        //                //if (discountValue == "No")
        //                //{
        //                //    TextData.discount = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.discount;
        //                //    TextData.disLimit = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.disLimit;
        //                //}

        //                if (double.Parse(GetSetData.Data) == 0)
        //                {
        //                    TextData.quantity = TextData.pkg * TextData.tab_pieces;
        //                    TextData.prod_price = double.Parse(pur_price) * TextData.quantity;
        //                    TextData.sale_price = double.Parse(sale_price) * TextData.quantity;
        //                    //TextData.market_value = double.Parse(marketPrice) * TextData.quantity;
        //                    TextData.totalWholeSale = double.Parse(TextData.remarks) * TextData.quantity;
        //                    TextData.size = TextData.totalWholeSale.ToString();

        //                    if (discountValue == "No")
        //                    {
        //                        TextData.discount = TextData.discount * TextData.quantity;
        //                        TextData.disLimit = TextData.disLimit * TextData.quantity;
        //                    }
        //                }

        //                TextData.pkg = double.Parse(pkg);
        //                TextData.tab_pieces = double.Parse(full_pag);
        //                TextData.quantity = double.Parse(GetSetData.Data);
        //                TextData.quantity = (TextData.quantity / TextData.pkg) / TextData.tab_pieces;
        //            }

        //            GetSetData.SaveLogHistoryDetails("Products Details Form", "Updating item [" + TextData.prod_name + "  " + TextData.barcode + "] details (Modify button click...)", role_id);

        //            using (add_new_product add_customer = new add_new_product())
        //            {
        //                add_new_product.role_id = role_id;
        //                add_customer.ShowDialog();
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage("Please select the row first!");
        //        error.ShowDialog();
        //        return false;
        //    }
        //}

        private bool fun_update_details()
        {
            try
            {
                GetSetData.query = "select products_details_modify from pos_tbl_authorities_button_controls2 where (role_id = '" + role_id.ToString() + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "True")
                {
                    add_new_product.saveEnable = true;
                    add_new_product.isProductCreateFromPurchase = false;

                    TextData.stock_id = productDetailGridView1.SelectedRows[0].Cells[0].Value.ToString();

                    if (TextData.stock_id != "")
                    {
                        TextData.product_id = data.UserPermissions("prod_id", "pos_stock_details", "stock_id", TextData.stock_id.ToString());


                        string category_id = data.UserPermissions("category_id", "pos_products", "product_id", TextData.product_id);
                        TextData.category = data.UserPermissions("title", "pos_category", "category_id", category_id);
                        
                        
                        string sub_category_id = data.UserPermissions("sub_cate_id", "pos_products", "product_id", TextData.product_id);
                        TextData.sub_category = data.UserPermissions("title", "pos_subcategory", "sub_cate_id", sub_category_id);


                        string brand_id = data.UserPermissions("brand_id", "pos_products", "product_id", TextData.product_id);
                        TextData.brand = data.UserPermissions("brand_title", "pos_brand", "brand_id", brand_id);


                        TextData.status = data.UserPermissions("status", "pos_products", "product_id", TextData.product_id);


                        TextData.barcode = productDetailGridView1.SelectedRows[0].Cells["Barcode"].Value.ToString();
                        TextData.exp_date = productDetailGridView1.SelectedRows[0].Cells["Expiry"].Value.ToString();


                        TextData.prod_name = data.UserPermissions("prod_name", "pos_products", "product_id", TextData.product_id);
                        TextData.prod_name_key = TextData.prod_name;
                        
                        TextData.switchPromotion = data.UserPermissions("allow_promotion", "pos_products", "product_id", TextData.product_id);

                        TextData.remarks = data.UserPermissions("remarks", "pos_products", "product_id", TextData.product_id);

                        TextData.mfg_date = data.UserPermissions("date_of_manufacture", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", TextData.product_id);

                        TextData.prod_unit = data.UserPermissions("unit", "pos_products", "product_id", TextData.product_id);
                        
                        TextData.wholeSalePrice = data.UserPermissions("whole_sale_price", "pos_stock_details", "stock_id", TextData.stock_id);
                        TextData.remarks = TextData.wholeSalePrice;

                        TextData.discount = data.NumericValues("discount", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.disLimit = data.NumericValues("discount_limit", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.qty_alert = data.UserPermissions("qty_alert", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.chk_qty_alert = data.UserPermissions("alert_status", "pos_stock_details", "stock_id", TextData.stock_id);

                        TextData.full_pak = data.NumericValues("full_pak", "pos_stock_details", "stock_id", TextData.stock_id);


                        string marketPrice = data.UserPermissions("market_value", "pos_stock_details", "stock_id", TextData.stock_id);
                        TextData.market_value = double.Parse(marketPrice);
                        // *********************************************************************************

                        GetSetData.Data = productDetailGridView1.SelectedRows[0].Cells["Quantity"].Value.ToString();
                        TextData.quantity = double.Parse(GetSetData.Data);

                        string pkg = productDetailGridView1.SelectedRows[0].Cells["PKG"].Value.ToString();
                        TextData.pkg = double.Parse(pkg);

                        string full_pag = productDetailGridView1.SelectedRows[0].Cells["Tab PCS"].Value.ToString();
                        TextData.tab_pieces = double.Parse(full_pag);


                        string pur_price = productDetailGridView1.SelectedRows[0].Cells["Purchase"].Value.ToString();
                        TextData.purchase_price = double.Parse(pur_price);


                        string sale_price = productDetailGridView1.SelectedRows[0].Cells["Sale Price"].Value.ToString();
                        TextData.sale_price = double.Parse(sale_price);


                        string taxation = productDetailGridView1.SelectedRows[0].Cells["Tax %"].Value.ToString();
                        TextData.tax = double.Parse(taxation);


                        // *********************************************************************************

                        if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets" || TextData.prod_state == "pack")
                        {
                            if (TextData.pkg == 0)
                            {
                                TextData.pkg = 1;
                            }

                            if (TextData.tab_pieces == 0)
                            {
                                TextData.tab_pieces = 1;
                            }

                            TextData.quantity = (TextData.quantity / TextData.pkg) / TextData.tab_pieces;

                            if (TextData.quantity == 0)
                            {
                                TextData.quantity = 1;
                            }

                            TextData.purchase_price = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.purchase_price;
                            TextData.sale_price = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.sale_price;
                            TextData.market_value = (double.Parse(GetSetData.Data) / TextData.quantity) * double.Parse(marketPrice);
                            TextData.totalWholeSale = (double.Parse(GetSetData.Data) / TextData.quantity) * double.Parse(TextData.wholeSalePrice);
                            TextData.wholeSalePrice = TextData.totalWholeSale.ToString();

                            TextData.discount = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.discount;
                            TextData.disLimit = (double.Parse(GetSetData.Data) / TextData.quantity) * TextData.disLimit;

                            if (double.Parse(GetSetData.Data) == 0)
                            {
                                TextData.quantity = TextData.pkg * TextData.tab_pieces;
                                TextData.purchase_price = double.Parse(pur_price) * TextData.quantity;
                                TextData.sale_price = double.Parse(sale_price) * TextData.quantity;
                                TextData.market_value = double.Parse(marketPrice) * TextData.quantity;
                                TextData.totalWholeSale = double.Parse(TextData.remarks) * TextData.quantity;
                                TextData.wholeSalePrice = TextData.totalWholeSale.ToString();

                            }


                            TextData.pkg = double.Parse(pkg);
                            TextData.tab_pieces = double.Parse(full_pag);
                            TextData.quantity = double.Parse(GetSetData.Data);


                            if (TextData.tab_pieces == 0)
                            {
                                TextData.tab_pieces = 1;
                            }

                            TextData.quantity = (TextData.quantity / TextData.pkg) / TextData.tab_pieces;
                        }

                        TextData.remarks = data.UserPermissions("remarks", "pos_products", "product_id", TextData.product_id.ToString());

                        //pur_price = productDetailGridView1.SelectedRows[0].Cells["Purchase"].Value.ToString();
                        //TextData.prod_price = double.Parse(pur_price);

                        //GetSetData.SaveLogHistoryDetails("Products Details Form", "Updating item [" + TextData.prod_name + "  " + TextData.barcode + "] details (Modify button click...)", role_id);

                        using (add_new_product add_customer = new add_new_product())
                        {
                            add_new_product.user_id = user_id;
                            add_new_product.role_id = role_id;
                            add_customer.ShowDialog();
                        }

                    }
                }

                return true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                //error.errorMessage("Please select the row first!");
                //error.ShowDialog();
                return false;
            }
        }

        private void updates_product_details(object sender, DataGridViewCellEventArgs e)
        {
            //fun_update_details();
        }

        private void BlurFormBackground(Form formName)
        {
            Form winform = new Form();
            try
            {
                winform.StartPosition = FormStartPosition.Manual;
                winform.FormBorderStyle = FormBorderStyle.None;
                winform.Opacity = .70d;
                winform.BackColor = Color.Black;
                winform.WindowState = FormWindowState.Maximized;
                winform.TopMost = true;
                winform.Location = this.Location;
                winform.ShowInTaskbar = false;
                winform.Show();

                formName.Owner = winform;
                formName.ShowDialog();
                //Button_controls.Add_transaction_buttons();
                winform.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                winform.Dispose();
            }
        }

        private void addNewDetails()
        {
            using (add_new_product add_customer = new add_new_product())
            {
                add_new_product.isProductCreateFromPurchase = false;
                //GetSetData.SaveLogHistoryDetails("Products Details Form", "Add new product button click...", role_id);
                add_new_product.role_id = role_id;
                add_new_product.user_id = user_id;
                add_new_product.saveEnable = false;
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("notNull");
            search_box.Text = "";
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Products Details Form", "Print button click...", role_id);
            //TextData.prod_code = test.Text;
            productsReport list = new productsReport();
            list.Show();
        }

        private void btn_group_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Products Details Form", "Regular items button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            form_grouped_products.role_id = role_id;
            Button_controls.group_items_buttons();
            this.Dispose();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private bool fun_delete_products()
        {
            try
            {
                string product_id = productDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();

                //========================================================

                GetSetData.query = @"select sales_id from pos_sales_details where (prod_id = '" + product_id + "');";
                int sales_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"select return_id from pos_returns_details where prod_id = '" + product_id + "';";
                int return_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"select items_id from pos_purchased_items where prod_id = '" + product_id + "';";
                int purchase_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"select items_id from pos_pur_return_items where prod_id = '" + product_id + "';";
                int pur_return_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                if (sales_id_db == 0 && return_id_db == 0 && purchase_id_db == 0 && pur_return_id_db == 0)
                {
                    GetSetData.query = @"delete from pos_stock_details where (prod_id = '" + product_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.query = @"delete from pos_stock_backup where (prod_id = '" + product_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.query = @"delete from pos_stock_history where (product_id = '" + product_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"delete from pos_products where (product_id = '" + product_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================
                    //GetSetData.SaveLogHistoryDetails("Products Details Form", "Deleting item [" + TextData.prod_name + "  " + TextData.barcode + "] details", role_id);
                    
                    return true;
                }
                else
                {
                    error.errorMessage("Sorry this item cannot be deleted!");
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

        public static bool true_message(string result)
        {
            return true;
        }

        private void deleteSelectedDetails()
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            string product_id = productDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();

            try
            {
                //this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + product_id + "'");
                sure.ShowDialog();
                //this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();

                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("notNull");
                    search_box.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("'" + TextData.prod_name.ToString() + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void btn_expired_items_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Products Details Form", "Expired items button click...", role_id);
            form_expired_items.role_id = role_id;
            Button_controls.expired_items_buttons();
            this.Dispose();
        }

        private void btnShelfItems_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Products Details Form", "Shelf items button click...", role_id);
            formShelfItems.count = 1;
            Button_controls.shelf_items_buttons();
            this.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void product_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                //GetSetData.SaveLogHistoryDetails("Products Details Form", "Print button click...", role_id);
                //TextData.prod_code = test.Text;
                productsReport list = new productsReport();
                list.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                addNewDetails();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                fun_update_details();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                //GetSetData.SaveLogHistoryDetails("Products Details Form", "Regular items button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                form_grouped_products.role_id = role_id;
                Button_controls.group_items_buttons();
                this.Dispose();
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                //GetSetData.SaveLogHistoryDetails("Products Details Form", "Expired items button click...", role_id);
                form_expired_items.role_id = role_id;
                Button_controls.expired_items_buttons();
                this.Dispose();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }

        private void ProductsDetailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string product_id = productDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();

                bool can_delete_stock = bool.Parse(data.UserPermissions("products_details_delete", "pos_tbl_authorities_button_controls2", role_id));
                bool can_modify_stock = bool.Parse(data.UserPermissions("products_details_modify", "pos_tbl_authorities_button_controls2", role_id));

                clearDataGridViewItems();
                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureProductSubItems", product_id);
             
                createproductDetailButtonInGridView();
                
                if (can_modify_stock)
                {
                    createUpdateButtonInGridView();
                }

                if (can_delete_stock)
                {
                    createDeleteButtonInGridView();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void createUpdateButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Modify";
            btn.Name = "Modify";
            btn.Text = "Modify";
            btn.Width = 60;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView1.Columns.Add(btn);
        }
        private void createproductDetailButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Details";
            btn.Name = "Details";
            btn.Text = "Details";
            btn.Width = 60;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView1.Columns.Add(btn);
        }

        private void createDeleteButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Delete";
            btn.Name = "Delete";
            btn.Text = "Delete";
            btn.Width = 60;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.Red;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView1.Columns.Add(btn);
        }

        private void clearDataGridViewItems()
        {
            this.productDetailGridView1.DataSource = null;
            this.productDetailGridView1.Refresh();
            productDetailGridView1.Rows.Clear();
            productDetailGridView1.Columns.Clear();
        }

        private void productDetailGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (productDetailGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    deleteRowFromGridView();

                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("notNull");
                    search_box.Text = "";

                }
                else if (productDetailGridView1.Columns[e.ColumnIndex].Name == "Modify")
                {
                    fun_update_details();
                }
                else if (productDetailGridView1.Columns[e.ColumnIndex].Name == "Details")
                {
                    form_product_info.selectedStockID = productDetailGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    form_product_info.selectedProductID = data.UserPermissions("prod_id", "pos_stock_details", "stock_id", productDetailGridView1.SelectedRows[0].Cells[0].Value.ToString());


                    form_product_info.role_id = role_id;
                    form_product_info _obj = new form_product_info();
                    _obj.ShowDialog();
                }

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
                string stock_id = productDetailGridView1.SelectedRows[0].Cells[0].Value.ToString();


                if (stock_id != "")
                {
                    GetSetData.query = "delete from pos_stock_details where (stock_id = '" + stock_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    done.DoneMessage(stock_id + " is deleted successfully.");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        private void btnInventoryAudit_Click(object sender, EventArgs e)
        {
            formInventoryAudit.user_id = user_id;
            formInventoryAudit.role_id = role_id;
            formInventoryAudit _obj = new formInventoryAudit();
            _obj.Show();
            this.Dispose();
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            formInventoryAuditReport _obj = new formInventoryAuditReport();
            _obj.ShowDialog();
        }
    }
}
