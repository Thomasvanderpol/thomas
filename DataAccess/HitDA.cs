using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class HitDA : IHitDA
    {
        private IDb conn;
        public HitDA(IDb conn)
        {
            this.conn = conn;
        }

        public void AddCard(int currentGameID, int playerID, int cardID, int hitID)
        {
            string query = "INSERT INTO CardPlayer(HitID, PlayerID, GameID, CardID) VALUES (@HitID, @PlayerID, @GameID, @CardID)";

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@HitID", SqlDbType.Int);
            sqlParameters[0].Value = hitID;
            sqlParameters[1] = new SqlParameter("@PlayerID", SqlDbType.Int);
            sqlParameters[1].Value = playerID;
            sqlParameters[2] = new SqlParameter("@GameID", SqlDbType.Int);
            sqlParameters[2].Value = currentGameID;
            sqlParameters[3] = new SqlParameter("@CardID", SqlDbType.Int);
            sqlParameters[3].Value = cardID;


            conn.executeInsertQuery(query, sqlParameters);
        }

        public void addHitInGame(int currentGameID)
        {
            string query = "INSERT INTO Hit(GameID) VALUES (@GameID)";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@GameID", SqlDbType.Int);
            sqlParameters[0].Value = currentGameID;
          
            conn.executeInsertQuery(query, sqlParameters);
        }

        public int GetCard(string cardPlayer)
        {
            SqlDataReader dr;

            string query = "SELECT CardID FROM Cards WHERE CardName = '" + cardPlayer + "'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            int Card = 0;
            while (dr.Read())
            {
                Card = Convert.ToInt32(dr["CardID"]);
            }

            return Card;
        }

        public List<int> getCardsByHit(int currentGameID, int hitID)
        {
            SqlDataReader dr;

            string query = "SELECT CardID FROM CardPlayer WHERE GameID = " + currentGameID + "AND HitID = " + hitID;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            List<int> CardsinHit = new List<int>();
            while (dr.Read())
            {
                int CardID = Convert.ToInt32(dr["CardID"]);
                CardsinHit.Add(CardID);
            }

            return CardsinHit;
        }

        public int GetLastHitInGame(int currentGameID)
        {
            SqlDataReader dr;

            string query = "SELECT top 1 HitID FROM Hit WHERE GameID = " + currentGameID + "Order by HitID desc";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            int hitID = 0;
            while (dr.Read())
            {
                hitID = Convert.ToInt32(dr["HitID"]);
            }

            return hitID;
        }

        public bool HitInGame(int currentGameID)
        {
            SqlDataReader dr;

            string query = "SELECT * FROM Hit WHERE GameID = " + currentGameID;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            bool hitInGame = false;
            while (dr.Read())
            {
                hitInGame = true;
            }

            return hitInGame;
        }
    }
}
