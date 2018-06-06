using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessLogic;
using BusinessObject;
using Factory;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class FakeDatabase : IGameDA
    {
        public void BeginGame(List<int> players)
        {
            throw new System.NotImplementedException();
        }

      
        public string GetBidPlayer(int player, int CurrentGameID)
        {
            return "rik";
        }

        public List<BidBO> getBidsCurrentGame(int currentGameID)
        {
            List<BidBO> bidsCurrentGame = new List<BidBO>();
            BidBO bid1InGame = new BidBO();
            bid1InGame.GameID = 1;
            bid1InGame.GameTypeName = "rik";
            bid1InGame.level = 2;
            bid1InGame.PlayerID = 5;

            BidBO bid2InGame = new BidBO();
            bid2InGame.GameID = 1;
            bid2InGame.GameTypeName = "pas";
            bid2InGame.level = 1;
            bid2InGame.PlayerID = 6;

            BidBO bid3InGame = new BidBO();
            bid3InGame.GameID = 1;
            bid3InGame.GameTypeName = "rik beter";
            bid3InGame.level = 4;
            bid3InGame.PlayerID = 7;

            BidBO bid4InGame = new BidBO();
            bid4InGame.GameID = 1;
            bid4InGame.GameTypeName = "pas";
            bid4InGame.level = 2;
            bid4InGame.PlayerID = 8;

            bidsCurrentGame.Add(bid1InGame);
            bidsCurrentGame.Add(bid2InGame);
            bidsCurrentGame.Add(bid3InGame);
            bidsCurrentGame.Add(bid4InGame);

            return bidsCurrentGame;
        }

        public int GetCurrentGameID()
        {
            throw new System.NotImplementedException();
        }

        public int GetHitsByGame(int currentGameID, int playerid)
        {
            if (playerid == 5)
            {
                return 4;
            }
            else if (playerid == 6)
            {
                return 2;
            }
            else if (playerid == 7)
            {
                return 6;
            }
            else
            {
                return 1;
            }
        }

        public List<BidLevelBO> GetLevelsBids()
        {
            BidLevelBO bidlevel1 = new BidLevelBO(1, "pas");
            BidLevelBO bidlevel2 = new BidLevelBO(2, "rik");
            BidLevelBO bidlevel3 = new BidLevelBO(3, "rik beter");
            BidLevelBO bidlevel4 = new BidLevelBO(4, "8 alleen");
            BidLevelBO bidlevel5 = new BidLevelBO(5, "8 alleen beter");
            BidLevelBO bidlevel6 = new BidLevelBO(6, "misère");
            BidLevelBO bidlevel7 = new BidLevelBO(7, "9 alleen");
            BidLevelBO bidlevel8 = new BidLevelBO(8, "9 alleen beter");
            BidLevelBO bidlevel9 = new BidLevelBO(9, "10 alleen");
            BidLevelBO bidlevel10 = new BidLevelBO(10, "10 alleen beter");
            BidLevelBO bidlevel11 = new BidLevelBO(11, "11 alleen");
            BidLevelBO bidlevel12 = new BidLevelBO(12, "11 alleen beter");
            BidLevelBO bidlevel13 = new BidLevelBO(13, "12 alleenr");
            BidLevelBO bidlevel14 = new BidLevelBO(14, "12 alleen beter");
            BidLevelBO bidlevel15 = new BidLevelBO(15, "13 alleen");
            BidLevelBO bidlevel16 = new BidLevelBO(16, "14 alleen beter");
            List<BidLevelBO> Bidlevels = new List<BidLevelBO>() { bidlevel1, bidlevel2, bidlevel3, bidlevel4, bidlevel5, bidlevel6, bidlevel7, bidlevel8, bidlevel9, bidlevel10, bidlevel11, bidlevel12, bidlevel13, bidlevel14, bidlevel15, bidlevel16 };
            return Bidlevels;
        }

        public List<int> getPlayersBidsCurrentGame(int currentGameID)
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetPlayersIDs()
        {
            List<int> PlayerIDs = new List<int>() { 3, 6, 8, 2 };
            return PlayerIDs;
        }

        public List<int> GetTeam(int currentGameID, int bid)
        {
            if (bid == 1)
            {
                List<int> PlayerIDs = new List<int>() { 7, 8 };
                return PlayerIDs;
            }
            else
            {
                List<int> PlayerIDs = new List<int>() { 5, 6 };
                return PlayerIDs;
            }
           
        }

        public List<string> GetTrumpAce(int currentGameID)
        {
            throw new System.NotImplementedException();
        }

        public void SetBothTeams(int currentGameID, int playersInGame)
        {
            throw new System.NotImplementedException();
        }

        public void SetSecondTeam(int playerID, int CurrentGameID)
        {
            throw new System.NotImplementedException();
        }

        public void SetTeams(int currentGameID, int playerID)
        {
            throw new System.NotImplementedException();
        }

        public void SubmitChoice(int currentGameID, string bid, int playerID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTeams(int playerID, int CurrentGameID)
        {
            throw new System.NotImplementedException();
        }
    }
}
