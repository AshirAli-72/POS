using System;
using System.Windows.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using CounterSales_info.forms;
using Recoverier_info.ReportCustomerStatement;
using Customers_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;
using Settings_info.controllers;

namespace Recoverier_info.forms
{
    public partial class formCustomerWiseBillsDetails : Form
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

        public formCustomerWiseBillsDetails()
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
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel10, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnPrint);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnModify);
                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, btnContractForm);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        private void system_user_permissions()
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                //formModifyBillDetails.role_id = role_id;
                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_contractForm", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnContractForm.Visible = bool.Parse(GetSetData.Data);

                // ***************************************************************************************************
                GetSetData.Data = data.UserPermissions("customerOrders_modify", "pos_tbl_authorities_button_controls3", "role_id", role_id.ToString());
                btnModify.Visible = bool.Parse(GetSetData.Data);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillGridViewUsingPagination(string condition1)
        {
            try
            {
                if (condition1 == "invoices")
                {
                    GetSetData.query = "select * from ViewInstallmentAccountDetails where ([Customer] = '" + customer_name_text.Text + "') and ([Code] = '" + customer_code_text.Text + "') order by [Date] asc;";
                }
                else if (condition1 == "search")
                {
                    GetSetData.query = "select * from ViewInstallmentAccountDetails where ([Date] like '" + search_box.Text + "%' or [Receipt No] like '" + search_box.Text + "%' or [Customer] like '" + search_box.Text + "%' or [Father Name] like '" + search_box.Text + "' or [Code] like '" + search_box.Text + "%' );";
                }

                GetSetData.FillDataGridViewUsingPagination(productDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Exit...", role_id);
            this.Close();
        }

        private void formCustomerWiseBillsDetails_Load(object sender, EventArgs e)
        {
            system_user_permissions();
            customer_name_text.Select();
        }

        private void customerContractForm()
        {
            try
            {
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Customer Contract form [" + TextData.invoiceNo + "] details (Contract form button click...)", role_id);

                if (TextData.invoiceNo != "")
                {
                    //agreementForm.billNo = TextData.invoiceNo;
                    //agreementForm form = new agreementForm();
                    //form.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the invoice first!");
                error.ShowDialog();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            customerContractForm();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetSetData.ResetPageNumbers(lblPageNo);
                FillGridViewUsingPagination("search");
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void FillComboBoxCustomerName()
        {
            customer_name_text.Text = data.UserPermissions("full_name", "pos_customers", "cus_code", customer_code_text.Text);
        }

        private void FillComboBoxCustomeCodes()
        {
            customer_code_text.Text = data.UserPermissions("cus_code", "pos_customers", "full_name", customer_name_text.Text);
        }

        private void customer_name_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(customer_name_text, "fillComboBoxCustomerNames", "full_name");
        }

        private void customer_code_text_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(customer_code_text, "fillComboBoxCustomerNames", "cus_code");  

        }

        private void customer_name_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Customer_details.selected_customer = customer_name_text.Text;
                Customer_details.role_id = role_id;
                buttonControls.CustomerDetailsbuttons();
                customer_name_text.Text = Customer_details.selected_customer;
                customer_code_text.Text = Customer_details.selected_customerCode;

                GetSetData.ResetPageNumbers(lblPageNo);
                search_box.Text = "";

                if (customer_name_text.Text != "")
                {
                    FillGridViewUsingPagination("invoices");
                }

                FillComboBoxCustomeCodes();
                GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Search customer [" + customer_name_text.Text + "  " + customer_code_text.Text + "] invoices details", role_id);
            }
        }

        private void customer_code_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSetData.ResetPageNumbers(lblPageNo);
                search_box.Text = "";

                if (customer_name_text.Text != "")
                {
                    FillGridViewUsingPagination("invoices");
                }

                FillComboBoxCustomerName();
                GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Search customer [" + customer_name_text.Text + "  " + customer_code_text.Text + "] invoices details", role_id);
            }
        }

        private void productDetailGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.FillDataGridView(productDetailGridView1, "ProcedureOrderSoldItemsList", TextData.invoiceNo);
                GetSetData.FillDataGridView(DataGridInstallments, "ProcedurePendingInstallmentDetails", TextData.invoiceNo);


            }
            catch (Exception es)
            {
                error.errorMessage("Please select the bill first!");
                error.ShowDialog();
                //MessageBox.Show(es.Message);
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

        private void ModifyBillDetails()
        {
            //try
            //{
            //    TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
            //    GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Updating customer invoices [" + TextData.invoiceNo + "] details (Modify button click...)", role_id);

            //    if (TextData.invoiceNo != "")
            //    {
            //        formModifyBillDetails.billNo = TextData.invoiceNo;
            //        formModifyBillDetails form = new formModifyBillDetails();
            //        form.ShowDialog();
            //    }
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage("Please select the invoice first!");
            //    error.ShowDialog();
            //}
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyBillDetails();
        }

        private void productDetailGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ModifyBillDetails();
        }

        private void PrintBillDetails()
        {
            try
            {
                TextData.invoiceNo = productDetailGridView.SelectedRows[0].Cells["Receipt No"].Value.ToString();
                GetSetData.SaveLogHistoryDetails("Customer Invoices Detail Form", "Print customer invoices [" + TextData.invoiceNo + "] details", role_id);

                if (TextData.invoiceNo != "")
                {
                    formCustomerInstallmentStatement.billNo = TextData.invoiceNo;
                    formCustomerInstallmentStatement form = new formCustomerInstallmentStatement();
                    form.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the invoice first!");
                error.ShowDialog();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintBillDetails();
        }

        private void formCustomerWiseBillsDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                PrintBillDetails();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                customerContractForm();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                ModifyBillDetails();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                search_box.Select();
            }
        }
    }
}
