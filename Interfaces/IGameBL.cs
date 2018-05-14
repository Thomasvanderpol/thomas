using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IGameBL
    {
        int GetCurrentGameID();

        void BeginGame(List<int> playersInGameObjecten);
        List<int> GetPlayersIDs();
        void SubmitChoice(int currentGameID, string bid, int playerID);
        string GetBidPlayer(int player, int CurrentGameID);
        List<string> GetBidsCurrentGame(int currentGameID);
        string GetHighestBidInGame(int currentGameID);
        
    }
}
