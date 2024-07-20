using Spices_pos.DashboardInfo.Forms;
using Products_info.forms;
using Purchase_info.forms;

namespace Spices_pos.ProductsInfo.controllers
{
    public class Button_controls
    {
        public static void mainMenu_buttons()
        {
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();
        }

        public static void addPurchase_buttons()
        {
            addNewPurchase main = new addNewPurchase();
            main.Show();
        }

        //public static void Add_Product_buttons()
        //{
        //    add_new_product add_product = new add_new_product();
        //    add_product.ShowDialog();
        //}

        public static void Products_detail_buttons()
        {
            product_details product_detail = new product_details();
            product_detail.Show();
        }

        //public static void add_brand_buttons()
        //{
        //    add_brand add_brand = new add_brand();
        //    add_brand.ShowDialog();
        //}

        //public static void add_color_buttons()
        //{
        //    form_color color = new form_color();
        //    color.ShowDialog();
        //}

        //public static void add_category_buttons()
        //{
        //    add_category add_cat = new add_category();
        //    add_cat.ShowDialog();
        //}

        //public static void add_sub_category_buttons()
        //{
        //    new_sub_category add_sub_cat = new new_sub_category();
        //    add_sub_cat.ShowDialog();
        //}

        public static void group_items_buttons()
        {
            form_grouped_products add_sub_cat = new form_grouped_products();
            add_sub_cat.Show();
        }

        public static void expired_items_buttons()
        {
            form_expired_items form = new form_expired_items();
            form.Show();
        }

        public static void CounterSalesButtons()
        {
            //form_counter_sales form = new form_counter_sales();
            //form.Show();
        }

        public static void addNewShelfTitle()
        {
            formAddShelfTitle add_brand = new formAddShelfTitle();
            add_brand.ShowDialog();
        }

        public static void shelf_items_buttons()
        {
            formShelfItems add_sub_cat = new formShelfItems();
            add_sub_cat.Show();
        }
    }
}
