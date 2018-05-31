using BusinessObject;
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

            List<CardPlayerBO> cardsInHit = conn.getCardsByHit(currentGameID, hitID);

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

        public List<int> GetAllHits(int currentGameID, List<int> PlayersInGame)
        {
            List<int> AllHits = new List<int>();
            foreach (int PlayerID in PlayersInGame)
            {
                int PlayerHits = conn.GetAllHits(currentGameID, PlayerID);
                AllHits.Add(PlayerHits);
            }
           
            return AllHits;
        }

        public void WhoWonBid(int currentGameID, string trump)
        {
            //method to get HitID
            int hitID = conn.GetLastHitInGame(currentGameID);

            //method to get PlayedCards by players in hit
            List<CardPlayerBO> cardsInHit = conn.getCardsByHit(currentGameID, hitID);
            string TrumpChar = GetTrumpChar(trump);
            
          
            string samecolorChar = "";
            
            List<string> allcards = new List<string>();
            List<string> trumpcards = new List<string>();
            List<string> samecolorcards = new List<string>();
            List<string> remainingcards = new List<string>();

            //get all card strings from id's
            foreach (CardPlayerBO card in cardsInHit)
            {
                string cardstring = conn.GetCardString(card.CardID);
                allcards.Add(cardstring);
            }

            samecolorChar = allcards[0].Substring(0,1);
          
            //sort cards in trumpcards cards with samecolor as the first and remaing cards
            foreach (string card in allcards)
            {
                if (card.Contains(TrumpChar))
                {
                    trumpcards.Add(card);
                }
                else if (card.Contains(samecolorChar))
                {
                    samecolorcards.Add(card);
                }
                else
                {
                    remainingcards.Add(card);
                }
            }

            string WonCard = "";
            if (trumpcards.Count != 0)
            {
                trumpcards.Sort();
                WonCard = trumpcards[trumpcards.Count - 1];
            }
            else 
            {
                samecolorcards.Sort();
                WonCard = samecolorcards[samecolorcards.Count - 1];
            }
            //method to get id from woncard
            int cardid = conn.GetCard(WonCard);
            int wonPlayerID = 0;
            //method to get playerid who's got woncard
            foreach (CardPlayerBO cardplayer in cardsInHit)
            {
                if (cardplayer.CardID == cardid )
                {
                    wonPlayerID = cardplayer.PlayerID;
                }
            }


            //method to set wonplayerid in Hit table
            conn.SetWinPlayerID(wonPlayerID, currentGameID, hitID);

        }

        private string GetTrumpChar(string trump)
        {
            if (trump == "Clubs")
            {
                return "K";
            }
            else if (trump == "Hearts")
            {
                return "H";
            }
            else if (trump == "Diamonds")
            {
                return "R";
            }
            else if (trump == "Spades")
            {
                return "S";
            }
            else
            {
                return "NoTrump";
            }
        }
    }
}
