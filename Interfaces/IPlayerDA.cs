using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IPlayerDA
    {
        void AddPlayer(PlayerBO player);
        List<PlayerBO> GetPlayers();

        List<int> GetFourPlayers(List<string> playersInGame);
        string getPlayerName(int playerid);
    }
}
