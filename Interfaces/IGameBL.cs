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
        List<BidBO> GetBidsCurrentGame(int currentGameID);
        BidBO GetHighestBidInGame(int currentGameID);
        List<string> GetChoicesPlayer(int currentGameID);
        void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID);
        List<string> GetTrumpAce(int currentGameID);
        void SetTeam(int currentGameID, int PlayerID, string GameTypeGame);
        void UpdateTeams(int playerID, int CurrentGameID);
    }
}
