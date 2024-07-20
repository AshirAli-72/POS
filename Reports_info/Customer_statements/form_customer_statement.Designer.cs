namespace Reports_info.Customer_statements
{
    partial class form_customer_statement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_customer_statement));
            this.Customer_SalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.customer_statement_ds = new Reports_info.Customer_statements.customer_statement_ds();
            this.pnl_date_wise = new System.Windows.Forms.Panel();
            this.Viewer_dateWise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.customer_code_text = new System.Windows.Forms.ComboBox();
            this.lbl_cus_name = new System.Windows.Forms.Label();
            this.lbl_cus_code = new System.Windows.Forms.Label();
            this.customer_name_text = new System.Windows.Forms.ComboBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.pnl_bill_wise = new System.Windows.Forms.Panel();
            this.viewer_bill_wise = new Microsoft.Reporting.WinForms.ReportViewer();
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
            this.btnCustomerWise = new Guna.UI2.WinForms.Guna2Button();
            this.date_wise_button = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Customer_SalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customer_statement_ds)).BeginInit();
            this.pnl_date_wise.SuspendLayout();
            this.pnl_bill_wise.SuspendLayout();
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
            // Customer_SalesBindingSource
            // 
            this.Customer_SalesBindingSource.DataMember = "Customer_Sales";
            this.Customer_SalesBindingSource.DataSource = this.customer_statement_ds;
            // 
            // customer_statement_ds
            // 
            this.customer_statement_ds.DataSetName = "customer_statement_ds";
            this.customer_statement_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnl_date_wise
            // 
            this.pnl_date_wise.Controls.Add(this.Viewer_dateWise);
            this.pnl_date_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_date_wise.Location = new System.Drawing.Point(3, 3);
            this.pnl_date_wise.Name = "pnl_date_wise";
            this.pnl_date_wise.Size = new System.Drawing.Size(841, 555);
            this.pnl_date_wise.TabIndex = 29;
            // 
            // Viewer_dateWise
            // 
            this.Viewer_dateWise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Viewer_dateWise.DocumentMapWidth = 86;
            reportDataSource1.Name = "loyalCusSales";
            reportDataSource1.Value = this.Customer_SalesBindingSource;
            this.Viewer_dateWise.LocalReport.DataSources.Add(reportDataSource1);
            this.Viewer_dateWise.LocalReport.ReportEmbeddedResource = "Reports_info.Customer_statements.date_wise.loyal_cus_date_wise_sales.rdlc";
            this.Viewer_dateWise.Location = new System.Drawing.Point(0, 0);
            this.Viewer_dateWise.Name = "Viewer_dateWise";
            this.Viewer_dateWise.ServerReport.BearerToken = null;
            this.Viewer_dateWise.Size = new System.Drawing.Size(841, 555);
            this.Viewer_dateWise.TabIndex = 0;
            // 
            // customer_code_text
            // 
            this.customer_code_text.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.customer_code_text.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.customer_code_text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(157)))));
            this.customer_code_text.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.customer_code_text.ForeColor = System.Drawing.Color.Black;
            this.customer_code_text.FormattingEnabled = true;
            this.customer_code_text.IntegralHeight = false;
            this.customer_code_text.Location = new System.Drawing.Point(410, 45);
            this.customer_code_text.MaxLength = 14;
            this.customer_code_text.Name = "customer_code_text";
            this.customer_code_text.Size = new System.Drawing.Size(275, 25);
            this.customer_code_text.TabIndex = 92;
            this.customer_code_text.Visible = false;
            this.customer_code_text.SelectedIndexChanged += new System.EventHandler(this.customer_code_text_SelectedIndexChanged);
            // 
            // lbl_cus_name
            // 
            this.lbl_cus_name.AutoSize = true;
            this.lbl_cus_name.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cus_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_cus_name.Location = new System.Drawing.Point(9, 49);
            this.lbl_cus_name.Name = "lbl_cus_name";
            this.lbl_cus_name.Size = new System.Drawing.Size(46, 16);
            this.lbl_cus_name.TabIndex = 93;
            this.lbl_cus_name.Text = "Name:";
            // 
            // lbl_cus_code
            // 
            this.lbl_cus_code.AutoSize = true;
            this.lbl_cus_code.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cus_code.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_cus_code.Location = new System.Drawing.Point(362, 49);
            this.lbl_cus_code.Name = "lbl_cus_code";
            this.lbl_cus_code.Size = new System.Drawing.Size(43, 16);
            this.lbl_cus_code.TabIndex = 94;
            this.lbl_cus_code.Text = "Code:";
            this.lbl_cus_code.Visible = false;
            // 
            // customer_name_text
            // 
            this.customer_name_text.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.customer_name_text.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.customer_name_text.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.customer_name_text.ForeColor = System.Drawing.Color.Black;
            this.customer_name_text.FormattingEnabled = true;
            this.customer_name_text.Location = new System.Drawing.Point(59, 45);
            this.customer_name_text.MaxLength = 14;
            this.customer_name_text.Name = "customer_name_text";
            this.customer_name_text.Size = new System.Drawing.Size(626, 25);
            this.customer_name_text.TabIndex = 95;
            this.customer_name_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customer_name_text_KeyDown);
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(410, 9);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(275, 24);
            this.ToDate.TabIndex = 90;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(59, 9);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(276, 24);
            this.FromDate.TabIndex = 91;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_to_date.Location = new System.Drawing.Point(381, 13);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(24, 16);
            this.lbl_to_date.TabIndex = 88;
            this.lbl_to_date.Text = "To:";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_from_date.Location = new System.Drawing.Point(16, 13);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(39, 16);
            this.lbl_from_date.TabIndex = 89;
            this.lbl_from_date.Text = "From:";
            // 
            // pnl_bill_wise
            // 
            this.pnl_bill_wise.Controls.Add(this.viewer_bill_wise);
            this.pnl_bill_wise.Location = new System.Drawing.Point(456, 59);
            this.pnl_bill_wise.Name = "pnl_bill_wise";
            this.pnl_bill_wise.Size = new System.Drawing.Size(300, 429);
            this.pnl_bill_wise.TabIndex = 30;
            this.pnl_bill_wise.Visible = false;
            // 
            // viewer_bill_wise
            // 
            this.viewer_bill_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "loyalCusSales";
            reportDataSource2.Value = this.Customer_SalesBindingSource;
            this.viewer_bill_wise.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_bill_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Customer_statements.Over_all.loyal_cus_all_sales.rdlc";
            this.viewer_bill_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_bill_wise.Name = "viewer_bill_wise";
            this.viewer_bill_wise.ServerReport.BearerToken = null;
            this.viewer_bill_wise.Size = new System.Drawing.Size(300, 429);
            this.viewer_bill_wise.TabIndex = 0;
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
            this.guna2Panel2.Size = new System.Drawing.Size(1024, 736);
            this.guna2Panel2.TabIndex = 109;
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
            this.guna2Panel3.Size = new System.Drawing.Size(897, 736);
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
            this.guna2Panel10.Size = new System.Drawing.Size(877, 716);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.guna2Panel6);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 54);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(857, 652);
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
            this.guna2Panel6.Size = new System.Drawing.Size(857, 652);
            this.guna2Panel6.TabIndex = 141;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.pnl_date_wise);
            this.guna2Panel1.Controls.Add(this.pnl_bill_wise);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(5, 86);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.guna2Panel1.Size = new System.Drawing.Size(847, 561);
            this.guna2Panel1.TabIndex = 26;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.customer_name_text);
            this.panel7.Controls.Add(this.customer_code_text);
            this.panel7.Controls.Add(this.view_button);
            this.panel7.Controls.Add(this.lbl_cus_name);
            this.panel7.Controls.Add(this.guna2Separator2);
            this.panel7.Controls.Add(this.lbl_cus_code);
            this.panel7.Controls.Add(this.FromDate);
            this.panel7.Controls.Add(this.lbl_from_date);
            this.panel7.Controls.Add(this.ToDate);
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
            this.sidePanel.Controls.Add(this.btnCustomerWise);
            this.sidePanel.Controls.Add(this.date_wise_button);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(127, 736);
            this.sidePanel.TabIndex = 2;
            // 
            // btnCustomerWise
            // 
            this.btnCustomerWise.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomerWise.BorderColor = System.Drawing.Color.Transparent;
            this.btnCustomerWise.BorderRadius = 15;
            this.btnCustomerWise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomerWise.CustomBorderColor = System.Drawing.Color.White;
            this.btnCustomerWise.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCustomerWise.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomerWise.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomerWise.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCustomerWise.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCustomerWise.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomerWise.FillColor = System.Drawing.Color.White;
            this.btnCustomerWise.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCustomerWise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCustomerWise.HoverState.BorderColor = System.Drawing.Color.White;
            this.btnCustomerWise.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btnCustomerWise.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnCustomerWise.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnCustomerWise.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCustomerWise.Location = new System.Drawing.Point(10, 170);
            this.btnCustomerWise.Name = "btnCustomerWise";
            this.btnCustomerWise.Size = new System.Drawing.Size(107, 39);
            this.btnCustomerWise.TabIndex = 538;
            this.btnCustomerWise.Text = "Overall";
            this.btnCustomerWise.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCustomerWise.Click += new System.EventHandler(this.btn_bill_wise_CheckedChanged);
            // 
            // date_wise_button
            // 
            this.date_wise_button.BackColor = System.Drawing.Color.Transparent;
            this.date_wise_button.BorderColor = System.Drawing.Color.Transparent;
            this.date_wise_button.BorderRadius = 15;
            this.date_wise_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.date_wise_button.CustomBorderColor = System.Drawing.Color.White;
            this.date_wise_button.DialogResult = System.Windows.Forms.DialogResult.No;
            this.date_wise_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.date_wise_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.date_wise_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.date_wise_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.date_wise_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.date_wise_button.FillColor = System.Drawing.Color.White;
            this.date_wise_button.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.date_wise_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.date_wise_button.HoverState.BorderColor = System.Drawing.Color.White;
            this.date_wise_button.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.date_wise_button.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.date_wise_button.HoverState.ForeColor = System.Drawing.Color.White;
            this.date_wise_button.ImageSize = new System.Drawing.Size(18, 18);
            this.date_wise_button.Location = new System.Drawing.Point(10, 131);
            this.date_wise_button.Name = "date_wise_button";
            this.date_wise_button.Size = new System.Drawing.Size(107, 39);
            this.date_wise_button.TabIndex = 537;
            this.date_wise_button.Text = "Date Wise";
            this.date_wise_button.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.date_wise_button.Click += new System.EventHandler(this.date_wise_button_CheckedChanged);
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
            this.lbl_shop_title.Text = "Customer Statement";
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
            // form_customer_statement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_customer_statement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_customer_statement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_customer_statement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Customer_SalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customer_statement_ds)).EndInit();
            this.pnl_date_wise.ResumeLayout(false);
            this.pnl_bill_wise.ResumeLayout(false);
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

        private System.Windows.Forms.Panel pnl_date_wise;
        private Microsoft.Reporting.WinForms.ReportViewer Viewer_dateWise;
        private System.Windows.Forms.Panel pnl_bill_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_bill_wise;
        private System.Windows.Forms.ComboBox customer_code_text;
        private System.Windows.Forms.Label lbl_cus_name;
        private System.Windows.Forms.Label lbl_cus_code;
        private System.Windows.Forms.ComboBox customer_name_text;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.BindingSource Customer_SalesBindingSource;
        private customer_statement_ds customer_statement_ds;
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
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
        private Guna.UI2.WinForms.Guna2Button btnCustomerWise;
        private Guna.UI2.WinForms.Guna2Button date_wise_button;
    }
}