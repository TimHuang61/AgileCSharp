using System.Collections.Generic;

namespace Demo_CP
{
    internal class CompositeComponent : IComponent
    {
        private ICollection<IComponent> components;

        public CompositeComponent()
        {
            components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            components.Remove(component);
        }


        public void DoSomething()
        {
            foreach (var component in components)
            {
                component.DoSomething();
            }
        }
    }
}