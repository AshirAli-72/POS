using System;
using System.Data;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using System.IO;
using RefereningMaterial;
using ExcelDataReader;
using System.Globalization;
using Spices_pos.DatabaseInfo.WebConfig;
using System.Threading;

namespace Settings_info.forms
{
    public partial class formImportExcelFile : Form
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

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static int user_id = 0;

        public formImportExcelFile()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

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
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }
    

        private void formImportExcelFile_Load(object sender, EventArgs e)
        {
            //GetSetData.addFormCopyrights(lblCopyrights);
            txtDate.Text = DateTime.Now.ToLongDateString();
        }

        private void formImportExcelFile_Shown(object sender, EventArgs e)
        {
            try
            {
                clearGridView();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings reg = new settings();
            reg.Show();

            this.Dispose();
        }

        private void clearGridView()
        {
            try
            {
                int a = ProductsDetailGridView.Rows.Count;

                for (int i = 0; i < a; i++)
                {
                    foreach (DataGridViewRow row in ProductsDetailGridView.SelectedRows)
                    {

                        ProductsDetailGridView.Rows.Remove(row);
                    }
                }
                ProductsDetailGridView.DataSource = null;

                dt = null;
                tableCollection = null;

                txtType.Text = null;
                txtFileName.Text = "";
                txtSheet.Text = null;

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        DataTable dt = null;
        private void txtSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tableCollection != null)
                {
                    dt = tableCollection[txtSheet.SelectedItem.ToString()];
                    ProductsDetailGridView.DataSource = dt;
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        DataTableCollection tableCollection;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter="Excel 97-2003 Workbook|*.xls|Excel WorkBook|*.xlsx"})
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtFileName.Text = openFileDialog.FileName;

                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });

                                tableCollection = result.Tables;
                                txtSheet.Items.Clear();

                                foreach (DataTable table in tableCollection)
                                {
                                    txtSheet.Items.Add(table.TableName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            clearGridView();
            txtFileName.Text = "";
            txtSheet.Text = null;
        }

        private void importInventory()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    int is_category_exist = data.UserPermissionsIds("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[2].Value.ToString().ToUpper());

                    if (is_category_exist == 0)
                    {
                        GetSetData.query = @"insert into pos_category values ('" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }

                    int is_brand_exist = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[3].Value.ToString().ToUpper());

                    if (is_brand_exist == 0)
                    {
                        GetSetData.query = @"insert into pos_brand values ('" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }


                    int category_id_db = data.UserPermissionsIds("category_id", "pos_category", "title", ProductsDetailGridView.Rows[i].Cells[2].Value.ToString().ToUpper());
                    //========================================================================================================
                    int brand_id_db = data.UserPermissionsIds("brand_id", "pos_brand", "brand_title", ProductsDetailGridView.Rows[i].Cells[3].Value.ToString().ToUpper());
                    //========================================================================================================
                    int subcategory_id_db = data.UserPermissionsIds("sub_cate_id", "pos_subcategory", "title", "others");
                    //========================================================================================================
                    int color_id_db = data.UserPermissionsIds("color_id", "pos_color", "title", "none");
                    //========================================================================================================


                    // Insert Data From GridView to SaleItems in Database:  ** (prod_name, barcode, manufacture_date, expiry_date, prod_state, unit, item_type, size, image_path, status, remarks, category_id, brand_id, sub_cate_id, color_id) **
                    GetSetData.query = @"insert into pos_products values ('" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ProductsDetailGridView.Rows[i].Cells[0].Value.ToString()) + "' , '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + DateTime.Now.ToShortDateString() + "' , '" + DateTime.Now.ToShortDateString() + "' , 'pieces' , 'kg' , 'Inventory' , '0' , 'nill' , 'Enabled' , 'nill' , 'false' , '" + category_id_db.ToString() + "' , '" + brand_id_db.ToString() + "' , '" + subcategory_id_db.ToString() + "' , '" + color_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"select product_id from pos_products where (prod_name = '" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ProductsDetailGridView.Rows[i].Cells[0].Value.ToString()) + "') and (barcode = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "') and (brand_id = '" + brand_id_db.ToString() + "') and (category_id = '" + category_id_db.ToString() + "');";
                    int prod_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (prod_id_db != 0)
                    {
                        double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
                        double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
                        string taxation = ProductsDetailGridView.Rows[i].Cells[7].Value.ToString();
                        double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());

                        string expiry_date = "";

                        if (ProductsDetailGridView.Rows[i].Cells[8].Value.ToString() != "")
                        {
                            expiry_date = ProductsDetailGridView.Rows[i].Cells[8].Value.ToString();
                        }
                        else
                        {
                            expiry_date = DateTime.Now.ToShortDateString();
                        }


                        // ******************************************************************************************


                        double total_pur_price = prod_price * quantity;
                        double total_sale_price = sale_price * quantity;

                        GetSetData.Ids = data.UserPermissionsIds("stock_id", "pos_stock_details", "prod_id", prod_id_db.ToString());
                        if (GetSetData.Ids == 0)
                        {
                            // Insert Data From GridView to pos_stock in Database:
                            GetSetData.query = @"insert into pos_stock_details values ('" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "','" + quantity.ToString() + "' , '0' , '0' , '0' , '" + prod_price.ToString() + "' , '" + sale_price.ToString() + "' , '"+ ProductsDetailGridView.Rows[i].Cells[7].Value.ToString() + "' , '0' , '" + expiry_date + "' , '" + expiry_date + "' , '0' , '0' , '" + total_pur_price.ToString() + "' , '" + total_sale_price.ToString() + "' , '5' , 'false' , '0' , '0' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            GetSetData.query = @"insert into pos_stock_history values ('" + DateTime.Now.ToShortDateString() + "','" + quantity.ToString() + "' , '" + quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + prod_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + sale_price.ToString() + "' , 'Import Inventory From Excel File' , '" + user_id.ToString() + "' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }

                done.DoneMessage("Inventory Imported Successfully.");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }  
        
        private void importInventoryHistory()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    GetSetData.query = @"select product_id from pos_products where (barcode = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "');";
                    int prod_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (prod_id_db != 0)
                    {
                        double prod_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[5].Value.ToString());
                        double sale_price = double.Parse(ProductsDetailGridView.Rows[i].Cells[6].Value.ToString());
                        string taxation = ProductsDetailGridView.Rows[i].Cells[7].Value.ToString();
                        double quantity = double.Parse(ProductsDetailGridView.Rows[i].Cells[4].Value.ToString());

                        // ******************************************************************************************


                        double total_pur_price = prod_price * quantity;
                        double total_sale_price = sale_price * quantity;


                        GetSetData.query = @"select id from pos_stock_history where (date = '" + txtDate.Text + "') and (product_id = '" + prod_id_db.ToString() + "');";
                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (GetSetData.Ids == 0)
                        {
                            GetSetData.query = @"insert into pos_stock_history values ('" + txtDate.Text + "','" + quantity.ToString() + "' , '" + quantity.ToString() + "' , '" + prod_price.ToString() + "' , '" + prod_price.ToString() + "' , '" + sale_price.ToString() + "' , '" + sale_price.ToString() + "' , 'Import Inventory History' , '" + user_id.ToString() + "' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }

                        GetSetData.query = @"select stock_id from pos_stock_backup where (date = '" + txtDate.Text + "') and (prod_id = '" + prod_id_db.ToString() + "');";
                        string is_backup_already_exist = data.SearchStringValuesFromDb(GetSetData.query);

                        if (is_backup_already_exist == "")
                        {
                            GetSetData.query = @"insert into pos_stock_backup values ('" + txtDate.Text + "','" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + quantity.ToString() + "' , '0' , '0' , '0' , '" + prod_price.ToString() + "' , '" + sale_price.ToString() + "' , '"+ ProductsDetailGridView.Rows[i].Cells[7].Value.ToString() + "' , '0' , '" + txtDate.Text + "' , '" + txtDate.Text + "' , '0' , '0' , '" + (prod_price * quantity).ToString() + "' , '" + (sale_price * quantity).ToString() + "' , '5' , 'Active' , '0' , '0' , '" + prod_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                        //else
                        //{
                        //    string newStock = ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();

                        //    GetSetData.query = @"update pos_stock_backup set quantity = '" + newStock + "' where (date = '" + txtDate.Text + "') and (stock_id = '" + is_backup_already_exist + "')";
                        //    data.insertUpdateCreateOrDelete(GetSetData.query);
                        //}
                    }
                }

                done.DoneMessage("Inventory History Imported Successfully.");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }  
        
        private void importVendors()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    int country_id_db = data.UserPermissionsIds("country_id", "pos_country", "title", "nill");
                    //========================================================================================================
                    int city_id_db = data.UserPermissionsIds("city_id", "pos_city", "title", "nill");
                    //========================================================================================================
                

                    string email = "";
                    string address = "";
                    string date_of_entry = "";

                    if (ProductsDetailGridView.Rows[i].Cells[0].Value.ToString() != "")
                    {
                        date_of_entry = ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
                    }
                    else
                    {
                        date_of_entry = DateTime.Now.ToShortDateString();
                    }


                    if (ProductsDetailGridView.Rows[i].Cells[4].Value.ToString() != "")
                    {
                        address = ProductsDetailGridView.Rows[i].Cells[4].Value.ToString();
                    }
                    else
                    {
                        address = "nill";
                    }


                    if (ProductsDetailGridView.Rows[i].Cells[5].Value.ToString() != "")
                    {
                        email = ProductsDetailGridView.Rows[i].Cells[5].Value.ToString();
                    }
                    else
                    {
                        email = "nill";
                    }


                    GetSetData.query = @"insert into pos_suppliers values ('" + date_of_entry + "' , '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' , '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , 'nill' , '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , 'nill' , '" + address + "' , '" + email + "' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , 'Active' , '" + ProductsDetailGridView.Rows[i].Cells[6].Value.ToString() + "' , '" + country_id_db.ToString() + "' , '" + city_id_db.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"select supplier_id from pos_suppliers where (full_name = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "') and (code = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "');";
                    int supplier_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (supplier_id_db != 0)
                    {
                        GetSetData.Ids = data.UserPermissionsIds("supplier_payable_id", "pos_supplier_payables", "supplier_id", supplier_id_db.ToString());

                        if (GetSetData.Ids == 0)
                        {
                            GetSetData.query = @"insert into pos_supplier_payables values ('" + ProductsDetailGridView.Rows[i].Cells[7].Value.ToString() + "' , '" + date_of_entry + "' , '" + supplier_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }

                done.DoneMessage("Customers Imported Successfully.");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        private void importCustomers()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    int batch_id_db = data.UserPermissionsIds("batch_id", "pos_batchNo", "title", "nill");
                    //========================================================================================================
                    int country_id_db = data.UserPermissionsIds("country_id", "pos_country", "title", "nill");
                    //========================================================================================================
                    int city_id_db = data.UserPermissionsIds("city_id", "pos_city", "title", "nill");
                    //========================================================================================================

                    string email = "";
                    string address = "";
                    string date_of_entry = "";

                    if (ProductsDetailGridView.Rows[i].Cells[0].Value.ToString() != "")
                    {
                        date_of_entry = ProductsDetailGridView.Rows[i].Cells[0].Value.ToString();
                    }
                    else
                    {
                        date_of_entry = DateTime.Now.ToShortDateString();
                    }


                    if (ProductsDetailGridView.Rows[i].Cells[4].Value.ToString() != "")
                    {
                        address = ProductsDetailGridView.Rows[i].Cells[4].Value.ToString();
                    }
                    else
                    {
                        address = "nill";
                    }

                    
                    if (ProductsDetailGridView.Rows[i].Cells[5].Value.ToString() != "")
                    {
                        email = ProductsDetailGridView.Rows[i].Cells[5].Value.ToString();
                    }
                    else
                    {
                        email = "nill";
                    }


                    GetSetData.query = @"insert into pos_customers values ('" + date_of_entry + "' , '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' , 'nill' , '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '" + ProductsDetailGridView.Rows[i].Cells[3].Value.ToString() + "' , '" + address + "' , 'nill' , '" + email + "' , 'nill' , 'nill' , 'nill' , '0' , '0'  , '" + ProductsDetailGridView.Rows[i].Cells[6].Value.ToString() + "' , 'nill' , '" + ProductsDetailGridView.Rows[i].Cells[7].Value.ToString() + "' , 'Active' , '" + country_id_db.ToString() + "' , '" + city_id_db.ToString() + "' , '" + batch_id_db.ToString() + "', '0');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "') and (cus_code = '" + ProductsDetailGridView.Rows[i].Cells[2].Value.ToString() + "');";
                    int customer_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (customer_id_db != 0)
                    {
                        GetSetData.Ids = data.UserPermissionsIds("last_credit_id", "pos_customer_lastCredits", "customer_id", customer_id_db.ToString());

                        if (GetSetData.Ids == 0)
                        {
                            GetSetData.query = @"insert into pos_customer_lastCredits values ('" + ProductsDetailGridView.Rows[i].Cells[8].Value.ToString() + "' , '" + date_of_entry + "' , '" + customer_id_db.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                    }
                }

                done.DoneMessage("Customers Imported Successfully.");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void importAndResetInventorbyBarcode()
        {
            try
            {
                for (int i = 0; i < ProductsDetailGridView.Rows.Count; i++)
                {
                    GetSetData.query = @"update pos_products set barcode = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' where (barcode = '" + ProductsDetailGridView.Rows[i].Cells[0].Value.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"update pos_stock_details set item_barcode = '" + ProductsDetailGridView.Rows[i].Cells[1].Value.ToString() + "' where (item_barcode = '" + ProductsDetailGridView.Rows[i].Cells[0].Value.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                done.DoneMessage("Inventory Barcode Reset Successfully.");
                done.ShowDialog();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            form_loading loadingForm = new form_loading();
            loadingForm.SetLoadingMessage("Importing Excel File...");
            Screen secondaryScreen = Screen.PrimaryScreen;
            loadingForm.Location = secondaryScreen.WorkingArea.Location;
            loadingForm.TopMost = true;
            loadingForm.Show();

            Thread loading = new Thread(() => LoadThreadMethod((message) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    loadingForm.Dispose();
                });
            }));

            loading.Start();
        }

        private void LoadThreadMethod(Action<string> callback)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (txtType.Text == "Inventory")
                    {
                        importInventory();
                    }
                    else if (txtType.Text == "Inventory History")
                    {
                        importInventoryHistory();

                    }
                    else if (txtType.Text == "Customers")
                    {
                        importCustomers();
                    }
                    else if (txtType.Text == "Vendors")
                    {
                        importVendors();
                    }
                    else if (txtType.Text == "Reset Barcode")
                    {
                        importAndResetInventorbyBarcode();
                    }
                    else
                    {
                        error.errorMessage("Please choose type first!");
                        error.ShowDialog();
                    }
                });

                // Invoke callback on UI thread
                this.Invoke(new Action(() =>
                {
                    callback?.Invoke("Data imported successfully...");
                }));
            }
            catch (Exception ex)
            {
                // Invoke callback on UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    callback("An error occurred: " + ex.Message);
                });
            }
        }
    }
}
