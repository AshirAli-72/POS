using System;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class formContractPolicies : Form
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

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        public formContractPolicies()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        private void setFormColorsDynamically()
        {
            try
            {
                int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
                int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
                int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

                int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

                int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

                //****************************************************************

                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formContractPolicies_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            fill_ContractPolicies_details();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fill_ContractPolicies_details()
        {
            try
            {
                TextData.backup_path = data.UserPermissions("condition1", "pos_contractPolicies");
                TextData.pic_path = data.UserPermissions("condition2", "pos_contractPolicies");
                TextData.auto_backup = data.UserPermissions("condition3", "pos_contractPolicies");
                TextData.show_graph = data.UserPermissions("condition4", "pos_contractPolicies");
                TextData.page_size = data.UserPermissions("condition5", "pos_contractPolicies");
                TextData.pos_security = data.UserPermissions("condition6", "pos_contractPolicies");
                TextData.auto_expiry = data.UserPermissions("condition7", "pos_contractPolicies");
                TextData.box_notifications = data.UserPermissions("condition8", "pos_contractPolicies");
                TextData.show_discount = data.UserPermissions("condition9", "pos_contractPolicies");
                TextData.box_discount = data.UserPermissions("condition10", "pos_contractPolicies");
                TextData.show_price = data.UserPermissions("condition11", "pos_contractPolicies");
                TextData.show_tax = data.UserPermissions("condition12", "pos_contractPolicies");
                TextData.show_hold = data.UserPermissions("condition13", "pos_contractPolicies");
                TextData.show_unhold = data.UserPermissions("condition14", "pos_contractPolicies");

                txtContract_condition1.Text = TextData.backup_path;
                txtContract_condition2.Text = TextData.pic_path;
                txtContract_condition3.Text = TextData.auto_backup;
                txtContract_condition4.Text = TextData.show_graph;
                txtContract_condition5.Text = TextData.page_size;
                txtContract_condition6.Text = TextData.pos_security;
                txtContract_condition7.Text = TextData.auto_expiry;
                txtContract_condition8.Text = TextData.box_notifications;
                txtContract_condition9.Text = TextData.show_discount;
                txtContract_condition10.Text = TextData.box_discount;
                txtContract_condition11.Text = TextData.show_price;
                txtContract_condition12.Text = TextData.show_tax;
                txtContract_condition13.Text = TextData.show_hold;
                txtContract_condition14.Text = TextData.show_unhold;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnSaveContractPolicies_Click(object sender, EventArgs e)
        {
            try
            {
                TextData.backup_path = txtContract_condition1.Text;
                TextData.pic_path = txtContract_condition2.Text;
                TextData.auto_backup = txtContract_condition3.Text;
                TextData.show_graph = txtContract_condition4.Text;
                TextData.page_size = txtContract_condition5.Text;
                TextData.pos_security = txtContract_condition6.Text;
                TextData.auto_expiry = txtContract_condition7.Text;
                TextData.box_notifications = txtContract_condition8.Text;
                TextData.show_discount = txtContract_condition9.Text;
                TextData.box_discount = txtContract_condition10.Text;
                TextData.show_price = txtContract_condition11.Text;
                TextData.show_tax = txtContract_condition12.Text;
                TextData.show_hold = txtContract_condition13.Text;
                TextData.show_unhold = txtContract_condition14.Text;

                GetSetData.SaveLogHistoryDetails("Settings Form", "Saving/Updating contract form Policies...", role_id);
                buttonControls.insert_contractPolicies_settings();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
