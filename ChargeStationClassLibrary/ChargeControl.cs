using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    class ChargeControl : IChargeControl
    {
        public bool isConnected()
        {
            throw new NotImplementedException();
        }

        public void stopCharge()
        {
            throw new NotImplementedException();
        }

        public void startChange()
        {
            throw new NotImplementedException();
        }

        // her skal koden til charging messages ligge
        // PhoneConnected, RFID, Error, Occupied og Remove
    }
}
