using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class Door : IDoor
    {
        public event EventHandler<DoorStateEventArgs> OpenDoorEvent;
      


        protected virtual void OnOpenDoor(DoorStateEventArgs e) 
        {
            OpenDoorEvent?.Invoke(this, e);
        }

      



        public void DoorOpen()
        {
            OnOpenDoor(new DoorStateEventArgs()
            {
                DoorIsOpen = true
            });

    
        }

        public void DoorClose()
        {
            OnOpenDoor(new DoorStateEventArgs()
            {
                DoorIsOpen = false
            });
        }

        public void LockDoor()
        {
           Console.WriteLine("(Handling) Døren er låst");
        }

        public void UnlockDoor()
        {
           Console.WriteLine("Døren er låst op");
        }
    }
}
