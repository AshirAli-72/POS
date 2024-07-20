using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Purchase_info.forms
{
    public partial class formAdd_imei : Form
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

        public formAdd_imei()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static bool saveEnable;
        int checkQuantity = 1;

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
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel6, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel7, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
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

        private void clearDataGridViewItems()
        {
            this.productDetailGridView.DataSource = null;
            this.productDetailGridView.Refresh();
            productDetailGridView.Rows.Clear();
            productDetailGridView.Columns.Clear();
        }

        private void createCheckBoxInGridView()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Delete";
            btn.Name = "Delete";
            btn.Text = "x";
            btn.Width = 35;
            btn.MinimumWidth = 8;
            btn.UseColumnTextForButtonValue = true;
            btn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            btn.FlatStyle = FlatStyle.Flat;
            btn.DefaultCellStyle.ForeColor = Color.FromArgb(5, 100, 146);
            btn.DefaultCellStyle.Font = new Font("Verdana", 8F, FontStyle.Bold);
            btn.DefaultCellStyle.SelectionBackColor = Color.Red;
            btn.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
            productDetailGridView.Columns.Add(btn);
        }

        private void formAdd_imei_Load(object sender, EventArgs e)
        {
            txtImei.Select();
            GetSetData.addFormCopyrights(lblCopyrights);
            
            //if (saveEnable == true)
            //{
                clearDataGridViewItems();
                GetSetData.FillDataGridView(productDetailGridView, "ProcedurePurchaseIMEIDetails", TextData.invoiceNo, TextData.prod_name);
                createCheckBoxInGridView();
                checkQuantity = productDetailGridView.Rows.Count + 1;
            //}
        }

        private void add_records_dataGridView()
        {
            try
            {
                TextData.remarks = txtImei.Text;

                GetSetData.Data = "";

                for (int i = 0; i < productDetailGridView.Rows.Count; i++)
                {
                    GetSetData.Data = productDetailGridView.Rows[i].Cells[0].Value.ToString();

                    if (TextData.remarks == GetSetData.Data)
                    {
                        GetSetData.Data = TextData.remarks;
                        break;
                    }
                }

                if (TextData.remarks != "")
                {
                    if (checkQuantity <= TextData.quantity)
                    {
                        GetSetData.query = @"select imei_id from pos_purchase_imei where (imei_no = '" + TextData.remarks + "');";
                        GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        if (GetSetData.Ids == 0)
                        {
                            if (GetSetData.Data != TextData.remarks)
                            {
                                //if (saveEnable == false)
                                //{
                                //    int n = productDetailGridView.Rows.Add();
                                //    productDetailGridView.Rows[n].Cells[0].Value = TextData.remarks;
                                //    checkQuantity++;
                                //}
                                //else if (saveEnable == true)
                                //{
                                    DataTable dt = productDetailGridView.DataSource as DataTable;
                                    DataRow row = dt.NewRow();

                                    //Populate the row with data
                                    row[0] = TextData.remarks;
                                    dt.Rows.Add(row);
                                    checkQuantity++;
                                //}
                            }
                            else
                            {
                                error.errorMessage("'" + TextData.remarks + "' is already exist in list!");
                            }
                        }
                        else
                        {
                            error.errorMessage("'" + TextData.remarks + "' is already exist!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("IMIE's exceeded its limit!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter the IMIE number!");
                    error.ShowDialog();
                }

                txtImei.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private bool insert_record_db()
        {
            try
            {
                GetSetData.query = @"select product_id from pos_products where (prod_name = '" + TextData.prod_name + "') and (barcode = '" + TextData.barcode + "');";
                GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                for (int i = 0; i < productDetailGridView.Rows.Count; i++)
                {
                    GetSetData.query = @"select imei_id from pos_purchase_imei where (imei_no = '" + productDetailGridView.Rows[i].Cells[0].Value.ToString() + "');";
                    GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    
                    if (GetSetData.fks == 0)
                    {
                        GetSetData.query = @"insert into pos_purchase_imei values ('" + TextData.invoiceNo + "' , '" + productDetailGridView.Rows[i].Cells[0].Value.ToString() + "' , 'False' , '" + GetSetData.Ids.ToString() + "');";
                        data.insertUpdateCreateOrDelete(GetSetData.query);   
                    }
                    //***************************************************************************
                }
                return true;
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
            if (insert_record_db())
            {
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();
            }
        }

        private void txtImei_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                add_records_dataGridView();
            }
        }

        private void deleteRowFromGridView()
        {
            try
            {
                TextData.remarks = productDetailGridView.SelectedRows[0].Cells[0].Value.ToString();

                GetSetData.query = @"select imei_id from pos_purchase_imei where (imei_no = '" + TextData.remarks + "');";
                GetSetData.fks = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                if (GetSetData.fks != 0)
                {
                    GetSetData.query = @"delete from pos_purchase_imei where (invoiceNo = '" + TextData.invoiceNo + "') and (imei_no = '" + TextData.remarks + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }

                GetSetData.Ids = productDetailGridView.CurrentCell.RowIndex;
                productDetailGridView.Rows.RemoveAt(GetSetData.Ids);
                checkQuantity--;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                //error.errorMessage("Please select the row first!");
                //error.ShowDialog();
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (productDetailGridView.Columns[e.ColumnIndex].Name == "Delete")
            {
                deleteRowFromGridView();
            }
        }

        private void formAdd_imei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
