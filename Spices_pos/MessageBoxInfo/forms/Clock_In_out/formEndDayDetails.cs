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
using Message_box_info.CharityReports;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Spices_pos.DashboardInfo.Forms;
using Message_box_info.forms.Clock_In_out.Reports.OverallReports;
using DataModel.Cash_Drawer_Data_Classes;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;

namespace Message_box_info.forms.Clock_In
{
    public partial class formEndDayDetails : Form
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

        public formEndDayDetails()
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

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
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
                formAddClockIn.role_id = role_id;
                formAddClockIn.user_id = user_id;
                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("charityPaybook_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //pnl_print.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("charityPaybook_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //pnl_delete.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("charityPaybook_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("charityPaybook_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //pnl_modify.Visible = bool.Parse(GetSetData.Data);

                sidePanel.Visible = true;
                setSideBarInSidePanel();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void setSideBarInSidePanel()
        {
            try
            {
                sidebarUserControl sidebar = new sidebarUserControl();

                sidebar.role_id = role_id;
                sidebar.user_id = user_id;

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
                sidebar.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select top 500 * from ViewStoreEndDay";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([ID] like '" + SearchByBox.Text + "%' or [Date] like '" + SearchByBox.Text + "%' or [User] like '" + SearchByBox.Text + "%')";
                }
                //else if (condition == "DateWiseSales")
                //{
                //    GetSetData.query = "select * from ViewCashOutDetails where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "')";
                //} 
                //else if (condition == "EmployeeWise")
                //{
                //    GetSetData.query = "select * from ViewCashOutDetails where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "') and ([User] = '" + txtEmployeeName.Text +"')";
                //}

                GetSetData.query = GetSetData.query + " order by [ID] desc";

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formEndDayDetails_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                SearchByBox.Text = "";

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
            this.Dispose();
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

        private void addNewDetails()
        {
            GetSetData.query = @"SELECT count(id) from pos_clock_in where (status  = '-1');";
            int is_all_users_shift_close = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            if (is_all_users_shift_close == 0)
            {
                autoOpenCashDrawer();
                formStoreEndDay.user_id = user_id;
                formStoreEndDay.role_id = role_id;
                formStoreEndDay.saveEnable = false;

                using (formAddClockIn add_customer = new formAddClockIn())
                {
                    formStoreEndDay _obj = new formStoreEndDay(); 
                    _obj.ShowDialog();

                }
            }
            else
            {
                error.errorMessage("Please clock-out all the employees first!");
                error.ShowDialog();

                using (formClockInDetails add_customer = new formClockInDetails())
                {
                    formClockInDetails.user_id = user_id;
                    formClockInDetails.role_id = role_id;
                    formClockInDetails _obj = new formClockInDetails();
                    _obj.Show();
                    this.Dispose();
                }
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            SearchByBox.Text = "";
            FillGridViewUsingPagination("");
        }

        private void SearchByBox_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private bool fun_update_details()
        {
            try
            {
                //GetSetData.Data = data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());

                //if (GetSetData.Data == "True")
                //{
                formStoreEndDay.saveEnable = true;
                formStoreEndDay.user_id = user_id;
                formStoreEndDay.role_id = role_id;

                TextData.clockOut_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                TextData.cashReceived = productDetailGridView.SelectedRows[0].Cells["Cash Received"].Value.ToString();
                TextData.totalExpenses = productDetailGridView.SelectedRows[0].Cells["Shortage Amount"].Value.ToString();
                TextData.openingAmount = productDetailGridView.SelectedRows[0].Cells["Opening Balance"].Value.ToString();
                TextData.cashAmount = productDetailGridView.SelectedRows[0].Cells["Cash Amount"].Value.ToString();
                TextData.creditCard = productDetailGridView.SelectedRows[0].Cells["Credit Card"].Value.ToString();
                TextData.applePay = productDetailGridView.SelectedRows[0].Cells["Apple Pay"].Value.ToString();
                TextData.googlePay = productDetailGridView.SelectedRows[0].Cells["Zelle / Cashapp / Venmo"].Value.ToString();
                TextData.miscSales = productDetailGridView.SelectedRows[0].Cells["Misc Sales"].Value.ToString();
                TextData.totalReturns = productDetailGridView.SelectedRows[0].Cells["Total Returns"].Value.ToString();
                TextData.totalDiscount = productDetailGridView.SelectedRows[0].Cells["Discounts"].Value.ToString();
                TextData.totalTaxation = productDetailGridView.SelectedRows[0].Cells["Total Tax"].Value.ToString();
                TextData.totalVoidOrders = productDetailGridView.SelectedRows[0].Cells["Void Orders"].Value.ToString();
                TextData.totalNoSale = productDetailGridView.SelectedRows[0].Cells["No Sale"].Value.ToString();
                TextData.totalPayout = productDetailGridView.SelectedRows[0].Cells["Payout"].Value.ToString();
                TextData.expectedAmount = productDetailGridView.SelectedRows[0].Cells["Expected Amount"].Value.ToString();
                TextData.totalTickets = productDetailGridView.SelectedRows[0].Cells["Total Tickets"].Value.ToString();
                TextData.balance = double.Parse(productDetailGridView.SelectedRows[0].Cells["Balance"].Value.ToString());

                using (formStoreEndDay add_customer = new formStoreEndDay())
                {
                    add_customer.ShowDialog();
                }

                //}

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
                return false;
            }
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            fun_update_details();
        }

        private void updates_expense_details(object sender, DataGridViewCellEventArgs e)
        {
            fun_update_details();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.clockOut_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();


               TextData.clockIn_id = data.UserPermissions("clock_in_id", "pos_clock_out", "id", TextData.clockOut_id);


                GetSetData.query = @"delete from pos_clock_out where (id = '" + TextData.clockOut_id + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            try
            {
                TextData.clockOut_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();


                sure.Message_choose("Are you sure you want to delete '" + TextData.clockOut_id + "'!");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    SearchByBox.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("'" + TextData.date + "  " + TextData.time + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            try
            {
                GetSetData.query = @"select default_printer from pos_general_settings;";
                string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


                TextData.clockOut_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();

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
            catch (Exception es)
            {
                error.errorMessage("Record not found!");
                error.ShowDialog();
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
                                    Where (pos_store_day_end.id = '" + TextData.clockOut_id + "')";


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

        private void formEndDayDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                formCharityReport report = new formCharityReport();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                addNewDetails();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                fun_update_details();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                SearchByBox.Select();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGridViewUsingPagination("DateWiseSales");

                if (txtEmployeeName.Text == "")
                {
                    lblTotalDuration.Text = "Total Duration: " + GetSetData.ProcedureGetEmployeeDuration(FromDate.Text, ToDate.Text, "");

                    FillGridViewUsingPagination("DateWiseSales");
                }
                else
                {
                    lblTotalDuration.Text = "Total Duration: " + GetSetData.ProcedureGetEmployeeDuration(FromDate.Text, ToDate.Text, txtEmployeeName.Text);

                    FillGridViewUsingPagination("EmployeeWise");
                }

                
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            overallDurationReport.fromDate = FromDate.Text;
            overallDurationReport.toDate = ToDate.Text;

            overallDurationReport _obj = new overallDurationReport();
            _obj.ShowDialog();
        }

        private void SearchByBox_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
