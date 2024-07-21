using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class ReportPermissionsManager
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public ReportPermissionsManager(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "report_permissions.json"); // Updated file name
            //CreateJsonFilesFolder();
            //CreateOrUpdateJsonFile();
        }

        //private void CreateJsonFilesFolder()
        //{
        //    if (!Directory.Exists(jsonFilesFolder))
        //    {
        //        Directory.CreateDirectory(jsonFilesFolder);
        //    }
        //}


        //private void CreateOrUpdateJsonFile()
        //{
        //    if (!File.Exists(jsonFilePath))
        //    {
        //        var data = FetchDataFromDatabase();
        //        File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        //    }
        //}

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }


        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_tbl_authorities_reports"; // Updated query

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
                        ["reports_id"] = reader["reports_id"] != DBNull.Value ? (int)reader["reports_id"] : default(int),
                        ["company_ledger"] = reader["company_ledger"] != DBNull.Value ? reader["company_ledger"].ToString() : string.Empty,
                        ["company_statement"] = reader["company_statement"] != DBNull.Value ? reader["company_statement"].ToString() : string.Empty,
                        ["customer_ledger"] = reader["customer_ledger"] != DBNull.Value ? reader["customer_ledger"].ToString() : string.Empty,
                        ["customer_statement"] = reader["customer_statement"] != DBNull.Value ? reader["customer_statement"].ToString() : string.Empty,
                        ["day_book"] = reader["day_book"] != DBNull.Value ? reader["day_book"].ToString() : string.Empty,
                        ["stock"] = reader["stock"] != DBNull.Value ? reader["stock"].ToString() : string.Empty,
                        ["sales_report"] = reader["sales_report"] != DBNull.Value ? reader["sales_report"].ToString() : string.Empty,
                        ["returns_report"] = reader["returns_report"] != DBNull.Value ? reader["returns_report"].ToString() : string.Empty,
                        ["receivables"] = reader["receivables"] != DBNull.Value ? reader["receivables"].ToString() : string.Empty,
                        ["payables"] = reader["payables"] != DBNull.Value ? reader["payables"].ToString() : string.Empty,
                        ["recoveries"] = reader["recoveries"] != DBNull.Value ? reader["recoveries"].ToString() : string.Empty,
                        ["balance_in_out"] = reader["balance_in_out"] != DBNull.Value ? reader["balance_in_out"].ToString() : string.Empty,
                        ["income_statement"] = reader["income_statement"] != DBNull.Value ? reader["income_statement"].ToString() : string.Empty,
                        ["generateInvoices"] = reader["generateInvoices"] != DBNull.Value ? reader["generateInvoices"].ToString() : string.Empty,
                        ["chequeDetails"] = reader["chequeDetails"] != DBNull.Value ? reader["chequeDetails"].ToString() : string.Empty,
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
