using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.Forms;
using Spices_pos.MessageBoxInfo.forms.Clock_In_out.Reports;

namespace Reports_info.Income_statement
{
    public partial class form_x_report : Form
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

        public form_x_report()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Menus _obj = new Menus();
            _obj.Show();
            this.Dispose();
        }

        private void printXReport()
        {
            try
            {
                GetSetData.query = "SELECT count(id) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                int is_day_end_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                if (is_day_end_exist != 0)
                {
                    clock_out purcahses_report = new clock_out();
                    ReportDataSource purchases_rds = null;
                    SqlDataAdapter purchases_da = null;

                    // ************************************************************************************************************************************************
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
                                    Where (pos_store_day_end.date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";


                    // Script for Purchases ********************************
                    purchases_da = new SqlDataAdapter(GetSetData.query, conn);
                    purchases_da.Fill(purcahses_report, purcahses_report.Tables["storeEndDayDS"].TableName);

                    purchases_rds = new Microsoft.Reporting.WinForms.ReportDataSource("storeEndDayDS", purcahses_report.Tables["storeEndDayDS"]);
                    this.reportviewer1.LocalReport.DataSources.Clear();
                    this.reportviewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    this.reportviewer1.ZoomMode = ZoomMode.Percent;
                    this.reportviewer1.ZoomPercent = 100;
                    this.reportviewer1.LocalReport.DataSources.Add(purchases_rds);
                    this.reportviewer1.LocalReport.EnableExternalImages = true;


                    // Retrive Report Settings from db *******************************************************************************************
                    GetSetData.query = "SELECT sum(cash_amount_received) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalReceivedAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalReceivedAmount = new ReportParameter("pTotalReceivedAmount", totalReceivedAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalReceivedAmount);

                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(opening_cash) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double openingCash = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pOpeningCash = new ReportParameter("pOpeningCash", openingCash.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pOpeningCash);
                    // ******************************************************************************************* 

                    GetSetData.query = "SELECT sum(shortage_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double shortageAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pShortageAmount = new ReportParameter("pShortageAmount", shortageAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pShortageAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total_sales) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalSales = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalSales = new ReportParameter("pTotalSales", totalSales.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalSales);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(credit_card_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double creditCardAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", creditCardAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pCreditCardAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(paypal_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double paypalAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", paypalAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pPaypalAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(google_pay_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double googlePayAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", googlePayAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pGooglePayAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(expected_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double expectedAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pExpectedAmount = new ReportParameter("pExpectedAmount", expectedAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pExpectedAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(balance) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double balance = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pBalance = new ReportParameter("pBalance", balance.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pBalance);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(misc_items_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double miscItemsAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pMiscItemsAmount = new ReportParameter("pMiscItemsAmount", miscItemsAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pMiscItemsAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total_discount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalDiscount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalDiscount = new ReportParameter("pTotalDiscount", totalDiscount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalDiscount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total_return_amount) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalReturnAmount = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalReturnAmount = new ReportParameter("pTotalReturnAmount", totalReturnAmount.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalReturnAmount);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(payout) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalPayout = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pPayout = new ReportParameter("pPayout", totalPayout.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pPayout);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total_taxation) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalTaxation = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalTaxation = new ReportParameter("pTotalTaxation", totalTaxation.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalTaxation);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(pos_void_orders) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalVoidOrders = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pVoidOrders = new ReportParameter("pVoidOrders", totalVoidOrders.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pVoidOrders);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(no_sales) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalNoSale = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pNoSale = new ReportParameter("pNoSale", totalNoSale.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pNoSale);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total_tickets) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double totalTickets = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotalTickets = new ReportParameter("pTotalTickets", totalTickets.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotalTickets);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total100s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total100s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal100s = new ReportParameter("pTotal100s", total100s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal100s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total50s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total50s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal50s = new ReportParameter("pTotal50s", total50s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal50s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total20s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total20s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal20s = new ReportParameter("pTotal20s", total20s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal20s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total10s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total10s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal10s = new ReportParameter("pTotal10s", total10s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal10s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total5s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total5s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal5s = new ReportParameter("pTotal5s", total5s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal5s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total2s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total2s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal2s = new ReportParameter("pTotal2s", total2s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal2s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total1s) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total1s = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal1s = new ReportParameter("pTotal1s", total1s.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal1s);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total1c) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total1c = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal1c = new ReportParameter("pTotal1c", total1c.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal1c);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total5c) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total5c = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal5c = new ReportParameter("pTotal5c", total5c.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal5c);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total10c) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total10c = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal10c = new ReportParameter("pTotal10c", total10c.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal10c);
                    // *******************************************************************************************

                    GetSetData.query = "SELECT sum(total25c) FROM pos_store_day_end  Where (date between '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                    double total25c = data.SearchNumericValuesDb(GetSetData.query);

                    ReportParameter pTotal25c = new ReportParameter("pTotal25c", total25c.ToString());
                    this.reportviewer1.LocalReport.SetParameters(pTotal25c);
                    // *******************************************************************************************


                    GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                    ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                    this.reportviewer1.LocalReport.SetParameters(title);

                    GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                    ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                    this.reportviewer1.LocalReport.SetParameters(address);

                    GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                    ReportParameter phone = new ReportParameter("pPhoneNo", GetSetData.Data);
                    this.reportviewer1.LocalReport.SetParameters(phone);


                    GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                    ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                    this.reportviewer1.LocalReport.SetParameters(copyrights);
                    // *******************************************************************************************


                    this.reportviewer1.RefreshReport();
                }
                else
                {
                    error.errorMessage("No record found! Please create end day first and try again.");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_x_report_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                printXReport();
            }
            catch (Exception es)
            {
                error.errorMessage("No Record Found! Please try another date.");
                error.ShowDialog();
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                printXReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }

       
    }
}
