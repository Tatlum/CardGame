using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// В картах хранятся значения параметров атаки и здоровья без учёта аурических способностей.
    /// Для получения истинных значений параметров нужно использовать этот менеджер.
    /// </summary>
    internal static class CardParameterManager
    {
        public static int GetAttack(Card card)
        {
            if (card.HasComponent<CreatureComponent>())
                return SetAffects(card).GetComponent<CreatureComponent>().CurrentAttack;
            else
                return 0;
        }

        public static int GetHealth(Card card)
        {
            if (card.HasComponent<CreatureComponent>())
                return SetAffects(card).GetComponent<CreatureComponent>().CurrentHealth;
            else
                return 0;
        }

        private static Card SetAffects(Card card)
        {
            Card copy = card.Copy();

            if (card.Position == InGamePosition.Table)
            {
                foreach (Card creature in GameManager.ActivePlayer.Table)
                {
                    foreach (Affect affect in creature.GetAffects())
                    {
                        affect.SetAffect(copy);
                    }
                }

                foreach (Card creature in GameManager.PassivePlayer.Table)
                {
                    foreach (Affect affect in creature.GetAffects())
                    {
                        affect.SetAffect(copy);
                    }
                }
            }

            return copy;
        }
    }
}
