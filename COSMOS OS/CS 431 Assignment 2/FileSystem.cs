using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Should try an array of nodes

namespace CS_431_Assignment_2
{
    class FileSystem
    {
        //public static List<string> files = new List<string>();


        public class Node
        {
            public string name, data, dateCreated;
            int size;
            public Node next;

            public Node()
            {
                name = "null";
                data = "null";
                next = null;
            }

            public Node(string name, string data)
            {
                this.name = name;
                this.data = data;
                this.dateCreated = Cosmos.Hardware.RTC.Month + "/" + Cosmos.Hardware.RTC.DayOfTheMonth + "/" + Cosmos.Hardware.RTC.Century + Cosmos.Hardware.RTC.Year;
                next = null;
                size = data.Length * sizeof(Char);
            }

            public int getSize()
            {
                return size;
            }
            public string getDateCreated()
            {
                return dateCreated;
            }
            public string getName()
            {
                return name;
            }
            public void setName(string name)
            {
                this.name = name;
            }
            public string getData()
            {
                return data;
            }
            
            public Node getNextNode()
            {
                return next;
            }

            public void setData(string data)
            {
                this.data = data;
            }

            public void setNextNode(Node nextNode)
            {
                next = nextNode;
            }

        }
        
        public class fileArray
        {
            public Node[] theFiles;
            public static int count;

            public fileArray()
            {
                theFiles = new Node[100];
                count = 0;
            }

            public void add(string name, string data)
            {
                theFiles[count] = new Node(name, data);
                count++;

                // just testing if the system works
                for (int i = 0; i < count; i++)
                {

                   // Console.WriteLine(theFiles[i].getName());
                }
            }


            public int getCount()
            {
                return count;
            }


            public string find(string name)
            {
                for (int i = 0; i < theFiles.Length; i++)
                {
                    if (theFiles[i].getName() == name)
                    {
                        string temp = theFiles[i].getData();
                        return temp;
                    }
                }
                return ("File NOT FOUND");
            }

            public bool findIt(string name)
            {
                for (int i = 0; i < theFiles.Length; i++)
                {
                    if (theFiles[i].getName() == name)
                    {
                        
                        return true;
                    }
                }

                return false;
            }

            public string[] getList()
            {
                string[] temp = new string[count];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = theFiles[i].getName();
                }
                return temp;
            }

            public string[] getDates()
            {
                string[] temp = new string[count];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = theFiles[i].getDateCreated();
                }
                return temp;
            }

            public int[] getSizes()
            {
                int[] temp = new int[count];
                for (int i = 0; i < count; i++)
                {
                    temp[i] = theFiles[i].getSize();
                }
                return temp;
            }

            public void setData(string name,string data)
            {
                for (int x = 0; x < count; x++)
                {
                    if (theFiles[x].getName() == name)
                    {
                        theFiles[x].setData(data);
                    }
                }
            }
        }
    }

}
