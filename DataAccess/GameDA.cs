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
    }
}
