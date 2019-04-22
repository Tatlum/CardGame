using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Базовый класс для всех аурных способностей.
    /// </summary>
    internal abstract class Affect
    {
        public virtual string Description { get; set; } = string.Empty;
        internal Card bindedCard;
        public abstract void SetAffect(Card card);
    }
}
