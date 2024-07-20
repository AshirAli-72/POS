using System;
using System.Windows.Forms;
using Datalayer;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.Clock_In
{
    public partial class form_payout : Form
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

        public form_payout()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int user_id = 0;
        public static int role_id = 0;
        public static bool saveEnable;

        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
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

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Payout";
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Payout";
            }
        }

        private bool insert_records()
        {
            try
            {
                if (txtAmount.Text != "")
                {
                    if (txtRemarks.Text != "")
                    {
                        GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '0') ORDER BY id DESC;";
                        string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

                        GetSetData.query = @"insert into pos_payout values ('" + txtDate.Text + "' , '" + time_text.Text + "' , '" + txtAmount.Text + "', '" + txtRemarks.Text + "' , '" + user_id.ToString() + "' , '" + clock_in_id.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        return true;
                    }
                    else
                    {
                        error.errorMessage("Please enter payout reasion!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter payout amount!");
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
                //refresh();

                this.Close();
            }
        }

        //private bool Update_records()
        //{
        //    try
        //    {
        //        fillVariableValues();

        //        TextData.date = txtDate.Text;


        //        if (txtShift.Text != "")
        //        {
        //            if (txtCounter.Text != "")
        //            {
        //                if (txtAmount.Text != "")
        //                {
        //                    int counter_id = data.UserPermissionsIds("id", "pos_counter", "title", TextData.counter);
        //                    int shift_id = data.UserPermissionsIds("id", "pos_shift", "title", TextData.shift);


        //                    GetSetData.query = @"update pos_clock_in set date = '" + TextData.date + "',  amount = '" + TextData.amount + "', remarks = '" + TextData.remarks + "', shift_id = '" + shift_id.ToString() + "' , counter_id = '" + counter_id.ToString() + "' where (id = '" + TextData.clockIn_id + "');";
        //                    data.insertUpdateCreateOrDelete(GetSetData.query);

        //                    return true;

        //                }
        //                else
        //                {
        //                    error.errorMessage("Please enter the  amount!");
        //                    error.ShowDialog();
        //                }
        //            }
        //            else
        //            {
        //                error.errorMessage("Please select counter!");
        //                error.ShowDialog();
        //            }
        //        }
        //        else
        //        {
        //            error.errorMessage("Please select shift!");
        //            error.ShowDialog();
        //        }

        //        return false;
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        return false;
        //    }
        //}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (Update_records())
            //{
            //    done.DoneMessage("Updated Successfully!");
            //    done.ShowDialog();
            //}
        }

        private void form_payout_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
        }

        private void refresh()
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                time_text.Text = DateTime.Now.ToLongTimeString();
                txtFromUser.Text = "";
                txtToUser.Text = "";
                txtAmount.Text = "0";
                txtRemarks.Text = "";

                system_user_permissions();
                enableSaveButton();
                txtAmount.Select();
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


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAmount.Text, e);
        }

        private void txtAmount_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
