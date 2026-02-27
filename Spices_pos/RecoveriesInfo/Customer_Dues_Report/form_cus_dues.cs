using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.RecoveriesInfo.Customer_Dues_Report;

namespace Recoverier_info.Customer_Dues_Report
{
    public partial class form_cus_dues : Form
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

        public form_cus_dues()
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
            txtTitle.Text = null;
            lblTitle.Visible = false;
            txtTitle.Visible = false;
            lblFromDate.Visible = true;
            lblToDate.Visible = true;
            FromDate.Visible = true;
            ToDate.Visible = true;

            pnl_over_all.Visible = false;
            pnl_province.Visible = false;
            pnl_country.Visible = false;
            pnlBatchNo.Visible = false;
            pnlDefaulter.Visible = false;
            pnlAllDefaulters.Visible = true;

            if (btnAllDefaulters.Checked == true)
            {
                this.pnlAllDefaulters.Dock = DockStyle.Fill;
                this.viewerAllDefaulters.Dock = DockStyle.Fill;
            }

            this.viewer_province.Clear();
            this.viewer_over_all.Clear();
            this.viewer_country.Clear();
            this.viewerBatchNo.Clear();
            this.viewer_over_all.Clear();
            this.viewerAllDefaulters.Clear();
        }

        private void form_cus_dues_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                //DisplayOverAllReportInReportViewer(this.viewer_over_all);
                DisplayDefaultersWiseReportInReportViewer(this.viewerAllDefaulters);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btn_area_wise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnl_province.Visible = true;

                if (btn_province.Checked == true)
                {
                    this.pnl_province.Dock = DockStyle.Fill;
                    this.viewer_province.Dock = DockStyle.Fill;
                }

                pnl_over_all.Visible = false;
                pnl_country.Visible = false;
                pnlBatchNo.Visible = false;
                pnlDefaulter.Visible = false;
                pnlAllDefaulters.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
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

        private void btn_over_all_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnl_over_all.Visible = true;

                if (btn_over_all.Checked == true)
                {
                    this.pnl_over_all.Dock = DockStyle.Fill;
                    this.viewer_over_all.Dock = DockStyle.Fill;
                }
                
                pnl_province.Visible = false;
                pnl_country.Visible = false;
                pnlBatchNo.Visible = false;
                pnlDefaulter.Visible = false;
                pnlAllDefaulters.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
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

                pnl_country.Visible = true;

                if (btn_country.Checked == true)
                {
                    this.pnl_country.Dock = DockStyle.Fill;
                    this.viewer_country.Dock = DockStyle.Fill;
                }

                pnl_province.Visible = false;
                pnl_over_all.Visible = false;
                pnlBatchNo.Visible = false;
                pnlDefaulter.Visible = false;
                pnlAllDefaulters.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                lblToDate.Visible = false;
                lblToDate.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
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

        private void btnBatchNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlBatchNo.Visible = true;

                if (btnBatchNo.Checked == true)
                {
                    this.pnlBatchNo.Dock = DockStyle.Fill;
                    this.viewerBatchNo.Dock = DockStyle.Fill;
                }

                pnl_over_all.Visible = false;
                pnl_country.Visible = false;
                pnl_province.Visible = false;
                pnlDefaulter.Visible = false;
                pnlAllDefaulters.Visible = false;

                lblTitle.Visible = true;
                txtTitle.Visible = true;
                lblFromDate.Visible = false;
                lblToDate.Visible = false;
                FromDate.Visible = false;
                ToDate.Visible = false;
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

        private void btnDefaulter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlDefaulter.Visible = true;

                if (btnDefaulter.Checked == true)
                {
                    this.pnlDefaulter.Dock = DockStyle.Fill;
                    this.viewerDefaulter.Dock = DockStyle.Fill;
                }

                pnl_over_all.Visible = false;
                pnl_country.Visible = false;
                pnl_province.Visible = false;
                pnlBatchNo.Visible = false;
                pnlAllDefaulters.Visible = false;

                lblTitle.Visible = false;
                txtTitle.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnAllDefaulters_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                pnlAllDefaulters.Visible = true;

                if (btnAllDefaulters.Checked == true)
                {
                    this.pnlAllDefaulters.Dock = DockStyle.Fill;
                    this.viewerAllDefaulters.Dock = DockStyle.Fill;
                }

                pnl_over_all.Visible = false;
                pnl_country.Visible = false;
                pnl_province.Visible = false;
                pnlBatchNo.Visible = false;
                pnlDefaulter.Visible = false;
                
                lblTitle.Visible = false;
                txtTitle.Visible = false;
                lblFromDate.Visible = true;
                lblToDate.Visible = true;
                FromDate.Visible = true;
                ToDate.Visible = true;
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

        private void DisplayOverAllReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureOverAllCustomerDuesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureOverAllCustomerDues);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                //**********************************************************
                Linear customerId = new Linear();

                foreach (customer_dues_ds.ReportProcedureOverAllCustomerDuesRow row in this.customer_dues_ds.ReportProcedureOverAllCustomerDues.Rows)
                {
                    customerId.Data = row.customer_id.ToString();

                    GetSetData.query = @"select status from pos_sales_accounts where (customer_id = '" + customerId.Data.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "Installment")
                    {
                        row.dueStatus = "Installment";
                    }
                    else
                    {
                        row.dueStatus = "Creditor";
                    }
                }

                reportParamenters(viewer);
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
                this.ReportProcedureCountryWiseCustomerDuesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureCountryWiseCustomerDues, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                //**********************************************************
                Linear customerId = new Linear();

                foreach (customer_dues_ds.ReportProcedureCountryWiseCustomerDuesRow row in this.customer_dues_ds.ReportProcedureCountryWiseCustomerDues.Rows)
                {
                    customerId.Data = row.customer_id.ToString();

                    GetSetData.query = @"select status from pos_sales_accounts where (customer_id = '" + customerId.Data.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "Installment")
                    {
                        row.dueStatus = "Installment";
                    }
                    else
                    {
                        row.dueStatus = "Creditor";
                    }
                }

                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayCityWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureCityWiseCustomerDuesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureCityWiseCustomerDues, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                //**********************************************************
                Linear customerId = new Linear();

                foreach (customer_dues_ds.ReportProcedureCityWiseCustomerDuesRow row in this.customer_dues_ds.ReportProcedureCityWiseCustomerDues.Rows)
                {
                    customerId.Data = row.customer_id.ToString();

                    GetSetData.query = @"select status from pos_sales_accounts where (customer_id = '" + customerId.Data.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "Installment")
                    {
                        row.dueStatus = "Installment";
                    }
                    else
                    {
                        row.dueStatus = "Creditor";
                    }
                }

                reportParamenters(viewer);
                viewer.RefreshReport();
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
                this.ReportProcedureBatchWiseCustomerDuesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureBatchWiseCustomerDues, txtTitle.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;

                //**********************************************************
                Linear customerId = new Linear();

                foreach (customer_dues_ds.ReportProcedureBatchWiseCustomerDuesRow row in this.customer_dues_ds.ReportProcedureBatchWiseCustomerDues.Rows)
                {
                    customerId.Data = row.customer_id.ToString();

                    GetSetData.query = @"select status from pos_sales_accounts where (customer_id = '" + customerId.Data.ToString() + "');";
                    GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                    if (GetSetData.Data == "Installment")
                    {
                        row.dueStatus = "Installment";
                    }
                    else
                    {
                        row.dueStatus = "Creditor";
                    }
                }

                reportParamenters(viewer);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void DisplayDefaultersWiseReportInReportViewer(ReportViewer viewer)
        {
            try
            {
                this.ReportProcedureInstallmentDefaultersListTableAdapter.Fill(this.customer_dues_ds.ReportProcedureInstallmentDefaultersList, FromDate.Text, ToDate.Text);
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.customer_dues_ds.ReportProcedureReportsTitles);
                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.ZoomMode = ZoomMode.Percent;
                viewer.ZoomPercent = 100;
                reportParamenters(viewer);

                ReportParameter fromDate = new ReportParameter("pFromDate", FromDate.Text);
                viewer.LocalReport.SetParameters(fromDate);

                ReportParameter toDate = new ReportParameter("pToDate", ToDate.Text);
                viewer.LocalReport.SetParameters(toDate);

                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void view_button_Click(object sender, EventArgs e)
        {
            //if (btn_province.Checked == true && btn_over_all.Checked == false && btn_country.Checked == false && btnBatchNo.Checked == false && btnDefaulter.Checked == false && btnAllDefaulters.Checked == false)
            //{
            //    DisplayCityWiseReportInReportViewer(this.viewer_province);
            //}
            //else if (btn_over_all.Checked == true && btn_province.Checked == false && btn_country.Checked == false && btnBatchNo.Checked == false && btnDefaulter.Checked == false && btnAllDefaulters.Checked == false)
            //{
            //    DisplayOverAllReportInReportViewer(this.viewer_over_all);
            //}
            //else if (btn_country.Checked == true && btn_province.Checked == false && btn_over_all.Checked == false && btnBatchNo.Checked == false && btnDefaulter.Checked == false && btnAllDefaulters.Checked == false)
            //{
            //    DisplayCountryWiseReportInReportViewer(this.viewer_country);
            //}
            //else if (btnBatchNo.Checked == true && btn_province.Checked == false && btn_over_all.Checked == false && btn_country.Checked == false && btnDefaulter.Checked == false && btnAllDefaulters.Checked == false)
            //{
            //    DisplayBatchWiseReportInReportViewer(this.viewerBatchNo);
            //}
            //else if (btnDefaulter.Checked == true && btn_province.Checked == false && btn_over_all.Checked == false && btn_country.Checked == false && btnBatchNo.Checked == false && btnAllDefaulters.Checked == false)
            //{
            //    DisplayDefaultersWiseReportInReportViewer(this.viewerDefaulter);
            //}
            //else if (btnAllDefaulters.Checked == true && btn_province.Checked == false && btn_over_all.Checked == false && btn_country.Checked == false && btnBatchNo.Checked == false && btnDefaulter.Checked == false)
            //{
                DisplayDefaultersWiseReportInReportViewer(this.viewerAllDefaulters);
            //}
        }
    }
}
