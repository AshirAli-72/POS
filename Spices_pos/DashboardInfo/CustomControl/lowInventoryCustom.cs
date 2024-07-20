using System.Drawing;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.DashboardInfo.CustomControls
{
    public partial class lowInventoryCustom : UserControl
    {
        public lowInventoryCustom()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public string RowNumber
        {
            get { return lblRowNumber.Text; }
            set { lblRowNumber.Text = value; }
        }

        public string ItemsName
        {
            get { return lblProductName.Text; }
            set { lblProductName.Text = value; }
        }
        
        //public string Barcode
        //{
        //    get { return lblBarcode.Text; }
        //    set { lblBarcode.Text = value; }
        //}
        
        public string Quantity
        {
            get { return lblQuantity.Text; }
            set { lblQuantity.Text = value; }
        } 
        
        public string Brand
        {
            get { return lblBrand.Text; }
            set { lblBrand.Text = value; }
        }
        
        //public string Category
        //{
        //    get { return lblCategory.Text; }
        //    set { lblCategory.Text = value; }
        //} 
        
        public string Amount
        {
            get { return lblAmount.Text; }
            set { lblAmount.Text = value; }
        } 

        public Color FillColor
        {
            get { return pnlCartItems.FillColor; }
            set { pnlCartItems.FillColor = value; }
        }

        public Color FillColor2
        {
            get { return pnlCartItems.FillColor2; }
            set { pnlCartItems.FillColor2 = value; }
        }
    }
}
