using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class form_product_info : Form
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


        public form_product_info()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string selectedProductID = "";
        public static string selectedStockID = "";

        private void setFormColorsDynamically()
        {
            //try
            //{
            //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
            //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
            //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

            //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
            //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
            //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

            //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
            //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
            //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

            //    //****************************************************************

            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void FillGridViewUsingPagination()
        {
            try
            {
                GetSetData.query = @"SELECT pos_stock_history.date as [Date], pos_stock_history.old_quantity as [Old Stock], pos_stock_history.new_quantity as [New Stock],
									pos_stock_history.old_cost_price as [Old Cost Price], pos_stock_history.new_cost_price as [New Cost Price],
									pos_stock_history.old_sale_price as [Old Sale Price], pos_stock_history.new_sale_price as [New Sale Price],
									pos_stock_history.details as [Detail],  pos_employees.full_name as [Employee]
                                    from pos_stock_history inner join pos_products on pos_products.product_id = pos_stock_history.product_id 
                                    inner join pos_users on pos_stock_history.user_id = pos_users.user_id
                                    inner join pos_employees on pos_employees.employee_id = pos_users.emp_id
                                    where (pos_products.product_id = '" + selectedProductID +"') order by pos_stock_history.id desc";

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setProductInfoInFields()
        {
            try
            {
                lblProductName.Text = data.UserPermissions("prod_name", "pos_products", "product_id", selectedProductID);
                lblBarcode.Text = data.UserPermissions("item_barcode", "pos_stock_details", "stock_id", selectedStockID);

                string categoryId = data.UserPermissions("category_id", "pos_products", "product_id", selectedProductID);
                lblCategory.Text = data.UserPermissions("title", "pos_category", "category_id", categoryId);

                string brandId = data.UserPermissions("brand_id", "pos_products", "product_id", selectedProductID);
                lblBrand.Text = data.UserPermissions("brand_title", "pos_brand", "brand_id", brandId);

                //***********************************************************


                GetSetData.query = @"select sum(Total_price) from pos_sales_details inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                     where (pos_products.product_id = '" + selectedProductID + "');";
                string netSalesPrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (netSalesPrice == "NULL" || netSalesPrice == "")
                {
                    netSalesPrice = "0";
                }

                GetSetData.query = @"select sum(Total_price) from pos_returns_details inner join pos_products on pos_products.product_id = pos_returns_details.prod_id
                                     where (pos_products.product_id = '" + selectedProductID + "');";
                string netReturnsPrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (netReturnsPrice == "NULL" || netReturnsPrice == "")
                {
                    netReturnsPrice = "0";
                }  
                
                GetSetData.query = @"select sum(total_purchase) from pos_sales_details inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                     where (pos_products.product_id = '" + selectedProductID + "');";
                string netSalesCostPrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (netSalesCostPrice == "NULL" || netSalesCostPrice == "")
                {
                    netSalesCostPrice = "0";
                }

                GetSetData.query = @"select sum(total_purchase) from pos_returns_details inner join pos_products on pos_products.product_id = pos_returns_details.prod_id
                                     where (pos_products.product_id = '" + selectedProductID + "');";
                string netReturnsCostPrice = data.SearchStringValuesFromDb(GetSetData.query);

                if (netReturnsCostPrice == "NULL" || netReturnsCostPrice == "")
                {
                    netReturnsCostPrice = "0";
                }
         
                double NetSalesAmount = Math.Round(double.Parse(netSalesPrice) - double.Parse(netReturnsPrice), 2);
                double NetCostOfGood = Math.Round(double.Parse(netSalesCostPrice) - double.Parse(netReturnsCostPrice), 2);
                double netProfit = Math.Round(NetSalesAmount - NetCostOfGood, 2);
                double netProfitMargin = Math.Round((((NetSalesAmount - NetCostOfGood) / NetSalesAmount) * 100), 2);

                lblNetSales.Text = GetSetData.currency() + NetSalesAmount.ToString();
                lblCostOfGoods.Text = GetSetData.currency() + NetCostOfGood.ToString();
                lblProfit.Text = GetSetData.currency() + netProfit.ToString();
                lblProfitMargin.Text = netProfitMargin.ToString() + " %";
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            
        }

        private void form_product_info_Load(object sender, EventArgs e)
        {
            try
            {
                setProductInfoInFields();
                FillGridViewUsingPagination();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_product_info_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
