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
        public Bitmap go_state;                                                         // Current image state
        
        //Draw Image based on current image state
        public void Draw(Graphics g)
        {
            go_area = new RectangleF(go_x, go_y, go_w, go_h);
            g.DrawImage(go_state, go_area);
        }
    }    
}
