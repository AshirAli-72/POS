using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class DashboardPermissionsManager
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public DashboardPermissionsManager(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "dashboard_permissions.json");
        }

        public void CreateJsonFilesFolder()
        {
            if (!Directory.Exists(jsonFilesFolder))
            {
                Directory.CreateDirectory(jsonFilesFolder);
            }
        }

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_tbl_authorities_dashboard";

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
                        ["dashboard_id"] = reader["dashboard_id"] != DBNull.Value ? (int)reader["dashboard_id"] : default(int),
                        ["pos"] = reader["pos"] != DBNull.Value ? reader["pos"].ToString() : string.Empty,
                        ["purchases"] = reader["purchases"] != DBNull.Value ? reader["purchases"].ToString() : string.Empty,
                        ["products"] = reader["products"] != DBNull.Value ? reader["products"].ToString() : string.Empty,
                        ["recoveries"] = reader["recoveries"] != DBNull.Value ? reader["recoveries"].ToString() : string.Empty,
                        ["expenses"] = reader["expenses"] != DBNull.Value ? reader["expenses"].ToString() : string.Empty,
                        ["suppliers"] = reader["suppliers"] != DBNull.Value ? reader["suppliers"].ToString() : string.Empty,
                        ["employee"] = reader["employee"] != DBNull.Value ? reader["employee"].ToString() : string.Empty,
                        ["customers"] = reader["customers"] != DBNull.Value ? reader["customers"].ToString() : string.Empty,
                        ["stock"] = reader["stock"] != DBNull.Value ? reader["stock"].ToString() : string.Empty,
                        ["reports"] = reader["reports"] != DBNull.Value ? reader["reports"].ToString() : string.Empty,
                        ["customer_dues"] = reader["customer_dues"] != DBNull.Value ? reader["customer_dues"].ToString() : string.Empty,
                        ["settings"] = reader["settings"] != DBNull.Value ? reader["settings"].ToString() : string.Empty,
                        ["notifications"] = reader["notifications"] != DBNull.Value ? reader["notifications"].ToString() : string.Empty,
                        ["backups"] = reader["backups"] != DBNull.Value ? reader["backups"].ToString() : string.Empty,
                        ["restores"] = reader["restores"] != DBNull.Value ? reader["restores"].ToString() : string.Empty,
                        ["about"] = reader["about"] != DBNull.Value ? reader["about"].ToString() : string.Empty,
                        ["logout"] = reader["logout"] != DBNull.Value ? reader["logout"].ToString() : string.Empty,
                        ["capital"] = reader["capital"] != DBNull.Value ? reader["capital"].ToString() : string.Empty,
                        ["dailyBalance"] = reader["dailyBalance"] != DBNull.Value ? reader["dailyBalance"].ToString() : string.Empty,
                        ["investors"] = reader["investors"] != DBNull.Value ? reader["investors"].ToString() : string.Empty,
                        ["investorPaybook"] = reader["investorPaybook"] != DBNull.Value ? reader["investorPaybook"].ToString() : string.Empty,
                        ["guarantors"] = reader["guarantors"] != DBNull.Value ? reader["guarantors"].ToString() : string.Empty,
                        ["aboutLicense"] = reader["aboutLicense"] != DBNull.Value ? reader["aboutLicense"].ToString() : string.Empty,
                        ["EmployeeSalaries"] = reader["EmployeeSalaries"] != DBNull.Value ? reader["EmployeeSalaries"].ToString() : string.Empty,
                        ["bankLoan"] = reader["bankLoan"] != DBNull.Value ? reader["bankLoan"].ToString() : string.Empty,
                        ["bankLoanPaybook"] = reader["bankLoanPaybook"] != DBNull.Value ? reader["bankLoanPaybook"].ToString() : string.Empty,
                        ["supplierPaybook"] = reader["supplierPaybook"] != DBNull.Value ? reader["supplierPaybook"].ToString() : string.Empty,
                        ["charity"] = reader["charity"] != DBNull.Value ? reader["charity"].ToString() : string.Empty,
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

