using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Create : Command
    {
        

        public Create()
        {
            this.Name = "create";
        }

        public override void Execute(string[] args)
        {
            string name = Kernel.globalName[1];
            string text = "", line = "", checkSave;
            int x = 1;
            
                bool test = true;
                while (test)
                {
                    Console.Write(x.ToString() + ". ");
                    checkSave = Console.ReadLine();
                    line = checkSave + "\n";
                    if (checkSave == "save")
                    {
                        Console.WriteLine("*** File Saved ***");
                        break;
                    }
                    text += line;
                    x++;
                }
                
                Kernel.setNode(name, text);
                //Console.WriteLine("Tadaa");
            }
    }
}
