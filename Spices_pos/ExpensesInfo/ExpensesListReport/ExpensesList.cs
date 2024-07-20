using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ExpensesInfo.ExpensesListReport;

namespace Expenses_info.ExpensesListReport
{
    public partial class ExpensesList : Form
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

        public ExpensesList()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        public void refresh()
        {
            lblReportTitle.Text = "Date Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            txt_title.Visible = false;
            lbl_title.Visible = false;

            viewer_date_wise.Visible = true;
            viewer_name_wise.Visible = false;
            viewer_over_all.Visible = false;

     
            this.viewer_date_wise.Dock = DockStyle.Fill;
         

            FromDate.Visible = true;
            ToDate.Visible = true;
            lbl_from_date.Visible = true;
            lbl_to_date.Visible = true;
            
            this.viewer_date_wise.Clear();
            this.viewer_name_wise.Clear();
            this.viewer_over_all.Clear();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExpensesList_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //DisplayReportInReportViewer(this.viewer_date_wise, "DateWise");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void date_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Date Wise Report";
                reportType = 0;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                
                viewer_date_wise.Visible = true;
                viewer_name_wise.Visible = false;
                viewer_over_all.Visible = false;

                this.viewer_date_wise.Dock = DockStyle.Fill;

                lbl_title.Visible = false;
                txt_title.Visible = false;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void title_wise_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Expense Title Wise Report";
                reportType = 1;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                viewer_date_wise.Visible = false;
                viewer_name_wise.Visible = true;
                viewer_over_all.Visible = false;

                this.viewer_name_wise.Dock = DockStyle.Fill;
                

                lbl_title.Visible = true;
                txt_title.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;

                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_expenses;", "title", txt_title);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void over_all_button_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Expenses Wise Report";
                reportType = 2;

                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                viewer_date_wise.Visible = false;
                viewer_name_wise.Visible = false;
                viewer_over_all.Visible = true;

                this.viewer_over_all.Dock = DockStyle.Fill;
                

                lbl_title.Visible = false;
                txt_title.Visible = false;

                FromDate.Visible = false;
                ToDate.Visible = false;
                lbl_from_date.Visible = false;
                lbl_to_date.Visible = false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string condition)
        {
            try
            {
                all_expenses_ds report = new all_expenses_ds();
                GetSetData.query = @"SELECT pos_expense_details.date, pos_expense_details.time, pos_expense_details.net_amount, pos_expense_items.amount, pos_expense_items.remarks, pos_expenses.title
                                    FROM pos_expense_details INNER JOIN pos_expense_items ON pos_expense_details.expense_id = pos_expense_items.expense_id INNER JOIN pos_expenses ON pos_expense_details.exp_id = pos_expenses.exp_id";

                if (condition == "DateWise")
                {
                    GetSetData.query += " WHERE (pos_expense_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "') ORDER BY pos_expense_details.date asc;";
                }
                else if (condition == "TitleWise")
                {
                    GetSetData.query += " where pos_expenses.title = '" + txt_title.Text + "' and pos_expense_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ORDER BY pos_expense_details.date asc;";                    
                }

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlDataAdapter da = new SqlDataAdapter(GetSetData.query, conn);
                da.Fill(report, report.Tables[0].TableName);

                ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("all_expenses", report.Tables[0]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(rds);


                // **********************************************************************************************************
                all_expenses_ds report_setting = new all_expenses_ds();
                GetSetData.query = @"SELECT title, address, phone_no, note, copyrights FROM pos_report_settings;";

                SqlDataAdapter report_da = new SqlDataAdapter(GetSetData.query, conn);
                report_da.Fill(report_setting, report_setting.Tables[0].TableName);

                ReportDataSource report_rds = new Microsoft.Reporting.WinForms.ReportDataSource("report_setting_ds", report_setting.Tables[0]);
                viewer.LocalReport.DataSources.Add(report_rds);

                viewer.LocalReport.Refresh();
                viewer.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

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

                if (condition == "DateWise")
                {
                    ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                    viewer.LocalReport.SetParameters(fromDate);

                    ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                    viewer.LocalReport.SetParameters(toDate);
                }

                viewer.RefreshReport();
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
                    DisplayReportInReportViewer(this.viewer_date_wise, "DateWise");
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.viewer_name_wise, "TitleWise");
                }
                else if (reportType == 2)
                {
                    DisplayReportInReportViewer(this.viewer_over_all, "nill");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
