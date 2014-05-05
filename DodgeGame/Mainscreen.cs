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
        private const int fps = 60;                                           //FPS per second for loop
        private const int maxfallobjs1 = 13;                                  //number of the same falling objects
        private const int scoreinterval = 10;                                 //used to slow score speed
        private bool player1left;                                             //trigger used to see if player1 as moved left 
        private bool player1right;                                            //trigger used to see if player1 as moved right  
        private bool gamerun = false;                                         //trigger to start game
        private int scoretimer;                                               //used in conjuction with score interval  
        private int score;                                                    //Actual score
        private int highscore;                                                //Actual highscore                      
        private List<Player> players;                                         //List for Player objects
        private List<FallingObjects> fallobjs;                                //List for Falling objects
        private Task task;                                                    //Task variable
        private Graphics gamegraphics;                                        //Graphic variable
        private Random playerrandom = new Random();                           //Used randomize player start position
        private Random objectrandom = new Random();                           //Used randomize a few thing in Falling Objects
        private Keys player1leftkey;                                          //Set player1 keys 
        private Keys player1rightkey;
        private Keys player1startkey;


        public MainScreen()
        {
            InitializeComponent();
            LoadSettings();
            LoadStartPage();
        }

        //Set Highscore and Player1 keys
        private void LoadSettings()
        {
            highscore = DodgeGame.Properties.Settings.Default.HighScore;
            player1leftkey = DodgeGame.Properties.Settings.Default.Player1LeftKey;
            player1rightkey = DodgeGame.Properties.Settings.Default.Player1RightKey;
            player1startkey = DodgeGame.Properties.Settings.Default.Startkey;
        }

        //Create a separate paint handler so startup screen can show
        private void LoadStartPage()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(BackScreenPaint);
        }

        //Draw the start up screen made it dynamic to cater of screen resolution change
        //will think about create a menu screen if there is time
        private void BackScreenPaint(object sender, PaintEventArgs e)
        {
                Graphics startscreen = e.Graphics;
                startscreen.Clear(Color.FromArgb(94, 228, 255));
                startscreen.DrawString("Dodge Game", new Font("Arial", 40), Brushes.Black, new PointF((this.Width / 3), (this.Height)/ 8));
                startscreen.DrawString("Avoid raindrops by moving left and right " + "(" + player1leftkey.ToString() + " and " + player1rightkey.ToString() + ")", new Font("Arial", 20), Brushes.Black, new PointF((this.Width / 6), ((this.Height) / 4)));
                startscreen.DrawString("The more you move the greater the score reward"   , new Font("Arial", 20), Brushes.Black, new PointF((this.Width / 6), (this.Height) / 3));
                startscreen.DrawString("Press " + player1startkey.ToString() + " to begin ", new Font("Arial", 40), Brushes.Black, new PointF((this.Width / 4), ((this.Height /2))));      
        }
         
        //Endless loop while game activate
        protected void Run()
        {
            while (gamerun)
            {
                //Method used to move the player for now only player 1
                foreach (var player in players)
                {
                    if (player.player_index == 1)
                    {
                        player.ControlledHMove(player1left, player1right, this.Width, this.Height);
                    }
                    
                }

                //Method used to move falling objects
                //and end game and loop to if falling objects to collides with players
                //Save new highscore if there in one 
                foreach (var fallobj in fallobjs)
                {
                   fallobj.AutoVMove(this.Width, this.Height);
                   foreach (var player in players)
                   {
                       if (player.go_area.IntersectsWith(fallobj.go_area))
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
                               player.Returnstate();
                               gamerun = false;
                           }
                       }
                   }  
                }
                //the intial speed at which this runs was too fast for direct score
                //slowed it down for score per second
                scoretimer += 1;
                if (scoretimer == scoreinterval)
                {
                    score += 1;
                    scoretimer = 0;
                }
                //Refreshes the actions
                this.Invalidate();
                //Prevents the game from running too fast set to 60 fps
                Thread.Sleep(1000 / fps);
            }
            
        }

        
        
        private void MainScreen_KeyDown(object sender, KeyEventArgs e)
        {
            // Activate the move left trigger by using a bool value 
            // Give the player only some points for 1 left move    
            if (e.KeyCode == player1leftkey)
            {
                if (player1left == false)
                {
                    score += 10;
                }
                player1left = true;
            }

            // Activate the move right trigger by using a bool value 
            // Give the player only some points for 1 right move    
            if (e.KeyCode == player1rightkey)
            {
               if (player1right == false)
               {
                    score += 10;
               }
               player1right = true;
            }

            //Press space to start
            if(e.KeyCode ==  player1startkey)
            {
                 if (!gamerun)
                {
                    //intialize score for each game
                    score = 0;
                    //Only create 1 player within the player list, future use you can create more 
                    players = new List<Player>();
                    players.Add(new Player(this.Width, this.Height,1,80,80,playerrandom, DodgeGame.Properties.Resources.PlayerNor, DodgeGame.Properties.Resources.PlayerLeft, DodgeGame.Properties.Resources.PlayerRight));
                    
                  
                    //Create several of the same object with the falling objects list  
                    fallobjs = new List<FallingObjects>();
                    for (int i = 0; i < maxfallobjs1; i++)
                    {
                        fallobjs.Add(new FallingObjects(this.Width, this.Height,objectrandom, DodgeGame.Properties.Resources.Raindrop));
                    }
                    
                    //trigger to disable space bar as game has started
                    gamerun = true;
                    
                    //create th paint handler so the objects can be shown
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainScreen_Paint);
                    
                    // start an endless loop while game activate
                    task = new Task(Run);
                    task.Start();
                }
            }
        }

                
        private void MainScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //if players left key is not used stop the player and show the orginal state 
            //use the player index to determine should player to stop for now hard code we'll see later
            if (e.KeyCode == player1leftkey)
            {
                player1left = false;
                foreach (var player in players)
                {
                    if (player.player_index == 1)
                    {
                        player.Returnstate();
                    }
                }
            }

            //if players right key is not used stop the player and show the orginal state
            //use the player index to determine should player to stop for now hard code we'll see later
            if (e.KeyCode == player1rightkey)
            {
                player1right = false;
                foreach (var player in players)
                {
                    if (player.player_index == 1)
                    {
                        player.Returnstate();
                    }
                }
            }
        }
        
        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            if (gamerun)
            {
                //Use the in built in graphics to display the objects 
                gamegraphics = e.Graphics;
                gamegraphics.Clear(Color.FromArgb(94, 228, 255));

                // if highscore reached change the colour in the score to red
                if (score > highscore)
                {
                    gamegraphics.DrawString("Score: " + score.ToString(), new Font("Arial", 15), Brushes.Red, new PointF(10, 10));
                }
                else
                {
                    gamegraphics.DrawString("Score: " + score.ToString(), new Font("Arial", 15), Brushes.Black, new PointF(10, 10));
                }

                //Draw each fall object on screen
                foreach (var fallobj in fallobjs)
                {
                    fallobj.Draw(gamegraphics);
                }

                //Draw each player on screen
                foreach (var player in players)
                {
                    player.Draw(gamegraphics);
                }
                
            }
            
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Exit the application
            Application.Exit();
        }

    }
}
