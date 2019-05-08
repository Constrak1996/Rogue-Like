using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public class Controller
    {
        private Model model;
        /// <summary>
        /// The Controllers Constructor
        /// </summary>
        public Controller()
        {
            model = new Model();
            //Structure the Tables
            {
                model.highscoreStructure();
            }
            //Fill the Tables
            {
                
                //model.fillHighscoreTable();
                
            }
            
        }
        public void newPlayer()
        {
            model.newPlayerScore();
        }
        //Get the games HighScore
        public String getHighscore()
        {
            return model.getHighscore();
        }
        public String getNewHighscore()
        {
            return model.getNewHighScore();
        }
        public String getBestscore()
        {
            return model.getBestHeighscore();
        }
        public String getPlayerScore()
        {
            return model.getUpdateNewScore();
        }

    }
}