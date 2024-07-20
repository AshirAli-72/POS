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
    public partial class formAddTimeClockHistory : Form
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


        public formAddTimeClockHistory()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
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
            //GetSetData.FillComboBoxUsingProcedures(txtToUser, "fillComboBoxUsers", "name");
            txtToUser.Text = null;
            txtToUser.Items.Clear();
            GetSetData.FillComboBoxWithValues("select full_name from pos_users inner join pos_employees on pos_employees.employee_id = pos_users.emp_id where (pos_employees.full_name != 'nill');", "full_name", txtToUser);
        }

        private void fillComboBoxCounters()
        {
            GetSetData.FillComboBoxUsingProcedures(txtCounter, "fillComboBoxCounters", "title");
        }

        private void fillComboBoxShifts()
        {
            GetSetData.FillComboBoxUsingProcedures(txtShift, "fillComboBoxShifts", "title");
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Time Clock History";
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Add New Time Clock History";
            }
        }

        private bool insert_records()
        {
            try
            {
                TextData.remarks = txtRemarks.Text;

                if (txtRemarks.Text == "")
                {
                    TextData.remarks = "nill";
                }

                if (txtShift.Text != "")
                {
                    if (txtCounter.Text != "")
                    {
                        if (txtAmount.Text != "")
                        {
                            if (txtToUser.Text != "")
                            {
                                if (txtCashReceived.Text != "")
                                {
                                    if (txtShortAmount.Text != "")
                                    {
                                        int counter_id = data.UserPermissionsIds("id", "pos_counter", "title", txtCounter.Text);
                                        int shift_id = data.UserPermissionsIds("id", "pos_shift", "title", txtShift.Text);

                                        //************************************************************

                                        string employee_id = data.UserPermissions("employee_id", "pos_employees", "full_name", txtToUser.Text);
                                        string user_id_db = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id);
                                       
                                        //************************************************************

                                        GetSetData.query = "select id from pos_clock_in where (date = '" + txtStartDate.Text + "') and (time = '" + txtStartTime.Text + "') and (to_user_id = '" + user_id_db + "');";
                                        double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);


                                        if (clock_in_id == 0)
                                        {
                                            GetSetData.query = @"insert into pos_clock_in values ('" + txtStartDate.Text + "','" + txtStartTime.Text + "' , '" + txtAmount.Text + "', '" + TextData.remarks + "', '0', '" + shift_id.ToString() + "', '" + user_id_db + "', '" + user_id_db + "', '" + counter_id.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                        }


                                        GetSetData.query = "select id from pos_clock_in where (date = '" + txtStartDate.Text + "') and (to_user_id = '" + user_id_db + "');";
                                        clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

                                        //************************************************************

                                        GetSetData.query = @"SELECT id FROM pos_clock_out where (date = '" + txtEndDate.Text + "') and (time = '" + txtEndTime.Text + "') and (to_user_id = '" + user_id_db + "');";
                                        int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                        if (is_already_exist == 0)
                                        {
                                            DateTime startTime = DateTime.Parse(txtStartDate.Text) + DateTime.Parse(txtStartTime.Text).TimeOfDay;
                                            DateTime endTime = DateTime.Parse(txtEndDate.Text) + DateTime.Parse(txtEndTime.Text).TimeOfDay;

                                            // Calculate duration
                                            TimeSpan duration = endTime - startTime;


                                            txtNumberOfHours.Text = duration.Duration().ToString();

                                            //********************************************************

                                            GetSetData.query = @"insert into pos_clock_out values ('" + txtEndDate.Text + "', '" + txtEndTime.Text + "', '" + duration.Duration().ToString() + "',  '" + txtAmount.Text + "', '" + txtSalesAmount.Text+ "', '" + txtSalesReturns.Text + "', '" + txtVoidOrders.Text + "', '" + txtExpectedAmount.Text + "', '" + txtCashReceived.Text+ "', '" + txtBalance.Text + "', '" + TextData.remarks + "', '" + user_id_db + "', '" + user_id_db + "', '" + clock_in_id.ToString() + "', '" + txtShortAmount.Text + "', '" + txtNoSale.Text + "', '" + txtPayout.Text + "', '0');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);
                                           

                                            GetSetData.query = @"update pos_clock_in set status  = '1' where ( id = '" + clock_in_id.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);

                                            return true;
                                        }
                                        else
                                        {
                                            error.errorMessage("This user is already clocked-out in selected date and time!");
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
                            }
                            else
                            {
                                error.errorMessage("Please select the employee first!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the opening amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select counter first!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select employee shift first!");
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

 
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //
        }

        private void formAddTimeClockHistory_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void refresh()
        {
            try
            {
                txtEndDate.Text = DateTime.Now.ToLongDateString();
                txtStartDate.Text = DateTime.Now.ToLongDateString();
                txtEndTime.Text = DateTime.Now.ToLongTimeString();
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
                fillComboBoxShifts();
                fillComboBoxCounters();

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
                                     pos_clock_out.balance, pos_clock_out.remarks, pos_clock_in.status, pos_clock_out.total_void_orders, pos_clock_in.start_time, pos_clock_out.end_time, pos_clock_out.total_hours, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout
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

        private void txtAmount_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
