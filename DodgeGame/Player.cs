using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    class Player : GameObject
    {
        public int player_index;                          // track what player is used
        
        //set attributes to a player instance
        public Player(int gameWidth, int gameHeight, int playerind, int width, int height, Random playerrandom, Bitmap imagenor, Bitmap imageleft, Bitmap imageright)
            : base(new Visible(), new ControlledHMove(), new PlayerCollisions())
        {
            this.go_minXborder = 15;
            this.go_minYborder = 50;
            this.go_w = width;
            this.go_h = height;
            this.go_x = playerrandom.Next(this.go_minXborder, (gameWidth) - (int.Parse(this.go_w.ToString()) + this.go_minXborder));
            this.go_y = gameHeight - this.go_h - this.go_minYborder;
            player_index = playerind;
            this.go_xvel = 10;
            this.go_nor = imagenor;
            this.go_nor.MakeTransparent(Color.White);
            this.go_img_left = imageleft;
            this.go_img_left.MakeTransparent(Color.White);
            this.go_img_right = imageright;
            this.go_img_right.MakeTransparent(Color.White);
            this.go_state = this.go_nor;
        }
    }
}

