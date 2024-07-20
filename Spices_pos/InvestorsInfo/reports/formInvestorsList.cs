using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Investors_info.reports
{
    public partial class formInvestorsList : Form
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

        public formInvestorsList()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public int reportType = 0; // 0 for date, 1 for bill, 2 for customer, 3 for salesman

        private void refresh()
        {
            lblReportTitle.Text = "Overall Investors Report";
            reportType = 3;

            lblTitle.Visible = false;
            txtTitle.Visible = false;

            pnl_over_all.Visible = true;
            pnl_area.Visible = false;
            pnl_supplier_wise.Visible = false;
            pnl_status.Visible = false;

            this.pnl_over_all.Dock = DockStyle.Fill;
               

            this.viewer_area.Clear();
            this.viewer_over_all.Clear();
            this.viewer_supplier_wise.Clear();
            this.viewer_status.Clear();
        }

        private void formInvestorsList_Load(object sender, EventArgs e)
        {
           refresh();
        }

        private void btn_country_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Country Wise Investors Report";
                reportType = 0;

                pnl_area.Visible = true;

                this.pnl_area.Dock = DockStyle.Fill;
                    

                pnl_area.Visible = true;
                pnl_over_all.Visible = false;
                pnl_supplier_wise.Visible = false;
                pnl_status.Visible = false;

                txtTitle.Visible = true;
                lblTitle.Visible = true;
                lblTitle.Text = "Country:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_country;", "title", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_province_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Province Wise Investors Report";
                reportType = 1;

                pnl_supplier_wise.Visible = true;

                this.pnl_supplier_wise.Dock = DockStyle.Fill;
                   

                pnl_area.Visible = false;
                pnl_over_all.Visible = false;
                pnl_status.Visible = false;

                lblTitle.Visible = true;
                lblTitle.Text = "Province:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_city;", "title", txtTitle);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_status_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Status Wise Investors Report";
                reportType = 2;
                pnl_status.Visible = true;

                this.pnl_status.Dock = DockStyle.Fill;
                   

                pnl_area.Visible = false;
                pnl_over_all.Visible = false;
                pnl_supplier_wise.Visible = false;

                lblTitle.Text = "Status:";
                lblTitle.Visible = true;
                txtTitle.Visible = true;

                txtTitle.Text = "Active";
                txtTitle.Items.Clear();
                txtTitle.Items.Add("Active");
                txtTitle.Items.Add("Deactive");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_over_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = "Overall Investors Report";
                reportType = 3;

                txtTitle.Text = null;
                pnl_over_all.Visible = true;

                this.pnl_over_all.Dock = DockStyle.Fill;
                   

                pnl_area.Visible = false;
                pnl_supplier_wise.Visible = false;
                pnl_status.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reportParamenters(ReportViewer viewer)
        {
            try
            {
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
            }
            catch (Exception es)
            {

                MessageBox.Show(es.Message);
            }
        }

        private void DisplayOverAllReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureOverAllInvestorsListTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureOverAllInvestorsList);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayStatusWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureStatusWiseInvestorsListTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureStatusWiseInvestorsList, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
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

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayCountryWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCountryWiseInvestorsListTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureCountryWiseInvestorsList, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayProvinceWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureProvinceWiseInvestorsListTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureProvinceWiseInvestorsList, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.InvestorsList_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            if (reportType == 0)
            {
                DisplayCountryWiseReportInReportViewer(this.viewer_area);
            }
            else if (reportType == 1)
            {
                DisplayProvinceWiseReportInReportViewer(this.viewer_supplier_wise);
            }
            else if (reportType == 2)
            {
                DisplayStatusWiseReportInReportViewer(this.viewer_status);
            }
            else if (reportType == 3)
            {
                DisplayOverAllReportInReportViewer(this.viewer_over_all);
            }
        }
    }
}
