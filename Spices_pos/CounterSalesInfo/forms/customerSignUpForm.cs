using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Login_info.controllers;
using CounterSales_info.forms;
using Guna.UI2.WinForms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Products_info.forms
{
    public partial class customerSignUpForm : Form
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

        public customerSignUpForm()
        {
            InitializeComponent();

            date_reg_text.Text = DateTime.Now.ToLongDateString();

            txtFullName.Select();

            instance = this;

            selectedTextBox = 0;
        }


        public static customerSignUpForm instance;
        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        //form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        int selectedTextBox = 0;

        private void showbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string auto_generate_code(string condition)
        {
            string customerCode = "";

            try
            {
                GetSetData.query = @"SELECT top 1 customerCodes FROM pos_AllCodes order by id desc;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                GetSetData.Ids++;

                if (condition != "show")
                {
                    GetSetData.query = @"update pos_AllCodes set customerCodes = '" + GetSetData.Ids.ToString() + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }


                if (GetSetData.Ids > 0 && GetSetData.Ids < 10)
                {
                    GetSetData.Data = "100000000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 10 && GetSetData.Ids < 100)
                {
                    GetSetData.Data = "10000000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 100 && GetSetData.Ids < 1000)
                {
                    GetSetData.Data = "1000000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                else if (GetSetData.Ids >= 1000 && GetSetData.Ids < 10000)
                {
                    GetSetData.Data = "100000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000 && GetSetData.Ids < 100000)
                {
                    GetSetData.Data = "10000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 100000 && GetSetData.Ids < 1000000)
                {
                    GetSetData.Data = "1000";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 1000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "100";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "10";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }
                else if (GetSetData.Ids >= 10000000 && GetSetData.Ids < 10000000)
                {
                    GetSetData.Data = "1";
                    customerCode = GetSetData.Data + GetSetData.Ids.ToString();
                }

                return customerCode;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();

                return customerCode;
            }
        }

        private bool InsertCustomerDetails()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("mobile_no", "pos_customers", "mobile_no", txtContactNumber.Text);

                if (txtFullName.Text != "")
                {
                    if (txtContactNumber.Text != "")
                    {
                        if ((GetSetData.Data == "") && (GetSetData.Data != txtContactNumber.Text))
                        {
                            string customerCode = auto_generate_code(""); // generating Customer Code

                            GetSetData.Ids = data.UserPermissionsIds("country_id", "pos_country", "title", "nill");
                            GetSetData.fks = data.UserPermissionsIds("city_id", "pos_city", "title", "nill");
                            int batchNo_id_db = data.UserPermissionsIds("batch_id", "pos_batchNo", "title", "nill");


                            GetSetData.query = @"insert into pos_customers values ('" + date_reg_text.Text + "' , '" + txtFullName.Text + "' , 'nill' , '" + customerCode + "' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '" + txtContactNumber.Text + "' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , 'nill' , '0' , '0' , '0' , 'nill' , '0' , 'Active' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "' , '" + batchNo_id_db.ToString() + "', '0');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + txtFullName.Text + "') and mobile_no = '" + txtContactNumber.Text + "';";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            GetSetData.query = @"insert into pos_customer_lastCredits (lastCredits, due_days, customer_id) values ('0' , '" + date_reg_text.Text + "' , '" + GetSetData.Ids.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);


                            //***********************************************************

                            if (IsFormOpen(typeof(form_counter_sales)))
                            {
                                form_counter_sales.instance.BeginInvoke((MethodInvoker)delegate
                                {
                                    form_counter_sales.instance.instanceCustomerName.Text = txtFullName.Text;

                                    GetSetData.query = "select cus_code from pos_customers where (full_name = '" + txtFullName.Text + "') and (mobile_no = '" + txtContactNumber.Text + "');";
                                    form_counter_sales.instance.instanceCustomerCode.Text = data.SearchStringValuesFromDb(GetSetData.query);


                                    //GetSetData.query = "select points from pos_customers where (full_name = '" + txtFullName.Text + "') and (mobile_no = '" + txtContactNumber.Text + "');";
                                    //form_counter_sales.instance.instanceCustomerPoints.Text = data.SearchStringValuesFromDb(GetSetData.query);


                                    form_counter_sales.instance.calculateAmountDue();

                                });
                            }

                            return true;
                        }
                        else
                        {
                            error.errorMessage("'" + txtContactNumber.Text + "' is already exist!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter the mobile Number!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter customer full name!");
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

        private bool IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formType)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (InsertCustomerDetails())
            {
                done.DoneMessage("Successfully Sign Up!");
                done.Show();

                TextData.invoiceCustomerName = txtFullName.Text;
                TextData.customerCellNumber = txtContactNumber.Text;

                if (generalSettings.ReadField("salesmanTips") == "Yes")
                {
                    if (Screen.AllScreens.Length > 1)
                    {
                        if (!IsFormOpen(typeof(form_salesman_tips)))
                        {
                            form_salesman_tips secondaryForm = new form_salesman_tips();

                            Screen secondaryScreen = Screen.AllScreens[1];
                            secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                            secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                            secondaryForm.WindowState = FormWindowState.Maximized;
                            secondaryForm.Show();
                        }
                    }
                }
                this.Close();
            }
        }

        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void guna2Button31_Click(object sender, EventArgs e)
        {
            if (txta.Text == "A")
            {
                txtq.Text = "q";
                txtw.Text = "w";
                txte.Text = "e";
                txtr.Text = "r";
                txtt.Text = "t";
                txty.Text = "y";
                txtu.Text = "u";
                txti.Text = "i";
                txto.Text = "o";
                txtp.Text = "p";
                txta.Text = "a";
                txts.Text = "s";
                txtd.Text = "d";
                txtf.Text = "f";
                txtg.Text = "g";
                txth.Text = "h";
                txtj.Text = "j";
                txtk.Text = "k";
                txtl.Text = "l";
                txtz.Text = "z";
                txtx.Text = "x";
                txtc.Text = "c";
                txtv.Text = "v";
                txtb.Text = "b";
                txtn.Text = "n";
                txtm.Text = "m";
            }
            else
            {
                txtq.Text = "Q";
                txtw.Text = "W";
                txte.Text = "E";
                txtr.Text = "R";
                txtt.Text = "T";
                txty.Text = "Y";
                txtu.Text = "U";
                txti.Text = "I";
                txto.Text = "O";
                txtp.Text = "P";
                txta.Text = "A";
                txts.Text = "S";
                txtd.Text = "D";
                txtf.Text = "F";
                txtg.Text = "G";
                txth.Text = "H";
                txtj.Text = "J";
                txtk.Text = "K";
                txtl.Text = "L";
                txtz.Text = "Z";
                txtx.Text = "X";
                txtc.Text = "C";
                txtv.Text = "V";
                txtb.Text = "B";
                txtn.Text = "N";
                txtm.Text = "M";
            }
        }

        private void guna2Button39_Click(object sender, EventArgs e)
        {
            if (selectedTextBox == 0)
            {
                if (txtFullName.Text.Length > 0)
                {
                    txtFullName.Text = txtFullName.Text.Remove(txtFullName.Text.Length - 1);
                }
            }
            else
            {
                if (txtContactNumber.Text.Length > 0)
                {
                    txtContactNumber.Text = txtContactNumber.Text.Remove(txtContactNumber.Text.Length - 1);
                }
            }
        }

        private void txtFullName_Click(object sender, EventArgs e)
        {
            selectedTextBox = 0;
        }

        private void txtContactNumber_Click(object sender, EventArgs e)
        {
            selectedTextBox = 1;
        }

        private void txt1_Click(object sender, EventArgs e)
        {
            Guna2Button button = sender as Guna2Button; // Cast the sender to Button

            if (button != null)
            {
                if (selectedTextBox == 0)
                {
                    txtFullName.Text += button.Text;
                }
                else
                {
                    txtContactNumber.Text += button.Text; // Append button text to TextBox
                }
            }
        }

        private void txtspace_Click(object sender, EventArgs e)
        {
            if (selectedTextBox == 0)
            {
                txtFullName.Text += " ";
            }
            else
            {
                txtContactNumber.Text += " "; // Append button text to TextBox
            }
        }
    }
}
