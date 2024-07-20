using Spices_pos.StockManagementInfo.Low_inventory_reports;

namespace Stock_management.Low_inventory_reports
{
    partial class form_low_inventory
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_low_inventory));
            this.low_inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stocks_ds = new Spices_pos.StockManagementInfo.Low_inventory_reports.stocks_ds();
            this.expired_itemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.beforeExpiryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.viewer_low_inventory = new Microsoft.Reporting.WinForms.ReportViewer();
            this.viewer_expiry = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ViewerBeforeExpiry = new Microsoft.Reporting.WinForms.ReportViewer();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2Panel6 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
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
            this.btnBefore_expiry = new Guna.UI2.WinForms.Guna2Button();
            this.btn_expiry = new Guna.UI2.WinForms.Guna2Button();
            this.btn_low_inventory = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.low_inventoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocks_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expired_itemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeExpiryBindingSource)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2Panel6.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).BeginInit();
            this.SuspendLayout();
            // 
            // low_inventoryBindingSource
            // 
            this.low_inventoryBindingSource.DataMember = "low_inventory";
            this.low_inventoryBindingSource.DataSource = this.stocks_ds;
            // 
            // stocks_ds
            // 
            this.stocks_ds.DataSetName = "stocks_ds";
            this.stocks_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // expired_itemsBindingSource
            // 
            this.expired_itemsBindingSource.DataMember = "expired_items";
            this.expired_itemsBindingSource.DataSource = this.stocks_ds;
            // 
            // beforeExpiryBindingSource
            // 
            this.beforeExpiryBindingSource.DataMember = "beforeExpiry";
            this.beforeExpiryBindingSource.DataSource = this.stocks_ds;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(347, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 104;
            this.label1.Text = "To:";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_from_date.Location = new System.Drawing.Point(9, 14);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(39, 16);
            this.lbl_from_date.TabIndex = 104;
            this.lbl_from_date.Text = "From:";
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(377, 10);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(261, 24);
            this.ToDate.TabIndex = 103;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(54, 10);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(261, 24);
            this.FromDate.TabIndex = 103;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // viewer_low_inventory
            // 
            this.viewer_low_inventory.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "low_inventory";
            reportDataSource2.Value = this.low_inventoryBindingSource;
            this.viewer_low_inventory.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_low_inventory.LocalReport.ReportEmbeddedResource = "Spices_pos.StockManagementInfo.Low_inventory_reports.low_inventory_report.rdlc";
            this.viewer_low_inventory.Location = new System.Drawing.Point(3, 3);
            this.viewer_low_inventory.Name = "viewer_low_inventory";
            this.viewer_low_inventory.ServerReport.BearerToken = null;
            this.viewer_low_inventory.Size = new System.Drawing.Size(841, 610);
            this.viewer_low_inventory.TabIndex = 12;
            // 
            // viewer_expiry
            // 
            this.viewer_expiry.LocalReport.ReportEmbeddedResource = "Spices_pos.StockManagementInfo.Low_inventory_reports.expired_items_report.rdlc";
            this.viewer_expiry.Location = new System.Drawing.Point(218, 24);
            this.viewer_expiry.Name = "viewer_expiry";
            this.viewer_expiry.ServerReport.BearerToken = null;
            this.viewer_expiry.Size = new System.Drawing.Size(132, 311);
            this.viewer_expiry.TabIndex = 13;
            // 
            // ViewerBeforeExpiry
            // 
            this.ViewerBeforeExpiry.LocalReport.ReportEmbeddedResource = "Spices_pos.StockManagementInfo.Low_inventory_reports.before_expiry_report.rdlc";
            this.ViewerBeforeExpiry.Location = new System.Drawing.Point(382, 24);
            this.ViewerBeforeExpiry.Name = "ViewerBeforeExpiry";
            this.ViewerBeforeExpiry.ServerReport.BearerToken = null;
            this.ViewerBeforeExpiry.Size = new System.Drawing.Size(132, 311);
            this.ViewerBeforeExpiry.TabIndex = 13;
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
            this.guna2Panel2.Size = new System.Drawing.Size(1024, 768);
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
            this.guna2Panel3.Size = new System.Drawing.Size(897, 768);
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
            this.guna2Panel10.Size = new System.Drawing.Size(877, 748);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel6);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 54);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(857, 684);
            this.guna2GradientPanel1.TabIndex = 137;
            // 
            // guna2Panel6
            // 
            this.guna2Panel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel6.BorderRadius = 20;
            this.guna2Panel6.Controls.Add(this.guna2Panel1);
            this.guna2Panel6.Controls.Add(this.panel7);
            this.guna2Panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel6.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel6.Margin = new System.Windows.Forms.Padding(10);
            this.guna2Panel6.Name = "guna2Panel6";
            this.guna2Panel6.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel6.Size = new System.Drawing.Size(857, 684);
            this.guna2Panel6.TabIndex = 141;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.ViewerBeforeExpiry);
            this.guna2Panel1.Controls.Add(this.viewer_expiry);
            this.guna2Panel1.Controls.Add(this.viewer_low_inventory);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(5, 63);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.guna2Panel1.Size = new System.Drawing.Size(847, 616);
            this.guna2Panel1.TabIndex = 26;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.view_button);
            this.panel7.Controls.Add(this.lbl_from_date);
            this.panel7.Controls.Add(this.guna2Separator2);
            this.panel7.Controls.Add(this.ToDate);
            this.panel7.Controls.Add(this.FromDate);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel7.Location = new System.Drawing.Point(5, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(847, 58);
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
            this.view_button.Location = new System.Drawing.Point(731, 10);
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
            this.guna2Separator2.Location = new System.Drawing.Point(0, 48);
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
            this.sidePanel.Controls.Add(this.btnBefore_expiry);
            this.sidePanel.Controls.Add(this.btn_expiry);
            this.sidePanel.Controls.Add(this.btn_low_inventory);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(127, 768);
            this.sidePanel.TabIndex = 2;
            // 
            // btnBefore_expiry
            // 
            this.btnBefore_expiry.BackColor = System.Drawing.Color.Transparent;
            this.btnBefore_expiry.BorderColor = System.Drawing.Color.Transparent;
            this.btnBefore_expiry.BorderRadius = 15;
            this.btnBefore_expiry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBefore_expiry.CustomBorderColor = System.Drawing.Color.White;
            this.btnBefore_expiry.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnBefore_expiry.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBefore_expiry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBefore_expiry.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBefore_expiry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBefore_expiry.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBefore_expiry.FillColor = System.Drawing.Color.White;
            this.btnBefore_expiry.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBefore_expiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBefore_expiry.HoverState.BorderColor = System.Drawing.Color.White;
            this.btnBefore_expiry.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btnBefore_expiry.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnBefore_expiry.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnBefore_expiry.ImageSize = new System.Drawing.Size(18, 18);
            this.btnBefore_expiry.Location = new System.Drawing.Point(10, 209);
            this.btnBefore_expiry.Name = "btnBefore_expiry";
            this.btnBefore_expiry.Size = new System.Drawing.Size(107, 39);
            this.btnBefore_expiry.TabIndex = 529;
            this.btnBefore_expiry.Text = "Before Expiry";
            this.btnBefore_expiry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBefore_expiry.Click += new System.EventHandler(this.btnBefore_expiry_CheckedChanged);
            // 
            // btn_expiry
            // 
            this.btn_expiry.BackColor = System.Drawing.Color.Transparent;
            this.btn_expiry.BorderColor = System.Drawing.Color.Transparent;
            this.btn_expiry.BorderRadius = 15;
            this.btn_expiry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_expiry.CustomBorderColor = System.Drawing.Color.White;
            this.btn_expiry.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_expiry.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_expiry.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_expiry.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_expiry.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_expiry.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_expiry.FillColor = System.Drawing.Color.White;
            this.btn_expiry.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_expiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_expiry.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_expiry.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_expiry.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_expiry.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_expiry.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_expiry.Location = new System.Drawing.Point(10, 170);
            this.btn_expiry.Name = "btn_expiry";
            this.btn_expiry.Size = new System.Drawing.Size(107, 39);
            this.btn_expiry.TabIndex = 526;
            this.btn_expiry.Text = "Expired Items";
            this.btn_expiry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_expiry.Click += new System.EventHandler(this.btn_expiry_CheckedChanged);
            // 
            // btn_low_inventory
            // 
            this.btn_low_inventory.BackColor = System.Drawing.Color.Transparent;
            this.btn_low_inventory.BorderColor = System.Drawing.Color.Transparent;
            this.btn_low_inventory.BorderRadius = 15;
            this.btn_low_inventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_low_inventory.CustomBorderColor = System.Drawing.Color.White;
            this.btn_low_inventory.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_low_inventory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_low_inventory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_low_inventory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_low_inventory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_low_inventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_low_inventory.FillColor = System.Drawing.Color.White;
            this.btn_low_inventory.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_low_inventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_low_inventory.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_low_inventory.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_low_inventory.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_low_inventory.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_low_inventory.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_low_inventory.Location = new System.Drawing.Point(10, 131);
            this.btn_low_inventory.Name = "btn_low_inventory";
            this.btn_low_inventory.Size = new System.Drawing.Size(107, 39);
            this.btn_low_inventory.TabIndex = 525;
            this.btn_low_inventory.Text = "Low Inventory";
            this.btn_low_inventory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_low_inventory.Click += new System.EventHandler(this.btn_low_inventory_CheckedChanged);
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
            this.lbl_shop_title.Text = "Alertss";
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
            // form_low_inventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_low_inventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_low_inventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_low_inventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.low_inventoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocks_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expired_itemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beforeExpiryBindingSource)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel10.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2Panel6.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo_img2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_from_date;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_low_inventory;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_expiry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.BindingSource expired_itemsBindingSource;
        private stocks_ds stocks_ds;
        private System.Windows.Forms.BindingSource low_inventoryBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer ViewerBeforeExpiry;
        private System.Windows.Forms.BindingSource beforeExpiryBindingSource;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel6;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
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
        private Guna.UI2.WinForms.Guna2Button btnBefore_expiry;
        private Guna.UI2.WinForms.Guna2Button btn_expiry;
        private Guna.UI2.WinForms.Guna2Button btn_low_inventory;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
    }
}