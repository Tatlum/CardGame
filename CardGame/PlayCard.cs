using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Abilities
{
    internal class PlayCard : Ability
    {
        public override InGamePosition ZoneOfUse { get; set; } = InGamePosition.Hand;
        public override string Name { get; set; } = "Выложить существо на стол";

        public PlayCard()
        {

        }

        protected override void SetManualAction()
        {
            AbilitySandbox.PlayCard(bindedCard);
        }
    }
}
