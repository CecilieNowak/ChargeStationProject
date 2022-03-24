using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class RfidReader : IRfidReader
    {
        private int _correctRfid = 12345678;
        private bool _validRfid = false;
        public bool ValidateRfidEntryRequest(int rfid)
        {
            _validRfid = false; 

            Console.WriteLine("RFIDValidation::ValidateRFIDEntryRequest called");
            if (rfid == _correctRfid)
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
