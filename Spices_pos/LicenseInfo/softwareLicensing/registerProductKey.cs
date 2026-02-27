using FoxLearn.License;
using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using RefereningMaterial.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

namespace Spices_pos.LicenseInfo.softwareLicensing
{
    public partial class registerProductKey : Form
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

        public registerProductKey()
        {
            InitializeComponent();
        }

        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;


        private void registerProductKey_Load(object sender, EventArgs e)
        {
            try
            {
                //rootsguruadmin@123
                //7X9G1-0YZT4-EH509-D3XW3-06D5E-7WYB4

                //Run Migrations Scripts
                ClassRunMigrations.migrationScripts();

                //Run ReadWriteJson Files Functions
        
                ClassDefaultValuesSetInDB.InsertValuesInTableRegistrationAndPermissions();

                ClassCreateUpdateJsonFile.readWritePermissionsJsonFile();
                ClassCreateUpdateJsonFile.readWriteGeneralSettingsJsonFile();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void registerProductKey_Shown(object sender, EventArgs e)
        {
            try
            {
                txtProductId.Text = ComputerInfo.GetComputerId();
                txtPassword.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        const int productCode = 1;

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.query = "select password from pos_role where password = '" + txtPassword.Text + "';";
                //GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                //if (txtPassword.Text == "rootsguruadmin@123")
                //{
                    KeyManager km = new KeyManager(txtProductId.Text);
                    string productKey = txtSerialKey.Text;

                    if (km.ValidKey(ref productKey))
                    {
                        KeyValuesClass kv = new KeyValuesClass();

                        if (km.DisassembleKey(productKey, ref kv))
                        {
                            FoxLearn.License.LicenseInfo lic = new FoxLearn.License.LicenseInfo();
                            lic.ProductKey = productKey;
                            lic.FullName = "Smart Installment Management System";

                            if (kv.Type == LicenseType.TRIAL)
                            {
                                lic.Day = kv.Expiration.Day;
                                lic.Month = kv.Expiration.Month;
                                lic.Year = kv.Expiration.Year;

                            }

                            km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);

                            done.DoneMessage("License is successfully registered .....");
                            done.ShowDialog();
                            //MessageBox.Show("License is successfully registered .....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.Close();
                        }
                    }
                    else
                    {
                        error.errorMessage("Your serial key is invalid!");
                        error.ShowDialog();
                        //MessageBox.Show("Your serial key is invalid! .....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                //}
                //else
                //{
                //    error.errorMessage("Invalid password!");
                //    error.ShowDialog();

                //    txtPassword.Text = "";
                //    txtPassword.Focus();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            using (formLoginForConfigureLicense reg = new formLoginForConfigureLicense())
            {
                reg.Show();
                this.Dispose();
            }
        }
    }
}
