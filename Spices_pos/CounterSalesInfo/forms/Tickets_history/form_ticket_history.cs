using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Login_info.controllers;
using Datalayer;
using Message_box_info.forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using System.Diagnostics;
using OnBarcode.Barcode;
using System.IO;
using DataModel.Cash_Drawer_Data_Classes;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms.Unhold_orders
{
    public partial class form_ticket_history : Form
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

        public form_ticket_history()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string advanceAmount = "0";
        public static string ticketNumber = "";

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewTicketHistory order by [TID] desc";

                if (condition == "search")
                {
                    GetSetData.query = "select * from ViewTicketHistory where ([TID] like '%" + search_box.Text + "%' or [Date] like '%" + search_box.Text + "%' or [Receipt No] like '%" + search_box.Text + "%' or [Customer] like '%" + search_box.Text + "%' or [Employee] like '%" + search_box.Text + "%') order by [TID] desc";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_ticket_history_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;
                advanceAmount = "0";
                ticketNumber = "";
                search_box.Text = "";
                FillGridViewUsingPagination("");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }            
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            try
            {
                advanceAmount = "0";
                ticketNumber = "";
                this.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                search_box.Text = "";
                FillGridViewUsingPagination("");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FillGridViewUsingPagination("search");
                //search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                if (ticketNumber != "")
                {
                    //Print Ticket
                    GetSetData.query = @"select default_printer from pos_general_settings;";
                    string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                    if (printer_name != "")
                    {
                        PrintTicketDirectReceipt(printer_name, ticketNumber);
                    }
                    else
                    {
                        error.errorMessage("Printer not found!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureTicketItemsList", ticketNumber);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool fun_delete_products()
        {
            try
            {
                ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.Ids = data.UserPermissionsIds("sales_acc_id", "pos_tickets", "billNo", ticketNumber);

                GetSetData.query = @"delete from pos_ticket_details where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_tickets where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
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
            ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

            try
            {
                sure.Message_choose("Are you sure you want to delete Bill [" + ticketNumber.ToString() + "]");
                sure.ShowDialog();
             
                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    FillGridViewUsingPagination("");
                    search_box.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("[" + ticketNumber.ToString() + "'] cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void unHoldSelectedBill()
        {
            try
            {
                ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
              
                if (ticketNumber != "")
                {
                    if (productDetailGridView.SelectedRows[0].Cells["Status"].Value.ToString() == "Pending")
                    {
                        advanceAmount = data.UserPermissions("advance_amount", "pos_tickets", "billNo", ticketNumber);
                        this.Close();
                    }
                    else
                    {
                        error.errorMessage("Sorry this ticket is completed and cannot be regenerated again.!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            unHoldSelectedBill();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void form_ticket_history_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                try
                {
                    ticketNumber = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                    if (ticketNumber != "")
                    {
                        //Print Ticket
                        GetSetData.query = @"select default_printer from pos_general_settings;";
                        string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                        if (printer_name != "")
                        {
                            PrintTicketDirectReceipt(printer_name, ticketNumber);
                        }
                        else
                        {
                            error.errorMessage("Printer not found!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the row first!");
                        error.ShowDialog();
                    }
                }
                catch (Exception es)
                {
                    error.errorMessage(es.Message);
                    error.ShowDialog();
                }
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                unHoldSelectedBill();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ticketNumber = "";
                this.Close();
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void PrintTicketDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from reportViewPrintTicket WHERE (billNo = '" + BillNo + "');";

                var dt = GetDataTable(GetSetData.query);

                dt.Columns.Add("invoiceBarcode", typeof(byte[])); // Assuming 'invoiceBarcode' will store barcode image bytes

                Linear barcode = new Linear();
                barcode.Type = BarcodeType.CODE128;

                // Loop through each row in the dataset
                foreach (DataRow row in dt.Rows)
                {
                    // Set the data (billNo) for the barcode
                    barcode.Data = row["billNo"].ToString();
                    barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;

                    // Generate the barcode image as bytes
                    byte[] barcodeImageBytes = barcode.drawBarcodeAsBytes();

                    // Assign the barcode image bytes to the 'invoiceBarcode' column
                    row["invoiceBarcode"] = barcodeImageBytes;
                }

                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_ticket.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("ticket_ds", dt));

                //*********************************************************************************
                report.EnableExternalImages = true;

                //Report Parameters **********************************************************
                //GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                //string logo1 = data.UserPermissions("logo_path", "pos_configurations");

                //if (logo1 != "nill" && logo1 != "")
                //{
                //    GetSetData.query = GetSetData.Data + logo1;
                //    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                //    report.SetParameters(pLogo1);
                //}
                //else
                //{
                //    Microsoft.Reporting.WinForms.ReportParameter pLogo1 = new Microsoft.Reporting.WinForms.ReportParameter("pLogo", "");
                //    report.SetParameters(pLogo1);
                //}

                //*******************************************************************************************


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

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pNote = new Microsoft.Reporting.WinForms.ReportParameter("pNote", GetSetData.Data);
                report.SetParameters(pNote);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                Microsoft.Reporting.WinForms.ReportParameter pCopyrights = new Microsoft.Reporting.WinForms.ReportParameter("pCopyrights", GetSetData.Data);
                report.SetParameters(pCopyrights);
                //*******************************************************************************************

                TextData.general_options = "";
                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
                Microsoft.Reporting.WinForms.ReportParameter showNote = new Microsoft.Reporting.WinForms.ReportParameter("showNote", TextData.general_options);
                report.SetParameters(showNote);

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

        public static void Export(LocalReport report, string printer_name, bool print = true, bool openCashDrawer = true)
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

            if (openCashDrawer)
            {
                CashDrawerData.OpenDrawer(printer_name, string.Empty);
            }

            if (TextData.checkPrintReport == true)
            {
                if (print)
                {
                    Print(printer_name);
                }
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
    }
}
