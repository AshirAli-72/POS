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
using CounterSales_info.CustomerSalesReport;
using RefereningMaterial;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using OnBarcode.Barcode;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using CounterSales_info.CustomerSalesInfo.CustomerProductsReturned;

namespace CounterSales_info.forms.Customer_orders_list
{
    public partial class form_customer_orders : Form
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

        public form_customer_orders()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static int user_id = 0;
        private bool option = true;

        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlDelete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlPrint.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_allSales", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlAllReturns.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_allReturns", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlAllReturns.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_contractForm", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlContractForm.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnlModify.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("customerOrders_search", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //btnSearch.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination(string condition1, string condition2)
        {
            try
            {
                if (condition1 == "customerWise")
                {
                    GetSetData.query = "select top 500 * from ViewCustomerWiseOrders where ([CID] = '" + condition2 + "')  order by [SID] desc;";
                }
                else if (condition1 == "overAllSales")
                {
                    GetSetData.query = "select top 500 * from ViewOverAllOrders";
                   
                    if (condition2 == "search")
                    {
                        GetSetData.query = GetSetData.query + " where ([Date] like '%" + search_box.Text + "%' or [Receipt No] like '%" + search_box.Text + "%' or [Customer] like '%" + search_box.Text + "%' or [Employee] like '%" + search_box.Text + "%');";
                    }
                }
                else if (condition1 == "overAllReturns")
                {
                    GetSetData.query = "select top 500 * from ViewOrdersReturned";

                    if (condition2 == "search")
                    {
                        GetSetData.query = GetSetData.query + " where ([Date] like '%" + search_box.Text + "%' or [Receipt No] like '%" + search_box.Text + "%' or [Customer] like '%" + search_box.Text + "%' or [Employee] like '%" + search_box.Text + "%');";
                    }
                }
                else if (condition1 == "DateWiseSales")
                {
                    GetSetData.query = "select top 500 * from ViewOverAllOrders where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                }
                else if (condition1 == "DateWiseReturns")
                {
                    GetSetData.query = "select top 500 * from ViewOrdersReturned where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                }
                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_customer_orders_Load(object sender, EventArgs e)
        {
            try
            {

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "')  and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_db = data.SearchStringValuesFromDb(GetSetData.query);


                system_user_permissions();

                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                option = true;
                search_box.Text = "";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                if (clock_in_db != "")
                {
                    FillGridViewUsingPagination("customerWise", clock_in_db);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Exit...", role_id);
            this.Dispose();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                option = true;
                GetSetData.ResetPageNumbers(lblPageNo);
                search_box.Text = "";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                
                
                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string clock_in_db = data.SearchStringValuesFromDb(GetSetData.query);


                if (clock_in_db != "")
                {
                    FillGridViewUsingPagination("customerWise", clock_in_db);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void printSelectedDetails()
        {
            try
            {
                TextData.billNo = "";
                GetSetData.ResetPageNumbers(lblPageNo);
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                if (option == true)
                {
                    if (TextData.billNo != "")
                    {
                        GetSetData.query = @"select default_printer from pos_general_settings;";
                        string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

                        GetSetData.query = @"select preview_receipt from pos_general_settings;";
                        string preview_receipt = data.SearchStringValuesFromDb(GetSetData.query);

                        if (preview_receipt == "Yes")
                        {
                            form_cus_sales_report report = new form_cus_sales_report();
                            report.ShowDialog();
                        }
                        else
                        {
                            if (printer_name != "")
                            {
                                PrintDirectReceipt(printer_name, TextData.billNo);
                            }
                            else
                            {
                                error.errorMessage("Printer not found!");
                                error.ShowDialog();
                            }
                        }

                    }
                    else
                    {
                        error.errorMessage("Sorry no recent bill found!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    form_cus_returns_report report = new form_cus_returns_report();
                    report.ShowDialog();
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                //error.errorMessage("Please select the invoice first!");
                //error.ShowDialog();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printSelectedDetails();
        }

        private void allSalesDetails()
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "All Sales button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                search_box.Text = "";
                option = true;
                FillGridViewUsingPagination("overAllSales", "");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            allSalesDetails();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetSetData.ResetPageNumbers(lblPageNo);

                if (option == true)
                {
                    FillGridViewUsingPagination("overAllSales", "search");
                }
                else
                {
                    FillGridViewUsingPagination("overAllReturns", "search");
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
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                if (option == true)
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedureOrderSoldItemsList", TextData.billNo);
                }
                else
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedureOrderReturnedItemsList", TextData.billNo);
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the bill first!");
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }  
        }

        private void searchByDate()
        {
            try
            {
                GetSetData.ResetPageNumbers(lblPageNo);

                if (option == true)
                {
                    //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Searching Sales [" + FromDate.Text + " to " + ToDate.Text + "] details", role_id);
                    FillGridViewUsingPagination("DateWiseSales", "");
                }
                else
                {
                    //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Searching Sales Returns [" + FromDate.Text + " to " + ToDate.Text + "] details", role_id);
                    FillGridViewUsingPagination("DateWiseReturns", "");
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            searchByDate();
        }

        private void allReturnsDetails()
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "All sales returns button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                search_box.Text = "";
                option = false;
                FillGridViewUsingPagination("overAllReturns", "");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allReturnsDetails();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.Ids = data.UserPermissionsIds("sales_acc_id", "pos_sales_accounts", "billNo", TextData.billNo);
                GetSetData.fks = data.UserPermissionsIds("installment_acc_id", "pos_installment_accounts", "sales_acc_id", GetSetData.Ids.ToString());
                TextData.status = data.UserPermissions("status", "pos_installment_plan", "installment_acc_id", GetSetData.fks.ToString());
                int customerId_db = data.UserPermissionsIds("customer_id", "pos_sales_accounts", "sales_acc_id", GetSetData.Ids.ToString());

                if (TextData.status != "Completed")
                {
                    TextData.lastCredit = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customerId_db.ToString());
                    TextData.net_total = data.NumericValues("amount_due", "pos_sales_accounts", "sales_acc_id", GetSetData.Ids.ToString());
                    TextData.cash = data.NumericValues("paid", "pos_sales_accounts", "sales_acc_id", GetSetData.Ids.ToString());

                    TextData.credits = TextData.net_total - TextData.cash;
                    TextData.lastCredit -= TextData.credits;

                    if (TextData.lastCredit >= 0)
                    {
                        GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredit.ToString() + "' where customer_id = '" + customerId_db.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);   
                    }

                    GetSetData.query = @"delete from pos_payment_grantors where billNo = '" + TextData.billNo + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    GetSetData.query = @"delete from pos_installment_plan where installment_acc_id = '" + GetSetData.fks.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    GetSetData.query = @"delete from pos_installment_accounts where installment_acc_id = '" + GetSetData.fks.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    GetSetData.query = @"delete from pos_customerChequeDetails where billNo = '" + TextData.billNo + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //========================================================
                    GetSetData.query = @"delete from pos_sales_details where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.query = @"delete from pos_sales_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.query = @"delete from pos_customer_transactions where billNo = '" + TextData.billNo + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //========================================================

                    GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Deleting Sales invoice [" + TextData.billNo + "] details", role_id);
                    return true;
                }
                else
                {
                    error.errorMessage("Sorry this bill is in use!");
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

        private bool fun_delete_products2()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.Ids = data.UserPermissionsIds("return_acc_id", "pos_return_accounts", "billNo", TextData.billNo);
                //========================================================

                GetSetData.query = @"delete from pos_returns_details where return_acc_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_return_accounts where return_acc_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Deleting Sales Return invoice [" + TextData.billNo + "] details", role_id);
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
            TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

            try
            {
                if (option == true)
                {
                    //this.Opacity = .850;
                    sure.Message_choose("Are you sure you want to delete Bill [" + TextData.billNo.ToString() + "]");
                    sure.ShowDialog();
                    //this.Opacity = .999;

                    if (form_sure_message.sure == true)
                    {
                        fun_delete_products();
                        GetSetData.ResetPageNumbers(lblPageNo);
                        //FillGridViewUsingPagination("overAllSales", "");

                        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "')  and (status = '0' or status = '-1') ORDER BY id DESC;";
                        string clock_in_db = data.SearchStringValuesFromDb(GetSetData.query);


                        if (clock_in_db != "")
                        {
                            FillGridViewUsingPagination("customerWise", clock_in_db);
                        }


                        search_box.Text = "";
                    }
                }
                else
                {
                    sure.Message_choose("Are you sure you want to delete Bill [" + TextData.billNo.ToString() + "]");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        fun_delete_products2();
                        FillGridViewUsingPagination("overAllReturns", "");
                        search_box.Text = "";
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage("[" + TextData.billNo.ToString() + "'] cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
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

        private void SelectedContractForm()
        {
            try
            {
                if (option == true)
                {
                    TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                    TextData.status = data.UserPermissions("status", "pos_sales_accounts", "billNo", TextData.billNo);

                    if (TextData.billNo != "" && TextData.status == "Installment")
                    {
                        //GetSetData.SaveLogHistoryDetails("Customer Orders Details Form", "Print Sales invoice [" + TextData.billNo + "] Customer Contract form", role_id);
                        //agreementForm.billNo = TextData.billNo;
                        //agreementForm report = new agreementForm();
                        //report.ShowDialog();
                    }
                    else
                    {
                        error.errorMessage("Invoice belongs to direct sale!");
                        error.ShowDialog();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the invoice first!");
                error.ShowDialog();
            }
        }

        private void btnContractForm_Click(object sender, EventArgs e)
        {
            SelectedContractForm();
        }

        private void BtnModify_Click(object sender, EventArgs e)
        {
          
        }

        private void productDetailGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        //private void PrintDirectReceipt(string printer_name)
        //{
        //    try
        //    {
        //        //GetSetData.query = @"SELECT * from ReportViewBillWiseSales WHERE (billNo = '" + TextData.billNo + "');";

        //        //var dt = GetDataTable(GetSetData.query);

        //        string query = "SELECT * from ReportViewBillWiseSales WHERE billNo = @BillNo";

        //        using (SqlConnection connection = new SqlConnection(webConfig.con_string))
        //        {
        //            connection.Open();

        //            using (SqlCommand cmd = new SqlCommand(query, connection))
        //            {
        //                cmd.Parameters.AddWithValue("@BillNo", TextData.billNo);
        //                DataTable dt = new DataTable();
        //                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //                {
        //                    adapter.Fill(dt);
        //                }

        //                LocalReport report = new LocalReport();

        //                //string path = Path.GetDirectoryName(Application.StartupPath);
        //                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_sales_report.rdlc";
        //                report.ReportPath = fullPath;


        //                report.EnableExternalImages = true;

        //                //Linear barcode = new Linear();
        //                ////Linear ItemNote = new Linear();
        //                //barcode.Type = BarcodeType.CODE128;

        //                //barcode.Data = TextData.billNo.ToString();
        //                //barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;
        //                ////invoiceBarcode = barcode.drawBarcodeAsBytes();


        //                //report.SetParameters(new ReportParameter("pBarcodeImage", new Uri(barcode.drawBarcodeAsBytes().ToString()).AbsoluteUri));



        //                //Report Parameters **********************************************************
        //                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
        //                string logo1 = data.UserPermissions("logo_path", "pos_configurations");


        //                if (logo1 != "nill" && logo1 != "")
        //                {
        //                    GetSetData.query = GetSetData.Data + logo1;
        //                    //ReportParameter pLogo1 = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
        //                    //report.SetParameters(pLogo1);   

        //                    report.SetParameters(new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri));
        //                }
        //                else
        //                {
        //                    //ReportParameter pLogo1 = new ReportParameter("pLogo", "");
        //                    //report.SetParameters(pLogo1);
        //                    report.SetParameters(new ReportParameter("pLogo", ""));
        //                }

        //                //*******************************************************************************************

        //                GetSetData.query = @"select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
        //                                 where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";
        //                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

        //                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
        //                {
        //                    GetSetData.Data = "0";
        //                }

        //                //ReportParameter pCounterSalesLastCredits = new ReportParameter("pCounterSalesLastCredits", GetSetData.Data);
        //                //report.SetParameters(pCounterSalesLastCredits);

        //                report.SetParameters(new ReportParameter("pCounterSalesLastCredits", GetSetData.Data));
        //                //*******************************************************************************************


        //                // Retrive Report Settings from db *******************************************************************************************
        //                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
        //                //ReportParameter pMainTitle = new ReportParameter("pTitle", GetSetData.Data);
        //                //report.SetParameters(pMainTitle);
        //                report.SetParameters(new ReportParameter("pTitle", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
        //                //ReportParameter pAddress = new ReportParameter("pAddress", GetSetData.Data);
        //                //report.SetParameters(pAddress);
        //                report.SetParameters(new ReportParameter("pAddress", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
        //                //ReportParameter pPhoneNo = new ReportParameter("pPhoneNo", GetSetData.Data);
        //                //report.SetParameters(pPhoneNo);
        //                report.SetParameters(new ReportParameter("pPhoneNo", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
        //                //ReportParameter pNote = new ReportParameter("pNote", GetSetData.Data);
        //                //report.SetParameters(pNote);
        //                report.SetParameters(new ReportParameter("pNote", GetSetData.Data));


        //                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
        //                //ReportParameter pCopyrights = new ReportParameter("pCopyrights", GetSetData.Data);
        //                //report.SetParameters(pCopyrights);
        //                report.SetParameters(new ReportParameter("pCopyrights", GetSetData.Data));
        //                //*******************************************************************************************

        //                TextData.general_options = "";
        //                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
        //                //ReportParameter showNote = new ReportParameter("showNote", TextData.general_options);
        //                //report.SetParameters(showNote);
        //                report.SetParameters(new ReportParameter("showNote", TextData.general_options));
        //                //*******************************************************************************************

        //                //PrintToPrinter(report, printer_name);

        //                report.DataSources.Add(new ReportDataSource("cus_sales", dt));

        //                // Enable external images
        //                report.EnableExternalImages = true;

        //                PrintToPrinter(report, printer_name);
        //            }
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //public static void PrintToPrinter(LocalReport report, string printerName)
        //{
        //    try
        //    {
        //        byte[] renderedBytes = report.Render("Image", null, out _, out _, out _, out _, out _);

        //        // Create a print document
        //        using (PrintDocument printDoc = new PrintDocument())
        //        {
        //            printDoc.PrinterSettings.PrinterName = printerName;

        //            if (!printDoc.PrinterSettings.IsValid)
        //            {
        //                throw new Exception("Error: Cannot find the specified printer.");
        //            }
        //            else
        //            {
        //                int m_currentPageIndex = 0;

        //                int desiredPageWidth = 300; // Adjust to your preferred width in pixels
        //                int desiredPageHeight = 800;

        //                printDoc.PrintPage += (sender, e) =>
        //                {
        //                    //if (m_currentPageIndex < report.GetTotalPages())
        //                    //{
        //                    using (MemoryStream ms = new MemoryStream(renderedBytes))
        //                    using (Image renderedImage = Image.FromStream(ms))
        //                    {
        //                        // Adjust the rectangular area with printer margins and set the desired page dimensions
        //                        Rectangle adjustedRect = new Rectangle(
        //                            2,
        //                            0,
        //                            desiredPageWidth, // Set the desired width in pixels
        //                            desiredPageHeight // Set the desired height in pixels
        //                        );


        //                        // Draw a white background for the report
        //                        e.Graphics.FillRectangle(Brushes.White, adjustedRect);

        //                        // Draw the report content
        //                        e.Graphics.DrawImage(renderedImage, adjustedRect);

        //                        // Prepare for the next page. Make sure we haven't hit the end.
        //                        m_currentPageIndex++;
        //                        e.HasMorePages = (m_currentPageIndex < report.GetTotalPages());
        //                    }
        //                    //}
        //                };

        //                // Start the printing process
        //                printDoc.Print();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception gracefully (e.g., log the error)
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}


        private void PrintDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from ReportViewBillWiseSales WHERE (billNo = '" + BillNo + "');";

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
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_sales_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("cus_sales", dt));

                //*********************************************************************************

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

                //*******************************************************************************************

                GetSetData.query = @"select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
                                         where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                Microsoft.Reporting.WinForms.ReportParameter pCounterSalesLastCredits = new Microsoft.Reporting.WinForms.ReportParameter("pCounterSalesLastCredits", GetSetData.Data);
                report.SetParameters(pCounterSalesLastCredits);
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


        private void form_customer_orders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                printSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                allSalesDetails();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                allReturnsDetails();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                SelectedContractForm();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                searchByDate();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void form_customer_orders_FormClosing(object sender, FormClosingEventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Last Receipt Details Form", "Exit...", role_id); 
            TextData.billNo = "";
            //this.Close();
        }
    }
}
