using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses
{
    public class ClassCreateUpdateJsonFile
    {
        public static void readWritePermissionsJsonFile()
        {
            DashboardPermissionsManager dashboardPermissions = new DashboardPermissionsManager(webConfig.con_string);
            ReportPermissionsManager reportPermissions = new ReportPermissionsManager(webConfig.con_string);
            ButtonPermissions1 buttonPermission1 = new ButtonPermissions1(webConfig.con_string);
            ButtonPermissions2 buttonPermission2 = new ButtonPermissions2(webConfig.con_string);
            ButtonPermissions3 buttonPermission3 = new ButtonPermissions3(webConfig.con_string);


            //Creating BaseDirectory
            dashboardPermissions.CreateJsonFilesFolder();

            //Create JsonFiles in BaseDirectory Files
            dashboardPermissions.CreateOrUpdateJsonFile();
            reportPermissions.CreateOrUpdateJsonFile();
            buttonPermission1.CreateOrUpdateJsonFile();
            buttonPermission2.CreateOrUpdateJsonFile();
            buttonPermission3.CreateOrUpdateJsonFile();
        }
        
        public static void readWriteGeneralSettingsJsonFile()
        {
            GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);

            generalSettings.CreateOrUpdateJsonFile();
        }
    }
}
