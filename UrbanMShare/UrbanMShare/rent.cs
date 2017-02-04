using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace UrbanMShare
{
    public partial class rent : UserControl
    {
        private static rent _instance;
        SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
        SqlDataReader reader;
        byte[] outbyte = null;
        Double renttime=0;
        Double rentprice=0;
        Double simprice = 0;
        int rentaddkms=0;
        int rentkmstodo = 0;
        int parsedValue;
        int idlocality;
        Double rentkmsprice=0;
        DateTime dtrent;
        DateTime dtdelivery;

        public static rent Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new rent();
                return _instance;
            }
        }

        public rent()
        {
            InitializeComponent();

        }

        public void fillform()
        {
            sqlconn.Open();
            SqlCommand qry1 = new SqlCommand(@"SELECT 
                                            [0501Vehicle].IDvehicle, [0302CarBrand].Brand, [0301CarModel].Model, [01BaseData].DataValue AS Colour,
                                            [0501Vehicle].Mileage, [0501Vehicle].AirConditioner, [0501Vehicle].PriceHour, [0501Vehicle].VehicleYear,
                                            [0501Vehicle].CurrentState, [01BaseData_1].DataValue AS FuelType, [0203Locality].IDlocality, [0203Locality].Locality,
                                            [0301CarModel].EngineSize, [0301CarModel].PowerHp, [0301CarModel].PowerNm, [0301CarModel].TrunkLiters,
                                            [0301CarModel].FuelAvg, [0301CarModel].SeatsNr, [0301CarModel].pic, [01BaseData_2].DataValue AS Segment,
                                            [0601EvaluationAvg].EvTopic1, [0601EvaluationAvg].EvTopic2, [0601EvaluationAvg].EvTopic3
                                            FROM([01BaseData] AS[01BaseData_2] INNER JOIN([0203Locality] INNER JOIN([01BaseData] AS[01BaseData_1] INNER JOIN(
                                            [01BaseData] INNER JOIN([0302CarBrand] INNER JOIN([0301CarModel] INNER JOIN[0501Vehicle] ON
                                            [0301CarModel].IDmodel = [0501Vehicle].IDmodel) ON [0302CarBrand].IDbrand = [0301CarModel].IDbrande) ON
                                            [01BaseData].IDbaseData = [0501Vehicle].IDcolor) ON [01BaseData_1].IDbaseData =
                                            [0501Vehicle].FuelType) ON [0203Locality].IDlocality = [0501Vehicle].Localization) ON
                                            [01BaseData_2].IDbaseData = [0301CarModel].Segment) INNER JOIN
                                            [0601EvaluationAvg] ON[0501Vehicle].IDvehicle = [0601EvaluationAvg].IDvehicle
                                            WHERE [0501Vehicle].IDvehicle = @IDrent; ", sqlconn);

            qry1.Parameters.Add("@IDrent", SqlDbType.VarChar).Value = Program.idrent;

            reader = qry1.ExecuteReader();
            
                while (reader.Read())
                {
                    outbyte = (byte[])reader["Pic"];
                    using (MemoryStream ms = new MemoryStream(outbyte))
                    {
                        Bitmap image = new Bitmap(ms);
                        piccar.Image = image;
                    }

                    if (Convert.ToInt32(reader["EvTopic1"]) > 0)
                    {
                        p1.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p1.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic1"]) > 2)
                    {
                        p2.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p2.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic1"]) > 4)
                    {
                        p3.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p3.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic1"]) > 6)
                    {
                        p4.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p4.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic1"]) > 8)
                    {
                        p5.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        p5.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic2"]) > 0)
                    {
                        c1.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c1.Image = global::UrbanMShare.Properties.Resources.s2;
                    }
 
                    if (Convert.ToInt32(reader["EvTopic2"]) > 2)
                    {
                        c2.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c2.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic2"]) > 4)
                    {
                        c3.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c3.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic2"]) > 6)
                    {
                        c4.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c4.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic2"]) > 8)
                    {
                        c5.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        c5.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic3"]) > 0)
                    {
                        s1.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s1.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic3"]) > 2)
                    {
                        s2.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s2.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic3"]) > 4)
                    {
                        s3.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s3.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic3"]) > 6)
                    {
                        s4.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s4.Image = global::UrbanMShare.Properties.Resources.s2;
                    }

                    if (Convert.ToInt32(reader["EvTopic3"]) > 8)
                    {
                        s5.Image = global::UrbanMShare.Properties.Resources.s1;
                    }
                    else
                    {
                        s5.Image = global::UrbanMShare.Properties.Resources.s2;
                    }
                    lbbrand.Text = reader["Brand"].ToString();
                    lbmodel.Text = reader["Model"].ToString();
                    lbyear.Text = reader["VehicleYear"].ToString();
                    lbmileage.Text = reader["Mileage"].ToString() + " kms";
                    lbcolour.Text = reader["Colour"].ToString();
                    lbpower.Text = reader["PowerHp"].ToString() + "cv / " + reader["PowerNm"].ToString() + "nm";
                    lbengsize.Text = reader["EngineSize"].ToString() + "cm3";
                    lbfueltype.Text = reader["FuelType"].ToString();
                    lblocality.Text = reader["Locality"].ToString();
                    lbfuelavg.Text = reader["FuelAvg"].ToString() + " l/100";
                    if (reader["CurrentState"].ToString() == "True")
                    {
                        lbcurrentstate.Text = "Sim";
                    }
                    else
                    {
                        lbcurrentstate.Text = "Não";
                    }
                    lbsegment.Text = reader["Segment"].ToString();
                    lbtrunk.Text = reader["TrunkLiters"].ToString() + " litros";
                    lbsegment.Text = reader["Segment"].ToString();
                    if (reader["AirConditioner"].ToString() == "True")
                    {
                        lbac.Text = "Sim";
                    }
                    else
                    {
                        lbac.Text = "Não";
                    }
                    lbseatsnr.Text = reader["SeatsNr"].ToString() + " lugares";
                    tbpricehour.Text = reader["PriceHour"].ToString() + "€";

                    idlocality = Convert.ToInt32(reader["IDlocality"]);
                    rentprice = Convert.ToDouble(reader["PriceHour"]);
            }

            reader.Close();
            sqlconn.Close();

            daterent.Format = DateTimePickerFormat.Custom;
            daterent.CustomFormat = "dd-MM-yyyy HH:mm";
            daterent.Value = DateTime.Now;
            datedelivery.Format = DateTimePickerFormat.Custom;
            datedelivery.CustomFormat = "dd-MM-yyyy HH:mm";
            datedelivery.Value = DateTime.Now.AddHours(1);
            tbkmsadd.Text = "";
            lbextrakm.Visible = false;
            info.Visible = false;
        }

        private void daterent_ValueChanged(object sender, EventArgs e)
        {
            calclulate();
        }

        private void datedelivery_ValueChanged(object sender, EventArgs e)
        {
            calclulate();
        }

        private void tbkmsadd_Leave(object sender, EventArgs e)
        {
            calclulate();
        }

        private void calclulate()
        {
            if (daterent.Text != "" && datedelivery.Text != "")
            {

            dtrent = daterent.Value;
            dtdelivery = datedelivery.Value;
            renttime = Math.Round(Convert.ToDouble((dtdelivery - dtrent).TotalHours),1);
            tbrenttime.Text = renttime.ToString() + " horas";
            rentkmstodo = Convert.ToInt32((renttime * 40));
            tbkmstodo.Text = rentkmstodo.ToString() + " kms";

            if (tbkmsadd.Text != "" && int.TryParse(tbkmsadd.Text, out parsedValue))
                {
                    rentaddkms = Convert.ToInt32(tbkmsadd.Text);
                }else
                {
                    rentaddkms = 0;
                }

            tbkmstotal.Text = (rentaddkms + rentkmstodo).ToString() + " kms";
            rentkmsprice = rentaddkms * (rentprice * 0.10);

            if (rentaddkms > 0)
            {
                lbextrakm.Text = "+ " + rentkmsprice.ToString() + " €";
                lbextrakm.Visible = true;
                info.Visible = true;
            }
            else
            {
                lbextrakm.Visible = false;
                info.Visible = false;
            }
            simprice = Math.Round((rentprice * renttime + rentkmsprice), 2);
            tbtotal.Text = simprice.ToString() + " €";
            }
        }

        private void info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cobrado 10% do valor do veiculo por cada km adicional.", "DriveMi - UrbanMShare");
        }

        private void btrent_Click(object sender, EventArgs e)
        {
            if(lbcurrentstate.Text == "Não")
            {
                MessageBox.Show("Esta Viatura encontra-se indisponivel de momento.", "DriveMi - UrbanMShare");
                return;
            }
            DialogResult rentconfirm = MessageBox.Show("Confirma o aluguer desta viatura?", "DriveMi - UrbanMShare", MessageBoxButtons.YesNo);
            if (rentconfirm == DialogResult.Yes)
            {
                sqlconn.Open();

                SqlCommand qry2 = new SqlCommand(@"INSERT INTO [0502Service] (
                                                 [IDclient], [IDvehicle], [ReservDateStart], [ReservDateEnd],[PickUpPlace],
                                                 [SimPrice],[CurrentState])
                                                 VALUES (@IDclient, @IDvehicle, @ReservDateStart, @ReservDateEnd, @PickUpPlace, 
                                                 @SimPrice, @CurrentState)", sqlconn);

                qry2.Parameters.Add("@IDclient", SqlDbType.VarChar).Value = Program.idclient;
                qry2.Parameters.Add("@IDvehicle", SqlDbType.VarChar).Value = Program.idrent;
                qry2.Parameters.Add("@ReservDateStart", SqlDbType.VarChar).Value = daterent.Value.ToString("yyyy-MM-dd HH:mm:ss"); ;
                qry2.Parameters.Add("@ReservDateEnd", SqlDbType.VarChar).Value = datedelivery.Value.ToString("yyyy-MM-dd HH:mm:ss"); ;
                qry2.Parameters.Add("@PickUpPlace", SqlDbType.VarChar).Value = idlocality;
                qry2.Parameters.Add("@SimPrice", SqlDbType.VarChar).Value = simprice;
                qry2.Parameters.Add("@CurrentState", SqlDbType.VarChar).Value = 1;

                qry2.ExecuteScalar();

                SqlCommand qry3 = new SqlCommand(@"UPDATE [0501Vehicle] SET [0501Vehicle].CurrentState = 0 WHERE [0501Vehicle].IDvehicle = @IDvehicle;", sqlconn);

                qry3.Parameters.Add("@IDvehicle", SqlDbType.VarChar).Value = Program.idrent;

                qry3.ExecuteScalar();

                sqlconn.Close();

                urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
                Panel panel1 = (Panel)form1.Controls["panel"];
                panel1.AutoScroll = false;
                panel1.Controls.Add(services.Instance);
                services.Instance.Dock = DockStyle.Fill;
                services.Instance.BringToFront();
                services.Instance.cleanfilters();
                services.Instance.createform();

                MessageBox.Show("Viatura alugada com sucesso." + Environment.NewLine + "Pode proceder ao levantamnto da mesma.", "DriveMi - UrbanMShare");
            }

        }
    }
}
