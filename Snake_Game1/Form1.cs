using System.Windows.Forms;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Snake_Game1
{    //******** MY first Code. it didnt work but i couldnt dare to delete it************
    public partial class Form1 : Form
    {
        int Time;
    
        string OneLineSpace = ".";
        int RowNum = 25;       
        int ColNum = 100;

        bool DirLeft,DirRight,DirUp, DirDown;
        string Snake = "ABCDEF";
        char SnakeBody;
        int SnakeLength = 6;
        int HeadLoc=1249,TailLoc=1255;
        int k = 0;

        public Form1()
        {                                                        
            InitializeComponent();
            this.KeyPreview = true;
            timer1.Start();
            Accelerator.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // GameBoard.SelectionColor = Color.Black;
            GameBoard.SelectAll();
            GameBoard.SelectionColor = Color.Black;
            //GameBoard.ForeColor = Color.Black;
            for (int i = 0; i < RowNum; i++)
            {
                for (int j = 0; j < ColNum; j++)
                {
                    GameBoard.AppendText(OneLineSpace);
                }
            }
            GameBoard.Select(1249, 6);
            GameBoard.SelectionColor = Color.White;
            GameBoard.SelectedText = Snake;
            HeadLoc = 1249;
            DirLeft=true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.W || e.KeyCode== Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                e.SuppressKeyPress = true;  // prevent error sound
            }
            if (e.KeyCode == Keys.W && e.KeyCode!=Keys.S)
            {
                DirUp = true;
                DirLeft =false;
                DirDown =false;
                DirRight =false;

            }
            if(e.KeyCode == Keys.A && e.KeyCode != Keys.D)
            {
                DirLeft=true;
                DirUp = false;
                DirDown = false;
                DirRight = false;

            }

            if (e.KeyCode == Keys.D && e.KeyCode != Keys.A)
            {
                DirRight = true;
                DirLeft = false;
                DirDown = false;
                DirUp = false;
            }
            if (e.KeyCode == Keys.S && e.KeyCode != Keys.W)
            {
                DirDown = true;
                DirLeft = false;
                DirUp = false;
                DirRight = false;
            }         
        }
       
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Time 
            Time++;
            label1.Text ="Time: " + Time.ToString();    
        }

       
        private void GameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            //label2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        //one step Movement
        private void button2_Click(object sender, EventArgs e)
        {
            SnakeBody = GameBoard.Text[HeadLoc + k];
            GameBoard.Text = GameBoard.Text.Insert(HeadLoc - 1 + k, SnakeBody.ToString()).Remove(HeadLoc + k, 1);
            k++;
        }


        private void SnakeAccelerator(object sender, EventArgs e)
        {
            if (DirLeft == true)
            {
               
                
                
                
                ////one block of left Movement          (old code)

                //for (int i = 0; i <= SnakeLength; i++)
                //{
                //    SnakeBody = GameBoard.Text[HeadLoc + k];
                //    GameBoard.Text = GameBoard.Text.Insert(HeadLoc - 1 + k, SnakeBody.ToString()).Remove(HeadLoc + k, 1);
                //    k++;
                //}
                //k = 0;
                //HeadLoc--;
            }

            if (DirRight == true)
            {

            }
            if (DirUp == true)
            {

            }
            if (DirDown == true)
            {

            }


        }

        private void TestBut_Click(object sender, EventArgs e)
        {
            SnakeBody = GameBoard.Text[HeadLoc];
            label2.Text ="Snake body: "+ SnakeBody.ToString();
            label4.Text ="headloc: "+ HeadLoc.ToString();
            label5.Text ="k value: " + k.ToString();
            label6.Text = "line count: " + GameBoard.Lines.Length;
            label3.Text = " Characters count: " + GameBoard.Text.Length.ToString();
        }

    }
}