using System;
using System.Drawing;
using System.Windows.Forms;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using System.IO;
using System.Drawing.Printing;
using System.Globalization;
using System.Diagnostics;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class add_new_product : Form
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

        public add_new_product()
        {
            InitializeComponent();
            //setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static int user_id = 0;
        public static bool saveEnable;
        public static bool isProductCreateFromPurchase = false;
        string discountValue = "";

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
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

        //    //    //****************************************************************

        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
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
                //**********************************************************************************************
                savebutton.Visible = bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id));
                btnUpdate.Visible = bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id));

                if (bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id)) == false && bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id)) == false)
                {
                    pnl_exit.Visible = false;
                }

                pnl_save.Visible = bool.Parse(data.UserPermissions("products_exit", "pos_tbl_authorities_button_controls2", role_id));
                discountValue = data.UserPermissions("discountType", "pos_general_settings");

                if (discountValue == "Yes")
                {
                    lbl_percentage.Visible = true;
                    lbl_percentage1.Visible = true;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private void fillAddProductsFormTextBoxes()
        //{
        //    try
        //    {
        //        prod_name_text.Text = TextData.prod_name;
        //        prod_code_text.Text = TextData.barcode;
        //        txt_expire_date.Text = TextData.exp_date;
        //        txt_qty.Text = TextData.quantity.ToString();
        //        txt_pkg.Text = TextData.pkg.ToString();
        //        txt_tab_pieces.Text = TextData.tab_pieces.ToString();
        //        txt_status.Text = TextData.status;
        //        cate_text.Text = TextData.category;
        //        sub_cat_text.Text = TextData.sub_category;
        //        brand_text.Text = TextData.brand;
        //        pur_price_text.Text = TextData.prod_price.ToString();
        //        sale_price_text.Text = TextData.sale_price.ToString();
        //        txt_wholeSale.Text = TextData.size;
        //        txtTaxation.Text = TextData.market_value.ToString();
        //        txtDiscount.Text = TextData.discount.ToString();
        //        txtDiscountLimit.Text = TextData.disLimit.ToString();

        //        GetSetData.query = "select product_id from pos_products where prod_name = '" + TextData.prod_name_key.ToString() + "' and barcode = '" + TextData.barcode_key.ToString() + "';";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        TextData.full_pak = data.NumericValues("full_pak", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
        //        txt_full_pag.Text = TextData.full_pak.ToString();

        //        txt_qty_alert.Text = data.UserPermissions("qty_alert", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
        //        txt_item_type.Text = data.UserPermissions("item_type", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        txt_mfg_date.Text = data.UserPermissions("manufacture_date", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        unit_text.Text = data.UserPermissions("unit", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        //txt_wholeSale.Text = data.UserPermissions("size", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        state_text.Text = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString());

        //        TextData.image_path = data.UserPermissions("picture_path", "pos_general_settings");
        //        TextData.saved_image_path = data.UserPermissions("image_path", "pos_products", "product_id", GetSetData.Ids.ToString());

        //        if (TextData.saved_image_path != "nill" && TextData.saved_image_path != "")
        //        {
        //            img_pic_box.Image = Image.FromFile(TextData.image_path + TextData.saved_image_path);
        //        }

        //        txt_remarks.Text = data.UserPermissions("remarks", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        TextData.chk_qty_alert = data.UserPermissions("alert_status", "pos_products", "product_id", GetSetData.Ids.ToString());

        //        if (TextData.chk_qty_alert == "true")
        //        {
        //            chk_active_alert.Checked = true;
        //        }
        //        else
        //        {
        //            chk_active_alert.Checked = false;
        //        }

        //        GetSetData.Ids = data.UserPermissionsIds("color_id", "pos_products", "product_id", GetSetData.Ids.ToString());
        //        txt_color.Text = data.UserPermissions("title", "pos_color", "color_id", GetSetData.Ids.ToString());
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                cate_text.Text = TextData.category;
                sub_cat_text.Text = TextData.sub_category;
                brand_text.Text = TextData.brand;
                prod_name_text.Text = TextData.prod_name;
                prod_code_text.Text = TextData.barcode;
                txt_expire_date.Text = TextData.exp_date;
                txt_mfg_date.Text = TextData.mfg_date;
                txt_qty.Text = TextData.quantity.ToString();
                txt_pkg.Text = TextData.pkg.ToString();
                txt_tab_pieces.Text = TextData.tab_pieces.ToString();
                txt_status.Text = TextData.status;
                pur_price_text.Text = TextData.purchase_price.ToString();
                sale_price_text.Text = TextData.sale_price.ToString();
                txt_wholeSale.Text = TextData.wholeSalePrice;
                txtDiscount.Text = TextData.discount.ToString();
                txtDiscountLimit.Text = TextData.disLimit.ToString();
                txtTaxation.Text = TextData.tax.ToString();
                txt_remarks.Text = TextData.remarks;
                state_text.Text = TextData.prod_state;
                unit_text.Text = TextData.prod_unit;
                txt_qty_alert.Text = TextData.qty_alert;
                txt_full_pag.Text = TextData.full_pak.ToString();
                chk_active_alert.Checked = bool.Parse(TextData.chk_qty_alert);
                switchPromotion.Checked = bool.Parse(TextData.switchPromotion);


                TextData.image_path = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.saved_image_path = data.UserPermissions("image_path", "pos_stock_details", "stock_id", TextData.stock_id.ToString());

                if (TextData.saved_image_path != "nill" && TextData.saved_image_path != "")
                {
                    img_pic_box.Image = Image.FromFile(TextData.image_path + TextData.saved_image_path);
                }
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
                txt_pkg.ReadOnly = false;
                title_lable.Text = "Update Product Details";

                fillAddProductsFormTextBoxes();
                modify_show_barcode();
            }
            else if (saveEnable == false)
            {
                btnUpdate.Visible = false;
                title_lable.Text = "Create New Product";
                cate_text.Select();
            }
        }

        private void clearGridView()
        {
            try
            {
                int a = productListGridView.Rows.Count;

                for (int i = 0; i < a; i++)
                {
                    foreach (DataGridViewRow row in productListGridView.SelectedRows)
                    {

                        productListGridView.Rows.Remove(row);
                    }
                }
                productListGridView.DataSource = null;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void RefreshFieldCategory()
        {
            GetSetData.FillComboBoxUsingProcedures(cate_text, "fillComboBoxCategory", "title");
        }

        private void RefreshFieldBrand()
        {
            GetSetData.FillComboBoxUsingProcedures(brand_text, "fillComboBoxBrand", "brand_title");
        }

        private void RefreshFieldSubCategory()
        {
           GetSetData.FillComboBoxUsingProcedures(sub_cat_text, "fillComboBoxSubCategory", "title");
        }

        private void RefreshFieldColor()
        {
            GetSetData.FillComboBoxUsingProcedures(txt_color, "fillComboBoxColors", "title");
        }

        private void resetForm()
        {
            txt_pkg.ReadOnly = true;
            txt_full_pag.ReadOnly = true;
            prod_name_text.Text = "";
            prod_code_text.Text = "";
            txt_full_pag.Text = "0";
            txt_pkg.Text = "0";
            txt_qty.Text = "0";
            txt_tab_pieces.Text = "0";
            unit_text.Text = "kg";
            state_text.Text = "pieces";

            txt_qty_alert.Text = "5";
            chk_active_alert.Checked = true;
            txt_wholeSale.Text = "0";
            txt_status.Text = "Enabled";
            txt_item_type.Text = "Inventory";
            img_pic_box.Image = null;
            txt_remarks.Text = "";
            pur_price_text.Text = "0";
            sale_price_text.Text = "0";
            TextData.total_pur_price = 0;
            TextData.total_sale_price = 0;
            txtDiscount.Text = "0";
            txtDiscountLimit.Text = "0";

            string get_taxation_db = @"select taxation from pos_general_settings;";
            txtTaxation.Text = data.SearchStringValuesFromDb(get_taxation_db);
            
            string get_promotionDiscount_db = @"select promotionDiscount from pos_general_settings;";
            txtDiscount.Text = data.SearchStringValuesFromDb(get_promotionDiscount_db);

            cate_text.Select();
        }


        private void ResetComboBoxItems()
        {
            //GetSetData.FillComboBoxUsingProcedures(prod_name_text, "fillComboBoxProductNames", "prod_name");
            RefreshFieldCategory();
            RefreshFieldBrand();
            RefreshFieldSubCategory();
            RefreshFieldColor();
        }

        private void refresh()
        {
            try
            {
                txt_mfg_date.Text = DateTime.Now.ToLongDateString();
                txt_expire_date.Text = DateTime.Now.ToLongDateString();
                
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
            //GetSetData.SaveLogHistoryDetails("Add New Product Form", "Exit...", role_id);
            clearGridView();
            this.Close();
        }

        private void FillValuesFromTextFieldsToVariables()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.prod_name = prod_name_text.Text;
                TextData.barcode = prod_code_text.Text;
                TextData.mfg_date = txt_mfg_date.Text;
                TextData.exp_date = txt_expire_date.Text;
                TextData.quantity = double.Parse(txt_qty.Text);
                TextData.pkg = double.Parse(txt_pkg.Text);
                TextData.full_pak = double.Parse(txt_full_pag.Text);
                TextData.tab_pieces = double.Parse(txt_tab_pieces.Text);
                TextData.prod_unit = unit_text.Text;
                TextData.prod_state = state_text.Text;
                TextData.color = txt_color.Text;
                TextData.size = txt_wholeSale.Text;
                TextData.status = txt_status.Text;
                TextData.item_type = txt_item_type.Text;
                TextData.qty_alert = txt_qty_alert.Text;
                TextData.category = cate_text.Text;
                TextData.sub_category = sub_cat_text.Text;
                TextData.brand = brand_text.Text;
                TextData.saved_image_path = fun_saved_image();
                TextData.remarks = txt_remarks.Text;
                TextData.discount = double.Parse(txtDiscount.Text);
                TextData.disLimit = double.Parse(txtDiscountLimit.Text);

                TextData.market_value = double.Parse(txtTaxation.Text);
                TextData.prod_price = double.Parse(pur_price_text.Text);
                TextData.sale_price = double.Parse(sale_price_text.Text);

                if (chk_active_alert.Checked == true)
                {
                    TextData.chk_qty_alert = "true";
                }
                else
                {
                    TextData.chk_qty_alert = "false";
                }


                if (switchPromotion.Checked == true)
                {
                    TextData.switchPromotion = "true";
                }
                else
                {
                    TextData.switchPromotion = "false";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool add_records_Grid_view()
        {
            try
            {
                FillValuesFromTextFieldsToVariables();

                if (TextData.category == "")
                {
                    TextData.category = "others";
                }

                if (TextData.sub_category == "")
                {
                    TextData.sub_category = "others";
                }

                if (TextData.brand == "")
                {
                    TextData.brand = "others";
                }

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }

                if (TextData.color == "-select-" || TextData.color == "")
                {
                    TextData.color = "none";
                }

                if (TextData.quantity == 0)
                {
                    TextData.quantity = 1;   
                }


                if (discountValue == "Yes")
                {
                    TextData.discount = (TextData.sale_price * TextData.discount) / 100;
                    TextData.disLimit = (TextData.sale_price * TextData.disLimit) / 100;
                }


                int subcategory_id_db = data.UserPermissionsIds("sub_cate_id", "pos_subcategory", "title", TextData.sub_category);
                //========================================================================================================
                int brand_id_db = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", TextData.brand);
                //========================================================================================================

                //GetSetData.query = @"select barcode from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id where (pos_products.barcode = '" + TextData.barcode.ToString() + "') and (pos_products.sub_cate_id = '" + subcategory_id_db.ToString() + "') and (pos_products.brand_id = '" + brand_id_db.ToString() + "') and (pos_stock_details.quantity != 0);";
                //string product_code_db = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select barcode from pos_products where (barcode = '" + TextData.barcode.ToString() + "');";
                string product_code_db = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select pos_products.product_id from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id where (prod_name = '" + TextData.prod_name.ToString() + "') and (pos_products.barcode = '" + TextData.barcode.ToString() + "')  and (pos_products.sub_cate_id = '" + subcategory_id_db.ToString() + "') and (pos_products.brand_id = '" + brand_id_db.ToString() + "') and (pos_stock_details.quantity != 0)";
                int product_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                if (img_pic_box.Image == null)
                {
                    TextData.saved_image_path = "nill";
                }


                if (prod_code_text.Text == "")
                {
                    generate_barcode();
                }

                string temp_name = "";
                string temp_barcode = "";
                string temp_brand = "";
                string temp_sub_category = "";

                for (int i = 0; i < productListGridView.Rows.Count; i++)
                {
                    temp_name = productListGridView.Rows[i].Cells[1].Value.ToString();
                    temp_barcode = productListGridView.Rows[i].Cells[2].Value.ToString();
                    temp_brand = productListGridView.Rows[i].Cells[4].Value.ToString();
                    temp_sub_category = productListGridView.Rows[i].Cells[5].Value.ToString();

                    if (TextData.prod_name == temp_name && TextData.barcode == temp_barcode && TextData.brand == temp_brand && TextData.sub_category == temp_sub_category )
                    {
                        temp_name = TextData.prod_name;
                        temp_barcode = TextData.barcode;
                        temp_brand = TextData.brand;
                        temp_sub_category = TextData.sub_category;

                        break;
                    }
                }


                string get_image_path_db = @"select picture_path from pos_general_settings;";
                string image_path_db = data.SearchStringValuesFromDb(get_image_path_db);

                if (TextData.saved_image_path != "" && TextData.saved_image_path != image_path_db && TextData.saved_image_path != null)
                {
                    if (product_id_db == 0)
                    {
                        if (product_code_db != TextData.barcode)
                        {
                            if (prod_name_text.Text != "" && TextData.prod_name != "")
                            {
                                if (prod_code_text.Text != "" && TextData.barcode != "")
                                {
                                    if (pur_price_text.Text != "")
                                    {
                                        if (sale_price_text.Text != "")
                                        {
                                            if (txtDiscount.Text != "")
                                            {
                                                if (state_text.Text == "carton" || state_text.Text == "bag" || state_text.Text == "Tablets" || state_text.Text == "pack")
                                                {
                                                    if (state_text.Text == "carton" || state_text.Text == "bag" || state_text.Text == "pack")
                                                    {
                                                        TextData.tab_pieces = 1;
                                                    }

                                                    // Calculating the total quantity
                                                    TextData.full_pak = (TextData.quantity * TextData.pkg) * TextData.tab_pieces;
                                                    TextData.tab_pieces = double.Parse(txt_qty.Text);
                                                    TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                    TextData.prod_price = TextData.total_pur_price / TextData.full_pak; // calculating the purchase price of per piece

                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                    TextData.sale_price = TextData.total_sale_price / TextData.full_pak; // calculating the sale price of per piece

                                                    TextData.totalWholeSale = double.Parse(TextData.size) * TextData.quantity;
                                                    TextData.totalWholeSale = TextData.totalWholeSale / TextData.full_pak; // calculating the sale price of per piece
                                                    TextData.size = TextData.totalWholeSale.ToString();
                                                    //TextData.market_value = TextData.market_value * TextData.quantity;
                                                    //TextData.market_value = TextData.market_value / TextData.full_pak;
                                                    TextData.discount = TextData.discount * TextData.quantity;
                                                    TextData.discount = TextData.discount / TextData.full_pak;
                                                    TextData.disLimit = TextData.disLimit * TextData.quantity;
                                                    TextData.disLimit = TextData.disLimit / TextData.full_pak;
                                                    TextData.quantity = double.Parse(txt_qty.Text);
                                                }
                                                else
                                                {
                                                    TextData.quantity = double.Parse(txt_qty.Text);
                                                    TextData.pkg = 0;
                                                    TextData.full_pak = 0;
                                                    TextData.tab_pieces = 0;

                                                    TextData.prod_price = double.Parse(pur_price_text.Text);
                                                    TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                                    TextData.sale_price = double.Parse(sale_price_text.Text);
                                                    TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                                    TextData.quantity = double.Parse(txt_qty.Text);
                                                    //TextData.discount = double.Parse(txtDiscount.Text);
                                                    //TextData.disLimit = double.Parse(txtDiscountLimit.Text);
                                                }


                                                if ((temp_name != TextData.prod_name) || (temp_barcode != TextData.barcode) || (temp_brand != TextData.brand) || (temp_sub_category != TextData.sub_category))
                                                {
                                                    int is_category_exist = data.UserPermissionsIds("category_id", "pos_category", "title", TextData.category);
                                                    //========================================================================================================
                                                    int is_brand_exist = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", TextData.brand);
                                                    //========================================================================================================

                                                    if (is_category_exist != 0)
                                                    { 
                                                        if (is_brand_exist != 0)
                                                        {
                                                            int n = productListGridView.Rows.Add();

                                                            productListGridView.Rows[n].Cells[1].Value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TextData.prod_name);
                                                            productListGridView.Rows[n].Cells[2].Value = TextData.barcode.ToString();
                                                            productListGridView.Rows[n].Cells[3].Value = TextData.category.ToString();
                                                            productListGridView.Rows[n].Cells[4].Value = TextData.brand.ToString();
                                                            productListGridView.Rows[n].Cells[5].Value = TextData.sub_category.ToString();
                                                            productListGridView.Rows[n].Cells[6].Value = TextData.mfg_date.ToString();
                                                            productListGridView.Rows[n].Cells[7].Value = TextData.exp_date.ToString();
                                                            productListGridView.Rows[n].Cells[8].Value = TextData.quantity.ToString();
                                                            productListGridView.Rows[n].Cells[9].Value = TextData.pkg.ToString();
                                                            productListGridView.Rows[n].Cells[10].Value = txt_tab_pieces.Text;
                                                            productListGridView.Rows[n].Cells[11].Value = TextData.tab_pieces.ToString();
                                                            productListGridView.Rows[n].Cells[12].Value = TextData.prod_state.ToString();
                                                            productListGridView.Rows[n].Cells[13].Value = TextData.prod_unit.ToString();
                                                            productListGridView.Rows[n].Cells[14].Value = TextData.color.ToString();
                                                            productListGridView.Rows[n].Cells[15].Value = TextData.size.ToString();
                                                            productListGridView.Rows[n].Cells[16].Value = TextData.item_type.ToString();
                                                            productListGridView.Rows[n].Cells[17].Value = TextData.status.ToString();
                                                            productListGridView.Rows[n].Cells[18].Value = TextData.qty_alert.ToString();
                                                            productListGridView.Rows[n].Cells[19].Value = TextData.chk_qty_alert.ToString();
                                                            productListGridView.Rows[n].Cells[20].Value = TextData.market_value.ToString();
                                                            productListGridView.Rows[n].Cells[21].Value = TextData.prod_price.ToString();
                                                            productListGridView.Rows[n].Cells[22].Value = TextData.sale_price.ToString();
                                                            productListGridView.Rows[n].Cells[23].Value = TextData.discount.ToString();
                                                            productListGridView.Rows[n].Cells[24].Value = TextData.disLimit.ToString();
                                                            productListGridView.Rows[n].Cells[25].Value = TextData.saved_image_path.ToString();
                                                            productListGridView.Rows[n].Cells[26].Value = TextData.remarks.ToString();
                                                            productListGridView.Rows[n].Cells[27].Value = TextData.switchPromotion.ToString();

                                                            return true;
                                                        }
                                                        else
                                                        {
                                                            error.errorMessage("Brand (" + TextData.brand + ") is not found! Please select the brand first.");
                                                            error.ShowDialog();

                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        error.errorMessage("Category (" + TextData.category + ") is not found! Please select the category first.");
                                                        error.ShowDialog();

                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    error.errorMessage("'" + TextData.prod_name + "' is already exist!");
                                                    error.ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                error.errorMessage("Please enter Discount!");
                                                error.ShowDialog();
                                            }
                                        }
                                        else
                                        {
                                            error.errorMessage("Please enter Sale Price!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter Purchase Price!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter Barcode!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter Product Name!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage(TextData.barcode.ToString() + " is already exist!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage(TextData.prod_name.ToString() + " is already exist!");
                        error.ShowDialog();
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                error.errorMessage(ex.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            if (add_records_Grid_view())
            {
                refresh();
                resetForm();
                ResetComboBoxItems();
            }
        }

        private bool insert_records_into_db()
        {
            try
            {
                for (int i = 0; i < productListGridView.Rows.Count; i++)
                {
                    int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", productListGridView.Rows[i].Cells[3].Value.ToString());
                    //========================================================================================================
                    int subcategory_id_db = data.UserPermissionsIds("sub_cate_id", "pos_subcategory", "title", productListGridView.Rows[i].Cells[5].Value.ToString());
                    //========================================================================================================
                    int brand_id_db = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", productListGridView.Rows[i].Cells[4].Value.ToString());
                    //========================================================================================================
                    int color_id_db = data.UserPermissionsIds("color_id", "pos_color", "title", productListGridView.Rows[i].Cells[14].Value.ToString());
                    //========================================================================================================


                    // Insert Data From GridView to SaleItems in Database:  ** (prod_name, barcode, manufacture_date, expiry_date, prod_state, unit, item_type, size, image_path, status, remarks, category_id, brand_id, sub_cate_id, color_id) **
                    GetSetData.query = @"insert into pos_products values ('" + productListGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[2].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[12].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[13].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[16].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[15].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[25].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[17].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[26].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[27].Value.ToString() + "' , '" + category_id_db.ToString() + "' , '" + brand_id_db.ToString() + "' , '" + subcategory_id_db.ToString() + "' , '" + color_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"select product_id from pos_products where (prod_name = '" + productListGridView.Rows[i].Cells[1].Value.ToString() + "') and (barcode = '" + productListGridView.Rows[i].Cells[2].Value.ToString() + "') and (brand_id = '" + brand_id_db.ToString() + "') and (sub_cate_id = '" + subcategory_id_db.ToString() + "');";
                    int prod_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (prod_id_db != 0)
                    {
                        TextData.prod_price = double.Parse(productListGridView.Rows[i].Cells[21].Value.ToString());
                        TextData.sale_price = double.Parse(productListGridView.Rows[i].Cells[22].Value.ToString());
                        TextData.quantity = double.Parse(productListGridView.Rows[i].Cells[8].Value.ToString());
                        TextData.pkg = double.Parse(productListGridView.Rows[i].Cells[9].Value.ToString());
                        TextData.tab_pieces = double.Parse(productListGridView.Rows[i].Cells[10].Value.ToString());
                        TextData.prod_state = productListGridView.Rows[i].Cells[12].Value.ToString();
                        // ******************************************************************************************

                        if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets" || TextData.prod_state == "pack")
                        {
                            if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "pack")
                            {
                                TextData.tab_pieces = 1;
                            }

                            TextData.quantity = (TextData.quantity * TextData.pkg) * TextData.tab_pieces;
                        }

                        TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;

                        GetSetData.Ids = data.UserPermissionsIds("stock_id", "pos_stock_details", "prod_id", prod_id_db.ToString());
                        if (GetSetData.Ids == 0)
                        {
                            // Insert Data From GridView to pos_stock in Database:
                            GetSetData.query = @"insert into pos_stock_details values ('"+ productListGridView.Rows[i].Cells[2].Value.ToString() + "','" + TextData.quantity.ToString() + "' , '" + productListGridView.Rows[i].Cells[9].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[10].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[11].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[21].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[22].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[20].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[15].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[7].Value.ToString() + "' , '0' , '0' , '" + TextData.total_pur_price.ToString() + "' , '" + TextData.total_sale_price.ToString() + "' , '" + productListGridView.Rows[i].Cells[18].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[19].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[23].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[24].Value.ToString() + "' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                            
                            
                            GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "','" + TextData.quantity.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + productListGridView.Rows[i].Cells[21].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[21].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[22].Value.ToString() + "' , '" + productListGridView.Rows[i].Cells[22].Value.ToString() + "' , 'Create New Inventory' , '" + user_id.ToString() + "' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }

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
            int numnberOfRows = productListGridView.RowCount;

            if (numnberOfRows > 0)
            {
                if (insert_records_into_db())
                {
                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();

                    clearGridView();
                    resetForm();
                    refresh();
                    ResetComboBoxItems();
                }
            }
            else
            {
                error.errorMessage("Please add the product details in cart before saving!");
                error.ShowDialog();
            }
        }
        
        private bool update_records_db()
        {
            try
            {
                FillValuesFromTextFieldsToVariables();

                if (TextData.discount == 0)
                {
                    TextData.discount = 1;
                }

                if (TextData.disLimit == 0)
                {
                    TextData.disLimit = 1;
                }

                if (discountValue == "Yes")
                {
                    TextData.discount = (TextData.sale_price * TextData.discount) / 100;
                    TextData.disLimit = (TextData.sale_price * TextData.disLimit) / 100;
                }

                if (img_pic_box.Image == null)
                {
                    TextData.saved_image_path = "nill";
                }


                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                if (TextData.saved_image_path == GetSetData.Data || TextData.saved_image_path == null || TextData.saved_image_path == "")
                {
                    GetSetData.query = @"select image_path from pos_products where prod_name = '" + TextData.prod_name_key.ToString() + "' and barcode = '" + TextData.barcode_key.ToString() + "';";
                    TextData.saved_image_path = data.SearchStringValuesFromDb(GetSetData.query);

                    if (TextData.saved_image_path == "")
                    {
                        TextData.saved_image_path = "nill";
                    }
                }

                if (prod_name_text.Text != "" && TextData.prod_name != "")
                {
                    if (prod_code_text.Text != "" && TextData.barcode != "")
                    {
                        if (pur_price_text.Text != "")
                        {
                            if (sale_price_text.Text != "")
                            {
                                if (txt_qty.Text != "")
                                {
                                    int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", TextData.category);
                                    //========================================================
                                    int brand_id_db = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", TextData.brand);
                                    //========================================================
                                    int sub_category_id_db = data.UserPermissionsIds("sub_cate_id", "pos_subcategory", "title", TextData.sub_category);
                                    //========================================================
                                    int color_id_db = data.UserPermissionsIds("color_id", "pos_color", "title", TextData.color);
                                    //========================================================


                                    if (state_text.Text == "carton" || state_text.Text == "bag" || state_text.Text == "Tablets" || state_text.Text == "pack")
                                    {
                                        if (TextData.pkg == 0)
                                        {
                                            TextData.pkg = 1;
                                        }

                                        if (TextData.tab_pieces == 0)
                                        {
                                            TextData.tab_pieces = 1;
                                        }

                                        if (TextData.quantity == 0)
                                        {
                                            TextData.quantity = 1;
                                        }

                                        // Calculating the total quantity
                                        TextData.full_pak = (TextData.quantity * TextData.pkg) * TextData.tab_pieces; // total quantity
                                        TextData.total_pur_price = TextData.prod_price * TextData.quantity; // total purchase price
                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity; // total sale price
                                        TextData.totalMarketPrice = TextData.market_value * TextData.quantity; // total Market price
                                        TextData.totalWholeSale = double.Parse(TextData.size) * TextData.quantity; // total whole sale price
                                        TextData.discount = TextData.discount * TextData.quantity; // total discount
                                        TextData.disLimit = TextData.disLimit * TextData.quantity; // total discount Limit

                                        if (TextData.full_pak == 0)
                                        {
                                            TextData.full_pak = 1;
                                        }

                                        TextData.prod_price = TextData.total_pur_price / TextData.full_pak; // calculating the purchase price of per piece
                                        TextData.sale_price = TextData.total_sale_price / TextData.full_pak; // calculating the sale price of per piece
                                        TextData.market_value = TextData.totalMarketPrice / TextData.full_pak; // calculating the sale price of per piece
                                        TextData.totalWholeSale = TextData.totalWholeSale / TextData.full_pak; // calculating the sale price of per piece
                                        TextData.size = TextData.totalWholeSale.ToString();
                                        TextData.discount = TextData.discount / TextData.full_pak;
                                        TextData.disLimit = TextData.disLimit / TextData.full_pak; 
                                        TextData.quantity = double.Parse(txt_qty.Text);
                                        TextData.quantity = (TextData.quantity * TextData.pkg) * TextData.tab_pieces; // total quantity // store the total quantity in quantity variable
                                        txt_full_pag.Text = txt_qty.Text; // full paking 
                                        TextData.pkg = double.Parse(txt_pkg.Text);
                                        TextData.tab_pieces = double.Parse(txt_tab_pieces.Text);
                                        TextData.total_pur_price = TextData.prod_price * TextData.quantity; // total purchase price
                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity; // total sale price
                                        //TextData.totalMarketPrice = TextData.market_value * TextData.quantity; // total sale price
                                    }
                                    else
                                    {
                                        txt_pkg.Text = "0";
                                        TextData.quantity = double.Parse(txt_qty.Text);
                                        TextData.pkg = 0;
                                        TextData.full_pak = 0;
                                        TextData.tab_pieces = 0;
                                        txt_full_pag.Text = "0";

                                        TextData.market_value = double.Parse(txtTaxation.Text);
                                        TextData.prod_price = double.Parse(pur_price_text.Text);
                                        TextData.total_pur_price = TextData.prod_price * TextData.quantity;
                                        TextData.sale_price = double.Parse(sale_price_text.Text);
                                        TextData.total_sale_price = TextData.sale_price * TextData.quantity;
                                    }

                                    if (txtDiscount.Text == "0")
                                    {
                                        TextData.discount = double.Parse(txtDiscount.Text);
                                    }

                                    if (txtDiscountLimit.Text == "0")
                                    {
                                        TextData.disLimit = double.Parse(txtDiscountLimit.Text);
                                    }

                                    GetSetData.query = @"update pos_products set prod_name = '" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prod_name_text.Text) + "', barcode = '" + prod_code_text.Text + "', expiry_date = '" + TextData.exp_date.ToString() + "', prod_state = '" + TextData.prod_state.ToString() + "', unit = '" + TextData.prod_unit.ToString() + "', item_type = '" + TextData.item_type.ToString() + "' , size  = '" + TextData.size.ToString() + "', image_path = '" + TextData.saved_image_path.ToString() + "', status = '" + TextData.status.ToString() + "', remarks = '" + TextData.remarks.ToString() + "', allow_promotion = '" + TextData.switchPromotion.ToString() + "', category_id = '" + category_id_db.ToString() + "', brand_id = '" + brand_id_db.ToString() + "', sub_cate_id = '" + sub_category_id_db.ToString() + "' where (product_id = '" + TextData.product_id.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                    //========================================================

                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", TextData.stock_id.ToString());
                                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", TextData.stock_id.ToString());
                                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", TextData.stock_id.ToString());


                                    GetSetData.query = @"update pos_stock_details set item_barcode = '" + prod_code_text.Text + "', quantity = '" + TextData.quantity.ToString() + "', pkg = '" + TextData.pkg.ToString() + "' , tab_pieces = '" + TextData.tab_pieces + "' , full_pak = '" + txt_full_pag.Text + "' , pur_price = '" + TextData.prod_price.ToString() + "', sale_price = '" + TextData.sale_price.ToString() + "', market_value = '" + TextData.market_value.ToString() + "' , whole_sale_price  = '" + TextData.size.ToString() + "', date_of_manufacture = '" + TextData.mfg_date.ToString() + "' , date_of_expiry = '" + TextData.exp_date.ToString() + "', total_pur_price  = '" + TextData.total_pur_price.ToString() + "', total_sale_price = '" + TextData.total_sale_price.ToString() + "', discount = '" + TextData.discount.ToString() + "', discount_limit = '" + TextData.disLimit.ToString() + "',  qty_alert = '" + TextData.qty_alert.ToString() + "', alert_status = '" + TextData.chk_qty_alert.ToString() + "'   where (stock_id = '" + TextData.stock_id.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);


                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + TextData.quantity.ToString() + "' , '" + oldQuantityDB + "' , '" + TextData.prod_price.ToString() + "' , '" + oldCostPriceDB + "' , '" + TextData.sale_price.ToString() + "' , '" + oldSalePriceDB + "' , 'Adjustment of Inventory' , '" + user_id.ToString() +"' , '" + TextData.product_id.ToString() +"');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);


                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter Quantity!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter Sale Price!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter Purchase Price!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter Barcode!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter Product Name!");
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
            if (update_records_db())
            {
                done.DoneMessage("Successfully Updated!");
                done.ShowDialog();
            }
        }

        private void add_supplier_Click(object sender, EventArgs e)
        {
            using (add_brand add_customer = new add_brand())
            {
                add_brand.role_id = role_id;
                add_brand.brand = brand_text.Text;
                add_customer.ShowDialog();
                RefreshFieldBrand();
            }
        }

        private void add_category_Click(object sender, EventArgs e)
        {
            using (add_category add_customer = new add_category())
            {
                add_category.role_id = role_id;
                add_customer.ShowDialog();

                TextData.category = "";
                TextData.category = cate_text.Text;

                RefreshFieldCategory();
            }
        }

        private void add_sub_category_Click(object sender, EventArgs e)
        {
            using (new_sub_category add_customer = new new_sub_category())
            {
                new_sub_category.role_id = role_id;
                new_sub_category.sub_category = sub_cat_text.Text;
                add_customer.ShowDialog();
                RefreshFieldSubCategory();
            }
        }

        private void modify_show_barcode()
        {
            try
            {
                if (prod_code_text.Text != "")
                {
                    string currency = data.UserPermissions("currency", "pos_general_settings");

                    Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    img_barcode.Image = brcode.Draw(prod_code_text.Text, 60);
                    lbl_barcode.Text = prod_code_text.Text + "    " + currency + sale_price_text.Text;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
      
        private void add_new_product_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void add_new_product_Shown(object sender, EventArgs e)
        {
            refresh();

            if (isProductCreateFromPurchase)
            {
                txt_qty.Enabled = false;
            }
            else
            {
                txt_qty.Enabled = true;
            }
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            clearGridView();
            resetForm();
            refresh();
            ResetComboBoxItems();
        }

        private void p_price_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(pur_price_text.Text, e);
        }

        private void sale_price_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(sale_price_text.Text, e);
        }

        private void min_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_qty_alert.Text, e);
        }

        private void grams_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_wholeSale.Text, e);
        }

        private void qty_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_qty.Text, e);
        }

        private void p_code_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void state_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (state_text.Text == "carton" || state_text.Text == "bag" || state_text.Text == "Tablets" || state_text.Text == "pack")
                {
                    txt_pkg.ReadOnly = false;
                    txt_full_pag.ReadOnly = false;
                    txt_tab_pieces.ReadOnly = false;
                }
                else
                {
                    txt_pkg.ReadOnly = true;
                    txt_full_pag.ReadOnly = true;
                    txt_tab_pieces.ReadOnly = true;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void market_value_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtTaxation.Text, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
        }

        private void add_color_Click(object sender, EventArgs e)
        {
            using (form_color add_customer = new form_color())
            {
                form_color.role_id = role_id;
                form_color.colorValue = txt_color.Text;
                add_customer.ShowDialog();
                RefreshFieldColor();
            }
        }

        private void fun_upload_image()
        {
            TextData.image_path = "";

            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Images only. |*.jpg; *.jpeg; *.png; *.gif;";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    TextData.image_path = open.FileName;
                    img_pic_box.Image = new Bitmap(open.FileName);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private string fun_saved_image()
        {
            TextData.saved_image_path = "";

            try
            {
                if (img_pic_box.Image != null && TextData.image_path != "" && TextData.image_path != null)
                {
                    GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                    GetSetData.Permission= GetSetData.Data + Path.GetFileName(TextData.image_path);
                    TextData.saved_image_path = GetSetData.Data;

                    if (TextData.image_path != GetSetData.Permission)
                    {
                        File.Copy(TextData.image_path, Path.Combine(TextData.saved_image_path, Path.GetFileName(TextData.image_path)), true);
                    }
                    TextData.saved_image_path = Path.GetFileName(TextData.image_path);
                }

                return TextData.saved_image_path;
            }
            catch (Exception es)
            {
                error.errorMessage("'" + Path.GetFileName(TextData.image_path) + "' is already exist! \nPlease change file name");
                error.ShowDialog();
                return TextData.saved_image_path;
            }
        }

        private void upload_logo_Click(object sender, EventArgs e)
        {
            //this.Opacity = .850;
            fun_upload_image();
            //this.Opacity = .999;
        }

        private string auto_generate_code(string condition)
        {
            TextData.barcode = "";

            try
            {
                GetSetData.query = @"SELECT top 1 productCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set productCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //**********************************************************************************************

                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.values = "P100000000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.values = "P10000000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.values = "P1000000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.values = "P100000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.values = "P10000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.values = "P1000";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.values = "P100";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.values = "P10";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.values = "P1";
                    TextData.barcode = GetSetData.values + GetSetData.Ids.ToString();
                }

                return TextData.barcode;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.barcode;
            }
        }

        private void generate_barcode()
        {
            try
            {
                TextData.barcode = "";
                prod_code_text.Text = "";

                if (prod_code_text.Text == "")
                {
                    prod_code_text.Text = auto_generate_code("");
                    TextData.barcode = prod_code_text.Text;
                }

                if (prod_code_text.Text != "")
                {
                    string currency = data.UserPermissions("currency", "pos_general_settings");

                    Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    img_barcode.Image = brcode.Draw(TextData.barcode, 60);
                    lbl_barcode.Text = prod_code_text.Text + "    " + currency + sale_price_text.Text;
                    //GetSetData.SaveLogHistoryDetails("Add New Product Form", "Generating [" + prod_name_text.Text + "  " + prod_code_text.Text + "] barcode", role_id);
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

        private void generate_barcode_Click(object sender, EventArgs e)
        {
            generate_barcode();
        }

        private void fun_print_barcode()
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                PrintDocument doc = new PrintDocument();

                doc.PrinterSettings.Copies = 1;

                doc.PrintPage += Doc_PrintPage;
                pd.Document = doc;

                if (pd.ShowDialog() == DialogResult.OK)
                {
                    doc.Print();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                string currency = data.UserPermissions("currency", "pos_general_settings");

                PointF point = new PointF(23f, 60f);
                Font font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
                SolidBrush black = new SolidBrush(Color.Black);
              
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                e.Graphics.DrawString(GetSetData.Data, font, Brushes.Black, 6, 12);
            
                Bitmap bm = new Bitmap(img_barcode.Width, img_barcode.Height);
                img_barcode.DrawToBitmap(bm, new Rectangle(0, 0, img_barcode.Width, img_barcode.Height));
             
                e.Graphics.DrawImage(bm, 6, 25);
                font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
                e.Graphics.DrawString(prod_name_text.Text, font, Brushes.Black, 6, 82);
                e.Graphics.DrawString(prod_code_text.Text + "    "  + currency + sale_price_text.Text, font, Brushes.Black, 6, 90);
                bm.Dispose();

                //GetSetData.SaveLogHistoryDetails("Add New Product Form", "Printing [" + prod_name_text.Text + "  " + prod_code_text.Text + "] barcode sticker", role_id);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void printBarcodeLabel()
        {
            //this.Opacity = .850;
            //generate_barcode();
            if (img_barcode.Image != null)
            {
                if (sale_price_text.Text != "")
                {
                    fun_print_barcode();
                }
                else
                {
                    error.errorMessage("Please enter the sale price!");
                    error.ShowDialog();
                }
            }
            else
            {
                error.errorMessage("Please generate the barcode first!");
                error.ShowDialog();
            }
            //this.Opacity = .999;
        }

        private void print_barocde_Click(object sender, EventArgs e)
        {
            printBarcodeLabel();   
        }

        private void prod_code_text_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (sale_price_text.Text != "" && prod_code_text.Text != "" && img_barcode.Image != null)
                {
                    string currency = data.UserPermissions("currency", "pos_general_settings");

                    lbl_barcode.Text = prod_code_text.Text + "    " + currency + sale_price_text.Text;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void fillItemsInGridView()
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
            try
            {
                if (productListGridView.Columns[e.ColumnIndex].Name == "Delete")
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

        private bool fun_delete_category()
        {
            try
            {
                if (cate_text.Text != "")
                {
                    GetSetData.query = @"delete from pos_category where title = '" + cate_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Please enter category title first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(cate_text.Text + " cannot be deleted!");
                error.ShowDialog();
                return false;
            }
        }

        private bool fun_delete_brand()
        {
            try
            {
                if (brand_text.Text != "")
                {
                    GetSetData.query = @"delete from pos_brand where brand_title = '" + brand_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Please enter brand title first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(brand_text.Text + " cannot be deleted!");
                error.ShowDialog();
                return false;
            }
        }

        private bool fun_delete_Subcategory()
        {
            try
            {
                if (sub_cat_text.Text != "")
                {
                    GetSetData.query = @"delete from pos_subcategory where title = '" + sub_cat_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Please enter sub-category title first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(sub_cat_text.Text + " cannot be deleted!");
                error.ShowDialog();
                return false;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscount.Text, e);
        }

        private void txtDiscountLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscountLimit.Text, e);
        }

        private void txtPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPercentage.Text, e);
        }

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            if (sale_price_text.Text != "")
            {
                if (txtPercentage.Text != "")
                {
                    TextData.total_sale_price = ((double.Parse(sale_price_text.Text) * double.Parse(txtPercentage.Text)) / 100);
                    TextData.prod_price = double.Parse(sale_price_text.Text) - TextData.total_sale_price;
                    pur_price_text.Text = TextData.prod_price.ToString();
                }
            }
        }

        private void cate_text_Enter(object sender, EventArgs e)
        {
            RefreshFieldCategory();
        }

        private void brand_text_Enter(object sender, EventArgs e)
        {
            RefreshFieldBrand();
        }

        private void sub_cat_text_Enter(object sender, EventArgs e)
        {
            RefreshFieldSubCategory();
        }

        private void prod_name_text_Enter(object sender, EventArgs e)
        {
            //GetSetData.FillComboBoxUsingProcedures(prod_name_text, "fillComboBoxProductNames", "prod_name");

        }

        private void txt_color_Enter(object sender, EventArgs e)
        {
            RefreshFieldColor();
        }

        private void add_new_product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                printBarcodeLabel();
            }
        }

        private void productListGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string product_name = productListGridView.SelectedRows[0].Cells[1].Value.ToString();
                //string barcode = productListGridView.SelectedRows[0].Cells[2].Value.ToString();
                string category = productListGridView.SelectedRows[0].Cells[3].Value.ToString();
                string brand = productListGridView.SelectedRows[0].Cells[4].Value.ToString();
                string quantity = productListGridView.SelectedRows[0].Cells[8].Value.ToString();
                string itemPerPack = productListGridView.SelectedRows[0].Cells[9].Value.ToString();
                string qty_alert = productListGridView.SelectedRows[0].Cells[18].Value.ToString();
                string unit = productListGridView.SelectedRows[0].Cells[13].Value.ToString();
                string purchase_price = productListGridView.SelectedRows[0].Cells[21].Value.ToString();
                string sale_price = productListGridView.SelectedRows[0].Cells[22].Value.ToString();
                string whole_sale = productListGridView.SelectedRows[0].Cells[15].Value.ToString();
                string tax = productListGridView.SelectedRows[0].Cells[20].Value.ToString();
                string discount = productListGridView.SelectedRows[0].Cells[23].Value.ToString();
                string discount_limit = productListGridView.SelectedRows[0].Cells[24].Value.ToString();
                string expiry = productListGridView.SelectedRows[0].Cells[7].Value.ToString();
                string status = productListGridView.SelectedRows[0].Cells[17].Value.ToString();


                prod_name_text.Text = product_name;
                //prod_code_text.Text = barcode;
                cate_text.Text = category;
                brand_text.Text = brand;
                txt_qty.Text = quantity;
                txt_pkg.Text = itemPerPack;
                txt_qty_alert.Text = qty_alert;
                unit_text.Text = unit;
                pur_price_text.Text = purchase_price;
                sale_price_text.Text = sale_price;
                txt_wholeSale.Text = whole_sale;
                txtTaxation.Text = tax;
                txtDiscount.Text = discount;
                txtDiscountLimit.Text = discount_limit;
                txt_expire_date.Text = expiry;
                txt_status.Text = status;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void cate_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}