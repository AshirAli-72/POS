using System;
using System.IO.Ports;
using System.Windows.Forms;
using Login_info.controllers;
using Message_box_info.forms;
using Datalayer;
using Customers_info.forms;
using RefereningMaterial;
using DataModel.Cash_Drawer_Data_Classes;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace CounterSales_info.forms
{
    public partial class form_payment : Form
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


        public form_payment()
        {
            InitializeComponent();
            //setFormColorsDynamically();
        }

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        private SerialPort serialPort;
        int selectTextBox = 0; // 0 for cash received, 1 for cash on hand
        string creditCardApiAmount = "0";
        double cashAmount = 0;
        public static bool isReturned = false;
        public static string advancePaidAmount = "0";
        public static string tipAmount = "0";
        double CC_SplitedAmount = 0;

        private string creditCardLinkedWithAPI()
        {
            try
            {
                string api_url = data.getApiRequestDB("api_url", "pos_credit_card_api_settings");
                string register_id = data.getApiRequestDB("register_id", "pos_credit_card_api_settings");
                string authentication_key = data.getApiRequestDB("authentication_key", "pos_credit_card_api_settings");

                if (api_url != "" && register_id != "" && authentication_key != "")
                {
                    TextData.billNo = auto_generate_sales_code();

                    if (TextData.billNo != "")
                    {
                        decimal amountDue = decimal.Parse(creditCardApiAmount); // Example amount, you can set this dynamically
                        decimal amount = amountDue + 0.00m;
                        decimal salesmanTip = 0;

                        if (txtTipAmount.Text != "")
                        {
                            salesmanTip = decimal.Parse(txtTipAmount.Text); // Example amount, you can set this dynamically
                        }
                   
                        decimal totalTip = salesmanTip + 0.00m;
                        string xmlRequest = "";
                       
                        if (isReturned)
                        {
                            xmlRequest = $@"<request>
                                        <PaymentType>Credit</PaymentType>
                                        <TransType>Return</TransType>
                                        <Amount>{amount}</Amount>
                                        <Tip>{totalTip}</Tip>
                                        <InvNum>1</InvNum>
                                        <RefId>{TextData.billNo}</RefId>
                                        <RegisterId>{register_id}</RegisterId>
                                        <AuthKey>{authentication_key}</AuthKey>
                                        <PrintReceipt>Merchant</PrintReceipt>
                                        <SigCapture>No</SigCapture>
                                        </request>";
                        }
                        else
                        {
                            xmlRequest = $@"<request>
                                        <PaymentType>Credit</PaymentType>
                                        <TransType>Sale</TransType>
                                        <Amount>{amount}</Amount>
                                        <Tip>0.00</Tip>
                                        <InvNum>1</InvNum>
                                        <RefId>{TextData.billNo}</RefId>
                                        <RegisterId>{register_id}</RegisterId>
                                        <AuthKey>{authentication_key}</AuthKey>
                                        <PrintReceipt>Merchant</PrintReceipt>
                                        <SigCapture>No</SigCapture>
                                        </request>";
                        }
                      

                        //string url = "https://spinpos.net/spin/cgi.html?TerminalTransaction=" + Uri.EscapeUriString(xmlRequest);
                        string url = api_url + Uri.EscapeUriString(xmlRequest);

                        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            using (Stream dataStream = response.GetResponseStream())
                            {
                                StreamReader reader = new StreamReader(dataStream);
                                string responseFromServer = reader.ReadToEnd();

                                // Display the response in a message box
                                //MessageBox.Show(responseFromServer, "Response");

                                string transactionStatus = ParseTransactionStatus(responseFromServer);

                                //MessageBox.Show(transactionStatus, "Transaction Status");

                                return transactionStatus;
                            }
                        }
                    }
                }
                else
                {
                    error.errorMessage("transaction failed. Please try again to record sale.");
                    error.ShowDialog();

                    return "";
                }

                return "";
            }
            catch (WebException ex)
            {
                // Handle any errors
                MessageBox.Show($"Request error: {ex.Message}", "Error");

                //error.errorMessage("Sorry unable to connect with the terminal. Please try again.");
                //error.ShowDialog();

                return "";
            }
        }

        //private string creditCardLinkedWithAPI()
        //{
        //    try
        //    {
        //        TextData.billNo = auto_generate_sales_code();

        //        string api_url = data.UserPermissions("api_url", "pos_credit_card_api_settings");
        //        string register_id = data.UserPermissions("register_id", "pos_credit_card_api_settings");
        //        string authentication_key = data.UserPermissions("authentication_key", "pos_credit_card_api_settings");

        //        decimal amountDue = decimal.Parse(creditCardApiAmount); // Example amount, you can set this dynamically
        //        decimal amount = amountDue + 0.00m;

        //        string xmlRequest = $@"<request>
        //                         <PaymentType>Credit</PaymentType>
        //                         <TransType>Sale</TransType>
        //                         <Amount>{amount}</Amount>
        //                         <Tip>0.00</Tip>
        //                         <InvNum>1</InvNum>
        //                         <RefId>{TextData.billNo}</RefId>
        //                         <RegisterId>{register_id}</RegisterId>
        //                         <AuthKey>{authentication_key}</AuthKey>
        //                         <PrintReceipt>Merchant</PrintReceipt>
        //                         <SigCapture>No</SigCapture>
        //                         </request>";

        //        string url = api_url + Uri.EscapeUriString(xmlRequest);

        //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Method = "GET";
        //        request.Timeout = 60000; // 10 seconds timeout

        //        int retryCount = 3;
        //        int delay = 6000; // 2 seconds

        //        for (int i = 0; i < retryCount; i++)
        //        {
        //            try
        //            {
        //                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //                {
        //                    using (Stream dataStream = response.GetResponseStream())
        //                    {
        //                        StreamReader reader = new StreamReader(dataStream);
        //                        string responseFromServer = reader.ReadToEnd();
        //                        string transactionStatus = ParseTransactionStatus(responseFromServer);
        //                        return transactionStatus;
        //                    }
        //                }
        //            }
        //            catch (WebException ex)
        //            {
        //                if (i == retryCount - 1)
        //                {
        //                    throw; // Rethrow exception if all retries fail
        //                }
        //                Thread.Sleep(delay); // Wait before retrying
        //            }
        //        }

        //        return "";
        //    }
        //    catch (WebException ex)
        //    {
        //        MessageBox.Show($"Request error: {ex.Message}", "Error");
        //        //error.errorMessage("Sorry unable to connect with the terminal. Please try again.");
        //        //error.ShowDialog();
        //        return "";
        //    }
        //}

        private string ParseTransactionStatus(string responseFromServer)
        {
            // Convert the response to lowercase for case-insensitive comparison
            string responseLower = responseFromServer.ToLower();
            
            // Check if the response contains "approve" or "cancel"
            if (responseLower.Contains("approve"))
            {
                GetSetData.query = @"insert into pos_credit_card_history values ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', '" + TextData.billNo + "', 'Transaction Approved', '', '" + TextData.customerId +"', '" + TextData.employeeId +"', '" + TextData.clockInId +"');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                return "Transaction Approved";
            }
            else if (responseLower.Contains("cancel"))
            {
                GetSetData.query = @"insert into pos_credit_card_history values ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', '" + TextData.billNo + "', 'Transaction Canceled', '', '" + TextData.customerId + "', '" + TextData.employeeId + "', '" + TextData.clockInId + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
            
                return "Transaction Canceled";
            }
            else
            {
                // Log the raw response for debugging purposes
                //GetSetData.query = @"insert into pos_credit_card_history values ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', '" + TextData.billNo + "', 'Connection timeout. Please check the credit card machine and try again', '', '" + TextData.customerId + "', '" + TextData.employeeId + "', '" + TextData.clockInId + "');";
                //data.insertUpdateCreateOrDelete(GetSetData.query);
                
                GetSetData.query = @"insert into pos_credit_card_history values ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', '" + TextData.billNo + "', 'Transaction Canceled. Service busy please try again later.', '', '" + TextData.customerId + "', '" + TextData.employeeId + "', '" + TextData.clockInId + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                // Return a generic error message if the status cannot be determined
                return "Unable to determine transaction status";
            }
        }

        #region

        //private void btnCreditCardAPI_Click(object sender, EventArgs e)
        //{132691002, 8RDbHYirQz
        //    decimal amountDue = decimal.Parse(txt_due_amount.Text); // Example amount, you can set this dynamically
        //    decimal amount = amountDue + 0.00m;

        //    string xmlRequest = $@"<request>
        //                            <PaymentType>Credit</PaymentType>
        //                            <TransType>Sale</TransType>
        //                            <Amount>{amount}</Amount>
        //                            <Tip>0.00</Tip>
        //                            <InvNum>1</InvNum>
        //                            <RefId>8</RefId>
        //                            <RegisterId>132691002</RegisterId>
        //                            <AuthKey>cdPaPSZbmq</AuthKey>
        //                            <PrintReceipt>Merchant</PrintReceipt>
        //                            <SigCapture>No</SigCapture>
        //                        </request>";

       
        //    string url = "https://spinpos.net/spin/cgi.html?TerminalTransaction=" + Uri.EscapeUriString(xmlRequest);

        //    try
        //    {
        //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Method = "GET";

        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream dataStream = response.GetResponseStream())
        //            {
        //                StreamReader reader = new StreamReader(dataStream);
        //                string responseFromServer = reader.ReadToEnd();

        //                // Display the response in a message box
        //                //MessageBox.Show(responseFromServer, "Response");

        //                string transactionStatus = ParseTransactionStatus(responseFromServer);
        //                MessageBox.Show(transactionStatus, "Transaction Status");
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // Handle any errors
        //        MessageBox.Show($"Request error: {ex.Message}", "Error");
        //    }
        //}

        //private void btnCreditCardAPI_Click(object sender, EventArgs e)
        //{
        //    decimal amountDue = decimal.Parse(txt_due_amount.Text); // Example amount, you can set this dynamically
        //    decimal amount = amountDue + 0.00m;

        //    string xmlRequest = $@"<request>
        //                            <PaymentType>Credit</PaymentType>
        //                            <TransType>Sale</TransType>
        //                            <Amount>{amount}</Amount>
        //                            <Tip>0.00</Tip>
        //                            <InvNum>1</InvNum>
        //                            <RefId>2</RefId>
        //                            <RegisterId>132691002</RegisterId>
        //                            <AuthKey>cdPaPSZbmq</AuthKey>
        //                            <PrintReceipt>Merchant</PrintReceipt>
        //                            <SigCapture>No</SigCapture>
        //                        </request>";

        //    string url = "https://spinpos.net/spin/cgi.html?TerminalTransaction=" + Uri.EscapeUriString(xmlRequest);

        //    try
        //    {
        //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Method = "GET";

        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream dataStream = response.GetResponseStream())
        //            {
        //                StreamReader reader = new StreamReader(dataStream);
        //                string responseFromServer = reader.ReadToEnd();

        //                // Display the response in a message box
        //                MessageBox.Show(responseFromServer, "Response");
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // Handle any errors
        //        MessageBox.Show($"Request error: {ex.Message}", "Error");
        //    }
        //}
        #endregion

        private  void autoOpenCashDrawer()
        {
            try
            {
                CashDrawerData.OpenDrawer(generalSettings.ReadField("default_printer"), generalSettings.ReadField("printer_model"));
            }
            catch (Exception es)
            {
                error.errorMessage("Error opening cash drawer: " + es.Message);
                error.ShowDialog();
            }
        }

        private void btnCashDrawer_Click(object sender, EventArgs e)
        {
            autoOpenCashDrawer();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Counter Cash Payment Form", "Exit...", role_id);
            TextData.feedbackAmountDue = "";
            TextData.feedbackChangeAmount = "0";
            TextData.feedbackChangeAmount = "0";
           
            TextData.aknowledged = "";
            TextData.invoiceAmountDue = "";
            TextData.tipAmount = "0";

            this.Close();
            TextData.showPopUpForm = false;
        }

        private void setCurrency()
        {
            try
            {
                TextData.general_options = data.UserPermissions("currency", "pos_general_settings");


                button15.Text = TextData.general_options + "1";
                button16.Text = TextData.general_options + "2";
                button17.Text = TextData.general_options + "5";
                button18.Text = TextData.general_options + "10";
                button19.Text = TextData.general_options + "20";
                button20.Text = TextData.general_options + "50";
                guna2Button2.Text = TextData.general_options + "100";
                button22.Text = TextData.general_options + "200";

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_payment_Load(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);

                if (generalSettings.ReadField("directly_print_receipt") == "Yes")
                {
                    chk_print_receipt.Checked = true;
                }

                if (generalSettings.ReadField("auto_open_cash_drawer") == "Yes")
                {
                    chkCashDrawer.Checked = true;
                }


                if (TextData.returnStatus == "Return")
                {
                    btn_credits.Enabled = false;
                }


                //txt_due_amount.Text = TextData.net_total.ToString();
                //CC_SplitedAmount = TextData.net_total;

                txtAdvancePaidAmount.Text = advancePaidAmount;
              
                txtTotalAmount.Text = Math.Round(TextData.net_total, 2).ToString();
                txt_due_amount.Text = Math.Round(TextData.net_total - double.Parse(advancePaidAmount), 2).ToString();
                CC_SplitedAmount = Math.Round(TextData.net_total - double.Parse(advancePaidAmount), 2);

                txtTipAmount.Text = tipAmount;

                if (generalSettings.ReadField("salesmanTips") != "Yes")
                {
                    txtTipAmount.ReadOnly = false;
                    txtTipAmount.Text = "0.00";
                }


                setCurrency();

                txt_cash.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void cash_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_cash.Text, e);
        }

        private void txtTipAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtTipAmount.Text, e);
        }

        private void txt_on_hand_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_on_hand.Text, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "0";
            }
            else
            {
                txt_on_hand.Text += "0";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "00";
            }
            else
            {
                txt_on_hand.Text += "00";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += ".";
            }
            else
            {
                txt_on_hand.Text += ".";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "1";
            }
            else
            {
                txt_on_hand.Text += "1";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "2";
            }
            else
            {
                txt_on_hand.Text += "2";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "3";
            }
            else
            {
                txt_on_hand.Text += "3";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "4";
            }
            else
            {
                txt_on_hand.Text += "4";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "5";
            }
            else
            {
                txt_on_hand.Text += "5";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "6";
            }
            else
            {
                txt_on_hand.Text += "6";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "7";
            }
            else
            {
                txt_on_hand.Text += "7";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "8";
            }
            else
            {
                txt_on_hand.Text += "8";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text += "9";
            }
            else
            {
                txt_on_hand.Text += "9";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectTextBox == 0)
            {
                txt_cash.Text = "";
                lblRemaningAmount.Text = "0";
            }
            else
            {
                txt_on_hand.Text = "0";
                txt_remaining.Text = "0";
            }

          
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {
                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 1;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 1;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {
                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 2;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 2;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {

                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 5;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 5;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {

                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 10;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 10;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {

                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 20;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 20;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {

                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 50;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 50;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (selectTextBox == 0)
            {
                if (txt_cash.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_cash.Text);
                }

                TextData.cash = TextData.totalAmount + 100;
                txt_cash.Text = TextData.cash.ToString();
            }
            else
            {
                if (txt_on_hand.Text != "")
                {
                    TextData.totalAmount = double.Parse(txt_on_hand.Text);
                }

                TextData.cash = TextData.totalAmount + 100;
                txt_on_hand.Text = TextData.cash.ToString();
            }

            fun_cash_button("Cash");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            TextData.totalAmount = 0;

            if (txt_cash.Text != "")
            {
                TextData.totalAmount = double.Parse(txt_cash.Text);
            }

            TextData.cash = TextData.totalAmount + 200;
            txt_cash.Text = TextData.cash.ToString();
        }

        //private void ExportThreadMethod(Action<string> callback)
        //{
        //    try
        //    {
        //        if (creditCardLinkedWithAPI() == "Transaction Approved")
        //        {
        //            // Invoke callback on UI thread
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                callback("Transaction Approved");
        //            });
        //        }
        //        else if (creditCardLinkedWithAPI() == "Transaction Canceled")
        //        {
        //            // Invoke callback on UI thread
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                callback("Transaction Canceled");
        //            });
        //        }
        //        else
        //        {
        //            //error.errorMessage("No connection made. Please check the credit card machine and try again.");
        //            //error.ShowDialog();

        //            // Invoke callback on UI thread
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                //callback("No connection made. Please check the credit card machine and try again.");
        //                callback("Transaction Canceled. Service busy please try again later.");
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            //callback("Transaction has been terminated.");
        //            callback("An error occurred: " + ex.Message);
        //        });
        //    }
        //}
        //private void ExportThreadMethod(Action<string> callback)
        //{
        //    try
        //    {
        //        if (creditCardLinkedWithAPI() == "Transaction Approved")
        //        {
        //            // Ensure handle is created before invoking
        //            if (this.IsHandleCreated)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    callback("Transaction Approved");
        //                });
        //            }
        //        }
        //        else if (creditCardLinkedWithAPI() == "Transaction Canceled")
        //        {
        //            // Ensure handle is created before invoking
        //            if (this.IsHandleCreated)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    callback("Transaction Canceled");
        //                });
        //            }
        //        }
        //        else
        //        {
        //            // Ensure handle is created before invoking
        //            if (this.IsHandleCreated)
        //            {
        //                this.Invoke((MethodInvoker)delegate
        //                {
        //                    callback("Transaction Canceled. Service busy please try again later.");
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ensure handle is created before invoking
        //        if (this.IsHandleCreated)
        //        {
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                callback("An error occurred: " + ex.Message);
        //            });
        //        }
        //        else
        //        {
        //            // Handle the case where the handle is not created, e.g., log the error
        //            // You may also use another method to communicate the error if the handle is not created
        //            Console.WriteLine("An error occurred: " + ex.Message);
        //        }
        //    }
        //}

        private void ExportThreadMethod(Action<string> callback)
        {
            try
            {
                // Ensure the control's handle is created
                this.CreateControl();

                if (creditCardLinkedWithAPI() == "Transaction Approved")
                {
                    // Invoke callback on UI thread
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        callback("Transaction Approved");
                    });
                }
                else if (creditCardLinkedWithAPI() == "Transaction Canceled")
                {
                    // Invoke callback on UI thread
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        callback("Transaction Canceled");
                    });
                }
                else
                {
                    // Invoke callback on UI thread
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        callback("Transaction Canceled. Service busy please try again later.");
                    });
                }
            }
            catch (Exception ex)
            {
                // Ensure handle is created before invoking
                this.CreateControl();

                if (this.IsHandleCreated)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        callback("An error occurred: " + ex.Message);
                    });
                }
                else
                {
                    // Handle the case where the handle is not created, e.g., log the error
                    // You may also use another method to communicate the error if the handle is not created
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        private void fun_cash_button(string payment_type)
        {
            try
            {
                TextData.totalAmount = 0;
                TextData.aknowledged = "Cash";
                TextData.checkSaleStatus = payment_type;
                TextData.send_cash = "";
                TextData.remaining_amount = txt_remaining.Text;
                TextData.totalAmount = double.Parse(txt_due_amount.Text);
                TextData.credit_card_amount = 0;
                TextData.paypal_amount = 0;
                TextData.google_pay_amount = 0;
                TextData.showPopUpForm = true;


                if (chk_print_receipt.Checked == true)
                {
                    TextData.checkPrintReport = true;
                }
                else
                {
                    TextData.checkPrintReport = false;
                }

                if (chkCashDrawer.Checked)
                {
                    TextData.checkAutoOpenCashDrawer = true;
                }
                else
                {
                    TextData.checkAutoOpenCashDrawer = false;
                }


                if (chk_print_a4_receipt.Checked == true)
                {
                    TextData.checkPrintA4Report = true;
                }
                else
                {
                    TextData.checkPrintA4Report = false;
                }


                if (txt_on_hand.Text != "")
                {
                    TextData.cash_on_hand = txt_on_hand.Text;
                }
                else
                {
                    TextData.cash_on_hand = "0";
                }

                if (txt_cash.Text != "")
                {
                    if (payment_type != "Cash")
                    {
                        string isCreditCardConnected = data.UserPermissions("isCreditCardConnected", "pos_general_settings");
                     
                        if ((cashAmount == double.Parse(txt_cash.Text)) || (isCreditCardConnected == "Yes"))
                        {
                            TextData.send_cash = txt_cash.Text;
                            TextData.advanceAmount = double.Parse(txt_cash.Text);


                            if (payment_type == "Credit Card")
                            {
                                if (isCreditCardConnected == "No")
                                {
                                    TextData.billNo = auto_generate_sales_code();

                                    if (TextData.billNo != "")
                                    {
                                        TextData.credit_card_amount = TextData.totalAmount - double.Parse(TextData.send_cash);
                                        creditCardApiAmount = TextData.credit_card_amount.ToString();

                                        TextData.checkSaleStatus = "Cash, Credit Card";

                                        TextData.credits = 0;

                                        TextData.remaining_amount = txt_remaining.Text;
                                        TextData.checkAutoOpenCashDrawer = false;

                                        TextData.tipAmount = "0";

                                        if (txtTipAmount.Text != "")
                                        {
                                            TextData.tipAmount = txtTipAmount.Text;
                                        }

                                        this.Close();
                                    }
                                    else
                                    {
                                        error.errorMessage("transaction failed. Please try again to record sale.");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    TextData.billNo = auto_generate_sales_code();

                                    if (TextData.billNo != "")
                                    {
                                        //?????????????????????????????

                                        if (CC_SplitedAmount != 0)
                                        {
                                            lblRemaningAmount.Text = (Math.Round(CC_SplitedAmount - double.Parse(txt_cash.Text), 2)).ToString();
                                            lblChangeAmount.Text = "Remaining:";

                                            //**********************************************************

                                            if (TextData.customer_name != "")
                                            {
                                                TextData.credits = 0;
                                            }
                                            else
                                            {
                                                TextData.customer_name = "nill";
                                                GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                                                TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                                                TextData.credits = 0;
                                            }

                                            TextData.remaining_amount = txt_remaining.Text;

                                            //?????????????????????????????

                                            creditCardApiAmount = txt_cash.Text;

                                            btnCreditCard.Enabled = false;

                                            form_loading loadingForm = new form_loading();
                                            loadingForm.SetLoadingMessage("Transaction is in process......");
                                            loadingForm.TopMost = true;
                                            loadingForm.Show();

                                            // Execute exportDataToDrive in a separate thread

                                            Thread exportThread = new Thread(() => ExportThreadMethod((message) =>
                                            {
                                                // Invoke the loadingForm.Close on the main UI thread
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    loadingForm.Dispose();


                                                    if (message == "Transaction Approved")
                                                    {
                                                        done.DoneMessage(message);
                                                        done.ShowDialog();

                                                        CC_SplitedAmount = (Math.Round(CC_SplitedAmount - double.Parse(txt_cash.Text), 2));

                                                        if (CC_SplitedAmount == 0)
                                                        {
                                                            TextData.checkAutoOpenCashDrawer = false;
                                                            TextData.credit_card_amount = TextData.totalAmount;
                                                            TextData.send_cash = "0";
                                                            TextData.tipAmount = "0";

                                                            if (txtTipAmount.Text != "")
                                                            {
                                                                TextData.tipAmount = txtTipAmount.Text;
                                                            }

                                                            this.Close();
                                                        }
                                                        else
                                                        {
                                                            btnCreditCard.Enabled = true;
                                                        }
                                                    }
                                                    else if (message == "Transaction Canceled")
                                                    {
                                                        error.errorMessage(message);
                                                        error.ShowDialog();

                                                        btnCreditCard.Enabled = true;
                                                    }
                                                    else
                                                    {
                                                        //error.errorMessage("Connection timeout. Please check the credit card machine and try again.");
                                                        //error.ShowDialog();
                                                        error_form error1 = new error_form();
                                                        error1.errorMessage("Transaction Canceled. Service busy please try again later.");
                                                        error1.TopMost = true;
                                                        error1.ShowDialog();


                                                        btnCreditCard.Enabled = true;
                                                    }
                                                });
                                            }));

                                            exportThread.Start();

                                            TextData.checkSaleStatus = "Cash, Credit Card";
                                            TextData.showPopUpForm = false;
                                        }
                                        else
                                        {
                                            TextData.checkAutoOpenCashDrawer = false;
                                            TextData.credit_card_amount = TextData.totalAmount;
                                            TextData.send_cash = "0";
                                            TextData.tipAmount = "0";

                                            if (txtTipAmount.Text != "")
                                            {
                                                TextData.tipAmount = txtTipAmount.Text;
                                            }

                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("transaction failed. Please try again to record sale.");
                                        error.ShowDialog();
                                    }
                                }
                            }
                        }
                        else
                        {
                            error.errorMessage("Please click on cash button to receive cash amount!");
                            error.ShowDialog();
                        }


                        if (payment_type == "Apple Pay")
                        {
                            TextData.billNo = auto_generate_sales_code();

                            if (TextData.billNo != "")
                            {
                                TextData.paypal_amount = TextData.totalAmount - double.Parse(TextData.send_cash);

                                TextData.checkSaleStatus = "Cash, Apple Pay";
                                TextData.credits = 0;

                                TextData.remaining_amount = txt_remaining.Text;
                                TextData.checkAutoOpenCashDrawer = false;
                                TextData.tipAmount = "0";

                                if (txtTipAmount.Text != "")
                                {
                                    TextData.tipAmount = txtTipAmount.Text;
                                }

                                this.Close();
                            }
                            else
                            {
                                error.errorMessage("transaction failed. Please try again to record sale.");
                                error.ShowDialog();
                            }
                        }

                        if (payment_type == "Zelle-Cashapp-Venmo")
                        {
                            TextData.billNo = auto_generate_sales_code();

                            if (TextData.billNo != "")
                            {
                                TextData.google_pay_amount = TextData.totalAmount - double.Parse(TextData.send_cash);

                                TextData.checkSaleStatus = "Cash, Zelle-Cashapp-Venmo";

                                TextData.credits = 0;

                                TextData.remaining_amount = txt_remaining.Text;
                                TextData.checkAutoOpenCashDrawer = false;
                                TextData.tipAmount = "0";

                                if (txtTipAmount.Text != "")
                                {
                                    TextData.tipAmount = txtTipAmount.Text;
                                }

                                this.Close();
                            }
                            else
                            {
                                error.errorMessage("transaction failed. Please try again to record sale.");
                                error.ShowDialog();
                            }
                        }

                        //TextData.customer_name = "nill";
                        //GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                        //TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);


                    }
                    else
                    {
                        TextData.advanceAmount = double.Parse(txt_cash.Text);
                       

                        //**********************************************************

                        if (txt_cash.Text != "" && txt_due_amount.Text != "")
                        {
                            if (double.Parse(txt_cash.Text) >= double.Parse(txt_due_amount.Text))
                            {
                                lblRemaningAmount.Text = (Math.Round(double.Parse(txt_cash.Text) - double.Parse(txt_due_amount.Text), 2)).ToString();
                                txt_remaining.Text = lblRemaningAmount.Text;

                                lblChangeAmount.Text = "Change:";

                                //**********************************************************

                                if (TextData.customer_name != "")
                                {
                                    TextData.credits = 0;
                                }
                                else
                                {
                                    TextData.customer_name = "nill";
                                    GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                                    TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                                    TextData.credits = 0;
                                }


                                if (payment_type == "Credit Card")
                                {
                                    string isCreditCardConnected = data.UserPermissions("isCreditCardConnected", "pos_general_settings");


                                    if (isCreditCardConnected == "Yes")
                                    {
                                        creditCardApiAmount = txt_due_amount.Text;

                                        btnCreditCard.Enabled = false;

                                        form_loading loadingForm = new form_loading();
                                        loadingForm.SetLoadingMessage("Transaction is in process......");
                                        loadingForm.TopMost = true;
                                        loadingForm.Show();

                                        // Execute exportDataToDrive in a separate thread
                                        Thread exportThread = new Thread(() => ExportThreadMethod((message) =>
                                        {
                                            // Invoke the loadingForm.Close on the main UI thread
                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                loadingForm.Dispose();


                                                if (message == "Transaction Approved")
                                                {
                                                    TextData.checkAutoOpenCashDrawer = false;
                                                    TextData.tipAmount = "0";

                                                    if (txtTipAmount.Text != "")
                                                    {
                                                        TextData.tipAmount = txtTipAmount.Text;
                                                    }
                                                    done.DoneMessage(message);
                                                    done.ShowDialog();

                                                    this.Close();
                                                }
                                                else if (message == "Transaction Canceled")
                                                {
                                                    error.errorMessage(message);
                                                    error.ShowDialog();

                                                    btnCreditCard.Enabled = true;
                                                }
                                                else
                                                {
                                                    //error.errorMessage("Connection timeout. Please check the credit card machine and try again.");
                                                    //error.ShowDialog();
                                                    error_form error1 = new error_form();
                                                    error1.errorMessage("Transaction Canceled. Service busy please try again later.");
                                                    error1.TopMost = true;
                                                    error1.ShowDialog();

                                                    btnCreditCard.Enabled = true;
                                                }
                                            });
                                        }));

                                        exportThread.Start();
                                      
                                        TextData.credit_card_amount = TextData.totalAmount;
                                        TextData.send_cash = "0";
                                        TextData.showPopUpForm = false;
                                    }
                                    else
                                    {
                                        TextData.billNo = auto_generate_sales_code();

                                        if (TextData.billNo != "")
                                        {
                                            TextData.credit_card_amount = TextData.totalAmount;

                                            TextData.send_cash = "0";

                                            TextData.remaining_amount = txt_remaining.Text;

                                            TextData.checkAutoOpenCashDrawer = false;

                                            TextData.tipAmount = "0";

                                            if (txtTipAmount.Text != "")
                                            {
                                                TextData.tipAmount = txtTipAmount.Text;
                                            }

                                            this.Close();
                                        }
                                        else
                                        {
                                            error.errorMessage("transaction failed. Please try again to record sale.");
                                            error.ShowDialog();
                                        }
                                    }
                                }
                                else if (payment_type == "Apple Pay")
                                {
                                    TextData.billNo = auto_generate_sales_code();

                                    if (TextData.billNo != "")
                                    {
                                        TextData.paypal_amount = TextData.totalAmount;

                                        TextData.send_cash = "0";

                                        TextData.remaining_amount = txt_remaining.Text;

                                        TextData.checkAutoOpenCashDrawer = false;

                                        TextData.tipAmount = "0";

                                        if (txtTipAmount.Text != "")
                                        {
                                            TextData.tipAmount = txtTipAmount.Text;
                                        }

                                        this.Close();
                                    }
                                    else
                                    {
                                        error.errorMessage("transaction failed. Please try again to record sale.");
                                        error.ShowDialog();
                                    }
                                }
                                else if (payment_type == "Zelle-Cashapp-Venmo")
                                {
                                    TextData.billNo = auto_generate_sales_code();

                                    if (TextData.billNo != "")
                                    {
                                        TextData.google_pay_amount = TextData.totalAmount;

                                        TextData.send_cash = "0";

                                        TextData.remaining_amount = txt_remaining.Text;

                                        TextData.checkAutoOpenCashDrawer = false;

                                        TextData.tipAmount = "0";

                                        if (txtTipAmount.Text != "")
                                        {
                                            TextData.tipAmount = txtTipAmount.Text;
                                        }

                                        this.Close();
                                    }
                                    else
                                    {
                                        error.errorMessage("transaction failed. Please try again to record sale.");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    TextData.billNo = auto_generate_sales_code();

                                    if (TextData.billNo != "")
                                    {
                                        TextData.send_cash = "";

                                        TextData.remaining_amount = txt_remaining.Text;

                                        TextData.checkAutoOpenCashDrawer = false;
                                     
                                        if (chkCashDrawer.Checked)
                                        {
                                            TextData.checkAutoOpenCashDrawer = true;
                                        }

                                        TextData.tipAmount = "0";

                                        if (txtTipAmount.Text != "")
                                        {
                                            TextData.tipAmount = txtTipAmount.Text;
                                        }

                                        this.Close();
                                    }
                                    else
                                    {
                                        error.errorMessage("transaction failed. Please try again to record sale.");
                                        error.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                TextData.checkAutoOpenCashDrawer = false;

                                if (chkCashDrawer.Checked)
                                {
                                    autoOpenCashDrawer();
                                }


                                lblRemaningAmount.Text = (Math.Round(double.Parse(txt_due_amount.Text) - double.Parse(txt_cash.Text), 2)).ToString();
                                lblChangeAmount.Text = "Remaining:";

                                //**********************************************************

                                if (TextData.customer_name != "")
                                {
                                    TextData.credits = 0;
                                }
                                else
                                {
                                    TextData.customer_name = "nill";
                                    GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                                    TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                                    TextData.credits = 0;
                                }

                                TextData.remaining_amount = txt_remaining.Text;


                                done.DoneMessage("Amount successfully received.");
                                done.ShowDialog();

                                cashAmount = double.Parse(txt_cash.Text);
                            }
                        }
                        else
                        {
                            lblRemaningAmount.Text = "0.00";
                        }

                        #region

                        //done.DoneMessage("Cash amount received successfully.");
                        //done.ShowDialog();

                        //if (TextData.customer_name != "" && TextData.customer_name != "nill")
                        //{
                        //    TextData.credits_limit = 0;
                        //    TextData.lastCredit = 0;

                        //    GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                        //    int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        //    TextData.credits_limit = data.NumericValues("credit_limit", "pos_customers", "customer_id", customer_id_db.ToString());
                        //    TextData.lastCredit = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());


                        //    TextData.credits = TextData.totalAmount - double.Parse(TextData.send_cash);

                        //    TextData.lastCredit += TextData.credits;

                        //    if (TextData.lastCredit <= TextData.credits_limit)
                        //    {
                        //        TextData.checkSaleStatus = "Cash, Credit";

                        //        this.Close();
                        //    }
                        //    else
                        //    {
                        //        error.errorMessage(TextData.customer_name + " credit is exceeding from its Limit!");
                        //        error.ShowDialog();
                        //    }
                        //}
                        //else
                        //{
                        //    Customer_details.role_id = role_id;
                        //    Button_controls.customer_buttons();
                        //    TextData.customer_name = Customer_details.selected_customer;
                        //    TextData.customerCode = Customer_details.selected_customerCode;
                        //}

                        #endregion
                    }
                }
                else
                {
                    if (TextData.customer_name != "")
                    {
                        TextData.credits = 0;            
                    }
                    else
                    {
                        TextData.customer_name = "nill";
                        GetSetData.query = "select cus_code from pos_customers where (full_name = '" + TextData.customer_name + "');";
                        TextData.customerCode = data.SearchStringValuesFromDb(GetSetData.query);

                        TextData.credits = 0;
                    }

                    if (payment_type == "Credit Card")
                    {
                        string isCreditCardConnected = data.UserPermissions("isCreditCardConnected", "pos_general_settings");


                        if (isCreditCardConnected == "Yes")
                        {
                            btnCreditCard.Enabled = false;

                            creditCardApiAmount = txt_due_amount.Text;

                            form_loading loadingForm = new form_loading();
                            loadingForm.SetLoadingMessage("Transaction is in process......");
                            loadingForm.TopMost = true;
                            loadingForm.Show();

                            // Execute exportDataToDrive in a separate thread
                            Thread exportThread = new Thread(() => ExportThreadMethod((message) =>
                            {
                                // Invoke the loadingForm.Close on the main UI thread
                                this.Invoke((MethodInvoker)delegate
                                {
                                    loadingForm.Dispose();


                                    if (message == "Transaction Approved")
                                    {
                                        done.DoneMessage(message);
                                        done.ShowDialog();

                                        btnCreditCard.Enabled = true;

                                        TextData.tipAmount = "0";

                                        if (txtTipAmount.Text != "")
                                        {
                                            TextData.tipAmount = txtTipAmount.Text;
                                        }

                                        this.Close();
                                    }
                                    else if (message == "Transaction Canceled")
                                    {
                                        error.errorMessage(message);
                                        error.ShowDialog();

                                        btnCreditCard.Enabled = true;
                                    }
                                    else
                                    {
                                        //error.errorMessage("Connection timeout. Please check the credit card machine and try again.");
                                        //error.ShowDialog();
                                        error_form error1 = new error_form();
                                        error1.errorMessage("Transaction Canceled. Service busy please try again later.");
                                        error1.TopMost = true;
                                        error1.ShowDialog();


                                        btnCreditCard.Enabled = true;
                                    }
                                });
                            }));

                            exportThread.Start();

                            TextData.checkAutoOpenCashDrawer = false;
                            TextData.credit_card_amount = TextData.totalAmount;
                            TextData.send_cash = "0";
                            TextData.showPopUpForm = false;
                        }
                        else
                        {
                            TextData.billNo = auto_generate_sales_code();

                            if (TextData.billNo != "")
                            {
                                TextData.credit_card_amount = TextData.totalAmount;

                                TextData.send_cash = "0";

                                TextData.checkAutoOpenCashDrawer = false;

                                TextData.remaining_amount = txt_remaining.Text;

                                TextData.tipAmount = "0";

                                if (txtTipAmount.Text != "")
                                {
                                    TextData.tipAmount = txtTipAmount.Text;
                                }

                                this.Close();
                            }
                            else
                            {
                                error.errorMessage("transaction failed. Please try again to record sale.");
                                error.ShowDialog();
                            }
                        }
                    }
                    else if (payment_type == "Apple Pay")
                    {
                        TextData.billNo = auto_generate_sales_code();

                        if (TextData.billNo != "")
                        {
                            TextData.paypal_amount = TextData.totalAmount;

                            TextData.send_cash = "0";

                            TextData.checkAutoOpenCashDrawer = false;

                            TextData.remaining_amount = txt_remaining.Text;

                            TextData.tipAmount = "0";

                            if (txtTipAmount.Text != "")
                            {
                                TextData.tipAmount = txtTipAmount.Text;
                            }

                            this.Close();
                        }
                        else
                        {
                            error.errorMessage("transaction failed. Please try again to record sale.");
                            error.ShowDialog();
                        }
                    }
                    else if (payment_type == "Zelle-Cashapp-Venmo")
                    {
                        TextData.billNo = auto_generate_sales_code();

                        if (TextData.billNo != "")
                        {
                            TextData.google_pay_amount = TextData.totalAmount;

                            TextData.send_cash = "0";

                            TextData.checkAutoOpenCashDrawer = false;

                            TextData.remaining_amount = txt_remaining.Text;

                            TextData.tipAmount = "0";

                            if (txtTipAmount.Text != "")
                            {
                                TextData.tipAmount = txtTipAmount.Text;
                            }

                            this.Close();
                        }
                        else
                        {
                            error.errorMessage("transaction failed. Please try again to record sale.");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        TextData.billNo = auto_generate_sales_code();

                        if (TextData.billNo != "")
                        {
                            TextData.send_cash = "";

                            TextData.remaining_amount = txt_remaining.Text;
                            TextData.checkAutoOpenCashDrawer = false;

                            if (chkCashDrawer.Checked)
                            {
                                TextData.checkAutoOpenCashDrawer = true;
                            }

                            TextData.tipAmount = "0";

                            if (txtTipAmount.Text != "")
                            {
                                TextData.tipAmount = txtTipAmount.Text;
                            }

                            this.Close();
                        }
                        else
                        {
                            error.errorMessage("transaction failed. Please try again to record sale.");
                            error.ShowDialog();
                        }
                    }
                }

                string changeAmountPopup = data.UserPermissions("changeAmountPopUp", "pos_general_settings");

                if (changeAmountPopup == "Yes")
                {
                    TextData.feedbackAmountDue = txt_due_amount.Text;
                    TextData.feedbackChangeAmount = lblRemaningAmount.Text;

                    if (txt_cash.Text != "")
                    {
                        TextData.feedbackCashAmount = txt_cash.Text;
                    }
                    else
                    {
                        TextData.feedbackCashAmount = txt_due_amount.Text;
                    }


                    if (lblChangeAmount.Text == "Remaining:" && txt_cash.Text != "")
                    {
                        TextData.isCashPayment = false;
                    }
                    else
                    {
                        TextData.isCashPayment = true;

                        if (txt_cash.Text != "")
                        {
                            TextData.feedbackCashAmount = txt_cash.Text;
                        }
                        else
                        {
                            TextData.feedbackCashAmount = txt_due_amount.Text;
                        }
                    }
                    
                    if (payment_type == "Credit Card" && payment_type == TextData.checkSaleStatus)
                    {
                        TextData.paymentType = payment_type;
                    }
                    else
                    {
                        TextData.paymentType = TextData.checkSaleStatus;
                    }
                }
                else
                {
                    TextData.feedbackAmountDue = "";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_Cash_Click(object sender, EventArgs e)
        {
            fun_cash_button("Cash");
        }

        private void form_payment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                fun_cash_button("Cash");
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                fun_credits_button();
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnByCreditCard.Checked = true;
                btnByCash.Checked = false;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnByCash.Checked = true;
                btnByCreditCard.Checked = false;
            }
            else if (e.KeyCode == Keys.O)
            {
                chk_print_receipt.Checked = false;
            }
            else if (e.KeyCode == Keys.P)
            {
                chk_print_receipt.Checked = true;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                autoOpenCashDrawer();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TextData.tipAmount = "0";

                this.Close();
            }
        }

        private void fun_credits_button()
        {
            try
            {
                if (txt_cash.Text == "")
                {
                    if (TextData.customer_name != "" && TextData.customer_name != "nill")
                    {
                        TextData.credits_limit = 0;
                        TextData.lastCredit = 0;

                        GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                        int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        TextData.credits_limit = data.NumericValues("credit_limit", "pos_customers", "customer_id", customer_id_db.ToString());
                        TextData.lastCredit = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());


                        TextData.lastCredit += double.Parse(txt_due_amount.Text);

                        if (TextData.lastCredit <= TextData.credits_limit)
                        {
                            TextData.aknowledged = "Credits";
                            TextData.credits = double.Parse(txt_due_amount.Text);
                            TextData.send_cash = "0";
                            TextData.tipAmount = "0";

                            if (txtTipAmount.Text != "")
                            {
                                TextData.tipAmount = txtTipAmount.Text;
                            }

                            this.Close();
                        }
                        else
                        {
                            error.errorMessage(TextData.customer_name + " credit is exceeding from its Limit!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        Customer_details.role_id = role_id;
                        Button_controls.customer_buttons();
                        TextData.customer_name = Customer_details.selected_customer;
                        TextData.customerCode = Customer_details.selected_customerCode;
                    }
                }
                else
                {
                    error.errorMessage("Please click on the Cash button!");
                    error.ShowDialog();
                }

                TextData.checkSaleStatus = "Credit";

                if (chk_print_receipt.Checked == true)
                {
                    TextData.checkPrintReport = true;
                }
                else
                {
                    TextData.checkPrintReport = false;
                }

                if (chkCashDrawer.Checked)
                {
                    TextData.checkAutoOpenCashDrawer = true;
                }
                else
                {
                    TextData.checkAutoOpenCashDrawer = false;
                }

                if (chk_print_a4_receipt.Checked == true)
                {
                    TextData.checkPrintA4Report = true;
                }
                else
                {
                    TextData.checkPrintA4Report = false;
                }


                if (txt_on_hand.Text != "")
                {
                    TextData.cash_on_hand = txt_on_hand.Text;
                }
                else
                {
                    TextData.cash_on_hand = "0";
                }

                TextData.remaining_amount = txt_remaining.Text;
                TextData.showPopUpForm = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_credits_Click(object sender, EventArgs e)
        {
            fun_credits_button();
        }

        private void txt_on_hand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextData.totalAmount = double.Parse(txt_due_amount.Text);
                TextData.cash = 0;

                if (txt_on_hand.Text != "")
                {
                    TextData.cash = double.Parse(txt_on_hand.Text);
                    TextData.cash = TextData.cash - TextData.totalAmount;
                    txt_remaining.Text = TextData.cash.ToString();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnChequeDetails_Click(object sender, EventArgs e)
        {
            using (formChequeDetails add_customer = new formChequeDetails())
            {
                //GetSetData.SaveLogHistoryDetails("Counter Cash Payment Form", "Cheque details button click...", role_id);
                add_customer.ShowDialog();
            }
        }

        private void btnPaypal_Click(object sender, EventArgs e)
        {
            fun_cash_button("Apple Pay");
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            fun_cash_button("Credit Card");
        }

        private void btnGooglePay_Click(object sender, EventArgs e)
        {
            fun_cash_button("Zelle-Cashapp-Venmo");
        }

        private string auto_generate_sales_code()
        {
            TextData.billNo = "";

            try
            {
                int generateCode = 0;

                //GetSetData.query = @"SELECT top 1 salesCodes FROM pos_AllCodes order by id asc;";
                generateCode = data.UserPermissionsIds("salesCodes", "pos_AllCodes");

                if (generateCode > 0)
                {
                    generateCode = generateCode + 1;

                    GetSetData.query = @"update pos_AllCodes set salesCodes = '" + generateCode.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    return "";
                }
              
                //**********************************************************************************************

                TextData.billNo = "SALE_" + generateCode.ToString();

                return TextData.billNo;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();

                return TextData.billNo;
            }
        }
        private void txt_cash_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txt_cash.Text != "" && txt_due_amount.Text != "")
            //    {
            //        lblRemaningAmount.Text = (Math.Round(double.Parse(txt_due_amount.Text) - double.Parse(txt_cash.Text), 2)).ToString();
            //    }
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void txt_cash_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
            selectTextBox = 0;
        }

        private void txt_on_hand_Click(object sender, EventArgs e)
        {
            selectTextBox = 1;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txt_cash.Text.Length > 0)
            {
                txt_cash.Text = txt_cash.Text.Remove(txt_cash.Text.Length - 1);
            }
        }

      
    }
}
