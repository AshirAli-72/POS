using System;
using System.Windows.Forms;
using Login_info.controllers;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Recoverier_info.ReportCustomerStatement
{
    public partial class formCustomerInstallmentStatement : Form
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

        public formCustomerInstallmentStatement()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static string billNo = "";

        private void DisplayCustomerWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCustomerInstallmentStatementTableAdapter.Fill(this.customerInstallmentStatement_db.ReportProcedureCustomerInstallmentStatement, billNo);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customerInstallmentStatement_db.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                viewer.LocalReport.EnableExternalImages = true;

                TextData.credits = 0;

                GetSetData.query = @"select sum(amount) from pos_recoveries inner join pos_recovery_details on pos_recoveries.recovery_id = pos_recovery_details.recovery_id
                                        where invoiceNo = '" + billNo + "';";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "" && GetSetData.Data != "NULL")
                {
                    TextData.credits = double.Parse(GetSetData.Data);
                }

                //*********************************************************************************
                ReportParameter recoveries = new ReportParameter("pRecoveries", TextData.credits.ToString());
                viewer.LocalReport.SetParameters(recoveries);
                // *******************************************************************************************

                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");

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

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formCustomerInstallmentStatement_Load(object sender, EventArgs e)
        {
            DisplayCustomerWiseReportInReportViewer(this.reportViewer1);
        }

    }
}
