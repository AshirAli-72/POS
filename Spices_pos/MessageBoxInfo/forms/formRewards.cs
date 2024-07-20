using System;
using System.Windows.Forms;
using Datalayer;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Message_box_info.forms
{
    public partial class formRewards : Form
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
        public static string rewardsPointsRedeem = "";
        public static string rewardPointsDiscount = "0";

        public formRewards()
        {
            InitializeComponent();
        }

        public void Message_choose(string result)
        {
            message.Text = result.ToString();
        }

        public static bool sure = false;

        private void donebutton_Click(object sender, EventArgs e)
        {
            if (txtRedeemPoints.Text != "")
            {
                rewardsPointsRedeem = txtRedeemPoints.Text;

                if (txtDiscount.Text == "")
                {
                    rewardPointsDiscount = data.UserPermissions("pointsRedeemDiscount", "pos_general_settings");
                }
                else
                {
                    rewardPointsDiscount = txtDiscount.Text;
                }

                sure = true;
                this.Close();
            }
            else
            {
                error.errorMessage("Please enter the redeem points first!");
                error.ShowDialog();
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            sure = false;

            this.Close();
        }

        private void formRewards_Load(object sender, EventArgs e)
        {
            btn_no.Select();
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtRedeemPoints.Text, e);
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtDiscount.Text, e);
        }

        private void formRewards_Shown(object sender, EventArgs e)
        {
            try
            {
                rewardsPointsRedeem = "";
                rewardPointsDiscount = "0";

                txtRedeemPoints.Text = "";
                txtDiscount.Text = "";

                string pointsDiscountInPercentageDb = data.UserPermissions("pointsDiscountInPercentage", "pos_general_settings");
                txtDiscount.Text = data.UserPermissions("pointsRedeemDiscount", "pos_general_settings");

                if (bool.Parse(pointsDiscountInPercentageDb))
                {
                    lblDiscount.Text = "Discount in %";
                }
                else
                {
                    lblDiscount.Text = "Discount";
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
