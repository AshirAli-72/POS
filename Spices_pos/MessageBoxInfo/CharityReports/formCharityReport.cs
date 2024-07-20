using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.CharityReports
{
    public partial class formCharityReport : Form
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

         public formCharityReport()
         {
             InitializeComponent();
         }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formCharityReport_Load(object sender, EventArgs e)
        {
            FromDate.Text = DateTime.Now.ToLongDateString();
            ToDate.Text = DateTime.Now.ToLongDateString();
            DisplayReportInReportViewer(this.reportViewer1);
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

                ReportParameter pFromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(pFromDate);

                ReportParameter pToDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(pToDate);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCharityDetailsTableAdapter.Fill(this.charityPayments_ds.ReportProcedureCharityDetails, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.charityPayments_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                //**********************************************************
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
            DisplayReportInReportViewer(this.reportViewer1);
        }
    }
}
