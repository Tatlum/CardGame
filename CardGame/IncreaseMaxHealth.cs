using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Affects
{
    internal class IncreaseMaxHealth : Affect
    {
        public override string Description { get; set; } = "Пока это существо на столе, все другие ваши существа получают +1 к Здоровью.";
        private int healthIncrease;

        public IncreaseMaxHealth(int healthIncrease)
        {
            this.healthIncrease = healthIncrease;
        }

        public override void SetAffect(Card card)
        {
            if (bindedCard.Position == InGamePosition.Table &&
                bindedCard.Owner == card.Owner)
            {
                AbilitySandbox.IncreaseCreatureMaxHealth(card, healthIncrease);
                AbilitySandbox.LimitedHealCreature(card, healthIncrease);
            }
        }
    }
}
