using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms.RecipeDetails
{
    public partial class add_promotion : Form
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


        public add_promotion()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static bool saveEnable;

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void system_user_permissions()
        {
            try
            {
                //GetSetData.addFormCopyrights(lblCopyrights);
                //**********************************************************************************************
                savebutton.Visible = bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id));
                update_button.Visible = bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id));

                if (bool.Parse(data.UserPermissions("products_save", "pos_tbl_authorities_button_controls2", role_id)) == false && bool.Parse(data.UserPermissions("products_update", "pos_tbl_authorities_button_controls2", role_id)) == false)
                {
                    pnl_exit.Visible = false;
                }

                pnl_save.Visible = bool.Parse(data.UserPermissions("products_exit", "pos_tbl_authorities_button_controls2", role_id));
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fillAddProductsFormTextBoxes()
        {
            try
            {
                txtPromoTitle.Text = TextData.dealTitle;
                txtGroupTitle.Text = TextData.groupTitle;
                txtStartDate.Text = TextData.startDate;
                txtEndDate.Text = TextData.endDate;
                txtStartTime.Text = TextData.startTime;
                txtEndTime.Text = TextData.endTime;
                txtNewPrice.Text = TextData.newPrice;
                txtQuantity.Text = TextData.quantity.ToString();
                txtStatus.Text = TextData.status;

                txtDescription.Text = data.UserPermissions("note", "pos_promotions", "id", TextData.dealId.ToString());
                chkDiscountInPercentage.Checked = bool.Parse(data.UserPermissions("is_discount_in_percentage", "pos_promotions", "id", TextData.dealId.ToString()));
                TextData.discountInPercentage = data.UserPermissions("discount_percentage", "pos_promotions", "id", TextData.dealId.ToString());

                if (chkDiscountInPercentage.Checked)
                {
                    txtDiscount.Text = TextData.discountInPercentage;
                }
                else
                {
                    txtDiscount.Text = TextData.discount.ToString();
                }
            
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void enableSaveButton()
        {
            if (saveEnable == false)
            {
                update_button.Visible = false;
                savebutton.Visible = true;
                FormNamelabel.Text = "Create New Promotion";
            }
            else if (saveEnable == true)
            {
                savebutton.Visible = false;
                update_button.Visible = true;
                FormNamelabel.Text = "Update Promotion";
                fillAddProductsFormTextBoxes();
            }
        }

        private void refresh()
        {
            txtStartDate.Text = DateTime.Now.ToLongDateString();
            txtEndDate.Text = DateTime.Now.ToLongDateString();
            txtStartTime.Text = DateTime.Now.ToLongTimeString();
            txtEndTime.Text = DateTime.Now.ToLongTimeString();

            txtPromoTitle.Text = "";
            txtGroupTitle.Text = null;
            txtQuantity.Text = "0";
            txtNewPrice.Text = "0";
            txtDiscount.Text = "0";
            chkDiscountInPercentage.Checked = false;
            txtDescription.Text = "";

            txtStatus.SelectedIndex = 0;

            system_user_permissions();
            enableSaveButton();
            txtPromoTitle.Select();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void add_new_connection_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
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

        private void txt_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtQuantity.Text, e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtNewPrice.Text, e);
        }

        private void fillValuesInVariablesForUse()
        {
            try
            {
                //Store Data from Textboxes to textdata properties:
                TextData.dealTitle = txtPromoTitle.Text;
                TextData.groupTitle = txtGroupTitle.Text;
                TextData.startDate = txtStartDate.Text;
                TextData.endDate = txtEndDate.Text;
                TextData.startTime = txtStartTime.Text;
                TextData.endTime = txtEndTime.Text;
                TextData.remarks = txtDescription.Text;
                TextData.status = txtStatus.Text;
                // ******************************************************************

                if (TextData.remarks == "")
                {
                    TextData.remarks = "nill";
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_records_into_db()
        {
            try
            {
                fillValuesInVariablesForUse();

                if (txtPromoTitle.Text != "")
                {
                    if (txtGroupTitle.Text != "")
                    {
                        if (txtQuantity.Text != "")
                        {
                            if (txtDiscount.Text != "")
                            {
                                TextData.quantity = double.Parse(txtQuantity.Text);
                                TextData.newPrice = txtNewPrice.Text;

                                if (txtNewPrice.Text == "")
                                {
                                    TextData.newPrice = "0";
                                }

                                if (chkDiscountInPercentage.Checked)
                                {
                                    TextData.discountInPercentage = txtDiscount.Text;
                                    TextData.discount = 0;
                                    TextData.isDiscountInPercentage = true;
                                }
                                else
                                {
                                    TextData.discount = double.Parse(txtDiscount.Text);
                                    TextData.discountInPercentage = "0";
                                    TextData.isDiscountInPercentage = false;
                                }

                                //========================================================================================================

                                GetSetData.query = "select id from pos_promo_groups where (title = '" + TextData.groupTitle + "');";
                                int promo_group_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                //========================================================================================================

                                if (promo_group_id_db != 0)
                                {
                                    GetSetData.query = "insert into pos_promotions values ('" + TextData.dealTitle + "' , '" + TextData.startDate + "' , '" + TextData.endDate + "' , '" + TextData.startTime + "' , '" + TextData.endTime + "' , '" + TextData.newPrice + "' , '" + TextData.discount.ToString() + "' , '" + TextData.discountInPercentage + "' , '" + TextData.isDiscountInPercentage.ToString() + "' , '" + TextData.quantity.ToString() + "' , '" + TextData.remarks + "' , '" + TextData.status + "' , '" + promo_group_id_db.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    return true;
                                }

                            }
                            else
                            {
                                error.errorMessage("Please enter the discount!");
                                error.ShowDialog();
                            } 
                        }
                        else
                        {
                            error.errorMessage("Please enter the quantity!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the group title!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the promotion title!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }

            return false;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {

            if (insert_records_into_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                refresh();
            }
        }

        private bool update_records_db()
        {
            try
            {
                fillValuesInVariablesForUse();

                if (txtPromoTitle.Text != "")
                {
                    if (txtGroupTitle.Text != "")
                    {
                        if (txtQuantity.Text != "")
                        {
                            if (txtDiscount.Text != "")
                            {
                                TextData.quantity = double.Parse(txtQuantity.Text);
                                TextData.newPrice = txtNewPrice.Text;

                                if (txtNewPrice.Text == "")
                                {
                                    TextData.newPrice = "0";
                                }

                                if (chkDiscountInPercentage.Checked)
                                {
                                    TextData.discountInPercentage = txtDiscount.Text;
                                    TextData.discount = 0;
                                    TextData.isDiscountInPercentage = true;
                                }
                                else
                                {
                                    TextData.discount = double.Parse(txtDiscount.Text);
                                    TextData.discountInPercentage = "0";
                                    TextData.isDiscountInPercentage = false;
                                }

                                //========================================================================================================

                                GetSetData.query = "select id from pos_promo_groups where (title = '" + TextData.groupTitle + "');";
                                int promo_group_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                                //========================================================================================================

                                if (promo_group_id_db != 0)
                                {
                                    GetSetData.query = "update pos_promotions set title = '" + TextData.dealTitle + "' , start_date = '" + TextData.startDate + "' , end_date =  '" + TextData.endDate + "' , start_time = '" + TextData.startTime + "' , end_time = '" + TextData.endTime + "' , new_price = '" + TextData.newPrice + "' , discount = '" + TextData.discount.ToString() + "' , discount_percentage = '" + TextData.discountInPercentage + "' , is_discount_in_percentage = '" + TextData.isDiscountInPercentage.ToString() + "' , quantity = '" + TextData.quantity.ToString() + "' , note = '" + TextData.remarks + "' , status = '" + TextData.status + "' , promo_group_id = '" + promo_group_id_db.ToString() + "' where (id = '" + TextData.dealId.ToString() + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);

                                    return true;
                                }

                            }
                            else
                            {
                                error.errorMessage("Please enter the discount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the quantity!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Please select the group title!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the promotion title!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }

            return false;
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (update_records_db())
                {
                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void customer_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtGroupTitle, "fillComboBoxPromoGroups", "title");
        }

        private void btn_add_employee_Click(object sender, EventArgs e)
        {
            using (add_new_product main = new add_new_product())
            {
                add_new_product.role_id = role_id;
                add_new_product.saveEnable = false;
                main.ShowDialog();
            }
        }    

    }
}
