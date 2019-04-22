using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.ConsoleUI
{
    internal static class ConsolePrinter
    {
        public static void PrintGameState(Player activePlayer, Player passivePlayer)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;

            PrintPassivePlayerTable(passivePlayer);
            PrintActivePlayerTable(activePlayer);
            PrintHand(activePlayer);
            Console.WriteLine("\n\n\nДля завершения хода введите \".\"");
        }

        public static void PrintActivePlayerTable(Player activePlayer)
        {
            int counter = 1;

            Console.Write("Ваш стол: ");

            foreach (var card in activePlayer.Table)
            {
                if (CardManager.GetAvaliableManualAbilities(card).Count > 0)
                {
                    ConsoleColor temp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write('*');
                    Console.ForegroundColor = temp;
                }

                PrintCard(card);
                Console.Write($"#{counter++}, ");
            }

            Console.WriteLine();
        }

        internal static void PrintGameResult(Player winner)
        {
            Console.WriteLine($"Победитель: {winner.Name}");
            Console.ReadLine();
        }

        public static void PrintCardInfo(Card card)
        {
            char commandLetter = 'a';
            int commandIndex = 1;

            //TODO: Не пишется комманда для выбора карты.
            Console.WriteLine();
            Console.Write($"Выбрана карта ");
            PrintCard(card);

            foreach (Ability ability in card.GetAbilities())
            {
                if (!string.IsNullOrWhiteSpace(ability.Description))
                {
                    Console.Write($"\n{ability.Description}");
                }
            }

            foreach (Affect affect in card.GetAffects())
            {
                if (!string.IsNullOrWhiteSpace(affect.Description))
                {
                    Console.Write($"\n{affect.Description}");
                }
            }

            Console.WriteLine();
            Console.Write($"Команды: ");

            foreach (Ability ability in CardManager.GetAvaliableManualAbilities(card))
            {
                Console.Write(ability.Name);
                Console.Write($"#{commandLetter}{commandIndex++}, ");
            }
        }

        public static void PrintPassivePlayerTable(Player passivePlayer)
        {
            Console.Write("Стол противника: ");

            foreach (var card in passivePlayer.Table)
            {
                PrintCard(card);
                Console.Write(", ");
            }

            Console.WriteLine();
        }

        public static void PrintHand(Player activePlayer)
        {
            char cardLetter = 'A';

            Console.WriteLine();
            Console.Write("Ваша рука: ");

            foreach (var card in activePlayer.Hand)
            {
                PrintCard(card);
                Console.Write($"#{cardLetter++}, ");
            }

            Console.WriteLine();
        }

        private static void PrintCard(Card card)
        {
            ConsoleColor temp = Console.ForegroundColor;

            if (card.HasComponent<CreatureComponent>())
                Console.ForegroundColor = ConsoleColor.Gray;

            if (card.HasComponent<CharmComponent>())
                Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.Write($"{card.Name}");

            Console.ForegroundColor = temp;

            if (card.HasComponent<CreatureComponent>())
            {
                Console.Write("(");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{CardParameterManager.GetAttack(card)}");
                Console.ForegroundColor = temp;
                Console.Write("/");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"{CardParameterManager.GetHealth(card)}");
                Console.ForegroundColor = temp;
                Console.Write(")");

            }

        }

        public static CommandInfo ProcessCommand()
        {
            Console.WriteLine();
            Console.Write("Введите команду: ");
            return CommandParser.Parse(Console.ReadLine());
        }

        public static void PrintWrongCommand()
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Недопустимая команда");
            Console.ReadLine();

            Console.ForegroundColor = temp;
        }
    }
}
