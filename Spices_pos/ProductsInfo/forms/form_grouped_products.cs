using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.ProductsInfo.controllers;

namespace Products_info.forms
{
    public partial class form_grouped_products : Form
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

        public form_grouped_products()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        //private string choose = "false";

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }
        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select top 500 * from ViewGroupedItems";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Product Name] like '%" + search_box.Text + "%' or [Barcode] like '%" + search_box.Text + "%' or [Category] like '%" + search_box.Text + "%' or [Brand] like '%" + search_box.Text + "%' or [Sub Category] like '%" + search_box.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Regular Items Details Form", "Exit...", role_id);
            Button_controls.Products_detail_buttons();
            this.Close();
        }

        private void clearGridView()
        {
            this.ProductsDetailGridView.DataSource = null;
            this.ProductsDetailGridView.Refresh();
            ProductsDetailGridView.Rows.Clear();
            ProductsDetailGridView.Columns.Clear();
        }

        private void createCheckBoxInGridView()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Select";
            chk.Name = "select";
            chk.Width = 15;
            ProductsDetailGridView.Columns.Add(chk);
        }

        private void form_grouped_products_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;
                GetSetData.addFormCopyrights(lblCopyrights);
                clearGridView();
                FillGridViewUsingPagination("");
                search_box.Text = "";
                createCheckBoxInGridView();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();
            FillGridViewUsingPagination("");
            search_box.Text = "";
            createCheckBoxInGridView();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();
            FillGridViewUsingPagination("search");
            createCheckBoxInGridView();
        }

        private void btn_check_all_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                {
                    item.Cells["select"].Value = true;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_uncheck_all_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                {
                    item.Cells["select"].Value = false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void saved_gouped_items_db()
        {
            try
            {
                data.Connect();

                foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                {
                    GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                    if (GetSetData.checkBoxSelected)
                    {
                        string product_id = item.Cells[0].Value.ToString();
                        string stock_id = item.Cells[1].Value.ToString();

                        //GetSetData.query = "select product_id from pos_products where prod_name = '" + TextData.prod_name.ToString() + "' and barcode = '" + TextData.barcode.ToString() + "';";
                        //GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "select group_id from pos_grouped_items where (prod_id = '" + product_id + "') and (stock_id = '" + stock_id + "');";
                        GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (GetSetData.fks == 0)
                        {
                            GetSetData.query = "insert into pos_grouped_items values ('" + product_id + "' , '" + stock_id + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }

                        //GetSetData.SaveLogHistoryDetails("Regular Items Details Form", "Saving [" + TextData.prod_name + "  " + TextData.barcode + "] as Regular items", role_id);
                    }
                }

                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
                //return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //return false;
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saved_gouped_items_db();
        }

        private bool Remove_gouped_items_db()
        {
            try
            {
                data.Connect();
                foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                {
                    GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                    if (GetSetData.checkBoxSelected)
                    {
                        string product_id = item.Cells[0].Value.ToString();
                        string stock_id = item.Cells[1].Value.ToString();

                        //GetSetData.query = "select product_id from pos_products where prod_name = '" + TextData.prod_name.ToString() + "' and barcode = '" + TextData.barcode.ToString() + "';";
                        //GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        GetSetData.query = "delete from pos_grouped_items where (prod_id = '" + product_id + "') and (stock_id = '" + stock_id + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);

                        //GetSetData.SaveLogHistoryDetails("Regular Items Details Form", "Remove [" + TextData.prod_name + "  " + TextData.barcode + "] from Regular items", role_id);
                    }
                }

                done.DoneMessage("Successfully Removed!");
                done.ShowDialog();

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            Remove_gouped_items_db();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            clearGridView();
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
            createCheckBoxInGridView();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            clearGridView();
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
            createCheckBoxInGridView();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
