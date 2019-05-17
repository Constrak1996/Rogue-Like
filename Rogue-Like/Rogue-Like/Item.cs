using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class Item
    {
        private SQLiteConnection m_dbConnection;
        private const String CONNECTIONSTRING = @"Data Source=testtabel.db;version=3";
        public SpriteFont textFont;
        public Item()
        {
            m_dbConnection = new SQLiteConnection(CONNECTIONSTRING);
            m_dbConnection.Open();
        }
        public void itemStructure()
        {
            string sql = "CREATE TABLE IF NOT EXISTS fish (id INT, name VARCHAR(40), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        public void fillitemTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(1,'Brasen', 20)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(2,'Sterlet', 50)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(3,'Sturgeon', 40)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(4,'Bighead carp', 30)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(5,'White bream', 40)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(6,'Goldfish', 30)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(7,'Bullhead', 30)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO fish (id, name, score) VALUES(8,'Gudgeon', 50)";
            cmd.ExecuteNonQuery();

        }
        public String getscore(int id)
        {
            String sqlexpItem = "SELECT score FROM fish WHERE id ='" + id + "';";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpItem, m_dbConnection)
            {
                CommandText = sqlexpItem
            };
            // res = cmd.ExecuteScalar();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlItem = "";
            while (reader.Read())
            {
                sqlItem += reader["score"];
            }
            return sqlItem;
        }
    }
}
