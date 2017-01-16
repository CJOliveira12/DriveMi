using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrbanMShare
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menubtn_Click(object sender, EventArgs e)
        {
            if (this.menupanel.Location == new Point(-176, 73))
            {
                this.menupanel.Location = new Point(0, 73);
                this.menubtn.Size = new Size(176, 27);
            }
            else
            {
                this.menupanel.Location = new Point(-176, 73);
                this.menubtn.Size = new Size(13, 27);
            }
            
            
        }

        //_______________________MOVER JANELA____________________________
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // Botão adicionar -> redireciona para AddVehicleForm
        private void button4_Click(object sender, EventArgs e)
        {
            AddVehicleForm addvf = new AddVehicleForm();
            addvf.Show();
        }

        private void servicosbtn_Click(object sender, EventArgs e)
        {
            //servicospanel.BringToFront();
            menupanel.BringToFront();
            menubtn.BringToFront();
        }

        private void veiculosbtn_Click(object sender, EventArgs e)
        {
            veiculospanel.BringToFront();
            menupanel.BringToFront();
            menubtn.BringToFront();
        }

        //Botão logout
        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LoginFormB lfb = new LoginFormB();
            lfb.Show();
            this.Hide();
        }
    }
}
