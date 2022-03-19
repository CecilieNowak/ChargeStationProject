using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IChargeControl
    {
        public bool Connected { get; set; }
      public void stopCharge();
        public void startCharge();


    }
   
}
