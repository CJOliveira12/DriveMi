using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace UrbanMShare
{
    public partial class login : UserControl
    {      
        private static login _instance;

        public static login Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new login();
                return _instance;
            }
        }

        public login()
        {
            InitializeComponent();
        }

        private void btlogin_Click(object sender, EventArgs e)
        {            
            try
            {
                SqlConnection sqlconn = new SqlConnection(Program.strconn_str);

                SqlCommand cmd = new SqlCommand(@"SELECT 
                                                [0401Client].IDclient, 
                                                [0401Client].SmallName, 
                                                [00Autentication].Usrname, 
                                                [00Autentication].Usrpass, 
                                                [00Autentication].CurrentState, 
                                                [0203Locality].Locality
                                                FROM (
                                                [00Autentication] INNER JOIN [0401Client] ON 
                                                [00Autentication].IDperson = [0401Client].IDclient) INNER JOIN [0203Locality] 
                                                ON [0401Client].IDlocality = [0203Locality].IDlocality
                                                WHERE [00Autentication].Usrname = @user and [00Autentication].Usrpass = @password;", sqlconn);

                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = tbusername.Text;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = Program.GetMD5(tbpassword.Text);

                sqlconn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader["CurrentState"] ) == 0)
                        {
                            MessageBox.Show("O utilizador encontra-se desativado", "DriveMi - UrbanMShare");
                            this.tbpassword.Text = string.Empty;
                            this.tbusername.Text = string.Empty;
                            return;
                        }
                        
                        Program.smallname = reader["SmallName"].ToString();
                        Program.idclient = Convert.ToInt32(reader["IDclient"]);
                        urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
                        Panel panel1 = (Panel)form1.Controls["panel"];
                        panel1.AutoScroll = true;
                        panel1.AutoScrollMargin = new System.Drawing.Size(20, 490);
                        panel1.AutoScrollMinSize = new System.Drawing.Size(20, 490);
                        panel1.Controls.Add(services.Instance);
                        panel1.Width = 790;
                        panel1.Left = 160;
                        services.Instance.Dock = DockStyle.Fill;
                        services.Instance.BringToFront();
                        this.tbpassword.Text = string.Empty;
                        this.tbusername.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Utilizador ou password incorretos.", "DriveMi - UrbanMShare");
                    this.tbpassword.Text = string.Empty;
                    this.tbusername.Text = string.Empty;
                }
                reader.Close();
                sqlconn.Close();                

            }
            catch (SqlException)
            {
                MessageBox.Show("Erro na ligação à base de dados", "DriveMi - UrbanMShare");
                this.tbpassword.Text = string.Empty;
                this.tbusername.Text = string.Empty;
            }

        }

        private void btnew_Click(object sender, EventArgs e)
        {
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.Controls.Add(newaccount.Instance);
            newaccount.Instance.Dock = DockStyle.Fill;
            newaccount.Instance.BringToFront();
            this.tbpassword.Text = string.Empty;
            this.tbusername.Text = string.Empty;
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
