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
        
        public Item()
        {
            m_dbConnection = new SQLiteConnection(CONNECTIONSTRING);
            m_dbConnection.Open();
        }
        public void itemStructure()
        {
            string sql = "CREATE TABLE IF NOT EXISTS item (id INT, name VARCHAR(40), Value INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        public void fillitemTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(1,'Sword', 4)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(2,'Shield', 5)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(3,'Trinket', 40)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(4,'Gold', '1')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(5,'Food', 1)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(6,'Bones', 0)";
            cmd.ExecuteNonQuery();
            
        }
        public String getItem(int id)
        {
            String sqlexpItem = "SELECT Value FROM item WHERE id =" + id + ""; 
              SQLiteCommand cmd = new SQLiteCommand(sqlexpItem, m_dbConnection)
            {
                CommandText = sqlexpItem
            };
            // res = cmd.ExecuteScalar();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String slqItem = "";
            while (reader.Read())
            {
                slqItem += reader["Value"];
                break;
            }
            return slqItem;
        }
    }
}
