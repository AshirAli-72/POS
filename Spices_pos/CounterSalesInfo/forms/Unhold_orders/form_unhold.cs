using System;
using System.Windows.Forms;
using Login_info.controllers;
using Datalayer;
using Message_box_info.forms;
using CounterSales_info.LoyalCustomerSales;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms.Unhold_orders
{
    public partial class form_unhold : Form
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
        public form_unhold()
        {
            TextData.billNo = "";
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
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
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, button9);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btn_refresh);
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
                GetSetData.query = "select * from ViewUnholdItems order by [Date] desc";

                if (condition == "search")
                {
                    GetSetData.query = "select * from ViewUnholdItems where ([Date] like '%" + search_box.Text + "%' or [Receipt No] like '%" + search_box.Text + "%' or [Customer] like '%" + search_box.Text + "%' or [Employee] like '%" + search_box.Text + "%') order by [Date] desc";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void form_unhold_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                TextData.billNo = "";
                search_box.Text = "";
                FillGridViewUsingPagination("");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }            
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            try
            {
                GetSetData.SaveLogHistoryDetails("UnHold Invoices Details Form", "Exit...", role_id);
                TextData.billNo = "";
                this.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                search_box.Text = "";
                FillGridViewUsingPagination("");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FillGridViewUsingPagination("search");
                //search_box.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void printSelectedDetails()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                //GetSetData.SaveLogHistoryDetails("UnHold Invoices Details Form", "Print Hold invoice [" + TextData.billNo + "] details", role_id);

                if (TextData.billNo != "")
                {
                    LoyalCusSales_report report = new LoyalCusSales_report();
                    report.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printSelectedDetails();
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureUnholdItemsList", TextData.billNo);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool fun_delete_products()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.Ids = data.UserPermissionsIds("sales_acc_id", "pos_hold_items", "billNo", TextData.billNo);

                GetSetData.query = @"delete from pos_hold_items_details where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.query = @"delete from pos_hold_items where sales_acc_id = '" + GetSetData.Ids.ToString() + "';";
                data.insertUpdateCreateOrDelete(GetSetData.query);
                //========================================================

                GetSetData.SaveLogHistoryDetails("UnHold Invoices Details Form", "Deleting Hold invoice [" + TextData.billNo + "] details", role_id);
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
            TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();

            try
            {
                sure.Message_choose("Are you sure you want to delete Bill [" + TextData.billNo.ToString() + "]");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    fun_delete_products();
                    FillGridViewUsingPagination("");
                    search_box.Text = "";
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

        private void unHoldSelectedBill()
        {
            try
            {
                TextData.billNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                //GetSetData.SaveLogHistoryDetails("UnHold Invoices Details Form", "Un-Hold invoice [" + TextData.billNo + "] details", role_id);

                if (TextData.billNo != "")
                {
                    this.Close();
                }
                else
                {
                    error.errorMessage("Please select the row first!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_show_all_Click(object sender, EventArgs e)
        {
            unHoldSelectedBill();
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

        private void form_unhold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                printSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                deleteSelectedDetails();
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {
                unHoldSelectedBill();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TextData.billNo = "";
                this.Close();
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
