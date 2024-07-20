using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebConfig;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using Login_info.controllers;
using Reports_info.Payables;

namespace Reports_info.Payables
{
    public partial class form_payables : Form
    {
        public form_payables()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public void Purchases()
        {
            try
            {
                payables_ds purcahses_report = new payables_ds();
                ReportDataSource purchases_rds = null;
                SqlDataAdapter purchases_da = null;

                // ************************************************************************************************************************************************
                string quer_get_purchases_db = @"SELECT Products.prod_name, Products.prod_code, Products.quantity, Products.carton_qty, Products.unit, Products.prod_state, Products.pur_price, Products.sale_price, Purchase.billNo, Purchase.invoice_no, Purchase.receiveDate, 
                                                 Purchase.billDate, Purchase.paid_amount, Purchase.credits, Purchase.billAmount, Purchase.freight, Purchase.trade_offer, Sub_category.title, Category.title AS Expr1, Brand.brand_title, Products.comments, tbl_purchase_from.title AS Expr2
                                                 FROM Purchase_items INNER JOIN Products ON Purchase_items.prod_id = Products.product_id INNER JOIN Purchase ON Purchase_items.pur_id = Purchase.purchase_id INNER JOIN
                                                 Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id INNER JOIN
                                                 tbl_purchase_from ON Purchase.pur_from_id = tbl_purchase_from.pur_from_id
                                                 WHERE (Purchase.billDate BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY Purchase.billDate asc;";


                // Script for Purchases ********************************
                purchases_da = new SqlDataAdapter(quer_get_purchases_db, conn);
                purchases_da.Fill(purcahses_report, purcahses_report.Tables["Purchases_tbl"].TableName);

                purchases_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", purcahses_report.Tables["Purchases_tbl"]);
                this.viewer_payables.LocalReport.DataSources.Clear();
                this.viewer_payables.LocalReport.DataSources.Add(purchases_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Company_payments()
        {
            try
            {
                payables_ds company_payments_report = new payables_ds();
                ReportDataSource company_payments_rds = null;
                SqlDataAdapter company_payments_da = null;

                // ************************************************************************************************************************************************
                string quer_get_company_payments_db = @"SELECT Company_reg.company_title, Company_reg.invester_name, Company_reg.invester_code, Company_paybook.payables, Corporate_business.times, Corporate_business.payble_date, Corporate_business.dates, 
                                                        Corporate_business.reference, Corporate_business.description, Corporate_business.cash, Corporate_business.debits
                                                        FROM Company_paybook INNER JOIN Company_reg ON Company_paybook.compny_reg_id = Company_reg.company_reg_id INNER JOIN
                                                        Corporate_business ON Company_reg.company_reg_id = Corporate_business.compny_reg_id
                                                        WHERE (Corporate_business.dates BETWEEN '" +FromDate.Text+ "' AND '" + ToDate.Text + "') ORDER BY Corporate_business.dates asc;";


                // Script for Recoveries ********************************
                company_payments_da = new SqlDataAdapter(quer_get_company_payments_db, conn);
                company_payments_da.Fill(company_payments_report, company_payments_report.Tables["Company_Payments"].TableName);
                company_payments_rds = new Microsoft.Reporting.WinForms.ReportDataSource("company_payment", company_payments_report.Tables["Company_Payments"]);
                this.viewer_payables.LocalReport.DataSources.Add(company_payments_rds);

                this.viewer_payables.LocalReport.Refresh();

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_payables.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_payables.LocalReport.SetParameters(toDate);

                this.viewer_payables.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void form_payables_Load(object sender, EventArgs e)
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            Purchases();
            Company_payments();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                Purchases();
                Company_payments();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
