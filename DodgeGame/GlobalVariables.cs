using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DodgeGame
{
    //Global variables used through out program
    public class GlobalVariables
    {
        public bool player1left;                                             //trigger used to see if player1 as moved left 
        public bool player1right;                                            //trigger used to see if player1 as moved right  
        public bool gamerun;                                                 //trigger to start game
        public int score;                                                    //Actual score
        public int highscore;                                                //Actual highscore                      
        public Keys player1leftkey;                                          //Set player1 keys 
        public Keys player1rightkey;
        public Keys player1startkey;
    }
}
