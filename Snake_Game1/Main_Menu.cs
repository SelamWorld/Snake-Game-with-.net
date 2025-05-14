using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.VisualBasic.ApplicationServices;

namespace Snake_Game1
{
    public partial class Main_Menu : Form
    {
        SoundPlayer SoundMenu = new SoundPlayer(Properties.Resources.MainMenu);
        Image Onimg = Properties.Resources.Sound_On;  


        Image Offimg = Properties.Resources.Sound_Off;
        bool ifSoundOn=true;
        public Main_Menu()
        {
            InitializeComponent();
            SoundMenu.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TutoriaL tutorial = new TutoriaL(this);
            tutorial.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HI_Scores hI_Scoresform = new HI_Scores();
            hI_Scoresform.Show();
            hI_Scoresform.Focus();

        }

        private void button3_Click(object sender, EventArgs e)      //sound off/on
        {
            if (ifSoundOn)
            {
                SoundMenu.Stop();
                button3.Image = Onimg;
                ifSoundOn = !ifSoundOn;
            }
            else
            {
                SoundMenu.PlayLooping();
                button3.Image = Offimg;
                ifSoundOn = !ifSoundOn;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
