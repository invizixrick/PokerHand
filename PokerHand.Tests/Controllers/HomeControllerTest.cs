using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cards;

namespace PokerHand.Tests
{
    [TestClass]
    public class PokerHandTests
    {
        [TestMethod]
        public void TestPlayerCreated()
        {
            Player player = new Player("p1", "2H", "4h", "5h", "8h", "kh");
            Assert.AreEqual(player.Name, "p1");
            Assert.AreEqual(player.Hand.HandRank, PokerRankings.Flush);
            Assert.AreEqual(player.Hand.highestRank, 13);
        }
        [TestMethod]
        public void TestAddPlayersToGame()
        {
            PokerGame pokergame = new PokerGame();
            pokergame.AddPlayer(new Player("p1", "2H", "4h", "5h", "8h", "kh"));

            Assert.AreEqual(pokergame.Players[0].Name, "p1");
            Assert.AreEqual(pokergame.NumPlayers, 1);

            pokergame.AddPlayer(new Player("p2", "4c", "6h", "8d", "7s", "5s"));
            Assert.AreEqual(pokergame.NumPlayers, 2);
        }
        [TestMethod]
        public void TestFlushBeatsStraight()
        {
            PokerGame pokergame = new PokerGame();
            pokergame.AddPlayer(new Player("p1", "2H", "4h", "5h", "8h", "kh"));
            pokergame.AddPlayer(new Player("p2", "4c", "6h", "8d", "7s", "5s"));

            string winner = pokergame.GetWinner();
            Assert.AreEqual(winner, "p1");
        }
        [TestMethod]
        public void TestFOAKBeatsFullHouse()
        {
            PokerGame pokergame = new PokerGame();
            pokergame.AddPlayer(new Player("p1", "ah", "as", "ac", "2h", "2d"));
            pokergame.AddPlayer(new Player("p2", "4c", "4d", "4s", "7s", "4h"));

            string winner = pokergame.GetWinner();
            Assert.AreEqual(winner, "p2");
        }
        [TestMethod]
        public void TestHighCard()
        {
            PokerGame pokergame = new PokerGame();
            pokergame.AddPlayer(new Player("p1", "2H", "4h", "5s", "8d", "kc"));
            pokergame.AddPlayer(new Player("p2", "4c", "jc", "8d", "7s", "5s"));

            string winner = pokergame.GetWinner();
            Assert.AreEqual(winner, "p1");
        }
        [TestMethod]
        public void TestTieGame()
        {
            PokerGame pokergame = new PokerGame();
            pokergame.AddPlayer(new Player("p1", "2H", "4h", "5s", "8d", "kc"));
            pokergame.AddPlayer(new Player("p2", "4c", "jc", "8d", "ks", "5s"));

            string winner = pokergame.GetWinner();
            Assert.AreEqual(winner, "Tie");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestBadCardSuit()
        {
            Player player = new Player("p1", "2H", "4p", "5h", "8h", "kh");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestBadCardRank()
        {
            Player player = new Player("p1", "2H", "1h", "5h", "8h", "kh");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestBadCardStringSize()
        {
            Player player = new Player("p1", "2HHH", "4h", "5h", "8h", "kh");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestNullCard()
        {
            Player player = new Player("p1", "2HHH", "4h", "5h", "8h", null);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestStringEmptyCard()
        {
            Player player = new Player("p1", "2HHH", "4h", "5h", "8h", string.Empty);
        }
    }
}

