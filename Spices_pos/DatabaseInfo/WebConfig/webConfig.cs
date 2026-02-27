using System;
using System.IO;
using System.Windows.Forms;

namespace Spices_pos.DatabaseInfo.WebConfig
{
    public static class webConfig
    {
        // 🔹 Connection file path
        private static string ConnectionFilePath
        {
            get
            {
                string folder = @"D:\POS_LATEST_USA_New_Design\Spices_pos\DatabaseInfo\WebConfig";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                return Path.Combine(folder, "pos_connection.txt");
            }
        }

        // 🔹 Public static field for connection string
        public static string con_string;

        // 🔹 Static constructor - lazy load + safety
        static webConfig()
        {
            try
            {
                if (File.Exists(ConnectionFilePath))
                {
                    con_string = File.ReadAllText(ConnectionFilePath);

                    // ⚠️ Safety check
                    if (string.IsNullOrWhiteSpace(con_string))
                        throw new Exception("Connection string file is empty, using default connection.");
                }
                else
                {
                    // File missing → create default
                    con_string = @"Data Source=DESKTOP-N9QMTPG\SQLEXPRESS;Initial Catalog=installment_db;Integrated Security=True;";
                    File.WriteAllText(ConnectionFilePath, con_string);
                }
            }
            catch (Exception ex)
            {
                // Fallback default
                con_string = @"Data Source=DESKTOP-N9QMTPG\SQLEXPRESS;Initial Catalog=installment_db;Integrated Security=True;";
                MessageBox.Show("Error reading connection file. Using default connection.\n\nDetails: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 🔹 Save connection string dynamically
        public static void SaveConnectionString(string connectionString)
        {
            try
            {
                File.WriteAllText(ConnectionFilePath, connectionString);
                con_string = connectionString; // Update static field immediately
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving connection string.\n\nDetails: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Optional helper: validate connection string
        public static bool TestConnection()
        {
            try
            {
                using (var conn = new System.Data.SqlClient.SqlConnection(con_string))
                {
                    conn.Open();
                    conn.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}





/*
namespace Spices_pos.DatabaseInfo.WebConfig
{
    public static class webConfig
    {
        public static string server_name = @"DESKTOP-N9QMTPG\SQLEXPRESS";

        // Data Source= keyword add karna ZAROORI hai
        public static string con_string =
            @"Data Source=" + server_name + @";Initial Catalog=installment_db;Integrated Security=True;";
    }
}*/
