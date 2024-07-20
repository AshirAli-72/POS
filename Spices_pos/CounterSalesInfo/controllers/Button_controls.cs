using Spices_pos.DashboardInfo.Forms;
using CounterSales_info.forms;
using Customers_info.forms;
using CounterSales_info.forms.Customer_last_receipt;
using CounterSales_info.forms.Customer_orders_list;
using CounterSales_info.forms.Unhold_orders;
using Products_info.forms;
using Supplier_Chain_info.forms;

namespace Login_info.controllers
{
    public class Button_controls
    {
        public static void mainMenu_buttons()
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
        }

        public static void Pos_button()
        {
            form_counter_sales payment = new form_counter_sales();
            payment.Show();
        }

        public static void Add_Grantors_buttons()
        {
            //Supliers_details.role_id = TextData.role_id;
            form_GranterDetails addSalePerson = new form_GranterDetails();
            addSalePerson.ShowDialog();
        }

        //public static void AddCustomer_buttons()
        //{
        //    add_customer.role_id = TextData.role_id;
        //    add_customer customer = new add_customer();
        //    customer.ShowDialog();
        //}

        //public static void payment_buttons()
        //{
        //    form_payment payment = new form_payment();
        //    payment.ShowDialog();
        //}

        //public static void chequeDetails_buttons()
        //{
        //    formChequeDetails payment = new formChequeDetails();
        //    payment.ShowDialog();
        //}

        //public static void bankDetails_buttons()
        //{
        //    form_bank_title payment = new form_bank_title();
        //    payment.ShowDialog();
        //}

        public static void employeeButton()
        {
            Add_supplier payment = new Add_supplier();
            payment.ShowDialog();
        }

        public static void CustomerButton()
        {
            add_customer payment = new add_customer();
            payment.ShowDialog();
        }

        //public static void Installment_months_buttons()
        //{
        //    formMonths payment = new formMonths();
        //    payment.ShowDialog();
        //}

        public static void customer_last_receipt_buttons()
        {
            form_last_receipt payment = new form_last_receipt();
            payment.ShowDialog();
        }

        //public static void discount_buttons()
        //{
        //    form_discount discount = new form_discount();
        //    discount.ShowDialog();
        //}

        //public static void remarks_buttons()
        //{
        //    form_remarks remarks = new form_remarks();
        //    remarks.ShowDialog();
        //}

        //public static void recovery_buttons()
        //{
        //    Customer_sales_recovery recovery = new Customer_sales_recovery();
        //    recovery.ShowDialog();
        //}

        //public static void calculator_buttons()
        //{
        //    form_calculator recovery = new form_calculator();
        //    recovery.ShowDialog();
        //}

        //public static void Barcodebuttons()
        //{
        //    form_barcode recovery = new form_barcode();
        //    recovery.ShowDialog();
        //}

        public static void customer_orders_buttons()
        {
            form_customer_orders recovery = new form_customer_orders();
            recovery.ShowDialog();
        }

        public static void un_hold_orders_buttons()
        {
            form_unhold recovery = new form_unhold();
            recovery.ShowDialog();
        }

        public static void customer_buttons()
        {
            Customer_details recovery = new Customer_details();
            recovery.ShowDialog();
        }

        public static void Stock_buttons()
        {
            product_details recovery = new product_details();
            recovery.ShowDialog();
        }

        public static void Shelf_Items_buttons()
        {
            formShelfItems recovery = new formShelfItems();
            recovery.ShowDialog();
        }
    }
}
