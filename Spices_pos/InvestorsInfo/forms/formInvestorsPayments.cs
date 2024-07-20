using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Investors_info.PaybookReceipts;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Investors_info.forms
{
    public partial class formInvestorsPayments : Form
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
        public formInvestorsPayments()
         {
             InitializeComponent();
            setFormColorsDynamically();
         }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static bool message_choose = false;
        public static string get_customer = "";
        //public static string get_employee = "";
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCapital);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, txtCapitalAmount);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

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
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_investorPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.query = data.UserPermissions("new_investorPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnUpdate.Visible = bool.Parse(GetSetData.query);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.query) == false)
                {
                    pnl_save.Visible = false;
                }
                //***************************************************************************************************
                GetSetData.Data = data.UserPermissions("new_investorPayment_savePrint", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_save_and_print.Visible = bool.Parse(GetSetData.Data);

                //***************************************************************************************************
                GetSetData.Data = data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", role_id.ToString());

                if (GetSetData.Data == "TURE" || GetSetData.Data == "true" || GetSetData.Data == "True")
                {
                    lblCapital.Visible = true;
                    txtCapitalAmount.Visible = true;
                }
                else
                {
                    lblCapital.Visible = false;
                    txtCapitalAmount.Visible = false;
                }

                string lessAmount_db = data.UserPermissions("lessAmount", "pos_general_settings");
                string employeeSalary_db = data.UserPermissions("employeeSalary", "pos_general_settings");
                txtLess.Text = lessAmount_db;
                txtSalaries.Text = employeeSalary_db;
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
                txtEmployeeName.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", GetSetData.Ids.ToString());
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
                btnUpdate.Visible = false;
                savebutton.Visible = true;
                FormNamelabel.Text = "Create New Investor Ledger";
                LoginEmployee();
                FromDate.Select();
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                //salesAndReturnsGridView.ReadOnly = true;
                FormNamelabel.Text = "Update Investor Ledger";
                fillAddProductsFormTextBoxes();
                FormNamelabel.Select();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtDate.Text = TextData.dates;
                time_text.Text = TextData.times;
                txtInvestor.Text = TextData.full_name;
                txtInvestorCode.Text = TextData.cus_code;
                txtEmployeeName.Text = TextData.employee;
                references_text.Text = TextData.reference;
                comments_text.Text = TextData.remarks;
                txtInvestment.Text = TextData.investment.ToString();
                txtShare.Text = TextData.sharePercentage.ToString();

                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();

                GetSetData.query = "select paybook_id from pos_investorPaybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //*********************************************************************
                FromDate.Text = data.UserPermissions("fromDate", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());
                ToDate.Text = data.UserPermissions("toDate", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());

                //GetSetData.fks = data.UserPermissionsIds("investor_id", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());
                //TextData.profitPercentage = data.NumericValues("netProfit", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());>>
                txtSalaries.Text = data.UserPermissions("salaries", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());
                TextData.cash = data.NumericValues("payment", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());

                LoadCapitalAndProfitDetails();
                TextData.lessPercentage = data.NumericValues("lessAmount", "pos_investorPaybook", "paybook_id", GetSetData.Ids.ToString());

                TextData.profitPercentage = double.Parse(txtNetProfit.Text);
                TextData.lessPercentage = (TextData.lessPercentage / TextData.profitPercentage) * 100;

                txtLess.Text = TextData.lessPercentage.ToString();
                txtPayment.Text = TextData.cash.ToString();
                //txtNetProfit.Text = TextData.profitPercentage.ToString();
              
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomeDetails()
        {
            GetSetData.query = @"select mobile_no from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
            cellno_text.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select cnic from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
            txt_cnic.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select address1 from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
            txt_address.Text = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select picture_path from pos_general_settings;";
            TextData.image_path_db = data.SearchStringValuesFromDb(GetSetData.query);

            GetSetData.query = @"select image_path from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
            TextData.image = data.SearchStringValuesFromDb(GetSetData.query);

            if (TextData.image != "nill" && TextData.image != "")
            {
                img_pic_box.Image = Image.FromFile(TextData.image_path_db + TextData.image);
            }
        }

        private void FillComboBoxCusLastCredits()
        {
            try
            {
                GetSetData.query = @"select share_percentage from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
                txtShare.Text = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = @"select investment from pos_investors where full_name = '" + txtInvestor.Text + "' and code = '" + txtInvestorCode.Text + "';";
                txtInvestment.Text = data.SearchStringValuesFromDb(GetSetData.query);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoadCapitalAndProfitDetails()
        {
            try
            {
                // Sales Details *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Sale_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_SalePrice == "" || Sale_SalePrice == "NULL")
                {
                    Sale_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_wholeSale)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Sale_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_WholeSale == "" || Sale_WholeSale == "NULL")
                {
                    Sale_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase)
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Sale_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Sale_PurchasePrice == "" || Sale_PurchasePrice == "NULL")
                {
                    Sale_PurchasePrice = "0";
                }

                // return Details*******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Return_SalePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_SalePrice == "" || Return_SalePrice == "NULL")
                {
                    Return_SalePrice = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_wholeSale) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Return_WholeSale = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_WholeSale == "" || Return_WholeSale == "NULL")
                {
                    Return_WholeSale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_return_accounts INNER JOIN pos_returns_details ON 
                                    pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN pos_products ON 
                                    pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";

                string Return_PurchasePrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (Return_PurchasePrice == "" || Return_PurchasePrice == "NULL")
                {
                    Return_PurchasePrice = "0";
                }

                // Get the Paid Profit to the Investors*******************************************************************************************
                //GetSetData.query = @"select sum(lessAmount + salaries + payment) from pos_investorPaybook where (fromDate = '" + FromDate.Text + "') and (toDate = '" + ToDate.Text + "');";
                //string totalPaidProfitToInvestors = data.SearchStringValuesFromDb(GetSetData.query);

                // *******************************************************************************************
                string checkInvestorProfit_db = data.UserPermissions("investorProfit", "pos_general_settings");
                TextData.profitPercentage = 0;

                if (checkInvestorProfit_db == "W_Sale - Pur")
                {
                    TextData.profitPercentage = (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }
                else if (checkInvestorProfit_db == "Sale - W_Sale")
                {
                    TextData.profitPercentage = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale));
                }
                else if (checkInvestorProfit_db == "Sale - Pur")
                {
                    TextData.profitPercentage = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }

                // Calculating Monthly Profit
                if (TextData.profitPercentage >= 0)
                {
                    txtNetProfit.Text = Math.Round(TextData.profitPercentage, 4).ToString();
                }

                txtCapitalAmount.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomerName()
        {
            txtInvestor.Text = data.UserPermissions("full_name", "pos_investors", "code", txtInvestorCode.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            txtInvestorCode.Text = data.UserPermissions("code", "pos_investors", "full_name", txtInvestor.Text);
        }
        
        private void FillGridViewUsingPagination()
        {
            try
            {
                GetSetData.query = "select * from ViewInvestorPaybookDetails where ([Investor] = '" + txtInvestor.Text + "') and ([Code] = '" + txtInvestorCode.Text + "');";
                GetSetData.FillDataGridViewUsingPagination(purchaseDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refreshEveryFields()
        {
            string lessAmount_db = data.UserPermissions("lessAmount", "pos_general_settings");
            string employeeSalary_db = data.UserPermissions("employeeSalary", "pos_general_settings");
            
            txtEmployeeName.Text = "";
            txtLess.Text = lessAmount_db;
            txtSalaries.Text = employeeSalary_db;
        }

        private void refresh()
        {
            txtDate.Text = DateTime.Now.ToLongDateString();
            time_text.Text = DateTime.Now.ToLongTimeString();
            txtInvestor.Text = "";
            txtInvestorCode.Text = "";
            txtShare.Text = "0.00";
            cellno_text.Text = "";
            references_text.Text = "";
            comments_text.Text = "";
            txtPayment.Text = "0";
            txtBalance.Text = "0.00";
            txtInvestment.Text = "0.00";
            txt_address.Text = "";
            txt_cnic.Text = "";
            img_pic_box.Image = null;
        }

        private void funFillCusEmpNames()
        {
            try
            {
                //txtEmployeeName.Text = get_employee;
                txtInvestor.Text = get_customer;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
       
        private void formInvestorsPayments_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                txtDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToLongTimeString();

                funFillCusEmpNames();
                system_user_permissions();
                enableSaveButton();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private void sendSmsToCustomers()
        //{
        //    try
        //    {
        //        SerialPort sp = new SerialPort();
        //        sp.PortName = "LPT1";
        //        sp.Open();
        //        sp.WriteLine("AT" + Environment.NewLine);
        //        Thread.Sleep(100);
        //        sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
        //        Thread.Sleep(100);
        //        sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
        //        Thread.Sleep(100);
        //        sp.WriteLine("AT+CMGS=\"" + "+923133879645" + "\"" + Environment.NewLine);
        //        Thread.Sleep(100);
        //        sp.WriteLine("Hello World!" + Environment.NewLine);
        //        Thread.Sleep(100);
        //        sp.Write(new byte[] {26}, 0, 1);
        //        Thread.Sleep(100);
        //        var response = sp.ReadExisting();
        //        if (response.Contains("ERROR"))
        //            MessageBox.Show("Send Failed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        else
        //            MessageBox.Show("SMS Send!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        sp.Close();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //        //error.errorMessage(es.Message);
        //        //error.ShowDialog();
        //    }
        //}

        //private void sendSmsToCustomers()
        //{
        //    using (System.Net.WebClient client = new System.Net.WebClient())
        //    {
        //        try
        //        {
        //            string url = "http://smsc.vianett.no/v3/send.ashx?" +
        //            "src=" + "+923133879645" + "&" +
        //            "dst=" + "+923133879645" + "&" +
        //            "msg=" + System.Web.HttpUtility.UrlEncode("Hello World!" + System.Text.Encoding.GetEncoding("ISO-8859-1")) + "" +
        //           "username=" + System.Web.HttpUtility.UrlEncode("") + "&" +
        //           "password=" + System.Web.HttpUtility.UrlEncode("");
        //           string result = client.DownloadString(url);
        //           if (result.Contains("OK"))
        //               MessageBox.Show("Send SMS!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //           else
        //               MessageBox.Show("Send Failed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        catch (Exception es)
        //        {
        //            MessageBox.Show(es.Message);
        //            //error.errorMessage(es.Message);
        //            //error.ShowDialog();
        //        }
        //    }
        //}

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Investor Payment Form", "Exit...", role_id);
            this.Close();
            //sendSmsToCustomers();
        }

        private void add_sub_category_Click(object sender, EventArgs e)
        {
            using (formNewInvestor add_customer = new formNewInvestor())
            {
                formNewInvestor.role_id = role_id;
                add_customer.ShowDialog();
            }
        }

        private void add_spplier_Click(object sender, EventArgs e)
        {
            using (Add_supplier add_customer = new Add_supplier())
            {
                Add_supplier.role_id = role_id;
                add_customer.ShowDialog();
            }
        }

        private void cash_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtPayment.Text, e);
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            refresh();
            LoadCapitalAndProfitDetails();
            refreshEveryFields();
        }

        private void FillVariablesWithValues()
        {
            try
            {
                TextData.dates = txtDate.Text;
                TextData.fromDate = FromDate.Text;
                TextData.toDate = ToDate.Text;
                TextData.times = time_text.Text;
                TextData.full_name = txtInvestor.Text;
                TextData.cus_code = txtInvestorCode.Text;
                TextData.employee = txtEmployeeName.Text;
                TextData.phone1 = cellno_text.Text;
                TextData.reference = references_text.Text;
                TextData.remarks = comments_text.Text;
                TextData.sharePercentage = double.Parse(txtShare.Text);
                TextData.investment = double.Parse(txtInvestment.Text);
                TextData.CapitalAmount = double.Parse(txtCapitalAmount.Text);
                TextData.profitPercentage = double.Parse(txtNetProfit.Text);
                TextData.Availableprofit = double.Parse(txtAvailableProfit.Text);
                TextData.lessPercentage = 0;
                TextData.salaries = 0;
                TextData.cash = 0;
                TextData.credits = 0;


                if (txtLess.Text != "")
                {
                    TextData.lessPercentage = double.Parse(txtLess.Text);
                }

                if (txtCredits.Text != "")
                {
                    TextData.credits = double.Parse(txtCredits.Text);
                }

                if (txtSalaries.Text != "")
                {
                    TextData.salaries = double.Parse(txtSalaries.Text);
                }

                if (txtPayment.Text != "")
                {
                    TextData.cash = double.Parse(txtPayment.Text);
                }

                if (comments_text.Text == "")
                {
                    TextData.remarks = "nill";
                }

                if (references_text.Text == "")
                {
                    TextData.reference = "nill";
                }

                if (txtEmployeeName.Text == "")
                {
                    TextData.employee = "nill";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_into_db()
        {
            try
            {
                FillVariablesWithValues();

                if (txtEmployeeName.Text != "")
                {
                    if (txtInvestor.Text != "")
                    {
                        if (txtLess.Text != "")
                        {
                            if (txtSalaries.Text != "")
                            {
                                if (txtPayment.Text != "")
                                {
                                    if (txtCredits.Text != "")
                                    {
                                        GetSetData.query = @"select investor_id from pos_investors where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                        // *******************************************************************************
                                        GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                        TextData.lessPercentage = (TextData.profitPercentage * TextData.lessPercentage) / 100;
                                        //TextData.cash = (TextData.profitPercentage * TextData.cash) / 100;

                                        // *******************************************************************************
                                        GetSetData.query = @"insert into pos_investorPaybook values ('" + TextData.dates.ToString() + "' , '" + TextData.times.ToString() + "' , '" + TextData.fromDate.ToString() + "' , '" + TextData.toDate.ToString() + "' , '" + TextData.reference.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + TextData.sharePercentage.ToString() + "' , '" + TextData.investment.ToString() + "' , '" + TextData.profitPercentage.ToString() + "', '" + TextData.Availableprofit.ToString() + "' , '" + TextData.lessPercentage.ToString() + "' , '" + TextData.salaries.ToString() + "' , '" + TextData.cash.ToString() + "' , '" + TextData.credits.ToString() + "', '" + TextData.balance.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                        // *****************************************************************************************

                                        GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                        if (GetSetData.Data == "Yes")
                                        {
                                            TextData.balance = TextData.cash + TextData.salaries;

                                            if (TextData.balance <= TextData.CapitalAmount)
                                            {
                                                TextData.CapitalAmount = TextData.CapitalAmount - TextData.balance;

                                                if (TextData.CapitalAmount <= 0)
                                                {
                                                    TextData.CapitalAmount = 0;
                                                }

                                                GetSetData.query = "update pos_capital set total_capital = '" + TextData.CapitalAmount.ToString() + "';";
                                                data.insertUpdateCreateOrDelete(GetSetData.query);
                                            }
                                        }
                                        // *****************************************************************************************
                                        GetSetData.SaveLogHistoryDetails("Add New Investor Payment Form", "Saving investor payment [" + TextData.dates + "  " + TextData.times + "] details", role_id);

                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("Please enter the investor credited amount!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please enter the payment percentage!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the salaries!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the less percentage!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the investor!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the casher!");
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
            //form_sure_message choose = new form_sure_message();

            //choose.Message_choose("Please confirm all the records before saving!");
            //choose.ShowDialog();

            //if (message_choose == true)
            //{
            if (insert_into_db())
            {
                this.Opacity = .850;
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                this.Opacity = .999;
                refresh();
                LoadCapitalAndProfitDetails();
            }
            //}
        }

        private bool UpdateRecoveryDetailsdb()
        {
            try
            {
                FillVariablesWithValues();

                if (txtEmployeeName.Text != "")
                {
                    if (txtInvestor.Text != "")
                    {
                        if (txtLess.Text != "")
                        {
                            if (txtSalaries.Text != "")
                            {
                                if (txtPayment.Text != "")
                                {
                                    GetSetData.query = @"select investor_id from pos_investors where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    // *******************************************************************************
                                    GetSetData.fks = data.UserPermissionsIds("employee_id", "pos_employees", "full_name", TextData.employee);

                                    GetSetData.query = "select netProfit from pos_investorPaybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                                    TextData.profitPercentage = data.SearchNumericValuesDb(GetSetData.query);

                                    GetSetData.query = "select profit from pos_investorPaybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                                    TextData.Availableprofit = data.SearchNumericValuesDb(GetSetData.query);
                                    // *****************************************************************************************

                                    GetSetData.query = "select (payment) from pos_investorPaybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                                    double previousPaidAmount = data.SearchNumericValuesDb(GetSetData.query);
                                    // *****************************************************************************************

                                    TextData.lessPercentage = (TextData.profitPercentage * TextData.lessPercentage) / 100;

                                    // *******************************************************************************
                                    GetSetData.query = @"update pos_investorPaybook set reference = '" + TextData.reference.ToString() + "' , remarks = '" + TextData.remarks.ToString() + "' ,  investorShare = '" + TextData.sharePercentage.ToString() + "' , investment = '" + TextData.investment.ToString() + "' , netProfit = '" + TextData.profitPercentage.ToString() + "', profit = '" + TextData.Availableprofit.ToString() + "' , lessAmount = '" + TextData.lessPercentage.ToString() + "' , salaries = '" + TextData.salaries.ToString() + "' , payment = '" + TextData.cash.ToString() + "', credits = '" + TextData.credits.ToString() + "', balance = '" + TextData.balance.ToString() + "' , investor_id = '" + GetSetData.Ids.ToString() + "' , employee_id = '" + GetSetData.fks.ToString() + "' where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                    // *****************************************************************************************
                                    TextData.balance = TextData.cash;
                                    TextData.CapitalAmount += previousPaidAmount;

                                    if (GetSetData.Data == "Yes")
                                    {
                                        GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                                        if (TextData.balance <= TextData.CapitalAmount)
                                        {
                                            TextData.CapitalAmount -= TextData.balance;

                                            GetSetData.query = "update pos_capital set total_capital = '" + TextData.CapitalAmount.ToString() + "';";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }
                                    }
                                    // *****************************************************************************************
                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Please enter the payment percentage!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the salaries!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the less percentage!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the investor!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the casher!");
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
            form_sure_message choose = new form_sure_message();

            choose.Message_choose("Please confirm all the records before modifing!");
            choose.ShowDialog();

            if (message_choose == true)
            {
                if (UpdateRecoveryDetailsdb())
                {
                    this.Opacity = .850;
                    done.DoneMessage("Updated Successfully!");
                    done.ShowDialog();
                    this.Opacity = .999;
                }
            }
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            TextData.dates = txtDate.Text;
            TextData.times = time_text.Text;

            if (saveEnable == false)
            {
                if (insert_into_db())
                {
                    formInvestorPaybookReceipt reports = new formInvestorPaybookReceipt();
                    reports.ShowDialog();
                    refresh();
                    LoadCapitalAndProfitDetails();
                }
            }
            else if (saveEnable == true)
            {
                if (UpdateRecoveryDetailsdb())
                {
                    GetSetData.SaveLogHistoryDetails("Add New Investor Payment Form", "Print investor payment invoice button click...", role_id);

                    formInvestorPaybookReceipt reports = new formInvestorPaybookReceipt();
                    reports.ShowDialog();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(purchaseDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void txt_sale_person_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtInvestor, "fillComboBoxInvestorsNames", "full_name");
        }

        private void customer_code_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtInvestorCode, "fillComboBoxInvestorsNames", "code");
        }

        private void customer_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxCustomeCodes();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void customer_code_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxCustomerName();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void txtDues_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtSalaries.Text, e);
        }

        private void txtCredits_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtCredits.Text, e);
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //LoadIntallmentDetails();
                FillComboBoxCustomeDetails();
                FillComboBoxCusLastCredits();
                FillGridViewUsingPagination();
            }
        }

        private void txtLess_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtLess.Text, e);
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadCapitalAndProfitDetails();
            }
        }

        private void BalanceAmount()
        {
            try
            {
                TextData.cash = 0;
                TextData.lessPercentage = 0;
                TextData.salaries = 0;
                TextData.profitPercentage = 0;

                if (txtLess.Text != "" && txtSalaries.Text != "" && txtPayment.Text != "" && txtCredits.Text != "")
                {
                    TextData.profitPercentage = double.Parse(txtNetProfit.Text);
                    TextData.cash = double.Parse(txtPayment.Text);
                    TextData.lessPercentage = double.Parse(txtLess.Text);
                    TextData.salaries = double.Parse(txtSalaries.Text);
                    TextData.credits = double.Parse(txtCredits.Text);
                    TextData.profitDivide = data.UserPermissions("profitDivide", "pos_general_settings");

                    // Calculating the profit amount
                    TextData.lessPercentage = (TextData.profitPercentage * TextData.lessPercentage) / 100;
                    TextData.profitPercentage = TextData.profitPercentage - TextData.lessPercentage;
                    TextData.profitPercentage = TextData.profitPercentage - TextData.salaries;
                    TextData.profitPercentage = TextData.profitPercentage / double.Parse(TextData.profitDivide);

                    if (TextData.profitPercentage >= 0)
                    {
                        txtAvailableProfit.Text = Math.Round(TextData.profitPercentage, 4).ToString();
                    }
                    else
                    {
                        txtAvailableProfit.Text = "0";
                    }
                    //GetSetData.query = @"select investment from pos_investors where (full_name = '" + txtInvestor.Text + "') and (code = '" + txtInvestorCode.Text + "');";
                    //double investorShare_db = data.SearchNumericValuesDb(GetSetData.query);
                    double investorShare_db = double.Parse(txtInvestment.Text);

                    // Calculating the shares for each shareholder
                    GetSetData.query = @"select sum(investment) from pos_investors where status = 'Active';";
                    string investorInvestments = data.SearchStringValuesFromDb(GetSetData.query);

                    if (investorInvestments == "" || investorInvestments == "NULL")
                    {
                        investorInvestments = "0";
                    }

                    TextData.totalInvestment = double.Parse(investorInvestments);
                    //*****************************************************************

                    TextData.sharePercentage = TextData.profitPercentage / TextData.totalInvestment;
                    TextData.sharePercentage = TextData.sharePercentage * investorShare_db;
                    TextData.credits = TextData.sharePercentage - TextData.credits;

                    if (TextData.credits >= 0)
                    {
                        txtPayment.Text = Math.Round(TextData.credits, 4).ToString(); 
                    }
                    else
                    {
                        txtCredits.Text = "0";
                    }

                    if (TextData.sharePercentage <= TextData.profitPercentage)
                    {
                        TextData.totalInvestment = TextData.profitPercentage - TextData.sharePercentage;
                        txtBalance.Text = Math.Round(TextData.totalInvestment, 4).ToString(); 

                    }
                }
                //else
                //{
                //    error.errorMessage("Please fill the empty fields!");
                //    error.ShowDialog();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void cash_text_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }

        private void txtLess_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }

        private void txtSalaries_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }

        private void txtInvestment_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }

        private void txtCredits_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }

        private void txtNetProfit_TextChanged(object sender, EventArgs e)
        {
            BalanceAmount();
        }
    }
}
