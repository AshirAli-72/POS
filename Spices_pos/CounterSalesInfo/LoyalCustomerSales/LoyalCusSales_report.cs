using System;
using System.Windows.Forms;
using Login_info.controllers;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.CounterSalesInfo.LoyalCustomerSales;

namespace CounterSales_info.LoyalCustomerSales
{
    public partial class LoyalCusSales_report : Form
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

        public LoyalCusSales_report()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoyalCusSales_report_Load(object sender, EventArgs e)
        {
            try
            {
                loyalCusSalesReport_ds report = new loyalCusSalesReport_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_hold_items.billNo, pos_hold_items.date, pos_hold_items.no_of_items, pos_hold_items.total_qty, pos_hold_items.sub_total, pos_hold_items.discount, 
                                    pos_hold_items.tax, pos_hold_items.amount_due, pos_hold_items.paid, pos_hold_items.credits, pos_hold_items.pCredits, pos_hold_items.status, pos_hold_items.remarks, pos_hold_items_details.quantity, 
                                    pos_hold_items_details.pkg, pos_hold_items_details.full_pkg, pos_hold_items_details.Total_price, pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_hold_items_details.taxation, pos_hold_items.total_taxation
                                    FROM pos_customers INNER JOIN pos_hold_items ON pos_customers.customer_id = pos_hold_items.customer_id INNER JOIN
                                    pos_employees ON pos_hold_items.employee_id = pos_employees.employee_id INNER JOIN pos_hold_items_details ON pos_hold_items.sales_acc_id = pos_hold_items_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_hold_items_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.stock_id
                                    WHERE pos_hold_items.billNo = '" + TextData.billNo.ToString() + "';";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.reportViewer1.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.reportViewer1.LocalReport.SetParameters(logo);
                }

                //// Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(phone);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void LoyalCusSales_report_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Space)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
