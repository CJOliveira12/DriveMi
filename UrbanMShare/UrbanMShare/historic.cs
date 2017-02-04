using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace UrbanMShare
{
    public partial class historic : UserControl
    {
        private static historic _instance;
        SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
        SqlDataReader reader;
        private string fbrand = string.Empty;
        private string fmodel = string.Empty;

        public static historic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new historic();
                return _instance;
            }
        }

        public historic()
        {
            createform();
        }

        public void createform()
        {
            while (Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();

            sqlconn.Open();
            SqlCommand qry1 = new SqlCommand(@"SELECT 
                                            [0502Service].IDservice, 
                                            [0502Service].IDclient,
                                            [0501Vehicle].IDvehicle, 
                                            [0302CarBrand].Brand, 
                                            [0301CarModel].Model, 
                                            [0502Service].PickUpDate, 
                                            [0502Service].DeliveryDate, 
                                            [0502Service].RentPrice, 
                                            [0602Evaluation].EvTopic1, 
                                            [0602Evaluation].EvTopic2, 
                                            [0602Evaluation].EvTopic3
                                            FROM ((([0502Service] INNER JOIN [0501Vehicle] ON 
                                            [0502Service].IDvehicle = [0501Vehicle].IDvehicle) INNER JOIN [0301CarModel] ON 
                                            [0501Vehicle].IDmodel = [0301CarModel].IDmodel) INNER JOIN [0302CarBrand] ON 
                                            [0301CarModel].IDbrande = [0302CarBrand].IDbrand) LEFT JOIN [0602Evaluation] ON 
                                            [0502Service].IDservice = [0602Evaluation].IDservice
                                            WHERE 
                                            [0502Service].DeliveryDate Is Not Null and 
                                            [0502Service].IDclient = @Client" + fbrand + fmodel + " ORDER BY [0502Service].IDservice", sqlconn);
            
            qry1.Parameters.Add("@Client", SqlDbType.VarChar).Value = Program.idclient;

            reader = qry1.ExecuteReader();

            int nrveicles = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nrveicles += 1;
                }
                reader.Close();
            }
            else
            {
                Label NoResults;
                NoResults = new Label();
                NoResults.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NoResults.ForeColor = SystemColors.InactiveCaptionText;
                NoResults.ImageAlign = ContentAlignment.MiddleLeft;
                NoResults.Location = new Point(10, 90);
                NoResults.Name = "NoResults";
                NoResults.Size = new Size(197, 18);
                NoResults.TabIndex = 14;
                NoResults.Text = "Sem histórico a apresentar.";
                Controls.Add(NoResults);
                reader.Close();
                sqlconn.Close();
                return;
            }

            int n = nrveicles;
            int position = 80;

            Label[] tbrentid;
            Label[] tbbrand;
            Label[] tbmodel;
            Label[] tbrentdate;
            Label[] tbrentduration;
            Label[] tbpayde;
            PictureBox[] s1;
            PictureBox[] s2;
            PictureBox[] s3;
            PictureBox[] s4;
            PictureBox[] s5;
            Button[] btevaluate;
            Label[] lbdivider;

            tbrentid = new Label[n];
            tbbrand = new Label[n];
            tbmodel = new Label[n];
            tbrentdate = new Label[n];
            tbrentduration = new Label[n];
            tbpayde = new Label[n];
            s1 = new PictureBox[n];
            s2 = new PictureBox[n];
            s3 = new PictureBox[n];
            s4 = new PictureBox[n];
            s5 = new PictureBox[n];
            btevaluate = new Button[n];
            lbdivider = new Label[n];
            reader = qry1.ExecuteReader();

            int i = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbrentid[i] = new Label();
                    tbrentid[i].Name = "tbrentid";
                    tbrentid[i].TextAlign = ContentAlignment.TopCenter;
                    tbrentid[i].Text = reader["IDservice"].ToString();
                    tbrentid[i].Width = 76;
                    tbrentid[i].Location = new Point(10, position + 5);
                    Controls.Add(tbrentid[i]);

                    tbbrand[i] = new Label();
                    tbbrand[i].Name = "tbbrand";
                    tbbrand[i].TextAlign = ContentAlignment.TopCenter;
                    tbbrand[i].Text = reader["Brand"].ToString();
                    tbbrand[i].Width = 117;
                    tbbrand[i].Location = new Point(96, position + 5);
                    Controls.Add(tbbrand[i]);

                    tbmodel[i] = new Label();
                    tbmodel[i].Name = "tbmodel";
                    tbmodel[i].TextAlign = ContentAlignment.TopCenter;
                    tbmodel[i].Text = reader["Model"].ToString();
                    tbmodel[i].Width = 117;
                    tbmodel[i].Location = new Point(222, position + 5);
                    Controls.Add(tbmodel[i]);

                    tbrentdate[i] = new Label();
                    tbrentdate[i].Name = "tbrentdate";
                    tbrentdate[i].TextAlign = ContentAlignment.TopCenter;
                    DateTime pickup;
                    DateTime.TryParse(reader["PickUpDate"].ToString(), out pickup);
                    tbrentdate[i].Text = pickup.ToString("dd-MM-yyyy");
                    tbrentdate[i].Width = 107;
                    tbrentdate[i].Location = new Point(348, position + 5);
                    Controls.Add(tbrentdate[i]);

                    tbrentduration[i] = new Label();
                    tbrentduration[i].Name = "tbrentduration";
                    tbrentduration[i].TextAlign = ContentAlignment.TopCenter;
                    DateTime deldate;
                    DateTime.TryParse(reader["DeliveryDate"].ToString(), out deldate);
                    tbrentduration[i].Text = Math.Round(Convert.ToDouble((deldate - pickup).TotalHours), 1).ToString() + " horas";
                    tbrentduration[i].Width = 77;
                    tbrentduration[i].Location = new Point(464, position + 5);
                    Controls.Add(tbrentduration[i]);

                    tbpayde[i] = new Label();
                    tbpayde[i].Name = "tbpayde";
                    tbpayde[i].TextAlign = ContentAlignment.TopCenter;
                    tbpayde[i].Text = reader["RentPrice"].ToString() + " €";
                    tbpayde[i].Width = 77;
                    tbpayde[i].Location = new Point(550, position + 5);
                    Controls.Add(tbpayde[i]);

                    int stars = 0;

                    if (!DBNull.Value.Equals(reader["EvTopic1"]))
                    {                        
                        stars = (Convert.ToInt32(reader["EvTopic1"]) + Convert.ToInt32(reader["EvTopic2"]) + Convert.ToInt32(reader["EvTopic3"])) / 3;
                    }                    

                    if (stars == 0)
                    {
                        btevaluate[i] = new Button();
                        btevaluate[i].Name = "btevaluate";
                        btevaluate[i].Text = "Avaliar Serviço";
                        btevaluate[i].Width = 130;
                        btevaluate[i].Tag = Convert.ToInt32(reader["IDservice"]);
                        btevaluate[i].Click += new EventHandler(btevaluate_Click);
                        btevaluate[i].Location = new Point(636, position);
                        btevaluate[i].FlatAppearance.BorderSize = 1;
                        btevaluate[i].FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
                        btevaluate[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
                        btevaluate[i].FlatStyle = FlatStyle.Flat;
                        btevaluate[i].BackColor = Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(103)))));
                        btevaluate[i].ForeColor = SystemColors.ControlLightLight;
                        Controls.Add(btevaluate[i]);
                    }else
                    {
                        s1[i] = new PictureBox();
                        s1[i].Name = "s1";
                        if (stars > 0)
                        {
                            s1[i].Image = global::UrbanMShare.Properties.Resources.s1;
                        }
                        else
                        {
                            s1[i].Image = global::UrbanMShare.Properties.Resources.s2;
                        }
                        s1[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        s1[i].Size = new Size(20, 20);
                        s1[i].Location = new Point(636, position);
                        Controls.Add(s1[i]);

                        s2[i] = new PictureBox();
                        s2[i].Name = "s2";
                        if (stars > 2)
                        {
                            s2[i].Image = global::UrbanMShare.Properties.Resources.s1;
                        }
                        else
                        {
                            s2[i].Image = global::UrbanMShare.Properties.Resources.s2;
                        }
                        s2[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        s2[i].Size = new Size(20, 20);
                        s2[i].Location = new Point(666, position);
                        Controls.Add(s2[i]);

                        s3[i] = new PictureBox();
                        s3[i].Name = "s3";
                        if (stars > 4)
                        {
                            s3[i].Image = global::UrbanMShare.Properties.Resources.s1;
                        }
                        else
                        {
                            s3[i].Image = global::UrbanMShare.Properties.Resources.s2;
                        }
                        s3[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        s3[i].Size = new Size(20, 20);
                        s3[i].Location = new Point(694, position);
                        Controls.Add(s3[i]);

                        s4[i] = new PictureBox();
                        s4[i].Name = "s4";
                        if (stars > 6)
                        {
                            s4[i].Image = global::UrbanMShare.Properties.Resources.s1;
                        }
                        else
                        {
                            s4[i].Image = global::UrbanMShare.Properties.Resources.s2;
                        }
                        s4[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        s4[i].Size = new Size(20, 20);
                        s4[i].Location = new Point(723, position);
                        Controls.Add(s4[i]);

                        s5[i] = new PictureBox();
                        s5[i].Name = "s5";
                        if (stars > 8)
                        {
                            s5[i].Image = global::UrbanMShare.Properties.Resources.s1;
                        }
                        else
                        {
                            s5[i].Image = global::UrbanMShare.Properties.Resources.s2;
                        }
                        s5[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        s5[i].Size = new Size(20, 20);
                        s5[i].Location = new Point(753, position);
                        Controls.Add(s4[i]);
                    }

                    lbdivider[i] = new Label();
                    lbdivider[i].Name = "lbdivider";
                    lbdivider[i].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    lbdivider[i].Location = new Point(5, position + 28);
                    lbdivider[i].Name = "divider";
                    lbdivider[i].Size = new Size(765, 2);
                    lbdivider[i].TabIndex = 0;
                    lbdivider[i].Text = "";
                    Controls.Add(lbdivider[i]);

                    position += 35;
                    i += 1;
                }

                reader.Close();

                reader = qry1.ExecuteReader();
                cbbrand.Items.Add("Todas");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cbbrand.Items.Add(reader["Brand"].ToString());
                    }
                }
                reader.Close();

                reader = qry1.ExecuteReader();
                cbmodel.Items.Add("Todos");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cbmodel.Items.Add(reader["Model"].ToString());
                    }
                }
                reader.Close();

                sqlconn.Close();
            }
            
    }

        private void btevaluate_Click(object sender, EventArgs e)
        {
            Program.idservice = Convert.ToInt32((sender as Button).Tag);
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.AutoScroll = false;
            panel1.Controls.Add(evaluation.Instance);
            evaluation.Instance.Dock = DockStyle.Fill;
            evaluation.Instance.BringToFront();
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            fbrand = string.Empty;
            fmodel = string.Empty;
            createform();
        }

        private void cbbrand_DropDownClosed(object sender, EventArgs e)
        {
            if (cbbrand.SelectedItem.ToString() == "Todas")
            {
                fbrand = string.Empty;
            }
            else
            {
                fbrand = " and [0302CarBrand].Brand='" + cbbrand.SelectedItem.ToString() + "'";
            }

            createform();
        }

        private void cbmodel_DropDownClosed(object sender, EventArgs e)
        {
            if (cbmodel.SelectedItem.ToString() == "Todos")
            {
                fmodel = string.Empty;
            }
            else
            {
                fmodel = " and [0301CarModel].Model='" + cbmodel.SelectedItem.ToString() + "'";
            }
            createform();
        }

        private void btimport_Click(object sender, EventArgs e)
        {
            string xmlPath;

            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                xmlPath = FD.FileName;
                FileInfo File = new FileInfo(FD.FileName);
            }
            else
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlNodeList nodeservice = doc.GetElementsByTagName("service");
            XmlNodeList nodefactor1 = doc.GetElementsByTagName("factor1");
            XmlNodeList nodefactor2 = doc.GetElementsByTagName("factor2");
            XmlNodeList nodefactor3 = doc.GetElementsByTagName("factor3");

            int n = nodeservice.Count;
            int[,] evaluation = new int[n, 4];

            for(int i = 0; i < n; i++)
            {            
                try
                {
                    evaluation[i, 0] = Convert.ToInt32(nodeservice[i].InnerXml);
                    evaluation[i, 1] = Convert.ToInt32(nodefactor1[i].InnerXml);
                    evaluation[i, 2] = Convert.ToInt32(nodefactor2[i].InnerXml);
                    evaluation[i, 3] = Convert.ToInt32(nodefactor3[i].InnerXml);
                }
                catch (Exception)
                {
                    MessageBox.Show("Os dados constantes no ficheiro de importação não são válidos.", "DriveMi - UrbanMShare");
                    return;
                }

                if (evaluation[i, 1] < 1 || evaluation[i, 1] > 9 || evaluation[i, 2] < 1 || evaluation[i, 2] > 9 || evaluation[i, 3] < 1 || evaluation[i, 3] > 9)
                {
                    MessageBox.Show("O valores de avaliação do ficheiro importado não estão dentro dos parâmetros estipulados.", "DriveMi - UrbanMShare");
                    return;
                }                
            }

            sqlconn.Open();

            for (int i = 0; i < n; i++)
            {
                SqlCommand qry1 = new SqlCommand(@"INSERT INTO [0602Evaluation] (
                                                [IDservice], [EvTopic1], [EvTopic2], [EvTopic3])
                                                VALUES ( 
                                                @IDservice, @factor1, @factor2, @factor3)", sqlconn);

                qry1.Parameters.Add("@IDservice", SqlDbType.VarChar).Value = evaluation[i, 0];
                qry1.Parameters.Add("@factor1", SqlDbType.VarChar).Value = evaluation[i, 1];
                qry1.Parameters.Add("@factor2", SqlDbType.VarChar).Value = evaluation[i, 2];
                qry1.Parameters.Add("@factor3", SqlDbType.VarChar).Value = evaluation[i, 3];

                qry1.ExecuteScalar();

                SqlCommand qry2 = new SqlCommand(@"SELECT [0502Service].IDvehicle
                                                FROM [0602Evaluation] INNER JOIN [0502Service] ON 
                                                [0602Evaluation].IDservice = [0502Service].IDservice
                                                WHERE [0602Evaluation].IDservice=@IDservice;", sqlconn);

                qry2.Parameters.Add("@IDservice", SqlDbType.VarChar).Value = evaluation[i, 0];

                int IdVehicle = Convert.ToInt32(qry2.ExecuteScalar());

                SqlCommand qry3 = new SqlCommand(@"UPDATE [0601EvaluationAvg]
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
           }

           sqlconn.Close();
           MessageBox.Show(n + " avaliações importadas com sucesso." + Environment.NewLine + "A UrbanMShare agradece a sua avaliação.", "DriveMi - UrbanMShare");
           createform();
        }
    }
}
