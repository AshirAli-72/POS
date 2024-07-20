using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;


namespace Products_info.forms
{
    public partial class setCommissionItems : Form
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
        public setCommissionItems()
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
        public static string commissionID = "";
        public static int count = 0;

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_expired_items);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void FillGridViewUsingPagination(string condition1, string condition2)
        {
            try
            {
                if (condition1 == "grouped")
                {
                    //GetSetData.query = "select * from ViewGroupedItems";

                    GetSetData.query = @"SELECT pos_products.product_id AS PID, pos_stock_details.stock_id AS SID, pos_products.prod_name AS [Product Name], pos_stock_details.item_barcode AS Barcode, pos_category.title AS Category, 
                                        pos_brand.brand_title AS Brand, pos_stock_details.date_of_expiry AS Expiry, pos_stock_details.quantity AS Stock, pos_stock_details.pur_price AS Purchase, 
                                        pos_stock_details.sale_price AS [Sale Price], pos_products.status
                                        FROM pos_employee_commission INNER JOIN pos_employee_commission_detail ON pos_employee_commission.commission_id = pos_employee_commission_detail.commission_id INNER JOIN
                                        pos_products on pos_products.product_id = pos_employee_commission_detail.prod_id inner join pos_stock_details on pos_stock_details.stock_id = pos_employee_commission_detail.stock_id
                                        inner join pos_category on pos_category.category_id = pos_products.category_id inner join pos_brand on pos_brand.brand_id = pos_products.brand_id
                                        where pos_employee_commission.commission_id = '" + commissionID + "';";

                    if (condition2 == "search")
                    {
                        GetSetData.query = "select * from ViewGroupedItems where ([Product Name] like '" + search_box.Text + "%' or [Barcode] like '" + search_box.Text + "%' or [Category] like '" + search_box.Text + "%' or [Brand] like '" + search_box.Text + "%' or [Sub Category] like '" + search_box.Text + "%');";
                    }
                }
                else if (condition1 == "groupWise")
                {
                    GetSetData.query = "select * from ViewPromoGroupItems where ([Group Title] = '" + txtGroupTitle.Text + "') and ([Status] = 'Enabled')";

                    if (condition2 == "search")
                    {
                        GetSetData.query = "select * from ViewPromoGroupItems where ([Product Name] like '" + search_box.Text + "%' or [Barcode] like '" + search_box.Text + "%' or [Category] like '" + search_box.Text + "%' or [Brand] like '" + search_box.Text + "%' or [Sub Category] like '" + search_box.Text + "%' or [Group Title] like '" + search_box.Text + "%');";
                    }
                }
                else if (condition1 == "category")
                {
                    GetSetData.query = "select * from ViewGroupedItems where ([Category] = '" + txtSearchItems.Text +"');";
                }
                else if (condition1 == "brand")
                {
                    GetSetData.query = "select * from ViewGroupedItems where ([Brand] = '" + txtSearchItems.Text +"');";
                }

                GetSetData.FillDataGridViewWithoutPagination(ProductsDetailGridView, GetSetData.query);
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void RefreshFieldShelfTitle()
        {

            GetSetData.FillComboBoxUsingProcedures(txtGroupTitle, "fillComboBoxPromoGroups", "title");
        }

        private void RefreshFieldCategory()
        {
            GetSetData.FillComboBoxUsingProcedures(txtSearchItems, "fillComboBoxCategory", "title");
        }

        private void RefreshFieldBrand()
        {
            GetSetData.FillComboBoxUsingProcedures(txtSearchItems, "fillComboBoxBrand", "brand_title");
        }

        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchBy.SelectedIndex == 1)
                {
                    RefreshFieldCategory();
                }
                else if (txtSearchBy.SelectedIndex == 2)
                {
                    RefreshFieldBrand();
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void txtSearchItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearGridView();

                if (txtSearchBy.SelectedIndex == 1)
                {
                    FillGridViewUsingPagination("category", "");
                }
                else if (txtSearchBy.SelectedIndex == 2)
                {
                    FillGridViewUsingPagination("brand", "");
                }
        
                createCheckBoxInGridView();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
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

        private void setCommissionItems_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                GetSetData.addFormCopyrights(lblCopyrights);
                //formAddShelfTitle.role_id = role_id;
                //RefreshFieldShelfTitle();
                clearGridView();
                FillGridViewUsingPagination("grouped", "");
                createCheckBoxInGridView();

                search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btnAddNewShelf_Click(object sender, EventArgs e)
        {
            //addGroup _obj = new addGroup();
            //_obj.ShowDialog();
            //RefreshFieldShelfTitle();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            search_box.Text = "";
            GetSetData.ResetPageNumbers(lblPageNo);
            RefreshFieldShelfTitle();
            clearGridView();
            FillGridViewUsingPagination("grouped", "");
            createCheckBoxInGridView();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            clearGridView();

            if (chkShelfSearch.Checked == true)
            {
                FillGridViewUsingPagination("groupWise", "search");
            }
            else
            {
                FillGridViewUsingPagination("grouped","search");
            }

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

                if (commissionID != "")
                {
                    foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                    {
                        GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                        if (GetSetData.checkBoxSelected)
                        {
                            string product_id = item.Cells[0].Value.ToString();
                            string stock_id = item.Cells[1].Value.ToString();

                            GetSetData.query = "select commission_detail_id from pos_employee_commission_detail where (prod_id = '" + product_id + "') and (stock_id = '" + stock_id + "') and (commission_id = '" + commissionID + "');";
                            GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (GetSetData.fks == 0)
                            {
                                GetSetData.query = "insert into pos_employee_commission_detail values ('" + product_id + "' , '" + stock_id + "' , '" + commissionID + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);
                            }     
                        }
                    }

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please select the shelf title first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                data.Disconnect();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saved_gouped_items_db();

            search_box.Text = "";
            GetSetData.ResetPageNumbers(lblPageNo);
            RefreshFieldShelfTitle();
            clearGridView();
            FillGridViewUsingPagination("grouped", "");
            createCheckBoxInGridView();
        }

        private bool Remove_gouped_items_db()
        {
            try
            {
                data.Connect();

                if (commissionID != "")
                {
                    foreach (DataGridViewRow item in ProductsDetailGridView.Rows)
                    {
                        GetSetData.checkBoxSelected = Convert.ToBoolean(item.Cells["select"].Value);

                        if (GetSetData.checkBoxSelected)
                        {
                            string product_id = item.Cells[0].Value.ToString();
                            string stock_id = item.Cells[1].Value.ToString();

                           
                            GetSetData.query = "delete from pos_employee_commission_detail where (prod_id = '" + product_id + "') and (stock_id = '" + stock_id + "') and (commission_id = '" + commissionID + "');";
                            data.insertUpdateCreateOrDelete(GetSetData.query);
                        }
                     }
                
                    done.DoneMessage("Successfully Removed!");
                    done.ShowDialog();

                    return true;
                }
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

            return false;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            Remove_gouped_items_db();

            search_box.Text = "";
            GetSetData.ResetPageNumbers(lblPageNo);
            RefreshFieldShelfTitle();
            clearGridView();
            FillGridViewUsingPagination("grouped", "");
            createCheckBoxInGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtGroupTitle.Text != "")
            {
                //GetSetData.SaveLogHistoryDetails("Shelf Items Details Form", "Searching [" + txtShelfTitle + "] (search button click...)", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                clearGridView();
                FillGridViewUsingPagination("groupWise", "");
                search_box.Text = "";
                createCheckBoxInGridView();
            }
            else
            {
                error.errorMessage("Please select the group title first!");
                error.ShowDialog();
            }
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

       
    }
}
