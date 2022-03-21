using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IRfidReader
    {

        bool ValidateRFIDEntryRequest(int RFID);
    }
}

