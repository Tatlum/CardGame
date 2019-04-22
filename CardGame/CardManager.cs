using CardGame.Library.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Класс со всей базовой логикой управления картами.
/// </summary>
namespace CardGame
{
    internal static class CardManager
    {
        private static int actionsInStack = 0;

        public static void CreateBaseDeck(Player player)
        {
            List<Card> deck = new List<Card>()
            {
                CardLibrary.CreateLeader(),
                CardLibrary.CreateWarrior(),
                CardLibrary.CreateBerserker(),
                CardLibrary.CreateHealer(),
                CardLibrary.CreateKnight(),
                CardLibrary.CreateSpearman(),
                CardLibrary.CreatePreacher(),
                CardLibrary.CreateLeeroyJenkins(),
                CardLibrary.CreateSoulReaper(),
                CardLibrary.CreateGuardsman(),
                CardLibrary.CreateRider(),
                CardLibrary.CreateSkeleton(),
                CardLibrary.CreateGhoul(),

                CardLibrary.CreateFireball(),
                CardLibrary.CreateShovel(),
                CardLibrary.CreateGrace(),
            };

            foreach (Card card in deck)
            {
                card.Owner = player;
            }

            player.Deck = deck;
        }

        public static void StartAction()
        {
            ++actionsInStack;
        }

        public static void EndAction()
        {
            if (--actionsInStack == 0)
            {
                for (int i = 0; i < GameManager.ActivePlayer.Table.Count; i++)
                {
                    if (GameManager.ActivePlayer.Table[i].HasComponent<CreatureComponent>() &&
                        GameManager.ActivePlayer.Table[i].GetComponent<CreatureComponent>().CurrentHealth <= 0)
                    {
                        ReplaceCard(GameManager.ActivePlayer.Table[i], InGamePosition.Graveyard);
                        --i;
                    }
                }

                for (int i = 0; i < GameManager.PassivePlayer.Table.Count; i++)
                {
                    if (GameManager.PassivePlayer.Table[i].HasComponent<CreatureComponent>() &&
                        GameManager.PassivePlayer.Table[i].GetComponent<CreatureComponent>().CurrentHealth <= 0)
                    {
                        ReplaceCard(GameManager.PassivePlayer.Table[i], InGamePosition.Graveyard);
                        --i;
                    }
                }
            }
        }

        public static void ReplaceCard(Card card, InGamePosition inGamePosition)
        {
            switch (card.Position)
            {
                case InGamePosition.Unknown:
                    break;
                case InGamePosition.Deck:
                    card.Owner.Deck.Remove(card);
                    break;
                case InGamePosition.Hand:
                    card.Owner.Hand.Remove(card);
                    break;
                case InGamePosition.Table:
                    card.Owner.Table.Remove(card);
                    card.GetAbilities().ForEach(a => a.DoCardLeaveFromTable());
                    break;
                case InGamePosition.Graveyard:
                    card.Owner.Graveyard.Remove(card);
                    break;
            }

            switch (inGamePosition)
            {
                case InGamePosition.Unknown:
                    card.Position = InGamePosition.Unknown;
                    break;
                case InGamePosition.Deck:
                    card.Owner.Deck.Add(card);
                    card.Position = InGamePosition.Deck;
                    break;
                case InGamePosition.Hand:
                    card.Owner.Hand.Add(card);
                    card.Position = InGamePosition.Hand;
                    break;
                case InGamePosition.Table:
                    card.Owner.Table.Add(card);
                    card.GetAbilities().ForEach(a => a.DoCardPutOnTable());
                    card.Position = InGamePosition.Table;
                    break;
                case InGamePosition.Graveyard:
                    card.Owner.Graveyard.Add(card);
                    card.Position = InGamePosition.Graveyard;
                    break;
            }
        }

        public static void ResetCard(Card card)
        {
            card.IsTurned = false;

            if (card.HasComponent<CreatureComponent>())
            {
                CreatureComponent creatureComponent = card.GetComponent<CreatureComponent>();

                creatureComponent.CurrentAttack = creatureComponent.BaseAttack;
                creatureComponent.CurrentHealth = creatureComponent.BaseHealth;
            }
        }

        public static void UseAbility(Ability ability)
        {
            if (!ability.bindedCard.IsTurned)
            {
                if (ability.bindedCard.HasComponent<CreatureComponent>())
                {
                    ability.DoManualAction();
                    ability.bindedCard.IsTurned = true;
                }
                else if (ability.bindedCard.HasComponent<CharmComponent>())
                {
                    ability.DoManualAction();
                    ReplaceCard(ability.bindedCard, InGamePosition.Graveyard);
                }

                if (ability.ZoneOfUse == InGamePosition.Hand)
                    GameManager.DecreasePutCardsCounter();
            }
        }

        public static bool IsCanAbilityManualUsedNow(Ability ability)
        {
            Card card = ability.bindedCard;
            return ability.IsManual && !card.IsTurned && card.Position == ability.ZoneOfUse && !(ability.ZoneOfUse == InGamePosition.Hand && !GameManager.CanPutCardOnTable);
        }

        public static void TakeCardFromDeck(Player player, Card card)
        {
            if (player.Deck.Contains(card))
            {
                player.Hand.Add(card);
                player.Deck.Remove(card);
                card.Position = InGamePosition.Hand;
            }
        }

        public static void CardSetHealth(Card card, int value)
        {
            if (card.HasComponent<CreatureComponent>())
            {
                CreatureComponent creature = card.GetComponent<CreatureComponent>();

                if (creature.CurrentHealth < value)
                {
                    foreach (var ability in card.GetAbilities())
                    {
                        ability.DoCardGetDamage();
                    }
                }

                if (creature.CurrentHealth > value && creature.CurrentHealth < creature.MaxHealth)
                {
                    foreach (var ability in card.GetAbilities())
                    {
                        ability.DoCardGetHeal();
                    }
                }

                creature.CurrentHealth = GameMath.Clamp(value, 0, creature.MaxHealth);
            }
        }

        public static List<Card> GetCreatures(PlayerType playerType, InGamePosition inGamePosition)
        {
            // TODO: GetPlayer может вернуть null.
            switch (inGamePosition)
            {
                case InGamePosition.Unknown:
                    return null;
                case InGamePosition.Deck:
                    return GameManager.GetPlayer(playerType).Deck
                        .FindAll(card => card.HasComponent<CreatureComponent>());
                case InGamePosition.Hand:
                    return GameManager.GetPlayer(playerType).Hand
                        .FindAll(card => card.HasComponent<CreatureComponent>());
                case InGamePosition.Table:
                    return GameManager.GetPlayer(playerType).Table
                        .FindAll(card => card.HasComponent<CreatureComponent>());
                case InGamePosition.Graveyard:
                    return GameManager.GetPlayer(playerType).Graveyard
                        .FindAll(card => card.HasComponent<CreatureComponent>());
                default:
                    return null;
            }
        }

        public static Card GetRandCreature(PlayerType playerType, InGamePosition inGamePosition)
        {
            var creatures = GetCreatures(playerType, inGamePosition);

            if (creatures.Count > 0)
                return creatures[GameMath.Rand.Next(0, creatures.Count)];
            else
                return null;
        }

        public static List<Card> GetCards(PlayerType playerType, InGamePosition inGamePosition)
        {
            // TODO: GetPlayer может вернуть null.
            switch (inGamePosition)
            {
                case InGamePosition.Unknown:
                    return null;
                case InGamePosition.Deck:
                    return GameManager.GetPlayer(playerType).Deck;
                case InGamePosition.Hand:
                    return GameManager.GetPlayer(playerType).Hand;
                case InGamePosition.Table:
                    return GameManager.GetPlayer(playerType).Table;
                case InGamePosition.Graveyard:
                    return GameManager.GetPlayer(playerType).Graveyard;
                default:
                    return null;
            }
        }

        public static Card GetRandCard(PlayerType playerType, InGamePosition inGamePosition)
        {
            var cards = GetCards(playerType, inGamePosition);

            if (cards.Count > 0)
                return cards[GameMath.Rand.Next(0, cards.Count)];
            else
                return null;
        }

        public static IEnumerable<Ability> GetCardAbilities(Card card, InGamePosition zoneOfUse)
        {
            return card.GetAbilities().FindAll(a => a.ZoneOfUse == zoneOfUse);
        }

        public static List<Ability> GetAvaliableManualAbilities(Card card)
        {
            return (from a in card.GetAbilities()
                    where IsCanAbilityManualUsedNow(a)
                    select a).ToList();
        }

        public static Ability GetRandAvaliableAbility(Card card)
        {
            var abilities = GetAvaliableManualAbilities(card);

            if (abilities.Count > 0)
                return abilities[GameMath.Rand.Next(0, abilities.Count)];
            else
                return null;
        }

        public static Card GetPlaifulCard(Player player)
        {
            foreach (Card card in player.Table)
            {
                var ability = GetRandAvaliableAbility(card);

                if (ability != null)
                    return card;
            }

            return null;
        }
    }
}
