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
    public partial class exportWeblinkExcelFiles : Form
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

        public exportWeblinkExcelFiles()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private string CreateFolderInOneDrive(string directoryName)
        {
            string tbfDataFolderPath = txt_path.Text + directoryName;

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

        private void ExportThreadMethod(int selectedIndex, Action<string> callback)
        {
            try
            {
                string query = "";
                string fileName = "";
                string directoryName = "";

                // Determine the query, file name, and directory based on the selected index
                if (selectedIndex == 0)
                {
                    query = @"select pos_products.product_id, prod_name, item_barcode, title as category, brand_title as brand, quantity, pur_price, sale_price, market_value as tax 
                      from pos_products inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
                      inner join pos_category on pos_category.category_id = pos_products.category_id
                      inner join pos_brand on pos_brand.brand_id = pos_products.brand_id;";

                    fileName = "Inventory_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";
                    directoryName = @"Inventory\";
                }
                else if (selectedIndex == 1)
                {
                    query = @"select customer_id, date, full_name, cus_code, mobile_no, address1, email, opening_balance, points, opening_balance as last_balance 
                      from pos_customers;";

                    fileName = "Customers_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".xlsx";
                    directoryName = @"Customers\";
                }

                if (!string.IsNullOrEmpty(query) && !string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(directoryName))
                {
                    exportDailyInventory(query, directoryName, fileName);
                }

                // Invoke callback on UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    callback("Data exported successfully.");
                });
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

        private void btnExportData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_path.Text))
            {
                MessageBox.Show("Please specify a path.");
                return;
            }

            // Capture necessary values from UI controls on the UI thread
            int selectedIndex = cbOptions.SelectedIndex;

            // Show loading form
            form_loading loadingForm = new form_loading();
            loadingForm.SetLoadingMessage("Exporting Excel File to Location...");
            loadingForm.Show();

            // Start background thread
            Thread exportThread = new Thread(() =>
            {
                ExportThreadMethod(selectedIndex, (message) =>
                {
                    // Close loading form and show done message on the UI thread
                    this.Invoke((MethodInvoker)delegate
                    {
                        loadingForm.Close();
                        done.DoneMessage(message);
                        done.ShowDialog();
                    });
                });
            });

            exportThread.Start();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dig = new FolderBrowserDialog();

            if (dig.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = dig.SelectedPath;
            }
        }
    }
}
