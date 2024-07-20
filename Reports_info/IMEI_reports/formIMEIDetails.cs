using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Investors_info.reports
{
    public partial class formIMEIDetails : Form
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

        public formIMEIDetails()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void refresh()
        {
            lblFromDate.Visible = true;
            lblToDate.Visible = true;
            lblTitle.Visible = false;
            lblCode.Visible = false;

            txtFromDate.Visible = true;
            txtToDate.Visible = true;
            txtTitle.Visible = false;
            txtCode.Visible = false;

            btnDateWise.Checked = true;
            pnlDateWise.Visible = true;
            pnlItemWise.Visible = false;
            pnlSupplierWise.Visible = false;
            pnlInvoiceWise.Visible = false;
            pnlStatusWise.Visible = false;

            if (btnDateWise.Checked == true)
            {
                this.pnlDateWise.Dock = DockStyle.Fill;
                this.viewerDateWise.Dock = DockStyle.Fill;
            }

            this.viewerSupplierWise.Clear();
            this.viewerInvoiceWise.Clear();
            this.viewerDateWise.Clear();
            this.viewerItemWise.Clear();
            this.viewerStatusWise.Clear();
        }

        private void formInvestorsList_Load(object sender, EventArgs e)
        {
            txtFromDate.Text = DateTime.Now.ToLongDateString();
            txtToDate.Text = DateTime.Now.ToLongDateString();
            refresh();
        }

        private void btn_country_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlSupplierWise.Visible = true;

                if (btnSupplier.Checked == true)
                {
                    this.pnlSupplierWise.Dock = DockStyle.Fill;
                    this.viewerSupplierWise.Dock = DockStyle.Fill;
                }

                pnlDateWise.Visible = false;
                pnlInvoiceWise.Visible = false;
                pnlItemWise.Visible = false;
                pnlStatusWise.Visible = false;

                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                lblTitle.Visible = true;
                lblCode.Visible = true;

                txtFromDate.Visible = true;
                txtToDate.Visible = true;
                txtTitle.Visible = true;
                txtCode.Visible = true;

                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "full_name", txtTitle);
                txtCode.Text = null;
                txtCode.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_suppliers;", "code", txtCode);
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
                pnlDateWise.Visible = true;

                if (btnDateWise.Checked == true)
                {
                    this.pnlDateWise.Dock = DockStyle.Fill;
                    this.viewerDateWise.Dock = DockStyle.Fill;
                }

                pnlSupplierWise.Visible = false;
                pnlInvoiceWise.Visible = false;
                pnlItemWise.Visible = false;
                pnlStatusWise.Visible = false;

                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                lblTitle.Visible = false;
                lblCode.Visible = false;

                txtFromDate.Visible = true;
                txtToDate.Visible = true;
                txtTitle.Visible = false;
                txtCode.Visible = false;
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
                pnlItemWise.Visible = true;

                if (btnItemWise.Checked == true)
                {
                    this.pnlItemWise.Dock = DockStyle.Fill;
                    this.viewerItemWise.Dock = DockStyle.Fill;
                }

                pnlDateWise.Visible = false;
                pnlInvoiceWise.Visible = false;
                pnlSupplierWise.Visible = false;
                pnlStatusWise.Visible = false;

                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                lblTitle.Visible = true;
                lblCode.Visible = true;

                txtFromDate.Visible = true;
                txtToDate.Visible = true;
                txtTitle.Visible = true;
                txtCode.Visible = true;

                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_products;", "prod_name", txtTitle);
                txtCode.Text = null;
                txtCode.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_products;", "barcode", txtCode);
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
                txtTitle.Text = null;
                pnlInvoiceWise.Visible = true;

                if (btnInvoiceWise.Checked == true)
                {
                    this.pnlInvoiceWise.Dock = DockStyle.Fill;
                    this.viewerInvoiceWise.Dock = DockStyle.Fill;
                }

                pnlDateWise.Visible = false;
                pnlItemWise.Visible = false;
                pnlSupplierWise.Visible = false;
                pnlStatusWise.Visible = false;

                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                lblTitle.Visible = true;
                lblCode.Visible = false;

                txtFromDate.Visible = false;
                txtToDate.Visible = false;
                txtTitle.Visible = true;
                txtCode.Visible = false;

                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select distinct(invoiceNo) from pos_purchase_imei;", "invoiceNo", txtTitle);
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
                txtTitle.Text = null;
                pnlStatusWise.Visible = true;

                if (btnStatusWise.Checked == true)
                {
                    this.pnlStatusWise.Dock = DockStyle.Fill;
                    this.viewerStatusWise.Dock = DockStyle.Fill;
                }

                pnlDateWise.Visible = false;
                pnlItemWise.Visible = false;
                pnlSupplierWise.Visible = false;
                pnlInvoiceWise.Visible = false;

                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                lblTitle.Visible = true;
                lblCode.Visible = false;

                txtFromDate.Visible = true;
                txtToDate.Visible = true;
                txtTitle.Visible = true;
                txtCode.Visible = false;

                txtTitle.Text = "True";
                txtTitle.Items.Clear();
                txtTitle.Items.Add("True");
                txtTitle.Items.Add("False");
                
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

        private void DisplayInvoiceWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureInvoiceWiseIMEIDetailsTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureInvoiceWiseIMEIDetails, txtTitle.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayItemWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureItemsWiseIMEIDetailsTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureItemsWiseIMEIDetails, txtFromDate.Text, txtToDate.Text, txtTitle.Text, txtCode.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplaySupplierWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureSupplierWiseIMEIDetailsTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureSupplierWiseIMEIDetails, txtFromDate.Text, txtToDate.Text, txtTitle.Text, txtCode.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureReportsTitles);
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

        private void DisplayDateWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.reportProcedureDateWiseIMEIDetailsTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureDateWiseIMEIDetails, txtFromDate.Text, txtToDate.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureReportsTitles);
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
                this.reportProcedureStatusWiseIMEIDetailsTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureStatusWiseIMEIDetails, txtFromDate.Text, txtToDate.Text, txtTitle.Text);
                this.reportProcedureReportsTitlesTableAdapter.Fill(this.imeiDetails_ds.ReportProcedureReportsTitles);
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
            if (btnSupplier.Checked == true && btnInvoiceWise.Checked == false && btnDateWise.Checked == false && btnItemWise.Checked == false && btnStatusWise.Checked == false)
            {
                DisplaySupplierWiseReportInReportViewer(this.viewerSupplierWise);
            }
            else if (btnInvoiceWise.Checked == true && btnSupplier.Checked == false && btnDateWise.Checked == false && btnItemWise.Checked == false && btnStatusWise.Checked == false)
            {
                DisplayInvoiceWiseReportInReportViewer(this.viewerInvoiceWise);
            }
            else if (btnDateWise.Checked == true && btnSupplier.Checked == false && btnInvoiceWise.Checked == false && btnItemWise.Checked == false && btnStatusWise.Checked == false)
            {
                DisplayDateWiseReportInReportViewer(this.viewerDateWise);
            }
            else if (btnItemWise.Checked == true && btnSupplier.Checked == false && btnInvoiceWise.Checked == false && btnDateWise.Checked == false && btnStatusWise.Checked == false)
            {
                DisplayItemWiseReportInReportViewer(this.viewerItemWise);
            }
            else if (btnStatusWise.Checked == true && btnSupplier.Checked == false && btnInvoiceWise.Checked == false && btnDateWise.Checked == false && btnItemWise.Checked == false)
            {
                DisplayStatusWiseReportInReportViewer(this.viewerStatusWise);
            }
        }

        private void FillComboBoxCustomerName()
        {
            try
            {
                if (btnSupplier.Checked == true)
                {
                    txtTitle.Text = data.UserPermissions("full_name", "pos_suppliers", "code", txtCode.Text);
                }
                else if (btnItemWise.Checked == true)
                {
                    txtTitle.Text = data.UserPermissions("prod_name", "pos_products", "barcode", txtCode.Text);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomeCodes()
        {
            try
            {
                if (btnSupplier.Checked == true)
                {
                    txtCode.Text = data.UserPermissions("code", "pos_suppliers", "full_name", txtTitle.Text);
                }
                else if (btnItemWise.Checked == true)
                {
                    txtCode.Text = data.UserPermissions("barcode", "pos_products", "prod_name", txtTitle.Text);
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomeCodes();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txtCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillComboBoxCustomerName();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }
    }
}
