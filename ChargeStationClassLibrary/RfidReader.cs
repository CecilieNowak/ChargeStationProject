using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class RfidReader : IRfidReader
    {
        public void ValidateRfidEntryRequest(int rfid)
        {
            if (rfid >= 0)
            {
                RFIDChanged(new RfidEventArgs
                {
                    RFID = rfid
                });
            }
        }

        public event EventHandler<RfidEventArgs> RFIDChangedEvent;

        protected virtual void RFIDChanged(RfidEventArgs e)
        {
            RFIDChangedEvent?.Invoke(this, e);
        }
    }
}
