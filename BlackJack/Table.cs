using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Table
    {
        public Player dealer;
        public List<Player> players = new List<Player>();
        List<TableCommand> commands = new List<TableCommand>();

        private List<Card> Deck;

        public Card DrawCard()
        {
            if (Deck.Count > 0)
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                return card;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private void AddPlayer(Player player)
        {
            players.Add(player);
        }

        private void ShuffleDeck()
        {
            InitDeck();

            Random rng = new Random();
            Deck = Deck.OrderBy(x => rng.Next()).ToList();
        }

        private void InitDeck()
        {
            Deck = new List<Card>();
            foreach (Suit suit in (Suit[]) Enum.GetValues(typeof(Suit)))
            {
                foreach (Face face in (Face[]) Enum.GetValues(typeof(Face)))
                {
                    Deck.Add(new Card(suit, face));
                }
            }
        }

        public Table()
        {
            dealer = new Player("Dealer");
            InitDeck();
        }

        public void Play(TableCommand c)
        {
            commands.Add(c);
            switch (c.action)
            {
                case TableCommand.Action.ShuffleDeck:
                    ShuffleDeck();
                    break;
                case TableCommand.Action.AddPlayer:
                    AddPlayer(c.player);
                    break;
                case TableCommand.Action.Hit:
                    if (c.card != null)
                    {
                        c.player.DealCard(c.card);
                    }
                    else
                    {
                        c.card = DrawCard();
                        c.player.DealCard(c.card);
                    }

                    break;
                case TableCommand.Action.Stand: //Player stands
                    break;
                case TableCommand.Action.Bust: //Bust Players do nothing and wait for the endgame
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool GameOver()
        {
            if (dealer.GetScore() >= 21)
                return true;
            
            foreach (Player player in players)
            {
                if (player.GetScore() >= 21)
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("============\n");
            builder.Append($"Dealer has {dealer.ToString()}\n");
            foreach (Player player in players)
            {
                builder.Append($"Player \"{player.Name}\" has {player.ToString()}\n");    
            }

            builder.Append("============");

            return builder.ToString();
        }
    }
}