using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UrbanMShare
{
    public partial class search : UserControl
    {
        private static search _instance;
        SqlConnection sqlconn = new SqlConnection(Program.strconn_str);
        SqlDataReader reader;

        public static search Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new search();
                return _instance;
            }
        }
        public search()
        {
            InitializeComponent();
        }

    private void ahpmethod()
        {
            double impF3F2;
            double impF2F3;
            double impF3F1;
            double impF1F3;
            double impF2F1;
            double impF1F2;
            double generalCIsum;
            double[,] general = new double[3, 5];

            if (tb1.Value > 8)
            {
                impF2F3 = Math.Abs(tb1.Value - 7);
                impF3F2 = Convert.ToDouble(1.0 / Math.Abs(tb1.Value - 7));
            }
            else
            {
                impF3F2 = Math.Abs(tb1.Value - 9);
                impF2F3 = Convert.ToDouble(1.0 / Math.Abs(tb1.Value - 9));
            }

            if (tb2.Value > 8)
            {
                impF1F2 = Math.Abs(tb2.Value -7);
                impF2F1 = Convert.ToDouble(1.0 / Math.Abs(tb2.Value - 7));
            }
            else
            {
                impF2F1 = Math.Abs(tb2.Value - 9);
                impF1F2 = Convert.ToDouble(1.0 / Math.Abs(tb2.Value - 9));
            }

            if (tb3.Value > 8)
            {
                impF1F3 = Math.Abs(tb3.Value - 7);
                impF3F1 = Convert.ToDouble(1.0 / Math.Abs(tb3.Value - 7));
            }
            else
            {
                impF3F1 = Math.Abs(tb3.Value - 9);
                impF1F3 = Convert.ToDouble(1.0 / Math.Abs(tb3.Value - 9));
            }

            general[0, 0] = 1.0;
            general[0, 1] = impF1F2;
            general[0, 2] = impF1F3;
            general[0, 3] = Math.Pow(general[0, 0] * general[0, 1] * general[0, 2], 1.0 / 3.0);
            general[1, 0] = impF2F1;
            general[1, 1] = 1.0;
            general[1, 2] = impF2F3;
            general[1, 3] = Math.Pow(general[1, 0] * general[1, 1] * general[1, 2],1.0/3.0);
            general[2, 0] = impF3F1;
            general[2, 1] = impF3F2;
            general[2, 2] = 1.0;
            general[2, 3] = Math.Pow(general[2, 0] * general[2, 1] * general[2, 2], 1.0 / 3.0);
            generalCIsum = Convert.ToDouble(general[0, 3] + general[1, 3] + general[2, 3]);
            general[0, 4] = general[0, 3] / generalCIsum;
            general[1, 4] = general[1, 3] / generalCIsum;
            general[2, 4] = general[2, 3] / generalCIsum;

            sqlconn.Open();
            SqlCommand qry1 = new SqlCommand(@"SELECT [IDvehicle], [EvTopic1], [EvTopic2], [EvTopic3]
                                             FROM [0601EvaluationAvg] ORDER BY [IDvehicle]", sqlconn);

            reader = qry1.ExecuteReader();

            int n = 0;
    
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    n += 1;
                }
            }
            reader.Close();

            double[,] evaluation = new double[n, 5];
            double[,] factor_1 = new double[n, n + 2];
            double[,] factor_2 = new double[n, n + 2];
            double[,] factor_3 = new double[n, n + 2];

            reader = qry1.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    evaluation[i, 0] = Convert.ToDouble(reader["IDvehicle"]);
                    evaluation[i, 1] = Convert.ToDouble(reader["EvTopic1"]);
                    evaluation[i, 2] = Convert.ToDouble(reader["EvTopic2"]);
                    evaluation[i, 3] = Convert.ToDouble(reader["EvTopic3"]);
                    i += 1;
                }
            }
            reader.Close();
            sqlconn.Close();

            for (int i = 0; i < n ; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    factor_1[i, j] = Convert.ToDouble(evaluation[i, 1]/ evaluation[j, 1]);
                    factor_2[i, j] = Convert.ToDouble(evaluation[i, 2] / evaluation[j, 2]);
                    factor_3[i, j] = Convert.ToDouble(evaluation[i, 3] / evaluation[j, 3]);
                }
                factor_1[i, n] = 1;
                factor_2[i, n] = 1;
                factor_3[i, n] = 1;
            }

            double factor1CIsum = 0, factor2CIsum = 0, factor3CIsum = 0;
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    factor_1[i, n] *= factor_1[i, j];
                    factor_2[i, n] *= factor_2[i, j];
                    factor_3[i, n] *= factor_3[i, j];
                }
                factor_1[i, n] = Math.Pow(factor_1[i, n], 1.0 / n);
                factor1CIsum += factor_1[i, n];
                factor_2[i, n] = Math.Pow(factor_2[i, n], 1.0 / n);
                factor2CIsum += factor_2[i, n];
                factor_3[i, n] = Math.Pow(factor_3[i, n], 1.0 / n);
                factor3CIsum += factor_3[i, n];
            }

            for (int i = 0; i < n; i++)
            {
                factor_1[i, n + 1] = factor_1[i, n] / factor1CIsum;
                factor_2[i, n + 1] = factor_2[i, n] / factor2CIsum;
                factor_3[i, n + 1] = factor_3[i, n] / factor3CIsum;
            }

            for (int i = 0; i < n; i++)
            {
                evaluation[i, 4] = factor_1[i, n + 1]* general[0, 4] + factor_2[i, n + 1]* general[1, 4] + factor_3[i, n + 1]* general[2, 4];
            }

            double[] scores = new double[n];

            for (int i = 0; i < n; i++)
            {
                scores[i] = evaluation[i,4];
            }

            Array.Sort(scores);
            Array.Reverse(scores);

            Program.ahp_order = " ORDER BY CASE [0501Vehicle].IDvehicle ";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (evaluation[j, 4] == scores[i])
                    {
                        Program.ahp_order += " WHEN " + evaluation[j, 0] + " THEN " + (i+1);
                    }
                }
            }
            Program.ahp_order += " End";
            Program.qrystate = 1;
            urbanmshare form1 = (urbanmshare)Application.OpenForms["urbanmshare"];
            Panel panel1 = (Panel)form1.Controls["panel"];
            panel1.AutoScroll = true;
            services.Instance.BringToFront();
            services.Instance.cleanfilters();
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            ahpmethod();
        }

        public void searchLoad()
        {
            tb1.Value = 8;
            tb2.Value = 8;
            tb3.Value = 8;
        }
    }
}
