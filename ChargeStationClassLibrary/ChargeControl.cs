using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace ChargeStationProject
{
    public class ChargeControl : IChargeControl
    {
        private IDisplay _display; // CBE tilføjet

        public IUsbCharger _UsbCharger;
        public bool Connected { get; set; }
        public bool IsCompleted { get; set; }

        public ChargeControl(IUsbCharger usbCharger)
        {
            _UsbCharger = usbCharger;
            IsCompleted = false;
            Connected = false; //TODO skal sættes til true et sted?
            _UsbCharger.CurrentValueEvent += OnCurrentValueEvent;
        }


        public void stopCharge()
        {
            if (IsCompleted == true)
            {
                _UsbCharger.StopCharge();
            }
        }

        public void startCharge()
        {
            if (Connected == true)
            {
                _UsbCharger.StartCharge();
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
                    IsCompleted = true;
                    _display.showMessage("telefonen er fuldt opladt"); //tilføjet af CBE
                    //TODO Display viser at telefonen er fuld opladt 
                }

                else if (5 < e.Current && e.Current <= 500)
                {
                    IsCompleted = false;
                    _display.showMessage("telefonen oplader"); //tilføjet af CBE
                    //TODO Display viser at ladning foregår
            }

                else if (e.Current > 500)
                {
                    IsCompleted = true; //Ikke completed man skal stoppes
                    _UsbCharger.StopCharge();
                    _display.showMessage("fejl i opladning"); //tilføjet af CBE
                    //TODO Display viser at fejlmeddelse 
            }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
           

            
        }


        // her skal koden til charging messages ligge
        
    }

   
}
