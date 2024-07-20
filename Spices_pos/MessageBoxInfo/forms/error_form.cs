using System;
using System.Windows.Forms;

namespace Message_box_info.forms
{
    public partial class error_form : Form
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

        public error_form()
        {
            InitializeComponent();
        }

        public void errorMessage(string result)
        {
            message.Text = result.ToString();
        }

        private void errorbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void error_form_Load(object sender, EventArgs e)
        {
            errorbutton.Select();
        }

        private void error_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
