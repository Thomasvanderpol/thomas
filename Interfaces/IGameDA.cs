using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IGameDA
    {
        void BeginGame(List<int> players);
        int GetCurrentGameID();
        List<int> GetPlayersIDs();
        void SubmitChoice(int currentGameID, string bid, int playerID);
    }
}
