using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject.Test
{
    class FakeDisplayInstructions : IDisplay
    {
        public string SaveMessage { get; set; }
        public void showMessage(string message)
        {
            SaveMessage = message;  // for at kunne test klassen er der oprettet dette, da jeg ikke kan teste om den udskrive det
            System.Console.WriteLine(message);
        }
    }
}
