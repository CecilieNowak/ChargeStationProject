using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    //Klasse
    public class RfidDetectedEventArgs : EventArgs
    {
        public int Rfid { get; set; }
    }

    //Interface
    public interface IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
    }
}
