using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using System.IO;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.controllers;

namespace Purchase_info.forms
{
    public partial class form_purchase_from : Form
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

        public form_purchase_from()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static bool saveEnable = false;

        private void setFormColorsDynamically()
        {
            //try
            //{
            //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
            //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
            //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

            //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
            //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
            //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

            //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
            //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
            //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

            //    //****************************************************************

            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button1);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }
        private void system_user_permissions()
        {
            try
            {
                add_country.role_id = role_id;
                add_City.role_id = role_id;
                GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("suppliers_new", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Permission = data.UserPermissions("suppliers_update", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.Permission);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.Permission) == false)
                {
                    pnl_add_update.Visible = false;
                }

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("suppliers_exit", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_exit.Visible = bool.Parse(GetSetData.Data);
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
                txt_full_name.Text = TextData.full_name;
                txt_cus_code.Text = TextData.cus_code;
                txt_country.Text = TextData.country;
                city_text.Text = TextData.city;
                txt_address1.Text = TextData.address;
                txt_mobile_no.Text = TextData.phone1;
                txt_telephone_no.Text = TextData.phone2;
                txt_contact_person.Text = TextData.contact_person;
                txt_bank_name.Text = TextData.bank_name;
                txt_bank_account.Text = TextData.bank_account;
                txt_status.Text = TextData.status;
                //img_pic_box.Image = Bitmap.FromFile(TextData.saved_image_path);

                GetSetData.query = @"select supplier_id from pos_suppliers where full_name = '" + TextData.full_name_key.ToString() + "' and code = '" + TextData.cus_codekey.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                date_reg_text.Text = data.UserPermissions("date", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());
                txt_email_address.Text = data.UserPermissions("email", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());
                txt_website.Text = data.UserPermissions("website", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());
                txt_remarks.Text = data.UserPermissions("remarks", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());
                txt_balance.Text = data.UserPermissions("previous_payables", "pos_supplier_payables", "supplier_id", GetSetData.Ids.ToString());
                TextData.image_path = TextData.dates = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.saved_image_path = TextData.dates = data.UserPermissions("image_path", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());

                if (TextData.saved_image_path != "nill" && TextData.saved_image_path != "")
                {
                    img_pic_box.Image = Image.FromFile(TextData.image_path + TextData.saved_image_path);
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
                FormNamelabel.Text = "Update Vendor";
                fillAddCustomerFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                txt_cus_code.Text = auto_generate_code("show");
                FormNamelabel.Text = "Create New Vendor";
            }
        }
      
        private void refresh()
        {
            date_reg_text.Text = DateTime.Now.ToLongDateString();
            txt_full_name.Text = "";
            txt_cus_code.Text = "";
            txt_country.Text = "--Select--";
            city_text.Text = "--Select--";
            txt_mobile_no.Text = "";
            txt_telephone_no.Text = "";
            txt_email_address.Text = "";
            txt_contact_person.Text = "";
            txt_remarks.Text = "";
            txt_status.Text = "Active";
            txt_balance.Text = "0";
            txt_address1.Text = "";
            txt_bank_account.Text = "";
            txt_bank_name.Text = "";
            txt_website.Text = "";
            img_pic_box.Image = null;
            txt_full_name.TabIndex = 0;
            txt_full_name.Select();
        }

        public static string auto_generate_code(string condition)
        {
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            error_form error = new error_form();
            done_form done = new done_form();

            TextData.cus_code = "";

            try
            {
                GetSetData.query = @"SELECT top 1 supplierCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set supplierCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                //**********************************************************************************************


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

                return TextData.cus_code;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return TextData.cus_code;
            }
        }

        private void savebutton_Click_1(object sender, EventArgs e)
        {
            TextData.dates = date_reg_text.Text;
            TextData.full_name = txt_full_name.Text;
            txt_cus_code.Text = TextData.cus_code;
            TextData.country = txt_country.Text;
            TextData.city = city_text.Text;
            TextData.address = txt_address1.Text;
            TextData.phone1 = txt_mobile_no.Text;
            TextData.phone2 = txt_telephone_no.Text;
            TextData.email = txt_email_address.Text;
            TextData.website = txt_website.Text;
            TextData.bank_name = txt_bank_name.Text;
            TextData.bank_account = txt_bank_account.Text;
            TextData.contact_person = txt_contact_person.Text;
            TextData.status = txt_status.Text;
            TextData.remarks = txt_remarks.Text;
            TextData.last_balance = 0;
            TextData.saved_image_path = fun_saved_image();

            if (img_pic_box.Image == null)
            {
                TextData.saved_image_path = "nill";
            }

            if (txt_balance.Text != "")
            {
                 TextData.last_balance = double.Parse(txt_balance.Text);
            }

            GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

            if (TextData.saved_image_path != "" && TextData.saved_image_path != GetSetData.Data && TextData.saved_image_path != null)
            {
                GetSetData.checkBoxSelected = Button_controls.save_Customers_records_db();

                if (GetSetData.checkBoxSelected == true)
                {
                    //GetSetData.SaveLogHistoryDetails("Company Details Form", "Saving company [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);
                    this.Opacity = .850;
                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                    this.Opacity = .999;
                    refresh();
                }
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Company Details Form", "Exit...", role_id);
            this.Close();
        }

        private void form_purchase_from_Load(object sender, EventArgs e)
        {
            try
            {
                //system_user_permissions();
                date_reg_text.Text = DateTime.Now.ToLongDateString();
                system_user_permissions();
                enableSaveButton();
                //refresh();
                txt_full_name.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            
        }

        private void Update_Customers_records_db()
        {
            try
            {
                TextData.dates = date_reg_text.Text;
                TextData.full_name = txt_full_name.Text;
                TextData.cus_code = txt_cus_code.Text;
                TextData.country = txt_country.Text;
                TextData.city = city_text.Text;
                TextData.address = txt_address1.Text;
                TextData.phone1 = txt_mobile_no.Text;
                TextData.phone2 = txt_telephone_no.Text;
                TextData.email = txt_email_address.Text;
                TextData.website = txt_website.Text;
                TextData.bank_name = txt_bank_name.Text;
                TextData.bank_account = txt_bank_account.Text;
                TextData.contact_person = txt_contact_person.Text;
                TextData.status = txt_status.Text;
                TextData.remarks = txt_remarks.Text;
                TextData.last_balance = 0;
                TextData.saved_image_path = fun_saved_image();

                if (img_pic_box.Image == null)
                {
                    TextData.saved_image_path = "nill";
                }

                if (txt_balance.Text != "")
                {
                    TextData.last_balance = double.Parse(txt_balance.Text);
                }

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                GetSetData.query = @"select supplier_id from pos_suppliers where full_name = '" + TextData.full_name_key.ToString() + "' and code = '" + TextData.cus_codekey.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                if (TextData.saved_image_path == GetSetData.Data || TextData.saved_image_path == null || TextData.saved_image_path == "")
                {
                    TextData.saved_image_path = data.UserPermissions("image_path", "pos_suppliers", "supplier_id", GetSetData.Ids.ToString());

                    if (TextData.saved_image_path == "")
                    {
                        TextData.saved_image_path = "nill";
                    }
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
                        int country_id_db = data.UserPermissionsIds("country_id", "pos_country", "title", TextData.countrykey);
                        int city_id_db = data.UserPermissionsIds("city_id", "pos_city", "title", TextData.provincekey);

                        GetSetData.query = @"update pos_suppliers set date = '" + TextData.dates.ToString() + "' , full_name = '" + TextData.full_name.ToString() + "' , telephone_no = '" + TextData.phone2.ToString() + "' , mobile_no = '" + TextData.phone1.ToString() + "' , address = '" + TextData.address.ToString() + "' , email = '" + TextData.email.ToString() + "' , website = '" + TextData.website.ToString() + "' , bank_name = '" + TextData.bank_name.ToString() + "' , bank_account = '" + TextData.bank_account.ToString() + "' ,  image_path = '" + TextData.saved_image_path.ToString() + "' , remarks = '" + TextData.remarks.ToString() + "' , status = '" + TextData.status.ToString() + "' , country_id = '" + country_id_db.ToString() + "' , city_id = '" + city_id_db.ToString() + "' where full_name = '" + TextData.full_name_key.ToString() + "' and code = '" + TextData.cus_codekey.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        GetSetData.query = @"select supplier_id from pos_suppliers where full_name = '" + TextData.full_name.ToString() + "' and code = '" + TextData.cus_code.ToString() + "';";
                        int cus_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                        //MessageBox.Show(cus_id_db.ToString());


                        GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.last_balance.ToString() + "' where supplier_id = '" + cus_id_db.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        GetSetData.SaveLogHistoryDetails("Company Details Form", "Updating company [" + TextData.full_name + "  " + TextData.cus_code + "] details", role_id);

                        this.Opacity = .850;
                        done.DoneMessage("Successfully Updated!");
                        done.ShowDialog();
                        this.Opacity = .999;
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
            catch (Exception es)
            {
                //MessageBox.Show("'" + TextData.saved_image_path + "' is already exist! \nPlease change file name");
                error.errorMessage("Please Fill all the empty fields!");
                error.ShowDialog();
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            Update_Customers_records_db();
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

        private void last_balance_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_balance.Text, e);
        }

        private void txt_telephone_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
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
                    GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                    GetSetData.Permission = GetSetData.Data + Path.GetFileName(TextData.image_path);

                    TextData.saved_image_path = GetSetData.Data;
                    if (TextData.image_path != GetSetData.Permission)
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

        private void txt_cus_code_Click(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
        }

        private void txt_country_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txt_country, "fillComboBoxCountryNames", "title");
        }

        private void city_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(city_text, "fillComboBoxCityNames", "title");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void form_purchase_from_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txt_remarks_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
