using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrbanMShare
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            checkloginlb.Text = "";


            LoginFormB lfb = new LoginFormB();
            lfb.Show();
            /*MainForm mf = new MainForm();
            mf.Show();*/
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistForm registform = new RegistForm();
            registform.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkLogin() == true)
            {
                MenuForm menuform = new UrbanMShare.MenuForm();
                menuform.Show();
                this.Hide();
            }
        }

        public bool checkLogin()
        {
            string connectionString = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=danielteste;Integrated Security=True";
            SqlCommand query;
            SqlConnection cnn = new SqlConnection(connectionString);
            string selectquery = "SELECT Password FROM Clientes WHERE Username=@Usern";
            try
            {
                cnn.Open();
                query = new SqlCommand(selectquery, cnn);
                query.Parameters.AddWithValue("@Usern", usernametb.Text);
                query.ExecuteNonQuery();
                using (var reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!reader.GetString(0).Equals(GetMD5(passwordtb.Text)))
                        {
                            checkloginlb.Text = "Username ou Password incorretos!";
                            query.Dispose();
                            cnn.Close();
                            return false;
                        }
                        else if (reader.GetString(0).Equals(GetMD5(passwordtb.Text)))
                        {
                            //checkloginlb.Text = "Correct password!";
                            query.Dispose();
                            cnn.Close();
                            return true;
                        }
                    }
                    else checkloginlb.Text = "Username ou Password incorretos!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
            }
            return false;
        }

        public void checkUserType()
        {
            string connectionString = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=danielteste;Integrated Security=True";
            SqlCommand query;
            SqlConnection cnn = new SqlConnection(connectionString);
            string typequery = "SELECT Tipo FROM Clientes WHERE Username=@Usern";
            try
            {
                cnn.Open();
                query = new SqlCommand(typequery, cnn);
                query.Parameters.AddWithValue("@Usern", usernametb.Text);
                query.ExecuteNonQuery();
                using (var reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.GetInt32(0) != 0)
                        {
                            //
                            // CRIAR AMBIENTE DE ADMINS
                            //
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
            }
        }
        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }

        //Para a tela mover-se sem a border da form-----------------------------------------------
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //------------------------------------------------------------------------------------------
        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
