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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Chantier2
{
    public partial class FormAddMO : Form
    {
        static int cpt1 = 1;
        static int cpt2 = 1;
        public FormAddMO()
        {
            InitializeComponent();
            button1.Enabled = false;
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                button1.Enabled = true;
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            

            dateTimePicker1.CustomFormat = " ";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = " ";
            comboBox1.Items.Add(""); comboBox1.Items.Add("Adrar"); comboBox1.Items.Add("Chlef"); comboBox1.Items.Add("Laghouat"); comboBox1.Items.Add("Oum El Bouaghi"); comboBox1.Items.Add("Batna"); comboBox1.Items.Add("Béjaïa"); comboBox1.Items.Add("Biskra"); comboBox1.Items.Add("Béchar"); comboBox1.Items.Add("Blida"); comboBox1.Items.Add("Bouira"); comboBox1.Items.Add("Tamanrasset"); comboBox1.Items.Add("Tébessa"); comboBox1.Items.Add("Tlemcen"); comboBox1.Items.Add("Tiaret"); comboBox1.Items.Add("Tizi Ouzou"); comboBox1.Items.Add("Alger"); comboBox1.Items.Add("Djelfa"); comboBox1.Items.Add("Jijel"); comboBox1.Items.Add("Sétif"); comboBox1.Items.Add("Saïda"); comboBox1.Items.Add("Skikda"); comboBox1.Items.Add("Sidi Bel Abbès"); comboBox1.Items.Add("Annaba"); comboBox1.Items.Add("Guelma"); comboBox1.Items.Add("Constantine"); comboBox1.Items.Add("Médéa"); comboBox1.Items.Add("Mostaganem"); comboBox1.Items.Add("M'Sila"); comboBox1.Items.Add("Mascara"); comboBox1.Items.Add("Ouargla"); comboBox1.Items.Add("Oran"); comboBox1.Items.Add("El Bayadh"); comboBox1.Items.Add("Illizi"); comboBox1.Items.Add("Bordj Bou Arreridj"); comboBox1.Items.Add("Boumerdès"); comboBox1.Items.Add("El Tarf"); comboBox1.Items.Add("Tindouf"); comboBox1.Items.Add("Tissemsilt"); comboBox1.Items.Add("El Oued"); comboBox1.Items.Add("Khenchela"); comboBox1.Items.Add("Souk Ahras"); comboBox1.Items.Add("Tipaza"); comboBox1.Items.Add("Mila"); comboBox1.Items.Add("Aïn Defla"); comboBox1.Items.Add("Naâma"); comboBox1.Items.Add("Aïn Témouchent");
            comboBox1.SelectedIndex = 0;
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) ;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (cpt1 > 0)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Short;
                cpt1--;
            }
           Console.WriteLine("hello"+dateTimePicker1.Value.Date.ToString("dd/MM/yyyy"));
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (cpt2 > 0)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                cpt2--;
            }
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox1.SelectedItem.ToString());
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int Rafik = 0;
            DateTime? dateNaissance = new Nullable<DateTime>();
            DateTime? dateAjout = new Nullable<DateTime>();

            if (cpt1 == 1)
            {
                dateNaissance = null;

            }
            else
            {
                this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
                this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
                dateNaissance = this.dateTimePicker1.Value;
                Console.WriteLine(dateNaissance);
            }
            if (cpt2 == 1)
            {

                dateAjout = null;
            }
            else
            {
                this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
                this.dateTimePicker2.CustomFormat = "yyyy/MM/dd";
                dateAjout = this.dateTimePicker2.Value;
            }

            Program.DataBaseConnection DB = new Program.DataBaseConnection();
            var connectionString = String.Format("server = localhost; user id = root; database = chantier");
            DB.OpenConnection();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string queryString = "SELECT id_value from idvalue where table_name='worker'";
                MySqlDataReader reader = null;
                MySqlCommand command = new MySqlCommand(queryString, conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Rafik = reader.GetInt32(0);
                    Console.WriteLine(Rafik);
                    Rafik++;
                }
                conn.Close();
                conn.Open();
                Console.WriteLine("rani djit");
                string m = "INSERT into worker (worker_id,nom,prenom,surnom,willaya,birth_date,join_date) Values (" + Rafik + ",'" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + comboBox1.SelectedItem + "','" + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + "','" + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy") + "')";
                MySqlCommand command2 = new MySqlCommand(m, conn);
                command2.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string k = "Update idvalue set id_value = id_value +1 where table_name='worker'";
                MySqlCommand command3 = new MySqlCommand(k, conn);
                command3.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
