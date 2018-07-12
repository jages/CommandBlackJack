using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace BlackJack
{
    public class Player
    {
        private List<Card> cards = new List<Card>();

        public readonly String Name;

        public Player(String name)
        {
            Name = name;
        }

        public void DealCard(Card card)
        {
            cards.Add(card);
            Console.WriteLine($"{Name} draws {card.Face} of {card.Suit}s");
        }

        public int GetScore()
        {
            int score = 0;
            bool ace = false; // only a single ace can be worth 11
            foreach (Card card in cards)
            {
                if ((card.getValue() == 1) && !ace){
                    ace = true;
                    score += 11;
                }
                else
                {
                    score += card.getValue();
                }
            }

            if (score > 21 && ace)
            { //subtract ace value if overdraft
                score -= 10;
            }

            return score;
        }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(GetScore().ToString());
            build.Append(" ( ");
            foreach (Card card in cards)
            {
                build.Append(card.ToString());
            }
            build.Append(")");
            return build.ToString();
        }
    }
}