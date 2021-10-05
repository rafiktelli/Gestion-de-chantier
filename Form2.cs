using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.VisualStyles;

namespace Chantier2
{

    public partial class Form2 : Form
    {
        public static int yearIndex = DateTime.Now.Year - 2021;
        public static int monthIndex = DateTime.Now.Month - 1;
        public static bool cvc = false;
        public static int cpt = 0;
        static List<ListElement> changedCells = new List<ListElement>();





        public Form2()
        {
            InitializeComponent();

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            Program.DataBaseConnection DB = new Program.DataBaseConnection();
            DB.OpenConnection();



            // combo only
            dt1 = DB.FillCombo();
            comboBox1.DataSource = dt1;
            comboBox1.ValueMember = "year";
            comboBox1.DisplayMember = "year";
            // combo2 only 
            dt2 = DB.FillComboMonth();
            comboBox2.DataSource = dt2;
            comboBox2.ValueMember = "monthName";
            comboBox2.DisplayMember = "monthName";


            comboBox2.SelectedIndex = DateTime.Now.Month - 1;
            comboBox1.SelectedIndex = DateTime.Now.Year - 2021;
            




            DB.CloseConnection();


        }

        private void X_Click(object sender, EventArgs e)
        {
            //this.confirm_changes_saving();
            this.Close();
        }


        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cvc == true)
            {
                Console.WriteLine("Please save your data");
                DialogResult res = MessageBox.Show("Voulez-vous enregistrer les modifications?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    this.save_changes();
                    //Some task…  
                }
                if (res == DialogResult.No)
                {
                    cvc = false;
                    
                    //Some task…  
                }
                if (res == DialogResult.Cancel)
                {
                    goto Finish;
                }
            }

            Program.DataBaseConnection DB = new Program.DataBaseConnection();
            var connectionString = String.Format("server = localhost; user id = root; database = chantier");

            DB.OpenConnection();
            int Rafik = 0; 
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //monthIndex = DateTime.Now.Month;
                int realYear = yearIndex + 2021;
                int realMonth = monthIndex + 1;


                if (comboBox2.SelectedIndex != monthIndex)
                {
                    monthIndex = comboBox2.SelectedIndex;
                    realMonth = monthIndex + 1;
                    string queryString = "SELECT count(day) from edate where month=" + realMonth + " and year = " + realYear;
                    MySqlDataReader reader = null;
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Rafik = reader.GetInt32(0);
                    }
                    //comboBox2.SelectedIndex = Rafik ;
                    Console.WriteLine("month"+Rafik);
                    DataTable dt8 = new DataTable();
                    dt8 = DB.ReadValue(Rafik, realMonth, realYear);
                    dataGridView1.DataSource = dt8;
                    dataGridView1.Columns["worker_id"].Visible = false;
                    dataGridView1.Columns[0].Frozen = true;
                    dataGridView1.Columns[1].Frozen = true;
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns[0].Width = 120;
                    dataGridView1.Columns[1].Width = 120;
                    // dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[22];
                    for (int i = 0; i < Rafik; i++)
                    {
                        ((DataGridViewTextBoxColumn)dataGridView1.Columns[i + 2]).MaxInputLength = Program.cellMaxInput;
                    }

                }




                conn.Close();
            }

        /*  if (comboBox2.SelectedIndex != monthIndex)
          {
              monthIndex = comboBox2.SelectedIndex;

              string queryString = "SELECT count(day) from edate where month=" + monthIndex + " and year = " + yearIndex;




          } */

        Finish:;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cvc == true)
            {
                Console.WriteLine("Please save your data");
                DialogResult res = MessageBox.Show("Voulez-vous enregistrer les modifications?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    save_changes();
                    //Some task…  
                }
                if (res == DialogResult.No)
                {
                    cvc = false;
                    //Some task…  
                }
                if (res == DialogResult.Cancel)
                {
                    goto Finish;
                }
            }
            Program.DataBaseConnection DB = new Program.DataBaseConnection();
            var connectionString = String.Format("server = localhost; user id = root; database = chantier");

            DB.OpenConnection();
            int Rafik = 0;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                //monthIndex = DateTime.Now.Month;
                int realYear = yearIndex + 2021;
                int realMonth = monthIndex + 1;


                if (comboBox1.SelectedIndex != yearIndex)
                {
                    monthIndex = comboBox2.SelectedIndex;
                    realMonth = monthIndex + 1;
                    yearIndex = comboBox1.SelectedIndex;
                    realYear = yearIndex + 2021;


                    string queryString = "SELECT count(day) from edate where month=" + realMonth + " and year = " + realYear;
                    MySqlDataReader reader = null;
                    MySqlCommand command = new MySqlCommand(queryString, conn);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Rafik = reader.GetInt32(0);
                    }
                    //comboBox2.SelectedIndex = Rafik ;
                    Console.WriteLine("year"+Rafik);
                    DataTable dt8 = new DataTable();
                    dt8 = DB.ReadValue(Rafik, realMonth, realYear);
                    dataGridView1.DataSource = dt8;
                    dataGridView1.Columns[0].Frozen = true;
                    dataGridView1.Columns[1].Frozen = true;
                    //dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[22];
                    for (int i = 0; i < Rafik; i++)
                    {
                        ((DataGridViewTextBoxColumn)dataGridView1.Columns[i + 2]).MaxInputLength = Program.cellMaxInput;
                    }

                }

            }
        Finish:;
        }

        public void Save_Click(object sender, EventArgs e)
        {
            this.save_changes();
        }




        private void dataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            ListElement l;
            l.column = e.ColumnIndex - 1;
            l.row = dataGridView1.Rows[e.RowIndex].Cells["worker_id"].Value.ToString();
            l.rowIndex = e.RowIndex;
            Console.WriteLine("l= " + l.column + " | " + l.row);
            changedCells.Add(l);
            Console.WriteLine("cpt = " + cpt);
            dataGridView1.CurrentCell.Style.BackColor = Color.LightGray;
            if (cvc == false)
            {
                cvc = true;
            }
            Console.WriteLine(cvc);
            cpt++;

        }

        public void save_changes()
        {
            cpt = 0;

            cvc = false;
            Program.DataBaseConnection DB = new Program.DataBaseConnection();
            var connectionString = String.Format("server = localhost; user id = root; database = chantier");

            DB.OpenConnection();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                int datee = 0;
                string m = "";
                if (changedCells.Count > 0)
                {
                    for (int i = 0; i < changedCells.Count; i++)
                    {
                        datee = (yearIndex + 2021) * 10000 + (monthIndex + 1) * 100 + changedCells[i].column;
                        m = "INSERT INTO pointage (etat, worker_idd, dateID) VALUES ('" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'," + changedCells[i].row + "," + datee + " ) ON DUPLICATE KEY UPDATE etat ='" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'";
                        MySqlCommand command = new MySqlCommand(m, conn);
                        command.ExecuteNonQuery();
                    }
                }
            }
            changedCells.Clear();
        }

        public void confirm_changes_saving()
        {
            if (cvc == true)
            {
                Console.WriteLine("Please save your data");
                DialogResult res = MessageBox.Show("Voulez-vous enregistrer les modifications?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    this.save_changes();
                    //Some task…  
                }
                if (res == DialogResult.No)
                {
                    cvc = false;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.confirm_changes_saving();
        }

    }
}
