using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace DodgeGame
{
    public partial class MainScreen : Form
    {
        private const int fps = 60;
        private const int maxfallobjs1 = 13;
        private const int scoreinterval = 10;
        private const float movelocation = 20;
        
        private bool player1left;
        private bool player1right;
        private bool gamerun = false;
        private int scoretimer;
        private int score;
        private int highscore;
        private Player player1;
        private List<FallingObjects> fallobjs;
        private Task task;
        private Graphics gamegraphics;
        private Random playerrandom = new Random();
        private Random objectrandom = new Random();
        private Keys player1leftkey;
        private Keys player1rightkey;
        private Keys player1startkey;


        public MainScreen()
        {
            InitializeComponent();
            LoadSettings();
            LoadStartPage();
        }

        private void LoadSettings()
        {
            highscore = DodgeGame.Properties.Settings.Default.HighScore;
            player1leftkey = DodgeGame.Properties.Settings.Default.Player1LeftKey;
            player1rightkey = DodgeGame.Properties.Settings.Default.Player1RightKey;
            player1startkey = DodgeGame.Properties.Settings.Default.Startkey;
        }

        private void LoadStartPage()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(BackScreenPaint);
        }

        private void BackScreenPaint(object sender, PaintEventArgs e)
        {
                Graphics startscreen = e.Graphics;
                startscreen.Clear(Color.FromArgb(94, 228, 255));
                startscreen.DrawString("Dodge Game", new Font("Arial", 40), Brushes.Black, new PointF((this.Width / 3), (this.Height)/ 8));
                startscreen.DrawString("Avoid raindrops by moving left and right " + "(" + player1leftkey.ToString() + " and " + player1rightkey.ToString() + ")", new Font("Arial", 20), Brushes.Black, new PointF((this.Width / 6), ((this.Height) / 4) + movelocation));
                startscreen.DrawString("The more you move the greater the score reward"   , new Font("Arial", 20), Brushes.Black, new PointF((this.Width / 6), (this.Height) / 3));
                startscreen.DrawString("Press " + player1startkey.ToString() + " to begin ", new Font("Arial", 40), Brushes.Black, new PointF((this.Width / 4), ((this.Height /2) + movelocation)));      
        }
         
        

        protected void Run()
        {
            while (gamerun)
            {

                player1.ControlledHMove(player1left, player1right, this.Width, this.Height);
                foreach (FallingObjects fallobj in fallobjs)
                {
                   fallobj.AutoVMove(this.Width, this.Height);
                    if (player1.go_area.IntersectsWith(fallobj.go_area))
                    {
                        if (gamerun)
                        {
                            if (score > highscore)
                            {
                                highscore = score;
                                DodgeGame.Properties.Settings.Default.HighScore = highscore;
                                DodgeGame.Properties.Settings.Default.Save();
                            }
                            MessageBox.Show("Game Over - Your Score: " + score + " High Score: " + highscore);
                            player1left = false;
                            player1right = false;
                            player1.Returnstate();
                            gamerun = false;
                        }
                    }
                    
                }

                scoretimer += 1;
                if (scoretimer == scoreinterval)
                {
                    score += 1;
                    scoretimer = 0;
                }

                this.Invalidate();
                Thread.Sleep(1000 / fps);
            }
            
        }

        
        
        private void MainScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == player1leftkey)
            {
                if (player1left == false)
                {
                    score += 10;
                }
                player1left = true;
            }

            if (e.KeyCode == player1rightkey)
            {
               if (player1right == false)
               {
                    score += 10;
               }
               player1right = true;
            }

         
            if(e.KeyCode ==  player1startkey)
            {
                 if (!gamerun)
                {
                    score = 0;
                    player1 = new Player(this.Width, this.Height, playerrandom, DodgeGame.Properties.Resources.PlayerNor, DodgeGame.Properties.Resources.PlayerLeft, DodgeGame.Properties.Resources.PlayerRight);
                    fallobjs = new List<FallingObjects>();
                    for (int i = 0; i < maxfallobjs1; i++)
                    {
                        fallobjs.Add(new FallingObjects(this.Width, this.Height,objectrandom, DodgeGame.Properties.Resources.Raindrop));
                    }
                    gamerun = true;
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainScreen_Paint);
                    task = new Task(Run);
                    task.Start();
                }
            }

        }

                
        private void MainScreen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                player1right = false;
                player1.Returnstate();
            }

            if (e.KeyCode == Keys.Left)
            {
                player1left = false;
                player1.Returnstate();
            }
   
        }
        
        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            if (gamerun)
            {
                gamegraphics = e.Graphics;
                gamegraphics.Clear(Color.FromArgb(94, 228, 255));
                if (score > highscore)
                {
                    gamegraphics.DrawString("Score: " + score.ToString(), new Font("Arial", 15), Brushes.Red, new PointF(10, 10));
                }
                else
                {
                    gamegraphics.DrawString("Score: " + score.ToString(), new Font("Arial", 15), Brushes.Black, new PointF(10, 10));
                }


                foreach (FallingObjects fallobj in fallobjs)
                {
                    fallobj.Draw(gamegraphics);
                }
                player1.Draw(gamegraphics);
                
            }
            
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        
        

        

    }
}
