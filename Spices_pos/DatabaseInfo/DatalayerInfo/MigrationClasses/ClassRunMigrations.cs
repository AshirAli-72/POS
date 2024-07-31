using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses
{
    public class ClassRunMigrations
    {
        public static void migrationScripts()
        {
            RunMigrations runScripts = new RunMigrations(webConfig.con_string);

            runScripts.runMigration("tables_script_version_0.2.23.sql");
            runScripts.runMigration("indexes_script_version_0.2.23.sql");
            runScripts.runMigration("create_views_procedures_script_version_0.2.23.sql");
            runScripts.runMigration("alter_views_procedures_script_version_0.2.23.sql");
            runScripts.runMigration("tables_script_version_0.2.25.sql");

            System.Windows.Forms.MessageBox.Show("Migrations Executed Successfully...");
        }
    }
}
