using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using RefereningMaterial.ReferenceClasses;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class add_brand : Form
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

        public add_brand()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string brand = "";

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Product Brand Title Form", "Exit...", role_id); 
            this.Close();
        }
        
        private void insert_records()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"insert into pos_brand (brand_title) values ('" + title_text.Text.ToUpper() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Product Brand Title Form", "Saving Brand [" + title_text.Text.ToUpper() + "]", role_id); 

                    //this.Opacity = .850;
                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                    //this.Opacity = .999;

                    title_text.Text = "";
                    title_text.TabIndex = 0;
                }
                else
                {
                    error.errorMessage("Please fill the empty fields!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool DeleteItems()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = "delete from pos_brand where brand_title = '" + title_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Product Brand Title Form", "Deleting Brand [" + title_text.Text + "]", role_id);
                }
                else
                {
                    error.errorMessage("Please enter title first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(title_text.Text + " cannot be deleted!");
                error.ShowDialog();
                return false;
            }
        }

        private void FillComboBoxWithItems()
        {
            GetSetData.FillComboBoxUsingProcedures(title_text, "fillComboBoxBrand", "brand_title");
        }


        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
            FillComboBoxWithItems();
            this.Close();
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                insert_records();
                FillComboBoxWithItems();
                this.Close();
            }
        }

        private void add_brand_Load(object sender, EventArgs e)
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                ClassDefaultValuesSetInDB.InsertValuesInTableBrand();
                FillComboBoxWithItems();

                title_text.Text = brand;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + title_text.Text + "'");
                sure.ShowDialog();
                //this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    DeleteItems();
                    FillComboBoxWithItems();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"update pos_brand  set brand_title = '" + title_text.Text.ToUpper() + "' where (brand_title = '" + brand + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Product Brand Title Form", "Saving Brand [" + title_text.Text + "]", role_id);

                    //this.Opacity = .850;
                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                    //this.Opacity = .999;

                    title_text.Text = "";
                    title_text.TabIndex = 0;

                    this.Close();
                }
                else
                {
                    error.errorMessage("Please fill the empty fields!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void title_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
