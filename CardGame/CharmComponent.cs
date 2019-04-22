using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    internal class CharmComponent : IComponent
    {
        public IComponent Copy()
        {
            return new CharmComponent();
        }
    }
}
