using CardGame.Library.Abilities;
using CardGame.Library.Affects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Library.Cards
{
    internal static class CardLibrary
    {
        private static Card CreateCreature(string name, int attack, int health)
        {
            Card card = new Card(name);
            card.AddComponent(new CreatureComponent(attack, health));
            card.AddAbility(new PlayCard());
            card.AddAbility(new Attack());

            return card;
        }

        private static Card CreateCharm(string name)
        {
            Card card = new Card(name);
            card.AddComponent(new CharmComponent());

            return card;
        }

        public static Card CreateLeader()
        {
            return CreateCreature("Предводитель", 2, 6);
        }

        public static Card CreateWarrior()
        {
            return CreateCreature("Воитель", 1, 2);
        }

        public static Card CreateBerserker()
        {
            Card card = CreateCreature("Берсеркер", 1, 4);
            card.AddAbility(new Rage(1));

            return card;
        }

        public static Card CreateHealer()
        {
            Card card = CreateCreature("Лекарь", 1, 3);
            card.AddAbility(new Heal(2));

            return card;
        }

        public static Card CreateKnight()
        {
            return CreateCreature("Рыцарь", 2, 4);
        }

        public static Card CreateSpearman()
        {
            Card card = CreateCreature("Копейщик", 1, 2);
            card.AddAbility(new InstantFury(3));

            return card;
        }

        public static Card CreatePreacher()
        {
            Card card = CreateCreature("Проповедник", 0, 3);
            card.AddAffect(new IncreaseMaxHealth(1));

            return card;
        }

        public static Card CreateLeeroyJenkins()
        {
            Card card = CreateCreature("Лерой Дженкинс", 1, 1);
            card.AddAbility(new DumpAttack(1));

            return card;
        }

        public static Card CreateSoulReaper()
        {
            Card card = CreateCreature("Похититель Душ", 1, 1);
            card.AddAbility(new InstantKill());

            return card;
        }

        public static Card CreateGuardsman()
        {
            Card card = CreateCreature("Гвардеец", 2, 2);

            return card;
        }

        public static Card CreateRider()
        {
            Card card = CreateCreature("Наездник", 3, 3);

            return card;
        }

        public static Card CreateSkeleton()
        {
            Card card = CreateCreature("Скелет", 1, 1);

            return card;
        }

        public static Card CreateGhoul()
        {
            Card card = CreateCreature("Вурдалак", 2, 1);

            return card;
        }

        public static Card CreateFireball()
        {
            Card card = CreateCharm("Огненный шар");
            card.AddAbility(new Fireball(3));

            return card;
        }

        public static Card CreateGrace()
        {
            Card card = CreateCharm("Благодать");
            card.AddAbility(new Grace(1));

            return card;
        }

        public static Card CreateShovel()
        {
            Card card = CreateCharm("Лопата");
            card.AddAbility(new Revive());

            return card;
        }
    }
}
