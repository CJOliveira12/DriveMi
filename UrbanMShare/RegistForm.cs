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
    public partial class RegistForm : Form
    {

        string username, name, password, email, city, address, nif, ccnr, licensenr;
        int cellphone;
        bool booleancheck;

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
            this.Hide();
        }
        //Para a tela mover-se sem a border da form-----------------------------------------------
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void RegistForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        //------------------------------------------------------------------------------------------


        public RegistForm()
        {
            InitializeComponent();
            emptychecklb.Text = "";
            checknamelb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = rusernametb.Text;
            name = rnametb.Text;
            password = rpasswordtb.Text;
            email = remailtb.Text;
            city = rcitytb.Text;
            try {
                cellphone = Int32.Parse(rcellphonetb.Text);
            } catch (FormatException ex){
                Console.WriteLine(ex);
            }
            address = raddresstb.Text;
            nif = rniftb.Text;
            ccnr = rccnrtb.Text;
            licensenr = rlicensenrtb.Text;
            registindb(username,name,GetMD5(password),email,cellphone,city,address,nif,ccnr,licensenr);
        }

        public void registindb(string username, string name, string password, string email, int cellphone, string city, string address, string nif, string ccnr, string licensenr)
        {
            checknamelb.Text = "";
            emptychecklb.Text = "";
            string connectionString = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=danielteste;Integrated Security=True";
            SqlCommand query;
            SqlConnection cnn = new SqlConnection(connectionString);
            string insertquery = "INSERT INTO Clientes (Username, Nome, Password, Email, Contacto, Cidade, Morada, Nif, NrCartao, NrCarta) VALUES (@Username, @Name, @Password, @Email, @Cellphone, @City, @Address, @Nif, @Ccnr, @Licensenr)";

            if (checkTbEmptyness() == true && userNameExists(username) == false)
            {
                try
                {
                    cnn.Open();
                    query = new SqlCommand(insertquery, cnn);
                    query.Parameters.AddWithValue("@Username", username);
                    query.Parameters.AddWithValue("@Name", name);
                    query.Parameters.AddWithValue("@Password", password);
                    query.Parameters.AddWithValue("@Email", email);
                    query.Parameters.AddWithValue("@Cellphone", cellphone);
                    query.Parameters.AddWithValue("@City", city);
                    query.Parameters.AddWithValue("@Address", address);
                    query.Parameters.AddWithValue("@Nif", nif);
                    query.Parameters.AddWithValue("@Ccnr", ccnr);
                    query.Parameters.AddWithValue("@Licensenr", licensenr);
                    query.ExecuteNonQuery();
                    query.Dispose();
                    cnn.Close();
                    LoginForm loginform = new LoginForm();
                    loginform.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Servidor desligado, verifique a sua ligação à internet e tente novamente!");
                }
            }
            else if(userNameExists(username) == true)
            {
                checknamelb.Text = "O username que inseriu já está a ser utilizado";
            }
        }

        public bool checkTbEmptyness()
        {
            if (username.Equals("") || name.Equals("") || password.Equals("") || email.Equals(""))
            {
                emptychecklb.Text = "* Preencha todos os campos obrigatórios";
                booleancheck = false;
                return booleancheck;
            }
            else
            {
                booleancheck = true;
                return booleancheck;
            }
        }
        private void RegistForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm loginform = new LoginForm();
            loginform.Show();
        }

        public bool userNameExists(string username)
        {
            string connectionString = @"Data Source=ASUS-PC\SQLEXPRESS;Initial Catalog=danielteste;Integrated Security=True";
            SqlCommand query;
            SqlConnection cnn = new SqlConnection(connectionString);
            string searchnamequery = "SELECT * FROM Clientes WHERE Username=@Usern";
            try
            {
                cnn.Open();
                query = new SqlCommand(searchnamequery, cnn);
                query.Parameters.AddWithValue("@Usern", username);
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

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for(int i=1 ; i<result.Length ; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
    }
}
