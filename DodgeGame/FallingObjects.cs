using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace DodgeGame
{
    class FallingObjects : GameObject
    {
        public int fo_minvel;
        public float fo_yvel;
        public Bitmap fo_nor;
        
        public FallingObjects(int gameWidth, int gameHeight,Random objectrandom, Bitmap image)
        {
            this.go_minXborder = 50;
            this.go_minYborder = 20;
            this.go_w = 20;
            this.go_h = 20;
            this.go_x = objectrandom.Next(this.go_minXborder, (gameWidth) - (int.Parse(this.go_w.ToString()) + this.go_minXborder));       //Randomize X Position 
            this.go_y = objectrandom.Next(gameHeight) / 4;                                                                                //Randomize Y Postion, start it roughly at the top quarter of the screen
            fo_minvel = 3;
            fo_yvel = objectrandom.Next(8) + fo_minvel;                                                                                  //Randomize Y Speed at least contains minimum velocity 
            fo_nor = image;
            fo_nor.MakeTransparent(Color.White);
            this.go_state = fo_nor;
        }

        //Method to Move object Vertically Automatically
        //Create next object 
        public void AutoVMove(int gameWidth, int gameHeight)
        {
            if (this.go_y < 0)
            {
                NextObject(gameWidth, gameHeight);
            }

            if (this.go_y > (gameHeight - this.go_minYborder))
            {
                NextObject(gameWidth, gameHeight);
            }

            this.go_y += fo_yvel;
        }


        //Method to Create new object once at the bottom of the screen   
        private void NextObject(int gameWidth, int gameHeight)
        {
            Random r = new Random();
            this.go_x = r.Next(go_minXborder, (gameWidth) - (int.Parse(this.go_w.ToString()) + this.go_minXborder));
            this.go_y = r.Next(gameHeight) / 4;
        }
    }
}

