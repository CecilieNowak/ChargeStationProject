using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
   public class DisplayInstructions : IDisplay
    {

        public void showMessage(string message)
        {
            SaveMessage = message;
           System.Console.WriteLine(message);
        }

        public string SaveMessage { get; set; }
    }
}
