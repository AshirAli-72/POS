using System;
using System.Windows.Forms;
using Login_info.controllers;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.CustomerSalesInfo.CustomerProductsReturned
{
    public partial class form_cus_returns_report : Form
    {
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams handleParam = base.CreateParams;
        //        handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
        //        return handleParam;
        //    }
        //}

        public form_cus_returns_report()
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

        private void DisplayReportInReportViewer(ReportViewer viewer, string reportTitle)
        {
            try
            {
                this.ReporProcedureBillWiseCounterReturnsTableAdapter.Fill(this.CustomerReturns_Ds.ReporProcedureBillWiseCounterReturns, TextData.billNo.ToString());
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.CustomerReturns_Ds.ReportProcedureReportsTitles);

                //viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                //viewer.ZoomMode = ZoomMode.Percent;
                //viewer.ZoomPercent = 100;
                viewer.LocalReport.EnableExternalImages = true;

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

                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_cus_returns_report_Load(object sender, EventArgs e)
        {
            try
            {
                //TextData.general_options = data.UserPermissions("page_size", "pos_general_settings");

                //if (TextData.general_options == "A6")
                //{
                    reportViewer2.Visible = false;
                    reportViewer1.Visible = true;
                    reportViewer1.Dock = DockStyle.Fill;
                    DisplayReportInReportViewer(this.reportViewer1, "cus_sales");
                //}
                //else
                //{
                //    reportViewer1.Visible = false;
                //    reportViewer2.Visible = true;
                //    reportViewer2.Dock = DockStyle.Fill;
                //    DisplayReportInReportViewer(this.reportViewer2, "loyalCusSales"); 
                //}
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_cus_returns_report_KeyDown(object sender, KeyEventArgs e)
        {
            TextData.general_options = data.UserPermissions("page_size", "pos_general_settings");

            if (TextData.general_options == "A6")
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
            }
            else
            {
                if (e.KeyCode == Keys.P)
                {
                    reportViewer2.PrintDialog();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    reportViewer2.PrintDialog();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    reportViewer2.PrintDialog();
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
