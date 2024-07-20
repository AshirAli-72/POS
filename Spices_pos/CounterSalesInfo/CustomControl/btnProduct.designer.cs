namespace CustomerSales_info.CustomerSalesInfo.CustomControls
{
    partial class btnProduct
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButton = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.panelQuantity = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblStockId = new System.Windows.Forms.Label();
            this.lblProductId = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.pnlButton.SuspendLayout();
            this.panelQuantity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.BorderColor = System.Drawing.Color.Gainsboro;
            this.pnlButton.BorderRadius = 15;
            this.pnlButton.BorderThickness = 1;
            this.pnlButton.Controls.Add(this.panelQuantity);
            this.pnlButton.Controls.Add(this.lblProductName);
            this.pnlButton.Controls.Add(this.lblPrice);
            this.pnlButton.Controls.Add(this.lblStockId);
            this.pnlButton.Controls.Add(this.lblProductId);
            this.pnlButton.Controls.Add(this.lblBarcode);
            this.pnlButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.FillColor = System.Drawing.Color.White;
            this.pnlButton.FillColor2 = System.Drawing.Color.White;
            this.pnlButton.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.pnlButton.Location = new System.Drawing.Point(2, 2);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(180, 128);
            this.pnlButton.TabIndex = 0;
            this.pnlButton.Click += new System.EventHandler(this.lblProductName_Click);
            // 
            // panelQuantity
            // 
            this.panelQuantity.BackColor = System.Drawing.Color.Transparent;
            this.panelQuantity.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.panelQuantity.BorderRadius = 18;
            this.panelQuantity.BorderThickness = 3;
            this.panelQuantity.Controls.Add(this.lblStock);
            this.panelQuantity.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(89)))), ((int)(((byte)(69)))));
            this.panelQuantity.Location = new System.Drawing.Point(70, 6);
            this.panelQuantity.Name = "panelQuantity";
            this.panelQuantity.Size = new System.Drawing.Size(41, 39);
            this.panelQuantity.TabIndex = 529;
            this.panelQuantity.Click += new System.EventHandler(this.lblStock_Click);
            this.panelQuantity.MouseLeave += new System.EventHandler(this.lblStock_MouseLeave);
            this.panelQuantity.MouseHover += new System.EventHandler(this.lblStock_MouseHover);
            // 
            // lblStock
            // 
            this.lblStock.BackColor = System.Drawing.Color.Transparent;
            this.lblStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStock.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblStock.ForeColor = System.Drawing.Color.White;
            this.lblStock.Location = new System.Drawing.Point(0, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(41, 39);
            this.lblStock.TabIndex = 509;
            this.lblStock.Text = "100";
            this.lblStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStock.Click += new System.EventHandler(this.lblStock_Click);
            this.lblStock.MouseLeave += new System.EventHandler(this.lblStock_MouseLeave);
            this.lblStock.MouseHover += new System.EventHandler(this.lblStock_MouseHover);
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Century Gothic", 9.5F);
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(7, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(167, 46);
            this.lblProductName.TabIndex = 11;
            this.lblProductName.Text = "Baby Diaper for childern and babies of gilgit";
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProductName.Click += new System.EventHandler(this.lblProductName_Click);
            this.lblProductName.MouseLeave += new System.EventHandler(this.lblStock_MouseLeave);
            this.lblProductName.MouseHover += new System.EventHandler(this.lblStock_MouseHover);
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPrice.Location = new System.Drawing.Point(7, 101);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblPrice.Size = new System.Drawing.Size(167, 20);
            this.lblPrice.TabIndex = 9;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPrice.Click += new System.EventHandler(this.lblPrice_Click);
            this.lblPrice.MouseLeave += new System.EventHandler(this.lblStock_MouseLeave);
            this.lblPrice.MouseHover += new System.EventHandler(this.lblStock_MouseHover);
            // 
            // lblStockId
            // 
            this.lblStockId.BackColor = System.Drawing.Color.Transparent;
            this.lblStockId.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStockId.Location = new System.Drawing.Point(0, 152);
            this.lblStockId.Name = "lblStockId";
            this.lblStockId.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.lblStockId.Size = new System.Drawing.Size(96, 17);
            this.lblStockId.TabIndex = 14;
            this.lblStockId.Text = "barcode";
            this.lblStockId.Visible = false;
            // 
            // lblProductId
            // 
            this.lblProductId.BackColor = System.Drawing.Color.Transparent;
            this.lblProductId.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblProductId.Location = new System.Drawing.Point(0, 141);
            this.lblProductId.Name = "lblProductId";
            this.lblProductId.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.lblProductId.Size = new System.Drawing.Size(96, 17);
            this.lblProductId.TabIndex = 13;
            this.lblProductId.Text = "barcode";
            this.lblProductId.Visible = false;
            // 
            // lblBarcode
            // 
            this.lblBarcode.BackColor = System.Drawing.Color.Transparent;
            this.lblBarcode.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblBarcode.Location = new System.Drawing.Point(0, 124);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.lblBarcode.Size = new System.Drawing.Size(96, 17);
            this.lblBarcode.TabIndex = 12;
            this.lblBarcode.Text = "barcode";
            this.lblBarcode.Visible = false;
            // 
            // btnProduct
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlButton);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Name = "btnProduct";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(184, 132);
            this.pnlButton.ResumeLayout(false);
            this.panelQuantity.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientPanel pnlButton;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblStockId;
        private System.Windows.Forms.Label lblProductId;
        private Guna.UI2.WinForms.Guna2Panel panelQuantity;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblPrice;
    }
}
