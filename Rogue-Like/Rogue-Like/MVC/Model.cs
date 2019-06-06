using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rogue_Like
{
    class Model
    {

        public static SQLiteConnection m_dbConnection;
        private const String CONNECTIONSTRING = @"Data Source=Roguetabel2.db;version=3"; //Acces the DataBase
        
        /// <summary>
        /// The Constructor of the model
        /// </summary>
        public Model()
        {
            m_dbConnection = new SQLiteConnection(CONNECTIONSTRING);
            if(m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
            }
            
            //Sørg for at der altid er en entry med ID == 1
            string sql = "INSERT or IGNORE into savechar(id, name, score, gold, food, health) VALUES(1,'peter', 0, 0, 0, 0); ";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }
        public void ItemStructure()
        {
            string sql = "CREATE TABLE IF NOT EXISTS item (id INT, name VARCHAR(40), Value INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }
        public void PlayerHealth()
        {
            string sql = "CREATE TABLE IF NOT EXISTS playerhealth (name VARCHAR(40), health INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        
        
        /// <summary>
        /// Constructs the HighScore in DataBase
        /// </summary>
        public void HighScoreStructure()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS highscores (id INTEGER PRIMARY KEY ASC, name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }
        public void CharacterTable()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS savechar (id INTEGER PRIMARY KEY ASC, name VARCHAR(20), score INT, gold INT, food INT, health INT, UNIQUE(id))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }



        public void FillCharTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = $"INSERT INTO savechar (id, name, score, gold, food, health) VALUES(null,'{Player.name}',{Player.dataScore},{Player.Coin},{Player.Food},{Player.health})";
            cmd.ExecuteNonQuery();
        }

        public void ThreadUpdate(int Food, int Coin, int DataScore, int health)

        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = $"UPDATE savechar set score = {Player.dataScore}, health = {Player.health}, food = {Player.Food}, gold = {Player.Coin};";
            cmd.ExecuteNonQuery();
        }

        public void FillItemTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(1,'Sword', 4)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(2,'Shield', 5)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(3,'Trinket', 40)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(4,'Gold', '0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(5,'Food', 0)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO item (id, name, Value) VALUES(6,'Bones', 0)";
            cmd.ExecuteNonQuery();


        }
        
        /// <summary>
        /// Fills the HighScore in DataBase
        /// </summary>
        public void FillHighScoreTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();

        }
        public String GetItem(int id)
        {
            String sqlexpItem = "SELECT Value FROM item WHERE id =" + id + "";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpItem, m_dbConnection)
            {
                CommandText = sqlexpItem
            };
            // res = cmd.ExecuteScalar();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String slqItem = string.Empty;
            while (reader.Read())
            {
                if (slqItem == string.Empty)
                {
                    slqItem += reader["Value"];
                }

            }

            return slqItem;

        }
        /// <summary>
        /// Get the HighScore from the DataBase
        /// </summary>
        /// <returns></returns>
        public String GetHighScore()
        {
            String sqlexpHeigscore = "SELECT * FROM highscores ORDER BY score DESC;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpHeigscore, m_dbConnection)
            {
                CommandText = sqlexpHeigscore
            };
            // res = cmd.ExecuteScalar();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlHighScore = string.Empty;
            while (reader.Read())
            {
                if (sqlHighScore == string.Empty)
                {
                    sqlHighScore += $"Name {Player.name}"+ "     " + $"Score {Player.score}" + Environment.NewLine;
                }
                
            }
            
            return sqlHighScore;
        }
        /// <summary>
        /// Get a new Score for a player
        /// </summary>
        public void NewPlayerScore()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = $"INSERT INTO highscores (id, name,score) VALUES(NULL,'name', 'score')";
            cmd.ExecuteNonQuery();
           
        }
        /// <summary>
        /// Get a new HighScore
        /// </summary>
        /// <returns></returns>
        public String GetNewHighScore()
        {
            String sqlexpPlayerscore = "SELECT * FROM highscores ORDER BY id DESC LIMIT 1;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpPlayerscore, m_dbConnection)
            {
                CommandText = sqlexpPlayerscore
            };
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlPlayerscore = "";
            while (reader.Read())
            {
                sqlPlayerscore += "Name: " + reader["name"] + "     " + "Score:" + reader["score"];
            }
            return sqlPlayerscore;
        }
        /// <summary>
        /// Get the Best HighScore
        /// </summary>
        /// <returns></returns>
        public String GetBestHighScore()
        {
            String sqlexpPlayerscore = "SELECT * FROM highscores ORDER BY score DESC LIMIT 1;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpPlayerscore, m_dbConnection)
            {
                CommandText = sqlexpPlayerscore
            };
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlPlayerscore = "";
            while (reader.Read())
            {
                sqlPlayerscore += "Name: " + reader["name"] + "     " + "Score:" + reader["score"];
            }
            
            return sqlPlayerscore;
        }
        /// <summary>
        /// Update Score
        /// </summary>
        /// <returns></returns>
        public String GetUpdateNewScore()
        {
            String sqlexpPlayerscore = "SELECT score FROM highscores ORDER BY id DESC LIMIT 1;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpPlayerscore, m_dbConnection)
            {
                CommandText = sqlexpPlayerscore
            };
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlPlayerscore = "";
            while (reader.Read())
            {
                sqlPlayerscore += reader["score"];
            }
            
            return sqlPlayerscore;
        }
        /// <summary>
        /// Skulle updatatere player score løbende, men bliv ikke helt færdig da den ikke for valuen fra vores Hooked klasse.
        /// </summary>
        /// <returns></returns>
        public String UpdatePlayerScore()
        {
            String sqlexpPlayerscore = "UPDATE highscores SET score = score + {value} ORDER BY id DESC LIMIT 1;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpPlayerscore, m_dbConnection)
            {
                CommandText = sqlexpPlayerscore
            };
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlPlayerscore = "";
            while (reader.Read())
            {
                sqlPlayerscore += reader["score"];
            }
            
            return sqlPlayerscore;
        }
        public void QuitGame()
        {
            m_dbConnection.Close();
        }
    }
}
