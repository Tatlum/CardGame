using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.ConsoleUI
{
    /// <summary>
    /// Класс реализующий взаимодействие с игрой через консоль.
    /// </summary>
    internal class ConsoleActor : IActor
    {
        private static Card currentCard;

        public void PlayTurn(Player activePlayer, Player enemyPlayer)
        {
            while (true)
            {
                ConsolePrinter.PrintGameState(activePlayer, enemyPlayer);

                if (currentCard != null)
                {
                    ConsolePrinter.PrintCardInfo(currentCard);
                }

                CommandInfo commandInfo = ConsolePrinter.ProcessCommand();

                if (commandInfo.CommandType == CommandType.EndTurn)
                {
                    currentCard = null;
                    break;
                }

                if (commandInfo.CommandType == CommandType.ShowHandCard)
                {
                    if (CheckIndex(activePlayer.Hand, commandInfo.Index))
                    {
                        currentCard = activePlayer.Hand[commandInfo.Index];
                    }
                }

                if (commandInfo.CommandType == CommandType.ShowTableCard)
                {
                    if (CheckIndex(activePlayer.Table, commandInfo.Index))
                    {
                        currentCard = activePlayer.Table[commandInfo.Index];
                    }
                }

                if (commandInfo.CommandType == CommandType.UseAbility && currentCard != null)
                {
                    var abilities = CardManager.GetAvaliableManualAbilities(currentCard);

                    if (CheckIndex(abilities, commandInfo.Index))
                    {
                        CardManager.UseAbility(abilities[commandInfo.Index]);
                        currentCard = null;
                    }
                }
            }

                GameManager.EndTurn();
        }

        private static bool CheckIndex<T>(IEnumerable<T> collection, int index)
        {
            return index >= 0 && index < collection.Count();
        }
    }
}
