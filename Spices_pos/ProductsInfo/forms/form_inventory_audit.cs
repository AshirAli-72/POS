using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Threading;
using System.Diagnostics;
using Spices_pos.ProductsInfo.controllers;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class form_inventory_audit : Form
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

        public form_inventory_audit()
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
        public static int user_id = 0;
        int isCurrentInventory = 0;
        int searching = 0;
        //private string choose = "false";

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
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
                if (isCurrentInventory == 0)
                {
                    GetSetData.query = @"select pos_products.product_id as [PID], pos_stock_details.stock_id as [SID], prod_name as [Product Name], item_barcode as [Barcode], pos_category.title as [Category], pos_brand.brand_title as [Brand],
                                        pos_stock_details.quantity as [Stock], pos_stock_details.pur_price as [Cost Price], pos_stock_details.sale_price [Sale Price], pos_stock_details.market_value as [Tax %]
                                        from pos_products inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id inner join pos_category on pos_category.category_id = pos_products.category_id
                                        inner join pos_brand on pos_brand.brand_id = pos_products.brand_id";

                    if (searching == 1)
                    {
                      
                        GetSetData.query = GetSetData.query + " where ([prod_name] like '%" + search_box.Text + "%' or [item_barcode] like '%" + search_box.Text + "%' or [title] like '%" + search_box.Text + "%' or [brand_title] like '%" + search_box.Text + "%');";
                   
                        txtTotalQuantity.Text = "";
                        txtTotalCostPrice.Text = "";
                        txtTotalSalePrice.Text = "";
                    } 
                }
                else
                {
                    string query = "select count(date) from pos_stock_backup where (date = '" + txtDate.Text + "');";
                    double isRecordsExist = data.SearchNumericValuesDb(query);
                 
                    if (isRecordsExist > 0)
                    {
                        GetSetData.query = @"select pos_products.product_id as [PID], pos_stock_backup.stock_id as [SID], prod_name as [Product Name], item_barcode as [Barcode], pos_category.title as [Category], pos_brand.brand_title as [Brand],
                                            pos_stock_backup.quantity as [Stock], pos_stock_backup.pur_price as [Cost Price], pos_stock_backup.sale_price [Sale Price], pos_stock_backup.market_value as [Tax %]
                                            from pos_products inner join pos_stock_backup on pos_stock_backup.prod_id = pos_products.product_id inner join pos_category on pos_category.category_id = pos_products.category_id
                                            inner join pos_brand on pos_brand.brand_id = pos_products.brand_id
                                            where (pos_stock_backup.date = '" + txtDate.Text + "')";

                        if (searching == 1)
                        {
                            GetSetData.query = GetSetData.query + " and ([prod_name] like '%" + search_box.Text + "%' or [item_barcode] like '%" + search_box.Text + "%' or [title] like '%" + search_box.Text + "%' or [brand_title] like '%" + search_box.Text + "%');";

                            txtTotalQuantity.Text = "";
                            txtTotalCostPrice.Text = "";
                            txtTotalSalePrice.Text = "";
                        }
                    }
                    else
                    {
                        error.errorMessage("No Records found!");
                        error.ShowDialog();
                    }
                }

                if (searching == 1)
                {
                    GetSetData.FillDataGridViewWithoutPagination(ProductsDetailGridView, GetSetData.query);
                }
                else
                {
                    GetSetData.FillDataGridViewWithoutPaginationWithLoadingScreen(ProductsDetailGridView, GetSetData.query);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void LoadInventoryThreadMethod(Action<string> callback)
        {
            try
            {
                FillGridViewUsingPagination();

                this.Invoke(new Action(() =>
                {
                    callback?.Invoke("Inventory loaded");
                }));
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.Products_detail_buttons();
            this.Close();
        }

        private void clearGridView()
        {
            this.ProductsDetailGridView.DataSource = null;
            this.ProductsDetailGridView.Refresh();
            ProductsDetailGridView.Rows.Clear();
            ProductsDetailGridView.Columns.Clear();
        }

        private void form_inventory_audit_Load(object sender, EventArgs e)
        {
            try
            {
                isCurrentInventory = 0;
                searching = 0;

                GetSetData.addFormCopyrights(lblCopyrights);
                txtDate.Text = DateTime.Now.ToLongDateString();

              
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_inventory_audit_Shown(object sender, EventArgs e)
        {
            try
            {
                form_loading loadingForm = new form_loading();
                loadingForm.SetLoadingMessage("Loading Inventory...");
                loadingForm.Show();

                // Execute exportDataToDrive in a separate thread
                Thread inventoryThread = new Thread(() => LoadInventoryThreadMethod((message) =>
                {
                    // Invoke the loadingForm.Close on the main UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        loadingForm.Close();
                    });
                }));

                inventoryThread.Start();


                string queryQuantity = "select Round(sum(quantity), 2) from pos_stock_details;";
                txtTotalQuantity.Text = "Total Quantity: " + data.SearchStringValuesFromDb(queryQuantity);


                string queryCostPrice = "select Round(sum(quantity * pur_price), 2) as totalCostPrice from pos_stock_details;";
                txtTotalCostPrice.Text = "Total Cost Price: " + data.SearchStringValuesFromDb(queryCostPrice);


                string querySalePrice = "select Round(sum(quantity * sale_price), 2) from pos_stock_details;";
                txtTotalSalePrice.Text = "Total Sale Price: " + data.SearchStringValuesFromDb(querySalePrice);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            isCurrentInventory = 0;
            searching = 0;
            clearGridView();

            form_loading loadingForm = new form_loading();
            loadingForm.SetLoadingMessage("Loading Inventory...");
            loadingForm.Show();

            // Execute exportDataToDrive in a separate thread
            Thread inventoryThread = new Thread(() => LoadInventoryThreadMethod((message) =>
            {
                // Invoke the loadingForm.Close on the main UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    loadingForm.Close();
                });
            }));

            inventoryThread.Start();


            string queryQuantity = "select Round(sum(quantity), 2) from pos_stock_details;";
            txtTotalQuantity.Text = "Total Quantity: " + data.SearchStringValuesFromDb(queryQuantity);


            string queryCostPrice = "select Round(sum(quantity * pur_price), 2) as totalCostPrice from pos_stock_details;";
            txtTotalCostPrice.Text = "Total Cost Price: " + data.SearchStringValuesFromDb(queryCostPrice);


            string querySalePrice = "select Round(sum(quantity * sale_price), 2) from pos_stock_details;";
            txtTotalSalePrice.Text = "Total Sale Price: " + data.SearchStringValuesFromDb(querySalePrice);

        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            searching = 1;

            clearGridView();
            FillGridViewUsingPagination();
        }

        private void updateCurrentInventory()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    string category_id_db = data.UserPermissions("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());
                    //========================================================================================================
                    string brand_id_db = data.UserPermissions("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
                    //========================================================================================================
                    string product_id_db =  ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
                    //========================================================================================================
                    string stock_id_db = ProductsDetailGridView.Rows[i].Cells[1].Value.ToString();
                    //========================================================================================================

                    if (product_id_db != "")
                    {
                        GetSetData.query = @"update pos_products set prod_name = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , barcode = '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (product_id = '" + product_id_db +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                   

                    if (stock_id_db != "")
                    {
                        string old_quantity = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db);
                        string old_cost_price = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", stock_id_db);
                        string old_sale_price = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", stock_id_db);


                        double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
                        double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[7].Value.ToString());
                        double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[8].Value.ToString());
                        string taxation = ProductsDetailGridView.Rows[i].Cells[9].Value.ToString();
                        

                        // ******************************************************************************************


                        double total_pur_price = prod_price * quantity;
                        double total_sale_price = sale_price * quantity;


                        GetSetData.query = @"update pos_stock_details set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , quantity = '" + quantity.ToString() + "' , pur_price = '" + prod_price.ToString() + "' , sale_price = '" + sale_price.ToString() + "' , market_value =  '" + taxation + "' ,  total_pur_price = '" + total_pur_price.ToString() + "' , total_sale_price = '" + total_sale_price.ToString() + "'  where (stock_id = '" + stock_id_db +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        // ******************************************************

                        if ((quantity.ToString() != old_quantity) || (prod_price.ToString() != old_cost_price) || (sale_price.ToString() != old_sale_price))
                        {
                            GetSetData.query = @"insert into pos_stock_history values ('" + txtDate.Text + "','" + quantity.ToString() + "' , '" + old_quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + old_cost_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + old_sale_price.ToString() + "' , 'Modify from inventory audit' , '" + user_id.ToString() + "' , '" + product_id_db + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void updateInventoryBackup()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    string category_id_db = data.UserPermissions("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());
                    //========================================================================================================
                    string brand_id_db = data.UserPermissions("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
                    //========================================================================================================
                    string product_id_db =  ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
                    //========================================================================================================
                    string stock_id_db = ProductsDetailGridView.Rows[i].Cells[1].Value.ToString();
                    //========================================================================================================

                    if (product_id_db != "")
                    {
                        GetSetData.query = @"update pos_products set prod_name = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , barcode = '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (product_id = '" + product_id_db +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                   

                    if (stock_id_db != "")
                    {
                        string old_quantity = data.UserPermissions("quantity", "pos_stock_backup", "stock_id", stock_id_db);
                        string old_cost_price = data.UserPermissions("pur_price", "pos_stock_backup", "stock_id", stock_id_db);
                        string old_sale_price = data.UserPermissions("sale_price", "pos_stock_backup", "stock_id", stock_id_db);

                        double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
                        double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[7].Value.ToString());
                        double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[8].Value.ToString());
                        string taxation = ProductsDetailGridView.Rows[i].Cells[9].Value.ToString();
                        

                        // ******************************************************************************************


                        double total_pur_price = prod_price * quantity;
                        double total_sale_price = sale_price * quantity;


                        GetSetData.query = @"update pos_stock_backup set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , quantity = '" + quantity.ToString() + "' , pur_price = '" + prod_price.ToString() + "' , sale_price = '" + sale_price.ToString() + "' , market_value =  '" + taxation + "' ,  total_pur_price = '" + total_pur_price.ToString() + "' , total_sale_price = '" + total_sale_price.ToString() + "'  where (stock_id = '" + stock_id_db + "') and (date  = '" + txtDate.Text +"');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        // ******************************************************

                        GetSetData.query = @"update pos_stock_details set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (stock_id = '" + stock_id_db + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        // ******************************************************

                        if ((quantity.ToString() != old_quantity) || (prod_price.ToString() != old_cost_price) || (sale_price.ToString() != old_sale_price))
                        {
                            GetSetData.query = @"insert into pos_stock_history values ('" + txtDate.Text + "','" + quantity.ToString() + "' , '" + old_quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + old_cost_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + old_sale_price.ToString() + "' , 'Modify from inventory audit' , '" + user_id.ToString() + "' , '" + product_id_db + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            sure.Message_choose("Are you sure you want to apply these changes!");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                form_loading loadingForm = new form_loading();
                loadingForm.SetLoadingMessage("Applying Changes...");
                loadingForm.Show();

                // Execute exportDataToDrive in a separate thread
                Thread exportThread = new Thread(() => ExportThreadMethod((message) =>
                {
                    // Invoke the loadingForm.Close on the main UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        loadingForm.Close();

                        done.DoneMessage("Changes Applied Successfully.");
                        done.ShowDialog();
                    });
                }));

                exportThread.Start();
            }
        }

        private void ExportThreadMethod(Action<string> callback)
        {
            try
            {
                if (isCurrentInventory == 0)
                {
                    updateCurrentInventory();
                }
                else
                {
                    updateInventoryBackup();
                }

                this.Invoke(new Action(() =>
                {
                    //search_box.Text = "";  // Update UI element
                    callback?.Invoke("Changes Applied Successfully.");  // Optional callback for completion
                }));
                  
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            try
            {
                isCurrentInventory = 1;
                searching = 0;
                clearGridView();

                form_loading loadingForm = new form_loading();
                loadingForm.SetLoadingMessage("Loading Inventory...");
                loadingForm.Show();

                // Execute exportDataToDrive in a separate thread
                Thread inventoryThread = new Thread(() => LoadInventoryThreadMethod((message) =>
                {
                    // Invoke the loadingForm.Close on the main UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        loadingForm.Close();
                    });
                }));

                inventoryThread.Start();

                string queryQuantity = "select Round(sum(quantity), 2) from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                txtTotalQuantity.Text = "Total Quantity: " + data.SearchStringValuesFromDb(queryQuantity);


                string queryCostPrice = "select Round(sum(quantity * pur_price), 2) as totalCostPrice from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                txtTotalCostPrice.Text = "Total Cost Price: " + data.SearchStringValuesFromDb(queryCostPrice);


                string querySalePrice = "select Round(sum(quantity * sale_price), 2) from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                txtTotalSalePrice.Text = "Total Sale Price: " + data.SearchStringValuesFromDb(querySalePrice);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
