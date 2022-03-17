using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace ChargeStationProject
{
    class ChargeControl : IChargeControl
    {
        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        public void StartChange()
        {
            throw new NotImplementedException();
        }

        //Event
        private void HandleCurrentEvent(object sender, CurrentEventArgs e)
        {

        }
    }
}
