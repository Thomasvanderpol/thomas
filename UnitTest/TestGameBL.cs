using BusinessLogic;
using BusinessObject;
using Factory;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class TestGameBL
    {
        private IGameBL logic;

        [TestInitialize]
        public void TestInitialize()
        {
            logic = new GameBL(new FakeDatabase());
        }


        [TestMethod]
        public void TestGetBidPlayer()
        {
            Assert.AreEqual(logic.GetBidPlayer(1, 2), "rik");
        }

        [TestMethod]
        public void TestGetPlayersIDs()
        {
            Assert.AreEqual(logic.GetPlayersIDs().Count, 4);
        }

        [TestMethod]
        public void TestGetBidsCurrentGame()
        {
            List<BidBO> bidsInCurrentGame = logic.GetBidsCurrentGame(3);
            Assert.AreEqual(bidsInCurrentGame[0].GameTypeName, "rik");
            Assert.AreEqual(bidsInCurrentGame[0].GameID, 1);
            Assert.AreEqual(bidsInCurrentGame[0].level, 2);
            Assert.AreEqual(bidsInCurrentGame[0].PlayerID, 5);

        }

        [TestMethod]
        public void TestWhoWonGame()
        {
            List<int> WonTeam = logic.WhoWonGAme(1);
            Assert.AreEqual(WonTeam.Count, 2);
            Assert.AreEqual(WonTeam[0], 5);

            Assert.AreEqual(WonTeam[1], 6);

            Assert.AreNotEqual(WonTeam[0], 7);

            Assert.AreNotEqual(WonTeam[1], 8);


        }

    }
}
