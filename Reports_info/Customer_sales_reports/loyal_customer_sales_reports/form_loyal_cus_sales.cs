using Customers_info.forms;
using Datalayer;
using Login_info.controllers;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using Products_info.forms;
using RefereningMaterial;
using Reports_info.controllers;
using Spices_pos.DashboardInfo.Forms;
using Spices_pos.DatabaseInfo.WebConfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reports_info.Customer_sales_reports.loyal_customer_sales_reports
{
    public partial class form_loyal_cus_sales : Form
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
        public form_loyal_cus_sales()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static int user_id = 0;
        public string product_id_db = "";
        public string stock_id_db = "";
        public int reportType = 0; // 0 for date, 1 for bill, 2 for category, 3 for brand, 4 for customer, 5 for salesman, 6 for product, 7 for void, 8 for summary


        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_name_text.Text);
        }

        private void FillComboBoxEmployeeName()
        {
            txt_saleman_name.Text = data.UserPermissions("full_name", "pos_employees", "emp_code", txt_saleman_code.Text);
        }
        
        private void FillComboBoxEmployeeCodes()
        {
            txt_saleman_code.Text = data.UserPermissions("emp_code", "pos_employees", "full_name", txt_saleman_name.Text);
        }

        private void RefreshComboBoxCustomerName()
        {
            try
            {
                customer_name_text.Text = null;
                customer_name_text.Items.Clear();
                GetSetData.FillComboBoxWithValues("select top 30 full_name from pos_customers;", "full_name", customer_name_text);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void RefreshComboBoxCategory()
        {
            try
            {
                customer_name_text.Text = null;
                customer_name_text.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_category;", "title", customer_name_text);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void RefreshComboBoxBrand()
        {
            try
            {
                customer_name_text.Text = null;
                customer_name_text.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_brand;", "brand_title", customer_name_text);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void RefreshComboBoxProduct()
        {
            try
            {
                txt_product.Text = null;
                txt_product.Items.Clear();
                GetSetData.FillComboBoxWithValues("select prod_name from pos_products;", "prod_name", txt_product);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            customer_code_text.Text = null;
            txt_saleman_name.Text = null;
            txt_saleman_code.Text = null;

            customer_code_text.Items.Clear();
            txt_saleman_name.Items.Clear();
            txt_saleman_code.Items.Clear();


            lbl_cus_name.Visible = false;
            lbl_cus_code.Visible = false;
            lbl_saleman_name.Visible = false;
            lbl_saleman_code.Visible = false;
            lbl_billNo.Visible = false;

            customer_name_text.Visible = false;
            customer_code_text.Visible = false;
            txt_saleman_name.Visible = false;
            txt_saleman_code.Visible = false;
            txt_product.Visible = false;
            txt_billNo.Visible = false;


            pnl_date_wise.Visible = true;
            pnl_all_transaction.Visible = false;
            pnl_bill_wise.Visible = false;
            pnl_saleman.Visible = false;
            pnl_category.Visible = false;
            pnl_brand.Visible = false;
            pnl_product.Visible = false;
            pnl_summary.Visible = false;
            pnl_void.Visible = false;
            pnlTaxation.Visible = false;


            this.pnl_date_wise.Dock = DockStyle.Fill;

            this.Viewer_dateWise.Clear();
            this.viewer_bill_wise.Clear();
            this.viewer_all_transaction.Clear();
            this.viewer_saleman.Clear();
            this.viewer_category.Clear();
            this.viewer_brand.Clear();
            this.viewer_summary.Clear();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //Menus.user_id = user_id;
            //Menus.role_id = role_id;
            button_controls.Main_menu();
            this.Dispose();
        }

        private void form_loader_cus_sales_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //date_wise_sales();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }

            this.viewer_product.RefreshReport();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                this.pnl_date_wise.Dock = DockStyle.Fill;

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = true;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_bill_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Receipt Wise Report";
                reportType = 1;

                customer_code_text.Text = null;
                customer_name_text.Text = null;
                lbl_billNo.Visible = true;
                txt_billNo.Visible = true;

                pnl_bill_wise.Visible = true;
                this.pnl_bill_wise.Dock = DockStyle.Fill;

               
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void all_transactions_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;

                lblReportTitle.Text = "Customer Wise Report";
                reportType = 4;

                this.pnl_all_transaction.Dock = DockStyle.Fill;
                
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = true;
                pnl_bill_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = true;
                //lbl_cus_code.Visible = true;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;
                lbl_cus_name.Text = "Name:";

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = true;
                //customer_code_text.Visible = true;
                txt_billNo.Visible = false;
                txt_product.Visible = false;

                //RefreshComboBoxCustomerName();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_salesman_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                txt_saleman_name.Text = null;
                txt_saleman_code.Text = null;
               
                lblReportTitle.Text = "Salesman Wise Report";
                reportType = 5;

                this.pnl_saleman.Dock = DockStyle.Fill;
                    
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = true;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = true;
                lbl_saleman_code.Visible = true;
                pnl_product.Visible = false;
                lbl_saleman_name.Text = "Name:";

                txt_saleman_name.Visible = true;
                txt_saleman_code.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnCategoryWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshComboBoxCategory();

                lblReportTitle.Text = "Category Wise Report";
                reportType = 2;

                this.pnl_category.Dock = DockStyle.Fill;

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = true;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;
                lbl_cus_name.Text = "Category:";

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = true;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;
                txt_product.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnBrandWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_name_text.Text = null;
                RefreshComboBoxBrand();

                lblReportTitle.Text = "Brand Wise Report";
                reportType = 3;

                this.pnl_brand.Dock = DockStyle.Fill;
                   

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = true;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;
                lbl_cus_name.Text = "Brand:";

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = true;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;
                txt_product.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }


        private void btn_product_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txt_product.Text = null;
                RefreshComboBoxProduct();
               
                lblReportTitle.Text = "Product Wise Report";
                reportType = 6;

                this.pnl_product.Dock = DockStyle.Fill;
                    

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = true;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = true;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;
                lbl_cus_name.Text = "Product:";

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;
                txt_product.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnSummary_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                pnl_summary.Visible = true;

                lblReportTitle.Text = "Summary Report";
                reportType = 8;

                this.pnl_summary.Dock = DockStyle.Fill;
                

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_product.Visible = false;
                pnl_brand.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_billNo.Visible = false;
                txt_product.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnVoid_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                txt_saleman_name.Text = null;
                txt_saleman_code.Text = null;

                lblReportTitle.Text = "Void Orders Report";
                reportType = 7;

                this.pnl_void.Dock = DockStyle.Fill;
                    

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = true;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = true;
                lbl_saleman_code.Visible = true;
                pnl_product.Visible = false;
                lbl_saleman_name.Text = "Name:";

                txt_saleman_name.Visible = true;
                txt_saleman_code.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnNoSaleReport_Click(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                txt_saleman_name.Text = null;
                txt_saleman_code.Text = null;

                lblReportTitle.Text = "No Sale Items Report";
                reportType = 9;

                this.pnlNoSale.Dock = DockStyle.Fill;


                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = true;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = true;
                lbl_saleman_code.Visible = true;
                pnl_product.Visible = false;
                lbl_saleman_name.Text = "Name:";

                txt_saleman_name.Visible = true;
                txt_saleman_code.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnPayoutReport_Click(object sender, EventArgs e)
        {
            try
            {
                customer_code_text.Text = null;
                customer_name_text.Text = null;
                txt_saleman_name.Text = null;
                txt_saleman_code.Text = null;

                lblReportTitle.Text = "Payout Report";
                reportType = 10;

                this.pnlPayout.Dock = DockStyle.Fill;


                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = true;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = false;

                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = true;
                lbl_saleman_code.Visible = true;
                pnl_product.Visible = false;
                lbl_saleman_name.Text = "Name:";

                txt_saleman_name.Visible = true;
                txt_saleman_code.Visible = true;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void customer_code_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxCustomerName();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            // ReportViewer setup
            viewer.ZoomMode = ZoomMode.Percent;
            viewer.ZoomPercent = 100;
            viewer.LocalReport.EnableExternalImages = true;

            // Collect parameters
            var parameters = new List<ReportParameter>();

            // ------------------ Logo ------------------
            GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
            GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");

            if (!string.IsNullOrEmpty(GetSetData.query) && GetSetData.query != "nill")
            {
                GetSetData.query = GetSetData.Data + GetSetData.query;
                parameters.Add(new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri));
            }
            else
            {
                parameters.Add(new ReportParameter("pLogo", ""));
            }

            // ------------------ Title, Address, Phone ------------------
            GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
            parameters.Add(new ReportParameter("pTitle", string.IsNullOrEmpty(GetSetData.Data) ? "" : GetSetData.Data));

            GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
            parameters.Add(new ReportParameter("pAddress", string.IsNullOrEmpty(GetSetData.Data) ? "" : GetSetData.Data));

            GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
            parameters.Add(new ReportParameter("pPhone", string.IsNullOrEmpty(GetSetData.Data) ? "" : GetSetData.Data));

            // ------------------ Note ------------------
            GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
            parameters.Add(new ReportParameter("pNote", string.IsNullOrEmpty(GetSetData.Data) || GetSetData.Data == "nill" ? "" : GetSetData.Data));

            // ------------------ Copyrights ------------------
            GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
            parameters.Add(new ReportParameter("pCopyrights", string.IsNullOrEmpty(GetSetData.Data) ? "" : GetSetData.Data));

            // ------------------ showNote ------------------
            GetSetData.Data = data.UserPermissions("showNoteInReport", "pos_general_settings");

            // Pass "Yes" or "No" as string to match RDLC
            string showNoteValue = string.IsNullOrEmpty(GetSetData.Data) || GetSetData.Data == "nill"
                                   ? "Yes"   // default: show
                                   : GetSetData.Data;
            parameters.Add(new ReportParameter("showNote", showNoteValue));


            // ------------------ Currency ------------------
            parameters.Add(new ReportParameter("pCurrency", GetSetData.currency()));

            // ------------------ Set all parameters ------------------
            viewer.LocalReport.SetParameters(parameters.ToArray());

            // Refresh report
            viewer.RefreshReport();
        }

        //private void date_wise_sales()
        //{
        //    try
        //    {
        //        customer_sales_ds report = new customer_sales_ds();

        //        GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
        //                            pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
        //                            pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
        //                            pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
        //                            pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
        //                            pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
        //                            FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
        //                            INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN
        //                            pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
        //                            pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
        //                            pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment') and (pos_category.title != 'MISCELLANEOUS') ORDER BY pos_sales_accounts.date asc;";

        //        SqlConnection conn = new SqlConnection(webConfig.con_string);
        //        SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
        //        da.Fill(report, report.Tables["DataTable1"].TableName);

        //        ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["DataTable1"]);
        //        this.Viewer_dateWise.LocalReport.DataSources.Clear();
        //        this.Viewer_dateWise.SetDisplayMode(DisplayMode.PrintLayout);
        //        this.Viewer_dateWise.LocalReport.DataSources.Add(rds);
        //        this.Viewer_dateWise.LocalReport.Refresh();

        //        //Return Items List****************************************************************
        //        customer_sales_ds sales_return_report = new customer_sales_ds();
        //        ReportDataSource sales_return_rds = null;
        //        SqlDataAdapter sales_return_da = null;

        //        GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
        //                            pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
        //                            pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
        //                            FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
        //                            INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

        //        sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
        //        sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
        //        sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
        //        this.Viewer_dateWise.LocalReport.DataSources.Add(sales_return_rds);
        //        //*******************************************************************************************

        //        customer_sales_ds void_orders_report = new customer_sales_ds();
        //        ReportDataSource void_orders_rds = null;
        //        SqlDataAdapter void_orders_da = null;

        //        GetSetData.query = @"SELECT pos_products.prod_name, pos_void_orders.date, pos_void_orders.quantity, pos_void_orders.pkg, pos_void_orders.full_pkg, 
        //                            pos_void_orders.discount, pos_void_orders.total_purchase, pos_void_orders.Total_price, pos_void_orders.total_marketPrice, 
        //                            pos_void_orders.total_wholeSale, pos_void_orders.note, pos_customers.full_name as customerName, pos_employees.full_name AS employeeName
        //                            FROM pos_void_orders INNER JOIN pos_clock_in ON pos_void_orders.clock_in_id = pos_clock_in.id INNER JOIN pos_products ON pos_void_orders.prod_id = pos_products.product_id INNER JOIN
        //                            pos_customers ON pos_void_orders.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_void_orders.employee_id = pos_employees.employee_id
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_void_orders.date asc;";


        //        void_orders_da = new SqlDataAdapter(GetSetData.query, conn);
        //        void_orders_da.Fill(void_orders_report, void_orders_report.Tables["VoidOrders"].TableName);

        //        void_orders_rds = new ReportDataSource("voidOrders", void_orders_report.Tables["VoidOrders"]);
        //        this.Viewer_dateWise.LocalReport.DataSources.Add(void_orders_rds);
        //        //*******************************************************************************************


        //        customer_sales_ds misc_items_report = new customer_sales_ds();
        //        ReportDataSource misc_items_rds = null;
        //        SqlDataAdapter misc_items_da = null;

        //        GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
        //                            pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
        //                            pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
        //                            pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
        //                            pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
        //                            pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
        //                            FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
        //                            INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN
        //                            pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
        //                            pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
        //                            pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment') and (pos_category.title = 'MISCELLANEOUS') ORDER BY pos_sales_accounts.date asc;";


        //        misc_items_da = new SqlDataAdapter(GetSetData.query, conn);
        //        misc_items_da.Fill(misc_items_report, misc_items_report.Tables["MiscItems"].TableName);

        //        misc_items_rds = new ReportDataSource("miscItems", misc_items_report.Tables["MiscItems"]);
        //        this.Viewer_dateWise.LocalReport.DataSources.Add(misc_items_rds);
        //        //*******************************************************************************************



        //        DisplayReportInReportViewer(this.Viewer_dateWise);
        //        // Retrive Report Settings from db *******************************************************************************************
        //        GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
        //                            pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

        //        string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (return_amount_sale == "" || return_amount_sale == "NULL")
        //        {
        //            return_amount_sale = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
        //	INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

        //        string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (return_perItem_discount == "" || return_perItem_discount == "NULL")
        //        {
        //            return_perItem_discount = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
        //	INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

        //        string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (return_amount_purchase == "" || return_amount_purchase == "NULL")
        //        {
        //            return_amount_purchase = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(paid) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                             WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
        //        string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
        //        {
        //            total_paid_aomunt = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(discount) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                             WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
        //        string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
        //        {
        //            total_discount_aomunt = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(distinct(credits)) from from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                             WHERE(pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and(pos_sales_accounts.status != 'Installment');";
        //        string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
        //        {
        //            total_credits_aomunt = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as totalMarketPrice FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
        //                            INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
        //                            INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
        //                            pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
        //        GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (GetSetData.Data == "" || GetSetData.Data == "NULL")
        //        {
        //            GetSetData.Data = "0";
        //        }

        //        // ************************************************************
        //        GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
        //	INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_credits_return == "" || total_credits_return == "NULL")
        //        {
        //            total_credits_return = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(pos_sales_accounts.tax) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
        //        string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_discount_tax == "" || total_discount_tax == "NULL")
        //        {
        //            total_discount_tax = "0";
        //        }
        //        // *********************************************************************************

        //        GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as totalMarketPrice FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
        //	INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
        //                            pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
        //                            where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

        //        string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
        //        {
        //            total_marketPrice_return_items = "0";
        //        }
        //        //*****************************************************************************************
        //        GetSetData.query = @"select sum(pos_return_accounts.tax) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
        //        {
        //            total_discount_taxReturn = "0";
        //        }
        //        // *********************************************************************************

        //        GetSetData.query = @"select sum(paid) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_paid_return == "" || total_paid_return == "NULL")
        //        {
        //            total_paid_return = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(discount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_discount_Return == "" || total_discount_Return == "NULL")
        //        {
        //            total_discount_Return = "0";
        //        }

        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(credit_card_amount) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
        //        string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
        //        {
        //            total_creditCard_aomunt = "0";
        //        }

        //        // *************************************

        //        GetSetData.query = @"select sum(credit_card_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_creditCard_return == "" || total_creditCard_return == "NULL")
        //        {
        //            total_creditCard_return = "0";
        //        }

        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(paypal_amount) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                            WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
        //        string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
        //        {
        //            total_paypal_aomunt = "0";
        //        }

        //        // *************************************

        //        GetSetData.query = @"select sum(paypal_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_paypal_return == "" || total_paypal_return == "NULL")
        //        {
        //            total_paypal_return = "0";
        //        }
        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(google_pay_amount) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
        //                             WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
        //        string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
        //        {
        //            total_googlePay_aomunt = "0";
        //        }

        //        // *************************************

        //        GetSetData.query = @"select sum(google_pay_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id
        //	where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
        //        string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (total_googlePay_return == "" || total_googlePay_return == "NULL")
        //        {
        //            total_googlePay_return = "0";
        //        }


        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(Total_price) from pos_no_sale INNER JOIN pos_clock_in ON pos_no_sale.clock_in_id = pos_clock_in.id
        //                             WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
        //        string totalNoSaleAmount = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (totalNoSaleAmount == "" || totalNoSaleAmount == "NULL")
        //        {
        //            totalNoSaleAmount = "0";
        //        }


        //        // *******************************************************************************************

        //        GetSetData.query = @"select sum(pos_payout.amount) from pos_payout INNER JOIN pos_clock_in ON pos_payout.clock_in_id = pos_clock_in.id
        //                             WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
        //        string totalPayoutAmount = data.SearchStringValuesFromDb(GetSetData.query);

        //        if (totalPayoutAmount == "" || totalPayoutAmount == "NULL")
        //        {
        //            totalPayoutAmount = "0";
        //        }



        //        // *******************************************************************************************

        //        ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pCreditCardAmount);

        //        ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pCreditCardReturn);

        //        ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pGooglePayAmount);

        //        ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pGooglePayReturn);

        //        ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pPaypalAmount);

        //        ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pPaypalReturn);


        //        ReportParameter pNoSaleAmount = new ReportParameter("pNoSaleAmount", totalNoSaleAmount);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pNoSaleAmount);


        //        ReportParameter pPayoutAmount = new ReportParameter("pPayoutAmount", totalPayoutAmount);
        //        this.Viewer_dateWise.LocalReport.SetParameters(pPayoutAmount);

        //        // *******************************************************************************************

        //        ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_discount_Return1);

        //        ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_paid_return1);

        //        ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_discount_taxReturn1);

        //        ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_marketPrice_return_items1);

        //        ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_discount_tax1);

        //        ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_credits_return1);

        //        ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_marketPrice);
        //        // *********************************************************************************

        //        ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_paid_aomunt1);

        //        ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_discount_aomunt1);

        //        ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
        //        this.Viewer_dateWise.LocalReport.SetParameters(total_credits_aomunt1);

        //        ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
        //        this.Viewer_dateWise.LocalReport.SetParameters(return_amountsPur1);

        //        ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
        //        this.Viewer_dateWise.LocalReport.SetParameters(return_amount_sale1);

        //        ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
        //        this.Viewer_dateWise.LocalReport.SetParameters(return_perItem_discount1);


        //        ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
        //        this.Viewer_dateWise.LocalReport.SetParameters(fromDate);

        //        ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
        //        this.Viewer_dateWise.LocalReport.SetParameters(toDate);

        //        this.Viewer_dateWise.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //    }
        //}  

        private void date_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
                                    pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment') and (pos_category.title != 'MISCELLANEOUS') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["DataTable1"]);
                this.Viewer_dateWise.LocalReport.DataSources.Clear();
                this.Viewer_dateWise.SetDisplayMode(DisplayMode.PrintLayout);
                this.Viewer_dateWise.LocalReport.DataSources.Add(rds);
                this.Viewer_dateWise.LocalReport.Refresh();

                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
                                    INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.Viewer_dateWise.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************

                customer_sales_ds void_orders_report = new customer_sales_ds();
                ReportDataSource void_orders_rds = null;
                SqlDataAdapter void_orders_da = null;

                GetSetData.query = @"SELECT pos_products.prod_name, pos_void_orders.date, pos_void_orders.quantity, pos_void_orders.pkg, pos_void_orders.full_pkg, 
                                    pos_void_orders.discount, pos_void_orders.total_purchase, pos_void_orders.Total_price, pos_void_orders.total_marketPrice, 
                                    pos_void_orders.total_wholeSale, pos_void_orders.note, pos_customers.full_name as customerName, pos_employees.full_name AS employeeName
                                    FROM pos_void_orders INNER JOIN pos_products ON pos_void_orders.prod_id = pos_products.product_id INNER JOIN
                                    pos_customers ON pos_void_orders.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_void_orders.employee_id = pos_employees.employee_id
                                    WHERE (pos_void_orders.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_void_orders.date asc;";


                void_orders_da = new SqlDataAdapter(GetSetData.query, conn);
                void_orders_da.Fill(void_orders_report, void_orders_report.Tables["VoidOrders"].TableName);

                void_orders_rds = new ReportDataSource("voidOrders", void_orders_report.Tables["VoidOrders"]);
                this.Viewer_dateWise.LocalReport.DataSources.Add(void_orders_rds);
                //*******************************************************************************************


                customer_sales_ds misc_items_report = new customer_sales_ds();
                ReportDataSource misc_items_rds = null;
                SqlDataAdapter misc_items_da = null;

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
                                    pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment') and (pos_category.title = 'MISCELLANEOUS') ORDER BY pos_sales_accounts.date asc;";


                misc_items_da = new SqlDataAdapter(GetSetData.query, conn);
                misc_items_da.Fill(misc_items_report, misc_items_report.Tables["MiscItems"].TableName);

                misc_items_rds = new ReportDataSource("miscItems", misc_items_report.Tables["MiscItems"]);
                this.Viewer_dateWise.LocalReport.DataSources.Add(misc_items_rds);
                //*******************************************************************************************



                DisplayReportInReportViewer(this.Viewer_dateWise);
                // Retrive Report Settings from db *******************************************************************************************

                GetSetData.query = @"SELECT sum(amount_due) FROM pos_sales_accounts
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string total_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_amount_sale == "" || total_amount_sale == "NULL")
                {
                    total_amount_sale = "0";
                }
                // *******************************************************************************************
                
                GetSetData.query = @"SELECT sum(amount_due) FROM pos_return_accounts
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
                                    INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase * quantity) FROM pos_sales_accounts inner join pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string sale_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (sale_amount_purchase == "" || sale_amount_purchase == "NULL")
                {
                    sale_amount_purchase = "0";
                }
                
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase * quantity) FROM pos_return_accounts INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************
                
                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price * quantity) FROM pos_sales_accounts inner join pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string saleAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (saleAmount == "" || saleAmount == "NULL")
                {
                    saleAmount = "0";
                }
                
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price * quantity) FROM pos_return_accounts INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string returnsAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (returnsAmount == "" || returnsAmount == "NULL")
                {
                    returnsAmount = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(paid) from pos_sales_accounts
                                     WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(discount) from pos_sales_accounts
                                     WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(credits) from from pos_sales_accounts
                                     WHERE(pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and(pos_sales_accounts.status != 'Installment');";
                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as totalMarketPrice FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
                                    INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
                                    INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                // ************************************************************
                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(pos_sales_accounts.tax) from pos_sales_accounts
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as totalMarketPrice FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************
                GetSetData.query = @"select sum(pos_return_accounts.tax) from pos_return_accounts
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"select sum(paid) from pos_return_accounts
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(discount) from pos_return_accounts
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
               
                // *******************************************************************************************

                GetSetData.query = @"select sum(credit_card_amount) from pos_sales_accounts
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
                {
                    total_creditCard_aomunt = "0";
                }
                
                // *************************************
                
                GetSetData.query = @"select sum(credit_card_amount) from pos_return_accounts
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_return == "" || total_creditCard_return == "NULL")
                {
                    total_creditCard_return = "0";
                }
               
                // *******************************************************************************************

                GetSetData.query = @"select sum(paypal_amount) from pos_sales_accounts
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
                {
                    total_paypal_aomunt = "0";
                }

                // *************************************
                
                GetSetData.query = @"select sum(paypal_amount) from pos_return_accounts
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_return == "" || total_paypal_return == "NULL")
                {
                    total_paypal_return = "0";
                }
                // *******************************************************************************************
               
                GetSetData.query = @"select sum(google_pay_amount) from pos_sales_accounts
                                     WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
                {
                    total_googlePay_aomunt = "0";
                }

                // *************************************
                
                GetSetData.query = @"select sum(google_pay_amount) from pos_return_accounts 
									where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";
                string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_return == "" || total_googlePay_return == "NULL")
                {
                    total_googlePay_return = "0";
                }


                // *******************************************************************************************

                GetSetData.query = @"select sum(Total_price) from pos_no_sale
                                     WHERE (pos_no_sale.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                string totalNoSaleAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalNoSaleAmount == "" || totalNoSaleAmount == "NULL")
                {
                    totalNoSaleAmount = "0";
                }
                
                
                // *******************************************************************************************

                GetSetData.query = @"select sum(pos_payout.amount) from pos_payout
                                     WHERE (pos_payout.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "');";
                string totalPayoutAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalPayoutAmount == "" || totalPayoutAmount == "NULL")
                {
                    totalPayoutAmount = "0";
                }


                // *******************************************************************************************

                GetSetData.query = @"select sum(employeeTip) from pos_sales_accounts
                                     WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string totalTipsAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalTipsAmount == "" || totalTipsAmount == "NULL")
                {
                    totalTipsAmount = "0";
                }
                
                // *******************************************************************************************

                GetSetData.query = @"select sum(surcharges) from pos_sales_accounts
                                     WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string totalSurcharges = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalSurcharges == "" || totalSurcharges == "NULL")
                {
                    totalSurcharges = "0";
                }


                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(advance_amount) FROM pos_tickets
                                    where (pos_tickets.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string advance_payment = data.SearchStringValuesFromDb(GetSetData.query);

                if (advance_payment == "" || advance_payment == "NULL")
                {
                    advance_payment = "0";
                }
                
                // *******************************************************************************************


                ReportParameter pAdvancePayment = new ReportParameter("pAdvancePayment", advance_payment);
                this.Viewer_dateWise.LocalReport.SetParameters(pAdvancePayment);

                ReportParameter pTotalEmployeeTips = new ReportParameter("pTotalEmployeeTips", totalTipsAmount);
                this.Viewer_dateWise.LocalReport.SetParameters(pTotalEmployeeTips);

                ReportParameter pTotalSurCharges = new ReportParameter("pTotalSurCharges", totalSurcharges);
                this.Viewer_dateWise.LocalReport.SetParameters(pTotalSurCharges);
                
                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(pCreditCardAmount);
                
                ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
                this.Viewer_dateWise.LocalReport.SetParameters(pCreditCardReturn);
               
                ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(pGooglePayAmount);
                
                ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
                this.Viewer_dateWise.LocalReport.SetParameters(pGooglePayReturn);
               
                ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(pPaypalAmount);
                
                ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
                this.Viewer_dateWise.LocalReport.SetParameters(pPaypalReturn);
                

                ReportParameter pNoSaleAmount = new ReportParameter("pNoSaleAmount", totalNoSaleAmount);
                this.Viewer_dateWise.LocalReport.SetParameters(pNoSaleAmount);
               

                ReportParameter pPayoutAmount = new ReportParameter("pPayoutAmount", totalPayoutAmount);
                this.Viewer_dateWise.LocalReport.SetParameters(pPayoutAmount);
               
                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.Viewer_dateWise.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.Viewer_dateWise.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.Viewer_dateWise.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.Viewer_dateWise.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.Viewer_dateWise.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.Viewer_dateWise.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.Viewer_dateWise.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.Viewer_dateWise.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.Viewer_dateWise.LocalReport.SetParameters(return_amountsPur1);
                
                ReportParameter pReturnsAmount = new ReportParameter("pReturnsAmount", returnsAmount);
                this.Viewer_dateWise.LocalReport.SetParameters(pReturnsAmount);

                ReportParameter pSalesAmount = new ReportParameter("pSalesAmount", saleAmount);
                this.Viewer_dateWise.LocalReport.SetParameters(pSalesAmount);
                
                ReportParameter pTotalSalePurchase = new ReportParameter("pTotalSalePurchase", sale_amount_purchase);
                this.Viewer_dateWise.LocalReport.SetParameters(pTotalSalePurchase);

                ReportParameter pTotalSaleAmount = new ReportParameter("pTotalSaleAmount", total_amount_sale);
                this.Viewer_dateWise.LocalReport.SetParameters(pTotalSaleAmount);
                
                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.Viewer_dateWise.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.Viewer_dateWise.LocalReport.SetParameters(return_perItem_discount1);
                

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.Viewer_dateWise.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.Viewer_dateWise.LocalReport.SetParameters(toDate);

                this.Viewer_dateWise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void product_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["DataTable1"]);
                this.viewer_product.LocalReport.DataSources.Clear();
                this.viewer_product.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_product.LocalReport.DataSources.Add(rds);
                this.viewer_product.LocalReport.Refresh();

                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN 
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.viewer_product.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************

                DisplayReportInReportViewer(this.viewer_product);

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(paid) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(discount) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(distinct(credits)) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment');";
                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as totalMarketPrice from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                // ************************************************************
                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(pos_sales_accounts.tax) from pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as totalMarketPrice FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************
                GetSetData.query = @"select sum(pos_return_accounts.tax) from pos_return_accounts cross JOIN pos_products where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "')  and (pos_products.product_id = '" + product_id_db + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"select sum(paid) from pos_return_accounts cross JOIN pos_products where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "');";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = "select sum(discount) from pos_return_accounts cross JOIN pos_products where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_products.product_id = '" + product_id_db + "');";
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_product.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.viewer_product.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_product.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_product.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_product.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_product.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_product.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_product.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_product.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_product.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_product.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_product.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_product.LocalReport.SetParameters(return_perItem_discount1);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_product.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_product.LocalReport.SetParameters(toDate);

                this.viewer_product.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void bill_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, 
                                    pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, pos_sales_accounts.pCredits, pos_sales_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_sales_details.note
                                    FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.billNo = '" + txt_billNo.Text + "');";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables[0]);
                this.viewer_bill_wise.LocalReport.DataSources.Clear();
                this.viewer_bill_wise.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_bill_wise.LocalReport.DataSources.Add(rds);
                this.viewer_bill_wise.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_bill_wise);

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE pos_sales_accounts.billNo = '" + txt_billNo.Text + "';";

                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_bill_wise.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                this.viewer_bill_wise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void voidOrders()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_products.prod_name, pos_void_orders.date, pos_void_orders.quantity, pos_void_orders.pkg, pos_void_orders.full_pkg, 
                                    pos_void_orders.discount, pos_void_orders.total_purchase, pos_void_orders.Total_price, pos_void_orders.total_marketPrice, 
                                    pos_void_orders.total_wholeSale, pos_void_orders.note, pos_customers.full_name as customerName, pos_employees.full_name AS employeeName, pos_void_orders.time
                                    FROM pos_void_orders INNER JOIN pos_products ON pos_void_orders.prod_id = pos_products.product_id INNER JOIN
                                    pos_customers ON pos_void_orders.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_void_orders.employee_id = pos_employees.employee_id
                                    WHERE (pos_void_orders.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') ORDER BY pos_void_orders.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["VoidOrders"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["VoidOrders"]);
                this.viewer_void.LocalReport.DataSources.Clear();
                this.viewer_void.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_void.LocalReport.DataSources.Add(rds);
                this.viewer_void.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_void);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_void.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_void.LocalReport.SetParameters(toDate);

                this.viewer_void.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        private void noSaleItems()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_products.prod_name, pos_no_sale.date, pos_no_sale.quantity, pos_no_sale.pkg, pos_no_sale.full_pkg, 
                                    pos_no_sale.discount, pos_no_sale.total_purchase, pos_no_sale.Total_price, pos_no_sale.total_marketPrice, 
                                    pos_no_sale.total_wholeSale, pos_no_sale.note, pos_customers.full_name as customerName, pos_employees.full_name AS employeeName, pos_no_sale.time
                                    FROM pos_no_sale INNER JOIN pos_products ON pos_no_sale.prod_id = pos_products.product_id INNER JOIN
                                    pos_customers ON pos_no_sale.customer_id = pos_customers.customer_id INNER JOIN pos_employees ON pos_no_sale.employee_id = pos_employees.employee_id
                                    WHERE (pos_no_sale.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') ORDER BY pos_no_sale.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["noSaleItems"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["noSaleItems"]);
                this.viewer_noSale.LocalReport.DataSources.Clear();
                this.viewer_noSale.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_noSale.LocalReport.DataSources.Add(rds);
                this.viewer_noSale.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_noSale);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_noSale.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_noSale.LocalReport.SetParameters(toDate);

                this.viewer_noSale.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        private void payoutDetails()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"select pos_payout.date, pos_payout.time, pos_payout.amount, pos_payout.reason, pos_employees.full_name, 
                                    pos_employees.emp_code, pos_employees.mobile_no, pos_employees.address1 
                                    from pos_payout inner join pos_users on pos_payout.user_id = pos_users.user_id 
                                    inner join pos_employees on pos_employees.employee_id = pos_users.emp_id
                                    WHERE (pos_payout.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') order by pos_payout.id asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["payout"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["payout"]);
                this.viewer_payout.LocalReport.DataSources.Clear();
                this.viewer_payout.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_payout.LocalReport.DataSources.Add(rds);
                this.viewer_payout.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_payout);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_payout.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_payout.LocalReport.SetParameters(toDate);

                this.viewer_payout.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        
        private void miscItemsReport()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
                                    pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id 
                                    INNER JOIN pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "')  and (pos_sales_accounts.status != 'Installment') and (pos_category.title = 'MISCELLANEOUS') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["MiscItems"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["MiscItems"]);
                this.viewerMiscItems.LocalReport.DataSources.Clear();
                this.viewerMiscItems.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewerMiscItems.LocalReport.DataSources.Add(rds);
                this.viewerMiscItems.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewerMiscItems);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewerMiscItems.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewerMiscItems.LocalReport.SetParameters(toDate);

                this.viewerMiscItems.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables[0]);
                this.viewer_all_transaction.LocalReport.DataSources.Clear();
                this.viewer_all_transaction.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_all_transaction.LocalReport.DataSources.Add(rds);
                this.viewer_all_transaction.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_all_transaction);


                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.viewer_all_transaction.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(amount_due) FROM pos_sales_accounts
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "');";

                string total_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_amount_sale == "" || total_amount_sale == "NULL")
                {
                    total_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_purchase * quantity) FROM pos_sales_accounts inner join pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
                                    inner join pos_customers on pos_customers.customer_id = pos_sales_accounts.customer_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string sale_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (sale_amount_purchase == "" || sale_amount_purchase == "NULL")
                {
                    sale_amount_purchase = "0";
                }


                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.Total_price * quantity) FROM pos_sales_accounts inner join pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
                                    inner join pos_customers on pos_customers.customer_id = pos_sales_accounts.customer_id
                                    where (pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string saleAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (saleAmount == "" || saleAmount == "NULL")
                {
                    saleAmount = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price * quantity) FROM pos_return_accounts INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id
                                    inner join pos_customers on pos_customers.customer_id = pos_return_accounts.customer_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string returnsAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (returnsAmount == "" || returnsAmount == "NULL")
                {
                    returnsAmount = "0";
                }
                // *******************************************************************************************
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase * quantity) FROM pos_return_accounts inner join pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id
                                    inner join pos_customers on pos_customers.customer_id = pos_return_accounts.customer_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.paid) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_return_accounts.paid) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.credits)) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id
                                    WHERE (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string CustomerLastCredits = data.SearchStringValuesFromDb(GetSetData.query);

                if (CustomerLastCredits == "" || CustomerLastCredits == "NULL")
                {
                    CustomerLastCredits = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.tax)  FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.tax) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************
               
                GetSetData.query = @"select sum(credit_card_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
                {
                    total_creditCard_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_return == "" || total_creditCard_return == "NULL")
                {
                    total_creditCard_return = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
                {
                    total_paypal_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_return == "" || total_paypal_return == "NULL")
                {
                    total_paypal_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
                {
                    total_googlePay_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
									INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_customers.full_name = '" + customer_name_text.Text + "') AND (pos_customers.cus_code = '" + customer_code_text.Text + "');";
                string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_return == "" || total_googlePay_return == "NULL")
                {
                    total_googlePay_return = "0";
                }

                // *******************************************************************************************

                ReportParameter pTotalSaleAmount = new ReportParameter("pTotalSaleAmount", total_amount_sale);
                this.viewer_all_transaction.LocalReport.SetParameters(pTotalSaleAmount);

                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(pCreditCardAmount);

                ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
                this.viewer_all_transaction.LocalReport.SetParameters(pCreditCardReturn);

                ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(pGooglePayAmount);

                ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
                this.viewer_all_transaction.LocalReport.SetParameters(pGooglePayReturn);

                ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(pPaypalAmount);

                ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
                this.viewer_all_transaction.LocalReport.SetParameters(pPaypalReturn);

                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_all_transaction.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_all_transaction.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_all_transaction.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_all_transaction.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter pTotalSalePurchase = new ReportParameter("pTotalSalePurchase", sale_amount_purchase);
                this.viewer_all_transaction.LocalReport.SetParameters(pTotalSalePurchase);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_all_transaction .LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_all_transaction.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_all_transaction.LocalReport.SetParameters(return_perItem_discount1);

                ReportParameter pReturnsAmount = new ReportParameter("pReturnsAmount", returnsAmount);
                this.viewer_all_transaction.LocalReport.SetParameters(pReturnsAmount);

                ReportParameter pSalesAmount = new ReportParameter("pSalesAmount", saleAmount);
                this.viewer_all_transaction.LocalReport.SetParameters(pSalesAmount);

                this.viewer_all_transaction.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void saleman_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_clock_in.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables[0]);
                this.viewer_saleman.LocalReport.DataSources.Clear();
                this.viewer_saleman.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_saleman.LocalReport.DataSources.Add(rds);
                this.viewer_saleman.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_saleman);


                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, pos_returns_details.Total_price
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["SalesReturns"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["SalesReturns"]);
                this.viewer_saleman.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(Total_price) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);
                // *******************************************************************************************
                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************


                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.paid) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
					                INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.credits) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                    pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                // ************************************************************
                GetSetData.query = @"SELECT sum(pos_return_accounts.credits) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.tax)) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.tax)) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }

                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.paid) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.discount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
               
                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
                {
                    total_creditCard_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_return == "" || total_creditCard_return == "NULL")
                {
                    total_creditCard_return = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
                {
                    total_paypal_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_return == "" || total_paypal_return == "NULL")
                {
                    total_paypal_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
                {
                    total_googlePay_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "');";
                string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_return == "" || total_googlePay_return == "NULL")
                {
                    total_googlePay_return = "0";
                }
                // *******************************************************************************************

                // *******************************************************************************************

                GetSetData.query = @"select sum(employeeTip) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string totalTipsAmount = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalTipsAmount == "" || totalTipsAmount == "NULL")
                {
                    totalTipsAmount = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(surcharges) FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id 
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_employees.full_name = '" + txt_saleman_name.Text + "') AND (pos_employees.emp_code = '" + txt_saleman_code.Text + "') and (pos_sales_accounts.status != 'Installment'); ";
                string totalSurcharges = data.SearchStringValuesFromDb(GetSetData.query);

                if (totalSurcharges == "" || totalSurcharges == "NULL")
                {
                    totalSurcharges = "0";
                }



                // *******************************************************************************************

                ReportParameter pTotalEmployeeTips = new ReportParameter("pTotalEmployeeTips", totalTipsAmount);
                this.viewer_saleman.LocalReport.SetParameters(pTotalEmployeeTips);

                ReportParameter pTotalSurCharges = new ReportParameter("pTotalSurCharges", totalSurcharges);
                this.viewer_saleman.LocalReport.SetParameters(pTotalSurCharges);

                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(pCreditCardAmount);

                ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
                this.viewer_saleman.LocalReport.SetParameters(pCreditCardReturn);

                ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(pGooglePayAmount);

                ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
                this.viewer_saleman.LocalReport.SetParameters(pGooglePayReturn);

                ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(pPaypalAmount);

                ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
                this.viewer_saleman.LocalReport.SetParameters(pPaypalReturn);

                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_saleman.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.viewer_saleman.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_saleman.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_saleman.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_saleman.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_saleman.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_saleman.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_saleman.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_saleman.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_saleman.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_saleman.LocalReport.SetParameters(return_perItem_discount1);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_saleman.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_saleman.LocalReport.SetParameters(toDate);

                this.viewer_saleman.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Category_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables[0]);
                this.viewer_category.LocalReport.DataSources.Clear();
                this.viewer_category.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_category.LocalReport.DataSources.Add(rds);
                this.viewer_category.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_category);


                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_products.prod_state, pos_return_accounts.billNo, 
                                    pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, 
                                    pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, 
                                    pos_returns_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.total_purchase, pos_returns_details.note
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["CategoryAndBrandWise"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["CategoryAndBrandWise"]);
                this.viewer_category.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(pos_returns_details.Total_price) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);
                // *******************************************************************************************
                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************

                
                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.paid) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.credits) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) as 'T.Market Price' FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                // ************************************************************
                GetSetData.query = @"SELECT sum(pos_return_accounts.tax) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.tax) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.tax) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }

                // *********************************************************************************
                GetSetData.query = @"SELECT sum(pos_return_accounts.paid) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.discount) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";

                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
                {
                    total_creditCard_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_return == "" || total_creditCard_return == "NULL")
                {
                    total_creditCard_return = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(paypal_amount) from pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                    where(pos_sales_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and(pos_category.title = '" + customer_name_text.Text + "');";
                string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
                {
                    total_paypal_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_return == "" || total_paypal_return == "NULL")
                {
                    total_paypal_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_sales_accounts inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
									INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_category on pos_products.category_id = pos_category.category_id
                                     WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
                {
                    total_googlePay_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_category.title = '" + customer_name_text.Text + "');";
                string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_return == "" || total_googlePay_return == "NULL")
                {
                    total_googlePay_return = "0";
                }
                // *******************************************************************************************

                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
                this.viewer_category.LocalReport.SetParameters(pCreditCardAmount);

                ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
                this.viewer_category.LocalReport.SetParameters(pCreditCardReturn);

                ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
                this.viewer_category.LocalReport.SetParameters(pGooglePayAmount);

                ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
                this.viewer_category.LocalReport.SetParameters(pGooglePayReturn);

                ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
                this.viewer_category.LocalReport.SetParameters(pPaypalAmount);

                ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
                this.viewer_category.LocalReport.SetParameters(pPaypalReturn);

                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_category.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.viewer_category.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_category.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_category.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_category.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_category.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_category.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_category.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_category.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_category.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_category.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_category.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_category.LocalReport.SetParameters(return_perItem_discount1);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_category.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_category.LocalReport.SetParameters(toDate);

                this.viewer_category.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void brand_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();
                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
                                    FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable2"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["DataTable2"]);
                this.viewer_brand.LocalReport.DataSources.Clear();
                this.viewer_brand.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewer_brand.LocalReport.DataSources.Add(rds);
                this.viewer_brand.LocalReport.Refresh();
                DisplayReportInReportViewer(this.viewer_brand);

                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_products.prod_state, pos_return_accounts.billNo, 
                                    pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, 
                                    pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, 
                                    pos_returns_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.total_purchase, pos_returns_details.note
                                    FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
                                    pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id 
									INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    where (pos_clock_in.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["CategoryAndBrandWise"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["CategoryAndBrandWise"]);
                this.viewer_brand.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT sum(Total_price) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                string return_amount_sale = data.SearchStringValuesFromDb(GetSetData.query);
                // *******************************************************************************************
                if (return_amount_sale == "" || return_amount_sale == "NULL")
                {
                    return_amount_sale = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.discount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                string return_perItem_discount = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_perItem_discount == "" || return_perItem_discount == "NULL")
                {
                    return_perItem_discount = "0";
                }
                // *******************************************************************************************

                
                GetSetData.query = @"SELECT sum(pos_returns_details.total_purchase) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                string return_amount_purchase = data.SearchStringValuesFromDb(GetSetData.query);

                if (return_amount_purchase == "" || return_amount_purchase == "NULL")
                {
                    return_amount_purchase = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.paid) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_paid_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_aomunt == "" || total_paid_aomunt == "NULL")
                {
                    total_paid_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.discount) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_discount_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_aomunt == "" || total_discount_aomunt == "NULL")
                {
                    total_discount_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.credits) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";

                string total_credits_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_aomunt == "" || total_credits_aomunt == "NULL")
                {
                    total_credits_aomunt = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_details.total_marketPrice) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                {
                    GetSetData.Data = "0";
                }

                GetSetData.query = @"SELECT sum(pos_return_accounts.credits) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_credits_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_credits_return == "" || total_credits_return == "NULL")
                {
                    total_credits_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_sales_accounts.tax) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_discount_tax = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_tax == "" || total_discount_tax == "NULL")
                {
                    total_discount_tax = "0";
                }
                // *********************************************************************************

                GetSetData.query = @"SELECT sum(pos_returns_details.total_marketPrice) as 'T.Market Price' from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                string total_marketPrice_return_items = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_marketPrice_return_items == "" || total_marketPrice_return_items == "NULL")
                {
                    total_marketPrice_return_items = "0";
                }
                //*****************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.tax) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_discount_taxReturn = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_taxReturn == "" || total_discount_taxReturn == "NULL")
                {
                    total_discount_taxReturn = "0";
                }

                // *********************************************************************************

                // *********************************************************************************
                GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.paid)) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_paid_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paid_return == "" || total_paid_return == "NULL")
                {
                    total_paid_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"SELECT sum(pos_return_accounts.discount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";

                string total_discount_Return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_discount_Return == "" || total_discount_Return == "NULL")
                {
                    total_discount_Return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(credit_card_amount) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_creditCard_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_aomunt == "" || total_creditCard_aomunt == "NULL")
                {
                    total_creditCard_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(credit_card_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_creditCard_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_creditCard_return == "" || total_creditCard_return == "NULL")
                {
                    total_creditCard_return = "0";
                }

                // *******************************************************************************************

                GetSetData.query = @"select sum(paypal_amount) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_paypal_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_aomunt == "" || total_paypal_aomunt == "NULL")
                {
                    total_paypal_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(paypal_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_paypal_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_paypal_return == "" || total_paypal_return == "NULL")
                {
                    total_paypal_return = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(google_pay_amount) FROM pos_sales_accounts INNER JOIN pos_clock_in ON pos_sales_accounts.clock_in_id = pos_clock_in.id 
									inner join pos_sales_details on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
                                    inner join pos_products on pos_products.product_id = pos_sales_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "') and (pos_sales_accounts.status != 'Installment');";
                string total_googlePay_aomunt = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_aomunt == "" || total_googlePay_aomunt == "NULL")
                {
                    total_googlePay_aomunt = "0";
                }

                // *************************************

                GetSetData.query = @"select sum(google_pay_amount) from pos_return_accounts INNER JOIN pos_clock_in ON pos_return_accounts.clock_in_id = pos_clock_in.id  
									inner join pos_returns_details on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
                                    inner join pos_products on pos_products.product_id = pos_returns_details.prod_id inner JOIN pos_brand on pos_products.brand_id = pos_brand.brand_id
                                    WHERE (pos_clock_in.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_brand.brand_title = '" + customer_name_text.Text + "');";
                string total_googlePay_return = data.SearchStringValuesFromDb(GetSetData.query);

                if (total_googlePay_return == "" || total_googlePay_return == "NULL")
                {
                    total_googlePay_return = "0";
                }
                // *******************************************************************************************

          
                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", total_creditCard_aomunt);
                this.viewer_brand.LocalReport.SetParameters(pCreditCardAmount);

                ReportParameter pCreditCardReturn = new ReportParameter("pCreditCardReturn", total_creditCard_return);
                this.viewer_brand.LocalReport.SetParameters(pCreditCardReturn);

                ReportParameter pGooglePayAmount = new ReportParameter("pGooglePayAmount", total_googlePay_aomunt);
                this.viewer_brand.LocalReport.SetParameters(pGooglePayAmount);

                ReportParameter pGooglePayReturn = new ReportParameter("pGooglePayReturn", total_googlePay_return);
                this.viewer_brand.LocalReport.SetParameters(pGooglePayReturn);

                ReportParameter pPaypalAmount = new ReportParameter("pPaypalAmount", total_paypal_aomunt);
                this.viewer_brand.LocalReport.SetParameters(pPaypalAmount);

                ReportParameter pPaypalReturn = new ReportParameter("pPaypalReturn", total_paypal_return);
                this.viewer_brand.LocalReport.SetParameters(pPaypalReturn);

                // *******************************************************************************************

                ReportParameter total_discount_Return1 = new ReportParameter("pDiscountReturn", total_discount_Return);
                this.viewer_brand.LocalReport.SetParameters(total_discount_Return1);

                ReportParameter total_paid_return1 = new ReportParameter("pPaidReturn", total_paid_return);
                this.viewer_brand.LocalReport.SetParameters(total_paid_return1);

                ReportParameter total_discount_taxReturn1 = new ReportParameter("pDiscountTaxReturn", total_discount_taxReturn);
                this.viewer_brand.LocalReport.SetParameters(total_discount_taxReturn1);

                ReportParameter total_marketPrice_return_items1 = new ReportParameter("pMarketPriceReturnItems", total_marketPrice_return_items);
                this.viewer_brand.LocalReport.SetParameters(total_marketPrice_return_items1);

                ReportParameter total_discount_tax1 = new ReportParameter("pTotalDiscountTax", total_discount_tax);
                this.viewer_brand.LocalReport.SetParameters(total_discount_tax1);

                ReportParameter total_credits_return1 = new ReportParameter("pTotalCreditReturn", total_credits_return);
                this.viewer_brand.LocalReport.SetParameters(total_credits_return1);

                ReportParameter total_marketPrice = new ReportParameter("pMarketPrice", GetSetData.Data);
                this.viewer_brand.LocalReport.SetParameters(total_marketPrice);
                // *********************************************************************************

                ReportParameter total_paid_aomunt1 = new ReportParameter("pTotalPaidAmount", total_paid_aomunt);
                this.viewer_brand.LocalReport.SetParameters(total_paid_aomunt1);

                ReportParameter total_discount_aomunt1 = new ReportParameter("pTotalDiscount", total_discount_aomunt);
                this.viewer_brand.LocalReport.SetParameters(total_discount_aomunt1);

                ReportParameter total_credits_aomunt1 = new ReportParameter("pTotalCredits", total_credits_aomunt);
                this.viewer_brand.LocalReport.SetParameters(total_credits_aomunt1);

                ReportParameter return_amountsPur1 = new ReportParameter("pTotalReturnPur", return_amount_purchase);
                this.viewer_brand.LocalReport.SetParameters(return_amountsPur1);

                ReportParameter return_amount_sale1 = new ReportParameter("pTotalReturnSale", return_amount_sale);
                this.viewer_brand.LocalReport.SetParameters(return_amount_sale1);

                ReportParameter return_perItem_discount1 = new ReportParameter("pTotalReturnPerItemDiscount", return_perItem_discount);
                this.viewer_brand.LocalReport.SetParameters(return_perItem_discount1);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_brand.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_brand.LocalReport.SetParameters(toDate);

                this.viewer_brand.RefreshReport();


            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void salesSummary()
        {
            try
            {
                this.reportProcedureDateWiseSalesSummaryTableAdapter.Fill(this.customer_sales_ds.ReportProcedureDateWiseSalesSummary, FromDate.Text, ToDate.Text);
                this.reportProcedureDateWiseSalesReturnSummaryTableAdapter.Fill(this.customer_sales_ds.ReportProcedureDateWiseSalesReturnSummary, FromDate.Text, ToDate.Text);
              
                //Sales Amounts**************************************************************************

                double subTotal = 0;
                double discountAmount = 0;
                double amountDue = 0;
                double cashAmount = 0;
                double creditCardAmount = 0;
                double creditAmount = 0;
                double totalQuantity = 0;

                foreach (customer_sales_ds.ReportProcedureDateWiseSalesSummaryRow row in this.customer_sales_ds.ReportProcedureDateWiseSalesSummary.Rows)
                {
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.sub_total)) as total
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.sub_total)) as total
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    subTotal = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (subTotal >= 0)
                    {
                        row.subTotalDB = subTotal;
                    }
                    else
                    {
                        row.subTotalDB = 0;
                    }
                    
                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.discount)) as discount
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.discount)) as discount
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    discountAmount = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (discountAmount >= 0)
                    {
                        row.discountAmountDB = discountAmount;
                    }
                    else
                    {
                        row.discountAmountDB = 0;
                    }

                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.amount_due)) as amount_due
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.amount_due)) as amount_due
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    amountDue = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (amountDue >= 0)
                    {
                        row.amountDueDB = amountDue;
                    }
                    else
                    {
                        row.amountDueDB = 0;
                    }

                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.paid)) as paid
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "') and (check_sale_status = 'Cash');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.paid)) as paid
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "') and (check_sale_status = 'Cash');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    cashAmount = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (cashAmount >= 0)
                    {
                        row.cashDB = cashAmount;
                    }
                    else
                    {
                        row.cashDB = 0;
                    }

                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.paid)) as paid
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "') and (check_sale_status = 'Credit Card');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.paid)) as paid
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "') and (check_sale_status = 'Credit Card');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    creditCardAmount = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (creditCardAmount >= 0)
                    {
                        row.creditCardDB = creditCardAmount;
                    }
                    else
                    {
                        row.creditCardDB = 0;
                    }

                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(distinct(pos_sales_accounts.credits)) as credits
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(distinct(pos_return_accounts.credits)) as credits
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    creditAmount = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (creditAmount >= 0)
                    {
                        row.creditsDB = creditAmount;
                    }
                    else
                    {
                        row.creditsDB = 0;
                    }

                    //**************************************************************************
                    GetSetData.query = @"SELECT sum(pos_sales_details.quantity) as quantity
                                         FROM pos_sales_details Inner JOIN pos_sales_accounts ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
                                         pos_products  ON pos_sales_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id
                                         where (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_sales_accounts.status != 'Installment') and  (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    GetSetData.query = @"SELECT sum(pos_returns_details.quantity) as quantity
                                         FROM pos_returns_details Inner JOIN pos_return_accounts ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id JOIN
                                         pos_products  ON pos_returns_details.prod_id = pos_products.product_id  INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id 
                                         where (pos_return_accounts.date BETWEEN '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_category.title = '" + row.title.ToString() + "');";
                    GetSetData.Permission = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "" || GetSetData.Data == "NULL")
                    {
                        GetSetData.Data = "0";
                    }

                    if (GetSetData.Permission == "" || GetSetData.Permission == "NULL")
                    {
                        GetSetData.Permission = "0";
                    }

                    totalQuantity = double.Parse(GetSetData.Data) - double.Parse(GetSetData.Permission);

                    if (totalQuantity >= 0)
                    {
                        row.totalQuantity = totalQuantity;
                    }
                    else
                    {
                        row.totalQuantity = 0;
                    }
                }

                this.viewer_summary.SetDisplayMode(DisplayMode.PrintLayout);
                DisplayReportInReportViewer(this.viewer_summary);


                ReportParameter pSubTotal = new ReportParameter("pSubTotal", subTotal.ToString());
                this.viewer_summary.LocalReport.SetParameters(pSubTotal);

                ReportParameter pDiscountAmount = new ReportParameter("pDiscountAmount", discountAmount.ToString());
                this.viewer_summary.LocalReport.SetParameters(pDiscountAmount);


                ReportParameter pAmountDue = new ReportParameter("pAmountDue", amountDue.ToString());
                this.viewer_summary.LocalReport.SetParameters(pAmountDue);


                ReportParameter pCashAmount = new ReportParameter("pCashAmount", cashAmount.ToString());
                this.viewer_summary.LocalReport.SetParameters(pCashAmount);


                ReportParameter pCreditCardAmount = new ReportParameter("pCreditCardAmount", creditCardAmount.ToString());
                this.viewer_summary.LocalReport.SetParameters(pCreditCardAmount);


                ReportParameter pCreditAmount = new ReportParameter("pCreditAmount", creditAmount.ToString());
                this.viewer_summary.LocalReport.SetParameters(pCreditAmount);


                ReportParameter pTotalQuantity = new ReportParameter("pTotalQuantity", totalQuantity.ToString());
                this.viewer_summary.LocalReport.SetParameters(pTotalQuantity);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_summary.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_summary.LocalReport.SetParameters(toDate);

                this.viewer_summary.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void tax_wise_sales()
        {
            try
            {
                customer_sales_ds report = new customer_sales_ds();

                GetSetData.query = @"SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
                                    pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credit_card_amount, 
                                    pos_sales_accounts.paypal_amount, pos_sales_accounts.google_pay_amount, pos_sales_accounts.credits,  pos_sales_accounts.total_taxation, pos_sales_accounts.check_sale_status, 
                                    pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
                                    pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
                                    pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount, pos_sales_details.total_marketPrice as perItemTax
                                    FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
                                    pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
                                    pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
                                    pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_details.total_marketPrice > '0') ORDER BY pos_sales_accounts.date asc;";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["TaxWiseSales"].TableName);

                ReportDataSource rds = new ReportDataSource("loyalCusSales", report.Tables["TaxWiseSales"]);
                this.viewerTaxWise.LocalReport.DataSources.Clear();
                this.viewerTaxWise.SetDisplayMode(DisplayMode.PrintLayout);
                this.viewerTaxWise.LocalReport.DataSources.Add(rds);
                this.viewerTaxWise.LocalReport.Refresh();

                //Return Items List****************************************************************
                customer_sales_ds sales_return_report = new customer_sales_ds();
                ReportDataSource sales_return_rds = null;
                SqlDataAdapter sales_return_da = null;

                GetSetData.query = @"SELECT pos_customers.full_name, pos_employees.full_name AS Expr1, pos_return_accounts.billNo, pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, 
                                    pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, 
                                    pos_products.prod_name, pos_products.barcode, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.quantity, pos_returns_details.pkg, 
									pos_returns_details.full_pkg, pos_returns_details.Total_price, pos_returns_details.total_marketPrice as perItemTax
                                    FROM pos_customers INNER JOIN pos_return_accounts ON pos_customers.customer_id = pos_return_accounts.customer_id 
                                    INNER JOIN pos_employees ON pos_return_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id INNER JOIN
                                    pos_products ON pos_returns_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_returns_details.total_marketPrice > '0');";

                sales_return_da = new SqlDataAdapter(GetSetData.query, conn);
                sales_return_da.Fill(sales_return_report, sales_return_report.Tables["TaxWiseSalesReturns"].TableName);
                sales_return_rds = new ReportDataSource("sales_returns", sales_return_report.Tables["TaxWiseSalesReturns"]);
                this.viewerTaxWise.LocalReport.DataSources.Add(sales_return_rds);
                //*******************************************************************************************


                DisplayReportInReportViewer(this.viewerTaxWise);

                // *********************************************************************************

                GetSetData.query = @"select sum(pos_sales_details.total_marketPrice)  as perItemTax
                                    FROM pos_sales_accounts INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
                                    WHERE (pos_sales_accounts.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') and (pos_sales_details.total_marketPrice > '0');";
                
                string saleWiseTax = data.SearchStringValuesFromDb(GetSetData.query);

                if (saleWiseTax == "" || saleWiseTax == "NULL")
                {
                    saleWiseTax = "0";
                }
                // *******************************************************************************************

                GetSetData.query = @"select sum(pos_returns_details.total_marketPrice)  as perItemTax
                                    FROM pos_return_accounts INNER JOIN pos_returns_details ON pos_return_accounts.return_acc_id = pos_returns_details.return_acc_id
                                    where (pos_return_accounts.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_returns_details.total_marketPrice > '0');";
                
                string returnWiseTax = data.SearchStringValuesFromDb(GetSetData.query);

                if (returnWiseTax == "" || returnWiseTax == "NULL")
                {
                    returnWiseTax = "0";
                }
                // *******************************************************************************************

                ReportParameter pSalesWiseTax = new ReportParameter("pSalesWiseTax", saleWiseTax);
                this.viewerTaxWise.LocalReport.SetParameters(pSalesWiseTax);

                
                ReportParameter pReturnWiseTax = new ReportParameter("pReturnWiseTax", returnWiseTax);
                this.viewerTaxWise.LocalReport.SetParameters(pReturnWiseTax);


                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewerTaxWise.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewerTaxWise.LocalReport.SetParameters(toDate);

                this.viewerTaxWise.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 0)
                {
                    date_wise_sales();
                }
                else if (reportType == 1)
                {
                    bill_wise_sales();
                }
                else if (reportType == 2)
                {
                    Category_wise_sales();
                }
                else if (reportType == 3)
                {
                    brand_wise_sales();
                }
                else if (reportType == 4)
                {
                    customer_wise_sales();
                }
                else if (reportType == 5)
                {
                    saleman_wise_sales();
                }
                else if (reportType == 6)
                {
                    product_wise_sales();
                }
                else if (reportType == 7)
                {
                    voidOrders();
                }
                else if (reportType == 8)
                {
                    salesSummary();
                }
                else if (reportType == 9)
                {
                    noSaleItems();
                }
                else if (reportType == 10)
                {
                    payoutDetails();
                } 
                else if (reportType == 11)
                {
                    miscItemsReport();
                }
                else if (reportType == 12)
                {
                    tax_wise_sales();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_saleman_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxEmployeeCodes();
        }

        private void txt_saleman_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboBoxEmployeeName();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }

        private void customer_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (customer_name_text.Text.Length > 2)
                {
                    Customer_details.isDropDown = true;
                    Customer_details.role_id = role_id;
                    Customer_details.selected_customer = customer_name_text.Text;
                    button_controls.CustomerDetailsbuttons();
                    customer_name_text.Text = Customer_details.selected_customer;
                    customer_code_text.Text = Customer_details.selected_customerCode;
                }
                else
                {
                    error.errorMessage("Please enter minimum 3 characters.");
                    error.ShowDialog();
                }
            }
        }

        private void prod_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    using (choose_product product = new choose_product())
                    {
                        choose_product.role_id = role_id;
                        choose_product.selectedProductName = "";
                        choose_product.selectedProductBarcode = "";
                        choose_product.selectedProductID = "";
                        choose_product.providedValueType = "product name";
                        choose_product.providedValue = txt_product.Text;

                        product.ShowDialog();
                    }

                    product_id_db = choose_product.selectedProductID;
                    stock_id_db = choose_product.selectedStockID;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnMiscItems_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Misc. Items Sale Report";
                reportType = 11;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                this.pnlMiscItems.Dock = DockStyle.Fill;

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = true;
                pnlTaxation.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void btnTaxWise_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Tax Wise Sale Report";
                reportType = 12;

                customer_code_text.Text = null;
                customer_name_text.Text = null;

                this.pnlTaxation.Dock = DockStyle.Fill;

                TextData.from_Date = FromDate.Text;
                TextData.to_Date = ToDate.Text;

                pnl_bill_wise.Visible = false;
                pnl_all_transaction.Visible = false;
                pnl_date_wise.Visible = false;
                pnl_saleman.Visible = false;
                pnl_category.Visible = false;
                pnl_brand.Visible = false;
                pnl_product.Visible = false;
                pnl_summary.Visible = false;
                pnl_void.Visible = false;
                pnlNoSale.Visible = false;
                pnlPayout.Visible = false;
                pnlMiscItems.Visible = false;
                pnlTaxation.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lbl_cus_name.Visible = false;
                lbl_cus_code.Visible = false;
                lbl_billNo.Visible = false;
                lbl_saleman_name.Visible = false;
                lbl_saleman_code.Visible = false;

                txt_saleman_name.Visible = false;
                txt_saleman_code.Visible = false;
                customer_name_text.Visible = false;
                customer_code_text.Visible = false;
                txt_product.Visible = false;
                txt_billNo.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void txt_saleman_name_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txt_saleman_name, "fillComboBoxEmployeeNames", "full_name");
        }

        private void txt_saleman_code_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txt_saleman_code, "fillComboBoxEmployeeNames", "emp_code");
        }

        private void form_loyal_cus_sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txt_product_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
