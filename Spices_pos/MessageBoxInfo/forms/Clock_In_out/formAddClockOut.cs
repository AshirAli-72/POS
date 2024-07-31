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
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Message_box_info.forms.Clock_In
{
    public partial class formAddClockOut : Form
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


        public formAddClockOut()
        {
            InitializeComponent();

            if (generalSettings.ReadField("showShiftCurrency") == "Yes")
            {
                this.Width = 899;
            }
            else
            {
                this.Width = 678;
            }
        }

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        public static string  clock_id_id = "";
        public static bool saveEnable;

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
            //GetSetData.FillComboBoxUsingProcedures(txtToUser, "fillComboBoxUsers", "name");
            txtToUser.Text = null;
            txtToUser.Items.Clear();
            GetSetData.FillComboBoxWithValues("select full_name from pos_users inner join pos_employees on pos_employees.employee_id = pos_users.emp_id where (pos_employees.full_name != 'nill');", "full_name", txtToUser);
        }

        private void fillAddProductsFormTextBoxes()
        {
            fillComboBoxToUsers();

            txtDate.Text = TextData.date;
            txtOpeningCash.Text = TextData.openingAmount;
            txtSalesAmount.Text = TextData.totalSales;
            txtSalesReturns.Text = TextData.totalReturns;
            txtVoidOrders.Text = TextData.totalVoidOrders;
            txtExpectedAmount.Text = TextData.expectedAmount;
            txtCashReceived.Text = TextData.cashReceived;
            txtRemarks.Text = TextData.remarks;

            txtDate.Text = data.UserPermissions("date", "pos_clock_out", "id", TextData.clockOut_id);
            txtEndTime.Text = data.UserPermissions("end_time", "pos_clock_out", "id", TextData.clockOut_id);
            txtNumberOfHours.Text = data.UserPermissions("total_hours", "pos_clock_out", "id", TextData.clockOut_id);

            string clock_in_id_db = data.UserPermissions("clock_in_id", "pos_clock_out", "id", TextData.clockOut_id);
            txtStartDate.Text = data.UserPermissions("date", "pos_clock_in", "id", clock_in_id_db);
            txtStartTime.Text = data.UserPermissions("start_time", "pos_clock_in", "id", clock_in_id_db);


            //*******************************************************

            string toUserId = data.UserPermissions("to_user_id", "pos_clock_out", "id", TextData.clockOut_id);

            int employee_id = data.UserPermissionsIds("emp_id", "pos_users", "user_id", toUserId);

            txtToUser.Text = data.UserPermissions("full_name", "pos_employees", "employee_id", employee_id.ToString());

            //*******************************************************

            txt100.Text = data.UserPermissions("total100s", "pos_clock_out", "id", TextData.clockOut_id);
            txt50.Text = data.UserPermissions("total50s", "pos_clock_out", "id", TextData.clockOut_id);
            txt20.Text = data.UserPermissions("total20s", "pos_clock_out", "id", TextData.clockOut_id);
            txt10.Text = data.UserPermissions("total10s", "pos_clock_out", "id", TextData.clockOut_id);
            txt5.Text = data.UserPermissions("total5s", "pos_clock_out", "id", TextData.clockOut_id);
            txt2.Text = data.UserPermissions("total2s", "pos_clock_out", "id", TextData.clockOut_id);
            txt1.Text = data.UserPermissions("total1s", "pos_clock_out", "id", TextData.clockOut_id);
            txt1c.Text = data.UserPermissions("total1c", "pos_clock_out", "id", TextData.clockOut_id);
            txt5c.Text = data.UserPermissions("total5c", "pos_clock_out", "id", TextData.clockOut_id);
            txt10c.Text = data.UserPermissions("total10c", "pos_clock_out", "id", TextData.clockOut_id);
            txt25c.Text = data.UserPermissions("total25c", "pos_clock_out", "id", TextData.clockOut_id);
        }

        private void fillClockOutValueInForm()
        {
            fillComboBoxToUsers();

            TextData.clockIn_id = clock_id_id;

            txtDate.Text = data.UserPermissions("date", "pos_clock_in", "id", TextData.clockIn_id);
            txtOpeningCash.Text = data.UserPermissions("amount", "pos_clock_in", "id", TextData.clockIn_id);
            txtStartTime.Text = data.UserPermissions("start_time", "pos_clock_in", "id", TextData.clockIn_id);
            txtStartDate.Text = data.UserPermissions("date", "pos_clock_in", "id", TextData.clockIn_id);


            GetSetData.query = "select sum(paid) from pos_sales_accounts where (pos_sales_accounts.clock_in_id = '" + TextData.clockIn_id + "');";
            string total_sales = data.SearchStringValuesFromDb(GetSetData.query);


            GetSetData.query = "select sum(paid) from pos_return_accounts where (pos_return_accounts.clock_in_id = '" + TextData.clockIn_id + "');";
            string total_returns = data.SearchStringValuesFromDb(GetSetData.query);


            GetSetData.query = "select sum(Total_price) from pos_void_orders where (clock_in_id = '" + TextData.clockIn_id + "');";
            string total_void_orders = data.SearchStringValuesFromDb(GetSetData.query);


            GetSetData.query = "select sum(Total_price) from pos_no_sale where (clock_in_id = '" + TextData.clockIn_id + "');";
            string total_no_sale = data.SearchStringValuesFromDb(GetSetData.query);
            

            GetSetData.query = "select sum(amount) from pos_payout where (clock_in_id = '" + TextData.clockIn_id + "');";
            string total_payout_amount = data.SearchStringValuesFromDb(GetSetData.query);


            GetSetData.query = "select sum(amount) from pos_expense_details inner join pos_expense_items on pos_expense_items.expense_id = pos_expense_details.expense_id where (pos_expense_details.clock_in_id = '" + TextData.clockIn_id +"');";
            string total_expenses = data.SearchStringValuesFromDb(GetSetData.query);


            if (total_sales == "")
            {
                total_sales = "0";
            } 
            
            if (total_returns == "")
            {
                total_returns = "0";
            } 

            if (total_expenses == "")
            {
                total_expenses = "0";
            } 
             
            if (total_void_orders == "")
            {
                total_void_orders = "0";
            }
            
            if (total_no_sale == "")
            {
                total_no_sale = "0";
            }
            
            if (total_payout_amount == "")
            {
                total_payout_amount = "0";
            }

            double net_sales = double.Parse(total_sales) - double.Parse(total_returns);

            txtSalesAmount.Text = Math.Round(net_sales, 2).ToString();
            txtSalesReturns.Text = Math.Round(double.Parse(total_returns), 2).ToString();
            txtVoidOrders.Text = Math.Round(double.Parse(total_void_orders), 2).ToString();
            txtShortAmount.Text = Math.Round(double.Parse(total_expenses), 2).ToString();
            txtNoSale.Text = Math.Round(double.Parse(total_no_sale), 2).ToString();
            txtPayout.Text = Math.Round(double.Parse(total_payout_amount), 2).ToString();

            txtExpectedAmount.Text = ((double.Parse(txtOpeningCash.Text) + net_sales) - double.Parse(total_expenses)).ToString();

            //********************************************************

            txtEndTime.Text = DateTime.Now.ToLongTimeString();

            DateTime startTime = DateTime.Parse(txtStartDate.Text) + DateTime.Parse(txtStartTime.Text).TimeOfDay;
            DateTime endTime = DateTime.Today + DateTime.Parse(txtEndTime.Text).TimeOfDay;
          
            // Calculate duration
            TimeSpan duration = endTime - startTime;


            txtNumberOfHours.Text = duration.Duration().ToString();
            //txtNumberOfHours.Text = duration.ToString();
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Time Clock History";
                fillAddProductsFormTextBoxes();

                txtToUser.Visible = true;
                txtChooseEmployee.Visible = true;
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Add New Clock Out";
                fillClockOutValueInForm();

                txtToUser.Visible = false;
                txtChooseEmployee.Visible = false;
            }
        }

        private void fillVariableValues()
        {
            try
            {
                TextData.toUses = txtToUser.Text;
                TextData.openingAmount = txtOpeningCash.Text;
                TextData.totalSales = txtSalesAmount.Text;
                TextData.totalReturns = txtSalesReturns.Text;
                TextData.totalVoidOrders = txtVoidOrders.Text;
                TextData.totalNoSale = txtNoSale.Text;
                TextData.totalPayout = txtPayout.Text;
                TextData.expectedAmount = txtExpectedAmount.Text;
                TextData.cashReceived = txtCashReceived.Text;
                TextData.excessCash = txtBalance.Text;
                TextData.remarks = txtRemarks.Text;
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

                TextData.date = txtDate.Text;

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }

                //if (txtToUser.Text != "")
                //{
                    if (txtCashReceived.Text != "")
                    {
                        if (txtShortAmount.Text != "")
                        {
                            string employee_id = data.UserPermissions("emp_id", "pos_users", "user_id", user_id.ToString());
                            int hold_items_id = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "employee_id", employee_id);


                            if (hold_items_id == 0)
                            {
                                GetSetData.query = @"SELECT id FROM pos_clock_out where (clock_in_id = '" + TextData.clockIn_id + "');";
                                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                if (is_already_exist == 0)
                                {
                                    GetSetData.query = @"insert into pos_clock_out values ('" + TextData.date + "', '" + txtEndTime.Text + "', '" + txtNumberOfHours.Text + "',  '" + TextData.openingAmount + "', '" + TextData.totalSales + "', '" + TextData.totalReturns + "', '" + TextData.totalVoidOrders + "', '" + TextData.expectedAmount + "', '" + TextData.cashReceived + "', '" + TextData.excessCash + "', '" + TextData.remarks + "', '" + user_id.ToString() + "', '" + user_id.ToString() + "', '" + TextData.clockIn_id + "', '" + txtShortAmount.Text + "', '" + txtNoSale.Text + "', '" + txtPayout.Text + "', '0', '" + txt100.Text + "', '" + txt50.Text + "', '" + txt20.Text + "', '" + txt10.Text + "', '" + txt5.Text + "', '" + txt2.Text + "', '" + txt1.Text + "', '" + txt1c.Text + "', '" + txt5c.Text + "', '" + txt10c.Text + "', '" + txt25c.Text + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);


                                    GetSetData.query = @"update pos_clock_in set status  = '1' where ( id = '" + TextData.clockIn_id + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("This shift is already closed. Please clock-in to start new shift!");
                                    error.ShowDialog();

                                    return false;
                                }
                            }
                            else
                            {
                                error.errorMessage("The user is not allowed to clock out until the held items have been cleared! ");
                                error.ShowDialog();
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
            //}
            //    else
            //    {
            //        error.errorMessage("Please select the employee first!");
            //        error.ShowDialog();
            //    }

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

                sure.Message_choose("Would you like to print the Report.");
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
                }
                
                refresh();
            }
        }

        private bool Update_records()
        {
            try
            {
                if (txtToUser.Text != "")
                {
                    if (txtCashReceived.Text != "")
                    {
                        if (txtShortAmount.Text != "")
                        {
                            string employee_id = data.UserPermissions("employee_id", "pos_employees", "full_name", txtToUser.Text);
                            string user_id_db = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id);

                            //string employee_id = data.UserPermissions("emp_id", "pos_users", "user_id", user_id.ToString());
                            int hold_items_id = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "employee_id", employee_id);
                           

                            if (hold_items_id == 0)
                            {
                                //********************************************************

                                DateTime startTime = DateTime.Parse(txtStartDate.Text) + DateTime.Parse(txtStartTime.Text).TimeOfDay;
                                DateTime endTime = DateTime.Parse(txtDate.Text) + DateTime.Parse(txtEndTime.Text).TimeOfDay;

                                // Calculate duration
                                TimeSpan duration = endTime - startTime;


                                txtNumberOfHours.Text = duration.Duration().ToString();

                                //********************************************************

                                GetSetData.query = @"update pos_clock_out set date = '" + txtDate.Text + "' , end_time = '" + txtEndTime.Text + "' , total_hours = '" + duration.Duration().ToString() + "' , opening_cash = '" + txtOpeningCash.Text + "', total_sales = '" + txtSalesAmount.Text + "', total_return_amount = '" + txtSalesReturns.Text + "' , total_void_orders = '" + txtVoidOrders.Text + "', expected_amount = '" + txtExpectedAmount.Text + "',  cash_amount_received = '" + txtCashReceived.Text + "' , balance = '" + txtBalance.Text + "', shortage_amount = '" + txtShortAmount.Text + "', remarks = '" + txtRemarks.Text + "' , no_sales = '" + txtNoSale.Text + "' , payout = '" + txtPayout.Text + "' , from_user_id = '" + user_id_db + "' , to_user_id = '" + user_id_db +"', total100s = '" + txt100.Text + "', total50s = '" + txt50.Text + "', total20s = '" + txt20.Text + "', total10s = '" + txt10.Text + "', total5s = '" + txt5.Text + "', total2s = '" + txt2.Text + "', total1s = '" + txt1.Text + "', total1c = '" + txt1c.Text + "', total5c = '" + txt5c.Text + "', total10c = '" + txt10c.Text + "', total25c = '" + txt25c.Text + "' where (id = '" + TextData.clockOut_id + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                string clock_in_id_db = data.UserPermissions("clock_in_id", "pos_clock_out", "id", TextData.clockOut_id);


                                GetSetData.query = @"update pos_clock_in set date = '" + txtStartDate.Text + "' , start_time = '" + txtStartTime.Text + "' where (id = '" + clock_in_id_db + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                return true;
                            }
                            else
                            {
                                error.errorMessage("The user is not allowed to clock out until the held items have been cleared!");
                                error.ShowDialog();
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
                }
                else
                {
                    error.errorMessage("Please select the employee first!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Update_records())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();

                sure.Message_choose("Would you like to print the Report.");
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
                }
            }
        }

        private void formAddClockOut_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
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

                    txtBalance.Text = balance.ToString();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            if (insert_records())
            {
                //done.DoneMessage("Successfully Saved!");
                //done.ShowDialog();

                GetSetData.query = @"select default_printer from pos_general_settings;";
                string printer_name = data.SearchStringValuesFromDb(GetSetData.query);
               
                
                GetSetData.query = @"SELECT TOP 1 * FROM pos_clock_out ORDER BY [ID] DESC;";
                TextData.clockOut_id = data.SearchStringValuesFromDb(GetSetData.query);


                if (printer_name != "")
                {
                    PrintDirectReceipt(printer_name);
                }
                else
                {
                    error.errorMessage("Printer not found!");
                    error.ShowDialog();
                }

                refresh();
            }
        }


        private void PrintDirectReceipt(string printer_name)
        {
            try
            {
                GetSetData.query = @"SELECT  pos_clock_out.date, from_user_employee.full_name as from_user, to_user_employee.full_name as to_user, pos_shift.title as shift_title,pos_counter.title as counter_title,
                                     pos_clock_out.opening_cash, pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received, 
                                     pos_clock_out.balance, pos_clock_out.remarks, pos_clock_in.status, pos_clock_out.total_void_orders, pos_clock_in.start_time, pos_clock_out.end_time,
                                     pos_clock_out.total_hours, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout,
                                     pos_clock_out.total100s, pos_clock_out.total50s, pos_clock_out.total20s, pos_clock_out.total10s, pos_clock_out.total5s, pos_clock_out.total2s, pos_clock_out.total1s,
                                     pos_clock_out.total1c, pos_clock_out.total5c, pos_clock_out.total10c, pos_clock_out.total25c
                                     FROM pos_clock_in INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id  INNER JOIN 
                                     pos_users AS from_user ON pos_clock_in.from_user_id = from_user.user_id
                                     INNER JOIN pos_users AS to_user ON pos_clock_in.to_user_id = to_user.user_id
                                     INNER JOIN pos_employees as to_user_employee ON to_user_employee.employee_id = to_user.emp_id
                                     INNER JOIN pos_employees as from_user_employee ON from_user_employee.employee_id = from_user.emp_id
                                     INNER JOIN pos_shift ON pos_clock_in.shift_id = pos_shift.id INNER JOIN pos_counter ON pos_clock_in.counter_id = pos_counter.id
                                     Where (pos_clock_out.id = '" + TextData.clockOut_id +"')";


                var dt = GetDataTable(GetSetData.query);
                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_clock_out_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("clock_out_details", dt));

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


                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);


                GetSetData.Data = data.UserPermissions("showShiftCurrency", "pos_general_settings");
                Microsoft.Reporting.WinForms.ReportParameter pShowShiftCurrency = new Microsoft.Reporting.WinForms.ReportParameter("pShowShiftCurrency", GetSetData.Data);
                report.SetParameters(pShowShiftCurrency);

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

        private void txt1_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
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

        private void txt1c_TextChanged(object sender, EventArgs e)
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
    }
}
