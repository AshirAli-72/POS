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
using CounterSales_info.CustomerSalesReport;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using OnBarcode.Barcode;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms.Customer_last_receipt
{
    public partial class form_last_receipt : Form
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

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        if (originalExStyle == -1)
        //            originalExStyle = base.CreateParams.ExStyle;

        //        CreateParams handleParam = base.CreateParams;

        //        if (enableFormLevelDoubleBuffering)
        //        {
        //            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
        //        }
        //        else
        //        {
        //            handleParam.ExStyle = originalExStyle;
        //        }

        //        return handleParam;
        //    }
        //}

        //public void TrunOffFormLevelDoubleBuffering()
        //{
        //    enableFormLevelDoubleBuffering = false;
        //    this.MinimizeBox = true;
        //    this.WindowState = FormWindowState.Minimized;
        //}

        public form_last_receipt()
        {
            InitializeComponent(); 
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        int isSaleTransactions = 1; //0 for return, 1 for sale, 2 for noSale 

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Last Receipt Details Form", "Exit...", role_id); 
            TextData.billNo = "";
            this.Dispose();
        }

        private void clearDataGridViewItems()
        {
            this.productDetailGridView.DataSource = null;
            this.productDetailGridView.Refresh();
            productDetailGridView.Rows.Clear();
            productDetailGridView.Columns.Clear();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                if (isSaleTransactions == 1)
                {
                    if (condition == "DateWiseSales")
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerLastReceipt where ([Date] between '" + FromDate.Text +"' and '" + ToDate.Text + "') and ([Is Returned] = 'false')";
                        }
                        else
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerLastReceipt  where ([Date] between '" + FromDate.Text +"' and '" + ToDate.Text +"') and ([Customer] = '" + TextData.customer_name + "') and ([Code] = '" + TextData.customerCode + "') and ([Is Returned] = 'false')";
                        }


                        //********************************************************


                        GetSetData.Data = @"select COUNT(pos_sales_accounts.customer_id) as totalTickets from pos_sales_accounts inner join pos_customers on pos_customers.customer_id = pos_sales_accounts.customer_id
                                            where (pos_sales_accounts.date between '" + FromDate.Text +"' and '" + ToDate.Text +"') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "') and (pos_sales_accounts.is_returned = 'false');";

                        string totalTickets = data.SearchStringValuesFromDb(GetSetData.Data);

                        lblTotalTickets.Text = "Total Tickets: " + totalTickets;

                      
                        //********************************************************


                        GetSetData.Data = @"select SUM(pos_sales_accounts.amount_due) as totalTickets from pos_sales_accounts inner join pos_customers on pos_customers.customer_id = pos_sales_accounts.customer_id
                                            where (pos_sales_accounts.date between '" + FromDate.Text +"' and '" + ToDate.Text +"') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalSales = data.SearchStringValuesFromDb(GetSetData.Data);

                        if (totalSales == "NULL" || totalSales == "")
                        {
                            totalSales = "0";
                        }
                        //********************************************************


                        GetSetData.Data = @"select SUM(pos_return_accounts.amount_due) as totalTickets from pos_return_accounts inner join pos_customers on pos_customers.customer_id = pos_return_accounts.customer_id
                                            where (pos_return_accounts.date between '" + FromDate.Text +"' and '" + ToDate.Text +"') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalReturns = data.SearchStringValuesFromDb(GetSetData.Data);

                        if (totalReturns == "NULL" || totalReturns == "")
                        {
                            totalReturns = "0";
                        }


                        lblSales.Text = "Total Sales: " + (double.Parse(totalSales) - double.Parse(totalReturns)).ToString();
                    }
                    else
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerLastReceipt where ([Is Returned] = 'false')";
                        }
                        else
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerLastReceipt  where ([Customer] = '" + TextData.customer_name + "') and ([Code] = '" + TextData.customerCode + "') and ([Is Returned] = 'false')";
                        }
                    }
                }
                else if(isSaleTransactions == 2)
                {
                    if (condition == "DateWiseNoSale")
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = @"SELECT top 200 pos_no_sale.id as [ID], pos_no_sale.date as [Date], pos_employees.full_name as [Employee], pos_customers.full_name as [Customer], pos_customers.cus_code as [Code],
                                                pos_products.prod_name as [Product Name], pos_no_sale.quantity as [Quantity], 
                                                pos_no_sale.discount as [Discount], pos_no_sale.total_purchase as [Cost Price], pos_no_sale.Total_price as [Sale Price], pos_no_sale.total_marketPrice as [Tax]
                                                FROM pos_no_sale INNER JOIN pos_products ON pos_no_sale.prod_id = pos_products.product_id INNER JOIN
                                                pos_customers ON pos_no_sale.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_no_sale.employee_id = pos_employees.employee_id
                                                where (pos_no_sale.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')";
                        }
                        else
                        {
                            GetSetData.query = @"SELECT top 200 pos_no_sale.id as [ID], pos_no_sale.date as [Date], pos_employees.full_name as [Employee], pos_customers.full_name as [Customer], pos_customers.cus_code as [Code],
                                                pos_products.prod_name as [Product Name], pos_no_sale.quantity as [Quantity], 
                                                pos_no_sale.discount as [Discount], pos_no_sale.total_purchase as [Cost Price], pos_no_sale.Total_price as [Sale Price], pos_no_sale.total_marketPrice as [Tax]
                                                FROM pos_no_sale INNER JOIN pos_products ON pos_no_sale.prod_id = pos_products.product_id INNER JOIN
                                                pos_customers ON pos_no_sale.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_no_sale.employee_id = pos_employees.employee_id
                                                where (pos_no_sale.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.emp_code = '" + TextData.customerCode + "')";
                        }

                        //********************************************************


                        GetSetData.Data = @"select COUNT(pos_no_sale.customer_id) as totalTickets from pos_no_sale inner join pos_customers on pos_customers.customer_id = pos_no_sale.customer_id
                                            where (pos_no_sale.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalTickets = data.SearchStringValuesFromDb(GetSetData.Data);


                        lblTotalTickets.Text = "Total Tickets: " + totalTickets;

                        //********************************************************


                        GetSetData.Data = @"select sum(pos_no_sale.Total_price) as totalTickets from pos_no_sale inner join pos_customers on pos_customers.customer_id = pos_no_sale.customer_id
                                            where (pos_no_sale.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalAmount = data.SearchStringValuesFromDb(GetSetData.Data);

                        if (totalAmount == "NULL" || totalAmount == "")
                        {
                            totalAmount = "0";
                        }

                        lblSales.Text = "Total Amount: " + totalAmount;
                    }
                    else
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = @"SELECT top 200 pos_no_sale.id as [ID], pos_no_sale.date as [Date], pos_employees.full_name as [Employee], pos_customers.full_name as [Customer], pos_customers.cus_code as [Code],
                                                pos_products.prod_name as [Product Name], pos_no_sale.quantity as [Quantity], 
                                                pos_no_sale.discount as [Discount], pos_no_sale.total_purchase as [Cost Price], pos_no_sale.Total_price as [Sale Price], pos_no_sale.total_marketPrice as [Tax]
                                                FROM pos_no_sale INNER JOIN pos_products ON pos_no_sale.prod_id = pos_products.product_id INNER JOIN
                                                pos_customers ON pos_no_sale.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_no_sale.employee_id = pos_employees.employee_id";
                        }
                        else
                        {
                            GetSetData.query = @"SELECT top 200 pos_no_sale.id as [ID], pos_no_sale.date as [Date], pos_employees.full_name as [Employee], pos_customers.full_name as [Customer], pos_customers.cus_code as [Code],
                                                pos_products.prod_name as [Product Name], pos_no_sale.quantity as [Quantity], 
                                                pos_no_sale.discount as [Discount], pos_no_sale.total_purchase as [Cost Price], pos_no_sale.Total_price as [Sale Price], pos_no_sale.total_marketPrice as [Tax]
                                                FROM pos_no_sale INNER JOIN pos_products ON pos_no_sale.prod_id = pos_products.product_id INNER JOIN
                                                pos_customers ON pos_no_sale.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_no_sale.employee_id = pos_employees.employee_id
                                                where (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "')";
                        }
                    }
                }
                else
                {
                    if (condition == "DateWiseReturns")
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerSalesReturnLastReceipt where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "')";
                        }
                        else
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerSalesReturnLastReceipt  where ([Date] between '" + FromDate.Text + "' and '" + ToDate.Text + "') and ([Customer] = '" + TextData.customer_name + "') and ([Code] = '" + TextData.customerCode + "')";
                        }

                        //********************************************************


                        GetSetData.Data = @"select COUNT(pos_return_accounts.customer_id) as totalTickets from pos_return_accounts inner join pos_customers on pos_customers.customer_id = pos_return_accounts.customer_id
                                            where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalTickets = data.SearchStringValuesFromDb(GetSetData.Data);


                        lblTotalTickets.Text = "Total Tickets: " + totalTickets;

                        //********************************************************


                        GetSetData.Data = @"select SUM(pos_return_accounts.amount_due) as totalTickets from pos_return_accounts inner join pos_customers on pos_customers.customer_id = pos_return_accounts.customer_id
                                            where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + TextData.customer_name + "') and (pos_customers.cus_code = '" + TextData.customerCode + "');";

                        string totalReturns = data.SearchStringValuesFromDb(GetSetData.Data);

                        if (totalReturns == "NULL" || totalReturns == "")
                        {
                            totalReturns = "0";
                        }

                        lblSales.Text = "Total Returns: " + totalReturns;
                    }
                    else
                    {
                        if (TextData.customer_name == "" || TextData.customer_name == "nill")
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerSalesReturnLastReceipt where ([Customer] = 'Walk-In')";
                        }
                        else
                        {
                            GetSetData.query = "select top 200 * from ViewCustomerSalesReturnLastReceipt  where ([Customer] = '" + TextData.customer_name + "') and ([Code] = '" + TextData.customerCode + "')";
                        }
                    }
                }


                if (isSaleTransactions == 0 || isSaleTransactions == 1)
                {
                    if (condition == "search")
                    {
                        GetSetData.query = GetSetData.query + " and ([SID] like '%" + search_box.Text + "%' or [Date] like '%" + search_box.Text + "%' or [Receipt No] like '%" + search_box.Text + "%' or [Employee] like '%" + search_box.Text + "%')";
                    }


                    GetSetData.query = GetSetData.query + " order by [SID] desc";
                }
                else
                {
                    if (condition == "search")
                    {
                        GetSetData.query = GetSetData.query + " and (pos_no_sale.id like '%" + search_box.Text + "%' or pos_no_sale.date like '%" + search_box.Text + "%' or pos_employees.full_name like '%" + search_box.Text + "%')";
                    }


                    GetSetData.query = GetSetData.query + " order by pos_no_sale.id desc";
                }

                
                clearDataGridViewItems();


                if (isSaleTransactions == 1)
                {
                    GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                    lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
                    createSelectButtonInGridView();
                }
                else
                {
                    GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                    lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void createSelectButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Return";
            btn.Name = "Return";
            btn.Text = "Return";
            btn.Width = 60;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 0, 0);
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView.Columns.Add(btn);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetSetData.ResetPageNumbers(lblPageNo);

                if (isSaleTransactions == 1)
                {
                    FillGridViewUsingPagination("DateWiseSales");
                }
                else if (isSaleTransactions == 2)
                {
                     FillGridViewUsingPagination("DateWiseNoSale");
                } else
                {
                     FillGridViewUsingPagination("DateWiseReturns");
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }

        }
        private void form_last_receipt_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;
                GetSetData.addFormCopyrights(lblCopyrights);
                TextData.billNo = "";

                title_lable.Text = "Customer Sales Transaction History";
                FillGridViewUsingPagination("");

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isSaleTransactions == 0 || isSaleTransactions == 1)
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                if (isSaleTransactions == 1)
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedureCustomerLastReceiptItems", TextData.billNo);
                }
                else
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedureCustomerLastReceiptReturnItems", TextData.billNo);
                }
            }
        }

        private void reOrderBill()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

                if (TextData.billNo != "")
                {
                    form_counter_sales.isInvoiceReturned = true;
                    TextData.customer_name = productDetailGridView.SelectedRows[0].Cells["Customer"].Value.ToString();
                    TextData.customerCode = productDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();

                    this.Close();
                }
                else
                {
                    error.errorMessage("Please select the any record first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reOrderBill();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            clearDataGridViewItems();
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);

            if (isSaleTransactions == 1)
            {
                createSelectButtonInGridView();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            clearDataGridViewItems();
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);

            if (isSaleTransactions == 1)
            {
                createSelectButtonInGridView();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void form_last_receipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                form_counter_sales.isInvoiceReturned = true;

                reOrderBill();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TextData.billNo = "";

                this.Close();
            }
        }

        private void printSelectedDetails()
        {
            try
            {
                TextData.billNo = "";
                GetSetData.ResetPageNumbers(lblPageNo);
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();


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
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                //error.errorMessage("Please select the invoice first!");
                //error.ShowDialog();
            }
        } 

        private void printSelectedReturnInvoiceDetails()
        {
            try
            {
                TextData.billNo = "";
                GetSetData.ResetPageNumbers(lblPageNo);
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();


                if (TextData.billNo != "")
                {
                    GetSetData.query = @"select default_printer from pos_general_settings;";
                    string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


                    if (printer_name != "")
                    {
                        PrintReturnSaleDirectReceipt(printer_name, TextData.billNo);
                    }
                    else
                    {
                        error.errorMessage("Printer not found!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Sorry no recent bill found!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (isSaleTransactions == 1)
            {
                printSelectedDetails();
            }
            else
            {
                printSelectedReturnInvoiceDetails();
            }
           
        }

        private void PrintReturnSaleDirectReceipt(string printer_name, string BillNo)
        {
            try
            {
                GetSetData.query = @"SELECT * from ReportViewBillWiseSalesReturns WHERE (billNo = '" + BillNo + "');";

                var dt = GetDataTable(GetSetData.query);
                Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();

                string path = Path.GetDirectoryName(Application.StartupPath);
                string fullPath = Path.GetDirectoryName(Application.StartupPath) + @"\Reports\directly_print_return_sales_report.rdlc";
                report.ReportPath = fullPath;

                report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("return_sales", dt));

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

        private void productDetailGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "Return")
            {
                reOrderBill();
            }
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void btnAllReturns_Click(object sender, EventArgs e)
        {
            isSaleTransactions = 0;
            title_lable.Text = "Customer Returns Transaction History";

            FillGridViewUsingPagination("");
        }

        private void btnAllSales_Click(object sender, EventArgs e)
        {
            isSaleTransactions = 1;
            title_lable.Text = "Customer Sales Transaction History";

            FillGridViewUsingPagination("");
        }


        private void btnNoSale_Click(object sender, EventArgs e)
        {
            isSaleTransactions = 2;
            title_lable.Text = "No Sales Transaction History";

            FillGridViewUsingPagination("");
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void form_last_receipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Last Receipt Details Form", "Exit...", role_id); 
            //TextData.billNo = "";
            //this.Close();
        }
    }
}
