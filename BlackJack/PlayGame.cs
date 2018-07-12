using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class PlayGame
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Table table = new Table();
            int NumberofPlayers = GetPlayerCount();
            Player[] players = InitPlayers(NumberofPlayers);
            AddPlayers(players, table);
            table.Play(new TableCommand(){ action = TableCommand.Action.ShuffleDeck});
            Console.Out.WriteLine(table.ToString());
            FristRound(table, players);
            Console.Out.WriteLine(table.ToString());
            while (!table.GameOver())
            {
                PlayRound(table, players);
                Console.Out.WriteLine(table.ToString());
            }
            Console.WriteLine("The Game is over.");
        }

        private static void AddPlayers(Player[] players, Table table)
        {
            List<TableCommand> commands = new List<TableCommand>();
            foreach (Player player in players)
            {
                commands.Add(new TableCommand() {action = TableCommand.Action.AddPlayer, player = player});
            }

            foreach (TableCommand command in commands)
            {
                table.Play(command);
            }
        }

        static void FristRound(Table table, Player[] players)
        {
            foreach (Player player in players)
            {
                table.Play(new TableCommand(){ action = TableCommand.Action.Hit, player = player});
            }
            PlayDealer(table);
        }

        static void PlayRound(Table table, Player[] players)
        {
            List<TableCommand> commands = new List<TableCommand>();
            foreach (Player player in players)
            {
                if (player.GetScore() < 21)
                {
                    Console.Out.WriteLine($"Player {player.Name}: Hit (H) or Stand (S)?");
                    String input = Console.ReadLine();
                    if (input == "H" || input == "h")
                    {
                        commands.Add(new TableCommand() {action = TableCommand.Action.Hit, player = player});
                    }
                    else
                    {
                        commands.Add(new TableCommand() {action = TableCommand.Action.Stand, player = player});
                    }
                }
                else
                {
                    commands.Add(new TableCommand() {action = TableCommand.Action.Bust, player = player});
                }
            }

            foreach (TableCommand command in commands)
            {
                table.Play(command);
            }

            if (commands.Count(x => x.action == TableCommand.Action.Hit) >= 1)
            {
                PlayDealer(table);
            }
            else//American Rule; Te Dealer Draws until 21 or bust, if no hits were performed
            {
                ResolveGame(table);
            }
        }

        static void PlayDealer(Table table)
        {
            if (table.dealer.GetScore() < 17)
            {
                table.Play(new TableCommand() {action = TableCommand.Action.Hit, player = table.dealer});
            }
            else
            {
                table.Play(new TableCommand() {action = TableCommand.Action.Stand, player = table.dealer});
            }
        }

        static void ResolveGame(Table table)
        {
            Console.WriteLine("No Hits taken. Resolving the Game.");
            
            while (!table.GameOver())
            {
                table.Play(new TableCommand() {action = TableCommand.Action.Hit, player = table.dealer});
            }
        }

        static int GetPlayerCount()
        {
            int playercount = 0;
            while (playercount == 0)
            {
                Console.WriteLine("How many are playing today? ");
                String input = Console.ReadLine();
                try
                {
                    playercount = int.Parse(input);
                }
                catch (FormatException fex)
                {
                    Console.Out.WriteLine("That's not a number i can accept.");
                }
            }

            return playercount;
        }

        static Player[] InitPlayers(int NumberOfPlayers)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Console.Out.WriteLine($"Player {i+1}, please enter your name: ");
                String input = Console.ReadLine();
                players.Add(new Player(input));
            }

            return players.ToArray();
        }
    }
}