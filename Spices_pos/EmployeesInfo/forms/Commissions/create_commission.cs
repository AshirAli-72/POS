using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Products_info.forms.RecipeDetails
{
    public partial class create_commission : Form
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

        public create_commission()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
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
                //GetSetData.addFormCopyrights(lblCopyrights);
                //**********************************************************************************************
                savebutton.Visible = bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id));
                update_button.Visible = bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id));

                if (bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id)) == false && bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id)) == false)
                {
                    pnl_exit.Visible = false;
                }

                pnl_save.Visible = bool.Parse(data.UserPermissions("products_exit", "pos_tbl_authorities_button_controls2", role_id));
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtCommissionTitle.Text = TextData.commissionTitle;
                txtEmployee.Text = TextData.employee;
                txtStartDate.Text = TextData.startDate;
                txtEndDate.Text = TextData.endDate;
                txtStartTime.Text = TextData.startTime;
                txtEndTime.Text = TextData.endTime;
                txtStatus.Text = TextData.status;

                txtDescription.Text = data.UserPermissions("note", "pos_employee_commission", "commission_id", TextData.commissionId);
                chkCommissionInPercentage.Checked = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", TextData.commissionId));
                TextData.commissionPercentage = data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", TextData.commissionId);

                if (chkCommissionInPercentage.Checked)
                {
                    txtCommissionAmount.Text = TextData.commissionPercentage;
                }
                else
                {
                    txtCommissionAmount.Text = TextData.commissionAmount;
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
            if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                FormNamelabel.Text = "Create New Commission";
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Commission";
                fillAddProductsFormTextBoxes();
            }
        }

        private void refresh()
        {
            txtStartDate.Text = DateTime.Now.ToLongDateString();
            txtEndDate.Text = DateTime.Now.ToLongDateString();
            txtStartTime.Text = DateTime.Now.ToLongTimeString();
            txtEndTime.Text = DateTime.Now.ToLongTimeString();

            txtCommissionTitle.Text = "";
            txtEmployee.Text = null;
            txtCommissionAmount.Text = "0";
            chkCommissionInPercentage.Checked = false;
            txtDescription.Text = "";

            txtStatus.SelectedIndex = 0;

            system_user_permissions();
            enableSaveButton();
            txtCommissionTitle.Select();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void add_new_connection_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillValuesInVariablesForUse()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.commissionTitle = txtCommissionTitle.Text;
                TextData.employee = txtEmployee.Text;
                TextData.startDate = txtStartDate.Text;
                TextData.endDate = txtEndDate.Text;
                TextData.startTime = txtStartTime.Text;
                TextData.endTime = txtEndTime.Text;
                TextData.remarks = txtDescription.Text;
                TextData.status = txtStatus.Text;
                // ******************************************************************

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_records_into_db()
        {
            try
            {
                fillValuesInVariablesForUse();

                if (txtCommissionTitle.Text != "")
                {
                    if (txtEmployee.Text != "")
                    {
                        if (txtCommissionAmount.Text != "")
                        {
                            if (chkCommissionInPercentage.Checked)
                            {
                                TextData.commissionPercentage = txtCommissionAmount.Text;
                                TextData.commissionAmount = "0";
                                TextData.isCommissionInPercentage = true;
                            }
                            else
                            {
                                TextData.commissionAmount = txtCommissionAmount.Text;
                                TextData.commissionPercentage = "0";
                                TextData.isCommissionInPercentage = false;
                            }

                            //========================================================================================================

                            GetSetData.query = "select employee_id from pos_employees where (full_name = '" + TextData.employee + "');";
                            int employee_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                            //========================================================================================================

                            if (employee_id_db != 0)
                            {
                                GetSetData.query = "insert into pos_employee_commission values ('" + TextData.commissionTitle + "' , '" + TextData.startDate + "' , '" + TextData.endDate + "' , '" + TextData.startTime + "' , '" + TextData.endTime + "' , '" + TextData.commissionAmount + "' , '" + TextData.commissionPercentage + "' , '" + TextData.isCommissionInPercentage.ToString() + "' , '" + TextData.remarks + "' , '" + TextData.status + "' , '" + employee_id_db.ToString() + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                return true;
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the commission!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee title!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the commission title!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }

            return false;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {

            if (insert_records_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                refresh();
            }
        }

        private bool update_records_db()
        {
            try
            {

                fillValuesInVariablesForUse();

                if (txtCommissionTitle.Text != "")
                {
                    if (txtEmployee.Text != "")
                    {
                        if (txtCommissionAmount.Text != "")
                        {
                            if (chkCommissionInPercentage.Checked)
                            {
                                TextData.commissionPercentage = txtCommissionAmount.Text;
                                TextData.commissionAmount = "0";
                                TextData.isCommissionInPercentage = true;
                            }
                            else
                            {
                                TextData.commissionAmount = txtCommissionAmount.Text;
                                TextData.commissionPercentage = "0";
                                TextData.isCommissionInPercentage = false;
                            }

                            //========================================================================================================

                            GetSetData.query = "select employee_id from pos_employees where (full_name = '" + TextData.employee + "');";
                            int employee_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                            //========================================================================================================

                            if (employee_id_db != 0)
                            {
                                GetSetData.query = "update pos_employee_commission set title = '" + TextData.commissionTitle + "' , start_date = '" + TextData.startDate + "' , end_date =  '" + TextData.endDate + "' , start_time = '" + TextData.startTime + "' , end_time = '" + TextData.endTime + "' , commission_amount = '" + TextData.commissionAmount.ToString() + "' , commission_percentage = '" + TextData.commissionPercentage + "' , is_commission_in_percentage = '" + TextData.isCommissionInPercentage.ToString() + "' , note = '" + TextData.remarks + "' , status = '" + TextData.status + "' , employee_id = '" + employee_id_db.ToString() + "' where (commission_id = '" + TextData.commissionId + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                return true;
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the commission!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the employee title!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the commission title!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }

            return false;
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (update_records_db())
                {
                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployee, "fillComboBoxEmployeeNames", "full_name");
        }

    }
}
