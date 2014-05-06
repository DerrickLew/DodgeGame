using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace DodgeGame
{
    class FallingObjects : GameObject
    {
        public int fo_minvel;                             //minimum velocity falling object should fall
        
        //set attributes to a falling object instance
        public FallingObjects(int gameWidth, int gameHeight, int width, int height, Random objectrandom, Bitmap image)
            : base(new Visible(), new AutoVMove(), new FallingObjectCollisions())
        {
            this.go_minXborder = 50;
            this.go_minYborder = 30;
            this.go_w = width;
            this.go_h = height;
            this.go_x = objectrandom.Next(this.go_minXborder, (gameWidth) - (int.Parse(this.go_w.ToString()) + this.go_minXborder));      //Randomize X Position 
            this.go_y = objectrandom.Next(gameHeight) / 4;                                                                                //Randomize Y Postion, start it roughly at the top quarter of the screen
            fo_minvel = 3;
            this.go_yvel = objectrandom.Next(8) + fo_minvel;                                                                              //Randomize Y Speed at least contains minimum velocity 
            this.go_nor = image;
            this.go_nor.MakeTransparent(Color.White);
            this.go_state = this.go_nor;
        }
    }
}

