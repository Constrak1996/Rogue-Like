using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class Controller
    {
        private Model model;
        private Item item;
        
        /// <summary>
        /// The Controllers Constructor
        /// </summary>
        public Controller()
        {
            model = new Model();
            item = new Item();
            
            //Structure the Tables
            {
                model.highscoreStructure();
                item.itemStructure();
                
            }
            //Fill the Tables
            {
                //bait.fillBaitTable();
                //model.fillHighscoreTable();
                //item.fillitemTable();
            }
        }

        //Get the games HighScore
        public String getHighscore()
        {
            return model.getHighscore();
        }
        //Get the games BaitTabel
        //public String getItem()
        //{
        //    //return bait.getBait();
        //}
        //Get the games FishScore
        public String getItem(int id)
        {
            return item.getItem(id);
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
    }
}
