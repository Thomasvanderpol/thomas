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

        public string GetBidPlayer(int player, int CurrentGameID)
        {
            return conn.GetBidPlayer(player, CurrentGameID);
        }

        public List<string> GetBidsCurrentGame(int currentGameID)
        {
            return conn.getBidsCurrentGame(currentGameID);
        }

        public int GetCurrentGameID()
        {
            return conn.GetCurrentGameID();
        }

        public string GetHighestBidInGame(int currentGameID)
        {
            string HighestBid = "";
            List<string> BidsInGame = conn.getBidsCurrentGame(currentGameID);
            List<int> PlayersBidsInGame = conn.getPlayersBidsCurrentGame(currentGameID);

            return HighestBid;
        }

        public List<int> GetPlayersIDs()
        {
            return conn.GetPlayersIDs();
        }

        public void SubmitChoice(int currentGameID, string bid, int playerID)
        {
            conn.SubmitChoice(currentGameID, bid, playerID);
        }
    }
}
