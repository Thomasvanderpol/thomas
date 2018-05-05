using BusinessObject;
using DataAccess;
using Interfaces;
using System;
using System.Collections.Generic;

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

        public List<string> GetPlayers() 
        {
            try
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
            }
        }



     
    }
}