using System;
using System.Data;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using System.Data.SqlClient;
using System.IO;
using RefereningMaterial;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class exportFileToDrive : Form
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

        public exportFileToDrive()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void exportFileToDrive_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToLongDateString();

            setValuesFromDb();
        }
        
        private string CreateFolderInOneDrive(string directoryName)
        {
            string DrivePath = "";

            GetSetData.Data = data.UserPermissions("drive", "pos_onedrive_options");

            if (GetSetData.Data == "Google Drive")
            {
                DrivePath = @"G:\My Drive\";
            }
            else
            {
                DrivePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\OneDrive\";
            }
   

            string tbfDataFolderPath = DrivePath + directoryName;

            if (!Directory.Exists(tbfDataFolderPath))
            {
                Directory.CreateDirectory(tbfDataFolderPath);
            }

            return tbfDataFolderPath;
        }

        public void exportDailyInventory(string query, string directoryName, string fileName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(webConfig.con_string))
                {
                    DataTable dataTable = new DataTable();

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Set command timeout to 60 seconds (adjust as needed)
                        command.CommandTimeout = 120;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    string inventoryFolderPath = CreateFolderInOneDrive(directoryName);
                    string filePath = Path.Combine(inventoryFolderPath, fileName);
                   
                    ExportToExcel(dataTable, filePath);

                    connection.Close();
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void ExportToExcel(DataTable dataTable, string filePath)
        {
            // Create Excel application
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // Create a new Excel workbook
            Excel.Workbook workbook = excelApp.Workbooks.Add();

            // Create a new Excel worksheet
            Excel.Worksheet worksheet = workbook.Sheets[1];
            worksheet.Name = "Sheet1";

            // Write headers to Excel worksheet
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
            }

            // Fill the worksheet with data from the DataTable
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                }
            }

            // Save the Excel file to the specified path
            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();
        }

        private void ExportThreadMethod(Action<string> callback)
        {
            try
            {
                if (chkDailyInventoryHistory.Checked)
                {
                    string query = @"SELECT pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_category.title as category,  pos_brand.brand_title as brand,
                                pos_stock_details.quantity, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as tax, pos_stock_details.qty_alert, 
                                pos_stock_details.date_of_expiry as expiry_date
                                FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id
                                INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id;";

                    string fileName = "Inventory_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";


                    exportDailyInventory(query, @"Inventory\", fileName);
                }


                if (chkDailySales.Checked)
                {
                    string query = @"SELECT pos_sales_accounts.date, pos_sales_accounts.billNo as [Receipt No], pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
                                    pos_sales_details.quantity, pos_sales_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_sales_details.discount as per_item_discount,
                                    pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount as total_discount, pos_sales_accounts.total_taxation, pos_sales_accounts.amount_due, pos_sales_accounts.paid as cash_amount,
                                    pos_sales_accounts.credit_card_amount, pos_sales_accounts.paypal_amount as apple_pay, pos_sales_accounts.google_pay_amount as zelle_pay,  
                                    pos_sales_accounts.check_sale_status as payment_type, pos_sales_accounts.status 
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id  INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_sales_accounts.date = '" + txtDate.Text + "');";

                    string fileName = "Sales_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

                    exportDailyInventory(query, @"Sales\", fileName);
                }


                if (chkDailyReturns.Checked)
                {
                    string query = @"SELECT pos_return_accounts.date, pos_return_accounts.billNo as receipt_no, pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
                                pos_returns_details.quantity, pos_returns_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_returns_details.discount as per_item_discount,
                                pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount as total_discount, pos_return_accounts.total_taxation, pos_return_accounts.amount_due, pos_return_accounts.paid as cash_amount,
                                pos_return_accounts.credit_card_amount, pos_return_accounts.paypal_amount as apple_pay, pos_return_accounts.google_pay_amount as zelle_pay,  
                                pos_return_accounts.check_sale_status as payment_type, pos_return_accounts.status 
                                FROM pos_return_accounts INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                pos_customers ON pos_return_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id  INNER JOIN
                                pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN
                                pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                where (pos_return_accounts.date = '" + txtDate.Text + "');";

                    string fileName = "Returns_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

                    exportDailyInventory(query, @"Returns\", fileName);
                }


                if (chkDailyReceivingItems.Checked)
                {
                    string query = @"SELECT pos_purchase.date, pos_purchase.bill_no as receipt_no, pos_purchase.invoice_no, pos_suppliers.full_name, pos_products.prod_name, pos_products.barcode, pos_purchased_items.quantity,
                                pos_purchased_items.pur_price as cost_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, 
                                pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, 
                                pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.freight,
                                pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
                                FROM pos_purchase INNER JOIN pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN 
                                pos_products ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id
                                where (pos_purchase.date = '" + txtDate.Text + "');";

                    string fileName = "Receivings_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

                    exportDailyInventory(query, @"Receivings\", fileName);
                }


                if (chkDailyExpenses.Checked)
                {
                    string query = @"SELECT pos_expense_details.date, pos_expense_details.time, pos_expenses.title, pos_expense_details.net_amount, pos_expense_items.amount, pos_expense_items.remarks
                                FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
                                where (pos_expense_details.date = '" + txtDate.Text + "');";

                    string fileName = "Expenses_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

                    exportDailyInventory(query, @"Expenses\", fileName);
                }

                // Invoke callback on UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    callback("Data exported successfully.");
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    callback("An error occurred: " + ex.Message);
                });
            }
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            form_loading loadingForm = new form_loading();
            loadingForm.SetLoadingMessage("Exporting data to cloud drive...");
            loadingForm.Show();


            Thread exportThread = new Thread(() => ExportThreadMethod((message) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    loadingForm.Close();

                    done.DoneMessage(message);
                    done.ShowDialog();
                });
            }));

            exportThread.Start();
        }

        private void setValuesFromDb()
        {
            try
            {
                int is_already_exist = data.UserPermissionsIds("id", "pos_onedrive_options");

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_onedrive_options values ('False', 'False', 'False', 'False', 'False', '1 Day', 'OneDrive' );";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                GetSetData.Data = data.UserPermissions("daily_sales", "pos_onedrive_options");
                chkDailySales.Checked = bool.Parse(GetSetData.Data);
                
                GetSetData.Data = data.UserPermissions("daily_returns", "pos_onedrive_options");
                chkDailyReturns.Checked = bool.Parse(GetSetData.Data);
                
                GetSetData.Data = data.UserPermissions("expenses", "pos_onedrive_options");
                chkDailyExpenses.Checked = bool.Parse(GetSetData.Data);
                
                GetSetData.Data = data.UserPermissions("inventory_history", "pos_onedrive_options");
                chkDailyInventoryHistory.Checked = bool.Parse(GetSetData.Data);
                
                GetSetData.Data = data.UserPermissions("daily_receivings", "pos_onedrive_options");
                chkDailyReceivingItems.Checked = bool.Parse(GetSetData.Data);


                txtTimeLimit.Text = data.UserPermissions("task_schedule", "pos_onedrive_options");

                txtDrive.Text = data.UserPermissions("drive", "pos_onedrive_options");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                int is_already_exist = data.UserPermissionsIds("id", "pos_onedrive_options");

                if (is_already_exist == 0)
                {
                    GetSetData.query = @"insert into pos_onedrive_options values ('" + chkDailySales.Checked.ToString() + "', '" + chkDailyReturns.Checked.ToString() + "', '" + chkDailyExpenses.Checked.ToString() + "', '" + chkDailyInventoryHistory.Checked.ToString() + "', '" + chkDailyReceivingItems.Checked.ToString() + "', '"+txtTimeLimit.Text+"', '"+txtDrive.Text+"' );";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    GetSetData.query = @"update pos_onedrive_options set daily_sales = '" + chkDailySales.Checked.ToString() + "', daily_returns = '" + chkDailyReturns.Checked.ToString() + "', expenses = '" + chkDailyExpenses.Checked.ToString() + "', inventory_history = '" + chkDailyInventoryHistory.Checked.ToString() + "', daily_receivings = '" + chkDailyReceivingItems.Checked.ToString() + "', task_schedule = '" + txtTimeLimit.Text+"', drive = '" + txtDrive.Text+"';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        #region

        //private string CreateFolderInOneDrive(string directoryName)
        //{
        //    string oneDrivePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\OneDrive\";
        //    string tbfDataFolderPath = oneDrivePath + directoryName;

        //    if (!Directory.Exists(tbfDataFolderPath))
        //    {
        //        Directory.CreateDirectory(tbfDataFolderPath);
        //    }

        //    return tbfDataFolderPath;
        //}


        //private void exportDailyInventory()
        //{
        //    string query = @"select * from ViewExportInventoryHistory where (date = '" + txtDate.Text +"');";

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(webConfig.con_string))
        //        {
        //            DataTable dataTable = new DataTable();

        //            connection.Open();

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                // Set command timeout to 60 seconds (adjust as needed)
        //                command.CommandTimeout = 60;

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    dataTable.Load(reader);
        //                }
        //            }

        //            //string filePath = data.UserPermissions("picture_path", "pos_general_settings");
        //            string filePath = @"C:\Users\AFAQ ALI\OneDrive\TBF_Data\YourFileName.xlsx";

        //            //filePath = filePath + "daily_inventory [" + txtDate.Text + " - " + DateTime.Now.ToLongTimeString() + "].xlsx"; 
        //            ExportToExcel(dataTable, filePath);
        //        }

        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        //private void btnExportData_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        form_loading loadingForm = new form_loading();
        //        loadingForm.SetLoadingMessage("Exporting data to OneDrive...");
        //        loadingForm.Show();

        //        if (chkDailyInventoryHistory.Checked)
        //        {
        //            string query = @"SELECT pos_products.prod_name, pos_stock_details.item_barcode as barcode, pos_category.title as category,  pos_brand.brand_title as brand,
        //                        pos_stock_details.quantity, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as tax, pos_stock_details.qty_alert, 
        //                        pos_stock_details.date_of_expiry as expiry_date
        //                        FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id
        //                        INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id;";

        //            string fileName = "Inventory_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";


        //            exportDailyInventory(query, @"Inventory\", fileName);
        //        }

        //        if (chkDailySales.Checked)
        //        {
        //            string query = @"SELECT pos_sales_accounts.date, pos_sales_accounts.billNo as [Receipt No], pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
        //                        pos_sales_details.quantity, pos_sales_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_sales_details.discount as per_item_discount,
        //                        pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount as total_discount, pos_sales_accounts.total_taxation, pos_sales_accounts.amount_due, pos_sales_accounts.paid as cash_amount,
        //                        pos_sales_accounts.credit_card_amount, pos_sales_accounts.paypal_amount as apple_pay, pos_sales_accounts.google_pay_amount as zelle_pay,  
        //                        pos_sales_accounts.check_sale_status as payment_type, pos_sales_accounts.status 
        //                        FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
        //                        pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id  INNER JOIN
        //                        pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
        //                        pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                        where (pos_sales_accounts.date = '" + txtDate.Text + "');";

        //            string fileName = "Sales_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            exportDailyInventory(query, @"Sales\", fileName);
        //        }


        //        if (chkDailyReturns.Checked)
        //        {
        //            string query = @"SELECT pos_return_accounts.date, pos_return_accounts.billNo as receipt_no, pos_employees.full_name as employee, pos_customers.full_name as customer, pos_products.prod_name, pos_products.barcode, 
        //                        pos_returns_details.quantity, pos_returns_details.Total_price, pos_stock_details.pur_price as cost_price, pos_stock_details.sale_price, pos_stock_details.market_value as per_item_tax, pos_returns_details.discount as per_item_discount,
        //                        pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount as total_discount, pos_return_accounts.total_taxation, pos_return_accounts.amount_due, pos_return_accounts.paid as cash_amount,
        //                        pos_return_accounts.credit_card_amount, pos_return_accounts.paypal_amount as apple_pay, pos_return_accounts.google_pay_amount as zelle_pay,  
        //                        pos_return_accounts.check_sale_status as payment_type, pos_return_accounts.status 
        //                        FROM pos_return_accounts INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN
        //                        pos_customers ON pos_return_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id  INNER JOIN
        //                        pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN
        //                        pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                        where (pos_return_accounts.date = '" + txtDate.Text + "');";

        //            string fileName = "Returns_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            exportDailyInventory(query, @"Returns\", fileName);
        //        }


        //        if (chkDailyReceivingItems.Checked)
        //        {
        //            string query = @"SELECT pos_purchase.date, pos_purchase.bill_no as receipt_no, pos_purchase.invoice_no, pos_suppliers.full_name, pos_products.prod_name, pos_products.barcode, pos_purchased_items.quantity,
        //                        pos_purchased_items.pur_price as cost_price, pos_purchased_items.sale_price, pos_purchased_items.trade_off, pos_purchased_items.carry_exp, 
        //                        pos_purchased_items.total_pur_price, pos_purchased_items.total_sale_price, pos_purchase.no_of_items, pos_purchase.total_quantity, pos_purchase.net_trade_off, 
        //                        pos_purchase.net_carry_exp, pos_purchase.net_total, pos_purchase.paid, pos_purchase.credits, pos_purchase.freight,
        //                        pos_purchased_items.new_purchase_price, pos_purchase.discount_percentage, pos_purchase.discount_amount, pos_purchase.fee_amount
        //                        FROM pos_purchase INNER JOIN pos_purchased_items ON pos_purchase.purchase_id = pos_purchased_items.purchase_id INNER JOIN 
        //                        pos_products ON pos_purchased_items.prod_id = pos_products.product_id INNER JOIN pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id
        //                        where (pos_purchase.date = '" + txtDate.Text + "');";

        //            string fileName = "Receivings_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            exportDailyInventory(query, @"Receivings\", fileName);
        //        }


        //        if (chkDailyExpenses.Checked)
        //        {
        //            string query = @"SELECT pos_expense_details.date, pos_expense_details.time, pos_expenses.title, pos_expense_details.net_amount, pos_expense_items.amount, pos_expense_items.remarks
        //                        FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id
        //                        where (pos_expense_details.date = '" + txtDate.Text + "');";

        //            string fileName = "Expenses_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";

        //            exportDailyInventory(query, @"Expenses\", fileName);
        //        }

        //        loadingForm.Close();

        //        done.DoneMessage("Data exported successfully.");
        //        done.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion
    }
}
