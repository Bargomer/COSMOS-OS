using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Set : Command
    {
        public Set() 
        {
            this.Name = "set";
        }

        public override void Execute(string[] args)
        {
            string output = "", varName, temp = Kernel.globalName[3];
            int a = 0;

            
            //check the first string to see if it's a variable saved in the system.
            if (Kernel.findIt(Kernel.globalName[3]))
            {
                temp = Kernel.output(Kernel.globalName[3]);
            }

            //if it's == 34, then it's "" save it as a string
            if (temp[0] == 34)
            {
                while (a < Kernel.globalName.Length - 3)
                {
                    output += temp;
                    output += " ";
                    a++;

                    //check string if there's a variable stored in memory 
                    if (Kernel.findIt(Kernel.globalName[a + 3]))
                    {
                        temp = Kernel.output(Kernel.globalName[a + 3]);
                        continue;
                    }
                    temp = Kernel.globalName[a + 3];
                }
                concat(Kernel.globalName[1], output);
                return;
            }

            //*******************Arithmetic********************************
            string[] expression = new string[Kernel.globalName.Length - 3];
            
            //get the expression of the input
            for (int x = 0; x < expression.Length; x++)
            {
                expression[x] = Kernel.globalName[x + 3];
            }

            //set varName to be used in case used later
            varName = Kernel.globalName[1];

            output = arithmetic(expression);

            if (Kernel.findIt(varName))
            {
                Kernel.setData(Kernel.globalName[1], output);
            }
            Kernel.setNode(varName, output);
        }

        //string variable
        public static void concat(string name, string data)
        {
            Kernel.setNode(name,data);
        }
        public static string arithmetic(string [] expression){

            string[] tempExpression = expression;
            string var1, var2; ;
            int value, w = 0, length = tempExpression.Length, x = 0;
            bool boolean = true;

            while (boolean)
            {
                var1 = expression[x];
                var2 = expression[x + 2];

                if (Kernel.findIt(var1)){ 
                    var1 = Kernel.output(var1);
                }

                if (Kernel.findIt(var2)) { 
                    var2 = Kernel.output(var2); }

                //first check for division and multiplication symbols
                while (x < length) {
                    //perform multiplication
                    if (expression[x + 1] == "*")
                    {
                        value = Int32.Parse(var1) * Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;

                    }
                
                   //perform division
                    else if (expression[x + 1] == "/")
                    {
                        value = Int32.Parse(var1) / Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;
                    }
                    x++;
                }//end while loop for division and multiplication

                x = 0;
                length = expression.Length;
                
                //Check for addition and subtraction symbols
                while(x < length){

                    //perform addtion
                    if (expression[x + 1] == "+")
                    {
                        value = Int32.Parse(var1) + Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;

                    }
                
                    //perform subtraction
                    else if (expression[x + 1] == "-")
                    {
                        value = Int32.Parse(var1) - Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;
                    }
                    x++;
                }//end while for addition and subtraction

                x = 0;
                length = expression.Length;

                //while loop for the rest of the operations
                while (x < length)
                {
                    //perform &
                    if (expression[x + 1] == "&")
                    {
                        value = Int32.Parse(var1) & Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;
                    }

                    //perform ^
                    else if (expression[x + 1] == "^")
                    {
                        value = Int32.Parse(var1) ^ Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;
                    }

                    //perform |
                    else if (expression[x + 1] == "|")
                    {
                        value = Int32.Parse(var1) | Int32.Parse(var2);
                        expression = newExpression(expression, value.ToString(), x);
                        x = 0;
                        length = expression.Length;
                        var1 = expression[x];
                        var2 = expression[x + 2];

                        if (Kernel.findIt(var1))
                        {
                            var1 = Kernel.output(var1);
                        }

                        if (Kernel.findIt(var2))
                        {
                            var2 = Kernel.output(var2);
                        }
                        continue;
                    }
                    x++;
                }
                

                boolean = false;
                
            }//end of while loop

            return expression[0];
        }

        public static string []newExpression(string[] expression, string value, int index)
        {
            string[] tempExp = new string[expression.Length - 2], temp =  {value};
      
            int x = tempExp.Length, y = 0,cursor = 0;

            //if length == 3 the the result is found already
            if (expression.Length == 3)
                return temp;

            //initialize the new string
            while (y <= x + 1)
            {
                if (y == index){
                    tempExp[cursor] = value; 
                    tempExp[cursor + 1] = expression[cursor + 3]; 
                    tempExp[cursor + 2] = expression[cursor + 4]; 
                    cursor += 3;
                    y += 5;
                    continue;
                }
                tempExp[cursor] = expression[y];
                cursor++;
                y++;

            }
                return tempExp;
        }//end of newExpression

        //check if the string is a variable within memory
        public static int checkVar(string var)
        {
            return Int32.Parse(Kernel.output(var));
        }
    }
}
