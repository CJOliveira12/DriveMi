using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UrbanMShare
{
    public partial class urbanmshare : Form
    {
        
        int togmove;
        int mvalx;
        int mvaly;       
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, 
        int nTopRect, 
        int nRightRect, 
        int nBottomRect, 
        int nWidthEllipse, 
        int nHeightEllipse 
        );

        public urbanmshare()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            panel.Width = 950;
            panel.Left = 0;
            panel.Controls.Add(login.Instance);
            login.Instance.Dock = DockStyle.Fill;
            login.Instance.BringToFront();
        }

        private void btservices_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(services.Instance))
            {
                panel.Controls.Add(services.Instance);
                services.Instance.Dock = DockStyle.Fill;
                services.Instance.BringToFront();
            }
            else
            {
                services.Instance.BringToFront();
                services.Instance.cleanfilters();
                panel.AutoScroll = true;
            }
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(search.Instance))
            {
                panel.AutoScroll = false;
                panel.Controls.Add(search.Instance);
                search.Instance.Dock = DockStyle.Fill;
                search.Instance.BringToFront();
                panel.AutoScroll = false;
            }
            else
            {
                search.Instance.BringToFront();
                panel.AutoScroll = false;
                search.Instance.searchLoad();
            }
        }

        private void bthistoric_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(historic.Instance))
            {
                panel.Controls.Add(historic.Instance);
                historic.Instance.Dock = DockStyle.Fill;
                historic.Instance.BringToFront();
                panel.AutoScroll = false;
            }
            else
            {
                historic.Instance.BringToFront();
                panel.AutoScroll = false;
                historic.Instance.createform();
            }
        }

        private void btloginout_Click(object sender, EventArgs e)
        {
            panel.Width = 950;
            panel.Left = 0;
            panel.AutoScroll = false;
            panel.Controls.Add(login.Instance);
            login.Instance.Dock = DockStyle.Fill;
            login.Instance.BringToFront();
        }

        private void btnewaccount_Click(object sender, EventArgs e)
        {

        }
        
        private void paneltop_MouseDown(object sender, MouseEventArgs e)
        {
            togmove = 1;
            mvalx = e.X;
            mvaly = e.Y;
        }

        private void paneltop_MouseUp(object sender, MouseEventArgs e)
        {
            togmove = 0;
        }

        private void paneltop_MouseMove(object sender, MouseEventArgs e)
        {
            if (togmove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mvalx, MousePosition.Y - mvaly);
            }
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ctime.Text = DateTime.Now.ToString("HH:mm:ss");
            cdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbsmallname.Text = Program.smallname;
        }
    }
}
