using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    //Each object inherits this fuction, enables object behaviour to change on the fly with less impact in too many making changes
    //Drawback is to each object requires all fuctions assigned to it even though it is not used     
    interface IMove
    {
        void Move(GameObject go, bool left, bool right, bool up, bool down, int gameWidth, int gameHeight);
    }

    class ControlledHMove : IMove
    {
        //Method used to move object left and right only
        public void Move(GameObject go, bool left, bool right, bool up, bool down, int gameWidth, int gameHeight)
        {
            if (go.go_x < go.go_minXborder)
            {
                go.go_x = go.go_minXborder;
            }

            if (go.go_x > gameWidth - (go.go_w + go.go_minXborder))
            {
                go.go_x = gameWidth - (go.go_w + go.go_minXborder);
            }

            if (left)
            {
                go.go_x -= go.go_xvel;
                if (go.go_img_left != null)
                {
                    go.go_state = go.go_img_left;
                }
                
            }

            if (right)
            {
                go.go_x += go.go_xvel;
                if (go.go_img_right != null)
                {
                    go.go_state = go.go_img_right;
                }
                   
            }
        }
    }

    class AutoVMove : IMove
    {

        //Method to Move object Vertically Automatically
        //and create next object instance once reached end of screen 
        public void Move(GameObject go, bool left, bool right, bool up, bool down, int gameWidth, int gameHeight)
        {
            if (go.go_y < 0)
            {
                NextObject(go, gameWidth, gameHeight);
            }

            if (go.go_y > (gameHeight - go.go_minYborder))
            {
                NextObject(go, gameWidth, gameHeight);
            }

            go.go_y += go.go_yvel;
        }


        //Method to Create new object once at the bottom of the screen   
        private void NextObject(GameObject go, int gameWidth, int gameHeight)
        {
            Random r = new Random();
            go.go_x = r.Next(go.go_minXborder, (gameWidth) - (int.Parse(go.go_w.ToString()) + go.go_minXborder));
            go.go_y = r.Next(gameHeight) / 4;
        }
    }
}

