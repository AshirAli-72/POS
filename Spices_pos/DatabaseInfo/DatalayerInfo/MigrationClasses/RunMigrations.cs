using System.Data.SqlClient;
using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions; // for Regex.Split
using Datalayer;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses
{
    public class RunMigrations
    {
        Datalayers data = new Datalayers(webConfig.con_string);
        public string connection_string = "";

        public RunMigrations(string con_String)
        {
            this.connection_string = con_String;
        }

        public void runMigration(string migrationFileName)
        {
            int isMigrationExist = data.UserPermissionsIds("id", "pos_migrations", "migration", migrationFileName);

            if (isMigrationExist == 0)
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string migrationsDirectory = Path.Combine(baseDirectory, "Migrations");

                string migrationFilePath = Path.Combine(migrationsDirectory, migrationFileName);

                // Ensure the Migrations directory exists
                if (!Directory.Exists(migrationsDirectory))
                {
                    Directory.CreateDirectory(migrationsDirectory);
                }

                // Copy the migration file to the Migrations directory if it doesn't exist there
                if (!File.Exists(migrationFilePath))
                {
                    string sourceMigrationFilePath = @"D:\Dot Net Projects\POS_LATEST_USA_New_Design\Spices_pos\DatabaseInfo\DatalayerInfo\Migrations\" + migrationFileName;

                    if (File.Exists(sourceMigrationFilePath))
                    {
                        File.Copy(sourceMigrationFilePath, migrationFilePath);
                    }
                }

                // Read and execute the SQL script from the Migrations directory
                if (File.Exists(migrationFilePath))
                {
                    string sql = File.ReadAllText(migrationFilePath);

                    if (ExecuteMigration(sql))
                    {
                        // use parameters instead of concatenation
                        string query = "INSERT INTO pos_migrations (date,time, migration) VALUES (@date, @time, @file)";
                        using (SqlConnection conn = new SqlConnection(this.connection_string))
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToLongTimeString());
                            cmd.Parameters.AddWithValue("@file", migrationFileName);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Migration file not found at {migrationFilePath}");
                }
            }
        }

        private bool ExecuteMigration(string sql)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.connection_string))
                {
                    connection.Open();

                    // Split script on "GO" (case-insensitive, line-based)
                    string[] sqlCommands = Regex.Split(sql, @"^\s*GO\s*$",
                        RegexOptions.Multiline | RegexOptions.IgnoreCase);

                    foreach (string command in sqlCommands)
                    {
                        string trimmed = command.Trim();
                        if (!string.IsNullOrWhiteSpace(trimmed))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(trimmed, connection))
                            {
                                try
                                {
                                    sqlCommand.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show($"Error executing SQL command:\n\n{trimmed}\n\n{ex.Message}",
                                        "Migration Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                    return false; // stop if error
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while executing the migration: {ex.Message}",
                    "Migration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
