using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.NewPurchaseReprots;

namespace Purchase_info.NewPurchaseReprots
{
    public partial class new_purchase_report : Form
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

        public new_purchase_report()
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
            this.Close();
        }

        private void fun_new_purchase_report()
        {
            try
            {              
                purchase_ds report = new purchase_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_employees.full_name, pos_employees.emp_code, pos_products.prod_name, pos_products.barcode, pos_products.manufacture_date, pos_products.expiry_date, pos_products.prod_state, 
                                    pos_products.unit, pos_products.item_type, pos_products.size, pos_suppliers.full_name AS Expr1, pos_suppliers.code, pos_purchased_items.quantity, pos_purchased_items.pkg, pos_purchased_items.full_pak, 
                                    pos_purchased_items.pur_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, 
                                    pos_purchase.date, pos_purchase.bill_no, pos_purchase.invoice_no, pos_purchase.no_of_items, pos_purchase.net_trade_off, pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits,                                   
                                    pos_purchase.freight, pos_purchase.remarks AS Expr2, pos_supplier_payables.previous_payables, pos_supplier_payables.due_days, pos_stock_details.market_value, pos_purchase.pCredits, pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
                                    FROM pos_purchase INNER JOIN pos_employees ON pos_purchase.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN pos_category INNER JOIN
                                    pos_products ON pos_category.category_id = pos_products.category_id ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN
                                    pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where pos_purchase.bill_no = '" + TextData.send_billNo.ToString() + "' and pos_purchase.invoice_no = '" + TextData.send_invoiceNo.ToString() + "';";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("new_purchase", report.Tables["DataTable1"]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
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

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT title FROM pos_report_settings";
                TextData.full_name = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter title = new ReportParameter("pTitle", TextData.full_name);
                this.reportViewer1.LocalReport.SetParameters(title);

                GetSetData.query = @"SELECT address FROM pos_report_settings";
                TextData.address = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter address = new ReportParameter("pAddress", TextData.address);
                this.reportViewer1.LocalReport.SetParameters(address);

                GetSetData.query = @"SELECT phone_no FROM pos_report_settings";
                TextData.phone1 = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter phone = new ReportParameter("pPhone", TextData.phone1);
                this.reportViewer1.LocalReport.SetParameters(phone);

                GetSetData.query = @"SELECT copyrights FROM pos_report_settings";
                TextData.remarks = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter copyrights = new ReportParameter("pCopyrights", TextData.remarks);
                this.reportViewer1.LocalReport.SetParameters(copyrights);


                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void new_purchase_report_Load(object sender, EventArgs e)
        {
            try
            {
                fun_new_purchase_report();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
