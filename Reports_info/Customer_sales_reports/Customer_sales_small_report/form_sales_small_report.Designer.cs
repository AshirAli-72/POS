namespace Reports_info.Customer_sales_reports.Customer_sales_small_report
{
    partial class form_sales_small_report
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_sales_small_report));
            this.Customer_SalesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.small_report_ds = new Reports_info.Customer_sales_reports.Customer_sales_small_report.small_report_ds();
            this.pnl_bill_wise = new System.Windows.Forms.Panel();
            this.viewer_bill_wise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_all_transaction = new System.Windows.Forms.Panel();
            this.viewer_all_transaction = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnl_date_wise = new System.Windows.Forms.Panel();
            this.Viewer_datewise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_billNo = new System.Windows.Forms.Label();
            this.txt_billNo = new System.Windows.Forms.TextBox();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.customer_code_text = new System.Windows.Forms.ComboBox();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_to_date = new System.Windows.Forms.Label();
            this.lbl_cus_name = new System.Windows.Forms.Label();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.view_button = new System.Windows.Forms.Button();
            this.lbl_cus_code = new System.Windows.Forms.Label();
            this.customer_name_text = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh_button = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.btn_bill_wise = new System.Windows.Forms.RadioButton();
            this.all_transactions_button = new System.Windows.Forms.RadioButton();
            this.date_wise_button = new System.Windows.Forms.RadioButton();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Customer_SalesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.small_report_ds)).BeginInit();
            this.pnl_bill_wise.SuspendLayout();
            this.pnl_all_transaction.SuspendLayout();
            this.pnl_date_wise.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Customer_SalesBindingSource
            // 
            this.Customer_SalesBindingSource.DataMember = "Customer_Sales";
            this.Customer_SalesBindingSource.DataSource = this.small_report_ds;
            // 
            // small_report_ds
            // 
            this.small_report_ds.DataSetName = "small_report_ds";
            this.small_report_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnl_bill_wise
            // 
            this.pnl_bill_wise.Controls.Add(this.viewer_bill_wise);
            this.pnl_bill_wise.Location = new System.Drawing.Point(616, 115);
            this.pnl_bill_wise.Name = "pnl_bill_wise";
            this.pnl_bill_wise.Size = new System.Drawing.Size(300, 429);
            this.pnl_bill_wise.TabIndex = 28;
            this.pnl_bill_wise.Visible = false;
            // 
            // viewer_bill_wise
            // 
            reportDataSource1.Name = "cus_sales";
            reportDataSource1.Value = this.Customer_SalesBindingSource;
            this.viewer_bill_wise.LocalReport.DataSources.Add(reportDataSource1);
            this.viewer_bill_wise.LocalReport.ReportEmbeddedResource = "Reports_info.Customer_sales_reports.Customer_sales_small_report.bill_wise_sales.l" +
    "oyal_cus_bill_wise_report.rdlc";
            this.viewer_bill_wise.Location = new System.Drawing.Point(0, 0);
            this.viewer_bill_wise.Name = "viewer_bill_wise";
            this.viewer_bill_wise.Size = new System.Drawing.Size(266, 246);
            this.viewer_bill_wise.TabIndex = 0;
            // 
            // pnl_all_transaction
            // 
            this.pnl_all_transaction.Controls.Add(this.viewer_all_transaction);
            this.pnl_all_transaction.Location = new System.Drawing.Point(358, 115);
            this.pnl_all_transaction.Name = "pnl_all_transaction";
            this.pnl_all_transaction.Size = new System.Drawing.Size(228, 429);
            this.pnl_all_transaction.TabIndex = 29;
            this.pnl_all_transaction.Visible = false;
            // 
            // viewer_all_transaction
            // 
            reportDataSource2.Name = "cus_sales";
            reportDataSource2.Value = this.Customer_SalesBindingSource;
            this.viewer_all_transaction.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_all_transaction.LocalReport.ReportEmbeddedResource = "Reports_info.Customer_sales_reports.Customer_sales_small_report.over_all.loyal_cu" +
    "s_all_sales.rdlc";
            this.viewer_all_transaction.Location = new System.Drawing.Point(3, 0);
            this.viewer_all_transaction.Name = "viewer_all_transaction";
            this.viewer_all_transaction.Size = new System.Drawing.Size(189, 246);
            this.viewer_all_transaction.TabIndex = 0;
            // 
            // pnl_date_wise
            // 
            this.pnl_date_wise.Controls.Add(this.Viewer_datewise);
            this.pnl_date_wise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_date_wise.Location = new System.Drawing.Point(0, 105);
            this.pnl_date_wise.Name = "pnl_date_wise";
            this.pnl_date_wise.Size = new System.Drawing.Size(1024, 631);
            this.pnl_date_wise.TabIndex = 27;
            // 
            // Viewer_datewise
            // 
            this.Viewer_datewise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Viewer_datewise.DocumentMapWidth = 86;
            reportDataSource3.Name = "cus_sales";
            reportDataSource3.Value = this.Customer_SalesBindingSource;
            this.Viewer_datewise.LocalReport.DataSources.Add(reportDataSource3);
            this.Viewer_datewise.LocalReport.ReportEmbeddedResource = "Reports_info.Customer_sales_reports.Customer_sales_small_report.date_wise.loyal_c" +
    "us_date_wise_sales.rdlc";
            this.Viewer_datewise.Location = new System.Drawing.Point(0, 0);
            this.Viewer_datewise.Name = "Viewer_datewise";
            this.Viewer_datewise.Size = new System.Drawing.Size(1024, 631);
            this.Viewer_datewise.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbl_billNo);
            this.panel5.Controls.Add(this.txt_billNo);
            this.panel5.Controls.Add(this.ToDate);
            this.panel5.Controls.Add(this.customer_code_text);
            this.panel5.Controls.Add(this.FromDate);
            this.panel5.Controls.Add(this.lbl_to_date);
            this.panel5.Controls.Add(this.lbl_cus_name);
            this.panel5.Controls.Add(this.lbl_from_date);
            this.panel5.Controls.Add(this.view_button);
            this.panel5.Controls.Add(this.lbl_cus_code);
            this.panel5.Controls.Add(this.customer_name_text);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel5.Location = new System.Drawing.Point(0, 33);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1024, 72);
            this.panel5.TabIndex = 26;
            // 
            // lbl_billNo
            // 
            this.lbl_billNo.AutoSize = true;
            this.lbl_billNo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_billNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_billNo.Location = new System.Drawing.Point(18, 12);
            this.lbl_billNo.Name = "lbl_billNo";
            this.lbl_billNo.Size = new System.Drawing.Size(55, 17);
            this.lbl_billNo.TabIndex = 84;
            this.lbl_billNo.Text = "Bill No:";
            // 
            // txt_billNo
            // 
            this.txt_billNo.BackColor = System.Drawing.SystemColors.Window;
            this.txt_billNo.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.txt_billNo.ForeColor = System.Drawing.Color.Black;
            this.txt_billNo.Location = new System.Drawing.Point(77, 8);
            this.txt_billNo.Name = "txt_billNo";
            this.txt_billNo.Size = new System.Drawing.Size(276, 24);
            this.txt_billNo.TabIndex = 85;
            this.txt_billNo.Text = "SALE_";
            // 
            // ToDate
            // 
            this.ToDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.ToDate.CustomFormat = "dd/MMMM/yyyy";
            this.ToDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(444, 8);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(275, 24);
            this.ToDate.TabIndex = 58;
            this.ToDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
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
            this.customer_code_text.Location = new System.Drawing.Point(444, 42);
            this.customer_code_text.MaxLength = 14;
            this.customer_code_text.Name = "customer_code_text";
            this.customer_code_text.Size = new System.Drawing.Size(275, 25);
            this.customer_code_text.TabIndex = 80;
            this.customer_code_text.SelectedIndexChanged += new System.EventHandler(this.customer_code_text_SelectedIndexChanged);
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(77, 8);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(276, 24);
            this.FromDate.TabIndex = 58;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // lbl_to_date
            // 
            this.lbl_to_date.AutoSize = true;
            this.lbl_to_date.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_to_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_to_date.Location = new System.Drawing.Point(387, 12);
            this.lbl_to_date.Name = "lbl_to_date";
            this.lbl_to_date.Size = new System.Drawing.Size(23, 17);
            this.lbl_to_date.TabIndex = 57;
            this.lbl_to_date.Text = "To";
            // 
            // lbl_cus_name
            // 
            this.lbl_cus_name.AutoSize = true;
            this.lbl_cus_name.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_cus_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_cus_name.Location = new System.Drawing.Point(19, 46);
            this.lbl_cus_name.Name = "lbl_cus_name";
            this.lbl_cus_name.Size = new System.Drawing.Size(54, 17);
            this.lbl_cus_name.TabIndex = 81;
            this.lbl_cus_name.Text = "Name:";
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_from_date.Location = new System.Drawing.Point(19, 11);
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
            this.view_button.Font = new System.Drawing.Font("Century Gothic", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.Location = new System.Drawing.Point(795, 7);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(106, 61);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "Go";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // lbl_cus_code
            // 
            this.lbl_cus_code.AutoSize = true;
            this.lbl_cus_code.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_cus_code.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_cus_code.Location = new System.Drawing.Point(373, 46);
            this.lbl_cus_code.Name = "lbl_cus_code";
            this.lbl_cus_code.Size = new System.Drawing.Size(50, 17);
            this.lbl_cus_code.TabIndex = 82;
            this.lbl_cus_code.Text = "Code:";
            // 
            // customer_name_text
            // 
            this.customer_name_text.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.customer_name_text.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.customer_name_text.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.customer_name_text.ForeColor = System.Drawing.Color.Black;
            this.customer_name_text.FormattingEnabled = true;
            this.customer_name_text.Location = new System.Drawing.Point(77, 42);
            this.customer_name_text.MaxLength = 14;
            this.customer_name_text.Name = "customer_name_text";
            this.customer_name_text.Size = new System.Drawing.Size(276, 25);
            this.customer_name_text.TabIndex = 83;
            this.customer_name_text.SelectedIndexChanged += new System.EventHandler(this.customer_name_text_SelectedIndexChanged);
            this.customer_name_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customer_name_text_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.refresh_button);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.btn_bill_wise);
            this.panel1.Controls.Add(this.all_transactions_button);
            this.panel1.Controls.Add(this.date_wise_button);
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 33);
            this.panel1.TabIndex = 25;
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
            this.refresh_button.TabIndex = 102;
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
            this.button9.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button9.Location = new System.Drawing.Point(926, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(49, 33);
            this.button9.TabIndex = 101;
            this.button9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btn_bill_wise
            // 
            this.btn_bill_wise.AutoSize = true;
            this.btn_bill_wise.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btn_bill_wise.ForeColor = System.Drawing.Color.White;
            this.btn_bill_wise.Location = new System.Drawing.Point(166, 7);
            this.btn_bill_wise.Name = "btn_bill_wise";
            this.btn_bill_wise.Size = new System.Drawing.Size(112, 20);
            this.btn_bill_wise.TabIndex = 100;
            this.btn_bill_wise.Text = "BILL WISE SALES";
            this.btn_bill_wise.UseVisualStyleBackColor = true;
            this.btn_bill_wise.CheckedChanged += new System.EventHandler(this.btn_bill_wise_CheckedChanged);
            // 
            // all_transactions_button
            // 
            this.all_transactions_button.AutoSize = true;
            this.all_transactions_button.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.all_transactions_button.ForeColor = System.Drawing.Color.White;
            this.all_transactions_button.Location = new System.Drawing.Point(313, 7);
            this.all_transactions_button.Name = "all_transactions_button";
            this.all_transactions_button.Size = new System.Drawing.Size(119, 20);
            this.all_transactions_button.TabIndex = 100;
            this.all_transactions_button.Text = "CUSTOMER WISE";
            this.all_transactions_button.UseVisualStyleBackColor = true;
            this.all_transactions_button.CheckedChanged += new System.EventHandler(this.all_transactions_button_CheckedChanged);
            // 
            // date_wise_button
            // 
            this.date_wise_button.AutoSize = true;
            this.date_wise_button.Checked = true;
            this.date_wise_button.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.date_wise_button.ForeColor = System.Drawing.Color.White;
            this.date_wise_button.Location = new System.Drawing.Point(11, 7);
            this.date_wise_button.Name = "date_wise_button";
            this.date_wise_button.Size = new System.Drawing.Size(120, 20);
            this.date_wise_button.TabIndex = 98;
            this.date_wise_button.TabStop = true;
            this.date_wise_button.Text = "DATE WISE SALES";
            this.date_wise_button.UseVisualStyleBackColor = true;
            this.date_wise_button.CheckedChanged += new System.EventHandler(this.date_wise_button_CheckedChanged);
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
            this.Closebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
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
            // form_sales_small_report
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 736);
            this.Controls.Add(this.pnl_bill_wise);
            this.Controls.Add(this.pnl_all_transaction);
            this.Controls.Add(this.pnl_date_wise);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_sales_small_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_sales_small_report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_sales_small_report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Customer_SalesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.small_report_ds)).EndInit();
            this.pnl_bill_wise.ResumeLayout(false);
            this.pnl_all_transaction.ResumeLayout(false);
            this.pnl_date_wise.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_bill_wise;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_bill_wise;
        private System.Windows.Forms.Panel pnl_all_transaction;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_all_transaction;
        private System.Windows.Forms.Panel pnl_date_wise;
        private Microsoft.Reporting.WinForms.ReportViewer Viewer_datewise;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_billNo;
        private System.Windows.Forms.TextBox txt_billNo;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.ComboBox customer_code_text;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_to_date;
        private System.Windows.Forms.Label lbl_cus_name;
        private System.Windows.Forms.Label lbl_from_date;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Label lbl_cus_code;
        private System.Windows.Forms.ComboBox customer_name_text;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton btn_bill_wise;
        private System.Windows.Forms.RadioButton all_transactions_button;
        private System.Windows.Forms.RadioButton date_wise_button;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource Customer_SalesBindingSource;
        private small_report_ds small_report_ds;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button refresh_button;
    }
}