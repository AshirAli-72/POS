using Banking_info.forms;
using Datalayer;
using Guna.UI2.WinForms;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.CashManagement.Forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Spices_pos.CashManagement
{
    public partial class Create_Transaction : Form
    {
        private readonly Datalayers dl;
        private Timer timeTimer;
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        public static bool saveEnable = false;  // Similar to form_add_transaction.saveEnable
        private bool isModifyMode = false;
        private int editingTransactionId = 0;
        public static int user_id = 0;
        public static int role_id = 0;


        public Create_Transaction()
        {
            InitializeComponent();
            dl = new Datalayers(webConfig.con_string);
            TransactionListGridView.CellContentClick += TransactionListGridView_CellContentClick;
            this.Load += Create_Transaction_Load;
        }
        private void LoadModifyData()
        {
            try
            {
                DataTable dt = dl.GetDataTable("SELECT * FROM pos_cash_management WHERE id = " + editingTransactionId);

                if (dt.Rows.Count > 0)
                {
                    txt_amount.Text = dt.Rows[0]["Amount"].ToString();
                    txt_type.Text = dt.Rows[0]["TransactionType"].ToString();
                    txt_name.Text = dt.Rows[0]["PersonName"].ToString();
                    txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
                    txt_status.Text = dt.Rows[0]["Status"].ToString();
                    txt_date.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("yyyy-MM-dd");
                    txt_time.Text = dt.Rows[0]["Time"].ToString();
                }
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
                // Load or reload all records from your database table
                string query = @"
            SELECT 
                c.id,
                p.name AS PersonName,
                pt.payment_title AS PaymentType,
                ps.status_title AS Status,
                c.amount,
                c.remarks,
                c.date,
                c.time
            FROM pos_cash_management c
            LEFT JOIN pos_persons p ON c.person_id = p.id
            LEFT JOIN pos_payment_type pt ON c.payment_id = pt.id
            LEFT JOIN pos_payment_status ps ON c.status_id = ps.id
            ORDER BY c.id DESC;
        ";

                Datalayers dl = new Datalayers(webConfig.con_string);
                DataTable dt = dl.GetDataTable(query);

                TransactionListGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing transactions: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Create_Transaction_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Show/Hide buttons based on mode
                if (saveEnable)
                {
                    // Modify Mode
                    isModifyMode = true;
                    editingTransactionId = Cash_Management_Details.selectedTransactionId;
                    LoadModifyData();

                    savebutton.Visible = false;
                    update_button.Visible = true;
                }
                else
                {
                    // Add Mode
                    isModifyMode = false;
                    savebutton.Visible = true;
                    update_button.Visible = false;
                }

                // ✅ Load dropdown data
                RefreshFieldPaymentStatus();
                RefreshPaymentTypes();
                RefreshPersons();

                // ✅ Start clock timer
                timeTimer = new Timer();
                timeTimer.Interval = 1000;
                timeTimer.Tick += TimeTimer_Tick;
                timeTimer.Start();

                // ✅ Prepare grid
                clearGridView();
                TransactionListGridView.MouseClick += new MouseEventHandler(TransactionListGridView_MouseClick);
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }



        private void clearGridView()
        {
            txt_status.Text = "-- Select --";
           
            int a = TransactionListGridView.Rows.Count;

            // Refresh Button Event is Generated:
            for (int i = 0; i < a; i++)
            {
                foreach (DataGridViewRow row in TransactionListGridView.SelectedRows)
                {
                    TransactionListGridView.Rows.Remove(row);
                }
            }
            TransactionListGridView.DataSource = null;
        }
        private void RefreshPersons()
        {
            try
            {
                DataTable dt = dl.GetDataTable("SELECT DISTINCT name FROM pos_persons ORDER BY name ASC");

                txt_name.Items.Clear(); // pehle clear karo

                foreach (DataRow row in dt.Rows)
                {
                    string personName = row["name"].ToString();

                    // Agar empty, null ya "--Select--" hai to skip karo
                    if (!string.IsNullOrWhiteSpace(personName) && personName.Trim() != "--Select--")
                    {
                        txt_name.Items.Add(personName);
                    }
                }

                // SuggestAppend mode enable karo
                txt_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_name.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading persons: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            // ✅ Shows time like "01 : 35 : 20 PM"
            txt_time.Text = DateTime.Now.ToString("hh : mm : ss tt");

            // ✅ Shows date like "5/October/2025"
            txt_date.Text = DateTime.Now.ToString("d/MMMM/yyyy");
        }


        // -------------------- DELETE SYSTEM --------------------
        private void TransactionListGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                GetSetData.Ids = TransactionListGridView.HitTest(e.X, e.Y).RowIndex;

                if (GetSetData.Ids >= 0)
                {
                    my_menu.Items.Add("Delete").Name = "Delete";
                }

                my_menu.Show(TransactionListGridView, new Point(e.X, e.Y));

                my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_EditClicked);
            }
        }

        private void my_menu_EditClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            deleteRowFromGridView();
        }




        private void TransactionListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TransactionListGridView.Columns[e.ColumnIndex].Name == "Delete")
            {
                deleteRowFromGridView();
            }
        }




        private void deleteRowFromGridView()
        {
            try
            {
                GetSetData.Ids = TransactionListGridView.CurrentCell.RowIndex;
               TransactionListGridView.Rows.RemoveAt(GetSetData.Ids);
            }
            catch (Exception es)
            {
                error.errorMessage("Please select the row first!");
                error.ShowDialog();
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // -------------------- STATUS --------------------
        private void btn_status_Click(object sender, EventArgs e)
        {
            using (status st = new status())
            {
                status.title = txt_status.Text;
                st.ShowDialog();
            }

            RefreshFieldPaymentStatus();
        }

        private void RefreshFieldPaymentStatus()
        {
            try
            {
                DataTable dt = dl.GetDataTable("SELECT status_title FROM pos_payment_status ORDER BY status_title ASC");

                var titles = dt.AsEnumerable()
                               .Select(r => r["status_title"].ToString())
                               .Distinct()
                               .ToArray();

                txt_status.Items.Clear();
                txt_status.Items.AddRange(titles);

                if (!string.IsNullOrWhiteSpace(status.title) && txt_status.Items.Contains(status.title))
                {
                    txt_status.SelectedItem = status.title;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing Payment Status: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------- PAYMENT TYPE --------------------
        private void btn_type_Click(object sender, EventArgs e)
        {
            using (payment_type pt = new payment_type())
            {
                payment_type.title = txt_type.Text;
                pt.ShowDialog();
            }

            RefreshPaymentTypes();
        }

        private void RefreshPaymentTypes()
        {
            try
            {
                DataTable dt = dl.GetDataTable("SELECT payment_title FROM pos_payment_type ORDER BY payment_title ASC");

                var titles = dt.AsEnumerable()
                               .Select(r => r["payment_title"].ToString())
                               .Distinct()
                               .ToArray();

                txt_type.Items.Clear();
                txt_type.Items.AddRange(titles);

                if (!string.IsNullOrWhiteSpace(payment_type.title) && txt_type.Items.Contains(payment_type.title))
                {
                    txt_type.SelectedItem = payment_type.title;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing Payment Types: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------- OTHER BUTTONS --------------------
        private void btn_name_Click(object sender, EventArgs e)
        {
            persons per = new persons(this);
            per.ShowDialog();
        }

        private void txt_status_Enter(object sender, EventArgs e) { }
        private void txt_status_DropDown(object sender, EventArgs e) { }

        // -------------------- ADD RECORDS TO GRID --------------------
        private bool add_records_Grid_view()
        {
            try
            {
                TextData.name = string.IsNullOrWhiteSpace(txt_name.Text) || txt_name.Text == "-- Select --" ? "-- Select --" : txt_name.Text;
                TextData.mobile = string.IsNullOrWhiteSpace(txtmobilenumber.Text) ? "nill" : txtmobilenumber.Text;
                TextData.cnic = string.IsNullOrWhiteSpace(txtcnic.Text) ? "nill" : txtcnic.Text;
                TextData.date = string.IsNullOrWhiteSpace(txt_date.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : txt_date.Text;
                TextData.time = string.IsNullOrWhiteSpace(txt_time.Text) ? DateTime.Now.ToString("hh:mm:ss tt") : txt_time.Text;
                TextData.address = string.IsNullOrWhiteSpace(txtaddress.Text) ? "nill" : txtaddress.Text;
                TextData.transation_type = string.IsNullOrWhiteSpace(txt_type.Text) || txt_type.Text == "-- Select --" ? "others" : txt_type.Text;
                TextData.amount = double.TryParse(txt_amount.Text, out double amt) ? amt : 0;
                TextData.remarks = string.IsNullOrWhiteSpace(txt_remarks.Text) || txt_remarks.Text == "-- Select --"
                    ? "nill"
                    : txt_remarks.Text;

                TextData.status = string.IsNullOrWhiteSpace(txt_status.Text) || txt_status.Text == "-- Select --"
                                  ? "other"
                                  : txt_status.Text;

                int n = TransactionListGridView.Rows.Add();
                var row = TransactionListGridView.Rows[n];

                row.Cells[1].Value = TextData.name;
                row.Cells[2].Value = TextData.mobile;
                row.Cells[3].Value = TextData.cnic;
                row.Cells[4].Value = TextData.date;
                row.Cells[5].Value = TextData.time;
                row.Cells[6].Value = TextData.address;
                row.Cells[7].Value = TextData.transation_type;
                row.Cells[8].Value = TextData.amount.ToString();
                row.Cells[9].Value = TextData.remarks;
                row.Cells[10].Value = TextData.status;

                return true;
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            add_records_Grid_view();
        }

        private void txt_time_TextChanged(object sender, EventArgs e) { }

        private void savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in TransactionListGridView.Rows)
                {
                    if (row.IsNewRow) continue; // skip last empty row

                    // ✅ Person data
                    string name = row.Cells[1].Value?.ToString()?.Trim() ?? "nill";
                    string mobile = row.Cells[2].Value?.ToString()?.Trim() ?? "nill";
                    string cnic = row.Cells[3].Value?.ToString()?.Trim();
                    string address = row.Cells[6].Value?.ToString()?.Trim() ?? "nill";
                    string statusText = row.Cells[10].Value?.ToString()?.Trim() ?? "other";

                    // ✅ Transaction data
                    string date = row.Cells[4].Value?.ToString();
                    string time = row.Cells[5].Value?.ToString();
                    string paymentText = row.Cells[7].Value?.ToString()?.Trim() ?? "other";
                    string remarks = row.Cells[9].Value?.ToString()?.Trim() ?? "nill";

                    if (string.IsNullOrWhiteSpace(date)) date = DateTime.Now.ToString("yyyy-MM-dd");
                    if (string.IsNullOrWhiteSpace(time)) time = DateTime.Now.ToString("HH:mm:ss");

                    decimal amount = decimal.TryParse(row.Cells[8].Value?.ToString(), out decimal amt) ? amt : 0;

                    // 🔹 Step 1: Always create a new person record (even if name/mobile are same)
                    string queryPerson = $@"
                INSERT INTO pos_persons (name, mobile_number, cnic_number, address, status)
                VALUES ('{name}', '{mobile}', {(string.IsNullOrEmpty(cnic) ? "NULL" : $"'{cnic}'")}, '{address}', '{statusText}')
            ";
                    dl.insertUpdateCreateOrDelete(queryPerson);

                    // Get new id
                    int personId = dl.Select_ID_for_Foreign_Key_from_db_for_Insertion(
                        "SELECT TOP 1 id FROM pos_persons ORDER BY id DESC"
                    );

                    // 🔹 Step 2: Get or insert status_id
                    int statusId = dl.Select_ID_for_Foreign_Key_from_db_for_Insertion(
                        $"SELECT TOP 1 id FROM pos_payment_status WHERE status_title = '{statusText}'"
                    );

                    if (statusId == 0)
                    {
                        dl.insertUpdateCreateOrDelete($"INSERT INTO pos_payment_status (status_title) VALUES ('{statusText}')");
                        statusId = dl.Select_ID_for_Foreign_Key_from_db_for_Insertion(
                            $"SELECT TOP 1 id FROM pos_payment_status WHERE status_title = '{statusText}'"
                        );
                    }

                    // 🔹 Step 3: Get or insert payment_id
                    int paymentId = dl.Select_ID_for_Foreign_Key_from_db_for_Insertion(
                        $"SELECT TOP 1 id FROM pos_payment_type WHERE payment_title = '{paymentText}'"
                    );

                    if (paymentId == 0)
                    {
                        dl.insertUpdateCreateOrDelete($"INSERT INTO pos_payment_type (payment_title) VALUES ('{paymentText}')");
                        paymentId = dl.Select_ID_for_Foreign_Key_from_db_for_Insertion(
                            $"SELECT TOP 1 id FROM pos_payment_type WHERE payment_title = '{paymentText}'"
                        );
                    }

                    // 🔹 Step 4: Insert new transaction (payment_id used correctly)
                    string queryCash = $@"
                INSERT INTO pos_cash_management (person_id, status_id, payment_id, date, time, remarks, amount)
                VALUES ({personId}, {statusId}, {paymentId}, '{date}', '{time}', '{remarks}', {amount})
            "; 
                    dl.insertUpdateCreateOrDelete(queryCash);
                }

                // ✅ Show success
                done.DoneMessage("Successfully Saved!");
                done.ShowDialog();

                // ✅ Refresh main Cash Management details form
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm is Cash_Management_Details detailsForm)
                    {
                        detailsForm.RefreshTransactions();
                        break;
                    }
                }

                // ✅ Clear Grid
                TransactionListGridView.Rows.Clear();

                // ✅ Clear inputs
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is TextBox txt) txt.Clear();
                    if (ctrl is ComboBox cmb) cmb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                error.errorMessage("Error saving records: " + ex.Message);
                error.ShowDialog();
            }
        }





    }
}