using Reports_info.CustomerLedgerReports;
using Reports_info.Customer_sales_reports.loyal_customer_sales_reports;
using Customers_info.forms;
using Spices_pos.DashboardInfo.Forms;

namespace Reports_info.controllers
{
    class button_controls
    {
        public static void Main_menu()
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
        }

        public static void LoyalCustomer()
        {
            Customer_legers_form ledger = new Customer_legers_form();
            ledger.Show();
        }

        public static void loyalCusSales()
        {
            form_loyal_cus_sales sales = new form_loyal_cus_sales();
            sales.Show();
        }

        public static void CustomerDetailsbuttons()
        {
            Customer_details customer_detail = new Customer_details();
            customer_detail.ShowDialog();
        }

    }
}
