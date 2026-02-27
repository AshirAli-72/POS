using Message_box_info.forms;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spices_pos.CashManagement.Forms
{
    public partial class persons : Form
    {
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        private string oldPerson = null;
        private Create_Transaction createTransactionForm;

        public persons(Create_Transaction formRef)
        {
            InitializeComponent();

            // store parent form reference
            this.createTransactionForm = formRef;

            // ensure event is subscribed (important)
            this.title_text.SelectedIndexChanged += new System.EventHandler(this.title_text_SelectedIndexChanged);

            // Sync items from parent
            if (createTransactionForm != null)
                SyncDropdowns();

            // don't pre-select anything
            title_text.SelectedIndex = -1;

            // oldPerson remains null until user actually selects an item
            oldPerson = null;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(title_text.Text))
            {
                if (createTransactionForm != null)
                {
                    string newPerson = title_text.Text.Trim();

                    // Add only if not already present
                    if (!createTransactionForm.txt_name.Items.Contains(newPerson))
                    {
                        createTransactionForm.txt_name.Items.Add(newPerson);
                    }

                    // Sync back to title_text dropdown
                    SyncDropdowns();

                    // Don't auto-select in parent or child
                    createTransactionForm.txt_name.SelectedIndex = -1;
                    title_text.SelectedIndex = -1;

                    // ✅ Clear text also
                    title_text.Text = string.Empty;
                }

                done.DoneMessage("Person added successfully.");
                done.ShowDialog();
            }
            else
            {
                error.errorMessage("Please fill the empty fields!");
                error.ShowDialog();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (createTransactionForm == null)
            {
                error.errorMessage("Parent form missing.");
                error.ShowDialog();
                return;
            }

            if (string.IsNullOrWhiteSpace(title_text.Text))
            {
                error.errorMessage("Please fill the empty field!");
                error.ShowDialog();
                return;
            }

            string newPerson = title_text.Text.Trim();

            int index = -1;
            if (!string.IsNullOrEmpty(oldPerson))
            {
                index = createTransactionForm.txt_name.Items.IndexOf(oldPerson);
            }

            if (index < 0)
            {
                for (int i = 0; i < createTransactionForm.txt_name.Items.Count; i++)
                {
                    var itm = createTransactionForm.txt_name.Items[i]?.ToString();
                    if (!string.IsNullOrEmpty(itm) &&
                        string.Equals(itm, title_text.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        index = i;
                        oldPerson = itm;
                        break;
                    }
                }
            }

            if (index >= 0)
            {
                bool existsElsewhere = false;
                for (int i = 0; i < createTransactionForm.txt_name.Items.Count; i++)
                {
                    if (i == index) continue;
                    var itm = createTransactionForm.txt_name.Items[i]?.ToString();
                    if (!string.IsNullOrEmpty(itm) &&
                        string.Equals(itm, newPerson, StringComparison.OrdinalIgnoreCase))
                    {
                        existsElsewhere = true;
                        break;
                    }
                }

                if (existsElsewhere)
                {
                    error.errorMessage("A person with this name already exists.");
                    error.ShowDialog();
                    return;
                }

                // Replace in parent
                createTransactionForm.txt_name.Items[index] = newPerson;
                createTransactionForm.txt_name.SelectedIndex = -1;

                // Replace in child
                if (index >= 0 && index < title_text.Items.Count)
                {
                    title_text.Items[index] = newPerson;
                    title_text.SelectedIndex = -1;
                }
                else
                {
                    SyncDropdowns();
                    title_text.SelectedIndex = -1;
                }

                done.DoneMessage($"'{oldPerson}' updated to '{newPerson}' successfully.");
                done.ShowDialog();

                // ✅ Clear after update
                oldPerson = null;
                title_text.SelectedIndex = -1;
                title_text.Text = string.Empty;
                createTransactionForm.txt_name.SelectedIndex = -1;
            }
            else
            {
                error.errorMessage("Please select an existing person from the list first (or type an exact existing name).");
                error.ShowDialog();
            }
        }

        private void title_text_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When user selects an item from dropdown, remember it
            if (title_text.SelectedItem != null)
            {
                oldPerson = title_text.SelectedItem.ToString();
            }
        }

        // Sync helper → show all txt_name items in title_text
        private void SyncDropdowns()
        {
            if (createTransactionForm != null)
            {
                title_text.Items.Clear();
                foreach (var item in createTransactionForm.txt_name.Items)
                {
                    title_text.Items.Add(item);
                }
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (createTransactionForm == null)
            {
                error.errorMessage("Parent form missing.");
                error.ShowDialog();
                return;
            }

            string personToDelete = title_text.Text.Trim();

            if (string.IsNullOrEmpty(personToDelete))
            {
                error.errorMessage("Please select or type a person to delete.");
                error.ShowDialog();
                return;
            }

            // Confirmation
            sure.Message_choose("Are you sure you want to delete '" + personToDelete + "'?");
            DialogResult result = sure.ShowDialog();

            if (result == DialogResult.Yes)   // ✅ ab ye kaam karega
            {
                if (createTransactionForm.txt_name.Items.Contains(personToDelete))
                    createTransactionForm.txt_name.Items.Remove(personToDelete);

                if (title_text.Items.Contains(personToDelete))
                    title_text.Items.Remove(personToDelete);

                title_text.SelectedIndex = -1;
                title_text.Text = string.Empty;
                createTransactionForm.txt_name.SelectedIndex = -1;
                oldPerson = null;

                done.DoneMessage($"'{personToDelete}' deleted successfully.");
                done.ShowDialog();
            }
        }

    }
}
