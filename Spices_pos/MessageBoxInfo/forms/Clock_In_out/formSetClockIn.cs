using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using RefereningMaterial;
using System.Diagnostics;
using DataModel.Cash_Drawer_Data_Classes;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms.Clock_In
{
    public partial class formSetClockIn : Form
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

        public formSetClockIn()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int user_id = 0;
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);

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
                GetSetData.addFormCopyrights(lblCopyrights);
                //***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_save", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //savebutton.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_update", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //update_button.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("newCharityPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                //btnExit.Visible = bool.Parse(GetSetData.Data);
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
        
        private void fillComboBoxFromUsers()
        {
            GetSetData.FillComboBoxUsingProcedures(txtFromUser, "fillComboBoxUsers", "full_name");
        }
            private void fillComboBoxToUsers()
        {
            GetSetData.FillComboBoxUsingProcedures(txtToUser, "fillComboBoxUsers", "full_name");
        }
        
        private void fillComboBoxCounters()
        {
            GetSetData.FillComboBoxUsingProcedures(txtCounter, "fillComboBoxCounters", "title");
        }
          
        private void fillComboBoxShifts()
        {
            GetSetData.FillComboBoxUsingProcedures(txtShift, "fillComboBoxShifts", "title");
        }

        private void fillAddProductsFormTextBoxes()
        {
            fillComboBoxFromUsers();
            fillComboBoxToUsers();
            fillComboBoxShifts();
            fillComboBoxCounters();

            txtDate.Text = TextData.date;
            txtFromUser.Text = TextData.fromUser;
            txtToUser.Text = TextData.toUses;
            txtShift.Text = TextData.shift;
            txtCounter.Text = TextData.counter;
            txtAmount.Text = TextData.cashAmount;
            txtRemarks.Text = TextData.remarks;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Your Day Details";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Start Your Day";
            }
        }

        private void fillVariableValues()
        {
            try
            {
                TextData.fromUser = txtFromUser.Text;
                TextData.shift = txtShift.Text;
                TextData.toUses = txtToUser.Text;
                TextData.counter = txtCounter.Text;
                TextData.remarks = txtRemarks.Text;
                TextData.cashAmount = txtAmount.Text;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private bool insert_records()
        {
            try
            {
                fillVariableValues();


                if (txtShift.Text != "")
                {
                    if (txtCounter.Text != "")
                    {
                        if (txtAmount.Text != "")
                        {
                            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (to_user_id = '" + user_id.ToString() + "') and (status = '-1') ORDER BY id DESC;";
                            double clock_in_id = data.SearchNumericValuesDb(GetSetData.query);

                            if (clock_in_id != 0)
                            {
                                int counter_id = data.UserPermissionsIds("id", "pos_counter", "title", TextData.counter);
                                int shift_id = data.UserPermissionsIds("id", "pos_shift", "title", TextData.shift);
                                string start_date = data.UserPermissions("date", "pos_clock_in", "id", clock_in_id.ToString());


                                GetSetData.query = "select id from pos_clock_in where (date = '" + start_date + "') and (shift_id = '" + shift_id.ToString() + "') and (counter_id = '" + counter_id.ToString() + "');";
                                double is_already_clock_in = data.SearchNumericValuesDb(GetSetData.query);


                                if (is_already_clock_in == 0)
                                {
                                    GetSetData.query = @"update pos_clock_in set amount = '" + TextData.cashAmount + "', remarks = '" + TextData.remarks + "', status = '0', total100s = '" + txt100.Text + "', total50s = '" + txt50.Text + "', total20s = '" + txt20.Text + "', total10s = '" + txt10.Text + "', total5s = '" + txt5.Text + "', total2s = '" + txt2.Text + "', total1s = '" + txt1.Text + "', total1c = '" + txt1c.Text + "', total5c = '" + txt5c.Text + "', total10c = '" + txt10c.Text + "', total25c = '" + txt25c.Text + "', shift_id = '" + shift_id.ToString() + "' , counter_id = '" + counter_id.ToString() + "' where (id = '" + clock_in_id + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("Sorry this shift and counter is already booked!");
                                    error.ShowDialog();
                                }
                            }

                            return false;
                        }
                        else
                        {
                            error.errorMessage("Please enter the  amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select counter!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select shift!");
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

                this.Close();
            }
        }

        private bool Update_records()
        {
            try
            {
                fillVariableValues();

                TextData.date = txtDate.Text;


                if (txtShift.Text != "")
                {
                    if (txtCounter.Text != "")
                    {
                        if (txtAmount.Text != "")
                        {
                            int counter_id = data.UserPermissionsIds("id", "pos_counter", "title", TextData.counter);
                            int shift_id = data.UserPermissionsIds("id", "pos_shift", "title", TextData.shift);


                            GetSetData.query = @"update pos_clock_in set date = '" + TextData.date + "',  amount = '" + TextData.cashAmount + "', remarks = '" + TextData.remarks + "', shift_id = '" + shift_id.ToString() + "' , counter_id = '" + counter_id.ToString() + "' where (id = '" + TextData.clockIn_id + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            return true;

                        }
                        else
                        {
                            error.errorMessage("Please enter the  amount!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select counter!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select shift!");
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

        private void formSetClockIn_Load(object sender, EventArgs e)
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
                txtShift.Text = "";
                txtCounter.Text = "";
                txtAmount.Text = "0";
                txtRemarks.Text = "";

                fillComboBoxFromUsers();
                fillComboBoxToUsers();
                fillComboBoxShifts();
                fillComboBoxCounters();

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

        private void btnShift_Click(object sender, EventArgs e)
        {
            try
            {
                using (formShifts customer = new formShifts())
                {
                    formShifts.role_id = role_id;
                    formShifts.title = txtShift.Text;
                    customer.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            try
            {
                using (formCounters customer = new formCounters())
                {
                    formCounters.role_id = role_id;
                    formCounters.title = txtCounter.Text;
                    customer.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtRemarks_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void autoOpenCashDrawer()
        {
            try
            {
                GetSetData.query = @"select default_printer from pos_general_settings;";
                string printer_name = data.SearchStringValuesFromDb(GetSetData.query);


                GetSetData.query = @"select printer_model from pos_general_settings;";
                string printer_model = data.SearchStringValuesFromDb(GetSetData.query);


                CashDrawerData.OpenDrawer(printer_name, printer_model);
            }
            catch (Exception es)
            {
                error.errorMessage("Error opening cash drawer: " + es.Message);
                error.ShowDialog();
            }
        }
        private void btnCashDrawer_Click(object sender, EventArgs e)
        {
            autoOpenCashDrawer();
        }

        private void txt10_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void txt10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int total100s = 0;
                int total50s = 0;
                int total20s = 0;
                int total10s = 0;
                int total5s = 0;
                int total2s = 0;
                int total1s = 0;


                if (txt100.Text != "")
                {
                    total100s = Convert.ToInt32(txt100.Text);
                }

                if (txt50.Text != "")
                {
                    total50s = Convert.ToInt32(txt50.Text);
                }

                if (txt20.Text != "")
                {
                    total20s = Convert.ToInt32(txt20.Text);
                }

                if (txt10.Text != "")
                {
                    total10s = Convert.ToInt32(txt10.Text);
                }

                if (txt5.Text != "")
                {
                    total5s = Convert.ToInt32(txt5.Text);
                }

                if (txt2.Text != "")
                {
                    total2s = Convert.ToInt32(txt2.Text);
                }

                if (txt1.Text != "")
                {
                    total1s = Convert.ToInt32(txt1.Text);
                }


                txtDollars.Text = ((total100s * 100) + (total50s * 50) + (total20s * 20) + (total10s * 10) + (total5s * 5) + (total2s * 2) + (total1s * 1)).ToString();


                txtTotalDollars.Text = Math.Round((double.Parse(txtDollars.Text) + double.Parse(txtTotalCents.Text)), 2).ToString();
                txtAmount.Text = Math.Round((double.Parse(txtDollars.Text) + double.Parse(txtTotalCents.Text)), 2).ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txt1c_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int total25c = 0;
                int total10c = 0;
                int total5c = 0;
                int total1c = 0;

                if (txt25c.Text != "")
                {
                    total25c = Convert.ToInt32(txt25c.Text);
                }

                if (txt10c.Text != "")
                {
                    total10c = Convert.ToInt32(txt10c.Text);
                }

                if (txt5c.Text != "")
                {
                    total5c = Convert.ToInt32(txt5c.Text);
                }

                if (txt1c.Text != "")
                {
                    total1c = Convert.ToInt32(txt1c.Text);
                }

                double totalCents = ((total25c * 25) + (total10c * 10) + (total5c * 5) + (total1c * 1));

                txtTotalCents.Text = (totalCents / 100).ToString();

                txtTotalDollars.Text = Math.Round((double.Parse(txtDollars.Text) + (totalCents / 100)), 2).ToString();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
