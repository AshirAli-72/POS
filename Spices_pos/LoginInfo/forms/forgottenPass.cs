using System;
using System.Windows.Forms;
using Login_info.controllers;

namespace Spices_pos.LoginInfo.forms
{
    public partial class forgottenPass : Form
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

        public forgottenPass()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            login_form log = new login_form();
            log.Show();
            this.Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            //TextData.username = username_text.Text;
            //TextData.password = confirm_pass_text.Text;
            //TextData.confirm_password = confirm_pass_text.Text;

            //Button_controls.forgot_pass();

        }
    }
}
