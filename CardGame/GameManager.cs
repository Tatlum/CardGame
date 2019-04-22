using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Класс инициализирующий игру, управляющий очерёдностью ходов и определяющий победителя.
    /// </summary>
    internal static class GameManager
    {
        public static Player ActivePlayer;
        public static Player PassivePlayer;

        public static int CanPutCardsOnTurn = 2;
        private static int putCardsCounter;

        public static bool CanPutCardOnTable
        {
            get
            {
                return putCardsCounter > 0;
            }
        }

        public static bool IsGameEnd { get; internal set; }
        public static Player Winner { get; internal set; }

        public static void InitGame(Player player1, Player player2)
        {
            int firstPlayer = GameMath.Rand.Next(0, 2);

            ActivePlayer = firstPlayer == 0 ? player1 : player2;
            PassivePlayer = firstPlayer == 0 ? player2 : player1;

            CardManager.CreateBaseDeck(player1);
            CardManager.CreateBaseDeck(player2);

            InitHand(ActivePlayer, 10);
            InitHand(PassivePlayer, 11);
        }

        public static void InitHand(Player player, int cardsNumber)
        {
            for (int i = 0; i < cardsNumber; ++i)
            {
                Card card = player.Deck[GameMath.Rand.Next(0, player.Deck.Count)];
                CardManager.TakeCardFromDeck(player, card);
            }
        }

        public static void BeginTurn(Player player)
        {
            putCardsCounter = CanPutCardsOnTurn;

            foreach (var card in player.Table)
            {
                card.IsTurned = false;
            }
        }

        private static void SwapPlayers()
        {
            Player temp = ActivePlayer;
            ActivePlayer = PassivePlayer;
            PassivePlayer = temp;
        }

        public static Player GetPlayer(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.CurrentPlayer: return ActivePlayer;
                case PlayerType.EnemyPlayer: return PassivePlayer;
                default: return null;
            }
        }

        public static void EndTurn()
        {
            SwapPlayers();

            if (IsLoose(ActivePlayer))
            {
                IsGameEnd = true;
                Winner = PassivePlayer;
            }

            if (IsLoose(PassivePlayer))
            {
                IsGameEnd = true;
                Winner = ActivePlayer;
            }
        }

        private static bool IsLoose(Player player)
        {
            return player.Table.Count == 0 && player.Hand.Count == 0;
        }

        public static void DecreasePutCardsCounter()
        {
            --putCardsCounter;
        }

    }
}
