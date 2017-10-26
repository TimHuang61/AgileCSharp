using System;

namespace Demo_CP
{
    internal class SecondOfLeaf : IComponent
    {
        public void DoSomething()
        {
            Console.WriteLine($"SecondOfLeaf:{this.GetHashCode()}");
        }
    }
}