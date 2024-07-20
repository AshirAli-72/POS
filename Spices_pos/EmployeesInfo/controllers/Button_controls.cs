using System;
using Datalayer;
using Message_box_info.forms;
using Supplier_Chain_info.forms;
using Supplier_Chain_info.Reports;
using RefereningMaterial;
using Supplier_Chain_info.SalariesReports;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Spices_pos.EmployeesInfo.controllers
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

        //public static void Add_Supplier_buttons()
        //{
        //    Add_supplier supplier = new Add_supplier();
        //    supplier.ShowDialog();
        //}

        public static void Print_Supplier_list_buttons()
        {
            form_supplier_list supplier = new form_supplier_list();
            supplier.ShowDialog();
        }

        public static void PrintSalariesPaymentsListbuttons()
        {
            formSalariesReports supplier = new formSalariesReports();
            supplier.ShowDialog();
        }

        public static void Supplier_detail_buttons()
        {
            Supliers_details supplier_detail = new Supliers_details();
            supplier_detail.Show();
        }

        //public static void AddNewSalaryPaymentbuttons()
        //{
        //    formAddSalaryPayment supplier = new formAddSalaryPayment();
        //    supplier.ShowDialog();
        //}

        public static bool save_Supplier_records_db()
        {
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);
            error_form error = new error_form();
            done_form done = new done_form();

            try
            {
                bool chose = false;
                GetSetData.Data = data.UserPermissions("full_name", "pos_employees", "full_name", TextData.full_name);

                if (GetSetData.Data == "" && GetSetData.Data != TextData.full_name)
                {
                    if (TextData.post_code == "")
                    {
                        TextData.post_code = "nill";
                    }

                    if (TextData.zip_code == "")
                    {
                        TextData.zip_code = "nill";
                    }

                    if (TextData.house_no == "")
                    {
                        TextData.house_no = "nill";
                    }

                    if (TextData.address2 == "")
                    {
                        TextData.address2 = "nill";
                    }

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

                    if (TextData.cnic == "")
                    {
                        TextData.cnic = "nill";
                    }

                    if (TextData.province == "--Select--" || TextData.province == "")
                    {
                        TextData.province = "nill";
                    }

                    if (TextData.remarks == "")
                    {
                        TextData.remarks = "nill";
                    }


                    if (TextData.address1 == "")
                    {
                        TextData.address1 = "nill";
                    }

                    if (TextData.full_name != "")
                    {
                        if (TextData.phone1 != "")
                        {
                            if (TextData.salary.ToString() != "")
                            {
                                if (TextData.daily_wages.ToString() != "")
                                {
                                    if (TextData.commission.ToString() != "")
                                    {
                                        TextData.cus_code = Add_supplier.auto_generate_code(""); // generating Employee Code
                                        GetSetData.Ids = data.UserPermissionsIds("country_id", "pos_country", "title", TextData.country);
                                        GetSetData.fks = data.UserPermissionsIds("city_id", "pos_city", "title", TextData.province);

                                        GetSetData.query = @"insert into pos_employees values ('" + TextData.dates.ToString() + "' , '" + TextData.full_name.ToString() + "' , '" + TextData.cus_code.ToString() + "' , '" + TextData.post_code.ToString() + "' , '" + TextData.zip_code.ToString() + "' , '" + TextData.cnic.ToString() + "' , '" + TextData.house_no.ToString() + "' , '" + TextData.phone2.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.address1.ToString() + "' , '" + TextData.address2.ToString() + "' , '" + TextData.email.ToString() + "' , '" + TextData.saved_image_path.ToString() + "' , '" + TextData.salary.ToString() + "' , '" + TextData.daily_wages.ToString() + "' , '" + TextData.commission.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + TextData.status.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                        chose = true;
                                    }
                                    else
                                    {
                                        error.errorMessage("Field commission is empty!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Field daily wages is empty!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Field salary is empty");
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
                        error.errorMessage("Please enter Employee full name!");
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
