using System.Data.SqlClient;
using System;
using System.Windows;
using System.IO;
using System.Windows.Markup;
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
                //string migrationFileName = "script_version_0.2.23.sql";


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
                    //else
                    //{
                    //    MessageBox.Show($"Source migration file not found at {sourceMigrationFilePath}");
                    //    return;
                    //}
                }

                // Read and execute the SQL script from the Migrations directory
                if (File.Exists(migrationFilePath))
                {
                    string sql = File.ReadAllText(migrationFilePath);


                    if (ExecuteMigration(sql))
                    {
                        string query = @"insert into pos_migrations values ('" + DateTime.Now.ToShortDateString() + "' , '" + DateTime.Now.ToLongTimeString() + "', '" + migrationFileName + "');";
                        data.insertUpdateCreateOrDelete(query);
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

                    // Split script on "GO" keyword
                    string[] sqlCommands = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string command in sqlCommands)
                    {
                        if (!string.IsNullOrWhiteSpace(command))
                        {
                            using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                            {
                                try
                                {
                                    sqlCommand.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    // Log or display the error message, but continue processing
                                    //Console.WriteLine($"Warning: Error executing command - {ex.Message}");
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log or display the overall error if the connection fails
                MessageBox.Show($"An error occurred while executing the migration: {ex.Message}");
                return false;
            }
        }


        //private bool ExecuteMigration(string sql)
        //{
        //    try
        //    {               
        //        using (SqlConnection connection = new SqlConnection(this.connection_string))
        //        {
        //            connection.Open();

        //            // Split script on "GO" keyword
        //            string[] sqlCommands = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

        //            foreach (string command in sqlCommands)
        //            {
        //                if (!string.IsNullOrWhiteSpace(command))
        //                {
        //                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
        //                    {
        //                        sqlCommand.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred while executing the migration: {ex.Message}");

        //        return false;
        //    }
        //}
    }
}
