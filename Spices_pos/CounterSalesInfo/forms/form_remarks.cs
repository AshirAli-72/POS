using System;
using System.Windows.Forms;
using Datalayer;
using Login_info.controllers;
using RefereningMaterial;
using Spices_pos.DatabaseInfo.WebConfig;

namespace CounterSales_info.forms
{
    public partial class form_remarks : Form
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

        public form_remarks()
        {
            InitializeComponent();
            setFormColorsDynamically();
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        int count = 0;

        private void setFormColorsDynamically()
        {
            try
            {
                int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
                int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
                int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

                int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
                int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
                int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

                int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
                int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
                int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

                //****************************************************************

                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel2, FormNamelabel);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel5, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel3, lblCopyrights);
                GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel15, lblCopyrights);

                //****************************************************************

                GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
                GetSetData.setButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, button3);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void numbers_visibility()
        {
            btn0.Visible = true;
            btn1.Visible = true;
            btn2.Visible = true;
            btn3.Visible = true;
            btn4.Visible = true;
            btn5.Visible = true;
            btn6.Visible = true;
            btn7.Visible = true;
            btn8.Visible = true;
            btn9.Visible = true;
        }

        private void fun_upper_case()
        {
            btn_upper_case.Visible = false;
            btn_lower_case.Visible = true;

            A_btn.Visible = true;
            S_btn.Visible = true;
            D_btn.Visible = true;
            F_btn.Visible = true;
            G_btn.Visible = true;
            H_btn.Visible = true;
            J_btn.Visible = true;
            K_btn.Visible = true;
            L_btn.Visible = true;
            Q_btn.Visible = true;
            W_btn.Visible = true;
            E_btn.Visible = true;
            R_btn.Visible = true;
            T_btn.Visible = true;
            Y_btn.Visible = true;
            U_btn.Visible = true;
            I_btn.Visible = true;
            O_btn.Visible = true;
            P_btn.Visible = true;
            Z_btn.Visible = true;
            X_btn.Visible = true;
            C_btn.Visible = true;
            V_btn.Visible = true;
            B_btn.Visible = true;
            N_btn.Visible = true;
            M_btn.Visible = true;

            btn_a.Visible = false;
            btn_s.Visible = false;
            btn_d.Visible = false;
            btn_f.Visible = false;
            btn_g.Visible = false;
            btn_h.Visible = false;
            btn_j.Visible = false;
            btn_k.Visible = false;
            btn_l.Visible = false;
            btn_q.Visible = false;
            btn_w.Visible = false;
            btn_e.Visible = false;
            btn_r.Visible = false;
            btn_t.Visible = false;
            btn_y.Visible = false;
            btn_u.Visible = false;
            btn_i.Visible = false;
            btn_o.Visible = false;
            btn_p.Visible = false;
            btn_z.Visible = false;
            btn_x.Visible = false;
            btn_c.Visible = false;
            btn_v.Visible = false;
            btn_b.Visible = false;
            btn_n.Visible = false;
            btn_m.Visible = false;
           
        }

        private void fun_lower_case()
        {
            btn_lower_case.Visible = false;
            btn_upper_case.Visible = true;

            btn_a.Visible = true;
            btn_s.Visible = true;
            btn_d.Visible = true;
            btn_f.Visible = true;
            btn_g.Visible = true;
            btn_h.Visible = true;
            btn_j.Visible = true;
            btn_k.Visible = true;
            btn_l.Visible = true;
            btn_q.Visible = true;
            btn_w.Visible = true;
            btn_e.Visible = true;
            btn_r.Visible = true;
            btn_t.Visible = true;
            btn_y.Visible = true;
            btn_u.Visible = true;
            btn_i.Visible = true;
            btn_o.Visible = true;
            btn_p.Visible = true;
            btn_z.Visible = true;
            btn_x.Visible = true;
            btn_c.Visible = true;
            btn_v.Visible = true;
            btn_b.Visible = true;
            btn_n.Visible = true;
            btn_m.Visible = true;

            A_btn.Visible = false;
            S_btn.Visible = false;
            D_btn.Visible = false;
            F_btn.Visible = false;
            G_btn.Visible = false;
            H_btn.Visible = false;
            J_btn.Visible = false;
            K_btn.Visible = false;
            L_btn.Visible = false;
            Q_btn.Visible = false;
            W_btn.Visible = false;
            E_btn.Visible = false;
            R_btn.Visible = false;
            T_btn.Visible = false;
            Y_btn.Visible = false;
            U_btn.Visible = false;
            I_btn.Visible = false;
            O_btn.Visible = false;
            P_btn.Visible = false;
            Z_btn.Visible = false;
            X_btn.Visible = false;
            C_btn.Visible = false;
            V_btn.Visible = false;
            B_btn.Visible = false;
            N_btn.Visible = false;
            M_btn.Visible = false;
        }

        private void form_remarks_Load(object sender, EventArgs e)
        {
            try
            {
                GetSetData.addFormCopyrights(lblCopyrights);
                fun_lower_case();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            TextData.comments = txt_remarks.Text;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnl_keypress.Visible = false;
        }

        private void btn_number_Click(object sender, EventArgs e)
        {
            numbers_visibility();
        }

        private void btn_upper_case_Click(object sender, EventArgs e)
        {
            fun_upper_case();
        }

        private void btn_lower_case_Click(object sender, EventArgs e)
        {
            fun_lower_case();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_remarks.Text = "";
        }

        private void btn_a_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "a"; 
        }

        private void btn_s_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "s"; 
        }

        private void btn_d_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "d"; 
        }

        private void btn_f_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "f"; 
        }

        private void btn_g_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "g"; 
        }

        private void btn_h_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "h"; 
        }

        private void btn_j_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "j"; 
        }

        private void btn_k_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "k";
        }

        private void btn_l_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "l";
        }

        private void btn_q_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "q";
        }

        private void btn_w_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "w";
        }

        private void btn_e_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "e";
        }

        private void btn_r_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "r";
        }

        private void btn_t_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "t";
        }

        private void btn_y_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "y";
        }

        private void btn_u_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "u";
        }

        private void btn_i_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "i";
        }

        private void btn_o_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "o";
        }

        private void btn_p_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "p";
        }

        private void btn_z_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "z";
        }

        private void btn_x_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "x";
        }

        private void btn_c_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "c";
        }

        private void btn_v_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "v";
        }

        private void btn_b_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "b";
        }

        private void btn_n_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "n";
        }

        private void btn_m_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "m";
        }

        private void btn_dot_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += ".";
        }

        private void A_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "A";
        }

        private void S_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "S";
        }

        private void D_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "D";
        }

        private void F_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "F";
        }

        private void G_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "G";
        }

        private void H_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "H";
        }

        private void J_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "J";
        }

        private void K_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "K";
        }

        private void L_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "L";
        }

        private void Q_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "Q";
        }

        private void W_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "W";
        }

        private void E_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "E";
        }

        private void R_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "R";
        }

        private void T_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "T";
        }

        private void Y_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "Y";
        }

        private void U_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "U";
        }

        private void I_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "I";
        }

        private void O_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "O";
        }

        private void P_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "P";
        }

        private void Z_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "Z";
        }

        private void X_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "X";
        }

        private void C_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "C";
        }

        private void V_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "V";
        }

        private void B_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "B";
        }

        private void N_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "N";
        }

        private void M_btn_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "M";
        }

        private void btn_space_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += " ";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txt_remarks.Text += "0";
        }

        private void form_remarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
