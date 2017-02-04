using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace UrbanMShare
{
    public partial class evaluation : UserControl
    {
        private static evaluation _instance;

        SqlConnection sqlconn = new SqlConnection(Program.strconn_str);

        int factor1=0;
        int factor2=0;
        int factor3=0;

        public static evaluation Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new evaluation();
                return _instance;
            }
        }

        public evaluation()
        {
            InitializeComponent();
        }
        
        private void btevaluate_Click(object sender, EventArgs e)
        {
            if(factor1 == 0 || factor2 == 0 || factor3 == 0)
            {
                MessageBox.Show("Para submeter a sua avaliação todos os fatores deverão ser avaliados.", "DriveMi - UrbanMShare");
            }else
            {
                sqlconn.Open();
                SqlCommand qry1 = new SqlCommand(@"INSERT INTO [0602Evaluation] (
                                                [IDservice], [EvTopic1], [EvTopic2], [EvTopic3])
                                                VALUES ( 
                                                @IDservice, @factor1, @factor2, @factor3)", sqlconn);

                qry1.Parameters.Add("@IDservice", SqlDbType.VarChar).Value = Program.idservice;
                qry1.Parameters.Add("@factor1", SqlDbType.VarChar).Value = factor1;
                qry1.Parameters.Add("@factor2", SqlDbType.VarChar).Value = factor2;
                qry1.Parameters.Add("@factor3", SqlDbType.VarChar).Value = factor3;

                qry1.ExecuteScalar();

                SqlCommand qry2 = new SqlCommand(@"SELECT [0502Service].IDvehicle
                                                FROM [0602Evaluation] INNER JOIN [0502Service] ON 
                                                [0602Evaluation].IDservice = [0502Service].IDservice
                                                WHERE [0602Evaluation].IDservice=@IDservice;", sqlconn);

                qry2.Parameters.Add("@IDservice", SqlDbType.VarChar).Value = Program.idservice;

                int IdVehicle = Convert.ToInt32(qry2.ExecuteScalar());
         
                SqlCommand qry3= new SqlCommand(@"UPDATE [0601EvaluationAvg]
                                                SET
                                                [0601EvaluationAvg].EvTopic1 = [updavg].EvTopic1,
                                                [0601EvaluationAvg].EvTopic2 = [updavg].EvTopic2,
	                                            [0601EvaluationAvg].EvTopic3 = [updavg].EvTopic3
                                                FROM [0601EvaluationAvg]
                                                INNER JOIN 
	                                                (SELECT 
	                                                [0502Service].IDvehicle, 
	                                                Avg([0602Evaluation].EvTopic1) AS EvTopic1, 
	                                                Avg([0602Evaluation].EvTopic2) AS EvTopic2, 
	                                                Avg([0602Evaluation].EvTopic3) AS EvTopic3
	                                                FROM ([0602Evaluation] INNER JOIN [0502Service] ON 
	                                                [0602Evaluation].IDservice = [0502Service].IDservice) INNER JOIN [0501Vehicle] ON 
	                                                [0502Service].IDvehicle = [0501Vehicle].IDvehicle
	                                                GROUP BY [0502Service].IDvehicle) AS [updavg]
                                                ON [0601EvaluationAvg].IDvehicle = [updavg].IDvehicle
                                                WHERE [updavg].IDvehicle = @IDVehicle", sqlconn);

                qry3.Parameters.Add("@IDVehicle", SqlDbType.VarChar).Value = IdVehicle;
                
                qry3.ExecuteScalar();

                MessageBox.Show("A UrbanMShare agradece a sua avaliação.", "DriveMi - UrbanMShare");

                urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
                Panel panel1 = (Panel)form1.Controls["panel"];
                panel1.AutoScroll = false;
                panel1.Controls.Add(historic.Instance);
                historic.Instance.Dock = DockStyle.Fill;
                historic.Instance.BringToFront();
                historic.Instance.createform();

                sqlconn.Close();
            }
        }

        private void p1_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 1;
        }

        private void p2_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 2;
        }

        private void p3_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 3;
        }

        private void p4_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 4;
        }

        private void p5_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 5;
        }

        private void p6_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 6;
        }

        private void p7_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 7;
        }

        private void p8_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 8;
        }

        private void p9_CheckedChanged(object sender, EventArgs e)
        {
            factor1 = 9;
        }

        private void s1_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 1;
        }

        private void s2_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 2;
        }

        private void s3_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 3;
        }

        private void s4_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 4;
        }

        private void s5_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 5;
        }

        private void s6_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 6;
        }

        private void s7_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 7;
        }

        private void s8_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 8;
        }

        private void s9_CheckedChanged(object sender, EventArgs e)
        {
            factor2 = 9;
        }

        private void c1_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 1;
        }

        private void c2_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 2;
        }

        private void c3_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 3;
        }

        private void c4_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 4;
        }

        private void c5_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 5;
        }

        private void c6_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 6;
        }

        private void c7_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 7;
        }

        private void c8_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 8;
        }

        private void c9_CheckedChanged(object sender, EventArgs e)
        {
            factor3 = 9;
        }

        private void btimport_Click(object sender, EventArgs e)
        {
            string xmlPath;
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                xmlPath = FD.FileName;
                FileInfo File = new FileInfo(FD.FileName);
            }else
            {
                return;
            }

            int xmlservice = 0;
            int xmlfactor1 = 0;
            int xmlfactor2 = 0;
            int xmlfactor3 = 0;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNodeList nodeservice = doc.GetElementsByTagName("service");
            XmlNodeList nodefactor1 = doc.GetElementsByTagName("factor1");
            XmlNodeList nodefactor2 = doc.GetElementsByTagName("factor2");
            XmlNodeList nodefactor3 = doc.GetElementsByTagName("factor3");

            if (nodeservice.Count > 1)
            {
                MessageBox.Show("O ficheiro selecionado possui mais que uma avaliação.", "DriveMi - UrbanMShare");
            }
            else
            {
                try
                {
                xmlservice = Convert.ToInt32(nodeservice[0].InnerXml);
                xmlfactor1 = Convert.ToInt32(nodefactor1[0].InnerXml);
                xmlfactor2 = Convert.ToInt32(nodefactor2[0].InnerXml);
                xmlfactor3 = Convert.ToInt32(nodefactor3[0].InnerXml);
                }
                catch (Exception)
                {
                    MessageBox.Show("Os dados constantes no ficheiro de importação não são válidos.", "DriveMi - UrbanMShare");
                    return;
                }

                if (xmlservice != Program.idservice)
                {
                    MessageBox.Show("O ficheiro selecionado não corresponde à avaliação do serviço aberto.", "DriveMi - UrbanMShare");
                }
                else if (xmlfactor1 < 1 || xmlfactor1 > 9 || xmlfactor2 < 1 || xmlfactor2 > 9 || xmlfactor3 < 1 || xmlfactor3 > 9)
                {
                    MessageBox.Show("O valores de avaliação do ficheiro importado não estão dentro dos parâmetros estipulados.", "DriveMi - UrbanMShare");
                }

                Control[] rbfactor1 = this.Controls.Find("p" + xmlfactor1, true);
                Control[] rbfactor2 = this.Controls.Find("s" + xmlfactor2, true);
                Control[] rbfactor3 = this.Controls.Find("c" + xmlfactor3, true);
                (rbfactor1[0] as RadioButton).PerformClick();
                (rbfactor2[0] as RadioButton).PerformClick();
                (rbfactor3[0] as RadioButton).PerformClick();       
                MessageBox.Show("Avaliação importada com sucesso." + Environment.NewLine + "Por favor valide e grave a sua avaliação.", "DriveMi - UrbanMShare");
            }
        }
    }
}
