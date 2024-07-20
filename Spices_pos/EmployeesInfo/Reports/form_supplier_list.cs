using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.EmployeesInfo.Reports;

namespace Supplier_Chain_info.Reports
{
    public partial class form_supplier_list : Form
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

        public form_supplier_list()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void Suppliers_list()
        {
            Supplier_list_ds report = new Supplier_list_ds();
            GetSetData.query = @"SELECT pos_employees.date, pos_employees.full_name, pos_employees.emp_code, pos_employees.post_code, pos_employees.zip_code, pos_employees.cnic, pos_employees.house_no, pos_employees.telephone_no, 
                                        pos_employees.mobile_no, pos_employees.address1, pos_employees.address2, pos_employees.email, pos_employees.image_path, pos_employees.salary, pos_employees.daily_wages, pos_employees.commission, 
                                        pos_employees.status, pos_country.title, pos_city.title AS Expr1
                                        FROM pos_city INNER JOIN pos_employees ON pos_city.city_id = pos_employees.city_id INNER JOIN pos_country ON pos_employees.country_id = pos_country.country_id;";

            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
            da.Fill(report, report.Tables[0].TableName);

            ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", report.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.LocalReport.DataSources.Add(rds);

            // **********************************************************************************************************
            Supplier_list_ds report_setting = new Supplier_list_ds();
            GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings";

            SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
            report_da.Fill(report_setting, report_setting.Tables[0].TableName);

            ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Add(report_rds);
            this.reportViewer1.LocalReport.Refresh();

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

            this.reportViewer1.RefreshReport();
        }

        private void form_supplier_list_Load(object sender, EventArgs e)
        {
            try
            {
                Suppliers_list();
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
