using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BlackJack
{
    public class Card
    {
        public Suit Suit;
        public Face Face;

        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;
        }

        public int getValue()
        {
            if (Face == Face.Jack || Face == Face.Queen || Face == Face.King)
            {
                return 10;
            }
            else
            {
                return (int) Face;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
                switch (Face)
                {
                    case Face.Jack:
                        sb.Append("J");
                        break;
                    case Face.Queen:
                        sb.Append("Q");
                        break;
                    case Face.King:
                        sb.Append("K");
                        break;
                    case Face.Ace:
                        sb.Append("A");
                        break;
                    default:
                        sb.Append(((int)Face).ToString());
                        break;
                }

            switch (Suit)
            {
                    case Suit.Club:
                        sb.Append("\u2663");
                        break;
                    case Suit.Diamond:
                        sb.Append("\u2666");
                        break;
                    case Suit.Heart:
                        sb.Append("\u2665");
                        break;
                    case  Suit.Spade:
                        sb.Append("\u2660");
                        break;
                    default:
                        sb.Append("?");
                        break;
            }

            return sb.ToString();
        }
    }

    public enum Suit
    {
        Diamond,
        Club,
        Heart,
        Spade
    }

    public enum Face
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
}