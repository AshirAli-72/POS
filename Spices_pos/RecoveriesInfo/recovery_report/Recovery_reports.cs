using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.RecoveriesInfo.recovery_report;

namespace Recoverier_info.recovery_report
{
    public partial class Recovery_reports : Form
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

        public Recovery_reports()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void DisplayReportInReportViewer(ReportViewer viewer, string reportTitle)
        {
            try
            {
                recovery_ds report = new recovery_ds();
                GetSetData.query = @"SELECT pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_customers.full_name, pos_customers.cus_code, pos_employees.full_name AS Expr1, pos_employees.emp_code, pos_recovery_details.date, 
                                    pos_recovery_details.time, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recoveries.amount, pos_recoveries.credits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, pos_customers.image_path, pos_customers.fatherName
                                    FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN
                                    pos_employees ON pos_recovery_details.employee_id = pos_employees.employee_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
                                    WHERE (pos_recovery_details.date = '" + TextData.dates.ToString() + "') AND (pos_recovery_details.time = '" + TextData.times.ToString() + "')  AND (pos_customers.full_name = '" + TextData.cus_name.ToString() + "') AND (pos_customers.cus_code = '" + TextData.cus_code.ToString() + "');";


                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["Recovery"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource(reportTitle, report.Tables["Recovery"]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                //viewer.ZoomMode = ZoomMode.Percent;
                //viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;


                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                viewer.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                viewer.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                viewer.LocalReport.SetParameters(phone);

                GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                viewer.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                viewer.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                ReportParameter Credits = new ReportParameter("pCredits", TextData.lastCredits.ToString());
                viewer.LocalReport.SetParameters(Credits);

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                // *******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    viewer.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    viewer.LocalReport.SetParameters(logo);
                }

                viewer.RefreshReport();
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

        private void Recovery_reports_Load(object sender, EventArgs e)
        {
            try
            {
                //TextData.description = data.UserPermissions("page_size", "pos_general_settings");

                //if (TextData.description == "A6")
                //{
                    reportViewer2.Visible = true;
                    reportViewer1.Visible = false;
                    reportViewer2.Dock = DockStyle.Fill;
                    DisplayReportInReportViewer(this.reportViewer2, "recovery");
                //}
                //else
                //{
                //    reportViewer1.Visible = true;
                //    reportViewer2.Visible = false;
                //    reportViewer1.Dock = DockStyle.Fill;
                //    DisplayReportInReportViewer(this.reportViewer1, "recovery_receipt");
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

    }
}
