using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Net.Http;
using Newtonsoft.Json;
//using Twilio;
//using System.Net;

namespace drivemi
{
    public partial class frmApp : Form
    {
        static double lat = 41.1197319;
        static double lng = -8.6235538;
        public GMapOverlay mapOverlay = new GMapOverlay();
        public GMarkerGoogle cityCenter = new GMarkerGoogle(new PointLatLng(double.Parse(lat.ToString()), double.Parse(lng.ToString())), GMarkerGoogleType.red_pushpin);
        public static List<ResourceModel> model { get; private set; }
        public static List<String> numbersInRadius = new List<string>();
        //public const string NameSpace = "smsgateway-android";
        public const string QueueName = "queue";

        //public static QueueClient MsgQueueClient;
        //public static MobileServiceClient MobileService = new MobileServiceClient("https://sms-ams.azure-mobile.net/", "vvvqqwgdejhhDbURXmpjnFspSPrzeR93");
        public frmApp()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblRadius.Text = "Radius: " + double.Parse(trackBar1.Value.ToString()) / 1000 + "km";
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            UpdateMap(trackBar1.Value);
        }

        private void gMapControl_OnMapDrag()
        {
            cityCenter.Position = gMapControl.Position;
            UpdateMap(trackBar1.Value);
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            lblZoom.Text = "Zoom: " + trackBarZoom.Value;
        }

        private void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            gMapControl.Zoom = trackBarZoom.Value;
        }

        private void gMapControl_DoubleClick(object sender, EventArgs e)
        {
            gMapControl.Position = gMapControl.FromLocalToLatLng(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
            cityCenter.Position = gMapControl.Position;
            UpdateMap(trackBar1.Value);
        }

        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                UpdateMap(trackBar1.Value);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                UpdateMap(trackBar1.Value);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                UpdateMap(trackBar1.Value);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                UpdateMap(trackBar1.Value);
        }

        private void frmApp_Load(object sender, EventArgs e)
        {
            gMapControl.Position = new PointLatLng(41.1197319, -8.6235538);
            gMapControl.Zoom = 14;
            gMapControl.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            
            GMaps.Instance.Mode = AccessMode.ServerAndCache;


            cityCenter = new GMarkerGoogle(new PointLatLng(41.1197319, -8.6235538), GMarkerGoogleType.red_pushpin);

            mapOverlay.Markers.Add(cityCenter);
            CreateCircle(cityCenter.Position, trackBar1.Value, 50);
        }

        #region Private_Func

        private async void UpdateMap(int radius)
        {
            await fetchDetails(radius);
            markPins(radius);
        }


        private async Task fetchDetails(int radius)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://resourceman.azurewebsites.net");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applocation/json"));

                    string url = "api/resource?latitude=" + gMapControl.Position.Lat +
                                        "&longitude=" + gMapControl.Position.Lng + "&radius=" + trackBar1.Value.ToString();

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonArray = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<List<ResourceModel>>(jsonArray);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radius"></param>
        private void markPins(int radius)
        {
            mapOverlay.Polygons.Clear();
            mapOverlay.Markers.Clear();
            gMapControl.Overlays.Clear();
            numbersInRadius.Clear();

            if (model != null)
            {
                foreach (var entry in model)
                {
                    if ((radioButton1.Checked ||
                        (radioButton2.Checked && radioButton2.Tag.ToString().Equals(entry.hierarchy)) ||
                        (radioButton3.Checked && radioButton3.Tag.ToString().Equals(entry.hierarchy)) ||
                        (radioButton4.Checked && radioButton4.Tag.ToString().Equals(entry.hierarchy))) &&
                        entry.distance <= trackBar1.Value)
                    {
                        Image img = Image.FromFile("unknown.png", true);

                        switch (entry.hierarchy)
                        {
                            case "Doctor":
                                img = Image.FromFile("doctor.png");
                                break;

                            case "Staff":
                                img = Image.FromFile("Staff.png");
                                break;

                            case "Volunteer":
                                img = Image.FromFile("cpr.png");
                                break;

                            default:
                                break;
                        }

                        Bitmap bmp = new Bitmap(img, new Size(30, 30));

                        GMarkerGoogle pin = new GMarkerGoogle(new PointLatLng(double.Parse(entry.latitude.ToString()), double.Parse(entry.longitude.ToString())), bmp);

                        pin.ToolTip = new GMapRoundedToolTip(pin);
                        pin.ToolTipText = entry.hierarchy + ": " + entry.name + "\n" +
                            entry.reserve1 + " | " + entry.blood_group + " | " + entry.phone;

                        mapOverlay.Markers.Add(pin);
                        numbersInRadius.Add(entry.phone);
                    }
                }
            }
            mapOverlay.Markers.Add(cityCenter);
            CreateCircle(cityCenter.Position, trackBar1.Value, 50000);
            gMapControl.Overlays.Add(mapOverlay);

        }

        private void CreateCircle(PointLatLng point, double radius, int segments)
        {
            List<PointLatLng> gpollist = new List<PointLatLng>();
            double seg = Math.PI * 2 / segments;
            radius /= 100000;

            for (int i = 0; i < segments; i++)
            {
                double theta = seg * i;
                double a = point.Lat + Math.Cos(theta) * radius * 0.9;
                double b = point.Lng + Math.Sin(theta) * radius;

                gpollist.Add(new PointLatLng(a, b));
            }
            GMapPolygon gpol = new GMapPolygon(gpollist, "Hot");
            gpol.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            gpol.Stroke = new Pen(Color.Red, 1);

            mapOverlay.Polygons.Add(gpol);
            gMapControl.Overlays.Add(mapOverlay);

        }
        #endregion
    }
}
