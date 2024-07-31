using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Spices_pos.LoginInfo.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using System.Data.SqlClient;
using Message_box_info.forms;
using Customers_info.forms;
using Products_info.forms;
using Purchase_info.forms;
using Recoverier_info.forms;
using Settings_info.forms;
using Stock_management.forms;
using Supplier_Chain_info.forms;
using RefereningMaterial;
using Demands_info.forms;
using Investors_info.forms;
using System.Media;
using Banks_Loan_info.forms;
using LiveCharts;
using LiveCharts.Wpf;
using CounterSales_info.forms;
using Message_box_info.forms.loanPayments;
//using Installments_info.reports;
using Reports_info.CustomerLedgerReports;
using Reports_info.Customer_statements;
using Reports_info.Recoveries;
using Reports_info.Customer_sales_reports.loyal_customer_sales_reports;
using Message_box_info.forms.Clock_In;
//using Separator = LiveCharts.Wpf.Separator;
using Spices_pos.DashboardInfo.CustomControls;
//using Application = Forms.Application;
using Reports_info.Income_statement;
using CounterSales_info.CustomerSalesReport;
using System.Threading;
using Spices_pos.DatabaseInfo.WebConfig;
using System.Windows.Forms;
using Spices_pos.DashboardInfo.controllers;
using Spices_pos.LicenseInfo.forms;
using Spices_pos.LicenseInfo.softwareLicensing;
using Products_info.forms.RecipeDetails;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Spices_pos.DashboardInfo.Forms
{
    public partial class Menus : Form
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


        //private static List<Stream> m_streams;
        //private static int m_currentPageIndex = 0;


        // ******************************************************************************************
        public static int registrations_id = 0;
        public static int user_id = 0;
        public static int role_id = 0;
        public static string authorized_person = "";
        public static string login_checked = "";
        // ******************************************************************************************
        List<double> allValues = new List<double>();
        List<double> allInstallmentValues = new List<double>();
        List<double> allReturnValues = new List<double>();
        List<double> allRecoverAmount = new List<double>();
        // ******************************************************************************************
      
        public Menus()
        {
            InitializeComponent();
            spanel2.Width = 0;

            role_id = Auth.role_id;
            user_id = Auth.user_id;
            authorized_person = Auth.user_name;
        }

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        DashboardPermissionsManager dashboardPermissions = new DashboardPermissionsManager(webConfig.con_string);
        ReportPermissionsManager reportPermissions = new ReportPermissionsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        //done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        //*******************************************************************
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Menus DataContext { get; private set; }
        public Func<double, string> Formatter { get; set; }

        Func<ChartPoint, string> labelPoint = chartpoint => string.Format("{0} ({1:P})", chartpoint.Y, chartpoint.Participation);

        private void setToolTips()
        {
            try
            {
                GetSetData.setToolTipForButtonControl(posBtn, "Make a sale, return items, view shift transactions, transactions history and payments.");
                GetSetData.setToolTipForButtonControl(product_btn, "Create/modify/delete/ products details, set regular, shelf items and print reports.");
                GetSetData.setToolTipForButtonControl(purchase_btn, "Create/return purchasing or receive items and print reports.");
                GetSetData.setToolTipForButtonControl(btnSuppliers, "Create/modify/delete/ vendors/supplier details and print reports.");
                GetSetData.setToolTipForButtonControl(btnEmployees, "Create/modify/delete/ employee/salesman details and print reports.");
                GetSetData.setToolTipForButtonControl(btnCustomers, "Create/modify/delete/ customers details and print reports.");
                GetSetData.setToolTipForButtonControl(btnClockIn, "Employee can clock-in and start shift here and print reports.");
                GetSetData.setToolTipForButtonControl(btnClockInHistory, "Start or end salesman shifts, set counters opening amounts, and print report");
                GetSetData.setToolTipForButtonControl(btnClockOut, "Click here to track time clock history and print report.");
                GetSetData.setToolTipForButtonControl(btnDeals, "Create/modify/delete customized promotions/deals and print reports.");
                GetSetData.setToolTipForButtonControl(btn_stock, "Can track stock history and print report.");
                GetSetData.setToolTipForButtonControl(btnCustomerDues, "Can track creditor customer outstanding balance and print reports.");
                GetSetData.setToolTipForButtonControl(btnDemands, "Create/modify/delete/ purchasing demands and print reports.");
                GetSetData.setToolTipForButtonControl(btnEndDay, "Create/modify store closing here and print X-Report.");
                GetSetData.setToolTipForButtonControl(button_settings, "Click here to open control panel and can on/off dynamic features.");
                GetSetData.setToolTipForButtonControl(button8, "You can logout from the pos.");
                GetSetData.setToolTipForButtonControl(logOutBtn, "You can logout from the pos.");
             
                GetSetData.setToolTipForButtonControl(reportsButton, "Click here to open reports panel.");
                GetSetData.setToolTipForButtonControl(btnXReport, "View and print date wise X-report here.");
                GetSetData.setToolTipForButtonControl(button_sales_report, "View and print daily or date wise sales reports.");
                GetSetData.setToolTipForButtonControl(customer_returns_btn, "View and print daily or date wise returns reports.");
                GetSetData.setToolTipForButtonControl(Cus_ledger_button, "View and print the customers transactions ledger.");
                GetSetData.setToolTipForButtonControl(btnCusStatement, "View and print the customers transactions statement.");
                GetSetData.setToolTipForButtonControl(btnCompanyLedger, "View and print the vendor transactions ledger.");
                GetSetData.setToolTipForButtonControl(btnCompStatement, "View and print the vendor transactions statement.");
                GetSetData.setToolTipForButtonControl(btn_recoveries, "View and print the customers outstanding recoveries.");
                GetSetData.setToolTipForButtonControl(btn_stock_report, "View and print the inventory reports.");
                //GetSetData.setToolTipForButtonControl(btnInstallmentSales, "View and print the installment sales reports.");
                //GetSetData.setToolTipForButtonControl(btn_day_book, "View and print daily transactions day book.");
                GetSetData.setToolTipForButtonControl(btnReceivables, "View and print the business receivable amounts.");
                GetSetData.setToolTipForButtonControl(btnPayables, "View and print the business payable amounts.");
                GetSetData.setToolTipForButtonControl(btnInOutBalance, "View and print the incoming and out going balance.");
                GetSetData.setToolTipForButtonControl(btnCheques, "View and print the customer cheques reports.");
                //GetSetData.setToolTipForButtonControl(btnGenerateInvoices, "View and print the generated invoices.");
                GetSetData.setToolTipForButtonControl(btnProfitLoss, "View and print the profit and loss statement.");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        private void system_user_permissions()
        {
            this.Invoke((MethodInvoker)delegate
            {
                //setFormColorsDynamically();
                //GetSetData.addFormCopyrights(lblCopyrights);
                //***************************************************************************************************

                //MessageBox.Show(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "products", role_id.ToString()));

                product_btn.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "products"));
                btnDeals.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "products"));
                purchase_btn.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "purchases"));
                guna2GroupBox4.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "purchases"));
                guna2GroupBox5.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "expenses"));
                btn_stock.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "stock"));
                posBtn.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "pos"));
                reportsButton.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "reports"));
                btn_db_backup.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "backups"));
                btn_restore.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "restores"));
                button_settings.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "settings"));
                btnCapital.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "capital"));
                btnDailyBalance.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "dailyBalance"));
                btnAbout.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "aboutLicense"));
                btnCharity.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "charity"));
                btnLoanGiven.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "charity"));
                btnCustomers.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "customers"));
                btnEmployees.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "employee"));
                btnSuppliers.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "suppliers"));
                btnDemands.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "logout"));
                btnCustomerDues.Visible = bool.Parse(dashboardPermissions.ReadFieldByRoleId(role_id, "customer_dues"));


                if (generalSettings.ReadField("singleAuthorityClosing") == "Yes")
                {
                    btnEndDay.Visible = true ;
                }
                else
                {
                    btnEndDay.Visible = false;
                }

                #region
                // Reports Permissions

                btnCompanyLedger.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "company_ledger"));
                btnCompStatement.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "company_statement"));
                Cus_ledger_button.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "customer_ledger"));
                btnCusStatement.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "customer_statement"));
                button_sales_report.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "sales_report"));
                guna2GroupBox2.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "sales_report"));
                customer_returns_btn.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "returns_report"));
                btn_day_book.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "day_book"));
                btn_stock_report.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "stock"));
                btn_recoveries.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "recoveries"));
                btnReceivables.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "receivables"));
                btnPayables.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "payables"));
                btnInOutBalance.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "balance_in_out"));
                btnProfitLoss.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "income_statement"));
                //btnCheques.Visible = bool.Parse(reportPermissions.ReadFieldByRoleId(role_id, "chequeDetails"));
              
                #endregion
            });
        }

        #region

        //private void system_user_permissions()
        //{
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        //setFormColorsDynamically();
        //        //GetSetData.addFormCopyrights(lblCopyrights);
        //        //***************************************************************************************************

        //        //MessageBox.Show(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "products", role_id.ToString()));

        //        product_btn.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "products", role_id.ToString()));
        //        btnDeals.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "products", role_id.ToString()));
        //        purchase_btn.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "purchases", role_id.ToString()));
        //        guna2GroupBox4.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "purchases", role_id.ToString()));
        //        guna2GroupBox5.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "expenses", role_id.ToString()));
        //        btn_stock.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "stock", role_id.ToString()));
        //        posBtn.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "pos", role_id.ToString()));
        //        reportsButton.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "reports", role_id.ToString()));
        //        btn_db_backup.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "backups", role_id.ToString()));
        //        btn_restore.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "restores", role_id.ToString()));
        //        button_settings.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "settings", role_id.ToString()));
        //        btnCapital.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "capital", role_id.ToString()));
        //        btnDailyBalance.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "dailyBalance", role_id.ToString()));
        //        btnAbout.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "aboutLicense", role_id.ToString()));
        //        btnCharity.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "charity", role_id.ToString()));
        //        btnLoanGiven.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "charity", role_id.ToString()));
        //        btnCustomers.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "customers", role_id.ToString()));
        //        btnEmployees.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "employee", role_id.ToString()));
        //        btnSuppliers.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "suppliers", role_id.ToString()));
        //        btnDemands.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "logout", role_id.ToString()));
        //        btnCustomerDues.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "customer_dues", role_id.ToString()));

        //        GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
        //        string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


        //        if (singleAuthorityClosing == "Yes")
        //        {
        //            btnEndDay.Visible = true;
        //        }
        //        else
        //        {
        //            btnEndDay.Visible = false;
        //        }

        //        // Reports Permissions

        //        btnCompanyLedger.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "company_ledger", role_id.ToString()));
        //        btnCompStatement.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "company_statement", role_id.ToString()));
        //        Cus_ledger_button.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "customer_ledger", role_id.ToString()));
        //        btnCusStatement.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "customer_statement", role_id.ToString()));
        //        button_sales_report.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "sales_report", role_id.ToString()));
        //        guna2GroupBox2.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "sales_report", role_id.ToString()));
        //        customer_returns_btn.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "returns_report", role_id.ToString()));
        //        btn_day_book.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "day_book", role_id.ToString()));
        //        btn_stock_report.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "stock", role_id.ToString()));
        //        btn_recoveries.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "recoveries", role_id.ToString()));
        //        btnReceivables.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "receivables", role_id.ToString()));
        //        btnPayables.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "payables", role_id.ToString()));
        //        btnInOutBalance.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "balance_in_out", role_id.ToString()));
        //        btnProfitLoss.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "income_statement", role_id.ToString()));
        //        //btnGenerateInvoices.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "generateInvoices", role_id.ToString()));
        //        //btnCheques.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetReportsAuthorities", "chequeDetails", role_id.ToString()));
        //    });
        //}

        //private void system_Reports_permissions()
        //{
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        //***************************************************************************************************
        //        btnCompanyLedger.Visible = bool.Parse(data.UserPermissions("company_ledger", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnCompStatement.Visible = bool.Parse(data.UserPermissions("company_statement", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        Cus_ledger_button.Visible = bool.Parse(data.UserPermissions("customer_ledger", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnCusStatement.Visible = bool.Parse(data.UserPermissions("customer_statement", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        button_sales_report.Visible = bool.Parse(data.UserPermissions("sales_report", "pos_tbl_authorities_reports", role_id));
        //        guna2GroupBox2.Enabled = bool.Parse(data.UserPermissions("sales_report", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        customer_returns_btn.Visible = bool.Parse(data.UserPermissions("returns_report", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btn_day_book.Visible = bool.Parse(data.UserPermissions("day_book", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btn_stock_report.Visible = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_reports", role_id));
        //        //guna2GroupBox1.Enabled = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_reports", role_id));
        //        //guna2GroupBox3.Enabled = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btn_recoveries.Visible = bool.Parse(data.UserPermissions("recoveries", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnReceivables.Visible = bool.Parse(data.UserPermissions("receivables", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnPayables.Visible = bool.Parse(data.UserPermissions("payables", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnInOutBalance.Visible = bool.Parse(data.UserPermissions("balance_in_out", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnProfitLoss.Visible = bool.Parse(data.UserPermissions("income_statement", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnGenerateInvoices.Visible = bool.Parse(data.UserPermissions("generateInvoices", "pos_tbl_authorities_reports", role_id));

        //        //***************************************************************************************************
        //        btnCheques.Visible = bool.Parse(data.UserPermissions("chequeDetails", "pos_tbl_authorities_reports", role_id));
        //    });
        //}

        //private void system_user_permissions()
        //{
        //    this.Invoke((MethodInvoker)delegate
        //    {
        //        setFormColorsDynamically();
        //        //GetSetData.addFormCopyrights(lblCopyrights);
        //        ////***************************************************************************************************
        //        product_btn.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetDashboardAuthorities", "products", role_id.ToString()));

        //        //product_btn.Visible = bool.Parse(data.UserPermissions("products", "pos_tbl_authorities_dashboard", role_id));
        //        //btnDeals.Visible = bool.Parse(data.UserPermissions("products", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        purchase_btn.Visible = bool.Parse(data.UserPermissions("purchases", "pos_tbl_authorities_dashboard", role_id));
        //        guna2GroupBox4.Enabled = bool.Parse(data.UserPermissions("purchases", "pos_tbl_authorities_dashboard", role_id));

        //        guna2GroupBox5.Enabled = bool.Parse(data.UserPermissions("expenses", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btn_stock.Visible = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        posBtn.Visible = bool.Parse(data.UserPermissions("pos", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        reportsButton.Visible = bool.Parse(data.UserPermissions("reports", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btn_db_backup.Enabled = bool.Parse(data.UserPermissions("backups", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btn_restore.Enabled = bool.Parse(data.UserPermissions("restores", "pos_tbl_authorities_dashboard", role_id));


        //        //***************************************************************************************************
        //        button_settings.Visible = bool.Parse(data.UserPermissions("settings", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnCapital.Visible = bool.Parse(data.UserPermissions("capital", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnDailyBalance.Visible = bool.Parse(data.UserPermissions("dailyBalance", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************

        //        //***************************************************************************************************
        //        btnAbout.Visible = bool.Parse(data.UserPermissions("aboutLicense", "pos_tbl_authorities_dashboard", role_id));


        //        //***************************************************************************************************
        //        btnCharity.Visible = bool.Parse(data.UserPermissions("charity", "pos_tbl_authorities_dashboard", role_id));
        //        btnLoanGiven.Visible = bool.Parse(data.UserPermissions("charity", "pos_tbl_authorities_dashboard", role_id));

        //        //new ***************************************************************************************************
        //        btnCustomers.Visible = bool.Parse(data.UserPermissions("customers", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnEmployees.Visible = bool.Parse(data.UserPermissions("employee", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnSuppliers.Visible = bool.Parse(data.UserPermissions("suppliers", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnDemands.Visible = bool.Parse(data.UserPermissions("logout", "pos_tbl_authorities_dashboard", role_id));

        //        //***************************************************************************************************
        //        btnCustomerDues.Visible = bool.Parse(data.UserPermissions("customer_dues", "pos_tbl_authorities_dashboard", role_id));

        //    });
        //}

        #endregion

        private void notificationAlertSound()
        {
            try
            {
                string notificationSound = generalSettings.ReadField("notificationSound");

                if (notificationSound == "Yes")
                {
                    GetSetData.Data = generalSettings.ReadField("picture_path");

                    // Play Sound *****************************************************
                    GetSetData.Data = @"" + GetSetData.Data + "notify.wav";
                    SoundPlayer player = new SoundPlayer(GetSetData.Data);
                    player.Play();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void CheckForLowInventory()
        {
            try
            {
                if (GetSetData.ProcedureGetSingleValues("DashboardProcedureGetCountForLowInventory") != 0)
                {
                    pnlLowInventory.Visible = true;
                }

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void CheckForExpiredInventory()
        {
            try
            {
                if (GetSetData.ProcedureGetSingleValues1("DashboardProcedureGetCountForExpiredInventory", FromDate.Text) != 0)
                {
                    pnlLowInventory.Visible = true;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void notifications()
        {

            //GetSetData.query = "select show_notifications from pos_general_settings";
            //TextData.server = data.SearchStringValuesFromDb(GetSetData.query);

            //GetSetData.query = "select show_graphs from pos_general_settings;";
            //TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);
            
            
            TextData.server = generalSettings.ReadField("show_notifications");
            TextData.general_options = generalSettings.ReadField("show_graphs");


            if (TextData.server == "Enabled")
            {
                CheckForLowInventory();
                CheckForExpiredInventory();
            }
            else
            {
                //btn_notifications.Visible = false;
            }

            if (TextData.general_options == "Yes")
            {
                graph_stock.Visible = true;
                graph_sales.Visible = true;
            }
            else
            {
                graph_stock.Visible = false;
                graph_sales.Visible = false;
            }
        }

        #region
        //private void CheckForAdvancedChequeNotifications()
        //{
        //    try
        //    {
        //        FromDate.Text = DateTime.Now.ToLongDateString();
        //        GetSetData.query = "select show_notifications from pos_general_settings";
        //        TextData.server = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (GetSetData.ProcedureGetSingleValues2("DashboardProcedureGetCountForAvailableCheques", FromDate.Text, FromDate.Text) != 0 && TextData.server == "Enabled")
        //        {
        //            pnlChequeNotification.Visible = true;
        //            btnCheckNotification.Text = "Alert " + GetSetData.ProcedureGetSingleValues2("DashboardProcedureGetCountForAvailableCheques", FromDate.Text, FromDate.Text).ToString() + " Cheques Available!";
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //private void CheckForDefaultersNotifications()
        //{
        //    try
        //    {
        //        FromDate.Text = DateTime.Now.ToLongDateString();
        //        GetSetData.query = "select show_notifications from pos_general_settings";
        //        TextData.server = data.SearchStringValuesFromDb(GetSetData.query);


        //        if (GetSetData.ProcedureGetSingleValues1("DashboardProcedureGetCountForDefaulters", FromDate.Text) != 0 && TextData.server == "Enabled")
        //        {
        //            pnlDefaultersNotification.Visible = true;
        //            btnDefaultersNotification.Text = "Alert " + GetSetData.ProcedureGetSingleValues1("DashboardProcedureGetCountForDefaulters", FromDate.Text).ToString() + " Defaulters Available!";
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}


        //private void Null_expired_items_quantity()
        //{
        //    try
        //    {
        //        GetSetData.query = @"select auto_expiry from pos_general_settings;";
        //        TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (TextData.general_options == "Yes")
        //        {
        //            GetSetData.query = @"select * from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id;";
        //            SqlConnection conn = new SqlConnection(webConfig.con_string);
        //            SqlCommand cmd = new SqlCommand(GetSetData.query, conn);
        //            SqlDataReader reader;

        //            conn.Open();
        //            reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                date_format.Text = reader["expiry_date"].ToString();
        //                string prod_name = reader["prod_name"].ToString();
        //                string barcode = reader["barcode"].ToString();
        //                FromDate.Text = DateTime.Now.ToLongDateString();

        //                if (date_format.Text == FromDate.Text)
        //                {
        //                    GetSetData.query = @"select product_id from pos_products where prod_name = '" + prod_name.ToString() + "' and barcode = '" + barcode.ToString() + "';";
        //                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //                    // ************************************************************************************************
        //                    GetSetData.query = @"select quantity from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double quantity_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select pkg from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double pkg_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select full_pak from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double full_pak_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select pur_price from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double pur_price_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select sale_price from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double sale_price_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select total_pur_price from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double total_pur_price_db = data.SearchNumericValuesDb(GetSetData.query);

        //                    GetSetData.query = @"select total_sale_price from pos_stock_details where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    double total_sale_price_db = data.SearchNumericValuesDb(GetSetData.query);
        //                    // ************************************************************************************************

        //                    GetSetData.query = @"select date from pos_expired_items where date = '" + FromDate.Text + "';";
        //                    string date_expired_items = data.SearchStringValuesFromDb(GetSetData.query);

        //                    if (date_expired_items != date_format.Text && quantity_db > 0)
        //                    {
        //                        string quer_pos_expired_items_db = @"insert into pos_expired_items values ('" + date_format.Text + "' , '" + quantity_db.ToString() + "' , '" + pkg_db.ToString() + "' , '" + full_pak_db.ToString() + "' , '" + pur_price_db.ToString() + "' , '" + sale_price_db.ToString() + "' , '" + total_pur_price_db.ToString() + "' , '" + total_sale_price_db.ToString() + "' , '" + GetSetData.Ids.ToString() + "')";
        //                        data.insertUpdateCreateOrDelete(quer_pos_expired_items_db);
        //                    }
        //                    // ************************************************************************************************

        //                    GetSetData.query = @"update pos_stock_details set quantity = '0', full_pak = '0', total_pur_price = '0', total_sale_price = '0' where prod_id = '" + GetSetData.Ids.ToString() + "';";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);

        //                    GetSetData.query = @"update pos_products set status = 'Disabled' where product_id = '" + GetSetData.Ids.ToString() + "';";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);
        //                }
        //            }

        //            reader.Close();
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //private void customersInstallmentDues()
        //{
        //    try
        //    {
        //        FromDate.Text = DateTime.Now.ToLongDateString();
        //        var nextDay = DateTime.Today.AddDays(1);
        //        double LastCredits_db = 0;

        //        GetSetData.query = @"select autoPenalties from pos_general_settings;";
        //        TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (TextData.general_options == "Yes")
        //        {
        //            GetSetData.query = @"select * from pos_installment_plan inner join pos_installment_accounts on pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id where (status = 'Incomplete') and (nextDueDate = '" + FromDate.Text + "');";
        //            SqlConnection conn = new SqlConnection(webConfig.con_string);
        //            SqlCommand cmd = new SqlCommand(GetSetData.query, conn);
        //            SqlDataReader reader;

        //            conn.Open();
        //            reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                date_format.Text = reader["nextDueDate"].ToString();
        //                string Installment = reader["amount"].ToString();
        //                string Interest = reader["interest"].ToString();
        //                string totalInstallment = reader["total_amount"].ToString();
        //                string duePercentage = reader["due_percentage"].ToString();
        //                string dues = reader["dues"].ToString();
        //                string installment_acc_id_db = reader["installment_acc_id"].ToString();

        //                double totalDues = ((double.Parse(Installment) + double.Parse(Interest)) * double.Parse(duePercentage)) / 100;
        //                double totalAmount = double.Parse(totalInstallment) + totalDues;
        //                totalDues = double.Parse(dues) + totalDues;

        //                if (date_format.Text == FromDate.Text)
        //                {
        //                    GetSetData.query = "update pos_installment_plan set dues = '" + totalDues.ToString() + "' , total_amount = '" + totalAmount.ToString() + "' where (status = 'Incomplete') and (nextDueDate = '" + FromDate.Text + "');";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);

        //                    GetSetData.query = "update pos_installment_plan set nextDueDate = '" + nextDay.ToString() + "' where nextDueDate = '" + FromDate.Text + "';";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);

        //                    int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_installment_accounts", "installment_acc_id", installment_acc_id_db.ToString());
        //                    int customerId_db = data.UserPermissionsIds("customer_id", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //                    LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customerId_db.ToString());

        //                    LastCredits_db += totalDues;
        //                    GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + LastCredits_db.ToString() + "' where customer_id = '" + customerId_db.ToString() + "';";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);
        //                }
        //            }

        //            reader.Close();
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //private void customersInstallmentDues()
        //{
        //    try
        //    {
        //        FromDate.Text = DateTime.Now.ToLongDateString();
        //        //var nextDay = DateTime.Today.AddDays(1);
        //        double LastCredits_db = 0;

        //        GetSetData.query = @"select autoPenalties from pos_general_settings;";
        //        TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (TextData.general_options == "Yes")
        //        {
        //            GetSetData.query = @"select * from pos_installment_plan inner join pos_installment_accounts on pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id where (status = 'Incomplete');";// and (nextDueDate = '" + FromDate.Text + "')
        //            SqlConnection conn = new SqlConnection(webConfig.con_string);
        //            SqlCommand cmd = new SqlCommand(GetSetData.query, conn);
        //            SqlDataReader reader;

        //            conn.Open();
        //            reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                date_format.Text = reader["installmentDate"].ToString();
        //                string principleAmount_db = reader["amount"].ToString();
        //                string Interest_db = reader["interest"].ToString();
        //                string totalInstallment_db = reader["total_amount"].ToString();
        //                string duePercentage_db = reader["due_percentage"].ToString();
        //                string PreviousDues_db = reader["dues"].ToString();
        //                string installment_acc_id_db = reader["installment_acc_id"].ToString();
        //                string installmentPlanId_db = reader["installment_plan_id"].ToString();

        //                //********************************************************
        //                int Payment_year = date_format.Value.Year;
        //                int Payment_month = date_format.Value.Month;
        //                int Payment_day = date_format.Value.Day;

        //                var Payment_actualMonth = new DateTime(double.Parse(Payment_year), Convert.ToInt32(Payment_month), Convert.ToInt32(Payment_day));
        //                //********************************************************

        //                int Current_year = FromDate.Value.Year;
        //                int Current_month = FromDate.Value.Month;
        //                int Current_day = FromDate.Value.Day;

        //                var Current_actualMonth = new DateTime(Convert.ToInt32(Current_year), Convert.ToInt32(Current_month), Convert.ToInt32(Current_day));
        //                //********************************************************

        //                double resultDays = Current_actualMonth.Subtract(Payment_actualMonth).TotalDays;
        //                double totalAmount = 0;
        //                double total_dues = 0;

        //                if (resultDays >= 0)
        //                {
        //                    totalAmount = Math.Round(double.Parse(principleAmount_db) + double.Parse(Interest_db), 2);
        //                    total_dues = Math.Round((totalAmount * double.Parse(duePercentage_db)) / 100, 2);
        //                    total_dues *= resultDays;
        //                    totalAmount += Math.Round(total_dues, 2);

        //                    GetSetData.query = "update pos_installment_plan set dues = '" + total_dues.ToString() + "' , total_amount = '" + totalAmount.ToString() + "' where (installment_plan_id = '" + installmentPlanId_db.ToString() + "') and (status = 'Incomplete');";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);

        //                    int sales_acc_id_db = data.UserPermissionsIds("sales_acc_id", "pos_installment_accounts", "installment_acc_id", installment_acc_id_db.ToString());
        //                    int customerId_db = data.UserPermissionsIds("customer_id", "pos_sales_accounts", "sales_acc_id", sales_acc_id_db.ToString());
        //                    LastCredits_db = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", customerId_db.ToString());

        //                    LastCredits_db =  (LastCredits_db + total_dues) - double.Parse(PreviousDues_db);
        //                    GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + LastCredits_db.ToString() + "' where (customer_id = '" + customerId_db.ToString() + "');";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);
        //                }
        //            }

        //            reader.Close();
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        #endregion

        private void logo_imag()
        {
            try
            {
                TextData.backup_path = generalSettings.ReadField("picture_path");
                TextData.image_path = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "logo_path");

                if (TextData.backup_path != "nill" && TextData.backup_path != "")
                {
                    if (TextData.image_path != "nill" && TextData.image_path != "" && TextData.image_path != null)
                    {
                        logo_img.Image = Bitmap.FromFile(TextData.backup_path + TextData.image_path);
                        logo_img2.Image = Bitmap.FromFile(TextData.backup_path + TextData.image_path);
                    }
                }

                GetSetData.query = @"select title from pos_report_settings;";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);
                lbl_shop_title.Text = GetSetData.Data; 
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

//        private List<string> cartesianChartSetMonths()
//        {
//            var dayLabels = new List<string>();
//            try
//            {
//                if (data.conn_.State == ConnectionState.Open)
//                {
//                    data.conn_.Close();
//                }

//                data.conn_.Open();
//                //GetSetData.query = @"SELECT pos_sales_accounts.date, sum(amount_due) as dueAmount FROM pos_sales_accounts WHERE DATEDIFF(DAY, CONVERT(datetime, pos_sales_accounts.date,7), GETDATE()) <= 7 group by date";
//                GetSetData.query = @"SELECT DATENAME(MONTH, dbo.pos_sales_accounts.date) AS MonthValue
//                                    FROM dbo.pos_recovery_details CROSS JOIN dbo.pos_return_accounts CROSS JOIN dbo.pos_sales_accounts
//                                    WHERE (YEAR(dbo.pos_sales_accounts.date) = YEAR(GETDATE())) AND  (YEAR(dbo.pos_return_accounts.date) = YEAR(GETDATE()))
//                                    AND (YEAR(dbo.pos_recovery_details.date) = YEAR(GETDATE())) GROUP BY DATENAME(MONTH, dbo.pos_sales_accounts.date) 
//                                    ORDER BY MIN(dbo.pos_sales_accounts.date)";

//                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
//                data.reader_ = data.cmd_.ExecuteReader();

//                while (data.reader_.Read())
//                {
//                    var d = data.reader_.GetString(data.reader_.GetOrdinal("MonthValue"));
//                    dayLabels.Add(d);
//                }

//                data.reader_.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.ToString());
//            }

//            return dayLabels;
//        }

        private List<string> cartesianChartSalesAndInsatallment()
        {
            var dayLabels = new List<string>();
            try
            {
                if (data.conn_.State == ConnectionState.Open)
                {
                    data.conn_.Close();
                }

                data.conn_.Open();
                //GetSetData.query = @"SELECT pos_sales_accounts.date, sum(amount_due) as dueAmount FROM pos_sales_accounts WHERE DATEDIFF(DAY, CONVERT(datetime, pos_sales_accounts.date,7), GETDATE()) <= 7 group by date";
                GetSetData.query = @"SELECT DATENAME(MONTH, d.date) AS MonthValue, round(SUM(CASE WHEN d.status = 'Sale' THEN amount_due ELSE 0 END), 2) AS DirectSales, 
                                    round(SUM(CASE WHEN d.status != 'Sale' THEN amount_due ELSE 0 END), 2) AS InstallmentSales FROM pos_sales_accounts d WHERE YEAR(d.date) = YEAR(GETDATE()) 
                                    GROUP BY DATENAME(MONTH, d.date) ORDER BY MIN(d.date)";

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                while (data.reader_.Read())
                {
                    // Check for DBNull and then assign the variable
                    if (data.reader_["DirectSales"] != DBNull.Value)
                        allValues.Add(double.Parse(data.reader_["DirectSales"].ToString()));

                    // Check for DBNull and then assign the variable
                    if (data.reader_["InstallmentSales"] != DBNull.Value)
                        allInstallmentValues.Add(double.Parse(data.reader_["InstallmentSales"].ToString()));

                    var d = data.reader_.GetString(data.reader_.GetOrdinal("MonthValue"));
                    dayLabels.Add(d);
                }
                data.reader_.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

            return dayLabels;
        }

        private List<string> cartesianChartReturnItems()
        {
            var dayLabels = new List<string>();
            try
            {
                if (data.conn_.State == ConnectionState.Open)
                {
                    data.conn_.Close();
                }

                data.conn_.Open();
                //GetSetData.query = @"SELECT pos_sales_accounts.date, sum(amount_due) as dueAmount FROM pos_sales_accounts WHERE DATEDIFF(DAY, CONVERT(datetime, pos_sales_accounts.date,7), GETDATE()) <= 7 group by date";
                GetSetData.query = @"SELECT DATENAME(MONTH, d.date) AS MonthValue, round(SUM(amount_due), 2) AS dueAmount
                                     FROM pos_return_accounts d WHERE (YEAR(d.date) = YEAR(GETDATE())) GROUP BY DATENAME(MONTH, d.date) ORDER BY MIN(d.date)";

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                while (data.reader_.Read())
                {
                    if (data.reader_["dueAmount"] != DBNull.Value)
                        allReturnValues.Add(double.Parse(data.reader_["dueAmount"].ToString()));

                    var d = data.reader_.GetString(data.reader_.GetOrdinal("MonthValue"));
                    dayLabels.Add(d);
                }
                data.reader_.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return dayLabels;
        }

        private void cartesianChartRcoveries()
        {
            var dayLabels = new List<string>();
            try
            {
                if (data.conn_.State == ConnectionState.Open)
                {
                    data.conn_.Close();
                }

                data.conn_.Open();
                //GetSetData.query = @"SELECT pos_sales_accounts.date, sum(amount_due) as dueAmount FROM pos_sales_accounts WHERE DATEDIFF(DAY, CONVERT(datetime, pos_sales_accounts.date,7), GETDATE()) <= 7 group by date";
                GetSetData.query = @"SELECT DATENAME(MONTH, d.date) AS MonthValue, round(SUM(amount), 2) AS dueAmount
                                     FROM pos_recoveries inner join pos_recovery_details d on pos_recoveries.recoveries_id = d.recovery_id
									 WHERE (YEAR(d.date) = YEAR(GETDATE())) GROUP BY DATENAME(MONTH, d.date) ORDER BY MIN(d.date)";

                data.cmd_ = new SqlCommand(GetSetData.query, data.conn_);
                data.reader_ = data.cmd_.ExecuteReader();

                while (data.reader_.Read())
                {
                    if (data.reader_["dueAmount"] != DBNull.Value)
                        allRecoverAmount.Add(double.Parse(data.reader_["dueAmount"].ToString()));

                    var d = data.reader_.GetString(data.reader_.GetOrdinal("MonthValue"));
                    dayLabels.Add(d);
                }
                data.reader_.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //return dayLabels;
        }

        private void ChartValues()
        {
            try
            {
                TextData.general_options = generalSettings.ReadField("show_graphs");

                if (TextData.general_options == "Yes")
                {
                    cartesianChartSalesAndInsatallment();
                    cartesianChartReturnItems();
                    cartesianChartRcoveries();

                    SeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Sales",
                            Values = new ChartValues<double>(allValues),
                            DataContext = true,
                            PointGeometrySize = 11
                        },
                        //new LineSeries
                        //{
                        //    Title = "Installment Sales",
                        //    Values = new ChartValues<double>(allInstallmentValues),
                        //    DataContext = true,
                        //    PointGeometrySize = 11
                        //},
                        new LineSeries
                        {
                            Title = "Returns",
                            Values = new ChartValues<double>(allReturnValues),
                            DataContext = true,
                            PointGeometrySize = 11
                        }
                        //new LineSeries
                        //{
                        //    Title = "Recoveries",
                        //    Values = new ChartValues<double>(allRecoverAmount),
                        //    DataContext = true,
                        //    PointGeometrySize = 11
                        //}
                    };
                    
                    Axis axisX = new Axis()
                    {
                        Separator = new Separator() { Step = 1, IsEnabled = false },
                        //Labels = new List<string>()
                        Labels = new List<string>(cartesianChartSalesAndInsatallment())
                    };

                    //Labels = new[]
                    //{
                    //    "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                    ////Labels = new[]{"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}
                    //};

                    graph_stock.Series = SeriesCollection;
                    graph_stock.AxisX.Add(axisX);
                    //graph_stock.AxisX.Add(axisX1);
                    //graph_stock.AxisX.Add(axisX2);
                    graph_stock.LegendLocation = LegendLocation.Bottom;
                }
                //DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #region
        //private void ChartValues()
        //{

        //    // Defines the variable for differnt lines.
        //    List<double> soldItems = new List<double>();
        //    List<double> ResidentialValues = new List<double>();


        //    try
        //    {
        //        GetSetData.query = @"SELECT pos_sales_accounts.date, sum(amount_due) as dueAmount FROM pos_sales_accounts WHERE DATEDIFF(DAY, CONVERT(datetime, pos_sales_accounts.date,7), GETDATE()) <= 7 group by date";
        //        SqlConnection conn = new SqlConnection(webConfig.con_string);
        //        SqlCommand cmd = new SqlCommand(GetSetData.query, conn);
        //        SqlDataReader reader;

        //        conn.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            date_format.Text = reader["date"].ToString();
        //            string amountDue = reader["dueAmount"].ToString();
        //            FromDate.Text = DateTime.Now.ToLongDateString();

        //            ColumnSeries series2 = new ColumnSeries()
        //            {

        //                Title = "Sold Items",
        //                Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfAssetsAmount") },
        //                DataLabels = true,
        //                LabelPoint = labelPoint

        //            };
        //            Axis axisX = new Axis()
        //            {
        //                Title = "Day",
        //                Separator = new Separator() { Step = 1, IsEnabled = false },
        //                Labels = new List<string>()

        //            };
        //            Axis axisY = new Axis()
        //            {
        //                LabelFormatter = y => y.ToString(),
        //                Separator = new Separator()

        //            };
        //            graph_stock.Series.Add(series2);
        //            graph_stock.AxisX.Add(axisX);
        //            graph_stock.AxisY.Add(axisY);

        //            series2.Values.Add(amountDue);
        //            graph_stock.LegendLocation = LegendLocation.Bottom;

        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //}
        #endregion
        private void fillTopSellingItemsInPanel()
        {
            try
            {
                int rowNumber = 1;
                
                GetSetData.query = @"SELECT TOP 5 prod_id, SUM(Quantity) AS TotalQuantity, SUM(Total_price) as TotalPrice
                                     FROM pos_sales_details
                                     INNER JOIN pos_sales_accounts ON pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                     WHERE date >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
                                     AND date < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                                     GROUP BY prod_id ORDER BY TotalQuantity DESC;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lowInventoryCustom cartItem = new lowInventoryCustom();

                    cartItem.RowNumber = rowNumber.ToString();

                    string brandId = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "brand_id", "pos_products", "product_id", reader["prod_id"].ToString());

                    cartItem.ItemsName = data.UserPermissions("prod_name", "pos_products", "product_id", reader["prod_id"].ToString());
                    cartItem.Brand = data.UserPermissions("brand_title", "pos_brand", "brand_id", brandId);
                    cartItem.Quantity = reader["TotalQuantity"].ToString();
                    cartItem.Amount = GetSetData.currency() + reader["TotalPrice"].ToString();


                    cartItem.Dock = DockStyle.Bottom;
                    panelTopSellingProducts.Controls.Add(cartItem);
                   
                    rowNumber++;
                }

                reader.Close();
                conn.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void fun_graph_details()
        {
            try
            {
                fillTopSellingItemsInPanel();

                // ********************************************************************************** 
                
                string totalInventory = GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfStock").ToString();
                string totalLowInventory = GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfLowInventory").ToString();
                lblTotalPurchasing.Text = GetSetData.currency() + (double.Parse(GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfPurchases").ToString()) + double.Parse(GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfSupplierPayments").ToString())).ToString();


                txtDate.Text = DateTime.Now.ToLongDateString();
                double currentDaySales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", txtDate.Text);
                double currentDaySaleReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", txtDate.Text);

                lblTotalSold.Text = GetSetData.currency() + (currentDaySales - currentDaySaleReturns).ToString();

                // ********************************************************************************** 

                GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') ORDER BY id DESC;";
                double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);


                double shiftSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "clock_in_id", clock_in_id.ToString());
                double shiftSaleReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "clock_in_id", clock_in_id.ToString());


                lblShiftSales.Text = GetSetData.currency() + (shiftSales - shiftSaleReturns).ToString();

                // ********************************************************************************** 

                double totalCustomers = data.NumericValues("round(count(customer_id), 2)", "pos_sales_accounts", "date", txtDate.Text);


                lblTotalCustomers.Text = totalCustomers.ToString();

                // ********************************************************************************** 
                double overallInventory  = double.Parse(totalInventory);

                progressBarOverallStock.Value = Convert.ToInt32(overallInventory);
                txtOverAllStockPercentage.Text = Convert.ToInt32(overallInventory).ToString();

                double lowStock = overallInventory;

                if (lowStock <= 0)
                {
                    lowStock = 1;
                }

                progressBarLowStock.Value = Convert.ToInt32((double.Parse(totalLowInventory) / lowStock) * 100);
                txtLowStockPercentage.Text = Convert.ToInt32((double.Parse(totalLowInventory) / lowStock) * 100).ToString() + "%";

                // ********************************************************************************** 

                string employee_id_db = data.UserPermissions("emp_id", "pos_users", "user_id", user_id.ToString());
                string totalComission = data.UserPermissions("commission", "pos_employees", "employee_id", employee_id_db.ToString());


                if (totalComission == "" || totalComission == "NULL")
                {
                    totalComission = "0";
                }

                lblEmployeeComission.Text = GetSetData.currency() + totalComission;

                // ********************************************************************************** 


                double TotalSales = 0;
                double TotalReturns = 0;

                DateTime currentDate = DateTime.Now;
                DateTime date2 = currentDate.AddDays(-1);
                DateTime date3 = currentDate.AddDays(-2);
                DateTime date4 = currentDate.AddDays(-3);
                DateTime date5 = currentDate.AddDays(-4);
                DateTime date6 = currentDate.AddDays(-5);
                DateTime date7 = currentDate.AddDays(-6);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", currentDate.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", currentDate.ToString("yyyy-MM-dd"));
                day7ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date2.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date2.ToString("yyyy-MM-dd"));
                day6ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date3.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date3.ToString("yyyy-MM-dd"));
                day5ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date4.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date4.ToString("yyyy-MM-dd"));
                day4ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date5.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date5.ToString("yyyy-MM-dd"));
                day3ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date6.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date6.ToString("yyyy-MM-dd"));
                day2ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

                // ***************************

                TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date7.ToString("yyyy-MM-dd"));
                TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date7.ToString("yyyy-MM-dd"));
                day1ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);


                // **********************************************************************************
                GetSetData.setProgressBarColor(day1ProgressBar, day1ProgressBar.Value);
                GetSetData.setProgressBarColor(day2ProgressBar, day2ProgressBar.Value);
                GetSetData.setProgressBarColor(day3ProgressBar, day3ProgressBar.Value);
                GetSetData.setProgressBarColor(day4ProgressBar, day4ProgressBar.Value);
                GetSetData.setProgressBarColor(day5ProgressBar, day5ProgressBar.Value);
                GetSetData.setProgressBarColor(day6ProgressBar, day6ProgressBar.Value);
                GetSetData.setProgressBarColor(day7ProgressBar, day7ProgressBar.Value);

                // **********************************************************************************

                TextData.general_options = generalSettings.ReadField("show_graphs");

                if (TextData.general_options == "Yes")
                {
                    SeriesCollection series = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Assets",
                            Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfAssetsAmount") },
                            //DataLabels = true,
                            LabelPoint = labelPoint
                        },
                        new PieSeries
                        {
                            Title = "Receivables",
                            Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfReceivablesAmount") - GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfDuesAmount") },
                            //DataLabels = true,
                            LabelPoint = labelPoint
                        },
                        new PieSeries
                        {
                            Title = "Payables",
                            Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfPayablesAmount") },
                            //DataLabels = true,
                            LabelPoint = labelPoint
                        }
                    };

                    graph_sales.Series = series;
                    graph_sales.InnerRadius = 45;
                    //graph_sales.LegendLocation = LegendLocation.Bottom;

                    // **********************************************************************************
                    ChartValues();
                }
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        } 


        //private void fun_graph_details()
        //{
        //    try
        //    {
        //        fillTopSellingItemsInPanel();

        //        // ********************************************************************************** 
                
        //        string totalInventory = GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfStock").ToString();
        //        string totalLowInventory = GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfLowInventory").ToString();
        //        lblTotalPurchasing.Text = GetSetData.currency() + (double.Parse(GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfPurchases").ToString()) + double.Parse(GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfSupplierPayments").ToString())).ToString();
               

        //        txtDate.Text = DateTime.Now.ToLongDateString();


        //        //double currentDaySales = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "round(sum(amount_due), 2)", "pos_sales_accounts", "date", txtDate.Text);
        //        //double currentDaySaleReturns = GetSetData.ProcedureGetNumericValues("ProcedureGetNumericValues", "round(sum(amount_due), 2)", "pos_return_accounts", "date", txtDate.Text);

        //        double currentDaySales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", txtDate.Text);
        //        double currentDaySaleReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", txtDate.Text);

        //        lblTotalSold.Text = GetSetData.currency() +  (currentDaySales - currentDaySaleReturns).ToString();

        //        // ********************************************************************************** 

        //        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') ORDER BY id DESC;";
        //        double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

                
        //        double shiftSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "clock_in_id", clock_in_id.ToString());
        //        double shiftSaleReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "clock_in_id", clock_in_id.ToString());


        //        lblShiftSales.Text = GetSetData.currency() + (shiftSales - shiftSaleReturns).ToString();

        //        // ********************************************************************************** 

        //        double totalCustomers = data.NumericValues("round(count(customer_id), 2)", "pos_sales_accounts", "date", txtDate.Text);


        //        lblTotalCustomers.Text =  totalCustomers.ToString();

        //        // ********************************************************************************** 


        //        progressBarOverallStock.Value = Convert.ToInt32(totalInventory);
        //        txtOverAllStockPercentage.Text = Convert.ToInt32(totalInventory).ToString();

        //        double lowStock = double.Parse(totalInventory);

        //        if (lowStock <= 0)
        //        {
        //            lowStock = 1;
        //        }

        //        progressBarLowStock.Value = Convert.ToInt32((double.Parse(totalLowInventory) / lowStock) * 100);
        //        txtLowStockPercentage.Text = Convert.ToInt32((double.Parse(totalLowInventory) / lowStock) * 100).ToString() + "%";

        //        // ********************************************************************************** 

        //        string employee_id_db = data.UserPermissions("emp_id", "pos_users", "user_id", user_id.ToString());

        //        string totalComission = data.UserPermissions("commission", "pos_employees", "employee_id", employee_id_db.ToString());

                
        //        if (totalComission == "" || totalComission == "NULL")
        //        {
        //            totalComission = "0";
        //        }

        //        lblEmployeeComission.Text = GetSetData.currency() + totalComission;

        //        // ********************************************************************************** 
             

        //        double TotalSales = 0;
        //        double TotalReturns = 0;

        //        DateTime currentDate = DateTime.Now;
        //        DateTime date2 = currentDate.AddDays(-1);
        //        DateTime date3 = currentDate.AddDays(-2);
        //        DateTime date4 = currentDate.AddDays(-3);
        //        DateTime date5 = currentDate.AddDays(-4);
        //        DateTime date6 = currentDate.AddDays(-5);
        //        DateTime date7 = currentDate.AddDays(-6);

        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", currentDate.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", currentDate.ToString("yyyy-MM-dd"));
        //        day7ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date2.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date2.ToString("yyyy-MM-dd"));
        //        day6ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date3.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date3.ToString("yyyy-MM-dd"));
        //        day5ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);
                
        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date4.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date4.ToString("yyyy-MM-dd"));
        //        day4ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);
  
        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date5.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date5.ToString("yyyy-MM-dd"));
        //        day3ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);

        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date6.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date6.ToString("yyyy-MM-dd"));
        //        day2ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);    

        //        // ***************************

        //        TotalSales = data.NumericValues("round(sum(amount_due), 2)", "pos_sales_accounts", "date", date7.ToString("yyyy-MM-dd"));
        //        TotalReturns = data.NumericValues("round(sum(amount_due), 2)", "pos_return_accounts", "date", date7.ToString("yyyy-MM-dd"));
        //        day1ProgressBar.Value = Convert.ToInt32(TotalSales - TotalReturns);


        //        // **********************************************************************************
        //        GetSetData.setProgressBarColor(day1ProgressBar, day1ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day2ProgressBar, day2ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day3ProgressBar, day3ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day4ProgressBar, day4ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day5ProgressBar, day5ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day6ProgressBar, day6ProgressBar.Value);
        //        GetSetData.setProgressBarColor(day7ProgressBar, day7ProgressBar.Value);
                
        //        // **********************************************************************************

        //        GetSetData.query = @"select show_graphs from pos_general_settings;";
        //        TextData.general_options = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (TextData.general_options == "Yes")
        //        {
        //            SeriesCollection series = new SeriesCollection
        //            {
        //                new PieSeries
        //                {
        //                    Title = "Assets",
        //                    Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfAssetsAmount") },
        //                    //DataLabels = true,
        //                    LabelPoint = labelPoint
        //                },
        //                new PieSeries
        //                {
        //                    Title = "Receivables",
        //                    Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfReceivablesAmount") - GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfDuesAmount") },
        //                    //DataLabels = true,
        //                    LabelPoint = labelPoint
        //                },
        //                new PieSeries
        //                {
        //                    Title = "Payables",
        //                    Values = new ChartValues<double> { GetSetData.ProcedureGetSingleValues("DashboardProcedureGetSumOfPayablesAmount") },
        //                    //DataLabels = true,
        //                    LabelPoint = labelPoint
        //                }
        //            };

        //            graph_sales.Series = series;
        //            graph_sales.InnerRadius = 45;
        //            //graph_sales.LegendLocation = LegendLocation.Bottom;

        //            // **********************************************************************************
        //            ChartValues();
        //        }
                
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        private void clock_timer_Tick(object sender, EventArgs e)
        {   
            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
            string is_exist = data.SearchStringValuesFromDb(GetSetData.query);

            if (is_exist != "")
            {
                lbl_time.Text = DateTime.Now.ToLongTimeString();

                GetSetData.query = "SELECT TOP 1 date FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string strartDateDb = data.SearchStringValuesFromDb(GetSetData.query);

                GetSetData.query = "SELECT TOP 1 start_time FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0' or status = '-1') ORDER BY id DESC;";
                string strartTimeDb = data.SearchStringValuesFromDb(GetSetData.query);

                if (strartDateDb != "" && strartTimeDb != "")
                {
                    string endTimeNow = DateTime.Now.ToLongTimeString();

                    DateTime startTime = DateTime.Parse(strartDateDb) + DateTime.Parse(strartTimeDb).TimeOfDay;
                    DateTime endTime = DateTime.Today + DateTime.Parse(endTimeNow).TimeOfDay;

                    // Calculate duration
                    TimeSpan duration = endTime - startTime;

                    // Get total hours
                    //double totalHours = duration.TotalHours;


                    lblShiftDuration.Text = duration.ToString();
                }
            }
            // **********************************************************************************
        }

        private void Menus_Shown(object sender, EventArgs e)
        {
            try
            {
                form_loading loadingForm = new form_loading();
                loadingForm.SetLoadingMessage("Loading Dashboard Data...");
                //loadingForm.TopMost = true;
                loadingForm.Show();

                Thread loading = new Thread(() => LoadThreadMethod((message) =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        loadingForm.Dispose();
                    });
                }));

                loading.Start();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void loadingData()
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    clock_timer.Start();
                    logo_imag();
                    fun_graph_details();

                    system_user_permissions();
                    setToolTips();
                    //*************************************

                    dates.Text = DateTime.Now.ToLongDateString();
                    FromDate.Text = DateTime.Now.ToLongDateString();
                    //title_lable.Text = authorized_person;
                    title_lable.Text = Auth.user_name;

                    notifications();
                    //CheckForAdvancedChequeNotifications();
                    //CheckForDefaultersNotifications();

                    if (pnlDefaultersNotification.Visible == true || pnlChequeNotification.Visible == true || pnlLowInventory.Visible == true)
                    {
                        notificationAlertSound();
                    }

                });
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoadThreadMethod(Action<string> callback)
        {
            try
            {
                loadingData();

                this.Invoke(new Action(() =>
                {
                    callback?.Invoke("Data Loaded...");
                }));
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }


        //private bool defaultShiftClockIn()
        //{
        //    try
        //    {
        //        GetSetData.query = "SELECT id from pos_default_shifts where (user_id = '" + user_id.ToString() + "');";
        //        string default_shift_id = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (default_shift_id != "")
        //        {
        //            txtDate.Text = DateTime.Now.ToLongDateString();

        //            string opening_amount = data.UserPermissions("opening_amount", "pos_default_shifts", "id", default_shift_id);
        //            string counter_id = data.UserPermissions("counter_id", "pos_default_shifts", "id", default_shift_id);
        //            string shift_id = data.UserPermissions("shift_id", "pos_default_shifts", "id", default_shift_id);


        //            GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') and (date = '" + txtDate.Text + "') ORDER BY id DESC;";
        //            double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

        //            if (clock_in_id == 0)
        //            {

        //                string start_time = "";
        //                start_time = lbl_time.Text;

        //                GetSetData.query = @"insert into pos_clock_in values ('" + txtDate.Text + "','" + start_time.ToString() + "' , '" + opening_amount + "', 'nill', '0', '" + shift_id.ToString() + "', '" + user_id.ToString() + "', '" + user_id.ToString() + "', '" + counter_id.ToString() + "');";
        //                data.insertUpdateCreateOrDelete(GetSetData.query);

        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage("Please clock-out the other clock-in shifts first.");
        //            error.ShowDialog();
        //        }

        //        return false;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        return false;
        //    }
        //}
        //private bool batchWiseAutoClockIn()
        //{
        //    try
        //    {
        //        GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '-2' or status = '0') and (date = '" + txtDate.Text + "') ORDER BY id DESC;";
        //        string is_already_clocked_in = data.SearchStringValuesFromDb(GetSetData.query);


        //        if (is_already_clocked_in == "")
        //        {
        //            txtDate.Text = DateTime.Now.ToLongDateString();
        //            string shift_id = "";
        //            string counter_id = "";


        //            //*********************************

        //            SqlConnection connForCounter = new SqlConnection(webConfig.con_string);

        //            string queryForCounters = "select id from pos_counter;";


        //            SqlCommand cmdCounters;
        //            SqlDataReader readerCounters;

        //            cmdCounters = new SqlCommand(queryForCounters, connForCounter);

        //            connForCounter.Open();
        //            readerCounters = cmdCounters.ExecuteReader();


        //            while (readerCounters.Read())
        //            {
        //                GetSetData.query = "SELECT count(pos_clock_in.counter_id) from pos_clock_in where (status = '-1' or status = '0') and (date = '" + txtDate.Text + "') and (counter_id = '" + readerCounters["id"].ToString() + "') ORDER BY id DESC;";
        //                double allocatedShifts = data.SearchNumericValuesDb(GetSetData.query);


        //                double counterShiftLimitDb = data.UserPermissionsIds("shift_limit", "pos_counter", "id", readerCounters["id"].ToString());

        //                if (allocatedShifts <= counterShiftLimitDb)
        //                {
        //                    //GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (status = '-1' or status = '0') and (date = '" + txtDate.Text + "') and (counter_id = '" + readerCounters["id"].ToString() + "') ORDER BY id DESC;";
        //                    //string is_counter_exists = data.SearchStringValuesFromDb(GetSetData.query);

        //                    //if (is_counter_exists == "")
        //                    //{
        //                    counter_id = readerCounters["id"].ToString();
        //                    break;
        //                    //}
        //                }
        //                else
        //                {
        //                    error.errorMessage("Sorry this counter is full or limit! Please create new counter to proceed.");
        //                    error.ShowDialog();
        //                }
        //            }

        //            readerCounters.Close();

        //            //*********************************

        //            if (counter_id != "")
        //            {
        //                string queryForShifts = "select id from pos_shift;";


        //                SqlConnection conn = new SqlConnection(webConfig.con_string);
        //                SqlCommand cmd;
        //                SqlDataReader reader;

        //                cmd = new SqlCommand(queryForShifts, conn);

        //                conn.Open();
        //                reader = cmd.ExecuteReader();


        //                while (reader.Read())
        //                {
        //                    GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (status = '-1' or status = '0') and (date = '" + txtDate.Text + "')  and (counter_id = '" + counter_id + "') and (shift_id = '" + reader["id"].ToString() + "') ORDER BY id DESC;";
        //                    string is_shift_exists = data.SearchStringValuesFromDb(GetSetData.query);

        //                    if (is_shift_exists == "")
        //                    {
        //                        shift_id = reader["id"].ToString();
        //                        break;
        //                    }
        //                }

        //                reader.Close();
        //            }
        //            else
        //            {
        //                error.errorMessage("Sorry no counter found!");
        //                error.ShowDialog();
        //            }


        //            if (shift_id != "")
        //            {
        //                if (counter_id != "")
        //                {
        //                    GetSetData.query = "SELECT TOP 1 id from pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') and (date = '" + txtDate.Text + "') and (counter_id = '" + counter_id + "') and (shift_id = '" + shift_id + "') ORDER BY id DESC;";
        //                    double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

        //                    if (clock_in_id == 0)
        //                    {
        //                        string start_time = "";
        //                        start_time = lbl_time.Text;


        //                        //GetSetData.query = @"update pos_clock_in set status  = '-2';";
        //                        //data.insertUpdateCreateOrDelete(GetSetData.query);


        //                        GetSetData.query = @"insert into pos_clock_in values ('" + txtDate.Text + "','" + start_time.ToString() + "' , '0', 'nill', '0', '" + shift_id.ToString() + "', '" + user_id.ToString() + "', '" + user_id.ToString() + "', '" + counter_id.ToString() + "');";
        //                        data.insertUpdateCreateOrDelete(GetSetData.query);


        //                        return true;
        //                    }
        //                }
        //                else
        //                {
        //                    error.errorMessage("Sorry no counter available now. Please create new counter to proceed!");
        //                    error.ShowDialog();
        //                }
        //            }
        //            else
        //            {
        //                error.errorMessage("Sorry no shift available now. Please create new shift to proceed!");
        //                error.ShowDialog();
        //            }
        //        }


        //        return false;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        return false;
        //    }
        //}


        private void Save_login_details_button()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.image_path = GetSetData.GetSystemMacAddress();

                if (TextData.image_path != "")
                {
                    GetSetData.query = @"insert into pos_login_details values ('" + TextData.image_path + "' , '" + DateTime.Now.ToShortDateString() + "' , '" + DateTime.Now.ToLongTimeString() + "', 'Logout' , '" + user_id.ToString() +"');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (spanel.Width != guna2Button1.Width)
            {
                spanel.Width = guna2Button1.Width;
                lbl_shop_title.Visible = false;
                logo_img.Visible = false;
                logo_img2.Visible = true;
            }
            else
            {
                spanel.Width = 185;
                lbl_shop_title.Visible = true;
                logo_img.Visible = true;
                logo_img2.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (spanel2.Width != pnl_reports_button.Width)
            {
                spanel2.Width = 162;

                guna2GroupBox1.Visible = false;
                guna2GroupBox7.Visible = false;
            }
            else
            {
                spanel2.Width = 0;
                guna2GroupBox1.Visible = true;
                guna2GroupBox7.Visible = true;
            }
        }

        private void logOut_Click(object sender, EventArgs e)
        {
            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0') ORDER BY id DESC;";
            string clock_in_db = data.SearchStringValuesFromDb(GetSetData.query);

            if (clock_in_db != "")
            {
                GetSetData.query = "SELECT id FROM pos_clock_out where (clock_in_id = '" + clock_in_db + "');";
                string is_shift_already_closed = data.SearchStringValuesFromDb(GetSetData.query);

                if (is_shift_already_closed == "")
                {
                    sure.Message_choose("Are you sure you want to clock out!");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
                        string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


                        if (singleAuthorityClosing == "No")
                        {
                            formAddClockOut.clock_id_id = clock_in_db;
                            formAddClockOut.user_id = user_id;
                            formAddClockOut.role_id = role_id;
                            formAddClockOut.saveEnable = false;

                            using (formAddClockOut add_customer = new formAddClockOut())
                            {
                                add_customer.ShowDialog();
                            }

                        }
                    }

                    Save_login_details_button();
                    button_controls.loginform();
                    login_form.isSwitchUser = false;
                    this.Dispose();
                }
                else
                {
                    sure.Message_choose("Are you sure you want to logout!");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        Save_login_details_button();
                        login_form.isSwitchUser = false;
                        button_controls.loginform();
                        this.Dispose();
                    }
                }
            }
            else
            {
                sure.Message_choose("Are you sure you want to logout!");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    Save_login_details_button();
                    login_form.isSwitchUser = false;
                    button_controls.loginform();
                    this.Dispose();
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button_controls.SettingsButton();
            this.Dispose();
        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            button_controls.productsButton();
            this.Dispose();
        }

        private void button_pos_Click(object sender, EventArgs e)
        {
            form_counter_sales.user_id = user_id;
            form_counter_sales.role_id = role_id;
            txtDate.Text = DateTime.Now.ToLongDateString();
        
            button_controls.CounterCashButton(role_id, user_id);

            if (TextData.isClockedIn)
            {
                //if (Screen.AllScreens.Length > 1)
                //{
                //    if (!IsFormOpen(typeof(fromSecondScreen)))
                //    {
                //        Thread thread = new Thread(() =>
                //        {
                //            fromSecondScreen secondaryForm = new fromSecondScreen();
                //            Screen secondaryScreen = Screen.AllScreens[1];
                //            secondaryForm.StartPosition = FormStartPosition.Manual;
                //            secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                //            secondaryForm.WindowState = FormWindowState.Maximized;
                //            //secondaryForm.TopMost = true;
                //            Application.Run(secondaryForm);
                //        });

                //        thread.SetApartmentState(ApartmentState.STA);
                //        thread.Start();
                //    }
                //}

                this.Dispose();
            }
        }

        //private bool IsFormOpen(Type formType)
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form.GetType() == formType)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private void notificationButton_Click(object sender, EventArgs e)
        {
            form_inventory_history.user_id = user_id;
            form_inventory_history.role_id = role_id;
            form_inventory_history stock = new form_inventory_history();
            stock.Show();
            this.Dispose();
        }

        private void suppliers_Click(object sender, EventArgs e)
        {
            //Supliers_details.role_id = role_id;
            //button_controls.suppliersButton();
            //this.Dispose();
        }

        private void loaderbutton_Click(object sender, EventArgs e)
        {
            product_details.role_id = role_id;
            product_details.user_id = user_id;
            product_details.providedValue = "";
            button_controls.productsButton();
            this.Dispose();
        }

        private void loaderReturnButton_Click(object sender, EventArgs e)
        {
            Purchase_details.role_id = role_id;
            Purchase_details.user_id = user_id;
            button_controls.purchaseButton();
            this.Dispose();
        }

        private void customerSalesBt_Click(object sender, EventArgs e)
        {
            button_controls.SettingsButton();
            this.Dispose();
        }

        private void customerReturnButton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings.user_id = user_id;
            button_controls.SettingsButton();
            this.Dispose();
        }

        private void recover_btn_Click(object sender, EventArgs e)
        {
            Supliers_details.role_id = role_id;
            Supliers_details.user_id = user_id;
            Supliers_details.count = 1;
            Supliers_details supplier = new Supliers_details();
            supplier.Show();

            this.Dispose();
        }

        private void customerDuesButton_Click(object sender, EventArgs e)
        {
           //formDemandList.role_id = role_id;
           // button_controls.DemandsListButton();
           // this.Dispose();
        }

        private void customerButton_Click(object sender, EventArgs e)
        {
            //Customer_details.role_id = role_id;
            //button_controls.CustomerButton();
            //this.Dispose();
        }

        private void customer_btn_Click(object sender, EventArgs e)
        {
            //formInvestorDetails.role_id = role_id;
            //button_controls.ShowInvesterDetails();
            //this.Dispose();
        }

        private void reg_company_btn_Click(object sender, EventArgs e)
        {
            //CustomerDues.role_id = role_id;
            //button_controls.customerDuesButton();
            //this.Dispose();
        }

        private void customer_returns_btn_Click(object sender, EventArgs e)
        {
            button_controls.Customer_Returns_reports_btn(role_id);
            this.Dispose();
        }

        private void btn_day_book_Click(object sender, EventArgs e)
        {
            button_controls.Day_book_btn();
            this.Dispose();
        }

        private void btn_stock_report_Click(object sender, EventArgs e)
        {
            button_controls.stock_report_btn();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             button_controls.Receivables_report_btn();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button_controls.Payables_report_btn();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button_controls.incoming_balance_report_btn();
            this.Dispose();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button_controls.profit_loss_report_btn();
            this.Dispose();
        }

        private void recoveries_Click(object sender, EventArgs e)
        {
            form_recoveries reprot = new form_recoveries();
            reprot.ShowDialog();
            //button_controls.Recoveries_report_btn();
            this.Dispose();
        }

        private void btn_db_backup_Click(object sender, EventArgs e)
        {
            using (form_backup_db add_customer = new form_backup_db())
            {
                form_backup_db.role_id = role_id;
                add_customer.ShowDialog();
                //button_controls.Database_backup_btn();
            }
        }

        private void btn_restore_backup_Click(object sender, EventArgs e)
        {
            using (form_restore_db add_customer = new form_restore_db())
            {
                form_restore_db.role_id = role_id;
                add_customer.ShowDialog();
                //button_controls.Database_Restore_btn();
            }
        }

        private void btn_notification_Click(object sender, EventArgs e)
        {
            Low_inventory.role_id = role_id;
            button_controls.Low_inventory_notificatoin_btn();
        }

        private void btn_company_Click(object sender, EventArgs e)
        {
            form_GranterDetails.role_id = role_id;
            button_controls.GurantorsButton();
            this.Dispose();
        }

        private void btn_notifications_Click(object sender, EventArgs e)
        {
            Low_inventory.role_id = role_id;
            button_controls.Low_inventory_notificatoin_btn();
        }

        private void Cus_ledger_button_Click(object sender, EventArgs e)
        {
            //button_controls.customer_ledger_button();
            Customer_legers_form reports = new Customer_legers_form();
            Customer_legers_form.role_id = role_id;
            reports.ShowDialog();
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
             //button_controls.customer_statement_button();
            form_customer_statement reports = new form_customer_statement();
            form_customer_statement.role_id = role_id;
            reports.ShowDialog();
            this.Dispose();
        }

        private void button_sales_report_Click(object sender, EventArgs e)
        {
            button_controls.customer_sales_button(role_id);
            this.Dispose(); 
        }

        private void btnInstallmentSales_Click(object sender, EventArgs e)
        {
            //form_installment_sales.role_id = role_id;
            //form_installment_sales reports = new form_installment_sales();
            //reports.Show();
            //this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_controls.company_ledger_button();
            this.Dispose(); 
        }

        private void btn_bank_Click(object sender, EventArgs e)
        {
            form_supplier_details.role_id = role_id;
            form_supplier_details.user_id = user_id;
            form_supplier_details.count = 1;
            form_supplier_details supplier = new form_supplier_details();
            supplier.Show();
            this.Dispose();
        }

        private void bank_btn_hover(object sender, EventArgs e)
        {
            //btn_bank.BackColor = button_controls.ButtonHoversBackColor();
            //btn_bank.ForeColor = button_controls.ButtonHoversForeColor();
        }

        private void bank_btn_leave(object sender, EventArgs e)
        {
            //btn_bank.BackColor = button_controls.ButtonHoversLeavesBackColor();
            //btn_bank.ForeColor = button_controls.ButtonHoversLeavesForeColor();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button_controls.company_statement_button();
            this.Dispose();
        }

        private void btnCapital_Click(object sender, EventArgs e)
        {
           formCapitalHistoryDetails.role_id = role_id;
            button_controls.ShowCapitalForm();
            this.Dispose();
        }

        private void btnGenerateInvoices_Click(object sender, EventArgs e)
        {
            //using (GenerateCustomerInvoices reports = new GenerateCustomerInvoices())
            //{
            //    GenerateCustomerInvoices.role_id = role_id;
            //    reports.ShowDialog();
            //}
            //button_controls.GenerateInvoicesReportButton();
        }

        private void btnInvestorPaybook_Click(object sender, EventArgs e)
        {
            formInvestorsPaybookDetails.role_id = role_id;
            button_controls.InvestorsPaymentDetailsButton();
            this.Dispose();
        }

        private void button_company_Click(object sender, EventArgs e)
        {
            form_supplier_details.role_id = role_id;
            button_controls.CompanyDetailsButton();
            this.Dispose();
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            using (formDailyBalance add_customer = new formDailyBalance())
            {
                add_customer.ShowDialog();
                 //button_controls.buttonDailyBalance();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlChequeNotification.Visible = false;
        }

        private void btnCheckNotification_Click(object sender, EventArgs e)
        {
            button_controls.chequeNotifications();
        }

        private void btnDefaultersNotification_Click(object sender, EventArgs e)
        {
            button_controls.DefaultersNotifications();
        }

        private void btnCheques_Click(object sender, EventArgs e)
        {
            button_controls.chequeNotifications();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (formLoginForConfigureLicense add_customer = new formLoginForConfigureLicense())
            {
                aboutProductKey.role_id = role_id;
                formLoginForConfigureLicense.count = 1;
                add_customer.ShowDialog();
                //button_controls.AboutSoftwareLicense();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            pnlDefaultersNotification.Visible = false;
        }

        private void btnNotificationLowInventory_Click(object sender, EventArgs e)
        {
            pnlLowInventory.Visible = false;
        }

        private void btnSalaries_Click(object sender, EventArgs e)
        {
            CustomerDues.role_id = role_id;
            CustomerDues dues = new CustomerDues();
            dues.Show();
            this.Dispose();
        }

        private void btnBankLoans_Click(object sender, EventArgs e)
        {
            formBanksLoansDetails.role_id = role_id;
            button_controls.bankLoanDetailsbutton();
            this.Dispose();
        }

        private void btnLoanPaybook_Click(object sender, EventArgs e)
        {
            formBankLoanPaybookDetails.role_id = role_id;
            button_controls.BankLoanPaybookbutton();
            this.Dispose();
        }

        private void btnSupplierPaybook_Click(object sender, EventArgs e)
        {
            formDemandList.user_id = user_id;
            formDemandList.role_id = role_id;
            formDemandList dues = new formDemandList();
            dues.Show();
            this.Dispose();
        }

        private void btnCharity_Click(object sender, EventArgs e)
        {
            //using (formCharityDetails charity = new formCharityDetails())
            //{
            //    formCharityDetails.role_id = role_id;
            //    charity.ShowDialog();
            //    //button_controls.buttonDailyBalance();
            //}
        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {
            //button_controls.customer_sales_button(role_id);

            form_loyal_cus_sales.user_id = user_id;
            form_loyal_cus_sales.role_id = role_id;
            form_loyal_cus_sales reports = new form_loyal_cus_sales();
            reports.Show();

            this.Dispose(); 
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {
            // button_controls.stock_report_btn();
            //this.Dispose();
        }

        private void guna2GroupBox5_Click(object sender, EventArgs e)
        {
            Expenses_info.ExpensesListReport.ExpensesList report = new Expenses_info.ExpensesListReport.ExpensesList();
            report.ShowDialog();
        }

        private void guna2GroupBox6_Click(object sender, EventArgs e)
        {
            Banking_info.Reports.bank_detail_reports list = new Banking_info.Reports.bank_detail_reports();
            list.ShowDialog();
        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {
            //Purchase_info.All_Purchases_List.PurchaseList report = new Purchase_info.All_Purchases_List.PurchaseList();
            //report.ShowDialog();
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {
            employeeCommission.employeeName = authorized_person;
            employeeCommission.user_id = user_id;
            employeeCommission.isSuperAdmin = 0;
            employeeCommission report = new employeeCommission();
            report.Show();
            this.Dispose();
        }

        private void btnLoanGiven_Click(object sender, EventArgs e)
        {
            using (formGivenLoanDetails loan = new formGivenLoanDetails())
            {
                formGivenLoanDetails.role_id = role_id;
                loan.ShowDialog();
                //button_controls.buttonDailyBalance();
            }
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            formClockInDetails charity = new formClockInDetails();

            formClockInDetails.user_id = user_id;
            formClockInDetails.role_id = role_id;
            charity.Show();

            this.Dispose();
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            formClockOutDetails charity = new formClockOutDetails();

            formClockOutDetails.user_id = user_id;
            formClockOutDetails.role_id = role_id;
            charity.Show();
            this.Dispose();
        }

        private void btnDeals_Click(object sender, EventArgs e)
        {
            promotion_details.user_id = user_id;
            promotion_details.role_id = role_id;
            promotion_details dues = new promotion_details();
            dues.Show();
            this.Dispose();
            
            //deal_details.user_id = user_id;
            //deal_details.role_id = role_id;
            //deal_details dues = new deal_details();
            //dues.Show();
            //this.Dispose();
        }

        private void btnCustomer(object sender, EventArgs e)
        {
            Customer_details.isDropDown = false;
            Customer_details.role_id = role_id;
            Customer_details.user_id = user_id;
            Customer_details.count = 1;
            Customer_details.selected_customer = "";
            Customer_details customer = new Customer_details();
            customer.Show();

            this.Dispose();
        }

        //private void endDay()
        //{
        //    try
        //    {
        //        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1' or status = '0') ORDER BY id DESC;";
        //        double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

        //        if (clock_in_id != 0)
        //        {
        //            txtDate.Text = DateTime.Now.ToLongDateString();

        //            string start_time = data.UserPermissions("start_time", "pos_clock_in", "id", clock_in_id.ToString());
        //            string start_date = data.UserPermissions("date", "pos_clock_in", "id", clock_in_id.ToString());

        //            DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
        //            DateTime endTime = DateTime.Today + DateTime.Parse(lbl_time.Text).TimeOfDay;

        //            // Calculate duration
        //            TimeSpan duration = endTime - startTime;


        //            GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + lbl_time.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + user_id.ToString() + "', '" + user_id.ToString() + "', '" + clock_in_id.ToString() + "', '0', '0', '0');";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);


        //            GetSetData.query = @"update pos_clock_in set status  = '1' where (id = '" + clock_in_id.ToString() + "');";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);


        //            GetSetData.query = @"select default_printer from pos_general_settings;";
        //            string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

        //            if (printer_name != "")
        //            {
        //                PrintDirectReceipt(printer_name, clock_in_id.ToString());
        //            }
        //            else
        //            {
        //                error.errorMessage("Printer not found!");
        //                error.ShowDialog();
        //            }

        //            done.DoneMessage("Day end successfully.");
        //            done.ShowDialog();
        //        }
        //        else
        //        {
        //            error.errorMessage("Current day is already ended!");
        //            error.ShowDialog();
        //        }

        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private bool storeEndDay()
        //{
        //    try
        //    {
        //        txtDate.Text = DateTime.Now.ToLongDateString();
        //        double openingCash = 0;
        //        double totalSales = 0;
        //        double totalSaleReturns = 0;
        //        double totalVoidOrders = 0;
        //        double expected_amount = 0;
        //        double cash_amount_received = 0;
        //        double balance = 0;
        //        double shortage_amount = 0;
        //        double no_sales = 0;
        //        double payout = 0;


        //        GetSetData.query = @"SELECT pos_clock_in.id as clock_in_id, pos_clock_in.date as start_date, pos_clock_in.start_time, pos_clock_out.date as end_date, pos_clock_out.end_time, pos_clock_out.opening_cash, 
        //                             pos_clock_out.total_sales, pos_clock_out.total_return_amount, pos_clock_out.total_void_orders, pos_clock_out.expected_amount, pos_clock_out.cash_amount_received,
        //                             pos_clock_out.balance, pos_clock_out.shortage_amount, pos_clock_out.no_sales, pos_clock_out.payout, pos_clock_in.status, pos_clock_in.to_user_id as user_id
        //                             FROM pos_clock_out inner join pos_clock_in on pos_clock_out.clock_in_id = pos_clock_in.id 
        //                             where (status = '-1' or status = '-2' or status = '0');";

        //        SqlConnection conn = new SqlConnection(webConfig.con_string);
        //        SqlCommand cmd;
        //        SqlDataReader reader;

        //        cmd = new SqlCommand(GetSetData.query, conn);

        //        conn.Open();
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            GetSetData.query = @"select singleAuthorityClosing from pos_general_settings;";
        //            string singleAuthorityClosing = data.SearchStringValuesFromDb(GetSetData.query);


        //            if (singleAuthorityClosing == "No")
        //            {
        //                if (reader["status"].ToString() == "-1" || reader["status"].ToString() == "-2" || reader["status"].ToString() == "0")
        //                {
        //                    error.errorMessage("Please make sure to clock-out all the employees.");
        //                    error.ShowDialog();

        //                    openingCash += double.Parse(reader["opening_cash"].ToString());
        //                    totalSales += double.Parse(reader["total_sales"].ToString());
        //                    totalSaleReturns += double.Parse(reader["total_return_amount"].ToString());
        //                    totalVoidOrders += double.Parse(reader["total_void_orders"].ToString());
        //                    expected_amount += double.Parse(reader["expected_amount"].ToString());
        //                    cash_amount_received += double.Parse(reader["cash_amount_received"].ToString());
        //                    balance += double.Parse(reader["balance"].ToString());
        //                    shortage_amount += double.Parse(reader["shortage_amount"].ToString());
        //                    no_sales += double.Parse(reader["no_sales"].ToString());
        //                    payout += double.Parse(reader["payout"].ToString());
        //                }
        //            }
        //            else
        //            {
        //                if (reader["status"].ToString() != "-1" || reader["status"].ToString() != "-2" || reader["status"].ToString() != "0")
        //                {
        //                    string start_date = reader["start_date"].ToString();
        //                    string start_time = reader["start_time"].ToString();


        //                    DateTime startTime = DateTime.Parse(start_date) + DateTime.Parse(start_time).TimeOfDay;
        //                    DateTime endTime = DateTime.Today + DateTime.Parse(lbl_time.Text).TimeOfDay;

        //                    // Calculate duration
        //                    TimeSpan duration = endTime - startTime;


        //                    GetSetData.query = @"insert into pos_clock_out values ('" + txtDate.Text + "', '" + lbl_time.Text + "', '" + duration.Duration().ToString() + "',  '0', '0', '0', '0', '0', '0', '0', 'nill', '" + reader["user_id"].ToString() + "', '" + reader["user_id"].ToString() + "', '" + reader["clock_in_id"].ToString() + "', '0', '0', '0');";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);
        //                }


        //                GetSetData.query = @"update pos_clock_in set status  = '1' where (id = '" + reader["clock_in_id"].ToString() + "');";
        //                data.insertUpdateCreateOrDelete(GetSetData.query);



        //                openingCash += double.Parse(reader["opening_cash"].ToString());
        //                totalSales += double.Parse(reader["total_sales"].ToString());
        //                totalSaleReturns += double.Parse(reader["total_return_amount"].ToString());
        //                totalVoidOrders += double.Parse(reader["total_void_orders"].ToString());
        //                expected_amount += double.Parse(reader["expected_amount"].ToString());
        //                cash_amount_received += double.Parse(reader["cash_amount_received"].ToString());
        //                balance += double.Parse(reader["balance"].ToString());
        //                shortage_amount += double.Parse(reader["shortage_amount"].ToString());
        //                no_sales += double.Parse(reader["no_sales"].ToString());
        //                payout += double.Parse(reader["payout"].ToString());
        //            }
        //            //*************************************************************

                  
        //        }

        //        reader.Close();

        //        //*************************************************************

        //        GetSetData.query = "select id from pos_store_day_end where (date = '" + txtDate.Text + "') and (user_id = '" + user_id.ToString() + "');";
        //        string is_already_exist = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (is_already_exist == "")
        //        {
        //            GetSetData.query = @"insert into pos_store_day_end values ('" + txtDate.Text + "', '" + openingCash.ToString() + "', '" + totalSales.ToString() + "', '" + totalSaleReturns.ToString() + "', '" + totalVoidOrders.ToString() + "', '" + expected_amount.ToString() + "', '" + cash_amount_received.ToString() + "', '" + balance.ToString() + "', '" + shortage_amount.ToString() + "', '" + no_sales.ToString() + "', '" + payout.ToString() + "', '" + user_id.ToString() + "');";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);

        //            //*************************************************************

        //            GetSetData.query = @"select default_printer from pos_general_settings;";
        //            string printer_name = data.SearchStringValuesFromDb(GetSetData.query);

        //            GetSetData.query = "SELECT TOP 1 id FROM pos_store_day_end where (user_id = '" + user_id.ToString() + "') ORDER BY id DESC;";
        //            string store_end_id = data.SearchStringValuesFromDb(GetSetData.query);

        //            if (printer_name != "")
        //            {
        //                PrintDirectReceipt(printer_name, store_end_id);
        //            }
        //            else
        //            {
        //                error.errorMessage("Printer not found!");
        //                error.ShowDialog();
        //            }

        //            done.DoneMessage("Store day ended successfully.");
        //            done.ShowDialog();

        //            return true;
        //        }
        //        else
        //        {
        //            error.errorMessage("Store day is already ended!");
        //            error.ShowDialog();
        //        }               
                

        //        return false;

        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //        return false;
        //    }
        //}

        private void btnEndDay_Click(object sender, EventArgs e)
        {
            //storeEndDay();

            formEndDayDetails.user_id = user_id;
            formEndDayDetails.role_id = role_id;
            formEndDayDetails _obj = new formEndDayDetails();
            _obj.ShowDialog();
            this.Dispose();

        }

        private void btnClockIn_Click_1(object sender, EventArgs e)
        {
            //if (clockIn())
            //{
            //    done.DoneMessage("Current User Clocked In Successfully.");
            //    done.ShowDialog();
            //}
        }

        private void btnMinMaxScreen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnXReport_Click(object sender, EventArgs e)
        {
            form_x_report reports = new form_x_report();
            reports.Show();
            this.Dispose();
        }

        private void Menus_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
