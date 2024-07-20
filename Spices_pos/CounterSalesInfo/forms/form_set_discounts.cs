using System;
using System.Windows.Forms;
using Datalayer;
using Login_info.controllers;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
namespace CounterSales_info.forms
{
    public partial class form_set_discounts : Form
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

        public form_set_discounts()
        {
            InitializeComponent();
        }


        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();

            TextData.allDiscounts = true;
        }

        private void form_set_discounts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void setDiscounts()
        {
            try
            {
                TextData.customerDiscount = false;
                TextData.promotionsDiscount = false;
                TextData.loyalityProgramDiscount = false;
                TextData.allDiscounts = false;

                if (chkCustomerDiscount.Checked)
                {
                    TextData.customerDiscount = true;
                }

                if (chkPromotionDiscount.Checked)
                {
                    TextData.promotionsDiscount = true;
                }

                if (chkLoyaltyDiscount.Checked)
                {
                    TextData.loyalityProgramDiscount = true;
                }

                if (chkAllOfTheAbove.Checked)
                {
                    TextData.allDiscounts = true;
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            setDiscounts();

            this.Close();
        }
    }
}
