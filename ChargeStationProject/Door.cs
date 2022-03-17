using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class Door : IDoor
    {
        public event EventHandler<DoorIsOpen> DoorIsOpenEvent;
      
        
        protected virtual void OnDoorOpen(DoorIsOpen e)
        {
            DoorIsOpenEvent?.Invoke(this, e);
        }



        public void DoorOpen()
        {
            throw new NotImplementedException();
        }

        public void DoorClose()
        {
            throw new NotImplementedException();
        }
    }
}
