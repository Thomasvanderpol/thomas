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
        string GetBidPlayer(int player, int CurrentGameID);
        List<BidBO> getBidsCurrentGame(int currentGameID);
        List<int> getPlayersBidsCurrentGame(int currentGameID);
        List<BidLevelBO> GetLevelsBids();
        void UpdateGame(string trump, string ace, string gameTypeGame, int currentGameID);
        List<string> GetTrumpAce(int currentGameID);


        void SetTeams(int currentGameID, int playerID);
        void SetBothTeams(int currentGameID, int playersInGame);
        void UpdateTeams(int playerID, int CurrentGameID);

        void SetSecondTeam(int playerID, int CurrentGameID);
        List<int> GetTeam(int currentGameID, int bid);
        int GetHitsByGame(int currentGameID, int playerid);
    }
}
