using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Datalayer;
using Message_box_info.forms;
using Purchase_info.All_Purchases_List;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.LoginInfo.controllers;

namespace Demands_info.forms
{
    public partial class formDemandList : Form
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

        public formDemandList()
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel9, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
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
                formAddNewDemand.role_id = role_id;

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("demand_list_print", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_print.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("demand_list_delete", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_delete.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("demand_list_new", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_new.Visible = bool.Parse(GetSetData.Data);

                //// ***************************************************************************************************
                //GetSetData.Data = data.UserPermissions("demand_list_returns", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                //pnl_modify.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("demand_list_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());
                pnl_refresh.Visible = bool.Parse(GetSetData.Data);

                //GetSetData.addFormCopyrights(lblCopyrights);

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

        private void fun_refresh()
        {
            productDetailGridView1.DataSource = null;
            productDetailGridView1.Rows.Clear();
            productDetailGridView1.Refresh();
        }

        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = "select * from ViewDemandList";

                if (condition == "search")
                {
                    GetSetData.query = GetSetData.query + " where ([Date] like '" + SearchByBox.Text + "%' or [Demand No] like '" + SearchByBox.Text + "%' or [Employee] like '" + SearchByBox.Text + "%' or [Supplier] like '" + SearchByBox.Text + "%');";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formDemandList_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                system_user_permissions();
                FillGridViewUsingPagination("");
                SearchByBox.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Demands Details Form", "Exit...", role_id);
            Button_controls.MenuScreen();
            this.Close();
        }

        private void addNewDetails()
        {
            //GetSetData.SaveLogHistoryDetails("Demands Details Form", "Add new demand button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo);
            formAddNewDemand.saveEnable = false;
            formAddNewDemand _obj = new formAddNewDemand();
            _obj.Show();

            this.Dispose();
        }

        private void Addnewbutton_Click(object sender, EventArgs e)
        {
            addNewDetails();
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Demand No"].Value.ToString();

                GetSetData.query = @"select demand_id from pos_demand_list where bill_no = '" + TextData.billNo.ToString() + "' ;";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_demand_items where demand_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_demand_list where demand_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                GetSetData.SaveLogHistoryDetails("Demands Details Form", "Deleting demand invoice [" + TextData.billNo + "]", role_id);
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
            TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Demand No"].Value.ToString();

            try
            {
                this.Opacity = .850;
                sure.Message_choose("Are you sure you want to delete Demand No [" + TextData.billNo.ToString() + "]");
                sure.ShowDialog();
                this.Opacity = .999;

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    SearchByBox.Text = "";
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

        private void SearchByBox_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            SearchByBox.Text = "";
            fun_refresh();
            FillGridViewUsingPagination("");
        }

        private void ModifyPurchasingDetails()
        {
            try
            {
                GetSetData.Data = data.UserPermissions("demand_list_modify", "pos_tbl_authorities_button_controls2", "role_id", role_id.ToString());

                if (GetSetData.Data == "True")
                {
                    TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Demand No"].Value.ToString();
                    TextData.dates = productDetailGridView.SelectedRows[0].Cells["Date"].Value.ToString();
                    TextData.supplier = productDetailGridView.SelectedRows[0].Cells["Supplier"].Value.ToString();
                    formAddNewDemand.saveEnable = true;

                    formAddNewDemand _obj = new formAddNewDemand();
                    _obj.Show();
                    this.Dispose();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            ModifyPurchasingDetails();
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Demand No"].Value.ToString();
                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureDemandItems", TextData.billNo);
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
            GetSetData.SaveLogHistoryDetails("Demands Details Form", "Print button click...", role_id);
            GetSetData.ResetPageNumbers(lblPageNo); 
            PurchaseList list = new PurchaseList();
            list.ShowDialog();
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

        private void formDemandList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                GetSetData.SaveLogHistoryDetails("Demands Details Form", "Print button click...", role_id);
                GetSetData.ResetPageNumbers(lblPageNo);
                PurchaseList list = new PurchaseList();
                list.ShowDialog();
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
            else if (e.Control && e.KeyCode == Keys.F)
            {
                SearchByBox.Select();
            }
        }
    }
}
