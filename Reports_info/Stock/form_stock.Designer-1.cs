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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_stock));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource13 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource14 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource17 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource18 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource16 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource15 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.view_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh_button = new System.Windows.Forms.Button();
            this.btn_returned = new System.Windows.Forms.RadioButton();
            this.btn_sold = new System.Windows.Forms.RadioButton();
            this.Closebutton = new System.Windows.Forms.Button();
            this.btn_low_items = new System.Windows.Forms.RadioButton();
            this.btn_summary = new System.Windows.Forms.RadioButton();
            this.btn_purchased = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_summary = new System.Windows.Forms.Panel();
            this.viewer_summary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_returned = new System.Windows.Forms.Panel();
            this.viewer_returned = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_purchased = new System.Windows.Forms.Panel();
            this.viewer_purchased = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_low_items = new System.Windows.Forms.Panel();
            this.viewer_low_items = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_sold = new System.Windows.Forms.Panel();
            this.viewer_sold = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btn_expiry = new System.Windows.Forms.RadioButton();
            this.lbl_company = new System.Windows.Forms.Label();
            this.txt_company = new System.Windows.Forms.ComboBox();
            this.pnl_expiry = new System.Windows.Forms.Panel();
            this.viewer_expiry = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chk_over_all = new System.Windows.Forms.CheckBox();
            this.chk_brand_wise = new System.Windows.Forms.CheckBox();
            this.chk_company_wise = new System.Windows.Forms.CheckBox();
            this.lbl_brand = new System.Windows.Forms.Label();
            this.txt_brand = new System.Windows.Forms.ComboBox();
            this.pnl_brand_wise = new System.Windows.Forms.Panel();
            this.viewer_brand_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_company_wise = new System.Windows.Forms.Panel();
            this.viewer_company_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.Loader_SalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stock_ds = new Reports_info.Stock.stock_ds();
            this.Customers_SalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LoaderReturnsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CustomerReturnsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PurchasesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Low_inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.exp_over_allBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.exp_company_wiseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.exp_brand_wiseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_summary.SuspendLayout();
            this.pnl_returned.SuspendLayout();
            this.pnl_purchased.SuspendLayout();
            this.pnl_low_items.SuspendLayout();
            this.pnl_sold.SuspendLayout();
            this.pnl_expiry.SuspendLayout();
            this.pnl_brand_wise.SuspendLayout();
            this.pnl_company_wise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loader_SalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers_SalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoaderReturnsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturnsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchasesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Low_inventoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_over_allBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_company_wiseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_brand_wiseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbl_brand);
            this.panel5.Controls.Add(this.txt_brand);
            this.panel5.Controls.Add(this.chk_company_wise);
            this.panel5.Controls.Add(this.chk_brand_wise);
            this.panel5.Controls.Add(this.chk_over_all);
            this.panel5.Controls.Add(this.lbl_company);
            this.panel5.Controls.Add(this.txt_company);
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
            this.panel5.Size = new System.Drawing.Size(1024, 81);
            this.panel5.TabIndex = 29;
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(359, 14);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(207, 24);
            this.ToDate.TabIndex = 58;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(89, 14);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(208, 24);
            this.FromDate.TabIndex = 58;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_to_date.Location = new System.Drawing.Point(312, 18);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(28, 17);
            this.lbl_to_date.TabIndex = 57;
            this.lbl_to_date.Text = "To";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_from_date.Location = new System.Drawing.Point(32, 17);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(54, 17);
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
            this.view_button.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.Location = new System.Drawing.Point(593, 14);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(120, 60);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "View";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.refresh_button);
            this.panel1.Controls.Add(this.btn_returned);
            this.panel1.Controls.Add(this.btn_sold);
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.btn_low_items);
            this.panel1.Controls.Add(this.btn_expiry);
            this.panel1.Controls.Add(this.btn_summary);
            this.panel1.Controls.Add(this.btn_purchased);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 33);
            this.panel1.TabIndex = 28;
            // 
            // refresh_button
            // 
            this.refresh_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refresh_button.FlatAppearance.BorderSize = 0;
            this.refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.refresh_button.ForeColor = System.Drawing.Color.White;
            this.refresh_button.Image = ((System.Drawing.Image)(resources.GetObject("refresh_button.Image")));
            this.refresh_button.Location = new System.Drawing.Point(929, -2);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(46, 37);
            this.refresh_button.TabIndex = 100;
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // btn_returned
            // 
            this.btn_returned.AutoSize = true;
            this.btn_returned.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_returned.ForeColor = System.Drawing.Color.White;
            this.btn_returned.Location = new System.Drawing.Point(122, 10);
            this.btn_returned.Name = "btn_returned";
            this.btn_returned.Size = new System.Drawing.Size(139, 20);
            this.btn_returned.TabIndex = 98;
            this.btn_returned.Text = "Returned Items";
            this.btn_returned.UseVisualStyleBackColor = true;
            this.btn_returned.CheckedChanged += new System.EventHandler(this.btn_returned_CheckedChanged);
            // 
            // btn_sold
            // 
            this.btn_sold.AutoSize = true;
            this.btn_sold.Checked = true;
            this.btn_sold.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_sold.ForeColor = System.Drawing.Color.White;
            this.btn_sold.Location = new System.Drawing.Point(7, 10);
            this.btn_sold.Name = "btn_sold";
            this.btn_sold.Size = new System.Drawing.Size(104, 20);
            this.btn_sold.TabIndex = 98;
            this.btn_sold.TabStop = true;
            this.btn_sold.Text = "Sold Items";
            this.btn_sold.UseVisualStyleBackColor = true;
            this.btn_sold.CheckedChanged += new System.EventHandler(this.btn_sold_CheckedChanged);
            // 
            // Closebutton
            // 
            this.Closebutton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Closebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.Closebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Closebutton.FlatAppearance.BorderSize = 0;
            this.Closebutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(116)))), ((int)(((byte)(163)))));
            this.Closebutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.Closebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Closebutton.Font = new System.Drawing.Font("Century Gothic", 17F, System.Drawing.FontStyle.Bold);
            this.Closebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Closebutton.Image = ((System.Drawing.Image)(resources.GetObject("Closebutton.Image")));
            this.Closebutton.Location = new System.Drawing.Point(978, -2);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(46, 37);
            this.Closebutton.TabIndex = 15;
            this.Closebutton.UseVisualStyleBackColor = false;
            this.Closebutton.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // btn_low_items
            // 
            this.btn_low_items.AutoSize = true;
            this.btn_low_items.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_low_items.ForeColor = System.Drawing.Color.White;
            this.btn_low_items.Location = new System.Drawing.Point(272, 10);
            this.btn_low_items.Name = "btn_low_items";
            this.btn_low_items.Size = new System.Drawing.Size(102, 20);
            this.btn_low_items.TabIndex = 99;
            this.btn_low_items.Text = "Low Items";
            this.btn_low_items.UseVisualStyleBackColor = true;
            this.btn_low_items.CheckedChanged += new System.EventHandler(this.btn_low_items_CheckedChanged);
            // 
            // btn_summary
            // 
            this.btn_summary.AutoSize = true;
            this.btn_summary.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_summary.ForeColor = System.Drawing.Color.White;
            this.btn_summary.Location = new System.Drawing.Point(688, 10);
            this.btn_summary.Name = "btn_summary";
            this.btn_summary.Size = new System.Drawing.Size(140, 20);
            this.btn_summary.TabIndex = 99;
            this.btn_summary.Text = "Stock Summary";
            this.btn_summary.UseVisualStyleBackColor = true;
            this.btn_summary.CheckedChanged += new System.EventHandler(this.btn_summary_CheckedChanged);
            // 
            // btn_purchased
            // 
            this.btn_purchased.AutoSize = true;
            this.btn_purchased.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_purchased.ForeColor = System.Drawing.Color.White;
            this.btn_purchased.Location = new System.Drawing.Point(386, 10);
            this.btn_purchased.Name = "btn_purchased";
            this.btn_purchased.Size = new System.Drawing.Size(149, 20);
            this.btn_purchased.TabIndex = 99;
            this.btn_purchased.Text = "Purchased Items";
            this.btn_purchased.UseVisualStyleBackColor = true;
            this.btn_purchased.CheckedChanged += new System.EventHandler(this.btn_purchased_CheckedChanged);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.pnl_summary);
            this.panel2.Controls.Add(this.pnl_expiry);
            this.panel2.Controls.Add(this.pnl_returned);
            this.panel2.Controls.Add(this.pnl_purchased);
            this.panel2.Controls.Add(this.pnl_low_items);
            this.panel2.Controls.Add(this.pnl_company_wise);
            this.panel2.Controls.Add(this.pnl_brand_wise);
            this.panel2.Controls.Add(this.pnl_sold);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 622);
            this.panel2.TabIndex = 30;
            // 
            // pnl_summary
            // 
            this.pnl_summary.Controls.Add(this.viewer_summary);
            this.pnl_summary.Location = new System.Drawing.Point(780, 306);
            this.pnl_summary.Name = "pnl_summary";
            this.pnl_summary.Size = new System.Drawing.Size(232, 286);
            this.pnl_summary.TabIndex = 29;
            this.pnl_summary.Visible = false;
            // 
            // viewer_summary
            // 
            reportDataSource1.Name = "Loader_Sales";
            reportDataSource1.Value = this.Loader_SalesBindingSource;
            reportDataSource2.Name = "customers_sales";
            reportDataSource2.Value = this.Customers_SalesBindingSource;
            reportDataSource3.Name = "loader_returns";
            reportDataSource3.Value = this.LoaderReturnsBindingSource;
            reportDataSource4.Name = "customer_returns";
            reportDataSource4.Value = this.CustomerReturnsBindingSource;
            reportDataSource5.Name = "purchases";
            reportDataSource5.Value = this.PurchasesBindingSource;
            reportDataSource6.Name = "low_inventory";
            reportDataSource6.Value = this.Low_inventoryBindingSource;
            reportDataSource7.Name = "exp_over_all";
            reportDataSource7.Value = this.exp_over_allBindingSource;
            reportDataSource8.Name = "exp_company_wise";
            reportDataSource8.Value = this.exp_company_wiseBindingSource;
            reportDataSource9.Name = "exp_brand_wise";
            reportDataSource9.Value = this.exp_brand_wiseBindingSource;
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource1);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource3);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource4);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource5);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource6);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource7);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource8);
            this.viewer_summary.LocalReport.DataSources.Add(reportDataSource9);
            this.viewer_summary.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Summary.report_summary.rdlc";
            this.viewer_summary.Location = new System.Drawing.Point(0, 2);
            this.viewer_summary.Name = "viewer_summary";
            this.viewer_summary.Size = new System.Drawing.Size(180, 226);
            this.viewer_summary.TabIndex = 0;
            // 
            // pnl_returned
            // 
            this.pnl_returned.Controls.Add(this.viewer_returned);
            this.pnl_returned.Location = new System.Drawing.Point(267, 17);
            this.pnl_returned.Name = "pnl_returned";
            this.pnl_returned.Size = new System.Drawing.Size(219, 258);
            this.pnl_returned.TabIndex = 27;
            this.pnl_returned.Visible = false;
            // 
            // viewer_returned
            // 
            this.viewer_returned.DocumentMapWidth = 8;
            reportDataSource11.Name = "loader_returns";
            reportDataSource11.Value = this.LoaderReturnsBindingSource;
            reportDataSource12.Name = "customer_returns";
            reportDataSource12.Value = this.CustomerReturnsBindingSource;
            this.viewer_returned.LocalReport.DataSources.Add(reportDataSource11);
            this.viewer_returned.LocalReport.DataSources.Add(reportDataSource12);
            this.viewer_returned.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Returned_items.Returned_report.rdlc";
            this.viewer_returned.Location = new System.Drawing.Point(3, 3);
            this.viewer_returned.Name = "viewer_returned";
            this.viewer_returned.Size = new System.Drawing.Size(170, 226);
            this.viewer_returned.TabIndex = 0;
            // 
            // pnl_purchased
            // 
            this.pnl_purchased.Controls.Add(this.viewer_purchased);
            this.pnl_purchased.Location = new System.Drawing.Point(777, 17);
            this.pnl_purchased.Name = "pnl_purchased";
            this.pnl_purchased.Size = new System.Drawing.Size(232, 258);
            this.pnl_purchased.TabIndex = 28;
            this.pnl_purchased.Visible = false;
            // 
            // viewer_purchased
            // 
            reportDataSource13.Name = "purchases";
            reportDataSource13.Value = this.PurchasesBindingSource;
            this.viewer_purchased.LocalReport.DataSources.Add(reportDataSource13);
            this.viewer_purchased.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.purchased_report.purchased_items_report.rdlc";
            this.viewer_purchased.Location = new System.Drawing.Point(0, 3);
            this.viewer_purchased.Name = "viewer_purchased";
            this.viewer_purchased.Size = new System.Drawing.Size(182, 226);
            this.viewer_purchased.TabIndex = 0;
            // 
            // pnl_low_items
            // 
            this.pnl_low_items.Controls.Add(this.viewer_low_items);
            this.pnl_low_items.Location = new System.Drawing.Point(517, 17);
            this.pnl_low_items.Name = "pnl_low_items";
            this.pnl_low_items.Size = new System.Drawing.Size(214, 258);
            this.pnl_low_items.TabIndex = 28;
            this.pnl_low_items.Visible = false;
            // 
            // viewer_low_items
            // 
            this.viewer_low_items.DocumentMapWidth = 8;
            reportDataSource14.Name = "low_inventory";
            reportDataSource14.Value = this.Low_inventoryBindingSource;
            this.viewer_low_items.LocalReport.DataSources.Add(reportDataSource14);
            this.viewer_low_items.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.low_inventory.low_inventory_report.rdlc";
            this.viewer_low_items.Location = new System.Drawing.Point(0, 3);
            this.viewer_low_items.Name = "viewer_low_items";
            this.viewer_low_items.Size = new System.Drawing.Size(171, 226);
            this.viewer_low_items.TabIndex = 0;
            // 
            // pnl_sold
            // 
            this.pnl_sold.Controls.Add(this.viewer_sold);
            this.pnl_sold.Location = new System.Drawing.Point(8, 17);
            this.pnl_sold.Name = "pnl_sold";
            this.pnl_sold.Size = new System.Drawing.Size(214, 258);
            this.pnl_sold.TabIndex = 26;
            // 
            // viewer_sold
            // 
            this.viewer_sold.DocumentMapWidth = 21;
            reportDataSource17.Name = "Loader_Sales";
            reportDataSource17.Value = this.Loader_SalesBindingSource;
            reportDataSource18.Name = "customers_sales";
            reportDataSource18.Value = this.Customers_SalesBindingSource;
            this.viewer_sold.LocalReport.DataSources.Add(reportDataSource17);
            this.viewer_sold.LocalReport.DataSources.Add(reportDataSource18);
            this.viewer_sold.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Sold_items_reports.sold_items_report.rdlc";
            this.viewer_sold.Location = new System.Drawing.Point(0, 0);
            this.viewer_sold.Name = "viewer_sold";
            this.viewer_sold.Size = new System.Drawing.Size(183, 229);
            this.viewer_sold.TabIndex = 0;
            // 
            // btn_expiry
            // 
            this.btn_expiry.AutoSize = true;
            this.btn_expiry.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_expiry.ForeColor = System.Drawing.Color.White;
            this.btn_expiry.Location = new System.Drawing.Point(548, 10);
            this.btn_expiry.Name = "btn_expiry";
            this.btn_expiry.Size = new System.Drawing.Size(128, 20);
            this.btn_expiry.TabIndex = 99;
            this.btn_expiry.Text = "Expired Items";
            this.btn_expiry.UseVisualStyleBackColor = true;
            this.btn_expiry.CheckedChanged += new System.EventHandler(this.btn_expiry_CheckedChanged);
            // 
            // lbl_company
            // 
            this.lbl_company.AutoSize = true;
            this.lbl_company.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.lbl_company.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_company.Location = new System.Drawing.Point(5, 53);
            this.lbl_company.Name = "lbl_company";
            this.lbl_company.Size = new System.Drawing.Size(82, 16);
            this.lbl_company.TabIndex = 81;
            this.lbl_company.Text = "Company:";
            // 
            // txt_company
            // 
            this.txt_company.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_company.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_company.BackColor = System.Drawing.SystemColors.Window;
            this.txt_company.Font = new System.Drawing.Font("Verdana", 10F);
            this.txt_company.ForeColor = System.Drawing.Color.Black;
            this.txt_company.FormattingEnabled = true;
            this.txt_company.IntegralHeight = false;
            this.txt_company.Location = new System.Drawing.Point(89, 49);
            this.txt_company.MaxLength = 14;
            this.txt_company.Name = "txt_company";
            this.txt_company.Size = new System.Drawing.Size(208, 24);
            this.txt_company.TabIndex = 82;
            // 
            // pnl_expiry
            // 
            this.pnl_expiry.Controls.Add(this.viewer_expiry);
            this.pnl_expiry.Location = new System.Drawing.Point(519, 303);
            this.pnl_expiry.Name = "pnl_expiry";
            this.pnl_expiry.Size = new System.Drawing.Size(212, 286);
            this.pnl_expiry.TabIndex = 27;
            this.pnl_expiry.Visible = false;
            // 
            // viewer_expiry
            // 
            reportDataSource10.Name = "low_inventory";
            reportDataSource10.Value = this.Low_inventoryBindingSource;
            this.viewer_expiry.LocalReport.DataSources.Add(reportDataSource10);
            this.viewer_expiry.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_inventory_report.rdlc";
            this.viewer_expiry.Location = new System.Drawing.Point(3, 3);
            this.viewer_expiry.Name = "viewer_expiry";
            this.viewer_expiry.Size = new System.Drawing.Size(162, 226);
            this.viewer_expiry.TabIndex = 0;
            // 
            // chk_over_all
            // 
            this.chk_over_all.AutoSize = true;
            this.chk_over_all.Checked = true;
            this.chk_over_all.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_over_all.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.chk_over_all.Location = new System.Drawing.Point(734, 56);
            this.chk_over_all.Name = "chk_over_all";
            this.chk_over_all.Size = new System.Drawing.Size(78, 17);
            this.chk_over_all.TabIndex = 101;
            this.chk_over_all.Text = "Over All";
            this.chk_over_all.UseVisualStyleBackColor = true;
            this.chk_over_all.CheckedChanged += new System.EventHandler(this.chk_over_all_CheckedChanged);
            // 
            // chk_brand_wise
            // 
            this.chk_brand_wise.AutoSize = true;
            this.chk_brand_wise.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.chk_brand_wise.Location = new System.Drawing.Point(734, 34);
            this.chk_brand_wise.Name = "chk_brand_wise";
            this.chk_brand_wise.Size = new System.Drawing.Size(99, 17);
            this.chk_brand_wise.TabIndex = 101;
            this.chk_brand_wise.Text = "Brand Wise";
            this.chk_brand_wise.UseVisualStyleBackColor = true;
            this.chk_brand_wise.CheckedChanged += new System.EventHandler(this.chk_brand_wise_CheckedChanged);
            // 
            // chk_company_wise
            // 
            this.chk_company_wise.AutoSize = true;
            this.chk_company_wise.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.chk_company_wise.Location = new System.Drawing.Point(734, 11);
            this.chk_company_wise.Name = "chk_company_wise";
            this.chk_company_wise.Size = new System.Drawing.Size(121, 17);
            this.chk_company_wise.TabIndex = 101;
            this.chk_company_wise.Text = "Company Wise";
            this.chk_company_wise.UseVisualStyleBackColor = true;
            this.chk_company_wise.CheckedChanged += new System.EventHandler(this.chk_company_wise_CheckedChanged);
            // 
            // lbl_brand
            // 
            this.lbl_brand.AutoSize = true;
            this.lbl_brand.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.lbl_brand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_brand.Location = new System.Drawing.Point(30, 53);
            this.lbl_brand.Name = "lbl_brand";
            this.lbl_brand.Size = new System.Drawing.Size(56, 16);
            this.lbl_brand.TabIndex = 102;
            this.lbl_brand.Text = "Brand:";
            // 
            // txt_brand
            // 
            this.txt_brand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_brand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txt_brand.BackColor = System.Drawing.SystemColors.Window;
            this.txt_brand.Font = new System.Drawing.Font("Verdana", 10F);
            this.txt_brand.ForeColor = System.Drawing.Color.Black;
            this.txt_brand.FormattingEnabled = true;
            this.txt_brand.IntegralHeight = false;
            this.txt_brand.Location = new System.Drawing.Point(89, 49);
            this.txt_brand.MaxLength = 14;
            this.txt_brand.Name = "txt_brand";
            this.txt_brand.Size = new System.Drawing.Size(208, 24);
            this.txt_brand.TabIndex = 103;
            // 
            // pnl_brand_wise
            // 
            this.pnl_brand_wise.Controls.Add(this.viewer_brand_wise);
            this.pnl_brand_wise.Location = new System.Drawing.Point(12, 303);
            this.pnl_brand_wise.Name = "pnl_brand_wise";
            this.pnl_brand_wise.Size = new System.Drawing.Size(210, 289);
            this.pnl_brand_wise.TabIndex = 26;
            // 
            // viewer_brand_wise
            // 
            this.viewer_brand_wise.DocumentMapWidth = 21;
            reportDataSource16.Name = "low_inventory";
            reportDataSource16.Value = this.Low_inventoryBindingSource;
            this.viewer_brand_wise.LocalReport.DataSources.Add(reportDataSource16);
            this.viewer_brand_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_items_brand_wise_report.rdlc";
            this.viewer_brand_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_brand_wise.Name = "viewer_brand_wise";
            this.viewer_brand_wise.Size = new System.Drawing.Size(173, 252);
            this.viewer_brand_wise.TabIndex = 0;
            // 
            // pnl_company_wise
            // 
            this.pnl_company_wise.Controls.Add(this.viewer_company_wise);
            this.pnl_company_wise.Location = new System.Drawing.Point(267, 303);
            this.pnl_company_wise.Name = "pnl_company_wise";
            this.pnl_company_wise.Size = new System.Drawing.Size(219, 289);
            this.pnl_company_wise.TabIndex = 26;
            // 
            // viewer_company_wise
            // 
            this.viewer_company_wise.DocumentMapWidth = 21;
            reportDataSource15.Name = "low_inventory";
            reportDataSource15.Value = this.Low_inventoryBindingSource;
            this.viewer_company_wise.LocalReport.DataSources.Add(reportDataSource15);
            this.viewer_company_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Stock.Expired_items.expired_items_company_wise_report.rdlc";
            this.viewer_company_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_company_wise.Name = "viewer_company_wise";
            this.viewer_company_wise.Size = new System.Drawing.Size(173, 252);
            this.viewer_company_wise.TabIndex = 0;
            // 
            // Loader_SalesBindingSource
            // 
            this.Loader_SalesBindingSource.DataMember = "Loader_Sales";
            this.Loader_SalesBindingSource.DataSource = this.stock_ds;
            // 
            // stock_ds
            // 
            this.stock_ds.DataSetName = "stock_ds";
            this.stock_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Customers_SalesBindingSource
            // 
            this.Customers_SalesBindingSource.DataMember = "Customers_Sales";
            this.Customers_SalesBindingSource.DataSource = this.stock_ds;
            // 
            // LoaderReturnsBindingSource
            // 
            this.LoaderReturnsBindingSource.DataMember = "LoaderReturns";
            this.LoaderReturnsBindingSource.DataSource = this.stock_ds;
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
            // Low_inventoryBindingSource
            // 
            this.Low_inventoryBindingSource.DataMember = "Low_inventory";
            this.Low_inventoryBindingSource.DataSource = this.stock_ds;
            // 
            // exp_over_allBindingSource
            // 
            this.exp_over_allBindingSource.DataMember = "exp_over_all";
            this.exp_over_allBindingSource.DataSource = this.stock_ds;
            // 
            // exp_company_wiseBindingSource
            // 
            this.exp_company_wiseBindingSource.DataMember = "exp_company_wise";
            this.exp_company_wiseBindingSource.DataSource = this.stock_ds;
            // 
            // exp_brand_wiseBindingSource
            // 
            this.exp_brand_wiseBindingSource.DataMember = "exp_brand_wise";
            this.exp_brand_wiseBindingSource.DataSource = this.stock_ds;
            // 
            // form_stock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_stock";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_stock_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnl_summary.ResumeLayout(false);
            this.pnl_returned.ResumeLayout(false);
            this.pnl_purchased.ResumeLayout(false);
            this.pnl_low_items.ResumeLayout(false);
            this.pnl_sold.ResumeLayout(false);
            this.pnl_expiry.ResumeLayout(false);
            this.pnl_brand_wise.ResumeLayout(false);
            this.pnl_company_wise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Loader_SalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stock_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Customers_SalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoaderReturnsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerReturnsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurchasesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Low_inventoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_over_allBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_company_wiseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exp_brand_wiseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.RadioButton btn_returned;
        private System.Windows.Forms.RadioButton btn_sold;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.RadioButton btn_low_items;
        private System.Windows.Forms.RadioButton btn_purchased;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_returned;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_returned;
        private System.Windows.Forms.Panel pnl_purchased;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_purchased;
        private System.Windows.Forms.Panel pnl_low_items;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_low_items;
        private System.Windows.Forms.Panel pnl_sold;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_sold;
        private System.Windows.Forms.RadioButton btn_summary;
        private System.Windows.Forms.Panel pnl_summary;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_summary;
        private System.Windows.Forms.BindingSource Loader_SalesBindingSource;
        private stock_ds stock_ds;
        private System.Windows.Forms.BindingSource Customers_SalesBindingSource;
        private System.Windows.Forms.BindingSource LoaderReturnsBindingSource;
        private System.Windows.Forms.BindingSource CustomerReturnsBindingSource;
        private System.Windows.Forms.BindingSource Low_inventoryBindingSource;
        private System.Windows.Forms.BindingSource PurchasesBindingSource;
        private System.Windows.Forms.RadioButton btn_expiry;
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
        private System.Windows.Forms.BindingSource exp_over_allBindingSource;
        private System.Windows.Forms.BindingSource exp_company_wiseBindingSource;
        private System.Windows.Forms.BindingSource exp_brand_wiseBindingSource;
    }
}