using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
 
    //Interface
    public interface IRfidReader
    {
        bool ValidateRfidEntryRequest(int rfid);
    }
}

