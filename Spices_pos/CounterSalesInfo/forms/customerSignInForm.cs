using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using CounterSales_info.forms;
using Guna.UI2.WinForms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Products_info.forms
{
    public partial class customerSignInForm : Form
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

        public customerSignInForm()
        {
            InitializeComponent();

            txtContactNumber.Select();

            instance = this;
        }

        public static customerSignInForm instance;

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;

       
        private void showbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                GetSetData.query = "select customer_id from pos_customers where (mobile_no = '" +   txtContactNumber.Text + "');";
                string is_cell_number_exist = data.SearchStringValuesFromDb(GetSetData.query);

                if (is_cell_number_exist != "")
                {
                    if (IsFormOpen(typeof(form_counter_sales)))
                    {
                        form_counter_sales.instance.BeginInvoke((MethodInvoker)delegate
                        {
                            form_counter_sales.instance.instanceCustomerName.Text = data.UserPermissions("full_name", "pos_customers", "customer_id", is_cell_number_exist);
                            form_counter_sales.instance.instanceCustomerCode.Text = data.UserPermissions("cus_code", "pos_customers", "customer_id", is_cell_number_exist);

                            form_counter_sales.instance.calculateAmountDue();
                        });
                    }
                    //form_counter_sales.instance.instanceCustomerName.Text = data.UserPermissions("full_name", "pos_customers", "customer_id", is_cell_number_exist);
                    //form_counter_sales.instance.instanceCustomerCode.Text = data.UserPermissions("cus_code", "pos_customers", "customer_id", is_cell_number_exist);

                    //form_counter_sales.instance.calculateAmountDue();

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
                else
                {
                    error.errorMessage("Invalid cell number. Please try other cell number!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
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

        private void txtContactNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Guna2Button button = sender as Guna2Button; // Cast the sender to Button

            if (button != null)
            {
                txtContactNumber.Text += button.Text; // Append button text to TextBox
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtContactNumber.Text = "";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtContactNumber.Text.Length > 0)
            {
                txtContactNumber.Text = txtContactNumber.Text.Remove(txtContactNumber.Text.Length - 1);
            }
        }
    }
}
