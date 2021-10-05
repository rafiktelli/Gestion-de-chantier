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
    public partial class Form4 : Form
    {
        MySqlConnection connectionString = new MySqlConnection("server=localhost;user id=root;database=chantier");
        Program.DataBaseConnection DB = new Program.DataBaseConnection();
        public Form4()
        {
            DB.OpenConnection();
            InitializeComponent();
            DataTable dt = DB.FillFicheJaune(15, 2021, 5);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["worker_id"].Visible = false;
            DB.CloseConnection();


        }
        

    }
}
