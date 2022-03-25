using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class RfidReader : IRfidReader
    {
        private int correctRfid = 12345678;
        private bool validRfid = false;
        public bool ValidateRfidEntryRequest(int rfid)
        {
            validRfid = false; 

            Console.WriteLine("RFIDValidation::ValidateRFIDEntryRequest called");
            if (rfid == correctRfid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
