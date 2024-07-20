using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Login_info.controllers;
using WebConfig;
using Datalayer;
using Message_box_info.forms;
using System.Data.SqlClient;
using Purchase_info.forms;
using Stock_management.Low_inventory_reports;

namespace Stock_management.forms
{
    public partial class Low_inventory : Form
    {
        public Low_inventory()
        {
            InitializeComponent();
        }

        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        public void system_user_permissions()
        {
            try
            {
                // ***************************************************************************************************
                string quer_get_expenses_print_db = "select whole_low_stock_print from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string whole_low_stock_print_db = data.Search_string_values_db(quer_get_expenses_print_db);

                pnl_print.Visible = bool.Parse(whole_low_stock_print_db);

                // ***************************************************************************************************
                string quer_get_expenses_new_db = "select whole_low_stock_refresh from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string whole_low_stock_refresh_db = data.Search_string_values_db(quer_get_expenses_new_db);

                pnl_refresh.Visible = bool.Parse(whole_low_stock_refresh_db);

                // ***************************************************************************************************
                string quer_get_settings_reports_db = "select whole_low_stock_exit from tbl_authorities_button_controls2 where role_id = '" + role_id.ToString() + "';";
                string whole_low_stock_exit_db = data.Search_string_values_db(quer_get_settings_reports_db);

                pnl_exit.Visible = bool.Parse(whole_low_stock_exit_db);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        // DataTables Object for creating Tables in the GridView:
        DataTable table = new DataTable();


        public void fill_data_grid_view()
        {
            try
            {
                ProductsDetailGridView.DataSource = null;

                int a = ProductsDetailGridView.Rows.Count;

                // Refresh Button Event is Generated:
                for (int i = 0; i < a; i++)
                {
                    foreach (DataGridViewRow row in ProductsDetailGridView.Rows)
                    {

                        ProductsDetailGridView.Rows.Clear();
                    }
                }
                string quer_to_get_data_from_Products_db = "select prod_name, prod_code, Category.title, Brand.brand_title, Sub_category.title, Products.quantity, Products.carton_qty, unit, prod_state, pur_price, sale_price, comments from Products inner join Category on Products.cat_id = Category.category_id inner join Sub_category on Products.sub_cat_id = Sub_category.sub_cate_id inner join Brand on Products.brnd_id = Brand.brand_id inner join inventoryLevel on Products.product_id = inventoryLevel.prod_id where Products.quantity <= inventoryLevel.minimum;";
                //string quer_to_get_data_from_Products_db = "select prod_name, prod_code, Category.title, Brand.brand_title, Sub_category.title, Products.quantity, Products.carton_qty, unit, prod_state, pur_price, sale_price, comments from Products, Category, Sub_category, Brand, inventoryLevel where Category.category_id = Products.cat_id and Sub_category.sub_cate_id = Products.sub_cat_id and Brand.brand_id = Products.brnd_id and Products.product_id = inventoryLevel.prod_id and Products.quantity <= inventoryLevel.minimum;";

                data.Connect();
                data.adptr_ = new SqlDataAdapter(quer_to_get_data_from_Products_db, data.conn_);
                data.dset_ = new System.Data.DataSet();
                data.adptr_.Fill(data.dset_, "Category, Sub_category, Brand, Products, inventoryLevel");
                ProductsDetailGridView.DataSource = data.dset_.Tables["Category, Sub_category, Brand, Products, inventoryLevel"];
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }


        // Method to Search a sepacific record from Teachers table DB.....
        public void method_for_searching()
        {
            try
            {
                ProductsDetailGridView.DataSource = null;

                int a = ProductsDetailGridView.Rows.Count;

                // Refresh Button Event is Generated:
                for (int i = 0; i < a; i++)
                {
                    foreach (DataGridViewRow row in ProductsDetailGridView.Rows)
                    {

                        ProductsDetailGridView.Rows.Clear();
                    }
                }

                string quer_to_get_data_from_Products_db = "select prod_name, prod_code, Category.title, Brand.brand_title, Sub_category.title, Products.quantity, Products.carton_qty, unit, prod_state, pur_price, sale_price, comments from Products inner join Category on Products.cat_id = Category.category_id inner join Sub_category on Products.sub_cat_id = Sub_category.sub_cate_id inner join Brand on Products.brnd_id = Brand.brand_id inner join inventoryLevel on Products.product_id = inventoryLevel.prod_id where prod_name like '" + search_box.Text + "%' or prod_code like '" + search_box.Text + "%' or Category.title like '" + search_box.Text + "%' or Sub_category.title like '" + search_box.Text + "%' or Brand.brand_title like '" + search_box.Text + "%' and Products.quantity <= inventoryLevel.minimum;";

                data.Connect();
                data.adptr_ = new SqlDataAdapter(quer_to_get_data_from_Products_db, data.conn_);
                data.dset_ = new System.Data.DataSet();
                data.adptr_.Fill(data.dset_, "Category, Sub_category, Brand, Products, inventoryLevel");
                ProductsDetailGridView.DataSource = data.dset_.Tables["Category, Sub_category, Brand, Products, inventoryLevel"];

                if (search_box.Text == "")
                {
                    fill_data_grid_view();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void Low_inventory_Load(object sender, EventArgs e)
        {
            fill_data_grid_view();
            search_box.Text = "";
            system_user_permissions();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            fill_data_grid_view();
            search_box.Text = "";
        }

        private void search_box_TextChanged_1(object sender, EventArgs e)
        {
            method_for_searching();
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            form_low_inventory report = new form_low_inventory();
            report.ShowDialog();
        }
    }
}
