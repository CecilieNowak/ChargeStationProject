using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace ChargeStationProject
{
    public class ChargeControl : IChargeControl
    {
        public IUsbCharger _UsbCharger;
        public bool Connected { get; set; }

        public ChargeControl(IUsbCharger usbCharger)
        {
            _UsbCharger = usbCharger;
            _UsbCharger.CurrentValueEvent += OnCurrentValueEvent;
        }


        public void stopCharge()
        {
           _UsbCharger.StopCharge();
        }

        public void startCharge()
        {
            if (Connected == true)
            {
                _UsbCharger.StartCharge();
            }
            else
            {
                Connected = false;
            }
        }
        private void OnCurrentValueEvent(object? sender, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                Connected = false;
            } 

            else if (0 < e.Current && e.Current <= 5)
            {
                _UsbCharger.StopCharge(); //TODO ??? KAN stoppes
                //TODO Display viser at telefonen er fuld opladt 
            }

            else if (5 < e.Current && e.Current <= 500)
            {
                //TODO Display viser at ladning foregår
            }

            else if (e.Current > 500)
            {
                _UsbCharger.StopCharge();
                //TODO Display viser at fejlmeddelse 
            }

            else
            {
                    
            }

        }


        // her skal koden til charging messages ligge
        // PhoneConnected, RFID, Error, Occupied og Remove
    }

   
}
