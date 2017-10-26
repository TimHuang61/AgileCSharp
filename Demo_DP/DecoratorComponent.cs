using System;

namespace Demo_DP
{
    internal class DecoratorComponent : IComponent
    {
        private readonly IComponent component;

        public DecoratorComponent(IComponent component)
        {
            this.component = component;
        }

        public void DoSomething()
        {
            component?.DoSomething();
            Console.WriteLine("DecoratorComponent:DoSomething");
        }
    }
}