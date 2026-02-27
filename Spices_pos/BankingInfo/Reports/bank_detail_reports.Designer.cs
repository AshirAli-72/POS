using Spices_pos.BankingInfo.Reports;

namespace Banking_info.Reports
{
    partial class bank_detail_reports
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bank_detail_reports));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.banking_ds = new Spices_pos.BankingInfo.Reports.banking_ds();
            this.lbl_title = new System.Windows.Forms.Label();
            this.txt_title = new System.Windows.Forms.ComboBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_employee = new System.Windows.Forms.Panel();
            this.viewer_employee = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_account_wise = new System.Windows.Forms.Panel();
            this.viewer_accounts = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_overall = new System.Windows.Forms.Panel();
            this.viewer_overall = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_branch_wise = new System.Windows.Forms.Panel();
            this.viewer_branch = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_status_wise = new System.Windows.Forms.Panel();
            this.viewer_status = new Microsoft.Reporting.WinForms.ReportViewer();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.view_button = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.btn_refresh = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button5 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.sidePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_overall = new Guna.UI2.WinForms.Guna2Button();
            this.btn_employee = new Guna.UI2.WinForms.Guna2Button();
            this.btn_status = new Guna.UI2.WinForms.Guna2Button();
            this.btn_account = new Guna.UI2.WinForms.Guna2Button();
            this.btn_branch = new Guna.UI2.WinForms.Guna2Button();
            this.btn_bank = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnl_bank_wise = new System.Windows.Forms.Panel();
            this.viewer_bank = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.banking_ds)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnl_employee.SuspendLayout();
            this.pnl_account_wise.SuspendLayout();
            this.pnl_overall.SuspendLayout();
            this.pnl_branch_wise.SuspendLayout();
            this.pnl_status_wise.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).BeginInit();
            this.pnl_bank_wise.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.banking_ds;
            // 
            // banking_ds
            // 
            this.banking_ds.DataSetName = "banking_ds";
            this.banking_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_title.Location = new System.Drawing.Point(14, 49);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(65, 16);
            this.lbl_title.TabIndex = 109;
            this.lbl_title.Text = "Bank Title:";
            // 
            // txt_title
            // 
            this.txt_title.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_title.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_title.BackColor = System.Drawing.SystemColors.Window;
            this.txt_title.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txt_title.ForeColor = System.Drawing.Color.Black;
            this.txt_title.FormattingEnabled = true;
            this.txt_title.IntegralHeight = false;
            this.txt_title.Location = new System.Drawing.Point(85, 45);
            this.txt_title.MaxLength = 14;
            this.txt_title.Name = "txt_title";
            this.txt_title.Size = new System.Drawing.Size(579, 25);
            this.txt_title.TabIndex = 110;
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(409, 8);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(255, 24);
            this.ToDate.TabIndex = 107;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(85, 8);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(256, 24);
            this.FromDate.TabIndex = 108;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_to_date.Location = new System.Drawing.Point(379, 12);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(24, 16);
            this.lbl_to_date.TabIndex = 105;
            this.lbl_to_date.Text = "To:";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_from_date.Location = new System.Drawing.Point(40, 11);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(39, 16);
            this.lbl_from_date.TabIndex = 106;
            this.lbl_from_date.Text = "From:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnl_bank_wise);
            this.panel2.Controls.Add(this.pnl_employee);
            this.panel2.Controls.Add(this.pnl_account_wise);
            this.panel2.Controls.Add(this.pnl_overall);
            this.panel2.Controls.Add(this.pnl_branch_wise);
            this.panel2.Controls.Add(this.pnl_status_wise);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(847, 590);
            this.panel2.TabIndex = 31;
            // 
            // pnl_employee
            // 
            this.pnl_employee.Controls.Add(this.viewer_employee);
            this.pnl_employee.Location = new System.Drawing.Point(43, 284);
            this.pnl_employee.Name = "pnl_employee";
            this.pnl_employee.Size = new System.Drawing.Size(208, 212);
            this.pnl_employee.TabIndex = 0;
            // 
            // viewer_employee
            // 
            this.viewer_employee.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource7.Name = "new_purchase";
            reportDataSource7.Value = this.DataTable1BindingSource;
            this.viewer_employee.LocalReport.DataSources.Add(reportDataSource7);
            this.viewer_employee.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.employee_wise_report.rdlc";
            this.viewer_employee.Location = new System.Drawing.Point(0, 0);
            this.viewer_employee.Name = "viewer_employee";
            this.viewer_employee.ServerReport.BearerToken = null;
            this.viewer_employee.Size = new System.Drawing.Size(208, 212);
            this.viewer_employee.TabIndex = 0;
            // 
            // pnl_account_wise
            // 
            this.pnl_account_wise.Controls.Add(this.viewer_accounts);
            this.pnl_account_wise.Location = new System.Drawing.Point(309, 284);
            this.pnl_account_wise.Name = "pnl_account_wise";
            this.pnl_account_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_account_wise.TabIndex = 0;
            // 
            // viewer_accounts
            // 
            this.viewer_accounts.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource8.Name = "new_purchase";
            reportDataSource8.Value = this.DataTable1BindingSource;
            this.viewer_accounts.LocalReport.DataSources.Add(reportDataSource8);
            this.viewer_accounts.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.accountNo_wise_report.rdlc";
            this.viewer_accounts.Location = new System.Drawing.Point(0, 0);
            this.viewer_accounts.Name = "viewer_accounts";
            this.viewer_accounts.ServerReport.BearerToken = null;
            this.viewer_accounts.Size = new System.Drawing.Size(208, 212);
            this.viewer_accounts.TabIndex = 0;
            // 
            // pnl_overall
            // 
            this.pnl_overall.Controls.Add(this.viewer_overall);
            this.pnl_overall.Location = new System.Drawing.Point(579, 284);
            this.pnl_overall.Name = "pnl_overall";
            this.pnl_overall.Size = new System.Drawing.Size(208, 212);
            this.pnl_overall.TabIndex = 0;
            // 
            // viewer_overall
            // 
            this.viewer_overall.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource9.Name = "new_purchase";
            reportDataSource9.Value = this.DataTable1BindingSource;
            this.viewer_overall.LocalReport.DataSources.Add(reportDataSource9);
            this.viewer_overall.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.overall_report.rdlc";
            this.viewer_overall.Location = new System.Drawing.Point(0, 0);
            this.viewer_overall.Name = "viewer_overall";
            this.viewer_overall.ServerReport.BearerToken = null;
            this.viewer_overall.Size = new System.Drawing.Size(208, 212);
            this.viewer_overall.TabIndex = 0;
            // 
            // pnl_branch_wise
            // 
            this.pnl_branch_wise.Controls.Add(this.viewer_branch);
            this.pnl_branch_wise.Location = new System.Drawing.Point(309, 29);
            this.pnl_branch_wise.Name = "pnl_branch_wise";
            this.pnl_branch_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_branch_wise.TabIndex = 0;
            // 
            // viewer_branch
            // 
            this.viewer_branch.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource10.Name = "new_purchase";
            reportDataSource10.Value = this.DataTable1BindingSource;
            this.viewer_branch.LocalReport.DataSources.Add(reportDataSource10);
            this.viewer_branch.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.branch_wise_report.rdlc";
            this.viewer_branch.Location = new System.Drawing.Point(0, 0);
            this.viewer_branch.Name = "viewer_branch";
            this.viewer_branch.ServerReport.BearerToken = null;
            this.viewer_branch.Size = new System.Drawing.Size(208, 212);
            this.viewer_branch.TabIndex = 0;
            // 
            // pnl_status_wise
            // 
            this.pnl_status_wise.Controls.Add(this.viewer_status);
            this.pnl_status_wise.Location = new System.Drawing.Point(579, 29);
            this.pnl_status_wise.Name = "pnl_status_wise";
            this.pnl_status_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_status_wise.TabIndex = 0;
            // 
            // viewer_status
            // 
            this.viewer_status.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource11.Name = "new_purchase";
            reportDataSource11.Value = this.DataTable1BindingSource;
            this.viewer_status.LocalReport.DataSources.Add(reportDataSource11);
            this.viewer_status.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.status_wise_report.rdlc";
            this.viewer_status.Location = new System.Drawing.Point(0, 0);
            this.viewer_status.Name = "viewer_status";
            this.viewer_status.ServerReport.BearerToken = null;
            this.viewer_status.Size = new System.Drawing.Size(208, 212);
            this.viewer_status.TabIndex = 0;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.White;
            this.guna2Panel2.BorderRadius = 30;
            this.guna2Panel2.Controls.Add(this.guna2Panel3);
            this.guna2Panel2.Controls.Add(this.sidePanel);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.FillColor = System.Drawing.Color.White;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1024, 765);
            this.guna2Panel2.TabIndex = 108;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 30;
            this.guna2Panel3.Controls.Add(this.guna2Panel10);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(127, 0);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Padding = new System.Windows.Forms.Padding(10);
            this.guna2Panel3.Size = new System.Drawing.Size(897, 765);
            this.guna2Panel3.TabIndex = 1;
            // 
            // guna2Panel10
            // 
            this.guna2Panel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel10.BorderRadius = 30;
            this.guna2Panel10.Controls.Add(this.guna2GradientPanel1);
            this.guna2Panel10.Controls.Add(this.panel10);
            this.guna2Panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel10.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel10.Location = new System.Drawing.Point(10, 10);
            this.guna2Panel10.Name = "guna2Panel10";
            this.guna2Panel10.Padding = new System.Windows.Forms.Padding(10);
            this.guna2Panel10.Size = new System.Drawing.Size(877, 745);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel6);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 54);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(857, 681);
            this.guna2GradientPanel1.TabIndex = 137;
            // 
            // guna2Panel6
            // 
            this.guna2Panel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel6.BorderRadius = 20;
            this.guna2Panel6.Controls.Add(this.panel2);
            this.guna2Panel6.Controls.Add(this.panel7);
            this.guna2Panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel6.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel6.Margin = new System.Windows.Forms.Padding(10);
            this.guna2Panel6.Name = "guna2Panel6";
            this.guna2Panel6.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel6.Size = new System.Drawing.Size(857, 681);
            this.guna2Panel6.TabIndex = 141;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.lbl_title);
            this.panel7.Controls.Add(this.view_button);
            this.panel7.Controls.Add(this.txt_title);
            this.panel7.Controls.Add(this.ToDate);
            this.panel7.Controls.Add(this.guna2Separator2);
            this.panel7.Controls.Add(this.FromDate);
            this.panel7.Controls.Add(this.lbl_from_date);
            this.panel7.Controls.Add(this.lbl_to_date);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel7.Location = new System.Drawing.Point(5, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(847, 81);
            this.panel7.TabIndex = 13;
            // 
            // view_button
            // 
            this.view_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.view_button.BackColor = System.Drawing.Color.Transparent;
            this.view_button.BorderColor = System.Drawing.Color.Transparent;
            this.view_button.BorderRadius = 5;
            this.view_button.BorderThickness = 1;
            this.view_button.DialogResult = System.Windows.Forms.DialogResult.No;
            this.view_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.view_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.view_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.view_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.view_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(190)))));
            this.view_button.Font = new System.Drawing.Font("Century Gothic", 7.5F, System.Drawing.FontStyle.Bold);
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.ImageOffset = new System.Drawing.Point(5, 0);
            this.view_button.ImageSize = new System.Drawing.Size(16, 16);
            this.view_button.Location = new System.Drawing.Point(757, 33);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(83, 37);
            this.view_button.TabIndex = 539;
            this.view_button.Text = "Search";
            this.view_button.TextTransform = Guna.UI2.WinForms.Enums.TextTransform.UpperCase;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Separator2.FillColor = System.Drawing.Color.LightGray;
            this.guna2Separator2.Location = new System.Drawing.Point(0, 71);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(847, 10);
            this.guna2Separator2.TabIndex = 538;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Controls.Add(this.lblReportTitle);
            this.panel10.Controls.Add(this.guna2Separator1);
            this.panel10.Controls.Add(this.btn_refresh);
            this.panel10.Controls.Add(this.guna2Button5);
            this.panel10.Controls.Add(this.guna2Button1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel10.Location = new System.Drawing.Point(10, 10);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(857, 44);
            this.panel10.TabIndex = 2;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblReportTitle.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold);
            this.lblReportTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblReportTitle.Location = new System.Drawing.Point(4, 6);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(166, 23);
            this.lblReportTitle.TabIndex = 539;
            this.lblReportTitle.Text = "Date Wise Report";
            this.lblReportTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Separator1.FillColor = System.Drawing.Color.LightGray;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 34);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(856, 10);
            this.guna2Separator1.TabIndex = 538;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.BorderColor = System.Drawing.Color.Gray;
            this.btn_refresh.BorderRadius = 7;
            this.btn_refresh.BorderThickness = 1;
            this.btn_refresh.CustomBorderColor = System.Drawing.Color.Gray;
            this.btn_refresh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_refresh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_refresh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_refresh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_refresh.FillColor = System.Drawing.Color.Transparent;
            this.btn_refresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_refresh.ForeColor = System.Drawing.Color.White;
            this.btn_refresh.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_refresh.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_refresh.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.btn_refresh.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btn_refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_refresh.Image")));
            this.btn_refresh.ImageSize = new System.Drawing.Size(15, 15);
            this.btn_refresh.Location = new System.Drawing.Point(771, 1);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(38, 33);
            this.btn_refresh.TabIndex = 531;
            this.btn_refresh.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // guna2Button5
            // 
            this.guna2Button5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(115)))), ((int)(((byte)(155)))));
            this.guna2Button5.BorderRadius = 2;
            this.guna2Button5.BorderThickness = 1;
            this.guna2Button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button5.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button5.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button5.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button5.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Button5.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button5.ForeColor = System.Drawing.Color.White;
            this.guna2Button5.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button5.Image")));
            this.guna2Button5.ImageOffset = new System.Drawing.Point(0, 3);
            this.guna2Button5.ImageSize = new System.Drawing.Size(25, 28);
            this.guna2Button5.Location = new System.Drawing.Point(856, 0);
            this.guna2Button5.Name = "guna2Button5";
            this.guna2Button5.Size = new System.Drawing.Size(1, 44);
            this.guna2Button5.TabIndex = 508;
            this.guna2Button5.Visible = false;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BorderColor = System.Drawing.Color.Gray;
            this.guna2Button1.BorderRadius = 7;
            this.guna2Button1.BorderThickness = 1;
            this.guna2Button1.CustomBorderColor = System.Drawing.Color.Gray;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.BorderColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.guna2Button1.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageSize = new System.Drawing.Size(15, 15);
            this.guna2Button1.Location = new System.Drawing.Point(814, 1);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(38, 33);
            this.guna2Button1.TabIndex = 522;
            this.guna2Button1.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.Transparent;
            this.sidePanel.Controls.Add(this.btn_overall);
            this.sidePanel.Controls.Add(this.btn_employee);
            this.sidePanel.Controls.Add(this.btn_status);
            this.sidePanel.Controls.Add(this.btn_account);
            this.sidePanel.Controls.Add(this.btn_branch);
            this.sidePanel.Controls.Add(this.btn_bank);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(127, 765);
            this.sidePanel.TabIndex = 2;
            // 
            // btn_overall
            // 
            this.btn_overall.BackColor = System.Drawing.Color.Transparent;
            this.btn_overall.BorderColor = System.Drawing.Color.Transparent;
            this.btn_overall.BorderRadius = 15;
            this.btn_overall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_overall.CustomBorderColor = System.Drawing.Color.White;
            this.btn_overall.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_overall.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_overall.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_overall.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_overall.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_overall.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_overall.FillColor = System.Drawing.Color.White;
            this.btn_overall.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_overall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_overall.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_overall.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_overall.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_overall.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_overall.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_overall.Location = new System.Drawing.Point(10, 326);
            this.btn_overall.Name = "btn_overall";
            this.btn_overall.Size = new System.Drawing.Size(107, 39);
            this.btn_overall.TabIndex = 536;
            this.btn_overall.Text = "Overall";
            this.btn_overall.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_overall.Click += new System.EventHandler(this.btn_overall_CheckedChanged);
            // 
            // btn_employee
            // 
            this.btn_employee.BackColor = System.Drawing.Color.Transparent;
            this.btn_employee.BorderColor = System.Drawing.Color.Transparent;
            this.btn_employee.BorderRadius = 15;
            this.btn_employee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_employee.CustomBorderColor = System.Drawing.Color.White;
            this.btn_employee.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_employee.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_employee.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_employee.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_employee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_employee.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_employee.FillColor = System.Drawing.Color.White;
            this.btn_employee.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_employee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_employee.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_employee.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_employee.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_employee.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_employee.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_employee.Location = new System.Drawing.Point(10, 287);
            this.btn_employee.Name = "btn_employee";
            this.btn_employee.Size = new System.Drawing.Size(107, 39);
            this.btn_employee.TabIndex = 535;
            this.btn_employee.Text = "Employee Wise";
            this.btn_employee.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_employee.Click += new System.EventHandler(this.btn_employee_CheckedChanged);
            // 
            // btn_status
            // 
            this.btn_status.BackColor = System.Drawing.Color.Transparent;
            this.btn_status.BorderColor = System.Drawing.Color.Transparent;
            this.btn_status.BorderRadius = 15;
            this.btn_status.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_status.CustomBorderColor = System.Drawing.Color.White;
            this.btn_status.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_status.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_status.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_status.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_status.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_status.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_status.FillColor = System.Drawing.Color.White;
            this.btn_status.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_status.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_status.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_status.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_status.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_status.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_status.Location = new System.Drawing.Point(10, 248);
            this.btn_status.Name = "btn_status";
            this.btn_status.Size = new System.Drawing.Size(107, 39);
            this.btn_status.TabIndex = 530;
            this.btn_status.Text = "Status Wise";
            this.btn_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_status.Click += new System.EventHandler(this.btn_status_CheckedChanged);
            // 
            // btn_account
            // 
            this.btn_account.BackColor = System.Drawing.Color.Transparent;
            this.btn_account.BorderColor = System.Drawing.Color.Transparent;
            this.btn_account.BorderRadius = 15;
            this.btn_account.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_account.CustomBorderColor = System.Drawing.Color.White;
            this.btn_account.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_account.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_account.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_account.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_account.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_account.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_account.FillColor = System.Drawing.Color.White;
            this.btn_account.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_account.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_account.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_account.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_account.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_account.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_account.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_account.Location = new System.Drawing.Point(10, 209);
            this.btn_account.Name = "btn_account";
            this.btn_account.Size = new System.Drawing.Size(107, 39);
            this.btn_account.TabIndex = 529;
            this.btn_account.Text = "Account Wise";
            this.btn_account.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_account.Click += new System.EventHandler(this.btn_account_CheckedChanged);
            // 
            // btn_branch
            // 
            this.btn_branch.BackColor = System.Drawing.Color.Transparent;
            this.btn_branch.BorderColor = System.Drawing.Color.Transparent;
            this.btn_branch.BorderRadius = 15;
            this.btn_branch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_branch.CustomBorderColor = System.Drawing.Color.White;
            this.btn_branch.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_branch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_branch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_branch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_branch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_branch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_branch.FillColor = System.Drawing.Color.White;
            this.btn_branch.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_branch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_branch.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_branch.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_branch.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_branch.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_branch.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_branch.Location = new System.Drawing.Point(10, 170);
            this.btn_branch.Name = "btn_branch";
            this.btn_branch.Size = new System.Drawing.Size(107, 39);
            this.btn_branch.TabIndex = 526;
            this.btn_branch.Text = "Branch Wise";
            this.btn_branch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_branch.Click += new System.EventHandler(this.btn_branch_CheckedChanged);
            // 
            // btn_bank
            // 
            this.btn_bank.BackColor = System.Drawing.Color.Transparent;
            this.btn_bank.BorderColor = System.Drawing.Color.Transparent;
            this.btn_bank.BorderRadius = 15;
            this.btn_bank.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_bank.CustomBorderColor = System.Drawing.Color.White;
            this.btn_bank.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_bank.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_bank.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_bank.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_bank.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_bank.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_bank.FillColor = System.Drawing.Color.White;
            this.btn_bank.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_bank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_bank.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_bank.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_bank.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_bank.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_bank.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_bank.Location = new System.Drawing.Point(10, 131);
            this.btn_bank.Name = "btn_bank";
            this.btn_bank.Size = new System.Drawing.Size(107, 39);
            this.btn_bank.TabIndex = 525;
            this.btn_bank.Text = "Bank Wise";
            this.btn_bank.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_bank.Click += new System.EventHandler(this.btn_bank_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.lbl_shop_title);
            this.panel8.Controls.Add(this.logo_img2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(107, 121);
            this.panel8.TabIndex = 534;
            // 
            // lbl_shop_title
            // 
            this.lbl_shop_title.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_shop_title.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_shop_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_shop_title.Location = new System.Drawing.Point(3, 67);
            this.lbl_shop_title.Name = "lbl_shop_title";
            this.lbl_shop_title.Size = new System.Drawing.Size(101, 36);
            this.lbl_shop_title.TabIndex = 9;
            this.lbl_shop_title.Text = "Banking Reports";
            this.lbl_shop_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logo_img2
            // 
            this.logo_img2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.logo_img2.BackColor = System.Drawing.Color.Transparent;
            this.logo_img2.ErrorImage = null;
            this.logo_img2.FillColor = System.Drawing.Color.Transparent;
            this.logo_img2.Image = ((System.Drawing.Image)(resources.GetObject("logo_img2.Image")));
            this.logo_img2.ImageRotate = 0F;
            this.logo_img2.InitialImage = null;
            this.logo_img2.Location = new System.Drawing.Point(33, 21);
            this.logo_img2.Name = "logo_img2";
            this.logo_img2.ShadowDecoration.BorderRadius = 0;
            this.logo_img2.Size = new System.Drawing.Size(40, 40);
            this.logo_img2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo_img2.TabIndex = 7;
            this.logo_img2.TabStop = false;
            this.logo_img2.UseTransparentBackground = true;
            // 
            // pnl_bank_wise
            // 
            this.pnl_bank_wise.Controls.Add(this.viewer_bank);
            this.pnl_bank_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_bank_wise.Location = new System.Drawing.Point(0, 0);
            this.pnl_bank_wise.Name = "pnl_bank_wise";
            this.pnl_bank_wise.Size = new System.Drawing.Size(847, 590);
            this.pnl_bank_wise.TabIndex = 0;
            // 
            // viewer_bank
            // 
            this.viewer_bank.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource6.Name = "new_purchase";
            reportDataSource6.Value = this.DataTable1BindingSource;
            this.viewer_bank.LocalReport.DataSources.Add(reportDataSource6);
            this.viewer_bank.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.bank_wise_report.rdlc";
            this.viewer_bank.Location = new System.Drawing.Point(0, 0);
            this.viewer_bank.Name = "viewer_bank";
            this.viewer_bank.ServerReport.BearerToken = null;
            this.viewer_bank.Size = new System.Drawing.Size(847, 590);
            this.viewer_bank.TabIndex = 0;
            // 
            // bank_detail_reports
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 765);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "bank_detail_reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "bank_detail_reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.bank_detail_reports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.banking_ds)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnl_employee.ResumeLayout(false);
            this.pnl_account_wise.ResumeLayout(false);
            this.pnl_overall.ResumeLayout(false);
            this.pnl_branch_wise.ResumeLayout(false);
            this.pnl_status_wise.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel10.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2Panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).EndInit();
            this.pnl_bank_wise.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_status;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_branch;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_accounts;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_overall;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.ComboBox txt_title;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_from_date;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_employee;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private banking_ds banking_ds;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel6;
        private System.Windows.Forms.Panel panel7;
        private Guna.UI2.WinForms.Guna2Button view_button;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblReportTitle;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2Button btn_refresh;
        private Guna.UI2.WinForms.Guna2Button guna2Button5;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Panel sidePanel;
        private Guna.UI2.WinForms.Guna2Button btn_status;
        private Guna.UI2.WinForms.Guna2Button btn_account;
        private Guna.UI2.WinForms.Guna2Button btn_branch;
        private Guna.UI2.WinForms.Guna2Button btn_bank;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
        private Guna.UI2.WinForms.Guna2Button btn_overall;
        private Guna.UI2.WinForms.Guna2Button btn_employee;
        private System.Windows.Forms.Panel pnl_employee;
        private System.Windows.Forms.Panel pnl_account_wise;
        private System.Windows.Forms.Panel pnl_overall;
        private System.Windows.Forms.Panel pnl_branch_wise;
        private System.Windows.Forms.Panel pnl_status_wise;
        private System.Windows.Forms.Panel pnl_bank_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_bank;
    }
}