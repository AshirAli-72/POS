using Datalayer;
using Guna.UI2.WinForms;
using Message_box_info.forms;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;
using Spices_pos.DatabaseInfo.DatalayerInfo.MigrationClasses;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using Spices_pos.DatabaseInfo.WebConfig;
using Spices_pos.LoginInfo.controllers;
using System;
using System.Collections.Generic;
using System.Data;              // DataTable, DataRow
using System.Data.Sql;          // SqlDataSourceEnumerator
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;


namespace Spices_pos.LoginInfo.forms
{
    public partial class login_form : Form
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
        // ✅ Cache SQL Servers
        private List<string> cachedServers = new List<string>();

        // ✅ Cache Auth Modes per server
        private Dictionary<string, List<string>> cachedAuthModes =
            new Dictionary<string, List<string>>();

        public login_form()
        {
            InitializeComponent();
            
        }

        //datalayer data = new datalayer(webConfig.con_string);
        //DashboardPermissionsManager dashboardPermissions = new DashboardPermissionsManager(webConfig.con_string);
        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        //done_form done = new done_form();
        error_form error = new error_form();
        form_sure_message sure = new form_sure_message();
        public static bool isSwitchUser;

        private void logo_imag()
        {
            try
            {
                TextData.address = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "picture_path");
                TextData.logo = GetSetData.ProcedureGeneralSettings("ProcedureGeneralSettings", "logo_path");

                if (TextData.address != "" && TextData.address != "NULL")
                {
                    if (TextData.logo != "nill" && TextData.logo != "" && TextData.logo != null)
                    {
                        logo.Image = Bitmap.FromFile(TextData.address + TextData.logo);
                        logo1.Image = Bitmap.FromFile(TextData.address + TextData.logo);
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sure.Message_choose("Are you sure you want logout!");
            sure.ShowDialog();

            if (form_sure_message.sure == true)
            {
                Application.Exit();
            }
        }

        private async void login_form_Shown(object sender, EventArgs e)
        {
            try
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                btnPin.FillColor = Color.LemonChiffon;

                clock.Start();
                logo_imag();
                GetSetData.addFormCopyrights(lblCopyrights);

                txtPinCode.Select();

                // 🔥 FAST startup load
                await InitializeServersAndAuthAsync();
            }
            catch (Exception ex)
            {
                error.errorMessage(ex.Message);
                error.ShowDialog();
            }
        }


        private void login_with_enter_key(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    TextData.username = username_text.Text;
                    TextData.password = pass_text.Text;

                    if (Button_controls.login_button(username_text, pass_text, txt_barcode, isSwitchUser))
                    { 
                        this.Hide();
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void login_btn_Click_1(object sender, EventArgs e)
        {
            try
            {
                TextData.username = username_text.Text;
                TextData.password = pass_text.Text;

                if (Button_controls.login_button(username_text, pass_text, txt_barcode, isSwitchUser))
                {
                    this.Hide();
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }
        private void chkLoginType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoginType.Checked == true)
            {
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = true;
                chkLoginType.Checked = false;
                chkLoginType1.Checked = false;
                txt_barcode.Focus();
            }
        }

        private void chkLoginType1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoginType1.Checked == true)
            {
                //Button_controls.CheckLoginDetails(username_text, pass_text, txt_barcode);

                pnlLogin.Visible = true;
                pnlLoginByScanner.Visible = false;
                chkLoginType.Checked = false;
                chkLoginType1.Visible = false;
                username_text.Focus();
            }
        }

        private void loadByScanner(Guna2TextBox txtPassword)
        {
            try
            {
                string username = "";

                if (txtPassword.Text != "")
                {
                    username = data.UserPermissions("username", "pos_users", "password", txtPassword.Text);

                    if (username != "")
                    {
                        username_text.Text = username;


                        if (Button_controls.login_button(username_text, txtPassword, txtPassword, isSwitchUser))
                        {
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loadByScanner(txt_barcode);
            }
        }
        private void login_form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnPin.FillColor = Color.LemonChiffon;
                btnPassword.FillColor = Color.White;
                btnScan.FillColor = Color.White;

                pnlPinCode.Visible = true;
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = false;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                txtPinCode.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnPin.FillColor = Color.White;
                btnPassword.FillColor = Color.LemonChiffon;
                btnScan.FillColor = Color.White;


                pnlPinCode.Visible = false;
                pnlLogin.Visible = true;
                pnlLoginByScanner.Visible = false;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                username_text.Select();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPin.FillColor = Color.White;
                btnPassword.FillColor = Color.White;
                btnScan.FillColor = Color.LemonChiffon ;


                pnlPinCode.Visible = false;
                pnlLogin.Visible = false;
                pnlLoginByScanner.Visible = true;

                username_text.Text = "";
                pass_text.Text = "";
                txt_barcode.Text = "";
                txtPinCode.Text = "";

                txt_barcode.Select();
            }
        }

        private void username_text_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(e);

            if (e.KeyChar == (char)Keys.Enter)
            {
                loadByScanner(txtPinCode);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Guna2Button button = sender as Guna2Button; // Cast the sender to Button

            if (button != null)
            {
                txtPinCode.Text += button.Text; // Append button text to TextBox
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtPinCode.Text.Length > 0)
            {
                txtPinCode.Text = txtPinCode.Text.Remove(txtPinCode.Text.Length - 1);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            loadByScanner(txtPinCode);
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.LemonChiffon;
            btnPassword.FillColor = Color.White;
            btnScan.FillColor = Color.White;
            btn_database.FillColor = Color.White;
            pnlPinCode.Visible = true;
            pnlLogin.Visible = false;
            pnlLoginByScanner.Visible = false;
            guna2Panel3.Visible = false;
            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            txtPinCode.Select();
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.White;
            btnPassword.FillColor = Color.LemonChiffon;
            btnScan.FillColor = Color.White;
            btn_database.FillColor=Color.White;
            pnlPinCode.Visible = false;
            pnlLogin.Visible = true;
            pnlLoginByScanner.Visible = false;
            guna2Panel3.Visible = false;
            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            username_text.Select();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnPin.FillColor = Color.White;
            btnPassword.FillColor = Color.White;
            btnScan.FillColor = Color.LemonChiffon;
            btn_database.FillColor = Color.White;

            pnlPinCode.Visible = false;
            pnlLogin.Visible = false;
            pnlLoginByScanner.Visible = true;
            guna2Panel3.Visible = false;
            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            txt_barcode.Select();
        }
        private async Task LoadSqlServersAsync()
        {
            server_name.Items.Clear();
            server_name.Items.Add("Loading servers..."); // placeholder

            // 1️⃣ Check in-memory cache
            if (cachedServers.Count > 0)
            {
                server_name.Items.Clear();
                server_name.Items.AddRange(cachedServers.ToArray());
                server_name.SelectedIndex = 0;
                return;
            }

            string cacheFile = Path.Combine(Application.StartupPath, "servers.cache");

            // 2️⃣ Load from disk cache if exists
            if (File.Exists(cacheFile))
            {
                cachedServers = File.ReadAllLines(cacheFile).ToList();
                server_name.Invoke((Action)(() =>
                {
                    server_name.Items.Clear();
                    server_name.Items.AddRange(cachedServers.ToArray());
                    server_name.SelectedIndex = 0;
                }));
                // Still run network discovery in background to refresh cache
                _ = Task.Run(() => RefreshSqlServerCacheAsync(cacheFile));
                return;
            }

            // 3️⃣ No cache → first-time discovery
            await RefreshSqlServerCacheAsync(cacheFile);
        }

        // Background method to refresh server cache
        private async Task RefreshSqlServerCacheAsync(string cacheFile)
        {
            HashSet<string> servers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DataTable dt = null;

            await Task.Run(() =>
            {
                try { dt = SqlDataSourceEnumerator.Instance.GetDataSources(); }
                catch { }
            });

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string server = row["ServerName"]?.ToString();
                    string instance = row["InstanceName"]?.ToString();
                    if (string.IsNullOrWhiteSpace(server)) continue;
                    string fullName = string.IsNullOrWhiteSpace(instance) ? server : $"{server}\\{instance}";
                    servers.Add(fullName);
                }
            }

            cachedServers = servers.OrderBy(s => s).ToList();

            // Save to disk cache
            try
            {
                File.WriteAllLines(cacheFile, cachedServers);
            }
            catch { }

            // Update UI safely
            if (server_name.IsHandleCreated)
            {
                server_name.Invoke((Action)(() =>
                {
                    server_name.Items.Clear();
                    if (cachedServers.Count > 0)
                    {
                        server_name.Items.AddRange(cachedServers.ToArray());
                        server_name.SelectedIndex = 0;
                    }
                }));
            }
        }

        private async Task InitializeServersAndAuthAsync()
        {
            // Load servers in background
            var loadServersTask = LoadSqlServersAsync();

            // Default server for auth check
            string defaultServer = Environment.MachineName;
            var loadAuthTask = LoadAuthModes_FromDataLayerAsync(defaultServer);

            await loadServersTask;

            // Check selected server
            string selectedServer = server_name.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedServer) && selectedServer != defaultServer)
            {
                await LoadAuthModes_FromDataLayerAsync(selectedServer);
            }
            else
            {
                await loadAuthTask;
            }
        }



        private async Task LoadAuthModes_FromDataLayerAsync(string server = null)
        {
            // Auto-pick selected server if not passed
            if (string.IsNullOrWhiteSpace(server))
                server = server_name.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(server))
                return;

            // ✅ Cache hit → instant UI
            if (cachedAuthModes.ContainsKey(server))
            {
                cmbAuth.Items.Clear();
                cmbAuth.Items.AddRange(cachedAuthModes[server].ToArray());
                cmbAuth.SelectedIndex = 0;
                cmbAuth_SelectedIndexChanged(null, null);
                return;
            }

            // Default UI (instant)
            cmbAuth.Items.Clear();
            cmbAuth.Items.Add("Windows Authentication");
            cmbAuth.SelectedIndex = 0;

            List<string> authModes = new List<string>
    {
        "Windows Authentication"
    };

            // 🔥 Background check
            await Task.Run(() =>
            {
                try
                {
                    string conStr =
                        $"Data Source={server};Initial Catalog=master;" +
                        $"Integrated Security=True;Connect Timeout=2;";

                    Datalayers dl = new Datalayers(conStr);

                    string result = dl.SearchStringValuesFromDb(
                        "SELECT CAST(SERVERPROPERTY('IsIntegratedSecurityOnly') AS INT)"
                    )?.Trim();

                    int isWindowsOnly = 1;
                    int.TryParse(result, out isWindowsOnly);

                    // Mixed Mode → allow SQL Auth
                    if (isWindowsOnly == 0)
                    {
                        authModes.Add("SQL Server Authentication");
                    }
                }
                catch
                {
                    // Ignore errors → fallback Windows Auth only
                }
            });

            cachedAuthModes[server] = authModes;

            // ✅ UI update (safe)
            if (cmbAuth.IsHandleCreated)
            {
                cmbAuth.Invoke(new Action(() =>
                {
                    cmbAuth.Items.Clear();
                    cmbAuth.Items.AddRange(authModes.ToArray());
                    cmbAuth.SelectedIndex = 0;
                    cmbAuth_SelectedIndexChanged(null, null);
                }));
            }
        }


       
      


      

        private void btnconnect_Click(object sender, EventArgs e)
        {
            string server = server_name.Text.Trim();
            string database = "installment_db"; // Fixed DB
            string authMode = cmbAuth.SelectedItem?.ToString() ?? "Windows Authentication";
            string user = txtuser.Text.Trim();
            string pass = txtpass.Text.Trim();

            // Validate server name
            if (string.IsNullOrEmpty(server))
            {
                MessageBox.Show("Please enter or select a valid SQL Server name.", "Missing Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                server_name.Focus();
                return;
            }

            // Validate SQL Server Authentication input
            if (authMode == "SQL Server Authentication")
            {
                if (string.IsNullOrEmpty(user))
                {
                    MessageBox.Show("Please enter username for SQL Server Authentication.", "Missing Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtuser.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Please enter password for SQL Server Authentication.", "Missing Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpass.Focus();
                    return;
                }
            }

            // Build connection string
            string connectionString = (authMode == "Windows Authentication")
                ? $"Data Source={server};Initial Catalog={database};Integrated Security=True;Connect Timeout=3;"
                : $"Data Source={server};Initial Catalog={database};User ID={user};Password={pass};Connect Timeout=3;";

            try
            {
                // Attempt connection
                Datalayers dl = new Datalayers(connectionString);

                if (dl.Connect())
                {
                    MessageBox.Show("Connection successful!");
                    dl.Disconnect();
                    webConfig.SaveConnectionString(connectionString);

                    // Update global instance
                    data = new Datalayers(connectionString);
                }
                else
                {
                    // Friendly error messages
                    if (Datalayers.getmessage.Contains("Login failed for user"))
                    {
                        MessageBox.Show("Invalid username or password. Please check your credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Datalayers.getmessage.Contains("A network-related or instance-specific error"))
                    {
                        MessageBox.Show("Cannot connect to the SQL Server. Please check the server name and network connection.", "Server Unreachable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Connection failed: " + Datalayers.getmessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_database_Click(object sender, EventArgs e)
        {
            // Button colors
            btn_database.FillColor = Color.LemonChiffon;
            btnPin.FillColor = Color.White;
            btnPassword.FillColor = Color.White;
            btnScan.FillColor = Color.White;

            // ❗ Sab panels pehle hide
            pnlPinCode.Visible = false;
            pnlLogin.Visible = false;
            pnlLoginByScanner.Visible = false;

            // ❗ Database panel show + front pe lao
            guna2Panel3.Visible = true;
             

            // Clear fields
            username_text.Text = "";
            pass_text.Text = "";
            txt_barcode.Text = "";
            txtPinCode.Text = "";

            // Background loading (UI block nahi hogi)
            _ = LoadSqlServersAsync();
            _ = LoadAuthModes_FromDataLayerAsync();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            pnlPinCode.Visible = true;
            guna2Panel3.Visible = false;
        }

        private void cmbAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isSqlAuth = cmbAuth.SelectedItem?.ToString() == "SQL Server Authentication";

            // Show/hide username & password dynamically
            lbluser.Visible = isSqlAuth;
            lblpass.Visible = isSqlAuth;
            txtuser.Visible = isSqlAuth;
            txtpass.Visible = isSqlAuth;

            // Enable/disable input boxes
            txtuser.Enabled = isSqlAuth;
            txtpass.Enabled = isSqlAuth;

            // Clear fields
            txtuser.Text = "";
            txtpass.Text = "";

            // Focus textbox if SQL Auth
            if (isSqlAuth)
                txtuser.Focus();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Optional: allow only letters and digits
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // ignore invalid characters
            }
        }

    }
}
