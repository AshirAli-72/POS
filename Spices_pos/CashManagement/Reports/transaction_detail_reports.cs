using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.BankingInfo.Reports;
using Spices_pos.DatabaseInfo.WebConfig;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Spices_pos.CashManagement.Reports
{
    public partial class transaction_detail_reports : Form
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

        public transaction_detail_reports()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0;


        private void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            // ✅ Use Value property, not Text, and apply custom format
            FromDate.Format = DateTimePickerFormat.Custom;
            ToDate.Format = DateTimePickerFormat.Custom;

            // ✅ Custom format to show "5/October/2025"
            FromDate.CustomFormat = "d/MMMM/yyyy";
            ToDate.CustomFormat = "d/MMMM/yyyy";

            // ✅ Set both to current date
            FromDate.Value = DateTime.Now.Date;
            ToDate.Value = DateTime.Now.Date;

            FromDate.Visible = true;
            ToDate.Visible = true;
            txt_title.Visible = false;

            lbl_from_date.Visible = true;
            lbl_to_date.Visible = true;
            lbl_title.Visible = false;

            pnl_person_wise.Visible = false;
            pnl_status_wise.Visible = false;
            pnl_date_wise.Visible = true;
            pnl_payment_wise.Visible = false;

            this.pnl_date_wise.Dock = DockStyle.Fill;

            this.viewer_status.Clear();
            this.viewer_date.Clear();
            this.viewer_payment.Clear();
            this.viewer_person.Clear();
        }

        private void transaction_detail_reports_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                this.viewer_status.RefreshReport();
                this.viewer_person.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        // ================= PERSON REPORT ===================
        // ================= DISPLAY REPORT (Supports Person, Status, Payment, Date) ===================
        private void DisplayReport(ReportViewer viewer, string mode)
        {
            try
            {
                // ✅ Date validation
                if (mode == "Date" && FromDate.Value.Date > ToDate.Value.Date)
                {
                    error.errorMessage("From Date cannot be greater than To Date.");
                    error.ShowDialog();
                    return;
                }

                // ✅ Base query (same for all modes)
                string query = @"
SELECT 
    p.id,
    p.name,
    p.mobile_number,
    p.cnic_number,
    p.address,
    p.status,
    COALESCE(cm.amount, 0) AS amount,
    ISNULL(pt.payment_title, 'Cash') AS payment_type,
    ISNULL(ps.status_title, 'Pending') AS status_title,
    CONVERT(varchar(20), cm.date, 103) AS transaction_date
FROM dbo.pos_persons AS p
LEFT JOIN dbo.pos_cash_management AS cm ON cm.person_id = p.id
LEFT JOIN dbo.pos_payment_type AS pt ON cm.payment_id = pt.id
LEFT JOIN dbo.pos_payment_status AS ps ON cm.status_id = ps.id
WHERE 1=1"; // ✅ Start with universal true condition

                // ✅ Build condition dynamically
                if (mode == "Date")
                {
                    query += " AND CONVERT(date, cm.date) BETWEEN @FromDate AND @ToDate";
                }
                else if (!string.IsNullOrEmpty(txt_title.Text))
                {
                    // ✅ User selected something — filter by mode
                    if (mode == "Person")
                        query += " AND p.name = @Value";
                    else if (mode == "Status")
                        query += " AND ps.status_title = @Value";
                    else if (mode == "Payment")
                        query += " AND pt.payment_title = @Value";
                }
                else
                {
                    // ✅ No selection — show all by date
                    query += " AND CONVERT(date, cm.date) BETWEEN @FromDate AND @ToDate";
                }

                query += " ORDER BY cm.date ASC;";

                // ✅ Set parameters
                SqlParameter[] parameters = {
            new SqlParameter("@Value", string.IsNullOrEmpty(txt_title.Text) ? (object)DBNull.Value : txt_title.Text.Trim()),
            new SqlParameter("@FromDate", FromDate.Value.Date),
            new SqlParameter("@ToDate", ToDate.Value.Date)
        };

                // ✅ Execute query
                DataTable dt = data.SelectData(query, parameters);
                if (dt == null) dt = new DataTable();

                // ✅ Bind dataset
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(new ReportDataSource("persondataset", dt));

                // ✅ Load correct report file
                switch (mode)
                {
                    case "Status":
                        viewer.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.status_wise_report.rdlc";
                        break;
                    case "Payment":
                        viewer.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.payment_wise_report.rdlc";
                        break;
                    case "Date":
                        viewer.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.date_wise_report.rdlc";
                        break;
                    default:
                        viewer.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.person_wise_report.rdlc";
                        break;
                }

                // ✅ Logo & header
                viewer.LocalReport.EnableExternalImages = true;
                string picturePath = data.UserPermissions("picture_path", "pos_general_settings");
                string logoFile = data.UserPermissions("logo_path", "pos_configurations");

                string fullLogoPath = (!string.IsNullOrEmpty(picturePath) && !string.IsNullOrEmpty(logoFile))
                    ? Path.Combine(Application.StartupPath, picturePath, logoFile)
                    : "";

                viewer.LocalReport.SetParameters(new ReportParameter("pLogo",
                    string.IsNullOrEmpty(fullLogoPath) ? "" : new Uri(fullLogoPath).AbsoluteUri));

                viewer.LocalReport.SetParameters(new[]
                {
            new ReportParameter("pTitle", data.UserPermissions("title", "pos_report_settings")),
            new ReportParameter("pAddress", data.UserPermissions("address", "pos_report_settings")),
            new ReportParameter("pPhone", data.UserPermissions("phone_no", "pos_report_settings")),
            new ReportParameter("pCopyrights", data.UserPermissions("copyrights", "pos_report_settings")),
            new ReportParameter("pFromDate", FromDate.Text),
            new ReportParameter("pToDate", ToDate.Text)
        });

                viewer.SetDisplayMode(DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.RefreshReport();
            }
            catch (Exception ex)
            {
                error.errorMessage("Error generating report: " + ex.Message);
                error.ShowDialog();
            }
        }







        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportType == 1) // Person-wise
                {
                    DisplayReport(this.viewer_person, "Person");
                }
                else if (reportType == 2) // Status-wise
                {
                    DisplayReport(this.viewer_status, "Status");
                }
                else if (reportType == 3) // Payment-wise
                {
                    DisplayReport(this.viewer_payment, "Payment");
                }
                else if (reportType == 4) // Date-wise
                {
                    DisplayReport(this.viewer_date, "Date");
                }
                else
                {
                    error.errorMessage("Please select a valid report type.");
                    error.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }


        private void btn_person_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Person Wise Report";
                reportType = 1;

                pnl_person_wise.Visible = true;
                pnl_person_wise.Dock = DockStyle.Fill;

                pnl_status_wise.Visible = false;
                pnl_payment_wise.Visible = false;
                pnl_date_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Name:";
                txt_title.Text = null;
                txt_title.Items.Clear();

                // ✅ Use DISTINCT to avoid duplicate names
                GetSetData.FillComboBoxWithValues("SELECT DISTINCT name FROM pos_persons;", "name", txt_title);
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }


     

        private void btn_payment_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "payment Wise Report";
                reportType = 3;



                // 🔹 Sirf transaction wala show karo
                pnl_payment_wise.Visible = true;
                pnl_payment_wise.Dock = DockStyle.Fill;

                pnl_person_wise.Visible = false;
                pnl_status_wise.Visible = false;
                pnl_date_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Status:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("SELECT payment_title FROM pos_payment_type;", "payment_title", txt_title);




            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }

        private void btn_date_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 4;

                pnl_date_wise.Visible = true;
                pnl_date_wise.Dock = DockStyle.Fill;

                pnl_person_wise.Visible = false;
                pnl_status_wise.Visible = false;
                pnl_payment_wise.Visible = false;

                // Hide title since date report uses date pickers
                txt_title.Visible = false;
                lbl_title.Visible = false;

                // Reset both to today's date
                FromDate.Value = DateTime.Now.Date;
                ToDate.Value = DateTime.Now.Date;

            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }

        private void btn_status_Click(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Status Wise Report";
                reportType = 2;



                // 🔹 Sirf transaction wala show karo
                pnl_status_wise.Visible = true;
                pnl_status_wise.Dock = DockStyle.Fill;

                pnl_person_wise.Visible = false;
                pnl_payment_wise.Visible = false;
                pnl_date_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Status:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("SELECT status_title FROM pos_payment_status;", "status_title", txt_title);




            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }

        // ================= OVERALL REPORT ===================

    }
}
