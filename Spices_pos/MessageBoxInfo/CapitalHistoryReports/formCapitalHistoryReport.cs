using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Login_info.controllers;

namespace Message_box_info.CapitalHistoryReports
{
    public partial class formCapitalHistoryReport : Form
    {
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams handleParam = base.CreateParams;

                if (enableFormLevelDoubleBuffering)
                {
                    handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED    
                }
                else
                {
                    handleParam.ExStyle = originalExStyle;
                }

                return handleParam;
            }
        }

        public void TrunOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MinimizeBox = true;
            this.WindowState = FormWindowState.Minimized;
        }

        public formCapitalHistoryReport()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void refresh()
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();

            txtStatus.Text = null;
            lblStatus.Visible = false;
            txtStatus.Visible = false;
            btnDateWise.Checked = true;

            pnlDateWise.Visible = true;
            pnlStatusWise.Visible = false;

            if (btnDateWise.Checked == true)
            {
                this.pnlDateWise.Dock = DockStyle.Fill;
                this.viewerDateWise.Dock = DockStyle.Fill;
            }

            this.viewerDateWise.Clear();
            this.viewerStatusWise.Clear();
        }

        private void formCapitalHistoryReport_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TrunOffFormLevelDoubleBuffering();
        }

        private void btnDateWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text = null;

                if (btnDateWise.Checked == true)
                {
                    this.pnlDateWise.Dock = DockStyle.Fill;
                    this.viewerDateWise.Dock = DockStyle.Fill;
                }

                pnlStatusWise.Visible = false;
                pnlDateWise.Visible = true;

                FromDate.Visible = true;
                ToDate.Visible = true;
                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                lblStatus.Visible = false;

                txtStatus.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnStatusWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Text = null;

                pnlStatusWise.Visible = true;

                if (btnStatusWise.Checked == true)
                {
                    this.pnlStatusWise.Dock = DockStyle.Fill;
                    this.viewerStatusWise.Dock = DockStyle.Fill;
                }

                lbl_from_date.Visible = true;
                lbl_to_date.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;

                pnlDateWise.Visible = false;
                lblStatus.Visible = true;
                txtStatus.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
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
            // *******************************************************************************************

            ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
            viewer.LocalReport.SetParameters(fromDate);

            ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
            viewer.LocalReport.SetParameters(toDate);
        }

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureDateWiseCapitalHistoryDetailsTableAdapter.Fill(this.capitalHistory_ds.ReportProcedureDateWiseCapitalHistoryDetails, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.capitalHistory_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                DisplayReportInReportViewer(viewer);
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
                this.ReportProcedureStatusCapitalHistoryDetailsTableAdapter.Fill(this.capitalHistory_ds.ReportProcedureStatusCapitalHistoryDetails, FromDate.Text, ToDate.Text, txtStatus.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.capitalHistory_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                DisplayReportInReportViewer(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDateWise.Checked == true && btnStatusWise.Checked == false)
                {
                    DisplayDateWiseReportInReportViewer(this.viewerDateWise);
                }
                else if (btnDateWise.Checked == false && btnStatusWise.Checked == true)
                {
                    DisplayStatusWiseReportInReportViewer(this.viewerStatusWise);
                }
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
    }
}
