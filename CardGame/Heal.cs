using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    // Способность Лекаря.
    // TODO: Должно лечить до изначального максимума.
    internal class Heal : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Table;
        public override string Name { get; set; } = "Лечение";
        public override string Description { get; set; } = "Лечит ваше случайное существо на 2 единицы здоровья.";

        private int healPower;

        public Heal(int healPower)
        {
            this.healPower = healPower;
        }

        protected override void SetManualAction()
        {
            var creature = AbilitySandbox.GetRandTableCreature(PlayerType.CurrentPlayer);
            AbilitySandbox.LimitedHealCreature(creature, healPower);
        }
    }
}
