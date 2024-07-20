using CounterSales_info.CustomerSalesInfo.CustomControls;
using Datalayer;
using Login_info.controllers;
using Message_box_info.forms;
using Products_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace CounterSales_info.forms
{
    public partial class fromSecondScreen : Form
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

        public static fromSecondScreen instance;
        public Label lblCustomer;

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public static string macAddress = "";
        public static int user_id = 0;
        public static int role_id = 0;

        public fromSecondScreen()
        {
            InitializeComponent();

            macAddress = get_mac_address();
            logo_imag();

            instance = this;

            lblCustomer = lbl_customer;
        }

        private void logo_imag()
        {
            try
            {
                string folderPath = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");
                TextData.image_path = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "logo_path");

                if (folderPath != "nill" && folderPath != "")
                {
                    if (TextData.image_path != "nill" && TextData.image_path != "" && TextData.image_path != null)
                    {
                        logo_img.Image = Bitmap.FromFile(folderPath + TextData.image_path);
                    }
                }

                GetSetData.query = @"select title from pos_report_settings;";
                txtStoreTitle.Text = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select phone_no from pos_report_settings;";
                txtStoreCellNo.Text = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select note from pos_report_settings;";
                txtMessage.Text = data.SearchStringValuesFromDb(GetSetData.query);
          
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private string get_mac_address()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;

                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }

                return sMacAddress;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return "";
            }
        }

        private void btnScrollUp_Click(object sender, EventArgs e)
        {
            CartFlowLayout.AutoScroll = false;

            int change = CartFlowLayout.VerticalScroll.Value - CartFlowLayout.VerticalScroll.SmallChange * 30;

            CartFlowLayout.AutoScrollPosition = new Point(0, change);
        }

        private void btnScrollDown_Click(object sender, EventArgs e)
        {
            CartFlowLayout.AutoScroll = false;

            int change = CartFlowLayout.VerticalScroll.Value + CartFlowLayout.VerticalScroll.SmallChange * 30;

            CartFlowLayout.AutoScrollPosition = new Point(0, change);
        }

        private void CartFlowLayout_ControlAdded(object sender, ControlEventArgs e)
        {
            CartFlowLayout.AutoScroll = true;
            CartFlowLayout.AutoScroll = false;
        }

        private void CartFlowLayout_ControlRemoved(object sender, ControlEventArgs e)
        {
            CartFlowLayout.AutoScroll = true;
            CartFlowLayout.AutoScroll = false;
        }

        public void getCartItems()
        {
            try
            {
                clearDataGridView();

                GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                int is_items_exists_in_cart = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                
                if (is_items_exists_in_cart != 0)
                {
                    TextData.totalAmount = 0;
                    double discount_db = 0;

                    GetSetData.query = "select * from pos_cart_items where (mac_address = '" + macAddress + "') order by id asc;";

                    SqlConnection conn = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd;
                    SqlDataReader reader;

                    cmd = new SqlCommand(GetSetData.query, conn);

                    conn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TextData.totalAmount = double.Parse(reader["total_amount"].ToString());

                        SecondScreenCartItem cartItem = new SecondScreenCartItem();

                        cartItem.ItemsName = data.UserPermissions("prod_name", "pos_products", "product_id", reader["product_id"].ToString());
                        cartItem.Brand = data.UserPermissions("brand_title", "pos_brand", "brand_id", reader["brand_id"].ToString());


                        cartItem.price = double.Parse(reader["price"].ToString());
                        cartItem.Quantity = reader["quantity"].ToString();
                        cartItem.discount = double.Parse(reader["discount"].ToString());
                        cartItem.tax = double.Parse(reader["tax"].ToString());


                        cartItem.TotalAmount = GetSetData.currency() + Math.Round(double.Parse(reader["total_amount"].ToString()) - double.Parse(reader["discount"].ToString()), 2).ToString();

                        CartFlowLayout.Controls.Add(cartItem);
                    }

                    reader.Close();

                    #region

                    //***********************************************

                    //string totalTaxation = "";
                    //string totalAmount = "";
                    //string totalDiscount = "";


                    //GetSetData.query = "select sum(discount) from pos_cart_items where (mac_address = '" + macAddress + "');";
                    //totalDiscount = data.SearchStringValuesFromDb(GetSetData.query);

                    //if (totalDiscount == "")
                    //{
                    //    totalDiscount = "0";
                    //}

                    //txt_total_discount.Text = Math.Round(double.Parse(totalDiscount), 2).ToString();


                    //GetSetData.query = "select sum(tax) from pos_cart_items where (mac_address = '" + macAddress + "');";
                    //totalTaxation = data.SearchStringValuesFromDb(GetSetData.query);

                    //if (totalTaxation == "")
                    //{
                    //    totalTaxation = "0";
                    //}
                    //txtTaxation.Text = Math.Round(double.Parse(totalTaxation), 2).ToString();


                    //GetSetData.query = "select sum(total_amount) from pos_cart_items where (mac_address = '" + macAddress + "');";
                    //totalAmount = data.SearchStringValuesFromDb(GetSetData.query);


                    //if (totalAmount != "" && totalTaxation != "")
                    //{
                    //    txt_sub_total.Text = Math.Round(double.Parse(totalAmount) - double.Parse(totalTaxation), 2).ToString();
                    //}
                    //else
                    //{
                    //    txt_sub_total.Text = "0.00";
                    //}

                    //if (totalAmount != "")
                    //{
                    //    txtGrandTotal.Text = Math.Round(double.Parse(totalAmount), 2).ToString();
                    //}
                    //else
                    //{
                    //    txtGrandTotal.Text = "0.00";
                    //}


                    //GetSetData.query = "select customer_id from pos_cart_items where (mac_address = '" + macAddress + "') and (customer_id != '');";
                    //string customer_id = data.SearchStringValuesFromDb(GetSetData.query);


                    //if (customer_id != "")
                    //{
                    //    lbl_customer.Text = data.UserPermissions("full_name", "pos_customers", "customer_id", customer_id);
                    //    txtCustomerPoints.Text = data.UserPermissions("customer_points", "pos_cart_items", "mac_address", macAddress);
                    //}
                    //else
                    //{
                    //    lbl_customer.Text = "";
                    //    txtCustomerPoints.Text = "0.00";
                    //}


                    //GetSetData.query = "select count(id) from pos_cart_items where (mac_address = '" + macAddress + "');";
                    //txt_total_items.Text = data.SearchStringValuesFromDb(GetSetData.query);
                    #endregion

                    txt_total_items.Text = TextData.invoiceTotalItems;
                    txt_total_qty.Text = TextData.invoiceQuantity;
                    txt_sub_total.Text = TextData.invoiceSubTotal;
                    txtTaxation.Text = TextData.invoiceTotalTaxation;
                    txtGrandTotal.Text = TextData.invoiceGrandTotal;
                    txt_amount_due.Text = TextData.invoiceAmountDue;
                    txtChangeAmount.Text = TextData.invoiceChangeAmount;
                   
                    //txt_status.Checked = TextData.isReturn;
                }
                else
                {
                    lbl_customer.Text = "";
                    txtCustomerPoints.Text = "0.00";
                    txt_total_discount.Text = "0.00";
                    txt_total_items.Text = "0.00";
                    txt_total_qty.Text = "0.00";
                    txt_sub_total.Text = "0.00";
                    txtTaxation.Text = "0.00";
                    txtGrandTotal.Text = "0.00";
                    txt_amount_due.Text = "0.00";
                    txtChangeAmount.Text = "0.00";

                    clearDataGridView();
                }

                lbl_customer.Text = TextData.invoiceCustomerName;
                txtCustomerPoints.Text = TextData.invoiceCustomerPoints;
                txt_total_discount.Text = TextData.invoiceDiscount;


                GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.invoiceCustomerName + "') and (mobile_no = '" + TextData.customerCellNumber + "');";
                TextData.invoiceCustomerCode = data.SearchStringValuesFromDb(GetSetData.query);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void clearDataGridView()
        {
            try
            {
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

                //****************************

                // Dispose the controls in the copied collection
                for (int i = CartFlowLayout.Controls.Count - 1; i >= 0; i--)
                {
                    Control control = CartFlowLayout.Controls[i];

                    if (control is SecondScreenCartItem cartItem)
                    {
                        cartItem.Dispose();
                    }
                }

            }
            catch (Exception es)
            {
                error.errorMessage("Cart is already empty!");
                error.ShowDialog();
            }
        }

        public void isReturn()
        {
            try
            {
                GetSetData.query = "select is_return from pos_cart_items where (mac_address = '" + macAddress + "') and (is_return != '');";
                string isReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (isReturn == "")
                {
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
                    //************************************

                    txtReturnSubTotal.Visible = true;
                    txtReturnGrandTotal.Visible = true;
                    txtReturnAmountDue.Visible = true;
                    txtReturnPoints.Visible = true;
                    txtReturnTax.Visible = true;
                    txtReturnDiscount.Visible = true;
                    txtReturnItems.Visible = true;
                    txtReturnQuantity.Visible = true;


                    //************************************

                    txt_sub_total.Visible = false;
                    txtGrandTotal.Visible = false;
                    txt_amount_due.Visible = false;
                    txtCustomerPoints.Visible = false;
                    txtTaxation.Visible = false;
                    txt_total_discount.Visible = false;
                    txt_total_items.Visible = false;
                    txt_total_qty.Visible = false;


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
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //private void btnSignUp_Click(object sender, EventArgs e)
        //{
        //    if (Screen.AllScreens.Length > 1)
        //    {
        //        if (!IsFormOpen(typeof(customerSignUpForm)))
        //        {
        //            customerSignUpForm secondaryForm = new customerSignUpForm();

        //            Screen secondaryScreen = Screen.AllScreens[1];
        //            secondaryForm.StartPosition = FormStartPosition.CenterScreen;
        //            secondaryForm.Location = secondaryScreen.WorkingArea.Location;
        //            secondaryForm.WindowState = FormWindowState.Maximized;
        //            secondaryForm.Show();
        //        }
        //    }
        //}
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            btnSignUp.Enabled = false;

            if (Screen.AllScreens.Length > 1)
            {
                if (!IsFormOpen(typeof(customerSignUpForm)))
                {
                    customerSignUpForm secondaryForm = new customerSignUpForm();

                    Screen secondaryScreen = Screen.AllScreens[1];
                    secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                    secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                    secondaryForm.WindowState = FormWindowState.Maximized;
                    secondaryForm.FormClosed += (s, args) => { btnSignUp.Enabled = true; }; // Re-enable the button when the form is closed
                    secondaryForm.Show();
                }
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            btnSignIn.Enabled = false;

            if (Screen.AllScreens.Length > 1)
            {
                if (!IsFormOpen(typeof(customerSignInForm)))
                {
                    customerSignInForm secondaryForm = new customerSignInForm();

                    Screen secondaryScreen = Screen.AllScreens[1];
                    secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                    secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                    secondaryForm.WindowState = FormWindowState.Maximized;
                    secondaryForm.FormClosed += (s, args) => { btnSignIn.Enabled = true; }; // Re-enable the button when the form is closed
                    secondaryForm.Show();
                }
            }
        }

        // Method to check if a form of a certain type is already open
        private bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                    return true;
            }
            return false;
        }

    }
}
