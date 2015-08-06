using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Run : Command
    {
        public Queue<string> queue = new Queue<string>();
        public Run()
        {
            this.Name = "run";
        }

        public override void Execute(string[] args)
        {
            string filename = Kernel.globalName[1];
            string data = Kernel.output(filename);
            string[]  subCommands, list;
            bool boolean = true;
            int x = 0;
            list = Kernel.words;

            if (Kernel.globalName[0] == "run" && Kernel.globalName[1] == "all")
                list = Kernel.globalName;
            //Console.WriteLine("Im in the run class");
            if (list[0] == "run" && list[1] == "all")
            {
                //Console.WriteLine("Im inside the if");
                for (int y = 2; y < list.Length; y++)
                {
                    //Console.WriteLine(list[y] + "****");
                    //Console.Read();
                    subCommands = Kernel.output(list[y]).Split('\n'); //run all time.bat
                   // Console.WriteLine(subCommands[y] + " FILE NAME");
                    //Console.Read();
                    for (int w = 0; w < subCommands.Length - 1; w++)
                    {
                        Console.WriteLine("");
                        //Console.Read();
                       queue.Enqueue(subCommands[w]);
                    }

                   // Console.WriteLine("**********Loop Whore*************");
                }
                Console.WriteLine("");
                
                while (queue.Count > 0)
                {
                    string name = queue.Dequeue();
                    Kernel.globalName = name.Split(' ');
                    Console.WriteLine("");
                    //Console.Read();
                    Kernel.Execute(Kernel.globalName);
                }
                return;
            } 

            else if(list[0] == "run" && list[1] != "all"){

                subCommands = Kernel.output(list[1]).Split('\n');
                //Console.WriteLine("RUN RUN RUN ");
                //Console.Read();
                //enqueue all the commands from run something.bat
                for (int y = 0; y < subCommands.Length - 1; y++)
                {
                    Console.WriteLine("");
                    queue.Enqueue(subCommands[y]);

                }

                Console.WriteLine("");
                //Console.Read();
                while (queue.Count > 0)
                {
                    string name = queue.Dequeue();
                    Kernel.globalName = name.Split(' ');
                    Kernel.Execute(Kernel.globalName);
                }
                return;
            }
            /*
            while(boolean)
            {
                Console.WriteLine("I'm in the while loop");
                
                if(commands[x][0] == 'r'){
                    list = commands[x].Split(' ');
                    if (Kernel.findIt(list[1]))
                    {
                        subCommands = data.Split('\n');


                        for (int y = 0; y < subCommands.Length; y++)
                        {
                            queue.Enqueue(subCommands[y]);
                        }
                    }
                }

             
                if (Kernel.findIt(commands[x]) && commands[x] == "run")
                {
                    Console.WriteLine("IF BICH");
                    data = Kernel.output(commands[x]);
                    subCommands = data.Split('\n');

                    for (int y = 0; y < subCommands.Length; y++)
                    {
                        Console.WriteLine("I'm stuck in the inner for loop");
                        queue.Enqueue(subCommands[y]);
                        
                    }
                    x++;
                    continue;
                }
                queue.Enqueue(commands[x]);

                x++;
                if (x >= commands.Length - 1)
                {
                    boolean = false;
                }
                
            }

            while(queue.Count > 0)
            {
                Kernel.Execute(queue.Dequeue());
            } */
        }

    }
}
