using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stock_management.Low_inventory_reports;
using Microsoft.Reporting.WinForms;
using WebConfig;
using System.Data.SqlClient;

namespace Stock_management.whole_stock_reports
{
    public partial class form_whole_stock : Form
    {
        public form_whole_stock()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_whole_stock_Load(object sender, EventArgs e)
        {
            try
            {
                stocks_ds report = new stocks_ds();
                string quer_get_data_db = @"SELECT Products.product_id, Products.prod_name, Products.prod_code, Products.quantity, Products.carton_qty, Products.unit, Products.pur_price, Products.prod_state, Products.sale_price, Products.total_pur_price, Products.total_sale_price, Products.sub_cat_id, Products.brnd_id, 
                                            Products.cat_id, Products.comments, Category.category_id, Category.title, Brand.brand_id, Brand.brand_title, Sub_category.title AS Expr1, Sub_category.sub_cate_id
                                            FROM Brand INNER JOIN Products ON Brand.brand_id = Products.brnd_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("whole_inventory", report.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.LocalReport.Refresh();
                

                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
