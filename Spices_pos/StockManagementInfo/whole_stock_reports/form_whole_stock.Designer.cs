using Reports_info.Stock;

namespace Stock_management.whole_stock_reports
{
    partial class form_whole_stock
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource13 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource14 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource15 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_whole_stock));
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stock_ds = new stock_ds();
            this.viewer_whole_inventory = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_summary = new System.Windows.Forms.Panel();
            this.viewer_summary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_brand_wise = new System.Windows.Forms.Panel();
            this.viewer_brand_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_company = new System.Windows.Forms.Panel();
            this.viewer_company_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_low_inventory = new System.Windows.Forms.Panel();
            this.viewer_low_inventory = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_whole_inventory = new System.Windows.Forms.Panel();
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
            this.btn_brand = new Guna.UI2.WinForms.Guna2Button();
            this.btn_company = new Guna.UI2.WinForms.Guna2Button();
            this.btn_low_inventory = new Guna.UI2.WinForms.Guna2Button();
            this.btn_whole_inventory = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnl_summary.SuspendLayout();
            this.pnl_brand_wise.SuspendLayout();
            this.pnl_company.SuspendLayout();
            this.pnl_low_inventory.SuspendLayout();
            this.pnl_whole_inventory.SuspendLayout();
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
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.stock_ds;
            // 
            // stock_ds
            // 
            this.stock_ds.DataSetName = "stock_ds";
            this.stock_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewer_whole_inventory
            // 
            this.viewer_whole_inventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_whole_inventory.DocumentMapWidth = 8;
            reportDataSource11.Name = "whole_inventory";
            reportDataSource11.Value = null;
            this.viewer_whole_inventory.LocalReport.DataSources.Add(reportDataSource11);
            this.viewer_whole_inventory.LocalReport.ReportEmbeddedResource = "Stock_management.whole_stock_reports.all_stock_report.rdlc";
            this.viewer_whole_inventory.Location = new System.Drawing.Point(0, 0);
            this.viewer_whole_inventory.Name = "viewer_whole_inventory";
            this.viewer_whole_inventory.ServerReport.BearerToken = null;
            this.viewer_whole_inventory.Size = new System.Drawing.Size(847, 616);
            this.viewer_whole_inventory.TabIndex = 10;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(11, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(65, 16);
            this.lblTitle.TabIndex = 81;
            this.lblTitle.Text = "Category:";
            // 
            // txtTitle
            // 
            this.txtTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.FormattingEnabled = true;
            this.txtTitle.IntegralHeight = false;
            this.txtTitle.Location = new System.Drawing.Point(82, 10);
            this.txtTitle.MaxLength = 14;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(359, 25);
            this.txtTitle.TabIndex = 82;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnl_whole_inventory);
            this.panel2.Controls.Add(this.pnl_summary);
            this.panel2.Controls.Add(this.pnl_brand_wise);
            this.panel2.Controls.Add(this.pnl_company);
            this.panel2.Controls.Add(this.pnl_low_inventory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(847, 616);
            this.panel2.TabIndex = 31;
            // 
            // pnl_summary
            // 
            this.pnl_summary.Controls.Add(this.viewer_summary);
            this.pnl_summary.Location = new System.Drawing.Point(675, 26);
            this.pnl_summary.Name = "pnl_summary";
            this.pnl_summary.Size = new System.Drawing.Size(93, 299);
            this.pnl_summary.TabIndex = 11;
            this.pnl_summary.Visible = false;
            // 
            // viewer_summary
            // 
            this.viewer_summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_summary.DocumentMapWidth = 8;
            reportDataSource12.Name = "whole_inventory";
            reportDataSource12.Value = null;
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource12);
            this.viewer_summary.LocalReport.ReportEmbeddedResource = "Stock_management.whole_stock_reports.all_stock_report.rdlc";
            this.viewer_summary.Location = new System.Drawing.Point(0, 0);
            this.viewer_summary.Name = "viewer_summary";
            this.viewer_summary.ServerReport.BearerToken = null;
            this.viewer_summary.Size = new System.Drawing.Size(93, 299);
            this.viewer_summary.TabIndex = 10;
            // 
            // pnl_brand_wise
            // 
            this.pnl_brand_wise.Controls.Add(this.viewer_brand_wise);
            this.pnl_brand_wise.Location = new System.Drawing.Point(524, 26);
            this.pnl_brand_wise.Name = "pnl_brand_wise";
            this.pnl_brand_wise.Size = new System.Drawing.Size(93, 299);
            this.pnl_brand_wise.TabIndex = 11;
            // 
            // viewer_brand_wise
            // 
            this.viewer_brand_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_brand_wise.DocumentMapWidth = 8;
            reportDataSource13.Name = "whole_inventory";
            reportDataSource13.Value = this.DataTable1BindingSource;
            this.viewer_brand_wise.LocalReport.DataSources.Add(reportDataSource13);
            this.viewer_brand_wise.LocalReport.ReportEmbeddedResource = "Stock_management.whole_stock_reports.brand_wise.brand_wise_report.rdlc";
            this.viewer_brand_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_brand_wise.Name = "viewer_brand_wise";
            this.viewer_brand_wise.ServerReport.BearerToken = null;
            this.viewer_brand_wise.Size = new System.Drawing.Size(93, 299);
            this.viewer_brand_wise.TabIndex = 10;
            // 
            // pnl_company
            // 
            this.pnl_company.Controls.Add(this.viewer_company_wise);
            this.pnl_company.Location = new System.Drawing.Point(373, 26);
            this.pnl_company.Name = "pnl_company";
            this.pnl_company.Size = new System.Drawing.Size(93, 299);
            this.pnl_company.TabIndex = 11;
            // 
            // viewer_company_wise
            // 
            this.viewer_company_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_company_wise.DocumentMapWidth = 8;
            reportDataSource14.Name = "whole_inventory";
            reportDataSource14.Value = this.DataTable1BindingSource;
            this.viewer_company_wise.LocalReport.DataSources.Add(reportDataSource14);
            this.viewer_company_wise.LocalReport.ReportEmbeddedResource = "Stock_management.whole_stock_reports.company_wise.company_wise_report.rdlc";
            this.viewer_company_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_company_wise.Name = "viewer_company_wise";
            this.viewer_company_wise.ServerReport.BearerToken = null;
            this.viewer_company_wise.Size = new System.Drawing.Size(93, 299);
            this.viewer_company_wise.TabIndex = 10;
            // 
            // pnl_low_inventory
            // 
            this.pnl_low_inventory.Controls.Add(this.viewer_low_inventory);
            this.pnl_low_inventory.Location = new System.Drawing.Point(222, 26);
            this.pnl_low_inventory.Name = "pnl_low_inventory";
            this.pnl_low_inventory.Size = new System.Drawing.Size(93, 299);
            this.pnl_low_inventory.TabIndex = 11;
            // 
            // viewer_low_inventory
            // 
            this.viewer_low_inventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_low_inventory.DocumentMapWidth = 8;
            reportDataSource15.Name = "whole_inventory";
            reportDataSource15.Value = this.DataTable1BindingSource;
            this.viewer_low_inventory.LocalReport.DataSources.Add(reportDataSource15);
            this.viewer_low_inventory.LocalReport.ReportEmbeddedResource = "Stock_management.whole_stock_reports.low_stock.low_stock_report.rdlc";
            this.viewer_low_inventory.Location = new System.Drawing.Point(0, 0);
            this.viewer_low_inventory.Name = "viewer_low_inventory";
            this.viewer_low_inventory.ServerReport.BearerToken = null;
            this.viewer_low_inventory.Size = new System.Drawing.Size(93, 299);
            this.viewer_low_inventory.TabIndex = 10;
            // 
            // pnl_whole_inventory
            // 
            this.pnl_whole_inventory.Controls.Add(this.viewer_whole_inventory);
            this.pnl_whole_inventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_whole_inventory.Location = new System.Drawing.Point(0, 0);
            this.pnl_whole_inventory.Name = "pnl_whole_inventory";
            this.pnl_whole_inventory.Size = new System.Drawing.Size(847, 616);
            this.pnl_whole_inventory.TabIndex = 11;
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
            this.guna2Panel6.Controls.Add(this.panel2);
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
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.lblTitle);
            this.panel7.Controls.Add(this.view_button);
            this.panel7.Controls.Add(this.txtTitle);
            this.panel7.Controls.Add(this.guna2Separator2);
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
            this.view_button.Location = new System.Drawing.Point(524, 10);
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
            this.sidePanel.Controls.Add(this.btn_brand);
            this.sidePanel.Controls.Add(this.btn_company);
            this.sidePanel.Controls.Add(this.btn_low_inventory);
            this.sidePanel.Controls.Add(this.btn_whole_inventory);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(127, 768);
            this.sidePanel.TabIndex = 2;
            // 
            // btn_brand
            // 
            this.btn_brand.BackColor = System.Drawing.Color.Transparent;
            this.btn_brand.BorderColor = System.Drawing.Color.Transparent;
            this.btn_brand.BorderRadius = 15;
            this.btn_brand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_brand.CustomBorderColor = System.Drawing.Color.White;
            this.btn_brand.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_brand.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_brand.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_brand.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_brand.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_brand.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_brand.FillColor = System.Drawing.Color.White;
            this.btn_brand.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_brand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_brand.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_brand.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_brand.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_brand.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_brand.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_brand.Location = new System.Drawing.Point(10, 248);
            this.btn_brand.Name = "btn_brand";
            this.btn_brand.Size = new System.Drawing.Size(107, 39);
            this.btn_brand.TabIndex = 530;
            this.btn_brand.Text = "Brand Wise";
            this.btn_brand.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_brand.Click += new System.EventHandler(this.btn_brand_CheckedChanged);
            // 
            // btn_company
            // 
            this.btn_company.BackColor = System.Drawing.Color.Transparent;
            this.btn_company.BorderColor = System.Drawing.Color.Transparent;
            this.btn_company.BorderRadius = 15;
            this.btn_company.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_company.CustomBorderColor = System.Drawing.Color.White;
            this.btn_company.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_company.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_company.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_company.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_company.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_company.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_company.FillColor = System.Drawing.Color.White;
            this.btn_company.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_company.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_company.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_company.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_company.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_company.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_company.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_company.Location = new System.Drawing.Point(10, 209);
            this.btn_company.Name = "btn_company";
            this.btn_company.Size = new System.Drawing.Size(107, 39);
            this.btn_company.TabIndex = 529;
            this.btn_company.Text = "Category Wise";
            this.btn_company.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_company.Click += new System.EventHandler(this.btn_company_CheckedChanged);
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
            this.btn_low_inventory.Location = new System.Drawing.Point(10, 170);
            this.btn_low_inventory.Name = "btn_low_inventory";
            this.btn_low_inventory.Size = new System.Drawing.Size(107, 39);
            this.btn_low_inventory.TabIndex = 526;
            this.btn_low_inventory.Text = "Low Inventory";
            this.btn_low_inventory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_low_inventory.Click += new System.EventHandler(this.btn_low_inventory_CheckedChanged);
            // 
            // btn_whole_inventory
            // 
            this.btn_whole_inventory.BackColor = System.Drawing.Color.Transparent;
            this.btn_whole_inventory.BorderColor = System.Drawing.Color.Transparent;
            this.btn_whole_inventory.BorderRadius = 15;
            this.btn_whole_inventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_whole_inventory.CustomBorderColor = System.Drawing.Color.White;
            this.btn_whole_inventory.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_whole_inventory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_whole_inventory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_whole_inventory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_whole_inventory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_whole_inventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_whole_inventory.FillColor = System.Drawing.Color.White;
            this.btn_whole_inventory.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_whole_inventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_whole_inventory.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_whole_inventory.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_whole_inventory.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_whole_inventory.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_whole_inventory.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_whole_inventory.Location = new System.Drawing.Point(10, 131);
            this.btn_whole_inventory.Name = "btn_whole_inventory";
            this.btn_whole_inventory.Size = new System.Drawing.Size(107, 39);
            this.btn_whole_inventory.TabIndex = 525;
            this.btn_whole_inventory.Text = "All Inventory";
            this.btn_whole_inventory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_whole_inventory.Click += new System.EventHandler(this.btn_whole_inventory_CheckedChanged);
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
            this.lbl_shop_title.Text = "Stock Report";
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
            // form_whole_stock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_whole_stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_whole_stock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_whole_stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnl_summary.ResumeLayout(false);
            this.pnl_brand_wise.ResumeLayout(false);
            this.pnl_company.ResumeLayout(false);
            this.pnl_low_inventory.ResumeLayout(false);
            this.pnl_whole_inventory.ResumeLayout(false);
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
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer viewer_whole_inventory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox txtTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_low_inventory;
        private System.Windows.Forms.Panel pnl_whole_inventory;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_low_inventory;
        private System.Windows.Forms.Panel pnl_brand_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_brand_wise;
        private System.Windows.Forms.Panel pnl_company;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_company_wise;
        private System.Windows.Forms.Panel pnl_summary;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_summary;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private stock_ds stock_ds;
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
        private Guna.UI2.WinForms.Guna2Button btn_brand;
        private Guna.UI2.WinForms.Guna2Button btn_company;
        private Guna.UI2.WinForms.Guna2Button btn_low_inventory;
        private Guna.UI2.WinForms.Guna2Button btn_whole_inventory;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
    }
}