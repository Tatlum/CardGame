using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    // Способность Лероя.
    // TODO: Должна ли способность действовать на самого себя?
    internal class DumpAttack : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = string.Empty;
        public override string Description { get; set; } = "При выкладывании на стол все существа получают 1 единицу урона.";

        private int attackPower;

        public DumpAttack(int attackPower)
        {
            this.attackPower = attackPower;
        }

        protected override void SetCardPutOnTable()
        {
            foreach (Card card in AbilitySandbox.GetTableCreatures(PlayerType.CurrentPlayer))
            {
                AbilitySandbox.DamageCreature(card, attackPower);
            }

            foreach (Card card in AbilitySandbox.GetTableCreatures(PlayerType.EnemyPlayer))
            {
                AbilitySandbox.DamageCreature(card, attackPower);
            }
        }
    }
}
