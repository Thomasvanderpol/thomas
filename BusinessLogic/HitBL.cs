using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class HitBL: IHitBL
    {
        private IHitDA conn;
        public HitBL(IHitDA conn)
        {
            this.conn = conn;
        }

        public void AddCard(int currentGameID, int playerID, string cardplayer)
        {
            int cardID = conn.GetCard(cardplayer);
            if (!conn.HitInGame(currentGameID))
            {
                //add hit to db
                conn.addHitInGame(currentGameID);
            }


            //method to get HitID
            int hitID = conn.GetLastHitInGame(currentGameID);

            List<int> cardsInHit = conn.getCardsByHit(currentGameID, hitID);

            if (cardsInHit.Count != 4)
            {
                //method to add card
                conn.AddCard(currentGameID, playerID, cardID, hitID);
            }
            else
            {
                conn.addHitInGame(currentGameID);
                int HitID = conn.GetLastHitInGame(currentGameID);
                conn.AddCard(currentGameID, playerID, cardID, HitID);
            }
            
           

            
        }
    }
}
