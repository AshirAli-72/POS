using System;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using System.Drawing.Printing;
using RefereningMaterial;
using Supplier_Chain_info.forms;
using Expenses_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses;

namespace Settings_info.forms.RolesAndPermissions
{
    public partial class formRegistration : Form
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

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        int count = 1;
        public static int role_id = 0;


        private void FillComboBoxWithItems()
        {
            GetSetData.FillComboBoxUsingProcedures(role_text, "fillComboBoxRoles", "roleTitle");
        }

        private void formRegistration_Load(object sender, EventArgs e)
        {
            //
        }

        public formRegistration()
        {
            InitializeComponent();
            setFormColorsDynamically(); 
        }


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
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }
        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings reg = new settings();
            reg.Show();

            this.Dispose();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            //this.Opacity = .850;
            Add_supplier.role_id = role_id;
            buttonControls.employeeButton();
            //this.Opacity = .999;
            //GetSetData.FillComboBoxUsingProcedures(txtEmployee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void generate_barcode()
        {
            //try
            //{
            //    GetSetData.Data = "";

            //    if (username_text.Text != "" && pass_text.Text != "")
            //    {
            //        GetSetData.query = "select role_id from pos_role where (username = '" + username_text.Text + "') and (password = '" + pass_text.Text + "');";
            //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //        if (GetSetData.Ids != 0)
            //        {
            //            GetSetData.Data = pass_text.Text;

            //            if (username_text.Text != "" && pass_text.Text != "")
            //            {
            //                Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //                img_barcode.Image = brcode.Draw(GetSetData.Data, 60);
            //            }
            //            else
            //            {
            //                error.errorMessage("Barcode field is empty!");
            //                error.ShowDialog();
            //            }
            //        }
            //        else
            //        {
            //            error.errorMessage("Invalid Username or Password!");
            //            error.ShowDialog();
            //        }
            //    }
            //    else
            //    {
            //        error.errorMessage("Please enter Username and Password!");
            //        error.ShowDialog();
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void fun_print_barcode()
        {
            //try
            //{
            //    if (img_barcode.Image != null)
            //    {
            //        PrintDialog pd = new PrintDialog();
            //        PrintDocument doc = new PrintDocument();

            //        doc.PrintPage += Doc_PrintPage;
            //        pd.Document = doc;

            //        if (pd.ShowDialog() == DialogResult.OK)
            //        {
            //            doc.Print();
            //        }
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //try
            //{
            //    if (username_text.Text != "" && pass_text.Text != "")
            //    {
            //        GetSetData.query = "select reg_id from pos_role where (username = '" + username_text.Text + "') and (password = '" + pass_text.Text + "');";
            //        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
            //        GetSetData.values = data.UserPermissions("name", "pos_registration", "reg_id", GetSetData.Ids.ToString());

            //        PointF point = new PointF(23f, 60f);
            //        Font font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
            //        SolidBrush black = new SolidBrush(Color.Black);
            //        GetSetData.Data = data.UserPermissions("shop_name", "pos_configurations");
            //        e.Graphics.DrawString(GetSetData.Data, font, Brushes.Black, 0, 2);
            //        Bitmap bm = new Bitmap(img_barcode.Width, img_barcode.Height);
            //        img_barcode.DrawToBitmap(bm, new Rectangle(0, 0, img_barcode.Width, img_barcode.Height));
            //        e.Graphics.DrawImage(bm, 0, 16);
            //        font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
            //        e.Graphics.DrawString(GetSetData.values, font, Brushes.Black, 0, 78);
            //        //e.Graphics.DrawString(prod_code_text.Text + "   RS: " + sale_price_text.Text, font, Brushes.Black, 0, 89);
            //        bm.Dispose();
            //    }
            //    else
            //    {
            //        error.errorMessage("Please enter Username and Password!");
            //        error.ShowDialog();
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        private void btn_print_barcode_Click(object sender, EventArgs e)
        {
            //if (username_text.Text != "" && pass_text.Text != "")
            //{
            //    generate_barcode();
            //    fun_print_barcode();
            //}
            //else
            //{
            //    error.errorMessage("Please enter Username and Password!");
            //    error.ShowDialog();
            //}
        }

        private void All_form_visibility()
        {
            try
            {
                switch (count)
                {
                    case 1:
                        pnl_all_forms1.Visible = true;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = false;
                        pnl_all_forms4.Visible = false;
                        pnl_all_forms5.Visible = false;
                        pnl_all_forms6.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = false;
                        btn_previous4.Enabled = false;
                        btn_previous5.Enabled = false;
                        btn_previous6.Enabled = false;

                        btn_next1.Enabled = true;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        btn_next4.Enabled = false;
                        btn_next5.Enabled = false;
                        btn_next6.Enabled = false;
                        pnl_all_forms1.Dock = DockStyle.Fill;
                        break;

                    case 2:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = true;
                        pnl_all_forms3.Visible = false;
                        pnl_all_forms4.Visible = false;
                        pnl_all_forms5.Visible = false;
                        pnl_all_forms6.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = true;
                        btn_previous3.Enabled = false;
                        btn_previous4.Enabled = false;
                        btn_previous5.Enabled = false;
                        btn_previous6.Enabled = false;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = true;
                        btn_next3.Enabled = false;
                        btn_next4.Enabled = false;
                        btn_next5.Enabled = false;
                        btn_next6.Enabled = false;
                        pnl_all_forms2.Dock = DockStyle.Fill;
                        break;

                    case 3:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = true;
                        pnl_all_forms4.Visible = false;
                        pnl_all_forms5.Visible = false;
                        pnl_all_forms6.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = true;
                        btn_previous4.Enabled = false;
                        btn_previous5.Enabled = false;
                        btn_previous6.Enabled = false;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = true;
                        btn_next4.Enabled = false;
                        btn_next5.Enabled = false;
                        btn_next6.Enabled = false;
                        pnl_all_forms3.Dock = DockStyle.Fill;
                        break;

                    case 4:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = false;
                        pnl_all_forms4.Visible = true;
                        pnl_all_forms5.Visible = false;
                        pnl_all_forms6.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = false;
                        btn_previous4.Enabled = true;
                        btn_previous5.Enabled = false;
                        btn_previous6.Enabled = false;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        btn_next4.Enabled = true;
                        btn_next5.Enabled = false;
                        btn_next6.Enabled = false;
                        pnl_all_forms4.Dock = DockStyle.Fill;
                        break;

                    case 5:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = false;
                        pnl_all_forms4.Visible = false;
                        pnl_all_forms5.Visible = true;
                        pnl_all_forms6.Visible = false;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = false;
                        btn_previous4.Enabled = false;
                        btn_previous5.Enabled = true;
                        btn_previous6.Enabled = false;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        btn_next4.Enabled = false;
                        btn_next5.Enabled = true;
                        btn_next6.Enabled = false;

                        pnl_all_forms5.Dock = DockStyle.Fill;
                        break;

                    case 6:
                        pnl_all_forms1.Visible = false;
                        pnl_all_forms2.Visible = false;
                        pnl_all_forms3.Visible = false;
                        pnl_all_forms4.Visible = false;
                        pnl_all_forms5.Visible = false;
                        pnl_all_forms6.Visible = true;

                        btn_previous1.Enabled = false;
                        btn_previous2.Enabled = false;
                        btn_previous3.Enabled = false;
                        btn_previous4.Enabled = false;
                        btn_previous5.Enabled = false;
                        btn_previous6.Enabled = true;

                        btn_next1.Enabled = false;
                        btn_next2.Enabled = false;
                        btn_next3.Enabled = false;
                        btn_next4.Enabled = false;
                        btn_next5.Enabled = false;
                        btn_next6.Enabled = false;
                        pnl_all_forms6.Dock = DockStyle.Fill;
                        break;

                    case 7:
                        count = 1;
                        btn_previous6.Enabled = true;
                        btn_next6.Enabled = false;
                        break;

                    default:
                        count = 1;
                        break;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_next1_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.role_title = role_text.Text;
                count++;
                All_form_visibility();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous2_Click(object sender, EventArgs e)
        {
            try
            {
                count--;
                All_form_visibility();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_next2_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 2)
                //{
                count++;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_exit_form2_Click(object sender, EventArgs e)
        {
            count--;
            All_form_visibility();
        }

        private void btn_next3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count++;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count--;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_exit_form3_Click(object sender, EventArgs e)
        {
            count--;
            All_form_visibility();
        }

        private void btn_next4_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count++;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous4_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count--;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_exit_form4_Click(object sender, EventArgs e)
        {
            count--;
            All_form_visibility();
        }

        private void btn_next5_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count++;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous5_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count--;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_next6_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                //count++;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_previous6_Click(object sender, EventArgs e)
        {
            try
            {
                //if (count == 3)
                //{
                count--;
                All_form_visibility();
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtEmployee_Enter(object sender, EventArgs e)
        {
            //GetSetData.FillComboBoxUsingProcedures(txtEmployee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void checked_values_for_tbl_dashboard_authorities()
        {
            TextData.products = false;
            TextData.purchases = false;
            TextData.expenses = false;
            TextData.isEmployee = false;
            TextData.recoveries = false;
            TextData.customers = false;
            TextData.suppliers = false;
            TextData.customer_dues = false;
            TextData.pos = false;
            TextData.reports = false;
            TextData.backup = false;
            TextData.restore = false;
            TextData.IsStock = false;
            TextData.settings = false;
            TextData.notifications = false;
            TextData.logOut = false;
            TextData.about = false;
            TextData.capital = false;
            TextData.dailyBalance = false;
            TextData.Investors = false;
            TextData.InvestorsPaybook = false;
            TextData.guarantors = false;
            TextData.aboutLicense = false;
            TextData.EmployeeSalaries = false;
            TextData.bankLoan = false;
            TextData.bankLoanPaybook = false;
            TextData.charity = false;
            TextData.supplierPaybook = false;

            if (chk_dashboard_salaries.Checked == true)
            {
                TextData.EmployeeSalaries = true;
            }

            if (chk_dashboard_bankLoan.Checked == true)
            {
                TextData.bankLoan = true;
            }

            if (chk_dashboard_bankLoanPaybook.Checked == true)
            {
                TextData.bankLoanPaybook = true;
            }

            if (chk_dashboard_Aboutlicense.Checked == true)
            {
                TextData.aboutLicense = true;
            }

            if (chk_dashboard_capital.Checked == true)
            {
                TextData.capital = true;
            }

            if (chk_dashboard_dailyBalance.Checked == true)
            {
                TextData.dailyBalance = true;
            }

            if (chk_dashboard_investors.Checked == true)
            {
                TextData.Investors = true;
            }

            if (chk_dashboard_investorPaybook.Checked == true)
            {
                TextData.InvestorsPaybook = true;
            }

            if (chk_dashboard_guarantors.Checked == true)
            {
                TextData.guarantors = true;
            }

            if (chk_dashboard_products.Checked == true)
            {
                TextData.products = true;
            }

            if (chk_dashboard_purchases.Checked == true)
            {
                TextData.purchases = true;
            }

            if (chk_dashboard_expenses.Checked == true)
            {
                TextData.expenses = true;
            }

            if (chk_dashboard_employee.Checked == true)
            {
                TextData.isEmployee = true;
            }

            if (chk_dashboard_recoveries.Checked == true)
            {
                TextData.recoveries = true;
            }

            if (chk_dashboard_customers.Checked == true)
            {
                TextData.customers = true;
            }

            if (chk_dashboard_suppliers.Checked == true)
            {
                TextData.suppliers = true;
            }

            if (chk_dashboard_customers_dues.Checked == true)
            {
                TextData.customer_dues = true;
            }

            if (chk_dashboard_pos.Checked == true)
            {
                TextData.pos = true;
            }

            if (chk_dashboard_reports.Checked == true)
            {
                TextData.reports = true;
            }

            if (chk_dashboard_backup.Checked == true)
            {
                TextData.backup = true;
            }

            if (chk_dashboard_restore.Checked == true)
            {
                TextData.restore = true;
            }

            if (chk_dashboard_settings.Checked == true)
            {
                TextData.settings = true;
            }

            if (chk_dashboard_notifications.Checked == true)
            {
                TextData.notifications = true;
            }

            if (chk_dashboard_banking.Checked == true)
            {
                TextData.about = true;
            }

            if (chk_dashboard_logout.Checked == true)
            {
                TextData.logOut = true;
            }

            if (chk_dashboard_stock.Checked == true)
            {
                TextData.IsStock = true;
            }

            if (chk_dashboard_supplierPaybook.Checked == true)
            {
                TextData.supplierPaybook = true;
            }

            if (chk_dashboard_Charity.Checked == true)
            {
                TextData.charity = true;
            }

            buttonControls.insert_dashboard_button_controls();
        }

        private void checked_values_for_tbl_Reports_authorities()
        {
            TextData.cus_ledger = false;
            TextData.cus_sales = false;
            TextData.cus_returns = false;
            TextData.company_statement = false;
            TextData.day_book = false;
            TextData.IsStock = false;
            TextData.cus_recoveries = false;
            TextData.customer_statement = false;
            TextData.receivables = false;
            TextData.payables = false;
            TextData.balance_in_out = false;
            TextData.income_statement = false;
            TextData.company_ledger = false;
            TextData.generateInvoices = false;
            TextData.chequeDetails = false;

            if (chk_reports_generateInvoices.Checked == true)
            {
                TextData.generateInvoices = true;
            }

            if (chk_reports_chequeDetails.Checked == true)
            {
                TextData.chequeDetails = true;
            }

            if (chk_reports_customer_ledger.Checked == true)
            {
                TextData.cus_ledger = true;
            }

            if (chk_reports_sales.Checked == true)
            {
                TextData.cus_sales = true;
            }

            if (chk_reports_returns.Checked == true)
            {
                TextData.cus_returns = true;
            }

            if (chk_reports_company_statement.Checked == true)
            {
                TextData.company_statement = true;
            }

            if (chk_reports_daybook.Checked == true)
            {
                TextData.day_book = true;
            }

            if (chk_reports_stock.Checked == true)
            {
                TextData.IsStock = true;
            }

            if (chk_reports_recoveries.Checked == true)
            {
                TextData.cus_recoveries = true;
            }

            if (chk_reports_customer_statement.Checked == true)
            {
                TextData.customer_statement = true;
            }

            if (chk_reports_receivables.Checked == true)
            {
                TextData.receivables = true;
            }

            if (chk_reports_payables.Checked == true)
            {
                TextData.payables = true;
            }

            if (chk_reports_balance_in_out.Checked == true)
            {
                TextData.balance_in_out = true;
            }

            if (chk_reports_income_statement.Checked == true)
            {
                TextData.income_statement = true;
            }

            if (chk_reports_company_ledger.Checked == true)
            {
                TextData.company_ledger = true;
            }


            if (chk_reports_generateInvoices.Checked == true)
            {
                TextData.generateInvoices = true;
            }


            if (chk_reports_chequeDetails.Checked == true)
            {
                TextData.chequeDetails = true;
            }

            buttonControls.insert_reports_button_controls();
        }

        private void checked_values_for_tbl_authorities_button_controls1()
        {
            TextData.supplier_detials_print = false;
            TextData.supplier_detials_delete = false;
            TextData.supplier_detials_new = false;
            TextData.supplier_detials_modify = false;
            TextData.supplier_detials_select = false;
            TextData.suppliers_save = false;
            TextData.suppliers_update = false;
            TextData.suppliers_exit = false;
            // ****************************************
            TextData.customer_detials_print = false;
            TextData.customer_detials_delete = false;
            TextData.customer_detials_new = false;
            TextData.customer_detials_modify = false;
            TextData.customer_detials_select = false;
            TextData.customer_save = false;
            TextData.customer_update = false;
            TextData.customer_exit = false;
            //*****************************************
            TextData.employee_detials_print = false;
            TextData.employee_detials_delete = false;
            TextData.employee_detials_new = false;
            TextData.employee_detials_modify = false;
            TextData.employee_detials_select = false;
            TextData.employee_save = false;
            TextData.employee_update = false;
            TextData.employee_exit = false;
            //*****************************************
            TextData.purchase_detials_print = false;
            TextData.purchase_detials_delete = false;
            TextData.purchase_detials_new = false;
            TextData.purchase_detials_returns = false;
            TextData.purchase_detials_refresh = false;
            TextData.purchase_save = false;
            TextData.purchase_print = false;
            TextData.purchase_exit = false;

            //***************************************** suppliers
            if (chk_supplier_details_print.Checked == true)
            {
                TextData.supplier_detials_print = true;
            }

            if (chk_supplier_details_delete.Checked == true)
            {
                TextData.supplier_detials_delete = true;
            }

            if (chk_supplier_details_new.Checked == true)
            {
                TextData.supplier_detials_new = true;
            }

            if (chk_supplier_details_modify.Checked == true)
            {
                TextData.supplier_detials_modify = true;
            }

            if (chk_supplier_details_select.Checked == true)
            {
                TextData.supplier_detials_select = true;
            }

            if (chk_supplier_save.Checked == true)
            {
                TextData.suppliers_save = true;
            }

            if (chk_supplier_update.Checked == true)
            {
                TextData.suppliers_update = true;
            }

            if (chk_supplier_exit.Checked == true)
            {
                TextData.suppliers_exit = true;
            }
            //********************************************** customers

            if (chk_cus_details_print.Checked == true)
            {
                TextData.customer_detials_print = true;
            }

            if (chk_cus_details_delete.Checked == true)
            {
                TextData.customer_detials_delete = true;
            }

            if (chk_cus_details_new.Checked == true)
            {
                TextData.customer_detials_new = true;
            }

            if (chk_cus_details_modify.Checked == true)
            {
                TextData.customer_detials_modify = true;
            }

            if (chk_cus_details_select.Checked == true)
            {
                TextData.customer_detials_select = true;
            }

            if (chk_customers_save.Checked == true)
            {
                TextData.customer_save = true;
            }

            if (chk_customers_update.Checked == true)
            {
                TextData.customer_update = true;
            }

            if (chk_customers_exit.Checked == true)
            {
                TextData.customer_exit = true;
            }

            //********************************************** Employees

            if (chk_employee_details_print.Checked == true)
            {
                TextData.employee_detials_print = true;
            }

            if (chk_employee_details_delete.Checked == true)
            {
                TextData.employee_detials_delete = true;
            }

            if (chk_employee_details_new.Checked == true)
            {
                TextData.employee_detials_new = true;
            }

            if (chk_employee_details_modify.Checked == true)
            {
                TextData.employee_detials_modify = true;
            }

            if (chk_employee_details_select.Checked == true)
            {
                TextData.employee_detials_select = true;
            }

            if (chk_employee_save.Checked == true)
            {
                TextData.employee_save = true;
            }

            if (chk_employee_update.Checked == true)
            {
                TextData.employee_update = true;
            }

            if (chk_employee_exit.Checked == true)
            {
                TextData.employee_exit = true;
            }

            //********************************************** Purchase

            if (chk_purchase_details_print.Checked == true)
            {
                TextData.purchase_detials_print = true;
            }

            if (chk_purchase_details_delete.Checked == true)
            {
                TextData.purchase_detials_delete = true;
            }

            if (chk_purchase_details_new.Checked == true)
            {
                TextData.purchase_detials_new = true;
            }

            if (chk_purchase_details_returns.Checked == true)
            {
                TextData.purchase_detials_returns = true;
            }

            if (chk_purchase_details_refresh.Checked == true)
            {
                TextData.purchase_detials_refresh = true;
            }

            if (chk_purchases_save.Checked == true)
            {
                TextData.purchase_save = true;
            }

            if (chk_purchases_print.Checked == true)
            {
                TextData.purchase_print = true;
            }

            if (chk_purchases_update.Checked == true)
            {
                TextData.purchase_exit = true;
            }
            //**********************************************

            buttonControls.insert_forms_button_controls1();
        }

        private void checked_values_for_tbl_authorities_button_controls2()
        {
            TextData.products_detials_print = false;
            TextData.products_detials_delete = false;
            TextData.products_detials_new = false;
            TextData.products_detials_modify = false;
            TextData.products_detials_regular = false;
            TextData.products_detials_expired = false;
            TextData.products_save = false;
            TextData.products_update = false;
            TextData.products_exit = false;
            // ******************************************
            TextData.recovery_detials_print = false;
            TextData.recovery_detials_new = false;
            TextData.recovery_detials_delete = false;
            TextData.recovery_detials_modify = false;
            TextData.recovery_detials_Invoices = false;
            TextData.recovery_save = false;
            TextData.recovery_print = false;
            TextData.recovery_exit = false;
            // ******************************************
            TextData.expense_detials_print = false;
            TextData.expense_detials_delete = false;
            TextData.expense_detials_new = false;
            TextData.expense_detials_modify = false;
            TextData.expense_detials_refresh = false;
            TextData.expense_save = false;
            TextData.expense_update = false;
            TextData.expense_exit = false;
            // ******************************************
            TextData.dues_print = false;
            TextData.dues_refresh = false;
            TextData.dues_exit = false;
            // ******************************************
            TextData.stock_whole = false;
            TextData.stock_low = false;
            TextData.stock_print = false;
            TextData.stock_refresh = false;
            TextData.stock_exit = false;
            // ******************************************
            TextData.settings_registration = false;
            TextData.settings_configuration = false;
            TextData.settings_reports = false;
            TextData.settings_login_details = false;
            TextData.settings_general_options = false;
            // ******************************************
            TextData.banking_details_print = false;
            TextData.banking_details_delete = false;
            TextData.banking_details_new = false;
            TextData.banking_details_modify = false;
            TextData.new_transaction_save = false;
            TextData.new_transaction_update = false;
            TextData.new_transaction_savePrint = false;
            TextData.new_transaction_exit = false;
            // ******************************************
            TextData.demands_list_print = false;
            TextData.demands_list_delete = false;
            TextData.demands_list_new = false;
            TextData.demands_list_modify = false;
            TextData.new_demand_save = false;
            TextData.new_demand_update = false;
            TextData.new_demand_savePrint = false;
            TextData.new_demand_exit = false;
            // ******************************************

            // Products Details ***********************************************************
            if (chk_product_details_print.Checked == true)
            {
                TextData.products_detials_print = true;
            }

            if (chk_product_details_delete.Checked == true)
            {
                TextData.products_detials_delete = true;
            }

            if (chk_product_details_new.Checked == true)
            {
                TextData.products_detials_new = true;
            }

            if (chk_product_details_modify.Checked == true)
            {
                TextData.products_detials_modify = true;
            }

            if (chk_product_details_regular_items.Checked == true)
            {
                TextData.products_detials_regular = true;
            }

            if (chk_product_details_expired_items.Checked == true)
            {
                TextData.products_detials_expired = true;
            }

            if (chk_products_save.Checked == true)
            {
                TextData.products_save = true;
            }

            if (chk_products_update.Checked == true)
            {
                TextData.products_update = true;
            }

            if (chk_products_exit.Checked == true)
            {
                TextData.products_exit = true;
            }

            // Recoveries Details *********************************************************
            if (chk_recoveries_details_print.Checked == true)
            {
                TextData.recovery_detials_print = true;
            }

            if (chk_recoveries_details_new.Checked == true)
            {
                TextData.recovery_detials_new = true;
            }

            if (chk_recoveries_details_delete.Checked == true)
            {
                TextData.recovery_detials_delete = true;
            }

            if (chk_recoveries_details_modify.Checked == true)
            {
                TextData.recovery_detials_modify = true;
            }

            if (chk_recoveries_details_invoices.Checked == true)
            {
                TextData.recovery_detials_Invoices = true;
            }

            if (chk_recoveries_save.Checked == true)
            {
                TextData.recovery_save = true;
            }

            if (chk_recoveries_print.Checked == true)
            {
                TextData.recovery_print = true;
            }

            if (chk_recoveries_exit.Checked == true)
            {
                TextData.recovery_exit = true;
            }

            // Expenses Details *********************************************************
            if (chk_expense_details_print.Checked == true)
            {
                TextData.expense_detials_print = true;
            }

            if (chk_expense_details_delete.Checked == true)
            {
                TextData.expense_detials_delete = true;
            }

            if (chk_expense_details_new.Checked == true)
            {
                TextData.expense_detials_new = true;
            }

            if (chk_expense_details_modify.Checked == true)
            {
                TextData.expense_detials_modify = true;
            }

            if (chk_expense_details_refresh.Checked == true)
            {
                TextData.expense_detials_refresh = true;
            }

            if (chk_expenses_save.Checked == true)
            {
                TextData.expense_save = true;
            }

            if (chk_expenses_update.Checked == true)
            {
                TextData.expense_update = true;
            }

            if (chk_expenses_exit.Checked == true)
            {
                TextData.expense_exit = true;
            }

            // Customer Dues Details *********************************************************
            if (chk_cus_dues_print.Checked == true)
            {
                TextData.dues_print = true;
            }

            if (chk_cus_dues_refresh.Checked == true)
            {
                TextData.dues_refresh = true;
            }

            if (chk_cus_dues_exit.Checked == true)
            {
                TextData.dues_exit = true;
            }

            // Expenses Details *********************************************************
            if (chk_stock_whole.Checked == true)
            {
                TextData.stock_whole = true;
            }

            if (chk_stock_low.Checked == true)
            {
                TextData.stock_low = true;
            }

            if (chk_stock_print.Checked == true)
            {
                TextData.stock_print = true;
            }

            if (chk_stock_refresh.Checked == true)
            {
                TextData.stock_refresh = true;
            }

            if (chk_stock_exit.Checked == true)
            {
                TextData.stock_exit = true;
            }

            // Banking Details *********************************************************
            if (chk_banking_details_print.Checked == true)
            {
                TextData.banking_details_print = true;
            }

            if (chk_banking_details_delete.Checked == true)
            {
                TextData.banking_details_delete = true;
            }

            if (chk_banking_details_new.Checked == true)
            {
                TextData.banking_details_new = true;
            }

            if (chk_banking_details_modify.Checked == true)
            {
                TextData.banking_details_modify = true;
            }

            if (chk_add_transaction_save.Checked == true)
            {
                TextData.new_transaction_save = true;
            }

            if (chk_add_transaction_update.Checked == true)
            {
                TextData.new_transaction_update = true;
            }

            if (chk_add_transaction_savePrint.Checked == true)
            {
                TextData.new_transaction_savePrint = true;
            }

            if (chk_add_transaction_exit.Checked == true)
            {
                TextData.new_transaction_exit = true;
            }

            // Demands Details *********************************************************
            if (chk_demand_list_print.Checked == true)
            {
                TextData.demands_list_print = true;
            }

            if (chk_demand_list_delete.Checked == true)
            {
                TextData.demands_list_delete = true;
            }

            if (chk_demand_list_new.Checked == true)
            {
                TextData.demands_list_new = true;
            }

            if (chk_demand_list_modify.Checked == true)
            {
                TextData.demands_list_modify = true;
            }

            if (chk_newDemand_save.Checked == true)
            {
                TextData.new_demand_save = true;
            }

            if (chk_newDemand_update.Checked == true)
            {
                TextData.new_demand_update = true;
            }

            if (chk_newDemand_savePrint.Checked == true)
            {
                TextData.new_demand_savePrint = true;
            }

            if (chk_newDemand_exit.Checked == true)
            {
                TextData.new_demand_exit = true;
            }

            // Settings *********************************************************
            if (chk_settings_reg.Checked == true)
            {
                TextData.settings_registration = true;
            }

            if (chk_settings_config.Checked == true)
            {
                TextData.settings_configuration = true;
            }

            if (chk_settings_reports.Checked == true)
            {
                TextData.settings_reports = true;
            }

            if (chk_settings_login_details.Checked == true)
            {
                TextData.settings_login_details = true;
            }

            if (chk_settings_general.Checked == true)
            {
                TextData.settings_general_options = true;
            }
            // **************************************************************************

            buttonControls.insert_forms_button_controls2();
        }

        private void checked_values_for_tbl_authorities_button_controls3()
        {
            TextData.investor_details_print = false;
            TextData.investor_details_delete = false;
            TextData.investor_details_new = false;
            TextData.investor_details_modify = false;
            TextData.newInvestor_save = false;
            TextData.newInvestor_update = false;
            TextData.newInvestor_barcode = false;
            // ******************************************
            TextData.investorPaybook_details_print = false;
            TextData.investorPaybook_details_new = false;
            TextData.investorPaybook_details_delete = false;
            TextData.investorPaybook_details_modify = false;
            TextData.newInvestorPayment_save = false;
            TextData.newInvestorPayment_savePrint = false;
            TextData.newInvestorPayment_update = false;
            // ******************************************
            TextData.guarantor_details_print = false;
            TextData.guarantor_details_delete = false;
            TextData.guarantor_details_new = false;
            TextData.guarantor_details_modify = false;
            TextData.newGuarantor_save = false;
            TextData.newGuarantor_update = false;
            TextData.newInvestor_barcode = false;
            // ******************************************

            TextData.customerOrders_print = false;
            TextData.customerOrders_delete = false;
            TextData.customerOrders_modify = false;
            TextData.customerOrders_allSales = false;
            TextData.customerOrders_allReturns = false;
            TextData.customerOrders_search = false;
            TextData.customerOrders_contractForm = false;
            // ******************************************
            TextData.salariesPaybook_details_print = false;
            TextData.salariesPaybook_details_new = false;
            TextData.salariesPaybook_details_delete = false;
            TextData.salariesPaybook_details_modify = false;
            TextData.newSalaryPayment_save = false;
            TextData.newSalaryPayment_savePrint = false;
            TextData.newSalaryPayment_update = false;
            // ******************************************
            TextData.bankLoan_details_print = false;
            TextData.bankLoan_details_new = false;
            TextData.bankLoan_details_delete = false;
            TextData.bankLoan_details_modify = false;
            TextData.newBankLoan_save = false;
            TextData.newBankLoan_exit = false;
            TextData.newBankLoan_update = false;
            // ******************************************
            TextData.bankLoanPaybook_details_print = false;
            TextData.bankLoanPaybook_details_new = false;
            TextData.bankLoanPaybook_details_delete = false;
            TextData.bankLoanPaybook_details_modify = false;
            TextData.newBankLoanPayment_save = false;
            TextData.newBankLoanPayment_exit = false;
            TextData.newBankLoanPayment_update = false;
            // ******************************************
            TextData.SupplierPaybook_details_print = false;
            TextData.SupplierPaybook_details_new = false;
            TextData.SupplierPaybook_details_delete = false;
            TextData.SupplierPaybook_details_modify = false;
            TextData.newSupplierPayment_save = false;
            TextData.newSupplierPayment_exit = false;
            TextData.newSalaryPayment_update = false;
            // ******************************************
            TextData.CharityPaybook_details_print = false;
            TextData.CharityPaybook_details_new = false;
            TextData.CharityPaybook_details_delete = false;
            TextData.CharityPaybook_details_modify = false;
            TextData.newCharityPayment_save = false;
            TextData.newCharityPayment_exit = false;
            TextData.newCharityPayment_update = false;
            // ******************************************

            // Investors Details ***********************************************************
            if (chk_investor_details_print.Checked == true)
            {
                TextData.investor_details_print = true;
            }

            if (chk_investor_details_delete.Checked == true)
            {
                TextData.investor_details_delete = true;
            }

            if (chk_investor_details_new.Checked == true)
            {
                TextData.investor_details_new = true;
            }

            if (chk_investor_details_modify.Checked == true)
            {
                TextData.investor_details_modify = true;
            }

            if (chk_newInvestor_save.Checked == true)
            {
                TextData.newInvestor_save = true;
            }

            if (chk_newInvestor_update.Checked == true)
            {
                TextData.newInvestor_update = true;
            }

            if (chk_newInvestor_barcode.Checked == true)
            {
                TextData.newInvestor_barcode = true;
            }

            // Investor Paybook Details *********************************************************
            if (chk_InvestorPaymentDetails_print.Checked == true)
            {
                TextData.investorPaybook_details_print = true;
            }

            if (chk_InvestorPaymentDetails_delete.Checked == true)
            {
                TextData.investorPaybook_details_delete = true;
            }

            if (chk_InvestorPaymentDetails_new.Checked == true)
            {
                TextData.investorPaybook_details_new = true;
            }

            if (chk_InvestorPaymentDetails_modify.Checked == true)
            {
                TextData.investorPaybook_details_modify = true;
            }

            if (chk_InvestorNewPayment_save.Checked == true)
            {
                TextData.newInvestorPayment_save = true;
            }

            if (chk_InvestorNewPayment_savePrint.Checked == true)
            {
                TextData.newInvestorPayment_savePrint = true;
            }

            if (chk_InvestorNewPayment_update.Checked == true)
            {
                TextData.newInvestorPayment_update = true;
            }

            // Guarantors Details *********************************************************
            if (chk_guarantorDetails_print.Checked == true)
            {
                TextData.guarantor_details_print = true;
            }

            if (chk_guarantorDetails_delete.Checked == true)
            {
                TextData.guarantor_details_delete = true;
            }

            if (chk_guarantorDetails_new.Checked == true)
            {
                TextData.guarantor_details_new = true;
            }

            if (chk_guarantorDetails_modify.Checked == true)
            {
                TextData.guarantor_details_modify = true;
            }

            if (chk_newGuarantor_save.Checked == true)
            {
                TextData.newGuarantor_save = true;
            }

            if (chk_newGuarantor_update.Checked == true)
            {
                TextData.newGuarantor_update = true;
            }

            if (chk_newGuarantor_barcode.Checked == true)
            {
                TextData.newGuarantor_barcode = true;
            }
            // **************************************************************************

            // Guarantors Details *********************************************************
            if (chkOrders_print.Checked == true)
            {
                TextData.customerOrders_print = true;
            }

            if (chkOrders_delete.Checked == true)
            {
                TextData.customerOrders_delete = true;
            }

            if (chkOrders_modify.Checked == true)
            {
                TextData.customerOrders_modify = true;
            }

            if (chkOrders_AllSales.Checked == true)
            {
                TextData.customerOrders_allSales = true;
            }

            if (chkOrders_AllReturns.Checked == true)
            {
                TextData.customerOrders_allReturns = true;
            }

            if (chkOrders_search.Checked == true)
            {
                TextData.customerOrders_search = true;
            }

            if (chkOrders_contractForm.Checked == true)
            {
                TextData.customerOrders_contractForm = true;
            }
            // **************************************************************************

            // Employee Salaries Paybook Details *********************************************************
            if (chkSalaryPaybook_print.Checked == true)
            {
                TextData.salariesPaybook_details_print = true;
            }

            if (chkSalaryPaybook_delete.Checked == true)
            {
                TextData.salariesPaybook_details_delete = true;
            }

            if (chkSalaryPaybook_new.Checked == true)
            {
                TextData.salariesPaybook_details_new = true;
            }

            if (chkSalaryPaybook_modify.Checked == true)
            {
                TextData.salariesPaybook_details_modify = true;
            }

            if (chkNewSalary_save.Checked == true)
            {
                TextData.newSalaryPayment_save = true;
            }

            if (chkNewSalary_savePrint.Checked == true)
            {
                TextData.newSalaryPayment_savePrint = true;
            }

            if (chkNewSalary_update.Checked == true)
            {
                TextData.newSalaryPayment_update = true;
            }
            // **************************************************************************
            // Bank Loan Details *********************************************************
            if (chkBankLoanDetails_print.Checked == true)
            {
                TextData.bankLoanPaybook_details_print = true;
            }

            if (chkBankLoanDetails_delete.Checked == true)
            {
                TextData.bankLoanPaybook_details_delete = true;
            }

            if (chkBankLoanDetails_new.Checked == true)
            {
                TextData.bankLoanPaybook_details_new = true;
            }

            if (chkBankLoanDetails_modify.Checked == true)
            {
                TextData.bankLoanPaybook_details_modify = true;
            }

            if (chkBankLoan_save.Checked == true)
            {
                TextData.newBankLoan_save = true;
            }

            if (chkBankLoan_exit.Checked == true)
            {
                TextData.newBankLoan_exit = true;
            }

            if (chkBankLoan_update.Checked == true)
            {
                TextData.newBankLoan_update = true;
            }
            // **************************************************************************

            // Bank Loan Details *********************************************************
            if (chkBankLoanPaymentDetails_print.Checked == true)
            {
                TextData.bankLoanPaybook_details_print = true;
            }

            if (chkBankLoanPaymentDetails_delete.Checked == true)
            {
                TextData.bankLoanPaybook_details_delete = true;
            }

            if (chkBankLoanPaymentDetails_new.Checked == true)
            {
                TextData.bankLoanPaybook_details_new = true;
            }

            if (chkBankLoanPaymentDetails_modify.Checked == true)
            {
                TextData.bankLoanPaybook_details_modify = true;
            }

            if (chkBankLoanPayment_save.Checked == true)
            {
                TextData.newBankLoanPayment_save = true;
            }

            if (chkBankLoanPayment_exit.Checked == true)
            {
                TextData.newBankLoanPayment_exit = true;
            }

            if (chkBankLoanPayment_update.Checked == true)
            {
                TextData.newBankLoanPayment_update = true;
            }
            // **************************************************************************

            // Supplier Paybook Details *********************************************************
            if (chkSupplierPaybook_print.Checked == true)
            {
                TextData.SupplierPaybook_details_print = true;
            }

            if (chkSupplierPaybook_delete.Checked == true)
            {
                TextData.SupplierPaybook_details_delete = true;
            }

            if (chkSupplierPaybook_new.Checked == true)
            {
                TextData.SupplierPaybook_details_new = true;
            }

            if (chkSupplierPaybook_modify.Checked == true)
            {
                TextData.SupplierPaybook_details_modify = true;
            }

            if (chkSupplierPayment_save.Checked == true)
            {
                TextData.newSupplierPayment_save = true;
            }

            if (chkSupplierPayment_exit.Checked == true)
            {
                TextData.newSupplierPayment_exit = true;
            }

            if (chkSupplierPayment_update.Checked == true)
            {
                TextData.newSupplierPayment_update = true;
            }
            // **************************************************************************

            // Charity Paybook Details *********************************************************
            if (chkCharityPaybook_print.Checked == true)
            {
                TextData.CharityPaybook_details_print = true;
            }

            if (chkCharityPaybook_delete.Checked == true)
            {
                TextData.CharityPaybook_details_delete = true;
            }

            if (chkCharityPaybook_new.Checked == true)
            {
                TextData.CharityPaybook_details_new = true;
            }

            if (chkCharityPaybook_modify.Checked == true)
            {
                TextData.CharityPaybook_details_modify = true;
            }

            if (chkCharityPayment_save.Checked == true)
            {
                TextData.newCharityPayment_save = true;
            }

            if (chkCharityPayment_exit.Checked == true)
            {
                TextData.newCharityPayment_exit = true;
            }

            if (chkCharityPayment_update.Checked == true)
            {
                TextData.newCharityPayment_update = true;
            }
            // **************************************************************************

            //GetSetData.SaveLogHistoryDetails("Settings Form", "Creating new user [" + TextData.username + "  " + TextData.password + "]", role_id);
            buttonControls.insert_forms_button_controls3();
        }

        private void btn_save_form4_Click(object sender, EventArgs e)
        {
            if (TextData.role_title != "")
            {
                //int role_id = data.UserPermissionsIds("role_id", "pos_roles", "roleTitle", role_text.Text);

                //TextData.role_title = role_text.Text;

                //buttonControls.Registration_info();
                checked_values_for_tbl_dashboard_authorities();
                checked_values_for_tbl_Reports_authorities();
                checked_values_for_tbl_authorities_button_controls1();
                checked_values_for_tbl_authorities_button_controls2();
                checked_values_for_tbl_authorities_button_controls3();

                ClassCreateUpdateJsonFile.readWritePermissionsJsonFile();
            }
            
        }

        private void checkAllDashboardAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_dashboard_salaries.Checked = checkUnCheck;
                chk_dashboard_bankLoan.Checked = checkUnCheck;
                chk_dashboard_bankLoanPaybook.Checked = checkUnCheck;
                chk_dashboard_Aboutlicense.Checked = checkUnCheck;
                chk_dashboard_capital.Checked = checkUnCheck;
                chk_dashboard_dailyBalance.Checked = checkUnCheck;
                chk_dashboard_investors.Checked = checkUnCheck;
                chk_dashboard_investorPaybook.Checked = checkUnCheck;
                chk_dashboard_guarantors.Checked = checkUnCheck;
                chk_dashboard_products.Checked = checkUnCheck;
                chk_dashboard_purchases.Checked = checkUnCheck;
                chk_dashboard_expenses.Checked = checkUnCheck;
                chk_dashboard_employee.Checked = checkUnCheck;
                chk_dashboard_recoveries.Checked = checkUnCheck;
                chk_dashboard_customers.Checked = checkUnCheck;
                chk_dashboard_suppliers.Checked = checkUnCheck;
                chk_dashboard_customers_dues.Checked = checkUnCheck;
                chk_dashboard_pos.Checked = checkUnCheck;
                chk_dashboard_reports.Checked = checkUnCheck;
                chk_dashboard_backup.Checked = checkUnCheck;
                chk_dashboard_restore.Checked = checkUnCheck;
                chk_dashboard_settings.Checked = checkUnCheck;
                chk_dashboard_notifications.Checked = checkUnCheck;
                chk_dashboard_banking.Checked = checkUnCheck;
                chk_dashboard_logout.Checked = checkUnCheck;
                chk_dashboard_stock.Checked = checkUnCheck;
                chk_dashboard_supplierPaybook.Checked = checkUnCheck;
                chk_dashboard_Charity.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_dashboardAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_dashboardAuthorities.Checked == true)
            {
                checkAllDashboardAuthorities(true);
            }
            else
            {
                checkAllDashboardAuthorities(false);
            }
        }

        private void checkAllReportAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_reports_generateInvoices.Checked = checkUnCheck;
                chk_reports_chequeDetails.Checked = checkUnCheck;
                chk_reports_customer_ledger.Checked = checkUnCheck;
                chk_reports_sales.Checked = checkUnCheck;
                chk_reports_returns.Checked = checkUnCheck;
                chk_reports_company_statement.Checked = checkUnCheck;
                chk_reports_daybook.Checked = checkUnCheck;
                chk_reports_stock.Checked = checkUnCheck;
                chk_reports_recoveries.Checked = checkUnCheck;
                chk_reports_customer_statement.Checked = checkUnCheck;
                chk_reports_receivables.Checked = checkUnCheck;
                chk_reports_payables.Checked = checkUnCheck;
                chk_reports_balance_in_out.Checked = checkUnCheck;
                chk_reports_income_statement.Checked = checkUnCheck;
                chk_reports_company_ledger.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_ReportsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_ReportsAuthorities.Checked == true)
            {
                checkAllReportAuthorities(true);
            }
            else
            {
                checkAllReportAuthorities(false);
            }
        }

        private void checkAllProductDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_product_details_print.Checked = checkUnCheck;
                chk_product_details_delete.Checked = checkUnCheck;
                chk_product_details_new.Checked = checkUnCheck;
                chk_product_details_modify.Checked = checkUnCheck;
                chk_product_details_regular_items.Checked = checkUnCheck;
                chk_product_details_expired_items.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewProductAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_products_save.Checked = checkUnCheck;
                chk_products_update.Checked = checkUnCheck;
                chk_products_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_ProductDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_ProductDetailsAuthorities.Checked == true)
            {
                checkAllProductDetailsAuthorities(true);
            }
            else
            {
                checkAllProductDetailsAuthorities(false);
            }
        }

        private void chkAll_NewProductAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewProductAuthorities.Checked == true)
            {
                checkAllNewProductAuthorities(true);
            }
            else
            {
                checkAllNewProductAuthorities(false);
            }
        }

        private void checkAllRecoveriesDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_recoveries_details_print.Checked = checkUnCheck;
                chk_recoveries_details_delete.Checked = checkUnCheck;
                chk_recoveries_details_new.Checked = checkUnCheck;
                chk_recoveries_details_modify.Checked = checkUnCheck;
                chk_recoveries_details_invoices.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewRecoveryAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_recoveries_save.Checked = checkUnCheck;
                chk_recoveries_print.Checked = checkUnCheck;
                chk_recoveries_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_RecoveriesDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_RecoveriesDetailsAuthorities.Checked == true)
            {
                checkAllRecoveriesDetailsAuthorities(true);
            }
            else
            {
                checkAllRecoveriesDetailsAuthorities(false);
            }
        }

        private void chkAll_NewRecoveryAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewRecoveryAuthorities.Checked == true)
            {
                checkAllNewRecoveryAuthorities(true);
            }
            else
            {
                checkAllNewRecoveryAuthorities(false);
            }
        }

        private void checkAllExpensesDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_expense_details_print.Checked = checkUnCheck;
                chk_expense_details_delete.Checked = checkUnCheck;
                chk_expense_details_new.Checked = checkUnCheck;
                chk_expense_details_modify.Checked = checkUnCheck;
                chk_expense_details_refresh.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewExpensesAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_expenses_save.Checked = checkUnCheck;
                chk_expenses_update.Checked = checkUnCheck;
                chk_expenses_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_ExpenseDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_ExpenseDetailsAuthorities.Checked == true)
            {
                checkAllExpensesDetailsAuthorities(true);
            }
            else
            {
                checkAllExpensesDetailsAuthorities(false);
            }
        }

        private void chkAll_NewExpenseAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewExpenseAuthorities.Checked == true)
            {
                checkAllNewExpensesAuthorities(true);
            }
            else
            {
                checkAllNewExpensesAuthorities(false);
            }
        }

        private void checkAllCustomerDuesAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_cus_dues_print.Checked = checkUnCheck;
                chk_cus_dues_refresh.Checked = checkUnCheck;
                chk_cus_dues_exit.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllStockDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_stock_whole.Checked = checkUnCheck;
                chk_stock_low.Checked = checkUnCheck;
                chk_stock_print.Checked = checkUnCheck;
                chk_stock_refresh.Checked = checkUnCheck;
                chk_stock_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_CustomerDuesAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_CustomerDuesAuthorities.Checked == true)
            {
                checkAllCustomerDuesAuthorities(true);
            }
            else
            {
                checkAllCustomerDuesAuthorities(false);
            }
        }

        private void chkAll_StockDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_StockDetailsAuthorities.Checked == true)
            {
                checkAllStockDetailsAuthorities(true);
            }
            else
            {
                checkAllStockDetailsAuthorities(false);
            }
        }

        private void checkAllSupplierDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_supplier_details_print.Checked = checkUnCheck;
                chk_supplier_details_delete.Checked = checkUnCheck;
                chk_supplier_details_new.Checked = checkUnCheck;
                chk_supplier_details_modify.Checked = checkUnCheck;
                chk_supplier_details_select.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewSupplierAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_supplier_save.Checked = checkUnCheck;
                chk_supplier_update.Checked = checkUnCheck;
                chk_supplier_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_SupplierDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_SupplierDetailsAuthorities.Checked == true)
            {
                checkAllSupplierDetailsAuthorities(true);
            }
            else
            {
                checkAllSupplierDetailsAuthorities(false);
            }
        }

        private void chkAll_NewSupplierAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewSupplierAuthorities.Checked == true)
            {
                checkAllNewSupplierAuthorities(true);
            }
            else
            {
                checkAllNewSupplierAuthorities(false);
            }
        }

        private void checkAllCustomerDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_cus_details_print.Checked = checkUnCheck;
                chk_cus_details_delete.Checked = checkUnCheck;
                chk_cus_details_new.Checked = checkUnCheck;
                chk_cus_details_modify.Checked = checkUnCheck;
                chk_cus_details_select.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewCustomerAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_customers_save.Checked = checkUnCheck;
                chk_customers_update.Checked = checkUnCheck;
                chk_customers_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_CustomerDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_CustomerDetailsAuthorities.Checked == true)
            {
                checkAllCustomerDetailsAuthorities(true);
            }
            else
            {
                checkAllCustomerDetailsAuthorities(false);
            }
        }

        private void chkAll_NewCustomerAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewCustomerAuthorities.Checked == true)
            {
                checkAllNewCustomerAuthorities(true);
            }
            else
            {
                checkAllNewCustomerAuthorities(false);
            }
        }

        private void checkAllEmployeeDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_employee_details_print.Checked = checkUnCheck;
                chk_employee_details_delete.Checked = checkUnCheck;
                chk_employee_details_new.Checked = checkUnCheck;
                chk_employee_details_modify.Checked = checkUnCheck;
                chk_employee_details_select.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewEmployeeAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_employee_save.Checked = checkUnCheck;
                chk_employee_update.Checked = checkUnCheck;
                chk_employee_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_EmployeeDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_EmployeeDetailsAuthorities.Checked == true)
            {
                checkAllEmployeeDetailsAuthorities(true);
            }
            else
            {
                checkAllEmployeeDetailsAuthorities(false);
            }
        }

        private void chkAll_NewEmployeeAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewEmployeeAuthorities.Checked == true)
            {
                checkAllNewEmployeeAuthorities(true);
            }
            else
            {
                checkAllNewEmployeeAuthorities(false);
            }
        }

        private void checkAllPurchaseDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_purchase_details_print.Checked = checkUnCheck;
                chk_purchase_details_delete.Checked = checkUnCheck;
                chk_purchase_details_new.Checked = checkUnCheck;
                chk_purchase_details_refresh.Checked = checkUnCheck;
                chk_purchase_details_returns.Checked = checkUnCheck;

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewPurchaseAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_purchases_save.Checked = checkUnCheck;
                chk_purchases_update.Checked = checkUnCheck;
                chk_purchases_print.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_PurchaseDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_PurchaseDetailsAuthorities.Checked == true)
            {
                checkAllPurchaseDetailsAuthorities(true);
            }
            else
            {
                checkAllPurchaseDetailsAuthorities(false);
            }
        }

        private void chkAll_NewPurchaseAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewPurchaseAuthorities.Checked == true)
            {
                checkAllNewPurchaseAuthorities(true);
            }
            else
            {
                checkAllNewPurchaseAuthorities(false);
            }
        }

        private void checkAllBankingDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_banking_details_print.Checked = checkUnCheck;
                chk_banking_details_delete.Checked = checkUnCheck;
                chk_banking_details_new.Checked = checkUnCheck;
                chk_banking_details_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewTransactionAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_add_transaction_save.Checked = checkUnCheck;
                chk_add_transaction_update.Checked = checkUnCheck;
                chk_add_transaction_savePrint.Checked = checkUnCheck;
                chk_add_transaction_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkall_BankingAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_BankingAuthorities.Checked == true)
            {
                checkAllBankingDetailsAuthorities(true);
            }
            else
            {
                checkAllBankingDetailsAuthorities(false);
            }
        }

        private void chkall_NewTransactionAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_NewTransactionAuthorities.Checked == true)
            {
                checkAllNewTransactionAuthorities(true);
            }
            else
            {
                checkAllNewTransactionAuthorities(false);
            }
        }

        private void checkAllDemandDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_demand_list_print.Checked = checkUnCheck;
                chk_demand_list_delete.Checked = checkUnCheck;
                chk_demand_list_new.Checked = checkUnCheck;
                chk_demand_list_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewDemandAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_newDemand_save.Checked = checkUnCheck;
                chk_newDemand_update.Checked = checkUnCheck;
                chk_newDemand_savePrint.Checked = checkUnCheck;
                chk_newDemand_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkall_DemandDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_DemandDetailsAuthorities.Checked == true)
            {
                checkAllDemandDetailsAuthorities(true);
            }
            else
            {
                checkAllDemandDetailsAuthorities(false);
            }
        }

        private void chkall_NewDemandAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_NewDemandAuthorities.Checked == true)
            {
                checkAllNewDemandAuthorities(true);
            }
            else
            {
                checkAllNewDemandAuthorities(false);
            }
        }

        private void checkAllControlPanelAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_settings_reg.Checked = checkUnCheck;
                chk_settings_config.Checked = checkUnCheck;
                chk_settings_login_details.Checked = checkUnCheck;
                chk_settings_general.Checked = checkUnCheck;
                chk_settings_reports.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkall_ControlPanelAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_ControlPanelAuthorities.Checked == true)
            {
                checkAllControlPanelAuthorities(true);
            }
            else
            {
                checkAllControlPanelAuthorities(false);
            }
        }

        private void checkAllInvestorsDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_investor_details_print.Checked = checkUnCheck;
                chk_investor_details_delete.Checked = checkUnCheck;
                chk_investor_details_new.Checked = checkUnCheck;
                chk_investor_details_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewInvestorAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_newInvestor_save.Checked = checkUnCheck;
                chk_newInvestor_update.Checked = checkUnCheck;
                chk_newInvestor_barcode.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkall_InvestorDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_InvestorDetailsAuthorities.Checked == true)
            {
                checkAllInvestorsDetailsAuthorities(true);
            }
            else
            {
                checkAllInvestorsDetailsAuthorities(false);
            }
        }

        private void chkall_NewInvestorAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall_NewInvestorAuthorities.Checked == true)
            {
                checkAllNewInvestorAuthorities(true);
            }
            else
            {
                checkAllNewInvestorAuthorities(false);
            }
        }

        private void checkAllInvestorsPaybookDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_InvestorPaymentDetails_print.Checked = checkUnCheck;
                chk_InvestorPaymentDetails_delete.Checked = checkUnCheck;
                chk_InvestorPaymentDetails_new.Checked = checkUnCheck;
                chk_InvestorPaymentDetails_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewInvestorPaymentAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_InvestorNewPayment_save.Checked = checkUnCheck;
                chk_InvestorNewPayment_update.Checked = checkUnCheck;
                chk_InvestorNewPayment_savePrint.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_InvestorPaybookAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_InvestorPaybookAuthorities.Checked == true)
            {
                checkAllInvestorsPaybookDetailsAuthorities(true);
            }
            else
            {
                checkAllInvestorsPaybookDetailsAuthorities(false);
            }
        }

        private void chkAll_NewInvestorPaymentAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewInvestorPaymentAuthorities.Checked == true)
            {
                checkAllNewInvestorPaymentAuthorities(true);
            }
            else
            {
                checkAllNewInvestorPaymentAuthorities(false);
            }
        }

        private void checkAllGuarantorDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_guarantorDetails_print.Checked = checkUnCheck;
                chk_guarantorDetails_delete.Checked = checkUnCheck;
                chk_guarantorDetails_new.Checked = checkUnCheck;
                chk_guarantorDetails_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewGuarantorAuthorities(bool checkUnCheck)
        {
            try
            {
                chk_newGuarantor_save.Checked = checkUnCheck;
                chk_newGuarantor_barcode.Checked = checkUnCheck;
                chk_newGuarantor_update.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_GuarantorDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_GuarantorDetailsAuthorities.Checked == true)
            {
                checkAllGuarantorDetailsAuthorities(true);
            }
            else
            {
                checkAllGuarantorDetailsAuthorities(false);
            }
        }

        private void chkAll_NewGuarantorAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewGuarantorAuthorities.Checked == true)
            {
                checkAllNewGuarantorAuthorities(true);
            }
            else
            {
                checkAllNewGuarantorAuthorities(false);
            }
        }

        private void checkAllCustomerOrdersAuthorities(bool checkUnCheck)
        {
            try
            {
                chkOrders_print.Checked = checkUnCheck;
                chkOrders_delete.Checked = checkUnCheck;
                chkOrders_modify.Checked = checkUnCheck;
                chkOrders_AllSales.Checked = checkUnCheck;
                chkOrders_AllReturns.Checked = checkUnCheck;
                chkOrders_search.Checked = checkUnCheck;
                chkOrders_contractForm.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_CustomerOrdersAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_CustomerOrdersAuthorities.Checked == true)
            {
                checkAllCustomerOrdersAuthorities(true);
            }
            else
            {
                checkAllCustomerOrdersAuthorities(false);
            }
        }

        private void checkAllSalaryPaybookDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chkSalaryPaybook_print.Checked = checkUnCheck;
                chkSalaryPaybook_delete.Checked = checkUnCheck;
                chkSalaryPaybook_new.Checked = checkUnCheck;
                chkSalaryPaybook_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewSalaryPaymentAuthorities(bool checkUnCheck)
        {
            try
            {
                chkNewSalary_save.Checked = checkUnCheck;
                chkNewSalary_update.Checked = checkUnCheck;
                chkNewSalary_savePrint.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_SalaryPaybookAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_SalaryPaybookAuthorities.Checked == true)
            {
                checkAllSalaryPaybookDetailsAuthorities(true);
            }
            else
            {
                checkAllSalaryPaybookDetailsAuthorities(false);
            }
        }

        private void chkAll_NewSalaryPaymentAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewSalaryPaymentAuthorities.Checked == true)
            {
                checkAllNewSalaryPaymentAuthorities(true);
            }
            else
            {
                checkAllNewSalaryPaymentAuthorities(false);
            }
        }

        private void checkAllBankLoanDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chkBankLoanDetails_print.Checked = checkUnCheck;
                chkBankLoanDetails_delete.Checked = checkUnCheck;
                chkBankLoanDetails_new.Checked = checkUnCheck;
                chkBankLoanDetails_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewBankLoanAuthorities(bool checkUnCheck)
        {
            try
            {
                chkBankLoan_save.Checked = checkUnCheck;
                chkBankLoan_update.Checked = checkUnCheck;
                chkBankLoan_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_BankLoanDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_BankLoanDetailsAuthorities.Checked == true)
            {
                checkAllBankLoanDetailsAuthorities(true);
            }
            else
            {
                checkAllBankLoanDetailsAuthorities(false);
            }
        }

        private void chkAll_NewBankLoanAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewBankLoanAuthorities.Checked == true)
            {
                checkAllNewBankLoanAuthorities(true);
            }
            else
            {
                checkAllNewBankLoanAuthorities(false);
            }
        }

        private void checkAllBankLoanPaybookDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chkBankLoanPaymentDetails_print.Checked = checkUnCheck;
                chkBankLoanPaymentDetails_delete.Checked = checkUnCheck;
                chkBankLoanPaymentDetails_new.Checked = checkUnCheck;
                chkBankLoanPaymentDetails_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewBankLoanPaymentAuthorities(bool checkUnCheck)
        {
            try
            {
                chkBankLoanPayment_save.Checked = checkUnCheck;
                chkBankLoanPayment_update.Checked = checkUnCheck;
                chkBankLoanPayment_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_BankLoanPaymentDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_BankLoanPaymentDetailsAuthorities.Checked == true)
            {
                checkAllBankLoanPaybookDetailsAuthorities(true);
            }
            else
            {
                checkAllBankLoanPaybookDetailsAuthorities(false);
            }
        }

        private void chkAll_NewBankLoanPaymentAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewBankLoanPaymentAuthorities.Checked == true)
            {
                checkAllNewBankLoanPaymentAuthorities(true);
            }
            else
            {
                checkAllNewBankLoanPaymentAuthorities(false);
            }
        }

        private void checkAllSupplierPaybookDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chkSupplierPaybook_print.Checked = checkUnCheck;
                chkSupplierPaybook_delete.Checked = checkUnCheck;
                chkSupplierPaybook_new.Checked = checkUnCheck;
                chkSupplierPaybook_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewSupplierPaymentAuthorities(bool checkUnCheck)
        {
            try
            {
                chkSupplierPayment_save.Checked = checkUnCheck;
                chkSupplierPayment_update.Checked = checkUnCheck;
                chkSupplierPayment_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_SupplierPaybookDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_SupplierPaybookDetailsAuthorities.Checked == true)
            {
                checkAllSupplierPaybookDetailsAuthorities(true);
            }
            else
            {
                checkAllSupplierPaybookDetailsAuthorities(false);
            }
        }

        private void chkAll_NewSupplierPaymentAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewSupplierPaymentAuthorities.Checked == true)
            {
                checkAllSupplierPaybookDetailsAuthorities(true);
            }
            else
            {
                checkAllSupplierPaybookDetailsAuthorities(false);
            }
        }

        private void checkAllCharityDetailsAuthorities(bool checkUnCheck)
        {
            try
            {
                chkCharityPaybook_print.Checked = checkUnCheck;
                chkCharityPaybook_delete.Checked = checkUnCheck;
                chkCharityPaybook_new.Checked = checkUnCheck;
                chkCharityPaybook_modify.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void checkAllNewCharityPaymentAuthorities(bool checkUnCheck)
        {
            try
            {
                chkCharityPayment_save.Checked = checkUnCheck;
                chkCharityPayment_update.Checked = checkUnCheck;
                chkCharityPayment_exit.Checked = checkUnCheck;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void chkAll_CharityDetailsAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_CharityDetailsAuthorities.Checked == true)
            {
                checkAllCharityDetailsAuthorities(true);
            }
            else
            {
                checkAllCharityDetailsAuthorities(false);
            }
        }

        private void chkAll_NewCharityAuthorities_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll_NewCharityAuthorities.Checked == true)
            {
                checkAllNewCharityPaymentAuthorities(true);
            }
            else
            {
                checkAllNewCharityPaymentAuthorities(false);
            }
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            using (AddRole add_customer = new AddRole())
            {
                AddRole.title = role_text.Text;

                add_customer.ShowDialog();

                FillComboBoxWithItems();
            }
        }

        private void fetch_dashboard_authorities(string roleId)
        {
            chk_dashboard_salaries.Checked = bool.Parse(data.UserPermissions("EmployeeSalaries", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_bankLoan.Checked = bool.Parse(data.UserPermissions("bankLoan", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_bankLoanPaybook.Checked = bool.Parse(data.UserPermissions("bankLoanPaybook", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_Aboutlicense.Checked = bool.Parse(data.UserPermissions("aboutLicense", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_capital.Checked = bool.Parse(data.UserPermissions("capital", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_dailyBalance.Checked = bool.Parse(data.UserPermissions("dailyBalance", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_investors.Checked = bool.Parse(data.UserPermissions("investors", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_investorPaybook.Checked = bool.Parse(data.UserPermissions("investorPaybook", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_guarantors.Checked = bool.Parse(data.UserPermissions("guarantors", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_products.Checked = bool.Parse(data.UserPermissions("products", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_purchases.Checked = bool.Parse(data.UserPermissions("purchases", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_expenses.Checked = bool.Parse(data.UserPermissions("expenses", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_employee.Checked = bool.Parse(data.UserPermissions("employee", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_recoveries.Checked = bool.Parse(data.UserPermissions("recoveries", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_customers.Checked = bool.Parse(data.UserPermissions("customers", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_suppliers.Checked = bool.Parse(data.UserPermissions("suppliers", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_customers_dues.Checked = bool.Parse(data.UserPermissions("customer_dues", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_pos.Checked = bool.Parse(data.UserPermissions("pos", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_reports.Checked = bool.Parse(data.UserPermissions("reports", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_backup.Checked = bool.Parse(data.UserPermissions("backups", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_restore.Checked = bool.Parse(data.UserPermissions("restores", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_settings.Checked = bool.Parse(data.UserPermissions("settings", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_notifications.Checked = bool.Parse(data.UserPermissions("notifications", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_banking.Checked = bool.Parse(data.UserPermissions("about", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_logout.Checked = bool.Parse(data.UserPermissions("logout", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_stock.Checked = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_supplierPaybook.Checked = bool.Parse(data.UserPermissions("supplierPaybook", "pos_tbl_authorities_dashboard", "role_id", roleId));
            chk_dashboard_Charity.Checked = bool.Parse(data.UserPermissions("charity", "pos_tbl_authorities_dashboard", "role_id", roleId));


        }

        private void fetch_Reports_authorities(string roleId)
        {
            chk_reports_generateInvoices.Checked = bool.Parse(data.UserPermissions("generateInvoices", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_chequeDetails.Checked = bool.Parse(data.UserPermissions("chequeDetails", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_customer_ledger.Checked = bool.Parse(data.UserPermissions("customer_ledger", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_customer_statement.Checked = bool.Parse(data.UserPermissions("customer_statement", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_company_ledger.Checked = bool.Parse(data.UserPermissions("company_ledger", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_company_statement.Checked = bool.Parse(data.UserPermissions("company_statement", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_sales.Checked = bool.Parse(data.UserPermissions("sales_report", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_returns.Checked = bool.Parse(data.UserPermissions("returns_report", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_daybook.Checked = bool.Parse(data.UserPermissions("day_book", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_stock.Checked = bool.Parse(data.UserPermissions("stock", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_recoveries.Checked = bool.Parse(data.UserPermissions("recoveries", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_receivables.Checked = bool.Parse(data.UserPermissions("receivables", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_payables.Checked = bool.Parse(data.UserPermissions("payables", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_balance_in_out.Checked = bool.Parse(data.UserPermissions("balance_in_out", "pos_tbl_authorities_reports", "role_id", roleId));
            chk_reports_income_statement.Checked = bool.Parse(data.UserPermissions("income_statement", "pos_tbl_authorities_reports", "role_id", roleId));

        }

        private void fetch_authorities_button_controls1(string roleId)
        {
            chk_supplier_details_print.Checked = bool.Parse(data.UserPermissions("supplier_details_print", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_details_delete.Checked = bool.Parse(data.UserPermissions("supplier_details_delete", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_details_new.Checked = bool.Parse(data.UserPermissions("supplier_details_new", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_details_modify.Checked = bool.Parse(data.UserPermissions("supplier_details_modify", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_details_select.Checked = bool.Parse(data.UserPermissions("supplier_details_select", "pos_tbl_authorities_button_controls1", "role_id", roleId));
         
            chk_supplier_save.Checked = bool.Parse(data.UserPermissions("suppliers_new", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_update.Checked = bool.Parse(data.UserPermissions("suppliers_update", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_supplier_exit.Checked = bool.Parse(data.UserPermissions("suppliers_exit", "pos_tbl_authorities_button_controls1", "role_id", roleId));

            //*********************************************************

            chk_cus_details_print.Checked = bool.Parse(data.UserPermissions("customer_details_print", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_cus_details_delete.Checked = bool.Parse(data.UserPermissions("customer_details_delete", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_cus_details_new.Checked = bool.Parse(data.UserPermissions("customer_details_new", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_cus_details_modify.Checked = bool.Parse(data.UserPermissions("customer_details_modify", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_cus_details_select.Checked = bool.Parse(data.UserPermissions("customer_details_select", "pos_tbl_authorities_button_controls1", "role_id", roleId));


            chk_customers_save.Checked = bool.Parse(data.UserPermissions("customers_save", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_customers_update.Checked = bool.Parse(data.UserPermissions("customers_update", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_customers_exit.Checked = bool.Parse(data.UserPermissions("customers_exit", "pos_tbl_authorities_button_controls1", "role_id", roleId));

            //*********************************************************

            chk_employee_details_print.Checked = bool.Parse(data.UserPermissions("employee_details_print", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_details_delete.Checked = bool.Parse(data.UserPermissions("employee_details_delete", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_details_new.Checked = bool.Parse(data.UserPermissions("employee_details_new", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_details_modify.Checked = bool.Parse(data.UserPermissions("employee_details_modify", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_details_select.Checked = bool.Parse(data.UserPermissions("employee_details_select", "pos_tbl_authorities_button_controls1", "role_id", roleId));


            chk_employee_save.Checked = bool.Parse(data.UserPermissions("employees_save", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_update.Checked = bool.Parse(data.UserPermissions("employees_update", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_employee_exit.Checked = bool.Parse(data.UserPermissions("employees_exit", "pos_tbl_authorities_button_controls1", "role_id", roleId));

            //*********************************************************

            chk_purchase_details_print.Checked = bool.Parse(data.UserPermissions("purchase_details_print", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchase_details_delete.Checked = bool.Parse(data.UserPermissions("purchase_details_delete", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchase_details_new.Checked = bool.Parse(data.UserPermissions("purchase_details_new", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchase_details_refresh.Checked = bool.Parse(data.UserPermissions("purchase_details_refresh", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchase_details_returns.Checked = bool.Parse(data.UserPermissions("purchase_details_returns", "pos_tbl_authorities_button_controls1", "role_id", roleId));


            chk_purchases_save.Checked = bool.Parse(data.UserPermissions("purchases_save", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchases_update.Checked = bool.Parse(data.UserPermissions("purchases_exit", "pos_tbl_authorities_button_controls1", "role_id", roleId));
            chk_purchases_print.Checked = bool.Parse(data.UserPermissions("purchases_print", "pos_tbl_authorities_button_controls1", "role_id", roleId));

            //*********************************************************

        }

        private void fetch_authorities_button_controls2(string roleId)
        {
            chk_product_details_print.Checked = bool.Parse(data.UserPermissions("products_details_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_product_details_delete.Checked = bool.Parse(data.UserPermissions("products_details_delete", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_product_details_new.Checked = bool.Parse(data.UserPermissions("products_details_new", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_product_details_modify.Checked = bool.Parse(data.UserPermissions("products_details_modify", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_product_details_regular_items.Checked = bool.Parse(data.UserPermissions("products_details_regular", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_product_details_expired_items.Checked = bool.Parse(data.UserPermissions("products_expired", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            chk_products_save.Checked = bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_products_update.Checked = bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_products_exit.Checked = bool.Parse(data.UserPermissions("products_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_recoveries_details_print.Checked = bool.Parse(data.UserPermissions("recovery_details_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_details_delete.Checked = bool.Parse(data.UserPermissions("recovery_details_delete", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_details_new.Checked = bool.Parse(data.UserPermissions("recovery_details_new", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_details_modify.Checked = bool.Parse(data.UserPermissions("recovery_details_modify", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_details_invoices.Checked = bool.Parse(data.UserPermissions("recovery_details_Invoices", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            chk_recoveries_save.Checked = bool.Parse(data.UserPermissions("recoveries_save", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_print.Checked = bool.Parse(data.UserPermissions("recoveries_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_recoveries_exit.Checked = bool.Parse(data.UserPermissions("recoveries_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_expense_details_print.Checked = bool.Parse(data.UserPermissions("expenses_details_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expense_details_delete.Checked = bool.Parse(data.UserPermissions("expenses_details_delete", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expense_details_new.Checked = bool.Parse(data.UserPermissions("expenses_details_new", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expense_details_modify.Checked = bool.Parse(data.UserPermissions("expenses_details_modify", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expense_details_refresh.Checked = bool.Parse(data.UserPermissions("expenses_details_refresh", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            chk_expenses_save.Checked = bool.Parse(data.UserPermissions("expenses_save", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expenses_update.Checked = bool.Parse(data.UserPermissions("expenses_update", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_expenses_exit.Checked = bool.Parse(data.UserPermissions("expenses_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_cus_dues_print.Checked = bool.Parse(data.UserPermissions("dues_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_cus_dues_refresh.Checked = bool.Parse(data.UserPermissions("dues_refresh", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_cus_dues_exit.Checked = bool.Parse(data.UserPermissions("dues_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_stock_whole.Checked = bool.Parse(data.UserPermissions("stock_whole", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_stock_low.Checked = bool.Parse(data.UserPermissions("stock_low", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_stock_print.Checked = bool.Parse(data.UserPermissions("stock_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_stock_refresh.Checked = bool.Parse(data.UserPermissions("stock_refresh", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_stock_exit.Checked = bool.Parse(data.UserPermissions("stock_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************


            chk_banking_details_print.Checked = bool.Parse(data.UserPermissions("banking_details_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_banking_details_delete.Checked = bool.Parse(data.UserPermissions("banking_details_delete", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_banking_details_new.Checked = bool.Parse(data.UserPermissions("banking_details_new", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_banking_details_modify.Checked = bool.Parse(data.UserPermissions("banking_details_modify", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            chk_add_transaction_save.Checked = bool.Parse(data.UserPermissions("new_transaction_save", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_add_transaction_update.Checked = bool.Parse(data.UserPermissions("new_transaction_update", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_add_transaction_savePrint.Checked = bool.Parse(data.UserPermissions("new_transaction_savePrint", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_add_transaction_exit.Checked = bool.Parse(data.UserPermissions("new_transaction_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_demand_list_print.Checked = bool.Parse(data.UserPermissions("demand_list_print", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_demand_list_delete.Checked = bool.Parse(data.UserPermissions("demand_list_delete", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_demand_list_new.Checked = bool.Parse(data.UserPermissions("demand_list_new", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_demand_list_modify.Checked = bool.Parse(data.UserPermissions("demand_list_modify", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            chk_newDemand_save.Checked = bool.Parse(data.UserPermissions("new_demand_save", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_newDemand_update.Checked = bool.Parse(data.UserPermissions("new_demand_update", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_newDemand_savePrint.Checked = bool.Parse(data.UserPermissions("new_demand_savePrint", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_newDemand_exit.Checked = bool.Parse(data.UserPermissions("new_demand_exit", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

            chk_settings_reg.Checked = bool.Parse(data.UserPermissions("settings_reg", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_settings_config.Checked = bool.Parse(data.UserPermissions("settings_config", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_settings_reports.Checked = bool.Parse(data.UserPermissions("settings_reports", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_settings_login_details.Checked = bool.Parse(data.UserPermissions("settings_login_details", "pos_tbl_authorities_button_controls2", "role_id", roleId));
            chk_settings_general.Checked = bool.Parse(data.UserPermissions("settings_general", "pos_tbl_authorities_button_controls2", "role_id", roleId));

            //*********************************************************

        }

        private void fetch_authorities_button_controls3(string roleId)
        {
            chk_investor_details_print.Checked = bool.Parse(data.UserPermissions("investor_details_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_investor_details_delete.Checked = bool.Parse(data.UserPermissions("investor_details_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_investor_details_new.Checked = bool.Parse(data.UserPermissions("investor_details_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_investor_details_modify.Checked = bool.Parse(data.UserPermissions("investor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chk_newInvestor_save.Checked = bool.Parse(data.UserPermissions("new_investor_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_newInvestor_update.Checked = bool.Parse(data.UserPermissions("new_investor_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_newInvestor_barcode.Checked = bool.Parse(data.UserPermissions("new_investor_barcode", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chk_InvestorPaymentDetails_print.Checked = bool.Parse(data.UserPermissions("investor_paybookDetails_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_InvestorPaymentDetails_delete.Checked = bool.Parse(data.UserPermissions("investor_paybookDetails_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_InvestorPaymentDetails_new.Checked = bool.Parse(data.UserPermissions("investor_paybookDetails_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_InvestorPaymentDetails_modify.Checked = bool.Parse(data.UserPermissions("investor_paybookDetails_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chk_InvestorNewPayment_save.Checked = bool.Parse(data.UserPermissions("new_investorPayment_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_InvestorNewPayment_savePrint.Checked = bool.Parse(data.UserPermissions("new_investorPayment_savePrint", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_InvestorNewPayment_update.Checked = bool.Parse(data.UserPermissions("new_investorPayment_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chk_guarantorDetails_print.Checked = bool.Parse(data.UserPermissions("guarantor_details_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_guarantorDetails_delete.Checked = bool.Parse(data.UserPermissions("guarantor_details_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_guarantorDetails_new.Checked = bool.Parse(data.UserPermissions("guarantor_details_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_guarantorDetails_modify.Checked = bool.Parse(data.UserPermissions("guarantor_details_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chk_newGuarantor_save.Checked = bool.Parse(data.UserPermissions("new_guarantor_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_newGuarantor_update.Checked = bool.Parse(data.UserPermissions("new_guarantor_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chk_newGuarantor_barcode.Checked = bool.Parse(data.UserPermissions("new_guarantor_barcode", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkOrders_print.Checked = bool.Parse(data.UserPermissions("customerOrders_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_delete.Checked = bool.Parse(data.UserPermissions("customerOrders_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_modify.Checked = bool.Parse(data.UserPermissions("customerOrders_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_AllSales.Checked = bool.Parse(data.UserPermissions("customerOrders_allSales", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_AllReturns.Checked = bool.Parse(data.UserPermissions("customerOrders_allReturns", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_search.Checked = bool.Parse(data.UserPermissions("customerOrders_search", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkOrders_contractForm.Checked = bool.Parse(data.UserPermissions("customerOrders_contractForm", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkSalaryPaybook_print.Checked = bool.Parse(data.UserPermissions("salariesPaybook_details_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSalaryPaybook_delete.Checked = bool.Parse(data.UserPermissions("salariesPaybook_details_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSalaryPaybook_new.Checked = bool.Parse(data.UserPermissions("salariesPaybook_details_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSalaryPaybook_modify.Checked = bool.Parse(data.UserPermissions("salariesPaybook_details_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chkNewSalary_save.Checked = bool.Parse(data.UserPermissions("new_salaryPayment_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkNewSalary_savePrint.Checked = bool.Parse(data.UserPermissions("new_salaryPayment_savePrint", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkNewSalary_update.Checked = bool.Parse(data.UserPermissions("new_salaryPayment_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkBankLoanDetails_print.Checked = bool.Parse(data.UserPermissions("bankLoan_details_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanDetails_delete.Checked = bool.Parse(data.UserPermissions("bankLoan_details_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanDetails_new.Checked = bool.Parse(data.UserPermissions("bankLoan_details_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanDetails_modify.Checked = bool.Parse(data.UserPermissions("bankLoan_details_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chkBankLoan_save.Checked = bool.Parse(data.UserPermissions("new_bankLoan_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoan_update.Checked = bool.Parse(data.UserPermissions("new_bankLoan_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoan_exit.Checked = bool.Parse(data.UserPermissions("new_bankLoan_exit", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkBankLoanPaymentDetails_print.Checked = bool.Parse(data.UserPermissions("bankLoanPaybook_details_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanPaymentDetails_delete.Checked = bool.Parse(data.UserPermissions("bankLoanPaybook_details_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanPaymentDetails_new.Checked = bool.Parse(data.UserPermissions("bankLoanPaybook_details_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanPaymentDetails_modify.Checked = bool.Parse(data.UserPermissions("bankLoanPaybook_details_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chkBankLoanPayment_save.Checked = bool.Parse(data.UserPermissions("new_bankLoanPayment_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanPayment_update.Checked = bool.Parse(data.UserPermissions("new_bankLoanPayment_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkBankLoanPayment_exit.Checked = bool.Parse(data.UserPermissions("new_bankLoanPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkSupplierPaybook_print.Checked = bool.Parse(data.UserPermissions("supplierPaybook_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSupplierPaybook_delete.Checked = bool.Parse(data.UserPermissions("supplierPaybook_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSupplierPaybook_new.Checked = bool.Parse(data.UserPermissions("supplierPaybook_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSupplierPaybook_modify.Checked = bool.Parse(data.UserPermissions("supplierPaybook_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chkSupplierPayment_save.Checked = bool.Parse(data.UserPermissions("newSupplierPayment_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSupplierPayment_update.Checked = bool.Parse(data.UserPermissions("newSupplierPayment_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkSupplierPayment_exit.Checked = bool.Parse(data.UserPermissions("newSupplierPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            //*********************************************************

            chkCharityPaybook_print.Checked = bool.Parse(data.UserPermissions("charityPaybook_print", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkCharityPaybook_delete.Checked = bool.Parse(data.UserPermissions("charityPaybook_delete", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkCharityPaybook_new.Checked = bool.Parse(data.UserPermissions("charityPaybook_new", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkCharityPaybook_modify.Checked = bool.Parse(data.UserPermissions("charityPaybook_modify", "pos_tbl_authorities_button_controls3", "role_id", roleId));

            chkCharityPayment_save.Checked = bool.Parse(data.UserPermissions("newCharityPayment_save", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkCharityPayment_update.Checked = bool.Parse(data.UserPermissions("newCharityPayment_update", "pos_tbl_authorities_button_controls3", "role_id", roleId));
            chkCharityPayment_exit.Checked = bool.Parse(data.UserPermissions("newCharityPayment_exit", "pos_tbl_authorities_button_controls3", "role_id", roleId));


            //*********************************************************

        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_text.Text != "")
                {
                    string role_id = data.UserPermissions("role_id", "pos_roles", "roleTitle", role_text.Text);

                    TextData.role_title = role_text.Text;

                    fetch_dashboard_authorities(role_id);
                    fetch_Reports_authorities(role_id);
                    fetch_authorities_button_controls1(role_id);
                    fetch_authorities_button_controls2(role_id);
                    fetch_authorities_button_controls3(role_id);
                }
                else
                {
                    error.errorMessage("Select the role first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void role_text_Enter(object sender, EventArgs e)
        {
            FillComboBoxWithItems();
        }

        private void formRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
