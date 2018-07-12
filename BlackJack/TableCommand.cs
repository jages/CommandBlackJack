using System;

namespace BlackJack
{
    public class TableCommand
    {
        public enum Action
        {
            ShuffleDeck,
            Hit,
            AddPlayer,
            Stand,
            Bust
        }

        public Action action;
        public Player player;
        public Card card;
    }
}