using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CS_431_Assignment_2
{
    public class Kernel : Sys.Kernel
    {
        public static string[] globalName, words;
        //private static FileSystem.LinkedList fileSystem = new FileSystem.LinkedList();
        private static FileSystem.fileArray fileSystem = new FileSystem.fileArray();
        private static List<Command> commands = new List<Command>();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            commands.Add(new Commands.Test());
            commands.Add(new Commands.Date());
            commands.Add(new Commands.Time());
            commands.Add(new Commands.Create());
            commands.Add(new Commands.Set());
            commands.Add(new Commands.Out());
            commands.Add(new Commands.Dir());
            commands.Add(new Commands.Run());
            commands.Add(new Commands.Clear());
        }

        protected override void Run()
        {
            while (true)
            {
                Console.Write("c:\\>");
                
                string cmd = Console.ReadLine();
                cmd = cmd.ToLower();
                words = cmd.Split(' ');
                if (words[0] == "\n" || words[0] == " ")
                    continue;
                if (words.Length > 1)
                    globalName = words;

                Execute(words);
            }

        }

        public static void Execute(string[] cmdArray)
        {
            string cmd = cmdArray[0];
            Scanner scanner = new Scanner();
            List<object> args = scanner.getTokens(cmd);
            List<string> argsString = new List<string>();

            for (int x = 0; x < args.Count; x++)
            {
                if (args[x] is Scanner.StringLiteral)
                    argsString.Add(((Scanner.StringLiteral)args[x]).Value);
                else
                    argsString.Add(args[x].ToString());
            }

            for (int x = 0; x < commands.Count; x++)
            {
                if (commands[x].Name == argsString[0])
                {
                    commands[x].Execute(argsString.ToArray());
                    break;
                }
            }
        }

        public static void Execute(string cmdArray)
        {
            string cmd = cmdArray;
            Scanner scanner = new Scanner();
            List<object> args = scanner.getTokens(cmd);
            List<string> argsString = new List<string>();

            for (int x = 0; x < args.Count; x++)
            {
                if (args[x] is Scanner.StringLiteral)
                    argsString.Add(((Scanner.StringLiteral)args[x]).Value);
                else
                    argsString.Add(args[x].ToString());
            }

            for (int x = 0; x < commands.Count; x++)
            {
                if (commands[x].Name == argsString[0])
                {
                    commands[x].Execute(argsString.ToArray());
                    break;
                }
            }
        }

        public static string[] getGlobal()
        {
            return globalName;
        }

        public static string output(string filename)
        {
            return fileSystem.find(filename);
        }

        public static void setNode(string name, string data)
        {
            fileSystem.add(name, data);
        }
        
        public static int getCount()
        {
            return fileSystem.getCount();
        }
        
        public static string[] getName()
        {
            return fileSystem.getList();
        }

        public static string[] getDate()
        {
            return fileSystem.getDates();
        }

        public static int[] getSizes()
        {
            return fileSystem.getSizes();
        }

        public static bool findIt(string name)
        {
            return fileSystem.findIt(name);
        }

        public static void setData(string name, string data)
        {
            fileSystem.setData(name, data);
        }

    }
}
