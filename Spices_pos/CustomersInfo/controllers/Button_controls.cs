using Customers_info.forms;
using Customers_info.GurantorReports;
using Customers_info.Reports;
using Settings_info.forms;

namespace Spices_pos.CustomersInfo.controllers
{
    public class Button_controls
    {
        //public static void mainMenu_buttons()
        //{
        //    Menus.login_checked = "";
        //    Menus main = new Menus();
        //    main.Show();
        //}

        public static void mainMenu_buttons()
        {
            settings main = new settings();
            main.Show();
        }


        //public static void Add_customer_buttons()
        //{
        //    add_customer add_customer = new add_customer();
        //    add_customer.ShowDialog();
        //}

        public static void Customer_detail_buttons()
        {
            Customer_details customer_detail = new Customer_details();
            customer_detail.Show();
        }

        //public static void Add_granter_buttons()
        //{
        //    formNewGrantors add_customer = new formNewGrantors();
        //    add_customer.ShowDialog();
        //}

        public static void granter_detail_buttons()
        {
            form_GranterDetails customer_detail = new form_GranterDetails();
            customer_detail.Show();
        }


        public static void CustomerDetailsbuttons()
        {
            Customer_details customer_detail = new Customer_details();
            customer_detail.ShowDialog();
        }

        public static void Customers_list_Report_buttons()
        {
            formGurantorsReports add_customer = new formGurantorsReports();
            add_customer.ShowDialog();
        }


        public static void CustomersDetailsButton()
        {
            form_Customer_list add_customer = new form_Customer_list();
            add_customer.ShowDialog();
        }
    }
}
