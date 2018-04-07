using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Card
    {
        public Suits suit;
        public int rank;
        string inputCard;

        public int Rank { get { return this.rank; } }
        public Suits Suit { get { return this.suit; } }

        public Card(string card)
        {
            InitCard(card);
        }
        void InitCard(string card)
        {
            if (card.Length != 2) throw new Exception("Incorrect Card Format");
            inputCard = card;
            switch (card.Substring(0, 1).ToUpper())
            {
                case ("A"): this.rank = 14; break;
                case ("2"): this.rank = 2; break;
                case ("3"): this.rank = 3; break;
                case ("4"): this.rank = 4; break;
                case ("5"): this.rank = 5; break;
                case ("6"): this.rank = 6; break;
                case ("7"): this.rank = 7; break;
                case ("8"): this.rank = 8; break;
                case ("9"): this.rank = 9; break;
                case ("T"): this.rank = 10; break;
                case ("J"): this.rank = 11; break;
                case ("Q"): this.rank = 12; break;
                case ("K"): this.rank = 13; break;
                default: throw new Exception("Incorrect Card Format");
            }
            switch (card.Substring(1, 1).ToUpper())
            {
                case ("H"): this.suit = Suits.Hearts; break;
                case ("D"): this.suit = Suits.Diamonds; break;
                case ("C"): this.suit = Suits.Clubs; break;
                case ("S"): this.suit = Suits.Spades; break;
                default: throw new Exception("Incorrect Card Format");
            }
        }
    }

    public enum Suits
    {
        Clubs = 10,
        Diamonds = 20,
        Hearts = 30,
        Spades = 40,
    }


}

