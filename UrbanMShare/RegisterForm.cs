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
    public partial class RegisterForm : Form
    {
        //VARIAVEIS___________________________________________________
        string connectionString = @"Data Source=ASUS-PC\DriveMi;Initial Catalog=UrbanMShare;User ID=sa;Password=admindrivemi";
        SqlCommand query;
        string nome, email, senha, cidade, morada, nif, licensecard;
        int telemovel, paycard;

        public RegisterForm()
        {
            InitializeComponent();
            checkemaillb.Text = "";
        }
        //BOTAO REGISTAR______________________________________________
        private void button1_Click(object sender, EventArgs e)
        {
            nome = nometb.Text;
            email = email1tb.Text+"@"+emailtb2.Text;
            senha = senhatb.Text;
            cidade = cidadetb.Text;
            morada = moradatb.Text;
            nif = niftb.Text;
            try
            {
                paycard = Int32.Parse(paycardtb.Text);
                telemovel = Int32.Parse(teletb.Text);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
            }
            licensecard = licensecardtb.Text;
            registintodb(nome,email,GetMD5(senha),morada,nif,paycard,licensecard);
        }
        //METODO REGISTAR
        private void registintodb(string nome, string email, string senha, string morada, string nif, int paycard, string licensecard)
        {
            checkemaillb.Text = "";
            SqlConnection cnn = new SqlConnection(connectionString);
            if(emailExists(email) == false)
            {
                try
                {
                    cnn.Open();
                    query = new SqlCommand("INSERT INTO Client (Name,Email,Password,Address,FiscalNr,PayCardNr,LicenceNr) VALUES (@nome,@email,@senha,@morada,@nif,@paycard,@licensecard)", cnn);
                    query.Parameters.AddWithValue("@nome", nome );
                    query.Parameters.AddWithValue("@email", email );
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@morada", morada);
                    query.Parameters.AddWithValue("@nif", nif);
                    query.Parameters.AddWithValue("@paycard", paycard);
                    query.Parameters.AddWithValue("@licensecard", licensecard);
                    query.ExecuteNonQuery();
                    query.Dispose();
                    cnn.Close();
                    LoginFormB lfb = new LoginFormB();
                    lfb.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
                }
            }
            else if(emailExists(email) == true)
            {
                checkemaillb.Text = "O email inserido já existe!";
            }
        }
        //METODO VERIFICA EMAIL EXISTE_________________________________
        public bool emailExists(string email)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                query = new SqlCommand("SELECT Email FROM Client WHERE Email=@email", cnn);
                query.Parameters.AddWithValue("@email", email);
                query.ExecuteNonQuery();
                using (var reader = query.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        query.Dispose();
                        cnn.Close();
                        return true;
                    }
                    else
                    {
                        query.Dispose();
                        cnn.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
            }
            return true;
        }

        //VOLTAR PARA O LOGIN FORM_____________________________________
        private void button2_Click(object sender, EventArgs e)
        {
            LoginFormB lgb = new LoginFormB();
            lgb.Show();
            this.Hide();
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
        //_____________________MOVER JANELA________________________
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
    }
}
