using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using Reports_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Reports_info.Payables
{
    public partial class form_payables : Form
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
        public form_payables()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void supplier_payments()
        {
            try
            {
                payables_ds purcahses_report = new payables_ds();
                ReportDataSource purchases_rds = null;
                SqlDataAdapter purchases_da = null;

                // ************************************************************************************************************************************************
                GetSetData.query = @"SELECT pos_supplier_payables.previous_payables, pos_supplier_payables.due_days, pos_suppliers.full_name, pos_suppliers.code, pos_suppliers.telephone_no, pos_suppliers.mobile_no, pos_suppliers.contact_person, 
                                    pos_suppliers.bank_name, pos_suppliers.bank_account, pos_suppliers.remarks
                                    FROM pos_supplier_payables INNER JOIN pos_suppliers ON pos_supplier_payables.supplier_id = pos_suppliers.supplier_id and pos_supplier_payables.previous_payables > 0
                                    where (pos_supplier_payables.due_days between '" + FromDate.Text + "' AND '" + ToDate.Text + "') order by pos_supplier_payables.due_days;";


                // Script for Purchases ********************************
                purchases_da = new SqlDataAdapter(GetSetData.query, conn);
                purchases_da.Fill(purcahses_report, purcahses_report.Tables["Supplier_payments"].TableName);

                purchases_rds = new Microsoft.Reporting.WinForms.ReportDataSource("purchases", purcahses_report.Tables["Supplier_payments"]);
                this.viewer_payables.LocalReport.DataSources.Clear();
                this.viewer_payables.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.viewer_payables.ZoomMode = ZoomMode.Percent;
                this.viewer_payables.ZoomPercent = 100;
                this.viewer_payables.LocalReport.DataSources.Add(purchases_rds);
                this.viewer_payables.LocalReport.EnableExternalImages = true;

                //*******************************************************************************************
                GetSetData.Data = data.UserPermissions("picture_path", "pos_general_settings");
                GetSetData.query = data.UserPermissions("logo_path", "pos_configurations");
                //*******************************************************************************************

                if (GetSetData.query != "nill" && GetSetData.query != "")
                {
                    GetSetData.query = GetSetData.Data + GetSetData.query;
                    ReportParameter logo = new ReportParameter("pLogo", new Uri(GetSetData.query).AbsoluteUri);
                    this.viewer_payables.LocalReport.SetParameters(logo);
                }
                else
                {

                    ReportParameter logo = new ReportParameter("pLogo", "");
                    this.viewer_payables.LocalReport.SetParameters(logo);
                }

                // Retrive Report Settings from db *******************************************************************************************
                GetSetData.Data = data.UserPermissions("title", "pos_report_settings");
                ReportParameter title = new ReportParameter("pTitle", GetSetData.Data);
                this.viewer_payables.LocalReport.SetParameters(title);

                GetSetData.Data = data.UserPermissions("address", "pos_report_settings");
                ReportParameter address = new ReportParameter("pAddress", GetSetData.Data);
                this.viewer_payables.LocalReport.SetParameters(address);

                GetSetData.Data = data.UserPermissions("phone_no", "pos_report_settings");
                ReportParameter phone = new ReportParameter("pPhone", GetSetData.Data);
                this.viewer_payables.LocalReport.SetParameters(phone);

                //GetSetData.Data = data.UserPermissions("note", "pos_report_settings");
                //ReportParameter note = new ReportParameter("pNote", GetSetData.Data);
                //this.viewer_payables.LocalReport.SetParameters(note);

                GetSetData.Data = data.UserPermissions("copyrights", "pos_report_settings");
                ReportParameter copyrights = new ReportParameter("pCopyrights", GetSetData.Data);
                this.viewer_payables.LocalReport.SetParameters(copyrights);
                // *******************************************************************************************

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                this.viewer_payables.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                this.viewer_payables.LocalReport.SetParameters(toDate);

                this.viewer_payables.RefreshReport();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void form_payables_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();

                supplier_payments();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            button_controls.Main_menu();
            this.Close();
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            supplier_payments();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
        }
    }
}
