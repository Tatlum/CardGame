using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    /// <summary>
    /// Простенькая система компонентов для реализации существ и заклинаний.
    /// </summary>
    internal abstract class GameObject
    {
        protected List<IComponent> components = new List<IComponent>();

        public bool HasComponent<T>() where T : class, IComponent
        {
            foreach (IComponent item in components)
            {
                if (item is T)
                    return true;
            }

            return false;
        }

        public T GetComponent<T>() where T : class, IComponent
        {
            foreach (IComponent item in components)
            {
                if (item is T)
                    return item as T;
            }

            return null;
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }
    }
}
