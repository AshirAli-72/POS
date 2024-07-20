
namespace Spices_pos.DatabaseInfo.WebConfig
{
    public class webConfig
    {
        //public static string server_name = @"Data Source=192.168.1.1,1433";
        public static string server_name = @"Data Source=.\SQLEXPRESS";

        //public static string con_string = server_name + @";Initial Catalog=installment_db;Integrated Security = True;";
        //public static string logDb_conString = server_name + @";Initial Catalog=logHistorySIMS_db;Integrated Security = True;";

        public static string con_string = server_name + @";Initial Catalog=installment_db;Persist Security Info=True;User ID=sa;Password=rootsguruadmin@123;";
        //public static string logDb_conString = server_name + @";Initial Catalog=logHistorySIMS_db;Persist Security Info=True;User ID=sa;Password=rootsguruadmin@123;";
    }
}
