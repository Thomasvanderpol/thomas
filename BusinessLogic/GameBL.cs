using BusinessObject;
using DataAccess;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class GameBL : IGameBL
    {

        private IGameDA conn;
        public GameBL(IGameDA conn)
        {
            this.conn = conn;
        }


        
        public void BeginGame(List<int> PlayersInGame)
        {
            conn.BeginGame(PlayersInGame);
        }
    }
}
