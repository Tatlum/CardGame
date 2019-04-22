using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    // Способность Берсеркера.
    internal class Rage : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = "Ярость";
        public override string Description { get; set; } = "Атака увеличивается на 1 каждый раз, когда это существо получает урон.";

        private int attackIncrease;

        public Rage(int attackIncrease)
        {
            this.attackIncrease = attackIncrease;
        }

        protected override void SetCardGetDamage()
        {
            AbilitySandbox.SetCreatureAttack(bindedCard, AbilitySandbox.GetCreatureAttack(bindedCard) + attackIncrease);
        }
    }
}
