namespace Message_box_info.forms.Clock_In
{
    partial class form_payout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_payout));
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.update_button = new Guna.UI2.WinForms.Guna2Button();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.savebutton = new Guna.UI2.WinForms.Guna2Button();
            this.txtRemarks = new Guna.UI2.WinForms.Guna2TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAmount = new Guna.UI2.WinForms.Guna2TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtToUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFromUser = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCopyrights = new System.Windows.Forms.Label();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.time_text = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.show_all = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.FormNamelabel = new System.Windows.Forms.Label();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel5.SuspendLayout();
            this.guna2Panel7.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDate
            // 
            this.txtDate.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.txtDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.txtDate.CustomFormat = "dd/MMMM/yyyy";
            this.txtDate.Enabled = false;
            this.txtDate.Font = new System.Drawing.Font("Century Gothic", 9F);
            this.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDate.Location = new System.Drawing.Point(177, 495);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(111, 22);
            this.txtDate.TabIndex = 600;
            this.txtDate.Value = new System.DateTime(2019, 9, 23, 0, 0, 0, 0);
            // 
            // update_button
            // 
            this.update_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.update_button.BackColor = System.Drawing.Color.Transparent;
            this.update_button.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(190)))));
            this.update_button.BorderRadius = 5;
            this.update_button.BorderThickness = 1;
            this.update_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.update_button.DialogResult = System.Windows.Forms.DialogResult.No;
            this.update_button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.update_button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.update_button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.update_button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.update_button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(190)))));
            this.update_button.Font = new System.Drawing.Font("Century Gothic", 7F, System.Drawing.FontStyle.Bold);
            this.update_button.ForeColor = System.Drawing.Color.White;
            this.update_button.Image = ((System.Drawing.Image)(resources.GetObject("update_button.Image")));
            this.update_button.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.update_button.ImageSize = new System.Drawing.Size(15, 15);
            this.update_button.Location = new System.Drawing.Point(316, 303);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(80, 48);
            this.update_button.TabIndex = 561;
            this.update_button.Text = "Update";
            this.update_button.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.update_button.TextOffset = new System.Drawing.Point(2, 0);
            this.update_button.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.BorderRadius = 8;
            this.btnExit.BorderThickness = 1;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnExit.Location = new System.Drawing.Point(26, 303);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(179, 48);
            this.btnExit.TabIndex = 560;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // savebutton
            // 
            this.savebutton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.savebutton.BackColor = System.Drawing.Color.Transparent;
            this.savebutton.BorderColor = System.Drawing.Color.SeaGreen;
            this.savebutton.BorderRadius = 8;
            this.savebutton.BorderThickness = 1;
            this.savebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.savebutton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.savebutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.savebutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.savebutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.savebutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.savebutton.FillColor = System.Drawing.Color.SeaGreen;
            this.savebutton.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.savebutton.ForeColor = System.Drawing.Color.White;
            this.savebutton.Image = ((System.Drawing.Image)(resources.GetObject("savebutton.Image")));
            this.savebutton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.savebutton.Location = new System.Drawing.Point(217, 303);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(179, 48);
            this.savebutton.TabIndex = 2;
            this.savebutton.Text = "Save";
            this.savebutton.TextOffset = new System.Drawing.Point(1, 0);
            this.savebutton.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txtRemarks.BorderColor = System.Drawing.Color.LightGray;
            this.txtRemarks.BorderRadius = 5;
            this.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemarks.DefaultText = "";
            this.txtRemarks.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtRemarks.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtRemarks.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRemarks.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtRemarks.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRemarks.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.ForeColor = System.Drawing.Color.Black;
            this.txtRemarks.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRemarks.IconLeftSize = new System.Drawing.Size(18, 18);
            this.txtRemarks.Location = new System.Drawing.Point(26, 117);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Padding = new System.Windows.Forms.Padding(2);
            this.txtRemarks.PasswordChar = '\0';
            this.txtRemarks.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtRemarks.PlaceholderText = "Add Reason";
            this.txtRemarks.SelectedText = "";
            this.txtRemarks.Size = new System.Drawing.Size(370, 172);
            this.txtRemarks.TabIndex = 1;
            this.txtRemarks.Click += new System.EventHandler(this.txtAmount_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.Location = new System.Drawing.Point(138, 499);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 15);
            this.label18.TabIndex = 552;
            this.label18.Text = "Date:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(23, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 15);
            this.label10.TabIndex = 550;
            this.label10.Text = "Reason:";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.txtAmount.BorderColor = System.Drawing.Color.LightGray;
            this.txtAmount.BorderRadius = 4;
            this.txtAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAmount.DefaultText = "";
            this.txtAmount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAmount.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAmount.IconLeftSize = new System.Drawing.Size(18, 18);
            this.txtAmount.Location = new System.Drawing.Point(26, 40);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Padding = new System.Windows.Forms.Padding(2);
            this.txtAmount.PasswordChar = '\0';
            this.txtAmount.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txtAmount.PlaceholderText = "";
            this.txtAmount.SelectedText = "";
            this.txtAmount.Size = new System.Drawing.Size(370, 35);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Click += new System.EventHandler(this.txtAmount_Click);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Location = new System.Drawing.Point(23, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 15);
            this.label11.TabIndex = 571;
            this.label11.Text = "Amount:";
            // 
            // txtToUser
            // 
            this.txtToUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtToUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtToUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtToUser.BackColor = System.Drawing.Color.White;
            this.txtToUser.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToUser.ForeColor = System.Drawing.Color.Black;
            this.txtToUser.FormattingEnabled = true;
            this.txtToUser.IntegralHeight = false;
            this.txtToUser.Location = new System.Drawing.Point(-65, 558);
            this.txtToUser.MaxDropDownItems = 7;
            this.txtToUser.Name = "txtToUser";
            this.txtToUser.Size = new System.Drawing.Size(50, 24);
            this.txtToUser.TabIndex = 500;
            this.txtToUser.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.label1.Location = new System.Drawing.Point(-117, 563);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 547;
            this.label1.Tag = " ";
            this.label1.Text = "To User:";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.label3.Location = new System.Drawing.Point(-12, 563);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 547;
            this.label3.Tag = " ";
            this.label3.Text = "From User:";
            this.label3.Visible = false;
            // 
            // txtFromUser
            // 
            this.txtFromUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFromUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFromUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtFromUser.BackColor = System.Drawing.Color.White;
            this.txtFromUser.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromUser.ForeColor = System.Drawing.Color.Black;
            this.txtFromUser.FormattingEnabled = true;
            this.txtFromUser.IntegralHeight = false;
            this.txtFromUser.Location = new System.Drawing.Point(54, 558);
            this.txtFromUser.MaxDropDownItems = 7;
            this.txtFromUser.Name = "txtFromUser";
            this.txtFromUser.Size = new System.Drawing.Size(55, 24);
            this.txtFromUser.TabIndex = 500;
            this.txtFromUser.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(73, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 12);
            this.label9.TabIndex = 620;
            this.label9.Text = "*";
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BackColor = System.Drawing.Color.Snow;
            this.guna2Panel5.BorderRadius = 30;
            this.guna2Panel5.Controls.Add(this.guna2Panel7);
            this.guna2Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel5.FillColor = System.Drawing.Color.White;
            this.guna2Panel5.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(455, 438);
            this.guna2Panel5.TabIndex = 622;
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel7.BorderRadius = 30;
            this.guna2Panel7.Controls.Add(this.lblCopyrights);
            this.guna2Panel7.Controls.Add(this.guna2Panel10);
            this.guna2Panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel7.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.guna2Panel7.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Padding = new System.Windows.Forms.Padding(8);
            this.guna2Panel7.Size = new System.Drawing.Size(455, 438);
            this.guna2Panel7.TabIndex = 1;
            // 
            // lblCopyrights
            // 
            this.lblCopyrights.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCopyrights.AutoSize = true;
            this.lblCopyrights.Font = new System.Drawing.Font("Century Gothic", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblCopyrights.ForeColor = System.Drawing.Color.White;
            this.lblCopyrights.Location = new System.Drawing.Point(143, 426);
            this.lblCopyrights.Name = "lblCopyrights";
            this.lblCopyrights.Size = new System.Drawing.Size(240, 12);
            this.lblCopyrights.TabIndex = 202;
            this.lblCopyrights.Text = "Copyrights 2020, all rights reserved by roots software solutions";
            this.lblCopyrights.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCopyrights.Visible = false;
            // 
            // guna2Panel10
            // 
            this.guna2Panel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel10.BorderRadius = 30;
            this.guna2Panel10.Controls.Add(this.guna2GradientPanel1);
            this.guna2Panel10.Controls.Add(this.panel1);
            this.guna2Panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel10.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel10.Location = new System.Drawing.Point(8, 8);
            this.guna2Panel10.Name = "guna2Panel10";
            this.guna2Panel10.Padding = new System.Windows.Forms.Padding(10);
            this.guna2Panel10.Size = new System.Drawing.Size(439, 422);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.White;
            this.guna2GradientPanel1.Controls.Add(this.panel5);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 55);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(419, 357);
            this.guna2GradientPanel1.TabIndex = 137;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.time_text);
            this.panel5.Controls.Add(this.btnExit);
            this.panel5.Controls.Add(this.txtRemarks);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.txtDate);
            this.panel5.Controls.Add(this.txtAmount);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.savebutton);
            this.panel5.Controls.Add(this.update_button);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(419, 357);
            this.panel5.TabIndex = 8;
            // 
            // time_text
            // 
            this.time_text.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.time_text.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.time_text.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(146)))));
            this.time_text.CustomFormat = "";
            this.time_text.Enabled = false;
            this.time_text.Font = new System.Drawing.Font("Century Gothic", 1F);
            this.time_text.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.time_text.Location = new System.Drawing.Point(-299, 420);
            this.time_text.Name = "time_text";
            this.time_text.Size = new System.Drawing.Size(10, 9);
            this.time_text.TabIndex = 623;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(72, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 620;
            this.label2.Text = "*";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.show_all);
            this.panel1.Controls.Add(this.guna2Separator1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.FormNamelabel);
            this.panel1.Controls.Add(this.guna2Button3);
            this.panel1.Controls.Add(this.guna2Button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 45);
            this.panel1.TabIndex = 2;
            // 
            // show_all
            // 
            this.show_all.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.show_all.BackColor = System.Drawing.Color.Transparent;
            this.show_all.BorderColor = System.Drawing.Color.Gray;
            this.show_all.BorderRadius = 7;
            this.show_all.BorderThickness = 1;
            this.show_all.CustomBorderColor = System.Drawing.Color.Gray;
            this.show_all.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.show_all.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.show_all.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.show_all.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.show_all.FillColor = System.Drawing.Color.Transparent;
            this.show_all.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.show_all.ForeColor = System.Drawing.Color.White;
            this.show_all.HoverState.BorderColor = System.Drawing.Color.White;
            this.show_all.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.show_all.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.show_all.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.show_all.Image = ((System.Drawing.Image)(resources.GetObject("show_all.Image")));
            this.show_all.ImageSize = new System.Drawing.Size(15, 15);
            this.show_all.Location = new System.Drawing.Point(328, 3);
            this.show_all.Name = "show_all";
            this.show_all.Size = new System.Drawing.Size(41, 32);
            this.show_all.TabIndex = 540;
            this.show_all.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Separator1.FillColor = System.Drawing.Color.LightGray;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 35);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(418, 10);
            this.guna2Separator1.TabIndex = 539;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 534;
            this.pictureBox2.TabStop = false;
            // 
            // FormNamelabel
            // 
            this.FormNamelabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.FormNamelabel.AutoSize = true;
            this.FormNamelabel.BackColor = System.Drawing.Color.Transparent;
            this.FormNamelabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.FormNamelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormNamelabel.Location = new System.Drawing.Point(38, 10);
            this.FormNamelabel.Name = "FormNamelabel";
            this.FormNamelabel.Size = new System.Drawing.Size(62, 19);
            this.FormNamelabel.TabIndex = 533;
            this.FormNamelabel.Text = "Payout";
            this.FormNamelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Button3
            // 
            this.guna2Button3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(115)))), ((int)(((byte)(155)))));
            this.guna2Button3.BorderRadius = 2;
            this.guna2Button3.BorderThickness = 1;
            this.guna2Button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2Button3.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button3.Image")));
            this.guna2Button3.ImageOffset = new System.Drawing.Point(0, 3);
            this.guna2Button3.ImageSize = new System.Drawing.Size(25, 28);
            this.guna2Button3.Location = new System.Drawing.Point(418, 0);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(1, 45);
            this.guna2Button3.TabIndex = 508;
            this.guna2Button3.Visible = false;
            // 
            // guna2Button4
            // 
            this.guna2Button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guna2Button4.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button4.BorderColor = System.Drawing.Color.Gray;
            this.guna2Button4.BorderRadius = 7;
            this.guna2Button4.BorderThickness = 1;
            this.guna2Button4.CustomBorderColor = System.Drawing.Color.Gray;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button4.ForeColor = System.Drawing.Color.White;
            this.guna2Button4.HoverState.BorderColor = System.Drawing.Color.White;
            this.guna2Button4.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.guna2Button4.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.guna2Button4.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.guna2Button4.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button4.Image")));
            this.guna2Button4.ImageSize = new System.Drawing.Size(15, 15);
            this.guna2Button4.Location = new System.Drawing.Point(374, 3);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Size = new System.Drawing.Size(41, 32);
            this.guna2Button4.TabIndex = 522;
            this.guna2Button4.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // form_payout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(455, 438);
            this.Controls.Add(this.txtFromUser);
            this.Controls.Add(this.txtToUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_payout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_payout";
            this.TransparencyKey = System.Drawing.Color.Snow;
            this.Load += new System.EventHandler(this.form_payout_Load);
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel7.ResumeLayout(false);
            this.guna2Panel7.PerformLayout();
            this.guna2Panel10.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker txtDate;
        private Guna.UI2.WinForms.Guna2Button update_button;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2Button savebutton;
        private Guna.UI2.WinForms.Guna2TextBox txtRemarks;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2TextBox txtAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox txtToUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox txtFromUser;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private System.Windows.Forms.Label lblCopyrights;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button show_all;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label FormNamelabel;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker time_text;
        private System.Windows.Forms.Label label2;
    }
}