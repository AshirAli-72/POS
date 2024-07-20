using Spices_pos.CustomersInfo.GurantorReports;

namespace Customers_info.GurantorReports
{
    partial class formGurantorsReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGurantorsReports));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportProcedureGurantorsCustomerWiseBindingSource = new System.Windows.Forms.BindingSource();
            this.gurantorsReportDs = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDs();
            this.ReportProcedureReportsTitlesBindingSource = new System.Windows.Forms.BindingSource();
            this.ReportProcedureGurantorsBillWiseBindingSource = new System.Windows.Forms.BindingSource();
            this.ReportProcedureGurantorsBatchNoWiseBindingSource = new System.Windows.Forms.BindingSource();
            this.ReportProcedureGurantorsOverallDetailsBindingSource = new System.Windows.Forms.BindingSource();
            this.t = new System.Windows.Forms.Panel();
            this.txtCode = new System.Windows.Forms.ComboBox();
            this.txtTitle = new System.Windows.Forms.ComboBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.view_button = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh_button = new System.Windows.Forms.Button();
            this.btnInvoiceNo = new System.Windows.Forms.RadioButton();
            this.btnCustomer = new System.Windows.Forms.RadioButton();
            this.btnOverAll = new System.Windows.Forms.RadioButton();
            this.btnBatchNo = new System.Windows.Forms.RadioButton();
            this.Closebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlCustomerWise = new System.Windows.Forms.Panel();
            this.viewerCustomerWise = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlInvoice = new System.Windows.Forms.Panel();
            this.viewerInvoiceNo = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlBatchNo = new System.Windows.Forms.Panel();
            this.viewerBatchNo = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlOverAll = new System.Windows.Forms.Panel();
            this.viewerOverAll = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportProcedureGurantorsOverallDetailsTableAdapter = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsOverallDetailsTableAdapter();
            this.ReportProcedureReportsTitlesTableAdapter = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureReportsTitlesTableAdapter();
            this.ReportProcedureGurantorsBatchNoWiseTableAdapter = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsBatchNoWiseTableAdapter();
            this.ReportProcedureGurantorsBillWiseTableAdapter = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsBillWiseTableAdapter();
            this.ReportProcedureGurantorsCustomerWiseTableAdapter = new Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsCustomerWiseTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsCustomerWiseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gurantorsReportDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsBillWiseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsBatchNoWiseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsOverallDetailsBindingSource)).BeginInit();
            this.t.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCustomerWise.SuspendLayout();
            this.pnlInvoice.SuspendLayout();
            this.pnlBatchNo.SuspendLayout();
            this.pnlOverAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportProcedureGurantorsCustomerWiseBindingSource
            // 
            this.ReportProcedureGurantorsCustomerWiseBindingSource.DataMember = "ReportProcedureGurantorsCustomerWise";
            this.ReportProcedureGurantorsCustomerWiseBindingSource.DataSource = this.gurantorsReportDs;
            // 
            // gurantorsReportDs
            // 
            this.gurantorsReportDs.DataSetName = "gurantorsReportDs";
            this.gurantorsReportDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReportProcedureReportsTitlesBindingSource
            // 
            this.ReportProcedureReportsTitlesBindingSource.DataMember = "ReportProcedureReportsTitles";
            this.ReportProcedureReportsTitlesBindingSource.DataSource = this.gurantorsReportDs;
            // 
            // ReportProcedureGurantorsBillWiseBindingSource
            // 
            this.ReportProcedureGurantorsBillWiseBindingSource.DataMember = "ReportProcedureGurantorsBillWise";
            this.ReportProcedureGurantorsBillWiseBindingSource.DataSource = this.gurantorsReportDs;
            // 
            // ReportProcedureGurantorsBatchNoWiseBindingSource
            // 
            this.ReportProcedureGurantorsBatchNoWiseBindingSource.DataMember = "ReportProcedureGurantorsBatchNoWise";
            this.ReportProcedureGurantorsBatchNoWiseBindingSource.DataSource = this.gurantorsReportDs;
            // 
            // ReportProcedureGurantorsOverallDetailsBindingSource
            // 
            this.ReportProcedureGurantorsOverallDetailsBindingSource.DataMember = "ReportProcedureGurantorsOverallDetails";
            this.ReportProcedureGurantorsOverallDetailsBindingSource.DataSource = this.gurantorsReportDs;
            // 
            // t
            // 
            this.t.Controls.Add(this.txtCode);
            this.t.Controls.Add(this.txtTitle);
            this.t.Controls.Add(this.lblCode);
            this.t.Controls.Add(this.view_button);
            this.t.Controls.Add(this.lblTitle);
            this.t.Dock = System.Windows.Forms.DockStyle.Top;
            this.t.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.t.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.t.Location = new System.Drawing.Point(0, 33);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(1024, 70);
            this.t.TabIndex = 33;
            // 
            // txtCode
            // 
            this.txtCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(157)))));
            this.txtCode.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.FormattingEnabled = true;
            this.txtCode.IntegralHeight = false;
            this.txtCode.Location = new System.Drawing.Point(460, 9);
            this.txtCode.MaxLength = 14;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(284, 24);
            this.txtCode.TabIndex = 80;
            this.txtCode.SelectedIndexChanged += new System.EventHandler(this.txtCode_SelectedIndexChanged);
            // 
            // txtTitle
            // 
            this.txtTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.Font = new System.Drawing.Font("Verdana", 10F);
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.FormattingEnabled = true;
            this.txtTitle.IntegralHeight = false;
            this.txtTitle.Location = new System.Drawing.Point(92, 9);
            this.txtTitle.MaxLength = 14;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(284, 24);
            this.txtTitle.TabIndex = 80;
            this.txtTitle.SelectedIndexChanged += new System.EventHandler(this.txtTitle_SelectedIndexChanged);
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTitle_KeyDown);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lblCode.Location = new System.Drawing.Point(394, 13);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(50, 17);
            this.lblCode.TabIndex = 82;
            this.lblCode.Text = "Code:";
            // 
            // view_button
            // 
            this.view_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.view_button.FlatAppearance.BorderSize = 2;
            this.view_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view_button.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view_button.ForeColor = System.Drawing.Color.White;
            this.view_button.Location = new System.Drawing.Point(815, 9);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(103, 57);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "Go";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lblTitle.Location = new System.Drawing.Point(11, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(77, 17);
            this.lblTitle.TabIndex = 82;
            this.lblTitle.Text = "Customer:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.refresh_button);
            this.panel1.Controls.Add(this.btnInvoiceNo);
            this.panel1.Controls.Add(this.btnCustomer);
            this.panel1.Controls.Add(this.btnOverAll);
            this.panel1.Controls.Add(this.btnBatchNo);
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 33);
            this.panel1.TabIndex = 32;
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
            this.refresh_button.Location = new System.Drawing.Point(924, 0);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(50, 33);
            this.refresh_button.TabIndex = 101;
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // btnInvoiceNo
            // 
            this.btnInvoiceNo.AutoSize = true;
            this.btnInvoiceNo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnInvoiceNo.ForeColor = System.Drawing.Color.White;
            this.btnInvoiceNo.Location = new System.Drawing.Point(142, 8);
            this.btnInvoiceNo.Name = "btnInvoiceNo";
            this.btnInvoiceNo.Size = new System.Drawing.Size(98, 20);
            this.btnInvoiceNo.TabIndex = 99;
            this.btnInvoiceNo.Text = "INVOICE NO";
            this.btnInvoiceNo.UseVisualStyleBackColor = true;
            this.btnInvoiceNo.CheckedChanged += new System.EventHandler(this.btn_supplier_wise_CheckedChanged);
            // 
            // btnCustomer
            // 
            this.btnCustomer.AutoSize = true;
            this.btnCustomer.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnCustomer.ForeColor = System.Drawing.Color.White;
            this.btnCustomer.Location = new System.Drawing.Point(279, 8);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(119, 20);
            this.btnCustomer.TabIndex = 99;
            this.btnCustomer.Text = "CUSTOMER WISE";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.CheckedChanged += new System.EventHandler(this.btn_status_CheckedChanged);
            // 
            // btnOverAll
            // 
            this.btnOverAll.AutoSize = true;
            this.btnOverAll.Checked = true;
            this.btnOverAll.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnOverAll.ForeColor = System.Drawing.Color.White;
            this.btnOverAll.Location = new System.Drawing.Point(437, 8);
            this.btnOverAll.Name = "btnOverAll";
            this.btnOverAll.Size = new System.Drawing.Size(163, 20);
            this.btnOverAll.TabIndex = 99;
            this.btnOverAll.TabStop = true;
            this.btnOverAll.Text = "OVER ALL GUARANTORS";
            this.btnOverAll.UseVisualStyleBackColor = true;
            this.btnOverAll.CheckedChanged += new System.EventHandler(this.btn_over_all_CheckedChanged);
            // 
            // btnBatchNo
            // 
            this.btnBatchNo.AutoSize = true;
            this.btnBatchNo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.btnBatchNo.ForeColor = System.Drawing.Color.White;
            this.btnBatchNo.Location = new System.Drawing.Point(13, 8);
            this.btnBatchNo.Name = "btnBatchNo";
            this.btnBatchNo.Size = new System.Drawing.Size(90, 20);
            this.btnBatchNo.TabIndex = 100;
            this.btnBatchNo.Text = "ZONE WISE ";
            this.btnBatchNo.UseVisualStyleBackColor = true;
            this.btnBatchNo.CheckedChanged += new System.EventHandler(this.btn_area_wise_CheckedChanged);
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
            this.Closebutton.Font = new System.Drawing.Font("Century Gothic", 17F, System.Drawing.FontStyle.Bold);
            this.Closebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.Closebutton.Location = new System.Drawing.Point(974, 0);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(50, 33);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlCustomerWise);
            this.panel2.Controls.Add(this.pnlInvoice);
            this.panel2.Controls.Add(this.pnlBatchNo);
            this.panel2.Controls.Add(this.pnlOverAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 665);
            this.panel2.TabIndex = 35;
            // 
            // pnlCustomerWise
            // 
            this.pnlCustomerWise.Controls.Add(this.viewerCustomerWise);
            this.pnlCustomerWise.Location = new System.Drawing.Point(793, 32);
            this.pnlCustomerWise.Name = "pnlCustomerWise";
            this.pnlCustomerWise.Size = new System.Drawing.Size(219, 361);
            this.pnlCustomerWise.TabIndex = 35;
            // 
            // viewerCustomerWise
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReportProcedureGurantorsCustomerWiseBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerCustomerWise.LocalReport.DataSources.Add(reportDataSource1);
            this.viewerCustomerWise.LocalReport.DataSources.Add(reportDataSource2);
            this.viewerCustomerWise.LocalReport.ReportEmbeddedResource = "Customers_info.GurantorReports.reportGuarantorCustomerWise.rdlc";
            this.viewerCustomerWise.Location = new System.Drawing.Point(10, 3);
            this.viewerCustomerWise.Name = "viewerCustomerWise";
            this.viewerCustomerWise.Size = new System.Drawing.Size(170, 335);
            this.viewerCustomerWise.TabIndex = 0;
            // 
            // pnlInvoice
            // 
            this.pnlInvoice.Controls.Add(this.viewerInvoiceNo);
            this.pnlInvoice.Location = new System.Drawing.Point(500, 29);
            this.pnlInvoice.Name = "pnlInvoice";
            this.pnlInvoice.Size = new System.Drawing.Size(219, 361);
            this.pnlInvoice.TabIndex = 33;
            // 
            // viewerInvoiceNo
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.ReportProcedureGurantorsBillWiseBindingSource;
            reportDataSource4.Name = "DataSet2";
            reportDataSource4.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerInvoiceNo.LocalReport.DataSources.Add(reportDataSource3);
            this.viewerInvoiceNo.LocalReport.DataSources.Add(reportDataSource4);
            this.viewerInvoiceNo.LocalReport.ReportEmbeddedResource = "Customers_info.GurantorReports.reportGuarantorInvoiceNo.rdlc";
            this.viewerInvoiceNo.Location = new System.Drawing.Point(10, 3);
            this.viewerInvoiceNo.Name = "viewerInvoiceNo";
            this.viewerInvoiceNo.Size = new System.Drawing.Size(170, 335);
            this.viewerInvoiceNo.TabIndex = 0;
            // 
            // pnlBatchNo
            // 
            this.pnlBatchNo.Controls.Add(this.viewerBatchNo);
            this.pnlBatchNo.Location = new System.Drawing.Point(249, 29);
            this.pnlBatchNo.Name = "pnlBatchNo";
            this.pnlBatchNo.Size = new System.Drawing.Size(238, 361);
            this.pnlBatchNo.TabIndex = 33;
            // 
            // viewerBatchNo
            // 
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.ReportProcedureGurantorsBatchNoWiseBindingSource;
            reportDataSource6.Name = "DataSet2";
            reportDataSource6.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerBatchNo.LocalReport.DataSources.Add(reportDataSource5);
            this.viewerBatchNo.LocalReport.DataSources.Add(reportDataSource6);
            this.viewerBatchNo.LocalReport.ReportEmbeddedResource = "Customers_info.GurantorReports.reportGuarantorBatchNo.rdlc";
            this.viewerBatchNo.Location = new System.Drawing.Point(10, 3);
            this.viewerBatchNo.Name = "viewerBatchNo";
            this.viewerBatchNo.Size = new System.Drawing.Size(170, 335);
            this.viewerBatchNo.TabIndex = 0;
            // 
            // pnlOverAll
            // 
            this.pnlOverAll.Controls.Add(this.viewerOverAll);
            this.pnlOverAll.Location = new System.Drawing.Point(0, 0);
            this.pnlOverAll.Name = "pnlOverAll";
            this.pnlOverAll.Size = new System.Drawing.Size(242, 651);
            this.pnlOverAll.TabIndex = 32;
            // 
            // viewerOverAll
            // 
            reportDataSource7.Name = "DataSet1";
            reportDataSource7.Value = this.ReportProcedureGurantorsOverallDetailsBindingSource;
            reportDataSource8.Name = "DataSet2";
            reportDataSource8.Value = this.ReportProcedureReportsTitlesBindingSource;
            this.viewerOverAll.LocalReport.DataSources.Add(reportDataSource7);
            this.viewerOverAll.LocalReport.DataSources.Add(reportDataSource8);
            this.viewerOverAll.LocalReport.ReportEmbeddedResource = "Customers_info.GurantorReports.reportGuarantorOverAll.rdlc";
            this.viewerOverAll.Location = new System.Drawing.Point(0, 29);
            this.viewerOverAll.Name = "viewerOverAll";
            this.viewerOverAll.Size = new System.Drawing.Size(223, 250);
            this.viewerOverAll.TabIndex = 0;
            // 
            // ReportProcedureGurantorsOverallDetailsTableAdapter
            // 
            this.ReportProcedureGurantorsOverallDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureReportsTitlesTableAdapter
            // 
            this.ReportProcedureReportsTitlesTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureGurantorsBatchNoWiseTableAdapter
            // 
            this.ReportProcedureGurantorsBatchNoWiseTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureGurantorsBillWiseTableAdapter
            // 
            this.ReportProcedureGurantorsBillWiseTableAdapter.ClearBeforeFill = true;
            // 
            // ReportProcedureGurantorsCustomerWiseTableAdapter
            // 
            this.ReportProcedureGurantorsCustomerWiseTableAdapter.ClearBeforeFill = true;
            // 
            // formGurantorsReports
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.t);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formGurantorsReports";
            this.Text = "formGurantorsReports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formGurantorsReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsCustomerWiseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gurantorsReportDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureReportsTitlesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsBillWiseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsBatchNoWiseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportProcedureGurantorsOverallDetailsBindingSource)).EndInit();
            this.t.ResumeLayout(false);
            this.t.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlCustomerWise.ResumeLayout(false);
            this.pnlInvoice.ResumeLayout(false);
            this.pnlBatchNo.ResumeLayout(false);
            this.pnlOverAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel t;
        private System.Windows.Forms.ComboBox txtTitle;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.RadioButton btnInvoiceNo;
        private System.Windows.Forms.RadioButton btnCustomer;
        private System.Windows.Forms.RadioButton btnOverAll;
        private System.Windows.Forms.RadioButton btnBatchNo;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlCustomerWise;
        private Microsoft.Reporting.WinForms.ReportViewer viewerCustomerWise;
        private System.Windows.Forms.Panel pnlInvoice;
        private Microsoft.Reporting.WinForms.ReportViewer viewerInvoiceNo;
        private System.Windows.Forms.Panel pnlBatchNo;
        private Microsoft.Reporting.WinForms.ReportViewer viewerBatchNo;
        private System.Windows.Forms.Panel pnlOverAll;
        private Microsoft.Reporting.WinForms.ReportViewer viewerOverAll;
        private System.Windows.Forms.BindingSource ReportProcedureGurantorsOverallDetailsBindingSource;
        private gurantorsReportDs gurantorsReportDs;
        private System.Windows.Forms.BindingSource ReportProcedureReportsTitlesBindingSource;
        private Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsOverallDetailsTableAdapter ReportProcedureGurantorsOverallDetailsTableAdapter;
        private Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureReportsTitlesTableAdapter ReportProcedureReportsTitlesTableAdapter;
        private System.Windows.Forms.BindingSource ReportProcedureGurantorsBatchNoWiseBindingSource;
        private Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsBatchNoWiseTableAdapter ReportProcedureGurantorsBatchNoWiseTableAdapter;
        private System.Windows.Forms.BindingSource ReportProcedureGurantorsBillWiseBindingSource;
        private Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsBillWiseTableAdapter ReportProcedureGurantorsBillWiseTableAdapter;
        private System.Windows.Forms.BindingSource ReportProcedureGurantorsCustomerWiseBindingSource;
        private Spices_pos.CustomersInfo.GurantorReports.gurantorsReportDsTableAdapters.ReportProcedureGurantorsCustomerWiseTableAdapter ReportProcedureGurantorsCustomerWiseTableAdapter;
    }
}