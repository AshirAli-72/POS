using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.LicenseInfo.forms
{
    public partial class form_backup_db : Form
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

        public form_backup_db()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        string password_db = "";
        string user_name_db = "";

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
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
        //    //}
        //    //catch (Exception es)
        //    //{
        //    //    MessageBox.Show(es.Message);
        //    //}
        //}

        private void form_backup_db_Load(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                ServerName_DatabaseName_db();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void copyDirectoryImages()
        {
            GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");

            string destPath = "";
            string fileName = "";
            string targetPath = @"" + txt_path.Text +  @"\images\";

            System.IO.Directory.CreateDirectory(targetPath);

            if (System.IO.Directory.Exists(GetSetData.Data))
            {
                string[] files = System.IO.Directory.GetFiles(GetSetData.Data);

                foreach (string items in files)
                {
                    fileName = System.IO.Path.GetFileName(items);
                    destPath = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(items, destPath, true);
                }
            }
            else
            {
                error.errorMessage("Source path does not exist!");
                error.ShowDialog();
            }
        }

        private void ServerName_DatabaseName_db()
        {
            GetSetData.Data = data.UserPermissions("server", "pos_server_config");
            txtServer.Text = GetSetData.Data;

            GetSetData.Data = data.UserPermissions("database_name", "pos_server_config");
            txtDatabase.Text = GetSetData.Data;

            user_name_db = data.UserPermissions("username", "pos_server_config");
            txt_username.Text = user_name_db;

            password_db = data.UserPermissions("password", "pos_server_config");
            txt_password.Text = password_db;
        }

        private void db_backup_Complete(object sender, ServerMessageEventArgs e)
        {
        }

        private void db_backup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            bar_complete.Invoke((MethodInvoker)delegate
            {
                bar_complete.Value = e.Percent;
                bar_complete.Update();
            });

            //lbl_percent.Invoke((MethodInvoker)delegate
            //{
            //    lbl_percent.Text = e.Percent.ToString() + " %";

            //    //if (lbl_percent.Text == "100 %")
            //    //{
            //    //    done.DoneMessage("Backup Completed!");
            //    //    done.ShowDialog();
            //    //}
            //}); 
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Backup Form", "Exit...", role_id);
            this.Close();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            this.Opacity = .850;
            FolderBrowserDialog dig = new FolderBrowserDialog();
            
            if(dig.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = dig.SelectedPath;
                btn_backup.Enabled = true;
                
            }
            this.Opacity = .999;

            GetSetData.SaveLogHistoryDetails("Backup Form", "click on browse button...", role_id);
        }

        private void savingDatabaseBackup(string database)
        {
            try
            {
                if (txt_path.Text != "")
                {
                    copyDirectoryImages();// Images copying

                    bar_complete.Value = 0;

                    Server db_server = new Server(new ServerConnection(txtServer.Text, user_name_db, password_db));//
                    Backup db_backup = new Backup() { Action = BackupActionType.Database, Database = database };

                    db_backup.Devices.AddDevice(txt_path.Text + "\\" + database + "  [" + DateTime.Now.ToLongDateString() + "]" + ".bak", DeviceType.File);  //@"C:\Data\pos_db.bak" 
                    db_backup.Initialize = true;
                    db_backup.PercentComplete += db_backup_PercentComplete;
                    db_backup.Complete += db_backup_Complete;
                    db_backup.SqlBackupAsync(db_server);
                    GetSetData.SaveLogHistoryDetails("Backup Form", "Saving Customer Databse Backup[" + txt_path.Text + "\\" + database + "  [" + DateTime.Now.ToLongDateString() + "]" + "]", role_id);
                }
                else
                {
                    error.errorMessage("Please select the backup location!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void savingLogDatabaseBackup(string database)
        {
            try
            {
                if (txt_path.Text != "")
                {
                    Server db_server = new Server(new ServerConnection(txtServer.Text, user_name_db, password_db));//
                    Backup db_backup = new Backup() { Action = BackupActionType.Database, Database = database };

                    db_backup.Devices.AddDevice(txt_path.Text + "\\" + database + "  [" + DateTime.Now.ToLongDateString() + "]" + ".bak", DeviceType.File);  //@"C:\Data\pos_db.bak" 
                    db_backup.Initialize = true;
                    db_backup.SqlBackupAsync(db_server);
                    GetSetData.SaveLogHistoryDetails("Backup Form", "Saving Log Databse Backup[" + txt_path.Text + "\\" + database + "  [" + DateTime.Now.ToLongDateString() + "]" + "]", role_id);
                }
                else
                {
                    error.errorMessage("Please select the backup location!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_backup_Click(object sender, EventArgs e)
        {
            savingDatabaseBackup("installment_db");
            savingLogDatabaseBackup("logHistorySIMS_db");
        }
    }
}
