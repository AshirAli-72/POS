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
    public partial class payment_type : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        public payment_type()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            RefreshPaymentTypes();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public static string title = "";
        private int selectedId = -1;   // ✅ track selected record id

        // ---------------------- INSERT ----------------------
        private void insert_records()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(title_text.Text))
                {
                    string newTitle = title_text.Text.Trim();

                    GetSetData.query = @"INSERT INTO pos_payment_type (payment_title) 
                                         VALUES ('" + newTitle + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Saved!");
                    done.ShowDialog();

                    RefreshPaymentTypes();
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

        // ---------------------- REFRESH ----------------------
        private void RefreshPaymentTypes()
        {
            try
            {
                string query = "SELECT id, payment_title FROM pos_payment_type ORDER BY payment_title ASC";
                DataTable dt = data.GetDataTable(query);

                title_text.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    // ✅ add id in Tag, show title in ComboBox
                    title_text.Items.Add(new ComboBoxItem
                    {
                        Text = row["payment_title"].ToString(),
                        Value = row["id"].ToString()
                    });
                }

                if (!string.IsNullOrWhiteSpace(title))
                {
                    foreach (var item in title_text.Items)
                    {
                        if (((ComboBoxItem)item).Text == title)
                        {
                            title_text.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing Payment Types: " + ex.Message);
            }
        }

        // ---------------------- DELETE ----------------------
        private bool DeleteItems()
        {
            try
            {
                if (title_text.SelectedItem is ComboBoxItem selectedItem)
                {
                    selectedId = Convert.ToInt32(selectedItem.Value);

                    GetSetData.query = @"DELETE FROM pos_payment_type WHERE id = " + selectedId;
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    return true;
                }
                else
                {
                    error.errorMessage("Please select a payment type first!");
                    error.ShowDialog();
                    return false;
                }
            }
            catch (Exception es)
            {
                error.errorMessage("Cannot delete!\n" + es.Message);
                error.ShowDialog();
                return false;
            }
        }

        // ---------------------- EVENTS ----------------------
        private void payment_type_Load(object sender, EventArgs e)
        {
            title_text.Focus();
            GetSetData.addFormCopyrights(lblCopyrights);
            RefreshPaymentTypes();
        }

        private void title_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (title_text.SelectedItem is ComboBoxItem selectedItem)
            {
                payment_type.title = selectedItem.Text;
                selectedId = Convert.ToInt32(selectedItem.Value);
                title_text.Text = payment_type.title;
            }
        }

        private void enter_keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                insert_records();
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            insert_records();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (title_text.SelectedItem is ComboBoxItem selectedItem && !string.IsNullOrWhiteSpace(title_text.Text))
                {
                    selectedId = Convert.ToInt32(selectedItem.Value);
                    string newTitle = title_text.Text.Trim();

                    GetSetData.query = "UPDATE pos_payment_type " +
                                       "SET payment_title = '" + newTitle + "' " +
                                       "WHERE id = " + selectedId;

                    data.insertUpdateCreateOrDelete(GetSetData.query);

                    done.DoneMessage("Successfully Updated!");
                    done.ShowDialog();

                    payment_type.title = newTitle;
                    RefreshPaymentTypes();
                }
                else
                {
                    error.errorMessage("Please select a payment type!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (title_text.SelectedItem == null)
                {
                    error.errorMessage("Please select a payment type to delete!");
                    error.ShowDialog();
                    return;
                }

                sure.Message_choose("Are you sure you want to delete '" + title_text.Text + "'?");
                sure.ShowDialog();

                if (form_sure_message.sure)
                {
                    if (DeleteItems())
                    {
                        done.DoneMessage("Successfully Deleted!");
                        done.ShowDialog();
                        RefreshPaymentTypes();
                        title_text.Text = "";
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

    // ✅ ComboBoxItem helper class
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
