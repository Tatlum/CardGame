using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Player
    {
        public int CanPlayCardsOnTurn = 2;

        public List<Card> Hand = new List<Card>();
        public List<Card> Table = new List<Card>();
        public List<Card> Graveyard = new List<Card>();
        public List<Card> Deck = new List<Card>();

        public string Name;
        public IActor Actor;

        public Player(string name, IActor actor)
        {
            Name = name;
            Actor = actor;
        }
    }
}
