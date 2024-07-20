using System;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.LoginInfo.controllers;

namespace Message_box_info.forms
{
    public partial class formCapitalAmount : Form
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

        public formCapitalAmount()
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
        public static bool saveEnable = false;

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
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("expenses_save", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //savebutton.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("expenses_update", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //update_button.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("expenses_exit", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnl_close.Visible = bool.Parse(GetSetData.Data);

                //GetSetData.Data = data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", role_id.ToString());

                //if (GetSetData.Data == "TURE" || GetSetData.Data == "true" || GetSetData.Data == "True")
                //{
                //    lblCapital.Visible = true;
                //    lblCapitalAmount.Visible = true;
                //    string capitalAmount = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                //    lblCapitalAmount.Text = capitalAmount;
                //}
                //else
                //{
                //    lblCapital.Visible = false;
                //    lblCapitalAmount.Visible = false;
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            txtDate.Text = TextData.date;
            txtTime.Text = TextData.time;
            txt_status.Text = TextData.status;
            txtCapital.Text = TextData.cashAmount;
            txtTotalCapital.Text = TextData.totalCapital;
            txtInvestment.Text = TextData.NetInvestment;
            txtRemarks.Text = TextData.remarks;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "UPDATE CAPITAL AMOUNT DETAILS";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "ADD NEW CAPITAL AMOUNT";
            }
        }

        private bool insert_records()
        {
            try
            {
                string capital = data.UserPermissions("total_capital", "pos_capital");
                string investment = data.UserPermissions("total_investments", "pos_capital");
                string remarks = txtRemarks.Text;

                if (txtRemarks.Text == "")
                {
                    remarks = "nill";
                }

                if (txt_status.Text != "" && txt_status.Text != "-- Select --")
                {
                    if (txtCapital.Text != "")
                    {
                        if (txtInvestment.Text != "")
                        {
                            GetSetData.query = @"insert into pos_capital_history values ('" + txtDate.Text + "', '" + txtTime.Text + "', '" + txtCapital.Text + "', '" + txtTotalCapital.Text + "', '" + txtInvestment.Text + "', '" + remarks + "', '" + txt_status.Text + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.query = @"update pos_capital set total_capital = '" + txtTotalCapital.Text + "', total_investments = '" + txtInvestment.Text + "';";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.SaveLogHistoryDetails("Add New Capital Investment Form", "Saving Capital Investment [" + txtDate.Text + "  " + txtTime.Text + "] details", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please fill the empty fields!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the capital amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the status!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (insert_records())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
                totalCapitalAndInvestments();
            }
        }

        private bool Update_records()
        {
            try
            {
                string capital =  data.UserPermissions("total_capital", "pos_capital");
                string investment = data.UserPermissions("total_investments", "pos_capital");
                string remarks = txtRemarks.Text;

                if (txtRemarks.Text == "")
                {
                    remarks = "nill";
                }

                if (txt_status.Text != "" && txt_status.Text != "-- Select --")
                {
                    if (txtCapital.Text != "")
                    {
                        if (txtInvestment.Text != "")
                        {

                            GetSetData.query = @"update pos_capital_history set date = '" + txtDate.Text + "',  time = '" + txtTime.Text + "', amount = '" + txtCapital.Text + "', total_capital = '" + txtTotalCapital.Text + "', total_investment = '" + txtInvestment.Text + "', remarks = '" + txtRemarks.Text + "', status = '" + txt_status.Text + "' where (date = '" + TextData.date + "') and (time = '" + TextData.time + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            //******************************************************************************************
                            GetSetData.query = @"update pos_capital set total_capital = '" + txtTotalCapital.Text + "',  total_investments = '" + txtInvestment.Text + "';";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.SaveLogHistoryDetails("Add New Capital Investment Form", "Updating Capital Investment History [" + txtDate.Text + "  " + txtTime.Text + "]", role_id);
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please fill the empty fields!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the capital amount!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select the status!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Update_records())
            {
                done.DoneMessage("Updated Successfully!");
                done.ShowDialog();
            }
        }

        private void totalCapitalAndInvestments()
        {
            try
            {
                txtTotalCapital.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                txtAvailableBalance.Text = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                txtInvestment.Text = data.UserPermissions("round(total_investments, 2)", "pos_capital");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void formCapitalAmount_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Capital Investment Form", "Exit...", role_id);
            Button_controls.CapitalHistoryDetails();
            this.Close();
        }

        private void txtCapitalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtTotalCapital.Text, e);
        }

        private void txtInvestment_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtInvestment.Text, e);
        }

        private void txtCapital_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double newCapitalAmount = 0;
                if (txtCapital.Text != "")
                {
                    if (txt_status.Text == "Withdraw")
                    {
                        newCapitalAmount = double.Parse(txtAvailableBalance.Text) - double.Parse(txtCapital.Text);
                    }
                    else
                    {
                        newCapitalAmount = double.Parse(txtAvailableBalance.Text) + double.Parse(txtCapital.Text);
                    }

                    if (newCapitalAmount >= 0)
                    {
                        txtTotalCapital.Text = newCapitalAmount.ToString();
                    }
                    else
                    {
                        txtTotalCapital.Text = "0.00";
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void refresh()
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                txtTime.Text = DateTime.Now.ToLongTimeString();
                txtCapital.Text = "0";
                txtRemarks.Text = "";
                txt_status.Text = "-- Select --";
                system_user_permissions();
                enableSaveButton();
                totalCapitalAndInvestments();
                txtDate.Focus();
            }
            catch (Exception es)
            {
                 error.errorMessage(es.Message);
                 error.ShowDialog();
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }


    }
}
