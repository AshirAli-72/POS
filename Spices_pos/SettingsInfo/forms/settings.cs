using System;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Purchase_info.forms;
using Banks_Loan_info.forms;
using Investors_info.forms;
using Recoverier_info.forms;
using Settings_info.forms.RolesAndPermissions;
using Expenses_info.forms;
using Products_info.forms.RecipeDetails;
using Banking_info.forms;
using Products_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.LicenseInfo.forms;
using Spices_pos.LicenseInfo.softwareLicensing;
using Spices_pos.CashManagement.Forms;

namespace Settings_info.forms
{
    public partial class settings : Form
    {
        //int originalExStyle = -1;
        //bool enableFormLevelDoubleBuffering = true;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        if (originalExStyle == -1)
        //            originalExStyle = base.CreateParams.ExStyle;

        //        CreateParams handleParam = base.CreateParams;

        //        if (enableFormLevelDoubleBuffering)
        //        {
        //            handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
        //        }
        //        else
        //        {
        //            handleParam.ExStyle = originalExStyle;
        //        }

        //        return handleParam;
        //    }
        //}

        //public void TrunOffFormLevelDoubleBuffering()
        //{
        //    enableFormLevelDoubleBuffering = false;
        //    this.MinimizeBox = true;
        //    this.WindowState = FormWindowState.Minimized;
        //}

        public settings()
        {
            InitializeComponent();
            //setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int user_id = 0;
        public static int role_id = 0;

        //private void setFormColorsDynamically()
        //{
        //    try
        //    {
        //        //int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
        //        //int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
        //        //int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

        //        //int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
        //        //int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
        //        //int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

        //        //int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
        //        //int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
        //        //int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

        //        //****************************************************************

        //        //GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
        //        //GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
        //        //GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
        //        //GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

        //        //****************************************************************

        //        //GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
        //        //GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}

        private void system_user_permissions()
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("settings_reg", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                btn_registration.Visible = bool.Parse(GetSetData.Data);
                btnUsers.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("settings_config", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                btnImportProductsExcel.Visible = bool.Parse(GetSetData.Data);

           
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("settings_reports", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                btn_reports.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("settings_login_details", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                btn_login_details.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("settings_general", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                btn_options.Visible = bool.Parse(GetSetData.Data);


                // New Buttons ***************************************************************************************************
                btnInvestors.Visible = bool.Parse(data.UserPermissions("customers", "pos_tbl_authorities_dashboard", role_id));
               
                //***************************************************************************************************
                btnInvestorPaybook.Visible = bool.Parse(data.UserPermissions("employee", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btn_db_backup.Visible= bool.Parse(data.UserPermissions("backups", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnRestore.Visible = bool.Parse(data.UserPermissions("restores", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnAbout.Visible = bool.Parse(data.UserPermissions("aboutLicense", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnBankLoan.Visible = bool.Parse(data.UserPermissions("bankLoan", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnLoanPaybook.Visible = bool.Parse(data.UserPermissions("bankLoanPaybook", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btn_bank.Visible = bool.Parse(data.UserPermissions("about", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                recover_btn.Visible = bool.Parse(data.UserPermissions("recoveries", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnSalaries.Visible = bool.Parse(data.UserPermissions("EmployeeSalaries", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                btnSupplierPaybook.Visible = bool.Parse(data.UserPermissions("supplierPaybook", "pos_tbl_authorities_dashboard", role_id));

                //***************************************************************************************************
                expense_btn.Visible = bool.Parse(data.UserPermissions("expenses", "pos_tbl_authorities_dashboard", role_id));

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Settings Form", "Exit...", role_id);
            buttonControls.mainMenu_buttons();
            this.Dispose();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    originalExStyle = -1;
            //    enableFormLevelDoubleBuffering = true;
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void settings_Shown(object sender, EventArgs e)
        {
            try
            {
                system_user_permissions();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_registration_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "Registration button click...", role_id);
                formRegistration.role_id = role_id;
                formRegistration reg = new formRegistration();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btn_configuration_Click(object sender, EventArgs e)
        {

        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            try
            {
                //    GetSetData.SaveLogHistoryDetails("Settings Form", "Reports Settings button click...", role_id);
                formReportsSettings.role_id = role_id;
                formReportsSettings reg = new formReportsSettings();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btn_login_details_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "User Attendance button click...", role_id);
                formAttendance.role_id = role_id;
                formAttendance reg = new formAttendance();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btn_options_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "General options button click...", role_id);
                formGeneralOptions.role_id = role_id;
                formGeneralOptions reg = new formGeneralOptions();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btnContractPolicies_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "Contract Policies button click...", role_id);
                formContractPolicies.role_id = role_id;
                formContractPolicies reg = new formContractPolicies();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btnImportProductsExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "User Attendance button click...", role_id);
                formImportExcelFile.user_id = user_id;
                formImportExcelFile.role_id = role_id;
                formImportExcelFile reg = new formImportExcelFile();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            form_bank_details.role_id = role_id;
            form_bank_details.user_id = user_id;
            form_bank_details supplier = new form_bank_details();
            supplier.Show();

            this.Dispose();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            form_recovery_list.user_id = user_id;
            form_recovery_list.role_id = role_id;
            form_recovery_list _obj = new form_recovery_list();
            _obj.Show();
            this.Dispose();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            using (formLoginForConfigureLicense add_customer = new formLoginForConfigureLicense())
            {
                aboutProductKey.role_id = role_id;
                formLoginForConfigureLicense.count = 1;
                add_customer.ShowDialog();
                //button_controls.AboutSoftwareLicense();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (form_backup_db add_customer = new form_backup_db())
            {
                form_backup_db.role_id = role_id;
                add_customer.ShowDialog();
                //button_controls.Database_backup_btn();
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            using (form_restore_db add_customer = new form_restore_db())
            {
                form_restore_db.role_id = role_id;
                add_customer.ShowDialog();
                //button_controls.Database_Restore_btn();
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            formSalaryPaymentDetails.user_id = user_id;
            formSalaryPaymentDetails.role_id = role_id;
            formSalaryPaymentDetails reports = new formSalaryPaymentDetails();
            reports.Show();
            this.Dispose();
        }

        private void btnBankLoan_Click(object sender, EventArgs e)
        {
            formBanksLoansDetails.user_id = user_id;
            formBanksLoansDetails.role_id = role_id;
            formBanksLoansDetails reports = new formBanksLoansDetails();
            reports.Show();
            this.Dispose();
        }

        private void btnLoanPaybook_Click(object sender, EventArgs e)
        {
            formBankLoanPaybookDetails.user_id = user_id;
            formBankLoanPaybookDetails.role_id = role_id;
            formBankLoanPaybookDetails reports = new formBankLoanPaybookDetails();
            reports.Show();
            this.Dispose();
        }

        private void btnInvestors_Click(object sender, EventArgs e)
        {
            formInvestorDetails.user_id = user_id;
            formInvestorDetails.role_id = role_id;
            formInvestorDetails customer = new formInvestorDetails();
            customer.Show();
            this.Dispose();
        }

        private void btnInvestorPaybook_Click(object sender, EventArgs e)
        {
            formInvestorsPaybookDetails.user_id = user_id;
            formInvestorsPaybookDetails.role_id = role_id;
            formInvestorsPaybookDetails supplier = new formInvestorsPaybookDetails();
            supplier.Show();
            this.Dispose();
        }


        private void btnCustomerDues_Click(object sender, EventArgs e)
        {
            formPurchasePaybookDetails.user_id = user_id;
            formPurchasePaybookDetails.role_id = role_id;
            formPurchasePaybookDetails dues = new formPurchasePaybookDetails();
            dues.Show();
            this.Dispose();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UserDetails main = new UserDetails();
            UserDetails.role_id = role_id;
            main.Show();
            this.Dispose();
        }

        private void btnEmployeeCommission_Click(object sender, EventArgs e)
        {
            commission_details.role_id = role_id;
            commission_details.user_id = user_id;
            commission_details _obj = new commission_details();
            _obj.Show();
            this.Dispose();
        }

        private void expense_btn_Click(object sender, EventArgs e)
        {
            Expenses_details.role_id = role_id;
            Expenses_details.user_id = user_id;
            Expenses_details _obj = new Expenses_details();
            _obj.Show();
            this.Dispose();
        }

        private void btnMinMaxScreen_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnDefaultShifts_Click(object sender, EventArgs e)
        {
            try
            {
                //GetSetData.SaveLogHistoryDetails("Settings Form", "User Attendance button click...", role_id);
                defaultShiftsDetails.role_id = role_id;
                defaultShiftsDetails reg = new defaultShiftsDetails();
                reg.Show();
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btnCreditCardApi_Click(object sender, EventArgs e)
        {
            try
            {
                creditCardApiSettings.role_id = role_id;
                creditCardApiSettings reg = new creditCardApiSettings();
                reg.ShowDialog();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void btnExportDataToDrive_Click(object sender, EventArgs e)
        {
            try
            {
                exportFileToDrive.role_id = role_id;
                exportFileToDrive reg = new exportFileToDrive();
                reg.ShowDialog();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
            }
        }

        private void settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnAgeRestrictedItems_Click(object sender, EventArgs e)
        {
            formAgeRestrictedProducts.role_id = role_id;
            formAgeRestrictedProducts.user_id = user_id;

            formAgeRestrictedProducts _obj = new formAgeRestrictedProducts();
            _obj.Show();

            this.Dispose();
        }

        private void btnExportExcelFile_Click(object sender, EventArgs e)
        {
            exportWeblinkExcelFiles.role_id = role_id;
            exportWeblinkExcelFiles reg = new exportWeblinkExcelFiles();
            reg.ShowDialog();
        }

        private void btnManagement_Click(object sender, EventArgs e)
        {

          /*  Cash_Management_Details.role_id = role_id;
           Cash_Management_Details.user_id = user_id;
           Cash_Management_Details cash = new Cash_Management_Details();
            cash.Show();

            this.Dispose();*/

        }
    }
}
