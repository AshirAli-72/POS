using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using RefereningMaterial;
using System.Data.SqlClient;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Products_info.forms
{
    public partial class formResetEmployeeCommission : Form
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

        public formResetEmployeeCommission()
        {
            InitializeComponent();
        }

        Datalayers data = new Datalayers(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();
        public static int role_id = 0;

     
        private void showbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetSalesCommission()
        {
            try
            {
                double totalCommission = 0;

                GetSetData.query = "select sales_acc_id, employee_id, employeeCommission from pos_sales_accounts where (date = '" + txtDate.Text + "')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sales_acc_id_db = reader["sales_acc_id"].ToString();
                    string employeeId = reader["employee_id"].ToString();
                    string employeeCommission = reader["employeeCommission"].ToString();
                 
                    // ***********************************************************

                    string querySalesDetails = "select sales_id, quantity, Total_price, discount, prod_id from pos_sales_details where (sales_acc_id = '" + sales_acc_id_db + "')";

                    SqlConnection conn1 = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd1;
                    SqlDataReader reader1;

                    cmd1 = new SqlCommand(querySalesDetails, conn1);

                    conn1.Open();
                    reader1 = cmd1.ExecuteReader();

                    while (reader1.Read())
                    {
                        string productId = reader1["prod_id"].ToString();


                        GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (pos_employee_commission_detail.prod_id = '" + productId + "') and (pos_employee_commission.employee_id = '" + employeeId + "');";
                        int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        double commissionAmount = 0;


                        if (commission_id != 0)
                        {
                            string saleId = reader1["sales_id"].ToString();
                            string quantityDb = reader1["quantity"].ToString();
                            double perItemSalePrice = double.Parse(reader1["Total_price"].ToString());
                            double perItemDiscount = double.Parse(reader1["discount"].ToString());
                            

                            // ***********************************************************

                            GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employeeId + "');";
                            int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (commission_id_db != 0)
                            {
                                bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));

                                if (is_commission_in_percentage)
                                {
                                    commissionAmount = ((perItemSalePrice - perItemDiscount) * commissionInPercentageDb) / 100;
                                }
                                else
                                {
                                    commissionAmount = commissionAmountDb;

                                    commissionAmount *= double.Parse(quantityDb);
                                }

                                GetSetData.query = @"update pos_sales_details set per_item_commission = '" + Math.Round(commissionAmount, 2).ToString() + "' where (sales_id = '" + saleId + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                totalCommission += commissionAmount;
                            }
                        }
                    }

                    reader1.Close();

                    //double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employeeId);

                    double saleCommission = totalCommission;

                    //totalCommission += employeeCommissionDb;

                    ////**************************************************************************************
                    //GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employeeId + "');";
                    //data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"update pos_sales_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (sales_acc_id = '" + sales_acc_id_db + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                }


                reader.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        
        private void resetReturnsCommission()
        {
            try
            {
                double totalCommission = 0;

                GetSetData.query = "select return_acc_id, employee_id, employeeCommission, amount_due from pos_return_accounts where (date = '" + txtDate.Text + "')";

                SqlConnection conn = new SqlConnection(webConfig.con_string);
                SqlCommand cmd;
                SqlDataReader reader;

                cmd = new SqlCommand(GetSetData.query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string return_acc_id_db = reader["return_acc_id"].ToString();
                    string employeeId = reader["employee_id"].ToString();
                    string employeeCommission = reader["employeeCommission"].ToString();
                    double amountDueDb = double.Parse(reader["amount_due"].ToString());

                    // ***********************************************************

                    string querySalesDetails = "select return_id, quantity, Total_price, discount, per_item_commission, prod_id from pos_returns_details where (return_acc_id = '" + reader["return_acc_id"].ToString() + "')";

                    SqlConnection conn1 = new SqlConnection(webConfig.con_string);
                    SqlCommand cmd1;
                    SqlDataReader reader1;

                    cmd1 = new SqlCommand(querySalesDetails, conn1);

                    conn1.Open();
                    reader1 = cmd1.ExecuteReader();

                    while (reader1.Read())
                    {
                        string return_id = reader1["return_id"].ToString();
                        string quantityDb = reader1["quantity"].ToString();
                        double perItemSalePrice = double.Parse(reader1["Total_price"].ToString());
                        double perItemDiscount = double.Parse(reader1["discount"].ToString());
                        string perItemCommission = reader1["per_item_commission"].ToString();
                        string productId = reader1["prod_id"].ToString();

                        // ***********************************************************

                        GetSetData.query = "select top 1 pos_employee_commission_detail.commission_id from pos_employee_commission_detail inner join pos_employee_commission on pos_employee_commission_detail.commission_id = pos_employee_commission.commission_id where (pos_employee_commission_detail.prod_id = '" + productId + "') and (pos_employee_commission.employee_id = '" + employeeId + "');";
                        int commission_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                        double commissionAmount = 0;

                        if (commission_id != 0)
                        {
                            GetSetData.query = "select commission_id from pos_employee_commission where (commission_id = '" + commission_id.ToString() + "') and (employee_id = '" + employeeId + "');";
                            int commission_id_db = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                            if (commission_id_db != 0)
                            {
                                bool is_commission_in_percentage = bool.Parse(data.UserPermissions("is_commission_in_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionAmountDb = double.Parse(data.UserPermissions("commission_amount", "pos_employee_commission", "commission_id", commission_id_db.ToString()));
                                double commissionInPercentageDb = double.Parse(data.UserPermissions("commission_percentage", "pos_employee_commission", "commission_id", commission_id_db.ToString()));


                                if (is_commission_in_percentage)
                                {
                                    commissionAmount = ((perItemSalePrice - perItemDiscount) * commissionInPercentageDb) / 100;
                                }
                                else
                                {
                                    commissionAmount = commissionAmountDb;

                                    commissionAmount *= double.Parse(quantityDb);
                                }

                                GetSetData.query = @"update pos_returns_details set per_item_commission = '" + Math.Round(commissionAmount, 2).ToString() + "' where (return_id = '" + return_id + "');";
                                data.insertUpdateCreateOrDelete(GetSetData.query);

                                totalCommission += commissionAmount;
                            }
                        }
                    }

                    reader1.Close();

                    //double employeeCommissionDb = data.NumericValues("commission", "pos_employees", "employee_id", employeeId);

                    double saleCommission = totalCommission;

                    //totalCommission += employeeCommissionDb;

                    ////**************************************************************************************
                    //GetSetData.query = @"update pos_employees set commission = '" + Math.Round(totalCommission, 2).ToString() + "' where (employee_id = '" + employeeId + "');";
                    //data.insertUpdateCreateOrDelete(GetSetData.query);


                    GetSetData.query = @"update pos_return_accounts set employeeCommission = '" + Math.Round(saleCommission, 2).ToString() + "' where (return_acc_id = '" + return_acc_id_db + "');";
                    data.insertUpdateCreateOrDelete(GetSetData.query);

                }


                reader.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            resetSalesCommission();

            resetReturnsCommission();

            done.DoneMessage("Successfully reset!");
            done.ShowDialog();
        }

        private void formResetEmployeeCommission_Load(object sender, EventArgs e)
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                txtDate.Text = DateTime.Now.ToLongDateString();
                
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            } 
        }

    }
}
