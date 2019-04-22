using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    // Способность Копейщика.
    internal class InstantFury : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = string.Empty;
        public override string Description { get; set; } = "При выкладывании на стол наносит 3 единицы урона случайному существу противника.";

        private int attackPower;

        public InstantFury(int attackPower)
        {
            this.attackPower = attackPower;
        }

        protected override void SetCardPutOnTable()
        {
            Card card = AbilitySandbox.GetRandTableCreature(PlayerType.EnemyPlayer);

            if (card != null)
                AbilitySandbox.DamageCreature(card, attackPower);
        }
    }
}
