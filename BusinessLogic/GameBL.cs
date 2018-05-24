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

        public List<string> GetChoicesPlayer(int currentGameID)
        {
            List<string> ChoicesPlayer = new List<string>();
            ChoicesPlayer.Add("pas");
            BidBO highestBid = this.GetHighestBidInGame(currentGameID);
            List<BidLevelBO> BidsLevels = conn.GetLevelsBids();
            
            if (highestBid.level == 1)
            {
                ChoicesPlayer.Add("rik");
                ChoicesPlayer.Add("misère");
            }

            else if (highestBid.level == 2)
            {
                ChoicesPlayer.Add("rik beter");
                ChoicesPlayer.Add("8 alleen");
                ChoicesPlayer.Add("misère");
            }

            else if (highestBid.level == 3)
            {
                ChoicesPlayer.Add("8 alleen");
                ChoicesPlayer.Add("misère");
            }

            else if (highestBid.level == 4)
            {
                ChoicesPlayer.Add("8 alleen beter");
                ChoicesPlayer.Add("misère");
                ChoicesPlayer.Add("9 alleen");
            }
            else if (highestBid.level == 5)
            {
                ChoicesPlayer.Add("9 alleen");
            }


            else if (highestBid.GameTypeName.Contains("beter"))
            {
                int count = 0;
                //bepaal de lijst van keuzes van deze speler door hoogste bieding
                foreach (BidLevelBO bidlevel in BidsLevels)
                {
                    if (count == 1)
                    {
                        break;
                    }
                    else if (highestBid.level < bidlevel.LevelBidsID)
                    {
                        ChoicesPlayer.Add(bidlevel.GameTypeName);
                        count++;
                    }
                }
            }

            else
            {
                int count = 0;
                //bepaal de lijst van keuzes van deze speler door hoogste bieding
                foreach (BidLevelBO bidlevel in BidsLevels)
                {
                    
                    if (count == 2)
                    {
                        break;
                    }
                    else if (highestBid.level < bidlevel.LevelBidsID)
                    {
                        ChoicesPlayer.Add(bidlevel.GameTypeName);
                        count++;
                    }
                }
            }
          
            return ChoicesPlayer;
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

        public void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID)
        {
            conn.UpdateGame(trump, ace, gameTypeGame, currentGameID);
        }
    }
}
