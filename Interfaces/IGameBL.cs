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
    }
}
