using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Microsoft.Reporting.WinForms;
using Spices_pos.DemandsInfo.PrintBill;

namespace Demands_info.PrintBill
{
    public partial class form_demand_receipt : Form
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

        public form_demand_receipt()
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
                demands_dataset report = new demands_dataset();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_demand_items.quantity, pos_demand_items.pkg, pos_demand_items.tab_pieces, pos_demand_items.full_pak, pos_demand_items.pur_price, pos_demand_items.sale_price, 
                                    pos_demand_items.total_pur_price, pos_demand_items.total_sale_price, pos_demand_list.date, pos_demand_list.bill_no, pos_demand_list.no_of_items, pos_demand_list.total_qty, pos_demand_list.net_amount, 
                                    pos_demand_list.paid, pos_demand_list.credits, pos_demand_list.discount, pos_demand_list.remarks, pos_employees.full_name, pos_employees.emp_code, pos_products.barcode, pos_products.prod_name, 
                                    pos_products.unit, pos_products.size, pos_suppliers.full_name AS Expr1, pos_suppliers.code
                                    FROM pos_category INNER JOIN pos_brand INNER JOIN pos_demand_list INNER JOIN pos_demand_items ON pos_demand_list.demand_id = pos_demand_items.demand_id INNER JOIN
                                    pos_employees ON pos_demand_list.employee_id = pos_employees.employee_id INNER JOIN pos_products ON pos_demand_items.prod_id = pos_products.product_id ON pos_brand.brand_id = pos_products.brand_id
                                    ON pos_category.category_id = pos_products.category_id INNER JOIN pos_suppliers ON pos_demand_list.supplier_id = pos_suppliers.supplier_id
                                    where pos_demand_list.bill_no = '" + TextData.send_billNo.ToString() + "';";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new ReportDataSource("productList", report.Tables["DataTable1"]);
                this.reportViewer2.LocalReport.DataSources.Clear();
                this.reportViewer2.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer2.ZoomMode = ZoomMode.Percent;
                this.reportViewer2.ZoomPercent = 100;
                this.reportViewer2.LocalReport.DataSources.Add(rds);
                this.reportViewer2.LocalReport.Refresh();

                this.reportViewer2.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.reportViewer2.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.reportViewer2.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT title FROM pos_report_settings";
                TextData.prod_name = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter title = new ReportParameter("pTitle", TextData.prod_name);
                this.reportViewer2.LocalReport.SetParameters(title);

                GetSetData.query = @"SELECT address FROM pos_report_settings";
                TextData.barcode = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter barcode = new ReportParameter("pAddress", TextData.barcode);
                this.reportViewer2.LocalReport.SetParameters(barcode);

                GetSetData.query = @"SELECT phone_no FROM pos_report_settings";
                TextData.category = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter phone = new ReportParameter("pPhone", TextData.category);
                this.reportViewer2.LocalReport.SetParameters(phone);

                GetSetData.query = @"SELECT copyrights FROM pos_report_settings";
                TextData.remarks = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter copyrights = new ReportParameter("pCopyrights", TextData.remarks);
                this.reportViewer2.LocalReport.SetParameters(copyrights);


                this.reportViewer2.RefreshReport();
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_demand_receipt_Load(object sender, EventArgs e)
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
