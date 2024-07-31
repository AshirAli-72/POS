using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Login_info.controllers;
using Message_box_info.forms;
using Datalayer;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using CounterSales_info.CustomerSalesReport;
using CounterSales_info.CustomerSalesInfo.CustomerProductsReturned;
using Customers_info.forms;
using System.Media;
using RefereningMaterial;
using Products_info.forms;
using CounterSales_info.forms.Customer_orders_list;
using CounterSales_info.forms.Customer_last_receipt;
using CounterSales_info.forms.Unhold_orders;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Threading;
using DataModel.Cash_Drawer_Data_Classes;
using Reports_info.Customer_sales_reports.loyal_customer_sales_reports;
using Message_box_info.forms.Clock_In;
using Guna.UI2.WinForms;
using OnBarcode.Barcode;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using CustomerSales_info.CustomerSalesInfo.CustomControls;
using Spices_pos.CounterSalesInfo.CustomControls;
using Spices_pos.DashboardInfo.Forms;
using Spices_pos.LoginInfo.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;


namespace CounterSales_info.forms
{
    public partial class form_counter_sales : Form
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
        
        
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;

        //******************************************************************************************
        //private fromSecondScreen fromSecondScreenInstance;
        public static form_counter_sales instance;
        public Label instanceCustomerName;
        public Label instanceCustomerCode;
        public Label instanceTipAmount;
        public Label instanceIsTipInPercentage;
        public Label instanceCustomerPoints;
        public Guna2ToggleSwitch instanceIsReturn;
        private System.Threading.Timer timer;

        public form_counter_sales()
        {
            InitializeComponent();

            role_id = Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses.Auth.role_id;
            user_id = Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses.Auth.user_id;

            //setFormColorsDynamically();
            setToolTips();
            instance = this;

            instanceCustomerName = lbl_customer;
            instanceCustomerCode = lblCustomerCode;
            instanceCustomerPoints = txtCustomerPoints;
            instanceTipAmount = txtTipAmount;
            instanceIsTipInPercentage = txtIsTipInPercentage;
            instanceIsReturn = txt_status;
        }

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        formRewards rewards = new formRewards();

        public static string macAddress = "";
        public static int user_id = 0;
        public static int role_id = 0;
        public static string employeeName = "";
        public static bool isInvoiceReturned = false;
        int group_counter = 0;
        int lastCountedValue = 0;
        string discountValue = "";

        private void TimerCallback(object state)
        {
            // Start the background worker to execute ExportThreadMethod
            if (!oneDriveBackgroundWorker.IsBusy)
            {
                oneDriveBackgroundWorker.RunWorkerAsync();
            }
        }

        private void setToolTips()
        {
            try
            {
                GetSetData.setToolTipForButtonControl(btnRegularItems, "Shows the regular/running/open or without barcode products in menu.");
                GetSetData.setToolTipForButtonControl(btnCategories, "Shows products categories in menu");
                GetSetData.setToolTipForButtonControl(btnTopSellingProducts, "Shows the current month top selling products");
                GetSetData.setToolTipForButtonControl(btnDeals, "Shows the customized promotions");
                GetSetData.setToolTipForButtonControl(btnMiscItems, "Add and sale the miscellaneous products here");
                GetSetData.setToolTipForButtonControl(btnProducts, "track, create modify and delete the products");
                GetSetData.setToolTipForButtonControl(btnShelf, "Track the shelfs and products here.");
                GetSetData.setToolTipForButtonControl(btnPayout, "Add the cash registor/Drawer payout amount here.");
                GetSetData.setToolTipForButtonControl(btnSalesReport, "Go to the sales reports by click this button.");
                GetSetData.setToolTipForButtonControl(btnExit, "Click here to open the main screen/dashboard");

                GetSetData.setToolTipForButtonControl(btn_refresh_menu, "Clear or refresh the menu.");
                GetSetData.setToolTipForButtonControl(btn_customer, "Add, modify and select loyal customers.");
                GetSetData.setToolTipForButtonControl(btn_order, "Show the shift transactions / sales and returns. Also print the receipts.");
                GetSetData.setToolTipForButtonControl(btnClearCart, "Click here to clear or refresh the cart.");
                GetSetData.setToolTipForButtonControl(btnStock, "Shows the products list and add product in cart by name, barcode, category and brand.");
                GetSetData.setToolTipForButtonControl(btn_print, "Print the latest sales receipt.");
               
                GetSetData.setToolTipForButtonControl(btnReturn, "Change sales status to return items.");
                GetSetData.setToolTipForButtonControl(btn_last_receipt, "Shows the transaction history of sales and returns via date.");
                GetSetData.setToolTipForButtonControl(btnSwitchUser, "Click here to switch the current user to another one.");
                GetSetData.setToolTipForButtonControl(btnMinMaxScreen, "Click here to minimize the screen.");
               
                GetSetData.setToolTipForButtonControl(btn_dicount, "Add discounts via percentage on total amount.");
                GetSetData.setToolTipForButtonControl(btn_hold, "Hold the current invoice and make another sale.");
                GetSetData.setToolTipForButtonControl(btn_unhold, "Un-Hold and fill the pending invoice items in cart.");
                GetSetData.setToolTipForButtonControl(btn_recovery, "Open the creditor customers recovery screen.");
                GetSetData.setToolTipForButtonControl(btn_void, "Cancel the current invoice and make new sales.");
                GetSetData.setToolTipForButtonControl(btn_payment, "Open the payment screen to complete the current invoice.");

            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
            }
        }

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
        //    //    GetSetData.setPanelColors(dark_red, dark_green, dark_blue, fore_red, fore_green, fore_blue, panel17);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, lbl_username);       
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
        //    //    //GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lbl_balance);

        //    //    //****************************************************************

        //    //    GetSetData.setLabelColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, lbl_time);
        //    //    GetSetData.setLabelColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dates);
        //    //    GetSetData.setLabelColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, lblStatus);

        //    //    //****************************************************************

        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnChequeDetails);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnShelf);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh_menu);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_minimize);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnExit);

        //    //    //****************************************************************

        //    //    back_red = data.UserPermissionsIds("menu_back_red", "pos_colors_settings");
        //    //    back_green = data.UserPermissionsIds("menu_back_green", "pos_colors_settings");
        //    //    back_blue = data.UserPermissionsIds("menu_back_blue", "pos_colors_settings");

        //    //    GetSetData.setGunaUILoginButonColors(back_red, back_green, back_blue, 255, 255, 255, back_red, back_green, back_blue, btnRegularItems);

        //    //    //****************************************************************

        //    //    back_red = data.UserPermissionsIds("category_back_red", "pos_colors_settings");
        //    //    back_green = data.UserPermissionsIds("category_back_green", "pos_colors_settings");
        //    //    back_blue = data.UserPermissionsIds("category_back_blue", "pos_colors_settings");


        //    //    GetSetData.setGunaUILoginButonColors(back_red, back_green, back_blue, 255, 255, 255, back_red, back_green, back_blue, btnCategories);

        //    //    //****************************************************************
        //    //}
        //    //catch (Exception es)
        //    //{
        //    //    MessageBox.Show(es.Message);
        //    //}
        //}

        private void pos_permissions()
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                GetSetData.Data = generalSettings.ReadField("show_discount");

                if (GetSetData.Data == "Enabled")
                {
                    btn_dicount.Visible = true;

                    txt_total_discount.ReadOnly = false;
                    //txt_total_discount.Enabled = true;

                    txtDiscount.ReadOnly = false;
                    txtDiscount.Enabled = true;
                }
                else
                {
                    btn_dicount.Visible = false;

                    txt_total_discount.ReadOnly = true;
                    //txt_total_discount.Enabled = false;

                    txtDiscount.ReadOnly = true;
                    txtDiscount.Enabled = false;
                }


                GetSetData.Data = generalSettings.ReadField("discount_box");

                if (GetSetData.Data == "No")
                {
                    chkWholeSale.Visible = false;
                }

                //GetSetData.Data = generalSettings.ReadField("search_box");

                //if (GetSetData.Data == "Yes")
                //{
                //    txtProductName.Enabled = true;
                //}
                //else
                //{
                //    txtProductName.Enabled = false;
                //}


                GetSetData.Data = generalSettings.ReadField("price_box");

                if (GetSetData.Data == "Yes")
                {
                    txt_price.ReadOnly = false;
                    txt_price.Enabled = true;
                }

                //GetSetData.Data = generalSettings.ReadField("show_recovery");

                //if (GetSetData.Data == "Yes")
                //{
                //    btn_recovery.Visible = true;
                //}

                GetSetData.Data = generalSettings.ReadField("show_last_order");

                if (GetSetData.Data == "Yes")
                {
                    btn_last_receipt.Visible = true;
                }

                GetSetData.Data = generalSettings.ReadField("show_order");

                if (GetSetData.Data == "Yes")
                {
                    btn_order.Visible = true;
                }

                //GetSetData.Data = generalSettings.ReadField("show_installmentPlan");

                //if (GetSetData.Data == "Yes")
                //{
                //    btnInstallmentPlan.Visible = true;
                //}

                GetSetData.Data = generalSettings.ReadField("show_unhold");

                if (GetSetData.Data == "Yes")
                {
                    btn_unhold.Visible = true;
                }

                GetSetData.Data = generalSettings.ReadField("show_hold");

                if (GetSetData.Data == "Yes")
                {
                    btn_hold.Visible = true;
                }

                //GetSetData.Data = generalSettings.ReadField("show_guarantors");

                //if (GetSetData.Data == "Yes")
                //{
                //    btnGuarantors.Visible = true;
                //}

                //GetSetData.Data = generalSettings.ReadField("show_remarks");

                //if (GetSetData.Data == "Yes")
                //{
                //    btn_remarks.Visible = true;
                //}

                //GetSetData.Data = data.UserPermissions("show_print_receipt", "pos_general_settings");

                //if (GetSetData.Data == "Yes")
                //{
                //    btn_print.Visible = true;
                //}

                GetSetData.Data = generalSettings.ReadField("discountType");
                if (discountValue == "Yes")
                {
                    txtDiscount.PlaceholderText = "Dicount %";
                    //lbl_percentage.Visible = true;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void login_by_user()
        {
            try
            {
                //GetSetData.Ids = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "emp_id", "pos_users", "user_id", user_id.ToString());
                //GetSetData.Data = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                //lbl_username.Text = GetSetData.Data;
                //lbl_username.Text = "Welcome " + GetSetData.Data;

                lbl_username.Text = Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses.Auth.user_name;

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillLastCreditsTextBox()
        {
            try
            {
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                double customer_discount = 0;
                double customer_points = 0;


                if (lbl_customer.Text != "" && lbl_customer.Text != "nill")
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //*****************************************************************************************

                    if (GetSetData.Ids != 0)
                    {
                        customer_discount = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "discount", "pos_customers", "customer_id", GetSetData.Ids.ToString());

                        customer_discount = (double.Parse(txtGrandTotal.Text) * customer_discount) / 100;

                        if (customer_discount > 0)
                        {
                            txt_total_discount.Text = customer_discount.ToString();
                        }

                        if (txt_status.Checked)
                        {
                            txtCustomerPoints.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "points", "pos_customers", "customer_id", GetSetData.Ids.ToString());
                        }

                        txt_lastCredits.Text = Math.Round(double.Parse(GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "lastCredits", "pos_customer_lastCredits", "customer_id", GetSetData.Ids.ToString())), 2).ToString();


                        GetSetData.query = "update pos_cart_items set customer_id = '" + GetSetData.Ids.ToString()  + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                else
                {
                    txt_lastCredits.Text = "0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void beep_sound()
        {
            try
            {
                string notificationSound = generalSettings.ReadField("notificationSound");

                if (notificationSound == "Yes")
                {
                    // Play Sound ****************************************************************


                    GetSetData.Data = generalSettings.ReadField("picture_path");

                    GetSetData.Data = @"" + GetSetData.Data + "sound.wav";
                    SoundPlayer player = new SoundPlayer(GetSetData.Data);
                    player.Play();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void FillRegularItemsPanal(string query, string condition)
        {
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;
            SqlDataReader reader;


            try
            {
                GetSetData.Permission = "";
                TextData.barcode = "";
                TextData.saved_image = generalSettings.ReadField("picture_path");

                cmd = new SqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string product_id_db = reader["product_id"].ToString();
                    string stock_id_db = reader["stock_id"].ToString();
                    TextData.prod_name = reader["prod_name"].ToString();
                    TextData.barcode = reader["item_barcode"].ToString();
                    TextData.image_path = reader["image_path"].ToString();
                    TextData.quantity = double.Parse(reader["quantity"].ToString());

                    TextData.rate = double.Parse(reader["sale_price"].ToString());
                    TextData.MarketPrice = double.Parse(reader["market_value"].ToString());

                    TextData.MarketPrice = ((TextData.rate * TextData.MarketPrice) / 100);
                    TextData.rate = TextData.rate + TextData.MarketPrice;


                    btnProduct b = new btnProduct();
                    Color fcol = Color.FromArgb(255, 255, 255);
                    //Color bcol = Color.FromArgb(5, 100, 146);
                    //b.BackColor = bcol;
                    b.ForeColor = fcol;

                    if (TextData.image_path != "nill" && TextData.image_path != "")
                    {
                        //b.Image = Image.FromFile(TextData.saved_image + TextData.image_path);
                        b.itemImage = Image.FromFile(TextData.saved_image + TextData.image_path);
                    }

                    // *********************************************************************
                    b.Name = TextData.prod_name;
                    b.ItemsName = TextData.prod_name;
                    b.ItemsBarcode = TextData.barcode;
                    b.ProductId = product_id_db;
                    b.StockId = stock_id_db;

                    b.Price = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();
                    b.Stock = TextData.quantity.ToString();
                    b.Cursor = Cursors.Hand;

                    // To Hide Redunduncy of buttons
                    //foreach (Control controls in pnl_list.Controls)
                    //{
                    //    GetSetData.Permission = controls.Name;

                    //    if (GetSetData.Permission == TextData.prod_name)
                    //    {
                    //        break;
                    //    }
                    //}

                    //// Adding Buttons in Panel
                    //if (GetSetData.Permission != TextData.prod_name)
                    //{
                    pnl_list.Controls.Add(b);
                    b.Click += new System.EventHandler(this.Button_Click);
                    b.MouseHover += new System.EventHandler(this.regularItemsMouseHover);
                    b.MouseLeave += new System.EventHandler(this.regularItemsMouseLeave);
                    //}
                }

                reader.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        // ***************************************************
        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn = sender as btnProduct;

                fun_add_records_from_menu_gridview(btn.Name, btn.ItemsBarcode, btn.ProductId, btn.StockId);
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool fun_add_records_from_menu_gridview(string prod_name, string barcode, string product_id, string stock_id)
        {
            try
            {
                int isItemAgeRestricted = data.UserPermissionsIds("id", "pos_age_restricted_items", "prod_id", product_id.ToString());

                int applyAgeLimit = data.UserPermissionsIds("customerAgeLimit", "pos_general_settings");

                if (applyAgeLimit > 0)
                {
                    if (isItemAgeRestricted != 0)
                    {
                        int itemAgeLimit = data.UserPermissionsIds("age_limit", "pos_age_restricted_items", "prod_id", product_id.ToString());

                        sure.Message_choose("Please verify the customer is a minimum of '" + itemAgeLimit.ToString() + " years' old.");
                        sure.ShowDialog();

                        if (form_sure_message.sure != true)
                        {
                            return false;
                        }
                    }
                }

                #region
                TextData.prod_price = "0";
                TextData.prod_name = prod_name;
                TextData.discount = 0;
                TextData.totalDiscount = 0;
                TextData.quantity = 0;
                TextData.promotionDiscount = 0;


                double wholeSale = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id);
                double sale_price = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id);
                double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id);
                double pkg_db = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id);
                double market_price = data.NumericValues("market_value", "pos_stock_details", "stock_id", stock_id);
                double tab_pieces_db = data.NumericValues("tab_pieces", "pos_stock_details", "stock_id", stock_id);
                //double discount = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "discount", "pos_stock_details", "stock_id", stock_id);

                TextData.prod_state = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "prod_state", "pos_products", "product_id", product_id.ToString());
            

                //GetSetData.query = "select pos_category.category_id from pos_products inner join pos_category on pos_products.category_id = pos_category.category_id where (product_id = '" + product_id.ToString() + "');";
                //string category_id_db = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select pos_brand.brand_id from pos_products inner join pos_brand on pos_products.brand_id = pos_brand.brand_id where (product_id = '" + product_id.ToString() + "');";
                string brand_id_db = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select brand_title from pos_products inner join pos_brand on pos_products.brand_id = pos_brand.brand_id where (product_id = '" + product_id.ToString() + "');";
                string brand_title_db = data.SearchStringValuesFromDb(GetSetData.query);

                TextData.quantity = 1;
                TextData.rate = sale_price;
                TextData.wholeSale = wholeSale;
                //TextData.discount = discount;
                //double discount_db = discount;
                TextData.MarketPrice = market_price;


                // **********************************************
                if (barcode == "")
                {
                    //TextData.barcode = data.UserPermissions("barcode", "pos_products", "prod_name", prod_name);
                    TextData.barcode = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", stock_id);
                    barcode = "nill";
                }
                else
                {
                    TextData.barcode = barcode;
                }
                // **********************************************
                if (pkg_db == 0)
                {
                    pkg_db = 1;
                }

                if (tab_pieces_db == 0)
                {
                    tab_pieces_db = 1;
                }

                // **********************************************

                GetSetData.query = "select id from pos_cart_items where (product_id = '" + product_id.ToString() + "') and (stock_id = '" + stock_id.ToString() + "') and (mac_address = '" + macAddress + "');";
                double cart_item_id_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);


                //*************************************************
                #region
                //if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets")
                //{
                //    switch (txt_sale_option.Text)
                //    {
                //        case "Pack":
                //            TextData.quantity = (TextData.quantity * pkg_db) * tab_pieces_db;
                //            TextData.rate = TextData.quantity * sale_price;
                //            TextData.wholeSale = TextData.quantity * wholeSale;
                //            //TextData.discount = TextData.quantity * discount;
                //            //TextData.MarketPrice = TextData.quantity * market_price;
                //            break;

                //        case "Pellet":
                //            TextData.quantity = TextData.quantity * tab_pieces_db;
                //            TextData.rate = TextData.quantity * sale_price;
                //            TextData.wholeSale = TextData.quantity * wholeSale;
                //            //TextData.discount = TextData.quantity * discount;
                //            //TextData.MarketPrice = TextData.quantity * market_price;
                //            break;

                //        case "Piece":
                //            TextData.quantity = 1;
                //            TextData.rate = sale_price;
                //            TextData.wholeSale = wholeSale;
                //            //TextData.discount = discount;
                //            //TextData.MarketPrice = market_price;
                //            break;

                //        case "Carton":
                //            TextData.quantity = (TextData.quantity * pkg_db) * tab_pieces_db;
                //            TextData.rate = TextData.quantity * sale_price;
                //            TextData.wholeSale = TextData.quantity * wholeSale;
                //            //TextData.discount = TextData.quantity * discount;
                //            //TextData.MarketPrice = TextData.quantity * market_price;
                //            break;

                //        case "Bag":
                //            TextData.quantity = (TextData.quantity * pkg_db) * tab_pieces_db;
                //            TextData.rate = TextData.quantity * sale_price;
                //            TextData.wholeSale = TextData.quantity * wholeSale;
                //            //TextData.discount = TextData.quantity * discount;
                //            //TextData.MarketPrice = TextData.quantity * market_price;
                //            break;

                //        default:
                //            TextData.quantity = 1;
                //            TextData.rate = sale_price;
                //            TextData.wholeSale = wholeSale;
                //            //TextData.discount = discount;
                //            //TextData.MarketPrice = market_price;
                //            break;
                //    }
                //}
                #endregion

                if (cart_item_id_db == 0) // Adding new item in the cart
                {
                    string isStockLimitSetToZero = generalSettings.ReadField("setStockLimitToZero");

                    if (((TextData.quantity) <= quantity_db) || (TextData.category == "Services") || (TextData.category == "MISCELLANEOUS") || (txt_status.Checked == true) || (isStockLimitSetToZero == "No"))
                    {
                        btnCart cartItem = new btnCart();

                        cartItem.Name = stock_id;
                        cartItem.Brand = brand_title_db;
                        cartItem.ItemsName = TextData.prod_name;
                        cartItem.barcode = TextData.barcode;
                        cartItem.Quantity = TextData.quantity.ToString();
                        cartItem.ChangeQuantity = TextData.quantity.ToString();
                        //cartItem.discount = Math.Round(TextData.discount, 2);
                        cartItem.availableStock = quantity_db;
                        cartItem.productID = product_id;
                        cartItem.stockID = stock_id;
                        //cartItem.categoryID = category_id_db;
                        //cartItem.brandID = brand_id_db;
                        cartItem.employee = lbl_employee.Text;
                        cartItem.customer_name = lbl_customer.Text;
                        cartItem.customer_code = lblCustomerCode.Text;
                        cartItem.clock_in_id = clock_in_id;
                        cartItem.is_return = txt_status.Checked;
                        cartItem.note = barcode;
                        cartItem.date = txt_date.Text;
                        cartItem.macAddress = macAddress;

                        #region

                        int number_of_quantity_in_cart = 0;


                        //*************************************************************

                        //string queryCheckPromoItems = "select promo_group_id from pos_promo_group_items where (prod_id = '" + product_id.ToString() + "') and (stock_id = '" + stock_id.ToString() + "');";

                        string queryCheckPromoItems = @"select pos_promotions.promo_group_id 
                                                        from pos_promotions inner join pos_promo_groups on pos_promo_groups.id = pos_promotions.promo_group_id
                                                        inner join pos_promo_group_items on pos_promo_group_items.promo_group_id = pos_promo_groups.id  
                                                        where (pos_promo_group_items.prod_id = '" + product_id.ToString() + "') and (pos_promo_group_items.stock_id = '" + stock_id.ToString() + "') and (pos_promotions.status = 'Active') and (pos_promotions.start_date <= '" + txt_date.Text + "') and (pos_promotions.end_date >= '" + txt_date.Text + "');";


                        SqlConnection connCheckPromo = new SqlConnection(webConfig.con_string);
                        SqlCommand cmdCheckPromo;
                        SqlDataReader readerCheckPromo;

                        cmdCheckPromo = new SqlCommand(queryCheckPromoItems, connCheckPromo);

                        connCheckPromo.Open();
                        readerCheckPromo = cmdCheckPromo.ExecuteReader();


                        int promo_group_id = 0;


                        while (readerCheckPromo.Read())
                        {
                            GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + readerCheckPromo["promo_group_id"].ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                            promo_group_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                            if (promo_group_id != 0)
                            {
                                promo_group_id = Convert.ToInt32(readerCheckPromo["promo_group_id"].ToString());
                                break;
                            }
                        }

                        readerCheckPromo.Close();
                        connCheckPromo.Close();
                        //*************************************************************

                        //GetSetData.query = "select promo_group_id from pos_promo_group_items where (prod_id = '" + product_id.ToString() + "') and (stock_id = '" + stock_id.ToString() + "');";
                        //int promo_group_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                        //*************************************************************

                        if (promo_group_id != 0)
                        {
                            GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + promo_group_id.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                            int promotion_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                            if (promotion_id != 0)
                            {
                                bool is_discount_in_percentage = bool.Parse(data.UserPermissions("is_discount_in_percentage", "pos_promotions", "id", promotion_id.ToString()));
                                double discount = double.Parse(data.UserPermissions("discount", "pos_promotions", "id", promotion_id.ToString()));
                                double discountInPercentage = double.Parse(data.UserPermissions("discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                                double newPrice = double.Parse(data.UserPermissions("discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                                int quantityLimit = data.UserPermissionsIds("quantity", "pos_promotions", "id", promotion_id.ToString());


                                // *****************************************

                                string query = "select product_id, stock_id, quantity from pos_cart_items where (mac_address = '" + macAddress + "');";


                                SqlConnection conn = new SqlConnection(webConfig.con_string);
                                SqlCommand cmd;
                                SqlDataReader reader;

                                cmd = new SqlCommand(query, conn);

                                conn.Open();
                                reader = cmd.ExecuteReader();


                                while (reader.Read())
                                {
                                    GetSetData.query = "select promo_group_items_id from pos_promo_group_items where (prod_id = '" + reader["product_id"].ToString() + "') and (stock_id = '" + reader["stock_id"].ToString() + "') and (promo_group_id = '" + promo_group_id + "');";
                                    int is_promo_item_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    if (is_promo_item_exist != 0)
                                    {
                                        number_of_quantity_in_cart += Convert.ToInt32(reader["quantity"].ToString());
                                    }

                                }

                                reader.Close();
                                conn.Close();
                                //*************************************************************

                                number_of_quantity_in_cart += 1;


                                if (number_of_quantity_in_cart >= quantityLimit)
                                {
                                    int promotionItemsRemainder = number_of_quantity_in_cart % quantityLimit;

                                    if (promotionItemsRemainder == 0)
                                    {
                                        if (chkWholeSale.Checked == true)
                                        {
                                            if (is_discount_in_percentage)
                                            {
                                                TextData.promotionDiscount = ((TextData.wholeSale * discountInPercentage) / 100);
                                            }
                                            else
                                            {
                                                TextData.promotionDiscount = discount;
                                            }
                                        }
                                        else
                                        {
                                            if (is_discount_in_percentage)
                                            {
                                                TextData.promotionDiscount = ((TextData.rate * discountInPercentage) / 100);
                                            }
                                            else
                                            {
                                                TextData.promotionDiscount = discount;
                                            }
                                        }

                                        cartItem.discount = TextData.promotionDiscount;
                                    }
                                }
                            }
                            else
                            {
                                cartItem.discount = 0;
                            }

                        }
                        else
                        {
                            cartItem.discount = 0;
                        }

                        //if (chkWholeSale.Checked == true)
                        //{
                        //    TextData.MarketPrice = ((TextData.wholeSale * TextData.MarketPrice) / 100);

                        //    cartItem.tax = Math.Round(TextData.MarketPrice, 2);
                        //    cartItem.price = Math.Round((TextData.wholeSale + TextData.MarketPrice), 2);


                        //    TextData.wholeSale = (TextData.wholeSale + TextData.MarketPrice);

                        //    cartItem.TotalAmount = GetSetData.currency() + Math.Round(TextData.wholeSale, 2).ToString();
                        //}
                        //else
                        //{
                        TextData.MarketPrice = ((TextData.rate * TextData.MarketPrice) / 100);

                        cartItem.tax = Math.Round(TextData.MarketPrice, 2);
                        cartItem.price = Math.Round((TextData.rate + TextData.MarketPrice), 2);


                        TextData.rate = (TextData.rate + TextData.MarketPrice);

                        cartItem.TotalAmount = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();
                        //}
                        //*********************************************************

                        CartFlowLayout.Controls.Add(cartItem);
                        cartItem.Click += new System.EventHandler(this.CartItemButton_Click);

                        //*********************************************************

                        string customer_id = "";

                        if (lbl_customer.Text != "")
                        {
                            GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                            customer_id = data.SearchStringValuesFromDb(GetSetData.query);
                        }

                        string isReturn = "";

                        if (txt_status.Checked)
                        {
                            isReturn = "";
                        }

                        int totalCartItems = CartFlowLayout.Controls.OfType<btnCart>().Count();

                        int totalItemsInCartDB = data.UserPermissionsIds("count(mac_address)", "pos_cart_items", "mac_address", macAddress);

                 
                        if (totalCartItems != totalItemsInCartDB)
                        {
                            GetSetData.query = "insert into pos_cart_items values ('" + cartItem.ItemsName + "' , '" + cartItem.barcode + "' , '" + cartItem.Quantity + "' , '" + Math.Round(cartItem.price, 2).ToString() + "' , '" + Math.Round(cartItem.tax, 2).ToString() + "' , '" + Math.Round(cartItem.discount, 2).ToString() + "' , '" + Math.Round(TextData.rate, 2).ToString() + "' , '" + cartItem.availableStock.ToString() + "' ,  '" + cartItem.note + "' , '" + cartItem.productID + "' , '" + cartItem.stockID + "' , '' , '' , '" + macAddress + "' , '" + txtCustomerPoints.Text + "' , '" + isReturn.ToString() + "' , '" + customer_id + "' , '" + user_id.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                        #endregion

                        //*********************************************************
                        
                        beep_sound();
                    }
                    else
                    {
                        error.errorMessage("Available stock is '" + quantity_db.ToString() + "'!");
                        error.ShowDialog();
                    }
                }
                else  // updating, items already exist in the cart 
                {
                    foreach (Control control in CartFlowLayout.Controls)
                    {
                        if (control is btnCart cartItem && cartItem.Name == stock_id)
                        {
                            double cart_quantity_db = 0;

                            GetSetData.query = "select quantity from pos_cart_items where  (product_id = '" + cartItem.productID + "') and (stock_id = '" + cartItem.stockID + "') and (mac_address = '" + macAddress + "');";
                            cart_quantity_db = data.SearchNumericValuesDb(GetSetData.query);

                            TextData.quantity += cart_quantity_db;

                            string isStockLimitSetToZero = generalSettings.ReadField("setStockLimitToZero");

                            if ((TextData.quantity <= quantity_db) || (TextData.category == "Services") || (TextData.category == "MISCELLANEOUS") || (txt_status.Checked == true) || (isStockLimitSetToZero == "No"))
                            {
                                // Update the properties of the found control
                                cartItem.is_return = txt_status.Checked;
                                cartItem.Quantity = TextData.quantity.ToString();
                                cartItem.ChangeQuantity = TextData.quantity.ToString();

                                beep_sound();
                            }
                            else
                            {
                                error.errorMessage("Available stock is '" + quantity_db.ToString() + "'!");
                                error.ShowDialog();
                            }

                            break;
                        }
                    }
                }


                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_items_exists_in_cart == 0)
                {
                    fun_clear_cart_records();
                }

                calculateGrandTotals();

                #endregion

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        } 

        //private void IsPopupFormOpen(Type formType)
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form.GetType() == formType && !form.IsDisposed) // Check if form is not disposed
        //        {
        //            form.Invoke(new Action(() => form.Dispose())); // Ensure closing operation is done on UI thread
        //            return;
        //        }
        //    }
        //}

        private void CartItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                txtChangeAmount.Text = "0.00";
                calculateGrandTotals();
                calculateAmountDue();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void ScanByBarcode()
        {
            try
            {
                int counter = 0;

                if (txt_barcode.Text != "")
                {
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

                        GetSetData.Data = choose_product.selectedProductName;
                        TextData.product_id = choose_product.selectedProductID;
                        TextData.stock_id = choose_product.selectedStockID;
                    }
                    else
                    {
                        TextData.stock_id = data.UserPermissions("stock_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                        TextData.product_id = data.UserPermissions("prod_id", "pos_stock_details", "item_barcode", txt_barcode.Text);
                        GetSetData.Data = data.UserPermissions("prod_name", "pos_products", "product_id", TextData.product_id);

                        if (TextData.product_id != "" && TextData.stock_id != "")
                        {
                            fun_add_records_from_menu_gridview(GetSetData.Data, txt_barcode.Text, TextData.product_id, TextData.stock_id);
                        }

                        txt_barcode.Text = "";
                        txt_barcode.Select();
                        beep_sound();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        private void ScanByProductName()
        {
            try
            {
                int counter = 0;

                if (txtProductName.Text != "")
                {
       
                    GetSetData.query = @"select count(prod_id) from pos_products 
                                         inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
                                         where (prod_name = '" + txtProductName.Text + "');";
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

                        GetSetData.Data = choose_product.selectedProductName;
                        TextData.product_id = choose_product.selectedProductID;
                        TextData.stock_id = choose_product.selectedStockID;
                    }
                    else
                    {
                        TextData.product_id = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "product_id", "pos_products", "prod_name", txtProductName.Text);
                        TextData.stock_id = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "stock_id", "pos_stock_details", "prod_id", TextData.product_id);
                        GetSetData.Data = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", TextData.stock_id);

                        if (TextData.product_id != "" && TextData.stock_id != "")
                        {
                            fun_add_records_from_menu_gridview(txtProductName.Text, GetSetData.Data, TextData.product_id, TextData.stock_id);
                        }

                        txtProductName.Text = "";
                        txtProductName.Select();
                        beep_sound();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.conn_.Close();
            }
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    TextData.prod_name = "";
            //    TextData.prod_state = "";
            //    TextData.rate = 0;
            //    TextData.prod_price = "0";
            //    TextData.net_total = 0;
            //    TextData.quantity = 0;
            //    TextData.discount = 0;
            //    TextData.MarketPrice = 0;
            //    TextData.rate = 0;
            //    double PreviousMarketPrice = 0;
            //    double previousDiscount = 0;

            //    if (txt_qty.Text != "")
            //    {
            //        TextData.quantity = double.Parse(txt_qty.Text);

            //        TextData.total_qty = double.Parse(CartDataGridView.SelectedRows[0].Cells["qty"].Value.ToString());
            //        TextData.rate = double.Parse(CartDataGridView.SelectedRows[0].Cells["rate"].Value.ToString());
            //        TextData.prod_name = CartDataGridView.SelectedRows[0].Cells["Item_name"].Value.ToString();
            //        TextData.barcode = CartDataGridView.SelectedRows[0].Cells["note"].Value.ToString();
            //        TextData.discount = double.Parse(CartDataGridView.SelectedRows[0].Cells["discount"].Value.ToString());
            //        TextData.MarketPrice = double.Parse(CartDataGridView.SelectedRows[0].Cells["market_price"].Value.ToString());

            //        TextData.total_sales_price = TextData.rate; // storing old rate
            //        previousDiscount = TextData.discount; // storing old discountT
            //        PreviousMarketPrice = TextData.MarketPrice; // storing old market price

            //        GetSetData.query = "select pos_category.title from pos_products inner join pos_category on pos_products.category_id = pos_category.category_id where prod_name = '" + TextData.prod_name + "';";
            //        TextData.category = data.SearchStringValuesFromDb(GetSetData.query);
            //        // *******************************************************************

            //        //GetSetData.Ids = data.UserPermissionsIds("product_id", "pos_products", "prod_name", TextData.prod_name);

            //        GetSetData.query = "select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
            //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);



            //        double quantity_db = data.NumericValues("quantity", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString());
            //        double pkg_db = data.NumericValues("pkg", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        double tab_pieces_db = data.NumericValues("tab_pieces", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        //TextData.MarketPrice = data.NumericValues("market_value", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        // *******************************************************************

            //        // **********************************************
            //        if (pkg_db == 0)
            //        {
            //            pkg_db = 1;
            //        }

            //        if (tab_pieces_db == 0)
            //        {
            //            tab_pieces_db = 1;
            //        }

            //        if (TextData.total_qty == 0)
            //        {
            //            TextData.total_qty = 1;
            //        }
            //        // **********************************************

            //        TextData.total_sales_price = TextData.rate; // storing old rate

            //        if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets")
            //        {
            //            switch (txt_sale_option.Text)
            //            {
            //                case "Pack":
            //                    TextData.quantity = ((TextData.quantity * pkg_db) * tab_pieces_db);

            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Pellet":
            //                    TextData.quantity = (TextData.quantity * tab_pieces_db);

            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Piece":
            //                    TextData.rate = (TextData.rate / TextData.total_qty);

            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Carton":
            //                    TextData.quantity = ((TextData.quantity * pkg_db) * tab_pieces_db);

            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Bag":
            //                    TextData.quantity = ((TextData.quantity * pkg_db) * tab_pieces_db);

            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;

            //                    break;

            //                default:
            //                    TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //                    TextData.discount = TextData.discount / TextData.total_qty;

            //                    TextData.discount = TextData.quantity * TextData.discount;

            //                    TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //                    TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //                    TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            TextData.rate = ((TextData.rate - TextData.MarketPrice) / TextData.total_qty);

            //            TextData.discount = TextData.discount / TextData.total_qty;

            //            TextData.discount = TextData.quantity * TextData.discount;

            //            TextData.MarketPrice = (TextData.MarketPrice / TextData.total_qty);

            //            TextData.rate = TextData.quantity * (TextData.rate + TextData.MarketPrice + TextData.discount);

            //            TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //        }

            //        if (txt_status.Checked == false)
            //        {
            //            string isStockLimitSetToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");

            //            if (TextData.quantity <= quantity_db || TextData.category == "Services" || TextData.category == "MISCELLANEOUS" || isStockLimitSetToZero == "No")
            //            {
            //                CartDataGridView.SelectedRows[0].Cells["market_price"].Value = Math.Round(TextData.MarketPrice, 2).ToString();

            //                // *************************************
            //                // TextData.rate = (TextData.rate + TextData.MarketPrice) - TextData.discount;

            //                CartDataGridView.SelectedRows[0].Cells["qty"].Value = TextData.quantity.ToString();
            //                CartDataGridView.SelectedRows[0].Cells["rate"].Value = Math.Round(TextData.rate, 2).ToString();
            //                CartDataGridView.SelectedRows[0].Cells["discount"].Value = Math.Round(TextData.discount, 2).ToString();
            //                //CartDataGridView.SelectedRows[0].Cells["market_price"].Value = Math.Round(TextData.MarketPrice, 2).ToString();
            //                // *******************************************************************

            //                // calculating the discount that(Market price - sale price)
            //                //===============================================================================
            //                TextData.MarketPrice = (TextData.MarketPrice - (TextData.rate + TextData.discount));
            //                PreviousMarketPrice = (PreviousMarketPrice - (TextData.total_sales_price + previousDiscount));

            //                if (TextData.MarketPrice > 0)
            //                {
            //                    TextData.MarketPrice = (double.Parse(txt_tax.Text) + TextData.MarketPrice) - PreviousMarketPrice;
            //                    txt_tax.Text = Math.Round(TextData.MarketPrice, 2).ToString();
            //                }

            //                // calculating net totals
            //                TextData.total_sales_price += previousDiscount;
            //                TextData.rate += TextData.discount;
            //                TextData.net_total = ((double.Parse(txt_sub_total.Text) + TextData.rate) - TextData.total_sales_price);// old price
            //                txt_sub_total.Text = Math.Round(TextData.net_total, 2).ToString();

            //                if (TextData.net_total < 0)
            //                {
            //                    txt_sub_total.Text = "0";
            //                }

            //                // calculating net discount
            //                TextData.discount = (double.Parse(txt_discount.Text) + TextData.discount) - previousDiscount;// old discount
            //                txt_discount.Text = Math.Round(TextData.discount, 2).ToString();

            //                if (TextData.discount < 0)
            //                {
            //                    txt_discount.Text = "0.00";
            //                }

            //                TextData.quantity = ((double.Parse(txt_total_qty.Text) + TextData.quantity) - TextData.total_qty);// old qty
            //                txt_total_qty.Text = TextData.quantity.ToString();
            //                // *******************************************************************
            //            }
            //            else
            //            {
            //                error.errorMessage("Available Stock is '" + quantity_db + "'");
            //                error.ShowDialog();
            //            }
            //        }
            //        else
            //        {
            //            TextData.MarketPrice = ((TextData.rate * TextData.MarketPrice) / 100);

            //            CartDataGridView.SelectedRows[0].Cells["market_price"].Value = Math.Round(TextData.MarketPrice, 2).ToString();

            //            // *************************************

            //            //TextData.rate -= TextData.discount;
            //            TextData.rate = (TextData.rate + TextData.MarketPrice) - TextData.discount;


            //            CartDataGridView.SelectedRows[0].Cells["qty"].Value = TextData.quantity.ToString();
            //            CartDataGridView.SelectedRows[0].Cells["discount"].Value = Math.Round(TextData.discount, 2).ToString();
            //            CartDataGridView.SelectedRows[0].Cells["rate"].Value = Math.Round(TextData.rate, 2).ToString();
            //            //CartDataGridView.SelectedRows[0].Cells["market_price"].Value = Math.Round(TextData.MarketPrice, 2).ToString();
            //            //===============================================================================

            //            TextData.MarketPrice = (TextData.MarketPrice - (TextData.rate + TextData.discount));
            //            PreviousMarketPrice = (PreviousMarketPrice - (TextData.total_sales_price + previousDiscount));

            //            if (TextData.MarketPrice > 0)
            //            {
            //                TextData.MarketPrice = (double.Parse(txt_tax.Text) + TextData.MarketPrice) - PreviousMarketPrice;
            //                txt_tax.Text = Math.Round(TextData.MarketPrice, 2).ToString();
            //            }
            //        }

            //        // calculating net totals
            //        //TextData.net_total = double.Parse(txt_sub_total.Text);
            //        decimal totalQuantity = 0;
            //        decimal totalDiscount = 0;
            //        decimal totalTaxation = 0;
            //        decimal totalAmount = 0;

            //        foreach (DataGridViewRow row in CartDataGridView.Rows)
            //        {
            //            if (!row.IsNewRow)
            //            {
            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[1].Value.ToString(), out decimal quantity))
            //                {
            //                    totalQuantity += quantity;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[2].Value.ToString(), out decimal discountAmount))
            //                {
            //                    totalDiscount += discountAmount;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[3].Value.ToString(), out decimal taxation))
            //                {
            //                    totalTaxation += taxation;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[4].Value.ToString(), out decimal amount))
            //                {
            //                    totalAmount += amount;
            //                }
            //            }
            //        }

            //        txt_total_qty.Text = totalQuantity.ToString();
            //        txt_tax.Text = Math.Round(totalDiscount, 2).ToString();
            //        txtTaxation.Text = Math.Round(totalTaxation, 2).ToString();

            //        txt_sub_total.Text = Math.Round((totalAmount - totalTaxation), 2).ToString();

            //        txtGrandTotal.Text = Math.Round(totalAmount, 2).ToString();


            //        int totalRows = CartDataGridView.RowCount;

            //        // Display the total number of rows in the TextBox
            //        txt_total_items.Text = totalRows.ToString();

            //        //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Changing Item quantity [" + TextData.prod_name + " price " + TextData.rate.ToString() + " Qty " + TextData.quantity.ToString() + "]", role_id);
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    TextData.rate = 0;
            //    TextData.discount = 0;
            //    double previousDiscount = 0;
            //    TextData.disLimit = 0;
            //    double pkg_db = 0;
            //    double tab_pieces_db = 0;
            //    double salePriceDb = 0;
            //    discountValue = data.UserPermissions("discountType", "pos_general_settings");

            //    if (txtDiscount.Text != "")
            //    {
            //        TextData.discount = double.Parse(txtDiscount.Text);

            //        TextData.prod_name = CartDataGridView.SelectedRows[0].Cells["Item_name"].Value.ToString();
            //        TextData.barcode = CartDataGridView.SelectedRows[0].Cells["note"].Value.ToString();
            //        TextData.quantity = double.Parse(CartDataGridView.SelectedRows[0].Cells["qty"].Value.ToString());
            //        TextData.rate = double.Parse(CartDataGridView.SelectedRows[0].Cells["rate"].Value.ToString());
            //        previousDiscount = double.Parse(CartDataGridView.SelectedRows[0].Cells["discount"].Value.ToString());
            //        TextData.prod_price = previousDiscount.ToString();
            //        TextData.MarketPrice = TextData.rate + previousDiscount;
            //        TextData.rate += previousDiscount;
            //        double check_discount = 0;
            //        double ratePerItem = double.Parse(CartDataGridView.SelectedRows[0].Cells["rate"].Value.ToString()); ;
            //        // ********************************************************************************

            //        //GetSetData.Ids = data.UserPermissionsIds("product_id", "pos_products", "prod_name", TextData.prod_name);

            //        GetSetData.query = "select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
            //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //        TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString());
            //        pkg_db = data.NumericValues("pkg", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        tab_pieces_db = data.NumericValues("tab_pieces", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        TextData.disLimit = data.NumericValues("discount_limit", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        salePriceDb = data.NumericValues("sale_price", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        // *******************************************************************

            //        // **********************************************
            //        if (pkg_db == 0)
            //        {
            //            pkg_db = 1;
            //        }

            //        if (tab_pieces_db == 0)
            //        {
            //            tab_pieces_db = 1;
            //        }

            //        if (discountValue == "Yes")
            //        {
            //            //TextData.disLimit = (salePriceDb * TextData.disLimit) / 100;
            //            if (TextData.quantity > 0)
            //            {
            //                ratePerItem = TextData.rate / TextData.quantity;
            //            }
            //            else
            //            {
            //                ratePerItem = TextData.rate / 1;
            //            }

            //            check_discount = (ratePerItem * TextData.discount) / 100;
            //        }
            //        else
            //        {
            //            check_discount = TextData.discount;
            //        }

            //        // **********************************************
            //        if (check_discount <= TextData.disLimit)
            //        {
            //            if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets")
            //            {
            //                switch (txt_sale_option.Text)
            //                {
            //                    case "Pack":
            //                        TextData.quantity = (TextData.quantity / pkg_db) / tab_pieces_db;
            //                        TextData.rate = TextData.rate / TextData.quantity;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;

            //                    case "Pellet":
            //                        TextData.quantity = TextData.quantity / tab_pieces_db;
            //                        TextData.rate = TextData.rate / TextData.quantity;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;

            //                    case "Piece":
            //                        TextData.rate = TextData.quantity * TextData.rate;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;

            //                    case "Carton":
            //                        TextData.quantity = (TextData.quantity / pkg_db) / tab_pieces_db;
            //                        TextData.rate = TextData.rate / TextData.quantity;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;

            //                    case "Bag":
            //                        TextData.quantity = (TextData.quantity / pkg_db) / tab_pieces_db;
            //                        TextData.rate = TextData.rate / TextData.quantity;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;

            //                    default:
            //                        TextData.rate = TextData.rate / TextData.quantity;
            //                        previousDiscount = previousDiscount / TextData.quantity;
            //                        TextData.rate = TextData.quantity * (TextData.rate + previousDiscount);

            //                        if (discountValue == "Yes")
            //                        {
            //                            TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                        }
            //                        else
            //                        {
            //                            TextData.discount = TextData.quantity * TextData.discount;
            //                        }
            //                        break;
            //                }
            //            }
            //            else
            //            {
            //                TextData.rate = TextData.rate / TextData.quantity;
            //                previousDiscount = previousDiscount / TextData.quantity;
            //                TextData.rate = TextData.quantity * TextData.rate;

            //                if (discountValue == "Yes")
            //                {
            //                    TextData.discount = (TextData.rate * TextData.discount) / 100;
            //                }
            //                else
            //                {
            //                    TextData.discount = TextData.quantity * TextData.discount;
            //                }
            //            }

            //            //TextData.totalAmount = TextData.total_qty * TextData.rate;
            //            TextData.rate -= TextData.discount;
            //            CartDataGridView.SelectedRows[0].Cells["rate"].Value = Math.Round(TextData.rate, 2).ToString();
            //            CartDataGridView.SelectedRows[0].Cells["discount"].Value = Math.Round(TextData.discount, 2).ToString();

            //            TextData.rate += TextData.discount;
            //            TextData.net_total = (double.Parse(txt_sub_total.Text) + TextData.rate) - TextData.MarketPrice;
            //            txt_sub_total.Text = Math.Round(TextData.net_total, 2).ToString();

            //            if (TextData.net_total < 0)
            //            {
            //                txt_sub_total.Text = "0.00";
            //            }

            //            double totalDiscounts = 0;
            //            totalDiscounts = ((double.Parse(txt_discount.Text) + TextData.discount) - double.Parse(TextData.prod_price));
            //            txt_discount.Text = Math.Round(totalDiscounts, 2).ToString();
            //            txt_total_discount.Text = Math.Round(totalDiscounts, 2).ToString();

            //            if (TextData.discount < 0)
            //            {
            //                txt_discount.Text = "0.00";
            //                txt_total_discount.Text = "0.00";
            //            }


            //            decimal totalQuantity = 0;
            //            decimal totalDiscount = 0;
            //            decimal totalTaxation = 0;
            //            decimal totalAmount = 0;

            //            foreach (DataGridViewRow row in CartDataGridView.Rows)
            //            {
            //                if (!row.IsNewRow)
            //                {
            //                    // Check if the cell value is a valid decimal
            //                    if (decimal.TryParse(row.Cells[1].Value.ToString(), out decimal quantity))
            //                    {
            //                        totalQuantity += quantity;
            //                    }

            //                    // Check if the cell value is a valid decimal
            //                    if (decimal.TryParse(row.Cells[2].Value.ToString(), out decimal discountAmount))
            //                    {
            //                        totalDiscount += discountAmount;
            //                    }

            //                    // Check if the cell value is a valid decimal
            //                    if (decimal.TryParse(row.Cells[3].Value.ToString(), out decimal taxation))
            //                    {
            //                        totalTaxation += taxation;
            //                    }

            //                    // Check if the cell value is a valid decimal
            //                    if (decimal.TryParse(row.Cells[4].Value.ToString(), out decimal amount))
            //                    {
            //                        totalAmount += amount;
            //                    }
            //                }
            //            }

            //            txt_total_qty.Text = totalQuantity.ToString();
            //            txt_tax.Text = totalDiscount.ToString();
            //            txtTaxation.Text = totalTaxation.ToString();

            //            txt_sub_total.Text = (totalAmount - totalTaxation).ToString();

            //            txtGrandTotal.Text = totalAmount.ToString();


            //            int totalRows = CartDataGridView.RowCount;

            //            // Display the total number of rows in the TextBox
            //            txt_total_items.Text = totalRows.ToString();

            //            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Changing discount [" + TextData.prod_name + " price " + TextData.rate.ToString() + " Qty " + TextData.quantity.ToString() + "]" + "Discount " + TextData.discount, role_id);
            //        }
            //        else
            //        {
            //            error.errorMessage("Discount is exceeded from its limit!");
            //            error.ShowDialog();
            //        }
            //    }
            //}
            //catch (Exception es)
            //{
            //    //error.errorMessage(es.Message);
            //    //error.ShowDialog();
            //}
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    TextData.prod_name = "";
            //    TextData.prod_state = "";
            //    TextData.rate = 0;
            //    TextData.prod_price = "0";
            //    TextData.net_total = 0;
            //    TextData.quantity = 0;
            //    double previousDiscount = 0;
            //    double previousMarketPrice = 0;
            //    double prod_price = 0;

            //    if (txt_price.Text != "")
            //    {
            //        TextData.rate = double.Parse(txt_price.Text);

            //        TextData.prod_name = CartDataGridView.SelectedRows[0].Cells["Item_name"].Value.ToString();
            //        TextData.barcode = CartDataGridView.SelectedRows[0].Cells["note"].Value.ToString();
            //        TextData.quantity = double.Parse(CartDataGridView.SelectedRows[0].Cells["qty"].Value.ToString());
            //        prod_price = double.Parse(CartDataGridView.SelectedRows[0].Cells["rate"].Value.ToString()) + double.Parse(CartDataGridView.SelectedRows[0].Cells["discount"].Value.ToString());
            //        TextData.prod_price = prod_price.ToString();
            //        TextData.discount = double.Parse(CartDataGridView.SelectedRows[0].Cells["discount"].Value.ToString());
            //        previousMarketPrice = double.Parse(CartDataGridView.SelectedRows[0].Cells["market_price"].Value.ToString());
            //        //TextData.MarketPrice = previousMarketPrice;
            //        // ********************************************************************************

            //        //GetSetData.Ids = data.UserPermissionsIds("product_id", "pos_products", "prod_name", TextData.prod_name);

            //        GetSetData.query = "select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
            //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //        TextData.MarketPrice = data.NumericValues("market_value", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", GetSetData.Ids.ToString());
            //        double pkg_db = data.NumericValues("pkg", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());
            //        double tab_pieces_db = data.NumericValues("tab_pieces", "pos_stock_details", "prod_id", GetSetData.Ids.ToString());

            //        // **********************************************
            //        if (pkg_db == 0)
            //        {
            //            pkg_db = 1;
            //        }

            //        if (tab_pieces_db == 0)
            //        {
            //            tab_pieces_db = 1;
            //        }
            //        // **********************************************

            //        if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Tablets")
            //        {
            //            switch (txt_sale_option.Text)
            //            {
            //                case "Pack":
            //                    TextData.quantity = ((TextData.quantity / pkg_db) / tab_pieces_db);
            //                    //TextData.quantity = (TextData.quantity / tab_pieces_db);
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Pellet":
            //                    TextData.quantity = (TextData.quantity / tab_pieces_db);
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Piece":
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Carton":
            //                    TextData.quantity = ((TextData.quantity / pkg_db) / tab_pieces_db);
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                case "Bag":
            //                    TextData.quantity = ((TextData.quantity / pkg_db) / tab_pieces_db);
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;

            //                default:
            //                    TextData.rate = (TextData.quantity * TextData.rate);
            //                    //TextData.discount = TextData.quantity * TextData.discount;
            //                    //TextData.MarketPrice = TextData.quantity * TextData.MarketPrice;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            TextData.rate = (TextData.quantity * TextData.rate);
            //            //TextData.discount = TextData.quantity * TextData.discount;
            //        }

            //        double rateBeforeUpdate = TextData.rate;


            //        // calculating Tax
            //        TextData.MarketPrice = ((TextData.rate * TextData.MarketPrice) / 100);

            //        CartDataGridView.SelectedRows[0].Cells["market_price"].Value = Math.Round(TextData.MarketPrice, 2).ToString();

            //        // *************************************

            //        //TextData.rate -= TextData.discount;
            //        TextData.rate = (TextData.rate + TextData.MarketPrice) - TextData.discount;



            //        CartDataGridView.SelectedRows[0].Cells["discount"].Value = Math.Round(TextData.discount, 2).ToString();
            //        CartDataGridView.SelectedRows[0].Cells["rate"].Value = Math.Round(TextData.rate, 2).ToString();

            //        decimal totalQuantity = 0;
            //        decimal totalDiscount = 0;
            //        decimal totalTaxation = 0;
            //        decimal totalAmount = 0;

            //        foreach (DataGridViewRow row in CartDataGridView.Rows)
            //        {
            //            if (!row.IsNewRow)
            //            {
            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[1].Value.ToString(), out decimal quantity))
            //                {
            //                    totalQuantity += quantity;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[2].Value.ToString(), out decimal discountAmount))
            //                {
            //                    totalDiscount += discountAmount;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[3].Value.ToString(), out decimal taxation))
            //                {
            //                    totalTaxation += taxation;
            //                }

            //                // Check if the cell value is a valid decimal
            //                if (decimal.TryParse(row.Cells[4].Value.ToString(), out decimal amount))
            //                {
            //                    totalAmount += amount;
            //                }
            //            }
            //        }

            //        txt_total_qty.Text = totalQuantity.ToString();
            //        txt_tax.Text = totalDiscount.ToString();
            //        txtTaxation.Text = totalTaxation.ToString();

            //        txt_sub_total.Text = (totalAmount - totalTaxation).ToString();

            //        txtGrandTotal.Text = totalAmount.ToString();


            //        int totalRows = CartDataGridView.RowCount;

            //        // Display the total number of rows in the TextBox
            //        txt_total_items.Text = totalRows.ToString();

            //        //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Changing Item [" + TextData.prod_name + " price " + TextData.rate.ToString() + " Qty " + TextData.quantity.ToString() + "]", role_id);
            //    }
            //}
            //catch (Exception es)
            //{
            //    //error.errorMessage(es.Message);
            //    //error.ShowDialog();
            //}
        }

        private void regularItemsMouseHover(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn = sender as btnProduct;
                //Color bcol = Color.FromArgb(95, 130, 250);
                Color bcol = Color.LemonChiffon;
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void regularItemsMouseLeave(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn = sender as btnProduct;
                //Color bcol = Color.FromArgb(65, 105, 225);
                Color bcol = Color.White;
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;


            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool refresh_menu()
        {
            try
            {
                txtProductName.Text = "";
                group_counter = 0;
                pnl_list.Controls.Clear();
                lastCountedValue = 0;
                lblPageNo.Text = "1";
                //lblPageNo.Text = "Page";
                txt_barcode.Select();

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh_menu();
        }
        private void groups_Click(object sender, EventArgs e)
        {
            try
            {
                refresh_menu();
                TextData.general_options = "1";
                GetSetData.SetNextPreviousButtonValues("");

                //*****************************************************************************************
                GetSetData.query = @"WITH CounterRegularItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, *
	                                 from ViewCounterCashRegularItems) SELECT * FROM CounterRegularItems   
                                     where (status = 'Enabled') and (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                if (group_counter == 0)
                {
                    FillRegularItemsPanal(GetSetData.query, "regular");
                    group_counter = 1;
                }

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
                lblPageNo.Text = "1";
                //lblPageNo.Text = "Page 1";
                txt_barcode.Focus();
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Regular items button click...", role_id);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Exit...", role_id);

                //if (txtGrandTotal.Text != "")
                //{
                //    TextData.totalAmount = double.Parse(txtGrandTotal.Text);
                //}

                //if (TextData.totalAmount != 0)
                //{

                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_items_exists_in_cart != 0)
                {
                    sure.Message_choose("Are you sure you want to cancel order!");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        //DeletePaymentGrantors_db();
                        refresh_menu();

                        GetSetData.query = "delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        Menus.authorized_person = lbl_username.Text;
                        Menus.user_id = user_id;
                        Menus.role_id = role_id;

                        if (IsFormOpen(typeof(fromSecondScreen)))
                        {
                            fromSecondScreen.instance.Invoke((MethodInvoker)delegate
                            {
                                fromSecondScreen.instance.getCartItems();
                                fromSecondScreen.instance.isReturn();
                            });
                        }

                        Login_info.controllers.Button_controls.mainMenu_buttons();
                        
                        this.Dispose();
                    }
                }
                else
                {
                    //DeletePaymentGrantors_db();
                    refresh_menu();
              
                    Menus.authorized_person = lbl_username.Text;
                    Menus.user_id = user_id;
                    Menus.role_id = role_id;

                    Login_info.controllers.Button_controls.mainMenu_buttons();
                    this.Dispose();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clock_timer_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToLongTimeString();

            //lbl_customer.Text = TextData.invoiceCustomerName;
            //lblCustomerCode.Text = TextData.invoiceCustomerCode;
            //TextData.invoiceCustomerPoints = txtCustomerPoints.Text;

            //calculateAmountDue();
        } 

        private void loadingData()
        {
            try
            {
                //this.Invoke((MethodInvoker)delegate
                //{
                    GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "') and (status = '-2') ORDER BY id DESC;";
                    string isClockedOutExist = data.SearchStringValuesFromDb(GetSetData.query);


                if (isClockedOutExist != "")
                {
                    sure.Message_choose("Sorry your shift is closed. Would you like to re-active your shift again!");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        GetSetData.query = @"update pos_clock_in set status  = '-1' where (id = '" + isClockedOutExist + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        GetSetData.query = @"delete from pos_clock_out where (clock_in_id = '" + isClockedOutExist + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //******************************************

                        int employee_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "emp_id", "pos_users", "user_id", user_id.ToString());

                        string employee_name = data.UserPermissions("full_name", "pos_employees", "employee_id", employee_id_db.ToString());
                        lbl_employee.Text = employee_name;

                        pos_permissions();
                        TextData.return_value = false;
                        TextData.promotionDiscount = 0;
                        clock_timer.Start();
                        //settingDefaultSaleOption();
                        txt_date.Text = DateTime.Now.ToShortDateString();
                        dates.Text = DateTime.Now.ToShortDateString();
                        fun_get_employee_balance();
                        getCreditCardBalance();
                        getCreditedBalance();
                        getCategoies();
                        TextData.comments = "";

                        login_by_user();

                        macAddress = GetSetData.GetSystemMacAddress();

                        holdItemsBackgroundWorker.RunWorkerAsync();

                        GetSetData.Data = data.UserPermissions("task_schedule", "pos_onedrive_options");

                        if (GetSetData.Data != "")
                        {
                            switch (GetSetData.Data)
                            {
                                case "30 Minutes":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
                                    break;

                                case "1 Hour":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(1));
                                    break;

                                case "6 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(6));
                                    break;

                                case "12 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(12));
                                    break;

                                case "20 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(20));
                                    break;

                                case "1 Day":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(24));
                                    break;

                                case "Weekly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(7));
                                    break;

                                case "Quarterly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(90));
                                    break;

                                case "Yearly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(365));
                                    break;

                                default:

                                    int is_already_exist = data.UserPermissionsIds("id", "pos_onedrive_options");

                                    if (is_already_exist == 0)
                                    {
                                        GetSetData.query = @"insert into pos_onedrive_options values ('False', 'False', 'False', 'False', 'False', '', 'OneDrive');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }

                                    //timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(1));
                                    break;

                            }
                        }


                        txt_barcode.Focus();

                        fun_clear_cart_records();
                    }
                    else
                    {
                        fun_clear_cart_records();

                        Menus.authorized_person = lbl_username.Text;
                        Menus.user_id = user_id;
                        Menus.role_id = role_id;

                        Login_info.controllers.Button_controls.mainMenu_buttons();
                        this.Dispose();
                    }
                }
                else
                {
                    GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') ORDER BY id DESC;";
                    string is_already_clocked_in = data.SearchStringValuesFromDb(GetSetData.query);

                    if (is_already_clocked_in != "")
                    {
                        int employee_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "emp_id", "pos_users", "user_id", user_id.ToString());

                        string employee_name = data.UserPermissions("full_name", "pos_employees", "employee_id", employee_id_db.ToString());
                        lbl_employee.Text = employee_name;

                        pos_permissions();
                        TextData.return_value = false;
                        TextData.promotionDiscount = 0;
                        clock_timer.Start();
                        //settingDefaultSaleOption();
                        txt_date.Text = DateTime.Now.ToShortDateString();
                        dates.Text = DateTime.Now.ToShortDateString();
                        fun_get_employee_balance();
                        getCreditCardBalance();
                        getCreditedBalance();
                        getCategoies();
                        TextData.comments = "";

                        login_by_user();

                        macAddress = GetSetData.GetSystemMacAddress();


                        string singleAuthorityClosing = generalSettings.ReadField("singleAuthorityClosing");


                        if (singleAuthorityClosing == "Yes")
                        {
                            batchWiseAutoClockIn();
                        }

                        holdItemsBackgroundWorker.RunWorkerAsync();


                        GetSetData.Data = data.UserPermissions("task_schedule", "pos_onedrive_options");

                        if (GetSetData.Data != "")
                        {
                            switch (GetSetData.Data)
                            {
                                case "30 Minutes":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
                                    break;

                                case "1 Hour":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(1));
                                    break;

                                case "6 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(6));
                                    break;

                                case "1 Day":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(24));
                                    break;

                                case "12 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(12));
                                    break;

                                case "20 Hours":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromHours(20));
                                    break;

                                case "Weekly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(7));
                                    break;

                                case "Quarterly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(90));
                                    break;

                                case "Yearly":
                                    timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(365));
                                    break;

                                default:

                                    int is_already_exist = data.UserPermissionsIds("id", "pos_onedrive_options");

                                    if (is_already_exist == 0)
                                    {
                                        GetSetData.query = @"insert into pos_onedrive_options values ('False', 'False', 'False', 'False', 'False', '', 'OneDrive');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);
                                    }

                                    //timer = new System.Threading.Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromDays(1));
                                    break;

                            }
                        }


                        txt_barcode.Focus();

                        fun_clear_cart_records();
                    }
                    else
                    {
                        fun_clear_cart_records();

                        error.errorMessage("Sorry you are not clock-in yet. Please go to clock-in/out and clock-in first.");
                        error.ShowDialog();

                        formClockInDetails.user_id = user_id;
                        formClockInDetails.role_id = role_id;
                        formClockInDetails _obj = new formClockInDetails();
                        _obj.Show();
                        this.Dispose();
                    }
                }
                //});
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_counter_sales_Shown(object sender, EventArgs e)
        {
            try
            {
                loadingData();

                //form_loading loadingForm = new form_loading();
                //loadingForm.SetLoadingMessage("Loading Data...");
                //Screen secondaryScreen = Screen.PrimaryScreen;
                //loadingForm.Location = secondaryScreen.WorkingArea.Location;
                ////loadingForm.TopMost = true;
                //loadingForm.Show();

                //Thread loading = new Thread(() => LoadThreadMethod((message) =>
                //{
                //    this.Invoke((MethodInvoker)delegate
                //    {
                //        loadingForm.Dispose();
                //    });
                //}));

                //loading.Start();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void LoadThreadMethod(Action<string> callback)
        {
            try
            {
                loadingData();

                this.Invoke(new Action(() =>
                {
                    callback?.Invoke("Data Loaded...");
                }));
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool batchWiseAutoClockIn()
        {
            try
            {
                GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                string is_already_clocked_in = data.SearchStringValuesFromDb(GetSetData.query);

                if (is_already_clocked_in != "")
                {
                    string shift_id = "";
                    string counter_id = "";


                    //*********************************

                    SqlConnection connForCounter = new SqlConnection(webConfig.con_string);

                    string queryForCounters = "select id from pos_counter;";


                    SqlCommand cmdCounters;
                    SqlDataReader readerCounters;

                    cmdCounters = new SqlCommand(queryForCounters, connForCounter);

                    connForCounter.Open();
                    readerCounters = cmdCounters.ExecuteReader();


                    while (readerCounters.Read())
                    {
                        GetSetData.query = "SELECT count(pos_clock_in.counter_id) from pos_clock_in where (pos_clock_in.status = '-1' or pos_clock_in.status = '0') and (counter_id = '" + readerCounters["id"].ToString() + "');";
                        double allocatedShifts = data.SearchNumericValuesDb(GetSetData.query);
                    

                        double counterShiftLimitDb = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "shift_limit", "pos_counter", "id", readerCounters["id"].ToString());
                        string counterName = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "title", "pos_counter", "id", readerCounters["id"].ToString());

                        if (counterName == "default")
                        {
                            if (allocatedShifts <= counterShiftLimitDb)
                            {
                                counter_id = readerCounters["id"].ToString();
                                break;
                            }
                        }
                        else
                        {
                            if (allocatedShifts < counterShiftLimitDb)
                            {
                                counter_id = readerCounters["id"].ToString();
                                break;
                            }
                        }
                    }

                    readerCounters.Close();
                    connForCounter.Close();

                    //*********************************

                    if (counter_id != "")
                    {
                        string queryForShifts = "select id from pos_shift;";


                        SqlConnection conn = new SqlConnection(webConfig.con_string);
                        SqlCommand cmd;
                        SqlDataReader reader;

                        cmd = new SqlCommand(queryForShifts, conn);

                        conn.Open();
                        reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (status = '-1' or status = '0') and (counter_id = '" + counter_id + "') and (shift_id = '" + reader["id"].ToString() + "') ORDER BY id DESC;";
                            string is_shift_exists = data.SearchStringValuesFromDb(GetSetData.query);

                            if (is_shift_exists == "")
                            {
                                shift_id = reader["id"].ToString();
                                break;
                            }
                        }

                        reader.Close();
                        conn.Close();
                        //*********************************

                        if (shift_id != "")
                        {

                            GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                            double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

                            if (clock_in_id != 0)
                            {
                                string start_time = "";
                                start_time = lbl_time.Text;

                                string counteroOpeningAmount = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "opening_amount", "pos_counter", "id", counter_id.ToString());

                                GetSetData.query = @"update pos_clock_in set amount = '" + counteroOpeningAmount + "',  shift_id = '" + shift_id.ToString() + "', counter_id = '" + counter_id.ToString() + "' where (id = '" + clock_in_id.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);


                                return true;
                            }

                        }
                        else
                        {
                            error.errorMessage("Sorry no shift available now. Please create new shift to proceed!");
                            error.ShowDialog();

                            formClockInDetails.user_id = user_id;
                            formClockInDetails.role_id = role_id;
                            formClockInDetails _obj = new formClockInDetails();
                            _obj.Show();
                            this.Dispose();
                        }
                    }
                    else
                    {
                        error.errorMessage("Sorry no counter available now. Please create a new counter to proceed!");
                        error.ShowDialog();

                        formClockInDetails.user_id = user_id;
                        formClockInDetails.role_id = role_id;
                        formClockInDetails _obj = new formClockInDetails();
                        _obj.Show();
                        this.Dispose();
                    }
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

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
            data.insertUpdateCreateOrDelete(GetSetData.query);

            lbl_customer.Text = "";
            txt_status.Checked = false;
            btnReturn.FillColor = Color.DodgerBlue;
            lblCustomerCode.Text = "";
            fun_clear_cart_records();
            clearDataGridView();

            fun_get_employee_balance();
            getCreditCardBalance();
            getCreditedBalance();

            txt_barcode.Select();
        }

        private void discountOnCurrentBill()
        {
            try
            {
                using (form_discount add_customer = new form_discount())
                {
                    //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Discount button click...", role_id);
                    TextData.discount = 0;
                    add_customer.ShowDialog();
                    txt_discount.Text = TextData.discount.ToString();
                    //txt_total_discount.Text = TextData.discount.ToString();
                    txt_barcode.Focus();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void discount_Click(object sender, EventArgs e)
        {
            discountOnCurrentBill();
        }

        private void price_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_price.Text, e);
        }

        private void qty_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_qty.Text, e);
        }

        private void discount_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_discount.Text, e);
        }

        private void tax_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_tax.Text, e);
        }

        //////private string auto_generate_sales_code()
        //{
        //    TextData.billNo = "";

        //    try
        //    {
        //        GetSetData.query = @"SELECT top 1 salesCodes FROM pos_AllCodes order by id desc;";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        GetSetData.Ids++;

        //        GetSetData.query = @"update pos_AllCodes set salesCodes = '" + GetSetData.Ids.ToString() + "';";
        //        data.insertUpdateCreateOrDelete(GetSetData.query);
        //        //**********************************************************************************************

        //        TextData.billNo = "SALE_" + GetSetData.Ids.ToString();

        //        return TextData.billNo;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        return TextData.billNo;
        //    }
        //}

        private string auto_generate_return_code()
        {
            TextData.billNo = "";

            try
            {
                GetSetData.Ids = 0;

                GetSetData.query = @"SELECT top 1 salesReturnsCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids = GetSetData.Ids + 1;

                GetSetData.query = @"update pos_AllCodes set salesReturnsCodes = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                TextData.billNo = "RETURN_" + GetSetData.Ids.ToString();

                return TextData.billNo;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.billNo;
            }
        }

        private string auto_generate_hold_items_code()
        {
            TextData.billNo = "";

            try
            {
                GetSetData.Ids = 0;

                GetSetData.query = @"SELECT top 1 holdItemsCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids = GetSetData.Ids + 1;

                GetSetData.query = @"update pos_AllCodes set holdItemsCodes = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                TextData.billNo = "HOLD_" + GetSetData.Ids.ToString();

                return TextData.billNo;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.billNo;
            }
        }

        private bool fun_insert_records_into_sales_as_Credits()
        {
            try
            {
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.no_of_items = double.Parse(txt_total_items.Text);
                TextData.total_qty = double.Parse(txt_total_qty.Text);
                TextData.net_total = double.Parse(txtGrandTotal.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);
                TextData.discount = double.Parse(txt_discount.Text);
                TextData.tax = txt_tax.Text;
                TextData.totalAmount = double.Parse(txt_amount_due.Text);
                txt_credits.Text = TextData.credits.ToString();
                TextData.lastCredit = double.Parse(txt_lastCredits.Text);
                TextData.return_value = false;
                TextData.cash = double.Parse(TextData.send_cash);

                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                if (txt_status.Checked == false)
                {
                    TextData.status = "Sale";
                }
                else
                {
                    TextData.status = "Return";
                }

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //*****************************************************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                TextData.credits_limit = data.NumericValues("credit_limit", "pos_customers", "customer_id", customer_id_db.ToString());
                TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());

                TextData.lastCredit = TextData.LastCredits_db;
                TextData.lastCredit += TextData.credits;

                if (employee_id_db != 0)
                {
                    if (TextData.lastCredit <= TextData.credits_limit)
                    {
                        if (TextData.totalAmount != 0)
                        {
                            //TextData.billNo = auto_generate_sales_code();

                            GetSetData.query = "insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.send_cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation.ToString() + "' , '0' , '0' , '0' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_sales_accounts", "billNo", TextData.billNo);

                            // *****************************************************************************************
                            double totalCommission = 0;

                            GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

                            SqlConnection conn = new SqlConnection(webConfig.con_string);
                            SqlCommand cmd;
                            SqlDataReader reader;

                            cmd = new SqlCommand(GetSetData.query, conn);

                            conn.Open();
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                string product_id_db = reader["product_id"].ToString();
                                string stock_id_db = reader["stock_id"].ToString();

                                TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
                                double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db);
                                TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
                                double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                                double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db);
                                double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


                                GetSetData.query = "select title from pos_category where (category_id = '" + reader["category_id"].ToString() + "');";
                                TextData.category = data.SearchStringValuesFromDb(GetSetData.query);
                                // **************************************************************************************

                                string isStockLimitSetToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");


                                if (double.Parse(reader["quantity"].ToString()) <= quantity_db || TextData.category == "Services" || TextData.category == "MISCELLANEOUS" || isStockLimitSetToZero == "No")
                                {
                                    quantity_db = quantity_db - double.Parse(reader["quantity"].ToString());
                                    TextData.total_pur_price = pur_price_db * quantity_db;
                                    TextData.total_sales_price = sale_price_db * quantity_db;

                                    //*********************************************************

                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Sale Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    //*********************************************************

                                    // update Products quantity from datagridview:
                                    GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                else
                                {
                                    error.errorMessage("Available stock is " + quantity_db.ToString());
                                    error.ShowDialog();
                                }
                            // **************************************************************************************

                                TextData.full_pkg = 0;

                                if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                                {
                                    TextData.quantity = double.Parse(reader["quantity"].ToString());
                                    TextData.full_pkg = TextData.quantity / TextData.pkg;
                                }

                                TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                                TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());

                                // **************************************************************************************

                                #region

                                GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() +"');";
                                int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                double commissionAmount = 0;

                                if (commission_id != 0)
                                {
                                    GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                                    int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    if (commission_id_db != 0)
                                    {
                                        bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                        double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                        double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                                        if (is_commission_in_percentage)
                                        {
                                            commissionAmount = ((double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString())) * commissionInPercentageDb) / 100;
                                        }
                                        else
                                        {
                                            commissionAmount = commissionAmountDb;

                                            commissionAmount *= double.Parse(reader["quantity"].ToString());
                                        }


                                       totalCommission += commissionAmount;
                                    }

                                }

                                GetSetData.query = @"insert into pos_sales_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "', '" + TextData.wholeSale.ToString() + "', '" + reader["note"].ToString() + "', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                #endregion
                            }

                            // *****************************************************************************************

                            double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                            double saleCommission = totalCommission;

                            totalCommission += employeeCommissionDb;

                            GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            GetSetData.query = @"update pos_sales_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (sales_acc_id = '" + sales_acc_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            // *****************************************************************************************

                            GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            reader.Close();
                            conn.Close();
                            // *****************************************************************************************

                            #region

                            TextData.lastCredit = double.Parse(txt_lastCredits.Text);

                            //  Customer Transactions *******************************************************************************
                            GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Customer Sales' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            if (form_ticket_history.ticketNumber != "")
                            {
                                GetSetData.query = "update pos_tickets set status = 'Completed' where (billNo = '" + form_ticket_history.ticketNumber + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
    

                            //GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                            //if (GetSetData.Data == "Yes")
                            //{
                            //    // *****************************************************************************************
                            //    TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                            //    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                            //    {
                            //        TextData.totalAmount = double.Parse(TextData.totalCapital) + double.Parse(TextData.send_cash);
                            //    }

                            //    GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                            //    data.insertUpdateCreateOrDelete(GetSetData.query);
                            //    // *****************************************************************************************
                            //}

                            #endregion

                            TextData.return_value = true;
                        }
                    }
                    else
                    {
                        error.errorMessage(TextData.customer_name + " credit is exceeding from its Limit!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the Employee first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                TextData.return_value = false;
                return false;
            }
        }

        private bool fun_insert_regular_sale_records_into_db()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                TextData.LastCredits_db = 0;

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }

                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                if (TextData.cash_on_hand == "")
                {
                    TextData.cash_on_hand = "0";
                }

                //*****************************************************************************************
                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                string customer_id_db = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                //if (TextData.totalAmount != 0)
                //{
                //TextData.billNo = auto_generate_sales_code();

                string useSubCharges = data.UserPermissions("useSurcharges", "pos_general_settings");

                double surchages = 0;

                if (useSubCharges == "Yes")
                {
                    string subChargesPercentage = data.UserPermissions("surchargePercentage", "pos_general_settings");

                    surchages = (TextData.totalAmount * double.Parse(subChargesPercentage)) / 100;
                }

                if (TextData.credit_card_amount != 0)
                {
                    GetSetData.query = @"insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "' , '" + TextData.total_taxation.ToString() + "' , '" + TextData.credit_card_amount.ToString() + "' , '0' , '0' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '"+ Math.Round(surchages,2).ToString() + "', '" + form_ticket_history.advanceAmount + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else if (TextData.paypal_amount != 0)
                {
                    GetSetData.query = @"insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "' , '" + TextData.total_taxation.ToString() + "' , '0' , '" + TextData.paypal_amount.ToString() + "', '0' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else if (TextData.google_pay_amount != 0)
                {
                    GetSetData.query = @"insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "' , '" + TextData.total_taxation.ToString() + "' , '0', '0' , '" + TextData.google_pay_amount.ToString() + "' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "' , '" + TextData.total_taxation.ToString() + "' , '0' , '0' , '0' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                GetSetData.query = @"select sales_acc_id from pos_sales_accounts where (billNo = '" + TextData.billNo.ToString() + "');";
                int sales_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                // *****************************************************************************************
            
                double totalCommission = 0;
               
                GetSetData.query = "select * from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string product_id_db = reader["product_id"].ToString();
                    string stock_id_db = reader["stock_id"].ToString();
                    

                    TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
                    double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db);
                    TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
                    double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                    double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db);
                    double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


                    GetSetData.query = "select title from pos_category where (category_id = '" + reader["category_id"].ToString() + "');";
                    TextData.category = data.SearchStringValuesFromDb(GetSetData.query);
                    // **************************************************************************************

                        string isStockLimitSetToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");


                    if (double.Parse(reader["quantity"].ToString()) <= quantity_db || TextData.category == "Services" || TextData.category == "MISCELLANEOUS" || isStockLimitSetToZero == "No")
                    {
                        quantity_db = quantity_db - double.Parse(reader["quantity"].ToString());
                        TextData.total_pur_price = pur_price_db * quantity_db;
                        TextData.total_sales_price = sale_price_db * quantity_db;

                        //*********************************************************

                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Sale Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //*********************************************************


                        // update Products quantity from datagridview:
                        GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                    }
                    else
                    {
                        error.errorMessage("Available stock is " + quantity_db.ToString());
                        error.ShowDialog();
                    }
                    // **************************************************************************************

                    TextData.full_pkg = 0;

                    if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                    {
                        TextData.quantity = double.Parse(reader["quantity"].ToString());
                        TextData.full_pkg = TextData.quantity / TextData.pkg;
                    }

                    TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                    TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());

                    // **************************************************************************************


                    //GetSetData.query = @"update pos_purchase_imei set isSold = 'True'  where (imei_no = '" + CartDataGridView.Rows[i].Cells[8].Value.ToString() + "');";
                    //data.insertUpdateCreateOrDelete(GetSetData.query);
                    // **************************************************************************************


                    #region

                    GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() + "');";
                    int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    double commissionAmount = 0;

                    if (commission_id != 0)
                    {
                        GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                        int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (commission_id_db != 0)
                        {
                            bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                            double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                            double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                            if (is_commission_in_percentage)
                            {
                                commissionAmount = ((double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString())) * commissionInPercentageDb) / 100;
                            }
                            else
                            {
                                commissionAmount = commissionAmountDb;

                                commissionAmount *= double.Parse(reader["quantity"].ToString());
                            }

                            totalCommission += commissionAmount;
                        }
                    }


                    GetSetData.query = @"insert into pos_sales_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "','" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "' , '" + TextData.wholeSale.ToString() + "', '" + reader["note"].ToString() + "', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    #endregion
                }

                double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                double saleCommission = totalCommission;

                totalCommission += employeeCommissionDb;

                //**************************************************************************************
                GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);


                GetSetData.query = @"update pos_sales_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (sales_acc_id = '" + sales_acc_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //**************************************************************************************

                GetSetData.query = @"delete from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                reader.Close();
                conn.Close();

                //**************************************************************************************

                string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                if (autoSetPoints == "Yes")
                {
                    double currentPoints = 0;

                    if (txtCustomerPoints.Text != "")
                    {
                        currentPoints = double.Parse(txtCustomerPoints.Text);
                    }

                    string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");


                    string autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");


                    if (autoRedeemPoints == "Yes")
                    {
                        string pointsRedeemLimit = data.UserPermissions("pointsRedeemLimit", "pos_general_settings");

                        double totalPoints = currentPoints;
                        double setNewPointsInDb = currentPoints;


                        while (totalPoints >= double.Parse(pointsRedeemLimit))
                        {
                            totalPoints -= double.Parse(pointsRedeemLimit);

                            if (totalPoints <= 0)
                            {
                                totalPoints = 0;
                            }

                            setNewPointsInDb = Math.Round(totalPoints, 2);
                        }

                        GetSetData.query = @"update pos_customers set points = '" + setNewPointsInDb.ToString() + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        GetSetData.query = @"update pos_customers set points = '" + txtCustomerPoints.Text + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }


                    string autoZeroCustomerDiscount = data.UserPermissions("autoZeroCustomerDiscount", "pos_general_settings");


                    if (autoZeroCustomerDiscount == "Yes")
                    {
                        GetSetData.query = @"update pos_customers set discount = '0' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }


                //*****************************************************************************************

                TextData.lastCredit = double.Parse(txt_lastCredits.Text);

                //  Customer Transactions *******************************************************************************
                GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Customer Sales' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);



                //*****************************************************************************************

                GetSetData.query = "select * from pos_cart_items where (is_return = 'true') and (mac_address = '" + macAddress + "')";

                SqlConnection returnConn = new SqlConnection(webConfig.con_string);
                SqlCommand returnCmd;
                SqlDataReader returnReader;

                returnCmd = new SqlCommand(GetSetData.query, returnConn);

                returnConn.Open();
                returnReader = returnCmd.ExecuteReader();

                while (returnReader.Read())
                {
                    string product_id_db = returnReader["product_id"].ToString();
                    string stock_id_db = returnReader["stock_id"].ToString();

                    singleItemReturned(product_id_db, stock_id_db, employee_id_db, customer_id_db, clock_in_id);
                }

                returnReader.Close();
                returnConn.Close();


                if (form_ticket_history.ticketNumber != "")
                {
                    GetSetData.query = "update pos_tickets set status = 'Completed' where (billNo = '" + form_ticket_history.ticketNumber + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                #region
                //*****************************************************************************************
                //GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                //if (GetSetData.Data == "Yes")
                //{
                //    TextData.totalAmount = 0;
                //    TextData.totalCapital = "0";
                //    TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                //    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                //    {
                //        TextData.totalAmount = double.Parse(TextData.totalCapital) + TextData.cash;
                //    }

                //    GetSetData.query = @"Update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                //    data.insertUpdateCreateOrDelete(GetSetData.query);
                //    // *****************************************************************************************
                //}
                //}

                #endregion

                TextData.return_value = true;
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void singleItemReturned(string product_id_db, string stock_id_db, int employee_id_db, string customer_id_db, string clock_in_id)
        {
            try
            {
                TextData.dates = txt_date.Text;
                TextData.no_of_items = 1;

                GetSetData.query = "select sum(quantity) from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                TextData.total_qty = data.SearchNumericValuesDb(GetSetData.query);

                GetSetData.query = "select sum(total_amount) from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                TextData.net_total = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select sum(tax) from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                TextData.total_taxation = data.SearchNumericValuesDb(GetSetData.query);
                TextData.tax = TextData.total_taxation.ToString();

                GetSetData.query = "select sum(discount) from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                TextData.discount = data.SearchNumericValuesDb(GetSetData.query);


                TextData.totalAmount = TextData.net_total - TextData.discount;
                TextData.cash = 0;
                TextData.credits = TextData.totalAmount;
                txt_credits.Text = TextData.credits.ToString();
                TextData.return_value = false;
                TextData.checkSaleStatus = "Return";


                TextData.cash = TextData.totalAmount - double.Parse(form_ticket_history.advanceAmount);
                TextData.status = "Return";
                TextData.returnStatus = "Return";
                TextData.cash_on_hand = "0";
                TextData.remaining_amount = "0";

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }


                //*****************************************************************************************
                TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());
                TextData.lastCredit = TextData.LastCredits_db;


                TextData.lastCredit -= TextData.credits;

                if (TextData.lastCredit < 0)
                {
                    TextData.lastCredit = 0;
                }

                //if (TextData.totalAmount != 0)
                //{
                TextData.billNo = auto_generate_return_code();

                //************************************************************************************** 
                double newPoints = 0;

                string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                if (autoSetPoints == "Yes")
                {
                    string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");

                    double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", customer_id_db.ToString());

                    newPoints = customer_points_db - ((TextData.totalAmount - TextData.total_taxation) / double.Parse(addPointPerAmount));

                    if (newPoints < 0)
                    {
                        newPoints = 0;
                    }

                    GetSetData.query = @"update pos_customers set points = '" + newPoints.ToString() + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                //*****************************************************************************************


                GetSetData.query = @"insert into pos_return_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "', '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation + "' , '0' , '0' , '0' , '" + clock_in_id + "', '" + newPoints.ToString() + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount +"');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
         
                //*****************************************************************************************

                GetSetData.query = "update pos_sales_accounts set is_returned = 'true' where (billNo = '" + TextData.returnBillNo + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //*****************************************************************************************

                int sales_acc_id_db = data.UserPermissionsIds("return_acc_id", "pos_return_accounts", "billNo", TextData.billNo);

                //*****************************************************************************************

                double totalCommission = 0;

                GetSetData.query = "select quantity from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                double cartQuantity = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select total_amount from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                double cartTotalAmount = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select tax from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                double cartTax = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select discount from pos_cart_items where (product_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (mac_address = '" + macAddress + "');";
                double cartDiscount = data.SearchNumericValuesDb(GetSetData.query);


                TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db.ToString());
                double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());
                TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db.ToString());
                double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                // **************************************************************************************


                quantity_db = quantity_db + cartQuantity;
                TextData.total_pur_price = pur_price_db * quantity_db;
                TextData.total_sales_price = sale_price_db * quantity_db;

                //*********************************************************

                string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


                GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Return Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //*********************************************************

                // update Products quantity from datagridview:
                GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                // **************************************************************************************

                TextData.full_pkg = 0;

                if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                {
                    TextData.quantity = cartQuantity;
                    TextData.full_pkg = TextData.quantity / TextData.pkg;
                }

                TextData.total_pur_price = pur_price_db * cartQuantity;
                TextData.wholeSale = wholeSalePrice_db * cartQuantity;


                //GetSetData.query = @"update pos_purchase_imei set isSold = 'False'  where (imei_no = '" + CartDataGridView.Rows[i].Cells[8].Value.ToString() + "');";
                //data.insertUpdateCreateOrDelete(GetSetData.query);
                // **************************************************************************************

                // **************************************************************************************

                #region

                GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() + "');";
                int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                double commissionAmount = 0;

                if (commission_id != 0)
                {
                    GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                    int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (commission_id_db != 0)
                    {
                        bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                        double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                        double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                        if (is_commission_in_percentage)
                        {
                            commissionAmount = ((cartTotalAmount - cartDiscount) * commissionInPercentageDb) / 100;
                        }
                        else
                        {
                            commissionAmount = commissionAmountDb;

                            commissionAmount *= cartQuantity;
                        }


                        totalCommission += commissionAmount;
                    }
                }

                GetSetData.query = @"insert into pos_returns_details values ('" + cartQuantity.ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + cartDiscount.ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + cartTotalAmount.ToString() + "', '" + cartTax.ToString() + "', '" + TextData.wholeSale.ToString() + "' , 'nill', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                #endregion

                // **************************************************************************************

                if (TextData.LastCredits_db != -1 && TextData.LastCredits_db >= 0 && TextData.customer_name != "nill")
                {
                    GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredit.ToString() + "' where customer_id = '" + customer_id_db.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                // **************************************************************************************

                double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                double returnCommission = totalCommission;

                totalCommission = employeeCommissionDb - totalCommission;

                if (totalCommission < 0)
                {
                    totalCommission = 0;
                }

                GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);


                GetSetData.query = @"update pos_return_accounts set employeeCommission = '" + Math.Round(returnCommission, 2).ToString() + "' where (return_acc_id = '" + sales_acc_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                // **************************************************************************************

                GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);



                TextData.lastCredit = double.Parse(txt_lastCredits.Text);

                //  Customer Transactions *******************************************************************************
                GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '0' , '" + TextData.credits.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Customer Returned' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);


                if (form_ticket_history.ticketNumber != "")
                {
                    GetSetData.query = "update pos_tickets set status = 'Completed' where (billNo = '" + form_ticket_history.ticketNumber + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //    GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                //    if (GetSetData.Data == "Yes")
                //    {
                //        // *****************************************************************************************
                //        TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                //        if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                //        {
                //            TextData.totalAmount = double.Parse(TextData.totalCapital) - TextData.totalAmount;
                //        }

                //        if (TextData.totalAmount <= 0)
                //        {
                //            TextData.totalAmount = 0;
                //        }
                //        GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                //        data.insertUpdateCreateOrDelete(GetSetData.query);
                //        // *****************************************************************************************
                //    }

                //  
                //    //}
                //}
                //else
                //{
                //    error.errorMessage("Cash must be less than or equal to Due Amount!");
                //    error.ShowDialog();
                //}

                TextData.returnBillNo = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool fun_insert_Credited_customers_records_into_db()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;

                if (lbl_customer.Text == "")
                {
                    TextData.customer_name = "nill";

                    GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                    TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                    lblCustomerCode.Text = TextData.customerCode;
                }

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }

                if (TextData.comments == "")
                {
                    TextData.comments = "";
                }

                if (TextData.cash_on_hand == "")
                {
                    TextData.cash_on_hand = "0";
                }

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //*****************************************************************************************
                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());

                //************************************************************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                //if (TextData.totalAmount != 0)
                //{
                //TextData.billNo = auto_generate_sales_code();

                GetSetData.query = "insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation.ToString() + "' , '0' , '0' , '0' , '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '" + TextData.tipAmount +"', '" + form_ticket_history.advanceAmount + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_sales_accounts", "billNo", TextData.billNo);
                // *****************************************************************************************

                double totalCommission = 0;

                GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string product_id_db = reader["product_id"].ToString();
                    string stock_id_db = reader["stock_id"].ToString();

                    TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
                    double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db);
                    TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
                    double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                    double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db);
                    double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


                    GetSetData.query = "select title from pos_category where (category_id = '" + reader["category_id"].ToString() + "');";
                    TextData.category = data.SearchStringValuesFromDb(GetSetData.query);
                    // **************************************************************************************

                    string isStockLimitSetToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");


                    if (double.Parse(reader["quantity"].ToString()) <= quantity_db || TextData.category == "Services" || TextData.category == "MISCELLANEOUS" || isStockLimitSetToZero == "No")
                    {
                        quantity_db = quantity_db - double.Parse(reader["quantity"].ToString());
                        TextData.total_pur_price = pur_price_db * quantity_db;
                        TextData.total_sales_price = sale_price_db * quantity_db;

                        //*********************************************************

                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Sale Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //*********************************************************

                        // update Products quantity from datagridview:
                        GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        error.errorMessage("Available stock is " + quantity_db.ToString());
                        error.ShowDialog();
                    }
                    // **************************************************************************************

                    TextData.full_pkg = 0;

                    if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                    {
                        TextData.quantity = double.Parse(reader["quantity"].ToString());
                        TextData.full_pkg = TextData.quantity / TextData.pkg;
                    }

                    TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                    TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());


                    //GetSetData.query = @"update pos_purchase_imei set isSold = 'True'  where (imei_no = '" + CartDataGridView.Rows[i].Cells[8].Value.ToString() + "');";
                    //data.insertUpdateCreateOrDelete(GetSetData.query);
                    // **************************************************************************************

                    // **************************************************************************************

                    #region

                    GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() + "');";
                    int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    double commissionAmount = 0;

                    if (commission_id != 0)
                    {
                        GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                        int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (commission_id_db != 0)
                        {
                            bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                            double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                            double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                            if (is_commission_in_percentage)
                            {
                                commissionAmount = ((double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString())) * commissionInPercentageDb) / 100;
                            }
                            else
                            {
                                commissionAmount = commissionAmountDb;

                                commissionAmount *= double.Parse(reader["quantity"].ToString());
                            }


                            totalCommission += commissionAmount;
                        }
                    }


                    GetSetData.query = @"insert into pos_sales_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "', '" + TextData.wholeSale.ToString() + "', '" + reader["note"].ToString() + "', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    #endregion

                    // **************************************************************************************

                    if (TextData.LastCredits_db == -1 && TextData.customer_name != "nill")
                    {
                        GetSetData.query = @"insert into pos_customer_lastCredits values ('" + TextData.lastCredit.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else if (TextData.LastCredits_db != -1 && TextData.LastCredits_db >= 0 && TextData.customer_name != "nill")
                    {
                        GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredit.ToString() + "' where customer_id = '" + customer_id_db.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **************************************************************************************  
                }

                // *****************************************************************************************

                double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                double saleCommission = totalCommission;

                totalCommission += employeeCommissionDb;

                GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);


                GetSetData.query = @"update pos_sales_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (sales_acc_id = '" + sales_acc_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                // *****************************************************************************************

                GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                reader.Close();
                conn.Close();
                //**************************************************************************************

                string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                if (autoSetPoints == "Yes")
                {
                    double currentPoints = 0;

                    if (txtCustomerPoints.Text != "")
                    {
                        currentPoints = double.Parse(txtCustomerPoints.Text);
                    }

                    string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");


                    string autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");


                    if (autoRedeemPoints == "Yes")
                    {
                        string pointsRedeemLimit = data.UserPermissions("pointsRedeemLimit", "pos_general_settings");

                        double totalPoints = currentPoints;
                        double setNewPointsInDb = currentPoints;


                        while (totalPoints >= double.Parse(pointsRedeemLimit))
                        {
                            totalPoints -= double.Parse(pointsRedeemLimit);

                            if (totalPoints <= 0)
                            {
                                totalPoints = 0;
                            }

                            setNewPointsInDb = Math.Round(totalPoints, 2);
                        }

                        GetSetData.query = @"update pos_customers set points = '" + setNewPointsInDb.ToString() + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        GetSetData.query = @"update pos_customers set points = '" + txtCustomerPoints.Text + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }


                    string autoZeroCustomerDiscount = data.UserPermissions("autoZeroCustomerDiscount", "pos_general_settings");


                    if (autoZeroCustomerDiscount == "Yes")
                    {
                        GetSetData.query = @"update pos_customers set discount = '0' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }

                //*****************************************************************************************


                TextData.lastCredit = double.Parse(txt_lastCredits.Text);

                //  Customer Transactions *******************************************************************************
                GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Customer Sales' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                if (form_ticket_history.ticketNumber != "")
                {
                    GetSetData.query = "update pos_tickets set status = 'Completed' where (billNo = '" + form_ticket_history.ticketNumber + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                // *****************************************************************************************
                //GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                //if (GetSetData.Data == "Yes")
                //{
                //    TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                //    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                //    {
                //        TextData.totalAmount = double.Parse(TextData.totalCapital) + double.Parse(TextData.send_cash);
                //    }

                //    GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                //    data.insertUpdateCreateOrDelete(GetSetData.query);
                //    // *****************************************************************************************
                //}

                TextData.return_value = true;
                //}

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btnRewards_Click(object sender, EventArgs e)
        {
            try
            {
                string autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");

                if (autoRedeemPoints == "No")
                {
                    formRewards.rewardsPointsRedeem = "";
                    formRewards.rewardPointsDiscount = "";
                    rewards.Message_choose("Would you like to apply a discount.");
                    rewards.ShowDialog();


                    if (formRewards.sure == true)
                    {
                        TextData.net_total = 0;
                        TextData.discount = 0;
                        TextData.totalAmount = 0;
                        double taxation = 0;
                        double customer_discount = 0;
                        double redeem_discount = 0;
                        string totalDiscount = "0";


                        if (txtGrandTotal.Text != "")
                        {
                            TextData.net_total = double.Parse(txtGrandTotal.Text);
                        }

                        if (txtTaxation.Text != "")
                        {
                            GetSetData.query = "select sum(tax) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                            taxation = data.SearchNumericValuesDb(GetSetData.query);
                        }


                        if (lbl_customer.Text != "" && lbl_customer.Text != "nill")
                        {
                            if (TextData.promotionsDiscount || TextData.allDiscounts)
                            {
                                GetSetData.query = "select sum(discount) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                                totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

                                if (totalDiscount == "NULL" || totalDiscount == "")
                                {
                                    totalDiscount = "0";
                                }
                            }
                            else
                            {
                                totalDiscount = "0";
                            }

                            //*****************************************************************************************

                            GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            //*****************************************************************************************

                            if (GetSetData.Ids != 0)
                            {
                                string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                                if (autoSetPoints == "Yes")
                                {
                                    double currentPoints = 0;

                                    string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");

                                    double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", GetSetData.Ids.ToString());


                                    if (TextData.customerDiscount || TextData.allDiscounts)
                                    {
                                        customer_discount = data.NumericValues("discount", "pos_customers", "customer_id", GetSetData.Ids.ToString());
                                    }

                                    redeem_discount = customer_discount;

                                    customer_points_db += Math.Round(((TextData.net_total - taxation) / double.Parse(addPointPerAmount)), 2);
                                    currentPoints = customer_points_db;
                                   

                                    string pointsDiscountInPercentageDb = data.UserPermissions("pointsDiscountInPercentage", "pos_general_settings");
                                    string autoRedeemDiscount = formRewards.rewardPointsDiscount;
                                   

                                    if (bool.Parse(pointsDiscountInPercentageDb))
                                    {
                                        autoRedeemDiscount = (((TextData.net_total - taxation) * double.Parse(autoRedeemDiscount)) / 100).ToString();
                                    }
                                    

                                    customer_discount = ((TextData.net_total - taxation) * customer_discount) / 100;

                                    redeem_discount = Math.Round((double.Parse(autoRedeemDiscount) + customer_discount), 2);

                             
                                    if (redeem_discount > 0)
                                    {
                                        txt_total_discount.Text = (redeem_discount + double.Parse(totalDiscount)).ToString();
                                    }
                                    else
                                    {
                                        txt_total_discount.Text = (customer_discount + double.Parse(totalDiscount)).ToString();
                                    }


                                    double totalPoints = 0;
                                    totalPoints = currentPoints - double.Parse(formRewards.rewardsPointsRedeem);

                                    if (totalPoints < 0)
                                    {
                                        totalPoints = 0;
                                    }

                                    txtCustomerPoints.Text = totalPoints.ToString();
                                   
                                }
                            }
                        }
                    }
                }
                else
                {
                    error.errorMessage("Auto points redeem is on. Please off the auto redeem option first.");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        //private void btnRewards_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");

        //        if (autoRedeemPoints == "No")
        //        {
        //            rewards.Message_choose("Would you like to apply a discount.");
        //            rewards.ShowDialog();

        //            if (formRewards.sure == true)
        //            {
        //                TextData.net_total = 0;
        //                TextData.discount = 0;
        //                TextData.totalAmount = 0;
        //                double taxation = 0;
        //                double customer_discount = 0;
        //                double redeem_discount = 0;
        //                string totalDiscount = "0";


        //                if (txtGrandTotal.Text != "")
        //                {
        //                    TextData.net_total = double.Parse(txtGrandTotal.Text);
        //                }

        //                if (txtTaxation.Text != "")
        //                {
        //                    taxation = double.Parse(txtTaxation.Text);
        //                }


        //                if (lbl_customer.Text != "" && lbl_customer.Text != "nill")
        //                {
        //                    if (TextData.promotionsDiscount || TextData.allDiscounts)
        //                    {
        //                        GetSetData.query = "select sum(discount) from pos_cart_items where (mac_address = '" + macAddress + "');";
        //                        totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

        //                        if (totalDiscount == "NULL" || totalDiscount == "")
        //                        {
        //                            totalDiscount = "0";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        totalDiscount = "0";
        //                    }

        //                    //*****************************************************************************************

        //                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
        //                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //                    //*****************************************************************************************

        //                    if (GetSetData.Ids != 0)
        //                    {
        //                        string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

        //                        if (autoSetPoints == "Yes")
        //                        {
        //                            double currentPoints = 0;

        //                            string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");

        //                            double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", GetSetData.Ids.ToString());


        //                            if (TextData.customerDiscount || TextData.allDiscounts)
        //                            {
        //                                customer_discount = data.NumericValues("discount", "pos_customers", "customer_id", GetSetData.Ids.ToString());
        //                            }

        //                            redeem_discount = customer_discount;

        //                            customer_points_db += Math.Round(((TextData.net_total - taxation) / double.Parse(addPointPerAmount)), 2);
        //                            currentPoints = customer_points_db;


        //                            string pointsRedeemLimit = data.UserPermissions("pointsRedeemLimit", "pos_general_settings");
        //                            string pointsDiscountInPercentageDb = data.UserPermissions("pointsDiscountInPercentage", "pos_general_settings");
        //                            string autoRedeemDiscount = data.UserPermissions("pointsRedeemDiscount", "pos_general_settings");

        //                            if (bool.Parse(pointsDiscountInPercentageDb))
        //                            {
        //                                autoRedeemDiscount = (((TextData.net_total - taxation) * double.Parse(autoRedeemDiscount)) / 100).ToString();
        //                            }
                                    
        //                            int total_points = Convert.ToInt32(customer_points_db) / Convert.ToInt32(pointsRedeemLimit);

        //                            customer_discount = ((TextData.net_total - taxation) * customer_discount) / 100;

        //                            redeem_discount = (double.Parse(autoRedeemDiscount) * total_points) + customer_discount;


        //                            if (redeem_discount > 0)
        //                            {
        //                                txt_total_discount.Text = (redeem_discount + double.Parse(totalDiscount)).ToString();
        //                            }
        //                            else
        //                            {
        //                                txt_total_discount.Text = (customer_discount + double.Parse(totalDiscount)).ToString();
        //                            }


        //                            double totalPoints = currentPoints;
        //                            double setNewPointsInDb = currentPoints;


        //                            while (totalPoints >= double.Parse(pointsRedeemLimit))
        //                            {
        //                                totalPoints -= double.Parse(pointsRedeemLimit);

        //                                if (totalPoints <= 0)
        //                                {
        //                                    totalPoints = 0;
        //                                }

        //                                setNewPointsInDb = Math.Round(totalPoints, 2);
        //                            }

        //                            txtCustomerPoints.Text = setNewPointsInDb.ToString();
                                   
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage("Auto points redeem is on. Please off the auto redeem option first.");
        //            error.ShowDialog();
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private bool fun_insert_records_into_sales_as_cash()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                TextData.no_of_items = double.Parse(txt_total_items.Text);
                TextData.total_qty = double.Parse(txt_total_qty.Text);
                TextData.net_total = double.Parse(txtGrandTotal.Text);
                TextData.discount = double.Parse(txt_total_discount.Text);
                TextData.tax = txt_tax.Text;
                TextData.totalAmount = double.Parse(txt_amount_due.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);
                TextData.cash = 0;
                txt_credits.Text = TextData.credits.ToString();
                TextData.return_value = false;

                if (txt_status.Checked == false)
                {
                    TextData.status = "Sale";
                }
                else
                {
                    TextData.status = "Return";
                }

                if (TextData.send_cash != "")
                {
                    TextData.cash = double.Parse(TextData.send_cash);
                }

                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //*****************************************************************************************
                TextData.credits_limit = data.UserPermissionsIds("credit_limit", "pos_customers", "customer_id", customer_id_db.ToString());
                TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());
                TextData.lastCredit = TextData.LastCredits_db;


                if (TextData.send_cash == "")
                {
                    TextData.cash = TextData.totalAmount - double.Parse(form_ticket_history.advanceAmount);

                    fun_insert_regular_sale_records_into_db();
                }
                else if (TextData.checkSaleStatus == "Cash, Credit")
                {
                    TextData.lastCredit += TextData.credits;

                    if (TextData.customer_name != "")
                    {
                        if (TextData.employee != "")
                        {
                            if (TextData.lastCredit <= TextData.credits_limit)
                            {
                                if (TextData.cash <= TextData.totalAmount)
                                {
                                    fun_insert_Credited_customers_records_into_db();
                                }
                                else
                                {
                                    error.errorMessage("Cash must be less than or equal to Due Amount!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage(TextData.customer_name + " credit is exceeding from its Limit!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please select the Employee first!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the Customer first!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    TextData.cash = double.Parse(TextData.send_cash);

                    fun_insert_regular_sale_records_into_db();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
                //TextData.return_value = false;
            }
        }

        private bool PaymentByInstallments_db()
        {
            return false;
            //try
            //{
            //    txt_date.Text = DateTime.Now.ToShortDateString();
            //    TextData.dates = txt_date.Text;
            //    TextData.employee = lbl_employee.Text;
            //    TextData.customer_name = lbl_customer.Text;
            //    TextData.customerCode = lblCustomerCode.Text;
            //    TextData.no_of_items = double.Parse(txt_total_items.Text);
            //    TextData.total_qty = double.Parse(txt_total_qty.Text);
            //    TextData.net_total = double.Parse(txtGrandTotal.Text);
            //    TextData.total_taxation = double.Parse(txtTaxation.Text);
            //    TextData.discount = double.Parse(txt_total_discount.Text);
            //    TextData.tax = txt_tax.Text;
            //    TextData.totalAmount = double.Parse(txt_amount_due.Text);
            //    txt_credits.Text = TextData.credits.ToString();
            //    TextData.cash = 0;
            //    TextData.return_value = false;

            //    if (TextData.comments == "")
            //    {
            //        TextData.comments = "nill";
            //    }

            //    if (txt_status.Checked == false)
            //    {
            //        TextData.status = "Sale";
            //    }
            //    else
            //    {
            //        TextData.status = "Return";
            //    }

            //    if (TextData.cash_on_hand == "")
            //    {
            //        TextData.cash_on_hand = "0";
            //    }

            //    if (TextData.send_cash != "")
            //    {
            //        TextData.cash = double.Parse(TextData.send_cash);

            //        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
            //        int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
            //        //*****************************************************************************************

            //        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
            //        string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

            //        //*****************************************************************************************

            //        int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
            //        TextData.credits_limit = data.UserPermissionsIds("credit_limit", "pos_customers", "customer_id", customer_id_db.ToString());
            //        TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());
            //        TextData.lastCredit = TextData.LastCredits_db;

            //        TextData.lastCredit += TextData.credits;

            //        if (TextData.customer_name != "")
            //        {
            //            if (TextData.employee != "")
            //            {
            //                if (TextData.lastCredit <= TextData.credits_limit)
            //                {
            //                    if (TextData.cash <= TextData.totalAmount)
            //                    {
            //                        if (TextData.totalAmount != 0)
            //                        {
            //                            //TextData.billNo = auto_generate_sales_code();
            //                            //agreementForm.billNo = TextData.billNo;

            //                            GetSetData.query = "insert into pos_sales_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "' , '" + TextData.total_taxation.ToString() + "' , '0' , '0' , '0', '" + clock_in_id + "' , 'false', '" + txtCustomerPoints.Text + "', '0', '0');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);

            //                            int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_sales_accounts", "billNo", TextData.billNo);
            //                            // *****************************************************************************************

            //                            GetSetData.query = "insert into pos_installment_accounts values ('0' , '0' , '0' , '0' , '0', '0', '" + sales_acc_id_db.ToString() + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);

            //                            int installment_acc_id_db = data.UserPermissionsIds("installment_acc_id", "pos_installment_accounts", "sales_acc_id", sales_acc_id_db.ToString());
            //                            // *****************************************************************************************
            //                            GetSetData.query = "insert into pos_installment_plan values ('0' , '" + TextData.dates + "' , '" + TextData.dates + "' , '" + TextData.cash.ToString() + "' , '0', '0', '0', '" + TextData.cash.ToString() + "', 'Advance', '" + installment_acc_id_db.ToString() + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);

            //                            // *****************************************************************************************

            //                            double totalCommission = 0;

            //                            GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

            //                            SqlConnection conn = new SqlConnection(webConfig.con_string);
            //                            SqlCommand cmd;
            //                            SqlDataReader reader;

            //                            cmd = new SqlCommand(GetSetData.query, conn);

            //                            conn.Open();
            //                            reader = cmd.ExecuteReader();

            //                            while (reader.Read())
            //                            {
            //                                string product_id_db = reader["product_id"].ToString();
            //                                string stock_id_db = reader["stock_id"].ToString();

            //                                TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
            //                                double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db);
            //                                TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
            //                                double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
            //                                double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db);
            //                                double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


            //                                GetSetData.query = "select title from pos_category where (category_id = '" + reader["category_id"].ToString() + "');";
            //                                TextData.category = data.SearchStringValuesFromDb(GetSetData.query);
            //                                // **************************************************************************************

            //                                string isStockLimitSetToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");

            //                                if (double.Parse(reader["quantity"].ToString()) <= quantity_db || TextData.category == "Services" || TextData.category == "MISCELLANEOUS" || isStockLimitSetToZero == "No")
            //                                {
            //                                    quantity_db = quantity_db - double.Parse(reader["quantity"].ToString());
            //                                    TextData.total_pur_price = pur_price_db * quantity_db;
            //                                    TextData.total_sales_price = sale_price_db * quantity_db;

            //                                    //*********************************************************

            //                                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


            //                                    GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Sale Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
            //                                    data.insertUpdateCreateOrDelete(GetSetData.query);

            //                                    //*********************************************************

            //                                    // update Products quantity from datagridview:
            //                                    GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
            //                                    data.insertUpdateCreateOrDelete(GetSetData.query);
            //                                }
            //                                else
            //                                {
            //                                    error.errorMessage("Available stoock is " + quantity_db.ToString());
            //                                    error.ShowDialog();
            //                                }
            //                                // **************************************************************************************

            //                                TextData.full_pkg = 0;

            //                                if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
            //                                {
            //                                    TextData.quantity = double.Parse(reader["quantity"].ToString());
            //                                    TextData.full_pkg = TextData.quantity / TextData.pkg;
            //                                }

            //                                TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
            //                                TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());


            //                                //GetSetData.query = @"update pos_purchase_imei set isSold = 'True'  where (imei_no = '" + CartDataGridView.Rows[i].Cells[8].Value.ToString() + "');";
            //                                //data.insertUpdateCreateOrDelete(GetSetData.query);
            //                                // **************************************************************************************

            //                                // **************************************************************************************

            //                                #region

            //                                GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() + "');";
            //                                int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //                                double commissionAmount = 0;

            //                                if (commission_id != 0)
            //                                {
            //                                    GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
            //                                    int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //                                    if (commission_id_db != 0)
            //                                    {
            //                                        bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
            //                                        double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
            //                                        double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));

            //                                        if (is_commission_in_percentage)
            //                                        {
            //                                            commissionAmount = ((double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString())) * commissionInPercentageDb) / 100;
            //                                        }
            //                                        else
            //                                        {
            //                                            commissionAmount = commissionAmountDb;

            //                                            commissionAmount *= double.Parse(reader["quantity"].ToString());
            //                                        }

            //                                        totalCommission += commissionAmount;
            //                                    }
            //                                }

            //                                GetSetData.query = @"insert into pos_returns_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "', '" + TextData.wholeSale.ToString() + "' , '" + reader["note"].ToString() + "', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
            //                                data.insertUpdateCreateOrDelete(GetSetData.query);

            //                                #endregion


            //                                // **************************************************************************************

            //                                if (TextData.LastCredits_db == -1 && TextData.customer_name != "nill")
            //                                {
            //                                    GetSetData.query = @"insert into pos_customer_lastCredits values ('" + TextData.lastCredit.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + customer_id_db.ToString() + "');";
            //                                    data.insertUpdateCreateOrDelete(GetSetData.query);
            //                                }
            //                                else if (TextData.LastCredits_db != -1 && TextData.LastCredits_db >= 0 && TextData.customer_name != "nill")
            //                                {
            //                                    GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredit.ToString() + "' where customer_id = '" + customer_id_db.ToString() + "';";
            //                                    data.insertUpdateCreateOrDelete(GetSetData.query);
            //                                }

            //                                formInstallmentPlan.customerId_db = customer_id_db;
            //                                formInstallmentPlan.customerName = TextData.customer_name;
            //                                // **************************************************************************************  
            //                            }

            //                            // **************************************************************************************  

            //                            double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

            //                            double saleCommission = totalCommission;

            //                            totalCommission += employeeCommissionDb;

            //                            GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);


            //                            GetSetData.query = @"update pos_sales_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (sales_acc_id = '" + sales_acc_id_db.ToString() + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);
            //                            // **************************************************************************************  

            //                            GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);

            //                            reader.Close();
            //                            conn.Close();
            //                            // *****************************************************************************************


            //                            TextData.lastCredit = double.Parse(txt_lastCredits.Text);

            //                            //  Customer Transactions *******************************************************************************
            //                            GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Installments' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
            //                            data.insertUpdateCreateOrDelete(GetSetData.query);

            //                            //*****************************************************************************************
            //                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

            //                            if (GetSetData.Data == "Yes")
            //                            {
            //                                TextData.totalAmount = 0;
            //                                TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

            //                                if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
            //                                {
            //                                    TextData.totalAmount = double.Parse(TextData.totalCapital) + TextData.advanceAmount;
            //                                }

            //                                GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
            //                                data.insertUpdateCreateOrDelete(GetSetData.query);
            //                                // *****************************************************************************************
            //                            }
            //                            TextData.return_value = true;
            //                            return true;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        error.errorMessage("Cash must be less than or equal to Due Amount!");
            //                        error.ShowDialog();
            //                    }
            //                }
            //                else
            //                {
            //                    error.errorMessage("Credits is exceeding from its Limit!");
            //                    error.ShowDialog();
            //                }
            //            }
            //            else
            //            {
            //                error.errorMessage("Please select the Employee first!");
            //                error.ShowDialog();
            //            }
            //        }
            //        else
            //        {
            //            error.errorMessage("Please select the Customer first!");
            //            error.ShowDialog();
            //        }
            //    }
            //    else
            //    {
            //        error.errorMessage("Please enter advance amount first!");
            //        error.ShowDialog();
            //    }
            //    return false;
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //    return false;
            //    //TextData.return_value = false;
            //}
        }

        private void paymentDetails()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.customer_name = "";
                TextData.customerCode = "";
                TextData.cash_on_hand = "";
                TextData.remaining_amount = "";
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                TextData.cash = 0;
                TextData.net_total = double.Parse(txt_amount_due.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);
                TextData.billNo = "";
                TextData.macAddress = macAddress;
                TextData.feedbackAmountDue = "0";
                TextData.feedbackCashAmount = "0";
                TextData.feedbackChangeAmount = "0";
                TextData.isCashPayment = false;
                TextData.paymentType = "";

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                TextData.customerId = data.SearchStringValuesFromDb(GetSetData.query);
                //*****************************************************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                TextData.clockInId = data.SearchStringValuesFromDb(GetSetData.query);
               
                //*****************************************************************************************

                TextData.employeeId = data.UserPermissions("employee_id", "pos_employees", "full_name", TextData.employee);
                // ****************************************************

                if (txt_status.Checked == false)
                {
                    form_payment.isReturned = false;
                }
                else
                {
                    form_payment.isReturned = true;
                }

                form_payment.tipAmount = txtTotalTipAmount.Text;
                form_payment.advancePaidAmount = form_ticket_history.advanceAmount;
                form_payment.role_id = role_id;
                form_payment add_customer = new form_payment();
                add_customer.ShowDialog();
                
                lbl_customer.Text = TextData.customer_name;
                lblCustomerCode.Text = TextData.customerCode;
                txt_credits.Text = TextData.credits.ToString();
                txtChangeAmount.Text = TextData.remaining_amount;

                TextData.general_options = generalSettings.ReadField("directly_print_receipt");
                string auto_clear_cart = generalSettings.ReadField("auto_clear_cart");
                // ************************

                if (txt_status.Checked == false)
                {
                    TextData.status = "Sale";
                    TextData.returnStatus = "Sale";
                }
                else
                {
                    TextData.status = "Return";
                    TextData.returnStatus = "Return";
                }

       
                if (TextData.status == "Sale")
                {
                    if (TextData.aknowledged != "")
                    {
                        if (TextData.aknowledged == "Cash")
                        {
                            fun_insert_records_into_sales_as_cash();
                        }
                        else if (TextData.aknowledged == "Credits")
                        {
                            fun_insert_records_into_sales_as_Credits();
                        }

                        //if (generalSettings.ReadField("salesmanTips") == "Yes")
                        //{
                        //    if (TextData.billNo != "" && TextData.credit_card_amount != 0)
                        //    {
                        //        Thread thread = new Thread(() =>
                        //        {
                        //            form_add_tips secondaryForm = new form_add_tips();
                        //            secondaryForm.invoiceNumber = TextData.billNo;
                        //            Screen secondaryScreen = Screen.PrimaryScreen;
                        //            secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                        //            secondaryForm.TopMost = true;
                        //            secondaryForm.ShowDialog();
                        //        });

                        //        thread.SetApartmentState(ApartmentState.STA);
                        //        thread.Start();
                        //    }
                        //}

                        if (TextData.return_value == true)
                        {
                            //GetSetData.query = @"select default_printer from pos_general_settings;";
                            //string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


                            if (TextData.checkAutoOpenCashDrawer == true)
                            {
                                //GetSetData.query = @"select printer_model from pos_general_settings;";
                                //string printer_model = data.SearchStringValuesFromDb(GetSetData.query);

                                CashDrawerData.OpenDrawer(generalSettings.ReadField("default_printer"), generalSettings.ReadField("printer_model"));
                            }

                            if (TextData.checkPrintReport == true)
                            {
                                GetSetData.query = @"select preview_receipt from pos_general_settings;";
                                string preview_receipt = data.SearchStringValuesFromDb(GetSetData.query);

                                if (preview_receipt == "Yes")
                                {
                                    form_cus_sales_report report = new form_cus_sales_report();
                                    report.ShowDialog();
                                }
                                else
                                {
                                    if (generalSettings.ReadField("default_printer") != "")
                                    {
                                        PrintDirectReceipt(generalSettings.ReadField("default_printer"), TextData.billNo);
                                    }
                                    else
                                    {
                                        error.errorMessage("Printer not found!");
                                        error.ShowDialog();
                                    }
                                }
                            }

                            if (auto_clear_cart == "Yes")
                            {
                                fun_clear_cart_records();
                            }

                            lbl_customer.Text = "";
                            lblCustomerCode.Text = "";
                            txt_status.Checked = false;
                            btnReturn.FillColor = Color.DodgerBlue;


                            clearDataGridView();
                        }
                    }
                }
                else if (TextData.status == "Return" && TextData.aknowledged != "")
                {
                    fun_insert_records_into_returns_db();

                    if (TextData.return_value == true)
                    {
                        //GetSetData.query = @"select default_printer from pos_general_settings;";
                        //string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                        if (TextData.checkPrintReport == true)
                        {
                            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Sale Return Invoice [" + TextData.billNo + "] payment details", role_id);
                            //form_cus_returns_report report = new form_cus_returns_report();
                            //report.ShowDialog();

                            PrintReturnSaleDirectReceipt(generalSettings.ReadField("default_printer"), TextData.billNo);
                        }

                        if (TextData.checkAutoOpenCashDrawer)
                        {
                            //GetSetData.query = @"select printer_model from pos_general_settings;";
                            //string printer_model = data.SearchStringValuesFromDb(GetSetData.query);

                            CashDrawerData.OpenDrawer(generalSettings.ReadField("default_printer"), generalSettings.ReadField("printer_model"));
                        }

                        if (auto_clear_cart == "Yes")
                        {
                            fun_clear_cart_records();
                        }

                        lbl_customer.Text = "";
                        lblCustomerCode.Text = "";
                        txt_status.Checked = false;
                        btnReturn.FillColor = Color.DodgerBlue;
                        clearDataGridView();
                    }
                }
                //else if (TextData.status == "Installment")
                //{
                //    if (TextData.aknowledged != "" && TextData.aknowledged == "Cash")
                //    {
                //        formInstallmentPlan.previewPlan = false;

                //        if (PaymentByInstallments_db())
                //        {
                //            TextData.net_total = double.Parse(txt_amount_due.Text);
                //            Login_info.controllers.Button_controls.Installment_plan_buttons();
                //        }

                //        if (TextData.return_value == true)
                //        {
                //            if (TextData.general_options == "Yes")
                //            {
                //                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Installment Invoice [" + TextData.billNo + "] payment details", role_id);
                //                //agreementForm report = new agreementForm();
                //                //report.ShowDialog();
                //            }

                //            if (auto_clear_cart == "Yes")
                //            {
                //                fun_clear_cart_records();
                //            }

                //            lbl_customer.Text = "";
                //            txt_status.Checked = false;
                //            btnReturn.FillColor = Color.DodgerBlue;
                //            lblCustomerCode.Text = "";
                //            clearDataGridView();
                //        }
                //    }
                //}


                //string changeAmountPopup = data.UserPermissions("changeAmountPopUp", "pos_general_settings");
           
                //if ((generalSettings.ReadField("changeAmountPopUp") == "Yes") && (TextData.showPopUpForm))
                if (generalSettings.ReadField("changeAmountPopUp") == "Yes")
                {
                    if (TextData.feedbackAmountDue != "")
                    {
                        formChangeAmountPopUp.amountDue = TextData.feedbackAmountDue;
                        formChangeAmountPopUp.cashAmount = TextData.feedbackCashAmount;
                        formChangeAmountPopUp.changeAmount = TextData.feedbackChangeAmount;
                        formChangeAmountPopUp.isCashPayment = TextData.isCashPayment;
                        formChangeAmountPopUp.paymentType = TextData.paymentType;

                        using (formChangeAmountPopUp _obj = new formChangeAmountPopUp())
                        {
                            _obj.ShowDialog();
                        }
                    }
                }

                fun_get_employee_balance();
                getCreditCardBalance();
                getCreditedBalance();
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_counter_sales_KeyDown(object sender, KeyEventArgs e)
        {
            //else if (e.Control && e.KeyCode == Keys.V)
            //{
            //    cancelCurrentBill();
            //}


            if (e.Control && e.KeyCode == Keys.S)
            {
                paymentDetails();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                txtProductName.Select();
            }
            else if (e.Control && e.KeyCode == Keys.H)
            {
                TextData.net_total = double.Parse(txtGrandTotal.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);

                if (TextData.net_total != 0)
                {
                    fun_Hold_items_details_db();
                }

                txt_barcode.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                ordersButtonDetails();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                customerList();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                printRecentBill();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                commentsOnCurrentBill();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                discountOnCurrentBill();
            }
            else if (e.Control && e.KeyCode == Keys.B)
            {
                txt_barcode.Select();
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                try
                {
                    form_unhold.role_id = role_id;
                    Login_info.controllers.Button_controls.un_hold_orders_buttons();

                    if (TextData.billNo != "")
                    {
                        funFillCurtWithUnholdItems();
                        fun_delete_hold_items_db();
                        FillLastCreditsTextBox();
                    }

                    holdItemsBackgroundWorker.RunWorkerAsync();
                }
                catch (Exception es)
                {
                    error.errorMessage(es.Message);
                    error.ShowDialog();
                }
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                refresh_menu();
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                customerLastRecipt();
            }
            else if (e.Control && e.KeyCode == Keys.W)
            {
                chkWholeSale.Checked = true;
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                chkWholeSale.Checked = false;
            }
            else if (e.KeyCode == Keys.F1)
            {
                txt_sale_option.SelectedIndex = 0;
            }
            else if (e.KeyCode == Keys.F2)
            {
                txt_sale_option.SelectedIndex = 1;
            }
            else if (e.KeyCode == Keys.F3)
            {
                txt_sale_option.SelectedIndex = 2;
            }
            else if (e.KeyCode == Keys.F4)
            {
                txt_sale_option.SelectedIndex = 3;
            }
            else if (e.KeyCode == Keys.F5)
            {
                txt_sale_option.SelectedIndex = 4;
            }
            else if (e.Control && e.KeyCode == Keys.D1)
            {
                txt_price.Select();
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                txt_qty.Select();
            }
            else if (e.Control && e.KeyCode == Keys.D3)
            {
                txtDiscount.Select();
            }
            //else if (e.Alt && e.KeyCode == Keys.D1)
            //{
            //    txt_status.SelectedIndex = 0;
            //}
            //else if (e.Alt && e.KeyCode == Keys.D2)
            //{
            //    txt_status.SelectedIndex = 1;
            //}
            //else if (e.Alt && e.KeyCode == Keys.D3)
            //{
            //    txt_status.SelectedIndex = 2;
            //}
        }

        private void btn_payment_Click(object sender, EventArgs e)
        {
            if (txt_total_items.Text != "" && double.Parse(txt_total_items.Text) > 0)
            {
                txtChangeAmount.Text = "0.00";

                paymentDetails();

                //CloseAllCustomerAgeForms();
            }
            //{
            //    error.errorMessage("No items found in cart!");
            //    error.ShowDialog();
            //}
        }

        //private void CloseAllCustomerAgeForms()
        //{
        //    // Iterate through all open forms in a thread-safe manner
        //    foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
        //    {
        //        if (form.GetType() == typeof(formCustomerAge))
        //        {
        //            form.Close();
        //        }
        //    }
        //}

        private bool fun_insert_records_into_returns_db()
        {
            try
            {
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;
                TextData.dates = txt_date.Text;
                TextData.no_of_items = double.Parse(txt_total_items.Text);
                TextData.total_qty = double.Parse(txt_total_qty.Text);
                TextData.net_total = double.Parse(txtGrandTotal.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);
                TextData.discount = double.Parse(txt_total_discount.Text);
                TextData.tax = txt_tax.Text;
                TextData.totalAmount = double.Parse(txt_amount_due.Text);
                TextData.cash = 0;
                TextData.credits = TextData.totalAmount;
                txt_credits.Text = TextData.credits.ToString();
                TextData.return_value = false;

                if (TextData.send_cash != "")
                {
                    TextData.cash = double.Parse(TextData.send_cash);
                    TextData.credits = TextData.totalAmount - TextData.cash;
                }
                else
                {
                    TextData.cash = TextData.totalAmount - double.Parse(ticketAdvanceAmount.advanceAmount);
                }

                if (txt_status.Checked == false)
                {
                    TextData.status = "Sale";
                    TextData.returnStatus = "Sale";
                }
                else
                {
                    TextData.status = "Return";
                    TextData.returnStatus = "Return";
                }

                //if (lbl_customer.Text == "")
                //{
                //    TextData.customer_name = "nill";
                //GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                //TextData.customerCode = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //lblCustomerCode.Text = TextData.customerCode;
                //}

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }

                if (TextData.comments == "")
                {
                    TextData.comments = "nill";
                }

                // ************************************************************************************************
                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                //*****************************************************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);
                TextData.LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());
                TextData.lastCredit = TextData.LastCredits_db;


                TextData.lastCredit -= TextData.credits;

                if (TextData.lastCredit < 0)
                {
                    TextData.lastCredit = 0;
                }

                if (TextData.cash <= TextData.totalAmount)
                {
                    //if (TextData.totalAmount != 0)
                    //{
                    TextData.billNo = auto_generate_return_code();

                    //************************************************************************************** 
                    double newPoints = 0;

                    string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                    if (autoSetPoints == "Yes")
                    {
                        string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");

                        double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", customer_id_db.ToString());

                        newPoints = customer_points_db - ((double.Parse(txtGrandTotal.Text) - double.Parse(txtTaxation.Text)) / double.Parse(addPointPerAmount));

                        if (newPoints < 0)
                        {
                            newPoints = 0;
                        }

                        GetSetData.query = @"update pos_customers set points = '" + newPoints.ToString() + "' where (customer_id = '" + customer_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    //*****************************************************************************************


                    if (TextData.credit_card_amount != 0)
                    {
                        GetSetData.query = @"insert into pos_return_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "', '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation + "' , '" + TextData.credit_card_amount + "', '0' , '0' , '" + clock_in_id + "', '" + newPoints.ToString() + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else if (TextData.paypal_amount != 0)
                    {
                        GetSetData.query = @"insert into pos_return_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "', '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation + "' , '0' , '" + TextData.paypal_amount + "', '0' , '" + clock_in_id + "', '" + newPoints.ToString() + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else if (TextData.google_pay_amount != 0)
                    {
                        GetSetData.query = @"insert into pos_return_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "', '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation + "' , '0' , '0' , '" + TextData.google_pay_amount + "' , '" + clock_in_id + "', '" + newPoints.ToString() + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        GetSetData.query = @"insert into pos_return_accounts values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '0' , '" + TextData.LastCredits_db.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.checkSaleStatus + "' , '" + TextData.comments + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "', '" + TextData.cash_on_hand + "' , '" + TextData.remaining_amount + "', '" + TextData.total_taxation + "' , '0' , '0' , '0' , '" + clock_in_id + "', '" + newPoints.ToString() + "', '0', '" + TextData.tipAmount +"', '0', '" + form_ticket_history.advanceAmount +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    //*****************************************************************************************
                   
                    GetSetData.query = "update pos_sales_accounts set is_returned = 'true' where (billNo = '" + TextData.returnBillNo + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    
                    //*****************************************************************************************

                    int sales_acc_id_db = data.UserPermissionsIds("return_acc_id", "pos_return_accounts", "billNo", TextData.billNo);

                    //*****************************************************************************************

                    double totalCommission = 0;

                    GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

                    SqlConnection conn = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd;
                    SqlDataReader reader;

                    cmd = new SqlCommand(GetSetData.query, conn);

                    conn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string product_id_db = reader["product_id"].ToString();
                        string stock_id_db = reader["stock_id"].ToString();

                        TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db.ToString());
                        double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());
                        TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db.ToString());
                        double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                        double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                        double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                        // **************************************************************************************


                        quantity_db = quantity_db + double.Parse(reader["quantity"].ToString());
                        TextData.total_pur_price = pur_price_db * quantity_db;
                        TextData.total_sales_price = sale_price_db * quantity_db;

                        //*********************************************************

                        string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());


                        GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "' , '" + quantity_db.ToString() + "' , '" + oldQuantityDB + "' , '" + pur_price_db.ToString() + "' , '" + pur_price_db + "' , '" + sale_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , 'Return Inventory' , '" + user_id.ToString() + "' , '" + product_id_db.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //*********************************************************

                        // update Products quantity from datagridview:
                        GetSetData.query = @"update pos_stock_details set quantity = '" + quantity_db.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sales_price.ToString() + "' where (stock_id = '" + stock_id_db + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        // **************************************************************************************

                        TextData.full_pkg = 0;

                        if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                        {
                            TextData.quantity = double.Parse(reader["quantity"].ToString());
                            TextData.full_pkg = TextData.quantity / TextData.pkg;
                        }

                        TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                        TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());


                        //GetSetData.query = @"update pos_purchase_imei set isSold = 'False'  where (imei_no = '" + CartDataGridView.Rows[i].Cells[8].Value.ToString() + "');";
                        //data.insertUpdateCreateOrDelete(GetSetData.query);
                        // **************************************************************************************

                        // **************************************************************************************

                        #region

                        GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "') and (pos_employee_commission.employee_id = '" + employee_id_db.ToString() + "');";
                        int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        double commissionAmount = 0;

                        if (commission_id != 0)
                        {
                            GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employee_id_db.ToString() + "') and (status = 'Active') and (start_date <= '" + txt_date.Text + "') and (end_date >= '" + txt_date.Text + "');";
                            int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (commission_id_db != 0)
                            {
                                bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                                if (is_commission_in_percentage)
                                {
                                    commissionAmount = ((double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString())) * commissionInPercentageDb) / 100;
                                }
                                else
                                {
                                    commissionAmount = commissionAmountDb;

                                    commissionAmount *= double.Parse(reader["quantity"].ToString());
                                }


                                totalCommission += commissionAmount;
                            }
                        }

                        GetSetData.query = @"insert into pos_returns_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "', '" + TextData.wholeSale.ToString() + "' , '" + reader["note"].ToString() + "', '" + sales_acc_id_db.ToString() + "' , '" + product_id_db.ToString() + "' , '" + commissionAmount.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        #endregion

                        // **************************************************************************************

                        if (TextData.LastCredits_db != -1 && TextData.LastCredits_db >= 0 && TextData.customer_name != "nill")
                        {
                            GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredit.ToString() + "' where customer_id = '" + customer_id_db.ToString() + "';";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }

                    reader.Close();
                    conn.Close();
                    // **************************************************************************************

                    double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                    double returnCommission = totalCommission;

                    totalCommission = employeeCommissionDb - totalCommission;

                    if (totalCommission < 0)
                    {
                        totalCommission = 0;
                    }

                    GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employee_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"update pos_return_accounts set employeeCommission = '" + Math.Round(returnCommission, 2).ToString() + "' where (return_acc_id = '" + sales_acc_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    // **************************************************************************************

                    GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);



                    TextData.lastCredit = double.Parse(txt_lastCredits.Text);

                    //  Customer Transactions *******************************************************************************
                    GetSetData.query = @"insert into pos_customer_transactions values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + DateTime.Now.ToLongTimeString() + "' , '0' , '" + TextData.credits.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , 'Customer Returned' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    if (form_ticket_history.ticketNumber != "")
                    {
                        GetSetData.query = "update pos_tickets set status = 'Completed' where (billNo = '" + form_ticket_history.ticketNumber + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }


                    //GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                    //if (GetSetData.Data == "Yes")
                    //{
                    //    // *****************************************************************************************
                    //    TextData.totalCapital = data.UserPermissions("total_capital", "pos_capital");

                    //    if (TextData.totalCapital != "NULL" && TextData.totalCapital != "")
                    //    {
                    //        TextData.totalAmount = double.Parse(TextData.totalCapital) - TextData.totalAmount;
                    //    }

                    //    if (TextData.totalAmount <= 0)
                    //    {
                    //        TextData.totalAmount = 0;
                    //    }
                    //    GetSetData.query = "update pos_capital set total_capital = '" + TextData.totalAmount.ToString() + "';";
                    //    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //    // *****************************************************************************************
                    //}
                    TextData.return_value = true;
                    TextData.returnBillNo = "";
                    //}
                }
                else
                {
                    error.errorMessage("Cash must be less than or equal to Due Amount!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
                TextData.return_value = false;
            }
        }

        private void commentsOnCurrentBill()
        {
            using (form_remarks add_customer = new form_remarks())
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Invoice [" + TextData.billNo + "] remarks details", role_id);
                add_customer.ShowDialog();
                txt_barcode.Focus();
            }
        }

        private void btn_remarks_Click(object sender, EventArgs e)
        {
            commentsOnCurrentBill();
        }

        private void clearDataGridView()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();

                // Clear the original controls collection
                //CartFlowLayout.Controls.Clear();

                // Dispose the controls in the copied collection
                for (int i = CartFlowLayout.Controls.Count - 1; i >= 0; i--)
                {
                    Control control = CartFlowLayout.Controls[i];

                    if (control is btnCart cartItem)
                    {
                        cartItem.Dispose();
                    }
                }

                TextData.allDiscounts = true;

                holdItemsBackgroundWorker.RunWorkerAsync();
            }
            catch (Exception es)
            {
                error.errorMessage("Cart is already empty!");
                error.ShowDialog();
            }
        }

        private void settingDefaultSaleOption()
        {
            //try
            //{
            //    GetSetData.Data = data.UserPermissions("sale_default_option", "pos_general_settings");

            //    if (GetSetData.Data == "Pack")
            //    {
            //        txt_sale_option.StartIndex = 0;
            //    }
            //    else if (GetSetData.Data == "Pellet")
            //    {
            //        txt_sale_option.StartIndex = 1;
            //    }
            //    else if (GetSetData.Data == "Piece")
            //    {
            //        txt_sale_option.StartIndex = 2;
            //    }
            //    else if (GetSetData.Data == "Carton")
            //    {
            //        txt_sale_option.StartIndex = 3;
            //    }
            //    else if (GetSetData.Data == "Bag")
            //    {
            //        txt_sale_option.StartIndex = 4;
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void fun_clear_cart_records()
        {
            try
            {
                txt_status.Checked = false;
                btnReturn.FillColor = Color.DodgerBlue;
                txt_qty.Text = "";
                txtDiscount.Text = "";
                txt_barcode.Text = "";
                txt_price.Text = "";
                lbl_customer.Text = "";
                lblCustomerCode.Text = "";
                txt_total_items.Text = "0.00";
                txt_total_qty.Text = "0.00";
                txt_total_discount.Text = "0";
                txt_discount.Text = "0";
                txt_sub_total.Text = "0.00";
                txtGrandTotal.Text = "0.00";
                txt_tax.Text = "0.00";
                txt_credits.Text = "0.00";
                txt_lastCredits.Text = "0.00";
                txt_amount_due.Text = "0.00";
                txtTaxation.Text = "0.00";
                txtCustomerPoints.Text = "0.00";
                txtTipAmount.Text = "0";
                txtIsTipInPercentage.Text = "False";
                txt_barcode.TabIndex = 0;
                chkChooseType.SelectedIndex = 0;

                //****************************

                TextData.quantity = 0;
                TextData.rate = 0;
                TextData.discount = 0;
                TextData.no_of_items = 0;
                TextData.total_qty = 0;
                TextData.net_total = 0;
                TextData.totalAmount = 0;
                TextData.total_pur_price = 0;
                TextData.total_sales_price = 0;
                TextData.net_taxation = 0;
                TextData.total_taxation = 0;
                TextData.promotionDiscount = 0;
                settingDefaultSaleOption();

                //****************************

                txtReturnSubTotal.Visible = false;
                txtReturnGrandTotal.Visible = false;
                txtReturnAmountDue.Visible = false;
                txtReturnPoints.Visible = false;
                txtReturnTax.Visible = false;
                txtReturnDiscount.Visible = false;
                txtReturnItems.Visible = false;
                txtReturnQuantity.Visible = false;


                //****************************

                txt_sub_total.Visible = true;
                txtGrandTotal.Visible = true;
                txt_amount_due.Visible = true;
                txtCustomerPoints.Visible = true;
                txtTaxation.Visible = true;
                txt_total_discount.Visible = true;
                txt_total_items.Visible = true;
                txt_total_qty.Visible = true;

                //****************************

                txtReturnSubTotal.Text = "0.00-";
                txtReturnGrandTotal.Text = "0.00-";
                txtReturnAmountDue.Text = "0.00-";
                txtReturnPoints.Text = "-0.00";
                txtReturnTax.Text = "-0.00";
                txtReturnDiscount.Text = "-0.00";
                txtReturnItems.Text = "-0.00";
                txtReturnQuantity.Text = "-0.00";

                customerLoyalityPointsAlert();

                if (IsFormOpen(typeof(fromSecondScreen)))
                {
                    fromSecondScreen.instance.Invoke((MethodInvoker)delegate
                    {
                        fromSecondScreen.instance.getCartItems();
                        fromSecondScreen.instance.isReturn();
                    });
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Cart is already empty!");
                error.ShowDialog();
            }
        }

        //private void DeletePaymentGrantors_db()
        //{
        //    try
        //    {
        //        GetSetData.query = @"SELECT salesCodes FROM pos_AllCodes order by id desc;";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        GetSetData.Ids++;
        //        TextData.billNo = "SALE_" + GetSetData.Ids.ToString();

        //        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
        //        //*****************************************************************************************

        //        GetSetData.query = "select employee_id from pos_employees where full_name = '" + lbl_employee.Text + "';";
        //        GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        GetSetData.query = "delete from pos_payment_grantors where billNo = '" + TextData.billNo + "' and customer_id = '" + GetSetData.Ids.ToString() + "' and employee_id = '" + GetSetData.fks.ToString() + "';";
        //        data.insertUpdateCreateOrDelete(GetSetData.query);
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private void cancelCurrentBill()
        {
            try
            {
                sure.Message_choose("Are you sure you want to cancel order!");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    insertIntoVoidTable();
                    fun_clear_cart_records();
                    clearDataGridView();

                    txt_barcode.Select();
                }
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insertIntoVoidTable()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }


                int customer_id_db = 0;
                //*****************************************************************************************
                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);


                if (TextData.customer_name == "" && TextData.customerCode == "")
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = 'nill');";
                    customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                }
                else
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                    customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                }

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "');";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string product_id_db = reader["product_id"].ToString();
                    string stock_id_db = reader["stock_id"].ToString();

                    TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
                    TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
                    double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                    double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


                    TextData.full_pkg = 0;

                    if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                    {
                        TextData.quantity = double.Parse(reader["quantity"].ToString());
                        TextData.full_pkg = TextData.quantity / TextData.pkg;
                    }

                    TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                    TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());


                    GetSetData.query = @"insert into pos_void_orders values ('" + TextData.dates + "' , '" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "','" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "' , '" + TextData.wholeSale.ToString() + "', '" + reader["note"].ToString() + "', '" + reader["product_id"].ToString() + "', '" + employee_id_db.ToString() + "', '" + customer_id_db.ToString() + "', '" + clock_in_id + "', '" + DateTime.Now.ToShortTimeString() +"');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                }

                reader.Close();
                conn.Close();


                GetSetData.query = "delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                //**************************************************************************************

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }
        
        private void addCurrentBillToNoSale()
        {
            try
            {
                sure.Message_choose("Are you sure you want to set this invoice in no sale!");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    insertIntoNoSaleTable();
                    fun_clear_cart_records();
                    clearDataGridView();

                    autoOpenCashDrawer();
                }

                txt_barcode.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insertIntoNoSaleTable()
        {
            try
            {
                txt_date.Text = DateTime.Now.ToShortDateString();
                TextData.dates = txt_date.Text;
                TextData.employee = lbl_employee.Text;
                TextData.customer_name = lbl_customer.Text;
                TextData.customerCode = lblCustomerCode.Text;

                if (lbl_employee.Text == "")
                {
                    TextData.employee = "nill";
                }


                int customer_id_db = 0;
                //*****************************************************************************************
                int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);


                if (TextData.customer_name == "" && TextData.customerCode == "")
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = 'nill');";
                    customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                }
                else
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                    customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                }

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //*****************************************************************************************

                GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "');";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string product_id_db = reader["product_id"].ToString();
                    string stock_id_db = reader["stock_id"].ToString();

                    TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db);
                    TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db);
                    double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                    double wholeSalePrice_db = data.NumericValues("whole_sale_price", "pos_stock_details", "stock_id", stock_id_db);


                    TextData.full_pkg = 0;

                    if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                    {
                        TextData.quantity = double.Parse(reader["quantity"].ToString());
                        TextData.full_pkg = TextData.quantity / TextData.pkg;
                    }

                    TextData.total_pur_price = pur_price_db * double.Parse(reader["quantity"].ToString());
                    TextData.wholeSale = wholeSalePrice_db * double.Parse(reader["quantity"].ToString());


                    GetSetData.query = @"insert into pos_no_sale values ('" + TextData.dates + "' , '" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "', '" + TextData.total_pur_price.ToString() + "','" + reader["total_amount"].ToString() + "', '" + reader["tax"].ToString() + "' , '" + TextData.wholeSale.ToString() + "', '" + reader["note"].ToString() + "', '" + reader["product_id"].ToString() + "', '" + employee_id_db.ToString() + "', '" + customer_id_db.ToString() + "', '" + clock_in_id + "', '" + DateTime.Now.ToShortTimeString() +"');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = "delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //**************************************************************************************
                }

                reader.Close();
                conn.Close();

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

      
        private void btn_void_Click(object sender, EventArgs e)
        {
            cancelCurrentBill();
        }

        private void btn_recovery_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (lbl_employee.Text != "" && lbl_customer.Text != "")
            //    {
            //        //Customer_sales_recovery.get_employee = lbl_employee.Text;
            //        Customer_sales_recovery.get_customer = lbl_customer.Text;
            //        Customer_sales_recovery.get_customerCode = lblCustomerCode.Text;
            //        //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Customer recovery [" + lbl_customer.Text + "  " + lblCustomerCode.Text + "] (Recovery button click...)", role_id);

            //        Customer_sales_recovery.role_id = role_id;

            //        using (Customer_sales_recovery add_customer = new Customer_sales_recovery())
            //        {
            //            add_customer.ShowDialog();
            //        }
            //    }
            //    else
            //    {
            //        error.errorMessage("Please select the employee and customer first!");
            //        error.ShowDialog();
            //    }
            //    txt_barcode.Focus();
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void funFillCurtWithLastOrder()
        {
            try
            {
                //fun_clear_cart_records();
                //clearDataGridView();
                //***************************************************

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select pos_sales_details.prod_id, pos_stock_details.stock_id, pos_products.prod_name as Items_Name, pos_sales_details.quantity as Qty,
                                    pos_sales_details.total_marketPrice as taxation, pos_sales_details.discount as 'Discount', pos_sales_details.Total_price as Amount
                                    from pos_sales_details inner join pos_sales_accounts on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_customers on pos_sales_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_sales_accounts.employee_id = pos_employees.employee_id
                                    inner join pos_products on pos_sales_details.prod_id = pos_products.product_id inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
                                    where pos_sales_accounts.billNo = '" + TextData.billNo.ToString() + "';";

                data.Connect();
                data.adptr_ = new SqlDataAdapter(GetSetData.query, data.conn_);
                DataTable dt = new DataTable();
                data.adptr_.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    string item_barcode = "";
                    string brand = "";
                    //string category = "";
                    string quantity_db = "";

                    item_barcode = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", item["stock_id"].ToString());
                    quantity_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "quantity", "pos_stock_details", "stock_id", item["stock_id"].ToString());

                    //string category_id_db = data.UserPermissions("category_id", "pos_products", "product_id", item["prod_id"].ToString());
                    //category = data.UserPermissions("title", "pos_category", "category_id", category_id_db);

                    string brand_id_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_id", "pos_products", "product_id", item["prod_id"].ToString());
                    brand = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_title", "pos_brand", "brand_id", brand_id_db);


                    btnCart cartItem = new btnCart();

                    cartItem.Name = item["stock_id"].ToString();
                    cartItem.productID = item["prod_id"].ToString();
                    cartItem.stockID = item["stock_id"].ToString();
                    cartItem.Brand = brand;
                    cartItem.ItemsName = item["Items_Name"].ToString();
                    cartItem.barcode = item_barcode;
                    cartItem.availableStock = double.Parse(quantity_db);
                    cartItem.note = item_barcode;
                    cartItem.Quantity = item["Qty"].ToString();
                    cartItem.ChangeQuantity = item["Qty"].ToString();
                    //cartItem.categoryID = category_id_db;
                    //cartItem.brandID = brand_id_db;
                    cartItem.employee = lbl_employee.Text;
                    cartItem.customer_name = lbl_customer.Text;
                    cartItem.customer_code = lblCustomerCode.Text;
                    cartItem.clock_in_id = clock_in_id;
                    cartItem.is_return = txt_status.Checked;
                    cartItem.macAddress = macAddress;


                    cartItem.tax = double.Parse(item["taxation"].ToString());
                    cartItem.price = (double.Parse(item["Amount"].ToString()) / double.Parse(item["Qty"].ToString()));
                    cartItem.TotalAmount = item["Amount"].ToString();
                    cartItem.discount = double.Parse(item["Discount"].ToString());
                    cartItem.Discount = item["Discount"].ToString();


                    //********************

                    CartFlowLayout.Controls.Add(cartItem);
                    cartItem.Click += new System.EventHandler(this.CartItemButton_Click);
                    //*********************************************************                          

                    string customer_id = "";

                    if (lbl_customer.Text != "")
                    {
                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                        customer_id = data.SearchStringValuesFromDb(GetSetData.query);
                    }


                    string isReturn = "";

                    if (txt_status.Checked)
                    {
                        isReturn = "True";

                        if (TextData.customer_name != "" && TextData.customer_name != "nill" && TextData.customer_name != "Walk-In")
                        {
                            lbl_customer.Text = TextData.customer_name;
                            lblCustomerCode.Text = TextData.customerCode;
                        }  
                    }


                    GetSetData.query = "insert into pos_cart_items values ('" + cartItem.ItemsName + "' , '" + cartItem.barcode + "' , '" + cartItem.Quantity + "' , '" + Math.Round(cartItem.price, 2).ToString() + "' , '" + Math.Round(cartItem.tax, 2).ToString() + "' , '" + Math.Round(cartItem.discount, 2).ToString() + "' , '" + Math.Round(double.Parse(cartItem.TotalAmount), 2).ToString() + "' , '" + cartItem.availableStock.ToString() + "' ,  '" + cartItem.note + "' , '" + cartItem.productID + "' , '" + cartItem.stockID + "' , '' , '' , '" + macAddress + "' , '" + txtCustomerPoints.Text + "' , '" + isReturn.ToString() + "' , '" + customer_id + "' , '" + user_id.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }


                //GetSetData.query = "update pos_sales_accounts set is_returned = 'true' where (billNo = '" + TextData.billNo.ToString() + "');";
                //data.insertUpdateCreateOrDelete(GetSetData.query);


                calculateGrandTotals();

                txt_total_discount.Text = data.UserPermissions("discount", "pos_sales_accounts", "billNo", TextData.billNo.ToString());
                txtReturnDiscount.Text = "-" + txt_total_discount.Text;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }
        
        //private void funFillCurtWithLastOrder()
        //{
        //    try
        //    {
        //        int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_sales_accounts", "billNo", TextData.billNo);
        //        txt_total_items.Text = data.UserPermissions("no_of_items", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //        txt_total_qty.Text = data.UserPermissions("total_qty", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //        txt_sub_total.Text = data.UserPermissions("sub_total", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //        txt_discount.Text = data.UserPermissions("discount", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //        txt_tax.Text = data.UserPermissions("tax", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());

        //        CartDataGridView.DataSource = null;

        //        int a = CartDataGridView.Rows.Count;

        //        // Refresh Button Event is Generated:
        //        for (int i = 0; i < a; i++)
        //        {
        //            foreach (DataGridViewRow row in CartDataGridView.Rows)
        //            {

        //                CartDataGridView.Rows.Clear();
        //            }
        //        }

        //        GetSetData.query = @"select pos_products.prod_name as Items_Name, pos_products.barcode as Barcode, pos_sales_details.quantity as Qty, pos_sales_details.pkg as PKG, pos_sales_details.full_pkg as Full_PKG, 
        //                            pos_sales_details.Total_price/pos_sales_details.quantity as Rate, pos_sales_details.discount as 'Discount', pos_sales_details.Total_price as Amount, 
        //                            pos_sales_details.quantity * pos_stock_details.market_value as marketPrice, pos_stock_details.quantity as stock, pos_sales_details.note
        //                            from pos_sales_details inner join pos_sales_accounts on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
        //                            inner join pos_customers on pos_sales_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_sales_accounts.employee_id = pos_employees.employee_id
        //                            inner join pos_products on pos_sales_details.prod_id = pos_products.product_id inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
        //                            where pos_sales_accounts.billNo = '" + TextData.billNo.ToString() + "';";

        //        data.Connect();
        //        data.adptr_ = new SqlDataAdapter(GetSetData.query, data.conn_);
        //        DataTable dt = new DataTable();
        //        data.adptr_.Fill(dt);

        //        foreach (DataRow item in dt.Rows)
        //        {
        //            int n = CartDataGridView.Rows.Add();
        //            CartDataGridView.Rows[n].Cells[0].Value = item["Items_Name"].ToString();
        //            CartDataGridView.Rows[n].Cells[1].Value = item["Qty"].ToString();
        //            CartDataGridView.Rows[n].Cells[2].Value = item["Discount"].ToString();
        //            CartDataGridView.Rows[n].Cells[3].Value = item["marketPrice"].ToString();
        //            CartDataGridView.Rows[n].Cells[4].Value = item["Amount"].ToString();
        //            CartDataGridView.Rows[n].Cells[5].Value = item["stock"].ToString();
        //            CartDataGridView.Rows[n].Cells[8].Value = item["note"].ToString();
        //            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Last Customer Invoices [" + TextData.billNo + "] items " + CartDataGridView.Rows[n].Cells[1].Value.ToString() + "]", role_id);
        //        }

        //        CartDataGridView.Rows.OfType<DataGridViewRow>().Last().Selected = true;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //    finally
        //    {
        //        data.Disconnect();
        //    }
        //}

        private void isTransactionInvoiceReturned()
        {
            try
            {
                if (isInvoiceReturned)
                {
                    if (txt_status.Checked)
                    {
                        txt_status.Checked = false;
                        btnReturn.FillColor = Color.DodgerBlue;

                        txtReturnSubTotal.Visible = false;
                        txtReturnGrandTotal.Visible = false;
                        txtReturnAmountDue.Visible = false;
                        txtReturnPoints.Visible = false;
                        txtReturnTax.Visible = false;
                        txtReturnDiscount.Visible = false;
                        txtReturnItems.Visible = false;
                        txtReturnQuantity.Visible = false;

                        //************************************

                        txt_sub_total.Visible = true;
                        txtGrandTotal.Visible = true;
                        txt_amount_due.Visible = true;
                        txtCustomerPoints.Visible = true;
                        txtTaxation.Visible = true;
                        txt_total_discount.Visible = true;
                        txt_total_items.Visible = true;
                        txt_total_qty.Visible = true;
                    }
                    else
                    {
                        txt_status.Checked = true;
                        btnReturn.FillColor = Color.Red;

                        txtReturnSubTotal.Visible = true;
                        txtReturnGrandTotal.Visible = true;
                        txtReturnAmountDue.Visible = true;
                        txtReturnPoints.Visible = true;
                        txtReturnTax.Visible = true;
                        txtReturnDiscount.Visible = true;
                        txtReturnItems.Visible = true;
                        txtReturnQuantity.Visible = true;

                        //************************************

                        txtReturnSubTotal.Text = txt_sub_total.Text + "-";
                        txtReturnGrandTotal.Text = txtGrandTotal.Text + "-";
                        txtReturnAmountDue.Text = txt_amount_due.Text + "-";

                        txtReturnPoints.Text = "-" + txtCustomerPoints.Text;
                        txtReturnTax.Text = "-" + txtTaxation.Text;
                        txtReturnDiscount.Text = "-" + txt_total_discount.Text;
                        txtReturnItems.Text = "-" + txt_total_items.Text;
                        txtReturnQuantity.Text = "-" + txt_total_qty.Text;

                        //************************************

                        txt_sub_total.Visible = false;
                        txtGrandTotal.Visible = false;
                        txt_amount_due.Visible = false;
                        txtCustomerPoints.Visible = false;
                        txtTaxation.Visible = false;
                        txt_total_discount.Visible = false;
                        txt_total_items.Visible = false;
                        txt_total_qty.Visible = false;
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void customerLastRecipt()
        {
            try
            {
                if (TextData.customer_name != "" && TextData.customer_name != "nill" && TextData.customer_name != "Walk-In")
                {
                    TextData.billNo = "";
                    isInvoiceReturned = false;
                    TextData.customer_name = lbl_customer.Text;
                    TextData.customerCode = lblCustomerCode.Text;
                    form_last_receipt.role_id = role_id;
                    Login_info.controllers.Button_controls.customer_last_receipt_buttons();

                    if (TextData.billNo != "")
                    {
                        TextData.returnBillNo = TextData.billNo;
                        funFillCurtWithLastOrder();
                        isTransactionInvoiceReturned();
                        FillLastCreditsTextBox();
                    }
                    else
                    {
                        TextData.returnBillNo = "";
                    }
                }
                else
                {
                    TextData.billNo = "";
                    isInvoiceReturned = false;
                    TextData.customer_name = "nill";
                    TextData.customerCode = data.UserPermissions("cus_code", "pos_customers", "full_name", TextData.customer_name);
                    form_last_receipt.role_id = role_id;
                    Login_info.controllers.Button_controls.customer_last_receipt_buttons();

                    if (TextData.billNo != "")
                    {
                        TextData.returnBillNo = TextData.billNo;
                        funFillCurtWithLastOrder();
                        isTransactionInvoiceReturned();
                        FillLastCreditsTextBox();
                    }
                    else
                    {
                        TextData.returnBillNo = "";
                    }
                }

                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_last_receipt_Click(object sender, EventArgs e)
        {
            customerLastRecipt();

            txt_barcode.Select();
        }

        private void ordersButtonDetails()
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Customer order button click [" + TextData.billNo + "]", role_id);

                TextData.customer_name = "";
                TextData.customer_name = lbl_customer.Text;
                TextData.customer_name = lblCustomerCode.Text;

                form_customer_orders.user_id = user_id;
                form_customer_orders.role_id = role_id;
                Login_info.controllers.Button_controls.customer_orders_buttons();
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            ordersButtonDetails();
        }

        private void funFillCurtWithUnholdItems()
        {
            try
            {
                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "billNo", TextData.billNo.ToString());
                string dicount = data.UserPermissions("discount", "pos_hold_items", "billNo", TextData.billNo.ToString());

                txt_total_discount.Text = dicount;


                GetSetData.query = @"select pos_products.prod_name as Items_Name, pos_stock_details.item_barcode as Barcode, pos_hold_items_details.quantity as Qty, pos_hold_items_details.pkg as PKG, 
                                    pos_hold_items_details.full_pkg as Full_PKG, pos_hold_items_details.Total_price/pos_hold_items_details.quantity as Rate,  pos_hold_items_details.discount as 'Discount', 
                                    pos_hold_items_details.Total_price as Amount,  pos_hold_items_details.quantity * pos_stock_details.market_value as marketPrice, pos_stock_details.quantity as stock,
                                    pos_hold_items_details.note,pos_hold_items_details.taxation, pos_hold_items_details.prod_id, pos_hold_items_details.stock_id
                                    from pos_hold_items_details inner join pos_hold_items on pos_hold_items_details.sales_acc_id = pos_hold_items.sales_acc_id
                                    inner join pos_customers on pos_hold_items.customer_id = pos_customers.customer_id inner join pos_employees on pos_hold_items.employee_id = pos_employees.employee_id
                                    inner join pos_products on pos_hold_items_details.prod_id = pos_products.product_id inner join pos_stock_details on pos_stock_details.stock_id = pos_hold_items_details.stock_id
                                    where pos_hold_items.billNo = '" + TextData.billNo.ToString() + "';";

                data.Connect();
                data.adptr_ = new SqlDataAdapter(GetSetData.query, data.conn_);
                DataTable dt = new DataTable();
                data.adptr_.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    string item_barcode = "";
                    string brand = "";
                    //string category = "";
                    string quantity_db = "";

                    item_barcode = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", item["stock_id"].ToString());
                    quantity_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "quantity", "pos_stock_details", "stock_id", item["stock_id"].ToString());

                    //string category_id_db = data.UserPermissions("category_id", "pos_products", "product_id", item["prod_id"].ToString());
                    //category = data.UserPermissions("title", "pos_category", "category_id", category_id_db);

                    string brand_id_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_id", "pos_products", "product_id", item["prod_id"].ToString());
                    brand = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_title", "pos_brand", "brand_id", brand_id_db);


                    btnCart cartItem = new btnCart();

                    cartItem.Name = item["stock_id"].ToString();
                    cartItem.productID = item["prod_id"].ToString();
                    cartItem.stockID = item["stock_id"].ToString();
                    cartItem.Brand = brand;
                    cartItem.ItemsName = item["Items_Name"].ToString();
                    cartItem.barcode = item_barcode;
                    cartItem.availableStock = double.Parse(quantity_db);
                    cartItem.note = item_barcode;
                    cartItem.Quantity = item["Qty"].ToString();
                    cartItem.ChangeQuantity = item["Qty"].ToString();
                    //cartItem.categoryID = category_id_db;
                    //cartItem.brandID = brand_id_db;
                    cartItem.employee = lbl_employee.Text;
                    cartItem.customer_name = lbl_customer.Text;
                    cartItem.customer_code = lblCustomerCode.Text;
                    cartItem.clock_in_id = clock_in_id;
                    cartItem.is_return = txt_status.Checked;
                    cartItem.macAddress = macAddress;


                    cartItem.tax = double.Parse(item["taxation"].ToString());
                    cartItem.price = (double.Parse(item["Amount"].ToString()) / double.Parse(item["Qty"].ToString()));
                    cartItem.TotalAmount = item["Amount"].ToString();
                    cartItem.discount = double.Parse(item["Discount"].ToString());


                    //********************

                    CartFlowLayout.Controls.Add(cartItem);
                    cartItem.Click += new System.EventHandler(this.CartItemButton_Click);
                    //*********************************************************                          
                  
                    
                    string customer_id = "";

                    if (lbl_customer.Text != "")
                    {
                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                        customer_id = data.SearchStringValuesFromDb(GetSetData.query);
                    }

                    string isReturn = "";

                    if (txt_status.Checked)
                    {
                        isReturn = "True";
                    }


                    GetSetData.query = "insert into pos_cart_items values ('" + cartItem.ItemsName + "' , '" + cartItem.barcode + "' , '" + cartItem.Quantity + "' , '" + Math.Round(cartItem.price, 2).ToString() + "' , '" + Math.Round(cartItem.tax, 2).ToString() + "' , '" + Math.Round(cartItem.discount, 2).ToString() + "' , '" + Math.Round(double.Parse(cartItem.TotalAmount), 2).ToString() + "' , '" + cartItem.availableStock.ToString() + "' ,  '" + cartItem.note + "' , '" + cartItem.productID + "' , '" + cartItem.stockID + "' , '' , '' , '" + macAddress + "' , '" + txtCustomerPoints.Text + "' , '" + isReturn.ToString() + "' , '" + customer_id + "' , '" + user_id.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                calculateGrandTotals();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }

        private bool fun_delete_hold_items_db()
        {
            try
            {
                GetSetData.Ids = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "billNo", TextData.billNo.ToString());
                //========================================================
                if (GetSetData.Ids != 0)
                {
                    GetSetData.query = @"delete from pos_hold_items_details where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.query = @"delete from pos_hold_items where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================
                    return true;
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

        private void btn_unhold_Click(object sender, EventArgs e)
        {
            try
            {
                form_unhold.role_id = role_id;
                Login_info.controllers.Button_controls.un_hold_orders_buttons();

                if (TextData.billNo != "")
                {
                    funFillCurtWithUnholdItems();
                    fun_delete_hold_items_db();
                    FillLastCreditsTextBox();
                }

                holdItemsBackgroundWorker.RunWorkerAsync();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customerList()
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Customer [" + TextData.billNo + "] details (customer button click...)", role_id);
                Customer_details.isDropDown = false;
                Customer_details.count = 0;
                Customer_details.role_id = role_id;
                Customer_details.user_id = user_id;
                Customer_details.selected_customer = "";
                Customer_details.selected_customerCode = "";
                Login_info.controllers.Button_controls.customer_buttons();
                lbl_customer.Text = Customer_details.selected_customer;
                lblCustomerCode.Text = Customer_details.selected_customerCode;
                FillLastCreditsTextBox();
                calculateAmountDue();
                txt_barcode.Focus();

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            customerList();
        }

        //private void ScanItemsByName()
        //{
        //    try
        //    {
        //        GetSetData.query = @"select prod_name, image_path, sale_price, quantity, item_barcode, product_id, stock_id
        //                            from pos_stock_details inner join  pos_products on pos_stock_details.prod_id = pos_products.product_id
        //                            inner join pos_category on pos_products.category_id = pos_category.category_id inner join pos_brand on pos_products.brand_id = pos_brand.brand_id
        //                            inner join pos_subcategory on pos_products.sub_cate_id = pos_subcategory.sub_cate_id and pos_products.status = 'Enabled'
        //                            where (pos_products.prod_name like '" + txtProductName.Text + "%');";

        //        FillRegularItemsPanal(GetSetData.query, "");
        //        // ******************************************************************

        //        //TextData.prod_name = "";
        //        //TextData.prod_name = txt_search.Text;
        //        //GetSetData.Permission = "";

        //        //if (TextData.prod_name != "")
        //        //{
        //        //    //GetSetData.Data = data.UserPermissions("prod_name", "pos_products", "barcode", TextData.barcode);

        //        //    GetSetData.query = @"select * from pos_products;";
        //        //    SqlCommand cmd = new SqlCommand(GetSetData.query, data.conn_);
        //        //    SqlDataReader reader;

        //        //    data.conn_.Open();
        //        //    reader = cmd.ExecuteReader();

        //        //    while (reader.Read())
        //        //    {
        //        //        GetSetData.Permission = reader["prod_name"].ToString();

        //        //        if (TextData.prod_name == GetSetData.Permission)
        //        //        {
        //        //            fun_add_records_from_menu_gridview(TextData.prod_name, "");
        //        //            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Searching item [" + TextData.prod_name + "] by name", role_id); 
        //        //            txt_barcode.Text = "";
        //        //            txt_price.Text = "";
        //        //            txt_qty.Text = "";
        //        //            txt_barcode.Focus();
        //        //            break;

        //        //            beep_sound();
        //        //        }
        //        //    }
        //        //    reader.Close();
        //        //}
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private void fun_get_employee_balance()
        {
            try
            {
                string amountSale = "";
                string amountReturn = "";
                TextData.totalAmount = 0;

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                //int employee_id_db = data.UserPermissionsIds("emp_id", "pos_users", "user_id", user_id.ToString());


                GetSetData.query = "select sum(paid) from pos_sales_accounts where (pos_sales_accounts.clock_in_id = '" + clock_in_id + "');";
                amountSale = data.SearchStringValuesFromDb(GetSetData.query);



                GetSetData.query = "select sum(paid) from pos_return_accounts where (pos_return_accounts.clock_in_id = '" + clock_in_id + "');";
                amountReturn = data.SearchStringValuesFromDb(GetSetData.query);


                if (amountSale == "NULL" || amountSale == "")
                {
                    amountSale = "0";
                }


                if (amountReturn == "NULL" || amountReturn == "")
                {
                    amountReturn = "0";
                }


                TextData.totalAmount = double.Parse(amountSale) - double.Parse(amountReturn);

                if (TextData.totalAmount >= 0)
                {
                    lbl_balance.Text = "Cash   |   " + GetSetData.currency() + Math.Round(TextData.totalAmount, 2).ToString();
                }
                else
                {
                    lbl_balance.Text = "Cash   |   0.00";
                }

                //*****************************************************************

                amountSale = "";
                amountReturn = "";
                TextData.totalAmount = 0;

                //GetSetData.query = "select sum(credit_card_amount) from pos_sales_accounts inner join pos_employees on pos_sales_accounts.employee_id = pos_employees.employee_id where (pos_sales_accounts.employee_id = '" + employee_id_db.ToString() + "') and (pos_sales_accounts.clock_in_id = '" + clock_in_id + "');";
                //amountSale = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "select sum(credit_card_amount) from pos_sales_accounts where (pos_sales_accounts.clock_in_id = '" + clock_in_id + "');";
                amountSale = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = "select sum(credit_card_amount) from pos_return_accounts where (pos_return_accounts.clock_in_id = '" + clock_in_id + "');";
                amountReturn = data.SearchStringValuesFromDb(GetSetData.query);


                if (amountSale == "NULL" || amountSale == "")
                {
                    amountSale = "0";
                }


                if (amountReturn == "NULL" || amountReturn == "")
                {
                    amountReturn = "0";
                }


                TextData.totalAmount = double.Parse(amountSale) - double.Parse(amountReturn);

                if (TextData.totalAmount >= 0)
                {
                    lblCreditCardBalance.Text = "Credit Card   |   " + GetSetData.currency() + Math.Round(TextData.totalAmount, 2).ToString();
                }
                else
                {
                    lblCreditCardBalance.Text = "Credit Card   |   0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void holdItemsAlert()
        {
            try
            {
                GetSetData.query = "select count(sales_acc_id) from pos_hold_items;";
                int is_hold_items_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                if (is_hold_items_exist > 0)
                {
                    btn_unhold.BackColor = Color.Transparent;
                    //btn_unhold.FillColor = Color.FromArgb(230, 0, 0);
                    btn_unhold.FillColor = Color.White;
                    btn_unhold.BorderColor = Color.FromArgb(230, 0, 0);
                    btn_unhold.BorderRadius = 5;
                    btn_unhold.BorderThickness = 1;
                    btn_unhold.FocusedColor = Color.FromArgb(240, 10, 10);
                    btn_unhold.HoverState.FillColor = Color.FromArgb(240, 10, 10);
                    btn_unhold.HoverState.ForeColor = Color.White;
                    btn_unhold.ForeColor = Color.FromArgb(230, 0, 0);

                    btn_unhold.Image = Spices_pos.Properties.Resources.uhold_item_red;
                    btn_unhold.HoverState.Image = Spices_pos.Properties.Resources.uhold_item_white;
                }
                else
                {
                    btn_unhold.BackColor = Color.Transparent;
                    btn_unhold.FillColor = Color.Transparent;
                    btn_unhold.BorderRadius = 5;
                    btn_unhold.BorderThickness = 1;
                    btn_unhold.BorderColor = Color.Gainsboro;
                    btn_unhold.FocusedColor = Color.LemonChiffon;
                    btn_unhold.HoverState.FillColor = Color.LemonChiffon;
                    btn_unhold.HoverState.ForeColor = Color.Gray;
                    btn_unhold.ForeColor = Color.Gray;

                    btn_unhold.Image = Spices_pos.Properties.Resources.unhold_item_gray;
                    btn_unhold.HoverState.Image = Spices_pos.Properties.Resources.unhold_item_gray;
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        private void customerLoyalityPointsAlert()
        {
            try
            {
                if (generalSettings.ReadField("autoRedeemPoints") == "No")
                {
                    if (double.Parse(txtCustomerPoints.Text) >= double.Parse(generalSettings.ReadField("pointsRedeemLimit")))
                    {
                        btnRewards.BackColor = Color.Transparent;
                        btnRewards.FillColor = Color.White;
                        btnRewards.BorderColor = Color.FromArgb(9, 170, 41);
                        btnRewards.BorderRadius = 5;
                        btnRewards.BorderThickness = 1;
                        btnRewards.FocusedColor = Color.FromArgb(19, 180, 51);
                        btnRewards.HoverState.FillColor = Color.FromArgb(19, 180, 51);
                        btnRewards.HoverState.ForeColor = Color.White;
                        btnRewards.ForeColor = Color.FromArgb(9, 170, 41);
            
                        btnRewards.Image = Spices_pos.Properties.Resources.reward_green;
                        btnRewards.HoverState.Image = Spices_pos.Properties.Resources.reward_white;
                    }
                    else
                    {
                        btnRewards.BackColor = Color.Transparent;
                        btnRewards.FillColor = Color.Transparent;
                        btnRewards.BorderRadius = 5;
                        btnRewards.BorderThickness = 1;
                        btnRewards.BorderColor = Color.Gainsboro;
                        btnRewards.FocusedColor = Color.LemonChiffon;
                        btnRewards.HoverState.FillColor = Color.LemonChiffon;
                        btnRewards.HoverState.ForeColor = Color.Gray;
                        btnRewards.ForeColor = Color.Gray;
                        
                        btnRewards.Image = Spices_pos.Properties.Resources.reward_gray;
                        btnRewards.HoverState.Image = Spices_pos.Properties.Resources.reward_gray;
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void getCreditCardBalance()
        {
            try
            {
                TextData.totalAmount = 0;
                TextData.net_total = 0;

                int employee_id_db = data.UserPermissionsIds("emp_id", "pos_users", "user_id", user_id.ToString());
                //int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", employeeName);

                GetSetData.query = @"select sum(amount_due) from pos_sales_accounts inner join pos_employees on 
                                    pos_sales_accounts.employee_id = pos_employees.employee_id 
                                    where (pos_sales_accounts.employee_id = '" + employee_id_db.ToString() + "') and (pos_sales_accounts.date = '" + txt_date.Text + "') and (check_sale_status = 'Credit Card');";

                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select sum(amount_due) from pos_return_accounts inner join pos_employees on 
                                    pos_return_accounts.employee_id = pos_employees.employee_id where (pos_return_accounts.employee_id = '" + employee_id_db.ToString() + "') and (pos_return_accounts.date = '" + txt_date.Text + "') and (check_sale_status = 'Credit Card');";

                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);


                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    TextData.totalAmount = double.Parse(GetSetData.Data);
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    TextData.net_total = double.Parse(GetSetData.Permission);
                }

                TextData.totalAmount -= TextData.net_total;

                if (TextData.totalAmount >= 0)
                {
                    lbl_credit_card.Text = "C-Card  |   " + Math.Round(TextData.totalAmount, 2).ToString();
                }
                else
                {
                    lbl_credit_card.Text = "C-Card  |   0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void getCreditedBalance()
        {
            try
            {
                TextData.totalAmount = 0;
                TextData.net_total = 0;

                //int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", employeeName);
                int employee_id_db = data.UserPermissionsIds("emp_id", "pos_users", "user_id", user_id.ToString());


                GetSetData.query = @"select sum(credits) from pos_sales_accounts inner join pos_employees on 
                                    pos_sales_accounts.employee_id = pos_employees.employee_id 
                                    where (pos_sales_accounts.employee_id = '" + employee_id_db.ToString() + "') and (pos_sales_accounts.date = '" + txt_date.Text + "');";

                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select sum(credits) from pos_return_accounts inner join pos_employees on 
                                    pos_return_accounts.employee_id = pos_employees.employee_id where (pos_return_accounts.employee_id = '" + employee_id_db.ToString() + "') and (pos_return_accounts.date = '" + txt_date.Text + "');";

                GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);


                if (GetSetData.Data != "NULL" && GetSetData.Data != "")
                {
                    TextData.totalAmount = double.Parse(GetSetData.Data);
                }

                if (GetSetData.Permission != "NULL" && GetSetData.Permission != "")
                {
                    TextData.net_total = double.Parse(GetSetData.Permission);
                }

                TextData.totalAmount -= TextData.net_total;

                if (TextData.totalAmount >= 0)
                {
                    lbl_credits.Text = "T.Credit  |   " + Math.Round(TextData.totalAmount, 2).ToString();
                }
                else
                {
                    lbl_credits.Text = "T.Credit  |   0.00";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private void guarantorsList()
        //{
        //    try
        //    {
        //        if (txt_status.Text == "Installment")
        //        {
        //            if (lbl_employee.Text != "" && lbl_employee.Text != "nill")
        //            {
        //                if (lbl_customer.Text != "" && lbl_customer.Text != "nill")
        //                {
        //                    GetSetData.query = @"SELECT salesCodes FROM pos_AllCodes order by id desc;";
        //                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //                    GetSetData.Ids++;
        //                    TextData.billNo = "SALE_" + GetSetData.Ids.ToString();
        //                    GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Guarantors [" + TextData.billNo + "] details (Guarantors button click...)", role_id);
        //                    form_GranterDetails.billNo = TextData.billNo;
        //                    form_GranterDetails.employeeName = lbl_employee.Text;
        //                    form_GranterDetails.customerName = lbl_customer.Text;
        //                    form_GranterDetails.customerCode = lblCustomerCode.Text;
        //                    form_GranterDetails.count = 0;
        //                    form_GranterDetails.role_id = role_id;
        //                    Button_controls.Add_Grantors_buttons();
        //                }
        //                else
        //                {
        //                    error.errorMessage("Please select the customer!");
        //                    error.ShowDialog();
        //                }
        //            }
        //            else
        //            {
        //                error.errorMessage("Please select the employee!");
        //                error.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage("Please set the status as installment!");
        //            error.ShowDialog();
        //        }

        //        txt_barcode.Focus();

        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private void btn_sale_men_Click(object sender, EventArgs e)
        {
            //guarantorsList();
        }

        private void btn_refresh_menu_Click(object sender, EventArgs e)
        {
            refresh_menu();
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.net_total = 0;
                TextData.discount = 0;

                if (txtGrandTotal.Text != "")
                {
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                }

                if (txt_discount.Text != "")
                {
                    TextData.discount = double.Parse(txt_discount.Text);
                }

                if (TextData.net_total > 0 && TextData.discount > 0)
                {
                    TextData.discount = (TextData.net_total * TextData.discount) / 100;

                    if (TextData.discount >= 0)
                    {
                        txt_total_discount.Text = Math.Round(TextData.discount, 2).ToString();
                    }
                    else
                    {
                        txt_total_discount.Text = "0";
                    }
                }
                else
                {
                    txt_total_discount.Text = "0";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
      
        public void calculateAmountDue()
        {
            try
            {
                TextData.net_total = 0;
                TextData.discount = 0;
                TextData.totalAmount = 0;
                double taxation = 0;
                double customer_discount = 0;
                double redeem_discount = 0;
                double tipAmount = 0;
                string totalDiscount = "";

                if (txtGrandTotal.Text != "")
                {
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                }  
                
                if (txtTaxation.Text != "")
                {
                    taxation = double.Parse(txtTaxation.Text);
                }
                
                if (txtTipAmount.Text != "")
                {
                    tipAmount = double.Parse(txtTipAmount.Text);
                }
          

                if (TextData.promotionsDiscount || TextData.allDiscounts)
                {
                    GetSetData.query = "select sum(discount) from pos_cart_items where (mac_address = '" + macAddress + "');";
                    totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

                    if (totalDiscount == "NULL" || totalDiscount == "")
                    {
                        totalDiscount = "0";
                    }
                }
                else
                {
                    totalDiscount = "0";
                }
            

                if (lbl_customer.Text != "" && lbl_customer.Text != "nill")
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    
                    //*****************************************************************************************
                
                    if (GetSetData.Ids != 0)
                    {
                        if (TextData.customerDiscount || TextData.allDiscounts)
                        {
                            customer_discount = data.NumericValues("discount", "pos_customers", "customer_id", GetSetData.Ids.ToString());

                            redeem_discount = customer_discount;
                        }
                        
                        //***************************************************

                        string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");

                        if (autoSetPoints == "Yes")
                        {
                            string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");

                            double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", GetSetData.Ids.ToString());

                            if (txt_status.Checked)
                            {
                                customer_points_db = Math.Round(((TextData.net_total - taxation) / double.Parse(addPointPerAmount)), 2);

                                txtCustomerPoints.Text = customer_points_db.ToString();
                            }
                            else
                            {
                                customer_points_db += Math.Round(((TextData.net_total - taxation) / double.Parse(addPointPerAmount)), 2);

                                txtCustomerPoints.Text = customer_points_db.ToString();
                            }


                            string autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");

                            if (autoRedeemPoints == "Yes")
                            {
                                if (TextData.loyalityProgramDiscount || TextData.allDiscounts)
                                {
                                    int pointsRedeemLimit = data.UserPermissionsIds("pointsRedeemLimit", "pos_general_settings");
                                    string pointsDiscountInPercentageDb = data.UserPermissions("pointsDiscountInPercentage", "pos_general_settings");
                                    string autoRedeemDiscount = data.UserPermissions("pointsRedeemDiscount", "pos_general_settings");

                                    if (bool.Parse(pointsDiscountInPercentageDb))
                                    {
                                        autoRedeemDiscount = (((TextData.net_total - taxation) * double.Parse(autoRedeemDiscount)) / 100).ToString();
                                    }

                                    int total_points = Convert.ToInt32(customer_points_db) / pointsRedeemLimit;

                                    customer_discount = ((TextData.net_total - taxation) * customer_discount) / 100;

                                    redeem_discount = (double.Parse(autoRedeemDiscount) * total_points) + customer_discount;
                                }
                                else
                                {
                                    redeem_discount = ((TextData.net_total - taxation) * customer_discount) / 100;
                                }


                                if (redeem_discount > 0)
                                {
                                    txt_total_discount.Text = (redeem_discount + double.Parse(totalDiscount)).ToString();
                                }
                                else
                                {
                                    txt_total_discount.Text = (customer_discount + double.Parse(totalDiscount)).ToString();
                                }


                                TextData.discount = double.Parse(txt_total_discount.Text);


                                txtCustomerPoints.Text = (customer_points_db - TextData.discount).ToString();
                            }
                            else
                            {

                                customer_discount = ((TextData.net_total - taxation) * customer_discount) / 100;
                               
                                if ((customer_discount + double.Parse(totalDiscount)) > 0)
                                {
                                    txt_total_discount.Text = (customer_discount + double.Parse(totalDiscount)).ToString();

                                    TextData.discount = double.Parse(txt_total_discount.Text);

                                    txtCustomerPoints.Text = (customer_points_db - TextData.discount).ToString();
                                }
                                else
                                {
                                    TextData.discount = 0;
                                    txt_total_discount.Text = "0";
                                }
                              
                            }
                           
                        }
                        else
                        {
                            customer_discount = (TextData.net_total * customer_discount) / 100;

                            if (customer_discount > 0)
                            {
                                txt_total_discount.Text = (customer_discount + double.Parse(totalDiscount)).ToString();
                            }
                        }
                    }
                    else
                    {
                        txt_total_discount.Text = Math.Round(double.Parse(totalDiscount), 2).ToString();
                    }
                }
                else
                {
                    //txt_total_discount.Text = "0";

                    txt_total_discount.Text = Math.Round(double.Parse(totalDiscount), 2).ToString();

                    TextData.discount = double.Parse(txt_total_discount.Text);  
                }

                //***************************************************

                if (TextData.discount >= 0 && TextData.discount <= TextData.net_total)
                {
                    TextData.totalAmount = TextData.net_total - double.Parse(txt_total_discount.Text);

                    txt_amount_due.Text = TextData.totalAmount.ToString();
                }
                else
                {
                    txt_total_discount.Text = "0";
                    txt_amount_due.Text = txtGrandTotal.Text;
                }


                //Managing Tips

                double amountDue = 0;
                double totalTips = 0;
            
                if (txt_amount_due.Text != "")
                {
                    amountDue = double.Parse(txt_amount_due.Text);
                }

                if (amountDue > 0)
                {
                    if (txtIsTipInPercentage.Text == "True")
                    {
                        totalTips = (tipAmount * amountDue) / 100;

                        txtTotalTipAmount.Text = Math.Round(totalTips, 2).ToString();
                    }
                }
                else
                {
                    txtTotalTipAmount.Text = Math.Round(tipAmount, 2).ToString();
                }
             

                #region
                //***************************************************
                txtReturnSubTotal.Text = txt_sub_total.Text + "-";
                txtReturnGrandTotal.Text = txtGrandTotal.Text + "-";
                txtReturnAmountDue.Text = txt_amount_due.Text + "-";

                txtReturnPoints.Text = "-" + txtCustomerPoints.Text;
                txtReturnTax.Text = "-" + txtTaxation.Text;
                txtReturnDiscount.Text = "-" + txt_total_discount.Text;
                txtReturnItems.Text = "-" + txt_total_items.Text;
                txtReturnQuantity.Text = "-" + txt_total_qty.Text;


                //Set 2nd Screen Values ***************************************************

                TextData.isReturn = txt_status.Checked;
                TextData.invoiceCustomerName = lbl_customer.Text;
                TextData.invoiceCustomerPoints = txtCustomerPoints.Text;
                TextData.invoiceDiscount = txt_total_discount.Text;
                TextData.invoiceTotalItems = txt_total_items.Text;
                TextData.invoiceQuantity = txt_total_qty.Text;
                TextData.invoiceSubTotal = txt_sub_total.Text;
                TextData.invoiceTotalTaxation = txtTaxation.Text;
                TextData.invoiceGrandTotal = txtGrandTotal.Text;
                TextData.invoiceAmountDue = txt_amount_due.Text;
                TextData.invoiceChangeAmount = txtChangeAmount.Text;

                #endregion

                //if (IsFormOpen(typeof(fromSecondScreen)))
                //{
                //    fromSecondScreen.instance.getCartItems();
                //    fromSecondScreen.instance.isReturn();
                //}

                if (IsFormOpen(typeof(fromSecondScreen)))
                {
                    fromSecondScreen.instance.Invoke((MethodInvoker)delegate
                    {
                        fromSecondScreen.instance.getCartItems();
                        fromSecondScreen.instance.isReturn();
                    });
                }

                customerLoyalityPointsAlert();

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_sub_total_TextChanged(object sender, EventArgs e)
        {
            calculateAmountDue();
        }

        private void txt_total_discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.net_total = 0;
                TextData.discount = 0;
                string totalDiscount = "";
                string totalTax = "";
                double taxation = 0;
                double newTaxation = 0;


                GetSetData.query = "select sum(discount) from pos_cart_items where (mac_address = '" + macAddress + "');";
                totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalDiscount == "NULL" || totalDiscount == "")
                {
                    totalDiscount = "0";
                }
                
                GetSetData.query = "select sum(tax) from pos_cart_items where (mac_address = '" + macAddress + "');";
                totalTax = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalTax == "NULL" || totalTax == "")
                {
                    totalTax = "0";
                }


                //if (txtTaxation.Text != "")
                //{
                //    taxation = double.Parse(txtTaxation.Text);
                //}

                //****************************************

                if (txtGrandTotal.Text != "")
                {
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                }

                //*****************************************************************************************

                GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                //*****************************************************************************************

                string autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");
                double customer_points_db = data.NumericValues("points", "pos_customers", "customer_id", GetSetData.Ids.ToString());

                string taxPercentage = data.UserPermissions("taxation", "pos_general_settings");

                if (taxPercentage == "")
                {
                    taxPercentage = "0";
                }

                if (autoSetPoints == "Yes")
                {
                    string addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");
                 
                    if (txt_status.Checked)
                    {
                        customer_points_db = Math.Round(((TextData.net_total - double.Parse(totalTax)) / double.Parse(addPointPerAmount)), 2);
                    }
                    else
                    {
                        customer_points_db += Math.Round(((TextData.net_total - double.Parse(totalTax)) / double.Parse(addPointPerAmount)), 2);

                    }
                }

                //*****************************************************************************************

                if (txt_total_discount.Text != "")
                {
                    if (double.Parse(txt_total_discount.Text) <= double.Parse(txtGrandTotal.Text))
                    {
                        TextData.discount = (TextData.net_total - double.Parse(totalTax)) - double.Parse(txt_total_discount.Text);


                        if (TextData.discount <= TextData.net_total)
                        {
                            if (double.Parse(totalTax) > 0)
                            {
                                newTaxation = ((TextData.discount * double.Parse(taxPercentage)) / 100);
                            }


                            txt_amount_due.Text = Math.Round((TextData.discount + newTaxation), 2).ToString();
                            txtTaxation.Text = Math.Round(newTaxation, 2).ToString();


                            if (double.Parse(txt_total_discount.Text) == 0)
                            {
                                txt_amount_due.Text = TextData.net_total.ToString();
                                txtTaxation.Text = Math.Round(double.Parse(totalTax), 2).ToString();
                            }



                            txtCustomerPoints.Text = Math.Round((customer_points_db - double.Parse(txt_total_discount.Text)), 2).ToString();
                        }
                        else
                        {
                            TextData.discount = (TextData.net_total - double.Parse(totalTax)) - double.Parse(totalDiscount);

                            if (TextData.discount <= TextData.net_total)
                            {
                                txt_total_discount.Text = totalDiscount;

                                if (double.Parse(totalTax) > 0)
                                {
                                    newTaxation = ((TextData.discount * double.Parse(taxPercentage)) / 100);
                                }


                                txt_amount_due.Text = Math.Round((TextData.discount + newTaxation), 2).ToString();
                                txtTaxation.Text = Math.Round(newTaxation, 2).ToString();

                                if (double.Parse(txt_total_discount.Text) == 0)
                                {
                                    txt_amount_due.Text = Math.Round((TextData.discount + double.Parse(totalTax)), 2).ToString();
                                    txtTaxation.Text = Math.Round(double.Parse(totalTax), 2).ToString();
                                }

                                txtCustomerPoints.Text = Math.Round((customer_points_db - double.Parse(totalDiscount)), 2).ToString();
                            }
                            else
                            {
                                txt_total_discount.Text = "0";
                                txt_amount_due.Text = Math.Round(TextData.net_total, 2).ToString();

                                txtCustomerPoints.Text = customer_points_db.ToString();
                            }

                        }

                    }
                    else
                    {
                        txt_total_discount.Text = "0";
                    }
                }
                else
                {
                    txt_amount_due.Text = Math.Round(TextData.net_total, 2).ToString();
                    txtCustomerPoints.Text = customer_points_db.ToString();
                }


                TextData.invoiceCustomerName = lbl_customer.Text;
                TextData.invoiceCustomerPoints = txtCustomerPoints.Text;
                TextData.invoiceDiscount = txt_total_discount.Text;
                TextData.invoiceTotalItems = txt_total_items.Text;
                TextData.invoiceQuantity = txt_total_qty.Text;
                TextData.invoiceSubTotal = txt_sub_total.Text;
                TextData.invoiceTotalTaxation = txtTaxation.Text;
                TextData.invoiceGrandTotal = txtGrandTotal.Text;
                TextData.invoiceAmountDue = txt_amount_due.Text;
                TextData.invoiceChangeAmount = txtChangeAmount.Text;

                if (IsFormOpen(typeof(fromSecondScreen)))
                {
                    fromSecondScreen.instance.Invoke((MethodInvoker)delegate
                    {
                        fromSecondScreen.instance.getCartItems();
                        fromSecondScreen.instance.isReturn();
                    });
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

        }

        private void printRecentBill()
        {
            try
            {
                if (TextData.billNo != "")
                {
                    if (TextData.return_value == true)
                    {
                        if (TextData.status == "Sale")
                        {
                            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Print Sale invoice[" + TextData.billNo + "] details", role_id);
                            form_cus_sales_report report = new form_cus_sales_report();
                            report.ShowDialog();
                            //Direct_report d_report = new Direct_report();
                            //d_report.print_btn_click();
                        }
                        else if (TextData.status == "Return")
                        {
                            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Print Sale Returns invoice[" + TextData.billNo + "] details", role_id);
                            form_cus_returns_report report = new form_cus_returns_report();
                            report.ShowDialog();
                        }
                        else if (TextData.status == "Installment")
                        {
                            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Print contract from invoice [" + TextData.billNo + "] details", role_id);
                            //agreementForm.billNo = TextData.billNo;
                            //agreementForm report = new agreementForm();
                            //report.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("There is nothing in the cart!");
                        error.ShowDialog();
                    }
                }

                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
           try
            {
                if (TextData.billNo != "")
                {
                    if (TextData.checkPrintReport == true)
                    {
                        GetSetData.query = @"select default_printer from pos_general_settings;";
                        string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                        GetSetData.query = @"select preview_receipt from pos_general_settings;";
                        string preview_receipt = data.SearchStringValuesFromDb(GetSetData.query);

                        if (preview_receipt == "Yes")
                        {
                            form_cus_sales_report report = new form_cus_sales_report();
                            report.ShowDialog();
                        }
                        else
                        {
                            if (printer_name != "")
                            {
                                PrintDirectReceipt(printer_name, TextData.billNo);
                            }
                            else
                            {
                                error.errorMessage("Printer not found!");
                                error.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    error.errorMessage("Sorry no recent bill found!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGroupedItemsPanal(string query)
        {
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;
            SqlDataReader reader;
    
            try
            {
                refresh_menu();
                GetSetData.Permission = "";

                TextData.saved_image = generalSettings.ReadField("picture_path");

                cmd = new SqlCommand(query, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TextData.prod_name = reader["title"].ToString();

                    btnProduct b = new btnProduct();
                    b.Name = TextData.prod_name;
                    b.ItemsName = TextData.prod_name;
                    b.Price = "";
                    b.Stock = "";
                    /* Color bcol = Color.FromArgb(220, 90, 0);*/ //200, 70, 60
                                                                  //Color fcol = Color.FromArgb(255, 255, 255);
                                                                  //b.FillColor = bcol;
                                                                  //b.FillColor2 = bcol;
                    b.Cursor = Cursors.Hand;

                    // To Hide Redunduncy of buttons
                    foreach (Control controls in pnl_list.Controls)
                    {
                        GetSetData.Permission = controls.Name;

                        if (GetSetData.Permission == TextData.prod_name)
                        {
                            break;
                        }
                    }

                    // Adding Buttons in Panel
                    if (GetSetData.Permission != TextData.prod_name)
                    {
                        pnl_list.Controls.Add(b);
                        //b.Text = TextData.prod_name + "\n" + "Items: " + categoryWiseItems(TextData.prod_name);
                        b.Click += new System.EventHandler(this.Button1_Click);
                        b.MouseHover += new System.EventHandler(this.categoryWiseItemsHover);
                        b.MouseLeave += new System.EventHandler(this.categoryWiseItemsLeave);
                    }
                }

                reader.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        private void categoryWiseItemsHover(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn = sender as btnProduct;
                //Color bcol = Color.FromArgb(235, 110, 10);
                Color bcol = Color.LemonChiffon;
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void categoryWiseItemsLeave(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn = sender as btnProduct;
                //Color bcol = Color.FromArgb(220, 90, 0);
                Color bcol = Color.White;
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn1 = sender as btnProduct;
                refresh_menu();
                TextData.general_options = "3";
                TextData.refereneValue = btn1.Name.ToString();
                GetSetData.SetNextPreviousButtonValues("");

                //*****************************************************************************************
                GetSetData.query = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * from ViewCounterCashGroupedIndividualItems 
                                     where (title = '" + btn1.Name.ToString() + "') and (status = 'Enabled')) SELECT * FROM CounterGroupedIndividualItems  where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                if (group_counter == 0)
                {
                    FillRegularItemsPanal(GetSetData.query, "");
                    group_counter = 1;
                }

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
                //lblPageNo.Text = "Page 1";
                lblPageNo.Text = "1";
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void getCategoies()
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Grouped items button click...", role_id);
                refresh_menu();
                TextData.general_options = "2";
                GetSetData.SetNextPreviousButtonValues("");

                //*****************************************************************************************
                GetSetData.query = @"WITH CounterGroupedItems AS (SELECT ROW_NUMBER() OVER (ORDER BY title) AS RowNumber, * from ViewCounterCashGroupedItems) SELECT * FROM CounterGroupedItems   
                                     where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                if (group_counter == 0)
                {
                    FillGroupedItemsPanal(GetSetData.query);
                    group_counter = 1;
                }

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
                lblPageNo.Text = "1";
                //lblPageNo.Text = "Page 1";
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_groups_Click(object sender, EventArgs e)
        {
            getCategoies();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 10, 68 size
                //pnl_new_sale.Size = new Size(10, 68);
                //pnl_new_sale.Visible = false;
                //btn_customer.Visible = true;
                //btn_print.Visible = true;
                btn_remarks.Visible = true;
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();           
            }
        }

        private void btn_new_sale_Click(object sender, EventArgs e)
        {
            try
            {
                fun_clear_cart_records();
                clearDataGridView();

                btn_customer.Visible = true;
                btn_remarks.Visible = true;
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage("Cart is already empty!");
                error.ShowDialog();
            }
        }

        private bool fun_Hold_items_details_db()
        {
            try
            {
                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_items_exists_in_cart != 0)
                {

                    TextData.customer_name = lbl_customer.Text;
                    TextData.customerCode = lblCustomerCode.Text;
                    TextData.employee = lbl_employee.Text;
                    TextData.dates = txt_date.Text;
                    TextData.no_of_items = double.Parse(txt_total_items.Text);
                    TextData.total_qty = double.Parse(txt_total_qty.Text);
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                    TextData.total_taxation = double.Parse(txtTaxation.Text);
                    TextData.discount = double.Parse(txt_total_discount.Text);
                    TextData.tax = txt_tax.Text;
                    TextData.totalAmount = double.Parse(txt_amount_due.Text);
                    TextData.credits = 0;
                    TextData.cash = 0;
                    TextData.lastCredit = 0;

                    if (txt_status.Checked == false)
                    {
                        TextData.status = "Sale";
                    }
                    else
                    {
                        TextData.status = "Return";
                    }

                    if (lbl_customer.Text == "")
                    {
                        TextData.customer_name = "nill";

                        GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                        TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                        lblCustomerCode.Text = TextData.customerCode;
                    }

                    if (lbl_employee.Text == "")
                    {
                        TextData.employee = "nill";
                    }

                    if (TextData.comments == "")
                    {
                        TextData.comments = "nill";
                    }

                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                    int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //*****************************************************************************************
                    int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                    if (TextData.totalAmount != 0)
                    {
                        TextData.billNo = auto_generate_hold_items_code();

                        GetSetData.query = @"insert into pos_hold_items values ('" + TextData.billNo.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "' , '" + TextData.lastCredit.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.comments.ToString() + "' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.total_taxation.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "billNo", TextData.billNo);
                        // *****************************************************************************************

                        // *****************************************************************************************

                        GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

                        SqlConnection conn = new SqlConnection(webConfig.con_string);
                        SqlCommand cmd;
                        SqlDataReader reader;

                        cmd = new SqlCommand(GetSetData.query, conn);

                        conn.Open();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string product_id_db = reader["product_id"].ToString();
                            string stock_id_db = reader["stock_id"].ToString();

                            TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db.ToString());
                            double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            // **************************************************************************************

                            TextData.full_pkg = 0;

                            if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                            {
                                TextData.quantity = double.Parse(reader["quantity"].ToString());
                                TextData.full_pkg = TextData.quantity / TextData.pkg;
                            }

                            GetSetData.query = @"insert into pos_hold_items_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "' , '" + reader["total_amount"].ToString() + "'  , '" + reader["tax"].ToString() + "'  , '" + reader["note"].ToString() + "' , '" + sales_acc_id_db.ToString() + "' , '" + product_id_db + "' , '" + stock_id_db + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                            // **************************************************************************************
                        }

                        reader.Close();
                        conn.Close();
                        // *****************************************************************************************

                        GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    fun_clear_cart_records();
                    clearDataGridView();

                    return true;
                }
                else
                {
                    error.errorMessage("Unable to hold bill without items!");
                    error.ShowDialog();

                    return false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btn_hold_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.net_total = double.Parse(txtGrandTotal.Text);
                TextData.total_taxation = double.Parse(txtTaxation.Text);

                if (TextData.net_total != 0)
                {
                    fun_Hold_items_details_db();
                }

                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void autoOpenCashDrawer()
        {
            try
            {
                GetSetData.query = @"select default_printer from pos_general_settings;";
                string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select printer_model from pos_general_settings;";
                string printer_model = data.SearchStringValuesFromDb(GetSetData.query);


                CashDrawerData.OpenDrawer(printer_name, printer_model);
            }
            catch (Exception es)
            {
                error.errorMessage("Error opening cash drawer: " + es.Message);
                error.ShowDialog();
            }
        }

        private void btn_calculator_Click(object sender, EventArgs e)
        {
            addCurrentBillToNoSale();
        }

        private void btn_expired_items_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Stock button click...", role_id);
            product_details.role_id = role_id;
            product_details.count = 0;
            Login_info.controllers.Button_controls.Stock_buttons();
            txt_barcode.Focus();
        }

        private void btn_stock_Click(object sender, EventArgs e)
        {
            using (choose_product product = new choose_product())
            {
                choose_product.count = 0;
                choose_product.role_id = role_id;
                choose_product.selectedProductName = "";
                choose_product.selectedProductBarcode = "";
                choose_product.selectedProductID = "";
                choose_product.providedValueType = "select";
                choose_product.providedValue = txt_barcode.Text;

                product.ShowDialog();
            }

            if (choose_product.selectedProductName != "" && choose_product.selectedProductBarcode != "")
            {
                fun_add_records_from_menu_gridview(choose_product.selectedProductName, choose_product.selectedProductBarcode, choose_product.selectedProductID, choose_product.selectedStockID);
            }

            txt_barcode.Focus();
        }

        private void btnShelfItems_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Counter Cash (POS) Form", "Shelf items button click...", role_id);
            product_details.role_id = role_id;
            formShelfItems.count = 0;
            Login_info.controllers.Button_controls.Shelf_Items_buttons();
            txt_barcode.Focus();
        }

        private void chkWholeSale_CheckedChanged(object sender, EventArgs e)
        {
            txt_barcode.Focus();
        }

        private void txt_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_barcode.Focus();
        }

        private void txt_sale_option_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_barcode.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextData.general_options != "5")
                {
                    refresh_menu();
                    GetSetData.SetNextPreviousButtonValues("next");
                }

                if (TextData.general_options == "1")
                {
                    GetSetData.query = @"WITH CounterRegularItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, *
	                                     from ViewCounterCashRegularItems) SELECT * FROM CounterRegularItems   
                                         where (status = 'Enabled') and (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterRegularItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber from ViewCounterCashRegularItems) SELECT max(RowNumber) FROM CounterRegularItems;";
                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }
                else if (TextData.general_options == "2")
                {
                    GetSetData.query = @"WITH CounterGroupedItems AS (SELECT ROW_NUMBER() OVER (ORDER BY title) AS RowNumber, * from ViewCounterCashGroupedItems) SELECT * FROM CounterGroupedItems   
                                         where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterGroupedItems AS(SELECT ROW_NUMBER() OVER (ORDER BY title) AS RowNumber from ViewCounterCashGroupedItems) SELECT max(RowNumber) FROM CounterGroupedItems;";
                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }
                else if (TextData.general_options == "3")
                {
                    GetSetData.query = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * from ViewCounterCashGroupedIndividualItems 
                                         where (title = '" + TextData.refereneValue + "') and (status = 'Enabled')) SELECT * FROM CounterGroupedIndividualItems  where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * 
                                        from ViewCounterCashGroupedIndividualItems where (title = '" + TextData.refereneValue + "') and (status = 'Enabled')) SELECT max(RowNumber) FROM CounterGroupedIndividualItems;";

                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }

                GetSetData.EnableDisableNextPreviousButton("next", lastCountedValue, btnNext, btnPrevious, lblPageNo);

                if (TextData.general_options == "2")
                {
                    if (group_counter == 0)
                    {
                        FillGroupedItemsPanal(GetSetData.query);
                        group_counter = 1;
                    }
                }
                else
                {
                    if (group_counter == 0)
                    {
                        if (TextData.general_options == "1")
                        {
                            FillRegularItemsPanal(GetSetData.query, "regular");
                        }
                        else
                        {
                            FillRegularItemsPanal(GetSetData.query, "");
                        }
                        group_counter = 1;
                    }
                }
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                refresh_menu();
                GetSetData.SetNextPreviousButtonValues("previous");

                if (TextData.general_options == "1")
                {
                    GetSetData.query = @"WITH CounterRegularItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, *
	                                     from ViewCounterCashRegularItems) SELECT * FROM CounterRegularItems   
                                         where (status = 'Enabled') and (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterRegularItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber from ViewCounterCashRegularItems) SELECT min(RowNumber) FROM CounterRegularItems;";
                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }
                else if (TextData.general_options == "2")
                {
                    GetSetData.query = @"WITH CounterGroupedItems AS (SELECT ROW_NUMBER() OVER (ORDER BY title) AS RowNumber, * from ViewCounterCashGroupedItems) SELECT * FROM CounterGroupedItems   
                                         where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterGroupedItems AS(SELECT ROW_NUMBER() OVER (ORDER BY title) AS RowNumber from ViewCounterCashGroupedItems) SELECT min(RowNumber) FROM CounterGroupedItems;";
                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }
                else if (TextData.general_options == "3")
                {
                    GetSetData.query = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * from ViewCounterCashGroupedIndividualItems 
                                         where (title = '" + TextData.refereneValue + "') and (status = 'Enabled')) SELECT * FROM CounterGroupedIndividualItems  where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                    GetSetData.Data = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * 
                                        from ViewCounterCashGroupedIndividualItems where (title = '" + TextData.refereneValue + "') and (status = 'Enabled')) SELECT min(RowNumber) FROM CounterGroupedIndividualItems;";
                    lastCountedValue = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.Data);
                }

                GetSetData.EnableDisableNextPreviousButton("previous", lastCountedValue, btnNext, btnPrevious, lblPageNo);

                if (TextData.general_options != "2")
                {
                    if (group_counter == 0)
                    {
                        if (TextData.general_options == "1")
                        {
                            FillRegularItemsPanal(GetSetData.query, "regular");
                        }
                        else
                        {
                            FillRegularItemsPanal(GetSetData.query, "");
                        }

                        group_counter = 1;
                    }
                }
                else
                {
                    if (group_counter == 0)
                    {
                        FillGroupedItemsPanal(GetSetData.query);
                        group_counter = 1;
                    }
                }

                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscount.Text, e);
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (chkChooseType.SelectedIndex == 0)
                {
                    ScanByBarcode();
                }
                else if (chkChooseType.SelectedIndex == 1)
                {
                    GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress +"');";
                    double items_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (items_already_exist == 0)
                    {
                        TextData.billNo = txt_barcode.Text;
                    
                        string is_invoice_exist = data.UserPermissions("sales_acc_id", "pos_sales_accounts", "billNo", txt_barcode.Text);


                        if (is_invoice_exist != "")
                        {
                            if (TextData.billNo != "")
                            {
                                isInvoiceReturned = true;

                                string customerId = data.UserPermissions("customer_id", "pos_sales_accounts", "billNo", txt_barcode.Text);
                                lbl_customer.Text = data.UserPermissions("full_name", "pos_customers", "customer_id", customerId);
                                lblCustomerCode.Text = data.UserPermissions("cus_code", "pos_customers", "customer_id", customerId);

                                TextData.returnBillNo = TextData.billNo;
                                funFillCurtWithLastOrder();
                                isTransactionInvoiceReturned();
                                FillLastCreditsTextBox();
                            }
                            else
                            {
                                TextData.returnBillNo = "";
                            }
                        }
                        else
                        {
                            error.errorMessage("No Record Found. Please check the receipt no and try again!");
                            error.ShowDialog();
                        }

                        txt_barcode.Text = "";
                    }
                    else
                    {
                        error.errorMessage("Please clear the cart first to made new transaction.");
                        error.ShowDialog();

                        txt_barcode.Text = "";
                    }
                }
                
            }
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ScanByProductName();
            }
        }

        private void btnChequeDetails_Click(object sender, EventArgs e)
        {
            using (formChequeDetails add_customer = new formChequeDetails())
            {
                //GetSetData.SaveLogHistoryDetails("Creating Installment Plans Form", "Cheques button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void chkScanByImei_CheckedChanged(object sender, EventArgs e)
        {
            txt_barcode.Select();
        }

        #region
        //private void PrintDirectReceipt(string printer_name)
        //{
        //    try
        //    {
        //        //GetSetData.query = @"SELECT * from ReportViewBillWiseSales WHERE (billNo = '" + TextData.billNo + "');";

        //        //var dt = GetDataTable(GetSetData.query);

        //        string query = "SELECT * from ReportViewBillWiseSales WHERE billNo = @BillNo";

        //        using (SqlConnection connection = new SqlConnection(webConfig.con_string))
        //        {
        //            connection.Open();

        //            using (SqlCommand cmd = new SqlCommand(query, connection))
        //            {
        //                cmd.Parameters.AddWithValue("@BillNo", TextData.billNo);
        //                DataTable dt = new DataTable();
        //                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //                {
        //                    adapter.Fill(dt);
        //                }

        //                LocalReport report = new LocalReport();

        //                //string path = Path.GetDirectoryName(Application.StartupPath);
        //                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_sales_report.rdlc";
        //                report.ReportPath = fullPath;


        //                report.EnableExternalImages = true;

        //                //Linear barcode = new Linear();
        //                ////Linear ItemNote = new Linear();
        //                //barcode.Type = BarcodeType.CODE128;

        //                //barcode.Data = TextData.billNo.ToString();
        //                //barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;
        //                ////invoiceBarcode = barcode.drawBarcodeAsBytes();


        //                //report.SetParameters(new ReportParameter("pBarcodeImage", new Uri(barcode.drawBarcodeAsBytes().ToString()).AbsoluteUri));



        //                //Report Parameters **********************************************************
        //                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
        //                string logo1 = data.UserPermissions("logo_path", "pos_configurations");


        //                if (logo1 != "nill" && logo1 != "")
        //                {
        //                    GetSetData.query = GetSetData.Data + logo1;
        //                    //ReportParameter pLogo1 = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
        //                    //report.SetParameters(pLogo1);   

        //                    report.SetParameters(new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri));
        //                }
        //                else
        //                {
        //                    //ReportParameter pLogo1 = new ReportParameter("pLogo", "");
        //                    //report.SetParameters(pLogo1);
        //                    report.SetParameters(new ReportParameter("pLogo", ""));
        //                }

        //                //*******************************************************************************************

        //                GetSetData.query = @"select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
        //                                 where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";
        //                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

        //                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
        //                {
        //                    GetSetData.Data = "0";
        //                }

        //                //ReportParameter pCounterSalesLastCredits = new ReportParameter("pCounterSalesLastCredits", GetSetData.Data);
        //                //report.SetParameters(pCounterSalesLastCredits);

        //                report.SetParameters(new ReportParameter("pCounterSalesLastCredits", GetSetData.Data));
        //                //*******************************************************************************************


        //                // Retrive Report Settings from db *******************************************************************************************
        //                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
        //                //ReportParameter pMainTitle = new ReportParameter("pTitle", GetSetData.Data);
        //                //report.SetParameters(pMainTitle);
        //                report.SetParameters(new ReportParameter("pTitle", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
        //                //ReportParameter pAddress = new ReportParameter("pAddress", GetSetData.Data);
        //                //report.SetParameters(pAddress);
        //                report.SetParameters(new ReportParameter("pAddress", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
        //                //ReportParameter pPhoneNo = new ReportParameter("pPhoneNo", GetSetData.Data);
        //                //report.SetParameters(pPhoneNo);
        //                report.SetParameters(new ReportParameter("pPhoneNo", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
        //                //ReportParameter pNote = new ReportParameter("pNote", GetSetData.Data);
        //                //report.SetParameters(pNote);
        //                report.SetParameters(new ReportParameter("pNote", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
        //                //ReportParameter pCopyrights = new ReportParameter("pCopyrights", GetSetData.Data);
        //                //report.SetParameters(pCopyrights);
        //                report.SetParameters(new ReportParameter("pCopyrights", GetSetData.Data));
        //                //*******************************************************************************************

        //                TextData.general_options = "";
        //                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
        //                //ReportParameter showNote = new ReportParameter("showNote", TextData.general_options);
        //                //report.SetParameters(showNote);
        //                report.SetParameters(new ReportParameter("showNote", TextData.general_options));
        //                //*******************************************************************************************

        //                //PrintToPrinter(report, printer_name);

        //                report.DataSources.Add(new ReportDataSource("cus_sales", dt));

        //                // Enable external images
        //                report.EnableExternalImages = true;

        //                PrintToPrinter(report, printer_name);
        //            }
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //public static void PrintToPrinter(LocalReport report, string printerName)
        //{
        //    try
        //    {
        //        byte[] renderedBytes = report.Render("Image", null, out _, out _, out _, out _, out _);

        //        // Create a print document
        //        using (PrintDocument printDoc = new PrintDocument())
        //        {
        //            printDoc.PrinterSettings.PrinterName = printerName;

        //            if (!printDoc.PrinterSettings.IsValid)
        //            {
        //                throw new Exception("Error: Cannot find the specified printer.");
        //            }
        //            else
        //            {
        //                int m_currentPageIndex = 0;

        //                int desiredPageWidth = 300; // Adjust to your preferred width in pixels
        //                int desiredPageHeight = 800;

        //                printDoc.PrintPage += (sender, e) =>
        //                {
        //                    //if (m_currentPageIndex < report.GetTotalPages())
        //                    //{
        //                    using (MemoryStream ms = new MemoryStream(renderedBytes))
        //                    using (Image renderedImage = Image.FromStream(ms))
        //                    {
        //                        // Adjust the rectangular area with printer margins and set the desired page dimensions
        //                        Rectangle adjustedRect = new Rectangle(
        //                            2,
        //                            0,
        //                            desiredPageWidth, // Set the desired width in pixels
        //                            desiredPageHeight // Set the desired height in pixels
        //                        );


        //                        // Draw a white background for the report
        //                        e.Graphics.FillRectangle(Brushes.White, adjustedRect);

        //                        // Draw the report content
        //                        e.Graphics.DrawImage(renderedImage, adjustedRect);

        //                        // Prepare for the next page. Make sure we haven't hit the end.
        //                        m_currentPageIndex++;
        //                        e.HasMorePages = (m_currentPageIndex < report.GetTotalPages());
        //                    }
        //                    //}
        //                };

        //                // Start the printing process
        //                printDoc.Print();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception gracefully (e.g., log the error)
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //Print Direct Receipt Here is the code................
        #endregion

        private void PrintTicketDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from reportViewPrintTicket WHERE (billNo = '" + BillNo + "');";

                var dt = GetDataTable(GetSetData.query);
              
                dt.Columns.Add("invoiceBarcode", typeof(byte[])); // Assuming 'invoiceBarcode' will store barcode image bytes

                Linear barcode = new Linear();
                barcode.Type = BarcodeType.CODE128;

                // Loop through each row in the dataset
                foreach (DataRow row in dt.Rows)
                {
                    // Set the data (billNo) for the barcode
                    barcode.Data = row["billNo"].ToString();
                    barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;

                    // Generate the barcode image as bytes
                    byte[] barcodeImageBytes = barcode.drawBarcodeAsBytes();

                    // Assign the barcode image bytes to the 'invoiceBarcode' column
                    row["invoiceBarcode"] = barcodeImageBytes;
                }

                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_ticket.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ticket_ds", dt));

                //*********************************************************************************
                report.EnableExternalImages = true;

                //Report Parameters **********************************************************
                //GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                //string logo1 = data.UserPermissions("logo_path", "pos_configurations");

                //if (logo1 != "nill" && logo1 != "")
                //{
                //    GetSetData.query = GetSetData.Data + logo1;
                //    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                //    report.SetParameters(pLogo1);
                //}
                //else
                //{
                //    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", "");
                //    report.SetParameters(pLogo1);
                //}

                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pMainTitle = new Microsoft.Reporting.WinForms.ReportParameter("pTitle", GetSetData.Data);
                report.SetParameters(pMainTitle);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pAddress = new Microsoft.Reporting.WinForms.ReportParameter("pAddress", GetSetData.Data);
                report.SetParameters(pAddress);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pPhoneNo = new Microsoft.Reporting.WinForms.ReportParameter("pPhoneNo", GetSetData.Data);
                report.SetParameters(pPhoneNo);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pNote = new Microsoft.Reporting.WinForms.ReportParameter("pNote", GetSetData.Data);
                report.SetParameters(pNote);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);
                //*******************************************************************************************

                TextData.general_options = "";
                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
                Microsoft.Reporting.WinForms.ReportParameter showNote = new Microsoft.Reporting.WinForms.ReportParameter("showNote", TextData.general_options);
                report.SetParameters(showNote);

                //*******************************************************************************************

                PrintToPrinter(report, printer_name);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        private void PrintDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from ReportViewBillWiseSales WHERE (billNo = '" + BillNo + "');";

                var dt = GetDataTable(GetSetData.query);
              
                dt.Columns.Add("invoiceBarcode", typeof(byte[])); // Assuming 'invoiceBarcode' will store barcode image bytes

                Linear barcode = new Linear();
                barcode.Type = BarcodeType.CODE128;

                // Loop through each row in the dataset
                foreach (DataRow row in dt.Rows)
                {
                    // Set the data (billNo) for the barcode
                    barcode.Data = row["billNo"].ToString();
                    barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;

                    // Generate the barcode image as bytes
                    byte[] barcodeImageBytes = barcode.drawBarcodeAsBytes();

                    // Assign the barcode image bytes to the 'invoiceBarcode' column
                    row["invoiceBarcode"] = barcodeImageBytes;
                }

                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_sales_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", dt));

                //*********************************************************************************
                report.EnableExternalImages = true;

                //Report Parameters **********************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                string logo1 = data.UserPermissions("logo_path", "pos_configurations");

                if (logo1 != "nill" && logo1 != "")
                {
                    GetSetData.query = GetSetData.Data + logo1;
                    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    report.SetParameters(pLogo1);
                }
                else
                {
                    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", "");
                    report.SetParameters(pLogo1);
                }


                //*******************************************************************************************

                GetSetData.query = @"select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
                                    where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                Microsoft.Reporting.WinForms.ReportParameter pCounterSalesLastCredits = new Microsoft.Reporting.WinForms.ReportParameter("pCounterSalesLastCredits", GetSetData.Data);
                report.SetParameters(pCounterSalesLastCredits);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pMainTitle = new Microsoft.Reporting.WinForms.ReportParameter("pTitle", GetSetData.Data);
                report.SetParameters(pMainTitle);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pAddress = new Microsoft.Reporting.WinForms.ReportParameter("pAddress", GetSetData.Data);
                report.SetParameters(pAddress);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pPhoneNo = new Microsoft.Reporting.WinForms.ReportParameter("pPhoneNo", GetSetData.Data);
                report.SetParameters(pPhoneNo);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pNote = new Microsoft.Reporting.WinForms.ReportParameter("pNote", GetSetData.Data);
                report.SetParameters(pNote);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);
                //*******************************************************************************************

                TextData.general_options = "";
                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
                Microsoft.Reporting.WinForms.ReportParameter showNote = new Microsoft.Reporting.WinForms.ReportParameter("showNote", TextData.general_options);
                report.SetParameters(showNote);

                //*******************************************************************************************

                PrintToPrinter(report, printer_name);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void PrintReturnSaleDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from ReportViewBillWiseSalesReturns WHERE (billNo = '" + BillNo + "');";

                var dt = GetDataTable(GetSetData.query);
                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_return_sales_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("return_sales", dt));

                report.EnableExternalImages = true;
                //Report Parameters **********************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                string logo1 = data.UserPermissions("logo_path", "pos_configurations");

                if (logo1 != "nill" && logo1 != "")
                {
                    GetSetData.query = GetSetData.Data + logo1;
                    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    report.SetParameters(pLogo1);
                }
                else
                {
                    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", "");
                    report.SetParameters(pLogo1);
                }

                //*******************************************************************************************

                GetSetData.query = @"select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
                                         where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                Microsoft.Reporting.WinForms.ReportParameter pCounterSalesLastCredits = new Microsoft.Reporting.WinForms.ReportParameter("pCounterSalesLastCredits", GetSetData.Data);
                report.SetParameters(pCounterSalesLastCredits);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pMainTitle = new Microsoft.Reporting.WinForms.ReportParameter("pTitle", GetSetData.Data);
                report.SetParameters(pMainTitle);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pAddress = new Microsoft.Reporting.WinForms.ReportParameter("pAddress", GetSetData.Data);
                report.SetParameters(pAddress);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pPhoneNo = new Microsoft.Reporting.WinForms.ReportParameter("pPhoneNo", GetSetData.Data);
                report.SetParameters(pPhoneNo);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pNote = new Microsoft.Reporting.WinForms.ReportParameter("pNote", GetSetData.Data);
                report.SetParameters(pNote);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);
                //*******************************************************************************************

                TextData.general_options = "";
                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
                Microsoft.Reporting.WinForms.ReportParameter showNote = new Microsoft.Reporting.WinForms.ReportParameter("showNote", TextData.general_options);
                report.SetParameters(showNote);
                //*******************************************************************************************

                PrintToPrinter(report, printer_name);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public DataTable GetDataTable(string sql)
        {
            try
            {
                var dt = new DataTable();
                data.conn_.Open();
                data.adptr_ = new SqlDataAdapter(sql, data.conn_);
                data.adptr_.Fill(dt);
                data.conn_.Close();
                return dt;
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
        }

        public static void PrintToPrinter(LocalReport report, string printer_name)
        {
            Export(report, printer_name);
        }

        public static void Export(LocalReport report, string printer_name, bool print = true, bool openCashDrawer = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3.5in</PageWidth>
                <PageHeight>8.3in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0.1</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (openCashDrawer)
            {
                CashDrawerData.OpenDrawer(printer_name, string.Empty);
            }

            if (TextData.checkPrintReport == true)
            {
                if (print)
                {
                    Print(printer_name);
                }
            }
        }

        public static void Print(string printer_name)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printer_name;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.PrintController = new StandardPrintController();
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        //private void SendingServerDetailsToAutoSavingBackup()
        //{
        //    try
        //    {
        //        GetSetData.query = @"select auto_backup from pos_general_settings;";
        //        string general_options = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (general_options == "Yes")
        //        {
        //            GetSetData.query = @"select server from pos_server_config;";
        //            string serverName = data.SearchStringValuesFromDb(GetSetData.query);

        //            GetSetData.query = @"select backup_path from pos_general_settings;";
        //            string backup_path = data.SearchStringValuesFromDb(GetSetData.query);


        //            if (backup_path != "")
        //            {
        //                Server db_server = new Server(new ServerConnection(serverName));//
        //                Backup db_backup = new Backup() { Action = BackupActionType.Database, Database = "installment_db" };

        //                db_backup.Devices.AddDevice(backup_path + "\\" + "installment_db" + "  [" + DateTime.Now.ToLongDateString() + "]" + ".bak", Microsoft.SqlServer.Management.Smo.DeviceType.File);  //@"C:\Data\pos_db.bak" 
        //                db_backup.Initialize = true;
        //                db_backup.SqlBackupAsync(db_server);
        //            }
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        //MessageBox.Show(es.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //private void autoSetInventoryHistory()
        //{
        //    try
        //    {
        //        DateTime currentDate = DateTime.Today;
        //        DateTime yesterdayDate = currentDate.AddDays(-1);

        //        GetSetData.query = @"select count(stock_id) from pos_stock_details;";
        //        double totalRecordsInStock = data.SearchNumericValuesDb(GetSetData.query);

        //        GetSetData.query = @"select count(stock_id) from pos_stock_backup where (date = '" + yesterdayDate.ToString("yyyy-MM-dd") + "');";
        //        double totalRecordsInStockBackup = data.SearchNumericValuesDb(GetSetData.query);


        //        if (totalRecordsInStockBackup < totalRecordsInStock)
        //        {
        //            string query = @"select item_barcode, quantity, pkg, tab_pieces, full_pak, pur_price, sale_price, market_value, whole_sale_price, date_of_manufacture, date_of_expiry,
        //                             trade_off, carry_exp, total_pur_price, total_sale_price, qty_alert, alert_status, discount, discount_limit, prod_id from pos_stock_details;";


        //            SqlConnection conn = new SqlConnection(webConfig.con_string);
        //            SqlCommand cmd;
        //            SqlDataReader reader;

        //            cmd = new SqlCommand(query, conn);

        //            conn.Open();
        //            reader = cmd.ExecuteReader();


        //            while (reader.Read())
        //            {
        //                string queryStock = @"select stock_id from pos_stock_backup where (date = '" + yesterdayDate.ToString("yyyy-MM-dd") + "') and (prod_id = '" + reader["prod_id"].ToString() + "');";
        //                double stockId = data.SearchNumericValuesDb(queryStock);

        //                if (stockId == 0)
        //                {
        //                    string queryHistory = @"insert into pos_stock_backup values ('" + yesterdayDate.ToString("yyyy-MM-dd") + "','" + reader["item_barcode"].ToString() + "' , '" + reader["quantity"].ToString() + "' , '" + reader["pkg"].ToString() + "' , '" + reader["tab_pieces"].ToString() + "' , '" + reader["full_pak"].ToString() + "' , '" + reader["pur_price"].ToString() + "' , '" + reader["sale_price"].ToString() + "' , '" + reader["market_value"].ToString() + "' , '" + reader["whole_sale_price"].ToString() + "' , '" + reader["date_of_manufacture"].ToString() + "' , '" + reader["date_of_expiry"].ToString() + "' , '" + reader["trade_off"].ToString() + "' , '" + reader["carry_exp"].ToString() + "' , '" + reader["total_pur_price"].ToString() + "' , '" + reader["total_sale_price"].ToString() + "' , '" + reader["qty_alert"].ToString() + "' , '" + reader["alert_status"].ToString() + "' , '" + reader["discount"].ToString() + "' , '" + reader["discount_limit"].ToString() + "' , '" + reader["prod_id"].ToString() + "');";
        //                    data.insertUpdateCreateOrDelete(queryHistory);
        //                }
        //                //else
        //                //{
        //                //    GetSetData.query = @"update pos_stock_backup set  quantity = '" + reader["quantity"].ToString() + "' , pur_price = '" + reader["pur_price"].ToString() + "' , sale_price = '" + reader["sale_price"].ToString() + "' , market_value = '" + reader["market_value"].ToString() + "' , whole_sale_price = '" + reader["whole_sale_price"].ToString() + "' , total_pur_price = '" + reader["total_pur_price"].ToString() + "' , total_sale_price = '" + reader["total_sale_price"].ToString() + "' , qty_alert = '" + reader["qty_alert"].ToString() + "' , alert_status = '" + reader["alert_status"].ToString() + "'  where (stock_id = '" + stockId.ToString() + "');";
        //                //    data.insertUpdateCreateOrDelete(GetSetData.query);
        //                //}
        //            }

        //            reader.Close();

        //            conn.Close();
        //        }

        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private void holdItemsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (holdItemsBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Thread.Sleep(800);

                holdItemsAlert();
            }
            catch (Exception es)
            {
                // Invoke the MessageBox on the main UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(es.Message);
                });
            }
        }

        private void holdItemsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    // Invoke the MessageBox on the main UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("An error occurred: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            }
            catch (Exception es)
            {
                // Invoke the MessageBox on the main UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(es.Message);
                });
            }
        }
        
        private void oneDriveBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GetSetData.runBackgroundServices();

                //// Execute exportDataToDrive in a separate STA thread
                //Thread exportThread = new Thread(ExportThreadMethod);
                //exportThread.SetApartmentState(ApartmentState.STA); // Set the apartment state before starting the thread
                //exportThread.Start();
                //exportThread.Join(); // Wait for the thread to finish
            }
            catch (Exception es)
            {
                // Invoke the MessageBox on the main UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(es.Message);
                });
            }
        }

        //private void ExportThreadMethod()
        //{
        //    try
        //    {
        //        //exportDataToDrive();
        //        //autoSetInventoryHistory();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Invoke the MessageBox on the main UI thread
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            MessageBox.Show(ex.Message);
        //        });
        //    }
        //}

        private void oneDriveBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    // Invoke the MessageBox on the main UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("An error occurred: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            }
            catch (Exception es)
            {
                // Invoke the MessageBox on the main UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(es.Message);
                });
            }
        }

        //protected override void OnFormClosed(FormClosedEventArgs e)
        //{
        //    base.OnFormClosed(e);
        //    timer.Dispose(); // Dispose the timer when the form is closed to release resources
        //}

        private void btnDeals_Click(object sender, EventArgs e)
        {
            fillDealsInMenu();
        }

        private void fillDealsInMenu()
        {
            try
            {
                refresh_menu();
                TextData.general_options = "4";
                GetSetData.SetNextPreviousButtonValues("");

                //*****************************************************************************************
                GetSetData.query = @"WITH CounterGroupedItems AS (SELECT ROW_NUMBER() OVER (ORDER BY deal_title) AS RowNumber, * from ViewCounterCashDeals) 
                                     SELECT * FROM CounterGroupedItems where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                if (group_counter == 0)
                {
                    FillDealItemsPanal(GetSetData.query);
                    group_counter = 1;
                }

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
                lblPageNo.Text = "Page 1";
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void FillDealItemsPanal(string query)
        {
            try
            {
                refresh_menu();
                TextData.general_options = "4";
                GetSetData.Permission = "";

                GetSetData.query = "select picture_path from pos_general_settings;";
                TextData.saved_image = data.SearchStringValuesFromDb(GetSetData.query);

                int back_red = data.UserPermissionsIds("deal_back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("deal_back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("deal_back_blue", "pos_colors_settings");


                data.cmd_ = new SqlCommand(query, data.conn_);

                data.conn_.Open();
                data.reader_ = data.cmd_.ExecuteReader();

                while (data.reader_.Read())
                {
                    TextData.prod_name = data.reader_["deal_title"].ToString();

                    btnProduct b = new btnProduct();
                    b.Name = TextData.prod_name;
                    b.ItemsName = TextData.prod_name;
                    b.Price = "";
                    b.Stock = "";

                    //Color bcol = Color.FromArgb(back_red, back_green, back_blue);
                    ////Color fcol = Color.FromArgb(fore_red, fore_green, fore_blue);

                    //b.FillColor = bcol;
                    //b.FillColor2 = bcol;
                    //b.ForeColor = fcol;
                    b.Cursor = Cursors.Hand;

                    // To Hide Redunduncy of buttons
                    foreach (Control controls in pnl_list.Controls)
                    {
                        GetSetData.Permission = controls.Name;

                        if (GetSetData.Permission == TextData.prod_name)
                        {
                            break;
                        }
                    }

                    // Adding Buttons in Panel
                    if (GetSetData.Permission != TextData.prod_name)
                    {
                        pnl_list.Controls.Add(b);
                        //b.Text = TextData.prod_name + "\n" + "Items: " + categoryWiseItems(TextData.prod_name);
                        b.Click += new System.EventHandler(this.Button2_Click);
                        b.MouseHover += new System.EventHandler(this.DealWiseItemsHover);
                        b.MouseLeave += new System.EventHandler(this.DealWiseItemsLeave);
                    }
                }

                data.reader_.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.conn_.Close();
            }
        }

        private void DealWiseItemsHover(object sender, EventArgs e)
        {
            try
            {
                int fore_red = data.UserPermissionsIds("deal_fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("deal_fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("deal_fore_blue", "pos_colors_settings");

                btnProduct btn = sender as btnProduct;
                Color bcol = Color.FromArgb(fore_red, fore_green, fore_blue);
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DealWiseItemsLeave(object sender, EventArgs e)
        {
            try
            {
                int back_red = data.UserPermissionsIds("deal_back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("deal_back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("deal_back_blue", "pos_colors_settings");

                btnProduct btn = sender as btnProduct;
                Color bcol = Color.FromArgb(back_red, back_green, back_blue);
                btn.FillColor = bcol;
                btn.FillColor2 = bcol;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                btnProduct btn1 = sender as btnProduct;
                refresh_menu();
                TextData.general_options = "4";
                TextData.refereneValue = btn1.Name.ToString();
                GetSetData.SetNextPreviousButtonValues("");

                //*****************************************************************************************
                GetSetData.query = @"WITH CounterGroupedIndividualItems AS (SELECT ROW_NUMBER() OVER (ORDER BY product_id) AS RowNumber, * from CounterDealIndividualItems 
                                     where (deal_title = '" + btn1.Name.ToString() + "') and (status = 'Enabled')) SELECT * FROM CounterGroupedIndividualItems  where (RowNumber between '" + GetSetData.minValue + "' and '" + GetSetData.menuMax + "') order by RowNumber;";

                if (group_counter == 0)
                {
                    SqlConnection conn = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd = new SqlCommand(GetSetData.query, conn);
                    SqlDataReader reader;
                    conn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string product_id_db = reader["product_id"].ToString();
                        string stock_id_db = reader["stock_id"].ToString();
                        TextData.prod_name = reader["prod_name"].ToString();
                        TextData.barcode = reader["item_barcode"].ToString();
                        TextData.rate = double.Parse(reader["dealPrice"].ToString());
                        TextData.quantity = double.Parse(reader["quantity"].ToString());

                        //fun_add_records_from_menu_deals_gridview(TextData.prod_name, TextData.barcode, product_id_db, stock_id_db,  TextData.quantity, TextData.rate);
                    }

                    reader.Close();
                    conn.Close();
                    //FillRegularItemsPanal(GetSetData.query, "");
                    group_counter = 1;
                }

                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
                GetSetData.countPages = 0;
                lblPageNo.Text = "Page 1";
                fillDealsInMenu();
                txt_barcode.Focus();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnScrollUp_Click(object sender, EventArgs e)
        {
            cartItemsControl();

            //CartFlowLayout.AutoScroll = false;

            int change = CartFlowLayout.VerticalScroll.Value - CartFlowLayout.VerticalScroll.SmallChange * 30;

            CartFlowLayout.AutoScrollPosition = new Point(0, change);
        }

        private void btnScrollDown_Click(object sender, EventArgs e)
        {
            cartItemsControl();

            //CartFlowLayout.AutoScroll = false;

            int change = CartFlowLayout.VerticalScroll.Value + CartFlowLayout.VerticalScroll.SmallChange * 30;

            CartFlowLayout.AutoScrollPosition = new Point(0, change);

        }

        private void calculateGrandTotals()
        {
            try
            {
                TextData.allDiscounts = true;

                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_items_exists_in_cart != 0)
                {
                    #region

                    TextData.totalAmount = 0;
                    double discount_db = 0;

                    GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "');";

                    SqlConnection conn = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd;
                    SqlDataReader reader;

                    cmd = new SqlCommand(GetSetData.query, conn);

                    conn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string product_id_db = reader["product_id"].ToString();
                        string stock_id_db = reader["stock_id"].ToString();
                        discount_db = double.Parse(reader["discount"].ToString());
                        TextData.totalAmount = double.Parse(reader["total_amount"].ToString());


                        foreach (Control control in CartFlowLayout.Controls)
                        {
                            if (control is btnCart cartItem && cartItem.Name == stock_id_db)
                            {
                                discount_db = data.NumericValues("discount", "pos_cart_items", "stock_id", cartItem.stockID);
                                TextData.totalAmount = data.NumericValues("total_amount", "pos_cart_items", "stock_id", cartItem.stockID);


                                cartItem.customer_name = lbl_customer.Text;
                                cartItem.customer_code = lblCustomerCode.Text;
                                cartItem.discount = discount_db;

                                cartItem.TotalAmount = GetSetData.currency() + Math.Round(TextData.totalAmount, 2).ToString();
                            }
                        }
                    }

                    reader.Close();
                    conn.Close();

                    #endregion
                }


                //***********************************************

                string totalTaxation = "";
                string totalAmount = "";
                string totalDiscount = "";

                GetSetData.query = "select sum(quantity) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                txt_total_qty.Text = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = "select sum(discount) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalDiscount == "")
                {
                    totalDiscount = "0";
                }

                txt_total_discount.Text = Math.Round(double.Parse(totalDiscount), 2).ToString();
                txt_tax.Text = Math.Round(double.Parse(totalDiscount), 2).ToString();


                GetSetData.query = "select sum(tax) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                totalTaxation = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalTaxation == "")
                {
                    totalTaxation = "0";
                }

                txtTaxation.Text = Math.Round(double.Parse(totalTaxation), 2).ToString();


                GetSetData.query = "select sum(total_amount) from pos_cart_items where  (is_return != 'true') and (mac_address = '" + macAddress + "');";
                totalAmount = data.SearchStringValuesFromDb(GetSetData.query);


                if (totalAmount != "" && totalTaxation != "")
                {
                    txt_sub_total.Text = Math.Round(double.Parse(totalAmount) - double.Parse(totalTaxation), 2).ToString();
                }
                else
                {
                    txt_sub_total.Text = "0.00";
                }

                if (totalAmount != "")
                {
                    txtGrandTotal.Text = Math.Round(double.Parse(totalAmount), 2).ToString();
                }
                else
                {
                    txtGrandTotal.Text = "0.00";
                }


                GetSetData.query = "select count(id) from pos_cart_items where (is_return != 'true') and (mac_address = '" + macAddress + "');";
                txt_total_items.Text = data.SearchStringValuesFromDb(GetSetData.query);

                //***********************************************

                txtReturnSubTotal.Text = txt_sub_total.Text + "-";
                txtReturnGrandTotal.Text = txtGrandTotal.Text + "-";
                txtReturnAmountDue.Text = txt_amount_due.Text + "-";

                txtReturnPoints.Text = "-" + txtCustomerPoints.Text;
                txtReturnTax.Text = "-" + txtTaxation.Text;
                txtReturnDiscount.Text = "-" + txt_total_discount.Text;
                txtReturnItems.Text = "-" + txt_total_items.Text;
                txtReturnQuantity.Text = "-" + txt_total_qty.Text;

               
                txt_barcode.Select();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void cartItemsControl()
        {
            calculateGrandTotals();
            //CartFlowLayout.AutoScroll = true;
            //CartFlowLayout.AutoScroll = false;
        }

        private void CartFlowLayout_ControlAdded(object sender, ControlEventArgs e)
        {
            cartItemsControl();
        }

        private void CartFlowLayout_ControlRemoved(object sender, ControlEventArgs e)
        {
            cartItemsControl();
        }

        private void btnTopSellingProducts_Click(object sender, EventArgs e)
        {
            try
            {
                refresh_menu();
                TextData.general_options = "5";
                GetSetData.Permission = "";
                TextData.barcode = "";

                TextData.saved_image = data.UserPermissions("picture_path", "pos_general_settings");

                string query = @"SELECT * from ViewTopSellingProducts;";


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    btnProduct b = new btnProduct();

                    string product_id_db = reader["prod_id"].ToString();
                    string stock_id_db = data.UserPermissions("stock_id", "pos_stock_details", "prod_id", product_id_db);

                    TextData.prod_name = data.UserPermissions("prod_name", "pos_products", "product_id", product_id_db);
                    TextData.image_path = data.UserPermissions("image_path", "pos_products", "product_id", product_id_db);
                    TextData.barcode = data.UserPermissions("item_barcode", "pos_stock_details", "stock_id", stock_id_db);
                    TextData.quantity = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db);
                    TextData.rate = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db);
                    TextData.MarketPrice = data.NumericValues("market_value", "pos_stock_details", "stock_id", stock_id_db);

                  
                    TextData.MarketPrice = ((TextData.rate * TextData.MarketPrice) / 100);
                    TextData.rate = TextData.rate + TextData.MarketPrice;


                    if (TextData.image_path != "nill" && TextData.image_path != "")
                    {
                        b.itemImage = Image.FromFile(TextData.saved_image + TextData.image_path);
                    }

                    // *********************************************************************
                    b.Name = TextData.prod_name;
                    b.ItemsName = TextData.prod_name;
                    b.ItemsBarcode = TextData.barcode;
                    b.ProductId = product_id_db;
                    b.StockId = stock_id_db;

                    b.Price = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();
                    b.Stock = TextData.quantity.ToString();
                    b.Cursor = Cursors.Hand;
                  
                    pnl_list.Controls.Add(b);
                    b.Click += new System.EventHandler(this.Button_Click);
                    b.MouseHover += new System.EventHandler(this.regularItemsMouseHover);
                    b.MouseLeave += new System.EventHandler(this.regularItemsMouseLeave);
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txt_status.Checked)
            {
                //TextData.isReturn = true;
                txt_status.Checked = false;
                btnReturn.FillColor = Color.DodgerBlue;

                txtReturnSubTotal.Visible = false;
                txtReturnGrandTotal.Visible = false;
                txtReturnAmountDue.Visible = false;
                txtReturnPoints.Visible = false;
                txtReturnTax.Visible = false;
                txtReturnDiscount.Visible = false;
                txtReturnItems.Visible = false;
                txtReturnQuantity.Visible = false;

                //************************************

                txt_sub_total.Visible = true;
                txtGrandTotal.Visible = true;
                txt_amount_due.Visible = true;
                txtCustomerPoints.Visible = true;
                txtTaxation.Visible = true;
                txt_total_discount.Visible = true;
                txt_total_items.Visible = true;
                txt_total_qty.Visible = true;

                //fromSecondScreen.instance.isReturn(true);
                //GetSetData.query = "update pos_cart_items set is_return = '' where (mac_address = '" + macAddress +"');";
                //data.insertUpdateCreateOrDelete(GetSetData.query);
            }
            else
            {
                //TextData.isReturn = false;
                txt_status.Checked = true;
                btnReturn.FillColor = Color.Red;

                txtReturnSubTotal.Visible = true;
                txtReturnGrandTotal.Visible = true;
                txtReturnAmountDue.Visible = true;
                txtReturnPoints.Visible = true;
                txtReturnTax.Visible = true;
                txtReturnDiscount.Visible = true;
                txtReturnItems.Visible = true;
                txtReturnQuantity.Visible = true;

                //************************************

                txtReturnSubTotal.Text = txt_sub_total.Text + "-";
                txtReturnGrandTotal.Text = txtGrandTotal.Text + "-";
                txtReturnAmountDue.Text = txt_amount_due.Text + "-";

                txtReturnPoints.Text = "-" + txtCustomerPoints.Text;
                txtReturnTax.Text = "-" + txtTaxation.Text;
                txtReturnDiscount.Text = "-" + txt_total_discount.Text;
                txtReturnItems.Text = "-" + txt_total_items.Text;
                txtReturnQuantity.Text = "-" + txt_total_qty.Text;

                //************************************

                txt_sub_total.Visible = false;
                txtGrandTotal.Visible = false;
                txt_amount_due.Visible = false;
                txtCustomerPoints.Visible = false;
                txtTaxation.Visible = false;
                txt_total_discount.Visible = false;
                txt_total_items.Visible = false;
                txt_total_qty.Visible = false;

                //fromSecondScreen.instance.isReturn(false);
                //GetSetData.query = "update pos_cart_items set is_return = 'true' where (mac_address = '" + macAddress +"');";
                //data.insertUpdateCreateOrDelete(GetSetData.query);
            }

            calculateAmountDue();
            txt_barcode.Select();
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            form_loyal_cus_sales.role_id = role_id;
            form_loyal_cus_sales _obj = new form_loyal_cus_sales();
            _obj.Show();
            this.Dispose();
        }

        private void txt_total_discount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnPayout_Click(object sender, EventArgs e)
        {
            form_payout.user_id = user_id;
            form_payout.role_id = role_id;
            form_payout _obj = new form_payout();
            _obj.ShowDialog();

            autoOpenCashDrawer();

            txt_barcode.Select();
        }

        private void btnMiscItems_Click(object sender, EventArgs e)
        {
            form_misc_items.user_id = user_id;
            form_misc_items.role_id = role_id;
            form_misc_items _obj = new form_misc_items();
            _obj.ShowDialog();

            fun_clear_cart_records();
            clearDataGridView();

            //****************************************************
            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
            string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);


            GetSetData.query = @"select * from pos_cart_items where (mac_address = '" + macAddress + "');";

            data.Connect();
            data.adptr_ = new SqlDataAdapter(GetSetData.query, data.conn_);
            DataTable dt = new DataTable();
            data.adptr_.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                string item_barcode = "";
                string brand = "";
                //string category = "";
                string quantity_db = "";

                item_barcode = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", item["stock_id"].ToString());
                quantity_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "quantity", "pos_stock_details", "stock_id", item["stock_id"].ToString());

                //string category_id_db = data.UserPermissions("category_id", "pos_products", "product_id", item["product_id"].ToString());
                //category = data.UserPermissions("title", "pos_category", "category_id", category_id_db);

                string brand_id_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_id", "pos_products", "product_id", item["product_id"].ToString());
                brand = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_title", "pos_brand", "brand_id", brand_id_db);


                btnCart cartItem = new btnCart();

                cartItem.Name = item["stock_id"].ToString();
                cartItem.productID = item["product_id"].ToString();
                cartItem.stockID = item["stock_id"].ToString();
                cartItem.Brand = brand;
                cartItem.ItemsName = item["product_name"].ToString();
                cartItem.barcode = item_barcode;
                cartItem.availableStock = double.Parse(quantity_db);
                cartItem.note = item_barcode;
                cartItem.Quantity = item["quantity"].ToString();
                cartItem.ChangeQuantity = item["quantity"].ToString();
                //cartItem.categoryID = category_id_db;
                //cartItem.brandID = brand_id_db;
                cartItem.employee = lbl_employee.Text;
                cartItem.customer_name = lbl_customer.Text;
                cartItem.customer_code = lblCustomerCode.Text;
                cartItem.clock_in_id = clock_in_id;
                cartItem.is_return = txt_status.Checked;
                cartItem.macAddress = macAddress;


                cartItem.tax = double.Parse(item["tax"].ToString());
                cartItem.price = (double.Parse(item["total_amount"].ToString()) / double.Parse(item["quantity"].ToString()));
                cartItem.TotalAmount = item["total_amount"].ToString();
                cartItem.discount = double.Parse(item["discount"].ToString());


                //********************

                CartFlowLayout.Controls.Add(cartItem);
                cartItem.Click += new System.EventHandler(this.CartItemButton_Click);
                //*********************************************************

                beep_sound();
            }

            txt_barcode.Select();
        }

        private void btnMinMaxScreen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSwitchUser_Click(object sender, EventArgs e)
        {
            login_form.isSwitchUser = true;
            login_form _obj = new login_form();
            _obj.Show();
            this.Dispose();
        }

        private void lbl_customer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.invoiceCustomerName = lbl_customer.Text;
                TextData.invoiceCustomerPoints = txtCustomerPoints.Text;
                TextData.invoiceDiscount = txt_total_discount.Text;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtCustomerPoints_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.invoiceCustomerName = lbl_customer.Text;
                TextData.invoiceCustomerPoints = txtCustomerPoints.Text;
                TextData.invoiceDiscount = txt_total_discount.Text;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnSetDiscount_Click(object sender, EventArgs e)
        {
            TextData.allDiscounts = true;
            TextData.customerDiscount = false;
            TextData.promotionsDiscount = false;
            TextData.loyalityProgramDiscount = false;
            

            form_set_discounts _obj = new form_set_discounts();
            _obj.ShowDialog();

            calculateAmountDue();
        }


        //private void exportDataToDrive()
        //{
        //    try
        //    {
        //        exportFileToDrive export = new exportFileToDrive();

        //        string enableDisable = "";
        //        string queryEnableDisable = "";

        //        queryEnableDisable = "select inventory_history from pos_onedrive_options;";
        //        enableDisable = data.SearchStringValuesFromDb(queryEnableDisable);

        //        if (enableDisable == "True")
        //        {
        //            string query = @"SELECT pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_category.title as category,  pos_brand.brand_title as brand,
        //                            pos_stock_details.quantity, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as tax, pos_stock_details.qty_alert, 
        //                            pos_stock_details.date_of_expiry as expiry_date
        //                            FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id
        //                            INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id;";

        //            string fileName = "Inventory_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            export.exportDailyInventory(query, @"Inventory\", fileName);
        //        }

        //        //******************************************


        //        #region

        //        queryEnableDisable = "select daily_sales from pos_onedrive_options;";
        //        enableDisable = data.SearchStringValuesFromDb(queryEnableDisable);


        //        if (enableDisable == "True")
        //        {
        //            string query = @"SELECT pos_sales_accounts.date, pos_sales_accounts.billNo as [Receipt No], pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
        //                            pos_sales_details.quantity, pos_sales_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_sales_details.discount as per_item_discount,
        //                            pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount as total_discount, pos_sales_accounts.total_taxation, pos_sales_accounts.amount_due, pos_sales_accounts.paid as cash_amount,
        //                            pos_sales_accounts.credit_card_amount, pos_sales_accounts.paypal_amount as apple_pay, pos_sales_accounts.google_pay_amount as zelle_pay,  
        //                            pos_sales_accounts.check_sale_status as payment_type, pos_sales_accounts.status 
        //                            FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
        //                            pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id  INNER JOIN
        //                            pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
        //                            pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_sales_accounts.date = '" + txt_date.Text + "');";

        //            string fileName = "Sales_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            export.exportDailyInventory(query, @"Sales\", fileName);
        //        }

        //        //******************************************
        //        queryEnableDisable = "select daily_returns from pos_onedrive_options;";
        //        enableDisable = data.SearchStringValuesFromDb(queryEnableDisable);


        //        if (enableDisable == "True")
        //        {
        //            string query = @"SELECT pos_return_accounts.date, pos_return_accounts.billNo as receipt_no, pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
        //                            pos_returns_details.quantity, pos_returns_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_returns_details.discount as per_item_discount,
        //                            pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount as total_discount, pos_return_accounts.total_taxation, pos_return_accounts.amount_due, pos_return_accounts.paid as cash_amount,
        //                            pos_return_accounts.credit_card_amount, pos_return_accounts.paypal_amount as apple_pay, pos_return_accounts.google_pay_amount as zelle_pay,  
        //                            pos_return_accounts.check_sale_status as payment_type, pos_return_accounts.status 
        //                            FROM pos_return_accounts INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
        //                            pos_customers ON pos_return_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id  INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN
        //                            pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_return_accounts.date = '" + txt_date.Text + "');";

        //            string fileName = "Returns_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            export.exportDailyInventory(query, @"Returns\", fileName);
        //        }

        //        //******************************************

        //        queryEnableDisable = "select daily_receivings from pos_onedrive_options;";
        //        enableDisable = data.SearchStringValuesFromDb(queryEnableDisable);

        //        if (enableDisable == "True")
        //        {
        //            string query = @"SELECT pos_purchase.date, pos_purchase.bill_no as receipt_no, pos_purchase.invoice_no, pos_suppliers.full_name, pos_products.prod_name, pos_products.barcode, pos_purchased_items.quantity,
        //                            pos_purchased_items.pur_price as cost_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, 
        //                            pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, 
        //                            pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.freight,
        //                            pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
        //                            FROM pos_purchase INNER JOIN pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN 
        //                            pos_products ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id
        //                            where (pos_purchase.date = '" + txt_date.Text + "');";

        //            string fileName = "Receivings_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            export.exportDailyInventory(query, @"Receivings\", fileName);
        //        }

        //        //******************************************

        //        queryEnableDisable = "select expenses from pos_onedrive_options;";
        //        enableDisable = data.SearchStringValuesFromDb(queryEnableDisable);


        //        if (enableDisable == "True")
        //        {
        //            string query = @"SELECT pos_expense_details.date, pos_expense_details.time, pos_expenses.title, pos_expense_details.net_amount, pos_expense_items.amount, pos_expense_items.remarks
        //                            FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
        //                            where (pos_expense_details.date = '" + txt_date.Text + "');";

        //            string fileName = "Expenses_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            export.exportDailyInventory(query, @"Expenses\", fileName);
        //        }

        //        #endregion
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private void chkChooseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkChooseType.SelectedIndex == 0)
            {
                txt_barcode.PlaceholderText = "Scan item barcode";
            }
            else if (chkChooseType.SelectedIndex == 1)
            {
                txt_barcode.PlaceholderText = "Scan receipt barcode";
            }
        }

        private void form_counter_sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void chkSearchBy_CheckedChanged(object sender, EventArgs e)
        {
            txtProductName.Text = "";
          
            if (chkSearchBy.Checked)
            {
                txtProductName.Visible = true;
                txt_barcode.Visible = false;

                GetSetData.FillComboBoxUsingProcedures(txtProductName, "fillComboBoxCounterSalesItems", "prod_name");
                txtProductName.Select();
            }
            else
            {
                txt_barcode.Visible = true;
                txtProductName.Visible = false;
                txt_barcode.Select();
            }
        }

        private void txtProductName_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private bool GenerateCustomerTicket(string advanceAmount)
        {
            try
            {
                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (is_items_exists_in_cart != 0)
                {
                    TextData.customer_name = lbl_customer.Text;
                    TextData.customerCode = lblCustomerCode.Text;
                    TextData.employee = lbl_employee.Text;
                    TextData.dates = txt_date.Text;
                    TextData.no_of_items = double.Parse(txt_total_items.Text);
                    TextData.total_qty = double.Parse(txt_total_qty.Text);
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                    TextData.total_taxation = double.Parse(txtTaxation.Text);
                    TextData.discount = double.Parse(txt_total_discount.Text);
                    TextData.tax = txt_tax.Text;
                    TextData.totalAmount = double.Parse(txt_amount_due.Text);
                    TextData.credits = 0;
                    TextData.cash = 0;
                    TextData.lastCredit = 0;

                    if (txt_status.Checked == false)
                    {
                        TextData.status = "Sale";
                    }
                    else
                    {
                        TextData.status = "Return";
                    }

                    if (lbl_customer.Text == "")
                    {
                        TextData.customer_name = "nill";

                        GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                        TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                        lblCustomerCode.Text = TextData.customerCode;
                    }

                    if (lbl_employee.Text == "")
                    {
                        TextData.employee = "nill";
                    }

                    if (TextData.comments == "")
                    {
                        TextData.comments = "nill";
                    }

                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                    int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //*****************************************************************************************
                    int employee_id_db = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                    if (TextData.totalAmount != 0)
                    {
                        double balance = 0;
                        balance = TextData.totalAmount - double.Parse(advanceAmount);

                        if (form_ticket_history.ticketNumber == "")
                        {
                            TextData.billNo = auto_generate_hold_items_code();

                            GetSetData.query = @"insert into pos_tickets values ('" + TextData.billNo + "', '" + TextData.dates.ToString() + "' , '" + lbl_time.Text + "' , '" + TextData.no_of_items.ToString() + "' , '" + TextData.total_qty.ToString() + "' , '" + TextData.net_total.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.tax.ToString() + "' , '" + TextData.totalAmount.ToString() + "' ,  '" + TextData.cash.ToString() + "' , '" + advanceAmount + "' , '" + balance.ToString() + "' , 'Pending' , '" + customer_id_db.ToString() + "' , '" + employee_id_db.ToString() + "' , '" + TextData.total_taxation.ToString() + "', '" + macAddress + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                        else
                        {
                            TextData.billNo = form_ticket_history.ticketNumber;

                            GetSetData.query = @"update pos_tickets set date = '" + TextData.dates.ToString() + "' , time = '" + lbl_time.Text + "' , no_of_items = '" + TextData.no_of_items.ToString() + "' , total_qty = '" + TextData.total_qty.ToString() + "' , sub_total = '" + TextData.net_total.ToString() + "' , discount = '" + TextData.discount.ToString() + "' , tax = '" + TextData.tax.ToString() + "' , amount_due = '" + TextData.totalAmount.ToString() + "' ,  paid = '" + TextData.cash.ToString() + "' , advance_amount = '" + advanceAmount + "' , balance = '" + balance.ToString() + "' , customer_id = '" + customer_id_db.ToString() + "' , employee_id = '" + employee_id_db.ToString() + "' , total_taxation = '" + TextData.total_taxation.ToString() + "' where (billNo = '" + TextData.billNo + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }

                        string sales_acc_id_db = data.UserPermissions("sales_acc_id", "pos_tickets", "billNo", TextData.billNo);

                        // *****************************************************************************************

                        GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "')";

                        SqlConnection conn = new SqlConnection(webConfig.con_string);
                        SqlCommand cmd;
                        SqlDataReader reader;

                        cmd = new SqlCommand(GetSetData.query, conn);

                        conn.Open();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string product_id_db = reader["product_id"].ToString();
                            string stock_id_db = reader["stock_id"].ToString();

                            TextData.prod_state = data.UserPermissions("prod_state", "pos_products", "product_id", product_id_db.ToString());
                            double quantity_db = data.NumericValues("quantity", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            TextData.pkg = data.NumericValues("pkg", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            double pur_price_db = data.NumericValues("pur_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            double sale_price_db = data.NumericValues("sale_price", "pos_stock_details", "stock_id", stock_id_db.ToString());
                            // **************************************************************************************

                            TextData.full_pkg = 0;

                            if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                            {
                                TextData.quantity = double.Parse(reader["quantity"].ToString());
                                TextData.full_pkg = TextData.quantity / TextData.pkg;
                            }

                            GetSetData.query = "select sales_id from pos_ticket_details where (sales_acc_id = '" + sales_acc_id_db.ToString() + "') and (prod_id = '" + product_id_db + "') and (stock_id = '" + stock_id_db + "');";
                            string is_already_exist = data.SearchStringValuesFromDb(GetSetData.query);

                            if (is_already_exist == "")
                            {
                                GetSetData.query = @"insert into pos_ticket_details values ('" + reader["quantity"].ToString() + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + reader["discount"].ToString() + "' , '" + reader["total_amount"].ToString() + "'  , '" + reader["tax"].ToString() + "'  , '" + reader["note"].ToString() + "' , '" + sales_acc_id_db.ToString() + "' , '" + product_id_db + "' , '" + stock_id_db + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            else
                            {
                                GetSetData.query = @"update pos_ticket_details set quantity = '" + reader["quantity"].ToString() + "'  , pkg = '" + TextData.pkg.ToString() + "' , full_pkg = '" + TextData.full_pkg.ToString() + "' , discount = '" + reader["discount"].ToString() + "' , Total_price = '" + reader["total_amount"].ToString() + "'  , taxation = '" + reader["tax"].ToString() + "'  , note = '" + reader["note"].ToString() + "' where (sales_id = '" + is_already_exist +"');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            // **************************************************************************************
                        }

                        reader.Close();
                        conn.Close();
                        // *****************************************************************************************

                        GetSetData.query = @"delete from pos_cart_items where (mac_address = '" + macAddress + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //Print Ticket
                        GetSetData.query = @"select default_printer from pos_general_settings;";
                        string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                        if (printer_name != "")
                        {
                            PrintTicketDirectReceipt(printer_name, TextData.billNo);
                        }
                        else
                        {
                            error.errorMessage("Printer not found!");
                            error.ShowDialog();
                        }
                    }

                    fun_clear_cart_records();
                    clearDataGridView();

                    return true;
                }
                else
                {
                    error.errorMessage("Unable to generate ticket. Please try again!");
                    error.ShowDialog();

                    return false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }
        private void btnTicket_Click(object sender, EventArgs e)
        {
            try
            {
                ticketAdvanceAmount.amountDue = txt_amount_due.Text;
                ticketAdvanceAmount _obj = new ticketAdvanceAmount();
                _obj.ShowDialog();

                if (ticketAdvanceAmount.sure == true)
                {
                    TextData.net_total = double.Parse(txtGrandTotal.Text);
                    TextData.total_taxation = double.Parse(txtTaxation.Text);

                    if (TextData.net_total != 0)
                    {
                        GenerateCustomerTicket(ticketAdvanceAmount.advanceAmount);
                    }

                    txt_barcode.Focus();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void setTicketDetailsInCart(string ticketNumber)
        {
            try
            {
                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_tickets", "billNo", ticketNumber);
                string dicount = data.UserPermissions("discount", "pos_tickets", "billNo", ticketNumber);

                txt_total_discount.Text = dicount;


                GetSetData.query = @"select pos_products.prod_name as Items_Name, pos_stock_details.item_barcode as Barcode, pos_ticket_details.quantity as Qty, pos_ticket_details.pkg as PKG, 
                                    pos_ticket_details.full_pkg as Full_PKG, pos_ticket_details.Total_price/pos_ticket_details.quantity as Rate,  pos_ticket_details.discount as 'Discount', 
                                    pos_ticket_details.Total_price as Amount,  pos_ticket_details.quantity * pos_stock_details.market_value as marketPrice, pos_stock_details.quantity as stock,
                                    pos_ticket_details.note,pos_ticket_details.taxation, pos_ticket_details.prod_id, pos_ticket_details.stock_id
                                    from pos_ticket_details inner join pos_tickets on pos_ticket_details.sales_acc_id = pos_tickets.sales_acc_id
                                    inner join pos_customers on pos_tickets.customer_id = pos_customers.customer_id inner join pos_employees on pos_tickets.employee_id = pos_employees.employee_id
                                    inner join pos_products on pos_ticket_details.prod_id = pos_products.product_id inner join pos_stock_details on pos_stock_details.stock_id = pos_ticket_details.stock_id
                                    where pos_tickets.billNo = '" + ticketNumber + "';";

                data.Connect();
                data.adptr_ = new SqlDataAdapter(GetSetData.query, data.conn_);
                DataTable dt = new DataTable();
                data.adptr_.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    string item_barcode = "";
                    string brand = "";
                    //string category = "";
                    string quantity_db = "";

                    item_barcode = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "item_barcode", "pos_stock_details", "stock_id", item["stock_id"].ToString());
                    quantity_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "quantity", "pos_stock_details", "stock_id", item["stock_id"].ToString());

                    //string category_id_db = data.UserPermissions("category_id", "pos_products", "product_id", item["prod_id"].ToString());
                    //category = data.UserPermissions("title", "pos_category", "category_id", category_id_db);

                    string brand_id_db = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_id", "pos_products", "product_id", item["prod_id"].ToString());
                    brand = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_title", "pos_brand", "brand_id", brand_id_db);


                    btnCart cartItem = new btnCart();

                    cartItem.Name = item["stock_id"].ToString();
                    cartItem.productID = item["prod_id"].ToString();
                    cartItem.stockID = item["stock_id"].ToString();
                    cartItem.Brand = brand;
                    cartItem.ItemsName = item["Items_Name"].ToString();
                    cartItem.barcode = item_barcode;
                    cartItem.availableStock = double.Parse(quantity_db);
                    cartItem.note = item_barcode;
                    cartItem.Quantity = item["Qty"].ToString();
                    cartItem.ChangeQuantity = item["Qty"].ToString();
                    //cartItem.categoryID = category_id_db;
                    //cartItem.brandID = brand_id_db;
                    cartItem.employee = lbl_employee.Text;
                    cartItem.customer_name = lbl_customer.Text;
                    cartItem.customer_code = lblCustomerCode.Text;
                    cartItem.clock_in_id = clock_in_id;
                    cartItem.is_return = txt_status.Checked;
                    cartItem.macAddress = macAddress;


                    cartItem.tax = double.Parse(item["taxation"].ToString());
                    cartItem.price = (double.Parse(item["Amount"].ToString()) / double.Parse(item["Qty"].ToString()));
                    cartItem.TotalAmount = item["Amount"].ToString();
                    cartItem.discount = double.Parse(item["Discount"].ToString());


                    //********************

                    CartFlowLayout.Controls.Add(cartItem);
                    cartItem.Click += new System.EventHandler(this.CartItemButton_Click);
                    //*********************************************************                          


                    string customer_id = "";

                    if (lbl_customer.Text != "")
                    {
                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + lbl_customer.Text + "') and (cus_code = '" + lblCustomerCode.Text + "');";
                        customer_id = data.SearchStringValuesFromDb(GetSetData.query);
                    }

                    string isReturn = "";

                    if (txt_status.Checked)
                    {
                        isReturn = "True";
                    }


                    GetSetData.query = "insert into pos_cart_items values ('" + cartItem.ItemsName + "' , '" + cartItem.barcode + "' , '" + cartItem.Quantity + "' , '" + Math.Round(cartItem.price, 2).ToString() + "' , '" + Math.Round(cartItem.tax, 2).ToString() + "' , '" + Math.Round(cartItem.discount, 2).ToString() + "' , '" + Math.Round(double.Parse(cartItem.TotalAmount), 2).ToString() + "' , '" + cartItem.availableStock.ToString() + "' ,  '" + cartItem.note + "' , '" + cartItem.productID + "' , '" + cartItem.stockID + "' , '' , '' , '" + macAddress + "' , '" + txtCustomerPoints.Text + "' , '" + isReturn.ToString() + "' , '" + customer_id + "' , '" + user_id.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                calculateGrandTotals();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }
      
        private void btnTicketHistory_Click(object sender, EventArgs e)
        {
            try
            {
                form_ticket_history.role_id = role_id;
                form_ticket_history _obj = new form_ticket_history();
                _obj.ShowDialog();

                if (form_ticket_history.ticketNumber != "")
                {
                    setTicketDetailsInCart(form_ticket_history.ticketNumber);
                    FillLastCreditsTextBox();
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
