using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    class DisplayStatus : IDisplay
    {
        public void showMessage(string message)
        {
            System.Console.WriteLine(message);

        }
    }
}
