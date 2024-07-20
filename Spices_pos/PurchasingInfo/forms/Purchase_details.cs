using System;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using System.Data.SqlClient;
using Purchase_info.All_Purchases_List;
using RefereningMaterial;
using Investors_info.reports;
using Spices_pos.DashboardInfo.Forms;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.PurchasingInfo.controllers;

namespace Purchase_info.forms
{
    public partial class Purchase_details : Form
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

        public Purchase_details()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int user_id = 0;
        public static int role_id = 0;
        private bool option = true;

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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel9, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, refresh_button);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnImeiDetails);
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
                addNewPurchase.role_id = role_id;
                addNewPurchase.user_id = user_id;
                GetSetData.addFormCopyrights(lblCopyrights);
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchase_details_print", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchase_details_delete", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchase_details_new", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_add_new.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchase_details_returns", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_modify.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("purchase_details_refresh", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());
                pnl_refresh.Visible = bool.Parse(GetSetData.Data);

                sidePanel.Visible = true;
                setSideBarInSidePanel();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void setSideBarInSidePanel()
        {
            try
            {
                sidebarUserControl sidebar = new sidebarUserControl();

                sidebar.user_id = user_id;
                sidebar.role_id = role_id;

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
                sidebar.Dock = DockStyle.Fill;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clearGridView()
        {
            this.productDetailGridView1.DataSource = null;
            this.productDetailGridView1.Refresh();
            productDetailGridView1.Rows.Clear();
            productDetailGridView1.Columns.Clear();
        }

        private void  fun_refresh()
        {
            productDetailGridView1.DataSource = null;
            productDetailGridView1.Rows.Clear();
            productDetailGridView1.Refresh();
        }

        private void clearDataGridViewItems()
        {
            this.productDetailGridView.DataSource = null;
            this.productDetailGridView.Refresh();
            productDetailGridView.Rows.Clear();
            productDetailGridView.Columns.Clear();
        }

        private void FillGridViewUsingPagination(string condition1, string condition2)
        {
            try
            {
                if (condition1 == "purchase")
                {
                    GetSetData.query = "select top 500 * from ViewPurchasingDetails";
                }
                else if(condition1 == "purReturn")
                {
                    GetSetData.query = "select top 500 * from ViewPurchaseReturnDetails";
                }

                if (condition2 == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Bill No] like '%" + SearchByBox.Text + "%' or [Invoice No] like '%" + SearchByBox.Text + "%' or [Date] like '%" + SearchByBox.Text + "%' or [Supplier] like '%" + SearchByBox.Text + "%') order by [Bill No] desc;";
                }
                else
                {
                    GetSetData.query = GetSetData.query + " order by [Bill No] desc";
                }

                clearDataGridViewItems();
                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
                createClockOutButtonInGridView();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void createClockOutButtonInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Complete";
            btn.Name = "Complete";
            btn.Text = "Complete";
            btn.Width = 65;
            btn.MinimumWidth = 10;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            btn.DefaultCellStyle.Font = new Font("Century Gothic", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView.Columns.Add(btn);
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Exit...", role_id);
            clearGridView();
            Menus.login_checked = "";
            Menus main = new Menus();
            main.Show();

            this.Dispose();
        }

        private void addNewDetails()
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            addNewPurchase.saveEnable = false;
            addNewPurchase.user_id = user_id;
            Button_controls.Add_purchase_buttons();
            this.Dispose();
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();   
        }

        private void Purchase_details_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                SearchByBox.Text = "";
                option = true;
                FillGridViewUsingPagination("purchase", "");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void SearchByBox_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);

            if (option == true)
            {
                FillGridViewUsingPagination("purchase", "search");
            }
            else
            {
                FillGridViewUsingPagination("purReturn", "search");
            }
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            option = true;
            SearchByBox.Text = "";
            fun_refresh();
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("purchase", "");
        }

        private void ModifyPurchasingDetails()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("purchase_details_refresh", "pos_tbl_authorities_button_controls1", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    addNewPurchase.user_id = user_id;

                    if (option)
                    {
                        addNewPurchase.isReturn = false;
                    }
                    else
                    {
                        addNewPurchase.isReturn = true;
                    }

                    TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Bill No"].Value.ToString();
                    TextData.send_billNo = TextData.billNo;
                    TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Invoice No"].Value.ToString();
                    TextData.invoiceNoKey = TextData.invoiceNo;
                    TextData.dates = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.supplier = productDetailGridView.SelectedRows[0].Cells["Supplier"].Value.ToString();
                    addNewPurchase.saveEnable = true;

                    //GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Updating purchasing invoice # [" + TextData.billNo + "] details (Modify button click...)", role_id);
                    Button_controls.Add_purchase_buttons();
                    this.Dispose();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void updates_purchase_details(object sender, DataGridViewCellEventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo); 
            AllPurchaseList report = new AllPurchaseList();
            report.ShowDialog();
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "View Returned Items button click...", role_id);
            SearchByBox.Text = "";
            option = false;
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("purReturn", "");
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Bill No"].Value.ToString();
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Invoice No"].Value.ToString();
                TextData.paid = double.Parse(productDetailGridView.SelectedRows[0].Cells["Paid Amount"].Value.ToString());

                GetSetData.query = @"select purchase_id from pos_purchase where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================


                string query = "select prod_id, quantity from pos_purchased_items where (purchase_id = '" + GetSetData.Ids.ToString() + "');";


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    double totalInventory = data.NumericValues("quantity", "pos_stock_details", "prod_id", reader["prod_id"].ToString());
                    double costPrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", reader["prod_id"].ToString());
                    double salePrice = data.NumericValues("sale_price", "pos_stock_details", "prod_id", reader["prod_id"].ToString());

                    totalInventory -= double.Parse(reader["quantity"].ToString());
                    
                    TextData.total_pur_price = costPrice * totalInventory;
                    TextData.total_sale_price = salePrice * totalInventory;


                    GetSetData.query = @"update pos_stock_details set quantity = '" + totalInventory.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (prod_id = '" + reader["prod_id"].ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                reader.Close();


                GetSetData.query = @"delete from pos_purchased_items where purchase_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_purchase where purchase_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_company_transactions where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                string capital = data.UserPermissions("round(total_capital, 2)", "pos_capital");
                GetSetData.Data = data.UserPermissions("useCapital", "pos_general_settings");

                if (GetSetData.Data == "Yes")
                {
                    if (capital != "NULL" && capital != "")
                    {
                        TextData.paid = double.Parse(capital) + TextData.paid;
                    }

                    if (TextData.paid >= 0)
                    {
                        GetSetData.query = "update pos_capital set total_capital = '" + TextData.paid.ToString() + "';";
                        data.insertUpdateCreateOrDelete(GetSetData.query);
                    }
                }
                // *****************************************************************************************

                //GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Deleting purchasing invoice # [" + TextData.billNo + "] details", role_id);
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private bool fun_delete_products2()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Bill No"].Value.ToString();
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Invoice No"].Value.ToString();

                GetSetData.query = @"select pur_return_id from pos_purchase_return where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                string query = "select prod_id, quantity from pos_pur_return_items where (purchase_id = '" + GetSetData.Ids.ToString() + "');";


                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    double totalInventory = data.NumericValues("quantity", "pos_stock_details", "prod_id", reader["prod_id"].ToString());
                    double costPrice = data.NumericValues("pur_price", "pos_stock_details", "prod_id", reader["prod_id"].ToString());
                    double salePrice = data.NumericValues("sale_price", "pos_stock_details", "prod_id", reader["prod_id"].ToString());

                    totalInventory += double.Parse(reader["quantity"].ToString());

                    TextData.total_pur_price = costPrice * totalInventory;
                    TextData.total_sale_price = salePrice * totalInventory;


                    GetSetData.query = @"update pos_stock_details set quantity = '" + totalInventory.ToString() + "' , total_pur_price = '" + TextData.total_pur_price.ToString() + "' , total_sale_price = '" + TextData.total_sale_price.ToString() + "' where (prod_id = '" + reader["prod_id"].ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                reader.Close();


                GetSetData.query = @"delete from pos_pur_return_items where purchase_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_purchase_return where pur_return_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_company_transactions where bill_no = '" + TextData.billNo.ToString() + "' and invoice_no = '" + TextData.invoiceNo.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Deleting purchasing return invoice # [" + TextData.billNo + "] details", role_id);
                
                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Bill No"].Value.ToString();

            try
            {
                if (option == true)
                {
                    sure.Message_choose("Are you sure you want to delete invoice [" + TextData.billNo.ToString() + "]");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        fun_delete_products();
                        GetSetData.ResetPageNumbers(lblPageNo);
                        FillGridViewUsingPagination("purchase", "");
                        SearchByBox.Text = "";
                    }
                }
                else
                {
                    sure.Message_choose("Are you sure you want to delete invoice [" + TextData.billNo.ToString() + "]");
                    sure.ShowDialog();

                    if (form_sure_message.sure == true)
                    {
                        fun_delete_products2();
                        GetSetData.ResetPageNumbers(lblPageNo);
                        FillGridViewUsingPagination("purReturn", "");
                        SearchByBox.Text = "";
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage("[" + TextData.billNo.ToString() + "'] cannot be deleted!");
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteSelectedDetails();
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Bill No"].Value.ToString();
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Invoice No"].Value.ToString();

                if (option == true)
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedurePurchasedItems", TextData.billNo, TextData.invoiceNo);
                }
                else
                {
                    GetSetData.FillDataGridView(productDetailGridView1, "ProcedurePurchaseReturnItems", TextData.billNo, TextData.invoiceNo);
                }
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void show_all_Click_1(object sender, EventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //TrunOffFormLevelDoubleBuffering();
            //originalExStyle = -1;
            //enableFormLevelDoubleBuffering = true;
        }

        private void btnImeiDetails_Click(object sender, EventArgs e)
        {
            try
            {
                formIMEIDetails imei = new formIMEIDetails();
                imei.ShowDialog();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Purchase_details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                AllPurchaseList report = new AllPurchaseList();
                report.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                addNewDetails();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                ModifyPurchasingDetails();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                GetSetData.SaveLogHistoryDetails("Purchasing Details Form", "View Returned Items button click...", role_id);
                SearchByBox.Text = "";
                option = false;
                GetSetData.ResetPageNumbers(lblPageNo);
                FillGridViewUsingPagination("purReturn", "");
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                SearchByBox.Select();
            }
        }

        private void productDetailGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "Complete")
            {
                string selectedID = productDetailGridView.SelectedRows[0].Cells["ID"].Value.ToString();

                if (option)
                {
                    GetSetData.query = @"update pos_purchase set status = '1' where (purchase_id = '" + selectedID +"');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    GetSetData.query = @"update pos_purchase_return set status = '1' where (pur_return_id = '" + selectedID + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                done.DoneMessage("Select invoice is completed successfully.");
                done.ShowDialog();
            }
        }

        private void SearchByBox_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
