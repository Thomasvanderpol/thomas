using BusinessObject;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class PlayerDA : IPlayerDA
    {
        private IDb conn;
        public PlayerDA(IDb conn)
        {
            this.conn = conn;
        }

        public List<PlayerBO> Players = new List<PlayerBO>();

        public List<PlayerBO> GetPlayers()
        {
            SqlDataReader dr;

            string query = "SELECT * FROM Player";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);

            while (dr.Read())
            {

                PlayerBO player = new PlayerBO();

                player.PlayerName = dr["PlayerName"].ToString();
                player.PlayerMoney = Convert.ToDouble(dr["PlayerMoney"]);
                player.PlayerID = Convert.ToInt32(dr["PlayerID"]);

                Players.Add(player);
            }

            return Players;
        }
        public void AddPlayer(PlayerBO player)
        {
            string query = "INSERT INTO Player(PlayerName, PlayerMoney) VALUES (@PlayerName, @PlayerMoney)";

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@PlayerName", SqlDbType.VarChar);
            sqlParameters[0].Value = player.PlayerName;
            sqlParameters[1] = new SqlParameter("@PlayerMoney", SqlDbType.SmallMoney);
            sqlParameters[1].Value = player.PlayerMoney;

            conn.executeInsertQuery(query, sqlParameters);
        }


        public List<int> GetFourPlayers(List<string> playersInGame)
        {
           
            SqlDataReader dr;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            List<int> players = new List<int>();
            

            foreach (string name in playersInGame)
            {
                string query = "Select PlayerID From Player where PlayerName = " + "'" + name + "'";
                dr = conn.executeSelectQuery(query, sqlParameters);
                PlayerBO player = new PlayerBO();

                while (dr.Read())
                {
                    player.PlayerID = Convert.ToInt32(dr["PlayerID"]);
                    players.Add(player.PlayerID);
                }
                
                
            }
            

            return players;
        }

        public string getPlayerName(int playerid)
        {
            string PlayerName = "";
            SqlDataReader dr;

            string query = "SELECT PlayerName FROM Player WHERE PlayerID =" + playerid;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);
        
            while (dr.Read())
            {
                 PlayerName = dr["PlayerName"].ToString();
               
            }

            return PlayerName;
        }

        public int GetPlayerID(string player)
        {
            int PlayerID = 0;
            SqlDataReader dr;
            string query = "SELECT PlayerID FROM Player WHERE PlayerName = '" + player + "'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);

            while (dr.Read())
            {
                PlayerID = Convert.ToInt32(dr["PlayerID"]);

            }
            return PlayerID;
        }
    }
}
