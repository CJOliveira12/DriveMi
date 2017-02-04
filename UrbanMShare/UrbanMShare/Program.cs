using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrbanMShare
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new urbanmshare());
        }

        private static string usr_smallname;
        private static Int32 usr_idclient;
        private static Int32 id_rent;
        private static Int32 id_service;
        private static string src_order=string.Empty;
        private static byte qry_state;

        private static string strconn = @"Data Source=TESZERO-PC\DRIVEMI;Initial Catalog=UrbanMShare;Persist Security Info=True;User ID=sa;Password=admindrivemi";

        public static string strconn_str
        {
            get
            {
                return strconn;
            }
        }

        public static int idrent
        {
            get
            {
                return id_rent;
            }
            set
            {
                id_rent = value;
            }
        }

        public static int idservice
        {
            get
            {
                return id_service;
            }
            set
            {
                id_service = value;
            }
        }

        public static string ahp_order
        {
            get
            {
                return src_order;
            }
            set
            {
                src_order = value;
            }
        }

        public static byte qrystate
        {
            get
            {
                return qry_state;
            }
            set
            {
                qry_state = value;
            }
        }

        public static string smallname
        {
            get
            {
                return usr_smallname;
            }
            set
            {
                usr_smallname = value;
            }
        }

        public static Int32 idclient
        {
            get
            {
                return usr_idclient;
            }
            set
            {
                usr_idclient = value;
            }
        }

        public static string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }



    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }


}
