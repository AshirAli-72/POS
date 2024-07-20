using System;
using System.Windows.Forms;
using Spices_pos.LoginInfo.forms;
using FoxLearn.License;
using Spices_pos.LicenseInfo.softwareLicensing;

namespace Spices_pos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new login_form());

            try
            {
                string txtProductId = "";
                string txtLicenseType = "";

                txtProductId = ComputerInfo.GetComputerId();
                KeyManager km = new KeyManager(txtProductId);
                FoxLearn.License.LicenseInfo lic = new FoxLearn.License.LicenseInfo();
                int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
                string productKey = lic.ProductKey;
                double days = 0;

                if (km.ValidKey(ref productKey))
                {
                    KeyValuesClass kv = new KeyValuesClass();

                    if (km.DisassembleKey(productKey, ref kv))
                    {
                        if (kv.Type == LicenseType.TRIAL)
                        {
                            txtLicenseType = string.Format("{0}", (kv.Expiration - DateTime.Now.Date).Days);
                            days = double.Parse(txtLicenseType);
                        }
                        else
                        {
                            txtLicenseType = "Full";
                        }
                    }
                }

                if ((txtLicenseType != "" && days > 0) || txtLicenseType == "Full")
                {
                    Application.Run(new login_form());
                }
                else
                {
                    Application.Run(new registerProductKey());
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
