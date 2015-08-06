using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2
{
    abstract class Command
    {
        public abstract void Execute(string[] args);
        public string Name;
    }
}
