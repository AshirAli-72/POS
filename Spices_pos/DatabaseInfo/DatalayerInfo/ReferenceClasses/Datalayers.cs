using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using RefereningMaterial;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Datalayer
{
    public class Datalayers
    {
        public SqlConnection conn_;
        public SqlCommand cmd_;
        public SqlDataReader reader_;
        public SqlDataAdapter adptr_;
        public System.Data.DataSet dset_;
        public System.Data.DataTable dt_;

        public static string ret = "";
        public static string getmessage { get; set; }

        public Datalayers(string con_String)
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
                MessageBox.Show(es.Message);
            }
        }

        public bool Connect()
        {
            try
            {
                conn_.Open();
                getmessage = "Connection established!";
                return true;
            }
            catch (Exception exp)
            {
                getmessage = "error while opening connection (Datalayer=>Connect()) : " + exp.Message;
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                conn_.Close();
                getmessage = "Connection Closed Successfully!";
                return true;
            }
            catch (Exception exp)
            {
                getmessage = "error while Closing connection (Datalayer=>Disconnect()) : " + exp.Message;
                return false;

            }

        }

        public string insertUpdateCreateOrDelete(string query)
        {
            string allqueries = query.ToLower();
            try
            {
                if (query != "")
                {
                    cmd_ = new SqlCommand(query, conn_);
                    cmd_.CommandText = query;
                    cmd_.Connection = conn_;
                    Connect();

                    cmd_.ExecuteNonQuery();


                    if (allqueries.Contains("insert into "))
                    {
                        ret = getmessage = "inserted Successfully!";

                    }
                    else if (allqueries.Contains("delete from "))
                    {
                        ret = getmessage = "Deleted Successfully!";
                    }
                    else if (allqueries.Contains("create table "))
                    {
                        ret = getmessage = "Table Created Successfully!";
                    }
                    else if (allqueries.Contains("update  ") && allqueries.Contains("set="))
                    {
                        ret = getmessage = "Updated Successfully";
                    }
                }
            }
            catch (Exception exp)
            {
                //ret = getmessage = "'" + TextData.CusName + "' is already exist!";
                return ret = exp.Message;
            }
            finally
            {
                Disconnect();
            }

            return ret;
        }

        public int Select_ID_for_Foreign_Key_from_db_for_Insertion(string query)
        {
            int temp_id = 0;
            try
            {
                if (query != "")
                {
                    Connect();
                    cmd_ = new SqlCommand(query, conn_);
                    reader_ = cmd_.ExecuteReader();

                    if (reader_.Read())
                    {
                        temp_id = Convert.ToInt32(reader_[0]);
                        reader_.Close();
                    }
                    else
                    {
                        return temp_id;
                    }
                    reader_.Close();
                }
                return temp_id;
            }
            catch (Exception ex)
            {
                //ret = ex.Message;
                return 0;
            }
            finally
            {
                Disconnect();
            }

        }

        public double SearchNumericValuesDb(string query)
        {
            string temp_Name = "0";
           
            try
            {
                if (query != "")
                {
                    Connect();
                    cmd_ = new SqlCommand(query, conn_);
                    reader_ = cmd_.ExecuteReader();

                    if (reader_.Read())
                    {
                        temp_Name = reader_[0].ToString();
                        reader_.Close();
                    }
                    else
                    {
                        return double.Parse(temp_Name);
                    }

                    reader_.Close();
                }
                return double.Parse(temp_Name);
            }
            catch (Exception ex)
            {
                ret = ex.Message;
                return double.Parse(temp_Name);
            }
            finally
            {
                Disconnect();
            }
        }

        public string SearchStringValuesFromDb(string query)
        {
            string temp_Name = "";

            try
            {
                if (query != "")
                {
                    Connect();
                    cmd_ = new SqlCommand(query, conn_);
                    reader_ = cmd_.ExecuteReader();

                    if (reader_.Read())
                    {
                        temp_Name = reader_[0].ToString();
                        reader_.Close();
                    }
                    else
                    {
                        return temp_Name;
                    }

                    reader_.Close();
                }

                return temp_Name;
            }
            catch (Exception ex)
            {
                ret = ex.Message;
                return temp_Name;
            }
            finally
            {
                Disconnect();
            }
        }
        
        public string SearchAPIStringValuesFromDb(string query)
        {
            string temp_Name = "";
            SqlConnection conn = new SqlConnection(webConfig.con_string);

            try
            {
                if (query != "")
                {
                    SqlCommand cmd;
                    SqlDataReader reader;
                    
                    conn.Open();

                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        temp_Name = reader[0].ToString();
                        reader.Close();
                    }
                    else
                    {
                        return temp_Name;
                    }

                    reader.Close();
                }

                return temp_Name;
            }
            catch (Exception ex)
            {
                ret = ex.Message;
                return temp_Name;
            }
            finally
            {
                conn.Close();
            }
        }

        public void NumericValuesOnly(KeyPressEventArgs e)
        {
            try
            {
                Char chr = e.KeyChar;

                if (chr == 46)
                {
                    e.Handled = true;
                    return;
                }

                if (!Char.IsDigit(chr) && chr != 8 && chr != 46)
                {
                    e.Handled = true;
                }
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
            }
        }

        public void NumericValuesOnly(string textvalue, KeyPressEventArgs e)
        {
            try
            {
                Char chr = e.KeyChar;

                if (chr == 46 && textvalue.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }

                if (!Char.IsDigit(chr) && chr != 8 && chr != 46)
                {
                    e.Handled = true;
                }
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
            }
        }

        public string UserPermissions(string selectItem, string tableName, int key)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Permission = "";

                GetSetData.query = "select " +selectItem +" from " + tableName + " where role_id = '" + key.ToString() + "';";
                GetSetData.Permission = SearchStringValuesFromDb(GetSetData.query);

                return GetSetData.Permission;
            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
                return "";
            }
        }

        public string UserPermissions(string selectItem, string tableName)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Permission = "";

                GetSetData.query = "select " + selectItem + " from " + tableName + ";";
                GetSetData.Permission = SearchStringValuesFromDb(GetSetData.query);

                return GetSetData.Permission;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }
        
        public string getApiRequestDB(string selectItem, string tableName)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Permission = "";

                GetSetData.query = "select " + selectItem + " from " + tableName + ";";
                GetSetData.Permission = SearchAPIStringValuesFromDb(GetSetData.query);

                return GetSetData.Permission;
            }
            catch (Exception es)
            {
                System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }

        public string UserPermissions(string selectItem, string tableName, string keyAttribute, string key)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Data = "";

                GetSetData.query = "select " + selectItem + " from " + tableName + " where " + keyAttribute + " = '" + key +"';";
                GetSetData.Data = SearchStringValuesFromDb(GetSetData.query);

                return GetSetData.Data;
            }
            catch (Exception es)
            {
                //System.Windows.Forms.MessageBox.Show(es.Message);
                return "";
            }
        }

        public int UserPermissionsIds(string selectItem, string tableName, string keyAttribute, string key)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Ids = 0;

                GetSetData.query = "select " + selectItem + " from " + tableName + " where " + keyAttribute + " = '" + key +"';";
                GetSetData.Ids = Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                return GetSetData.Ids;
            }
            catch (Exception es)
            {
                //System.Windows.Forms.MessageBox.Show(es.Message);
                return 0;
            }
        }

        public int UserPermissionsIds(string selectItem, string tableName)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.Ids = 0;

                GetSetData.query = "select " + selectItem + " from " + tableName + ";";
                GetSetData.Ids = Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

                return GetSetData.Ids;
            }
            catch (Exception es)
            {
                //System.Windows.Forms.MessageBox.Show(es.Message);
                return 0;
            }
        }

        public double NumericValues(string selectItem, string tableName, string keyAttribute, string key)
        {
            try
            {
                ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
                GetSetData.numericValue = 0;

                GetSetData.query = "select " + selectItem + " from " + tableName + " where " + keyAttribute + " = '" + key + "';";
                GetSetData.numericValue = SearchNumericValuesDb(GetSetData.query);

                return GetSetData.numericValue;
            }
            catch (Exception es)
            {
                //System.Windows.Forms.MessageBox.Show(es.Message);
                return 0;
            }
        }

        public void EnableDisableButtonControls(string tableName, string attribute, Button button)
        {
            string temp_Name = "";

            try
            {
                Connect();
                cmd_ = new SqlCommand("select " + attribute + " from " + tableName + ";", conn_);
                reader_ = cmd_.ExecuteReader();

                if (reader_.Read())
                {
                    temp_Name = reader_[attribute].ToString();
                    reader_.Close();
                }
                reader_.Close();

                if (temp_Name != "Enabled")
                {
                    button.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public void YesNoButtonControls(string tableName, string attribute, Button button)
        {
            string temp_Name = "";

            try
            {
                Connect();
                cmd_ = new SqlCommand("select " + attribute + " from " + tableName + ";", conn_);
                reader_ = cmd_.ExecuteReader();

                if (reader_.Read())
                {
                    temp_Name = reader_[attribute].ToString();
                    reader_.Close();
                }
                reader_.Close();

                if (temp_Name != "Enabled")
                {
                    button.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public void YesNoTextBoxControls(string tableName, string attribute, RichTextBox text)
        {
            string temp_Name = "";

            try
            {
                Connect();
                cmd_ = new SqlCommand("select " + attribute + " from " + tableName + ";", conn_);
                reader_ = cmd_.ExecuteReader();

                if (reader_.Read())
                {
                    temp_Name = reader_[attribute].ToString();
                    reader_.Close();
                }
                reader_.Close();

                if (temp_Name != "Yes")
                {
                    text.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
