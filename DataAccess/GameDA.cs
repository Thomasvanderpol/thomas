using BusinessObject;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class GameDA : IGameDA

    {

        private IDb conn;
        public GameDA(IDb conn)
        {
            this.conn = conn;
        }


        public void BeginGame(List<int> players)
        {
            string query = "INSERT INTO Game(Player1ID, Player2ID, Player3ID, Player4ID) VALUES (@Player1ID, @Player2ID, @Player3ID, @Player4ID)";

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Player1ID", SqlDbType.Int);
            sqlParameters[0].Value = players[0];
            sqlParameters[1] = new SqlParameter("@Player2ID", SqlDbType.Int);
            sqlParameters[1].Value = players[1];
            sqlParameters[2] = new SqlParameter("@Player3ID", SqlDbType.Int);
            sqlParameters[2].Value = players[2];
            sqlParameters[3] = new SqlParameter("@Player4ID", SqlDbType.Int);
            sqlParameters[3].Value = players[3];

            conn.executeInsertQuery(query, sqlParameters);

        }

        public int GetCurrentGameID()
        {
            SqlDataReader dr;

            string query = "SELECT TOP 1 GameID FROM Game ORDER BY GameID DESC";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            int GameID = 0;
            while (dr.Read())
            {
                GameID = Convert.ToInt32(dr["GameID"]);
            }

            return GameID;
        }

        public List<int> GetPlayersIDs()
        {
            SqlDataReader dr;

            string query = "SELECT TOP 1 Player1ID, Player2ID, Player3ID, Player4ID FROM Game ORDER BY GameID DESC";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            List<int> GameID = new List<int>();
            while (dr.Read())
            {
                GameID.Add(Convert.ToInt32(dr["Player1ID"]));
                GameID.Add(Convert.ToInt32(dr["Player2ID"]));
                GameID.Add(Convert.ToInt32(dr["Player3ID"]));
                GameID.Add(Convert.ToInt32(dr["Player4ID"]));
            }

            return GameID;
        }

        public void SubmitChoice(int currentGameID, string bid, int playerID)
        {
            string query = "INSERT INTO Bids(GameTypeName, GameID, PlayerID) VALUES (@GameTypeName, @GameID, @PlayerID)";

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@GameTypeName", SqlDbType.VarChar);
            sqlParameters[0].Value = bid;
            sqlParameters[1] = new SqlParameter("@GameID", SqlDbType.Int);
            sqlParameters[1].Value = currentGameID;
            sqlParameters[2] = new SqlParameter("@PlayerID", SqlDbType.Int);
            sqlParameters[2].Value = playerID;
          

            conn.executeInsertQuery(query, sqlParameters);

        }
    }
}
