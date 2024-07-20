using System;
using System.Windows.Forms;

namespace Message_box_info.forms
{
    public partial class done_form : Form
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

        public done_form()
        {
            InitializeComponent();
        }

        public void DoneMessage(string result)
        {
            message.Text = result.ToString();
        }

        private void done_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void done_form_Load(object sender, EventArgs e)
        {
            donebutton.Select();
        }

        private void done_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
