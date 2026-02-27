using System;
using System.Data;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using Settings_info.forms;
using Spices_pos.CashManagement.Reports;
using Spices_pos.DashboardInfo.CustomControls;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Spices_pos.CashManagement.Forms
{
    public partial class Cash_Management_Details : Form
    {
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);

        public static int selectedTransactionId = 0;
        public static int user_id = 0;
        public static int role_id = 0;

        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public Cash_Management_Details()
        {
            InitializeComponent();
            this.Load += Cash_Management_Details_Load;
        }

        private void Cash_Management_Details_Load(object sender, EventArgs e)
        {
            try
            {
                system_user_permissions();
                FillGridViewUsingPagination("");
                search_box.Text = "";

                TransactionDetailGridView.CellFormatting += TransactionDetailGridView_CellFormatting;
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }

        public void RefreshTransactions()
        {
            try
            {
                GetSetData.ResetPageNumbers(lblPageNo);
                FillGridViewUsingPagination("");
                search_box.Text = "";
            }
            catch (Exception ex)
            {
                error.errorMessage("Error refreshing transactions: " + ex.Message);
                error.ShowDialog();
            }
        }

        // ✅ Permissions
        private void system_user_permissions()
        {
            try
            {
                Create_Transaction.role_id = role_id;

                pnl_print.Visible = bool.Parse(data.UserPermissions("cash_details_print", "pos_tbl_authorities_button_controls2", role_id));
                pnl_delete.Visible = bool.Parse(data.UserPermissions("cash_details_delete", "pos_tbl_authorities_button_controls2", role_id));
                pnl_add_new.Visible = bool.Parse(data.UserPermissions("cash_details_new", "pos_tbl_authorities_button_controls2", role_id));
                pnl_modify.Visible = bool.Parse(data.UserPermissions("cash_details_modify", "pos_tbl_authorities_button_controls2", role_id));

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
                sidebarUserControl sidebar = new sidebarUserControl
                {
                    role_id = role_id,
                    user_id = user_id,
                    Dock = DockStyle.Fill
                };

                sidePanel.Controls.Add(sidebar);
                sidebar.Click += new System.EventHandler(this.sidePanelButton_Click);
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void sidePanelButton_Click(object sender, EventArgs e)
        {
            try { this.Dispose(); }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        // ✅ Load & Format Grid
        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = @"
            SELECT 
                pcm.id AS [Transaction_ID],
                p.name AS [Person_Name],
                p.mobile_number AS [Mobile_Number],
                p.cnic_number AS [CNIC_Number],
                p.address AS [Address],
                ps.status_title AS [Status],
                ppt.payment_title AS [Payment],
                pcm.date AS [Date],
                pcm.time AS [Time],
                pcm.amount AS [Amount],
                pcm.remarks AS [Remarks]
            FROM pos_cash_management pcm
            INNER JOIN pos_persons p ON pcm.person_id = p.id
            LEFT JOIN pos_payment_status ps ON pcm.status_id = ps.id
            LEFT JOIN pos_payment_type ppt ON pcm.payment_id = ppt.id";

                if (condition == "search" && !string.IsNullOrWhiteSpace(search_box.Text))
                {
                    GetSetData.query += " WHERE (p.name LIKE '" + search_box.Text + "%' " +
                                        "OR p.mobile_number LIKE '" + search_box.Text + "%' " +
                                        "OR p.cnic_number LIKE '" + search_box.Text + "%' " +
                                        "OR pcm.date LIKE '" + search_box.Text + "%' " +
                                        "OR pcm.amount LIKE '" + search_box.Text + "%')";
                }

                GetSetData.query += " ORDER BY pcm.id DESC;";

                // Fill DataGridView
                GetSetData.FillDataGridViewUsingPagination(TransactionDetailGridView, GetSetData.query, "");

                // ✅ Hide the ID column (still available internally)
                if (TransactionDetailGridView.Columns.Contains("Transaction_ID"))
                {
                    TransactionDetailGridView.Columns["Transaction_ID"].Visible = false;
                }

                // ✅ Make sure all columns auto-fit for clean view
                TransactionDetailGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                TransactionDetailGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                TransactionDetailGridView.MultiSelect = false;
                TransactionDetailGridView.ReadOnly = true;

                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception ex)
            {
                error.errorMessage("Error loading transactions: " + ex.Message);
                error.ShowDialog();
            }
        }


        private void TransactionDetailGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (TransactionDetailGridView.Columns[e.ColumnIndex].Name == "Date" && e.Value != null)
                {
                    if (DateTime.TryParse(e.Value.ToString(), out DateTime parsedDate))
                    {
                        e.Value = parsedDate.ToString("dd/MMMM/yyyy");
                        e.FormattingApplied = true;
                    }
                }

                if (TransactionDetailGridView.Columns[e.ColumnIndex].Name == "Time" && e.Value != null)
                {
                    string rawTime = e.Value.ToString().Trim();

                    if (TimeSpan.TryParse(rawTime, out TimeSpan parsedTime))
                    {
                        e.Value = DateTime.Today.Add(parsedTime).ToString("hh:mm:ss tt");
                        e.FormattingApplied = true;
                    }
                    else if (DateTime.TryParse(rawTime, out DateTime parsedDateTime))
                    {
                        e.Value = parsedDateTime.ToString("hh:mm:ss tt");
                        e.FormattingApplied = true;
                    }
                }
            }
            catch
            {
                // Ignore formatting errors
            }
        }

        // ✅ Add new
        private void addNewDetails()
        {
            using (Create_Transaction add_transaction = new Create_Transaction())
            {
                Create_Transaction.saveEnable = false;
                add_transaction.ShowDialog();
            }
        }

        private void Addnewbutton_Click(object sender, EventArgs e) => addNewDetails();

        // ✅ Modify (now uses Transaction_ID)
        private bool fun_update_details()
        {
            try
            {
                // ✅ Check modify permission
                GetSetData.query = "SELECT cash_details_modify FROM pos_tbl_authorities_button_controls2 WHERE role_id = '" + role_id + "'";
                GetSetData.Data = data.SearchStringValuesFromDb(GetSetData.query);

                if (GetSetData.Data != "True")
                {
                    error.errorMessage("You don't have permission to modify transactions!");
                    error.ShowDialog();
                    return false;
                }

                if (TransactionDetailGridView.SelectedRows.Count == 0)
                {
                    error.errorMessage("Please select a transaction first!");
                    error.ShowDialog();
                    return false;
                }

                DataGridViewRow row = TransactionDetailGridView.SelectedRows[0];

                // ✅ Get hidden Transaction_ID
                selectedTransactionId = Convert.ToInt32(row.Cells["Transaction_ID"].Value);

                // ✅ Enable modify mode
                Create_Transaction.saveEnable = true;

                // ✅ Pre-fill static TextData (optional)
                int selectedId = Convert.ToInt32(row.Cells["Transaction_ID"].Value);
                TextData.date = row.Cells["Date"].Value.ToString();
                TextData.time = row.Cells["Time"].Value.ToString();
                TextData.name = row.Cells["Person_Name"].Value.ToString();
                TextData.mobile = row.Cells["Mobile_Number"].Value.ToString();
                TextData.cnic = row.Cells["CNIC_Number"].Value.ToString();
                TextData.address = row.Cells["Address"].Value.ToString();
                TextData.status = row.Cells["Status"].Value.ToString();
                TextData.transation_type = row.Cells["Payment"].Value.ToString();
                TextData.amount = double.Parse(row.Cells["Amount"].Value.ToString());
                TextData.remarks = row.Cells["Remarks"].Value.ToString();

                // ✅ Open modify form
                using (Create_Transaction modifyForm = new Create_Transaction())
                {
                    GetSetData.SaveLogHistoryDetails("Cash Management Details Form", "Modify transaction button click...", role_id);
                    modifyForm.ShowDialog();
                }

                return true;
            }
            catch (Exception ex)
            {
                error.errorMessage("Error selecting record: " + ex.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void btn_modify_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.85;
            fun_update_details();
            this.Opacity = 1;
        }

        // ✅ Delete by Transaction_ID
        private bool fun_delete_transaction()
        {
            try
            {
                if (TransactionDetailGridView.SelectedRows.Count == 0)
                {
                    error.errorMessage("Please select a record to delete!");
                    error.ShowDialog();
                    return false;
                }

                int transId = Convert.ToInt32(TransactionDetailGridView.SelectedRows[0].Cells["Transaction_ID"].Value);

                GetSetData.query = "DELETE FROM pos_cash_management WHERE id = '" + transId + "'";
                data.insertUpdateCreateOrDelete(GetSetData.query);

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage("Unable to delete record: " + es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void deleteSelectedDetails()
        {
            try
            {
                this.Opacity = .85;
                sure.Message_choose("Are you sure you want to delete this record?");
                sure.ShowDialog();
                this.Opacity = 1;

                if (form_sure_message.sure)
                {
                    fun_delete_transaction();
                    GetSetData.ResetPageNumbers(lblPageNo);
                    FillGridViewUsingPagination("");
                    search_box.Text = "";
                }
            }
            catch (Exception)
            {
                error.errorMessage("This record cannot be deleted!");
                error.ShowDialog();
            }
        }

        // ✅ Print
        private void btn_print_Click(object sender, EventArgs e)
        {
            transaction_detail_reports list = new transaction_detail_reports();
            list.ShowDialog();
        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("search");
        }

        private void btnNext_Click(object sender, EventArgs e) =>
            GetSetData.GunaButtonNextItemsClick(TransactionDetailGridView, btnNext, btnPrevious, lblPageNo);

        private void btnPrevious_Click(object sender, EventArgs e) =>
            GetSetData.GunaButtonPreviousItemsClick(TransactionDetailGridView, btnNext, btnPrevious, lblPageNo);

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.user_id = user_id;
            settings.role_id = role_id;
            new settings().Show();
            this.Dispose();
        }

        private void Cash_Management_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P) btn_print.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) btn_delete.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) Addnewbutton.PerformClick();
            else if (e.Control && e.KeyCode == Keys.M) btn_modify.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F) search_box.Select();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            GetSetData.ResetPageNumbers(lblPageNo);
            FillGridViewUsingPagination("");
            search_box.Text = "";
        }
    }
}
