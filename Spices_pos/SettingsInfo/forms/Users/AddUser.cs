using System;
using System.Drawing;
using System.Windows.Forms;
using Message_box_info.forms;
using Datalayer;
using RefereningMaterial;
using Settings_info.controllers;
using Supplier_Chain_info.forms;
using System.Drawing.Printing;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Expenses_info.forms
{
    public partial class AddUser : Form
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

        public AddUser()
        {
            InitializeComponent();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;
        public static string user_id = "";
        public static bool saveEnable = false;
        public static string employeeName = "";
        public static string roleTitle = "";
        public static string username = "";
        public static string password = "";
        public static bool isPinSelected = false;


        private void fillAddProductsFormTextBoxes()
        {
            txtEmployee.Text = employeeName;
            role_text.Text = roleTitle;
            txtUserName.Text = username;
            txtPassword.Text = password;
        }

        private void enableSaveButton()
        {
            if (saveEnable == true)
            {
                savebutton.Visible = false;
                FormNamelabel.Text = "Update User";
                fillAddProductsFormTextBoxes();
            }
            else if (saveEnable == false)
            {
                update_button.Visible = false;
                FormNamelabel.Text = "Create New User";
            }
        }

        private void FillRefreshComboBoxValues()
        {
            GetSetData.FillComboBoxUsingProcedures(role_text, "fillComboBoxRoles", "roleTitle");
        }  
        
        private void FillRefreshComboBoxEmployeeValues()
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployee, "fillComboBoxEmployeeNames", "full_name");
        }

        private void refresh()
        {
            txtEmployee.Text = "";
            role_text.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            isPinSelected = false;

            FillRefreshComboBoxValues();
            FillRefreshComboBoxEmployeeValues();
            enableSaveButton(); 
        }  

        private void AddUser_Load(object sender, EventArgs e)
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
                    if (role_text.Text != "")
                    {
                        if (txtUserName.Text != "" || isPinSelected)
                        {
                            if (txtPassword.Text != "")
                            {
                                GetSetData.query = @"select user_id from pos_users where (password = '" + txtPassword.Text + "');";
                                int isPasswordExist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                if (isPasswordExist == 0)
                                {
                                    string employee_id = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "employee_id", "pos_employees", "full_name", txtEmployee.Text);
                                    string role_id = GetSetData.ProcedureGetStringValues("ProcedureGetStringValues", "role_id", "pos_roles", "roleTitle", role_text.Text);

                                    GetSetData.query = @"select user_id from pos_users where (username = '" + txtUserName.Text + "') and (password = '" + txtPassword.Text + "');";
                                    int is_user_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);


                                    if (is_user_exist == 0)
                                    {
                                        string username = txtUserName.Text;

                                        if (isPinSelected)
                                        {
                                            username = role_text.Text;
                                        }

                                        GetSetData.query = @"insert into pos_users values ('" + username + "' , '" + txtPassword.Text + "' , '" + role_id + "' , '" + employee_id + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);

                                        done.DoneMessage("Successfully Saved!");
                                        done.ShowDialog();

                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("This user already exist!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please try other password/pin!");
                                    error.ShowDialog();
                                }
                            }
                            else
                            {
                                error.errorMessage("Please enter the password!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Please enter the usename!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Select the role title first!");
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
                    if (role_text.Text != "")
                    {
                        if (txtUserName.Text != "")
                        {
                            if (txtPassword.Text != "")
                            {
                                GetSetData.query = @"select user_id from pos_users where (password = '" + txtPassword.Text + "') and (user_id != '" + user_id + "');";
                                int isPasswordExist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                if (isPasswordExist == 0)
                                {
                                    string employee_id = data.UserPermissions("employee_id", "pos_employees", "full_name", txtEmployee.Text);
                                    string role_id = data.UserPermissions("role_id", "pos_roles", "roleTitle", role_text.Text);

                                    GetSetData.query = @"select user_id from pos_users where (username = '" + txtUserName.Text + "') and (password = '" + txtPassword.Text + "') and (user_id != '" + user_id + "');";
                                    int is_user_exist = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                                    if (is_user_exist == 0)
                                    {
                                        GetSetData.query = @"update pos_users set username = '" + txtUserName.Text + "' , password =  '" + txtPassword.Text + "' , role_id = '" + role_id + "' , emp_id = '" + employee_id + "' where (user_id = '" + user_id + "');";
                                        data.insertUpdateCreateOrDelete(GetSetData.query);


                                        done.DoneMessage("Successfully Updated!");
                                        done.ShowDialog();

                                        return true;
                                    }
                                    else
                                    {
                                        error.errorMessage("This user already exist!");
                                        error.ShowDialog();
                                    }
                                }
                                else
                                {
                                    error.errorMessage("Please try other password/pin!");
                                    error.ShowDialog();
                                }

                            }
                            else
                            {
                                error.errorMessage("Enter the password!");
                                error.ShowDialog();
                            }
                        }
                        else
                        {
                            error.errorMessage("Enter the usename!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Select the role title first!");
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

        private void expense_text_Enter(object sender, EventArgs e)
        {
            FillRefreshComboBoxValues();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Add_supplier.role_id = role_id;
            buttonControls.employeeButton();
        }

        private void btn_print_barcode_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {
                generate_barcode();
                fun_print_barcode();
            }
            else
            {
                error.errorMessage("Please enter Username and Password!");
                error.ShowDialog();
            }
        }

        private void generate_barcode()
        {
            try
            {
                GetSetData.Data = "";

                if (txtUserName.Text != "" && txtPassword.Text != "")
                {
                    GetSetData.query = "select role_id from pos_role where (username = '" + txtUserName.Text + "') and (password = '" + txtPassword.Text + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                    if (GetSetData.Ids != 0)
                    {
                        GetSetData.Data = txtPassword.Text;

                        if (txtUserName.Text != "" && txtPassword.Text != "")
                        {
                            Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                            img_barcode.Image = brcode.Draw(GetSetData.Data, 60);
                        }
                        else
                        {
                            error.errorMessage("Barcode field is empty!");
                            error.ShowDialog();
                        }
                    }
                    else
                    {
                        error.errorMessage("Invalid Username or Password!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter Username and Password!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void fun_print_barcode()
        {
            try
            {
                if (img_barcode.Image != null)
                {
                    PrintDialog pd = new PrintDialog();
                    PrintDocument doc = new PrintDocument();

                    doc.PrintPage += Doc_PrintPage;
                    pd.Document = doc;

                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        doc.Print();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (txtUserName.Text != "" && txtPassword.Text != "")
                {
                    GetSetData.query = "select reg_id from pos_role where (username = '" + txtUserName.Text + "') and (password = '" + txtPassword.Text + "');";
                    GetSetData.Ids = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);
                    GetSetData.values = data.UserPermissions("name", "pos_registration", "reg_id", GetSetData.Ids.ToString());

                    PointF point = new PointF(23f, 60f);
                    Font font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
                    SolidBrush black = new SolidBrush(Color.Black);
                    GetSetData.Data = data.UserPermissions("shop_name", "pos_configurations");
                    e.Graphics.DrawString(GetSetData.Data, font, Brushes.Black, 0, 2);
                    Bitmap bm = new Bitmap(img_barcode.Width, img_barcode.Height);
                    img_barcode.DrawToBitmap(bm, new Rectangle(0, 0, img_barcode.Width, img_barcode.Height));
                    e.Graphics.DrawImage(bm, 0, 16);
                    font = new System.Drawing.Font("Verdana", 5, FontStyle.Bold);
                    e.Graphics.DrawString(GetSetData.values, font, Brushes.Black, 0, 78);
                    //e.Graphics.DrawString(prod_code_text.Text + "   RS: " + sale_price_text.Text, font, Brushes.Black, 0, 89);
                    bm.Dispose();
                }
                else
                {
                    error.errorMessage("Please enter Username and Password!");
                    error.ShowDialog();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void pass_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isPinSelected)
            {
                data.NumericValuesOnly(e);
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            isPinSelected = false;
            txtUserName.Enabled = true;
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            isPinSelected = true;
            txtUserName.Enabled = false;
        }
    }
}
