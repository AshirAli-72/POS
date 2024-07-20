using System;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.Clock_In
{
    public partial class formCounters : Form
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

        public formCounters()
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
        public static string counterId = "";
        public static string title = "";
        public static string openingAmount = "";
        public static string shiftLimit = "";
        public static bool saveEnable;
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

        private void fillAddProductsFormTextBoxes()
        {
            title_text.Text = title;
            txtOpeningAmount.Text = openingAmount;
            txtShiftLimit.Text = shiftLimit;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                btnUpdate.Visible = true;
                FormNamelabel.Text = "Update Counter";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                btnUpdate.Visible = false;
                FormNamelabel.Text = "Create New Counter";
            }
        }

        private void insert_records()
        {
            try
            {
                if (title_text.Text != "")
                {
                    if (txtOpeningAmount.Text != "")
                    {
                        if (txtShiftLimit.Text != "")
                        {
                            GetSetData.query = @"insert into pos_counter values ('" + title_text.Text + "' , '" + txtOpeningAmount.Text + "' , '" + txtShiftLimit.Text + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            done.DoneMessage("Successfully Saved!");
                            done.ShowDialog();


                            title_text.Text = "";
                            title_text.TabIndex = 0;
                        }
                        else
                        {
                            error.errorMessage("Please enter the shift limit!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the opening amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the counter name!");
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
                    GetSetData.query = "delete from pos_counter where title = '" + title_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    //GetSetData.SaveLogHistoryDetails("Add New Country Title Form", "Deleting Country [" + title_text.Text + "]", role_id);
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

        //private void FillComboBoxWithItems()
        //{
        //    GetSetData.FillComboBoxUsingProcedures(title_text, "fillComboBoxCounters", "title");
        //}

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {

        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
            //FillComboBoxWithItems();
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Add New Country Title Form", "Exit...", role_id);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formCounters_Load(object sender, EventArgs e)
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights); 
                //FillComboBoxWithItems();
                enableSaveButton();

                title_text.Select();
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
                    GetSetData.query = @"update pos_counter set title = '" + title_text.Text +"', opening_amount = '" + txtOpeningAmount.Text + "' , shift_limit = '" + txtShiftLimit.Text + "'  where (id = '" + counterId + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);


                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();


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

        private void title_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtOpeningAmount.Text = data.UserPermissions("opening_amount", "pos_counter", "title", title_text.Text);
                txtShiftLimit.Text = data.UserPermissions("shift_limit", "pos_counter", "title", title_text.Text);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtOpeningAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtOpeningAmount.Text, e);
        }

        private void txtShiftLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtShiftLimit.Text, e);
        }

        private void title_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
