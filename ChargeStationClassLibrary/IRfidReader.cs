using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    //Interface
    public interface IRfidReader
    {
        void ValidateRfidEntryRequest(int rfid);

        event EventHandler<RfidEventArgs> RFIDChangedEvent;
    }

    public class RfidEventArgs : EventArgs
    {
        public int RFID { get; set; }

    }
}

