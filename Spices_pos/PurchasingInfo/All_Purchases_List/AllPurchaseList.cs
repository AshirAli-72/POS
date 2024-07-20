using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.All_Purchases_List;

namespace Purchase_info.All_Purchases_List
{
    public partial class AllPurchaseList : Form
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

        public AllPurchaseList()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            all_transactions_button.Checked = false;
            bill_wise_button.Checked = false;
            customer_bill_text.Visible = false;
            lbl_cus_bill.Visible = false;
            lbl_company.Visible = false;
            txt_company.Visible = false;
            lbl_from_date.Visible = true;
            lbl_to_date.Visible = true;
            FromDate.Visible = true;
            ToDate.Visible = true;

            customer_bill_text.Text = "PUR";


            pnl_date_wise.Visible = true;
            pnl_Bill_wise.Visible = false;
            pnl_all_transaction.Visible = false;

            this.pnl_date_wise.Dock = DockStyle.Fill;

            txt_company.Items.Clear();
            GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "full_name", txt_company);

            this.Viewer_dateWise.Clear();
            this.viewer_bill_wise.Clear();
            this.viewer_all_transaction.Clear();
            this.viewer_company_wise.Clear();
        }

        private void AllPurchaseList_Load(object sender, EventArgs e)
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

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                customer_bill_text.Text = "PUR";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_date_wise.Visible = true;

                this.pnl_date_wise.Dock = DockStyle.Fill;

                lbl_cus_bill.Visible = false;
                customer_bill_text.Visible = false;
                lbl_company.Visible = false;
                txt_company.Visible = false;

                pnl_Bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = true;
                pnl_company_wise.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void bill_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Bill Wise Report";
                reportType = 1;

                customer_bill_text.Text = "PUR";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_Bill_wise.Visible = true;

                this.pnl_Bill_wise.Dock = DockStyle.Fill;

                FromDate.Visible = false;
                ToDate.Visible = false;
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                lbl_company.Visible = false;
                txt_company.Visible = false;

                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = false;
                pnl_Bill_wise.Visible = true;
                pnl_company_wise.Visible = false;

                lbl_cus_bill.Visible = true;
                customer_bill_text.Visible = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void all_transactions_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Purchase Report";
                reportType = 3;

                customer_bill_text.Text = "PUR";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                pnl_all_transaction.Visible = true;

                this.pnl_all_transaction.Dock = DockStyle.Fill;

                lbl_cus_bill.Visible = false;
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                lbl_company.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
                customer_bill_text.Visible = false;
                txt_company.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_Bill_wise.Visible = false;
                pnl_company_wise.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_company_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Supplier Wise Report";
                reportType = 2;

                customer_bill_text.Text = "PUR";
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                lbl_company.Visible = true;
                txt_company.Visible = true;

                pnl_company_wise.Visible = true;

                this.pnl_company_wise.Dock = DockStyle.Fill;

                lbl_cus_bill.Visible = false;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
                customer_bill_text.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_Bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition, string value, string reportTitle)
        {
            try
            {
                all_purchase_list_ds report = new all_purchase_list_ds();

                GetSetData.query = @"SELECT pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, 
                                    pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, 
                                    pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.freight, pos_suppliers.full_name, pos_suppliers.code, pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
                                    FROM pos_purchase INNER JOIN pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN 
                                    pos_products ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id";

                if (condition == "DataWise" && value == "nill")
                {
                    GetSetData.query += " WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase.date asc;";
                }
                else if (condition != "nill" && value != "nill")
                {
                     GetSetData.query += " where " + condition + " = '" + value + "'";
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource(reportTitle, report.Tables["purchases"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);


                // **********************************************************************************************************
                all_purchase_list_ds report_setting = new all_purchase_list_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;

                if (condition == "DataWise" && value == "nill")
                {
                    GetSetData.query += " WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_purchase.date asc;";

                    ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                    viewer.LocalReport.SetParameters(fromDate);

                    ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                    viewer.LocalReport.SetParameters(toDate);
                }

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

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplaySupplierWiseReportInReportViewer(ReportViewer viewer, string reportTitle)
        {
            try
            {
                all_purchase_list_ds report = new all_purchase_list_ds();
                GetSetData.query = @"SELECT pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, pos_purchased_items.pur_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, 
                                    pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, 
                                    pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.freight, pos_suppliers.full_name, pos_suppliers.code, pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
                                    FROM pos_purchase INNER JOIN pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN 
                                    pos_products ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id
                                    WHERE (pos_purchase.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_suppliers.full_name = '"+txt_company.Text+"') order by pos_purchase.date";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["purchases"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource(reportTitle, report.Tables["purchases"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);


                // **********************************************************************************************************
                all_purchase_list_ds report_setting = new all_purchase_list_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;
                
                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

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

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_bill_wise()
        {
            try
            {
                all_purchase_list_ds report = new all_purchase_list_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_employees.full_name, pos_employees.emp_code, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_products.size, pos_suppliers.full_name AS Expr1, pos_suppliers.code, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, 
                                    pos_purchased_items.pur_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, 
                                    pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, pos_purchase.no_of_items, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, 
                                    pos_purchase.pCredits, pos_purchase.freight, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days, pos_stock_details.market_value, pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
                                    FROM pos_purchase INNER JOIN pos_employees ON pos_purchase.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN pos_category INNER JOIN
                                    pos_products ON pos_category.category_id = pos_products.category_id ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN
                                    pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_purchase.bill_no = '" + customer_bill_text.Text + "';";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("new_purchase", report.Tables["DataTable1"]);
                this.viewer_bill_wise.LocalReport.DataSources.Clear();
                this.viewer_bill_wise.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_bill_wise.ZoomMode = ZoomMode.Percent;
                this.viewer_bill_wise.ZoomPercent = 100;
                this.viewer_bill_wise.LocalReport.DataSources.Add(rds);


                // **********************************************************************************************************
                all_purchase_list_ds report_setting = new all_purchase_list_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                this.viewer_bill_wise.LocalReport.DataSources.Add(report_rds);

                this.viewer_bill_wise.LocalReport.Refresh();
                this.viewer_bill_wise.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.viewer_bill_wise.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.viewer_bill_wise.LocalReport.SetParameters(logo);
                }
                this.viewer_bill_wise.RefreshReport();

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    DisplayReportInReportViewer(this.Viewer_dateWise, "DataWise", "nill", "purchases");
                }
                else if (reportType == 1)
                {
                    fun_bill_wise();
                }
                else if (reportType == 2)
                {
                    DisplaySupplierWiseReportInReportViewer(this.viewer_company_wise, "purchases");
                }
                else if (reportType == 3)
                {
                    DisplayReportInReportViewer(this.viewer_all_transaction, "nill", "nill", "purchases");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

       
    }
}
