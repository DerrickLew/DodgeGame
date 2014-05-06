using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    class GameObject
    {
        public int go_minXborder;                                                       // Constraint for Y position
        public int go_minYborder;                                                       // Constraint for X position
        public float go_x, go_y;                                                        // X Position, Y Position  
        public float go_w, go_h;                                                        // Width, Height
        public RectangleF go_area;                                                      // Rectangle Box used for Image     
        public float go_xvel;                                                           // X Velocity
        public float go_yvel;                                                           // Y Velocity 
        public Bitmap go_state;                                                         // Current image state
        public Bitmap go_nor;                                                           // Image used for normal state
        public Bitmap go_img_left, go_img_right;                                        // Image used for left and right animation
        
        readonly IVisible go_v;
        readonly IMove go_m;
        readonly ICollidable go_c;

        //Inherits these classes for function purpose
        //Show, Move, and Collide
        public GameObject( IVisible v , IMove m , ICollidable c )
       {
             go_v = v;
             go_m = m;
             go_c = c;
        }


        //Standard method used to move object
        public void Move(GameObject go, bool left, bool right, bool up, bool down, int gameWidth, int gameHeight)
        {
            go_m.Move(go, left, right, up, down, gameWidth, gameHeight);
        }

        //Standard method used to for object collisions
        public void Collide(GlobalVariables gv, GameObject go1, GameObject go2)
        {
            go_c.Collide(gv, go1, go2);
        }

        //Standard method used to for draw object to screen
        public void Draw(Graphics g)
        {
            go_area = new RectangleF(go_x, go_y, go_w, go_h);
            go_v.Draw(g, go_state, go_area);
        }

        //Returns image back to normal state
        public void Returnstate()
        {
            this.go_state = this.go_nor;
        }
    }
}
