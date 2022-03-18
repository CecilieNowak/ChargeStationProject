using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    class DisplayInstructions : IDisplay
    {
        public void showMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        // metoden skal skifte i mellem few forskellige udskrift 
        // PhoneConnected, RFID, Error, Occupied og Remove
        //jeg tænker at anvende if eller case
    }
}
