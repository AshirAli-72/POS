using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using System.Diagnostics;
using Guna.UI2.WinForms;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.LoginInfo.controllers;

namespace Spices_pos.LoginInfo.forms
{
    public partial class login_form : Form
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

        public login_form()
        {
            InitializeComponent();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        //done_form done = new done_form();
        error_form error = new error_form();
        form_sure_message sure = new form_sure_message();
        public static bool isSwitchUser;

        private void logo_imag()
        {
            try
            {
                TextData.address = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");
                TextData.logo = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "logo_path");

                if (TextData.address != "" && TextData.address != "NULL")
                {
                    if (TextData.logo != "nill" && TextData.logo != "" && TextData.logo != null)
                    {
                        logo.Image = Bitmap.FromFile(TextData.address + TextData.logo);
                        logo1.Image = Bitmap.FromFile(TextData.address + TextData.logo);
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sure.Message_choose("Are you sure you want logout!");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                Application.Exit();
            }
        }

        private void login_form_Shown(object sender, EventArgs e)
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                btnPin.FillColor = Color.LemonChiffon;

                clock.Start();
                logo_imag();
                GetSetData.addFormCopyrights(lblCopyrights);

                txtPinCode.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void login_with_enter_key(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    TextData.username = username_text.Text;
                    TextData.password = pass_text.Text;

                    if (Button_controls.login_button(username_text, pass_text, txt_barcode, isSwitchUser))
                    { 
                        this.Hide();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void login_btn_Click_1(object sender, EventArgs e)
        {
            try
            {
                TextData.username = username_text.Text;
                TextData.password = pass_text.Text;

                if (Button_controls.login_button(username_text, pass_text, txt_barcode, isSwitchUser))
                {
                    this.Hide();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }
        private void chkLoginType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoginType.Checked == true)
            {
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = true;
                chkLoginType.Checked = false;
                chkLoginType1.Checked = false;
                txt_barcode.Focus();
            }
        }

        private void chkLoginType1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoginType1.Checked == true)
            {
                //Button_controls.CheckLoginDetails(username_text, pass_text, txt_barcode);

                pnlLogin.Visible = true;
                pnlLoginByScanner.Visible = false;
                chkLoginType.Checked = false;
                chkLoginType1.Visible = false;
                username_text.Focus();
            }
        }

        private void loadByScanner(Guna2TextBox txtPassword)
        {
            try
            {
                string username = "";

                if (txtPassword.Text != "")
                {
                    username = data.UserPermissions("username", "pos_users", "password", txtPassword.Text);

                    if (username != "")
                    {
                        username_text.Text = username;


                        if (Button_controls.login_button(username_text, txtPassword, txtPassword, isSwitchUser))
                        {
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loadByScanner(txt_barcode);
            }
        }
        private void login_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnPin.FillColor = Color.LemonChiffon;
                btnPassword.FillColor = Color.White;
                btnScan.FillColor = Color.White;

                pnlPinCode.Visible = true;
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = false;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                txtPinCode.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnPin.FillColor = Color.White;
                btnPassword.FillColor = Color.LemonChiffon;
                btnScan.FillColor = Color.White;


                pnlPinCode.Visible = false;
                pnlLogin.Visible = true;
                pnlLoginByScanner.Visible = false;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                username_text.Select();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPin.FillColor = Color.White;
                btnPassword.FillColor = Color.White;
                btnScan.FillColor = Color.LemonChiffon ;


                pnlPinCode.Visible = false;
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = true;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                txt_barcode.Select();
            }
        }

        private void username_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                loadByScanner(txtPinCode);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Guna2Button button = sender as Guna2Button; // Cast the sender to Button

            if (button != null)
            {
                txtPinCode.Text += button.Text; // Append button text to TextBox
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtPinCode.Text.Length > 0)
            {
                txtPinCode.Text = txtPinCode.Text.Remove(txtPinCode.Text.Length - 1);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            loadByScanner(txtPinCode);
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.LemonChiffon;
            btnPassword.FillColor = Color.White;
            btnScan.FillColor = Color.White;

            pnlPinCode.Visible = true;
            pnlLogin.Visible = false;
            pnlLoginByScanner.Visible = false;

            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            txtPinCode.Select();
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.White;
            btnPassword.FillColor = Color.LemonChiffon;
            btnScan.FillColor = Color.White;

            pnlPinCode.Visible = false;
            pnlLogin.Visible = true;
            pnlLoginByScanner.Visible = false;

            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            username_text.Select();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.White;
            btnPassword.FillColor = Color.White;
            btnScan.FillColor = Color.LemonChiffon;


            pnlPinCode.Visible = false;
            pnlLogin.Visible = false;
            pnlLoginByScanner.Visible = true;

            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            txt_barcode.Select();
        }
    }
}
