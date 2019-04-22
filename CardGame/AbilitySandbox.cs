using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Набор часто используемых методов для упрощения описания способностей карт.
    /// Можно было бы включить методы прямо в класс Ability и получить тем самым
    /// паттерн Подкласс песочница.
    /// </summary>
    internal static class AbilitySandbox
    {
        public static Card GetRandTableCreature(PlayerType playerType)
        {
            return CardManager.GetRandCreature(playerType, InGamePosition.Table);
        }

        public static Card GetRandGraweyardCreature(PlayerType playerType)
        {
            return CardManager.GetRandCreature(playerType, InGamePosition.Graveyard);
        }

        public static List<Card> GetTableCreatures(PlayerType playerType)
        {
            return CardManager.GetCreatures(playerType, InGamePosition.Table);
        }

        public static void DamageCreature(Card card, int value)
        {
            if (card != null && card.HasComponent<CreatureComponent>())
            {
                CreatureComponent creature = card.GetComponent<CreatureComponent>();
                CardManager.CardSetHealth(card, creature.CurrentHealth - value);
            }
        }

        /// <summary>
        /// Восстанавливает здоровье карты не выше, чем до изначального максимума.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="value"></param>
        public static void LimitedHealCreature(Card card, int value)
        {
            if (card != null && card.HasComponent<CreatureComponent>())
            {
                CreatureComponent creature = card.GetComponent<CreatureComponent>();
                CardManager.CardSetHealth(card, GameMath.Clamp(creature.CurrentHealth + value, 0, creature.BaseHealth));
            }
        }

        public static void SetCreatureAttack(Card card, int value)
        {
            if (card != null && card.HasComponent<CreatureComponent>())
            {
                var creature = card.GetComponent<CreatureComponent>();
                creature.CurrentAttack -= value;

                if (creature.CurrentAttack < 0)
                    creature.CurrentAttack = 0;
            }
        }

        /// <summary>
        /// Увеличивает максимальное количество здоровья карты, но не повышает текущее здоровье.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="value"></param>
        public static void IncreaseCreatureMaxHealth(Card card, int value)
        {
            if (card != null && card.HasComponent<CreatureComponent>())
            {
                card.GetComponent<CreatureComponent>().MaxHealth += value;
            }
        }

        /// <summary>
        /// Уменьшает максимальное количество здоровья карты.
        /// Если текущее здоровье оказывается больше максимального,
        /// то текущее здоровье понижается до нового значения максимального.
        /// </summary>
        /// <param name="card"></param>
        /// <param name="value"></param>
        public static void DecreaseCreatureMaxHealth(Card card, int value)
        {
            if (card != null && card.HasComponent<CreatureComponent>())
            {
                CreatureComponent creature = card.GetComponent<CreatureComponent>();

                creature.MaxHealth -= value;

                if (creature.CurrentHealth > creature.MaxHealth)
                {
                    creature.CurrentHealth = creature.MaxHealth;
                }
            }
        }

        public static void PlayCard(Card card)
        {
            if (card != null)
                CardManager.ReplaceCard(card, InGamePosition.Table);
        }

        public static void KillCard(Card card)
        {
            if (card != null)
                CardManager.ReplaceCard(card, InGamePosition.Graveyard);
        }

        public static void ReviveCard(Card card)
        {
            if (card != null)
            {
                CardManager.ReplaceCard(card, InGamePosition.Hand);
                CardManager.ResetCard(card);
            }
        }

        public static int GetCreatureAttack(Card creature)
        {
            return CardParameterManager.GetAttack(creature);
        }

        public static int GetCreatureHealth(Card creature)
        {
            return CardParameterManager.GetHealth(creature);
        }
    }
}
