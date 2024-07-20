using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Login_info.controllers;

namespace Stock_management.forms
{
    public partial class inventoryPopForm : Form
    {
        public inventoryPopForm()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            Button_controls.mainMenu_buttons();
            this.Close();
        }

        private void LowInventorybutton_Click(object sender, EventArgs e)
        {
            Button_controls.LowInventory_buttons();
            this.Close();
        }

        private void WholeInventorybutton_Click(object sender, EventArgs e)
        {
            Button_controls.WholeInventory_buttons();
            this.Close();
        }
    }
}
