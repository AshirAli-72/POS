namespace Spices_pos.CashManagement.Reports
{
    partial class transaction_detail_reports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(transaction_detail_reports));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.pnl_person_wise = new System.Windows.Forms.Panel();
            this.viewer_person = new Microsoft.Reporting.WinForms.ReportViewer();
            this.view_button = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.btn_refresh = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button5 = new Guna.UI2.WinForms.Guna2Button();
            this.close_button = new Guna.UI2.WinForms.Guna2Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.txt_title = new System.Windows.Forms.ComboBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.sidePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_date = new Guna.UI2.WinForms.Guna2Button();
            this.btn_payment = new Guna.UI2.WinForms.Guna2Button();
            this.btn_status = new Guna.UI2.WinForms.Guna2Button();
            this.btn_person = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_date_wise = new System.Windows.Forms.Panel();
            this.viewer_date = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_status_wise = new System.Windows.Forms.Panel();
            this.viewer_status = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_payment_wise = new System.Windows.Forms.Panel();
            this.viewer_payment = new Microsoft.Reporting.WinForms.ReportViewer();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnl_person_wise.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).BeginInit();
            this.guna2Panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_date_wise.SuspendLayout();
            this.pnl_status_wise.SuspendLayout();
            this.pnl_payment_wise.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_person_wise
            // 
            this.pnl_person_wise.Controls.Add(this.viewer_person);
            this.pnl_person_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_person_wise.Location = new System.Drawing.Point(0, 0);
            this.pnl_person_wise.Name = "pnl_person_wise";
            this.pnl_person_wise.Size = new System.Drawing.Size(833, 590);
            this.pnl_person_wise.TabIndex = 0;
            // 
            // viewer_person
            // 
            this.viewer_person.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_person.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.person_wise_report.rdlc";
            this.viewer_person.Location = new System.Drawing.Point(0, 0);
            this.viewer_person.Name = "viewer_person";
            this.viewer_person.ServerReport.BearerToken = null;
            this.viewer_person.Size = new System.Drawing.Size(833, 590);
            this.viewer_person.TabIndex = 0;
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
            this.view_button.Location = new System.Drawing.Point(718, 33);
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
            this.guna2Separator2.Size = new System.Drawing.Size(833, 10);
            this.guna2Separator2.TabIndex = 538;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Controls.Add(this.lblReportTitle);
            this.panel10.Controls.Add(this.guna2Separator1);
            this.panel10.Controls.Add(this.btn_refresh);
            this.panel10.Controls.Add(this.guna2Button5);
            this.panel10.Controls.Add(this.close_button);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel10.Location = new System.Drawing.Point(10, 10);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(843, 44);
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
            this.guna2Separator1.Size = new System.Drawing.Size(842, 10);
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
            this.btn_refresh.Location = new System.Drawing.Point(757, 1);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(38, 33);
            this.btn_refresh.TabIndex = 531;
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
            this.guna2Button5.Location = new System.Drawing.Point(842, 0);
            this.guna2Button5.Name = "guna2Button5";
            this.guna2Button5.Size = new System.Drawing.Size(1, 44);
            this.guna2Button5.TabIndex = 508;
            this.guna2Button5.Visible = false;
            // 
            // close_button
            // 
            this.close_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.close_button.BackColor = System.Drawing.Color.Transparent;
            this.close_button.BorderColor = System.Drawing.Color.Gray;
            this.close_button.BorderRadius = 7;
            this.close_button.BorderThickness = 1;
            this.close_button.CustomBorderColor = System.Drawing.Color.Gray;
            this.close_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.close_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.close_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.close_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.close_button.FillColor = System.Drawing.Color.Transparent;
            this.close_button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.close_button.ForeColor = System.Drawing.Color.White;
            this.close_button.HoverState.BorderColor = System.Drawing.Color.White;
            this.close_button.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.close_button.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.close_button.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.close_button.Image = ((System.Drawing.Image)(resources.GetObject("close_button.Image")));
            this.close_button.ImageSize = new System.Drawing.Size(15, 15);
            this.close_button.Location = new System.Drawing.Point(800, 1);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(38, 33);
            this.close_button.TabIndex = 522;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
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
            this.panel7.Size = new System.Drawing.Size(833, 81);
            this.panel7.TabIndex = 13;
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
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.Transparent;
            this.sidePanel.Controls.Add(this.btn_date);
            this.sidePanel.Controls.Add(this.btn_payment);
            this.sidePanel.Controls.Add(this.btn_status);
            this.sidePanel.Controls.Add(this.btn_person);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(141, 765);
            this.sidePanel.TabIndex = 2;
            // 
            // btn_date
            // 
            this.btn_date.BackColor = System.Drawing.Color.Transparent;
            this.btn_date.BorderColor = System.Drawing.Color.Transparent;
            this.btn_date.BorderRadius = 15;
            this.btn_date.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_date.CustomBorderColor = System.Drawing.Color.White;
            this.btn_date.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_date.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_date.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_date.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_date.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_date.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_date.FillColor = System.Drawing.Color.White;
            this.btn_date.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_date.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_date.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_date.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_date.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_date.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_date.Location = new System.Drawing.Point(10, 248);
            this.btn_date.Name = "btn_date";
            this.btn_date.Size = new System.Drawing.Size(121, 39);
            this.btn_date.TabIndex = 530;
            this.btn_date.Text = "Date Wise";
            this.btn_date.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_date.Click += new System.EventHandler(this.btn_date_Click);
            // 
            // btn_payment
            // 
            this.btn_payment.BackColor = System.Drawing.Color.Transparent;
            this.btn_payment.BorderColor = System.Drawing.Color.Transparent;
            this.btn_payment.BorderRadius = 15;
            this.btn_payment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_payment.CustomBorderColor = System.Drawing.Color.White;
            this.btn_payment.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_payment.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_payment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_payment.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_payment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_payment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_payment.FillColor = System.Drawing.Color.White;
            this.btn_payment.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_payment.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_payment.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_payment.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_payment.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_payment.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_payment.Location = new System.Drawing.Point(10, 209);
            this.btn_payment.Name = "btn_payment";
            this.btn_payment.Size = new System.Drawing.Size(121, 39);
            this.btn_payment.TabIndex = 529;
            this.btn_payment.Text = "Payment Wise";
            this.btn_payment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_payment.Click += new System.EventHandler(this.btn_payment_Click);
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
            this.btn_status.Location = new System.Drawing.Point(10, 170);
            this.btn_status.Name = "btn_status";
            this.btn_status.Size = new System.Drawing.Size(121, 39);
            this.btn_status.TabIndex = 526;
            this.btn_status.Text = "Status Wise";
            this.btn_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_status.Click += new System.EventHandler(this.btn_status_Click);
            // 
            // btn_person
            // 
            this.btn_person.BackColor = System.Drawing.Color.Transparent;
            this.btn_person.BorderColor = System.Drawing.Color.Transparent;
            this.btn_person.BorderRadius = 15;
            this.btn_person.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_person.CustomBorderColor = System.Drawing.Color.White;
            this.btn_person.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_person.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_person.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_person.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_person.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_person.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_person.FillColor = System.Drawing.Color.White;
            this.btn_person.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_person.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_person.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_person.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_person.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_person.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_person.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_person.Location = new System.Drawing.Point(10, 131);
            this.btn_person.Name = "btn_person";
            this.btn_person.Size = new System.Drawing.Size(121, 39);
            this.btn_person.TabIndex = 525;
            this.btn_person.Text = "Person Wise";
            this.btn_person.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_person.Click += new System.EventHandler(this.btn_person_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.lbl_shop_title);
            this.panel8.Controls.Add(this.logo_img2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(10, 10);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(121, 121);
            this.panel8.TabIndex = 534;
            // 
            // lbl_shop_title
            // 
            this.lbl_shop_title.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_shop_title.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_shop_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_shop_title.Location = new System.Drawing.Point(10, 67);
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
            this.logo_img2.Location = new System.Drawing.Point(47, 21);
            this.logo_img2.Name = "logo_img2";
            this.logo_img2.ShadowDecoration.BorderRadius = 0;
            this.logo_img2.Size = new System.Drawing.Size(40, 40);
            this.logo_img2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo_img2.TabIndex = 7;
            this.logo_img2.TabStop = false;
            this.logo_img2.UseTransparentBackground = true;
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
            this.guna2Panel6.Size = new System.Drawing.Size(843, 681);
            this.guna2Panel6.TabIndex = 141;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnl_person_wise);
            this.panel2.Controls.Add(this.pnl_date_wise);
            this.panel2.Controls.Add(this.pnl_status_wise);
            this.panel2.Controls.Add(this.pnl_payment_wise);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 590);
            this.panel2.TabIndex = 31;
            // 
            // pnl_date_wise
            // 
            this.pnl_date_wise.Controls.Add(this.viewer_date);
            this.pnl_date_wise.Location = new System.Drawing.Point(309, 270);
            this.pnl_date_wise.Name = "pnl_date_wise";
            this.pnl_date_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_date_wise.TabIndex = 0;
            // 
            // viewer_date
            // 
            this.viewer_date.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "new_purchase";
            reportDataSource1.Value = null;
            this.viewer_date.LocalReport.DataSources.Add(reportDataSource1);
            this.viewer_date.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.employee_wise_report.rdlc";
            this.viewer_date.Location = new System.Drawing.Point(0, 0);
            this.viewer_date.Name = "viewer_date";
            this.viewer_date.ServerReport.BearerToken = null;
            this.viewer_date.Size = new System.Drawing.Size(208, 212);
            this.viewer_date.TabIndex = 0;
            // 
            // pnl_status_wise
            // 
            this.pnl_status_wise.Controls.Add(this.viewer_status);
            this.pnl_status_wise.Location = new System.Drawing.Point(309, 29);
            this.pnl_status_wise.Name = "pnl_status_wise";
            this.pnl_status_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_status_wise.TabIndex = 0;
            // 
            // viewer_status
            // 
            this.viewer_status.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "new_purchase";
            reportDataSource2.Value = null;
            this.viewer_status.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_status.LocalReport.ReportEmbeddedResource = "Spices_pos.CashManagement.Reports.transaction_wise_report.rdlc";
            this.viewer_status.Location = new System.Drawing.Point(0, 0);
            this.viewer_status.Name = "viewer_status";
            this.viewer_status.ServerReport.BearerToken = null;
            this.viewer_status.Size = new System.Drawing.Size(208, 212);
            this.viewer_status.TabIndex = 0;
            // 
            // pnl_payment_wise
            // 
            this.pnl_payment_wise.Controls.Add(this.viewer_payment);
            this.pnl_payment_wise.Location = new System.Drawing.Point(579, 29);
            this.pnl_payment_wise.Name = "pnl_payment_wise";
            this.pnl_payment_wise.Size = new System.Drawing.Size(208, 212);
            this.pnl_payment_wise.TabIndex = 0;
            // 
            // viewer_payment
            // 
            this.viewer_payment.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "new_purchase";
            reportDataSource3.Value = null;
            this.viewer_payment.LocalReport.DataSources.Add(reportDataSource3);
            this.viewer_payment.LocalReport.ReportEmbeddedResource = "Spices_pos.BankingInfo.Reports.status_wise_report.rdlc";
            this.viewer_payment.Location = new System.Drawing.Point(0, 0);
            this.viewer_payment.Name = "viewer_payment";
            this.viewer_payment.ServerReport.BearerToken = null;
            this.viewer_payment.Size = new System.Drawing.Size(208, 212);
            this.viewer_payment.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel6);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 54);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(843, 681);
            this.guna2GradientPanel1.TabIndex = 137;
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
            this.guna2Panel10.Size = new System.Drawing.Size(863, 745);
            this.guna2Panel10.TabIndex = 0;
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
            this.guna2Panel2.TabIndex = 109;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 30;
            this.guna2Panel3.Controls.Add(this.guna2Panel10);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(141, 0);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Padding = new System.Windows.Forms.Padding(10);
            this.guna2Panel3.Size = new System.Drawing.Size(883, 765);
            this.guna2Panel3.TabIndex = 1;
            // 
            // transaction_detail_reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 765);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "transaction_detail_reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "transaction_detail_reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.transaction_detail_reports_Load);
            this.pnl_person_wise.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).EndInit();
            this.guna2Panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnl_date_wise.ResumeLayout(false);
            this.pnl_status_wise.ResumeLayout(false);
            this.pnl_payment_wise.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2Panel10.ResumeLayout(false);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_person_wise;
        private Guna.UI2.WinForms.Guna2Button view_button;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblReportTitle;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2Button btn_refresh;
        private Guna.UI2.WinForms.Guna2Button guna2Button5;
        private Guna.UI2.WinForms.Guna2Button close_button;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.ComboBox txt_title;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.Label lbl_to_date;
        private Guna.UI2.WinForms.Guna2Panel sidePanel;
        private Guna.UI2.WinForms.Guna2Button btn_date;
        private Guna.UI2.WinForms.Guna2Button btn_payment;
        private Guna.UI2.WinForms.Guna2Button btn_status;
        private Guna.UI2.WinForms.Guna2Button btn_person;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_date_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_date;
        private System.Windows.Forms.Panel pnl_status_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_status;
        private System.Windows.Forms.Panel pnl_payment_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_payment;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_person;
    }
}