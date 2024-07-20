using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Login_info.controllers;
using System.Data.SqlClient;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Settings_info.controllers;
using Supplier_Chain_info.forms;
using System.Drawing.Printing;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Expenses_info.forms
{
    public partial class creditCardApiSettings : Form
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

        public creditCardApiSettings()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();   
        public static int role_id = 0;
        public static string user_id = "";


        private void getCreditCardApiDetails()
        {
            txtApiUrl.Text = data.UserPermissions("api_url", "pos_credit_card_api_settings"); 
            txtRegisterId.Text = data.UserPermissions("register_id", "pos_credit_card_api_settings"); 
            txtAuthKey.Text = data.UserPermissions("authentication_key", "pos_credit_card_api_settings");
            
            
            txtWebLinkUrl.Text = data.UserPermissions("api_url", "pos_weblink_api_settings"); 
            txtStoreId.Text = data.UserPermissions("store_id", "pos_weblink_api_settings"); 
            txtToken.Text = GetSetData.GetSystemMacAddress(); 
        }  

        private void creditCardApiSettings_Load(object sender, EventArgs e)
        {
            try
            {
                getCreditCardApiDetails();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

      
        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool insert_record_db()
        {
            try
            {
                if (txtApiUrl.Text != "")
                {
                    if (txtRegisterId.Text != "")
                    {
                        if (txtAuthKey.Text != "")
                        {
                            GetSetData.query = @"select id from pos_credit_card_api_settings;";
                            int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (is_already_exist == 0)
                            {
                                GetSetData.query = @"insert into pos_credit_card_api_settings values ('" + txtApiUrl.Text + "' , '" + txtRegisterId.Text + "' , '" + txtAuthKey.Text + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            else
                            {
                                GetSetData.query = @"update pos_credit_card_api_settings set  api_url =  '" + txtApiUrl.Text + "' , register_id = '" + txtRegisterId.Text + "' , authentication_key = '" + txtAuthKey.Text + "' where (id = '" + is_already_exist.ToString() +"');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            

                            //Weblink Api Settings*******************************************
                            
                            GetSetData.query = @"select id from pos_weblink_api_settings;";
                            int is_weblink_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (is_weblink_already_exist == 0)
                            {
                                GetSetData.query = @"insert into pos_weblink_api_settings values ('" + txtWebLinkUrl.Text + "' , '" + txtStoreId.Text + "' , '" + txtToken.Text + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }
                            else
                            {
                                GetSetData.query = @"update pos_weblink_api_settings set  api_url =  '" + txtWebLinkUrl.Text + "' , store_id = '" + txtStoreId.Text + "' , token = '" + txtToken.Text + "' where (id = '" + is_weblink_already_exist.ToString() +"');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }

                            done.DoneMessage("Successfully Saved!");
                            done.ShowDialog();

                            return true;
                        }
                        else
                        {
                            error.errorMessage("Enter the credit card machine authentication key!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Enter the credit card machine register id!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Enter the credit card machine api url!");
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
            insert_record_db();
        }

    }
}
