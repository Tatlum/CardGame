using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class CreatureComponent : IComponent
    {
        public int BaseAttack { get; }
        public int CurrentAttack { get; set; }

        public int BaseHealth { get; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public CreatureComponent(int attack, int health)
        {
            BaseAttack = attack;
            CurrentAttack = attack;

            BaseHealth = health;
            MaxHealth = health;
            CurrentHealth = health;
        }

        public IComponent Copy()
        {
            CreatureComponent copy = new CreatureComponent(BaseAttack, BaseHealth);
            copy.CurrentAttack = CurrentAttack;
            copy.MaxHealth = MaxHealth;
            copy.CurrentHealth = CurrentHealth;

            return copy;
        }
    }
}
