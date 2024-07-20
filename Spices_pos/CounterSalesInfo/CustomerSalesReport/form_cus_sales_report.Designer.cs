using Spices_pos.CounterSalesInfo.CustomerSalesReport;

namespace CounterSales_info.CustomerSalesReport
{
    partial class form_cus_sales_report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource13 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource14 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource15 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource16 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportProcedureBillWiseSalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerSales_Ds = new CustomerSales_Ds();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReportProcedureBillWiseTotalMarketPriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReportProcedureCounterSalesLastCreditsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportProcedureBillWiseSalesTableAdapter = new Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureBillWiseSalesTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            this.ReportProcedureBillWiseTotalMarketPriceTableAdapter = new Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureBillWiseTotalMarketPriceTableAdapter();
            this.ReportProcedureCounterSalesLastCreditsTableAdapter = new Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureCounterSalesLastCreditsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureBillWiseSalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerSales_Ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureBillWiseTotalMarketPriceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureCounterSalesLastCreditsBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportProcedureBillWiseSalesBindingSource
            // 
            this.ReportProcedureBillWiseSalesBindingSource.DataMember = "ReportProcedureBillWiseSales";
            this.ReportProcedureBillWiseSalesBindingSource.DataSource = this.CustomerSales_Ds;
            // 
            // CustomerSales_Ds
            // 
            this.CustomerSales_Ds.DataSetName = "CustomerSales_Ds";
            this.CustomerSales_Ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.CustomerSales_Ds;
            // 
            // ReportProcedureBillWiseTotalMarketPriceBindingSource
            // 
            this.ReportProcedureBillWiseTotalMarketPriceBindingSource.DataMember = "ReportProcedureBillWiseTotalMarketPrice";
            this.ReportProcedureBillWiseTotalMarketPriceBindingSource.DataSource = this.CustomerSales_Ds;
            // 
            // ReportProcedureCounterSalesLastCreditsBindingSource
            // 
            this.ReportProcedureCounterSalesLastCreditsBindingSource.DataMember = "ReportProcedureCounterSalesLastCredits";
            this.ReportProcedureCounterSalesLastCreditsBindingSource.DataSource = this.CustomerSales_Ds;
            // 
            // reportViewer1
            // 
            reportDataSource9.Name = "cus_sales";
            reportDataSource9.Value = this.ReportProcedureBillWiseSalesBindingSource;
            reportDataSource10.Name = "DataSet1";
            reportDataSource10.Value = this.ReportProcedureReportsTitlesBindingSource;
            reportDataSource11.Name = "DataSet2";
            reportDataSource11.Value = this.ReportProcedureBillWiseTotalMarketPriceBindingSource;
            reportDataSource12.Name = "LastCredits_ds";
            reportDataSource12.Value = this.ReportProcedureCounterSalesLastCreditsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource11);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource12);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CounterSales_info.CustomerSalesReport.cus_sales_report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 192);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(762, 573);
            this.reportViewer1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 32);
            this.panel1.TabIndex = 11;
            // 
            // Closebutton
            // 
            this.Closebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.Closebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Closebutton.Dock = System.Windows.Forms.DockStyle.Right;
            this.Closebutton.FlatAppearance.BorderSize = 0;
            this.Closebutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(116)))), ((int)(((byte)(163)))));
            this.Closebutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.Closebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Closebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.Closebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Closebutton.Location = new System.Drawing.Point(975, 0);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(49, 32);
            this.Closebutton.TabIndex = 15;
            this.Closebutton.Text = "x";
            this.Closebutton.UseVisualStyleBackColor = false;
            this.Closebutton.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1172, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // reportViewer2
            // 
            reportDataSource13.Name = "loyalCusSales";
            reportDataSource13.Value = this.ReportProcedureBillWiseSalesBindingSource;
            reportDataSource14.Name = "DataSet1";
            reportDataSource14.Value = this.ReportProcedureReportsTitlesBindingSource;
            reportDataSource15.Name = "DataSet2";
            reportDataSource15.Value = this.ReportProcedureBillWiseTotalMarketPriceBindingSource;
            reportDataSource16.Name = "DataSet3";
            reportDataSource16.Value = this.ReportProcedureCounterSalesLastCreditsBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource13);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource14);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource15);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource16);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "CounterSales_info.CustomerSalesReport.cus_sales_large_report.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(472, 77);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(415, 683);
            this.reportViewer2.TabIndex = 13;
            this.reportViewer2.Visible = false;
            // 
            // ReportProcedureBillWiseSalesTableAdapter
            // 
            this.ReportProcedureBillWiseSalesTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureBillWiseTotalMarketPriceTableAdapter
            // 
            this.ReportProcedureBillWiseTotalMarketPriceTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureCounterSalesLastCreditsTableAdapter
            // 
            this.ReportProcedureCounterSalesLastCreditsTableAdapter.ClearBeforeFill = true;
            // 
            // form_cus_sales_report
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 765);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form_cus_sales_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_cus_sales_report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_cus_sales_report_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_cus_sales_report_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureBillWiseSalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerSales_Ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureBillWiseTotalMarketPriceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureCounterSalesLastCreditsBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.Label label2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource ReportProcedureBillWiseSalesBindingSource;
        private CustomerSales_Ds CustomerSales_Ds;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private System.Windows.Forms.BindingSource ReportProcedureBillWiseTotalMarketPriceBindingSource;
        private System.Windows.Forms.BindingSource ReportProcedureCounterSalesLastCreditsBindingSource;
        private Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureBillWiseSalesTableAdapter ReportProcedureBillWiseSalesTableAdapter;
        private Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
        private Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureBillWiseTotalMarketPriceTableAdapter ReportProcedureBillWiseTotalMarketPriceTableAdapter;
        private Spices_pos.CounterSalesInfo.CustomerSalesReport.CustomerSales_DsTableAdapters.ReportProcedureCounterSalesLastCreditsTableAdapter ReportProcedureCounterSalesLastCreditsTableAdapter;
    }
}