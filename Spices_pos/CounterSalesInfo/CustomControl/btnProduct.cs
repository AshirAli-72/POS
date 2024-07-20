using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OnBarcode.Barcode;

namespace CustomerSales_info.CustomerSalesInfo.CustomControls
{
    public partial class btnProduct : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        public btnProduct()
        {
            InitializeComponent();
        }

        public string Price
        {
            get { return lblPrice.Text; }
            set { lblPrice.Text = value; }
        }

        public string ItemsName
        {
            get { return lblProductName.Text; }
            set { lblProductName.Text = value; }
        }
        public string ItemsBarcode
        {
            get { return lblBarcode.Text; }
            set { lblBarcode.Text = value; }
        }
        public string ProductId
        {
            get { return lblProductId.Text; }
            set { lblProductId.Text = value; }
        }  
        public string StockId
        {
            get { return lblStockId.Text; }
            set { lblStockId.Text = value; }
        }

        public string Stock
        {
            get { return lblStock.Text; }
            set { lblStock.Text = value; }
        }

        public Color FillColor
        {
            get { return pnlButton.FillColor; }
            set { pnlButton.FillColor = value; }
        }

        public Color FillColor2
        {
            get { return pnlButton.FillColor2; }
            set { pnlButton.FillColor2 = value; }
        }

        public Image itemImage
        {
            get { return lblProductName.Image; }
            set { lblProductName.Image = value; }
        }

        private void lblProductName_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblPrice_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void lblStock_MouseHover(object sender, EventArgs e)
        {
            this.OnMouseHover(e);
        }

        private void lblStock_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }
    }
}
