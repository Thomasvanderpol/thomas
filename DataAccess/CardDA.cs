using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using BusinessObject;
using Interfaces;

namespace DataAccess
{
    public class CardDA : ICardDA
    {
        public List<CardBO> cards = new List<CardBO>();
        private IDb conn;
        public CardDA(IDb conn)
        {
            this.conn = conn;
        }

        public List<CardBO> GetCards()
        {
            SqlDataReader dr;

            string query = "SELECT * FROM Cards";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            dr = conn.executeSelectQuery(query, sqlParameters);

            while (dr.Read())
            {

                CardBO card = new CardBO();

                card.CardID = Convert.ToInt32(dr["CardID"]);
                card.CardName = dr["CardName"].ToString();
                
                cards.Add(card);
            }

            return cards;
        }
    }
}
