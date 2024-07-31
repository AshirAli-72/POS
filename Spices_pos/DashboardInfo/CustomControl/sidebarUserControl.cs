using System;
using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using CounterSales_info.forms;
using Products_info.forms;
using Purchase_info.forms;
using Expenses_info.forms;
using Recoverier_info.forms;
using Stock_management.forms;
using Message_box_info.forms.Clock_In;
using Supplier_Chain_info.forms;
using Products_info.forms.RecipeDetails;
using Settings_info.forms;
using System.Net.NetworkInformation;
using Customers_info.forms;
using Demands_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.controllers;

namespace Spices_pos.DashboardInfo.CustomControls
{
    public partial class sidebarUserControl : UserControl
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

        public sidebarUserControl()
        {
            InitializeComponent();

            setToolTips();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        //error_form error = new error_form();
        //done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public int user_id { get; set; }
        public int role_id { get; set; }

        private void setToolTips()
        {
            try
            {
                GetSetData.setToolTipForButtonControl(posBtn, "Make a sale, return items, view shift transactions, transactions history and payments.");
                GetSetData.setToolTipForButtonControl(product_btn, "Create/modify/delete/ products details, set regular, shelf items and print reports.");
                GetSetData.setToolTipForButtonControl(purchase_btn, "Create/return purchasing or receive items and print reports.");
                GetSetData.setToolTipForButtonControl(btnSuppliers, "Create/modify/delete/ vendors/supplier details and print reports.");
                GetSetData.setToolTipForButtonControl(btnEmployees, "Create/modify/delete/ employee/salesman details and print reports.");
                GetSetData.setToolTipForButtonControl(btnCustomers, "Create/modify/delete/ customers details and print reports.");
                GetSetData.setToolTipForButtonControl(btnClockIn, "Employee can clock-in and start shift here and print reports.");
                //GetSetData.setToolTipForButtonControl(btnClockInHistory, "Start or end salesman shifts, set counters opening amounts, and print report");
                GetSetData.setToolTipForButtonControl(btnClockOut, "Click here to track time clock history and print report.");
                GetSetData.setToolTipForButtonControl(btnDeals, "Create/modify/delete customized promotions/deals and print reports.");
                GetSetData.setToolTipForButtonControl(btn_stock, "Can track stock history and print report.");
                GetSetData.setToolTipForButtonControl(btnCustomerDues, "Can track creditor customer outstanding balance and print reports.");
                GetSetData.setToolTipForButtonControl(btnDemands, "Create/modify/delete/ purchasing demands and print reports.");
                //GetSetData.setToolTipForButtonControl(btnEndDay, "Create/modify store closing here and print X-Report.");
                GetSetData.setToolTipForButtonControl(button_settings, "Click here to open control panel and can on/off dynamic features.");
                GetSetData.setToolTipForButtonControl(logOutBtn, "You can logout from the pos.");

            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
            }
        }

        private void system_user_permissions()
        {
            product_btn.Visible = bool.Parse(data.UserPermissions("products", "pos_tbl_authorities_dashboard", role_id));
            btnDeals.Visible = bool.Parse(data.UserPermissions("products", "pos_tbl_authorities_dashboard", role_id));
            purchase_btn.Visible = bool.Parse(data.UserPermissions("purchases", "pos_tbl_authorities_dashboard", role_id));
            expense_btn.Visible = bool.Parse(data.UserPermissions("expenses", "pos_tbl_authorities_dashboard", role_id));
            btn_stock.Visible = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_dashboard", role_id));
            posBtn.Visible = bool.Parse(data.UserPermissions("pos", "pos_tbl_authorities_dashboard", role_id));
            button_settings.Visible = bool.Parse(data.UserPermissions("settings", "pos_tbl_authorities_dashboard", role_id));


            //new ***************************************************************************************************
            btnCustomers.Visible = bool.Parse(data.UserPermissions("customers", "pos_tbl_authorities_dashboard", role_id));

            //***************************************************************************************************
            btnEmployees.Visible = bool.Parse(data.UserPermissions("employee", "pos_tbl_authorities_dashboard", role_id));

            //***************************************************************************************************
            btnSuppliers.Visible = bool.Parse(data.UserPermissions("suppliers", "pos_tbl_authorities_dashboard", role_id));

            //***************************************************************************************************
            btnDemands.Visible = bool.Parse(data.UserPermissions("logout", "pos_tbl_authorities_dashboard", role_id));

            //***************************************************************************************************
            btnCustomerDues.Visible = bool.Parse(data.UserPermissions("customer_dues", "pos_tbl_authorities_dashboard", role_id));
        }

        private void logo_imag()
        {
            try
            {
                TextData.backup_path = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.image_path = data.UserPermissions("logo_path", "pos_report_settings");

                if (TextData.backup_path != "nill" && TextData.backup_path != "")
                {
                    if (TextData.image_path != "nill" && TextData.image_path != "" && TextData.image_path != null)
                    {
                        logo_img2.Image = Bitmap.FromFile(TextData.backup_path + TextData.image_path);
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidebarUserControl_Load(object sender, EventArgs e)
        {
            logo_imag();
            system_user_permissions();
        }

        private void posBtn_Click(object sender, EventArgs e)
        {
            form_counter_sales.user_id = user_id;
            form_counter_sales.role_id = role_id;

            button_controls.CounterCashButton(role_id, user_id);

            if (TextData.isClockedIn)
            {
                if (Screen.AllScreens.Length > 1)
                {
                    if (!IsFormOpen(typeof(fromSecondScreen)))
                    {
                        fromSecondScreen secondaryForm = new fromSecondScreen();
                        Screen secondaryScreen = Screen.AllScreens[1];
                        secondaryForm.StartPosition = FormStartPosition.CenterScreen;
                        secondaryForm.Location = secondaryScreen.WorkingArea.Location;
                        secondaryForm.WindowState = FormWindowState.Maximized;
                        secondaryForm.Show();
                    }
                }

                this.OnClick(e);
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

        private void product_btn_Click(object sender, EventArgs e)
        {
            product_details.user_id = user_id;
            product_details.role_id = role_id;
            product_details.providedValue = "";
            button_controls.productsButton();
            this.OnClick(e);
        }

        private void purchase_btn_Click(object sender, EventArgs e)
        {
            Purchase_details.role_id = role_id;
            Purchase_details.user_id = user_id;
            button_controls.purchaseButton();
            this.OnClick(e);
        }

        private void expense_btn_Click(object sender, EventArgs e)
        {
            Expenses_details.role_id = role_id;
            Expenses_details.user_id = user_id;
            button_controls.expensesButton();
            this.OnClick(e);
        }

        private void btn_bank_Click(object sender, EventArgs e)
        {
            form_supplier_details.role_id = role_id;
            form_supplier_details.user_id = user_id;
            form_supplier_details.count = 1;
            form_supplier_details supplier = new form_supplier_details();
            supplier.Show();
            this.OnClick(e);
        }

        private void recover_btn_Click(object sender, EventArgs e)
        {
            Supliers_details.role_id = role_id;
            Supliers_details.user_id = user_id;
            Supliers_details.count = 1;
            Supliers_details supplier = new Supliers_details();
            supplier.Show();
            this.OnClick(e);
        }

        private void btn_stock_Click(object sender, EventArgs e)
        {
            form_inventory_history.user_id = user_id;
            form_inventory_history.role_id = role_id;
            form_inventory_history stock = new form_inventory_history();
            stock.Show();
            this.OnClick(e);
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            formClockInDetails charity = new formClockInDetails();

            formClockInDetails.user_id = user_id;
            formClockInDetails.role_id = role_id;
            charity.Show();
            this.OnClick(e);
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            formClockOutDetails charity = new formClockOutDetails();

            formClockOutDetails.user_id = user_id;
            formClockOutDetails.role_id = role_id;
            charity.Show();
            this.OnClick(e);
        }

        private void btnSalaries_Click(object sender, EventArgs e)
        {
            CustomerDues.role_id = role_id;
            CustomerDues dues = new CustomerDues();
            dues.Show();
            this.OnClick(e);
        }

        private void btnSupplierPaybook_Click(object sender, EventArgs e)
        {
            formDemandList.user_id = user_id;
            formDemandList.role_id = role_id;
            formDemandList dues = new formDemandList();
            dues.Show();
            this.OnClick(e);
        }


        private void btnDeals_Click(object sender, EventArgs e)
        {
            promotion_details.user_id = user_id;
            promotion_details.role_id = role_id;
            promotion_details dues = new promotion_details();
            dues.Show(); 
            this.OnClick(e);
        }

        private void button_settings_Click(object sender, EventArgs e)
        {
            settings.user_id = user_id;
            settings.role_id = role_id;
            button_controls.SettingsButton();
            this.OnClick(e);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Customer_details.role_id = role_id;
            Customer_details.user_id = user_id;
            Customer_details.count = 1;
            Customer_details.selected_customer = "";
            Customer_details customer = new Customer_details();
            customer.Show();
            this.OnClick(e);
        }

        private string get_mac_address()
        {
            error_form error = new error_form();
            done_form done = new done_form();

            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;

                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }

                return sMacAddress;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return "";
            }
        }

        private void Save_login_details_button()
        {
            error_form error = new error_form();
            done_form done = new done_form();
            Datalayers data = new Datalayers(webConfig.con_string);
            ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);

            try
            {
                TextData.image_path = get_mac_address();

                if (TextData.image_path != "")
                {
                    GetSetData.query = @"insert into pos_login_details values ('" + TextData.image_path + "' , '" + DateTime.Now.ToShortDateString() + "' , '" + DateTime.Now.ToLongTimeString() + "', 'Logout' , '" + role_id.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void logOutBtn_Click(object sender, EventArgs e)
        {
            sure.Message_choose("Are you sure you want logout!");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                Save_login_details_button();
                button_controls.loginform();
                this.OnClick(e);
            }
        }

       
    }
}
