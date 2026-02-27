using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class ButtonPermissions2
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public ButtonPermissions2(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "button_permissions2.json"); // Updated file name
        }

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_tbl_authorities_button_controls2"; // ✅ Updated table

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

                        // ✅ Products
                        ["products_details_print"] = reader["products_details_print"] != DBNull.Value ? reader["products_details_print"].ToString() : string.Empty,
                        ["products_details_delete"] = reader["products_details_delete"] != DBNull.Value ? reader["products_details_delete"].ToString() : string.Empty,
                        ["products_details_new"] = reader["products_details_new"] != DBNull.Value ? reader["products_details_new"].ToString() : string.Empty,
                        ["products_details_modify"] = reader["products_details_modify"] != DBNull.Value ? reader["products_details_modify"].ToString() : string.Empty,
                        ["products_details_regular"] = reader["products_details_regular"] != DBNull.Value ? reader["products_details_regular"].ToString() : string.Empty,
                        ["products_expired"] = reader["products_expired"] != DBNull.Value ? reader["products_expired"].ToString() : string.Empty,
                        ["products_save"] = reader["products_save"] != DBNull.Value ? reader["products_save"].ToString() : string.Empty,
                        ["products_update"] = reader["products_update"] != DBNull.Value ? reader["products_update"].ToString() : string.Empty,
                        ["products_exit"] = reader["products_exit"] != DBNull.Value ? reader["products_exit"].ToString() : string.Empty,

                        // ✅ Recovery
                        ["recovery_details_print"] = reader["recovery_details_print"] != DBNull.Value ? reader["recovery_details_print"].ToString() : string.Empty,
                        ["recovery_details_new"] = reader["recovery_details_new"] != DBNull.Value ? reader["recovery_details_new"].ToString() : string.Empty,
                        ["recovery_details_delete"] = reader["recovery_details_delete"] != DBNull.Value ? reader["recovery_details_delete"].ToString() : string.Empty,
                        ["recovery_details_modify"] = reader["recovery_details_modify"] != DBNull.Value ? reader["recovery_details_modify"].ToString() : string.Empty,
                        ["recovery_details_Invoices"] = reader["recovery_details_Invoices"] != DBNull.Value ? reader["recovery_details_Invoices"].ToString() : string.Empty,
                        ["recoveries_save"] = reader["recoveries_save"] != DBNull.Value ? reader["recoveries_save"].ToString() : string.Empty,
                        ["recoveries_print"] = reader["recoveries_print"] != DBNull.Value ? reader["recoveries_print"].ToString() : string.Empty,
                        ["recoveries_exit"] = reader["recoveries_exit"] != DBNull.Value ? reader["recoveries_exit"].ToString() : string.Empty,

                        // ✅ Expenses
                        ["expenses_details_print"] = reader["expenses_details_print"] != DBNull.Value ? reader["expenses_details_print"].ToString() : string.Empty,
                        ["expenses_details_delete"] = reader["expenses_details_delete"] != DBNull.Value ? reader["expenses_details_delete"].ToString() : string.Empty,
                        ["expenses_details_new"] = reader["expenses_details_new"] != DBNull.Value ? reader["expenses_details_new"].ToString() : string.Empty,
                        ["expenses_details_modify"] = reader["expenses_details_modify"] != DBNull.Value ? reader["expenses_details_modify"].ToString() : string.Empty,
                        ["expenses_details_refresh"] = reader["expenses_details_refresh"] != DBNull.Value ? reader["expenses_details_refresh"].ToString() : string.Empty,
                        ["expenses_save"] = reader["expenses_save"] != DBNull.Value ? reader["expenses_save"].ToString() : string.Empty,
                        ["expenses_update"] = reader["expenses_update"] != DBNull.Value ? reader["expenses_update"].ToString() : string.Empty,
                        ["expenses_exit"] = reader["expenses_exit"] != DBNull.Value ? reader["expenses_exit"].ToString() : string.Empty,

                        // ✅ Dues
                        ["dues_print"] = reader["dues_print"] != DBNull.Value ? reader["dues_print"].ToString() : string.Empty,
                        ["dues_refresh"] = reader["dues_refresh"] != DBNull.Value ? reader["dues_refresh"].ToString() : string.Empty,
                        ["dues_exit"] = reader["dues_exit"] != DBNull.Value ? reader["dues_exit"].ToString() : string.Empty,

                        // ✅ Stock
                        ["stock_whole"] = reader["stock_whole"] != DBNull.Value ? reader["stock_whole"].ToString() : string.Empty,
                        ["stock_low"] = reader["stock_low"] != DBNull.Value ? reader["stock_low"].ToString() : string.Empty,
                        ["stock_print"] = reader["stock_print"] != DBNull.Value ? reader["stock_print"].ToString() : string.Empty,
                        ["stock_refresh"] = reader["stock_refresh"] != DBNull.Value ? reader["stock_refresh"].ToString() : string.Empty,
                        ["stock_exit"] = reader["stock_exit"] != DBNull.Value ? reader["stock_exit"].ToString() : string.Empty,

                        // ✅ Settings
                        ["settings_reg"] = reader["settings_reg"] != DBNull.Value ? reader["settings_reg"].ToString() : string.Empty,
                        ["settings_config"] = reader["settings_config"] != DBNull.Value ? reader["settings_config"].ToString() : string.Empty,
                        ["settings_reports"] = reader["settings_reports"] != DBNull.Value ? reader["settings_reports"].ToString() : string.Empty,
                        ["settings_login_details"] = reader["settings_login_details"] != DBNull.Value ? reader["settings_login_details"].ToString() : string.Empty,
                        ["settings_general"] = reader["settings_general"] != DBNull.Value ? reader["settings_general"].ToString() : string.Empty,

                        // ✅ Banking
                        ["banking_details_print"] = reader["banking_details_print"] != DBNull.Value ? reader["banking_details_print"].ToString() : string.Empty,
                        ["banking_details_delete"] = reader["banking_details_delete"] != DBNull.Value ? reader["banking_details_delete"].ToString() : string.Empty,
                        ["banking_details_new"] = reader["banking_details_new"] != DBNull.Value ? reader["banking_details_new"].ToString() : string.Empty,
                        ["banking_details_modify"] = reader["banking_details_modify"] != DBNull.Value ? reader["banking_details_modify"].ToString() : string.Empty,

                        // ✅ Transactions
                        ["new_transaction_save"] = reader["new_transaction_save"] != DBNull.Value ? reader["new_transaction_save"].ToString() : string.Empty,
                        ["new_transaction_update"] = reader["new_transaction_update"] != DBNull.Value ? reader["new_transaction_update"].ToString() : string.Empty,
                        ["new_transaction_savePrint"] = reader["new_transaction_savePrint"] != DBNull.Value ? reader["new_transaction_savePrint"].ToString() : string.Empty,
                        ["new_transaction_exit"] = reader["new_transaction_exit"] != DBNull.Value ? reader["new_transaction_exit"].ToString() : string.Empty,

                        // ✅ Demands
                        ["demand_list_print"] = reader["demand_list_print"] != DBNull.Value ? reader["demand_list_print"].ToString() : string.Empty,
                        ["demand_list_delete"] = reader["demand_list_delete"] != DBNull.Value ? reader["demand_list_delete"].ToString() : string.Empty,
                        ["demand_list_new"] = reader["demand_list_new"] != DBNull.Value ? reader["demand_list_new"].ToString() : string.Empty,
                        ["demand_list_modify"] = reader["demand_list_modify"] != DBNull.Value ? reader["demand_list_modify"].ToString() : string.Empty,
                        ["new_demand_save"] = reader["new_demand_save"] != DBNull.Value ? reader["new_demand_save"].ToString() : string.Empty,
                        ["new_demand_update"] = reader["new_demand_update"] != DBNull.Value ? reader["new_demand_update"].ToString() : string.Empty,
                        ["new_demand_savePrint"] = reader["new_demand_savePrint"] != DBNull.Value ? reader["new_demand_savePrint"].ToString() : string.Empty,
                        ["new_demand_exit"] = reader["new_demand_exit"] != DBNull.Value ? reader["new_demand_exit"].ToString() : string.Empty,

                        // ✅ NEW SECTION — Cash Management Details
                        ["cash_details_new"] = reader["cash_details_new"] != DBNull.Value ? reader["cash_details_new"].ToString() : string.Empty,
                        ["cash_details_delete"] = reader["cash_details_delete"] != DBNull.Value ? reader["cash_details_delete"].ToString() : string.Empty,
                        ["cash_details_modify"] = reader["cash_details_modify"] != DBNull.Value ? reader["cash_details_modify"].ToString() : string.Empty,
                        ["cash_details_print"] = reader["cash_details_print"] != DBNull.Value ? reader["cash_details_print"].ToString() : string.Empty,

                        ["role_id"] = reader["role_id"] != DBNull.Value ? (int)reader["role_id"] : default(int)
                    };

                    result.Add(item);
                }

                return result;
            }
        }


        public string ReadFieldByRoleId(int roleId, string fieldName)
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
