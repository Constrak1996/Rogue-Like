using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Rogue_Like
{
    static class Autosave
    {
        static Model myDB;

        public static int Coin;
        public static int Food;
        public static int DataScore;
        public static int health;


        static void UpdateT()
        {
            Thread t = new Thread(AutoSave);
            t.IsBackground = true;
            t.Start();
        }

        static void AutoSave()
        {
            while (true)
            {
                myDB.ThreadUpdate(Food, Coin, DataScore, health);


                Thread.Sleep(500);

            }



        }

    }
}
