using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using System.Data;

namespace Message_box_info.forms.Clock_In_out.Reports.OverallReports
{
    public partial class overallDurationReport : Form
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

        public overallDurationReport()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static string fromDate = "";
        public static string toDate = "";


        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                // Use DataTable instead of duration_ds
                DataTable report = new DataTable();

                GetSetData.query = @"SELECT dbo.pos_clock_out.id, dbo.pos_clock_out.date, dbo.pos_clock_in.start_time, dbo.pos_clock_out.end_time, dbo.pos_clock_out.total_hours, dbo.pos_employees.full_name, 
                            dbo.pos_clock_out.opening_cash, dbo.pos_clock_out.total_sales, dbo.pos_clock_out.total_return_amount, dbo.pos_clock_out.total_void_orders, 
                            dbo.pos_clock_out.expected_amount, dbo.pos_clock_out.cash_amount_received, dbo.pos_clock_out.balance, dbo.pos_clock_out.remarks
                            FROM dbo.pos_clock_in INNER JOIN
                            dbo.pos_clock_out ON dbo.pos_clock_in.id = dbo.pos_clock_out.clock_in_id INNER JOIN
                            dbo.pos_users AS from_user ON dbo.pos_clock_in.from_user_id = from_user.user_id INNER JOIN
                            dbo.pos_users AS to_user ON dbo.pos_clock_in.to_user_id = to_user.user_id INNER JOIN
                            dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
                            WHERE (pos_clock_out.date between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "') and (pos_employees.full_name = '" + txtEmployeeName.Text + "')";

                using (SqlConnection conn = new SqlConnection(webConfig.con_string))
                {
                    SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                    da.Fill(report); // fill the DataTable
                }

                // Bind to ReportViewer
                ReportDataSource rds = new ReportDataSource("loyalCusSales", report); // "DataTable1" must match the RDLC dataset name
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.SetDisplayMode(DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;

                // Load logo and report settings
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");

                if (!string.IsNullOrEmpty(GetSetData.query) && GetSetData.query != "nill")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    viewer.LocalReport.SetParameters(logo);
                }
                else
                {
                    viewer.LocalReport.SetParameters(new ReportParameter("pLogo", ""));
                }

                // Other report parameters
                viewer.LocalReport.SetParameters(new ReportParameter("pTitle", data.UserPermissions("title", "pos_report_settings")));
                viewer.LocalReport.SetParameters(new ReportParameter("pAddress", data.UserPermissions("address", "pos_report_settings")));
                viewer.LocalReport.SetParameters(new ReportParameter("pPhone", data.UserPermissions("phone_no", "pos_report_settings")));
                viewer.LocalReport.SetParameters(new ReportParameter("pCopyrights", data.UserPermissions("copyrights", "pos_report_settings")));
                viewer.LocalReport.SetParameters(new ReportParameter("pTotalDuration", GetSetData.ProcedureGetEmployeeDuration(txtFromDate.Text, txtToDate.Text, txtEmployeeName.Text)));

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }


        private void overallDurationReport_Load(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Text = fromDate;
                txtToDate.Text = toDate;

                txtEmployeeName.Text = null;
                txtEmployeeName.Items.Clear();

                GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");

            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }


        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayReportInReportViewer(this.reportViewer1);
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void overallDurationReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Space)
            {
                reportViewer1.PrintDialog();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                reportViewer1.PrintDialog();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}
