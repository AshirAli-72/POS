using System;
using System.Windows.Forms;
using Login_info.controllers;
using Microsoft.Reporting.WinForms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using OnBarcode.Barcode;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.CounterSalesInfo.CustomerSalesReport;

namespace CounterSales_info.CustomerSalesReport
{
    public partial class form_cus_sales_report : Form
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

        public form_cus_sales_report()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);


        //}

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayReportInReportViewer(ReportViewer viewer, string reportTitle)
        {
            try
            {
                this.ReportProcedureBillWiseSalesTableAdapter.Fill(this.CustomerSales_Ds.ReportProcedureBillWiseSales, TextData.billNo.ToString());
                this.ReportProcedureReportsTitlesTableAdapter.Fill(this.CustomerSales_Ds.ReportProcedureReportsTitles);
                this.ReportProcedureBillWiseTotalMarketPriceTableAdapter.Fill(this.CustomerSales_Ds.ReportProcedureBillWiseTotalMarketPrice, TextData.billNo.ToString());
                this.ReportProcedureCounterSalesLastCreditsTableAdapter.Fill(this.CustomerSales_Ds.ReportProcedureCounterSalesLastCredits, TextData.customer_name, TextData.customerCode);
                //viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                //viewer.ZoomMode = ZoomMode.Percent;
                //viewer.ZoomPercent = 100;

                Linear barcode = new Linear();
                //Linear ItemNote = new Linear();
                barcode.Type = BarcodeType.CODE128;

                foreach (CustomerSales_Ds.ReportProcedureBillWiseSalesRow row in this.CustomerSales_Ds.ReportProcedureBillWiseSales.Rows)
                {
                    barcode.Data = row.billNo.ToString();
                    barcode.Format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    row.invoiceBarcode = barcode.drawBarcodeAsBytes();

                    //ItemNote.Data = row.note.ToString();
                    //row.itemNote = ItemNote.Data + " - ";
                }
                //*********************************************************************************

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

                TextData.general_options = "";
                TextData.general_options = data.UserPermissions("showNoteInReport", "pos_general_settings");
                ReportParameter showNote = new ReportParameter("showNote", TextData.general_options);
                viewer.LocalReport.SetParameters(showNote);

                GetSetData.Data = data.UserPermissions("cash_on_hand", "pos_sales_accounts", "billNo", TextData.billNo);
                ReportParameter cash_in_hand = new ReportParameter("pCashInHand", GetSetData.Data);
                viewer.LocalReport.SetParameters(cash_in_hand);

                GetSetData.Data = data.UserPermissions("balance_amount", "pos_sales_accounts", "billNo", TextData.billNo);
                ReportParameter balance_amount = new ReportParameter("pBalanceAmount", GetSetData.Data);
                viewer.LocalReport.SetParameters(balance_amount);

                viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                viewer.RefreshReport();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_cus_sales_report_Load(object sender, EventArgs e)
        {
            try
            {
                if (TextData.checkPrintA4Report == true)
                {
                    reportViewer1.Visible = false;
                    reportViewer2.Visible = true;
                    reportViewer2.Dock = DockStyle.Fill;
                    DisplayReportInReportViewer(this.reportViewer2, "loyalCusSales");
                    
                }
                else
                {
                    reportViewer2.Visible = false;
                    reportViewer1.Visible = true;
                    reportViewer1.Dock = DockStyle.Fill;
                    DisplayReportInReportViewer(this.reportViewer1, "cus_sales");
                }
            }
            catch (Exception es)
            {
                //error.errorMessage(es.Message);
                //error.ShowDialog();
                MessageBox.Show(es.Message);
            }
        }

        private void form_cus_sales_report_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (TextData.checkPrintA4Report == false)
                {
                    if (e.KeyCode == Keys.P || e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                    {
                        reportViewer1.PrintDialog();
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        this.Close();
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
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
