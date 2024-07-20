using Spices_pos.InvestorsInfo.PaybookReceipts;

namespace Investors_info.PaybookReceipts
{
    partial class formInvestorPaybookReceipt
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
            this.ReportProcedureInvestorPaybookReceiptBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.investorPaybookDS = new Spices_pos.InvestorsInfo.PaybookReceipts.investorPaybookDS();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ReportProcedureInvestorPaybookReceiptTableAdapter = new Spices_pos.InvestorsInfo.PaybookReceipts.investorPaybookDSTableAdapters.ReportProcedureInvestorPaybookReceiptTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.InvestorsInfo.PaybookReceipts.investorPaybookDSTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureInvestorPaybookReceiptBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.investorPaybookDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportProcedureInvestorPaybookReceiptBindingSource
            // 
            this.ReportProcedureInvestorPaybookReceiptBindingSource.DataMember = "ReportProcedureInvestorPaybookReceipt";
            this.ReportProcedureInvestorPaybookReceiptBindingSource.DataSource = this.investorPaybookDS;
            // 
            // investorPaybookDS
            // 
            this.investorPaybookDS.DataSetName = "investorPaybookDS";
            this.investorPaybookDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.investorPaybookDS;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "recovery";
            reportDataSource1.Value = this.ReportProcedureInvestorPaybookReceiptBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Spices_pos.InvestorsInfo.PaybookReceipts.reportInvestorPaybookReceipt.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 32);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1024, 704);
            this.reportViewer1.TabIndex = 13;
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
            this.panel1.TabIndex = 12;
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
            this.Closebutton.Font = new System.Drawing.Font("Century Gothic", 17F, System.Drawing.FontStyle.Bold);
            this.Closebutton.ForeColor = System.Drawing.Color.White;
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
            // ReportProcedureInvestorPaybookReceiptTableAdapter
            // 
            this.ReportProcedureInvestorPaybookReceiptTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // formInvestorPaybookReceipt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formInvestorPaybookReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formInvestorPaybookReceipt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formInvestorPaybookReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureInvestorPaybookReceiptBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.investorPaybookDS)).EndInit();
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
        private System.Windows.Forms.BindingSource ReportProcedureInvestorPaybookReceiptBindingSource;
        private investorPaybookDS investorPaybookDS;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private Spices_pos.InvestorsInfo.PaybookReceipts.investorPaybookDSTableAdapters.ReportProcedureInvestorPaybookReceiptTableAdapter ReportProcedureInvestorPaybookReceiptTableAdapter;
        private Spices_pos.InvestorsInfo.PaybookReceipts.investorPaybookDSTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
    }
}