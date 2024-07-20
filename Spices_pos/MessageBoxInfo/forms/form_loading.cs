using System;
using System.Windows.Forms;

namespace Message_box_info.forms
{
    public partial class form_loading : Form
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

        public form_loading()
        {
            InitializeComponent();
        }

        public void SetLoadingMessage(string message)
        {
            loadingLabel.Text = message;
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
