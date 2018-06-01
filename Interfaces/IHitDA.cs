using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IHitDA
    {
      
        void AddCard(int currentGameID, int playerID, int cardID, int hitID);
        int GetCard(string cardPlayer3);
        bool HitInGame(int currentGameID);
        void addHitInGame(int currentGameID);
        int GetLastHitInGame(int currentGameID);
        List<CardPlayerBO> getCardsByHit(int currentGameID, int hitID);
        string GetCardString(int cardID);
        void SetWinPlayerID(int wonPlayerID, int currentGameID, int HitID);
        int GetAllHits(int currentGameID, int PlayerID);
        List<CardPlayerBO> ShowLastHit(int currentGameID, int LastHitID);
    }
}
