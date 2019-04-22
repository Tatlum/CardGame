using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.ConsoleUI;

namespace CardGame
{
    public static class Program
    {
        private static void Main()
        {
            StartMatch();
        }

        private static void StartMatch()
        {
            // Настраиваем игроков и выбираем кто ими управляет.
            // Если передать обоим ConsoleActor, то можно играть вдвоём.
            Player player1 = new Player("Игрок 1", new ConsoleActor());
            Player player2 = new Player("Игрок 2", new AIActor());

            GameManager.InitGame(player1, player2);

            while (true)
            {
                if (GameManager.IsGameEnd)
                {
                    ConsolePrinter.PrintGameResult(GameManager.Winner);
                    break;
                }

                GameManager.BeginTurn(GameManager.ActivePlayer);
                GameManager.ActivePlayer.Actor.PlayTurn(GameManager.ActivePlayer, GameManager.PassivePlayer);
            }
        }
    }
}
