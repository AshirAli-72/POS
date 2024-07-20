using Spices_pos.MessageBoxInfo.LoanReports;

namespace Message_box_info.LoanReports
{
    partial class formLoanDetailsReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLoanDetailsReport));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ProcedureStatusWiseLoanDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loadReport_ds = new loadReport_ds();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProcedureDateWiseLoanDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.ComboBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.view_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh_button = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.btnStatusWise = new System.Windows.Forms.RadioButton();
            this.btnDateWise = new System.Windows.Forms.RadioButton();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlStatusWise = new System.Windows.Forms.Panel();
            this.viewerStatusWise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlDateWise = new System.Windows.Forms.Panel();
            this.viewerDateWise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ProcedureDateWiseLoanDetailsTableAdapter = new Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ProcedureDateWiseLoanDetailsTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            this.ProcedureStatusWiseLoanDetailsTableAdapter = new Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ProcedureStatusWiseLoanDetailsTableAdapter();
            this.btnName = new System.Windows.Forms.RadioButton();
            this.pnlNameWise = new System.Windows.Forms.Panel();
            this.viewerNameWise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportProcedureNameWiseLoanDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReportProcedureNameWiseLoanDetailsTableAdapter = new Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ReportProcedureNameWiseLoanDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ProcedureStatusWiseLoanDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadReport_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcedureDateWiseLoanDetailsBindingSource)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlStatusWise.SuspendLayout();
            this.pnlDateWise.SuspendLayout();
            this.pnlNameWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureNameWiseLoanDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcedureStatusWiseLoanDetailsBindingSource
            // 
            this.ProcedureStatusWiseLoanDetailsBindingSource.DataMember = "ProcedureStatusWiseLoanDetails";
            this.ProcedureStatusWiseLoanDetailsBindingSource.DataSource = this.loadReport_ds;
            // 
            // loadReport_ds
            // 
            this.loadReport_ds.DataSetName = "loadReport_ds";
            this.loadReport_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.loadReport_ds;
            // 
            // ProcedureDateWiseLoanDetailsBindingSource
            // 
            this.ProcedureDateWiseLoanDetailsBindingSource.DataMember = "ProcedureDateWiseLoanDetails";
            this.ProcedureDateWiseLoanDetailsBindingSource.DataSource = this.loadReport_ds;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblStatus);
            this.panel5.Controls.Add(this.txtStatus);
            this.panel5.Controls.Add(this.ToDate);
            this.panel5.Controls.Add(this.FromDate);
            this.panel5.Controls.Add(this.lbl_to_date);
            this.panel5.Controls.Add(this.lbl_from_date);
            this.panel5.Controls.Add(this.view_button);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel5.Location = new System.Drawing.Point(0, 33);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1024, 70);
            this.panel5.TabIndex = 29;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lblStatus.Location = new System.Drawing.Point(19, 43);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 17);
            this.lblStatus.TabIndex = 85;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtStatus.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txtStatus.ForeColor = System.Drawing.Color.Black;
            this.txtStatus.FormattingEnabled = true;
            this.txtStatus.Items.AddRange(new object[] {
            "Payment",
            "Received"});
            this.txtStatus.Location = new System.Drawing.Point(74, 40);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(278, 25);
            this.txtStatus.TabIndex = 87;
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(433, 7);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(277, 24);
            this.ToDate.TabIndex = 58;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(74, 7);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(278, 24);
            this.FromDate.TabIndex = 58;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_to_date.Location = new System.Drawing.Point(382, 11);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(23, 17);
            this.lbl_to_date.TabIndex = 57;
            this.lbl_to_date.Text = "To";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_from_date.Location = new System.Drawing.Point(23, 11);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(46, 17);
            this.lbl_from_date.TabIndex = 57;
            this.lbl_from_date.Text = "From:";
            // 
            // view_button
            // 
            this.view_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.FlatAppearance.BorderSize = 2;
            this.view_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view_button.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.Location = new System.Drawing.Point(799, 6);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(104, 60);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "Go";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.btnName);
            this.panel1.Controls.Add(this.refresh_button);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.btnStatusWise);
            this.panel1.Controls.Add(this.btnDateWise);
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 33);
            this.panel1.TabIndex = 28;
            // 
            // refresh_button
            // 
            this.refresh_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refresh_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.refresh_button.FlatAppearance.BorderSize = 0;
            this.refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.refresh_button.ForeColor = System.Drawing.Color.White;
            this.refresh_button.Image = ((System.Drawing.Image)(resources.GetObject("refresh_button.Image")));
            this.refresh_button.Location = new System.Drawing.Point(878, 0);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(48, 33);
            this.refresh_button.TabIndex = 105;
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // button9
            // 
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.Dock = System.Windows.Forms.DockStyle.Right;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.Location = new System.Drawing.Point(926, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(49, 33);
            this.button9.TabIndex = 104;
            this.button9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnStatusWise
            // 
            this.btnStatusWise.AutoSize = true;
            this.btnStatusWise.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnStatusWise.ForeColor = System.Drawing.Color.White;
            this.btnStatusWise.Location = new System.Drawing.Point(260, 7);
            this.btnStatusWise.Name = "btnStatusWise";
            this.btnStatusWise.Size = new System.Drawing.Size(97, 20);
            this.btnStatusWise.TabIndex = 102;
            this.btnStatusWise.Text = "STATUS WISE";
            this.btnStatusWise.UseVisualStyleBackColor = true;
            this.btnStatusWise.CheckedChanged += new System.EventHandler(this.btnStatusWise_CheckedChanged);
            // 
            // btnDateWise
            // 
            this.btnDateWise.AutoSize = true;
            this.btnDateWise.Checked = true;
            this.btnDateWise.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnDateWise.ForeColor = System.Drawing.Color.White;
            this.btnDateWise.Location = new System.Drawing.Point(16, 7);
            this.btnDateWise.Name = "btnDateWise";
            this.btnDateWise.Size = new System.Drawing.Size(85, 20);
            this.btnDateWise.TabIndex = 101;
            this.btnDateWise.TabStop = true;
            this.btnDateWise.Text = "DATE WISE";
            this.btnDateWise.UseVisualStyleBackColor = true;
            this.btnDateWise.CheckedChanged += new System.EventHandler(this.btnDateWise_CheckedChanged);
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
            this.Closebutton.Size = new System.Drawing.Size(49, 33);
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
            // pnlStatusWise
            // 
            this.pnlStatusWise.Controls.Add(this.viewerStatusWise);
            this.pnlStatusWise.Location = new System.Drawing.Point(385, 194);
            this.pnlStatusWise.Name = "pnlStatusWise";
            this.pnlStatusWise.Size = new System.Drawing.Size(300, 429);
            this.pnlStatusWise.TabIndex = 31;
            this.pnlStatusWise.Visible = false;
            // 
            // viewerStatusWise
            // 
            reportDataSource1.Name = "loyalCusSales";
            reportDataSource1.Value = this.ProcedureStatusWiseLoanDetailsBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerStatusWise.LocalReport.DataSources.Add(reportDataSource1);
            this.viewerStatusWise.LocalReport.DataSources.Add(reportDataSource2);
            this.viewerStatusWise.LocalReport.ReportEmbeddedResource = "Message_box_info.LoanReports.Over_all.loanStatusWiseReport.rdlc";
            this.viewerStatusWise.Location = new System.Drawing.Point(0, 0);
            this.viewerStatusWise.Name = "viewerStatusWise";
            this.viewerStatusWise.Size = new System.Drawing.Size(266, 246);
            this.viewerStatusWise.TabIndex = 0;
            // 
            // pnlDateWise
            // 
            this.pnlDateWise.Controls.Add(this.viewerDateWise);
            this.pnlDateWise.Location = new System.Drawing.Point(26, 164);
            this.pnlDateWise.Name = "pnlDateWise";
            this.pnlDateWise.Size = new System.Drawing.Size(343, 629);
            this.pnlDateWise.TabIndex = 30;
            // 
            // viewerDateWise
            // 
            this.viewerDateWise.DocumentMapWidth = 86;
            reportDataSource3.Name = "loyalCusSales";
            reportDataSource3.Value = this.ProcedureDateWiseLoanDetailsBindingSource;
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerDateWise.LocalReport.DataSources.Add(reportDataSource3);
            this.viewerDateWise.LocalReport.DataSources.Add(reportDataSource4);
            this.viewerDateWise.LocalReport.ReportEmbeddedResource = "Message_box_info.LoanReports.date_wise.loanDateWiseReport.rdlc";
            this.viewerDateWise.Location = new System.Drawing.Point(0, 0);
            this.viewerDateWise.Name = "viewerDateWise";
            this.viewerDateWise.Size = new System.Drawing.Size(284, 629);
            this.viewerDateWise.TabIndex = 0;
            // 
            // ProcedureDateWiseLoanDetailsTableAdapter
            // 
            this.ProcedureDateWiseLoanDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // ProcedureStatusWiseLoanDetailsTableAdapter
            // 
            this.ProcedureStatusWiseLoanDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // btnName
            // 
            this.btnName.AutoSize = true;
            this.btnName.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnName.ForeColor = System.Drawing.Color.White;
            this.btnName.Location = new System.Drawing.Point(135, 7);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(91, 20);
            this.btnName.TabIndex = 106;
            this.btnName.Text = "NAME WISE";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.CheckedChanged += new System.EventHandler(this.btnName_CheckedChanged);
            // 
            // pnlNameWise
            // 
            this.pnlNameWise.Controls.Add(this.viewerNameWise);
            this.pnlNameWise.Location = new System.Drawing.Point(712, 180);
            this.pnlNameWise.Name = "pnlNameWise";
            this.pnlNameWise.Size = new System.Drawing.Size(300, 429);
            this.pnlNameWise.TabIndex = 32;
            this.pnlNameWise.Visible = false;
            // 
            // viewerNameWise
            // 
            reportDataSource5.Name = "loyalCusSales";
            reportDataSource5.Value = this.ReportProcedureNameWiseLoanDetailsBindingSource;
            reportDataSource6.Name = "DataSet1";
            reportDataSource6.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerNameWise.LocalReport.DataSources.Add(reportDataSource5);
            this.viewerNameWise.LocalReport.DataSources.Add(reportDataSource6);
            this.viewerNameWise.LocalReport.ReportEmbeddedResource = "Message_box_info.LoanReports.NameWise.name_wise_report.rdlc";
            this.viewerNameWise.Location = new System.Drawing.Point(0, 0);
            this.viewerNameWise.Name = "viewerNameWise";
            this.viewerNameWise.Size = new System.Drawing.Size(266, 246);
            this.viewerNameWise.TabIndex = 0;
            // 
            // ReportProcedureNameWiseLoanDetailsBindingSource
            // 
            this.ReportProcedureNameWiseLoanDetailsBindingSource.DataMember = "ReportProcedureNameWiseLoanDetails";
            this.ReportProcedureNameWiseLoanDetailsBindingSource.DataSource = this.loadReport_ds;
            // 
            // ReportProcedureNameWiseLoanDetailsTableAdapter
            // 
            this.ReportProcedureNameWiseLoanDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // formLoanDetailsReport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.pnlNameWise);
            this.Controls.Add(this.pnlStatusWise);
            this.Controls.Add(this.pnlDateWise);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formLoanDetailsReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formLoanDetailsReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formLoanDetailsReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProcedureStatusWiseLoanDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadReport_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProcedureDateWiseLoanDetailsBindingSource)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlStatusWise.ResumeLayout(false);
            this.pnlDateWise.ResumeLayout(false);
            this.pnlNameWise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureNameWiseLoanDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox txtStatus;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.RadioButton btnStatusWise;
        private System.Windows.Forms.RadioButton btnDateWise;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlStatusWise;
        private Microsoft.Reporting.WinForms.ReportViewer viewerStatusWise;
        private System.Windows.Forms.Panel pnlDateWise;
        private Microsoft.Reporting.WinForms.ReportViewer viewerDateWise;
        private System.Windows.Forms.BindingSource ProcedureDateWiseLoanDetailsBindingSource;
        private loadReport_ds loadReport_ds;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ProcedureDateWiseLoanDetailsTableAdapter ProcedureDateWiseLoanDetailsTableAdapter;
        private Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
        private System.Windows.Forms.BindingSource ProcedureStatusWiseLoanDetailsBindingSource;
        private Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ProcedureStatusWiseLoanDetailsTableAdapter ProcedureStatusWiseLoanDetailsTableAdapter;
        private System.Windows.Forms.RadioButton btnName;
        private System.Windows.Forms.Panel pnlNameWise;
        private Microsoft.Reporting.WinForms.ReportViewer viewerNameWise;
        private System.Windows.Forms.BindingSource ReportProcedureNameWiseLoanDetailsBindingSource;
        private Spices_pos.MessageBoxInfo.LoanReports.loadReport_dsTableAdapters.ReportProcedureNameWiseLoanDetailsTableAdapter ReportProcedureNameWiseLoanDetailsTableAdapter;
    }
}