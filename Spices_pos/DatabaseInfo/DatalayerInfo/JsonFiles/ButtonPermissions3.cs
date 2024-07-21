using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class ButtonPermissions3
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public ButtonPermissions3(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "button_permissions3.json"); // Updated file name
        }

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_tbl_authorities_button_controls3"; // Updated query

            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();

                var reader = command.ExecuteReader();
                var result = new JArray();

                while (reader.Read())
                {
                    var item = new JObject
                    {
                        ["button_control_id"] = reader["button_control_id"] != DBNull.Value ? (int)reader["button_control_id"] : default(int),
                        ["investor_details_print"] = reader["investor_details_print"] != DBNull.Value ? reader["investor_details_print"].ToString() : string.Empty,
                        ["investor_details_delete"] = reader["investor_details_delete"] != DBNull.Value ? reader["investor_details_delete"].ToString() : string.Empty,
                        ["investor_details_new"] = reader["investor_details_new"] != DBNull.Value ? reader["investor_details_new"].ToString() : string.Empty,
                        ["investor_details_modify"] = reader["investor_details_modify"] != DBNull.Value ? reader["investor_details_modify"].ToString() : string.Empty,
                        ["new_investor_save"] = reader["new_investor_save"] != DBNull.Value ? reader["new_investor_save"].ToString() : string.Empty,
                        ["new_investor_update"] = reader["new_investor_update"] != DBNull.Value ? reader["new_investor_update"].ToString() : string.Empty,
                        ["new_investor_barcode"] = reader["new_investor_barcode"] != DBNull.Value ? reader["new_investor_barcode"].ToString() : string.Empty,
                        ["investor_paybookDetails_print"] = reader["investor_paybookDetails_print"] != DBNull.Value ? reader["investor_paybookDetails_print"].ToString() : string.Empty,
                        ["investor_paybookDetails_delete"] = reader["investor_paybookDetails_delete"] != DBNull.Value ? reader["investor_paybookDetails_delete"].ToString() : string.Empty,
                        ["investor_paybookDetails_new"] = reader["investor_paybookDetails_new"] != DBNull.Value ? reader["investor_paybookDetails_new"].ToString() : string.Empty,
                        ["investor_paybookDetails_modify"] = reader["investor_paybookDetails_modify"] != DBNull.Value ? reader["investor_paybookDetails_modify"].ToString() : string.Empty,
                        ["new_investorPayment_save"] = reader["new_investorPayment_save"] != DBNull.Value ? reader["new_investorPayment_save"].ToString() : string.Empty,
                        ["new_investorPayment_update"] = reader["new_investorPayment_update"] != DBNull.Value ? reader["new_investorPayment_update"].ToString() : string.Empty,
                        ["new_investorPayment_savePrint"] = reader["new_investorPayment_savePrint"] != DBNull.Value ? reader["new_investorPayment_savePrint"].ToString() : string.Empty,
                        ["guarantor_details_print"] = reader["guarantor_details_print"] != DBNull.Value ? reader["guarantor_details_print"].ToString() : string.Empty,
                        ["guarantor_details_delete"] = reader["guarantor_details_delete"] != DBNull.Value ? reader["guarantor_details_delete"].ToString() : string.Empty,
                        ["guarantor_details_new"] = reader["guarantor_details_new"] != DBNull.Value ? reader["guarantor_details_new"].ToString() : string.Empty,
                        ["guarantor_details_modify"] = reader["guarantor_details_modify"] != DBNull.Value ? reader["guarantor_details_modify"].ToString() : string.Empty,
                        ["new_guarantor_save"] = reader["new_guarantor_save"] != DBNull.Value ? reader["new_guarantor_save"].ToString() : string.Empty,
                        ["new_guarantor_update"] = reader["new_guarantor_update"] != DBNull.Value ? reader["new_guarantor_update"].ToString() : string.Empty,
                        ["new_guarantor_barcode"] = reader["new_guarantor_barcode"] != DBNull.Value ? reader["new_guarantor_barcode"].ToString() : string.Empty,
                        ["customerOrders_print"] = reader["customerOrders_print"] != DBNull.Value ? reader["customerOrders_print"].ToString() : string.Empty,
                        ["customerOrders_delete"] = reader["customerOrders_delete"] != DBNull.Value ? reader["customerOrders_delete"].ToString() : string.Empty,
                        ["customerOrders_modify"] = reader["customerOrders_modify"] != DBNull.Value ? reader["customerOrders_modify"].ToString() : string.Empty,
                        ["customerOrders_allSales"] = reader["customerOrders_allSales"] != DBNull.Value ? reader["customerOrders_allSales"].ToString() : string.Empty,
                        ["customerOrders_allReturns"] = reader["customerOrders_allReturns"] != DBNull.Value ? reader["customerOrders_allReturns"].ToString() : string.Empty,
                        ["customerOrders_search"] = reader["customerOrders_search"] != DBNull.Value ? reader["customerOrders_search"].ToString() : string.Empty,
                        ["customerOrders_contractForm"] = reader["customerOrders_contractForm"] != DBNull.Value ? reader["customerOrders_contractForm"].ToString() : string.Empty,
                        ["salariesPaybook_details_print"] = reader["salariesPaybook_details_print"] != DBNull.Value ? reader["salariesPaybook_details_print"].ToString() : string.Empty,
                        ["salariesPaybook_details_delete"] = reader["salariesPaybook_details_delete"] != DBNull.Value ? reader["salariesPaybook_details_delete"].ToString() : string.Empty,
                        ["salariesPaybook_details_new"] = reader["salariesPaybook_details_new"] != DBNull.Value ? reader["salariesPaybook_details_new"].ToString() : string.Empty,
                        ["salariesPaybook_details_modify"] = reader["salariesPaybook_details_modify"] != DBNull.Value ? reader["salariesPaybook_details_modify"].ToString() : string.Empty,
                        ["new_salaryPayment_save"] = reader["new_salaryPayment_save"] != DBNull.Value ? reader["new_salaryPayment_save"].ToString() : string.Empty,
                        ["new_salaryPayment_update"] = reader["new_salaryPayment_update"] != DBNull.Value ? reader["new_salaryPayment_update"].ToString() : string.Empty,
                        ["new_salaryPayment_savePrint"] = reader["new_salaryPayment_savePrint"] != DBNull.Value ? reader["new_salaryPayment_savePrint"].ToString() : string.Empty,
                        ["bankLoan_details_print"] = reader["bankLoan_details_print"] != DBNull.Value ? reader["bankLoan_details_print"].ToString() : string.Empty,
                        ["bankLoan_details_delete"] = reader["bankLoan_details_delete"] != DBNull.Value ? reader["bankLoan_details_delete"].ToString() : string.Empty,
                        ["bankLoan_details_new"] = reader["bankLoan_details_new"] != DBNull.Value ? reader["bankLoan_details_new"].ToString() : string.Empty,
                        ["bankLoan_details_modify"] = reader["bankLoan_details_modify"] != DBNull.Value ? reader["bankLoan_details_modify"].ToString() : string.Empty,
                        ["new_bankLoan_save"] = reader["new_bankLoan_save"] != DBNull.Value ? reader["new_bankLoan_save"].ToString() : string.Empty,
                        ["new_bankLoan_update"] = reader["new_bankLoan_update"] != DBNull.Value ? reader["new_bankLoan_update"].ToString() : string.Empty,
                        ["new_bankLoan_exit"] = reader["new_bankLoan_exit"] != DBNull.Value ? reader["new_bankLoan_exit"].ToString() : string.Empty,
                        ["bankLoanPaybook_details_print"] = reader["bankLoanPaybook_details_print"] != DBNull.Value ? reader["bankLoanPaybook_details_print"].ToString() : string.Empty,
                        ["bankLoanPaybook_details_delete"] = reader["bankLoanPaybook_details_delete"] != DBNull.Value ? reader["bankLoanPaybook_details_delete"].ToString() : string.Empty,
                        ["bankLoanPaybook_details_new"] = reader["bankLoanPaybook_details_new"] != DBNull.Value ? reader["bankLoanPaybook_details_new"].ToString() : string.Empty,
                        ["bankLoanPaybook_details_modify"] = reader["bankLoanPaybook_details_modify"] != DBNull.Value ? reader["bankLoanPaybook_details_modify"].ToString() : string.Empty,
                        ["new_bankLoanPayment_save"] = reader["new_bankLoanPayment_save"] != DBNull.Value ? reader["new_bankLoanPayment_save"].ToString() : string.Empty,
                        ["new_bankLoanPayment_update"] = reader["new_bankLoanPayment_update"] != DBNull.Value ? reader["new_bankLoanPayment_update"].ToString() : string.Empty,
                        ["new_bankLoanPayment_exit"] = reader["new_bankLoanPayment_exit"] != DBNull.Value ? reader["new_bankLoanPayment_exit"].ToString() : string.Empty,
                        ["supplierPaybook_print"] = reader["supplierPaybook_print"] != DBNull.Value ? reader["supplierPaybook_print"].ToString() : string.Empty,
                        ["supplierPaybook_delete"] = reader["supplierPaybook_delete"] != DBNull.Value ? reader["supplierPaybook_delete"].ToString() : string.Empty,
                        ["supplierPaybook_new"] = reader["supplierPaybook_new"] != DBNull.Value ? reader["supplierPaybook_new"].ToString() : string.Empty,
                        ["supplierPaybook_modify"] = reader["supplierPaybook_modify"] != DBNull.Value ? reader["supplierPaybook_modify"].ToString() : string.Empty,
                        ["newSupplierPayment_save"] = reader["newSupplierPayment_save"] != DBNull.Value ? reader["newSupplierPayment_save"].ToString() : string.Empty,
                        ["newSupplierPayment_update"] = reader["newSupplierPayment_update"] != DBNull.Value ? reader["newSupplierPayment_update"].ToString() : string.Empty,
                        ["newSupplierPayment_exit"] = reader["newSupplierPayment_exit"] != DBNull.Value ? reader["newSupplierPayment_exit"].ToString() : string.Empty,
                        ["charityPaybook_print"] = reader["charityPaybook_print"] != DBNull.Value ? reader["charityPaybook_print"].ToString() : string.Empty,
                        ["charityPaybook_delete"] = reader["charityPaybook_delete"] != DBNull.Value ? reader["charityPaybook_delete"].ToString() : string.Empty,
                        ["charityPaybook_new"] = reader["charityPaybook_new"] != DBNull.Value ? reader["charityPaybook_new"].ToString() : string.Empty,
                        ["charityPaybook_modify"] = reader["charityPaybook_modify"] != DBNull.Value ? reader["charityPaybook_modify"].ToString() : string.Empty,
                        ["newCharityPayment_save"] = reader["newCharityPayment_save"] != DBNull.Value ? reader["newCharityPayment_save"].ToString() : string.Empty,
                        ["newCharityPayment_update"] = reader["newCharityPayment_update"] != DBNull.Value ? reader["newCharityPayment_update"].ToString() : string.Empty,
                        ["newCharityPayment_exit"] = reader["newCharityPayment_exit"] != DBNull.Value ? reader["newCharityPayment_exit"].ToString() : string.Empty,
                        ["role_id"] = reader["role_id"] != DBNull.Value ? (int)reader["role_id"] : default(int)
                    };

                    result.Add(item);
                }

                return result;
            }
        }

        public string GetPermission(int roleId, string fieldName)
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                var jsonArray = JArray.Parse(jsonData);

                // Search for the item with the specified role_id
                var item = jsonArray.FirstOrDefault(obj => (int)obj["role_id"] == roleId);

                if (item != null)
                {
                    return item[fieldName]?.ToString() ?? $"Field '{fieldName}' not found.";
                }
                else
                {
                    return $"No item found with role_id {roleId}.";
                }
            }
            else
            {
                return "JSON file does not exist.";
            }
        }
    }
}
