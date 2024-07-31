using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Settings_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Settings_info.controllers;
using Reports_info.Recoveries;

namespace Recoverier_info.forms
{
    public partial class form_recovery_list : Form
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

        public form_recovery_list()
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
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnGenerateInvoices);
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
                Customer_sales_recovery.user_id = user_id;
                Customer_sales_recovery.role_id = role_id;
                formCustomerWiseBillsDetails.role_id = role_id;
                //GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recovery_details_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recovery_details_new", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recovery_details_delete", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("recovery_details_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("recovery_details_Invoices", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnlCustomerInvoices.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("generateInvoices", "pos_tbl_authorities_reports", "role_id", role_id.ToString());
                //btnGenerateInvoices.Visible = bool.Parse(GetSetData.Data);


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
                sidebar.user_id = user_id;

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
                GetSetData.query = "select * from ViewCustomerRecoveryDetails";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where [Date] like '" + search_box.Text + "%' or [Customer] like '" + search_box.Text + "%' or [Code] like '" + search_box.Text + "%' or [Mobile No] like '" + search_box.Text + "%';";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_recovery_list_Load(object sender, EventArgs e)
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
            using (Customer_sales_recovery add_customer = new Customer_sales_recovery())
            {
                //GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Add new recovery button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                Customer_sales_recovery.user_id = user_id;
                Customer_sales_recovery.role_id = role_id;
                Customer_sales_recovery.saveEnable = false;
                Customer_sales_recovery.get_customer = "";
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
            //GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            form_recoveries report = new form_recoveries();
            report.ShowDialog();
        }

        private bool fun_delete_recovery_details()
        {
            try
            {
                TextData.dates = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.times = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.cus_name = ProductsDetailGridView.SelectedRows[0].Cells["Customer"].Value.ToString();
                TextData.cus_code = ProductsDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                TextData.cash = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Paid Amount"].Value.ToString());

                GetSetData.query = @"select customer_id from pos_customers where (full_name = '" + TextData.cus_name.ToString() + "') and (cus_code = '" + TextData.cus_code.ToString() + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                TextData.lastCredits = data.NumericValues("lastCredits", "pos_customer_lastCredits", "customer_id", GetSetData.Ids.ToString());
                TextData.lastCredits += TextData.cash;

                GetSetData.query = @"update pos_customer_lastCredits set lastCredits = '" + TextData.lastCredits.ToString() + "' where customer_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "select recovery_id from pos_recovery_details where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "') and (customer_id = '" + GetSetData.Ids.ToString() +"');";
                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = "select invoiceNo from pos_recovery_details where (recovery_id = '" + GetSetData.fks.ToString() + "');";
                TextData.invoiceNo = data.SearchStringValuesFromDb(GetSetData.query);
                //========================================================

                GetSetData.query = "select format(installmentDate, 'dd/MMMM/yyyy') AS installmentDate from pos_recoveries where (recovery_id = '" + GetSetData.fks.ToString() + "');";
                TextData.installmentDate = data.SearchStringValuesFromDb(GetSetData.query);
                //========================================================

                GetSetData.query = "select installmentNo from pos_recoveries where (recovery_id = '" + GetSetData.fks.ToString() + "');";
                TextData.installmentNo = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = "select sales_acc_id from pos_sales_accounts where (billNo = '" + TextData.invoiceNo + "');";
                int sales_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = "select installment_acc_id from pos_installment_accounts where (sales_acc_id = '" + sales_acc_id_db.ToString() + "');";
                int installment_acc_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = "Update pos_installment_plan set status = 'Incomplete' where (installment_acc_id = '" + installment_acc_id_db.ToString() + "') and (installmentNo = '" + TextData.installmentNo.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "delete from pos_recoveries where (recovery_id = '" + GetSetData.fks.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "delete from pos_recovery_details where (recovery_id = '" + GetSetData.fks.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = "delete from pos_customer_transactions where (date = '" + TextData.dates.ToString() + "') and (time = '" + TextData.times.ToString() + "') and (customer_id = '" + GetSetData.Ids.ToString() + "');";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Deleting recovery [" + TextData.dates + "  " + TextData.times + "] details", role_id);
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
            TextData.cus_name = ProductsDetailGridView.SelectedRows[0].Cells["Customer"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete '" + TextData.cus_name.ToString() + "' recovery!");
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
                error.errorMessage("'" + TextData.cus_name.ToString() + "' cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
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
                Customer_sales_recovery.saveEnable = true;
                TextData.cus_name = ProductsDetailGridView.SelectedRows[0].Cells["Customer"].Value.ToString();
                TextData.cus_code = ProductsDetailGridView.SelectedRows[0].Cells["Code"].Value.ToString();
                TextData.dates = ProductsDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                TextData.installmentDate = ProductsDetailGridView.SelectedRows[0].Cells["Recovery Date"].Value.ToString();
                TextData.times = ProductsDetailGridView.SelectedRows[0].Cells["Time"].Value.ToString();
                TextData.employee = ProductsDetailGridView.SelectedRows[0].Cells["Received By"].Value.ToString();
                TextData.cash = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Paid Amount"].Value.ToString());
                TextData.credits = double.Parse(ProductsDetailGridView.SelectedRows[0].Cells["Credits"].Value.ToString());
                TextData.reference = ProductsDetailGridView.SelectedRows[0].Cells["References"].Value.ToString();
                TextData.description = ProductsDetailGridView.SelectedRows[0].Cells["Description"].Value.ToString();

                //GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Updating recovery [" + TextData.dates + "  " + TextData.times + "] details (Modify button click...)", role_id);

                using (Customer_sales_recovery add_customer = new Customer_sales_recovery())
                {
                    Customer_sales_recovery.user_id = user_id;
                    Customer_sales_recovery.role_id = role_id;
                    Customer_sales_recovery.get_customer = "";
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

        private void btnCustomerInvoices_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Customer invoices button click...", role_id);
            buttonControls.customersInvoicesButton();
        }

        private void btnGenerateInvoices_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Generate invoices button click...", role_id);
            //buttonControls.generateInvoicesButton();
        }

        private void form_recovery_list_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                form_recoveries report = new form_recoveries();
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
            else if (e.Control && e.KeyCode == Keys.I)
            {
                GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Customer invoices button click...", role_id);
                buttonControls.customersInvoicesButton();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                //GetSetData.SaveLogHistoryDetails("Customer Recoveries Detail Form", "Generate invoices button click...", role_id);
                //buttonControls.generateInvoicesButton();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
