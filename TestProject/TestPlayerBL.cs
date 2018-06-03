using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject
{
    public class TestPlayerBL: IPlayerBL
    {
        private IPlayerDA conn;
        public TestPlayerBL(IPlayerDA conn)
        {
            this.conn = conn;
        }

        public void AddPlayer(string PlayerName, double pot)
        {
            throw new NotImplementedException();
        }

        public List<int> GetFourPlayers(List<string> playersInGame)
        {
            throw new NotImplementedException();
        }

        public int GetPlayerID(string player)
        {
            throw new NotImplementedException();
        }

        public List<string> GetPlayerNames(List<int> playerids)
        {
            List<string> playernames = new List<string>();
            foreach (int playerid in playerids)
            {
                switch (playerid)
                {
                    case 1:
                        playernames.Add("Mohammed");
                        break;
                    case 2:
                        playernames.Add("Joshua");
                        break;
                    case 3:
                        playernames.Add("Lute");
                        break;
                    case 4:
                        playernames.Add("Thomas");
                        break;
                    case 5:
                        playernames.Add("Mike");
                        break;
                    case 6:
                        playernames.Add("Sil");
                        break;
                    case 7:
                        playernames.Add("Sybe");
                        break;
                    case 8:
                        playernames.Add("Koen");
                        break;
                    case 9:
                        playernames.Add("Rick");
                        break;
                    case 10:
                        playernames.Add("Driss");
                        break;
                    case 11:
                        playernames.Add("Melvin");
                        break;
                    case 12:
                        playernames.Add("Joey");
                        break;
                }

            }
            return playernames;
        }

        public List<string> GetPlayers()
        {
            throw new NotImplementedException();
        }
    }
}
