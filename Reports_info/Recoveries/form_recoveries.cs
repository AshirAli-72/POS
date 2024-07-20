using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using Login_info.controllers;
using RefereningMaterial;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Recoveries
{
    public partial class form_recoveries : Form
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

        public form_recoveries()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_name_text.Text);
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();


            customer_name_text.Text = null;
            customer_code_text.Text = null;

            customer_name_text.Items.Clear();
            customer_code_text.Items.Clear();

            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", customer_name_text);
            //GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", customer_code_text);

            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            customer_name_text.Visible = false;
            customer_code_text.Visible = false;


            pnl_date_wise.Visible = true;
            pnl_name_wise.Visible = false;
            pnl_over_all.Visible = false;

            this.pnl_date_wise.Dock = DockStyle.Fill;

            this.Viewer_date_wise.Clear();
            this.viewer_name_wise.Clear();
            this.viewer_over_all.Clear();
        }

        private void form_recoveries_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_name_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomeCodes();
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void btn_date_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
              
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                pnl_date_wise.Visible = true;

                this.pnl_date_wise.Dock = DockStyle.Fill;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;

                pnl_name_wise.Visible = false;
                pnl_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_name_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Customer Wise Report";
                reportType = 1;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                pnl_name_wise.Visible = true;

                this.pnl_name_wise.Dock = DockStyle.Fill;
                  

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;
                customer_name_text.Visible = true;
                customer_code_text.Visible = true;
                
                pnl_date_wise.Visible = false;
                pnl_over_all.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_over_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Customer Recovery Report";
                reportType = 2;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                pnl_over_all.Visible = true;

               
                this.pnl_over_all.Dock = DockStyle.Fill;
                

                FromDate.Visible = false;
                ToDate.Visible = false;
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = true;
                customer_name_text.Visible = true;
                customer_code_text.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_name_wise.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void reportParamenters(ReportViewer viewer)
        {
            try
            {
                viewer.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    viewer.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    viewer.LocalReport.SetParameters(logo);
                }
            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureDateWiseRecoveriesTableAdapter.Fill(this.recoveries_ds.ReportProcedureDateWiseRecoveries, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.recoveries_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                //**********************************************************
                Linear billNo = new Linear();

                foreach (recoveries_ds.ReportProcedureDateWiseRecoveriesRow row in this.recoveries_ds.ReportProcedureDateWiseRecoveries.Rows)
                {
                    billNo.Data = row.invoiceNo.ToString();

                    //double totalAmount = 0;
                    double totalPurchasePrice = 0;
                    double totalSalePrice = 0;
                    double totalWholeSale = 0;
                    TextData.balance = 0;

                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where (billNo = '" + billNo.Data.ToString() + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //**************************************************************

                    GetSetData.query = @"select count(installmentNo) from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "');";
                    string Total_installmentNos = data.SearchStringValuesFromDb(GetSetData.query);
                    //**************************************************************

                    for (int i = 1; i <= Convert.ToInt32(Total_installmentNos); i++)
                    {
                        GetSetData.query = @"select status from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                        string installment_status = data.SearchStringValuesFromDb(GetSetData.query);
                        //**************************************************************

                        if (installment_status == "Completed")
                        {
                            //GetSetData.query = @"select total_amount from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                            //string totalAmount_db = data.SearchStringValuesFromDb(GetSetData.query);

                            //if (totalAmount_db != "" && totalAmount_db != "NULL")
                            //{
                            //    totalAmount = double.Parse(totalAmount_db);
                            //}
                            ////**************************************************************

                            GetSetData.query = @"select sum(total_purchase) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalPurchasePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalPurchasePrice_db != "" && totalPurchasePrice_db != "NULL")
                            {
                                totalPurchasePrice = double.Parse(totalPurchasePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(total_wholeSale) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalWholeSale_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalWholeSale_db != "" && totalWholeSale_db != "NULL")
                            {
                                totalWholeSale = double.Parse(totalWholeSale_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(Total_price) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalSalePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalSalePrice_db != "" && totalSalePrice_db != "NULL")
                            {
                                totalSalePrice = double.Parse(totalSalePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            TextData.customer_name = "0";
                            GetSetData.query = @"select paid from pos_sales_accounts where (sales_acc_id = '" + GetSetData.Ids.ToString() + "') and (pos_sales_accounts.status = 'Installment');";
                            TextData.customer_name = data.SearchStringValuesFromDb(GetSetData.query);

                            if (TextData.customer_name != "" && TextData.customer_name != "NULL")
                            {
                                TextData.balance += double.Parse(TextData.customer_name);
                            }

                            row.purhcasePrice = totalPurchasePrice;
                            row.salePrice = totalSalePrice;
                            row.wholeSalePrice = totalWholeSale;
                            row.advanceAmount = TextData.balance;
                            //MessageBox.Show(TextData.balance.ToString());
                        }
                    }
                }
                

                //**********************************************************
                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);
                //**********************************************************

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayCustomerWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCustomerWiseRecoveriesTableAdapter.Fill(this.recoveries_ds.ReportProcedureCustomerWiseRecoveries, FromDate.Text, ToDate.Text, customer_name_text.Text, customer_code_text.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.recoveries_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);

                //**********************************************************
                Linear billNo = new Linear();

                foreach (recoveries_ds.ReportProcedureCustomerWiseRecoveriesRow row in this.recoveries_ds.ReportProcedureCustomerWiseRecoveries.Rows)
                {
                    billNo.Data = row.invoiceNo.ToString();

                    //double totalAmount = 0;
                    double totalPurchasePrice = 0;
                    double totalSalePrice = 0;
                    double totalWholeSale = 0;
                    TextData.balance = 0;

                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where (billNo = '" + billNo.Data.ToString() + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //**************************************************************

                    GetSetData.query = @"select count(installmentNo) from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "');";
                    string Total_installmentNos = data.SearchStringValuesFromDb(GetSetData.query);
                    //**************************************************************

                    for (int i = 1; i <= Convert.ToInt32(Total_installmentNos); i++)
                    {
                        GetSetData.query = @"select status from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                        string installment_status = data.SearchStringValuesFromDb(GetSetData.query);
                        //**************************************************************

                        if (installment_status == "Completed")
                        {
                            //GetSetData.query = @"select total_amount from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                            //string totalAmount_db = data.SearchStringValuesFromDb(GetSetData.query);

                            //if (totalAmount_db != "" && totalAmount_db != "NULL")
                            //{
                            //    totalAmount = double.Parse(totalAmount_db);
                            //}
                            ////**************************************************************

                            GetSetData.query = @"select sum(total_purchase) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalPurchasePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalPurchasePrice_db != "" && totalPurchasePrice_db != "NULL")
                            {
                                totalPurchasePrice = double.Parse(totalPurchasePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(total_wholeSale) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalWholeSale_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalWholeSale_db != "" && totalWholeSale_db != "NULL")
                            {
                                totalWholeSale = double.Parse(totalWholeSale_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(Total_price) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalSalePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalSalePrice_db != "" && totalSalePrice_db != "NULL")
                            {
                                totalSalePrice = double.Parse(totalSalePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            TextData.customer_name = "0";
                            GetSetData.query = @"select paid from pos_sales_accounts where (sales_acc_id = '" + GetSetData.Ids.ToString() + "') and (pos_sales_accounts.status = 'Installment');";
                            TextData.customer_name = data.SearchStringValuesFromDb(GetSetData.query);

                            if (TextData.customer_name != "" && TextData.customer_name != "NULL")
                            {
                                TextData.balance += double.Parse(TextData.customer_name);
                            }

                            row.purhcasePrice = totalPurchasePrice;
                            row.salePrice = totalSalePrice;
                            row.wholeSalePrice = totalWholeSale;
                            row.advanceAmount = TextData.balance;
                        }
                    }
                }
                //**********************************************************
                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);
                //**********************************************************

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayOverAllReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureOverAllRecoveriesTableAdapter.Fill(this.recoveries_ds.ReportProcedureOverAllRecoveries, customer_name_text.Text, customer_code_text.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.recoveries_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);

                //**********************************************************
                Linear billNo = new Linear();

                foreach (recoveries_ds.ReportProcedureOverAllRecoveriesRow row in this.recoveries_ds.ReportProcedureOverAllRecoveries.Rows)
                {
                    billNo.Data = row.invoiceNo.ToString();

                    //double totalAmount = 0;
                    double totalPurchasePrice = 0;
                    double totalSalePrice = 0;
                    double totalWholeSale = 0;
                    TextData.balance = 0;

                    GetSetData.query = @"select sales_acc_id from pos_sales_accounts where (billNo = '" + billNo.Data.ToString() + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    GetSetData.query = @"select installment_acc_id from pos_installment_accounts where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    //**************************************************************

                    GetSetData.query = @"select count(installmentNo) from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "');";
                    string Total_installmentNos = data.SearchStringValuesFromDb(GetSetData.query);
                    //**************************************************************

                    for (int i = 1; i <= Convert.ToInt32(Total_installmentNos); i++)
                    {
                        GetSetData.query = @"select status from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                        string installment_status = data.SearchStringValuesFromDb(GetSetData.query);
                        //**************************************************************

                        if (installment_status == "Completed")
                        {
                            //GetSetData.query = @"select total_amount from pos_installment_plan where (installment_acc_id = '" + GetSetData.fks.ToString() + "') and (installmentNo = '" + i.ToString() + "');";
                            //string totalAmount_db = data.SearchStringValuesFromDb(GetSetData.query);

                            //if (totalAmount_db != "" && totalAmount_db != "NULL")
                            //{
                            //    totalAmount = double.Parse(totalAmount_db);
                            //}
                            ////**************************************************************

                            GetSetData.query = @"select sum(total_purchase) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalPurchasePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalPurchasePrice_db != "" && totalPurchasePrice_db != "NULL")
                            {
                                totalPurchasePrice = double.Parse(totalPurchasePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(total_wholeSale) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalWholeSale_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalWholeSale_db != "" && totalWholeSale_db != "NULL")
                            {
                                totalWholeSale = double.Parse(totalWholeSale_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************

                            GetSetData.query = @"select sum(Total_price) from pos_sales_details where (sales_acc_id = '" + GetSetData.Ids.ToString() + "');";
                            string totalSalePrice_db = data.SearchStringValuesFromDb(GetSetData.query);

                            if (totalSalePrice_db != "" && totalSalePrice_db != "NULL")
                            {
                                totalSalePrice = double.Parse(totalSalePrice_db) / double.Parse(Total_installmentNos);
                            }
                            //**************************************************************
                            TextData.customer_name = "0";
                            GetSetData.query = @"select paid from pos_sales_accounts where (sales_acc_id = '" + GetSetData.Ids.ToString() + "') and (pos_sales_accounts.status = 'Installment');";
                            TextData.customer_name = data.SearchStringValuesFromDb(GetSetData.query);

                            if (TextData.customer_name != "" && TextData.customer_name != "NULL")
                            {
                                TextData.balance += double.Parse(TextData.customer_name);
                            }

                            row.purhcasePrice = totalPurchasePrice;
                            row.salePrice = totalSalePrice;
                            row.wholeSalePrice = totalWholeSale;
                            row.advanceAmount = TextData.balance;
                        }
                    }
                }
               
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

//        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
//        {
//            try
//            {
//                recoveries_ds recoveries_report = new recoveries_ds();
//                ReportDataSource recoveries_rds = null;
//                SqlDataAdapter recoveries_da = null;


//                // ************************************************************************************************************************************************
//                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_customers.full_name, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.telephone_no, 
//                                    pos_customers.address1, pos_customers.email, pos_customers.account_no, pos_customers.bank_name, pos_customers.image_path, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_recovery_details.date,
//                                    pos_recovery_details.time, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recoveries.amount, pos_recoveries.credits
//                                    FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id
//                                    INNER JOIN pos_employees ON pos_recovery_details.employee_id = pos_employees.employee_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id";


//                if (condition == "datewise")
//                {
//                    GetSetData.query += " WHERE (pos_recovery_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_recovery_details.date asc;";
//                }
//                else if (condition == "customerwise")
//                {
//                    GetSetData.query += " WHERE (pos_recovery_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and pos_customers.full_name = '" + customer_name_text.Text + "' and pos_customers.cus_code = '" + customer_code_text.Text + "' ORDER BY pos_recovery_details.date asc;";
//                }
//                else
//                {
//                    GetSetData.query += " WHERE pos_customers.full_name = '" + customer_name_text.Text + "' and pos_customers.cus_code = '" + customer_code_text.Text + "';";
//                }

//                // Script for Recoveries ********************************
//                recoveries_da = new SqlDataAdapter(GetSetData.query, conn);
//                recoveries_da.Fill(recoveries_report, recoveries_report.Tables["Recoveries"].TableName);

//                recoveries_rds = new Microsoft.Reporting.WinForms.ReportDataSource("recoveries", recoveries_report.Tables["Recoveries"]);
//                viewer.LocalReport.DataSources.Clear();
//                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
//                viewer.ZoomMode = ZoomMode.Percent;
//                viewer.ZoomPercent = 100;
//                viewer.LocalReport.DataSources.Add(recoveries_rds);
//                viewer.LocalReport.Refresh();

//                // Retrive Report Settings from db *******************************************************************************************
//                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
//                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
//                viewer.LocalReport.SetParameters(title);

//                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
//                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
//                viewer.LocalReport.SetParameters(address);

//                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
//                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
//                viewer.LocalReport.SetParameters(phone);

//                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
//                ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
//                viewer.LocalReport.SetParameters(note);

//                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
//                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
//                viewer.LocalReport.SetParameters(copyrights);
//                // *************************************************************************************

//                if (condition == "datewise" || condition == "customerwise")
//                {
//                    ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
//                    viewer.LocalReport.SetParameters(fromDate);

//                    ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
//                    viewer.LocalReport.SetParameters(toDate);
//                }

//                viewer.RefreshReport();
//            }
//            catch (Exception es)
//            {
//                error.errorMessage(es.Message);
//                error.ShowDialog();
//            }
//        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    DisplayDateWiseReportInReportViewer(this.Viewer_date_wise);
                }
                else if (reportType == 1)
                {
                    DisplayCustomerWiseReportInReportViewer(this.viewer_name_wise);
                }
                else if (reportType == 2)
                {
                    DisplayOverAllReportInReportViewer(this.viewer_over_all);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }

        private void customer_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (customer_name_text.Text.Length > 2)
                {
                    Customer_details.isDropDown = true;
                    Customer_details.selected_customer = customer_name_text.Text;
                    button_controls.CustomerDetailsbuttons();
                    customer_name_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                }
                else
                {
                    error.errorMessage("Please enter minimum 3 characters.");
                    error.ShowDialog();
                }
            }
        }
    }
}
