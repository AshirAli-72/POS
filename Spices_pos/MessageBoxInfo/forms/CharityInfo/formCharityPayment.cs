using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.CharityInfo
{
    public partial class formCharityPayment : Form
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

        public formCharityPayment()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static bool saveEnable;

        private void setFormColorsDynamically()
        {
            try
            {
                int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
                int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
                int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

                int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

                int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

                //****************************************************************

                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                //***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("newCharityPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnExit.Visible = bool.Parse(GetSetData.Data);
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
        
        private void fillAddProductsFormTextBoxes()
        {
            FromDate.Text = TextData.fromDate;
            ToDate.Text = TextData.toDate;
            txtPaymentDate.Text = TextData.date;
            txtTime.Text = TextData.time;
            txtFullName.Text = TextData.fullName;
            txtFatherName.Text = TextData.fatherName;
            txtContactNo.Text = TextData.mobileNo;
            txtAmount.Text = TextData.editAmount;
            txtReference.Text = TextData.references;
            txtRemarks.Text = TextData.remarks;
            txtLessAmount.Text = TextData.editLessAmount;
            txtNetProfit.Text = TextData.editProfit;
            txtBalance.Text = TextData.editBalance;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "UPDATE CHARITY PAYMENT DETAILS";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "ADD NEW CHARITY PAYMENT";
            }
        }

        private void fillVariableValues()
        {
            try
            {
                TextData.fromDate = FromDate.Text;
                TextData.toDate = ToDate.Text;
                TextData.fullName = txtFullName.Text;
                TextData.fatherName = txtFatherName.Text;
                TextData.mobileNo = txtContactNo.Text;
                TextData.references = txtReference.Text;
                TextData.remarks = txtRemarks.Text;
                TextData.cashAmount = txtAmount.Text;

                if (txtFatherName.Text == "")
                {
                    TextData.fatherName = "nill";
                }

                if (txtContactNo.Text == "")
                {
                    TextData.mobileNo = "nill";
                }

                if (txtReference.Text == "")
                {
                    TextData.references = "nill";
                }

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool insert_records()
        {
            try
            {
                fillVariableValues();
                TextData.date = txtPaymentDate.Text;
                TextData.time = txtTime.Text;

                if (txtFullName.Text != "")
                {
                    if (txtAmount.Text != "")
                    {
                        if (txtLessAmount.Text != "")
                        {
                            TextData.lessAmount = double.Parse(txtLessAmount.Text);
                            TextData.netProfit = double.Parse(txtNetProfit.Text);
                            TextData.balance = double.Parse(txtBalance.Text);

                            GetSetData.query = @"insert into pos_charityDetails values ('" + TextData.fromDate + "', '" + TextData.toDate + "', '" + TextData.date + "', '" + TextData.time + "', '" + TextData.fullName + "', '" + TextData.fatherName + "', '" + TextData.mobileNo + "', '" + TextData.cashAmount + "', '" + TextData.references + "',  '" + TextData.remarks + "', '" + TextData.lessAmount.ToString() + "', '" + TextData.netProfit.ToString() + "', '" + TextData.balance.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            //*****************************************************************************************
                            string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                            if (GetSetData.Data == "Yes")
                            {
                                if (capital != "NULL" && capital != "")
                                {
                                    TextData.cashAmount = (double.Parse(capital) - double.Parse(TextData.cashAmount)).ToString();
                                }

                                if (double.Parse(capital) >= 0)
                                {
                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.cashAmount + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                            }
                            
                            GetSetData.SaveLogHistoryDetails("Add New Charity Payment Form", "Saving charity payment [" + TextData.date + "  " + TextData.time + "] details", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please enter less amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the payment amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter full name!");
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
                refresh();
            }
        }

        private bool Update_records()
        {
            try
            {
                fillVariableValues();

                if (txtFullName.Text != "")
                {
                    if (txtAmount.Text != "")
                    {
                        if (txtLessAmount.Text != "")
                        {
                            TextData.lessAmount = double.Parse(txtLessAmount.Text);
                            TextData.netProfit = double.Parse(txtNetProfit.Text);
                            TextData.balance = double.Parse(txtBalance.Text);

                            GetSetData.query = @"select amount from pos_charityDetails where (paymentDate = '" + TextData.date + "') and (time = '" + TextData.time + "');";
                            double previousPaidAmount = data.SearchNumericValuesDb(GetSetData.query);
                            //*****************************************************************************************

                            string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                            GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");
                            //*****************************************************************************************

                            GetSetData.query = @"Update pos_charityDetails set formDate = '" + TextData.fromDate + "', toDate = '" + TextData.toDate + "', paymentDate = '" + txtPaymentDate.Text + "', time = '" + txtTime.Text + "', fullName = '" + TextData.fullName + "', fatherName = '" + TextData.fatherName + "', mobile_no = '" + TextData.mobileNo + "', amount = '" + TextData.cashAmount + "', reference = '" + TextData.references + "',  note = '" + TextData.remarks + "', lessAmount = '" + TextData.lessAmount.ToString() + "', netProfit = '" + TextData.netProfit.ToString() + "', balance = '" + TextData.balance.ToString() + "' where (paymentDate = '" + TextData.date + "') and (time = '" + TextData.time +"');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            //*****************************************************************************************

                            if (GetSetData.Data == "Yes")
                            {
                                if (capital != "NULL" && capital != "")
                                {
                                    TextData.cashAmount = ((double.Parse(capital) + previousPaidAmount) - double.Parse(TextData.cashAmount)).ToString();
                                }

                                if (double.Parse(TextData.cashAmount) >= 0)
                                {
                                    GetSetData.query = "update pos_capital set total_capital = '" + TextData.cashAmount + "';";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                }
                            }

                            GetSetData.SaveLogHistoryDetails("Add New Charity Payment Form", "Updating charity payment details [" + TextData.date + "  " + TextData.time + "]", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please enter less amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the payment amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter full name!");
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
            }
        }

        private void formCharityPayment_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void refresh()
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                txtPaymentDate.Text = DateTime.Now.ToLongDateString();
                txtTime.Text = DateTime.Now.ToLongTimeString();
                txtFullName.Text = "";
                txtFatherName.Text = "";
                txtContactNo.Text = "";
                txtAmount.Text = "0";
                txtReference.Text = "";
                txtRemarks.Text = "";
                txtLessAmount.Text = "5";
                txtNetProfit.Text = "0";
                txtBalance.Text = "0";

                system_user_permissions();
                enableSaveButton();
                FromDate.Focus();
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
                // *******************************************************************************************

                string checkInvestorProfit_db = data.UserPermissions("investorProfit", "pos_general_settings");
                string lessAmount_db = data.UserPermissions("lessAmount", "pos_general_settings");

                if (checkInvestorProfit_db == "W_Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }
                else if (checkInvestorProfit_db == "Sale - W_Sale")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_WholeSale) - double.Parse(Return_WholeSale));
                }
                else if (checkInvestorProfit_db == "Sale - Pur")
                {
                    GetSetData.numericValue = (double.Parse(Sale_SalePrice) - double.Parse(Return_SalePrice)) - (double.Parse(Sale_PurchasePrice) - double.Parse(Return_PurchasePrice));
                }

                if (GetSetData.numericValue >= 0)
                {
                    TextData.netProfit = (GetSetData.numericValue * double.Parse(lessAmount_db)) / 100;
                    //GetSetData.numericValue -= TextData.netProfit;
                    txtNetProfit.Text = Math.Round(TextData.netProfit, 4).ToString();
                }

                //txtCapitalAmount.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadCapitalAndProfitDetails();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAmount.Text, e);
        }

        private void txtLessAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtLessAmount.Text, e);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.payment = 0;
                TextData.netProfit = 0;

                if (txtAmount.Text != "")
                {
                    TextData.payment = double.Parse(txtAmount.Text);
                    TextData.netProfit = double.Parse(txtNetProfit.Text);

                    TextData.netProfit -= TextData.payment;

                    if (TextData.netProfit >= 0)
                    {
                        txtBalance.Text = TextData.netProfit.ToString();
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
