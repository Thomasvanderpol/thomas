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

      

        public List<string> GetTrumpAce(int currentGameID)
        {
            return conn.GetTrumpAce(currentGameID);
        }

        public void SetTeam(int currentGameID, int PlayerID, string GameTypeGame)
        {
            conn.SetTeams(currentGameID, PlayerID);
            //haal alle spelers op
            List<int> PlayersInGame = conn.GetPlayersIDs();
            //check wat voor type het is

            if (GameTypeGame.Contains("alleen") || GameTypeGame.Contains("misère"))
            {
                foreach (int player in PlayersInGame)
                {
                    if (player != PlayerID)
                    {
                        conn.SetBothTeams(currentGameID, player);
                    }
                }
               
            }
            //bij rik moet er gewacht worden op de updateTeams 
            

        }

        public void SubmitChoice(int currentGameID, string bid, int playerID)
        {
            conn.SubmitChoice(currentGameID, bid, playerID);

        }

        public void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID)
        {
            conn.UpdateGame(trump, ace, gameTypeGame, currentGameID);
        }
        public void UpdateTeams(int PlayerID, int CurrentGameID)
        {
            conn.UpdateTeams(PlayerID, CurrentGameID);
            List<int> PlayersInGame = conn.GetPlayersIDs();
            List<int> FirstTeam = conn.GetTeam(CurrentGameID, 1);

            foreach (int player in PlayersInGame)
            {
                
                    if (player != FirstTeam[0] && player != FirstTeam[1])
                    {
                        conn.SetSecondTeam(player, CurrentGameID);
                       
                    }
                
            }
        }

        public List<int> GetTeam1(int currentGameID)
        {
            return conn.GetTeam(currentGameID, 1);
        }
        public List<int> GetTeam2(int currentGameID)
        {
            return conn.GetTeam(currentGameID, 0);
        }

        public List<int> WhoWonGAme(int currentGameID)
        {
            //wat voor spel is er gespeeld
            BidBO Gameplay = GetHighestBidInGame(currentGameID);

            //wat is het doel voor elk spel
            int SlagenDoel;
            if (Gameplay.GameTypeName.Contains("rik") || Gameplay.GameTypeName.Contains("8"))
            {
                SlagenDoel = 8;
            }

            else if (Gameplay.GameTypeName.Contains("9"))
            {
                SlagenDoel = 9;
            }
            else if (Gameplay.GameTypeName.Contains("alleen"))
            {
                string sub = Gameplay.GameTypeName.Substring(0, 2);
                SlagenDoel = Convert.ToInt32(sub);
            }
            else
            {
                SlagenDoel = 0;
            }

            //welke spelers doen er mee en in welk team spelen ze
            List<int> Team1 = GetTeam1(currentGameID);
            List<int> Team2 = GetTeam2(currentGameID);
            int totaalTeam1 = 0;
            int totaalTeam2 = 0;
            foreach (int playerid in Team1)
            {
                totaalTeam1 += conn.GetHitsByGame(currentGameID, playerid);                
            }
            foreach (int playerid in Team2)
            {
                totaalTeam2 += conn.GetHitsByGame(currentGameID, playerid);
            }

           

            if (SlagenDoel == 0)
            {
                if (SlagenDoel == totaalTeam1)
                {
                    return Team1;
                }
                else
                {
                    return Team2;
                }
            }
            else if (totaalTeam1 >= SlagenDoel)
            {
                return Team1;
            }
            else
            {
                return Team2;
            }
            
        }
    }
}
