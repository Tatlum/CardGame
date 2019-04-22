using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class Fireball : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Hand;
        public override string Name { get; set; } = "Огненный шар";
        public override string Description { get; set; } = "Наносит 3 единицы урона случайному существу противника.";

        private int attackPower;

        public Fireball(int attackPower)
        {
            this.attackPower = attackPower;
        }

        protected override void SetManualAction()
        {
            Card enemy = AbilitySandbox.GetRandTableCreature(PlayerType.EnemyPlayer);
            AbilitySandbox.DamageCreature(enemy, attackPower);
        }
    }
}
