using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.SettingsInfo.Reports;

namespace Settings_info.Reports
{
    public partial class form_login_details_report : Form
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

        public form_login_details_report()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void login_details()
        {
            try
            {
                login_details_ds Customer_sales_report = new login_details_ds();
                ReportDataSource Customer_Sales_rds = new ReportDataSource();
                SqlDataAdapter Customer_Sales_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_login_details.mac_address, pos_login_details.date, pos_login_details.time, pos_employees.full_name as name,
                                    pos_roles.roleTitle, pos_users.password, pos_users.username, pos_login_details.status
                                    FROM dbo.pos_login_details INNER JOIN dbo.pos_users ON dbo.pos_login_details.user_id = dbo.pos_users.user_id 
                                    Inner JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = dbo.pos_users.emp_id
                                    Inner JOIN dbo.pos_roles ON dbo.pos_roles.role_id = dbo.pos_users.role_id
                                    where (pos_login_details.date between '" + TextData.fromdate + "' AND '" + TextData.todate + "') and (pos_login_details.user_id = '" + TextData.role_title + "') order by pos_login_details.date asc;";


                // Script for Customer Sales ********************************
                Customer_Sales_da = new SqlDataAdapter(GetSetData.query, conn);
                Customer_Sales_da.Fill(Customer_sales_report, Customer_sales_report.Tables["LoginDetails"].TableName);

                Customer_Sales_rds = new Microsoft.Reporting.WinForms.ReportDataSource("customer_sales", Customer_sales_report.Tables["LoginDetails"]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.LocalReport.DataSources.Add(Customer_Sales_rds);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.reportViewer1.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.reportViewer1.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(phone);

                //GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                //ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                //this.reportViewer1.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                this.reportViewer1.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                ReportParameter fromDate = new ReportParameter("pFromDate", TextData.fromdate);
                this.reportViewer1.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", TextData.todate);
                this.reportViewer1.LocalReport.SetParameters(toDate);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_login_details_report_Load(object sender, EventArgs e)
        {
            try
            {
                login_details();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
