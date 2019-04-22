using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class Attack : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = "Атаковать";

        protected override void SetManualAction()
        {
            Card enemy = AbilitySandbox.GetRandTableCreature(PlayerType.EnemyPlayer);

            if (enemy != null)
            {
                // TODO: Не учитываются аффекты
                AbilitySandbox.DamageCreature(enemy, AbilitySandbox.GetCreatureAttack(bindedCard));
                AbilitySandbox.DamageCreature(bindedCard, AbilitySandbox.GetCreatureAttack(bindedCard));
            }
        }
    }
}
