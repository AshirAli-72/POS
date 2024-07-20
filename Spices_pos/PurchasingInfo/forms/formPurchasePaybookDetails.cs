using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Reports_info.Recoveries;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;

namespace Purchase_info.forms
{
    public partial class formPurchasePaybookDetails : Form
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


        public formPurchasePaybookDetails()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;

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

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
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
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplierPaybook_print", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplierPaybook_new", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplierPaybook_delete", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("supplierPaybook_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                sidePanel.Visible = true;
                setSideBarInSidePanel();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void setSideBarInSidePanel()
        {
            try
            {
                sidebarUserControl sidebar = new sidebarUserControl();

                sidebar.role_id = role_id;

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
                sidebar.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewSupplierPaymentDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where [Date] like '" + search_box.Text + "%' or [Supplier] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or [Mobile No] like '" + search_box.Text + "%';";
                }

                GetSetData.FillDataGridViewUsingPagination(CustomerDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formPurchasePaybookDetails_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings.user_id = user_id;
            settings main = new settings();
            main.Show();
            this.Dispose();
        }

        private void addNewDetails()
        {
            using (formPurchasePayment add_customer = new formPurchasePayment())
            {
                //GetSetData.SaveLogHistoryDetails("Supplier Paybook Detail Form", "Add new payment button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                formPurchasePayment.user_id = user_id;
                formPurchasePayment.role_id = role_id;
                formPurchasePayment.saveEnable = false;
                add_customer.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Supplier Paybook Detail Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            formSupplierPaymentReport report = new formSupplierPaymentReport();
            report.ShowDialog();
        }

        private bool fun_delete_recovery_details()
        {
            try
            {
                TextData.dates = CustomerDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.times = CustomerDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                TextData.paid = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Payment"].Value.ToString());

                GetSetData.query = @"select supplier_id from pos_suppliers where (full_name = '" + TextData.full_name.ToString() + "') and (code = '" + TextData.cus_code.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                TextData.lastCredits = data.NumericValues("previous_payables", "pos_supplier_payables", "supplier_id", GetSetData.Ids.ToString());
                TextData.lastCredits += TextData.paid;

                GetSetData.query = @"update pos_supplier_payables set previous_payables = '" + TextData.lastCredits.ToString() + "' where supplier_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "delete from pos_supplier_paybook where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "') and (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "delete from pos_company_transactions where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "') and (supplier_id = '" + GetSetData.Ids.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Supplier Paybook Detail Form", "Deleting payment [" + TextData.dates + "  " + TextData.times + "] details", role_id);
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                // MessageBox.Show(es.Message);
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.full_name.ToString() + "' payment!");
                sure.ShowDialog();
                this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_recovery_details();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    search_box.Text = "";
                }
            }
            catch (Exception es)
            {
                error.errorMessage("'" + TextData.full_name.ToString() + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(CustomerDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void ModifyPurchasingDetails()
        {
            try
            {
                //GetSetData.Data = data.UserPermissions("purchase_details_refresh", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());

                //if (GetSetData.Data == "True")
                //{
                formPurchasePayment.saveEnable = true;
                TextData.full_name = CustomerDetailGridView.SelectedRows[0].Cells["Supplier"].Value.ToString();
                TextData.cus_code = CustomerDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                TextData.dates = CustomerDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.times = CustomerDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.employee = CustomerDetailGridView.SelectedRows[0].Cells["Employee"].Value.ToString();
                TextData.paid = double.Parse(CustomerDetailGridView.SelectedRows[0].Cells["Payment"].Value.ToString());
                TextData.reference = CustomerDetailGridView.SelectedRows[0].Cells["References"].Value.ToString();
                TextData.remarks = CustomerDetailGridView.SelectedRows[0].Cells["Description"].Value.ToString();

                //GetSetData.SaveLogHistoryDetails("Supplier Paybook Detail Form", "Updating payment [" + TextData.dates + "  " + TextData.times + "] details (Modify button click...)", role_id);

                using (formPurchasePayment add_customer = new formPurchasePayment())
                {
                    formPurchasePayment.user_id = user_id;
                    formPurchasePayment.role_id = role_id;
                    add_customer.ShowDialog();
                }
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void updates_purchase_details(object sender, DataGridViewCellEventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void formPurchasePaybookDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Supplier Paybook Detail Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                formSupplierPaymentReport report = new formSupplierPaymentReport();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                addNewDetails();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                ModifyPurchasingDetails();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
