using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    class RfidReader : IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
    }
}
