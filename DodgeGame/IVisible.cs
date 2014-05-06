using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    //Each object inherits this fuction, enables object behaviour to change on the fly with less impact in too many making changes
    //Drawback is to each object requires all fuctions assigned to it even though it is not used     
    interface IVisible
    {
        void Draw(Graphics g, Bitmap go_state, RectangleF go_area);
    }

    class Visible : IVisible
    {
        //Method used to draw objects to screen
        public void Draw(Graphics g, Bitmap go_state, RectangleF go_area)
        {
            g.DrawImage(go_state, go_area);
        }
    }
}
