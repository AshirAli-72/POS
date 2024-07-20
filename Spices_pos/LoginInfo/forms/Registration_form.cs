using System;
using System.Windows.Forms;
using Spices_pos.LoginInfo.controllers;

namespace Spices_pos.LoginInfo.forms
{
    public partial class Registration_form : Form
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

        public Registration_form()
        {
            InitializeComponent();
        }
        
        private void Closebutton_Click(object sender, EventArgs e)
        {
            login_form log = new login_form();
            log.Show();
            this.Close();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            //TextData.person_name = name_text.Text;
            //TextData.role_title = role_text.Text;
            //TextData.username = username_text.Text;
            //TextData.password = pass_text.Text;
            //TextData.confirm_password = txt_confirm_pass.Text;

            Button_controls.Registration_info();
        }

        private void close_Click(object sender, EventArgs e)
        {
            login_form log = new login_form();
            log.Show();
            this.Close();
        }

        private void Registration_form_Load(object sender, EventArgs e)
        {

        }
    }
}
