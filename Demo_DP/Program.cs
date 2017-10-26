using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DP
{
    class Program
    {
        static void Main(string[] args)
        {
            var component = new DecoratorComponent(new ConcreteComponent());
            component.DoSomething();

            Console.Read();
        }
    }
}
