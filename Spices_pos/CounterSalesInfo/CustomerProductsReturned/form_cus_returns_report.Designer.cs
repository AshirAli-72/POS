using Spices_pos.CounterSalesInfo.CustomerProductsReturned;

namespace CounterSales_info.CustomerSalesInfo.CustomerProductsReturned
{
    partial class form_cus_returns_report
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReporProcedureBillWiseCounterReturnsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerReturns_Ds = new Spices_pos.CounterSalesInfo.CustomerProductsReturned.CustomerReturns_Ds();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReporProcedureBillWiseCounterReturnsTableAdapter = new Spices_pos.CounterSalesInfo.CustomerProductsReturned.CustomerReturns_DsTableAdapters.ReporProcedureBillWiseCounterReturnsTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.CounterSalesInfo.CustomerProductsReturned.CustomerReturns_DsTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReporProcedureBillWiseCounterReturnsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturns_Ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReporProcedureBillWiseCounterReturnsBindingSource
            // 
            this.ReporProcedureBillWiseCounterReturnsBindingSource.DataMember = "ReporProcedureBillWiseCounterReturns";
            this.ReporProcedureBillWiseCounterReturnsBindingSource.DataSource = this.CustomerReturns_Ds;
            // 
            // CustomerReturns_Ds
            // 
            this.CustomerReturns_Ds.DataSetName = "CustomerReturns_Ds";
            this.CustomerReturns_Ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.CustomerReturns_Ds;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "cus_sales";
            reportDataSource1.Value = this.ReporProcedureBillWiseCounterReturnsBindingSource;
            reportDataSource2.Name = "reportTitles";
            reportDataSource2.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CounterSales_info.CustomerProductsReturned.cus_returns_report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 32);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1024, 704);
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
            this.Closebutton.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold);
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
            reportDataSource3.Name = "loyalCusSales";
            reportDataSource3.Value = this.ReporProcedureBillWiseCounterReturnsBindingSource;
            reportDataSource4.Name = "reportTitles";
            reportDataSource4.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "CounterSales_info.CustomerProductsReturned.cus_returns_large_report.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(438, 66);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(415, 608);
            this.reportViewer2.TabIndex = 14;
            // 
            // ReporProcedureBillWiseCounterReturnsTableAdapter
            // 
            this.ReporProcedureBillWiseCounterReturnsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // form_cus_returns_report
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form_cus_returns_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_cus_returns_report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_cus_returns_report_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_cus_returns_report_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ReporProcedureBillWiseCounterReturnsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturns_Ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource ReporProcedureBillWiseCounterReturnsBindingSource;
        private CustomerReturns_Ds CustomerReturns_Ds;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private Spices_pos.CounterSalesInfo.CustomerProductsReturned.CustomerReturns_DsTableAdapters.ReporProcedureBillWiseCounterReturnsTableAdapter ReporProcedureBillWiseCounterReturnsTableAdapter;
        private Spices_pos.CounterSalesInfo.CustomerProductsReturned.CustomerReturns_DsTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
    }
}