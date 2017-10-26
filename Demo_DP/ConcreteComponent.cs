using System;

namespace Demo_DP
{
    internal class ConcreteComponent : IComponent
    {
        public void DoSomething()
        {
            Console.WriteLine("ConcreteComponent:DoSomething");
        }
    }
}