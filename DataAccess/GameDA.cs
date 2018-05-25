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

        public string GetBidPlayer(int player, int CurrentGameID)
        {
            SqlDataReader dr;

            string query = "SELECT GameTypeName FROM Bids WHERE GameID = " + CurrentGameID + " AND PlayerID = " + player;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            string Bid = "";
            while (dr.Read())
            {
                Bid = dr["GameTypeName"].ToString();
            }

            return Bid;
        }

        public List<BidBO> getBidsCurrentGame(int currentGameID)
        {
            SqlDataReader dr;

            string query = "SELECT GameTypeName, PlayerID FROM Bids WHERE GameID = " + currentGameID + " ORDER BY BidID DESC";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            List<BidBO> Bids = new List<BidBO>();
            while (dr.Read())
            {
                BidBO bid = new BidBO();
                bid.GameTypeName = dr["GameTypeName"].ToString();
                bid.PlayerID = Convert.ToInt32(dr["PlayerID"]);
                Bids.Add(bid);
            }
            
            return Bids;
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

        public List<int> getPlayersBidsCurrentGame(int currentGameID)
        {
            SqlDataReader dr;

            string query = "SELECT PlayerID FROM Bids WHERE GameID = " + currentGameID;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            List<int> Bids = new List<int>();
            while (dr.Read())
            {
                Bids.Add(Convert.ToInt32(dr["PlayerID"]));
            }

            return Bids;
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

        public List<BidLevelBO> GetLevelsBids()
        {
            List<BidLevelBO> LevelBids = new List<BidLevelBO>();

            SqlDataReader dr;

            string query = "SELECT LevelBidsID, GameTypeName FROM LevelBids";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
            
            while (dr.Read())
            {
                BidLevelBO bidlevel = new BidLevelBO();
                bidlevel.LevelBidsID = Convert.ToInt32(dr["LevelBidsID"]);
                bidlevel.GameTypeName = dr["GameTypeName"].ToString();
                LevelBids.Add(bidlevel);
            }

            return LevelBids;
        }

        public void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID)
        {
            try
            {

                string query = "UPDATE Game SET Trump = @Trump, Ace = @Ace, HighestBid = @HighestBid WHERE GameID = " + currentGameID;
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@Trump", SqlDbType.VarChar);
                sqlParameters[0].Value = trump;
                sqlParameters[1] = new SqlParameter("@Ace", SqlDbType.VarChar);
                sqlParameters[1].Value = ace;
                sqlParameters[2] = new SqlParameter("@HighestBid", SqlDbType.VarChar);
                sqlParameters[2].Value = gameTypeGame;

                conn.executeUpdateQuery(query, sqlParameters);

            }

            catch
            {
                throw;
            }
        }

        public List<string> GetTrumpAce(int currentGameID)
        {
            List<string> TrumpAce = new List<string>();

            SqlDataReader dr;

            string query = "SELECT Trump, Ace FROM Game WHERE GameID =" + currentGameID;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);

            while (dr.Read())
            {

                string Trump = dr["Trump"].ToString();
                string Ace = dr["Ace"].ToString();
                TrumpAce.Add(Trump);
                TrumpAce.Add(Ace);

            }

            return TrumpAce;
        }
    }
}
