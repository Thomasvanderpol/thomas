using BusinessObject;
using DataAccess;
using Interfaces;
using System;
using System.Collections.Generic;
using TestProject;

namespace BusinessLogic
{
    public class PlayerBL : IPlayerBL
    {
        public List<string> players = new List<string>();
        public List<int> FourPlayersInGame = new List<int>();

        private IPlayerDA conn;
        public PlayerBL(IPlayerDA conn)
        {
            this.conn = conn;
        }

        public void AddPlayer(string PlayerName, double pot)
        {
            PlayerBO player = new PlayerBO();

            player.PlayerName = PlayerName;
            player.PlayerMoney = pot;
         

            conn.AddPlayer(player);
        }

        public List<int> GetFourPlayers(List<string> playersInGame)
        {
            try
            {
                foreach (int playerid in conn.GetFourPlayers(playersInGame))
                {

                    FourPlayersInGame.Add(playerid);

                }
                return FourPlayersInGame;
            }
            catch
            {
                throw;
            }
        }

        public int GetPlayerID(string player)
        {
            return conn.GetPlayerID(player);
        }

        public List<string> GetPlayerNames(List<int> playerids)
        {
            List<string> PlayerNames = new List<string>();
            foreach (int id in playerids)
            {
                string playername = conn.getPlayerName(id);
                PlayerNames.Add(playername);

            }

            return PlayerNames;
        }

        public List<string> GetPlayers() 
        {
            //in stead of conn.GetPlayers, use test getplayers
            //test
            TestClass test = new TestClass();
            return test.Allplayers();
            //normal
            /*try
            {
                foreach (PlayerBO player in conn.GetPlayers())
                {
                    
                   players.Add(player.PlayerName);
                    
                }
                return players;
            }
            catch
            {
                throw;
            }*/
        }



     
    }
}