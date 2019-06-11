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
                model.HighScoreStructure();
                model.ItemStructure();
                model.CharacterTable();
                
                
            }
            //Fill the Tables
            {

                model.FillHighScoreTable();
                model.FillItemTable();
                model.FillCharTable();
            }
        }

        //Get the games HighScore
        public String GetHighScore()
        {
            return model.GetHighScore();
        }
       
        //Get a item from the itemsList
        public string GetItem(int id)
        {
            return model.GetItem(id);
        }

        public String GetNewHighScore()
        {
            return model.GetNewHighScore();
        }
        public void NewPlayer()
        {
            model.NewPlayerScore();
        }
        public String GetBestScore()
        {
            return model.GetBestHighScore();
        }
        public String GetPlayerScore()
        {
            return model.GetUpdateNewScore();
        }
       /// <summary>
       /// Acces the savechar table and updates it
       /// </summary>
        public void SaveChar()
        {
            while (GameWorld.isPlaying)
            {
                model.ThreadUpdate(Player.myFood, Player.myCoin, Player.myScore, Player.currentHealth);


                Thread.Sleep(500);

            }
        }
    }
}
