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
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;

namespace Message_box_info.forms.Clock_In
{
    public partial class formCounterDeatils : Form
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


        public formCounterDeatils()
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
                GetSetData.query = "select id as [ID], title as [Counter Name], opening_amount as [Opening Amount], shift_limit as [Shift Limit] from pos_counter";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([ID] like '" + SearchByBox.Text + "%' or [Counter Name] like '" + SearchByBox.Text + "%')";
                }

                GetSetData.query = GetSetData.query + " order by [ID] desc";

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formCounterDeatils_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                SearchByBox.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            formClockInDetails.user_id = user_id;
            formClockInDetails.role_id = role_id;
            formClockInDetails main = new formClockInDetails();
            main.Show();

            this.Dispose();
        }

        private void addNewDetails()
        {
            using (formCounters add_customer = new formCounters())
            {
                formCounters.role_id = role_id;
                formCounters.saveEnable = false;
                add_customer.ShowDialog();
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
                formCounters.saveEnable = true;
               
                formCounters.counterId = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                formCounters.title = productDetailGridView.SelectedRows[0].Cells["Counter Name"].Value.ToString();
                formCounters.openingAmount = productDetailGridView.SelectedRows[0].Cells["Opening Amount"].Value.ToString();
                formCounters.shiftLimit = productDetailGridView.SelectedRows[0].Cells["Shift Limit"].Value.ToString();
               

                using (formCounters add_customer = new formCounters())
                {
                    formCounters.role_id = role_id;
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
                TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();

                GetSetData.query = @"delete from pos_counter where (id = '" + TextData.clockIn_id + "');";
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
            TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();

            try
            {
                sure.Message_choose("Are you sure you want to delete '" + TextData.clockIn_id + "'!");
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
                TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();


                GetSetData.query = @"SELECT pos_clock_in.date, from_user_employees.full_name AS from_user, to_user_employees.full_name AS to_user, pos_shift.title AS shift_title, 
                                    pos_counter.title AS counter_title, pos_clock_in.amount, pos_clock_in.remarks, pos_clock_in.status
                                    FROM  pos_clock_in INNER JOIN pos_counter ON pos_clock_in.counter_id = pos_counter.id INNER JOIN
                                    pos_shift ON pos_clock_in.shift_id = pos_shift.id INNER JOIN
                                    pos_users AS from_user ON dbo.pos_clock_in.from_user_id = from_user.user_id 
                                    Inner Join pos_users AS to_user ON dbo.pos_clock_in.to_user_id = to_user.user_id 
                                    Inner JOIN pos_employees AS from_user_employees ON from_user_employees.employee_id = from_user.emp_id
                                    Inner JOIN pos_employees AS to_user_employees ON to_user_employees.employee_id = to_user.emp_id
                                    Where (pos_clock_in.id = '" + TextData.clockIn_id + "')";


                var dt = GetDataTable(GetSetData.query);
                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_clock_in_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("clock_in_details", dt));

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

        private void formCounterDeatils_KeyDown(object sender, KeyEventArgs e)
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

            } else if (e.Control && e.KeyCode == Keys.C)
            {
                addNewClockOut();
            }
        }

        private void addNewClockOut()
        {
            try
            {
                formAddClockOut.clock_id_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();


                if (TextData.clockIn_id != "" || TextData.clockIn_id != null)
                {
                    string is_already_clock_out = data.UserPermissions("status", "pos_clock_in", "id", TextData.clockIn_id);

                    if (is_already_clock_out == "0" || is_already_clock_out == "-1")
                    {
                        using (formAddClockOut add_customer = new formAddClockOut())
                        {
                            formAddClockOut.user_id = user_id;
                            formAddClockOut.role_id = role_id;
                            formAddClockOut.saveEnable = false;
                            add_customer.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("This user is already clocked out!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("No record found!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage("No record found!");
                error.ShowDialog();
            }
        }

        private void clockOutOnly()
        {
            try
            {
                TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();

                if (TextData.clockIn_id != "")
                {
                    txtDate.Text = DateTime.Now.ToLongDateString();

                    string user_id_db = data.UserPermissions("to_user_id", "pos_clock_in", "id", TextData.clockIn_id);
                    string start_time = data.UserPermissions("start_time", "pos_clock_in", "id", TextData.clockIn_id);
                    string start_date = data.UserPermissions("date", "pos_clock_in", "id", TextData.clockIn_id);

                    DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
                    DateTime endTime = DateTime.Today + DateTime.Parse(time_text.Text).TimeOfDay;

                    // Calculate duration
                    TimeSpan duration = endTime - startTime;


                    GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + time_text.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + user_id_db + "', '" + user_id_db + "', '" + TextData.clockIn_id + "', '0', '0', '0', '0');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"update pos_clock_in set status  = '-2' where (id = '" + TextData.clockIn_id + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    done.DoneMessage("Clocked-Out successfully.");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please select any record first!");
                    error.ShowDialog();
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "clockout")
            {
                TextData.clockIn_id = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();


                GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
                string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


                if (singleAuthorityClosing == "Yes")
                {
                    clockOutOnly();
                }
                else
                {
                    string clock_in_status = data.UserPermissions("status", "pos_clock_in", "id", TextData.clockIn_id);

                    if (clock_in_status == "-1")
                    {
                        clockOutOnly();
                    }
                    else
                    {
                        addNewClockOut();
                    }
                }
            }
        }

        private bool clockIn()
        {
            try
            {
                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string is_already_clockedIn = data.SearchStringValuesFromDb(GetSetData.query);

                if (is_already_clockedIn == "")
                {
                    string start_time = "";
                    start_time = time_text.Text;

                    int counter_id = data.UserPermissionsIds("id", "pos_counter", "title", "default");
                    int shift_id = data.UserPermissionsIds("id", "pos_shift", "title", "default");

                    // status -1 = clock in only, 0 = shift, shift close -2 = , 1 = store day close  

                    GetSetData.query = @"insert into pos_clock_in values ('" + txtDate.Text + "','" + start_time.ToString() + "' , '0', 'nill', '-1', '" + shift_id.ToString() + "', '" + user_id.ToString() + "', '" + user_id.ToString() + "', '" + counter_id.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    return true;
                }
                else
                {
                    error.errorMessage("You are already clocked-In");
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

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            if (clockIn())
            {
                error.errorMessage("Please Count the Cash Drawer/Register.");
                error.ShowDialog();

                done.DoneMessage("Successfully Clocked-In");
                done.ShowDialog();
                //form_counter_sales.user_id = user_id;
                //form_counter_sales.role_id = role_id;
                //form_counter_sales _obj = new form_counter_sales();
                //_obj.Show();
                //this.Dispose();

            }
        }

        private void btnShifts_Click(object sender, EventArgs e)
        {
            formShifts.role_id = role_id;
            formShifts _obj = new formShifts();
            _obj.ShowDialog();
        }

        private void btnCounters_Click(object sender, EventArgs e)
        {
            formCounters.role_id = role_id;
            formCounters _obj = new formCounters();
            _obj.ShowDialog();
        }

        private void SearchByBox_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
