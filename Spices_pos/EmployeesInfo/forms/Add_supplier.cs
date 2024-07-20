using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using System.IO;
using System.Drawing.Printing;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.EmployeesInfo.controllers;

namespace Supplier_Chain_info.forms
{
    public partial class Add_supplier : Form
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

        public Add_supplier()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
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
                GetSetData.Data = data.UserPermissions("employees_save", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                savebutton.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Permission = data.UserPermissions("employees_update", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                update_button.Visible = bool.Parse(GetSetData.Permission);

                if (bool.Parse(GetSetData.Data) == false && bool.Parse(GetSetData.Permission) == false)
                {
                    pnl_add_update.Visible = false;
                }

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("employees_exit", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_barcode.Visible = bool.Parse(GetSetData.Data);

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
                city_text.Text = TextData.province;
                txt_address1.Text = TextData.address1;
                txt_mobile_no.Text = TextData.phone1;
                txt_telephone_no.Text = TextData.phone2;
                txt_salary.Text = TextData.salary.ToString();
                txt_status.Text = TextData.status.ToString();
                //img_pic_box.Image = Bitmap.FromFile(TextData.saved_image_path);

                GetSetData.query = @"select employee_id from pos_employees where full_name = '" + TextData.full_name_key.ToString() + "' and emp_code = '" + TextData.cus_codekey.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                date_reg_text.Text = data.UserPermissions("date", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_post_code.Text = data.UserPermissions("post_code", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_zip_code.Text = data.UserPermissions("zip_code", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_house_no.Text = data.UserPermissions("house_no", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_address2.Text = data.UserPermissions("address2", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_email_address.Text = data.UserPermissions("email", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_remarks.Text = data.UserPermissions("remarks", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_daily_wages.Text = data.UserPermissions("daily_wages", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_commission.Text = data.UserPermissions("commission", "pos_employees", "employee_id", GetSetData.Ids.ToString());
                txt_cnic.Text = data.UserPermissions("cnic", "pos_employees", "employee_id", GetSetData.Ids.ToString());

                TextData.image_path = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.saved_image_path = data.UserPermissions("image_path", "pos_employees", "employee_id", GetSetData.Ids.ToString());

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
                FormNamelabel.Text = "Update Employee";
                fillAddCustomerFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                txt_cus_code.Text = auto_generate_code("show");
                FormNamelabel.Text = "Create New Employee";
            }
        }

        private void refresh()
        {
            date_reg_text.Text = DateTime.Now.ToLongDateString();
            txt_full_name.Text = "";
            txt_cus_code.Text = "";
            txt_cnic.Text = "";
            txt_country.Text = "--Select--";
            city_text.Text = "--Select--";
            txt_mobile_no.Text = "";
            txt_telephone_no.Text = "";
            txt_email_address.Text = "";
            txt_remarks.Text = "";
            txt_status.Text = "Active";
            txt_salary.Text = "0";
            txt_post_code.Text = "";
            txt_zip_code.Text = "";
            txt_house_no.Text = "";
            txt_address1.Text = "";
            txt_address2.Text = "";
            txt_daily_wages.Text = "0";
            txt_commission.Text = "0";
            img_pic_box.Image = null;
            txt_full_name.TabIndex = 0;
            
            txt_full_name.Select();
        }

        private void Add_supplier_Load(object sender, EventArgs e)
        {
            try
            {
                date_reg_text.Text = DateTime.Now.ToLongDateString();
                system_user_permissions();
                enableSaveButton();

                txt_full_name.Select();
                //refresh();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Employee Form", "Exit...", role_id);
            refresh();
            this.Close();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            TextData.dates = date_reg_text.Text;
            TextData.full_name = txt_full_name.Text;
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
            TextData.status = txt_status.Text;
            TextData.remarks = txt_remarks.Text;
            TextData.salary = double.Parse(txt_salary.Text);
            TextData.daily_wages = double.Parse(txt_daily_wages.Text);
            TextData.commission = double.Parse(txt_commission.Text);
            TextData.saved_image_path = fun_saved_image();

            if (img_pic_box.Image == null)
            {
                TextData.saved_image_path = "nill";
            }

            GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

            if (TextData.saved_image_path != "" && TextData.saved_image_path != GetSetData.Data && TextData.saved_image_path != null)
            {
                bool chose = Button_controls.save_Supplier_records_db();

                if (chose == true)
                {
                    //GetSetData.SaveLogHistoryDetails("Add New Employee Form", "Saving [" + TextData.full_name + "   " + TextData.cus_code + "] details", role_id);
                    this.Opacity = .850;
                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                    this.Opacity = .999;
                    refresh();
                }
            }
        }

        private void Update_Customers_records_db()
        {
            try
            {
                TextData.dates = date_reg_text.Text;
                TextData.full_name = txt_full_name.Text;
                TextData.cus_code = txt_cus_code.Text;
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
                TextData.remarks = txt_remarks.Text;
                TextData.status = txt_status.Text;
                TextData.salary = double.Parse(txt_salary.Text);
                TextData.daily_wages = double.Parse(txt_daily_wages.Text);
                TextData.commission = double.Parse(txt_commission.Text);
                TextData.saved_image_path = fun_saved_image();

                if (img_pic_box.Image == null)
                {
                    TextData.saved_image_path = "nill";
                }

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                GetSetData.query = @"select employee_id from pos_employees where full_name = '" + TextData.full_name_key.ToString() + "' and emp_code = '" + TextData.cus_codekey.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                if (TextData.saved_image_path == GetSetData.Data || TextData.saved_image_path == null || TextData.saved_image_path == "")
                {
                    TextData.saved_image_path = data.UserPermissions("image_path", "pos_employees", "employee_id", GetSetData.Ids.ToString());

                    if (TextData.saved_image_path == "")
                    {
                        TextData.saved_image_path = "nill";
                    }
                }

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
                        //GetSetData.Ids = data.UserPermissionsIds("country_id", "pos_country", "title", TextData.countrykey);
                        //GetSetData.fks = data.UserPermissionsIds("city_id", "pos_city", "title", TextData.provincekey);

                        GetSetData.query = @"update pos_employees set date = '" + TextData.dates.ToString() + "' , full_name = '" + TextData.full_name.ToString() + "' , post_code = '" + TextData.post_code.ToString() + "' , zip_code = '" + TextData.zip_code.ToString() + "' , cnic = '" + TextData.cnic.ToString() + "' , house_no = '" + TextData.house_no.ToString() + "' , telephone_no = '" + TextData.phone2.ToString() + "' , mobile_no = '" + TextData.phone1.ToString() + "' , address1 = '" + TextData.address1.ToString() + "' , address2 = '" + TextData.address2.ToString() + "' , email = '" + TextData.email.ToString() + "'  ,  image_path = '" + TextData.saved_image_path.ToString() + "' ,  salary = '" + TextData.salary.ToString() + "' , daily_wages = '" + TextData.daily_wages.ToString() + "' , commission = '" + TextData.commission.ToString() + "' , remarks = '" + TextData.remarks.ToString() + "' , status = '" + TextData.status.ToString() + "' where full_name = '" + TextData.full_name_key.ToString() + "' and emp_code = '" + TextData.cus_codekey.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //GetSetData.SaveLogHistoryDetails("Add New Employee Form", "Updating [" + TextData.full_name + "   " + TextData.cus_code + "] details", role_id);

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
                    error.errorMessage("Please enter Employee full name!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                //MessageBox.Show("'" + TextData.saved_image_path + "' is already exist! \nPlease change file name");
                //error.errorMessage("'" + TextData.saved_image_path + "' is already exist! \nPlease change file name");
                //error.ShowDialog();
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            Update_Customers_records_db();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void cnic_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
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

        private void salary_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_salary.Text, e);
        }

        private void daily_wages_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_daily_wages.Text, e);
        }

        private void commission_keypress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txt_commission.Text, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
        }

        public static string auto_generate_code(string condition)
        {
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
            Datalayers data = new Datalayers(webConfig.con_string);
            error_form error = new error_form();
            done_form done = new done_form();

            TextData.cus_code = "";

            try
            {
                GetSetData.query = @"SELECT top 1 employeeCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set employeeCodes = '" + GetSetData.Ids.ToString() + "';";
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
                    error.errorMessage("Please save employee's records first!");
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
            GetSetData.SaveLogHistoryDetails("Add New Employee Form", "Print [" + txt_full_name.Text + "   " + txt_cus_code.Text + "] barcode", role_id);
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

        private void txt_country_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txt_country, "fillComboBoxCountryNames", "title");
        }

        private void city_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(city_text, "fillComboBoxCityNames", "title");
        }

        private void txt_mobile_no_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
