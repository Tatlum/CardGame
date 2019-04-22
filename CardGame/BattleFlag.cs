using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class BattleFlag : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Hand;
        public override string Name { get; set; } = "Боевое знамя";
        public override string Description { get; set; } = "Все ваши существа на столе получают +1 к Атаке.";

        private int attackIncrease;

        public BattleFlag(int attackIncrease)
        {
            this.attackIncrease = attackIncrease;
        }

        protected override void SetManualAction()
        {
            foreach (Card creature in AbilitySandbox.GetTableCreatures(PlayerType.CurrentPlayer))
            {
                AbilitySandbox.SetCreatureAttack(creature, AbilitySandbox.GetCreatureAttack(creature) + attackIncrease);
            }
        }
    }
}
