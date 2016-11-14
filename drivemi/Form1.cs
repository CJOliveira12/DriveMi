using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drivemi
{
    public partial class frmApp : Form
    {
        double lat = 23.179357;
        double lng = 2;
        public static List<ResourceModel> model { get; private set; }

        public frmApp()
        {
            
            InitializeComponent();

            if (radioButton3.Checked)
            {
                drawMap(trackBar1.Value);
            }
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
                        model = JsonConvert.DeserializeObject<List<ResourceModel>>(jsonArray);
                    }
                }
            }
        }
    }
}
