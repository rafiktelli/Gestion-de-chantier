using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chantier2
{
    public partial class Form3 : Form
    {
        public static bool cvc = false;
        public static int cpt = 0;
        public static DateTime _time;
        public static bool continu = true;

        static List<ListElement> changedCells = new List<ListElement>();

        MySqlConnection connectionString = new MySqlConnection("server=localhost;user id=root;database=chantier");
        Program.DataBaseConnection DB = new Program.DataBaseConnection();

        public Form3()
        {
            DB.OpenConnection();
            InitializeComponent();
            _time = dateTimePicker3.Value;
            int _date = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
            DataTable dt = DB.FillHours(_date);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["worker_id"].Visible = false;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 2;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[4]).MaxInputLength = 2;
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.Tomato;
            

            DB.CloseConnection();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }





        




        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            continu = true;
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
                        datee = _time.Year * 10000 + _time.Month * 100 + _time.Day;
                        if (changedCells[i].column == 2) { 
                            m = "INSERT INTO pointage (etat, worker_idd, dateID) VALUES('" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'," + changedCells[i].row + "," + datee + " ) ON DUPLICATE KEY UPDATE etat ='" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'";
                        }
                        else
                        { 
                            m = "INSERT INTO pointage (etatS, worker_idd, dateID) VALUES('" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'," + changedCells[i].row + "," + datee + " ) ON DUPLICATE KEY UPDATE etatS ='" + dataGridView1.Rows[changedCells[i].rowIndex].Cells[changedCells[i].column + 1].Value + "'";
                        }
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
                    continu = true;
                    cvc = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.save_changes();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.confirm_changes_saving();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            DB.OpenConnection();
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
                    continu = true;
                    cvc = false;
                }
                if (res == DialogResult.Cancel)
                {
                    continu = false;
                }
            }
            if (continu) 
            { 
                _time = dateTimePicker3.Value;
                int _date = dateTimePicker3.Value.Year * 10000 + dateTimePicker3.Value.Month * 100 + dateTimePicker3.Value.Day;
                DataTable dt = DB.FillHours(_date);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["worker_id"].Visible = false;
                ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 2;
                ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 2;
            }
            DB.CloseConnection();
        }
    }
}
