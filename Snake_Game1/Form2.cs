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
using System.Data.SqlClient; 

namespace Snake_Game1
{
    // this project made by Halil İbrahim Çalış for c# class assignment
    public partial class Form2 : Form
    {


        Image Onimg = Properties.Resources.Sound_On; 
        Image Offimg = Properties.Resources.Sound_Off;

        SoundPlayer SoundGame = new SoundPlayer(Properties.Resources.Gameplay);
        public SoundPlayer SoundOver = new SoundPlayer(Properties.Resources.GAmeOver);
        Random random=new Random();

        string SnakeBody = "ooooc",SnakeHead="O",Temp;
        int SnakeLength = 5;
        int HeadLoc=0,BehindTailLoc=0;
        List<int> OldLoc = new List<int>();
        List<int> NewLoc = new List<int>();

        bool ifBaitEaten=false, ifGameOver=false;
        int randomLoc = 0;
        string GameBlock = "\u00A0",Bait="S",Map;   // wrapable invisible character  also gameblock
        int Time = 0;
        public int Score=0,HIscore;
        string pauseText = " 0000        00        0     0   0000    00000   000       \r\n  0   0      0  0       0     0  0        0       0   0     \r\n  0 0       000000      0     0   0000    00000   0    0    \r\n  0        0      0     0     0       0   0       0   0     \r\n  0       0        0    0000000   0000    00000   000       \r\n";

        bool ifSoundOn = true;
        int width = 60;  // Characters per row
        int height = 22; // Number of row
        string Direction="Left";
        bool DirectionAllow = false,isStoped=false;
        public Form2()
        {
            InitializeComponent();
            this.KeyPreview = true;
            label3.Focus();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            ArrowBox.Text = " ←";
            timer1.Start();          //Move snake 
            timer2.Start();          // timer
            SoundGame.PlayLooping();

            //draw map                                                                       w
            for (int row = 0; row < height; row++)
            {
                richTextBox1.AppendText("|"); // Left border

                for (int col = 1; col < width - 1; col++)
                {
                    if (row == 1 || row == 21)
                    {
                        richTextBox1.AppendText("-");   // down and up borders
                    }

                    else
                    {
                        richTextBox1.AppendText(GameBlock); // Filling the row
                    }
                }
                richTextBox1.AppendText("|"); // Right border    
            }

            PlaceBait();

            //snake Head first appear on the map
            richTextBox1.Select(694, 1);
            richTextBox1.SelectedText = SnakeHead;
            
            // draw snake body
            richTextBox1.Select(695, SnakeLength);    
            richTextBox1.SelectedText = SnakeBody;
            HeadLoc = richTextBox1.Find("O");

            //set old loc of body characters. OldLoac hold body characters' location.
            // NewLoc: HEadLoc first index and bod chars next positions'.
            for (int i = 0; i <= SnakeLength; i++) 
            {
                OldLoc.Add(HeadLoc + i + 1);
                NewLoc.Add(HeadLoc + i);
            }
            this.ActiveControl = label3;
        }

       
        void MoveHead(int DirectInt)
        {
            richTextBox1.Select(HeadLoc, 1);                     // Select head character
            Temp = richTextBox1.SelectedText;                    // assign head character to Temp variable
            richTextBox1.SelectedText = GameBlock;               // change head character with empty area
            richTextBox1.Select(HeadLoc + DirectInt, 1);         // select next character on the way of movement
            CheckNextMove(richTextBox1.SelectedText);            // check next move if it bait or obstacle
            richTextBox1.Select(HeadLoc + DirectInt, 1);         // select again to prevent some bug
            richTextBox1.SelectedText = Temp;                    // change that empty character with head
            HeadLoc+=DirectInt;                                  // update head location
        }
        // places bait on map random location
        void PlaceBait()    
        {            
            do
            {   
                randomLoc = random.Next(0, 1320);
                richTextBox1.Select(randomLoc, 1);

            } while (richTextBox1.SelectedText!=GameBlock || randomLoc<=60);            
            
            richTextBox1.SelectedText = Bait;
            richTextBox1.SelectionLength = 0;
        }

        void StopGame()
        {
            if (!ifGameOver)
            {
                if (!isStoped)
                {
                    SoundGame.Stop();
                    button3.Enabled = false;
                    timer1.Stop();
                    timer2.Stop();
                    Map = richTextBox1.Text;
                    richTextBox1.Select(541, 304);
                    richTextBox1.SelectedText = pauseText;
                }
                else
                {
                    button3.Enabled = true;
                    SoundGame.PlayLooping();
                    timer1.Start();
                    timer2.Start();
                    richTextBox1.Text = Map;
                }
                isStoped = !isStoped;
            }
        }
        void MoveSnakeBody()
        {
            bool doUpdate = true;   // false if bait eaten so dont move tail           

            for (int i = 0; i <= SnakeLength; i++)
            {
                if (i == SnakeLength)
                {
                    BehindTailLoc = OldLoc[i];
                }
                if (i != SnakeLength)
                {
                   
                    
                    if (ifBaitEaten && i==SnakeLength-1)        //extend snake
                    {
                        richTextBox1.Select(NewLoc[i], 1);
                        richTextBox1.SelectedText = "o";
                        OldLoc[i] = NewLoc[i];

                        OldLoc.Add(BehindTailLoc);
                        NewLoc.Add(OldLoc[SnakeLength]);
                        SnakeLength++;
                        ifBaitEaten=false;
                        label3.Focus();
                        PlaceBait();
                        doUpdate = false;

                    }
                    else
                    {
                        if (doUpdate)   // this is normal movement 
                        {
                            richTextBox1.Select(OldLoc[i], 1);
                            Temp = richTextBox1.SelectedText;
                            richTextBox1.SelectedText = GameBlock;
                            richTextBox1.Select(NewLoc[i], 1);
                            richTextBox1.SelectedText = Temp;
                        }
                        
                        OldLoc[i] = NewLoc[i];
                    }
                }
                if (i == 0)
                {
                    NewLoc[i] = HeadLoc;
                }  
                else
                    NewLoc[i] = OldLoc[i - 1];
            }
        }   // To move the snake body, extend it after bait eaten

        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Menu menuform=new Main_Menu();
            this.Dispose();
            menuform.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ifSoundOn)
            {
                SoundGame.Stop();
                button3.Image = Onimg;
                ifSoundOn = !ifSoundOn;
            }
            else
            {
                SoundGame.PlayLooping();
                button3.Image = Offimg;
                ifSoundOn = !ifSoundOn;
            }
        }

        void GameOver()          //apper game over text show HS and give option to restart or exit 
        {
            
            HIscore = Score + Time / 2;
            ScoreInsert scoreInsertFrom = new ScoreInsert(this);
            ifGameOver =true;
            SoundGame.Stop();
            SoundOver.Play();
            timer1.Stop();
            timer2.Stop();
            
            scoreInsertFrom.ShowDialog();
            //MessageBox.Show("   Game Over   \n High Score: " + HIscore.ToString());
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void richTextBox1_Click(object sender, EventArgs e)     // to change focus so cursor won be seen
        {
            ScoreLabel.Focus();
        }

        void CheckNextMove(string nextMove)     
        {
            if (nextMove == "o" || nextMove == "c" || nextMove == "|" || nextMove == "-")  //obstacle (game over)
            {
                GameOver();
            }
            if (nextMove == Bait) // bait (grov snake and add point to score)
            {
                Score += 5;
                ScoreLabel.Text = "Score: " + Convert.ToString(Score);
                ifBaitEaten = true;
            }
            else
                return; // just keep going      
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DirectionAllow = true;

            if (Direction == "Right")
            {
                MoveHead(1);
                MoveSnakeBody();
            }
            if (Direction == "Left")
            {
                MoveHead(-1);
                MoveSnakeBody();
            }
            if (Direction == "Up")
            {
                MoveHead(-60);
                MoveSnakeBody();
            }
            if (Direction == "Down")
            {
                MoveHead(60);
                MoveSnakeBody();
            }
        }  // EVERYTHING 

        private void timer2_Tick(object sender, EventArgs e)   //time
        {
            Time++;
            timerLabel.Text = "Time: " + Time.ToString();
            if(Time%30==0 && Time<=121)
            {
                timer1.Interval -= 50;
            }


        } // Timer shows the time add final score 
          // after some time increase the snake speed
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            //prevent error sound for game controller keys
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.A || e.KeyCode== Keys.P)
            {
                e.SuppressKeyPress = true;
            }
            
            //Pause game
            if (e.KeyCode == Keys.P)          
            {
                StopGame();
            }      

            //changing direction
            if (DirectionAllow == true)         /* if user changed direction onetime before a movement- 
                                               dont allow a second time so we can prevent to 180 movement change */
                {
                if (e.KeyCode == Keys.W && Direction != "Down")
                {
                    Direction = "Up";
                    ArrowBox.Text = " \u2191";
                }
                if (e.KeyCode == Keys.A && Direction != "Right")
                {
                    Direction = "Left";
                    ArrowBox.Text = " \u2190";
                }

                if (e.KeyCode == Keys.D && Direction != "Left")
                {
                    Direction = "Right";
                    ArrowBox.Text = " \u2192";
                }
                if (e.KeyCode == Keys.S && Direction != "Up")
                {
                    Direction = "Down";
                    ArrowBox.Text = " \u2193";
                }
                DirectionAllow = false;      //prevent changing direciton a few times and doing 180 turn to eat itself
            }  
        }
    }
}
