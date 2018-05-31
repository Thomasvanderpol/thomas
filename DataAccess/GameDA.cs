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
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public string GetBidPlayer(int player, int CurrentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<BidBO> getBidsCurrentGame(int currentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public int GetCurrentGameID()
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<int> getPlayersBidsCurrentGame(int currentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<int> GetPlayersIDs()
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public void SubmitChoice(int currentGameID, string bid, int playerID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<BidLevelBO> GetLevelsBids()
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
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
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<string> GetTrumpAce(int currentGameID)
        {
            try
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
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }



        public void SetTeams(int currentGameID, int playerID)
        {
            try
            {
                //team maken
                string query = "INSERT INTO Team(GameID, GoalBid) VALUES (@GameID, @GoalBid)";

                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@GameID", SqlDbType.Int);
                sqlParameters[0].Value = currentGameID;
                sqlParameters[1] = new SqlParameter("@GoalBid", SqlDbType.Int);
                sqlParameters[1].Value = 1;


                conn.executeInsertQuery(query, sqlParameters);



                //team ophalen
                int TeamID = 0;
                SqlDataReader dr;
                string Query = "SELECT Top 1 TeamID from Team order by TeamID desc";

                SqlParameter[] SqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(Query, SqlParameters);

                while (dr.Read())
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]);
                }



                //spelers toevoegen aan dat team
                string QUERY = "INSERT INTO PlayerInTeam(PlayerID, TeamID) VALUES (@PlayerID, @TeamID)";

                SqlParameter[] SQLParameters = new SqlParameter[2];
                SQLParameters[0] = new SqlParameter("@PlayerID", SqlDbType.Int);
                SQLParameters[0].Value = playerID;
                SQLParameters[1] = new SqlParameter("@TeamID", SqlDbType.Int);
                SQLParameters[1].Value = TeamID;


                conn.executeInsertQuery(QUERY, SQLParameters);

                //team maken
                string Qry = "INSERT INTO Team(GameID, GoalBid) VALUES (@GameID, @GoalBid)";

                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@GameID", SqlDbType.Int);
                par[0].Value = currentGameID;
                par[1] = new SqlParameter("@GoalBid", SqlDbType.Int);
                par[1].Value = 0;


                conn.executeInsertQuery(Qry, par);

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

        public void SetBothTeams(int currentGameID, int playersInGame)
        {
            try
            {
                //team ophalen
                int TeamID = 0;
                SqlDataReader dr;
                string Query = "SELECT TeamID FROM Team WHERE GoalBid = 0 AND GameID = " + currentGameID;

                SqlParameter[] SqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(Query, SqlParameters);

                while (dr.Read())
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]);
                }

                //spelers toevoegen aan dat team
                string QUERY = "INSERT INTO PlayerInTeam(PlayerID, TeamID) VALUES (@PlayerID, @TeamID)";

                SqlParameter[] SQLParameters = new SqlParameter[2];
                SQLParameters[0] = new SqlParameter("@PlayerID", SqlDbType.Int);
                SQLParameters[0].Value = playersInGame;
                SQLParameters[1] = new SqlParameter("@TeamID", SqlDbType.Int);
                SQLParameters[1].Value = TeamID;


                conn.executeInsertQuery(QUERY, SQLParameters);

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

        public void UpdateTeams(int playerID, int CurrentGameID)
        {
            try
            {
                //team ophalen
                int TeamID = 0;
                SqlDataReader dr;
                string Query = "SELECT TeamID FROM Team WHERE GoalBid = 1 AND GameID = " + CurrentGameID;

                SqlParameter[] SqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(Query, SqlParameters);

                while (dr.Read())
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]);
                }

                //spelers toevoegen aan dat team
                string QUERY = "INSERT INTO PlayerInTeam(PlayerID, TeamID) VALUES (@PlayerID, @TeamID)";

                SqlParameter[] SQLParameters = new SqlParameter[2];
                SQLParameters[0] = new SqlParameter("@PlayerID", SqlDbType.Int);
                SQLParameters[0].Value = playerID;
                SQLParameters[1] = new SqlParameter("@TeamID", SqlDbType.Int);
                SQLParameters[1].Value = TeamID;


                conn.executeInsertQuery(QUERY, SQLParameters);
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
        public void SetSecondTeam(int playerID, int CurrentGameID)
        {
            try
            {
                //team ophalen
                int TeamID = 0;
                SqlDataReader dr;
                string Query = "SELECT TeamID FROM Team WHERE GoalBid = 0 AND GameID = " + CurrentGameID;

                SqlParameter[] SqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(Query, SqlParameters);

                while (dr.Read())
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]);
                }

                //spelers toevoegen aan dat team
                string QUERY = "INSERT INTO PlayerInTeam(PlayerID, TeamID) VALUES (@PlayerID, @TeamID)";

                SqlParameter[] SQLParameters = new SqlParameter[2];
                SQLParameters[0] = new SqlParameter("@PlayerID", SqlDbType.Int);
                SQLParameters[0].Value = playerID;
                SQLParameters[1] = new SqlParameter("@TeamID", SqlDbType.Int);
                SQLParameters[1].Value = TeamID;


                conn.executeInsertQuery(QUERY, SQLParameters);
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

        public List<int> GetTeam(int currentGameID, int bid)
        {
            try
            {
                //team ophalen
                int TeamID = 0;
                SqlDataReader dr;
                string Query = "SELECT TeamID FROM Team WHERE GoalBid = " + bid + " AND GameID = " + currentGameID;

                SqlParameter[] SqlParameters = new SqlParameter[0];
                dr = conn.executeSelectQuery(Query, SqlParameters);

                while (dr.Read())
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]);
                }
                //teamspelers ophalen

                SqlDataReader Dr;
                string qry = "SELECT PlayerID FROM Team INNER JOIN PlayerInTeam ON Team.TeamID = PlayerInTeam.TeamID where Team.TeamID = " + TeamID;
                List<int> spelersIDs = new List<int>();
                SqlParameter[] Sqlparas = new SqlParameter[0];
                Dr = conn.executeSelectQuery(qry, Sqlparas);

                while (Dr.Read())
                {
                    int SpelerID = Convert.ToInt32(Dr["PlayerID"]);
                    spelersIDs.Add(SpelerID);
                }
                return spelersIDs;
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
