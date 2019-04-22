using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Базовый класс каждой способности.
    /// Способность при реализации должна переопределять методы,
    /// в соответствие с событиями, по которым она будет срабатывать.
    /// </summary>
    internal abstract class Ability
    {
        public abstract InGamePosition ZoneOfUse { get; set; }
        public abstract string Name { get; set; }
        // TODO: В описаниях способностей захардкожены значения параметров.
        public virtual string Description { get; set; } = string.Empty;

        // Решение на скорую руку. Если при реализации способности переопределяется 
        // метод ManualAction(), то считается, что способность можно активировать вручную.
        public bool IsManual
        {
            get
            {
                Type type = GetType();
                return type.GetMethod("SetManualAction", 
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    .DeclaringType == type ? true : false;
            }
        }

        internal Card bindedCard;

        protected virtual void SetManualAction() { }

        protected virtual void SetCardGetDamage() { }

        protected virtual void SetCardGetHeal() { }

        protected virtual void SetCardPutOnTable() { }

        protected virtual void SetCardLeaveFromTable() { }

        public void DoManualAction()
        {
            CardManager.StartAction();
            SetManualAction();
            CardManager.EndAction();
        }
        public void DoCardGetDamage()
        {
            CardManager.StartAction();
            SetCardGetDamage();
            CardManager.EndAction();
        }
        public void DoCardGetHeal()
        {
            CardManager.StartAction();
            SetCardGetHeal();
            CardManager.EndAction();
        }
        public void DoCardPutOnTable()
        {
            CardManager.StartAction();
            SetCardPutOnTable();
            CardManager.EndAction();
        }
        public void DoCardLeaveFromTable()
        {
            CardManager.StartAction();
            SetCardLeaveFromTable();
            CardManager.EndAction();
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
