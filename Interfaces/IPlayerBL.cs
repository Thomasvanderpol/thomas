using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IPlayerBL
    {
        void AddPlayer(string PlayerName, double pot);
        List<string> GetPlayers();
        List<int> GetFourPlayers(List<string> playersInGame);
        List<string> GetPlayerNames(List<int> playerids);
    }
}
