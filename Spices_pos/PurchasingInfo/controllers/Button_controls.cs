using System;
using Datalayer;
using Message_box_info.forms;
using Purchase_info.forms;
using Purchase_info.Reports;
using RefereningMaterial;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Spices_pos.PurchasingInfo.controllers
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

        public static void Add_purchase_buttons()
        {
            addNewPurchase add_purchase = new addNewPurchase();
            add_purchase.Show();
        }

        public static void purchase_detail_buttons()
        {
            Purchase_details purchase_detail = new Purchase_details();
            purchase_detail.Show();
        }

        //public static void company_detail_buttons()
        //{
        //    form_supplier_details purchase_detail = new form_supplier_details();
        //    purchase_detail.Show();
        //}

        public static void Print_Supplier_list_buttons()
        {
            form_company_report supplier = new form_company_report();
            supplier.ShowDialog();
        }

        //public static void Add_company_buttons()
        //{
        //    form_purchase_from purchase_detail = new form_purchase_from();
        //    purchase_detail.ShowDialog();
        //}

        //public static void employee_detail_buttons()
        //{
        //    Add_supplier.saveEnable = false;
        //    Add_supplier purchase_detail = new Add_supplier();
        //    purchase_detail.Show();
        //}

        //public static void add_category_buttons()
        //{
        //    add_category company = new add_category();
        //    company.ShowDialog();
        //}

        //public static void add_color_buttons()
        //{
        //    form_color company = new form_color();
        //    company.ShowDialog();
        //}

        //public static void add_product_buttons(int role_id)
        //{
        //    add_new_product.role_id = role_id;
        //    add_new_product.saveEnable = false;
        //    add_new_product company = new add_new_product();
        //    company.ShowDialog();
        //}

        //public static void company_details_buttons()
        //{
        //    form_supplier_details company = new form_supplier_details();
        //    company.ShowDialog();
        //}

        public static bool save_Customers_records_db()
        {
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            error_form error = new error_form();
            done_form done = new done_form();

            try
            {
                bool chose = false;

                GetSetData.query = @"select full_name from pos_suppliers where full_name = '" + TextData.full_name.ToString() + "';";
                string cus_name_db = data.SearchStringValuesFromDb(GetSetData.query);


                if (cus_name_db == "" && cus_name_db != TextData.full_name)
                {
                    if (TextData.phone2 == "")
                    {
                        TextData.phone2 = "nill";
                    }

                    if (TextData.email == "")
                    {
                        TextData.email = "nill";
                    }

                    if (TextData.country == "--Select--" || TextData.country == "")
                    {
                        TextData.country = "nill";
                    }

                    if (TextData.city == "--Select--" || TextData.city == "")
                    {
                        TextData.city = "nill";
                    }

                    if (TextData.bank_name == "")
                    {
                        TextData.bank_name = "nill";
                    }

                    if (TextData.bank_account == "")
                    {
                        TextData.bank_account = "nill";
                    }

                    if (TextData.remarks == "")
                    {
                        TextData.remarks = "nill";
                    }

                    if (TextData.website == "")
                    {
                        TextData.website = "nill";
                    }

                    if (TextData.contact_person == "")
                    {
                        TextData.contact_person = "nill";
                    }

                    if (TextData.address == "")
                    {
                        TextData.address = "nill";
                    }


                    if (TextData.full_name != "")
                    {
                        if (TextData.phone1 != "")
                        {
                            if (TextData.last_balance.ToString() != "")
                            {
                                TextData.cus_code = form_purchase_from.auto_generate_code(""); // generating Customer Code

                                GetSetData.query = @"select country_id from pos_country where title = '" + TextData.country.ToString() + "';";
                                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                GetSetData.query = @"select city_id from pos_city where title = '" + TextData.city.ToString() + "';";
                                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                GetSetData.query = @"insert into pos_suppliers values ('" + TextData.dates.ToString() + "' , '" + TextData.full_name.ToString() + "' , '" + TextData.cus_code.ToString() + "' , '" + TextData.phone2.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.contact_person.ToString() + "' , '" + TextData.address.ToString() + "' , '" + TextData.email.ToString() + "' , '" + TextData.website.ToString() + "' , '" + TextData.bank_name.ToString() + "' , '" + TextData.bank_account.ToString() + "' , '" + TextData.saved_image_path.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + TextData.status.ToString() + "' , '" + TextData.last_balance.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                GetSetData.query = @"select supplier_id from pos_suppliers where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                                int cus_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                GetSetData.query = @"insert into pos_supplier_payables (previous_payables, due_days, supplier_id) values ('" + TextData.last_balance.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + cus_id_db.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                chose = true;
                            }
                            else
                            {
                                error.errorMessage("Field last balance is empty!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the mobile Number!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter company full name!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("'" + TextData.full_name.ToString() + "' is already exist!");
                    error.ShowDialog();
                }
                return chose;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

    }
}
