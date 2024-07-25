using System;
using System.Windows.Forms;
using Datalayer;
using Guna.UI2.WinForms;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms
{
    public partial class form_salesman_tips_copy : Form
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

        public form_salesman_tips_copy()
        {
            InitializeComponent();
            timer1.Start();

            progressBar1.Maximum = 60; // Set the maximum value of the progress bar
            progressBar1.Minimum = 0; // Set the minimum value of the progress bar
            progressBar1.Value = countdown; // Initialize the progress bar value
        }


        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public string invoiceNumber = "";
        private int countdown = 60;

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
                this.Close(); // Close the form
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_salesman_tips_copy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtOtherAmount.Text != "")
            {
                setSalesmanCurrentSaleTip(txtOtherAmount.Text);
            }
            else
            {
                MessageBox.Show("Please enter the amount first!");
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtOtherAmount.Text, e);
        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            //Process.Start("tabtip.exe");
        }

        private void form_salesman_tips_copy_Load(object sender, EventArgs e)
        {
            txt_due_amount.Text = data.UserPermissions("amount_due", "pos_sales_accounts", "billNo", invoiceNumber);

            txtOtherAmount.Select();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Guna2Button button = sender as Guna2Button; // Cast the sender to Button

            if (button != null)
            {
                txtOtherAmount.Text += button.Text; // Append button text to TextBox
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtOtherAmount.Text.Length > 0)
            {
                txtOtherAmount.Text = txtOtherAmount.Text.Remove(txtOtherAmount.Text.Length - 1);
            }
        }

        private void setSalesmanCurrentSaleTip(string tipAmount)
        {
            try
            {
                GetSetData.query = "update pos_sales_accounts set employeeTip = '" + Math.Round(double.Parse(tipAmount), 2) + "' where (billNo = '" + invoiceNumber +"');";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                this.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double tipAmount = (5 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double tipAmount = (10 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            double tipAmount = (15 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double tipAmount = (20 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double tipAmount = (25 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double tipAmount = (30 * double.Parse(txt_due_amount.Text)) / 100;

            setSalesmanCurrentSaleTip(tipAmount.ToString());
        }

    }
}
