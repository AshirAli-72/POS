using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Login_info.controllers;
using System.Data.SqlClient;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.CounterSalesInfo.CustomControls
{
    public partial class btnCart : UserControl
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

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        //done_form done = new done_form();
        //form_sure_message sure = new form_sure_message();


        public btnCart()
        {
            InitializeComponent();
            Height = 61;
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (Height == 61)
            {
                Height = 177;
                lblAmount.Visible = true;
                //btnExpand.Image = Properties.Resources.uhold_item_red;

                GetSetData.Data = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "show_discount");

                if (GetSetData.Data == "Enabled")
                {
                    txtDiscount.Visible = true;
                }
                else
                {
                    txtDiscount.Visible = false;
                }

                GetSetData.Data = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "price_box");

                if (GetSetData.Data == "Yes")
                {
                    txtEditPrice.Visible = true;
                }
                else
                {
                    txtEditPrice.Visible = false;
                }
            }
            else
            {
                Height = 61;
                lblAmount.Visible = false;
                //btnDashboard.OnPressedState.IconRightImage = Properties.Resources.downArrow;
            }
        }

        public string ItemsName
        {
            get { return lblProductName.Text; }
            set { lblProductName.Text = value; }
        }
        
        public string Quantity
        {
            get { return txtQuantity.Text; }
            
            set 
            {
                txtQuantity.Text = value;
                lblQuantity.Text = value;
            }
        }
        
        public string ChangeQuantity
        {
            get { return txtChangeQuantity.Text; }
            
            set 
            {
                txtChangeQuantity.Text = value;
            }
        }

        public string Discount
        {
            get { return txtDiscount.Text; }
            set { txtDiscount.Text = value; }
        }

        public string EditPrice
        {
            get { return txtEditPrice.Text; }
            set { txtEditPrice.Text = value; }
        }
        public string Brand
        {
            get { return lblBrand.Text; }
            set { lblBrand.Text = value; }
        }
        public double discount { get; set; }
        public double price { get; set; }
        public double availableStock { get; set; }
        public string note { get; set; }
        public string barcode { get; set; }
        public string productID { get; set; }
        public string stockID { get; set; }
        //public string brandID { get; set; }
        //public string categoryID { get; set; }
        public string employee { get; set; }
        public string clock_in_id { get; set; }
        public string customer_name { get; set; }
        public string customer_code { get; set; }
        public bool is_return { get; set; }
        public bool can_delete { get; set; }
        public string date { get; set; }
        public string macAddress { get; set; }

        public double tax { get; set; }
      
        public string TotalAmount
        {
            get { return lblAmount.Text; }

            set 
            { 
                lblAmount.Text = value;

                txtTaxation.Text = GetSetData.currency() + tax.ToString();

                txtAmountWithOutPrice.Text = GetSetData.currency() + (((price * double.Parse(lblQuantity.Text)) - discount) - tax).ToString();
            }

        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtQuantity.Text, e);
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            double quantity = double.Parse(txtQuantity.Text);

            if ((quantity) > 1)
            {
                txtQuantity.Text = (--quantity).ToString();
                txtChangeQuantity.Text = txtQuantity.Text;

                this.OnClick(e);
            }
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            double quantity = double.Parse(txtQuantity.Text);
            txtQuantity.Text = (++quantity).ToString();
            txtChangeQuantity.Text = txtQuantity.Text;

            this.OnClick(e);
        }
       
        private void chkIsReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsReturn.Checked)
            {
                pnlCartItems.FillColor = System.Drawing.Color.LemonChiffon;
                pnlCartItems.FillColor2 = System.Drawing.Color.LemonChiffon;

                txtReturnAmountWithOutPrice.Visible = true;
                txtReturnTaxation.Visible = true;
                lblReturnAmount.Visible = true;

                txtReturnAmountWithOutPrice.Text = "-" + txtAmountWithOutPrice.Text;
                txtReturnTaxation.Text = "-" + txtTaxation.Text;
                lblReturnAmount.Text = "-" + lblAmount.Text;

                //*********************************************************

                GetSetData.query = "update pos_cart_items set is_return = 'true'  where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
            }
            else
            {
                pnlCartItems.FillColor = System.Drawing.Color.WhiteSmoke;
                pnlCartItems.FillColor2 = System.Drawing.Color.WhiteSmoke;
               
                txtReturnAmountWithOutPrice.Visible = false;
                txtReturnTaxation.Visible = false;
                lblReturnAmount.Visible = false;

                //*********************************************************

                GetSetData.query = "update pos_cart_items set is_return = 'false'  where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
            }

            this.OnClick(e);
        }

        private void chkQuantityInPack_CheckedChanged(object sender, EventArgs e)
        {
            if (txtChangeQuantity.Text != "")
            {
                if (double.Parse(txtChangeQuantity.Text) > 0)
                {
                    GetSetData.query = "select pkg from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    double packing = data.SearchNumericValuesDb(GetSetData.query);

                    if (packing > 0)
                    {
                        double currentDiscount = 0;
                        double cart_price_db = 0;
                        double cart_tax_db = 0;
                        TextData.rate = 0;
                        TextData.discount = 0;
                        TextData.quantity = 0;
                        TextData.totalAmount = 0;
                        TextData.tax = "0";

                        //*******************************************************

                        GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        cart_price_db = data.SearchNumericValuesDb(GetSetData.query);

                        GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                        GetSetData.query = "select quantity from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        TextData.quantity = data.SearchNumericValuesDb(GetSetData.query);


                        GetSetData.query = "select discount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        currentDiscount = data.SearchNumericValuesDb(GetSetData.query);

                        GetSetData.query = "select market_value from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        double market_price = data.SearchNumericValuesDb(GetSetData.query);

                        GetSetData.query = "select pkg from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        packing = data.SearchNumericValuesDb(GetSetData.query);

                        if (chkQuantityInPack.Checked)
                        {
                            txtChangeQuantity.Text = (TextData.quantity * packing).ToString();
                        }
                        else
                        {
                            txtChangeQuantity.Text = "1";
                        }



                        //Calculating New Tax
                        TextData.tax = (cart_tax_db / TextData.quantity).ToString();
                        tax = double.Parse(TextData.tax) * double.Parse(txtChangeQuantity.Text);


                        //Calculating New Discount
                        TextData.discount = currentDiscount / TextData.quantity;
                        discount = TextData.discount * double.Parse(txtChangeQuantity.Text);

                        //Calculating Total Amount
                        TextData.totalAmount = cart_price_db * double.Parse(txtChangeQuantity.Text);
                        ChangeQuantity = txtChangeQuantity.Text;
                        Quantity = txtChangeQuantity.Text;


                        TotalAmount = GetSetData.currency() + Math.Round((TextData.totalAmount), 2).ToString();


                        GetSetData.query = "update pos_cart_items set quantity = '" + Quantity + "',  discount = '" + Math.Round(discount, 2).ToString() + "', tax = '" + Math.Round(tax, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.totalAmount, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //*********************************************************

                        this.OnClick(e);
                    }
                }
                else
                {
                    error.errorMessage("Quantity should be greater than 0.");
                    error.ShowDialog();
                }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
            {
                double quantity = double.Parse(txtQuantity.Text);
                double currentDiscount = 0;
               

                //lblAmount.Text = (price * quantity).ToString();

                double cart_quantity_db = 0;
                double cart_price_db = 0;
                double cart_tax_db = 0;
                double cart_discount_db = 0;
                double promotionItemsRemainder = -1;
                double existing_quantity_db = 0;
                TextData.promotionDiscount = 0;
                TextData.rate = 0;
                TextData.discount = 0;
                double number_of_quantity_in_cart = 0;
            

                double quantity_db = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "quantity", "pos_stock_details", "stock_id", stockID);
                TextData.MarketPrice = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "market_value", "pos_stock_details", "stock_id", stockID);
                TextData.rate = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "sale_price", "pos_stock_details", "stock_id", stockID);

                GetSetData.query = "select quantity from pos_cart_items where  (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                cart_quantity_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                cart_price_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select discount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                cart_discount_db = data.SearchNumericValuesDb(GetSetData.query);

                if (txtDiscount.Text == "")
                {
                    cart_discount_db = 0;
                }
                else
                {
                    cart_discount_db = cart_discount_db / cart_quantity_db;
                    cart_discount_db = cart_discount_db * quantity;
                }
               

                TextData.MarketPrice = cart_tax_db / cart_quantity_db;
                TextData.MarketPrice = TextData.MarketPrice * quantity;
                tax = Math.Round(TextData.MarketPrice, 2);


                //*********************************************************
                #region

                //*************************************************************

                string queryCheckPromoItems = @"select pos_promotions.promo_group_id 
                                                    from pos_promotions inner join pos_promo_groups on pos_promo_groups.id = pos_promotions.promo_group_id
                                                    inner join pos_promo_group_items on pos_promo_group_items.promo_group_id = pos_promo_groups.id  
                                                    where (pos_promo_group_items.prod_id = '" + productID.ToString() + "') and (pos_promo_group_items.stock_id = '" + stockID.ToString() + "') and (pos_promotions.status = 'Active') and (pos_promotions.start_date <= '" + date + "') and (pos_promotions.end_date >= '" + date + "');";


                SqlConnection connCheckPromo = new SqlConnection(webConfig.con_string);
                SqlCommand cmdCheckPromo;
                SqlDataReader readerCheckPromo;

                cmdCheckPromo = new SqlCommand(queryCheckPromoItems, connCheckPromo);

                connCheckPromo.Open();
                readerCheckPromo = cmdCheckPromo.ExecuteReader();

                int promo_group_id = 0;


                while (readerCheckPromo.Read())
                {
                    GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + readerCheckPromo["promo_group_id"].ToString() + "') and (status = 'Active') and (start_date <= '" + date + "') and (end_date >= '" + date + "');";
                    promo_group_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (promo_group_id != 0)
                    {
                        break;
                    }
                }

                readerCheckPromo.Close();

                //*************************************************************


                if (promo_group_id != 0)
                {
                    GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + promo_group_id.ToString() + "') and (status = 'Active') and (start_date <= '" + date + "') and (end_date >= '" + date + "');";
                    int promotion_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (promotion_id != 0)
                    {
                        bool is_discount_in_percentage = bool.Parse(data.UserPermissions("is_discount_in_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        double discount = double.Parse(data.UserPermissions("discount", "pos_promotions", "id", promotion_id.ToString()));
                        double discountInPercentage = double.Parse(data.UserPermissions("discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        //double newPrice = double.Parse(data.UserPermissions("discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        double quantityLimit = data.NumericValues("quantity", "pos_promotions", "id", promotion_id.ToString());


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
                                number_of_quantity_in_cart += double.Parse(reader["quantity"].ToString());
                            }
                        }

                        reader.Close();

                        
                        //*************************************************


                        GetSetData.query = "select quantity from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        existing_quantity_db = data.SearchNumericValuesDb(GetSetData.query);

                        number_of_quantity_in_cart = (number_of_quantity_in_cart + quantity) - existing_quantity_db;


                        if (number_of_quantity_in_cart >= quantityLimit)
                        {
                            promotionItemsRemainder = number_of_quantity_in_cart % quantityLimit;

                            if (is_discount_in_percentage)
                            {
                                TextData.promotionDiscount = ((TextData.rate * discountInPercentage) / 100);
                            }
                            else
                            {
                                TextData.promotionDiscount = discount;
                            }
                        }
                        else
                        {
                            TextData.promotionDiscount = cart_discount_db;
                        }
                    }
                    else
                    {
                        discount = 0;
                    }
                }
                else
                {
                    discount = 0;
                }

                #endregion

                //*********************************************************

                if (quantity >= existing_quantity_db) //consider as increment in quantity
                {
                    string isStockLimitSetToZero = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "setStockLimitToZero");

                    if ((quantity <= quantity_db) || (TextData.category == "Services") || (TextData.category == "MISCELLANEOUS") || (is_return == true) || (isStockLimitSetToZero == "No"))
                    {
                        if (promotionItemsRemainder == 0)
                        {
                            TextData.promotionDiscount += cart_discount_db;
                        }
                        else
                        {
                            TextData.promotionDiscount = cart_discount_db;
                        }

                        discount = Math.Round(TextData.promotionDiscount, 2);

                        TextData.rate = cart_price_db * quantity;

                        TotalAmount = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();

                        GetSetData.query = "update pos_cart_items set quantity = '" + quantity.ToString() + "' ,  tax = '" + Math.Round(tax, 2).ToString() + "' , discount = '" + Math.Round(discount, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.rate, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        error.errorMessage("Available stock is '" + quantity_db.ToString() + "'!");
                        error.ShowDialog();

                        txtQuantity.Text = cart_quantity_db.ToString();
                    }
                }
                else //consider as decrement in quantity
                {
                    if (promotionItemsRemainder == 0)
                    {
                        TextData.promotionDiscount = cart_discount_db;
                    }
                    else
                    {
                        TextData.promotionDiscount = cart_discount_db - TextData.promotionDiscount;
                    }
                   
                    discount = Math.Round(TextData.promotionDiscount, 2);

                    TextData.rate = (cart_price_db * quantity) + tax;

                    //TextData.rate = (TextData.rate + TextData.MarketPrice);

                    TotalAmount = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();


                    GetSetData.query = "update pos_cart_items set quantity = '" + quantity.ToString() + "' ,  tax = '" + Math.Round(tax, 2).ToString() + "' , discount = '" + Math.Round(discount, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.rate, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                lblQuantity.Text = txtQuantity.Text;
            }
        }

        private void txtChangeQuantity_KeyDown(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtChangeQuantity.Text, e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtChangeQuantity.Text != "")
                {  
                    if (double.Parse(txtChangeQuantity.Text) > 0)
                    {
                        double currentQuantity = double.Parse(txtChangeQuantity.Text);

                        double currentDiscount = 0;
                        double cart_price_db = 0;
                        double cart_tax_db = 0;
                        double packing = 1;
                        TextData.rate = 0;
                        TextData.discount = 0;
                        TextData.quantity = 0;
                        TextData.totalAmount = 0;
                        TextData.tax = "0";

                        //*******************************************************


                        GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        cart_price_db = data.SearchNumericValuesDb(GetSetData.query);

                        GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                        GetSetData.query = "select quantity from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        TextData.quantity = data.SearchNumericValuesDb(GetSetData.query);


                        GetSetData.query = "select discount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                        currentDiscount = data.SearchNumericValuesDb(GetSetData.query);

                        GetSetData.query = "select market_value from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        double market_price = data.SearchNumericValuesDb(GetSetData.query);


                        if (chkQuantityInPack.Checked)
                        {
                            GetSetData.query = "select pkg from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                            packing = data.SearchNumericValuesDb(GetSetData.query);


                            txtChangeQuantity.Text = (currentQuantity * packing).ToString();
                        }
                        else
                        {
                            txtChangeQuantity.Text = currentQuantity.ToString();
                        }
           

                        //currentQuantity = currentQuantity * packing;
                        //txtChangeQuantity.Text = currentQuantity.ToString();


                        //Calculating New Tax
                        TextData.tax = (cart_tax_db / TextData.quantity).ToString();
                        tax = double.Parse(TextData.tax) * double.Parse(txtChangeQuantity.Text);


                        //Calculating New Discount
                        TextData.discount = currentDiscount / TextData.quantity;
                        discount = TextData.discount * double.Parse(txtChangeQuantity.Text);

                        //Calculating Total Amount
                        TextData.totalAmount = cart_price_db * double.Parse(txtChangeQuantity.Text);
                        ChangeQuantity = txtChangeQuantity.Text;
                        Quantity = txtChangeQuantity.Text;


                        TotalAmount = GetSetData.currency() + Math.Round((TextData.totalAmount), 2).ToString();


                        GetSetData.query = "update pos_cart_items set quantity = '" + Quantity + "',  discount = '" + Math.Round(discount, 2).ToString() + "', tax = '" + Math.Round(tax, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.totalAmount, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //*********************************************************

                        this.OnClick(e);
                    }
                    else
                    {
                        error.errorMessage("Quantity should be greater than 0.");
                        error.ShowDialog();
                    }
                }
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscount.Text, e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtDiscount.Text != "")
                {
                    double currentDiscount = double.Parse(txtDiscount.Text);

                    double cart_price_db = 0;
                    double cart_tax_db = 0;
                    TextData.rate = 0;
                    TextData.discount = 0;
                    TextData.totalAmount = 0;


                    GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                    cart_price_db = data.SearchNumericValuesDb(GetSetData.query);
                    
                    GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                    cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                    discount = currentDiscount * double.Parse(txtQuantity.Text);

                    TextData.rate = cart_price_db * double.Parse(txtQuantity.Text);
                    TextData.totalAmount = cart_price_db * double.Parse(txtQuantity.Text);

                    if (txtDiscountInPercentage.Checked)
                    {
                        discount = (currentDiscount * (TextData.rate - cart_tax_db)) / 100;
                    }

                    //customer_discount = ((double.Parse(totalAmountDb) - double.Parse(totalTaxDb)) * customer_discount) / 100;

                    //discount += customer_discount;

                    TotalAmount = GetSetData.currency() + Math.Round((TextData.totalAmount), 2).ToString();


                    GetSetData.query = "update pos_cart_items set  discount = '" + Math.Round(discount, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.totalAmount, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //*********************************************************

                    this.OnClick(e);
                }
            }
        }

        private void txtEditPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtEditPrice.Text, e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtEditPrice.Text != "")
                {
                    double currentPrice = double.Parse(txtEditPrice.Text);
                    double newTax = 0;
                    double cartTax = 0;
                    TextData.rate = 0;
                    TextData.discount = 0;

                    if (txtDiscount.Text != "")
                    {
                        TextData.discount = double.Parse(txtDiscount.Text);
                    }
                    
                    GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                    cartTax = data.SearchNumericValuesDb(GetSetData.query);
                    
                    
                    GetSetData.query = "select market_value from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    double market_price = data.SearchNumericValuesDb(GetSetData.query);
                  

                    newTax = ((currentPrice * market_price) / 100);
                    price = (currentPrice + newTax) / double.Parse(txtQuantity.Text);


                    tax = newTax;
                    TextData.rate = currentPrice + newTax;

                    discount = TextData.discount * double.Parse(txtQuantity.Text);

                    if (txtDiscountInPercentage.Checked)
                    {
                        discount = (TextData.discount * (TextData.rate - tax)) / 100;
                    }


                    TotalAmount = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();


                    GetSetData.query = "update pos_cart_items set tax = '" + tax.ToString() +"', discount = '" + Math.Round(discount, 2).ToString() + "', price = '" + price.ToString() +"' , total_amount = '" + Math.Round(TextData.rate, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //*********************************************************
                    
                    this.OnClick(e);
                }
            }
        }
        
        //private void txtEditPrice_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    data.NumericValuesOnly(txtEditPrice.Text, e);

        //    if (e.KeyChar == (char)Keys.Enter)
        //    {
        //        if (txtEditPrice.Text != "")
        //        {
        //            double currentPrice = double.Parse(txtEditPrice.Text);
        //            double newTax = 0;
        //            double cartTax = 0;
        //            TextData.rate = 0;
        //            TextData.discount = 0;

        //            if (txtDiscount.Text != "")
        //            {
        //                TextData.discount = double.Parse(txtDiscount.Text);
        //            }
                    
        //            GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
        //            cartTax = data.SearchNumericValuesDb(GetSetData.query);
                    
                    
        //            GetSetData.query = "select market_value from pos_stock_details where (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
        //            double market_price = data.SearchNumericValuesDb(GetSetData.query);
                  

        //            newTax = ((currentPrice * market_price) / 100);
        //            price = currentPrice + newTax;


        //            tax = newTax * double.Parse(txtQuantity.Text);
        //            TextData.rate = price * double.Parse(txtQuantity.Text);

        //            discount = TextData.discount * double.Parse(txtQuantity.Text);

        //            if (txtDiscountInPercentage.Checked)
        //            {
        //                discount = (TextData.discount * (TextData.rate - tax)) / 100;
        //            }


        //            TotalAmount = GetSetData.currency() + Math.Round(TextData.rate, 2).ToString();


        //            GetSetData.query = "update pos_cart_items set tax = '" + tax.ToString() +"', discount = '" + Math.Round(discount, 2).ToString() + "', price = '" + price.ToString() +"' , total_amount = '" + Math.Round(TextData.rate, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);

        //            //*********************************************************
                    
        //            this.OnClick(e);
        //        }
        //    }
        //}

        private void btnTaxFree_Click(object sender, EventArgs e)
        {
            try
            {
                double cart_quantity_db = 0;
                double cart_price_db = 0;
                double cart_tax_db = 0;
                double cart_discount_db = 0;
                double cart_total_amount_db = 0;

             
                GetSetData.query = "select quantity from pos_cart_items where  (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_quantity_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                cart_price_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select discount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_discount_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select total_amount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_total_amount_db = data.SearchNumericValuesDb(GetSetData.query);


                if (cart_tax_db != 0)
                {
                    discount = cart_discount_db - tax;

                    if (discount < 0)
                    {
                        discount = 0;
                    }

                    string total_amount = Math.Round((cart_total_amount_db - tax) - cart_discount_db, 2).ToString();

                    price = double.Parse(total_amount) / cart_quantity_db;


                    GetSetData.query = "update pos_cart_items set price = '" + Math.Round(price, 2).ToString() + "' , discount = '" + Math.Round(discount, 2).ToString() + "' , total_amount = '" + total_amount + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    tax = 0;

                    TotalAmount = total_amount;


                    GetSetData.query = "update pos_cart_items set tax = '0'  where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                }

                this.OnClick(e);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void SetCartItems()
        {
            try
            {
                double cart_quantity_db = 0;
                double cart_price_db = 0;
                double cart_tax_db = 0;
                double cart_discount_db = 0;
                double promotionItemsRemainder = -1;
                TextData.promotionDiscount = 0;
                TextData.rate = 0;
                TextData.discount = 0;
                double number_of_quantity_in_cart = 0;


                double quantity_db = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "quantity", "pos_stock_details", "stock_id", stockID);
                TextData.MarketPrice = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "market_value", "pos_stock_details", "stock_id", stockID);
                TextData.rate = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "sale_price", "pos_stock_details", "stock_id", stockID);

                TextData.rate += TextData.MarketPrice;


                GetSetData.query = "select quantity from pos_cart_items where  (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_quantity_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select price from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_price_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select tax from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_tax_db = data.SearchNumericValuesDb(GetSetData.query);


                GetSetData.query = "select discount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                cart_discount_db = data.SearchNumericValuesDb(GetSetData.query);


                // *****************************************************************************************

                GetSetData.query = @"delete from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '" + macAddress + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                // *****************************************************************************************

                TextData.MarketPrice = cart_tax_db / cart_quantity_db;
                TextData.MarketPrice = TextData.MarketPrice * cart_quantity_db;
                tax = Math.Round(TextData.MarketPrice, 2);


                //*********************************************************
                #region

                //*************************************************************
                string queryCheckPromoItems = @"select pos_promotions.promo_group_id 
                                                    from pos_promotions inner join pos_promo_groups on pos_promo_groups.id = pos_promotions.promo_group_id
                                                    inner join pos_promo_group_items on pos_promo_group_items.promo_group_id = pos_promo_groups.id  
                                                    where (pos_promo_group_items.prod_id = '" + productID.ToString() + "') and (pos_promo_group_items.stock_id = '" + stockID.ToString() + "') and (pos_promotions.status = 'Active') and (pos_promotions.start_date <= '" + date + "') and (pos_promotions.end_date >= '" + date + "');";


                SqlConnection connCheckPromo = new SqlConnection(webConfig.con_string);
                SqlCommand cmdCheckPromo;
                SqlDataReader readerCheckPromo;

                cmdCheckPromo = new SqlCommand(queryCheckPromoItems, connCheckPromo);

                connCheckPromo.Open();
                readerCheckPromo = cmdCheckPromo.ExecuteReader();

                int promo_group_id = 0;


                while (readerCheckPromo.Read())
                {
                    GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + readerCheckPromo["promo_group_id"].ToString() + "') and (status = 'Active') and (start_date <= '" + date + "') and (end_date >= '" + date + "');";
                    promo_group_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (promo_group_id != 0)
                    {
                        break;
                    }
                }

                readerCheckPromo.Close();

                //*************************************************************


                if (promo_group_id != 0)
                {
                    GetSetData.query = "select id from pos_promotions where (promo_group_id = '" + promo_group_id.ToString() + "') and (status = 'Active') and (start_date <= '" + date + "') and (end_date >= '" + date + "');";
                    int promotion_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (promotion_id != 0)
                    {
                        bool is_discount_in_percentage = bool.Parse(GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "is_discount_in_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        double promo_discount = double.Parse(GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "discount", "pos_promotions", "id", promotion_id.ToString()));
                        double discountInPercentage = double.Parse(GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        //double newPrice = double.Parse(GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "discount_percentage", "pos_promotions", "id", promotion_id.ToString()));
                        double quantityLimit = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "quantity", "pos_promotions", "id", promotion_id.ToString());


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
                                number_of_quantity_in_cart += double.Parse(reader["quantity"].ToString());
                            }
                        }

                        reader.Close();


                        //*************************************************

                        if (number_of_quantity_in_cart >= quantityLimit)
                        {
                            promotionItemsRemainder = number_of_quantity_in_cart % quantityLimit;

                            if (is_discount_in_percentage)
                            {
                                TextData.promotionDiscount = ((TextData.rate * discountInPercentage) / 100);
                            }
                            else
                            {
                                TextData.promotionDiscount = promo_discount;
                            }
                        }
                        else
                        {
                            TextData.promotionDiscount = 0;
                        }
                    }
                    else
                    {
                        discount = 0;
                    }
                }
                else
                {
                    discount = 0;
                }

                #endregion

                //*********************************************************

                string isStockLimitSetToZero = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "setStockLimitToZero");


                if ((cart_quantity_db <= quantity_db) || (TextData.category == "Services") || (TextData.category == "MISCELLANEOUS") || (is_return == true) || (isStockLimitSetToZero == "No"))
                {
                    if (promotionItemsRemainder == 0)
                    {
                        TextData.promotionDiscount += cart_discount_db;
                    }
                    else
                    {
                        TextData.promotionDiscount = 0;
                    }


                    discount = Math.Round(TextData.promotionDiscount, 2);

                    TextData.rate = cart_price_db * cart_quantity_db;

                    TotalAmount = GetSetData.currency() + Math.Round(TextData.rate - (discount), 2).ToString();

                    GetSetData.query = "update pos_cart_items set quantity = '" + cart_quantity_db.ToString() + "' ,  tax = '" + Math.Round(tax, 2).ToString() + "' , discount = '" + Math.Round(discount, 2).ToString() + "' , total_amount = '" + Math.Round(TextData.rate, 2).ToString() + "' where (product_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Available stock is '" + quantity_db.ToString() + "'!");
                    error.ShowDialog();

                    txtQuantity.Text = cart_quantity_db.ToString();
                }

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
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.dates = DateTime.Now.ToShortDateString();
                TextData.LastCredits_db = 0;


                if (employee == "")
                {
                    employee = "nill";
                }

                int customer_id_db = 0;

                //*****************************************************************************************
                int employee_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "employee_id", "pos_employees", "full_name", employee);
               
                if (customer_name == "" && customer_code == "")
                {
                    customer_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "customer_id", "pos_customers", "full_name", "nill");
                }
                else
                {
                    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + customer_name + "') and (cus_code = '" + customer_code + "');";
                    customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                }

                TextData.prod_state = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "prod_state", "pos_products", "product_id", productID);
                TextData.pkg = GetSetData.ProcedureGetNumericValues("ProcedureGetStringValues", "pkg", "pos_stock_details", "stock_id", stockID);
                double pur_price_db = GetSetData.ProcedureGetNumericValues("ProcedureGetStringValues", "pur_price", "pos_stock_details", "stock_id", stockID);
                double wholeSalePrice_db = GetSetData.ProcedureGetNumericValues("ProcedureGetStringValues", "whole_sale_price", "pos_stock_details", "stock_id", stockID);

                GetSetData.query = "select total_amount from pos_cart_items where (product_id = '" + productID + "') and (stock_id = '" + stockID + "') and (mac_address = '"+ macAddress + "');";
                string totalAmount_db = data.SearchStringValuesFromDb(GetSetData.query);


                TextData.full_pkg = 0;

                //if (TextData.prod_state == "carton" || TextData.prod_state == "bag" || TextData.prod_state == "Liters")
                //{
                //    TextData.quantity = double.Parse(txtQuantity.Text);
                //    TextData.full_pkg = TextData.quantity / TextData.pkg;
                //}

                TextData.total_pur_price = pur_price_db * double.Parse(txtQuantity.Text.ToString());
                TextData.wholeSale = wholeSalePrice_db * double.Parse(txtQuantity.Text.ToString());


                // **************************************************************************************
                GetSetData.query = @"insert into pos_void_orders values ('" + TextData.dates + "' , '" + txtQuantity.Text + "'  , '" + TextData.pkg.ToString() + "' , '" + TextData.full_pkg.ToString() + "' , '" + discount.ToString() + "', '" + TextData.total_pur_price.ToString() + "', '" + totalAmount_db + "', '" + tax.ToString() + "' , '" + TextData.wholeSale.ToString() + "', '" + note + "', '" + productID + "', '" + employee_id_db.ToString() + "', '" + customer_id_db.ToString() + "', '" + clock_in_id + "', '" + DateTime.Now.ToShortTimeString() +"');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                // **************************************************************************************

                SetCartItems();

                // **************************************************************************************

                this.Dispose();

                this.OnClick(e);
            }
            catch (Exception es)
            {
                //error.errorMessage("Please select the row first!");
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }
    }
}
