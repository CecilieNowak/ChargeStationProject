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

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _UsbCharger = usbCharger;
            IsCompleted = false;
            Connected = false; 
            _UsbCharger.CurrentValueEvent += OnCurrentValueEvent;
            _display = display; //CBE tilføjet
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
        private void OnCurrentValueEvent(object sender, CurrentEventArgs e)
        {
                if (e.Current == 0)
                {
                    Connected = false;
                }

                else if (0 < e.Current && e.Current <= 5)
                {
                    IsCompleted = true;
                    _display.showMessage("telefonen er fuldt opladt"); //tilføjet af CBE
                    
                }

                else if (5 < e.Current && e.Current <= 500)
                {
                    IsCompleted = false;
                    _display.showMessage("telefonen oplader"); //tilføjet af CBE
                   
            }

                else if (e.Current > 500)
                {
                    IsCompleted = true; //Ikke completed man skal stoppes
                    _UsbCharger.StopCharge();
                    _display.showMessage("fejl i opladning"); //tilføjet af CBE
                   
            }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
           

            
        }


   
        
    }

   
}
