using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    class RfidReader : IRfidReader
    {
        private const int _correctRfid = 12345678;
        private bool _validRfid = false;
        public bool ValidateRFIDEntryRequest(int RFID)
        {
            _validRfid = false; 

            Console.WriteLine("UserValidation::ValidateRFIDEntryRequest called");
            if (RFID == _correctRfid)
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
