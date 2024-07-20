using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Products_info.forms.RecipeDetails;
using Spices_pos.DashboardInfo.Forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.EmployeesInfo.employeeCommissionReport;

namespace CounterSales_info.CustomerSalesReport
{
    public partial class employeeCommission : Form
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

        public employeeCommission()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public static int isSuperAdmin = 0;
        public static string employeeName = "";
        public static int user_id = 0;

        private void Closebutton_Click(object sender, EventArgs e)
        {
            if (isSuperAdmin == 1)
            {
                commission_details.user_id = user_id;
                commission_details _obj = new commission_details();
                _obj.Show();
                this.Dispose();
            }
            else
            {
                Menus.user_id = user_id;
                Menus _obj = new Menus();
                _obj.Show();
                this.Dispose();
            }
           
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                if (isSuperAdmin == 0)
                {
                    txtEmployee.Text = employeeName;
                }

                employeeCommissionDS report = new employeeCommissionDS();

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
                                    pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount, pos_sales_accounts.employeeCommission, pos_sales_details.per_item_commission
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txtEmployee.Text + "') and (pos_sales_details.per_item_commission != 0) ORDER BY pos_sales_accounts.date asc;";


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("loyalCusSales", report.Tables["DataTable1"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();

                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.EnableExternalImages = true;

                //Return Items List****************************************************************

                employeeCommissionDS sales_return_report = new employeeCommissionDS();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg,
                                    pos_returns_details.Total_price, pos_return_accounts.employeeCommission, pos_returns_details.per_item_commission
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
                                    INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_employees.full_name = '" + txtEmployee.Text + "') and (pos_returns_details.per_item_commission != 0);";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new Microsoft.Reporting.WinForms.ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                viewer.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************

            
                // Retrive Report Settings from db *******************************************************************************************

                GetSetData.query = @"SELECT sum(employeeCommission) FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_employees.full_name = '" + txtEmployee.Text +"');";

                string total_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_amount_sale == "" || total_amount_sale == "NULL")
                {
                    total_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(employeeCommission) FROM pos_return_accounts INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_employees.full_name = '" + txtEmployee.Text +"');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

            

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


                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                viewer.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                viewer.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                viewer.LocalReport.SetParameters(phone);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                viewer.LocalReport.SetParameters(copyrights);


                ReportParameter pNetSaleCommission = new ReportParameter("pNetSalesCommission", total_amount_sale);
                viewer.LocalReport.SetParameters(pNetSaleCommission);
                
                ReportParameter pNetReturnCommission = new ReportParameter("pNetReturnCommission", return_amount_sale);
                viewer.LocalReport.SetParameters(pNetReturnCommission);

                viewer.RefreshReport();
            }
            catch (Exception es)
             {
                MessageBox.Show(es.Message);
            }
        }

        private void employeeCommission_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                if (isSuperAdmin == 1)
                {
                    lblEmployeeName.Visible = true;
                    txtEmployee.Visible = true;
                    txtEmployee.Text = null;
                    txtEmployee.Items.Clear();
                    GetSetData.FillComboBoxWithValues("select full_name from pos_employees;", "full_name", txtEmployee);
                }
                else
                {
                    lblEmployeeName.Visible = false;
                    txtEmployee.Visible = false;
                    txtEmployee.Text = employeeName;
                }
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }


        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayReportInReportViewer(this.reportViewer1);
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void employeeCommission_KeyDown(object sender, KeyEventArgs e)
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

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
