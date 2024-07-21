using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class ButtonPermissions1
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public ButtonPermissions1(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "button_permissions1.json"); // Updated file name
        }

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_tbl_authorities_button_controls1"; // Updated query

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
                        ["supplier_details_print"] = reader["supplier_details_print"] != DBNull.Value ? reader["supplier_details_print"].ToString() : string.Empty,
                        ["supplier_details_delete"] = reader["supplier_details_delete"] != DBNull.Value ? reader["supplier_details_delete"].ToString() : string.Empty,
                        ["supplier_details_new"] = reader["supplier_details_new"] != DBNull.Value ? reader["supplier_details_new"].ToString() : string.Empty,
                        ["supplier_details_modify"] = reader["supplier_details_modify"] != DBNull.Value ? reader["supplier_details_modify"].ToString() : string.Empty,
                        ["supplier_details_select"] = reader["supplier_details_select"] != DBNull.Value ? reader["supplier_details_select"].ToString() : string.Empty,
                        ["suppliers_new"] = reader["suppliers_new"] != DBNull.Value ? reader["suppliers_new"].ToString() : string.Empty,
                        ["suppliers_update"] = reader["suppliers_update"] != DBNull.Value ? reader["suppliers_update"].ToString() : string.Empty,
                        ["suppliers_exit"] = reader["suppliers_exit"] != DBNull.Value ? reader["suppliers_exit"].ToString() : string.Empty,
                        ["customer_details_print"] = reader["customer_details_print"] != DBNull.Value ? reader["customer_details_print"].ToString() : string.Empty,
                        ["customer_details_delete"] = reader["customer_details_delete"] != DBNull.Value ? reader["customer_details_delete"].ToString() : string.Empty,
                        ["customer_details_new"] = reader["customer_details_new"] != DBNull.Value ? reader["customer_details_new"].ToString() : string.Empty,
                        ["customer_details_modify"] = reader["customer_details_modify"] != DBNull.Value ? reader["customer_details_modify"].ToString() : string.Empty,
                        ["customer_details_select"] = reader["customer_details_select"] != DBNull.Value ? reader["customer_details_select"].ToString() : string.Empty,
                        ["customers_save"] = reader["customers_save"] != DBNull.Value ? reader["customers_save"].ToString() : string.Empty,
                        ["customers_update"] = reader["customers_update"] != DBNull.Value ? reader["customers_update"].ToString() : string.Empty,
                        ["customers_exit"] = reader["customers_exit"] != DBNull.Value ? reader["customers_exit"].ToString() : string.Empty,
                        ["employee_details_print"] = reader["employee_details_print"] != DBNull.Value ? reader["employee_details_print"].ToString() : string.Empty,
                        ["employee_details_delete"] = reader["employee_details_delete"] != DBNull.Value ? reader["employee_details_delete"].ToString() : string.Empty,
                        ["employee_details_new"] = reader["employee_details_new"] != DBNull.Value ? reader["employee_details_new"].ToString() : string.Empty,
                        ["employee_details_modify"] = reader["employee_details_modify"] != DBNull.Value ? reader["employee_details_modify"].ToString() : string.Empty,
                        ["employee_details_select"] = reader["employee_details_select"] != DBNull.Value ? reader["employee_details_select"].ToString() : string.Empty,
                        ["employees_save"] = reader["employees_save"] != DBNull.Value ? reader["employees_save"].ToString() : string.Empty,
                        ["employees_update"] = reader["employees_update"] != DBNull.Value ? reader["employees_update"].ToString() : string.Empty,
                        ["employees_exit"] = reader["employees_exit"] != DBNull.Value ? reader["employees_exit"].ToString() : string.Empty,
                        ["purchase_details_print"] = reader["purchase_details_print"] != DBNull.Value ? reader["purchase_details_print"].ToString() : string.Empty,
                        ["purchase_details_delete"] = reader["purchase_details_delete"] != DBNull.Value ? reader["purchase_details_delete"].ToString() : string.Empty,
                        ["purchase_details_new"] = reader["purchase_details_new"] != DBNull.Value ? reader["purchase_details_new"].ToString() : string.Empty,
                        ["purchase_details_returns"] = reader["purchase_details_returns"] != DBNull.Value ? reader["purchase_details_returns"].ToString() : string.Empty,
                        ["purchase_details_refresh"] = reader["purchase_details_refresh"] != DBNull.Value ? reader["purchase_details_refresh"].ToString() : string.Empty,
                        ["purchases_save"] = reader["purchases_save"] != DBNull.Value ? reader["purchases_save"].ToString() : string.Empty,
                        ["purchases_print"] = reader["purchases_print"] != DBNull.Value ? reader["purchases_print"].ToString() : string.Empty,
                        ["purchases_exit"] = reader["purchases_exit"] != DBNull.Value ? reader["purchases_exit"].ToString() : string.Empty,
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
