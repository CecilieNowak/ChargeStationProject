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



        public void DoorOpen() // der er en med same navn i kontrol klassen
        {
            
            OnDoorOpen(new DoorIsOpen()
            {
                //hvad skal der ske
                // DoorOpenArgs sættes til true
            });
            
            throw new NotImplementedException();
        }

        public void DoorClose()
        {
            throw new NotImplementedException();
        }
    }
}
