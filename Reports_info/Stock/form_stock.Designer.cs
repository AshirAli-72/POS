namespace Reports_info.Stock
{
    partial class form_stock
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource13 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_stock));
            this.Customers_SalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stock_ds = new Reports_info.Stock.stock_ds();
            this.CustomerReturnsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PurchasesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.expired_itemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbl_brand = new System.Windows.Forms.Label();
            this.txt_brand = new System.Windows.Forms.ComboBox();
            this.chk_company_wise = new System.Windows.Forms.CheckBox();
            this.chk_brand_wise = new System.Windows.Forms.CheckBox();
            this.chk_over_all = new System.Windows.Forms.CheckBox();
            this.lbl_company = new System.Windows.Forms.Label();
            this.txt_company = new System.Windows.Forms.ComboBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_purchase_return = new System.Windows.Forms.Panel();
            this.viewer_purchase_return = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_summary = new System.Windows.Forms.Panel();
            this.viewer_summary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_expiry = new System.Windows.Forms.Panel();
            this.viewer_expiry = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_returned = new System.Windows.Forms.Panel();
            this.viewer_returned = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_purchased = new System.Windows.Forms.Panel();
            this.viewer_purchased = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_low_items = new System.Windows.Forms.Panel();
            this.viewer_low_items = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_company_wise = new System.Windows.Forms.Panel();
            this.viewer_company_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_brand_wise = new System.Windows.Forms.Panel();
            this.viewer_brand_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_sold = new System.Windows.Forms.Panel();
            this.viewer_sold = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Purchase_returnBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.btn_returned = new Guna.UI2.WinForms.Guna2Button();
            this.btn_sold = new Guna.UI2.WinForms.Guna2Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_shop_title = new System.Windows.Forms.Label();
            this.logo_img2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btn_purchased = new Guna.UI2.WinForms.Guna2Button();
            this.btn_low_items = new Guna.UI2.WinForms.Guna2Button();
            this.btn_summary = new Guna.UI2.WinForms.Guna2Button();
            this.btn_expiry = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.Customers_SalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturnsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchasesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expired_itemsBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnl_purchase_return.SuspendLayout();
            this.pnl_summary.SuspendLayout();
            this.pnl_expiry.SuspendLayout();
            this.pnl_returned.SuspendLayout();
            this.pnl_purchased.SuspendLayout();
            this.pnl_low_items.SuspendLayout();
            this.pnl_company_wise.SuspendLayout();
            this.pnl_brand_wise.SuspendLayout();
            this.pnl_sold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Purchase_returnBindingSource)).BeginInit();
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
            // Customers_SalesBindingSource
            // 
            this.Customers_SalesBindingSource.DataMember = "Customers_Sales";
            this.Customers_SalesBindingSource.DataSource = this.stock_ds;
            // 
            // stock_ds
            // 
            this.stock_ds.DataSetName = "stock_ds";
            this.stock_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CustomerReturnsBindingSource
            // 
            this.CustomerReturnsBindingSource.DataMember = "CustomerReturns";
            this.CustomerReturnsBindingSource.DataSource = this.stock_ds;
            // 
            // PurchasesBindingSource
            // 
            this.PurchasesBindingSource.DataMember = "Purchases";
            this.PurchasesBindingSource.DataSource = this.stock_ds;
            // 
            // InventoryBindingSource
            // 
            this.InventoryBindingSource.DataMember = "Inventory";
            this.InventoryBindingSource.DataSource = this.stock_ds;
            // 
            // expired_itemsBindingSource
            // 
            this.expired_itemsBindingSource.DataMember = "expired_items";
            this.expired_itemsBindingSource.DataSource = this.stock_ds;
            // 
            // lbl_brand
            // 
            this.lbl_brand.AutoSize = true;
            this.lbl_brand.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_brand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_brand.Location = new System.Drawing.Point(36, 49);
            this.lbl_brand.Name = "lbl_brand";
            this.lbl_brand.Size = new System.Drawing.Size(44, 16);
            this.lbl_brand.TabIndex = 102;
            this.lbl_brand.Text = "Brand:";
            // 
            // txt_brand
            // 
            this.txt_brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_brand.BackColor = System.Drawing.SystemColors.Window;
            this.txt_brand.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txt_brand.ForeColor = System.Drawing.Color.Black;
            this.txt_brand.FormattingEnabled = true;
            this.txt_brand.IntegralHeight = false;
            this.txt_brand.Location = new System.Drawing.Point(84, 45);
            this.txt_brand.MaxLength = 14;
            this.txt_brand.Name = "txt_brand";
            this.txt_brand.Size = new System.Drawing.Size(522, 25);
            this.txt_brand.TabIndex = 103;
            // 
            // chk_company_wise
            // 
            this.chk_company_wise.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk_company_wise.AutoSize = true;
            this.chk_company_wise.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.chk_company_wise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chk_company_wise.Location = new System.Drawing.Point(636, 8);
            this.chk_company_wise.Name = "chk_company_wise";
            this.chk_company_wise.Size = new System.Drawing.Size(103, 19);
            this.chk_company_wise.TabIndex = 101;
            this.chk_company_wise.Text = "Category Wise";
            this.chk_company_wise.UseVisualStyleBackColor = true;
            this.chk_company_wise.CheckedChanged += new System.EventHandler(this.chk_company_wise_CheckedChanged);
            // 
            // chk_brand_wise
            // 
            this.chk_brand_wise.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk_brand_wise.AutoSize = true;
            this.chk_brand_wise.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.chk_brand_wise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chk_brand_wise.Location = new System.Drawing.Point(636, 30);
            this.chk_brand_wise.Name = "chk_brand_wise";
            this.chk_brand_wise.Size = new System.Drawing.Size(86, 19);
            this.chk_brand_wise.TabIndex = 101;
            this.chk_brand_wise.Text = "Brand Wise";
            this.chk_brand_wise.UseVisualStyleBackColor = true;
            this.chk_brand_wise.CheckedChanged += new System.EventHandler(this.chk_brand_wise_CheckedChanged);
            // 
            // chk_over_all
            // 
            this.chk_over_all.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk_over_all.AutoSize = true;
            this.chk_over_all.Checked = true;
            this.chk_over_all.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_over_all.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.chk_over_all.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chk_over_all.Location = new System.Drawing.Point(636, 51);
            this.chk_over_all.Name = "chk_over_all";
            this.chk_over_all.Size = new System.Drawing.Size(69, 19);
            this.chk_over_all.TabIndex = 101;
            this.chk_over_all.Text = "Over All";
            this.chk_over_all.UseVisualStyleBackColor = true;
            this.chk_over_all.CheckedChanged += new System.EventHandler(this.chk_over_all_CheckedChanged);
            // 
            // lbl_company
            // 
            this.lbl_company.AutoSize = true;
            this.lbl_company.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_company.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_company.Location = new System.Drawing.Point(12, 49);
            this.lbl_company.Name = "lbl_company";
            this.lbl_company.Size = new System.Drawing.Size(68, 16);
            this.lbl_company.TabIndex = 81;
            this.lbl_company.Text = "Company:";
            // 
            // txt_company
            // 
            this.txt_company.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_company.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_company.BackColor = System.Drawing.SystemColors.Window;
            this.txt_company.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txt_company.ForeColor = System.Drawing.Color.Black;
            this.txt_company.FormattingEnabled = true;
            this.txt_company.IntegralHeight = false;
            this.txt_company.Location = new System.Drawing.Point(84, 45);
            this.txt_company.MaxLength = 14;
            this.txt_company.Name = "txt_company";
            this.txt_company.Size = new System.Drawing.Size(522, 25);
            this.txt_company.TabIndex = 82;
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(375, 9);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(231, 24);
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
            this.FromDate.Location = new System.Drawing.Point(84, 9);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(232, 24);
            this.FromDate.TabIndex = 58;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_to_date.Location = new System.Drawing.Point(345, 13);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(24, 16);
            this.lbl_to_date.TabIndex = 57;
            this.lbl_to_date.Text = "To:";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_from_date.Location = new System.Drawing.Point(41, 12);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(39, 16);
            this.lbl_from_date.TabIndex = 57;
            this.lbl_from_date.Text = "From:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnl_sold);
            this.panel2.Controls.Add(this.pnl_purchase_return);
            this.panel2.Controls.Add(this.pnl_summary);
            this.panel2.Controls.Add(this.pnl_expiry);
            this.panel2.Controls.Add(this.pnl_returned);
            this.panel2.Controls.Add(this.pnl_purchased);
            this.panel2.Controls.Add(this.pnl_low_items);
            this.panel2.Controls.Add(this.pnl_company_wise);
            this.panel2.Controls.Add(this.pnl_brand_wise);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(847, 561);
            this.panel2.TabIndex = 30;
            this.panel2.TabStop = true;
            // 
            // pnl_purchase_return
            // 
            this.pnl_purchase_return.Controls.Add(this.viewer_purchase_return);
            this.pnl_purchase_return.Location = new System.Drawing.Point(706, 239);
            this.pnl_purchase_return.Name = "pnl_purchase_return";
            this.pnl_purchase_return.Size = new System.Drawing.Size(134, 193);
            this.pnl_purchase_return.TabIndex = 29;
            this.pnl_purchase_return.Visible = false;
            // 
            // viewer_purchase_return
            // 
            this.viewer_purchase_return.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "purchases";
            reportDataSource2.Value = null;
            this.viewer_purchase_return.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_purchase_return.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Purchase_return.purchase_return_report.rdlc";
            this.viewer_purchase_return.Location = new System.Drawing.Point(0, 0);
            this.viewer_purchase_return.Name = "viewer_purchase_return";
            this.viewer_purchase_return.ServerReport.BearerToken = null;
            this.viewer_purchase_return.Size = new System.Drawing.Size(134, 193);
            this.viewer_purchase_return.TabIndex = 0;
            // 
            // pnl_summary
            // 
            this.pnl_summary.Controls.Add(this.viewer_summary);
            this.pnl_summary.Location = new System.Drawing.Point(540, 239);
            this.pnl_summary.Name = "pnl_summary";
            this.pnl_summary.Size = new System.Drawing.Size(134, 193);
            this.pnl_summary.TabIndex = 29;
            this.pnl_summary.Visible = false;
            // 
            // viewer_summary
            // 
            this.viewer_summary.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "Customer_Sales";
            reportDataSource3.Value = this.Customers_SalesBindingSource;
            reportDataSource4.Name = "customer_returns";
            reportDataSource4.Value = this.CustomerReturnsBindingSource;
            reportDataSource5.Name = "purchases";
            reportDataSource5.Value = this.PurchasesBindingSource;
            reportDataSource6.Name = "low_inventory";
            reportDataSource6.Value = this.InventoryBindingSource;
            reportDataSource7.Name = "expired_items";
            reportDataSource7.Value = this.expired_itemsBindingSource;
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource3);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource4);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource5);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource6);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource7);
            this.viewer_summary.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Summary.report_summary.rdlc";
            this.viewer_summary.Location = new System.Drawing.Point(0, 0);
            this.viewer_summary.Name = "viewer_summary";
            this.viewer_summary.ServerReport.BearerToken = null;
            this.viewer_summary.Size = new System.Drawing.Size(134, 193);
            this.viewer_summary.TabIndex = 0;
            // 
            // pnl_expiry
            // 
            this.pnl_expiry.Controls.Add(this.viewer_expiry);
            this.pnl_expiry.Location = new System.Drawing.Point(374, 239);
            this.pnl_expiry.Name = "pnl_expiry";
            this.pnl_expiry.Size = new System.Drawing.Size(134, 193);
            this.pnl_expiry.TabIndex = 27;
            this.pnl_expiry.Visible = false;
            // 
            // viewer_expiry
            // 
            this.viewer_expiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_expiry.DocumentMapWidth = 93;
            reportDataSource8.Name = "expired_items";
            reportDataSource8.Value = this.expired_itemsBindingSource;
            this.viewer_expiry.LocalReport.DataSources.Add(reportDataSource8);
            this.viewer_expiry.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_inventory_report.rdlc";
            this.viewer_expiry.Location = new System.Drawing.Point(0, 0);
            this.viewer_expiry.Name = "viewer_expiry";
            this.viewer_expiry.ServerReport.BearerToken = null;
            this.viewer_expiry.Size = new System.Drawing.Size(134, 193);
            this.viewer_expiry.TabIndex = 0;
            // 
            // pnl_returned
            // 
            this.pnl_returned.Controls.Add(this.viewer_returned);
            this.pnl_returned.Location = new System.Drawing.Point(263, 20);
            this.pnl_returned.Name = "pnl_returned";
            this.pnl_returned.Size = new System.Drawing.Size(134, 193);
            this.pnl_returned.TabIndex = 27;
            this.pnl_returned.Visible = false;
            // 
            // viewer_returned
            // 
            this.viewer_returned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_returned.DocumentMapWidth = 8;
            reportDataSource9.Name = "customer_returns";
            reportDataSource9.Value = this.CustomerReturnsBindingSource;
            this.viewer_returned.LocalReport.DataSources.Add(reportDataSource9);
            this.viewer_returned.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Returned_items.Returned_report.rdlc";
            this.viewer_returned.Location = new System.Drawing.Point(0, 0);
            this.viewer_returned.Name = "viewer_returned";
            this.viewer_returned.ServerReport.BearerToken = null;
            this.viewer_returned.Size = new System.Drawing.Size(134, 193);
            this.viewer_returned.TabIndex = 0;
            // 
            // pnl_purchased
            // 
            this.pnl_purchased.Controls.Add(this.viewer_purchased);
            this.pnl_purchased.Location = new System.Drawing.Point(615, 20);
            this.pnl_purchased.Name = "pnl_purchased";
            this.pnl_purchased.Size = new System.Drawing.Size(134, 193);
            this.pnl_purchased.TabIndex = 28;
            this.pnl_purchased.Visible = false;
            // 
            // viewer_purchased
            // 
            this.viewer_purchased.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource10.Name = "purchases";
            reportDataSource10.Value = this.PurchasesBindingSource;
            this.viewer_purchased.LocalReport.DataSources.Add(reportDataSource10);
            this.viewer_purchased.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.purchased_report.purchased_items_report.rdlc";
            this.viewer_purchased.Location = new System.Drawing.Point(0, 0);
            this.viewer_purchased.Name = "viewer_purchased";
            this.viewer_purchased.ServerReport.BearerToken = null;
            this.viewer_purchased.Size = new System.Drawing.Size(134, 193);
            this.viewer_purchased.TabIndex = 0;
            // 
            // pnl_low_items
            // 
            this.pnl_low_items.Controls.Add(this.viewer_low_items);
            this.pnl_low_items.Location = new System.Drawing.Point(439, 20);
            this.pnl_low_items.Name = "pnl_low_items";
            this.pnl_low_items.Size = new System.Drawing.Size(134, 193);
            this.pnl_low_items.TabIndex = 28;
            this.pnl_low_items.Visible = false;
            // 
            // viewer_low_items
            // 
            this.viewer_low_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_low_items.DocumentMapWidth = 8;
            reportDataSource11.Name = "low_inventory";
            reportDataSource11.Value = this.InventoryBindingSource;
            this.viewer_low_items.LocalReport.DataSources.Add(reportDataSource11);
            this.viewer_low_items.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.low_inventory.low_inventory_report.rdlc";
            this.viewer_low_items.Location = new System.Drawing.Point(0, 0);
            this.viewer_low_items.Name = "viewer_low_items";
            this.viewer_low_items.ServerReport.BearerToken = null;
            this.viewer_low_items.Size = new System.Drawing.Size(134, 193);
            this.viewer_low_items.TabIndex = 0;
            // 
            // pnl_company_wise
            // 
            this.pnl_company_wise.Controls.Add(this.viewer_company_wise);
            this.pnl_company_wise.Location = new System.Drawing.Point(208, 239);
            this.pnl_company_wise.Name = "pnl_company_wise";
            this.pnl_company_wise.Size = new System.Drawing.Size(134, 193);
            this.pnl_company_wise.TabIndex = 26;
            // 
            // viewer_company_wise
            // 
            this.viewer_company_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_company_wise.DocumentMapWidth = 21;
            reportDataSource12.Name = "expired_items";
            reportDataSource12.Value = this.expired_itemsBindingSource;
            this.viewer_company_wise.LocalReport.DataSources.Add(reportDataSource12);
            this.viewer_company_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_items_company_wise_report.rdlc";
            this.viewer_company_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_company_wise.Name = "viewer_company_wise";
            this.viewer_company_wise.ServerReport.BearerToken = null;
            this.viewer_company_wise.Size = new System.Drawing.Size(134, 193);
            this.viewer_company_wise.TabIndex = 0;
            // 
            // pnl_brand_wise
            // 
            this.pnl_brand_wise.Controls.Add(this.viewer_brand_wise);
            this.pnl_brand_wise.Location = new System.Drawing.Point(42, 239);
            this.pnl_brand_wise.Name = "pnl_brand_wise";
            this.pnl_brand_wise.Size = new System.Drawing.Size(134, 193);
            this.pnl_brand_wise.TabIndex = 26;
            // 
            // viewer_brand_wise
            // 
            this.viewer_brand_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_brand_wise.DocumentMapWidth = 21;
            reportDataSource13.Name = "expired_items";
            reportDataSource13.Value = this.expired_itemsBindingSource;
            this.viewer_brand_wise.LocalReport.DataSources.Add(reportDataSource13);
            this.viewer_brand_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_items_brand_wise_report.rdlc";
            this.viewer_brand_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_brand_wise.Name = "viewer_brand_wise";
            this.viewer_brand_wise.ServerReport.BearerToken = null;
            this.viewer_brand_wise.Size = new System.Drawing.Size(134, 193);
            this.viewer_brand_wise.TabIndex = 0;
            // 
            // pnl_sold
            // 
            this.pnl_sold.Controls.Add(this.viewer_sold);
            this.pnl_sold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_sold.Location = new System.Drawing.Point(0, 0);
            this.pnl_sold.Name = "pnl_sold";
            this.pnl_sold.Size = new System.Drawing.Size(847, 561);
            this.pnl_sold.TabIndex = 26;
            // 
            // viewer_sold
            // 
            this.viewer_sold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer_sold.DocumentMapWidth = 21;
            reportDataSource1.Name = "Customer_Sales";
            reportDataSource1.Value = this.Customers_SalesBindingSource;
            this.viewer_sold.LocalReport.DataSources.Add(reportDataSource1);
            this.viewer_sold.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Sold_items_reports.sold_items_report.rdlc";
            this.viewer_sold.Location = new System.Drawing.Point(0, 0);
            this.viewer_sold.Name = "viewer_sold";
            this.viewer_sold.ServerReport.BearerToken = null;
            this.viewer_sold.Size = new System.Drawing.Size(847, 561);
            this.viewer_sold.TabIndex = 0;
            // 
            // Purchase_returnBindingSource
            // 
            this.Purchase_returnBindingSource.DataMember = "Purchase_return";
            this.Purchase_returnBindingSource.DataSource = this.stock_ds;
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
            this.guna2Panel2.TabIndex = 110;
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
            this.guna2Panel6.Controls.Add(this.panel2);
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
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.chk_company_wise);
            this.panel7.Controls.Add(this.chk_brand_wise);
            this.panel7.Controls.Add(this.lbl_brand);
            this.panel7.Controls.Add(this.chk_over_all);
            this.panel7.Controls.Add(this.view_button);
            this.panel7.Controls.Add(this.txt_brand);
            this.panel7.Controls.Add(this.guna2Separator2);
            this.panel7.Controls.Add(this.FromDate);
            this.panel7.Controls.Add(this.lbl_from_date);
            this.panel7.Controls.Add(this.lbl_to_date);
            this.panel7.Controls.Add(this.lbl_company);
            this.panel7.Controls.Add(this.ToDate);
            this.panel7.Controls.Add(this.txt_company);
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
            this.sidePanel.Controls.Add(this.btn_summary);
            this.sidePanel.Controls.Add(this.btn_expiry);
            this.sidePanel.Controls.Add(this.btn_purchased);
            this.sidePanel.Controls.Add(this.btn_low_items);
            this.sidePanel.Controls.Add(this.btn_returned);
            this.sidePanel.Controls.Add(this.btn_sold);
            this.sidePanel.Controls.Add(this.panel8);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Padding = new System.Windows.Forms.Padding(10);
            this.sidePanel.Size = new System.Drawing.Size(127, 736);
            this.sidePanel.TabIndex = 2;
            // 
            // btn_returned
            // 
            this.btn_returned.BackColor = System.Drawing.Color.Transparent;
            this.btn_returned.BorderColor = System.Drawing.Color.Transparent;
            this.btn_returned.BorderRadius = 15;
            this.btn_returned.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_returned.CustomBorderColor = System.Drawing.Color.White;
            this.btn_returned.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_returned.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_returned.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_returned.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_returned.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_returned.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_returned.FillColor = System.Drawing.Color.White;
            this.btn_returned.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_returned.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_returned.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_returned.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_returned.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_returned.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_returned.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_returned.Location = new System.Drawing.Point(10, 170);
            this.btn_returned.Name = "btn_returned";
            this.btn_returned.Size = new System.Drawing.Size(107, 39);
            this.btn_returned.TabIndex = 536;
            this.btn_returned.Text = "Returned Items";
            this.btn_returned.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_returned.Click += new System.EventHandler(this.btn_returned_CheckedChanged);
            // 
            // btn_sold
            // 
            this.btn_sold.BackColor = System.Drawing.Color.Transparent;
            this.btn_sold.BorderColor = System.Drawing.Color.Transparent;
            this.btn_sold.BorderRadius = 15;
            this.btn_sold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_sold.CustomBorderColor = System.Drawing.Color.White;
            this.btn_sold.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_sold.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_sold.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_sold.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_sold.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_sold.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_sold.FillColor = System.Drawing.Color.White;
            this.btn_sold.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_sold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_sold.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_sold.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_sold.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_sold.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_sold.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_sold.Location = new System.Drawing.Point(10, 131);
            this.btn_sold.Name = "btn_sold";
            this.btn_sold.Size = new System.Drawing.Size(107, 39);
            this.btn_sold.TabIndex = 535;
            this.btn_sold.Text = "Sold Items";
            this.btn_sold.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_sold.Click += new System.EventHandler(this.btn_sold_CheckedChanged);
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
            // btn_purchased
            // 
            this.btn_purchased.BackColor = System.Drawing.Color.Transparent;
            this.btn_purchased.BorderColor = System.Drawing.Color.Transparent;
            this.btn_purchased.BorderRadius = 15;
            this.btn_purchased.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_purchased.CustomBorderColor = System.Drawing.Color.White;
            this.btn_purchased.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_purchased.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_purchased.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_purchased.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_purchased.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_purchased.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_purchased.FillColor = System.Drawing.Color.White;
            this.btn_purchased.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_purchased.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_purchased.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_purchased.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_purchased.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_purchased.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_purchased.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_purchased.Location = new System.Drawing.Point(10, 248);
            this.btn_purchased.Name = "btn_purchased";
            this.btn_purchased.Size = new System.Drawing.Size(107, 39);
            this.btn_purchased.TabIndex = 538;
            this.btn_purchased.Text = "Purchased";
            this.btn_purchased.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_purchased.Click += new System.EventHandler(this.btn_purchased_CheckedChanged);
            // 
            // btn_low_items
            // 
            this.btn_low_items.BackColor = System.Drawing.Color.Transparent;
            this.btn_low_items.BorderColor = System.Drawing.Color.Transparent;
            this.btn_low_items.BorderRadius = 15;
            this.btn_low_items.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_low_items.CustomBorderColor = System.Drawing.Color.White;
            this.btn_low_items.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_low_items.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_low_items.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_low_items.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_low_items.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_low_items.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_low_items.FillColor = System.Drawing.Color.White;
            this.btn_low_items.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_low_items.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_low_items.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_low_items.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_low_items.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_low_items.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_low_items.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_low_items.Location = new System.Drawing.Point(10, 209);
            this.btn_low_items.Name = "btn_low_items";
            this.btn_low_items.Size = new System.Drawing.Size(107, 39);
            this.btn_low_items.TabIndex = 537;
            this.btn_low_items.Text = "Low Inventory";
            this.btn_low_items.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_low_items.Click += new System.EventHandler(this.btn_low_items_CheckedChanged);
            // 
            // btn_summary
            // 
            this.btn_summary.BackColor = System.Drawing.Color.Transparent;
            this.btn_summary.BorderColor = System.Drawing.Color.Transparent;
            this.btn_summary.BorderRadius = 15;
            this.btn_summary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_summary.CustomBorderColor = System.Drawing.Color.White;
            this.btn_summary.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_summary.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_summary.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_summary.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_summary.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_summary.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_summary.FillColor = System.Drawing.Color.White;
            this.btn_summary.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_summary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_summary.HoverState.BorderColor = System.Drawing.Color.White;
            this.btn_summary.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.btn_summary.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btn_summary.HoverState.ForeColor = System.Drawing.Color.White;
            this.btn_summary.ImageSize = new System.Drawing.Size(18, 18);
            this.btn_summary.Location = new System.Drawing.Point(10, 326);
            this.btn_summary.Name = "btn_summary";
            this.btn_summary.Size = new System.Drawing.Size(107, 39);
            this.btn_summary.TabIndex = 540;
            this.btn_summary.Text = "Summary";
            this.btn_summary.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_summary.Click += new System.EventHandler(this.btn_summary_CheckedChanged);
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
            this.btn_expiry.Location = new System.Drawing.Point(10, 287);
            this.btn_expiry.Name = "btn_expiry";
            this.btn_expiry.Size = new System.Drawing.Size(107, 39);
            this.btn_expiry.TabIndex = 539;
            this.btn_expiry.Text = "Expired Items";
            this.btn_expiry.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_expiry.Click += new System.EventHandler(this.btn_expiry_CheckedChanged);
            // 
            // form_stock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_stock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Customers_SalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturnsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchasesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expired_itemsBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnl_purchase_return.ResumeLayout(false);
            this.pnl_summary.ResumeLayout(false);
            this.pnl_expiry.ResumeLayout(false);
            this.pnl_returned.ResumeLayout(false);
            this.pnl_purchased.ResumeLayout(false);
            this.pnl_low_items.ResumeLayout(false);
            this.pnl_company_wise.ResumeLayout(false);
            this.pnl_brand_wise.ResumeLayout(false);
            this.pnl_sold.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Purchase_returnBindingSource)).EndInit();
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
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_returned;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_returned;
        private System.Windows.Forms.Panel pnl_purchased;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_purchased;
        private System.Windows.Forms.Panel pnl_low_items;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_low_items;
        private System.Windows.Forms.Panel pnl_sold;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_sold;
        private System.Windows.Forms.Panel pnl_summary;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_summary;
        private System.Windows.Forms.Label lbl_company;
        private System.Windows.Forms.ComboBox txt_company;
        private System.Windows.Forms.Panel pnl_expiry;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_expiry;
        private System.Windows.Forms.CheckBox chk_company_wise;
        private System.Windows.Forms.CheckBox chk_brand_wise;
        private System.Windows.Forms.CheckBox chk_over_all;
        private System.Windows.Forms.Label lbl_brand;
        private System.Windows.Forms.ComboBox txt_brand;
        private System.Windows.Forms.Panel pnl_company_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_company_wise;
        private System.Windows.Forms.Panel pnl_brand_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_brand_wise;
        private System.Windows.Forms.Panel pnl_purchase_return;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_purchase_return;
        private System.Windows.Forms.BindingSource Customers_SalesBindingSource;
        private stock_ds stock_ds;
        private System.Windows.Forms.BindingSource CustomerReturnsBindingSource;
        private System.Windows.Forms.BindingSource InventoryBindingSource;
        private System.Windows.Forms.BindingSource PurchasesBindingSource;
        private System.Windows.Forms.BindingSource Purchase_returnBindingSource;
        private System.Windows.Forms.BindingSource expired_itemsBindingSource;
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
        private Guna.UI2.WinForms.Guna2Button btn_returned;
        private Guna.UI2.WinForms.Guna2Button btn_sold;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_shop_title;
        private Guna.UI2.WinForms.Guna2PictureBox logo_img2;
        private Guna.UI2.WinForms.Guna2Button btn_summary;
        private Guna.UI2.WinForms.Guna2Button btn_expiry;
        private Guna.UI2.WinForms.Guna2Button btn_purchased;
        private Guna.UI2.WinForms.Guna2Button btn_low_items;
    }
}