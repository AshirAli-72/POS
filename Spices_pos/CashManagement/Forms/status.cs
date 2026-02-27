using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using RefereningMaterial.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using System;
using System.Data;
using System.Windows.Forms;

namespace Spices_pos.CashManagement.Forms
{
    public partial class status : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // ❌ REMOVE THIS LINE
                return handleParam;
            }
        }


        public status()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            RefreshFieldPaymentStatus();   // ✅ Load hone ke sath hi data aaye
        }



        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string title = "";

      

        // ✅ INSERT
        private void insert_records()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"insert into pos_payment_status (status_title) values ('" + title_text.Text.ToString() + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();
                }
                else
                {
                    error.errorMessage("Please fill the empty fields!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        // ✅ REFRESH
        // ✅ REFRESH
        private void RefreshFieldPaymentStatus()
        {
            try
            {
                string query = "SELECT status_title FROM pos_payment_status ORDER BY status_title ASC";
                DataTable dt = data.GetDataTable(query); // Changed from GetTableData to GetDataTable

                title_text.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    title_text.Items.Add(row["status_title"].ToString());
                }

                if (!string.IsNullOrWhiteSpace(title) && title_text.Items.Contains(title))
                {
                    title_text.SelectedItem = title;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing Status: " + ex.Message);
            }
        }



        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                insert_records();
                RefreshFieldPaymentStatus();
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
            RefreshFieldPaymentStatus();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ✅ DELETE
        private bool DeleteItems()
        {
            try
            {
                if (title_text.Text != "")
                {
                    GetSetData.query = @"delete from pos_payment_status where status_title = '" + title_text.Text + "';";
                    data.insertUpdateCreateOrDelete(GetSetData.query);
                }
                else
                {
                    error.errorMessage("Please enter title first!");
                    error.ShowDialog();
                }

                return true;
            }
            catch (Exception es)
            {
                error.errorMessage(title_text.Text + " cannot be deleted!");
                error.ShowDialog();
                return false;
            }
        }

        private void showbutton_Click(object sender, EventArgs e)
        {
            try
            {
                sure.Message_choose("Are you sure you want to delete '" + title_text.Text + "'?");
                sure.ShowDialog();

                if (form_sure_message.sure == true)
                {
                    DeleteItems();
                    RefreshFieldPaymentStatus();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void status_Load(object sender, EventArgs e)
        {
            title_text.Focus();
            GetSetData.addFormCopyrights(lblCopyrights);
            RefreshFieldPaymentStatus();

            if (!string.IsNullOrWhiteSpace(title))  // ✅ sirf jab update/edit karna ho
            {
                title_text.Text = title;
            }
        }

        private void title_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (title_text.SelectedItem != null)
            {
                // ✅ Old value ko static variable me store karo
                status.title = title_text.SelectedItem.ToString();

                // ✅ Textbox me bhi show kar do edit ke liye
                title_text.Text = status.title;
            }
        }


        // ✅ UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(title_text.Text))
                {
                    string oldTitle = status.title;                 // Old value (selected)
                    string newTitle = title_text.Text.ToUpper();    // New value (user input)

                    GetSetData.query = "UPDATE pos_payment_status " +
                                       "SET status_title = '" + newTitle + "' " +
                                       "WHERE status_title = '" + oldTitle + "';";

                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();

                    // ✅ Update static variable pehle
                    status.title = newTitle;

                    // ✅ Refresh combobox
                    RefreshFieldPaymentStatus();

                    // ✅ Set selected item new value pe
                    if (title_text.Items.Contains(newTitle))
                        title_text.SelectedItem = newTitle;

                    // Clear input (optional)
                    title_text.Text = "";
                }
                else
                {
                    error.errorMessage("Please fill the empty fields!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title_text.Text))
                {
                    error.errorMessage("Please select or enter a status to delete!");
                    error.ShowDialog();
                    return;
                }

                // Confirmation
                sure.Message_choose("Are you sure you want to delete '" + title_text.Text + "'?");
                sure.ShowDialog();

                if (form_sure_message.sure)
                {
                    if (DeleteItems())
                    {
                        done.DoneMessage("Successfully Deleted!");
                        done.ShowDialog();
                        RefreshFieldPaymentStatus();
                        title_text.Text = ""; // ✅ Clear after delete
                    }
                }
            }
            catch (Exception ex)
            {
                error.errorMessage("Error while deleting: " + ex.Message);
                error.ShowDialog();
            }
        }

    }
}
