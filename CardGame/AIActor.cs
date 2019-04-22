using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Примитивный ИИ.
    /// </summary>
    internal class AIActor : IActor
    {
        public void PlayTurn(Player me, Player enemy)
        {
            while (GameManager.CanPutCardOnTable)
            {
                Card card = CardManager.GetRandCard(PlayerType.CurrentPlayer, InGamePosition.Hand);

                if (card == null)
                    break;
                else
                    CardManager.UseAbility(card.GetAbilities()[0]);
            }

            while (true)
            {
                Card card = CardManager.GetPlaifulCard(me);

                if (card == null)
                    break;

                var ability = CardManager.GetRandAvaliableAbility(card);
                CardManager.UseAbility(ability);
            }

            GameManager.EndTurn();
        }
    }
}
