using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Test : Command
    {

        public Test()
        {
            this.Name = "test";
        }

        public override void Execute(string[] args)
        {
            List<string> names = new List<string>();
           // names = Kernel.getName();

            Console.WriteLine(names[0]);
            Console.WriteLine(names[1]);
        }
    }
}
