using System;
using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Threading;
using System.Diagnostics;
using CounterSales_info.CustomerSalesReport;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.controllers;

namespace Products_info.forms
{
    public partial class formInventoryAudit : Form
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

        public formInventoryAudit()
        {
            InitializeComponent();
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

        private void FillGridViewUsingPagination()
        {
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    //if (isCurrentInventory == 0)
                    //{
                    GetSetData.query = @"select Top 100 pos_products.product_id as [PID], pos_stock_details.stock_id as [SID], prod_name as [Product Name], item_barcode as [Barcode], pos_category.title as [Category], pos_brand.brand_title as [Brand],
                                        pos_stock_details.quantity as [Stock], pos_stock_details.pur_price as [Cost Price], pos_stock_details.sale_price [Sale Price], pos_stock_details.market_value as [Tax %]
                                        from pos_products inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id inner join pos_category on pos_category.category_id = pos_products.category_id
                                        inner join pos_brand on pos_brand.brand_id = pos_products.brand_id
                                        Where (pos_products.status = 'Enabled')";

                    if (searching == 1)
                    {

                        GetSetData.query = GetSetData.query + " and ([prod_name] like '%" + search_box.Text + "%' or [item_barcode] like '%" + search_box.Text + "%' or [title] like '%" + search_box.Text + "%' or [brand_title] like '%" + search_box.Text + "%');";

                        txtTotalQuantity.Text = "";
                        txtTotalCostPrice.Text = "";
                        txtTotalSalePrice.Text = "";
                    }
                    //}
                    //else
                    //{
                    //    string query = "select count(date) from pos_stock_backup where (date = '" + txtDate.Text + "');";
                    //    double isRecordsExist = data.SearchNumericValuesDb(query);

                    //    if (isRecordsExist > 0)
                    //    {
                    //        GetSetData.query = @"select pos_products.product_id as [PID], pos_stock_backup.stock_id as [SID], prod_name as [Product Name], item_barcode as [Barcode], pos_category.title as [Category], pos_brand.brand_title as [Brand],
                    //                        pos_stock_backup.quantity as [Stock], pos_stock_backup.pur_price as [Cost Price], pos_stock_backup.sale_price [Sale Price], pos_stock_backup.market_value as [Tax %]
                    //                        from pos_products inner join pos_stock_backup on pos_stock_backup.prod_id = pos_products.product_id inner join pos_category on pos_category.category_id = pos_products.category_id
                    //                        inner join pos_brand on pos_brand.brand_id = pos_products.brand_id
                    //                        where (pos_stock_backup.date = '" + txtDate.Text + "')";

                    //        if (searching == 1)
                    //        {
                    //            GetSetData.query = GetSetData.query + " and ([prod_name] like '%" + search_box.Text + "%' or [item_barcode] like '%" + search_box.Text + "%' or [title] like '%" + search_box.Text + "%' or [brand_title] like '%" + search_box.Text + "%');";

                    //            txtTotalQuantity.Text = "";
                    //            txtTotalCostPrice.Text = "";
                    //            txtTotalSalePrice.Text = "";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        error.errorMessage("No Records found!");
                    //        error.ShowDialog();
                    //    }
                    //}

                    clearGridView();

                    if (searching == 1)
                    {
                        GetSetData.FillDataGridViewWithoutPagination(ProductsDetailGridView, GetSetData.query);
                    }
                    else
                    {
                        GetSetData.FillDataGridViewWithoutPaginationWithLoadingScreen(ProductsDetailGridView, GetSetData.query);
                    }

                    //createComboBoxInGridView();
                    createArchiveButtonInGridView();
                    createSelectButtonInGridView();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            });
        }

        private void createSelectButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Save";
            btn.Text = "No Changes";
            btn.Width = 100;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            ProductsDetailGridView.Columns.Add(btn);
        }

        private void createArchiveButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Archive";
            btn.Text = "Archive";
            btn.Width = 100;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.Red;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            ProductsDetailGridView.Columns.Add(btn);
        }
        
        private void createRemoveButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "Remove";
            btn.Text = "Remove";
            btn.Width = 100;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.Red;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            afterAuditInventoryGridView.Columns.Add(btn);
        }

        private void createComboBoxInGridView()
        {
            // Create a new DataGridViewComboBoxColumn
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();

            // Set the header text for the column
            comboBoxColumn.HeaderText = "Reason";

            // Set the name of the column
            comboBoxColumn.Name = "Reason";

            // Add items to the combo box
            comboBoxColumn.Items.AddRange("Inventory Audit Add", "Inventory Audit Deduct", "Transfered Inventory", "No Changes");

            // Optionally set the width and other properties
            comboBoxColumn.Width = 210;
            comboBoxColumn.MinimumWidth = 10;
            comboBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            comboBoxColumn.FlatStyle = FlatStyle.Standard;

            // Set default cell styles (optional)
            comboBoxColumn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            comboBoxColumn.DefaultCellStyle.Font = new Font("Century Gothic", 12F, FontStyle.Bold);
            //comboBoxColumn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            //comboBoxColumn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            // Add the combo box column to the DataGridView
            ProductsDetailGridView.Columns.Add(comboBoxColumn);
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
            sure.Message_choose("Are you sure you want to close this session.");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                Button_controls.Products_detail_buttons();
                this.Close();
            }
        }

        private void clearGridView()
        {
            this.ProductsDetailGridView.DataSource = null;
            this.ProductsDetailGridView.Refresh();
            ProductsDetailGridView.Rows.Clear();
            ProductsDetailGridView.Columns.Clear();
        }

        private void formInventoryAudit_Load(object sender, EventArgs e)
        {
            try
            {
                isCurrentInventory = 0;
                searching = 0;

                //GetSetData.addFormCopyrights(lblCopyrights);
                txtDate.Text = DateTime.Now.ToLongDateString();


            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void formInventoryAudit_Shown(object sender, EventArgs e)
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

                fillGridViewWithAuditedItems();

                string queryQuantity = "select Round(sum(quantity), 2) from pos_stock_details;";
                txtTotalQuantity.Text = "Total Quantity: " + data.SearchStringValuesFromDb(queryQuantity);


                string queryCostPrice = "select Round(sum(quantity * pur_price), 2) as totalCostPrice from pos_stock_details;";
                txtTotalCostPrice.Text = "Total Cost Price: " + data.SearchStringValuesFromDb(queryCostPrice);


                string querySalePrice = "select Round(sum(quantity * sale_price), 2) from pos_stock_details;";
                txtTotalSalePrice.Text = "Total Sale Price: " + data.SearchStringValuesFromDb(querySalePrice);

                search_box.Select();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refreshScreen()
        {
            try
            {
                isCurrentInventory = 0;
                searching = 0;
                clearGridView();

                search_box.Text = "";

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

                search_box.Select();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refreshScreen();
            fillGridViewWithAuditedItems();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            searching = 1;

            clearGridView();
            FillGridViewUsingPagination();
        }

        private void updateCurrentInventory()
        {
            //try
            //{
            //    for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
            //    {
            //        string category_id_db = data.UserPermissions("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());
            //        //========================================================================================================
            //        string brand_id_db = data.UserPermissions("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
            //        //========================================================================================================
            //        string product_id_db = ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
            //        //========================================================================================================
            //        string stock_id_db = ProductsDetailGridView.Rows[i].Cells[1].Value.ToString();
            //        //========================================================================================================

            //        if (product_id_db != "")
            //        {
            //            GetSetData.query = @"update pos_products set prod_name = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , barcode = '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (product_id = '" + product_id_db + "');";
            //            data.insertUpdateCreateOrDelete(GetSetData.query);
            //        }


            //        if (stock_id_db != "")
            //        {
            //            string old_quantity = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stock_id_db);
            //            string old_cost_price = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", stock_id_db);
            //            string old_sale_price = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", stock_id_db);


            //            double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
            //            double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[7].Value.ToString());
            //            double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[8].Value.ToString());
            //            string taxation = ProductsDetailGridView.Rows[i].Cells[9].Value.ToString();


            //            // ******************************************************************************************


            //            double total_pur_price = prod_price * quantity;
            //            double total_sale_price = sale_price * quantity;


            //            GetSetData.query = @"update pos_stock_details set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , quantity = '" + quantity.ToString() + "' , pur_price = '" + prod_price.ToString() + "' , sale_price = '" + sale_price.ToString() + "' , market_value =  '" + taxation + "' ,  total_pur_price = '" + total_pur_price.ToString() + "' , total_sale_price = '" + total_sale_price.ToString() + "'  where (stock_id = '" + stock_id_db + "');";
            //            data.insertUpdateCreateOrDelete(GetSetData.query);

            //            // ******************************************************

            //            if ((quantity.ToString() != old_quantity) || (prod_price.ToString() != old_cost_price) || (sale_price.ToString() != old_sale_price))
            //            {
            //                GetSetData.query = @"insert into pos_stock_history values ('" + txtDate.Text + "','" + quantity.ToString() + "' , '" + old_quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + old_cost_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + old_sale_price.ToString() + "' , 'Modify from inventory audit' , '" + user_id.ToString() + "' , '" + product_id_db + "');";
            //                data.insertUpdateCreateOrDelete(GetSetData.query);
            //            }
            //        }
            //    }
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void updateInventoryBackup()
        {
            //try
            //{
            //    for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
            //    {
            //        string category_id_db = data.UserPermissions("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());
            //        //========================================================================================================
            //        string brand_id_db = data.UserPermissions("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
            //        //========================================================================================================
            //        string product_id_db = ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
            //        //========================================================================================================
            //        string stock_id_db = ProductsDetailGridView.Rows[i].Cells[1].Value.ToString();
            //        //========================================================================================================

            //        if (product_id_db != "")
            //        {
            //            GetSetData.query = @"update pos_products set prod_name = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , barcode = '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (product_id = '" + product_id_db + "');";
            //            data.insertUpdateCreateOrDelete(GetSetData.query);
            //        }


            //        if (stock_id_db != "")
            //        {
            //            string old_quantity = data.UserPermissions("quantity", "pos_stock_backup", "stock_id", stock_id_db);
            //            string old_cost_price = data.UserPermissions("pur_price", "pos_stock_backup", "stock_id", stock_id_db);
            //            string old_sale_price = data.UserPermissions("sale_price", "pos_stock_backup", "stock_id", stock_id_db);

            //            double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
            //            double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[7].Value.ToString());
            //            double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[8].Value.ToString());
            //            string taxation = ProductsDetailGridView.Rows[i].Cells[9].Value.ToString();


            //            // ******************************************************************************************


            //            double total_pur_price = prod_price * quantity;
            //            double total_sale_price = sale_price * quantity;


            //            GetSetData.query = @"update pos_stock_backup set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , quantity = '" + quantity.ToString() + "' , pur_price = '" + prod_price.ToString() + "' , sale_price = '" + sale_price.ToString() + "' , market_value =  '" + taxation + "' ,  total_pur_price = '" + total_pur_price.ToString() + "' , total_sale_price = '" + total_sale_price.ToString() + "'  where (stock_id = '" + stock_id_db + "') and (date  = '" + txtDate.Text + "');";
            //            data.insertUpdateCreateOrDelete(GetSetData.query);

            //            // ******************************************************

            //            GetSetData.query = @"update pos_stock_details set item_barcode =  '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "'  where (stock_id = '" + stock_id_db + "');";
            //            data.insertUpdateCreateOrDelete(GetSetData.query);

            //            // ******************************************************

            //            if ((quantity.ToString() != old_quantity) || (prod_price.ToString() != old_cost_price) || (sale_price.ToString() != old_sale_price))
            //            {
            //                GetSetData.query = @"insert into pos_stock_history values ('" + txtDate.Text + "','" + quantity.ToString() + "' , '" + old_quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + old_cost_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + old_sale_price.ToString() + "' , 'Modify from inventory audit' , '" + user_id.ToString() + "' , '" + product_id_db + "');";
            //                data.insertUpdateCreateOrDelete(GetSetData.query);
            //            }
            //        }
            //    }
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
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
                fillGridViewWithAuditedItems();

                //isCurrentInventory = 1;
                //searching = 0;
                //clearGridView();

                //form_loading loadingForm = new form_loading();
                //loadingForm.SetLoadingMessage("Loading Inventory...");
                //loadingForm.Show();

                //// Execute exportDataToDrive in a separate thread
                //Thread inventoryThread = new Thread(() => LoadInventoryThreadMethod((message) =>
                //{
                //    // Invoke the loadingForm.Close on the main UI thread
                //    this.Invoke((MethodInvoker)delegate
                //    {
                //        loadingForm.Close();
                //    });
                //}));

                //inventoryThread.Start();

                //string queryQuantity = "select Round(sum(quantity), 2) from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                //txtTotalQuantity.Text = "Total Quantity: " + data.SearchStringValuesFromDb(queryQuantity);


                //string queryCostPrice = "select Round(sum(quantity * pur_price), 2) as totalCostPrice from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                //txtTotalCostPrice.Text = "Total Cost Price: " + data.SearchStringValuesFromDb(queryCostPrice);


                //string querySalePrice = "select Round(sum(quantity * sale_price), 2) from pos_stock_backup where (pos_stock_backup.date = '" + txtDate.Text + "');";
                //txtTotalSalePrice.Text = "Total Sale Price: " + data.SearchStringValuesFromDb(querySalePrice);
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

      
        private void ProductsDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductsDetailGridView.Columns[e.ColumnIndex].Name == "Save")
            {
                try
                {
                    string productID = ProductsDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();
                    string stockID = ProductsDetailGridView.SelectedRows[0].Cells["SID"].Value.ToString();

                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stockID);
                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", stockID);
                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", stockID);
                    string oldTax = data.UserPermissions("market_value", "pos_stock_details", "stock_id", stockID);



                    GetSetData.query = "SELECT id FROM pos_inventory_audit where (date = '" + txtDate.Text + "') and (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    string is_already_exist = data.SearchStringValuesFromDb(GetSetData.query);

                    if (is_already_exist == "")
                    {
                        GetSetData.query = @"insert into pos_inventory_audit values  ('" + txtDate.Text + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Product Name"].Value.ToString() + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Barcode"].Value.ToString() + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', '" + oldQuantityDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', '" + oldCostPriceDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', '" + oldSalePriceDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "', '" + oldTax + "', 'No Changes', '" + productID + "', '" + stockID + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    else
                    {
                        GetSetData.query = @"update pos_inventory_audit set quantity = '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', pur_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', sale_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', tax = '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "', reason = 'No Changes' where (id = '" + is_already_exist + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }


                    GetSetData.query = @"update pos_stock_details set quantity = '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', pur_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', sale_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', market_value = '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "' where (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //******************************************************

                    //done.DoneMessage("Successfully Saved");
                    //done.ShowDialog();

                    refreshScreen();
                    fillGridViewWithAuditedItems();
                }
                catch (Exception es)
                {
                    error.errorMessage("Please fill all empty fields");
                    error.ShowDialog();
                }
            }
            else if (ProductsDetailGridView.Columns[e.ColumnIndex].Name == "Archive")
            {
                try
                {
                    string productID = ProductsDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();

                    GetSetData.query = @"update pos_products set status = 'Disabled' where (product_id = '" + productID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //done.DoneMessage("Successfully Archived");
                    //done.ShowDialog();

                    refreshScreen();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }

            search_box.Select();
        }

        private void ProductsDetailGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductsDetailGridView.IsCurrentCellInEditMode)
            {
                try
                {
                    string productID = ProductsDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();
                    string stockID = ProductsDetailGridView.SelectedRows[0].Cells["SID"].Value.ToString();


                    string oldQuantityDB = data.UserPermissions("quantity", "pos_stock_details", "stock_id", stockID);
                    string oldCostPriceDB = data.UserPermissions("pur_price", "pos_stock_details", "stock_id", stockID);
                    string oldSalePriceDB = data.UserPermissions("sale_price", "pos_stock_details", "stock_id", stockID);
                    string oldTax = data.UserPermissions("market_value", "pos_stock_details", "stock_id", stockID);

                    string reason = "";

                
                    if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) > double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) > double.Parse(oldCostPriceDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) > double.Parse(oldSalePriceDB))
                    {
                        reason = "Stock, Cost & Price Addition";
                    } 
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) < double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) < double.Parse(oldCostPriceDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) < double.Parse(oldSalePriceDB))
                    {
                        reason = "Stock, Cost & Price Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) > double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) > double.Parse(oldCostPriceDB))
                    {
                        reason = "Stock & Cost Addition";
                    } 
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) < double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) < double.Parse(oldCostPriceDB))
                    {
                        reason = "Stock & Cost Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) > double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) > double.Parse(oldSalePriceDB))
                    {
                        reason = "Stock & Price Addition";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) < double.Parse(oldQuantityDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) < double.Parse(oldSalePriceDB))
                    {
                        reason = "Stock & Price Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) > double.Parse(oldCostPriceDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) > double.Parse(oldSalePriceDB))
                    {
                        reason = "Cost & Price Addition";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) < double.Parse(oldCostPriceDB) && double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) < double.Parse(oldSalePriceDB))
                    {
                        reason = "Cost & Price Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) > double.Parse(oldQuantityDB))
                    {
                        reason = "Stock Addition";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) < double.Parse(oldQuantityDB))
                    {
                        reason = "Stock Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) > double.Parse(oldCostPriceDB))
                    {
                        reason = "Cost Addition";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) < double.Parse(oldCostPriceDB))
                    {
                        reason = "Cost Deduction";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) > double.Parse(oldSalePriceDB))
                    {
                        reason = "Price Addition";
                    }
                    else if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) < double.Parse(oldSalePriceDB))
                    {
                        reason = "Price Deduction";
                    }
                    else
                    {
                        reason = "No Changes";
                    }


                    //if (double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString()) > double.Parse(oldQuantityDB) || double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString()) > double.Parse(oldCostPriceDB) || double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString()) > double.Parse(oldSalePriceDB))
                    //{
                    //    reason = "Inventory Audit Add";
                    //}
                    //else
                    //{
                    //    reason = "Inventory Audit Deduct";
                    //}

                    //GetSetData.query = "SELECT id FROM pos_inventory_audit where (date = '" + txtDate.Text + "') and (prod_id = '" + productID + "') and (stock_id = '" + stockID + "');";
                    //string is_already_exist = data.SearchStringValuesFromDb(GetSetData.query);


                    //if (is_already_exist == "")
                    //{
                    GetSetData.query = @"insert into pos_inventory_audit values  ('" + txtDate.Text + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Product Name"].Value.ToString() + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Barcode"].Value.ToString() + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', '" + oldQuantityDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', '" + oldCostPriceDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', '" + oldSalePriceDB + "', '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "', '" + oldTax + "', '" + reason + "', '" + productID + "', '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //}
                    //else
                    //{
                    //    GetSetData.query = @"update pos_inventory_audit set quantity = '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', pur_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', sale_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', tax = '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "', reason = '" + reason + "' where (id = '" + is_already_exist + "');";
                    //    data.insertUpdateCreateOrDelete(GetSetData.query);
                    //}


                    GetSetData.query = @"update pos_stock_details set quantity = '" + ProductsDetailGridView.SelectedRows[0].Cells["Stock"].Value.ToString() + "', pur_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Cost Price"].Value.ToString() + "', sale_price = '" + ProductsDetailGridView.SelectedRows[0].Cells["Sale Price"].Value.ToString() + "', market_value = '" + ProductsDetailGridView.SelectedRows[0].Cells["Tax %"].Value.ToString() + "' where (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //******************************************************

                    //done.DoneMessage("Successfully Saved");
                    //done.ShowDialog();

                    refreshScreen();
                    fillGridViewWithAuditedItems();

                }
                catch (Exception es)
                {
                    error.errorMessage("Please fill all empty fields");
                    error.ShowDialog();
                }

                search_box.Select();
            }
        }

        private void clearAfterAuditGridView()
        {
            this.afterAuditInventoryGridView.DataSource = null;
            this.afterAuditInventoryGridView.Refresh();
            afterAuditInventoryGridView.Rows.Clear();
            afterAuditInventoryGridView.Columns.Clear();
        }

        private void fillGridViewWithAuditedItems()
        {
            clearAfterAuditGridView();

            GetSetData.query = @"select id as [ID], date as [Date], prod_name as [Product Name], barcode as [Barcode], quantity as [Stock], old_quantity as [Old Stock], 
                                pur_price as [Cost], old_cost_price as [Old Cost], sale_price as [Price], old_sale_price as [Old Price], tax as [Tax%], old_tax as [Old Tax%], 
                                reason as [Reason] from pos_inventory_audit 
                                where (date = '" + txtDate.Text +"') order by id desc";

            GetSetData.FillDataGridViewWithoutPagination(afterAuditInventoryGridView, GetSetData.query);
            createRemoveButtonInGridView();

            search_box.Select();
        }

        private void formInventoryAudit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();

            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                refreshScreen();

            }
        }

        private void afterAuditInventoryGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (afterAuditInventoryGridView.Columns[e.ColumnIndex].Name == "Remove")
            {
                try
                {
                    string auditID = afterAuditInventoryGridView.SelectedRows[0].Cells["ID"].Value.ToString();

                    string productID = data.UserPermissions("prod_id", "pos_inventory_audit", "id", auditID);
                    string stockID = data.UserPermissions("stock_id", "pos_inventory_audit", "id", auditID);
                    string oldQuantityDB = data.UserPermissions("old_quantity", "pos_inventory_audit", "id", auditID);
                    string oldCostPriceDB = data.UserPermissions("old_cost_price", "pos_inventory_audit", "id", auditID);
                    string oldSalePriceDB = data.UserPermissions("old_sale_price", "pos_inventory_audit", "id", auditID);
                    string oldTax = data.UserPermissions("old_tax", "pos_inventory_audit", "id", auditID);


                    GetSetData.query = @"update pos_stock_details set quantity = '" + oldQuantityDB + "', pur_price = '" + oldCostPriceDB + "', sale_price = '" + oldSalePriceDB + "', market_value = '" + oldTax + "' where (stock_id = '" + stockID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"delete from pos_inventory_audit where (id = '" + auditID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                   

                    //******************************************************

                    done.DoneMessage("Successfully Removed");
                    done.ShowDialog();

                    refreshScreen();
                    fillGridViewWithAuditedItems();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            formInventoryAuditReport _obj = new formInventoryAuditReport();
            _obj.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            add_new_product.isProductCreateFromPurchase = false;
            //GetSetData.SaveLogHistoryDetails("Products Details Form", "Add new product button click...", role_id);
            add_new_product.role_id = role_id;
            add_new_product.user_id = user_id;
            add_new_product.saveEnable = false;
            add_new_product add_customer = new add_new_product();
            add_customer.ShowDialog();
        }

        private void ProductsDetailGridView_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
