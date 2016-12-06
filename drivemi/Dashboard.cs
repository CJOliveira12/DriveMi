using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drivemi
{
    public partial class Dashboard : Form
    {
        static double lat = 41.11984;
        static double lng = -8.6257907;
        public GMapOverlay mapOverlay = new GMapOverlay();
        public GMarkerGoogle cityCenter = new GMarkerGoogle(new PointLatLng(double.Parse(lat.ToString()), double.Parse(lng.ToString())), GMarkerGoogleType.red_pushpin);
        public static List<String> numbersInRadius = new List<string>();

        public static List<ResourceModel> model { get; private set; }

        static void GetLocationProperty()
        {
            //watcher = new System.Device.Location.GeoCoordinateWatcher();
        }

        public Dashboard(string role)
        {
            
            InitializeComponent();

            gMapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                UpdateMap(trackBar1.Value);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                UpdateMap(trackBar1.Value);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                UpdateMap(trackBar1.Value);
            }
        }

        private async void UpdateMap(int radius)
        {
            await fetchDetails(radius);
            markPins(radius);
        }
        
        private async Task fetchDetails (int radius)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://drivemi.dx.am/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applocation/json"));

                    HttpResponseMessage response = await client.GetAsync("api/resource?latitude=" + gMapControl.Position.Lat + "&longitude=" + gMapControl.Position.Lng + "&radius=" + trackBar1.Value.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonArray = await response.Content.ReadAsStringAsync();
                        //model = JsonConvert.DeserializeObject<List<ResourceModel>>(jsonArray);
                        //model = ;
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void markPins (int radius)
        {
            mapOverlay.Polygons.Clear();
            mapOverlay.Markers.Clear();
            gMapControl.Overlays.Clear();
            numbersInRadius.Clear();

            if (model != null)
            {
                foreach (var entry in model)
                {
                    if((radioButton1.Checked ||
                        (radioButton2.Checked && radioButton2.Tag.ToString().Equals(entry.hierarchy)) ||
                        (radioButton3.Checked && radioButton3.Tag.ToString().Equals(entry.hierarchy))) &&
                        entry.distance <= trackBar1.Value)
                    {
                        Image img = Image.FromFile("unknown.png", true);

                        switch (entry.hierarchy)
                        {
                            case "admin":
                                img = Image.FromFile("image.png");
                                break;
                            default:
                                break;
                        }

                        Bitmap bmp = new Bitmap(img, new Size(30, 30));

                        GMarkerGoogle pin = new GMarkerGoogle(new PointLatLng(double.Parse(entry.latitude.ToString()), double.Parse(entry.longitude.ToString())), bmp);

                        pin.ToolTip = new GMapRoundedToolTip(pin);
                        pin.ToolTipText = entry.hierarchy + ": " + entry.name + "\n" + entry.reserve1 + " | " + entry.blood_group + " | " + entry.phone;

                        mapOverlay.Markers.Add(pin);
                        numbersInRadius.Add(entry.phone);
                    }
                }
            }

            mapOverlay.Markers.Add(cityCenter);
            CreateCircle(cityCenter.Position, trackBar1.Value, 50000);
            gMapControl.Overlays.Add(mapOverlay);
        }

        private void CreateCircle (PointLatLng point, double radius, int segments)
        {
            List<PointLatLng> gpollist = new List<PointLatLng>();
            double seg = Math.PI * 2 / segments;
            radius /= 100000;

            for (int i = 0; i< segments; i++)
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

        private void gMapControl_Load(object sender, EventArgs e)
        {

        }
    }
}
