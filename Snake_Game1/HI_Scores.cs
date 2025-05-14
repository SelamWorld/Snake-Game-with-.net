using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Snake_Game1
{
    

    public partial class HI_Scores : Form
    {
        Dictionary<string, int> ScoresDict = new Dictionary<string, int>();
        List <string> veri=new List<string>();
        void DatafromDataBase()
        {
            //string connectionString = "Server=localhost;Database=SnakeData;Trusted_Connection=True;";
            //SqlConnection connection1 = new SqlConnection(connectionString);
            //SqlCommand command = new SqlCommand("SELECT TOP 7 * FROM Players"+" ORDER BY HighScore Desc",connection1);
            //connection1.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    ScoresDict.Add((string)reader[0]+"\t"+"............................",(int)reader[1]);
            //    veri.Add(reader[0].ToString()+"\t"+"............."+ reader[1].ToString());
            //}
            //connection1.Close();
        }

        public HI_Scores()
        {
            InitializeComponent();
        }

        private void HI_Scores_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DatafromDataBase();
            foreach (var  item in ScoresDict)
            {
                listBox1.Items.Add(item);
            }

            
            
            
        }

        
        private void button1_Click(object sender, EventArgs e)   // back to main menu
        {
            this.Hide();
            this.Close();


        }
    }
}
