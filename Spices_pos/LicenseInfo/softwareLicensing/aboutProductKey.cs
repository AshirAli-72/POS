using FoxLearn.License;
using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.LicenseInfo.softwareLicensing
{
    public partial class aboutProductKey : Form
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

        public aboutProductKey()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        const int productCode = 1;

        private void btnOk_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("About License Form", "Exit...", role_id);
            using (formLoginForConfigureLicense reg = new formLoginForConfigureLicense())
            {
                reg.Show();
                this.Dispose();
            }
        }

        private void aboutProductKey_Load(object sender, EventArgs e)
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

        private void aboutProductKey_Shown(object sender, EventArgs e)
        {
            try
            {
                txtProductId.Text = ComputerInfo.GetComputerId();
                KeyManager km = new KeyManager(txtProductId.Text);
                FoxLearn.License.LicenseInfo lic = new FoxLearn.License.LicenseInfo();
                int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
                string productKey = lic.ProductKey;

                if (km.ValidKey(ref productKey))
                {
                    KeyValuesClass kv = new KeyValuesClass();

                    if (km.DisassembleKey(productKey, ref kv))
                    {
                        txtSerialKey.Text = productKey;

                        if (kv.Type == LicenseType.TRIAL)
                        {
                            txtLicenseType.Text = string.Format("{0} Days", (kv.Expiration - DateTime.Now.Date).Days);
                        }
                        else
                        {
                            txtLicenseType.Text = "Full";
                        }
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

    }
}
