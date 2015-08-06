using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Clear : Command
    {

        public Clear()
        {
            this.Name = "clear";
        }

        public override void Execute(string[] args)
        {
            Console.Clear();
        }
    }
}
