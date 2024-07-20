using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login_info.controllers
{
    public class TextData
    {
        //Configuration Settings
        public static string shop_name { get; set; }
        public static string owner_name { get; set; }
        public static string city { get; set; }
        public static string address { get; set; }
        public static string business_type { get; set; }
        public static string branch { get; set; }
        public static string shop_no { get; set; }
        public static string phone1 { get; set; }
        public static string phone2 { get; set; }
        public static string logo_path { get; set; }
        public static string email { get; set; }
        public static string comments { get; set; }
        public static string person_name { get; set; }
        public static string role_title { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string confirm_password { get; set; }

        //Report Settings
        public static string title { get; set; }
        public static string sub_title { get; set; }
        public static string message { get; set; }
        public static string copyrights { get; set; }

        //Dashboard CheckBoxes
        public static bool loader { get; set; }
        public static bool loader_returns { get; set; }
        public static bool loyal_customer { get; set; }
        public static bool loyal_returns { get; set; }
        public static bool products { get; set; }
        public static bool purchases { get; set; }
        public static bool expenses { get; set; }
        public static bool company_reg { get; set; }
        public static bool paybook { get; set; }
        public static bool recoveries { get; set; }
        public static bool customers { get; set; }
        public static bool suppliers { get; set; }
        public static bool customer_dues { get; set; }
        public static bool pos { get; set; }
        public static bool logOut { get; set; }
        public static bool backup { get; set; }
        public static bool restore { get; set; }
        public static bool settings { get; set; }
        public static bool notifications { get; set; }
        public static bool about { get; set; }

        //Reports CheckBoxes
        public static bool cus_ledger { get; set; }
        public static bool cus_sales { get; set; }
        public static bool cus_returns { get; set; }
        public static bool loader_reports { get; set; }
        public static bool day_book { get; set; }
        public static bool stock { get; set; }
        public static bool cus_recoveries { get; set; }
        public static bool company_payments { get; set; }
        public static bool receivables { get; set; }
        public static bool payables { get; set; }
        public static bool balance_in_out { get; set; }
        public static bool income_statement { get; set; }
        public static bool company_ledger { get; set; }

        //Button Controls CheckBoxes
        public static bool company_reg_save { get; set; }
        public static bool company_reg_update { get; set; }
        public static bool company_reg_print { get; set; }
        public static bool company_reg_new { get; set; }
        public static bool company_reg_refresh { get; set; }

        public static bool corporate_business_save { get; set; }
        public static bool corporate_business_save_print { get; set; }
        public static bool corporate_business_exit { get; set; }

        public static bool customers_save { get; set; }
        public static bool customers_update { get; set; }
        public static bool customers_print { get; set; }
        public static bool customers_new { get; set; }
        public static bool customers_refresh { get; set; }

        public static bool expenses_save { get; set; }
        public static bool expenses_update { get; set; }
        public static bool expenses_print { get; set; }
        public static bool expenses_new { get; set; }
        public static bool expenses_refresh { get; set; }

        public static bool products_save { get; set; }
        public static bool products_update { get; set; }
        public static bool products_print { get; set; }
        public static bool products_new { get; set; }
        public static bool products_refresh { get; set; }

        public static bool purchases_save { get; set; }
        public static bool purchases_update { get; set; }
        public static bool purchases_print { get; set; }
        public static bool purchases_new { get; set; }
        public static bool purchases_refresh { get; set; }

        public static bool recoveries_save { get; set; }
        public static bool recoveries_save_print { get; set; }
        public static bool recoveries_exit { get; set; }

        public static bool customer_dues_print { get; set; }
        public static bool customer_dues_refresh { get; set; }
        public static bool customer_dues_exit { get; set; }

        public static bool pos_save { get; set; }
        public static bool pos_update { get; set; }
        public static bool pos_save_print { get; set; }
        public static bool pos_exit { get; set; }

        public static bool loyal_cus_sales_save { get; set; }
        public static bool loyal_cus_sales_update { get; set; }
        public static bool loyal_cus_sales_save_print { get; set; }
        public static bool loyal_cus_sales_exit { get; set; }

        public static bool loader_prod_return_save { get; set; }
        public static bool loader_prod_return_save_print { get; set; }
        public static bool loader_prod_return_exit { get; set; }

        public static bool loader_cus_sales_save { get; set; }
        public static bool loader_cus_sales_save_print { get; set; }
        public static bool loader_cus_sales_exit { get; set; }

        public static bool loader_prod_save { get; set; }
        public static bool loader_prod_save_print { get; set; }
        public static bool loader_prod_exit { get; set; }

        public static bool settings_reg { get; set; }
        public static bool settings_config { get; set; }
        public static bool settings_reports { get; set; }
        public static bool settings_authority { get; set; }

        public static bool whole_low_stock_print { get; set; }
        public static bool whole_low_stock_refresh { get; set; }
        public static bool whole_low_stock_exit { get; set; }

        public static bool suppliers_save { get; set; }
        public static bool suppliers_update { get; set; }
        public static bool suppliers_print { get; set; }
        public static bool suppliers_new { get; set; }
        public static bool suppliers_refresh { get; set; }

    }
}
