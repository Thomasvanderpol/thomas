using BusinessLogic;
using DataAccess;
using Interfaces;
using System;


namespace Factory
{
    public class BussinessLayerFactory
    {
        static public PlayerBL CreatePlayerBL()
        {
            IDb db = new DbConnection();
            IPlayerDA da = new PlayerDA(db);
            return new PlayerBL(da);
        }

        static public GameBL CreateGameBL()
        {
            IDb db = new DbConnection();
            IGameDA da = new GameDA(db);
            return new GameBL(da);
        }
        static public CardBL CreateCardBL()
        {
            IDb db = new DbConnection();
            ICardDA da = new CardDA(db);
            return new CardBL(da);
        }


    }
}
