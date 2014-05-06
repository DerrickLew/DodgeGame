using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DodgeGame
{
    //Each object inherits this fuction, enables object behaviour to change on the fly with less impact in too many making changes
    //Drawback is to each object requires all fuctions assigned to it even though it is not used     
    interface ICollidable
    {
        void Collide(GlobalVariables gv, GameObject go1, GameObject go2);
    }

    class PlayerCollisions : ICollidable
    {
        public void Collide(GlobalVariables gv, GameObject go1, GameObject go2)
        {
            //this is an end game collision when player gets hit by falling object
            if (go1.go_area.IntersectsWith(go2.go_area))
            {
                if (gv.gamerun)
                {
                    if (gv.score > gv.highscore)
                    {
                        gv.highscore = gv.score;
                        DodgeGame.Properties.Settings.Default.HighScore = gv.highscore;
                        DodgeGame.Properties.Settings.Default.Save();
                    }
                    MessageBox.Show("Game Over - Your Score: " + gv.score + " High Score: " + gv.highscore);
                    gv.player1left = false;
                    gv.player1right = false;
                    go1.Returnstate();
                    gv.gamerun = false;
                }
            }
        }

    }

    class FallingObjectCollisions : ICollidable
    {
        public void Collide(GlobalVariables gv, GameObject go1, GameObject go2)
        {
        }
    }

    
}
