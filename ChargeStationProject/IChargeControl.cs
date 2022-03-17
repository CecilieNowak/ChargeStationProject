using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IChargeControl
    {
        public bool IsConnected();
        public void StopCharge();
        public void StartChange();


    }
}
