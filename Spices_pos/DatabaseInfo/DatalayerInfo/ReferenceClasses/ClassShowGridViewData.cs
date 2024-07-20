using System;
using System.Windows.Forms;
using Datalayer;
using Message_box_info.forms;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;    
using Guna.UI2.WinForms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using DataModel.Cash_Drawer_Data_Classes;
using System.IO;
using Spices_pos.DatabaseInfo.WebConfig;

namespace RefereningMaterial
{
    public class ClassShowGridViewData
    {
        public bool checkBoxSelected { get; set; }
        public string query { get; set; }
        public string Permission { get; set; }
        public string Data { get; set; }
        public int Ids { get; set; }
        public int fks { get; set; }
        public string values { get; set; }
        public double numericValue { get; set; }
        public int minValue = 0;
        public int maxValue = 50;
        public int menuMax = 30;
        public int nextCount = 0;
        public int countPages = 0;

        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        form_sure_message sure = new form_sure_message();

        public SqlConnection conn_;
        public SqlCommand cmd_;
        public SqlDataReader reader_;
        public SqlDataAdapter adptr_;
        public System.Data.DataSet dset_;
        public System.Data.DataTable dt_;

        public string currency()
        {
            return ProcedureGeneralSettings("ProcedureGeneralSettings", "currency");
        }

        public void runBackgroundServices()
        {
            try
            {
                string pathToExe = data.UserPermissions("backgroundServicePath", "pos_server_config");

                if (File.Exists(pathToExe))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(pathToExe);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.CreateNoWindow = true;

                    Process.Start(startInfo);
                }
                else
                {
                    Console.WriteLine("Backgound Services could be run.");
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public string GetSystemMacAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;

                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }

                return sMacAddress;
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                return "";
            }
        }

        public void setToolTipForButtonControl(Guna2Button button, string message)
        {
            ToolTip tip = new ToolTip();
            tip.SetToolTip(button, message);
        }

        public ClassShowGridViewData(string con_String)
        {
            try
            {
                conn_ = new SqlConnection(con_String);
                cmd_ = new SqlCommand();
                adptr_ = new SqlDataAdapter();
                dset_ = new System.Data.DataSet();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        public void ResetPageNumbers(Label lblPageNo)
        {
            try
            {
                minValue = 0;
                nextCount = 0;
                countPages = 0;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

        }

        public void SetNextPreviousButtonValues(string condition)
        {
            try
            {
                if (condition == "next")
                {
                    minValue += 30;
                    menuMax += 30;
                }
                else if (condition == "previous")
                {
                    minValue -= 30;
                    menuMax -= 30;
                }
                else
                {
                    minValue = 1;
                    menuMax = 30;
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void EnableDisableNextPreviousButton(string condition, int lastCountedValue, Guna2CircleButton btnNext, Guna2CircleButton btnPrevious, Label lblPageNo)
        {
            if (condition == "next")
            {
                if (menuMax >= lastCountedValue)
                {
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages++;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            else if (condition == "previous")
            {
                if (minValue <= lastCountedValue)
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    minValue = 0;
                    countPages = 1;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages--;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
        }

        public void EnableDisableNextPreviousButton(string condition, int lastCountedValue, Guna2Button btnNext, Guna2Button btnPrevious, Label lblPageNo)
        {
            if (condition == "next")
            {
                if (menuMax >= lastCountedValue)
                {
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages++;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            else if (condition == "previous")
            {
                if (minValue <= lastCountedValue)
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    minValue = 0;
                    countPages = 1;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages--;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
        }

        public void FillDataGridView(DataGridView gridView, string procedure, string values)
        {
            try
            {
                conn_.Open();
                cmd_ = new SqlCommand(procedure, conn_);
                cmd_.CommandType = CommandType.StoredProcedure;

                if (values != "none")
                {
                    cmd_.Parameters.Add("@search", values);
                }

                dt_ = new DataTable();
                dt_.Load(cmd_.ExecuteReader());

                gridView.DataSource = null;
                gridView.DataSource = dt_;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
            finally
            {
                conn_.Close();
            }
        }

        public void GunaFillDataGridView(Guna2DataGridView gridView, string procedure, string values)
        {
            try
            {
                conn_.Open();
                cmd_ = new SqlCommand(procedure, conn_);
                cmd_.CommandType = CommandType.StoredProcedure;

                if (values != "none")
                {
                    cmd_.Parameters.Add("@search", values);
                    //data.cmd_.Parameters.AddWithValue("@search", search_box.Text);
                }

                dt_ = new DataTable();
                dt_.Load(cmd_.ExecuteReader());

                gridView.DataSource = null;
                gridView.DataSource = dt_;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
            finally
            {
                conn_.Close();
            }
        }

        public void FillDataGridView(DataGridView gridView, string procedure, string value1, string value2)
        {
            try
            {
                conn_.Open();
                cmd_ = new SqlCommand(procedure, conn_);
                cmd_.CommandType = CommandType.StoredProcedure;


                if (values != "none")
                {
                    cmd_.Parameters.Add("@BillNo", value1);
                    cmd_.Parameters.Add("@InvoiceNo", value2);
                    //data.cmd_.Parameters.AddWithValue("@search", search_box.Text);
                }

                dt_ = new DataTable();
                dt_.Load(cmd_.ExecuteReader());

                gridView.DataSource = null;
                gridView.DataSource = dt_;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                //MessageBox.Show(es.Message);
            }
            finally
            {
                conn_.Close();
            }
        }

        //public void FillDataGridView(string procedure, string values)
        //{
        //    try
        //    {
        //        conn_.Open();
        //        cmd_ = new SqlCommand(procedure, conn_);
        //        cmd_.CommandType = CommandType.StoredProcedure;

        //        if (values != "none")
        //        {
        //            cmd_.Parameters.Add("@search", values);
        //            //data.cmd_.Parameters.AddWithValue("@search", search_box.Text);
        //        }

        //        dt_ = new DataTable();
        //        dt_.Load(cmd_.ExecuteReader());
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        //MessageBox.Show(es.Message);
        //    }
        //    finally
        //    {
        //        conn_.Close();
        //    }
        //}

        //public void FillDataGridView(string procedure, string value1, string value2)
        //{
        //    try
        //    {
        //        conn_.Open();
        //        cmd_ = new SqlCommand(procedure, conn_);
        //        cmd_.CommandType = CommandType.StoredProcedure;

        //        if (values != "none")
        //        {
        //            cmd_.Parameters.Add("@BillNo", value1);
        //            cmd_.Parameters.Add("@InvoiceNo", value2);
        //            //data.cmd_.Parameters.AddWithValue("@search", search_box.Text);
        //        }

        //        dt_ = new DataTable();
        //        dt_.Load(cmd_.ExecuteReader());
        //    }
        //    catch (Exception es)
        //    {
        //        error.errorMessage(es.Message);
        //        error.ShowDialog();
        //        //MessageBox.Show(es.Message);
        //    }
        //    finally
        //    {
        //        conn_.Close();
        //    }
        //}

        public void FillDataGridViewUsingPagination(DataGridView gridView, string query, string btnPress)
        {
            try
            {
                dt_ = new DataTable();
                conn_.Open();

                if (btnPress == "previous")
                {
                    adptr_.Fill(nextCount, maxValue, dt_);
                }
                else if (btnPress == "next")
                {
                    adptr_.Fill(nextCount, maxValue, dt_);
                    gridView.DataSource = dt_;
                }
                else
                {
                    adptr_ = new SqlDataAdapter(query, conn_);
                    adptr_.Fill(0, maxValue, dt_);
                }
                

                gridView.DataSource = null;
                gridView.DataSource = dt_;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn_.Close();
            }
        }
        public void FillDataGridViewWithoutPagination(DataGridView gridView, string query)
        {
            try
            {
                dt_ = new DataTable();
                conn_.Open();

                adptr_ = new SqlDataAdapter(query, conn_);
                adptr_.Fill(dt_);

                gridView.DataSource = null;
                gridView.DataSource = dt_;
               
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn_.Close();
            }
        }  
        public void FillDataGridViewWithoutPaginationWithLoadingScreen(DataGridView gridView, string query)
        {
            try
            {
                dt_ = new DataTable();
                conn_.Open();

                adptr_ = new SqlDataAdapter(query, conn_);
                adptr_.Fill(dt_);

                gridView.DataSource = null;

                gridView.Invoke((MethodInvoker)delegate {
                    gridView.DataSource = dt_;
                });
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn_.Close();
            }
        }

        public void ButtonNextItemsClick(DataGridView grid, Button btnNext, Button btnPrevious, Label lblPageNo)
        {
            try
            {
                nextCount = minValue + dt_.Rows.Count;
                FillDataGridViewUsingPagination(grid, "", "next");
                minValue = nextCount;

                if (dt_.Rows.Count == maxValue)
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    minValue = nextCount + dt_.Rows.Count;
                }

                countPages++;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void ButtonPreviousItemsClick(DataGridView grid, Button btnNext, Button btnPrevious, Label lblPageNo)
        {
            try
            {
                nextCount = minValue - dt_.Rows.Count;

                if (nextCount < maxValue)
                {
                    nextCount = 0;
                }

                FillDataGridViewUsingPagination(grid, "", "previous");
                minValue = nextCount;

                if (minValue == 0)
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    countPages = 1;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages--;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void GunaButtonNextItemsClick(Guna2DataGridView grid, Guna2Button btnNext, Guna2Button btnPrevious, Label lblPageNo)
        {
            try
            {
                nextCount = 0;

                nextCount = minValue + dt_.Rows.Count;
                
                FillDataGridViewUsingPagination(grid, "", "next");

                minValue = nextCount;
               

                if (dt_.Rows.Count >= maxValue)
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    //minValue = nextCount + dt_.Rows.Count;
                }

                countPages++;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void GunaButtonPreviousItemsClick(Guna2DataGridView grid, Guna2Button btnNext, Guna2Button btnPrevious, Label lblPageNo)
        {
            try
            {
                nextCount = 0;

                nextCount = minValue - dt_.Rows.Count;

                if (nextCount < maxValue)
                {
                    nextCount = 0;
                }

                FillDataGridViewUsingPagination(grid, "", "previous");

                minValue = nextCount;


                if (minValue <= 0)
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    countPages = 1;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                }

                countPages--;
                lblPageNo.Text = "Page " + (countPages + 1);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        public void FillComboBoxWithValues(string query, string getValueDb, ComboBox values)
        {
            Data = "";
            SqlConnection conn = new SqlConnection(webConfig.con_string);

            try
            {
                cmd_ = new SqlCommand(query, conn);
                conn.Open();
                reader_ = cmd_.ExecuteReader();
                while (reader_.Read())
                {
                    Data = reader_[getValueDb].ToString();
                    values.Items.Add(Data);
                }
                reader_.Close();
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        public void FillComboBoxUsingProcedures(ComboBox comboBox, string procedure, string value)
        {
            SqlConnection conn = new SqlConnection(webConfig.con_string);

            try
            {
                cmd_ = new SqlCommand(procedure, conn);
                cmd_.CommandType = CommandType.StoredProcedure;
                adptr_ = new SqlDataAdapter(cmd_);
                dt_ = new DataTable();

                conn.Open();
                adptr_.Fill(dt_);
                conn.Close();

                comboBox.Text = null;
                comboBox.DataSource = dt_;
                comboBox.DisplayMember = value;
                comboBox.Text = "";
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
        }

        public void addFormCopyrights(Label lblCopyrights)
        {
            lblCopyrights.Text = data.UserPermissions("copyrights", "pos_report_settings");
        }

        public void setProgressBarColor(Guna2VProgressBar progressBar, int value)
        {
            if (value >= 800)
            {
                progressBar.ProgressColor = Color.SeaGreen;
                progressBar.ProgressColor2 = Color.SeaGreen;
            }
            else if (value <= 100)
            {
                progressBar.ProgressColor = Color.FromArgb(230, 0, 0);
                progressBar.ProgressColor2 = Color.FromArgb(230, 0, 0);
            }
            else
            {
                progressBar.ProgressColor = Color.FromArgb(30, 50, 120);
                progressBar.ProgressColor2 = Color.FromArgb(30, 50, 120);
            }
        }

        public void setFormColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, Panel panel, Label label)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);

            panel.BackColor = back_col;

            label.BackColor = back_col;
            label.ForeColor = fore_color;
        }

        public void setPanelColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, Panel panel)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);

            panel.BackColor = back_col;
        }

        public void setPanelColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, int border_red, int border_green, int border_blue, Guna2GradientPanel panel)
        {
            Color border_col = Color.FromArgb(border_red, border_green, border_blue);
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);

            panel.BorderColor = border_col;
        }

        public void setLabelColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, Label label)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);

            label.BackColor = back_col;
            label.ForeColor = fore_color;
        }

        public void setButonColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, Button button)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);

            button.BackColor = back_col;
            button.ForeColor = fore_color;
        }

        public void setGunaUILoginButonColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, int border_red, int border_green, int border_blue, Guna2Button button)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);
            Color border_color = Color.FromArgb(border_red, border_green, border_blue);

            button.BackColor = fore_color;
            button.ForeColor = fore_color;
            button.BorderColor = border_color;
            button.FillColor = back_col;
        }

        public void setGunaUIButonColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, int border_red, int border_green, int border_blue, Guna2Button button)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);
            Color border_color = Color.FromArgb(border_red, border_green, border_blue);

            button.BackColor = back_col;
            button.ForeColor = fore_color;
            button.BorderColor = border_color;
            button.FillColor = back_col;
        }

        public void setGunaUITextBoxColors(int back_red, int back_green, int back_blue, int fore_red, int fore_green, int fore_blue, int border_red, int border_green, int border_blue, Guna2TextBox textbox)
        {
            Color back_col = Color.FromArgb(back_red, back_green, back_blue);
            Color fore_color = Color.FromArgb(fore_red, fore_green, fore_blue);
            Color border_color = Color.FromArgb(border_red, border_green, border_blue);

            textbox.BackColor = back_col;
            textbox.ForeColor = fore_color;
            textbox.BorderColor = border_color;
            textbox.FillColor = back_col;
        }


        private string get_mac_address()
        {
            error_form error = new error_form();
            done_form done = new done_form();

            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;

                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }

                return sMacAddress;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
                return "";
            }
        }

        public string getIpAddress()
        {
            try
            {
                string CIP = "";
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        CIP = ip.ToString();
                    }
                }

                return CIP;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog(); 
                return "";
            }
        }

        public void SaveLogHistoryDetails(string title, string activity, int role_id)
        {
            //try
            //{
            //    Datalayers logDb = new Datalayers(webConfig.logDb_conString);
            //    TextData.ipAddress = getIpAddress();
            //    TextData.macAddress = get_mac_address();
            //    TextData.values = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString();

            //    if (TextData.ipAddress == "")
            //    {
            //        TextData.ipAddress = "IP";
            //    }

            //    if (TextData.macAddress == "")
            //    {
            //        TextData.ipAddress = "MAC";
            //    }

            //    Ids = data.UserPermissionsIds("reg_id", "pos_role", "role_id", role_id.ToString());
            //    TextData.name = data.UserPermissions("name", "pos_registration", "reg_id", Ids.ToString());
            //    TextData.roleTitle = data.UserPermissions("roleTitle", "pos_registration", "reg_id", Ids.ToString());
            //    TextData.username = data.UserPermissions("username", "pos_role", "role_id", role_id.ToString());
            //    TextData.password = data.UserPermissions("password", "pos_role", "role_id", role_id.ToString());
            //    //***********************************************************************************

            //    query = @"insert into pos_logHistory values ('" + TextData.values + "' , '" + TextData.name + "' , '" + TextData.roleTitle + "' , '" + TextData.username + "' , '" + TextData.password + "' , '" + title + "' , '" + activity + "' , '" + TextData.ipAddress + "' , '" + TextData.macAddress + "');";
            //    logDb.insertUpdateCreateOrDelete(query);
            //}
            //catch (Exception es)
            //{
            //    error.errorMessage(es.Message);
            //    error.ShowDialog();
            //}
        }

        public double ProcedureGetSingleValues(string procedure)
        {
            double result = 0;

            try
            {
                conn_.Open();
                //Here I have provided my sample procedure name, Ensure you have provided your procedurename here
                cmd_ = new SqlCommand(procedure, conn_);

                SqlParameter outputparameter = new SqlParameter();
                outputparameter.SqlDbType = SqlDbType.Float;
                outputparameter.ParameterName = "result";
                outputparameter.Value = 0;
                outputparameter.Direction = ParameterDirection.Output;

                //Set command type to Stored Procedure
                cmd_.CommandType = CommandType.StoredProcedure;
                var output = cmd_.Parameters.Add(outputparameter);

                //Execute query
                cmd_.ExecuteNonQuery();
                //Close connection
                conn_.Close();

                result = (double)output.Value;

            }
            catch (Exception e)
            {
                Debug.WriteLine("This is an internal error " + e.InnerException);
                result = 0;
            }

            return result;
        }

        public string ProcedureGetDashboardAuthorities(string procedure, string columnName, string searchValue)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Set up the output parameter
                SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.Parameters.AddWithValue("@value", columnName);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                cmd.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                result = outputParameter.Value.ToString();

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                result = "";
            }

            return result;
        }

        public string ProcedureGeneralSettings(string procedure, string columnName)
        {
            string result = "";
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Set up the output parameter
                SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.Parameters.AddWithValue("@value", columnName);

                cmd.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                result = outputParameter.Value.ToString();

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                result = "";
            }

            return result;
        }
    
        public double ProcedureGetNumericValues(string procedure, string columnName, string tableName, string condition, string searchValue)
        {
            double result = 0;
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Set up the output parameter
                SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.Float);
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.Parameters.AddWithValue("@columnName", columnName);
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@condition", condition);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                cmd.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                result = Convert.ToDouble(outputParameter.Value);

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                result = 0;
            }

            return result;
        }


        public string ProcedureGetStringValues(string procedure, string columnName, string tableName, string condition, string searchValue)
        {
            string result = ""; // Change the data type to string
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Set up the output parameter
                SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50); // Use SqlDbType.NVarChar for string output
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.Parameters.AddWithValue("@columnName", columnName);
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@condition", condition);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                cmd.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                result = outputParameter.Value.ToString(); // Convert output parameter value to string

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                result = ""; // Handle error by setting result to empty string
            }

            return result;
        }


        public int ProcedureGetIntegerValues(string procedure, string columnName, string tableName, string condition, string searchValue)
        {
            int result = 0; // Change the data type to int
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Set up the output parameter
                SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.Int); // Change data type to int
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.Parameters.AddWithValue("@columnName", columnName);
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@condition", condition);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                cmd.ExecuteNonQuery();

                // Retrieve the value of the output parameter
                result = (int)outputParameter.Value; // Convert output parameter value to int

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                result = 0; // Handle error by setting result to 0
            }

            return result;
        }

        //public string ProcedureQueryResult(string procedure, string query)
        //{
        //    string result = "";
        //    SqlConnection conn = new SqlConnection(webConfig.con_string);
        //    SqlCommand cmd;

        //    try
        //    {
        //        conn.Open();
        //        cmd = new SqlCommand(procedure, conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // Set up the output parameter
        //        SqlParameter outputParameter = new SqlParameter("@result", SqlDbType.NVarChar, 50);
        //        outputParameter.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(outputParameter);

        //        // Pass the full query as input parameter
        //        cmd.Parameters.AddWithValue("@query", query);

        //        cmd.ExecuteNonQuery();

        //        // Retrieve the value of the output parameter
        //        result = outputParameter.Value.ToString();

        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        // Handle exceptions
        //        MessageBox.Show("Error: " + e.Message);
        //        result = "";
        //    }

        //    return result;
        //}

        public double ProcedureGetSingleValues1(string procedure, string value1)
        {
            double result = 0;
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                //Here I have provided my sample procedure name, Ensure you have provided your procedurename here
                cmd = new SqlCommand(procedure, conn);

                SqlParameter outputparameter = new SqlParameter();
                outputparameter.SqlDbType = SqlDbType.Float;
                outputparameter.ParameterName = "result";
                outputparameter.Value = 0;
                outputparameter.Direction = ParameterDirection.Output;

                //Set command type to Stored Procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", value1);
                var output = cmd.Parameters.Add(outputparameter);

                //Execute query
                cmd.ExecuteNonQuery();
                //Close connection
                conn.Close();

                result = (double)output.Value;

            }
            catch (Exception e)
            {
                Debug.WriteLine("This is an internal error " + e.InnerException);
                result = 0;
            }

            return result;
        }


        public double ProcedureGetSingleValues2(string procedure, string value1, string value2)
        {
            double result = 0;
            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlCommand cmd;

            try
            {
                conn.Open();
                //Here I have provided my sample procedure name, Ensure you have provided your procedurename here
                cmd = new SqlCommand(procedure, conn);

                SqlParameter outputparameter = new SqlParameter();
                outputparameter.SqlDbType = SqlDbType.Float;
                outputparameter.ParameterName = "result";
                outputparameter.Value = 0;
                outputparameter.Direction = ParameterDirection.Output;

                //Set command type to Stored Procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", value1);
                cmd.Parameters.AddWithValue("@Date2", value2);
                var output = cmd.Parameters.Add(outputparameter);

                //Execute query
                cmd.ExecuteNonQuery();
                //Close connection
                conn.Close();

                result = (double)output.Value;

            }
            catch (Exception e)
            {
                Debug.WriteLine("This is an internal error " + e.InnerException);
                result = 0;
            }

            return result;
        }
        
        public string ProcedureGetEmployeeDuration(string fromDate, string toDate, string employeeName)
        {
            string total_duration = "";
            string query = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(webConfig.con_string))
                {
                    if (employeeName  != "")
                    {
                        query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
                                   FROM (
                                        SELECT SUM(ABS(DATEDIFF(SECOND, pos_clock_in.start_time, pos_clock_out.end_time))) AS [TotalDurationInSeconds]
                                        FROM pos_clock_in
                                        INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
                                        INNER JOIN dbo.pos_users AS to_user ON pos_clock_in.to_user_id = to_user.user_id
                                        INNER JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
                                        WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate) and (pos_employees.full_name = @employeeName)
                                    ) AS DurationInSeconds;";
                    }
                    else
                    {
                        query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
                                   FROM (
                                        SELECT SUM(ABS(DATEDIFF(SECOND, pos_clock_in.start_time, pos_clock_out.end_time))) AS [TotalDurationInSeconds]
                                        FROM pos_clock_in
                                        INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
                                        INNER JOIN dbo.pos_users AS to_user ON pos_clock_in.to_user_id = to_user.user_id
                                        INNER JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
                                        WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate)
                                    ) AS DurationInSeconds;";
                    }
                    

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromDate", fromDate); // Assuming FromDate.Text is in a valid date format
                        command.Parameters.AddWithValue("@ToDate", toDate);     // Assuming ToDate.Text is in a valid date format

                        if (employeeName != "")
                        {
                            command.Parameters.AddWithValue("@employeeName", employeeName);     // Assuming ToDate.Text is in a valid date format
                        }

                        connection.Open();

                        total_duration = command.ExecuteScalar()?.ToString(); // Assuming you expect a single value
                   

                        return total_duration;
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }

            return total_duration;
        }
        
        //public string ProcedureGetEmployeeDuration(string fromDate, string toDate, string employeeName)
        //{
        //    string total_duration = "";
        //    string query = "";

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(webConfig.con_string))
        //        {
        //            if (employeeName  != "")
        //            {
        //                query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
        //                                    RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
        //                                    RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
        //                            FROM (
        //                                SELECT SUM(DATEDIFF(SECOND, 0, CONVERT(TIME, pos_clock_out.total_hours))) AS [TotalDurationInSeconds]
        //                                FROM pos_clock_in
        //                                INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
								//		INNER JOIN dbo.pos_users AS to_user ON dbo.pos_clock_in.to_user_id = to_user.user_id INNER JOIN
								//		 dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
        //                                WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate) and (pos_employees.full_name = @employeeName)
        //                            ) AS DurationInSeconds;";
        //            }
        //            else
        //            {
        //                query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
        //                                    RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
        //                                    RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
        //                            FROM (
        //                                SELECT SUM(DATEDIFF(SECOND, 0, CONVERT(TIME, pos_clock_out.total_hours))) AS [TotalDurationInSeconds]
        //                                FROM pos_clock_in
        //                                INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
        //                                WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate)
        //                            ) AS DurationInSeconds;";
        //            }
                    

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@FromDate", fromDate); // Assuming FromDate.Text is in a valid date format
        //                command.Parameters.AddWithValue("@ToDate", toDate);     // Assuming ToDate.Text is in a valid date format

        //                if (employeeName != "")
        //                {
        //                    command.Parameters.AddWithValue("@employeeName", employeeName);     // Assuming ToDate.Text is in a valid date format
        //                }

        //                connection.Open();

        //                total_duration = command.ExecuteScalar()?.ToString(); // Assuming you expect a single value
                   

        //                return total_duration;
        //            }
        //        }
        //    }
        //    catch (Exception es)
        //    {
        //        MessageBox.Show(es.Message);
        //    }

        //    return total_duration;
        //}
        
        
        public string ProcedureGetEmployeeTotalSalary(string fromDate, string toDate, string employeeName, decimal perHourSalary)
        {
            string total_duration = "";
            string query = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(webConfig.con_string))
                {
                    if (employeeName  != "")
                    {
                        query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
                                   FROM (
                                        SELECT SUM(ABS(DATEDIFF(SECOND, pos_clock_in.start_time, pos_clock_out.end_time))) AS [TotalDurationInSeconds]
                                        FROM pos_clock_in
                                        INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
                                        INNER JOIN dbo.pos_users AS to_user ON pos_clock_in.to_user_id = to_user.user_id
                                        INNER JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
                                        WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate) and (pos_employees.full_name = @employeeName)
                                    ) AS DurationInSeconds;";
                    }
                    else
                    {
                        query = @"SELECT CONVERT(VARCHAR, (TotalDurationInSeconds / 3600)) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, (TotalDurationInSeconds % 3600) / 60), 2) + ':' +
                                   RIGHT('0' + CONVERT(VARCHAR, TotalDurationInSeconds % 60), 2) AS [FormattedDuration]
                                   FROM (
                                        SELECT SUM(ABS(DATEDIFF(SECOND, pos_clock_in.start_time, pos_clock_out.end_time))) AS [TotalDurationInSeconds]
                                        FROM pos_clock_in
                                        INNER JOIN pos_clock_out ON pos_clock_in.id = pos_clock_out.clock_in_id
                                        INNER JOIN dbo.pos_users AS to_user ON pos_clock_in.to_user_id = to_user.user_id
                                        INNER JOIN dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id
                                        WHERE (pos_clock_out.date >= @FromDate AND pos_clock_out.date <= @ToDate)
                                    ) AS DurationInSeconds;";
                    }
                    

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FromDate", fromDate); // Assuming FromDate.Text is in a valid date format
                        command.Parameters.AddWithValue("@ToDate", toDate);     // Assuming ToDate.Text is in a valid date format

                        if (employeeName != "")
                        {
                            command.Parameters.AddWithValue("@employeeName", employeeName);     // Assuming ToDate.Text is in a valid date format
                        }

                        connection.Open();

                        total_duration = command.ExecuteScalar()?.ToString(); // Assuming you expect a single value

                        if (total_duration != "")
                        {
                            double totalHours = ConvertToTotalHours(total_duration);

                            // Calculate total salary
                            string totalSalary = ((decimal)totalHours * perHourSalary).ToString();

                            return totalSalary;
                        }
                        else
                        {
                            error.errorMessage("No Record Found!");
                            error.ShowDialog();
                        }

                        return "0";
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage("No Record Found!");
                error.ShowDialog();
            }

            return "";
        }

        private double ConvertToTotalHours(string duration)
        {
            TimeSpan time = TimeSpan.Parse(duration);
            return time.TotalHours;
        }

        public static bool OpenDrawer(string PrinterName, string PrinterType)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                pd.PrinterSettings = new PrinterSettings();
                if (PrinterType == "STAR")
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { /*Convert.ToByte(Code)*/7 }));
                else if (PrinterType == "EPSON")
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 48, 55, 121 }));
                else
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { /*Convert.ToByte(Code)*/27, 112, 48, 55, 121 }));
            }
            return true;
        }
    }
}