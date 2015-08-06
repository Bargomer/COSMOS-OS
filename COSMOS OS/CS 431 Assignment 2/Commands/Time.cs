using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_431_Assignment_2.Commands
{
    class Time : Command
    {
         public Time()
        {
            this.Name = "time";
        }

        public override void Execute(string[] args)
        {
            Console.WriteLine("Time: " + (Cosmos.Hardware.RTC.Hour - 12) + ":" + Cosmos.Hardware.RTC.Minute +
                  ":" + Cosmos.Hardware.RTC.Second + (Cosmos.Hardware.RTC.Hour > 12 ? " pm" : " am"));

        }

    }
}
