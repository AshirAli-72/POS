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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_low_inventory));
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stocks_ds = new Stock_management.Low_inventory_reports.stocks_ds();
            this.low_inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh_button = new System.Windows.Forms.Button();
            this.Closebutton = new System.Windows.Forms.Button();
            this.btn_low_inventory = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_expiry = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_from_date = new System.Windows.Forms.Label();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.view_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.viewer_expiry = new Microsoft.Reporting.WinForms.ReportViewer();
            this.viewer_low_inventory = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocks_ds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.low_inventoryBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.stocks_ds;
            // 
            // stocks_ds
            // 
            this.stocks_ds.DataSetName = "stocks_ds";
            this.stocks_ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // low_inventoryBindingSource
            // 
            this.low_inventoryBindingSource.DataMember = "low_inventory";
            this.low_inventoryBindingSource.DataSource = this.stocks_ds;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.refresh_button);
            this.panel1.Controls.Add(this.Closebutton);
            this.panel1.Controls.Add(this.btn_low_inventory);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_expiry);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 32);
            this.panel1.TabIndex = 10;
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
            this.refresh_button.Location = new System.Drawing.Point(927, -2);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(48, 37);
            this.refresh_button.TabIndex = 103;
            this.refresh_button.UseVisualStyleBackColor = true;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
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
            this.Closebutton.Location = new System.Drawing.Point(977, -2);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(47, 37);
            this.Closebutton.TabIndex = 15;
            this.Closebutton.UseVisualStyleBackColor = false;
            this.Closebutton.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // btn_low_inventory
            // 
            this.btn_low_inventory.AutoSize = true;
            this.btn_low_inventory.Checked = true;
            this.btn_low_inventory.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_low_inventory.ForeColor = System.Drawing.Color.White;
            this.btn_low_inventory.Location = new System.Drawing.Point(12, 9);
            this.btn_low_inventory.Name = "btn_low_inventory";
            this.btn_low_inventory.Size = new System.Drawing.Size(131, 20);
            this.btn_low_inventory.TabIndex = 101;
            this.btn_low_inventory.TabStop = true;
            this.btn_low_inventory.Text = "Low Inventory";
            this.btn_low_inventory.UseVisualStyleBackColor = true;
            this.btn_low_inventory.CheckedChanged += new System.EventHandler(this.btn_low_inventory_CheckedChanged);
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
            // btn_expiry
            // 
            this.btn_expiry.AutoSize = true;
            this.btn_expiry.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Bold);
            this.btn_expiry.ForeColor = System.Drawing.Color.White;
            this.btn_expiry.Location = new System.Drawing.Point(165, 9);
            this.btn_expiry.Name = "btn_expiry";
            this.btn_expiry.Size = new System.Drawing.Size(128, 20);
            this.btn_expiry.TabIndex = 102;
            this.btn_expiry.Text = "Expired Items";
            this.btn_expiry.UseVisualStyleBackColor = true;
            this.btn_expiry.CheckedChanged += new System.EventHandler(this.btn_expiry_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbl_from_date);
            this.panel5.Controls.Add(this.FromDate);
            this.panel5.Controls.Add(this.view_button);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1024, 81);
            this.panel5.TabIndex = 15;
            // 
            // lbl_from_date
            // 
            this.lbl_from_date.AutoSize = true;
            this.lbl_from_date.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold);
            this.lbl_from_date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.lbl_from_date.Location = new System.Drawing.Point(13, 17);
            this.lbl_from_date.Name = "lbl_from_date";
            this.lbl_from_date.Size = new System.Drawing.Size(49, 17);
            this.lbl_from_date.TabIndex = 104;
            this.lbl_from_date.Text = "Date:";
            // 
            // FromDate
            // 
            this.FromDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.FromDate.CustomFormat = "dd/MMMM/yyyy";
            this.FromDate.Font = new System.Drawing.Font("Verdana", 10F);
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(64, 14);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(208, 24);
            this.FromDate.TabIndex = 103;
            this.FromDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
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
            this.view_button.Location = new System.Drawing.Point(311, 14);
            this.view_button.Name = "view_button";
            this.view_button.Size = new System.Drawing.Size(120, 60);
            this.view_button.TabIndex = 47;
            this.view_button.Text = "View";
            this.view_button.UseVisualStyleBackColor = false;
            this.view_button.Click += new System.EventHandler(this.view_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.viewer_expiry);
            this.panel2.Controls.Add(this.viewer_low_inventory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 655);
            this.panel2.TabIndex = 16;
            // 
            // viewer_expiry
            // 
            reportDataSource1.Name = "low_inventory";
            reportDataSource1.Value = this.DataTable1BindingSource;
            this.viewer_expiry.LocalReport.DataSources.Add(reportDataSource1);
            this.viewer_expiry.LocalReport.ReportEmbeddedResource = "Stock_management.Low_inventory_reports.expired_items_report.rdlc";
            this.viewer_expiry.Location = new System.Drawing.Point(492, 47);
            this.viewer_expiry.Name = "viewer_expiry";
            this.viewer_expiry.Size = new System.Drawing.Size(396, 311);
            this.viewer_expiry.TabIndex = 13;
            // 
            // viewer_low_inventory
            // 
            reportDataSource2.Name = "low_inventory";
            reportDataSource2.Value = this.low_inventoryBindingSource;
            this.viewer_low_inventory.LocalReport.DataSources.Add(reportDataSource2);
            this.viewer_low_inventory.LocalReport.ReportEmbeddedResource = "Stock_management.Low_inventory_reports.low_inventory_report.rdlc";
            this.viewer_low_inventory.Location = new System.Drawing.Point(35, 47);
            this.viewer_low_inventory.Name = "viewer_low_inventory";
            this.viewer_low_inventory.Size = new System.Drawing.Size(396, 311);
            this.viewer_low_inventory.TabIndex = 12;
            // 
            // form_low_inventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_low_inventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_low_inventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.form_low_inventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stocks_ds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.low_inventoryBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Closebutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource low_inventoryBindingSource;
        private stocks_ds stocks_ds;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.RadioButton btn_low_inventory;
        private System.Windows.Forms.RadioButton btn_expiry;
        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button view_button;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_expiry;
        private Microsoft.Reporting.WinForms.ReportViewer viewer_low_inventory;
        private System.Windows.Forms.DateTimePicker FromDate;
        private System.Windows.Forms.Label lbl_from_date;
    }
}