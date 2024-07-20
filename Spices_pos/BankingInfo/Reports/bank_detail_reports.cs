using System;
using System.Windows.Forms;
using RefereningMaterial;
using Datalayer;
using Message_box_info.forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.BankingInfo.Reports;

namespace Banking_info.Reports
{
    public partial class bank_detail_reports : Form
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
        public bank_detail_reports()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman


        private void refresh()
        {
            lblReportTitle.Text = "Bank Wise Report";
            reportType = 0;

            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            FromDate.Visible = true;
            ToDate.Visible = true;
            txt_title.Visible = false;

            lbl_from_date.Visible = true;
            lbl_to_date.Visible = true;
            lbl_title.Visible = false;

            pnl_overall.Visible = false;
            pnl_bank_wise.Visible = true;
            pnl_branch_wise.Visible = false;
            pnl_account_wise.Visible = false;
            pnl_employee.Visible = false;
            pnl_status_wise.Visible = false;

            this.pnl_bank_wise.Dock = DockStyle.Fill;

            this.viewer_overall.Clear();
            this.viewer_bank.Clear();
            this.viewer_branch.Clear();
            this.viewer_status.Clear();
            this.viewer_employee.Clear();
            this.viewer_accounts.Clear();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bank_detail_reports_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
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

        private void btn_bank_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Bank Wise Report";
                reportType = 0;

                pnl_bank_wise.Visible = true;

                this.pnl_bank_wise.Dock = DockStyle.Fill;

                pnl_overall.Visible = false;
                pnl_branch_wise.Visible = false;
                pnl_account_wise.Visible = false;
                pnl_employee.Visible = false;
                pnl_status_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Bank Title:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_bank;", "bank_title", txt_title);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_branch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Branch Wise Report";
                reportType = 1;

                pnl_branch_wise.Visible = true;
               
                this.pnl_branch_wise.Dock = DockStyle.Fill;

                pnl_overall.Visible = false;
                pnl_bank_wise.Visible = false;
                pnl_account_wise.Visible = false;
                pnl_employee.Visible = false;
                pnl_status_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Branch:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_bank_branch;", "branch_title", txt_title);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_status_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Status Wise Report";
                reportType = 3;

                pnl_status_wise.Visible = true;

                this.pnl_status_wise.Dock = DockStyle.Fill;

                pnl_overall.Visible = false;
                pnl_bank_wise.Visible = false;
                pnl_account_wise.Visible = false;
                pnl_employee.Visible = false;
                pnl_branch_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "T.Status:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_transaction_status;", "status_title", txt_title);

            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_account_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Account Wise Report";
                reportType = 2;

                pnl_account_wise.Visible = true;

                this.pnl_account_wise.Dock = DockStyle.Fill;

                pnl_overall.Visible = false;
                pnl_bank_wise.Visible = false;
                pnl_status_wise.Visible = false;
                pnl_employee.Visible = false;
                pnl_branch_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Account #";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_account_no;", "account_no", txt_title);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_employee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Employee Wise Report";
                reportType = 4;

                pnl_employee.Visible = true;

                this.pnl_employee.Dock = DockStyle.Fill;

                pnl_overall.Visible = false;
                pnl_bank_wise.Visible = false;
                pnl_status_wise.Visible = false;
                pnl_account_wise.Visible = false;
                pnl_branch_wise.Visible = false;

                txt_title.Visible = true;
                lbl_title.Visible = true;
                lbl_title.Text = "Employee:";
                txt_title.Text = null;
                txt_title.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_employees;", "full_name", txt_title);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_overall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Banking Report";
                reportType = 5;

                pnl_overall.Visible = true;

                this.pnl_overall.Dock = DockStyle.Fill;

                pnl_employee.Visible = false;
                pnl_bank_wise.Visible = false;
                pnl_status_wise.Visible = false;
                pnl_account_wise.Visible = false;
                pnl_branch_wise.Visible = false;

                txt_title.Visible = false;
                lbl_title.Visible = false;
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
                banking_ds Customer_retuned_report = new banking_ds();
                SqlDataAdapter customer_da = null;
                ReportDataSource Customer_rds = null;

                // ***************************************************************************************************************
                GetSetData.query = @"SELECT pos_banking_details.date, pos_banking_details.time, pos_banking_details.amount, pos_employees.full_name, pos_employees.emp_code, pos_transaction_status.status_title, 
                                    pos_transaction_type.transaction_type, pos_bank_branch.branch_title, pos_bank_account.account_title, pos_bank.bank_title, pos_account_no.account_no
                                    FROM pos_account_no INNER JOIN pos_banking_details ON pos_account_no.account_no_id = pos_banking_details.account_no_id INNER JOIN pos_bank ON 
                                    pos_banking_details.bank_id = pos_bank.bank_id INNER JOIN pos_bank_account ON pos_banking_details.account_id = pos_bank_account.account_id INNER JOIN pos_bank_branch ON 
                                    pos_banking_details.branch_id = pos_bank_branch.branch_id INNER JOIN pos_employees ON pos_banking_details.employee_id = pos_employees.employee_id INNER JOIN 
                                    pos_transaction_status ON pos_banking_details.status_id = pos_transaction_status.status_id INNER JOIN pos_transaction_type ON pos_banking_details.t_type_id = pos_transaction_type.transaction_id";


                if (condition != "nill")
                {
                    GetSetData.query += " WHERE pos_banking_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' and " + condition + " = '" + txt_title.Text + "' ORDER BY pos_banking_details.date asc;";
                }
                else
                {
                    GetSetData.query += " WHERE pos_banking_details.date BETWEEN '" + FromDate.Text + "' AND '" + ToDate.Text + "' ORDER BY pos_banking_details.date asc;";
                }

                customer_da = new SqlDataAdapter(GetSetData.query, conn);
                customer_da.Fill(Customer_retuned_report, Customer_retuned_report.Tables[0].TableName);

                Customer_rds = new Microsoft.Reporting.WinForms.ReportDataSource("new_purchase", Customer_retuned_report.Tables[0]);
                viewer.LocalReport.DataSources.Clear();
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.DataSources.Add(Customer_rds);
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

                //GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                //ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                //this.viewer_sold.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                viewer.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

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
                    DisplayReportInReportViewer(this.viewer_bank, "pos_bank.bank_title");
                }
                else if (reportType == 1)
                {
                    DisplayReportInReportViewer(this.viewer_branch, "pos_bank_branch.branch_title");
                }
                else if (reportType == 2)
                {
                    DisplayReportInReportViewer(this.viewer_accounts, "pos_account_no.account_no");

                }
                else if (reportType == 3)
                {
                    DisplayReportInReportViewer(this.viewer_status, "pos_transaction_status.status_title");
                }
                else if (reportType == 4)
                {
                    DisplayReportInReportViewer(this.viewer_employee, "pos_employees.full_name");
                }
                else if (reportType == 5)
                {
                    DisplayReportInReportViewer(this.viewer_overall, "nill");
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
