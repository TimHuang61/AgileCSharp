using System;

namespace Demo_CP
{
    internal class Leaf : IComponent
    {
        public void DoSomething()
        {
            Console.WriteLine($"Leaf:{this.GetHashCode()}");
        }
    }
}