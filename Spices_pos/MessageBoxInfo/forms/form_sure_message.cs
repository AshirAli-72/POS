using System;
using System.Windows.Forms;
using Recoverier_info.forms;

namespace Message_box_info.forms
{
    public partial class form_sure_message : Form
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

        public form_sure_message()
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
            Customer_sales_recovery.message_choose = true;
            sure = true;
            this.Close();
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            Customer_sales_recovery.message_choose = false;
            sure = false;

            this.Close();
        }

        private void form_sure_message_Load(object sender, EventArgs e)
        {
            btn_no.Select();
        }
    }
}
