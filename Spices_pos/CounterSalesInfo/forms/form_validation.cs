using System;
using System.Windows.Forms;
using Login_info.controllers;
using Message_box_info.forms;
using Datalayer;
using System.Data.SqlClient;
using System.Media;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms
{
    public partial class form_validation : Form
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

        public form_validation()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

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
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel5, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_validation_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            txt_barcode.Text = "";
            txt_barcode.TabIndex = 0;
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("POS Validation Form", "Exit...", role_id); 
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void loginByScanner()
        {
            try
            {
                TextData.barcode = "";
                TextData.barcode = txt_barcode.Text;
                GetSetData.Data = "";

                form_counter_sales.employeeName = data.UserPermissions("full_name", "pos_employees", "emp_code", TextData.barcode.ToString());
                TextData.image_path = data.UserPermissions("picture_path", "pos_general_settings");

                if (TextData.barcode != "")
                {
                    GetSetData.query = @"select * from pos_employees;";
                    SqlCommand cmd = new SqlCommand(GetSetData.query, data.conn_);
                    SqlDataReader reader;

                    data.conn_.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GetSetData.Data = reader["emp_code"].ToString();

                        if (TextData.barcode == GetSetData.Data)
                        {
                            GetSetData.SaveLogHistoryDetails("POS Validation Form", "login click...", role_id);
                            Button_controls.Pos_button();
                            this.Close();

                            // Play Sound ****************************************************************
                            TextData.image_path = @"" + TextData.image_path + "sound.wav";
                            SoundPlayer player = new SoundPlayer(TextData.image_path);
                            player.Play();
                            break;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.conn_.Close();
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            loginByScanner();
        }
    }
}
