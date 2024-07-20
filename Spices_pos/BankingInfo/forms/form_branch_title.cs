using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using RefereningMaterial.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Banking_info.forms
{
    public partial class form_branch_title : Form
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

        public form_branch_title()
        {
            InitializeComponent(); 
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string title = "";

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

        private void RefreshFieldBranch()
        {
            GetSetData.FillComboBoxUsingProcedures(title_text, "fillComboBoxBranchkTitle", "branch_title");
        }

        private void insert_records()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"insert into pos_bank_branch (branch_title) values ('" + title_text.Text.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
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

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                insert_records();
                RefreshFieldBranch();
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
            RefreshFieldBranch();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Bank Branch Title Form", "Exit...", role_id);
            this.Close();
        }

        private bool DeleteItems()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"delete from pos_bank_branch where branch_title = '" + title_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Bank Branch Title Form", "Deleting item [" + title_text.Text + "]", role_id);
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

        private void showbutton_Click(object sender, EventArgs e)
        {
            try
            {
                sure.Message_choose("Are you sure you want to delete '" + title_text.Text + "'");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    DeleteItems();
                    RefreshFieldBranch();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_branch_title_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            ClassDefaultValuesSetInDB.InsertValuesInTablesBankBranches();
            RefreshFieldBranch();
            title_text.Text = title;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"update pos_bank_branch set branch_title = '" + title_text.Text.ToUpper() + "' where (branch_title = '" + title + "');";
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
    }
}
