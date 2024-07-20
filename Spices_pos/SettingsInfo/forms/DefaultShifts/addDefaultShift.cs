using System;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Expenses_info.forms
{
    public partial class addDefaultShift : Form
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

        public addDefaultShift()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string default_shift_id = "";
        public static bool saveEnable = false;
        public static string employeeName = "";
        public static string shift_name = "";
        public static string counter_name = "";
        public static string opening_amount = "";


        private void fillAddProductsFormTextBoxes()
        {
            txtEmployee.Text = employeeName;
            txtShift.Text = shift_name;
            txtCounter.Text = counter_name;
            txtOpeningAmount.Text = opening_amount;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                FormNamelabel.Text = "Update Default Shift";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Create New Default Shift";
            }
        }

        private void FillRefreshComboBoxValues()
        {
            GetSetData.FillComboBoxUsingProcedures(txtShift, "fillComboBoxRoles", "roleTitle");
        }  
        
        private void FillRefreshComboBoxEmployeeValues()
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void fillComboBoxCounters()
        {
            GetSetData.FillComboBoxUsingProcedures(txtCounter, "fillComboBoxCounters", "title");
        }

        private void fillComboBoxShifts()
        {
            GetSetData.FillComboBoxUsingProcedures(txtShift, "fillComboBoxShifts", "title");
        }

        private void refresh()
        {
            txtEmployee.Text = "";
            txtShift.Text = "";
            txtOpeningAmount.Text = "";
            txtCounter.Text = "";

            FillRefreshComboBoxValues();
            FillRefreshComboBoxEmployeeValues();
            fillComboBoxCounters();
            fillComboBoxShifts();

            enableSaveButton(); 
        }  

        private void addDefaultShift_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

      
        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private bool insert_record_db()
        {
            try
            {
                if (txtEmployee.Text != "")
                {
                    if (txtShift.Text != "")
                    {
                        if (txtCounter.Text != "")
                        {
                            if (txtOpeningAmount.Text != "")
                            {
                                string employee_id = data.UserPermissions("employee_id", "pos_employees", "full_name", txtEmployee.Text);
                                string user_id = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id);
                                string counter_id = data.UserPermissions("id", "pos_counter", "title", txtCounter.Text);
                                string shift_id = data.UserPermissions("id", "pos_shift", "title", txtShift.Text);

                                GetSetData.query = @"select id from pos_default_shifts where (user_id = '" + user_id + "');";
                                int is_already_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                if (is_already_exist == 0)
                                {
                                    GetSetData.query = @"insert into pos_default_shifts values ('" + txtOpeningAmount.Text + "' , '" + shift_id + "' , '" +  counter_id + "' , '" + user_id + "');";
                                    data.insertUpdateCreateOrDelete(GetSetData.query);
                                    

                                    done.DoneMessage("Successfully Saved!");
                                    done.ShowDialog();

                                    return true;
                                }
                                else
                                {
                                    error.errorMessage("This user default shift  already exist!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Enter the opening amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Select the counter first!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Select the shift first!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Select the employee first!");
                    error.ShowDialog();
                }

                return false;
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
            insert_record_db();
            refresh();
        }

        private bool update_records_db()
        {
            try
            {
                if (txtEmployee.Text != "")
                {
                    if (txtShift.Text != "")
                    {
                        if (txtCounter.Text != "")
                        {
                            if (txtOpeningAmount.Text != "")
                            {
                                string employee_id = data.UserPermissions("employee_id", "pos_employees", "full_name", txtEmployee.Text);
                                string user_id = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id);
                                string counter_id = data.UserPermissions("id", "pos_counter", "title", txtCounter.Text);
                                string shift_id = data.UserPermissions("id", "pos_shift", "title", txtShift.Text);


                                GetSetData.query = @"update pos_default_shifts set opening_amount = '" + txtOpeningAmount.Text + "' , shift_id = '" + shift_id + "' , counter_id = '" + counter_id + "' , user_id = '" + user_id + "' where (id = '" + default_shift_id +"');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);


                                done.DoneMessage("Successfully Updated!");
                                done.ShowDialog();

                                return true;
                            }
                            else
                            {
                                error.errorMessage("Enter the opening amount!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Select the counter first!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Select the shift first!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Select the employee first!");
                    error.ShowDialog();
                }

                return false;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return false;
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            update_records_db();
        }


        private void btnEmployee_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
