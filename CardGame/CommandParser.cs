using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardGame.ConsoleUI
{
    internal static class CommandParser
    {
        public static CommandInfo Parse(string rawCommand)
        {
            rawCommand = rawCommand.ToLower().Trim();

            // Команда является точкой.
            if (rawCommand == ".")
            {
                return new CommandInfo()
                {
                    CommandType = CommandType.EndTurn,
                };
            }

            int number;

            // Команда является числом.
            if (int.TryParse(rawCommand, out number))
            {
                return new CommandInfo()
                {
                    CommandType = CommandType.ShowTableCard,
                    Index = number - 1
                };
            }

            // Команда является символом 'а' за которым идёт число.
            var match = Regex.Match(rawCommand, @"^a\d+$");

            if (match.Success)
            {
                return new CommandInfo()
                {
                    CommandType = CommandType.UseAbility,
                    Index = int.Parse(match.Value.Substring(1)) - 1
                };
            }

            // Команда является любым не числовым символом.
            if (rawCommand.Length == 1)
            {
                return new CommandInfo()
                {
                    CommandType = CommandType.ShowHandCard,
                    Index = CharToNumber(rawCommand[0])
                };
            }

            // Команда не является ни чем вышеперечисленным.
            return new CommandInfo()
            {
                CommandType = CommandType.Unknown
            };
        }

        private static int CharToNumber(char c)
        {
            return c - 'a';
        }
    }
}
