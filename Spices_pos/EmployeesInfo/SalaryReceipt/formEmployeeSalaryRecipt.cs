using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.EmployeesInfo.SalaryReceipt;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;

namespace Supplier_Chain_info.SalaryReceipt
{
    public partial class formEmployeeSalaryRecipt : Form
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
        public formEmployeeSalaryRecipt()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void DisplayReportInReportViewer()
        {
            try
            {
                employeeSalaryReceipt_ds report = new employeeSalaryReceipt_ds();

                GetSetData.query = @"SELECT dbo.pos_salariesPaybook.date, dbo.pos_salariesPaybook.time, dbo.pos_salariesPaybook.paymentDate, dbo.pos_salariesPaybook.amount, dbo.pos_salariesPaybook.credits, dbo.pos_salariesPaybook.balance, 
                                    dbo.pos_salariesPaybook.reference, dbo.pos_salariesPaybook.remarks, dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_employees.cnic, dbo.pos_employees.mobile_no, 
                                    dbo.pos_employees.address1, dbo.pos_employees.email, dbo.pos_salariesPaybook.salary, dbo.pos_salariesPaybook.commission, dbo.pos_salariesPaybook.hourly_wages, dbo.pos_salariesPaybook.form_date, dbo.pos_salariesPaybook.to_date, dbo.pos_salariesPaybook.commission_payment, dbo.pos_salariesPaybook.total_duration,
                                    dbo.pos_salariesPaybook.previous_paid_salary, dbo.pos_salariesPaybook.previous_paid_commission, dbo.pos_salariesPaybook.commission_balance 
                                    FROM dbo.pos_salariesPaybook INNER JOIN
                                    dbo.pos_employees ON dbo.pos_salariesPaybook.employee_id = dbo.pos_employees.employee_id
                                    where (dbo.pos_salariesPaybook.date = '" + TextData.dates + "') and (dbo.pos_salariesPaybook.time = '" + TextData.time + "');";

                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables["DataTable1"].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("recovery", report.Tables["DataTable1"]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.reportViewer1.LocalReport.DataSources.Add(rds);
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

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.query = @"SELECT title FROM pos_report_settings";
                TextData.full_name = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter title = new ReportParameter("pTitle", TextData.full_name);
                this.reportViewer1.LocalReport.SetParameters(title);

                GetSetData.query = @"SELECT address FROM pos_report_settings";
                string address = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter pAddress = new ReportParameter("pAddress", address);
                this.reportViewer1.LocalReport.SetParameters(pAddress);

                GetSetData.query = @"SELECT phone_no FROM pos_report_settings";
                TextData.phone1 = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter phone = new ReportParameter("pPhone", TextData.phone1);
                this.reportViewer1.LocalReport.SetParameters(phone);

                GetSetData.query = @"SELECT copyrights FROM pos_report_settings";
                TextData.remarks = data.SearchStringValuesFromDb(GetSetData.query);

                ReportParameter copyrights = new ReportParameter("pCopyrights", TextData.remarks);
                this.reportViewer1.LocalReport.SetParameters(copyrights);


                this.reportViewer1.RefreshReport();
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        //private void DisplayReportInReportViewer(ReportViewer viewer)
        //{
        //    try
        //    {
        //        this.ReportProcedureSalaryReceiptTableAdapter.Fill(this.employeeSalaryReceipt_ds.ReportProcedureSalaryReceipt, TextData.dates, TextData.time);
        //        this.ReportProcedureReportsTitlesTableAdapter.Fill(this.employeeSalaryReceipt_ds.ReportProcedureReportsTitles);
        //        viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
        //        //viewer.ZoomMode = ZoomMode.Percent;
        //        //viewer.ZoomPercent = 100;
        //        viewer.LocalReport.EnableExternalImages = true;

        //        //*******************************************************************************************
        //        GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
        //        GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
        //        //*******************************************************************************************

        //        if (GetSetData.query != "nill" && GetSetData.query != "")
        //        {
        //            GetSetData.query = GetSetData.Data + GetSetData.query;
        //            ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
        //            viewer.LocalReport.SetParameters(logo);
        //        }
        //        else
        //        {

        //            ReportParameter logo = new ReportParameter("pLogo", "");
        //            viewer.LocalReport.SetParameters(logo);
        //        }

        //        viewer.RefreshReport();
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }
        //}


        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formEmployeeSalaryRecipt_Load(object sender, EventArgs e)
        {
            DisplayReportInReportViewer();
        }
    }
}
