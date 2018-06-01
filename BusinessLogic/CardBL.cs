using BusinessObject;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CardBL : ICardBL
    {
        public List<string> Cards = new List<string>();
        private ICardDA conn;
        public CardBL(ICardDA conn)
        {
            this.conn = conn;
        }

        public List<string> GetCardNames(List<int> cardIDs)
        {
            List<string> cardNames = new List<string>();
            foreach (int card in cardIDs)
            {
                string cardName = conn.getCardNameByID(card);
                cardNames.Add(cardName);
            }
            return cardNames;

        }

        public List<string> GetCards()
        {

            List<CardBO> cards = conn.GetCards();


            foreach (CardBO card in cards)
            {
                Cards.Add(card.CardName);
            }

            //sorted cards unsorten
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                Random rng = new Random();

                int swapIndex = rng.Next(i + 1);
                string tmp = Cards[i];
                Cards[i] = Cards[swapIndex];
                Cards[swapIndex] = tmp;

            }
            return Cards;

        }
     
    }
}
