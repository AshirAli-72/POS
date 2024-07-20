using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Expenses_info.forms
{
    public partial class AddRole : Form
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

        public AddRole()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string title = "";

  
        private void showbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Expenses Title Form", "Exit...", role_id);
            this.Close();
        }

        private void insert_records()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.Ids = data.UserPermissionsIds("role_id", "pos_roles", "roleTitle", title_text.Text);

                    if (GetSetData.Ids == 0)
                    {
                        GetSetData.query = @"insert into pos_roles (roleTitle) values ('" + title_text.Text.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);


                        //this.Opacity = .850;
                        done.DoneMessage("Successfully Saved!");
                        done.ShowDialog();
                        //this.Opacity = .999;

                        title_text.Text = "";
                    }
                    else
                    {
                        error.errorMessage("'" + title_text.Text.ToString() + "' is already exist!");
                        error.ShowDialog();
                    }
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
                    GetSetData.query = "delete from pos_roles where (roleTitle = '" + title_text.Text + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Please enter role title first!");
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
            GetSetData.FillComboBoxUsingProcedures(title_text, "fillComboBoxRoles", "roleTitle");
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
            FillComboBoxWithItems();
            title_text.TabIndex = 0;
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                insert_records();
                FillComboBoxWithItems();
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

        private void AddRole_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            FillComboBoxWithItems();

            title_text.Text = title;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"update pos_roles set roleTitle = '" + title_text.Text.ToUpper() + "' where (roleTitle = '" + title + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Product Brand Title Form", "Saving Brand [" + title_text.Text + "]", role_id);

                    //this.Opacity = .850;
                    done.DoneMessage("Successfully Updated!");
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

        private void title_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
