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
using Reports_info.Receivables;

namespace Reports_info.Receivables
{
    public partial class form_receivables : Form
    {
        public form_receivables()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();


    
        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        public void Loader_Customer_receivables()
        {
            try
            {
                receivables_ds loader_sales_report = new receivables_ds();
                ReportDataSource loader_sales_rds = null;
                SqlDataAdapter loader_sales_da = null;

               
                // ************************************************************************************************************************************************
                string quer_get_loader_Sales_db = @"SELECT Products.prod_name, Products.prod_code, Products.prod_state, loaderRelateCusAccounts.billNo AS Expr1, loaderRelateCusAccounts.dates AS Expr2, loaderRelateCusAccounts.cash, loaderRelateCusAccounts.discount, 
                                                    loaderRelateCusAccounts.credits, loaderRelateCusAccounts.net_total, loaderRelateCusAccounts.totalAmount, loaderRelateCusAccounts.suplier_id, loaderRelatedCusSales.carton_sales, loaderRelatedCusSales.carton_pieces, 
                                                    loaderRelatedCusSales.quantity, loaderRelatedCusSales.unit, loaderRelatedCusSales.trade_price, loaderRelatedCusSales.prod_state, loaderRelatedCusSales.prod_id, loaderRelatedCusSales.cus_id, 
                                                    loaderRelatedCusSales.supplr_id, loaderRelatedCusSales.loadr_cus_acc_id, Customers.name, Customers.cus_code, Suppliers.name AS Expr3, Suppliers.code, loaderRelateCusAccounts.cus_id AS Expr4, CustomerLastCredits.lastCredits
                                                    FROM loaderRelatedCusSales INNER JOIN loaderRelateCusAccounts ON loaderRelatedCusSales.loadr_cus_acc_id = loaderRelateCusAccounts.loader_cus_acc_id INNER JOIN
                                                    Products ON loaderRelatedCusSales.prod_id = Products.product_id INNER JOIN
                                                    Suppliers ON loaderRelatedCusSales.supplr_id = Suppliers.supplier_id AND loaderRelateCusAccounts.suplier_id = Suppliers.supplier_id INNER JOIN
                                                    Customers ON loaderRelatedCusSales.cus_id = Customers.customer_id AND loaderRelateCusAccounts.cus_id = Customers.customer_id inner join CustomerLastCredits on Customers.customer_id = CustomerLastCredits.cus_id
                                                    WHERE (loaderRelateCusAccounts.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY loaderRelateCusAccounts.dates asc;";


                // Script for Loader Sales ********************************
                loader_sales_da = new SqlDataAdapter(quer_get_loader_Sales_db, conn);
                loader_sales_da.Fill(loader_sales_report, loader_sales_report.Tables["Loader_Sales"].TableName);

                loader_sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Loader_Sales", loader_sales_report.Tables["Loader_Sales"]);
                this.viewer_receivables.LocalReport.DataSources.Clear();
                this.viewer_receivables.LocalReport.DataSources.Add(loader_sales_rds);


                // ************************************************************************************************************************************************
                receivables_ds loader_Cus_lastCredits_report = new receivables_ds();
                ReportDataSource loader_Cus_lastCredits_rds = null;
                SqlDataAdapter loader_Cus_lastCredits_da = null;

                string quer_get_loader_lastCredits_Sales_db = @"SELECT CustomerLastCredits.lastCredits AS Expr1
                                                                FROM CustomerLastCredits INNER JOIN Customers ON CustomerLastCredits.cus_id = Customers.customer_id INNER JOIN
                                                                loaderRelateCusAccounts ON Customers.customer_id = loaderRelateCusAccounts.cus_id
                                                                WHERE (loaderRelateCusAccounts.dates BETWEEN '"+FromDate.Text+"' AND '"+ToDate.Text+"');";


                // Script for Loader Sales ********************************
                loader_Cus_lastCredits_da = new SqlDataAdapter(quer_get_loader_lastCredits_Sales_db, conn);
                loader_Cus_lastCredits_da.Fill(loader_Cus_lastCredits_report, loader_Cus_lastCredits_report.Tables["loader_cus_lastCredits"].TableName);

                loader_Cus_lastCredits_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Loader_Cus_last_Credits", loader_Cus_lastCredits_report.Tables["loader_cus_lastCredits"]);
                this.viewer_receivables.LocalReport.DataSources.Add(loader_Cus_lastCredits_rds);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void Loyal_Customer_receivables()
        {
            try
            {
                receivables_ds Customer_sales_report = new receivables_ds();
                ReportDataSource Customer_Sales_rds = null;
                SqlDataAdapter Customer_Sales_da = null;

                // ************************************************************************************************************************************************
                string quer_get_Customer_Sales_db = @"SELECT Customers.name, Customers.cus_code, LoyalCustomerSales.dates AS Expr7, LoyalCustomerSales.cartons, LoyalCustomerSales.carton_pieces AS Expr8, LoyalCustomerSales.quantity AS Expr9, 
                                                      LoyalCustomerSales.Total_price, LoyalCusSaleAccounts.billNo AS Expr10, LoyalCusSaleAccounts.dates AS Expr11, LoyalCusSaleAccounts.cash AS Expr12, LoyalCusSaleAccounts.discount AS Expr13, 
                                                      LoyalCusSaleAccounts.credits AS Expr14, LoyalCusSaleAccounts.net_total AS Expr15, LoyalCusSaleAccounts.total_amount AS Expr16, Products.prod_name, Products.prod_code, Products.prod_state, Products.sale_price, CustomerLastCredits.lastCredits
                                                      FROM Customers INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id INNER JOIN
                                                      LoyalCustomerSales ON LoyalCusSaleAccounts.loyal_cus_sal_acc_id = LoyalCustomerSales.loyalCusAcc_id INNER JOIN
                                                      Products ON LoyalCustomerSales.prod_id = Products.product_id inner join CustomerLastCredits on Customers.customer_id = CustomerLastCredits.cus_id
                                                      WHERE (LoyalCustomerSales.dates BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY LoyalCustomerSales.dates asc;";


                // Script for Customer Sales ********************************
                Customer_Sales_da = new SqlDataAdapter(quer_get_Customer_Sales_db, conn);
                Customer_Sales_da.Fill(Customer_sales_report, Customer_sales_report.Tables["Customers_Sales"].TableName);

                Customer_Sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_sales", Customer_sales_report.Tables["Customers_Sales"]);
                this.viewer_receivables.LocalReport.DataSources.Add(Customer_Sales_rds);


                // ************************************************************************************************************************************************
                receivables_ds Customer_LastCredits_report = new receivables_ds();
                ReportDataSource Customer_LastCredits_rds = null;
                SqlDataAdapter Customer_LastCredits_da = null;

                string quer_get_Customer_Sum_lastCredits_db = @"SELECT CustomerLastCredits.lastCredits AS Expr1
                                                                FROM CustomerLastCredits INNER JOIN Customers ON CustomerLastCredits.cus_id = Customers.customer_id INNER JOIN LoyalCusSaleAccounts ON Customers.customer_id = LoyalCusSaleAccounts.cus_id
                                                                WHERE (LoyalCusSaleAccounts.dates BETWEEN '" + FromDate.Text + "' AND '"+ToDate.Text+"');";


                // Script for Customer Sales ********************************
                Customer_LastCredits_da = new SqlDataAdapter(quer_get_Customer_Sum_lastCredits_db, conn);
                Customer_LastCredits_da.Fill(Customer_LastCredits_report, Customer_LastCredits_report.Tables["loyal_Cus_lastCredits"].TableName);

                Customer_LastCredits_rds = new Microsoft.Reporting.WinForms.ReportDataSource("Loyal_cus_last_Credits", Customer_LastCredits_report.Tables["loyal_Cus_lastCredits"]);
                this.viewer_receivables.LocalReport.DataSources.Add(Customer_LastCredits_rds);


                this.viewer_receivables.LocalReport.Refresh();

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_receivables.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_receivables.LocalReport.SetParameters(toDate);

                this.viewer_receivables.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_receivables_Load(object sender, EventArgs e)
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            Loader_Customer_receivables();
            Loyal_Customer_receivables();
            this.viewer_receivables.RefreshReport();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                Loader_Customer_receivables();
                Loyal_Customer_receivables();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

    }
}
