using System;
using System.Windows.Forms;
using Login_info.controllers;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Banking_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms
{
    public partial class formChequeDetails : Form
    {
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams handleParam = base.CreateParams;
        //        handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
        //        return handleParam;
        //    }
        //}

        public formChequeDetails()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Add New Cheques Form", "Exit...", role_id); 
            this.Close();
        }

        private void RefreshFieldBankTitle()
        {
            GetSetData.FillComboBoxUsingProcedures(txtBankTitle, "fillComboBoxBankTitle", "bank_title");
        }

        private void refresh()
        {
            txtDate.Text = DateTime.Now.ToLongDateString();
            txtBouncedDate.Text = DateTime.Now.ToLongDateString();
            txtBankTitle.Text = "-- select --";
            txtAccount.Text = "";
            txtAmount.Text = "0";
            txtRemarks.Text = "";
            txtChequeNo.Text = "";
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void formChequeDetails_Load(object sender, EventArgs e)
        {
            form_bank_title.role_id = role_id;
            GetSetData.addFormCopyrights(lblCopyrights);
            refresh();
            txtDate.Select();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtAmount.Text, e);
        }

        private void txtBankTitle_Enter(object sender, EventArgs e)
        {
            RefreshFieldBankTitle();
        }

        private void add_category_Click(object sender, EventArgs e)
        {
            using (form_bank_title add_customer = new form_bank_title())
            {
                add_customer.ShowDialog();
                RefreshFieldBankTitle();
            }
        }

        private bool insert_record_db()
        {
            try
            {
                TextData.prod_name = txtBankTitle.Text;
                TextData.prod_state = txtAccount.Text;
                TextData.comments = txtRemarks.Text;
                TextData.totalAmount = double.Parse(txtAmount.Text);

                if (txtAmount.Text == "")
                {
                    TextData.totalAmount = 0;
                }

                if (txtRemarks.Text == "")
                {
                    TextData.comments = "nill";
                }

                if (txtBankTitle.Text != "")
                {
                    if (txtAccount.Text != "")
                    {
                        if (txtChequeNo.Text != "")
                        {
                            GetSetData.query = "select customer_id from pos_customers where (full_name = '" + TextData.customer_name + "') and (cus_code = '" + TextData.customerCode + "');";
                            GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                            //*****************************************************************************************
                            GetSetData.fks = data.UserPermissionsIds("bank_id", "pos_bank", "bank_title", TextData.prod_name.ToString());

                            GetSetData.query = @"insert into pos_customerChequeDetails values ('" + TextData.billNo + "' , '" + txtDate.Text + "' , '" + txtBouncedDate.Text + "' , '" + TextData.prod_state + "' , '" + txtChequeNo.Text +"' , '" + TextData.totalAmount.ToString() + "' , '" + TextData.comments.ToString() + "' , '" + txtStatus.Text + "' , '" + GetSetData.Ids.ToString() + "' , '" + GetSetData.fks.ToString() + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);

                            GetSetData.SaveLogHistoryDetails("Add New Cheques Form", "Saving cheque [" + TextData.billNo + "  " + txtDate.Text + "] details", role_id); 
                            return true;
                        }
                        else
                        {
                            error.errorMessage("Please enter cheque number!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please enter account number!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please select bank title!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if(insert_record_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                refresh();
            }
        }

        private void formChequeDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
