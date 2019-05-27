using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class Login
    {
        private SQLiteConnection m_dbConnection;
        private const String CONNECTIONSTRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kristoffer\Desktop\DataBaseProject\Rogue-Like\Rogue-Like\Rogue-Like\DataBase.mdf;Integrated Security=True"; //Acces the DataBase
        public SpriteFont textFont;

        public Login()
        {
            m_dbConnection = new SQLiteConnection(CONNECTIONSTRING);
            m_dbConnection.Open();
        }

        public void LoginStructure()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS Logintable (id INTEGER PRIMARY KEY ASC, name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
