using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Out : Command
    {
        public Out()
        {
            this.Name = "out";
        }

        public override void Execute(string[] args)
        {
            string output = Kernel.output(Kernel.globalName[1]);

            if (output[0] == 34)
            {
                output = replace(output);
            }
            Console.WriteLine(output);
        }

        public static string replace(string output)
        {
            int x = 0;
            string newOutput = "";

            while (x < output.Length)
            {
                //if it's a quotation mark or + sign, skip it.
                if (output[x] == 34 || output[x] == 43)
                {
                    newOutput += "";
                    x++;
                    continue;
                }
                newOutput += output[x];
                x++;
            }
            return newOutput;
        }
    }
}
