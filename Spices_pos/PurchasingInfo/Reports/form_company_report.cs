using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.PurchasingInfo.Reports;

namespace Purchase_info.Reports
{
    public partial class form_company_report : Form
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

        public form_company_report()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();


        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Suppliers_list()
        {
            Company_list_db report = new Company_list_db();
            GetSetData.query = @"SELECT pos_city.title, pos_country.title AS Expr1, pos_suppliers.code, pos_suppliers.full_name, pos_suppliers.date, pos_suppliers.telephone_no, pos_suppliers.mobile_no, pos_suppliers.contact_person, pos_suppliers.address, 
                                        pos_suppliers.email, pos_suppliers.website, pos_suppliers.bank_name, pos_suppliers.bank_account, pos_suppliers.image_path, pos_suppliers.remarks, pos_suppliers.status, pos_supplier_payables.due_days, pos_supplier_payables.previous_payables
                                        FROM pos_city INNER JOIN pos_suppliers ON pos_city.city_id = pos_suppliers.city_id INNER JOIN
                                        pos_country ON pos_suppliers.country_id = pos_country.country_id INNER JOIN pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id;";

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
            Company_list_db report_setting = new Company_list_db();
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


        private void form_company_report_Load(object sender, EventArgs e)
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
    }
}
