using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Settings_info.Reports;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class formAttendance : Form
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
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;
        public static int user_id = 0;

        public formAttendance()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        private void setFormColorsDynamically()
        {
            //try
            //{
            //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
            //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
            //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

            //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
            //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
            //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

            //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
            //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
            //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

            //    //****************************************************************

            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
            //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

            //    //****************************************************************

            //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }
        private void FillGridViewUsingPagination(string condition)
        {
            try
            {
                GetSetData.query = @" SELECT  pos_login_details.user_id as [UID],   dbo.pos_login_details.date as [Date], dbo.pos_login_details.time as [Time],
                                     dbo.pos_login_details.mac_address AS [Mac Address], dbo.pos_employees.full_name AS [User Name],
                                     dbo.pos_roles.roleTitle AS [Role], dbo.pos_login_details.status as [Status]
                                     FROM dbo.pos_login_details INNER JOIN dbo.pos_users ON dbo.pos_login_details.user_id = dbo.pos_users.user_id 
                                     Inner JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = dbo.pos_users.emp_id
                                     Inner JOIN dbo.pos_roles ON dbo.pos_roles.role_id = dbo.pos_users.role_id";


                if (condition == "search")
                {
                    GetSetData.Data = "select employee_id from pos_employees where (full_name = '" + txtEmployeeName.Text + "');";
                    string employee_id_db = data.SearchStringValuesFromDb(GetSetData.Data);

                    string user_id = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id_db);


                    GetSetData.query = GetSetData.query + " where (pos_login_details.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') and (pos_login_details.user_id = '" + user_id + "') order by pos_login_details.date asc";
                }
                else
                {
                    GetSetData.query = GetSetData.query + " where (pos_login_details.date between '" + FromDate.Text + "' and '" + ToDate.Text + "') order by pos_login_details.date asc";
                }

                GetSetData.FillDataGridViewUsingPagination(ProductsDetailGridView, GetSetData.query, "");
                lblPageNo.Text = "Page " + (GetSetData.countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void formAttendance_Load(object sender, EventArgs e)
        {
            //GetSetData.addFormCopyrights(lblCopyrights);
        }

        private void formAttendance_Shown(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToLongDateString();
                ToDate.Text = DateTime.Now.ToLongDateString();
                FillGridViewUsingPagination("");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings reg = new settings();
            reg.Show();

            this.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //GetSetData.SaveLogHistoryDetails("Settings Form", "Searching user attendance [" + FromDate.Text + "  " + ToDate.Text + "] details", role_id);
            
            FillGridViewUsingPagination("search");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TextData.fromdate = FromDate.Text;
            TextData.todate = ToDate.Text;

            GetSetData.query = "select employee_id from pos_employees where (full_name = '" + txtEmployeeName.Text + "');";
            string employee_id_db = data.SearchStringValuesFromDb(GetSetData.query);

            TextData.role_title = data.UserPermissions("user_id", "pos_users", "emp_id", employee_id_db);

            //GetSetData.SaveLogHistoryDetails("Settings Form", "Print user attendance [" + FromDate.Text + "  " + ToDate.Text + "]", role_id);
            form_login_details_report report = new form_login_details_report();
            report.ShowDialog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonNextItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetSetData.GunaButtonPreviousItemsClick(ProductsDetailGridView, btnNext, btnPrevious, lblPageNo);
        }

        private void FillComboBoxEmployeeName()
        {
            txtEmployeeName.Text = data.UserPermissions("full_name", "pos_employees", "emp_code", txtEmployeeCode.Text);
        }

        private void FillComboBoxEmployeeCodes()
        {
            txtEmployeeCode.Text = data.UserPermissions("emp_code", "pos_employees", "full_name", txtEmployeeName.Text);
        }

        private void txtEmployeeName_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeName, "fillComboBoxEmployeeNames", "full_name");
        }

        private void txtEmployeeCode_Enter(object sender, EventArgs e)
        {
            GetSetData.FillComboBoxUsingProcedures(txtEmployeeCode, "fillComboBoxEmployeeNames", "emp_code");
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeCodes();
            }
        }

        private void txtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillComboBoxEmployeeName();
            }
        }
    }
}
