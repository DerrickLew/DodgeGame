using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DodgeGame
{
    class Assets
    {
        public Bitmap playnor;
        public Bitmap playleft;
        public Bitmap playright;
        public Bitmap raindrop;


        public void LoadAssets()
        {
            playnor = new Bitmap(DodgeGame.Properties.Resources.PlayerNor, 100,100);
            playleft = new Bitmap(DodgeGame.Properties.Resources.PlayerLeft, 100,100);
            playright = new Bitmap (DodgeGame.Properties.Resources.PlayerRight, 100,100);
            raindrop = DodgeGame.Properties.Resources.Raindrop;

            playnor.MakeTransparent(Color.White);

            playleft.MakeTransparent(Color.White);
            playright.MakeTransparent(Color.White);
            raindrop.MakeTransparent(Color.White);
            
        }

    }
}
