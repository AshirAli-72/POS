using System;
using System.Windows.Forms;
using FoxLearn.License;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.LicenseInfo.softwareLicensing
{
    public partial class generateProductKey : Form
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

        public generateProductKey()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.query = "select password from pos_role where password = '" + txtPassword.Text + "';";
                //GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                if (txtPassword.Text == "rootsguruadmin@123")
                {
                    KeyManager km = new KeyManager(txtProductId.Text);
                    KeyValuesClass kv;
                    string productKey = string.Empty;

                    if (txtLicenseType.SelectedIndex == 0)
                    {
                        kv = new KeyValuesClass()
                        {
                            Type = LicenseType.FULL,
                            Header = Convert.ToByte(9),
                            Footer = Convert.ToByte(6),
                            ProductCode = (byte)ProductCode,
                            Edition = Edition.ENTERPRISE,
                            Version = 1
                        };
                        if (!km.GenerateKey(kv, ref productKey))
                        {
                            txtSerialKey.Text = "ERROR";
                        }
                    }
                    else
                    {
                        kv = new KeyValuesClass()
                        {
                            Type = LicenseType.TRIAL,
                            Header = Convert.ToByte(9),
                            Footer = Convert.ToByte(6),
                            ProductCode = (byte)productCode,
                            Edition = Edition.ENTERPRISE,
                            Version = 1,
                            Expiration = DateTime.Now.AddDays(Convert.ToInt32(txtDays.Text))
                        };
                        if (!km.GenerateKey(kv, ref productKey))
                        {
                            txtSerialKey.Text = "ERROR";
                        }
                    }

                    txtSerialKey.Text = productKey;
                }
                else
                {
                    error.errorMessage("Invalid Password!");
                    error.ShowDialog();

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

        const int productCode = 1;

        private void generateProductKey_Load(object sender, EventArgs e)
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

        private void generateProductKey_Shown(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Select();
                txtLicenseType.SelectedIndex = 0;
                txtProductId.Text = ComputerInfo.GetComputerId();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (formLoginForConfigureLicense reg = new formLoginForConfigureLicense())
            {
                reg.Show();
                this.Dispose();
            }
        }

        public byte ProductCode { get; set; }

    }
}
