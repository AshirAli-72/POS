using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using RefereningMaterial.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.LicenseInfo.softwareLicensing
{
    public partial class formLoginForConfigureLicense : Form
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
        public formLoginForConfigureLicense()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int count = 0;


        private void btnClose_Click(object sender, EventArgs e)
        {
            switch (count)
            {
                case 1:
                    this.Close();
                    break;

                default:
                    Application.Exit();
                    break;
            }
        }

        private void formLoginForConfigureLicense_Load(object sender, EventArgs e)
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                txtUserName.Select();
                ClassDefaultValuesSetInDB.InsertValuesInTableRegistrationAndPermissions();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != "" && txtPassword.Text != "")
                {
                    if (txtUserName.Text == "sa" && txtPassword.Text == "rootsguruadmin@123")
                    {
                        using (generateProductKey generate = new generateProductKey())
                        {
                            generate.ShowDialog();
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtPassword.Focus();
                        }
                    }
                    else
                    {
                        error.errorMessage("Invalid username or password!");
                        error.ShowDialog();
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                }
                else
                {
                    error.errorMessage("Please enter username and password!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (registerProductKey reg = new registerProductKey())
                {
                    reg.ShowDialog();
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (aboutProductKey reg = new aboutProductKey())
            {
                reg.ShowDialog();
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }
    }
}
