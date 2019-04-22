using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.ConsoleUI
{
    internal class MappingData
    {
        public Dictionary<string, Card> HandCardMapping = new Dictionary<string, Card>();
        public Dictionary<string, Card> TableCardMapping = new Dictionary<string, Card>();
        public Dictionary<string, Ability> AbilityMapping = new Dictionary<string, Ability>();
    }
}
