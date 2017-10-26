using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_CP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 複合模式，如果分支太多也會造成難以維護
            // 因為子component還可以在包一個複合
            var composite = new CompositeComponent();
            var leaf = new Leaf();
            var secondOfLeaf = new SecondOfLeaf();
            composite.AddComponent(leaf);
            composite.AddComponent(secondOfLeaf);
            composite.DoSomething();


            composite.RemoveComponent(leaf);
            composite.DoSomething();

            Console.Read();
        }
    }
}
