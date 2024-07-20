using System;
using System.Drawing;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using System.Drawing.Printing;
using System.IO;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Customers_info.forms
{
    public partial class add_customer : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }


        public add_customer()
        {
            InitializeComponent();
            //setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static bool saveEnable = false;

        //private void setFormColorsDynamically()
        //{
        //    //try
        //    //{
        //    //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
        //    //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
        //    //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

        //    //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
        //    //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
        //    //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

        //    //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
        //    //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
        //    //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

        //    //    //****************************************************************

        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);

        //    //    //****************************************************************

        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button1);
        //    //}
        //    //catch (Exception es)
        //    //{
        //    //    MessageBox.Show(es.Message);
        //    //}
        //}
        private void system_user_permissions()
        {
            try
            {
                GetSetData.Data = GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customers_save", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                GetSetData.query = GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customers_update", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.query);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.query) == false)
                {
                    pnl_add_update.Visible = false;
                }

                pnl_barcode.Visible = bool.Parse(GetSetData.ProcedureGetDashboardAuthorities("ProcedureGetButtonAuthorities1", "customers_exit", role_id.ToString()));
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddCustomerFormTextBoxes()
        {
            try
            {
                txtBatchNo.Text = TextData.batchNo;
                txt_full_name.Text = TextData.full_name;
                txtFatherName.Text = TextData.fatherName;
                txt_cus_code.Text = TextData.cus_code;
                txt_cnic.Text = TextData.cnic;
                txt_country.Text = TextData.country;
                city_text.Text = TextData.province;
                txt_address1.Text = TextData.address1;
                txt_mobile_no.Text = TextData.phone1;
                txt_telephone_no.Text = TextData.phone2;
                txt_bank_name.Text = TextData.bank_name;
                txt_bank_account.Text = TextData.bank_account;
                txtPoints.Text = TextData.points;
                txt_credits.Text = TextData.credit_limit.ToString();
                txt_status.Text = TextData.status;

                GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.full_name_key.ToString() + "') and (cus_code = '" + TextData.cus_codekey.ToString() + "');";
                string customerId = data.SearchStringValuesFromDb(GetSetData.query);
                

                date_reg_text.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "date", "pos_customers", "customer_id", customerId);
                txt_post_code.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "post_code", "pos_customers", "customer_id", customerId);
                txt_zip_code.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","zip_code", "pos_customers", "customer_id", customerId);
                txt_house_no.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","house_no", "pos_customers", "customer_id", customerId);
                txt_address2.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","address2", "pos_customers", "customer_id", customerId);
                txt_email_address.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","email", "pos_customers", "customer_id", customerId);
                txt_remarks.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","remarks", "pos_customers", "customer_id", customerId);
                txt_opening_blnc.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","lastCredits", "pos_customer_lastCredits", "customer_id", customerId);
                txt_discount.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","discount", "pos_customers", "customer_id", customerId);
                txtAge.Text = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "age", "pos_customers", "customer_id", customerId);
              

                GetSetData.query = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");
                GetSetData.Data = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","image_path", "pos_customers", "customer_id",customerId);

                if (GetSetData.Data != "nill" && GetSetData.Data != "")
                {
                    img_pic_box.Image = Image.FromFile(GetSetData.query + GetSetData.Data);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                FormNamelabel.Text = "Update Customer Detail";
                fillAddCustomerFormTextBoxes();
                txt_full_name.Select();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Create New Customer";
                txtBatchNo.Focus();
            }
        }

        private void refresh()
        {
            date_reg_text.Text = DateTime.Now.ToLongDateString();
            txt_full_name.Text = "";
            txt_cus_code.Text = "";
            txt_cnic.Text = "";
            txtBatchNo.Text = "";
            txt_country.Text = "--Select--";
            city_text.Text = "--Select--";
            txt_mobile_no.Text = "";
            txt_telephone_no.Text = "";
            txt_email_address.Text = "";
            txt_remarks.Text = "";
            txt_status.Text = "Active";
            txt_opening_blnc.Text = "0";
            txt_post_code.Text = "";
            txt_zip_code.Text = "";
            txt_house_no.Text = "";
            txt_address1.Text = "";
            txt_address2.Text = "";
            txt_bank_account.Text = "";
            txt_bank_name.Text = "";
            txt_discount.Text = "0";
            txt_credits.Text = "0";
            txtPoints.Text = "0";
            img_pic_box.Image = null;

            txt_full_name.Select();
        }

        private void add_customer_Load(object sender, EventArgs e)
        {
            try
            {
                date_reg_text.Text = DateTime.Now.ToLongDateString();
                //ClassDefaultValuesSetInDB.InsertValuesInTableCustomers();
                system_user_permissions();
                enableSaveButton();

                txt_full_name.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void auto_generate_code(string condition)
        {
            TextData.cus_code = "";

            try
            {
                GetSetData.query = @"SELECT top 1 customerCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set customerCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }


                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.Data = "100000000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.Data = "10000000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.Data = "1000000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.Data = "100000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.Data = "10000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.Data = "1000";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "100";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "10";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "1";
                    TextData.cus_code = GetSetData.Data + GetSetData.Ids.ToString();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_upload_image()
        {
            TextData.image_path = "";

            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Images only. |*.jpg; *.jpeg; *.png; *.gif;";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    TextData.image_path = open.FileName;
                    img_pic_box.Image = new Bitmap(open.FileName);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private string fun_saved_image()
        {
            TextData.saved_image_path = "";
            try
            {
                if (img_pic_box.Image != null && TextData.image_path != "" && TextData.image_path != null)
                {
                    GetSetData.Data = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");

                    GetSetData.query = GetSetData.Data + Path.GetFileName(TextData.image_path); // my directory
                    TextData.saved_image_path = GetSetData.Data;

                    if ((TextData.image_path + Path.GetFileName(TextData.image_path)) != GetSetData.query)
                    {
                        File.Copy(TextData.image_path, Path.Combine(TextData.saved_image_path, Path.GetFileName(TextData.image_path)), true);
                    }

                    TextData.saved_image_path = Path.GetFileName(TextData.image_path);
                }

                return TextData.saved_image_path;
            }
            catch (Exception es)
            {
                error.errorMessage("'" + Path.GetFileName(TextData.image_path) + "' is already exist! \nPlease change file name");
                error.ShowDialog();
                return TextData.saved_image_path;
            }
        }

        private void upload_logo_Click(object sender, EventArgs e)
        {
            //this.Opacity = .850;
            fun_upload_image();
            //this.Opacity = .999;
        }

        private void LoadValuesInVariables()
        {
            try
            {
                TextData.batchNo = txtBatchNo.Text;
                TextData.dates = date_reg_text.Text;
                TextData.full_name = txt_full_name.Text;
                TextData.fatherName = txtFatherName.Text;
                txt_cus_code.Text = TextData.cus_code;
                TextData.cnic = txt_cnic.Text;
                TextData.post_code = txt_post_code.Text;
                TextData.zip_code = txt_zip_code.Text;
                TextData.country = txt_country.Text;
                TextData.house_no = txt_house_no.Text;
                TextData.province = city_text.Text;
                TextData.address1 = txt_address1.Text;
                TextData.address2 = txt_address2.Text;
                TextData.phone1 = txt_mobile_no.Text;
                TextData.phone2 = txt_telephone_no.Text;
                TextData.email = txt_email_address.Text;
                TextData.bank_name = txt_bank_name.Text;
                TextData.bank_account = txt_bank_account.Text;
                TextData.status = txt_status.Text;
                TextData.remarks = txt_remarks.Text;
                TextData.opening_balance = double.Parse(txt_opening_blnc.Text);
                TextData.credit_limit = double.Parse(txt_credits.Text);
                TextData.discount = double.Parse(txt_discount.Text);
                TextData.points = txtPoints.Text;
               
                TextData.saved_image_path = fun_saved_image();

                if (img_pic_box.Image == null)
                {
                    TextData.saved_image_path = "nill";
                }

                if (txtBatchNo.Text == "")
                {
                    TextData.batchNo = "nill";
                }

                if (txt_post_code.Text == "")
                {
                    TextData.post_code = "nill";
                }

                if (txt_zip_code.Text == "")
                {
                    TextData.zip_code = "nill";
                }

                if (txt_house_no.Text == "")
                {
                    TextData.house_no = "nill";
                }

                if (txt_address2.Text == "")
                {
                    TextData.address2 = "nill";
                }

                if (txt_telephone_no.Text == "")
                {
                    TextData.phone2 = "nill";
                }

                if (txt_email_address.Text == "")
                {
                    TextData.email = "nill";
                }

                if (txt_country.Text == "--Select--" || txt_country.Text == "")
                {
                    TextData.country = "nill";
                }

                if (txt_cnic.Text == "")
                {
                    TextData.cnic = "nill";
                }

                if (city_text.Text == "--Select--" || city_text.Text == "")
                {
                    TextData.province = "nill";
                }

                if (txt_bank_name.Text == "")
                {
                    TextData.bank_name = "nill";
                }

                if (txt_bank_account.Text == "")
                {
                    TextData.bank_account = "nill";
                }

                if (txt_remarks.Text == "")
                {
                    TextData.remarks = "nill";
                } 
                
                if (txtFatherName.Text == "")
                {
                    TextData.fatherName = "nill";
                }
                
                if (txt_address1.Text == "")
                {
                    TextData.address1 = "nill";
                }

                if (txtAge.Text == "")
                {
                    TextData.age = "0";
                }
                else
                {
                    TextData.age = txtAge.Text;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool InsertCustomerDetails()
        {
            try
            {
                LoadValuesInVariables();
                GetSetData.Data = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "mobile_no", "pos_customers", "mobile_no", TextData.phone1);

                if (txt_full_name.Text != "")
                {
                    if (txt_mobile_no.Text != "")
                    {
                        if ((GetSetData.Data == "") && (GetSetData.Data != txt_mobile_no.Text))
                        {
                            if (txtPoints.Text != "")
                            {
                                if (txt_discount.Text != "")
                                {
                                    if (txt_credits.Text != "")
                                    {
                                        if (txt_opening_blnc.Text != "")
                                        {
                                            //int ageLimit = data.UserPermissionsIds("customerAgeLimit", "pos_general_settings");

                                            //if (Convert.ToInt32(txtAge.Text) >= ageLimit)
                                            //{
                                            auto_generate_code(""); // generating Customer Code

                                            GetSetData.Ids = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "country_id", "pos_country", "title", TextData.country);
                                            GetSetData.fks = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "city_id", "pos_city", "title", TextData.province);
                                            int batchNo_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "batch_id", "pos_batchNo", "title", TextData.batchNo);

                                            GetSetData.query = @"insert into pos_customers values ('" + TextData.dates.ToString() + "' , '" + TextData.full_name.ToString() + "' , '" + TextData.fatherName.ToString() + "' , '" + TextData.cus_code.ToString() + "' , '" + TextData.post_code.ToString() + "' , '" + TextData.zip_code.ToString() + "' , '" + TextData.cnic.ToString() + "' , '" + TextData.house_no.ToString() + "' , '" + TextData.phone2.ToString() + "' , '" + TextData.phone1.ToString() + "' , '" + TextData.address1.ToString() + "' , '" + TextData.address2.ToString() + "' , '" + TextData.email.ToString() + "' , '" + TextData.bank_name.ToString() + "' , '" + TextData.bank_account.ToString() + "' , '" + TextData.saved_image_path.ToString() + "' , '" + TextData.discount.ToString() + "' , '" + TextData.credit_limit.ToString() + "' , '" + TextData.opening_balance.ToString() + "' , '" + TextData.remarks.ToString() + "' , '" + TextData.points + "' , '" + TextData.status.ToString() + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "' , '" + batchNo_id_db.ToString() + "' , '" + TextData.age + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);

                                            GetSetData.query = @"select customer_id from pos_customers where full_name = '" + TextData.full_name.ToString() + "' and cus_code = '" + TextData.cus_code.ToString() + "';";
                                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                            GetSetData.query = @"insert into pos_customer_lastCredits (lastCredits, due_days, customer_id) values ('" + TextData.opening_balance.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                                            data.insertUpdateCreateOrDelete(GetSetData.query);

                                            #region
                                            //done.DoneMessage("Successfully Saved!");
                                            //done.ShowDialog();


                                            //if (txtAge.Text == "" || txtAge.Text == "0")
                                            //{
                                            //    if (ageLimit > 0)
                                            //    {
                                            //        if (GetSetData.Ids != 0)
                                            //        {
                                            //            if (Screen.AllScreens.Length > 1)
                                            //            {
                                            //                if (!IsFormOpen(typeof(formCustomerAge)))
                                            //                {
                                            //                    formCustomerAge.customerId = GetSetData.Ids.ToString();

                                            //                    formCustomerAge secondaryForm = new formCustomerAge();
                                            //                    Screen secondaryScreen = Screen.AllScreens[1];
                                            //                    secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                                            //                    secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                                            //                    secondaryForm.WindowState = FormWindowState.Maximized;
                                            //                    secondaryForm.Show();
                                            //                }
                                            //            }
                                            //        }
                                            //    }
                                            //}
                                            #endregion

                                            return true;
                                            //}
                                            //else
                                            //{
                                            //    error.errorMessage("This customer is under age. Please verify the customer age first.!");
                                            //    error.ShowDialog();
                                            //}

                                        }
                                        else
                                        {
                                            error.errorMessage("Field opening balance is empty!");
                                            error.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        error.errorMessage("Field credit limit is empty!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Field discount is empty");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Field points is empty");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("'" + TextData.phone1.ToString() + "' is already exist!");
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
                    error.errorMessage("Please enter customer full name!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }
        
        private void savebutton_Click(object sender, EventArgs e)
        {
            TextData.saved_image_path = fun_saved_image();

            if (img_pic_box.Image == null)
            {
                TextData.saved_image_path = "nill";
            }

            GetSetData.Data = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");

            if (TextData.saved_image_path != "" && TextData.saved_image_path != GetSetData.Data && TextData.saved_image_path != null)
            {
                if (InsertCustomerDetails())
                {
                    //done_form _obj = new done_form();
                    //Screen primaryScreen = Screen.AllScreens[0];
                    //_obj.StartPosition = FormStartPosition.CenterScreen;
                    //_obj.Location = primaryScreen.WorkingArea.Location;
                    //_obj.WindowState = FormWindowState.Normal;
                    //_obj.DoneMessage("Successfully Saved!");
                    //_obj.ShowDialog();


                    //Screen primaryScreen = Screen.AllScreens[0];
                    //done.StartPosition = FormStartPosition.CenterScreen;
                    //done.Location = primaryScreen.WorkingArea.Location;

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();

                    refresh();
                }
            }
        }
        //private bool IsFormOpen(Type formType)
        //{
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form.GetType() == formType)
        //            return true;
        //    }
        //    return false;
        //}

        private bool Update_Customers_records_db()
        {
            try
            {
                LoadValuesInVariables();
                GetSetData.Data = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");
                GetSetData.Ids = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "customer_id", "pos_customers", "full_name", TextData.full_name_key);

                if (TextData.saved_image_path == GetSetData.Data || TextData.saved_image_path == null || TextData.saved_image_path == "")
                {
                    TextData.saved_image_path = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues","image_path", "pos_customers", "customer_id", GetSetData.Ids.ToString());

                    if (TextData.saved_image_path == "")
                    {
                        TextData.saved_image_path = "nill";
                    }
                }

                if (txt_full_name.Text != "")
                {
                    if (txt_mobile_no.Text != "")
                    {
                        //int ageLimit = Convert.ToInt32(GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "customerAgeLimit"));

                        //if (Convert.ToInt32(txtAge.Text) >= ageLimit)
                        //{
                        GetSetData.Ids = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "country_id", "pos_country", "title", TextData.countrykey);
                        GetSetData.fks = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "city_id", "pos_city", "title", TextData.provincekey);
                        int batch_id_db = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "batch_id", "pos_batchNo", "title", TextData.batchNo);

                        GetSetData.query = @"update pos_customers set date = '" + TextData.dates.ToString() + "' , full_name = '" + TextData.full_name.ToString() + "' , mobile_no = '" + TextData.phone1.ToString() + "' , telephone_no = '" + TextData.phone2.ToString() + "' , address1 = '" + TextData.address1.ToString() + "' ,  image_path = '" + TextData.saved_image_path.ToString() + "' ,  discount = '" + TextData.discount.ToString() + "' , credit_limit = '" + TextData.credit_limit.ToString() + "' , remarks = '" + TextData.remarks.ToString() + "' , points = '" + TextData.points + "' , status = '" + TextData.status.ToString() + "', age = '" + TextData.age + "' where (full_name = '" + TextData.full_name_key.ToString() + "') and (cus_code = '" + TextData.cus_codekey.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        GetSetData.query = @"select customer_id from pos_customers where full_name = '" + TextData.full_name.ToString() + "' and cus_code = '" + TextData.cus_code.ToString() + "';";
                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        int is_customer_lastCredits_exist = GetSetData.ProcedureGetIntegerValues("ProcedureGetIntergerValues", "last_credit_id", "pos_customer_lastCredits", "customer_id", GetSetData.Ids.ToString());

                        if (is_customer_lastCredits_exist == 0)
                        {
                            GetSetData.query = @"insert into pos_customer_lastCredits (lastCredits, due_days, customer_id) values ('" + TextData.opening_balance.ToString() + "' , '" + TextData.dates.ToString() + "' , '" + GetSetData.Ids.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                        else
                        {
                            GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.opening_balance.ToString() + "' where (customer_id = '" + GetSetData.Ids.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }

                        return true;
                        //}
                        //else
                        //{
                        //    error.errorMessage("This customer is under age. Please verify the customer age first.!");
                        //    error.ShowDialog();
                        //}
                        //}
                        //else
                        //{
                        //    error.errorMessage("Please enter customer age!");
                        //    error.ShowDialog();
                        //}
                    }
                    else
                    {
                        error.errorMessage("Please enter the mobile Number!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter customer full name!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                //MessageBox.Show("'" + TextData.saved_image_path + "' is already exist! \nPlease change file name");
                error.errorMessage("Please Fill all the empty fields!");
                error.ShowDialog();
                return false;
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (Update_Customers_records_db())
            {
                //Screen secondaryScreen = Screen.AllScreens[0];
                //done.StartPosition = FormStartPosition.CenterScreen;
                //done.Location = secondaryScreen.WorkingArea.Location;

                done.DoneMessage("Successfully Updated!");
                done.ShowDialog();
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Customer Form", "Exit...", role_id);
            refresh();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void cnic_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void balance_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_opening_blnc.Text, e);
        }

        private void debits_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_discount.Text, e);
        }

        private void txt_credits_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_credits.Text, e);
        }

        private void btn_country_Click(object sender, EventArgs e)
        {
            using (add_country add_customer = new add_country())
            {
                add_customer.ShowDialog();
            }
        }

        private void btn_city_Click(object sender, EventArgs e)
        {
            using (add_City add_customer = new add_City())
            {
                add_customer.ShowDialog();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
        }

        private void generate_barcode()
        {
            try
            {
                TextData.cus_code = "";
                //prod_code_text.Text = "";

                if (txt_cus_code.Text != "")
                {
                    //prod_code_text.Text = auto_generate_code();
                    TextData.cus_code = txt_cus_code.Text;
                }

                if (TextData.cus_code != "")
                {
                    Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                    img_barcode.Image = brcode.Draw(TextData.cus_code, 80);
                    //lbl_barcode.Text = TextData.cus_code;
                }
                else
                {
                    error.errorMessage("Please save customer's records first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_print_barcode()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();

            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            PointF point = new PointF(23f, 60f);
            Font font = new System.Drawing.Font("Verdana", 6, FontStyle.Bold);
            SolidBrush black = new SolidBrush(Color.Black);
            e.Graphics.DrawString(txt_full_name.Text, font, Brushes.Black, 2, 2);
            Bitmap bm = new Bitmap(img_barcode.Width, img_barcode.Height);
            img_barcode.DrawToBitmap(bm, new Rectangle(0, 0, img_barcode.Width, img_barcode.Height));
            e.Graphics.DrawImage(bm, 0, 18);
            e.Graphics.DrawString(txt_cus_code.Text, font, Brushes.Black, 2, 82);
            bm.Dispose();
            GetSetData.SaveLogHistoryDetails("Add New Customer Form", "Print [" + txt_full_name.Text + "  " + txt_cus_code.Text + "] barcode", role_id);
        }

        private void btn_print_barcode_Click(object sender, EventArgs e)
        {
            this.Opacity = .850;
            generate_barcode();

            if (img_barcode.Image != null)
            {
                fun_print_barcode();
            }
            this.Opacity = .999;
        }

        private void btnBatchNo_Click(object sender, EventArgs e)
        {
            using (formNewBatchNo add_customer = new formNewBatchNo())
            {
                add_customer.ShowDialog();
            }
        }

        private void txtBatchNo_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtBatchNo, "fillComboBoxBatchNumbers", "title");
        }

        private void txt_country_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txt_country, "fillComboBoxCountryNames", "title");
        }

        private void city_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(city_text, "fillComboBoxCityNames", "title");
        }

        private void add_customer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txt_email_address_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
              data.NumericValuesOnly(txt_discount.Text, e);
        }
    }
}
