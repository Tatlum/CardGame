using System.Collections.Generic;

namespace CardGame
{
    internal class Card : GameObject
    {
        public Player Owner { get; set; }
        public string Name { get; set; }
        public InGamePosition Position { get; set; }
        public bool IsTurned = false;

        private List<Ability> abilities = new List<Ability>();
        private List<Affect> affects = new List<Affect>();

        public Card(string name)
        {
            Name = name;
        }

        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
            ability.bindedCard = this;
        }

        public void AddAffect(Affect affect)
        {
            affects.Add(affect);
            affect.bindedCard = this;
        }

        public List<Ability> GetAbilities()
        {
            return abilities;
        }

        public List<Affect> GetAffects()
        {
            return affects;
        }

        public Card Copy()
        {
            Card copy = new Card(this.Name);
            copy.Owner = Owner;
            copy.Position = Position;
            copy.IsTurned = IsTurned;

            foreach (IComponent component in components)
            {
                copy.components.Add(component.Copy());
            }

            copy.abilities = abilities;
            copy.affects = affects;

            return copy;
        }
    }
}