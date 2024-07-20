using System;
using System.Windows.Forms;
using Customers_info.forms;
using Datalayer;
using Message_box_info.forms;
using Microsoft.Reporting.WinForms;
using RefereningMaterial;
using Spices_pos.CustomersInfo.controllers;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Customers_info.GurantorReports
{
    public partial class formGurantorsReports : Form
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

         public formGurantorsReports()
         {
             InitializeComponent();
         }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void FillComboBoxCustomerName()
        {
            txtTitle.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", txtCode.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            txtCode.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", txtTitle.Text);
        }

        private void refresh()
        {
            lblTitle.Visible = false;
            txtTitle.Visible = false;
            lblCode.Visible = false;
            txtCode.Visible = false;

            btnOverAll.Checked = true;
            pnlOverAll.Visible = true;
            pnlBatchNo.Visible = false;
            pnlInvoice.Visible = false;
            pnlCustomerWise.Visible = false;

            if (btnOverAll.Checked == true)
            {
                this.pnlOverAll.Dock = DockStyle.Fill;
                this.viewerOverAll.Dock = DockStyle.Fill;
            }

            this.viewerBatchNo.Clear();
            this.viewerOverAll.Clear();
            this.viewerInvoiceNo.Clear();
            this.viewerCustomerWise.Clear();
        }

        private void formGurantorsReports_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_area_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlBatchNo.Visible = true;

                if (btnBatchNo.Checked == true)
                {
                    this.pnlBatchNo.Dock = DockStyle.Fill;
                    this.viewerBatchNo.Dock = DockStyle.Fill;
                }

                pnlBatchNo.Visible = true;
                pnlOverAll.Visible = false;
                pnlInvoice.Visible = false;
                pnlCustomerWise.Visible = false;

                txtTitle.Visible = true;
                lblTitle.Visible = true;
                lblCode.Visible = false;
                txtCode.Visible = false;
                lblTitle.Text = "Zone:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select * from pos_batchNo;", "title", txtTitle);
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
                pnlOverAll.Visible = true;

                if (btnOverAll.Checked == true)
                {
                    this.pnlOverAll.Dock = DockStyle.Fill;
                    this.viewerOverAll.Dock = DockStyle.Fill;
                }

                pnlBatchNo.Visible = false;
                pnlInvoice.Visible = false;
                pnlCustomerWise.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                lblCode.Visible = false;
                txtCode.Visible = false;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btn_supplier_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlInvoice.Visible = true;

                if (btnInvoiceNo.Checked == true)
                {
                    this.pnlInvoice.Dock = DockStyle.Fill;
                    this.viewerInvoiceNo.Dock = DockStyle.Fill;
                }

                pnlBatchNo.Visible = false;
                pnlOverAll.Visible = false;
                pnlCustomerWise.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                lblCode.Visible = false;
                txtCode.Visible = false;
                lblTitle.Text = "Invoice #:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                GetSetData.FillComboBoxWithValues("select distinct(billNo) from pos_payment_grantors;", "billNo", txtTitle);
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
                pnlCustomerWise.Visible = true;

                if (btnCustomer.Checked == true)
                {
                    this.pnlCustomerWise.Dock = DockStyle.Fill;
                    this.viewerCustomerWise.Dock = DockStyle.Fill;
                }

                pnlBatchNo.Visible = false;
                pnlOverAll.Visible = false;
                pnlInvoice.Visible = false;

                lblTitle.Visible = true;
                lblCode.Visible = true;
                txtCode.Visible = true;
                txtTitle.Visible = true;
                lblTitle.Text = "Customer:";
                txtTitle.Text = null;
                txtTitle.Items.Clear();
                txtCode.Text = null;
                txtCode.Items.Clear();

                GetSetData.FillComboBoxWithValues("select * from pos_customers;", "full_name", txtTitle);
                GetSetData.FillComboBoxWithValues("select * from pos_customers;", "cus_code", txtCode);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
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

        private void DisplayBatchWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureGurantorsBatchNoWiseTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureGurantorsBatchNoWise, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureReportsTitles);

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

        private void DisplayInvoiceNoReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureGurantorsBillWiseTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureGurantorsBillWise, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureReportsTitles);
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

        private void DisplayCustomerWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureGurantorsCustomerWiseTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureGurantorsCustomerWise, txtTitle.Text, txtCode.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureReportsTitles);

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

        private void DisplayOverAllReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureGurantorsOverallDetailsTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureGurantorsOverallDetails);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.gurantorsReportDs.ReportProcedureReportsTitles);

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
            if (btnBatchNo.Checked == true && btnOverAll.Checked == false && btnInvoiceNo.Checked == false && btnCustomer.Checked == false)
            {
                DisplayBatchWiseReportInReportViewer(this.viewerBatchNo);
            }
            else if (btnOverAll.Checked == true && btnBatchNo.Checked == false && btnInvoiceNo.Checked == false && btnCustomer.Checked == false)
            {
                DisplayOverAllReportInReportViewer(this.viewerOverAll);
            }
            else if (btnInvoiceNo.Checked == true && btnBatchNo.Checked == false && btnOverAll.Checked == false && btnCustomer.Checked == false)
            {
                DisplayInvoiceNoReportInReportViewer(this.viewerInvoiceNo);
            }
            else if (btnCustomer.Checked == true && btnBatchNo.Checked == false && btnOverAll.Checked == false && btnInvoiceNo.Checked == false)
            {
                DisplayCustomerWiseReportInReportViewer(this.viewerCustomerWise);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnCustomer.Checked == true)
            {
                //FillComboBoxCustomeCodes();
            }
        }

        private void txtCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnCustomer.Checked == true)
            {
                FillComboBoxCustomerName();
            }
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnCustomer.Checked == true)
                {
                    Customer_details.selected_customer = txtTitle.Text;
                    Button_controls.CustomerDetailsbuttons();
                    txtTitle.Text = Customer_details.selected_customer;
                    txtCode.Text = Customer_details.selected_customerCode;
                }
            }
        }
    }
}
