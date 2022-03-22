using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject.Test
{
    public class FakeRfidValidation : IRfidReader

    {
    public bool EntryValidated { get; set; } = true;

    public bool ValidateRfidEntryRequest(int RFID)
    {
        return RFID == 11111111;
    }
    }
}
    
