using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Guna.UI2.WinForms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Customers_info.forms
{
    public partial class formCustomerAge : Form
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

        public formCustomerAge()
        {
            InitializeComponent();

            txtCustomerAge.Select();

            instance = this;
        }

        public static formCustomerAge instance;

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static string customerId = "";

       
        private void showbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {

                if (customerId != "")
                {
                    if (txtCustomerAge.Text  != "")
                    {
                        int ageLimit = data.UserPermissionsIds("customerAgeLimit", "pos_general_settings");

                        if (Convert.ToInt32(txtCustomerAge.Text) >= ageLimit)
                        {
                            GetSetData.query = @"update pos_customers set age = '" + txtCustomerAge.Text + "' where (customer_id = '" + customerId + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                        else
                        {
                            error_form _obj = new error_form();
                            Screen primaryScreen = Screen.PrimaryScreen; // Get the primary screen
                            _obj.StartPosition = FormStartPosition.CenterScreen;
                            _obj.Location = primaryScreen.WorkingArea.Location;
                            _obj.WindowState = FormWindowState.Normal;
                            _obj.errorMessage("This customer is under age. Please verify the customer age first.");
                            _obj.ShowDialog();
                        }

                        this.Close();
                    }
                    else
                    {
                        error_form _obj = new error_form();
                        Screen secondaryScreen = Screen.AllScreens[1];
                        _obj.StartPosition = FormStartPosition.CenterScreen;
                        _obj.Location = secondaryScreen.WorkingArea.Location;
                        _obj.WindowState = FormWindowState.Normal;
                        _obj.errorMessage("Please enter your age first!");
                        _obj.Show();
                    }
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
                txtCustomerAge.Text += button.Text; // Append button text to TextBox
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCustomerAge.Text = "";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtCustomerAge.Text.Length > 0)
            {
                txtCustomerAge.Text = txtCustomerAge.Text.Remove(txtCustomerAge.Text.Length - 1);
            }
        }
    }
}
