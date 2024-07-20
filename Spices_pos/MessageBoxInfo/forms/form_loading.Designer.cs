namespace Message_box_info.forms
{
    partial class form_loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_loading));
            this.pnlLoginByScanner = new Guna.UI2.WinForms.Guna2Panel();
            this.Closebutton = new Guna.UI2.WinForms.Guna2Button();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
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
            this.pnlLoginByScanner.BackColor = System.Drawing.Color.White;
            this.pnlLoginByScanner.BorderColor = System.Drawing.Color.Transparent;
            this.pnlLoginByScanner.BorderRadius = 15;
            this.pnlLoginByScanner.Controls.Add(this.Closebutton);
            this.pnlLoginByScanner.Controls.Add(this.loadingLabel);
            this.pnlLoginByScanner.Controls.Add(this.pictureBox2);
            this.pnlLoginByScanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoginByScanner.FillColor = System.Drawing.Color.Transparent;
            this.pnlLoginByScanner.Location = new System.Drawing.Point(0, 0);
            this.pnlLoginByScanner.Name = "pnlLoginByScanner";
            this.pnlLoginByScanner.Size = new System.Drawing.Size(428, 412);
            this.pnlLoginByScanner.TabIndex = 509;
            // 
            // Closebutton
            // 
            this.Closebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Closebutton.BackColor = System.Drawing.Color.Transparent;
            this.Closebutton.BorderColor = System.Drawing.Color.Gray;
            this.Closebutton.BorderRadius = 7;
            this.Closebutton.BorderThickness = 1;
            this.Closebutton.CustomBorderColor = System.Drawing.Color.Gray;
            this.Closebutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Closebutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Closebutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Closebutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Closebutton.FillColor = System.Drawing.Color.Transparent;
            this.Closebutton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Closebutton.ForeColor = System.Drawing.Color.White;
            this.Closebutton.HoverState.BorderColor = System.Drawing.Color.White;
            this.Closebutton.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.Closebutton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.Closebutton.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.Closebutton.Image = ((System.Drawing.Image)(resources.GetObject("Closebutton.Image")));
            this.Closebutton.ImageSize = new System.Drawing.Size(15, 15);
            this.Closebutton.Location = new System.Drawing.Point(383, 3);
            this.Closebutton.Name = "Closebutton";
            this.Closebutton.Size = new System.Drawing.Size(42, 33);
            this.Closebutton.TabIndex = 523;
            this.Closebutton.Click += new System.EventHandler(this.Closebutton_Click);
            // 
            // loadingLabel
            // 
            this.loadingLabel.BackColor = System.Drawing.Color.Transparent;
            this.loadingLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.loadingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(136)))), ((int)(((byte)(193)))));
            this.loadingLabel.Location = new System.Drawing.Point(0, 260);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(428, 152);
            this.loadingLabel.TabIndex = 2;
            this.loadingLabel.Text = "Message";
            this.loadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = global::Spices_pos.Properties.Resources.wait;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(428, 260);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel5.BorderRadius = 30;
            this.guna2Panel5.Controls.Add(this.guna2Panel7);
            this.guna2Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel5.FillColor = System.Drawing.Color.White;
            this.guna2Panel5.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(458, 442);
            this.guna2Panel5.TabIndex = 510;
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
            this.guna2Panel7.Size = new System.Drawing.Size(458, 442);
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
            this.guna2Panel10.Size = new System.Drawing.Size(448, 432);
            this.guna2Panel10.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.Controls.Add(this.pnlLoginByScanner);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(10, 10);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(428, 412);
            this.guna2GradientPanel1.TabIndex = 137;
            // 
            // form_loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(458, 442);
            this.Controls.Add(this.guna2Panel5);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form_loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "form_loading";
            this.TransparencyKey = System.Drawing.Color.Snow;
            this.pnlLoginByScanner.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel7;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel10;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.Label loadingLabel;
        private Guna.UI2.WinForms.Guna2Button Closebutton;
    }
}