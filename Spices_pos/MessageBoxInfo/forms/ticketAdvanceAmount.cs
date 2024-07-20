using System;
using System.Windows.Forms;
using Datalayer;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms
{
    public partial class ticketAdvanceAmount : Form
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

        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        public static string amountDue = "0";
        public static string advanceAmount = "0";

        public ticketAdvanceAmount()
        {
            InitializeComponent();
        }

        public static bool sure = false;

        private void donebutton_Click(object sender, EventArgs e)
        {
            if (txtAdvanceAmount.Text != "")
            {
                advanceAmount = txtAdvanceAmount.Text;

                sure = true;
                this.Close();
            }
            else
            {
                error.errorMessage("Please enter the advance amount first!");
                error.ShowDialog();
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            advanceAmount = "0";
            sure = false;
            this.Close();
        }

        private void ticketAdvanceAmount_Load(object sender, EventArgs e)
        {
            txtAmountDue.Text = amountDue;
            txtAdvanceAmount.Select();
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAdvanceAmount.Text, e);
        }
    }
}
