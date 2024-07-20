using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Receivables
{
    public partial class form_receivables : Form
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
        public form_receivables()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();


    
        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void Customer_receivables()
        {
            try
            {
                receivables_ds Customer_sales_report = new receivables_ds();
                ReportDataSource Customer_Sales_rds = null;
                SqlDataAdapter Customer_Sales_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_customers.full_name, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.telephone_no, 
                                    pos_customers.address1, pos_customers.email, pos_customers.bank_name, pos_customers.account_no, pos_customers.credit_limit, pos_customers.opening_balance
                                    FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id and pos_customer_lastCredits.lastCredits > 0
                                    where pos_customer_lastCredits.due_days between '" + FromDate.Text + "' AND '" + ToDate.Text + "' order by pos_customer_lastCredits.due_days asc;";


                // Script for Customer Sales ********************************
                Customer_Sales_da = new SqlDataAdapter(GetSetData.query, conn);
                Customer_Sales_da.Fill(Customer_sales_report, Customer_sales_report.Tables["Customers_Sales"].TableName);

                Customer_Sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_sales", Customer_sales_report.Tables["Customers_Sales"]);
                this.viewer_receivables.LocalReport.DataSources.Clear();
                this.viewer_receivables.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_receivables.ZoomMode = ZoomMode.Percent;
                this.viewer_receivables.ZoomPercent = 100;
                this.viewer_receivables.LocalReport.DataSources.Add(Customer_Sales_rds);
                this.viewer_receivables.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.viewer_receivables.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.viewer_receivables.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                this.viewer_receivables.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                this.viewer_receivables.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                this.viewer_receivables.LocalReport.SetParameters(phone);

                //GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                //ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                //this.viewer_receivables.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                this.viewer_receivables.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_receivables.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_receivables.LocalReport.SetParameters(toDate);

                this.viewer_receivables.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_receivables_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                Customer_receivables();
                this.viewer_receivables.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            Customer_receivables();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }

    }
}
