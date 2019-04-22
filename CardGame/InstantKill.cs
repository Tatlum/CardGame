using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    // Способность Похитителя душ.
    internal class InstantKill : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = string.Empty;
        public override string Description { get; set; } = "При выкладывании на стол переносит случайное существо противника в Сброс.";

        protected override void SetCardPutOnTable()
        {
            Card card = AbilitySandbox.GetRandTableCreature(PlayerType.EnemyPlayer);

            if (card != null)
                AbilitySandbox.KillCard(card);
        }
    }
}
