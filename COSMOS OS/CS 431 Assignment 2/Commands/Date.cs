using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Date : Command
    {
        public static string date;
        public Date() 
        {
            this.Name = "date";
        }

        public override void Execute(string[] args)
        {
            string month = changeMonth(Cosmos.Hardware.RTC.Month);
            date = "Date: " + Cosmos.Hardware.RTC.DayOfTheMonth + " " + month +
                 " 20" + Cosmos.Hardware.RTC.Year;
            Console.WriteLine(date);
        }

        //This function only goes up to April, hopefully it's tested and graded before then
        //@param month: month passed to function via numerical string
        public string changeMonth(Object month)
        {

            switch (month.ToString())
            {
                case "1":
                    return "JAN";
                case "2":
                    return "FEB";
                case "3":
                    return "MAR";
                case "4":
                    return "APR";
                default:
                    return "Null";
            }//end switch
        }//end method changeMonth

        public static string getMonth()
        {
            return date;
        }
    }
}
