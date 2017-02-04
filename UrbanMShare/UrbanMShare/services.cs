using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace UrbanMShare
{
    public partial class services : UserControl
    {
        SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
        SqlDataReader reader;
        private string fbrand = string.Empty;
        private string fcolour = string.Empty;
        private string ffueltype = string.Empty;
        private string flocalizaton = string.Empty;
        private string favailability = string.Empty;
        private string forder = " Order By [0302CarBrand].Brand";
        private string cbbrandtext = "Todas";
        private string cbcolourtext = "Todas";
        private string cbfueltypetext = "Todos";
        private string cblocalizatontext = "Todas";
        private string cbavailabilitytext = "Todas";
        private string cbordertext = "Marca";
        private static services _instance;

        public static services Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new services();
                return _instance;
            }
        }

        public services()
        {
            InitializeComponent();
        }

        private void btcalculate_Click(object sender, EventArgs e)
        {
            Program.idrent = Convert.ToInt32((sender as Button).Tag);
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.AutoScroll = false;
            panel1.Controls.Add(rent.Instance);
            rent.Instance.Dock = DockStyle.Fill;
            rent.Instance.BringToFront();
            rent.Instance.fillform();
        }

        private void services_Load(object sender, EventArgs e)
        {  
            while (Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            createform();

            sqlconn.Open();
            SqlCommand qry3 = new SqlCommand("SELECT [Brand] FROM [0302CarBrand] ORDER BY [Brand] ;", sqlconn);
            reader = qry3.ExecuteReader();
            cbbrand.Items.Add("Todas");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbbrand.Items.Add(reader["Brand"].ToString());
                }
            }
            reader.Close();

            SqlCommand qry4 = new SqlCommand("SELECT [DataValue] FROM [01BaseData] Where [DataType] = 2 Order by [DataValue] ;", sqlconn);
            reader = qry4.ExecuteReader();
            cbcolour.Items.Add("Todas");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbcolour.Items.Add(reader["DataValue"].ToString());
                }
            }
            reader.Close();
            
            SqlCommand qry5 = new SqlCommand("SELECT [DataValue] FROM [01BaseData] Where [DataType] = 8 Order by [DataValue] ;", sqlconn);
               reader = qry5.ExecuteReader();
               cbfueltype.Items.Add("Todas");
               if (reader.HasRows)
               {
                   while (reader.Read())
                   {
                       cbfueltype.Items.Add(reader["DataValue"].ToString());
                   }
               }
               reader.Close();
               
            SqlCommand qry6 = new SqlCommand("SELECT Distinct [Locality] FROM [0203Locality] ORDER BY [Locality];", sqlconn);
            reader = qry6.ExecuteReader();
            cblocalization.Items.Add("Todas");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cblocalization.Items.Add(reader["Locality"].ToString());
                }
            }
            reader.Close();

            sqlconn.Close();

            cbbrand.Text=cbbrandtext;
            cbcolour.Text=cbcolourtext;
            cbfueltype.Text=cbfueltypetext;
            cblocalization.Text = cblocalizatontext;
            cbavailability.Text=cbavailabilitytext;
            cborder.Text = cbordertext;

            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.AutoScroll = false;
            panel1.AutoScroll = true;
        }

        public void createform()
        {
            if (Program.ahp_order!= String.Empty && Program.qrystate == 1)
            {
                forder = Program.ahp_order;
                Program.qrystate = 0;
            }
                            
            sqlconn.Open();
            SqlCommand qry1 = new SqlCommand(@"SELECT 
                                            [0501Vehicle].IDvehicle, [0302CarBrand].Brand, [0301CarModel].Model,[0301CarModel].pic, 
                                            [0501Vehicle].VehicleYear, [0501Vehicle].Mileage, [0501Vehicle].CurrentState, 
                                            [0501Vehicle].PriceHour, [01BaseData].DataValue AS colour, [01BaseData_1].DataValue AS FuelType, 
                                            [0203Locality].Locality,[0601EvaluationAvg].EvTopic1,[0601EvaluationAvg].EvTopic2, [0601EvaluationAvg].EvTopic3 
                                            FROM 
                                            ([0203Locality] INNER JOIN (
                                            [01BaseData] AS [01BaseData_1] INNER JOIN (
                                            [01BaseData] INNER JOIN (
                                            [0302CarBrand] INNER JOIN (
                                            [0501Vehicle] INNER JOIN [0301CarModel] ON 
                                            [0501Vehicle].IDmodel = [0301CarModel].IDmodel) ON 
                                            [0302CarBrand].IDbrand = [0301CarModel].IDbrande) ON 
                                            [01BaseData].IDbaseData = [0501Vehicle].IDcolor) ON 
                                            [01BaseData_1].IDbaseData = [0501Vehicle].FuelType) ON 
                                            [0203Locality].IDlocality = [0501Vehicle].Localization) INNER JOIN 
                                            [0601EvaluationAvg] ON 
                                            [0501Vehicle].IDvehicle = [0601EvaluationAvg].IDvehicle
                                            WHERE
											[0501Vehicle].IDvehicle is not null" + fbrand + fcolour + ffueltype + flocalizaton + favailability + forder, sqlconn);

            reader = qry1.ExecuteReader();

            int nrveicles = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nrveicles += 1;
                }
            }else
            {
                Label NoResults;
                NoResults = new Label();
                NoResults.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NoResults.ForeColor = SystemColors.InactiveCaptionText;
                NoResults.ImageAlign = ContentAlignment.MiddleLeft;
                NoResults.Location = new Point(5, 65);
                NoResults.Name = "NoResults";
                NoResults.Size = new Size(197, 18);
                NoResults.TabIndex = 14;
                NoResults.Text = "Sem resultados a apresentar.";
                Controls.Add(NoResults);
                reader.Close();
                sqlconn.Close();
                return;
            }

            reader.Close();

            PictureBox[] pic;
            PictureBox[] p1;
            PictureBox[] p2;
            PictureBox[] p3;
            PictureBox[] p4;
            PictureBox[] p5;
            PictureBox[] c1;
            PictureBox[] c2;
            PictureBox[] c3;
            PictureBox[] c4;
            PictureBox[] c5;
            PictureBox[] s1;
            PictureBox[] s2;
            PictureBox[] s3;
            PictureBox[] s4;
            PictureBox[] s5;
            Label[] tbbrand;
            Label[] tbmodel;
            Label[] tbcolour;
            Label[] tbac;
            Label[] tbprice;
            Label[] tbfuel;
            Label[] tbmileage;
            Label[] tblocation;
            Label[] lbbrand;
            Label[] lbmodel;
            Label[] lbcolour;
            Label[] lbac;
            Label[] lbprice;
            Label[] lbfuel;
            Label[] lbmilage;
            Label[] lblocation;
            Label[] lbperf;
            Label[] lbfuelcon;
            Label[] lbsecurity;
            Button[] btcalculate;
            Label[] lbdivider;

            int n = nrveicles;
            int position = 60;

            pic = new PictureBox[n];
            p1 = new PictureBox[n];
            p2 = new PictureBox[n];
            p3 = new PictureBox[n];
            p4 = new PictureBox[n];
            p5 = new PictureBox[n];
            c1 = new PictureBox[n];
            c2 = new PictureBox[n];
            c3 = new PictureBox[n];
            c4 = new PictureBox[n];
            c5 = new PictureBox[n];
            s1 = new PictureBox[n];
            s2 = new PictureBox[n];
            s3 = new PictureBox[n];
            s4 = new PictureBox[n];
            s5 = new PictureBox[n];
            tbbrand = new Label[n];
            tbmodel = new Label[n];
            tbcolour = new Label[n];
            tbac = new Label[n];
            tbprice = new Label[n];
            tbfuel = new Label[n];
            tbmileage = new Label[n];
            tblocation = new Label[n];
            lbbrand = new Label[n];
            lbmodel = new Label[n];
            lbcolour = new Label[n];
            lbac = new Label[n];
            lbprice = new Label[n];
            lbfuel = new Label[n];
            lbmilage = new Label[n];
            lblocation = new Label[n];
            lbperf = new Label[n];
            lbfuelcon = new Label[n];
            lbsecurity = new Label[n];
            btcalculate = new Button[n];
            lbdivider = new Label[n];
            reader = qry1.ExecuteReader();

            int i = 0;
            byte[] outbyte = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pic[i] = new PictureBox();
                    pic[i].Name = "pic";
                    pic[i].Size = new Size(90, 90);
                    pic[i].Location = new Point(15, position + 10);
                    pic[i].BorderStyle = BorderStyle.FixedSingle;
                    outbyte = (byte[])reader["Pic"];
                    pic[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    using (MemoryStream ms = new MemoryStream(outbyte))
                    {
                        Bitmap image = new Bitmap(ms);
                        pic[i].Image = image;
                    }
                    Controls.Add(pic[i]);

                    tbbrand[i] = new Label();
                    tbbrand[i].Name = "tbbrand";
                    tbbrand[i].Text = reader["Brand"].ToString();
                    tbbrand[i].Width = 150;
                    tbbrand[i].Location = new Point(172, position + 10);
                    Controls.Add(tbbrand[i]);

                    tbmodel[i] = new Label();
                    tbmodel[i].Name = "tbmodel";
                    tbmodel[i].Text = reader["Model"].ToString();
                    tbmodel[i].Width = 150;
                    tbmodel[i].Location = new Point(172, position + 45);
                    Controls.Add(tbmodel[i]);

                    tbcolour[i] = new Label();
                    tbcolour[i].Name = "tbcolour";
                    tbcolour[i].Text = reader["Colour"].ToString();
                    tbcolour[i].Width = 150;
                    tbcolour[i].Location = new Point(172, position + 80);
                    Controls.Add(tbcolour[i]);

                    tbac[i] = new Label();
                    tbac[i].Name = "tbac";
                    if (reader["CurrentState"].ToString() == "True")
                    {
                        tbac[i].Text = "Sim";
                    }
                    else
                    {
                        tbac[i].Text = "Não";
                    }
                    tbac[i].Width = 150;
                    tbac[i].Location = new Point(172, position + 115);
                    Controls.Add(tbac[i]);

                    tbprice[i] = new Label();
                    tbprice[i].Name = "tbprice";
                    tbprice[i].Text = reader["PriceHour"].ToString() + " €/h";
                    tbprice[i].Width = 90;
                    tbprice[i].Location = new Point(412, position + 10);
                    Controls.Add(tbprice[i]);

                    tbfuel[i] = new Label();
                    tbfuel[i].Name = "tbfuel";
                    tbfuel[i].Text = reader["FuelType"].ToString();
                    tbfuel[i].Width = 90;
                    tbfuel[i].Location = new Point(412, position + 45);
                    Controls.Add(tbfuel[i]);

                    tbmileage[i] = new Label();
                    tbmileage[i].Name = "tbmileage";
                    tbmileage[i].Text = reader["mileage"].ToString() + " kms";
                    tbmileage[i].Width = 90;
                    tbmileage[i].Location = new Point(412, position + 80);
                    Controls.Add(tbmileage[i]);

                    tblocation[i] = new Label();
                    tblocation[i].Name = "tblocation";
                    tblocation[i].Text = reader["Locality"].ToString();
                    tblocation[i].Width = 335;
                    tblocation[i].Location = new Point(412, position + 115);
                    Controls.Add(tblocation[i]);

                    lbbrand[i] = new Label();
                    lbbrand[i].Name = "lbbrand";
                    lbbrand[i].Text = "Marca:";
                    lbbrand[i].Width = 150;
                    lbbrand[i].Location = new Point(117, position + 10);
                    lbbrand[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbbrand[i].ForeColor = SystemColors.InactiveCaptionText;
                    Controls.Add(lbbrand[i]);

                    lbmodel[i] = new Label();
                    lbmodel[i].Name = "lbmodel";
                    lbmodel[i].Text = "Modelo:";
                    lbmodel[i].Width = 150;
                    lbmodel[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbmodel[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbmodel[i].Location = new Point(117, position + 45);
                    Controls.Add(lbmodel[i]);

                    lbcolour[i] = new Label();
                    lbcolour[i].Name = "lbcolour";
                    lbcolour[i].Text = "Cor:";
                    lbcolour[i].Width = 150;
                    lbcolour[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbcolour[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbcolour[i].Location = new Point(117, position + 80);
                    Controls.Add(lbcolour[i]);

                    lbac[i] = new Label();
                    lbac[i].Name = "lbac";
                    lbac[i].Text = "Ar Cond:";
                    lbac[i].Width = 150;
                    lbac[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbac[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbac[i].Location = new Point(117, position + 115);
                    Controls.Add(lbac[i]);

                    lbprice[i] = new Label();
                    lbprice[i].Name = "lbprice";
                    lbprice[i].Text = "Preço/Hora:";
                    lbprice[i].Width = 90;
                    lbprice[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbprice[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbprice[i].Location = new Point(328, position + 10);
                    Controls.Add(lbprice[i]);

                    lbfuel[i] = new Label();
                    lbfuel[i].Name = "lbfuel";
                    lbfuel[i].Text = "Combustivel:";
                    lbfuel[i].Width = 90;
                    lbfuel[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbfuel[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbfuel[i].Location = new Point(328, position + 45);
                    Controls.Add(lbfuel[i]);

                    lbmilage[i] = new Label();
                    lbmilage[i].Name = "lbmilage";
                    lbmilage[i].Text = "Kms:";
                    lbmilage[i].Width = 90;
                    lbmilage[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbmilage[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbmilage[i].Location = new Point(328, position + 80);
                    Controls.Add(lbmilage[i]);

                    lblocation[i] = new Label();
                    lblocation[i].Name = "lblocation";
                    lblocation[i].Text = "Localização:";
                    lblocation[i].Width = 335;
                    lblocation[i].ForeColor = SystemColors.InactiveCaptionText;
                    lblocation[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lblocation[i].Location = new Point(328, position + 115);
                    Controls.Add(lblocation[i]);

                    lbperf[i] = new Label();
                    lbperf[i].Name = "lbperf";
                    lbperf[i].Text = "Performance:";
                    lbperf[i].Width = 90;
                    lbperf[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbperf[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbperf[i].Location = new Point(537, position + 10);
                    Controls.Add(lbperf[i]);

                    lbfuelcon[i] = new Label();
                    lbfuelcon[i].Name = "lbfuelcon";
                    lbfuelcon[i].Text = "Segurança:";
                    lbfuelcon[i].Width = 90;
                    lbfuelcon[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbfuelcon[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbfuelcon[i].Location = new Point(537, position + 45);
                    Controls.Add(lbfuelcon[i]);

                    lbsecurity[i] = new Label();
                    lbsecurity[i].Name = "lbsecurity";
                    lbsecurity[i].Text = "Conforto:";
                    lbsecurity[i].Width = 90;
                    lbsecurity[i].ForeColor = SystemColors.InactiveCaptionText;
                    lbsecurity[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbsecurity[i].Location = new Point(537, position + 80);
                    Controls.Add(lbsecurity[i]);

                    btcalculate[i] = new Button();
                    btcalculate[i].Name = "btcalculate";
                    btcalculate[i].Text = "Alugar";
                    btcalculate[i].Width = 90;
                    btcalculate[i].Tag = Convert.ToInt32(reader["IDvehicle"]);
                    btcalculate[i].Click += new EventHandler(btcalculate_Click);
                    btcalculate[i].Location = new Point(15, position + 110);
                    btcalculate[i].FlatAppearance.BorderSize = 1;
                    btcalculate[i].FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
                    btcalculate[i].FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
                    btcalculate[i].FlatStyle = FlatStyle.Flat;
                    btcalculate[i].BackColor = Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(103)))));
                    btcalculate[i].ForeColor = SystemColors.ControlLightLight;
                    Controls.Add(btcalculate[i]);

                    p1[i] = new PictureBox();
                    p1[i].Name = "p1";
                    if (Convert.ToInt32(reader["EvTopic1"]) > 0)
                    {
                        p1[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p1[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    p1[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p1[i].Size = new Size(20, 20);
                    p1[i].Location = new Point(631, position + 5);
                    Controls.Add(p1[i]);

                    p2[i] = new PictureBox();
                    p2[i].Name = "p2";
                    if (Convert.ToInt32(reader["EvTopic1"]) > 2)
                    {
                        p2[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p2[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    p2[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p2[i].Size = new Size(20, 20);
                    p2[i].Location = new Point(657, position + 5);
                    Controls.Add(p2[i]);

                    p3[i] = new PictureBox();
                    p3[i].Name = "p3";
                    if (Convert.ToInt32(reader["EvTopic1"]) > 4)
                    {
                        p3[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p3[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    p3[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p3[i].Size = new Size(20, 20);
                    p3[i].Location = new Point(683, position + 5);
                    Controls.Add(p3[i]);

                    p4[i] = new PictureBox();
                    p4[i].Name = "p4";
                    if (Convert.ToInt32(reader["EvTopic1"]) > 6)
                    {
                        p4[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p4[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    p4[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p4[i].Size = new Size(20, 20);
                    p4[i].Location = new Point(709, position + 5);
                    Controls.Add(p4[i]);

                    p5[i] = new PictureBox();
                    p5[i].Name = "p5";
                    if (Convert.ToInt32(reader["EvTopic1"]) > 8)
                    {
                        p5[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p5[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    p5[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    p5[i].Size = new Size(20, 20);
                    p5[i].Location = new Point(736, position + 5);
                    Controls.Add(p5[i]);

                    c1[i] = new PictureBox();
                    c1[i].Name = "c1";
                    if (Convert.ToInt32(reader["EvTopic2"]) > 0)
                    {
                        c1[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c1[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    c1[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    c1[i].Size = new Size(20, 20);
                    c1[i].Location = new Point(631, position + 40);
                    Controls.Add(c1[i]);

                    c2[i] = new PictureBox();
                    c2[i].Name = "c2";
                    if (Convert.ToInt32(reader["EvTopic2"]) > 2)
                    {
                        c2[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c2[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    c2[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    c2[i].Size = new Size(20, 20);
                    c2[i].Location = new Point(657, position + 40);
                    Controls.Add(c2[i]);

                    c3[i] = new PictureBox();
                    c3[i].Name = "c3";
                    if (Convert.ToInt32(reader["EvTopic2"]) > 4)
                    {
                        c3[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c3[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    c3[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    c3[i].Size = new Size(20, 20);
                    c3[i].Location = new Point(683, position + 40);
                    Controls.Add(c3[i]);

                    c4[i] = new PictureBox();
                    c4[i].Name = "c4";
                    if (Convert.ToInt32(reader["EvTopic2"]) > 6)
                    {
                        c4[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c4[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    c4[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    c4[i].Size = new Size(20, 20);
                    c4[i].Location = new Point(709, position + 40);
                    Controls.Add(c4[i]);

                    c5[i] = new PictureBox();
                    c5[i].Name = "c5";
                    if (Convert.ToInt32(reader["EvTopic2"]) > 8)
                    {
                        c5[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c5[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    c5[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    c5[i].Size = new Size(20, 20);
                    c5[i].Location = new Point(736, position + 40);
                    Controls.Add(c5[i]);

                    s1[i] = new PictureBox();
                    s1[i].Name = "s1";
                    if (Convert.ToInt32(reader["EvTopic3"]) > 0)
                    {
                        s1[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s1[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    s1[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    s1[i].Size = new Size(20, 20);
                    s1[i].Location = new Point(631, position + 75);
                    Controls.Add(s1[i]);

                    s2[i] = new PictureBox();
                    s2[i].Name = "s2";
                    if (Convert.ToInt32(reader["EvTopic3"]) > 2)
                    {
                        s2[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s2[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    s2[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    s2[i].Size = new Size(20, 20);
                    s2[i].Location = new Point(657, position + 75);
                    Controls.Add(s2[i]);

                    s3[i] = new PictureBox();
                    s3[i].Name = "s3";
                    if (Convert.ToInt32(reader["EvTopic3"]) > 4)
                    {
                        s3[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s3[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    s3[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    s3[i].Size = new Size(20, 20);
                    s3[i].Location = new Point(683, position + 75);
                    Controls.Add(s3[i]);

                    s4[i] = new PictureBox();
                    s4[i].Name = "s4";
                    if (Convert.ToInt32(reader["EvTopic3"]) > 6)
                    {
                        s4[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s4[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    s4[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    s4[i].Size = new Size(20, 20);
                    s4[i].Location = new Point(709, position + 75);
                    Controls.Add(s4[i]);

                    s5[i] = new PictureBox();
                    s5[i].Name = "s5";
                    if (Convert.ToInt32(reader["EvTopic3"]) > 8)
                    {
                        s5[i].Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s5[i].Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    s5[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    s5[i].Size = new Size(20, 20);
                    s5[i].Location = new Point(736, position + 75);
                    Controls.Add(s5[i]);

                    lbdivider[i] = new Label();
                    lbdivider[i].Name = "lbdivider";
                    lbdivider[i].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                    lbdivider[i].Location = new Point(5, position + 150);
                    lbdivider[i].Name = "divider";
                    lbdivider[i].Size = new Size(765, 2);
                    lbdivider[i].TabIndex = 0;
                    lbdivider[i].Text = "";
                    Controls.Add(lbdivider[i]);

                    position += 150;
                    i += 1;
                }

            }
            reader.Close();
            sqlconn.Close();

        }

        private void cbbrand_DropDownClosed(object sender, EventArgs e)
        {           
            if (cbbrand.SelectedItem.ToString() == "Todas")
            {
                fbrand = string.Empty;
            }else
            {
                fbrand = " and [0302CarBrand].Brand='" + cbbrand.SelectedItem.ToString() + "'";
            }
            cbbrandtext = cbbrand.SelectedItem.ToString();
            services_Load(this, null);
        }

        private void cbcolour_DropDownClosed(object sender, EventArgs e)
        {
            if (cbcolour.SelectedItem.ToString() == "Todas")
            {
                fcolour = string.Empty;
            }
            else
            {
                fcolour = " and [01BaseData].DataValue='" + cbcolour.SelectedItem.ToString() + "'";
            }
            cbcolourtext = cbcolour.SelectedItem.ToString();
            services_Load(this, null);
        }

        private void cbfueltype_DropDownClosed(object sender, EventArgs e)
        {
            if (cbfueltype.SelectedItem.ToString() == "Todas")
            {
                ffueltype = string.Empty;
            }
            else
            {
                ffueltype = " and [01BaseData_1].DataValue='" + cbfueltype.SelectedItem.ToString() + "'";
            }
            cbfueltypetext = cbfueltype.SelectedItem.ToString();
            services_Load(this, null);
        }

        private void cblocalization_DropDownClosed(object sender, EventArgs e)
        {
            if (cblocalization.SelectedItem.ToString() == "Todas")
            {
                flocalizaton = string.Empty;
            }
            else
            {
                flocalizaton = " and [0203Locality].Locality='" + cblocalization.SelectedItem.ToString() + "'";
            }
            cblocalizatontext = cblocalization.SelectedItem.ToString();
            services_Load(this, null);
        }

        private void cbavailability_DropDownClosed(object sender, EventArgs e)
        {
            if (cbavailability.SelectedItem.ToString() == "Todas")
            {
                favailability = string.Empty;
            }
            if (cbavailability.SelectedItem.ToString() == "Sim")
            {
                favailability = " and [0501Vehicle].CurrentState=1";
            }
            if (cbavailability.SelectedItem.ToString() == "Não")
            {
                favailability = " and [0501Vehicle].CurrentState=0";
            }
            cbavailabilitytext = cbavailability.SelectedItem.ToString();
            services_Load(this, null);
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            cleanfilters();
        }

        public void cleanfilters()
        {
        fbrand = string.Empty;
        fcolour = string.Empty;
        ffueltype = string.Empty;
        flocalizaton = string.Empty;
        favailability = string.Empty;
        forder = " Order By [0302CarBrand].Brand";
        cbbrandtext = "Todas";
        cbcolourtext = "Todas";
        cbfueltypetext = "Todos";
        cblocalizatontext = "Todas";
        cbavailabilitytext = "Todas";
        cbordertext = "Marca";
        services_Load(this, null);
    }

        private void cborder_DropDownClosed(object sender, EventArgs e)
        {
            if (cborder.SelectedItem.ToString() == "Marca")
            {
                forder = " Order By [0302CarBrand].Brand";
            }else if (cborder.SelectedItem.ToString() == "Cor")
            {
                forder = " Order By [01BaseData].DataValue";
            }else if (cborder.SelectedItem.ToString() == "Combustivel")
            {
                forder = " Order By [01BaseData_1].DataValue";
            }else if (cborder.SelectedItem.ToString() == "Preço")
            {
                forder = " Order By [0501Vehicle].PriceHour";
            } else if (cborder.SelectedItem.ToString() == "Localização")
            {
                forder = " Order By [0203Locality].Locality";
            }
            else if (cborder.SelectedItem.ToString() == "Por Importância")
            {
                if(Program.ahp_order != String.Empty)
                {
                    forder = Program.ahp_order;
                }else
                {
                    MessageBox.Show("Ainda não efetuou uma pesquisa de importância.", "DriveMi - UrbanMShare");
                    return;
                }                
            }
            cbordertext = cborder.SelectedItem.ToString();
            services_Load(this, null);
        }
    }
}
