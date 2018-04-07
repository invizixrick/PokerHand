using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Hand
    {
        protected List<Card> cards;
        public List<Card> Cards { get { return this.cards; } }
        public virtual void AddCard(Card card)
        {
            cards.Add(card);
        }
        public Hand() { cards = new List<Card>(); }

    }
    public class PokerHand : Hand
    {
        protected PokerRankings handRank;
        public PokerRankings HandRank { get { return this.handRank; } }
        protected Suits suitRank;
        public Suits SuitRank { get { return this.suitRank; } }

        public PokerHand(Card c1, Card c2, Card c3, Card c4, Card c5)
        {
            if (c1 == null || c2 == null || c3 == null || c4 == null || c5 == null) throw new Exception("Non-Existant Card");
            AddCard(c1);
            AddCard(c2);
            AddCard(c3);
            AddCard(c4);
            AddCard(c5);

            DetermineHand();
        }
        public void DetermineHand()
        {
            if (IsStraight() && IsFlush()) { handRank = PokerRankings.StraightFlush; }
            else if (NumMatching() == 4) { handRank = PokerRankings.FourOfAKind; }
            else if (IsFullHouse()) { handRank = PokerRankings.FullHouse; }
            else if (IsFlush()) { handRank = PokerRankings.Flush; }
            else if (IsStraight()) { handRank = PokerRankings.Straight; }
            else if (NumMatching() == 3) { handRank = PokerRankings.ThreeOfAKind; }
            else if (IsTwoPair()) { handRank = PokerRankings.TwoPairs; }
            else if (NumMatching() == 2) { handRank = PokerRankings.Pair; }
            else { handRank = PokerRankings.HighCard; highestRank = cards.OrderBy(c => c.rank).Last().rank; }

            return;
        }
        public int highestRank = 0;
        bool IsStraight()
        {
            int interval = Cards.OrderBy(c => c.rank).Last().rank - Cards.OrderBy(c => c.rank).First().rank;
            if (interval == 4) highestRank = Cards.OrderBy(c => c.rank).Last().rank;
            return interval == 4;
        }
        bool IsTwoPair()
        {
            if (NumMatching() != 2) return false;
            int tmpRank = matchingRank;
            List<Card> remainingCards = cards.Where(c => c.rank != matchingRank).ToList();
            bool rc = Matching(remainingCards) != 2;
            if (rc) highestRank = tmpRank >= matchingRank ? tmpRank : matchingRank;
            return rc;
        }
        bool IsFullHouse()
        {
            if (NumMatching() < 3) return false;
            List<Card> remainingCards = cards.Where(c => c.rank != matchingRank).ToList();
            bool rc = remainingCards[0].rank == remainingCards[1].rank;
            if (rc) highestRank = matchingRank;
            return rc;
        }
        int matchingRank = 0;
        int numMatchingRank = 0;
        int NumMatching()
        {
            if (numMatchingRank > 0) return numMatchingRank;

            numMatchingRank = Matching(this.cards);

            return numMatchingRank;
        }
        int Matching(List<Card> tmpCards)
        {
            Dictionary<int, int> ranks = new Dictionary<int, int>();
            foreach (Card card in this.cards)
            {
                if (ranks.ContainsKey(card.rank))
                    ranks[card.rank]++;
                else
                    ranks.Add(card.rank, 1);
            }
            int numMatching = ranks.OrderByDescending(r => r.Value).First().Value;
            matchingRank = numMatching == 1 ? ranks.OrderByDescending(r => r.Key).First().Key : ranks.OrderByDescending(r => r.Value).First().Key;
            return numMatching;
        }
        bool IsFlush()
        {
            bool rc = cards.Select(c => c.suit).Distinct().Count() == 1;
            //rc = cards.All(c => c.Suit == Suits.Spades) || cards.All(c => c.Suit == Suits.Hearts) || cards.All(c => c.Suit == Suits.Diamonds) || cards.All(c => c.Suit == Suits.Clubs)
            if (rc) highestRank = cards.OrderBy(c => c.rank).Last().rank;
            return rc;
        }
    }
    public class PokerGame
    {
        protected List<Player> players;
        public List<Player> Players { get { return this.players; } }
        public int NumPlayers { get { return this.players.Count; } }

        public PokerGame()
        {
            players = new List<Player>();
        }

        public string GetWinner()
        {
            string winner = string.Empty;

            if ((int)players[0].Hand.HandRank == (int)players[1].Hand.HandRank)
            { 
                if (players[0].Hand.highestRank == players[1].Hand.highestRank)
                { winner = "Tie"; }
                else
                { winner = players[0].Hand.highestRank > players[1].Hand.highestRank ? players[0].Name : players[1].Name; }
            }
            else if ((int)players[0].Hand.HandRank > (int)players[1].Hand.HandRank)
            { winner = players[0].Name; }
            else
            { winner = players[1].Name; }

            return winner;
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
    public class Player
    {
        public string Name;
        protected PokerHand hand;
        public PokerHand Hand { get { return this.hand; } }
        public Player(string name, string c1, string c2, string c3, string c4, string c5)
            : this(name, new PokerHand(new Card(c1), new Card(c2), new Card(c3), new Card(c4), new Card(c5)))
        {
        }

        public Player(string name, PokerHand hand)
        {
            this.Name = name;
            this.hand = hand;
        }

    }
    public enum PokerRankings
    {
        HighCard = 10,
        Pair = 20,
        TwoPairs = 30,
        ThreeOfAKind = 40,
        Straight = 50,
        Flush = 60,
        FullHouse = 70,
        FourOfAKind = 80,
        StraightFlush = 90,
    }
}
