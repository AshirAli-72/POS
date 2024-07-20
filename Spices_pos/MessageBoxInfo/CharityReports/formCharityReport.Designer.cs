using Spices_pos.MessageBoxInfo.CharityReports;

namespace Message_box_info.CharityReports
{
    partial class formCharityReport
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
            this.ReportProcedureCharityDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.charityPayments_ds = new charityPayments_ds();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.view_button = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportProcedureCharityDetailsTableAdapter = new Spices_pos.MessageBoxInfo.CharityReports.charityPayments_dsTableAdapters.ReportProcedureCharityDetailsTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.MessageBoxInfo.CharityReports.charityPayments_dsTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureCharityDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.charityPayments_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportProcedureCharityDetailsBindingSource
            // 
            this.ReportProcedureCharityDetailsBindingSource.DataMember = "ReportProcedureCharityDetails";
            this.ReportProcedureCharityDetailsBindingSource.DataSource = this.charityPayments_ds;
            // 
            // charityPayments_ds
            // 
            this.charityPayments_ds.DataSetName = "charityPayments_ds";
            this.charityPayments_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.charityPayments_ds;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ToDate);
            this.panel5.Controls.Add(this.FromDate);
            this.panel5.Controls.Add(this.lblToDate);
            this.panel5.Controls.Add(this.lblFromDate);
            this.panel5.Controls.Add(this.view_button);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1024, 61);
            this.panel5.TabIndex = 35;
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(400, 10);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(254, 24);
            this.ToDate.TabIndex = 86;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(61, 10);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(255, 24);
            this.FromDate.TabIndex = 85;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lblToDate.Location = new System.Drawing.Point(347, 14);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(23, 17);
            this.lblToDate.TabIndex = 87;
            this.lblToDate.Text = "To";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lblFromDate.Location = new System.Drawing.Point(10, 14);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(46, 17);
            this.lblFromDate.TabIndex = 88;
            this.lblFromDate.Text = "From:";
            // 
            // view_button
            // 
            this.view_button.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.view_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.FlatAppearance.BorderSize = 2;
            this.view_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view_button.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.Location = new System.Drawing.Point(778, 10);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(83, 46);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "Go";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 32);
            this.panel3.TabIndex = 34;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(116)))), ((int)(((byte)(163)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.button1.Location = new System.Drawing.Point(979, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 32);
            this.button1.TabIndex = 15;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1172, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet3";
            reportDataSource1.Value = this.ReportProcedureCharityDetailsBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Message_box_info.CharityReports.charityDetailReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 93);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1024, 643);
            this.reportViewer1.TabIndex = 36;
            // 
            // ReportProcedureCharityDetailsTableAdapter
            // 
            this.ReportProcedureCharityDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // formCharityReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formCharityReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formCharityReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formCharityReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureCharityDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.charityPayments_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReportProcedureCharityDetailsBindingSource;
        private charityPayments_ds charityPayments_ds;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private Spices_pos.MessageBoxInfo.CharityReports.charityPayments_dsTableAdapters.ReportProcedureCharityDetailsTableAdapter ReportProcedureCharityDetailsTableAdapter;
        private Spices_pos.MessageBoxInfo.CharityReports.charityPayments_dsTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
    }
}