using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    class Player : GameObject
    {
        public float player_xvel;                          //players movement speed set the a certain value not something you really want to change, leave for now  
        public Bitmap player_nor;                          // image used for player normal state
        public Bitmap player_left, player_right;          // image used for player left and righ
        public int player_index;                          // track what player is used
        
        
        //set attributes to a player instance
        public Player(int gameWidth, int gameHeight,int playerind, int width,int height, Random playerrandom,Bitmap imagenor, Bitmap imageleft, Bitmap imageright)
        {
            this.go_minXborder = 15;
            this.go_minYborder = 50;
            this.go_w = width;
            this.go_h = height;
            this.go_x = playerrandom.Next(this.go_minXborder, (gameWidth) - (int.Parse(this.go_w.ToString()) + this.go_minXborder));
            this.go_y = gameHeight - this.go_h - this.go_minYborder;
            player_index = playerind;
            player_xvel = 10;
            player_nor = imagenor;
            player_nor.MakeTransparent(Color.White);
            player_left = imageleft;
            player_left.MakeTransparent(Color.White);
            player_right = imageright;
            player_right.MakeTransparent(Color.White);
            this.go_state = player_nor;     
        }

        //Return back to normal state
        public void Returnstate()
        {
            this.go_state = player_nor;
        }

        //Method used to move player left and right
        public void ControlledHMove(bool left, bool right, int gameWidth, int gameHeight)
        {
            if (this.go_x < this.go_minXborder)
            {
                this.go_x = this.go_minXborder;
            }

            if (this.go_x > gameWidth - (this.go_w + this.go_minXborder))
            {
                this.go_x = gameWidth - (this.go_w + this.go_minXborder);
            }

            if (left)
            {
                this.go_x -= player_xvel;
                this.go_state = player_left;
                
            }

            if (right)
            {
                this.go_x += player_xvel;
                this.go_state = player_right;
                
            }
        }


    }
}

