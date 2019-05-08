using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Rogue_Like
{
    public class Model
    {
        private SQLiteConnection m_dbConnection;
        private const String CONNECTIONSTRING = @"Data Source=Rogue-Like.db;version=3"; //Acces the DataBase
        public SpriteFont textFont;

        /// <summary>
        /// The Constructor of the model
        /// </summary>
        public Model()
        {
            m_dbConnection = new SQLiteConnection(CONNECTIONSTRING);
            m_dbConnection.Open();
        }
        /// <summary>
        /// Constructs the HighScore in DataBase
        /// </summary>
        public void highscoreStructure()
        {
            string sql = "CREATE TABLE IF NOT EXISTS highscores (id INTEGER PRIMARY KEY ASC,name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Fills the HighScore in DataBase
        /// </summary>
        public void fillHighscoreTable()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO highscores (id,name, score) VALUES(NULL,'Alex', 4000)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO highscores (id,name, score) VALUES(NULL,'Peter', 5000)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO highscores (id,name, score) VALUES(NULL,'Mikkel', 2500)";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Get the HighScore from the DataBase
        /// </summary>
        /// <returns></returns>
        public String getHighscore()
        {
            String sqlexpHeigscore = "SELECT * FROM highscores ORDER BY score DESC;";
            SQLiteCommand cmd = new SQLiteCommand(sqlexpHeigscore, m_dbConnection)
            {
                CommandText = sqlexpHeigscore
            };
            // res = cmd.ExecuteScalar();
            SQLiteDataReader reader;
            reader = cmd.ExecuteReader();

            String sqlHigscore = "";
            while (reader.Read())
            {
                sqlHigscore += "Name: " + reader["name"] + "     " + "Score:" + reader["score"] + Environment.NewLine;
            }
            return sqlHigscore;
        }
        /// <summary>
        /// Get a new Score for a player
        /// </summary>
        public void newPlayerScore()
        {
            SQLiteCommand cmd = m_dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO highscores (id,name, score) VALUES(NULL,'Player', 0)";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Get a new HighScore
        /// </summary>
        /// <returns></returns>
        public String getNewHighScore()
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
        public String getBestHeighscore()
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
        public String getUpdateNewScore()
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
        public String updatePlayerScore()
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
    }
}