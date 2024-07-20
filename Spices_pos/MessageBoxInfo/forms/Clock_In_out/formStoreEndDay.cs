using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using System.Data.SqlClient;
using RefereningMaterial;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.Clock_In
{
    public partial class formStoreEndDay : Form
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

        public formStoreEndDay()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        public static string  clock_id_id = "";
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);

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
                //***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //savebutton.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //update_button.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //btnExit.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            private void fillComboBoxToUsers()
        {
            GetSetData.FillComboBoxUsingProcedures(txtToUser, "fillComboBoxUsers", "name");
        }

        private void fillAddProductsFormTextBoxes()
        {   
            txtCashReceived.Text = TextData.cashReceived;
            txtShortAmount.Text = TextData.totalExpenses;
            txtOpeningCash.Text = TextData.openingAmount;
            txtSalesAmount.Text = TextData.cashAmount;
            txtCreditCardAmount.Text = TextData.creditCard;
            txtApplyPayAmount.Text = TextData.applePay;
            txtGooglePayAmount.Text = TextData.googlePay;
            txtMiscItems.Text = TextData.miscSales;
            txtSalesReturns.Text = TextData.totalReturns;
            txtTotalDiscount.Text = TextData.totalDiscount;
            txtTotalTaxation.Text = TextData.totalTaxation;
            txtVoidOrders.Text = TextData.totalVoidOrders;
            txtNoSale.Text = TextData.totalNoSale;
            txtPayout.Text = TextData.totalPayout;
            txtExpectedAmount.Text = TextData.expectedAmount;
            txtTotalTickets.Text = TextData.totalTickets;
            txtBalance.Text = TextData.balance.ToString();

            //**************************************************************

            txtDate.Text = data.UserPermissions("date", "pos_store_day_end", "id", TextData.clockOut_id);
            txtRemarks.Text = data.UserPermissions("total100s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt100.Text = data.UserPermissions("total100s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt50.Text = data.UserPermissions("total50s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt20.Text = data.UserPermissions("total20s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt10.Text = data.UserPermissions("total10s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt5.Text = data.UserPermissions("total5s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt2.Text = data.UserPermissions("total2s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt1.Text = data.UserPermissions("total1s", "pos_store_day_end", "id", TextData.clockOut_id);
            txt1c.Text = data.UserPermissions("total1c", "pos_store_day_end", "id", TextData.clockOut_id);
            txt5c.Text = data.UserPermissions("total5c", "pos_store_day_end", "id", TextData.clockOut_id);
            txt10c.Text = data.UserPermissions("total10c", "pos_store_day_end", "id", TextData.clockOut_id);
            txt25c.Text = data.UserPermissions("total25c", "pos_store_day_end", "id", TextData.clockOut_id);

        }

        
        //private bool fillClockInValueInForm()
        //{
        //    //fillComboBoxToUsers();

        //    try
        //    {
        //        txtDate.Text = DateTime.Now.ToLongDateString();
        //        double openingCash = 0;
        //        double totalSales = 0;
        //        double creditCartAomount = 0;
        //        double applePayAmount = 0;
        //        double googlePayAmount = 0;
        //        double miscItemsSales = 0;
        //        double totalDiscount = 0;
        //        double totalTaxation = 0;
        //        double totalSaleReturns = 0;
        //        double totalVoidOrders = 0;
        //        double expected_amount = 0;
        //        double cash_amount_received = 0;
        //        double balance = 0;
        //        double shortage_amount = 0;
        //        double no_sales = 0;
        //        double payout = 0;
        //        double totalTickets = 0;


        //        GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
        //        string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


        //        if (singleAuthorityClosing == "No")
        //        {

        //            GetSetData.query = @"select count(id) from pos_clock_in  where (status = '-1') or (status = '0');";
        //            int countTotalClockedInUsers = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //            if (countTotalClockedInUsers > 0)
        //            {
        //                error.errorMessage("Please make sure to clock-out all the employees.");
        //                error.ShowDialog();

        //                this.Close();

        //                return false;
        //            }
        //        }


        //        if (singleAuthorityClosing == "No")
        //        {
        //            GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
        //                                    pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
        //                                    pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
        //                                    FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
        //                                    where (status = '-2');";
        //        }
        //        else
        //        {
        //            GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
        //                                pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
        //                                pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
        //                                FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
        //                                where (status = '-1') or (status = '-2') or (status = '0');";
        //        }

        //        SqlConnection conn = new SqlConnection(webConfig.con_string);
        //        SqlCommand cmd;
        //        SqlDataReader reader;

        //        cmd = new SqlCommand(GetSetData.query, conn);

        //        conn.Open();
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            GetSetData.query = @"select id from pos_clock_out  where (clock_in_id = '"+ reader["clock_in_id"].ToString() + "');";
        //            double isAlreadyClockedOut = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //            if (isAlreadyClockedOut == 0)
        //            {
        //                string start_date = reader["start_date"].ToString();
        //                string start_time = reader["start_time"].ToString();


        //                DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
        //                DateTime endTime = DateTime.Today + DateTime.Parse(txtEndTime.Text).TimeOfDay;

        //                // Calculate duration
        //                TimeSpan duration = endTime - startTime;


        //                GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + txtEndTime.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + reader["user_id"].ToString() + "', '" + reader["user_id"].ToString() + "', '" + reader["clock_in_id"].ToString() + "', '0', '0', '0');";
        //                data.insertUpdateCreateOrDelete(GetSetData.query);
        //            }


        //            //openingCash += double.Parse(reader["opening_cash"].ToString());
        //            //totalSales += double.Parse(reader["total_sales"].ToString());
        //            //totalSaleReturns += double.Parse(reader["total_return_amount"].ToString());
        //            //totalVoidOrders += double.Parse(reader["total_void_orders"].ToString());
        //            //shortage_amount += double.Parse(reader["shortage_amount"].ToString());
        //            //no_sales += double.Parse(reader["no_sales"].ToString());
        //            //payout += double.Parse(reader["payout"].ToString());

        //            expected_amount += double.Parse(reader["expected_amount"].ToString());
        //            cash_amount_received += double.Parse(reader["cash_amount_received"].ToString());
        //            balance += double.Parse(reader["balance"].ToString());

        //            //**********************************************

        //            totalSales += data.NumericValues("round(sum(paid), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            totalSaleReturns += data.NumericValues("round(sum(paid), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            totalVoidOrders += data.NumericValues("round(sum(Total_price), 2)", "pos_void_orders", "clock_in_id", reader["clock_in_id"].ToString());
        //            no_sales += data.NumericValues("round(sum(Total_price), 2)", "pos_no_sale", "clock_in_id", reader["clock_in_id"].ToString());
        //            payout += data.NumericValues("round(sum(amount), 2)", "pos_payout", "clock_in_id", reader["clock_in_id"].ToString());


        //            GetSetData.query = "select sum(amount) from pos_expense_details inner join pos_expense_items on pos_expense_items.expense_id = pos_expense_details.expense_id where (pos_expense_details.clock_in_id = '" + reader["clock_in_id"].ToString() + "');";
        //            string total_expenses = data.SearchStringValuesFromDb(GetSetData.query);

        //            if (total_expenses == "")
        //            {
        //                total_expenses = "0";
        //            }

        //            shortage_amount += double.Parse(total_expenses);

        //            //**********************************************

        //            double creditCardSales = data.NumericValues("round(sum(credit_card_amount), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            double creditCardReturn = data.NumericValues("round(sum(credit_card_amount), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());

        //            creditCartAomount += (creditCardSales - creditCardReturn);
                    
        //            //**********************************************

        //            double applePaySales = data.NumericValues("round(sum(paypal_amount), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            double applePayReturn = data.NumericValues("round(sum(paypal_amount), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());


        //            applePayAmount += (applePaySales - applePayReturn);  

        //            //**********************************************

        //            double googlePaySales = data.NumericValues("round(sum(paypal_amount), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            double googlePayReturn = data.NumericValues("round(sum(paypal_amount), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());


        //            googlePayAmount += (googlePaySales - googlePayReturn);

        //            //**********************************************

        //            GetSetData.query = @"SELECT round(sum(pos_sales_accounts.amount_due), 2) FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
        //                                INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN
        //                                pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
        //                                pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id
        //                                WHERE (pos_category.title = 'MISCELLANEOUS') and (pos_sales_accounts.clock_in_id = '" + reader["clock_in_id"].ToString() + "');";

        //            string miscItems = data.SearchStringValuesFromDb(GetSetData.query);

        //            if (miscItems != "" && miscItems != "NULL")
        //            {
        //                miscItemsSales += double.Parse(miscItems);
        //            }

                   
        //            //**********************************************

        //            double discountSales = data.NumericValues("round(sum(discount), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            double discountReturn = data.NumericValues("round(sum(discount), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());


        //            totalDiscount += (discountSales - discountReturn);
                    
        //            //**********************************************

        //            double taxationSales = data.NumericValues("round(sum(total_taxation), 2)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());
        //            double taxationReturn = data.NumericValues("round(sum(total_taxation), 2)", "pos_return_accounts", "clock_in_id", reader["clock_in_id"].ToString());


        //            totalTaxation += (taxationSales - taxationReturn);
                    
        //            //**********************************************

        //            totalTickets += data.NumericValues("count(customer_id)", "pos_sales_accounts", "clock_in_id", reader["clock_in_id"].ToString());

                 
        //            expected_amount = (shortage_amount + totalSales - (totalSaleReturns + payout));
        //        }
        //        //*************************************************************

        //        reader.Close();



        //        GetSetData.query = @"SELECT SUM(pc.opening_amount) AS total_opening_amount FROM pos_counter pc WHERE pc.id IN (SELECT DISTINCT pci.counter_id FROM pos_clock_in pci WHERE pci.status IN ('-1', '0', '-2'));";
        //        string totalCounterOpeningAmount = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (totalCounterOpeningAmount == "")
        //        {
        //            totalCounterOpeningAmount = "0";
        //        }

        //        //txtOpeningCash.Text = openingCash.ToString();
        //        txtOpeningCash.Text = totalCounterOpeningAmount;
        //        txtSalesAmount.Text = totalSales.ToString();
        //        txtCreditCardAmount.Text = creditCartAomount.ToString();
        //        txtApplyPayAmount.Text = applePayAmount.ToString();
        //        txtGooglePayAmount.Text = googlePayAmount.ToString();
        //        txtMiscItems.Text = miscItemsSales.ToString();
        //        txtSalesReturns.Text = totalSaleReturns.ToString();
        //        txtTotalDiscount.Text = totalDiscount.ToString();
        //        txtTotalTaxation.Text = totalTaxation.ToString();
        //        txtVoidOrders.Text = totalVoidOrders.ToString();
        //        txtNoSale.Text = no_sales.ToString();
        //        txtPayout.Text = payout.ToString();
        //        txtTotalTickets.Text = totalTickets.ToString();
        //        txtBalance.Text = balance.ToString();



        //        txtExpectedAmount.Text = expected_amount.ToString();

        //        return true;

        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);

        //        return false;
        //    }
        //}
        
        private bool fillClockInValueInForm()
        {
            //fillComboBoxToUsers();

            try
            {
                double openingCash = 0;
                double totalSales = 0;
                double creditCartAomount = 0;
                double applePayAmount = 0;
                double googlePayAmount = 0;
                double miscItemsSales = 0;
                double totalDiscount = 0;
                double totalTaxation = 0;
                double totalSaleReturns = 0;
                double totalVoidOrders = 0;
                double expected_amount = 0;
                double cash_amount_received = 0;
                double balance = 0;
                double shortage_amount = 0;
                double no_sales = 0;
                double payout = 0;
                double totalTickets = 0;


                GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
                string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


                if (singleAuthorityClosing == "No")
                {

                    GetSetData.query = @"select count(id) from pos_clock_in  where (status = '-1') or (status = '0');";
                    int countTotalClockedInUsers = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (countTotalClockedInUsers > 0)
                    {
                        error.errorMessage("Please make sure to clock-out all the employees.");
                        error.ShowDialog();

                        this.Close();

                        return false;
                    }
                }


                if (singleAuthorityClosing == "No")
                {
                    GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
                                            pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
                                            pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
                                            FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
                                            where (status = '-2');";
                }
                else
                {
                    GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
                                        pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
                                        pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
                                        FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
                                        where (status = '-1') or (status = '-2') or (status = '0');";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GetSetData.query = @"select id from pos_clock_out  where (clock_in_id = '"+ reader["clock_in_id"].ToString() + "');";
                    double isAlreadyClockedOut = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (isAlreadyClockedOut == 0)
                    {
                        string start_date = reader["start_date"].ToString();
                        string start_time = reader["start_time"].ToString();


                        DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
                        DateTime endTime = DateTime.Today + DateTime.Parse(txtEndTime.Text).TimeOfDay;

                        // Calculate duration
                        TimeSpan duration = endTime - startTime;


                        GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + txtEndTime.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + reader["user_id"].ToString() + "', '" + reader["user_id"].ToString() + "', '" + reader["clock_in_id"].ToString() + "', '0', '0', '0', '" + txt100.Text + "', '" + txt50.Text + "', '" + txt20.Text + "', '" + txt10.Text + "', '" + txt5.Text + "', '" + txt2.Text + "', '" + txt1.Text + "', '" + txt1c.Text + "', '" + txt5c.Text + "', '" + txt10c.Text + "', '" + txt25c.Text + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    //**********************************************

                    totalSales = data.NumericValues("round(sum(paid), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    totalSaleReturns = data.NumericValues("round(sum(paid), 2)", "pos_return_accounts", "date", txtDate.Text);
                    totalVoidOrders = data.NumericValues("round(sum(Total_price), 2)", "pos_void_orders", "date", txtDate.Text);
                    no_sales = data.NumericValues("round(sum(Total_price), 2)", "pos_no_sale", "date", txtDate.Text);
                    payout = data.NumericValues("round(sum(amount), 2)", "pos_payout", "date", txtDate.Text);


                    GetSetData.query = "select sum(amount) from pos_expense_details inner join pos_expense_items on pos_expense_items.expense_id = pos_expense_details.expense_id where (pos_expense_details.date = '" + txtDate.Text + "');";
                    string total_expenses = data.SearchStringValuesFromDb(GetSetData.query);

                    if (total_expenses == "")
                    {
                        total_expenses = "0";
                    }

                    shortage_amount = double.Parse(total_expenses);

                    //**********************************************

                    double creditCardSales = data.NumericValues("round(sum(credit_card_amount), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    double creditCardReturn = data.NumericValues("round(sum(credit_card_amount), 2)", "pos_return_accounts", "date", txtDate.Text);

                    creditCartAomount = (creditCardSales - creditCardReturn);
                    
                    //**********************************************

                    double applePaySales = data.NumericValues("round(sum(paypal_amount), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    double applePayReturn = data.NumericValues("round(sum(paypal_amount), 2)", "pos_return_accounts", "date", txtDate.Text);


                    applePayAmount = (applePaySales - applePayReturn);  

                    //**********************************************

                    double googlePaySales = data.NumericValues("round(sum(paypal_amount), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    double googlePayReturn = data.NumericValues("round(sum(paypal_amount), 2)", "pos_return_accounts", "date", txtDate.Text);


                    googlePayAmount = (googlePaySales - googlePayReturn);

                    //**********************************************

                    GetSetData.query = @"SELECT round(sum(pos_sales_accounts.amount_due), 2) FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                        pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                        pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id
                                        WHERE (pos_category.title = 'MISCELLANEOUS') and (pos_sales_accounts.date = '" + txtDate.Text + "');";

                    string miscItems = data.SearchStringValuesFromDb(GetSetData.query);

                    if (miscItems != "" && miscItems != "NULL")
                    {
                        miscItemsSales = double.Parse(miscItems);
                    }

                   
                    //**********************************************

                    double discountSales = data.NumericValues("round(sum(discount), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    double discountReturn = data.NumericValues("round(sum(discount), 2)", "pos_return_accounts", "date", txtDate.Text);


                    totalDiscount = (discountSales - discountReturn);
                    
                    //**********************************************

                    double taxationSales = data.NumericValues("round(sum(total_taxation), 2)", "pos_sales_accounts", "date", txtDate.Text);
                    double taxationReturn = data.NumericValues("round(sum(total_taxation), 2)", "pos_return_accounts", "date", txtDate.Text);


                    totalTaxation = (taxationSales - taxationReturn);
                    
                    //**********************************************

                    totalTickets = data.NumericValues("count(customer_id)", "pos_sales_accounts", "date", txtDate.Text);

                 
                    expected_amount = (shortage_amount + totalSales - (totalSaleReturns + payout));
                }
                //*************************************************************

                reader.Close();


                GetSetData.query = @"SELECT SUM(pc.opening_amount) AS total_opening_amount FROM pos_counter pc WHERE pc.id IN (SELECT DISTINCT pci.counter_id FROM pos_clock_in pci WHERE pci.status IN ('-1', '0', '-2'));";
                string totalCounterOpeningAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalCounterOpeningAmount == "")
                {
                    totalCounterOpeningAmount = "0";
                }

                //txtOpeningCash.Text = openingCash.ToString();
                txtOpeningCash.Text = totalCounterOpeningAmount;
                txtSalesAmount.Text = (totalSales - (totalSaleReturns + payout)).ToString();
                txtCreditCardAmount.Text = creditCartAomount.ToString();
                txtApplyPayAmount.Text = applePayAmount.ToString();
                txtGooglePayAmount.Text = googlePayAmount.ToString();
                txtMiscItems.Text = miscItemsSales.ToString();
                txtSalesReturns.Text = totalSaleReturns.ToString();
                txtTotalDiscount.Text = totalDiscount.ToString();
                txtTotalTaxation.Text = totalTaxation.ToString();
                txtVoidOrders.Text = totalVoidOrders.ToString();
                txtNoSale.Text = no_sales.ToString();
                txtPayout.Text = payout.ToString();
                txtTotalTickets.Text = totalTickets.ToString();
                txtBalance.Text = balance.ToString();



                txtExpectedAmount.Text = expected_amount.ToString();

                return true;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

                return false;
            }
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                FormNamelabel.Text = "Update Store Closing/X-Report";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                savebutton.Visible = true;
                btnUpdate.Visible = false;
                FormNamelabel.Text = "Create Store Closing/X-Report";
                fillClockInValueInForm();
            }
        }

        //private void fillVariableValues()
        //{
        //    try
        //    {
        //        TextData.toUses = txtToUser.Text;
        //        TextData.openingAmount = txtOpeningCash.Text;
        //        TextData.totalSales = txtSalesAmount.Text;
        //        TextData.totalReturns = txtSalesReturns.Text;
        //        TextData.totalVoidOrders = txtVoidOrders.Text;
        //        TextData.totalNoSale = txtNoSale.Text;
        //        TextData.totalPayout = txtPayout.Text;
        //        TextData.expectedAmount = txtExpectedAmount.Text;
        //        TextData.cashReceived = txtCashReceived.Text;
        //        TextData.excessCash = txtBalance.Text;
        //        TextData.remarks = txtRemarks.Text;
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private bool insert_records()
        {
            try
            {
                //fillVariableValues();

                txtDate.Text = DateTime.Now.ToLongDateString();

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }

                
                if (txtCashReceived.Text != "")
                {
                    if (txtShortAmount.Text != "")
                    {
                        GetSetData.query = @"SELECT id from pos_store_day_end where (date = '" + txtDate.Text + "');";
                        int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (is_already_exist == 0)
                        {
                            GetSetData.query = @"insert into pos_store_day_end values ('" + txtDate.Text + "', '" + txtOpeningCash.Text + "', '" + txtSalesAmount.Text + "', '" + txtSalesReturns.Text + "', '" + txtVoidOrders.Text + "', '" + txtExpectedAmount.Text + "', '" + txtCashReceived.Text + "', '" + txtBalance.Text + "', '" + txtShortAmount.Text + "', '" + txtNoSale.Text + "', '" + txtPayout.Text + "', '" + txtCreditCardAmount.Text + "', '" + txtApplyPayAmount.Text + "', '" + txtGooglePayAmount.Text + "', '" + txtMiscItems.Text + "', '" + txtTotalDiscount.Text + "', '" + txtTotalTaxation.Text + "', '" + txtTotalTickets.Text + "', '" + txt100.Text + "', '" + txt50.Text + "', '" + txt20.Text + "', '" + txt10.Text + "', '" + txt5.Text + "', '" + txt2.Text + "', '" + txt1.Text + "', '" + txt1c.Text + "', '" + txt5c.Text + "', '" + txt10c.Text + "', '" + txt25c.Text + "', '" + user_id.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            GetSetData.query = @"select id from pos_store_day_end where (date = '" + txtDate.Text + "');";
                            string store_day_end_id = data.SearchStringValuesFromDb(GetSetData.query);

                            //********************************************************************

                            GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
                                                pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
                                                pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
                                                FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
                                                where (status = '-1') or (status = '-2') or (status = '0');";


                            SqlConnection conn = new SqlConnection(webConfig.con_string);
                            SqlCommand cmd;
                            SqlDataReader reader;

                            cmd = new SqlCommand(GetSetData.query, conn);

                            conn.Open();
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                GetSetData.query = @"select id from pos_clock_out  where (clock_in_id = '" + reader["clock_in_id"].ToString() + "');";
                                double isAlreadyClockedOut = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                if (isAlreadyClockedOut == 0)
                                {
                                    string start_date = reader["start_date"].ToString();
                                    string start_time = reader["start_time"].ToString();


                                    DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
                                    DateTime endTime = DateTime.Today + DateTime.Parse(txtEndTime.Text).TimeOfDay;

                                    // Calculate duration
                                    TimeSpan duration = endTime - startTime;


                                    GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + txtEndTime.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + reader["user_id"].ToString() + "', '" + reader["user_id"].ToString() + "', '" + reader["clock_in_id"].ToString() + "', '0', '0', '0', '" + txt100.Text + "', '" + txt50.Text + "', '" + txt20.Text + "', '" + txt10.Text + "', '" + txt5.Text + "', '" + txt2.Text + "', '" + txt1.Text + "', '" + txt1c.Text + "', '" + txt5c.Text + "', '" + txt10c.Text + "', '" + txt25c.Text + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                                else
                                {
                                    GetSetData.query = @"update pos_clock_out set store_day_end_id = '" + store_day_end_id + "', total100s = '" + txt100.Text + "', total50s = '" + txt50.Text + "', total20s = '" + txt20.Text + "', total10s = '" + txt10.Text + "', total5s = '" + txt5.Text + "', total2s = '" + txt2.Text + "', total1s = '" + txt1.Text + "', total1c = '" + txt1c.Text + "', total5c = '" + txt5c.Text + "', total10c = '" + txt10c.Text + "', total25c = '" + txt25c.Text + "' where (clock_in_id = '" + reader["clock_in_id"].ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }

                                //*************************************************

                                GetSetData.query = @"update pos_clock_in set status  = '1' where (id = '" + reader["clock_in_id"].ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }

                            //*************************************************************

                            reader.Close();


                            return true;
                        }
                        else
                        {
                            error.errorMessage("Store day is already ended!");
                            error.ShowDialog();

                            return false;
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter stortage amount!");
                        error.ShowDialog();
                    }

                }
                else
                {
                    error.errorMessage("Please enter received amount!");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (insert_records())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                sure.Message_choose("Would you like to print the X-Report.");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    GetSetData.query = @"select default_printer from pos_general_settings;";
                    string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                    if (printer_name != "")
                    {
                        PrintDirectReceipt(printer_name);
                    }
                    else
                    {
                        error.errorMessage("Printer not found!");
                        error.ShowDialog();
                    }

                    //autoOpenCashDrawer();
                }

                refresh();
            }
        }

        //private void autoOpenCashDrawer()
        //{
        //    try
        //    {
        //        GetSetData.query = @"select default_printer from pos_general_settings;";
        //        string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


        //        GetSetData.query = @"select printer_model from pos_general_settings;";
        //        string printer_model = data.SearchStringValuesFromDb(GetSetData.query);


        //        CashDrawerData.OpenDrawer(printer_name, printer_model);
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage("Error opening cash drawer: " + es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private bool Update_records()
        {
            try
            {
                //fillVariableValues();

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }


                if (txtCashReceived.Text != "")
                {
                    if (txtShortAmount.Text != "")
                    {
                        GetSetData.query = @"update pos_store_day_end set date = '" + txtDate.Text +"', opening_cash = '" + txtOpeningCash.Text + "', total_sales = '" + txtSalesAmount.Text + "', total_return_amount = '" + txtSalesReturns.Text + "', total_void_orders = '" + txtVoidOrders.Text + "', expected_amount = '" + txtExpectedAmount.Text + "', cash_amount_received =  '" + txtCashReceived.Text + "', balance = '" + txtBalance.Text + "', shortage_amount = '" + txtShortAmount.Text + "', no_sales = '" + txtNoSale.Text + "', payout = '" + txtPayout.Text + "', credit_card_amount = '" + txtCreditCardAmount.Text + "', paypal_amount = '" + txtApplyPayAmount.Text + "', google_pay_amount = '" + txtGooglePayAmount.Text + "', misc_items_amount = '" + txtMiscItems.Text + "', total_discount = '" + txtTotalDiscount.Text + "', total_taxation = '" + txtTotalTaxation.Text + "', total_tickets = '" + txtTotalTickets.Text + "', total100s = '" + txt100.Text + "', total50s = '" + txt50.Text + "', total20s = '" + txt20.Text + "', total10s = '" + txt10.Text + "', total5s = '" + txt5.Text + "', total2s = '" + txt2.Text + "', total1s = '" + txt1.Text + "', total1c = '" + txt1c.Text + "', total5c = '" + txt5c.Text + "', total10c = '" + txt10c.Text + "', total25c = '" + txt25c.Text + "' where (id = '" + TextData.clockOut_id + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);


                        return true;
                    }
                    else
                    {
                        error.errorMessage("Please enter stortage amount!");
                        error.ShowDialog();
                    }

                }
                else
                {
                    error.errorMessage("Please enter received amount!");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Update_records())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();

                sure.Message_choose("Would you like to print the X-Report.");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    GetSetData.query = @"select default_printer from pos_general_settings;";
                    string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                    if (printer_name != "")
                    {
                        PrintDirectReceipt(printer_name);
                    }
                    else
                    {
                        error.errorMessage("Printer not found!");
                        error.ShowDialog();
                    }

                    //autoOpenCashDrawer();
                }
            }
        }

        private void formStoreEndDay_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();

            //autoOpenCashDrawer();
        }

        private void refresh()
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                txtEndTime.Text = DateTime.Now.ToLongTimeString();
                txtToUser.Text = "";
                txtOpeningCash.Text = "0.00";
                txtSalesAmount.Text = "0.00";
                txtSalesReturns.Text = "0.00";
                txtVoidOrders.Text = "0.00";
                txtNoSale.Text = "0.00";
                txtPayout.Text = "0.00";
                txtExpectedAmount.Text = "0.00";
                txtCreditCardAmount.Text = "0.00";
                txtApplyPayAmount.Text = "0.00";
                txtGooglePayAmount.Text = "0.00";
                txtMiscItems.Text = "0.00";
                txtTotalDiscount.Text = "0.00";
                txtTotalTaxation.Text = "0.00";
                txtTotalTickets.Text = "0";
                txtCashReceived.Text = "";
                txtBalance.Text = "0.00";
                txtRemarks.Text = "";

                fillComboBoxToUsers();

                system_user_permissions();
                enableSaveButton();

                txtCashReceived.Select();
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
        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtCashReceived.Text, e);
        }

        private void txt100_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void btnShift_Click(object sender, EventArgs e)
        {

        }

        private void btnCounter_Click(object sender, EventArgs e)
        {

        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double expected_amount = 0;
                double received_amount = 0;
                double balance = 0;

                if (txtExpectedAmount.Text != "")
                {
                    expected_amount = double.Parse(txtExpectedAmount.Text);
                }

                if (txtCashReceived.Text != "")
                {
                    received_amount = double.Parse(txtCashReceived.Text);

                    balance = received_amount - expected_amount;

                    txtBalance.Text = Math.Round(balance, 2).ToString();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            GetSetData.query = @"SELECT id from pos_store_day_end where (date = '" + txtDate.Text + "');";
            int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


            GetSetData.query = @"select default_printer from pos_general_settings;";
            string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


            if (is_already_exist == 0)
            {
                if (insert_records())
                {
                    if (printer_name != "")
                    {
                        PrintDirectReceipt(printer_name);

                        //autoOpenCashDrawer();
                    }
                    else
                    {
                        error.errorMessage("Printer not found!");
                        error.ShowDialog();
                    }

                    refresh();
                }
            }
            else
            {
                if (printer_name != "")
                {
                    PrintDirectReceipt(printer_name);
                }
                else
                {
                    error.errorMessage("Printer not found!");
                    error.ShowDialog();
                }
            }
        }


        private void PrintDirectReceipt(string printer_name)
        {
            try
            {
                GetSetData.query = @"SELECT  pos_store_day_end.date, from_user_employee.full_name as full_name,
                                    pos_clock_out.opening_cash, pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received, 
                                    pos_clock_out.total_void_orders, pos_clock_out.total_hours, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout,
                                    pos_store_day_end.opening_cash as totalOpeningCash, pos_store_day_end.total_return_amount as totalReturns, pos_store_day_end.total_void_orders as totalVoid,
                                    pos_store_day_end.expected_amount as totalExpectedAmount, pos_store_day_end.cash_amount_received as totalReceivedAmount, pos_store_day_end.balance as totalBalance,
                                    pos_store_day_end.shortage_amount as totalShortageAmount, pos_store_day_end.no_sales as totalNoSaleAmount, pos_store_day_end.payout as totalPayoutAmount, 
                                    pos_store_day_end.credit_card_amount as totalCreditCardAmount, pos_store_day_end.paypal_amount as totalPaypalAmount, pos_store_day_end.google_pay_amount as totalGooglePayAmount, 
                                    pos_store_day_end.misc_items_amount as totalMiscItemsAmount, pos_store_day_end.total_discount as totalDiscount, pos_store_day_end.total_taxation as totalTaxation, 
                                    pos_store_day_end.total_tickets as totalTickets, pos_store_day_end.total_sales as totalSales,
                                    pos_store_day_end.total100s, pos_store_day_end.total50s, pos_store_day_end.total20s, pos_store_day_end.total10s, pos_store_day_end.total5s, pos_store_day_end.total2s, pos_store_day_end.total1s,
                                    pos_store_day_end.total1c, pos_store_day_end.total5c, pos_store_day_end.total10c, pos_store_day_end.total25c
                                    FROM pos_store_day_end INNER JOIN pos_clock_out ON pos_store_day_end.id = pos_clock_out.store_day_end_id  INNER JOIN 
                                    pos_users ON pos_store_day_end.user_id = pos_users.user_id
                                    INNER JOIN pos_employees as to_user_employee ON to_user_employee.employee_id = pos_users.emp_id
                                    INNER JOIN pos_employees as from_user_employee ON from_user_employee.employee_id = pos_users.emp_id
                                    Where (pos_store_day_end.date = '" + txtDate.Text +"')";


                var dt = GetDataTable(GetSetData.query);
                LocalReport report = new LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_store_end_day_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new ReportDataSource("storeEndDayDS", dt));

                report.EnableExternalImages = true;
               
                //Report Parameters **********************************************************

                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pMainTitle = new Microsoft.Reporting.WinForms.ReportParameter("pTitle", GetSetData.Data);
                report.SetParameters(pMainTitle);


                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pAddress = new Microsoft.Reporting.WinForms.ReportParameter("pAddress", GetSetData.Data);
                report.SetParameters(pAddress);


                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pPhoneNo = new Microsoft.Reporting.WinForms.ReportParameter("pPhoneNo", GetSetData.Data);
                report.SetParameters(pPhoneNo);


                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);
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

        public static void Export(LocalReport report, string printer_name, bool print = true)
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

            if (print)
            {
                Print(printer_name);
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

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int total100s = 0;
                int total50s = 0;
                int total20s = 0;
                int total10s = 0;
                int total5s = 0;
                int total2s = 0;
                int total1s = 0;
               

                if (txt100.Text != "")
                {
                    total100s = Convert.ToInt32(txt100.Text);
                }

                if (txt50.Text != "")
                {
                    total50s = Convert.ToInt32(txt50.Text);
                }
                
                if (txt20.Text != "")
                {
                    total20s = Convert.ToInt32(txt20.Text);
                }

                if (txt10.Text != "")
                {
                    total10s = Convert.ToInt32(txt10.Text);
                }
                
                if (txt5.Text != "")
                {
                    total5s = Convert.ToInt32(txt5.Text);
                }

                if (txt2.Text != "")
                {
                    total2s = Convert.ToInt32(txt2.Text);
                }
                
                if (txt1.Text != "")
                {
                    total1s = Convert.ToInt32(txt1.Text);
                }


                txtDollars.Text = ((total100s * 100) + (total50s * 50) + (total20s * 20) + (total10s * 10) + (total5s * 5) + (total2s * 2) + (total1s * 1)).ToString();


                txtTotalDollars.Text = Math.Round((double.Parse(txtDollars.Text) + double.Parse(txtTotalCents.Text)), 2).ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txt25c_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int total25c = 0;
                int total10c = 0;
                int total5c = 0;
                int total1c = 0;

                if (txt25c.Text != "")
                {
                    total25c = Convert.ToInt32(txt25c.Text);
                }

                if (txt10c.Text != "")
                {
                    total10c = Convert.ToInt32(txt10c.Text);
                }

                if (txt5c.Text != "")
                {
                    total5c = Convert.ToInt32(txt5c.Text);
                }

                if (txt1c.Text != "")
                {
                    total1c = Convert.ToInt32(txt1c.Text);
                }

                double totalCents = ((total25c * 25) + (total10c * 10) + (total5c * 5) + (total1c * 1));

                txtTotalCents.Text = (totalCents / 100).ToString();

                txtTotalDollars.Text = Math.Round((double.Parse(txtDollars.Text) + (totalCents / 100)), 2).ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtGooglePayAmount_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
