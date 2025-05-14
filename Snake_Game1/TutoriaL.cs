using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game1
{
    public partial class TutoriaL : Form
    {
        Main_Menu MenuForm;
       
        public TutoriaL(Main_Menu menu)
        {
            InitializeComponent();
            MenuForm = menu;
        }

        private void TutoriaL_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 SnakeForm = new Form2();
            MenuForm.Hide();
            this.Hide();
            SnakeForm.ShowDialog();
            this.Close();



        }
    }
}
