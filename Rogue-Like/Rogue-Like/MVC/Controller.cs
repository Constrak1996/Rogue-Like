using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class Controller
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
                model.itemStructure();
                model.characterTable();
                
            }
            //Fill the Tables
            {

                model.fillHighscoreTable();
                model.fillitemTable();
            }
        }

        //Get the games HighScore
        public String getHighscore()
        {
            return model.getHighscore();
        }
       
        //Get a item from the itemsList
        public string getItem(int id)
        {
            return model.getItem(id);
        }

        public String getNewHighscore()
        {
            return model.getNewHighScore();
        }
        public void newPlayer()
        {
            model.newPlayerScore();
        }
        public String getBestscore()
        {
            return model.getBestHeighscore();
        }
        public String getPlayerScore()
        {
            return model.getUpdateNewScore();
        }

        public void SaveChar()
        {
            while (true)
            {
                model.ThreadUpdate(Player.Food, Player.Coin, Player.DataScore, Player.health);


                Thread.Sleep(500);

            }
        }
    }
}
