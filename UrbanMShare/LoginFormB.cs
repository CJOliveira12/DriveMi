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
    public partial class LoginFormB : Form
    {
        //VARIAVEIS___________________________________________________
        string connectionString = @"Data Source=ASUS-PC\DriveMi;Initial Catalog=UrbanMShare;User ID=sa;Password=admindrivemi";
        SqlCommand query;
        
        


        //INICIAR FORMA_______________________________________________
        public LoginFormB()
        {
            InitializeComponent();
            checkloginlb.Text = "";
        }

        //METODO ENCRIPTACAO__________________________________________
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
        //BOTAO CRIAR CONTA_____________________________________________
        private void label5_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.Show();
            this.Hide();
        }

        //_______________________MOVER JANELA____________________________
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //BOTAO FECHAR__________________________________________________

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        //BOTAO MINIMIZAR_______________________________________________
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //BOTAO ENTRAR__________________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            if(checklogin() == true)
            {
                MainForm mainform = new MainForm();
                mainform.Show();
                this.Hide();
            }
        }
        //METODO VERIFICA CREDENCIAIS___________________________________
        private bool checklogin()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                query = new SqlCommand("SELECT Password FROM Client WHERE Email=@Email", cnn);
                query.Parameters.AddWithValue("@Email", emailtb.Text);
                query.ExecuteNonQuery();
                using (SqlDataReader reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!reader.GetString(0).Equals(GetMD5(passwordtb.Text)))
                        {
                            checkloginlb.Text = "Email ou Password incorretos!";
                            query.Dispose();
                            cnn.Close();
                            return false;
                        }
                        if (reader.GetString(0).Equals(GetMD5(passwordtb.Text)))
                        {
                            checkloginlb.Text = "Sucesso!";
                            query.Dispose();
                            cnn.Close();
                            return true;
                        }
                    }
                    else checkloginlb.Text = "Email ou Password incorretos!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
            }
            return false;
        }
    }
}
