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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign()
        {
            panelMediaSubmenu.Visible = false;
            panelPlaylistSubmenu.Visible = false;
            panelToolsSubmenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelMediaSubmenu.Visible == true)
                panelMediaSubmenu.Visible = false;
            if (panelPlaylistSubmenu.Visible == true)
                panelPlaylistSubmenu.Visible = false;
            if (panelToolsSubmenu.Visible == true)
                panelToolsSubmenu.Visible = false;

        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubmenu);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool x = false;
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == "Form2")
                    x= true;
            if (x==false)
            { 
                openChildForm(new Form2());
                x = true;
            }
        }

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPlaylistSubmenu);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            showSubMenu(panelToolsSubmenu);
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                        bool x = false;
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == "FormAddMO")
                    x = true;
            if (x == false)
            {
                openChildForm(new FormAddMO());
                x = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new Form4());
        }
    }
}
