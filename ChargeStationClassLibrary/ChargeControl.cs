using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace ChargeStationProject
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger _UsbCharger;

        public ChargeControl()
        {
            _UsbCharger = new UsbChargerSimulator();
        }
        
        public bool isConnected()
        {
            if (_UsbCharger.Connected == true)
                return true;
            else
                return false;
        }

        public void stopCharge()
        {
           _UsbCharger.StopCharge();
        }

        public void startCharge()
        {
            _UsbCharger.StartCharge();
        }

        // her skal koden til charging messages ligge
        // PhoneConnected, RFID, Error, Occupied og Remove
    }

   
}
