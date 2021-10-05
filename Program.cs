using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Chantier2
{
    struct ListElement
    {
        public string row;
        public int rowIndex;
        public int column;
    }
    static class Program
    {
        public const int cellMaxInput = 2;
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major>=6)
                SetProcessDPIAware();
                
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        // Connexion
        public class DataBaseConnection
        {
            MySqlConnection connectionString = new MySqlConnection("server=localhost;user id=root;database=chantier");

            public void OpenConnection()
            {
                connectionString.Open();
            }
            public void CloseConnection()
            {
                connectionString.Close();
            }
            public DataTable ReadValue(int lm, int rm, int ry)
            {
                int i;
                string s;
                DataColumn Col;
                // 
                
                MySqlDataAdapter da = new MySqlDataAdapter("select worker_id,nom, prenom from worker", connectionString);
                DataTable dt = new DataTable();
                for (i=1;i<=lm;i++)
                {
                    s = i.ToString();
                    string date_s = ry + "-" + rm + "-" + i;
                    DateTime _date = Convert.ToDateTime(date_s);
                    String dayOfWeek = _date.DayOfWeek.ToString();
                    /*  if (dayOfWeek == "Friday")
                      {
                          Col = dt.Columns.Add(s + "v");
                      }
                      else
                      {
                          Col = dt.Columns.Add(s);
                      }*/
                    Col = dt.Columns.Add(s);
                    //Col.SetOrdinal(Col.Ordinal + 1);

                }
                //dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[22];

                da.Fill(dt);
                dt.Columns["nom"].SetOrdinal(0);
                dt.Columns["prenom"].SetOrdinal(1);
                dt.Columns["nom"].ReadOnly = true;
                dt.Columns["prenom"].ReadOnly = true;
                dt.Columns["worker_id"].ReadOnly = true;

                
                

                for (int j=1; j<=lm;j++)
                {
                    int datee = ry * 10000 + rm * 100 + j ;
                    string qqueryString = "SELECT worker_idd,etat from pointage where dateID="+datee;
                    //string __date = ry + "-" + rm + "-" + j ;
                    //DateTime d = DateTime.Parse(__date);
                    //string perm = d.DayOfWeek.ToString();
                    //Console.WriteLine(perm);
                    MySqlDataReader reader = null;
                    this.CloseConnection();
                    this.OpenConnection();
                    MySqlCommand command = new MySqlCommand(qqueryString, connectionString);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int worker_id = reader.GetInt32(0);
                        string pointage = reader.GetValue(1).ToString();
                        DataRow[] result = dt.Select("worker_id ="+worker_id);
                        int SelectedIndex = dt.Rows.IndexOf(result[0]);
                        dt.Rows[SelectedIndex][j+1] = pointage; 
                    }

                }

                

                return dt;
            }
            
           public DataTable FillCombo()
            {
                string Query = "select Distinct(year) from edate";
                MySqlDataAdapter da1 = new MySqlDataAdapter(Query, connectionString);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                return dt1;
            }

            public DataTable FillComboMonth()
            {
                string Query = "select Distinct(monthName) from edate";
                MySqlDataAdapter da2 = new MySqlDataAdapter(Query, connectionString);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                return dt2;
            }

            public DataTable FillHours(int _date)
            {
                DataColumn Col;
                MySqlDataAdapter da = new MySqlDataAdapter("select worker_id,nom, prenom from worker", connectionString);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string s = "Heures de travail"; dt.Columns.Add(s);
                s = "Heures de travail supplumentaires"; dt.Columns.Add(s);
                // fill with pointage 
                string qqueryString = "SELECT worker_idd,etat,etatS from pointage where dateID=" + _date;
                MySqlDataReader reader = null;
                MySqlCommand command = new MySqlCommand(qqueryString, connectionString);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int worker_id = reader.GetInt32(0);
                    string etat = reader.GetValue(1).ToString(); 
                    string etatS = reader.GetValue(2).ToString();

                    DataRow[] result = dt.Select("worker_id =" + worker_id);
                    int SelectedIndex = dt.Rows.IndexOf(result[0]);
                    dt.Rows[SelectedIndex][3] = etat;
                    dt.Rows[SelectedIndex][4] = etatS;
                }
                dt.Columns["nom"].SetOrdinal(0);
                dt.Columns["prenom"].SetOrdinal(1);
                dt.Columns["nom"].ReadOnly = true;
                dt.Columns["prenom"].ReadOnly = true;
                dt.Columns["worker_id"].ReadOnly = true;


                return dt;
            }

            public DataTable FillFicheJaune(int WID, int year, int month)
            {
                
                MySqlDataAdapter da = new MySqlDataAdapter("select worker_id from worker where worker_id="+0, connectionString);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for(int i=1;i<=16;i++)
                {
                    dt.Columns.Add(i.ToString());
                }
                int _dateM1 = year * 10000 + month * 100;
                int _dateM2 = _dateM1 + 100;
                string qqueryString = "SELECT dateID,etat,etatS from pointage where (dateID>" + _dateM1+" and dateID<"+_dateM2+" and worker_idd = "+WID +")";
                MySqlDataReader reader = null;
                MySqlCommand command = new MySqlCommand(qqueryString, connectionString);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int day = reader.GetInt32(0)%100;
                    string etat = reader.GetValue(1).ToString();
                    string etatS = reader.GetValue(2).ToString();

                }

                DataRow _row = dt.NewRow();

                for (int i = 1; i <= 16; i++)
                {
                    _row[i] = "6";
                }
                dt.Rows.InsertAt(_row, 0);



                DataRow _row3 = dt.NewRow();
                for (int i = 17; i <= 31; i++)
                {
                    int c = i - 16;
                    _row3[i-16] = i;
                }
                dt.Rows.InsertAt(_row3, 2);

                DataRow _row4 = dt.NewRow();
                dt.Rows.InsertAt(_row4, 3);


                return dt;
            }
            


        }
    }
}
