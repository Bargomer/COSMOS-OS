using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Dir : Command
    {
        public Dir()
        {
            this.Name = "dir";
        }

        public override void Execute(string[] args)
        {
            int counter = Kernel.getCount();
            string[] filename = new string[counter];
            int[] filesize = Kernel.getSizes();
            string[] ext = new string[counter];
            string[] date = Kernel.getDate();


            string[] names = Kernel.getName();

            Console.WriteLine("Filename  \t   Extension  \t  Date    \t   Size");
            Console.WriteLine("--------------------------------------------");
            
            for (int i = 0; i < Kernel.getCount(); i++)
            {
                //if the name doesn't contain a '.' than it's not a file
                if (!contains(names[i]))
                {
                    continue;
                }

                int dot = names[i].LastIndexOf('.', names[i].Length - 1, names[i].Length - 2);
                filename[i] = names[i].Substring(0, dot);
                ext[i] = names[i].Substring(dot, dot + 10);
                Console.WriteLine(filename[i] + "\t\t" + ext[i] + "\t\t" + date[i] + "\t\t" + filesize[i] + "bytes");
            }
        }

        public static bool contains(string name)
        {
            int x = 0;
            while (x < name.Length)
            {
                if (name[x] == '.')
                    return true;
                x++;
            }
            return false;
        }
    }
}
