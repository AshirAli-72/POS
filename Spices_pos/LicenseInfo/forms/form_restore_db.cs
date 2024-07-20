using System;
using System.Data;
using System.Windows.Forms;
using Message_box_info.forms;
using System.Data.SqlClient;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.LicenseInfo.forms
{
    public partial class form_restore_db : Form
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

        public form_restore_db()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;


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
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        //private void copyDirectoryImages()
        //{
        //    string quer_get_image_path_db = @"select picture_path from pos_general_settings;";
        //    string targetPath = data.Select_logo_path_db(quer_get_image_path_db);

        //    string sourcePath = @"" + txt_path.Text + @"\images\";
        //    string destPath = "";
        //    string fileName = "";

        //    System.IO.Directory.CreateDirectory(targetPath);

        //    if (System.IO.Directory.Exists(sourcePath))
        //    {
        //        string[] files = System.IO.Directory.GetFiles(sourcePath);

        //        foreach (string items in files)
        //        {
        //            fileName = System.IO.Path.GetFileName(items);
        //            destPath = System.IO.Path.Combine(targetPath, fileName);
        //            System.IO.File.Copy(items, destPath, true);
        //        }
        //    }
        //    else
        //    {
        //        error.errorMessage("Source path does not exist!");
        //        error.ShowDialog();
        //    }
        //}

        private void showbutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Restore Form", "Exit...", role_id);
            this.Close();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            this.Opacity = .850;
            OpenFileDialog dig = new OpenFileDialog();
            dig.Filter = "All files|*.bak"; // SQL SERVER database backup
            dig.Title = "Database restore";

            if (dig.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = dig.FileName;
                btn_restore.Enabled = true;
            }
            this.Opacity = .999;
            GetSetData.SaveLogHistoryDetails("Restore Form", "Click browse button...", role_id);
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            string database = conn.Database.ToString();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                //string db_name = Path.GetFileName(txt_path.Text);

                if (txt_path.Text != "")
                {
                    GetSetData.query = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    SqlCommand cmd2 = new SqlCommand(GetSetData.query, conn);
                    cmd2.ExecuteNonQuery();

                    GetSetData.query = string.Format("USE MASTER RESTORE DATABASE [" + database + "] FROM DISK= '" + txt_path.Text + "' WITH REPLACE;");
                    SqlCommand cmd3 = new SqlCommand(GetSetData.query, conn);
                    cmd3.ExecuteNonQuery();

                    GetSetData.query = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                    SqlCommand cmd4 = new SqlCommand(GetSetData.query, conn);
                    cmd4.ExecuteNonQuery();

                    GetSetData.SaveLogHistoryDetails("Restore Form", "Restoring the software data...", role_id);

                    done.DoneMessage("Database restoration done successfully");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please select the file location first!");
                    error.ShowDialog();
                }
            }
            catch (SqlException es)
            {
                MessageBox.Show(es.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void form_restore_db_Load(object sender, EventArgs e)
        {
            //GetSetData.addFormCopyrights(lblCopyrights);
        }
    }
}
