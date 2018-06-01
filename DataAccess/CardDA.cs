﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using BusinessObject;
using Interfaces;

namespace DataAccess
{
    public class CardDA : ICardDA
    {

        private IDb conn;
        public CardDA(IDb conn)
        {
            this.conn = conn;
        }

        public string getCardNameByID(int card)
        {
            try
            {
                string CardName = "";
                string query = "SELECT CardName FROM Cards where CardID = " + card;
                SqlParameter[] sqlParameters = new SqlParameter[0];
                SqlDataReader dr = conn.executeSelectQuery(query, sqlParameters);

                while (dr.Read())
                {
                    CardName = dr["CardName"].ToString();
                }

                return CardName;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public List<CardBO> GetCards()
        {
            try
            {
                List<CardBO> cards = new List<CardBO>();
                string query = "SELECT * FROM Cards";
                SqlParameter[] sqlParameters = new SqlParameter[0];
                SqlDataReader dr = conn.executeSelectQuery(query, sqlParameters);

                while (dr.Read())
                {

                    CardBO card = new CardBO();

                    card.CardID = Convert.ToInt32(dr["CardID"]);
                    card.CardName = dr["CardName"].ToString();

                    cards.Add(card);
                }

                return cards;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.CloseConnection();
            }
        }
    }
}
