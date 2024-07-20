using System;
using System.Drawing;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using System.IO;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class formReportsSettings : Form
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

        public formReportsSettings()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

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

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void formReportsSettings_Load(object sender, EventArgs e)
        {
            //GetSetData.addFormCopyrights(lblCopyrights);
            fillColorsInTextFields();
        }

        private void formReportsSettings_Shown(object sender, EventArgs e)
        {
            try
            {
                txt_title.Text = buttonControls.fun_title_db();
                txt_address.Text = buttonControls.fun_sub_title_db();
                txt_phone_no.Text = buttonControls.fun_phone_no_db();
                txt_note.Text = buttonControls.fun_message_db();
                txt_copyrights.Text = buttonControls.fun_copyrights_db();
                Reports_tab_indexes();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings reg = new settings();
            reg.Show();

            this.Dispose();
        }

        private void Reports_tab_indexes()
        {
            txt_title.TabIndex = 0;
            txt_address.TabIndex = 1;
            txt_phone_no.TabIndex = 2;
            txt_note.TabIndex = 3;
            txt_copyrights.TabIndex = 4;
        }

        private void btn_save_reports_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.title = txt_title.Text;
                TextData.sub_title = txt_address.Text;
                TextData.phone1 = txt_phone_no.Text;
                TextData.message = txt_note.Text;
                TextData.copyrights = txt_copyrights.Text;
         
                //*******************************************************

                TextData.saved_image_path = "";
                //string logoInDB = "";
                string imagePathDB = "";
                string fullImagePath = "";

                if (img_pic_box.Image != null && TextData.image_path != "" && TextData.image_path != null)
                {
                    
                    //logoInDB = data.UserPermissions("logo_path", "pos_report_settings");
                    imagePathDB = data.UserPermissions("picture_path", "pos_general_settings");
                    fullImagePath = imagePathDB + Path.GetFileName(TextData.image_path);


                    if ((TextData.image_path + Path.GetFileName(TextData.image_path)) != fullImagePath)
                    {
                        File.Copy(TextData.image_path, Path.Combine(imagePathDB, Path.GetFileName(TextData.image_path)), true);
                    }

                    TextData.saved_image_path = Path.GetFileName(TextData.image_path);

                }


                buttonControls.insert_update_reports_tables();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillColorsInTextFields()
        {
            try
            {
                txtDarkRed.Text = data.UserPermissions("dark_red", "pos_colors_settings");
                txtDarkGreen.Text = data.UserPermissions("dark_green", "pos_colors_settings");
                txtDarkBlue.Text = data.UserPermissions("dark_blue", "pos_colors_settings");
                txtBackRed.Text = data.UserPermissions("back_red", "pos_colors_settings");
                txtBackGreen.Text = data.UserPermissions("back_green", "pos_colors_settings");
                txtBackBlue.Text = data.UserPermissions("back_blue", "pos_colors_settings");
                txtForeRed.Text = data.UserPermissions("fore_red", "pos_colors_settings");
                txtForeGreen.Text = data.UserPermissions("fore_green", "pos_colors_settings");
                txtForeBlue.Text = data.UserPermissions("fore_blue", "pos_colors_settings");
                txtOthersRed.Text = data.UserPermissions("others_red", "pos_colors_settings");
                txtOthersGreen.Text = data.UserPermissions("others_green", "pos_colors_settings");
                txtOthersBlue.Text = data.UserPermissions("others_blue", "pos_colors_settings");
                txtMenuBackRed.Text = data.UserPermissions("menu_back_red", "pos_colors_settings");
                txtMenuBackGreen.Text = data.UserPermissions("menu_back_green", "pos_colors_settings");
                txtMenuBackBlue.Text = data.UserPermissions("menu_back_blue", "pos_colors_settings");
                txtMenuForeRed.Text = data.UserPermissions("menu_fore_red", "pos_colors_settings");
                txtMenuForeGreen.Text = data.UserPermissions("menu_fore_green", "pos_colors_settings");
                txtMenuForeBlue.Text = data.UserPermissions("menu_fore_blue", "pos_colors_settings");
                txtCategoryBackRed.Text = data.UserPermissions("category_back_red", "pos_colors_settings");
                txtCategoryBackGreen.Text = data.UserPermissions("category_back_green", "pos_colors_settings");
                txtCategoryBackBlue.Text = data.UserPermissions("category_back_blue", "pos_colors_settings");
                txtCategoryForeRed.Text = data.UserPermissions("category_fore_red", "pos_colors_settings");
                txtCategoryForeGreen.Text = data.UserPermissions("category_fore_green", "pos_colors_settings");
                txtCategoryForeBlue.Text = data.UserPermissions("category_fore_blue", "pos_colors_settings");
                txtDealBackRed.Text = data.UserPermissions("deal_back_red", "pos_colors_settings");
                txtDealBackGreen.Text = data.UserPermissions("deal_back_green", "pos_colors_settings");
                txtDealBackBlue.Text = data.UserPermissions("deal_back_blue", "pos_colors_settings");
                txtDealForeRed.Text = data.UserPermissions("deal_fore_red", "pos_colors_settings");
                txtDealForeGreen.Text = data.UserPermissions("deal_fore_green", "pos_colors_settings");
                txtDealForeBlue.Text = data.UserPermissions("deal_fore_blue", "pos_colors_settings");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnColorSave_Click(object sender, EventArgs e)
        {
            try
            {
                GetSetData.Ids = 0;
                GetSetData.Ids = data.UserPermissionsIds("id", "pos_colors_settings", "id", "1");

                if (GetSetData.Ids == 0)
                {
                    GetSetData.query = @"insert into pos_colors_settings values ('" + txtDarkRed.Text + "' , '" + txtDarkGreen.Text + "' , '" + txtDarkBlue.Text + "' , '" + txtBackRed.Text + "' , '" + txtBackGreen.Text + "', '" + txtBackBlue.Text + "', '" + txtForeRed.Text + "', '" + txtForeGreen.Text + "', '" + txtForeBlue.Text + "', '" + txtOthersRed.Text + "', '" + txtOthersGreen.Text + "', '" + txtOthersBlue.Text + "', '" + txtMenuBackRed.Text + "', '" + txtMenuBackGreen.Text + "', '" + txtMenuBackBlue.Text + "', '" + txtMenuForeRed.Text + "', '" + txtMenuForeGreen.Text + "', '" + txtMenuForeBlue.Text + "', '" + txtCategoryBackRed.Text + "', '" + txtCategoryBackGreen.Text + "', '" + txtCategoryBackBlue.Text + "', '" + txtCategoryForeRed.Text + "', '" + txtCategoryForeGreen.Text + "', '" + txtCategoryForeBlue.Text + "', '" + txtDealBackRed.Text + "', '" + txtDealBackGreen.Text + "', '" + txtDealBackBlue.Text + "', '" + txtDealForeRed.Text + "', '" + txtDealForeGreen.Text + "', '" + txtDealForeBlue.Text + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Colors Added Successfully!");
                    done.ShowDialog();
                }
                else
                {
                    GetSetData.query = @"update pos_colors_settings set dark_red = '" + txtDarkRed.Text + "' , dark_green = '" + txtDarkGreen.Text + "' , dark_blue = '" + txtDarkBlue.Text + "' , back_red = '" + txtBackRed.Text + "' , back_green = '" + txtBackGreen.Text + "', back_blue = '" + txtBackBlue.Text + "', fore_red = '" + txtForeRed.Text + "', fore_green = '" + txtForeGreen.Text + "', fore_blue = '" + txtForeBlue.Text + "', others_red = '" + txtOthersRed.Text + "', others_green = '" + txtOthersGreen.Text + "', others_blue = '" + txtOthersBlue.Text + "', menu_back_red = '" + txtMenuBackRed.Text + "', menu_back_green = '" + txtMenuBackGreen.Text + "', menu_back_blue = '" + txtMenuBackBlue.Text + "', menu_fore_red = '" + txtMenuForeRed.Text + "', menu_fore_green = '" + txtMenuForeGreen.Text + "', menu_fore_blue = '" + txtMenuForeBlue.Text + "', category_back_red = '" + txtCategoryBackRed.Text + "', category_back_green = '" + txtCategoryBackGreen.Text + "', category_back_blue = '" + txtCategoryBackBlue.Text + "', category_fore_red = '" + txtCategoryForeRed.Text + "', category_fore_green = '" + txtCategoryForeGreen.Text + "', category_fore_blue = '" + txtCategoryForeBlue.Text + "', deal_back_red = '" + txtDealBackRed.Text + "', deal_back_green = '" + txtDealBackGreen.Text + "', deal_back_blue = '" + txtDealBackBlue.Text + "', deal_fore_red = '" + txtDealForeRed.Text + "', deal_fore_green = '" + txtDealForeGreen.Text + "', deal_fore_blue = '" + txtDealForeBlue.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Colors Updated Successfully!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            img_pic_box.Image = null;
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
                string imagePathDB = "";
                string fullImagePath = "";

                if (img_pic_box.Image != null && TextData.image_path != "" && TextData.image_path != null)
                {
                    imagePathDB = data.UserPermissions("picture_path", "pos_general_settings");
                    fullImagePath = imagePathDB + Path.GetFileName(TextData.image_path);

                    TextData.saved_image_path = imagePathDB;

                    if ((TextData.image_path + Path.GetFileName(TextData.image_path)) != fullImagePath)
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

        private void formReportsSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txtDealForeBlue_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}