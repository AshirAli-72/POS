namespace Message_box_info.forms
{
    partial class form_sure_message
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_sure_message));
            this.pnlLoginByScanner = new Guna.UI2.WinForms.Guna2Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.message = new System.Windows.Forms.Label();
            this.btn_no = new Guna.UI2.WinForms.Guna2Button();
            this.btn_yes = new Guna.UI2.WinForms.Guna2Button();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel7 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel10 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.pnlLoginByScanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.guna2Panel5.SuspendLayout();
            this.guna2Panel7.SuspendLayout();
            this.guna2Panel10.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLoginByScanner
            // 
            this.pnlLoginByScanner.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoginByScanner.BorderColor = System.Drawing.Color.Transparent;
            this.pnlLoginByScanner.BorderRadius = 15;
            this.pnlLoginByScanner.Controls.Add(this.pictureBox2);
            this.pnlLoginByScanner.Controls.Add(this.message);
            this.pnlLoginByScanner.Controls.Add(this.btn_no);
            this.pnlLoginByScanner.Controls.Add(this.btn_yes);
            this.pnlLoginByScanner.Controls.Add(this.label5);
            this.pnlLoginByScanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoginByScanner.FillColor = System.Drawing.Color.Transparent;
            this.pnlLoginByScanner.Location = new System.Drawing.Point(0, 0);
            this.pnlLoginByScanner.Name = "pnlLoginByScanner";
            this.pnlLoginByScanner.Size = new System.Drawing.Size(428, 353);
            this.pnlLoginByScanner.TabIndex = 510;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(171, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(91, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // message
            // 
            this.message.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.message.BackColor = System.Drawing.Color.Transparent;
            this.message.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.message.Location = new System.Drawing.Point(20, 170);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(393, 104);
            this.message.TabIndex = 1;
            this.message.Text = "Error message";
            this.message.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_no
            // 
            this.btn_no.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_no.BackColor = System.Drawing.Color.Transparent;
            this.btn_no.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.btn_no.BorderRadius = 7;
            this.btn_no.BorderThickness = 1;
            this.btn_no.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_no.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_no.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_no.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_no.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_no.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.btn_no.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btn_no.ForeColor = System.Drawing.Color.White;
            this.btn_no.Location = new System.Drawing.Point(220, 294);
            this.btn_no.Name = "btn_no";
            this.btn_no.Size = new System.Drawing.Size(191, 52);
            this.btn_no.TabIndex = 0;
            this.btn_no.Text = "No";
            this.btn_no.Click += new System.EventHandler(this.btn_no_Click);
            // 
            // btn_yes
            // 
            this.btn_yes.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_yes.BackColor = System.Drawing.Color.Transparent;
            this.btn_yes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(143)))), ((int)(((byte)(131)))));
            this.btn_yes.BorderRadius = 7;
            this.btn_yes.BorderThickness = 1;
            this.btn_yes.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_yes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_yes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_yes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_yes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_yes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(143)))), ((int)(((byte)(131)))));
            this.btn_yes.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btn_yes.ForeColor = System.Drawing.Color.White;
            this.btn_yes.Location = new System.Drawing.Point(21, 294);
            this.btn_yes.Name = "btn_yes";
            this.btn_yes.Size = new System.Drawing.Size(191, 52);
            this.btn_yes.TabIndex = 0;
            this.btn_yes.Text = "Yes";
            this.btn_yes.Click += new System.EventHandler(this.donebutton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label5.Location = new System.Drawing.Point(159, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 28);
            this.label5.TabIndex = 531;
            this.label5.Text = "Warning!";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.guna2Panel5.Size = new System.Drawing.Size(458, 383);
            this.guna2Panel5.TabIndex = 511;
            // 
            // guna2Panel7
            // 
            this.guna2Panel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel7.BorderRadius = 30;
            this.guna2Panel7.Controls.Add(this.guna2Panel10);
            this.guna2Panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel7.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.guna2Panel7.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel7.Name = "guna2Panel7";
            this.guna2Panel7.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel7.Size = new System.Drawing.Size(458, 383);
            this.guna2Panel7.TabIndex = 1;
            // 
            // guna2Panel10
            // 
            this.guna2Panel10.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel10.BorderRadius = 30;
            this.guna2Panel10.Controls.Add(this.guna2GradientPanel1);
            this.guna2Panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel10.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.guna2Panel10.Location = new System.Drawing.Point(5, 5);
            this.guna2Panel10.Name = "guna2Panel10";
            this.guna2Panel10.Padding = new System.Windows.Forms.Padding(10);
            this.guna2Panel10.Size = new System.Drawing.Size(448, 373);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.pnlLoginByScanner);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 10);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(428, 353);
            this.guna2GradientPanel1.TabIndex = 137;
            // 
            // form_sure_message
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(458, 383);
            this.Controls.Add(this.guna2Panel5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form_sure_message";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_sure_message";
            this.TransparencyKey = System.Drawing.Color.Snow;
            this.Load += new System.EventHandler(this.form_sure_message_Load);
            this.pnlLoginByScanner.ResumeLayout(false);
            this.pnlLoginByScanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel7.ResumeLayout(false);
            this.guna2Panel10.ResumeLayout(false);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlLoginByScanner;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label message;
        private Guna.UI2.WinForms.Guna2Button btn_yes;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button btn_no;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
    }
}