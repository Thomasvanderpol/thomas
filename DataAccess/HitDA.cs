using BusinessObject;
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
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }

        }

        public void addHitInGame(int currentGameID)
        {
            try
            {
                string query = "INSERT INTO Hit(GameID) VALUES (@GameID)";

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@GameID", SqlDbType.Int);
                sqlParameters[0].Value = currentGameID;

                conn.executeInsertQuery(query, sqlParameters);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public int GetAllHits(int currentGameID, int PlayerID)
        {
            try
            {
                SqlDataReader dr;

                string query = "SELECT COUNT(HitID) AS aantalslagen FROM Hit WHERE GameID = " + currentGameID + " AND WinPlayerID IS NOT NULL AND WinPlayerID = " + PlayerID;
                SqlParameter[] sqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(query, sqlParameters);
                int PlayerHits = 0;
                while (dr.Read())
                {
                    PlayerHits = Convert.ToInt32(dr["aantalslagen"]);
                }

                return PlayerHits;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<AllHitsBO> GetAllHitsByPlayer(int playerID)
        {
            try
            {
                SqlDataReader dr;

                string query = "SELECT COUNT(HitID)AS AantalSlagen , GameID FROM dbo.Hit WHERE WinPlayerID = " + playerID + " GROUP BY GameID";
                SqlParameter[] sqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(query, sqlParameters);
                List<AllHitsBO> AllHits = new List<AllHitsBO>();
                while (dr.Read())
                {
                    AllHitsBO hits = new AllHitsBO();
                    hits.AllHits = Convert.ToInt32(dr["AantalSlagen"]);
                    hits.GameID = Convert.ToInt32(dr["GameID"]);
                    AllHits.Add(hits);
                }

                return AllHits;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public int GetCard(string cardPlayer)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<CardPlayerBO> getCardsByHit(int currentGameID, int hitID)
        {
            try
            {
                SqlDataReader dr;

                string query = "SELECT * FROM CardPlayer WHERE GameID = " + currentGameID + "AND HitID = " + hitID;
                SqlParameter[] sqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(query, sqlParameters);
                List<CardPlayerBO> CardsinHit = new List<CardPlayerBO>();
                while (dr.Read())
                {
                    CardPlayerBO card = new CardPlayerBO();
                    card.CardPlayerID = Convert.ToInt32(dr["CardPlayerID"]);
                    card.HitID = Convert.ToInt32(dr["HitID"]);
                    card.PlayerID = Convert.ToInt32(dr["PlayerID"]);
                    card.GameID = Convert.ToInt32(dr["GameID"]);
                    card.CardID = Convert.ToInt32(dr["CardID"]);
                    CardsinHit.Add(card);
                }

                return CardsinHit;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public string GetCardString(int cardID)
        {
            try
            {
                SqlDataReader dr;

                string query = "SELECT CardName FROM Cards WHERE CardID =" + cardID;
                SqlParameter[] sqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(query, sqlParameters);
                string Card = "";
                while (dr.Read())
                {
                    Card = dr["CardName"].ToString();
                }

                return Card;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public int GetLastHitInGame(int currentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public bool HitInGame(int currentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void SetWinPlayerID(int wonPlayerID, int currentGameID, int HitID)
        {
            try
            {

                string query = "UPDATE Hit SET WinPlayerID = @wonPlayerID WHERE GameID = " + currentGameID + " AND HitID = " + HitID;
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@wonPlayerID", SqlDbType.Int);
                sqlParameters[0].Value = wonPlayerID;


                conn.executeUpdateQuery(query, sqlParameters);

            }

            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<CardPlayerBO> ShowLastHit(int currentGameID, int LastHitID)
        {
            try
            {
                SqlDataReader dr;

                string query = "SELECT * FROM dbo.CardPlayer WHERE HitID = " + LastHitID + " AND GameID = " + currentGameID;
                SqlParameter[] sqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(query, sqlParameters);
                List<CardPlayerBO> lastHit = new List<CardPlayerBO>();
                while (dr.Read())
                {
                    CardPlayerBO cardplayer = new CardPlayerBO();
                    cardplayer.CardPlayerID = Convert.ToInt32(dr["CardPlayerID"]);
                    cardplayer.HitID = Convert.ToInt32(dr["HitID"]);
                    cardplayer.PlayerID = Convert.ToInt32(dr["PlayerID"]);
                    cardplayer.GameID = Convert.ToInt32(dr["GameID"]);
                    cardplayer.CardID = Convert.ToInt32(dr["CardID"]);
                    lastHit.Add(cardplayer);
                }

                return lastHit;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }
    }
}
