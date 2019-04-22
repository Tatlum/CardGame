using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class Revive : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Hand;
        public override string Name { get; set; } = "Копать";
        public override string Description { get; set; } = "Возвращает случайное существо из вашего кладбища обратно в руку.";

        protected override void SetManualAction()
        {
            AbilitySandbox.ReviveCard(AbilitySandbox.GetRandGraweyardCreature(PlayerType.CurrentPlayer));
        }
    }
}
