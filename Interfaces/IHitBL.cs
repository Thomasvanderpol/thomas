using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IHitBL
    {
        void AddCard(int currentGameID, int playerID, string cardPlayer);
        void WhoWonBid(int currentGameID, string trump);
        List<int> GetAllHits(int currentGameID, List<int> PlayersInGame);
    }
}
