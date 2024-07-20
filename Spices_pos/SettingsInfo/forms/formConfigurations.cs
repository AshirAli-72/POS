using System;
using System.Drawing;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using System.IO;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class formConfigurations : Form
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

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        public formConfigurations()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        private void setFormColorsDynamically()
        {
            try
            {
                int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
                int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
                int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

                int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

                int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

                //****************************************************************

                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh_configure);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formConfigurations_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
        }

        private void formConfigurations_Shown(object sender, EventArgs e)
        {
            try
            {
                fill_configuration_textboxes();
                Configuration_tab_indexes();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh()
        {
            TextData.shop_name = shop_name_text.Text = "";
            TextData.owner_name = owner_text.Text = "";
            TextData.city = city_text.Text = "";
            TextData.address = address_text.Text = "";
            TextData.business_type = business_text.Text = "";
            business_nature_text.Text = "";
            TextData.branch = branch_text.Text = "";
            TextData.shop_no = shop_no_text.Text = "";
            TextData.phone1 = mobile_text.Text = "";
            TextData.phone2 = telphone_text.Text = "";
            TextData.comments = comments_text.Text = "";

            img_pic_box = null;

            fill_configuration_textboxes();
            Configuration_tab_indexes();
        }

        private void btn_refresh_configure_Click(object sender, EventArgs e)
        {
            refresh();
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

                    if ((TextData.image_path + Path.GetFileName(TextData.image_path)) != GetSetData.Permission)
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
            fun_upload_image();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {

                TextData.shop_name = shop_name_text.Text;
                TextData.owner_name = owner_text.Text;
                TextData.city = city_text.Text;
                TextData.address = address_text.Text;
                TextData.business_type = business_nature_text.Text;
                TextData.branch = branch_text.Text;
                TextData.shop_no = shop_no_text.Text;
                TextData.phone1 = mobile_text.Text;
                TextData.phone2 = telphone_text.Text;
                TextData.saved_image_path = fun_saved_image();
                TextData.comments = comments_text.Text;

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

                if (TextData.saved_image_path != "" && TextData.saved_image_path != GetSetData.Data && TextData.saved_image_path != null)
                {
                    TextData.image_path = data.UserPermissions("logo_path", "pos_configurations");

                    if (TextData.image_path == "")
                    {
                        TextData.saved_image_path = "nill";
                    }
                }

                //GetSetData.SaveLogHistoryDetails("Settings Form", "Saving/Updating configuration details...", role_id);
                buttonControls.update_Configuration_details();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fill_configuration_textboxes()
        {
            try
            {
                TextData.shop_name = data.UserPermissions("shop_name", "pos_configurations");
                TextData.owner_name = data.UserPermissions("owner_name", "pos_configurations");
                TextData.city = data.UserPermissions("city", "pos_configurations");
                TextData.address = data.UserPermissions("address", "pos_configurations");
                TextData.branch = data.UserPermissions("branch", "pos_configurations");
                TextData.shop_no = data.UserPermissions("shop_no", "pos_configurations");
                TextData.phone1 = data.UserPermissions("phone_1", "pos_configurations");
                TextData.phone2 = data.UserPermissions("phone_2", "pos_configurations");
                TextData.image_path = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.saved_image_path = data.UserPermissions("logo_path", "pos_configurations");
                TextData.comments = data.UserPermissions("comments", "pos_configurations");

                shop_name_text.Text = TextData.shop_name;
                owner_text.Text = TextData.owner_name;
                city_text.Text = TextData.city;
                address_text.Text = TextData.address;
                business_nature_text.Text = TextData.business_type;
                branch_text.Text = TextData.branch;
                shop_no_text.Text = TextData.shop_no;
                mobile_text.Text = TextData.phone1;
                telphone_text.Text = TextData.phone2;

                if (TextData.saved_image_path != "nill" && TextData.saved_image_path != "")
                {
                    img_pic_box.Image = Bitmap.FromFile(TextData.image_path + TextData.saved_image_path);
                }

                comments_text.Text = TextData.comments;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Configuration_tab_indexes()
        {
            shop_name_text.TabIndex = 0;
            owner_text.TabIndex = 1;
            city_text.TabIndex = 2;
            address_text.TabIndex = 3;
            business_nature_text.TabIndex = 4;
            business_text.TabIndex = 5;
            branch_text.TabIndex = 6;
            shop_no_text.TabIndex = 7;
            mobile_text.TabIndex = 8;
            telphone_text.TabIndex = 9;
            comments_text.TabIndex = 10;
        }



    }
}
