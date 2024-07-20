using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class choose_product : Form
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


        public choose_product()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        //datalayer data = new datalayer(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static int count = 0;
        string discountValue = "";
        public static string selectedProductName = "";
        public static string selectedProductBarcode = "";
        public static string selectedProductID = "";
        public static string selectedStockID = "";
        public static string providedValue = "";
        public static string providedValueType = "";

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

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, show_all);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel8, lblCopyrights);
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
                GetSetData.query = "select * from ViewProductStocks";
                
                //if (condition == "search")
                //{
                //    GetSetData.query = GetSetData.query + " where ([Product Name] like '%" + search_box.Text + "%' or [Barcode] like '%" + search_box.Text + "%' or [Expiry] like '%" + search_box.Text + "%')";
                //}

                if (providedValueType == "barcode")
                {
                    GetSetData.query = GetSetData.query + " where ([Barcode] = '" + providedValue + "') and ([Status]  = 'Enabled');";
                }
                else if (providedValueType == "select")
                {
                    panel5.Visible = true;

                    if (condition == "search")
                    {
                        GetSetData.query = GetSetData.query + " where ([Status]  = 'Enabled') and ([Product Name] like '%" + search_box.Text + "%' or [Barcode] like '%" + search_box.Text + "%' or [Category] like '%" + search_box.Text + "%' or [Brand] like '%" + search_box.Text + "%')";
                    }
                    else
                    {
                        GetSetData.query = GetSetData.query + " where ([Status]  = 'Enabled');";
                    }
                }
                else
                {
                    GetSetData.query = GetSetData.query + " where ([Product Name] = '" + providedValue + "') and ([Status]  = 'Enabled');";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(productDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void search_box_TextChanged_1(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setProductName()
        {
            if (providedValueType == "barcode")
            {
                GetSetData.query = @"select prod_name from  pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id where (item_barcode = '" + providedValue + "');";
                //FormNamelabel.Text = data.SearchStringValuesFromDb(GetSetData.query);
            }
            else
            {
                //FormNamelabel.Text = providedValue;
            }
            
        }

        private void choose_product_Load(object sender, EventArgs e)
        {
            try
            {
                //originalExStyle = -1;
                //enableFormLevelDoubleBuffering = true;

                FillGridViewUsingPagination("");
                setProductName();

                search_box.Text = "";
                search_box.Select();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void show_all_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }

        private void choose_product_KeyDown(object sender, KeyEventArgs e)
        {
          if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

 
        private void ProductsDetailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedProductID = productDetailGridView.SelectedRows[0].Cells["PID"].Value.ToString();
                selectedProductName = productDetailGridView.SelectedRows[0].Cells["Product Name"].Value.ToString();
                selectedProductBarcode = productDetailGridView.SelectedRows[0].Cells["Barcode"].Value.ToString();
                selectedStockID = productDetailGridView.SelectedRows[0].Cells["SID"].Value.ToString();


                this.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void search_box_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
