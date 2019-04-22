using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class Grace : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Hand;
        public override string Name { get; set; } = "Благодать";
        public override string Description { get; set; } = "Вручную: Все ваши существа излечиваются на 1 единицу здоровья.";

        private int healthPower;

        public Grace(int healthPower)
        {
            this.healthPower = healthPower;
        }

        protected override void SetManualAction()
        {
            foreach (var creature in AbilitySandbox.GetTableCreatures(PlayerType.CurrentPlayer))
            {
                AbilitySandbox.LimitedHealCreature(creature, healthPower);
            }
        }
    }
}
