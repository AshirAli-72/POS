using Spices_pos.DashboardInfo.Forms;
using Banking_info.forms;

namespace Banking_info.controllers
{
    public class Button_controls
    {
        public static void mainMenu_buttons()
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
        }

        //public static void Add_transaction_buttons()
        //{
        //    form_add_transaction add_customer = new form_add_transaction();
        //    add_customer.ShowDialog();
        //}

        public static void bank_detail_buttons()
        {
            form_bank_details customer_detail = new form_bank_details();
            customer_detail.Show();
        }

        //public static void status_button()
        //{
        //    form_status customer_detail = new form_status();
        //    customer_detail.ShowDialog();
        //}

        //public static void Transaction_type_buttons()
        //{
        //    form_transaction_type customer_detail = new form_transaction_type();
        //    customer_detail.ShowDialog();
        //}

        //public static void bank_buttons()
        //{
        //    form_bank_title customer_detail = new form_bank_title();
        //    customer_detail.ShowDialog();
        //}

        //public static void branch_buttons()
        //{
        //    form_branch_title customer_detail = new form_branch_title();
        //    customer_detail.ShowDialog();
        //}

        //public static void Account_buttons()
        //{
        //    form_account_title customer_detail = new form_account_title();
        //    customer_detail.ShowDialog();
        //}

        //public static void AccountNo_buttons()
        //{
        //    form_account_number customer_detail = new form_account_number();
        //    customer_detail.ShowDialog();
        //}

        //public static void employee_buttons()
        //{
        //    Add_supplier customer_detail = new Add_supplier();
        //    customer_detail.ShowDialog();
        //}
    }
}
