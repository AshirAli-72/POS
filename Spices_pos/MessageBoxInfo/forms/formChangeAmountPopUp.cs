using System;
using System.Windows.Forms;

namespace Message_box_info.forms
{
    public partial class formChangeAmountPopUp : Form
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

        public static string amountDue = "";
        public static string cashAmount = "";
        public static string changeAmount = "";
        public static bool isCashPayment = false;
        public static string paymentType = "";
        private int countdown = 5;

        public formChangeAmountPopUp()
        {
            InitializeComponent();

            timer1.Start();

            progressBar1.Maximum = 5; // Set the maximum value of the progress bar
            progressBar1.Minimum = 0; // Set the minimum value of the progress bar
            progressBar1.Value = countdown; // Initialize the progress bar value

            lblAmountDue.Text = amountDue;
            lblCashTend.Text = cashAmount;
            lblChangeAmount.Text = changeAmount;

            if (isCashPayment)
            {
                lblChange.Text = "CHANGE";
            }
            else
            {
                lblChange.Text = "CREDIT CARD";
            }
            
            if (paymentType == "Credit Card")
            {
                lblCash.Text = "CREDIT CARD";
            }
            else
            {
                lblCash.Text = "CASH TEND";
            }

        }

        private void errorbutton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown--; // Decrement the countdown value

            if (countdown >= 0)
            {
                progressBar1.Value = countdown; // Update the progress bar value
            }
            else
            {
                timer1.Stop(); // Stop the timer when countdown reaches 0
                this.Dispose(); // Close the form
            }
        }

        private void formChangeAmountPopUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                timer1.Stop();
                this.Dispose();
            }
        }
    }
}
