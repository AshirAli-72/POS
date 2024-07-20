using System;
using System.Diagnostics;
using System.Windows.Forms;
using Datalayer;
using Login_info.controllers;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
namespace CounterSales_info.forms
{
    public partial class form_discount : Form
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

        public form_discount()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }


        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        private void setFormColorsDynamically()
        {
            //try
            //{
            //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
            //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
            //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

            //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
            //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
            //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

            //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
            //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
            //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

            //    //****************************************************************

            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel5, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sale_men_Click(object sender, EventArgs e)
        {
            TextData.discount = 100;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TextData.discount = 0;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TextData.discount = 40;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TextData.discount = 50;
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TextData.discount = 60;
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TextData.discount = 20;
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TextData.discount = 25;
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TextData.discount = 30;
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TextData.discount = 5;
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            TextData.discount = 10;
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TextData.discount = 15;
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TextData.discount = 90;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextData.discount = 80;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextData.discount = 70;
            this.Close();
        }

        private void form_discount_Load(object sender, EventArgs e)
        {
            //GetSetData.addFormCopyrights(lblCopyrights);
            txtDiscount.Select();
        }

        private void form_discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btn1Percent_Click(object sender, EventArgs e)
        {
            TextData.discount = 1;
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtDiscount.Text != "")
            {

                TextData.discount = double.Parse(txtDiscount.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter discount %!");
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtDiscount.Text != "")
                {

                    TextData.discount = double.Parse(txtDiscount.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter discount %!");
                }
            }
        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
