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
                //bait.fillBaitTable();
                //model.fillHighscoreTable();
                //fish.fillfishTable();
            }
            
        }
        public void newPlayer()
        {
            model.newPlayerScore();
        }

    }
}