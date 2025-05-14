using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game1
{
    public partial class ScoreInsert : Form
    {
        Dictionary<string, int> ScoresDict = new Dictionary<string, int>();
        List<string> veri = new List<string>();

        int highScore;
        string name;
        Main_Menu newMenu2 = new Main_Menu();

        Form2 form2;
        public ScoreInsert(Form2 form)
        {
            InitializeComponent();
            form2 = form;
        }


        void DatafromDataBase()
        {
            //name = textBox1.Text;
            //string connectionString = "Server=localhost;Database=SnakeData;Trusted_Connection=True;";
            //SqlConnection connection1 = new SqlConnection(connectionString);
            //SqlCommand command = new SqlCommand("INSERT INTO Players (Name, HighScore) VALUES (@Name, @HighScore)", connection1);
            //command.Parameters.AddWithValue("@Name", name);
            //command.Parameters.AddWithValue("@HighScore", highScore);
            //connection1.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //connection1.Close();
        }
        private void Back2MenuBut_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            DatafromDataBase();

            this.Hide();
            this.Close();
            form2.SoundOver.Stop();
            form2.Close();
            newMenu2.Show();
            
        }

        private void ScoreInsert_Load(object sender, EventArgs e)
        {
            highScore = form2.HIscore;
            ScoreLabel.Text = "Your Score: " + highScore.ToString();
        }
    }
}
