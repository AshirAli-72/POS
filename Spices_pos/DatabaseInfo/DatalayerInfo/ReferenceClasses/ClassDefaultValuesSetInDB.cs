using System;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace RefereningMaterial.ReferenceClasses
{
    public class ClassDefaultValuesSetInDB
    {
        public static void InsertValuesInTableRegistrationAndPermissions()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select role_id from pos_roles where (roleTitle = 'Admin');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_roles values ('Admin');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    // **********************************************************************
                    GetSetData.query = @"insert into pos_country values ('nill')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    // **********************************************************************

                    GetSetData.query = @"insert into pos_city values ('nill')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                    // **********************************************************************

                    GetSetData.query = @"select employee_id from pos_employees where (full_name = 'nill');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_employees values ('2021-01-21', 'nill', '1000000001', 'nill', 'nill', 'nill', 'nill', 'nill', '1', 'a' , 'nill', 'nill', 'nill', '0', '0', '0', 'nill', 'Active', '1', '1')";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select user_id from pos_users where (username = 'sa') and (password = 'rootsguruadmin@123');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_users values ('sa' , 'rootsguruadmin@123', '1', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select dashboard_id from pos_tbl_authorities_dashboard where (role_id = '1');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_tbl_authorities_dashboard values ('True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select reports_id from pos_tbl_authorities_reports where (role_id = '1');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_tbl_authorities_reports values ('True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls1 where (role_id = '1');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_tbl_authorities_button_controls1 values ('True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True','True','True','True','True','True','True','True','True','True','True', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls2 where (role_id = '1');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_tbl_authorities_button_controls2 values ('True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True','True', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select button_control_id from pos_tbl_authorities_button_controls3 where (role_id = '1');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_tbl_authorities_button_controls3 values ('True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', 'True', '1');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select id from pos_server_config where (username = 'sa') and (password = 'rootsguruadmin@123');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_server_config values ('sa', 'rootsguruadmin@123', '.\SQLEXPRESS', 'installment_db', 'nill');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select report_id from pos_report_settings;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_report_settings values ('THE BIG FAT POS', 'Address', '0313-3879645', '*** Refer a Friend To Smoke Drop & Get 5% off. ***', 'Copyrights 2023, Powered by The Big Fat POS', '');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************


                    GetSetData.query = @"select id from pos_colors_settings;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_colors_settings values ('5', '100', '146', '5', '100', '146', '255', '255', '255', '255', '255', '255', 65, 105, 225, 95, 130, 250, 220, 90, 0, 235, 110, 10, 6, 176, 37, 50, 190, 70);";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select config_id from pos_configurations;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_configurations values ('THE BIG FAT POS', 'nill', 'nill', 'Address', 'nill', 'nill', '1', '123', '123', 'nill', 'nill');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select id from pos_general_settings;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_general_settings values ('E:\The Big Fat POS\Backup\', 'E:\The Big Fat POS\images\', 'Yes', 'Yes', 'A6', 'Disabled', 'No', 'Enabled', 'Disabled', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Yes', 'Piece', 'No', 'No', 'No', 'No', 'No', 'No', 'W_Sale - Pur', '1', '1', '0', 'Sale - W_Sale', '$', '0', 'Microsoft Print to PDF', 'No', '', 'Yes', 'Yes', 'Yes', 'No', 'No', '100', '1000', 'No', '0', '0', 'Yes', 'No', '0', 'false', 'No', 'No', 'No', '0', 'No', 'No', 'No', 'No', '0', 'No', 'No', '0');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************


                    GetSetData.query = @"select id from pos_contractPolicies;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_contractPolicies values ('Installment must be paid upto 5th of each month.', 
                                            'In case of default to pay installment and must ensure both installments with the 2nd installment.', 
                                            'Consective failure of paying regular two installments, third month lump sum amount shall pay at all.', 
                                            'Customer is liable to issue cheques equal to installments.', 
                                            'Guarantor is fully responsible to pay whole amount in case of failure, bankruptcy, lunacy, death, or any other condition of customer.',
                                            'if customer change his/her address or shift without paying the total amount Guarantor shall laible to pay the some.',
                                            'Puts copy of CNIC  and passport size photograhp is mandatory with agreement form.',
                                            'Customer ensure to collect payment receipt  and keeping safe, if any  query then only trusted receipt shall be entertained.',
                                            'Cell number must be active both customer and guarantor whenever needed company may contact them.', 
                                            'Company shall consider free consent of guarantor after selling products, guarantors any kind of excuse may be treated as lame excuse.', 
                                            'Before purchasing any electronic gadget check it carefullly.', 
                                            'No any excuse will be accepted after purchasing.',
                                            'No any electronic gadget will be return after purchasing.',
                                            'There is no warranty or guarantee of any electronic gadget.');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select capital_id from pos_capital;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_capital values ('0', '0')";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    GetSetData.query = @"select id from pos_AllCodes;";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_AllCodes values ('0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0', '0', '0')";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                    // **********************************************************************

                    InsertValuesInTableBatchNumber();
                    InsertValuesInTableCustomers();
                    InsertValuesInTableLoanHolders();
                    InsertValuesInTableCategory();
                    InsertValuesInTableCounters();
                    InsertValuesInTableShifts();
                    InsertValuesInTableBrand();
                    InsertValuesInTableColors();
                    InsertValuesInTableSubCategory();
                    InsertValuesInTablesBankBranches();
                    InsertValuesInTablesBankAccountTitles();
                    InsertValuesInTablesBankTransactionStatus();
                    InsertValuesInTablesBankTransactionType();
                    // **********************************************************************
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableBatchNumber()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                // **********************************************************************
                GetSetData.query = @"select batch_id from pos_batchNo where (title = 'nill');";
                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.fks == 0)
                {
                    GetSetData.query = @"insert into pos_batchNo values ('nill')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableCustomers()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select customer_id from pos_customers where (full_name = 'nill');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_customers values ('2021-01-21', 'nill', 'nill', '1000000001' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '1' , 'a' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '0' , '0' , '0' , 'nill' , '0' , 'Active' , '1' , '1' , '1', '0');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableCategory()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select category_id from pos_category where (title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_category values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        public static void InsertValuesInTableShifts()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select id from pos_shift where (title = 'default');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_shift values ('default')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        public static void InsertValuesInTableCounters()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select id from pos_counter where (title = 'default');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_counter values ('default', '0', '5')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableBrand()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select brand_id from pos_brand where (brand_title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_brand values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableSubCategory()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select sub_cate_id from pos_subcategory where (title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_subcategory values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                } 
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableColors()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select color_id from pos_color where (title = 'none');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_color values ('none')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTableLoanHolders()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select loanHolder_id from pos_LoanHolders where (title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_LoanHolders values ('others', 'nill', 'nill')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //public static void InsertValuesInTableCategoryBrandSubCategoryColor()
        //{
        //    error_form error = new error_form();
        //    done_form done = new done_form();
        //    form_sure_message sure = new form_sure_message();
        //    ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        //    Datalayers data = new Datalayers(webConfig.con_string);

        //    try
        //    {
        //        // **********************************************************************
        //        GetSetData.query = @"select brand_id from pos_brand where (brand_title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_brand values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //        // **********************************************************************

        //        GetSetData.query = @"select category_id from pos_category where (title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_category values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //        // **********************************************************************

        //        GetSetData.query = @"select sub_cate_id from pos_subcategory where (title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_subcategory values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }  // **********************************************************************

        //        GetSetData.query = @"select color_id from pos_color where (title = 'none');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_color values ('none')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //public static void InsertValuesInTableBatchNoAndCustomers()
        //{
        //    error_form error = new error_form();
        //    done_form done = new done_form();
        //    form_sure_message sure = new form_sure_message();
        //    ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        //    Datalayers data = new Datalayers(webConfig.con_string);

        //    try
        //    {
        //        // **********************************************************************
        //        GetSetData.query = @"select batch_id from pos_batchNo where (title = 'nill');";
        //        GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.fks == 0)
        //        {
        //            GetSetData.query = @"insert into pos_batchNo values ('nill')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }

        //        GetSetData.query = @"select customer_id from pos_customers where (full_name = 'nill') and (cus_code = '1000000001');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_customers values ('2021-01-21', 'nill', 'nill', '1000000001' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '1' , 'a' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '0' , '0' , '0' , 'nill' , 'Active' , '1' , '1' , '1');";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        //public static void InsertValuesInTablesBankingDetails()
        //{
        //    error_form error = new error_form();
        //    done_form done = new done_form();
        //    form_sure_message sure = new form_sure_message();
        //    ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        //    Datalayers data = new Datalayers(webConfig.con_string);

        //    try
        //    {
        //        // **********************************************************************
        //        GetSetData.query = @"select branch_id from pos_bank_branch where (branch_title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_bank_branch values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //        // **********************************************************************

        //        GetSetData.query = @"select account_id from pos_bank_account where (account_title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_bank_account values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //        // **********************************************************************

        //        GetSetData.query = @"select status_id from pos_transaction_status where (status_title = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_transaction_status values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }  // **********************************************************************

        //        GetSetData.query = @"select transaction_id from pos_transaction_type where (transaction_type = 'others');";
        //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

        //        if (GetSetData.Ids == 0)
        //        {
        //            GetSetData.query = @"insert into pos_transaction_type values ('others')";
        //            data.insertUpdateCreateOrDelete(GetSetData.query);
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}

        public static void InsertValuesInTablesBankBranches()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                // **********************************************************************
                GetSetData.query = @"select branch_id from pos_bank_branch where (branch_title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_bank_branch values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTablesBankAccountTitles()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select account_id from pos_bank_account where (account_title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_bank_account values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTablesBankTransactionStatus()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select status_id from pos_transaction_status where (status_title = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_transaction_status values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //******************************************************************************
                GetSetData.query = @"select status_id from pos_transaction_status where (status_title = 'Deposite');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_transaction_status values ('Deposite')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //******************************************************************************
                GetSetData.query = @"select status_id from pos_transaction_status where (status_title = 'Withdraw');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_transaction_status values ('Withdraw')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                } 
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public static void InsertValuesInTablesBankTransactionType()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            form_sure_message sure = new form_sure_message();
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);

            try
            {
                GetSetData.query = @"select transaction_id from pos_transaction_type where (transaction_type = 'others');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_transaction_type values ('others')";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
