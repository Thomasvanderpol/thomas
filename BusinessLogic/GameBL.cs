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

        public List<BidBO> GetBidsCurrentGame(int currentGameID)
        {
            return conn.getBidsCurrentGame(currentGameID);
        }

        public int GetCurrentGameID()
        {
            return conn.GetCurrentGameID();
        }

        public BidBO GetHighestBidInGame(int currentGameID)
        {
            int highestBid = 0;
            BidBO HighestBid = new BidBO();
            List<BidBO> BidsInGame = conn.getBidsCurrentGame(currentGameID);
            List<BidLevelBO> BidsLevels = conn.GetLevelsBids();
            //method to decide which is the highest
            
            foreach (BidBO bid in BidsInGame)
            {
                foreach (BidLevelBO bidlevel in BidsLevels)
                {
                    if (bid.GameTypeName == bidlevel.GameTypeName)
                    {
                        bid.level = bidlevel.LevelBidsID;
                        break;
                    }
                }
                if (bid.level > highestBid)
                {
                    highestBid = bid.level;
                }
            }

            foreach (BidBO b in BidsInGame)
            {
                if (b.level == highestBid)
                {
                    HighestBid = b;
                    break;
                }
            }
          
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
