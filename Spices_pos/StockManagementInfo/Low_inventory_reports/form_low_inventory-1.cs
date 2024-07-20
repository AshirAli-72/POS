using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using WebConfig;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;

namespace Stock_management.Low_inventory_reports
{
    public partial class form_low_inventory : Form
    {
        public form_low_inventory()
        {
            InitializeComponent();
        }


        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public void Low_inventory()
        {
            try
            {
                stocks_ds report = new stocks_ds();
                string quer_get_data_db = @"SELECT Brand.brand_id, Brand.brand_title, Category.category_id, Category.title, Products.product_id, Products.prod_name, Products.prod_code, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, Products.prod_state,
                                            Products.pur_price, Products.sale_price,  Products.total_pur_price, Products.total_sale_price, Products.comments, Products.sub_cat_id, Products.brnd_id, Sub_category.sub_cate_id, Sub_category.title AS Expr1, Products.cat_id, inventoryLevel.minimum, 
                                            inventoryLevel.maximum, inventoryLevel.prod_id, inventoryLevel.inventory_id
                                            FROM  Brand INNER JOIN Products ON Brand.brand_id = Products.brnd_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN
                                            Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id INNER JOIN
                                            inventoryLevel ON Products.product_id = inventoryLevel.prod_id AND Products.quantity <= inventoryLevel.minimum";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", report.Tables[0]);
                this.viewer_low_inventory.LocalReport.DataSources.Clear();
                this.viewer_low_inventory.LocalReport.DataSources.Add(rds);
                this.viewer_low_inventory.LocalReport.Refresh();

                this.viewer_low_inventory.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void Expired_items()
        {
            try
            {
                //FromDate.Text = DateTime.Now.ToLongDateString();
                stocks_ds report = new stocks_ds();
                string quer_get_data_db = @"SELECT Products.prod_name, Products.prod_code, Products.manufacture_date, Products.expire_date, Products.quantity, Products.carton_qty, Products.unit, Products.prod_state, Products.pur_price, Products.total_pur_price, 
                                            Products.sale_price, Products.total_sale_price, Products.comments, Brand.brand_title, Category.title, Sub_category.title AS Expr1
                                            FROM Products INNER JOIN Brand ON Products.brnd_id = Brand.brand_id INNER JOIN Category ON Products.cat_id = Category.category_id INNER JOIN Sub_category ON Products.sub_cat_id = Sub_category.sub_cate_id 
                                            where Products.expire_date = '" + FromDate.Text + "';";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("low_inventory", report.Tables["DataTable1"]);
                this.viewer_expiry.LocalReport.DataSources.Clear();
                this.viewer_expiry.LocalReport.DataSources.Add(rds);
                this.viewer_expiry.LocalReport.Refresh();

                this.viewer_expiry.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void refresh()
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            btn_low_inventory.Checked = true;
            btn_expiry.Checked = false;

            viewer_low_inventory.Visible = true;
            viewer_expiry.Visible = false;

            this.viewer_low_inventory.Clear();
            //this.viewer_expiry.Clear();

            if (btn_low_inventory.Checked == true)
            {
                //this.pnl_date_wise.Dock = DockStyle.Fill;
                this.viewer_low_inventory.Dock = DockStyle.Fill;
                Low_inventory();
            }
            else if (btn_expiry.Checked == true)
            {
                //this.pnl_Bill_wise.Dock = DockStyle.Fill;
                this.viewer_expiry.Dock = DockStyle.Fill;
                Expired_items();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_low_inventory_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btn_low_inventory_CheckedChanged(object sender, EventArgs e)
        {
            viewer_low_inventory.Visible = true;

            if (btn_low_inventory.Checked == true)
            {
                //this.pnl_date_wise.Dock = DockStyle.Fill;
                this.viewer_low_inventory.Dock = DockStyle.Fill;
                //Low_inventory();
            }

            viewer_expiry.Visible = false;
        }

        private void btn_expiry_CheckedChanged(object sender, EventArgs e)
        {
            viewer_expiry.Visible = true;
            
            if (btn_expiry.Checked == true)
            {
                //this.pnl_date_wise.Dock = DockStyle.Fill;
                this.viewer_expiry.Dock = DockStyle.Fill;
                //Expired_items();
            }

            viewer_low_inventory.Visible = false;
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            if(btn_low_inventory.Checked == true && btn_expiry.Checked == false)
            {
                Low_inventory();
            }
            else if (btn_expiry.Checked == true && btn_low_inventory.Checked == false)
            {
                Expired_items();
            }
        }
    }
}
