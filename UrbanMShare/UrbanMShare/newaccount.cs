using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;

namespace UrbanMShare
{
    public partial class newaccount : UserControl
    {
        private static newaccount _instance;

       public static newaccount Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new newaccount();
                return _instance;
            }
        }
        public newaccount()
        {
            InitializeComponent();
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); //formato de email
            int parsedValue;
            DateTime tmpDate, cartaValidadeDate, cPagValidadeDate, birthdayDate;
            if (DateTime.TryParse(tblicenceval.Text, out tmpDate))
            {
                cartaValidadeDate = DateTime.ParseExact(tblicenceval.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                cartaValidadeDate = DateTime.ParseExact("01-01-1000", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            if (DateTime.TryParse(tbcardval.Text, out tmpDate))
            {
                cPagValidadeDate = DateTime.ParseExact(tbcardval.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                cPagValidadeDate = DateTime.ParseExact("01-01-1000", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            if (DateTime.TryParse(tbbirthday.Text, out tmpDate))
            {
                birthdayDate = DateTime.ParseExact(tbbirthday.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                birthdayDate = DateTime.ParseExact("01-01-1000", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            if (tbusername.Text == "" || tbusername.Text == null || int.TryParse(tbusername.Text, out parsedValue))
            {
                MessageBox.Show("Campo nome de utilizador não preenchido devidamente.", "DriveMi - UrbanMShare");
            }
            else if (tbpassword.Text == "" || tbpassword.Text == null)
            {
                MessageBox.Show("Campo nome de Senha não preenchido devidamente.", "DriveMi - UrbanMShare");
            }
            else if (tbname.Text == "" || tbname.Text == null || int.TryParse(tbname.Text, out parsedValue))
            {
                MessageBox.Show("Campo nome não preenchido devidamente.", "DriveMi - UrbanMShare");
            }
            else if (tbaddress.Text == "" || tbaddress.Text == null || int.TryParse(tbaddress.Text, out parsedValue))
            {
                MessageBox.Show("Campo morada não preenchido devidamente.", "DriveMi - UrbanMShare");
            }
            else if (tbpostal1.Text == "" || tbpostal1.Text == null)
            {
                MessageBox.Show("Campo do código postal não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tbpostal1.Text.Length != 4)
            {
                MessageBox.Show("Insira um código postal válido.", "DriveMi - UrbanMShare");
            }
            else if (tbpostal2.Text == "" || tbpostal2.Text == null)
            {
                MessageBox.Show("Campo do código postal não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tblocality.Text == "" || tblocality.Text == null || int.TryParse(tblocality.Text, out parsedValue))
            {
                MessageBox.Show("Campo localidade não preenchid devidamente.", "DriveMi - UrbanMShare");
            }
            else if (tbfiscal.Text == "" || tbfiscal.Text == null)
            {
                MessageBox.Show("Campo número fiscal não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tbfiscal.Text.Length != 9)
            {
                MessageBox.Show("Insira um NIF válido.", "DriveMi - UrbanMShare");
            }
            else if (tbbirthday.Text == "" || tbbirthday.Text == null || birthdayDate.ToString() == "01-01-1000 00:00:00")
            {
                MessageBox.Show("Campo da data de nascimento com data inválida.", "DriveMi - UrbanMShare");
            }
            else if (tblicence.Text == "" || tblicence.Text == null)
            {
                MessageBox.Show("Campo do número da carta de condução não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tblicenceval.Text == "" || tblicenceval.Text == null || cartaValidadeDate.ToString() == "01-01-1000 00:00:00")
            {
                MessageBox.Show("Campo da validade da carta de condução com data inválida.", "DriveMi - UrbanMShare");
            }
            else if (cartaValidadeDate < DateTime.Now)
            {
                MessageBox.Show("A validade da carta de condução está expirada.", "DriveMi - UrbanMShare");
            }
            else if (tbcard.Text == "" || tbcard.Text == null)
            {
                MessageBox.Show("Campo do cartão de pagamento não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tbcard.Text.Length != 16)
            {
                MessageBox.Show("Insira um número de cartão de pagamento válido.", "DriveMi - UrbanMShare");
            }
            else if (tbcardval.Text == "" || tbcardval.Text == null || cPagValidadeDate.ToString() == "01-01-1000 00:00:00")
            {
                MessageBox.Show("Campo da validade do cartão com data inválida", "DriveMi - UrbanMShare");
            }
            else if (cPagValidadeDate < DateTime.Now)
            {
                MessageBox.Show("A validade do cartão de pagamento está expirada.", "DriveMi - UrbanMShare");
            }
            else if (tbemail.Text == "" || tbemail.Text == null)
            {
                MessageBox.Show("Campo email não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tbphone.Text == "" || tbphone.Text == null)
            {
                MessageBox.Show("Campo contacto não preenchido.", "DriveMi - UrbanMShare");
            }
            else if (tbphone.Text.Length != 9)
            {
                MessageBox.Show("Insira um número de contacto válido.", "DriveMi - UrbanMShare");
            }
            else if (!reg.IsMatch(tbemail.Text))
            {
                MessageBox.Show("Introduza um email válido.", "DriveMi - UrbanMShare");
            }
            else if (!int.TryParse(tbfiscal.Text, out parsedValue))
            {
                MessageBox.Show("O NIF que inseriu não são numéricos.", "DriveMi - UrbanMShare");
            }else
            {

            string fullName = tbname.Text;
            string[] names = fullName.Split(' ');
            DateTime cardval = DateTime.ParseExact(tbcardval.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime licenceval = DateTime.ParseExact(tblicenceval.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime birthday = DateTime.ParseExact(tbbirthday.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
            sqlconn.Open();
            SqlCommand qry = new SqlCommand("SELECT count([IDperson]) FROM [00Autentication] Where [Usrname]=@username;", sqlconn);
            qry.Parameters.Add("@username", SqlDbType.VarChar).Value = tbusername.Text;
            int usercount = Convert.ToInt32(qry.ExecuteScalar());
            sqlconn.Close();

            if (usercount > 1)
            {
                MessageBox.Show("O nome de utilizador escolhido já existe.", "DriveMi - UrbanMShare");
                return; 
            }

            sqlconn.Open();
            SqlCommand qry2 = new SqlCommand(@"INSERT INTO [0401Client] (
                                            [IDaddress], [IDlocality], [RegDate], [Name],[Address],[FiscalNr],
                                            [PayCardNr],[PayCardValidity],[LicenceNr],[LicenceNrValidity],
                                            [DateBirth],[SmallName])
                                            output INSERTED.IDclient VALUES ( 
                                            @IDaddress, @IDlocality, @RegDate, @Name, @Address, @FiscalNr, 
                                            @PayCardNr, @PayCardValidity, @LicenceNr, @LicenceNrValidity, 
                                            @DateBirth, @SmallName)"
                                            , sqlconn);

            qry2.Parameters.Add("@IDaddress", SqlDbType.VarChar).Value = tbpostal2.Text;
            qry2.Parameters.Add("@IDlocality", SqlDbType.VarChar).Value = tbpostal1.Text;
            qry2.Parameters.Add("@RegDate", SqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy/MM/dd");
            qry2.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbname.Text;
            qry2.Parameters.Add("@Address", SqlDbType.VarChar).Value = tbaddress.Text;
            qry2.Parameters.Add("@FiscalNr", SqlDbType.VarChar).Value = tbfiscal.Text;
            qry2.Parameters.Add("@PayCardNr", SqlDbType.VarChar).Value = tbcard.Text;
            qry2.Parameters.Add("@PayCardValidity", SqlDbType.VarChar).Value = cardval.ToString("yyyy-MM-dd");
            qry2.Parameters.Add("@LicenceNr", SqlDbType.VarChar).Value = tblicence.Text;
            qry2.Parameters.Add("@LicenceNrValidity", SqlDbType.VarChar).Value = licenceval.ToString("yyyy-MM-dd");
            qry2.Parameters.Add("@DateBirth", SqlDbType.VarChar).Value = birthday.ToString("yyyy-MM-dd");
            qry2.Parameters.Add("@SmallName", SqlDbType.VarChar).Value = names.First() + " " + names.Last();

            int iduser = Convert.ToInt32(qry2.ExecuteScalar());

            SqlCommand qry3 = new SqlCommand(@"INSERT INTO [00Autentication] (
                                            [IDperson], [IDpersonType], [Usrname], [Usrpass],[CurrentState])
                                            VALUES ( 
                                            @IDperson, @IDpersontype, @Usrname, @Usrpass, @CurrentState)"
                                            , sqlconn);

            qry3.Parameters.Add("@IDperson", SqlDbType.VarChar).Value = iduser;
            qry3.Parameters.Add("@IDpersontype", SqlDbType.VarChar).Value = 24;
            qry3.Parameters.Add("@Usrname", SqlDbType.VarChar).Value = tbusername.Text;
            qry3.Parameters.Add("@Usrpass", SqlDbType.VarChar).Value = Program.GetMD5(tbpassword.Text);
            qry3.Parameters.Add("@CurrentState", SqlDbType.VarChar).Value = 1;

            qry3.ExecuteScalar();

            SqlCommand qry4 = new SqlCommand(@"INSERT INTO [0403Contact] (
                                        [IDperson], [IDcontactType], [IDpersonType], [Contact],[Obs])
                                        VALUES (@IDperson, @IDcontactType, @IDpersonType, @Contact, @Obs)", sqlconn);

            qry4.Parameters.Add("@IDperson", SqlDbType.VarChar).Value = iduser;
            qry4.Parameters.Add("@IDcontactType", SqlDbType.VarChar).Value = 4;
            qry4.Parameters.Add("@IDpersonType", SqlDbType.VarChar).Value = 24;
            qry4.Parameters.Add("@Contact", SqlDbType.VarChar).Value = tbemail.Text;
            qry4.Parameters.Add("@Obs", SqlDbType.VarChar).Value = "Contacto de email guardado durante o registo.";

            qry4.ExecuteScalar();

            SqlCommand qry5 = new SqlCommand(@"INSERT INTO [0403Contact] (
                                    [IDperson], [IDcontactType], [IDpersonType], [Contact],[Obs])
                                    VALUES (@IDperson, @IDcontactType, @IDpersonType, @Contact, @Obs)", sqlconn);

            qry5.Parameters.Add("@IDperson", SqlDbType.VarChar).Value = iduser;
            qry5.Parameters.Add("@IDcontactType", SqlDbType.VarChar).Value = 1;
            qry5.Parameters.Add("@IDpersonType", SqlDbType.VarChar).Value = 24;
            qry5.Parameters.Add("@Contact", SqlDbType.VarChar).Value = tbphone.Text;
            qry5.Parameters.Add("@Obs", SqlDbType.VarChar).Value = "Contacto telefónico guardado durante o registo.";

            qry5.ExecuteScalar();

                sqlconn.Close();
            
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.Controls.Add(login.Instance);
            login.Instance.Dock = DockStyle.Fill;
            login.Instance.BringToFront();
            login.Instance.tbusername.Text = tbusername.Text;
            login.Instance.tbpassword.Text = tbpassword.Text;

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                if (c.GetType() == typeof(MaskedTextBox))
                {
                    ((MaskedTextBox)(c)).Text = string.Empty;
                }
            }
            }
        }

        private void bcancel_Click(object sender, EventArgs e)
        {
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.Width = 950;
            panel1.Left = 0;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                if (c.GetType() == typeof(MaskedTextBox))
                {
                    ((MaskedTextBox)(c)).Text = string.Empty;
                }
            }
            panel1.Controls.Add(login.Instance);
            login.Instance.Dock = DockStyle.Fill;
            login.Instance.BringToFront();
         }

        private void tbpostal1_Leave(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [Locality] FROM [0203Locality] WHERE [IDlocality] = @postal", sqlconn);
            cmd.Parameters.Add("@postal", SqlDbType.VarChar).Value = tbpostal1.Text;
            tblocality.Text = (string)cmd.ExecuteScalar();
            sqlconn.Close();
        }

        private void tbpostal2_Leave(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [Locality] FROM [0203Locality] WHERE [IDlocality] = @postal", sqlconn);
            cmd.Parameters.Add("@postal", SqlDbType.VarChar).Value = tbpostal1.Text;
            tblocality.Text = (string)cmd.ExecuteScalar();
            sqlconn.Close();
        }

        private void tbpostal2_Leave_1(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
            sqlconn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [Address] FROM [0204Address] where [IDaddress]= @address and [IDlocality]=@postal", sqlconn);
            cmd.Parameters.Add("@postal", SqlDbType.VarChar).Value = tbpostal1.Text;
            cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = tbpostal2.Text;
            tbaddress.Text = (string)cmd.ExecuteScalar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tbname.Text = "Pedro Agostinho Ferreira da Silva";
            tbaddress.Text = "Rua do cimo nr 14 hab 56";
            tbpostal1.Text = "4445";
            tbpostal2.Text = "116";
            tblocality.Text = "Alfena";
            tblicence.Text = "P-123456";
            tblicenceval.Text = "27/08/2058";
            tbcard.Text = "1234123412341234";
            tbcardval.Text = "02/05/2018";
            tbfiscal.Text = "123456789";
            tbbirthday.Text = "27/05/1983";
            tbusername.Text = "pedro";
            tbpassword.Text = "pedro";
            tbemail.Text = "pedro@urbanmshare.pt";
            tbphone.Text = "914784521";
        }
    }
}
