using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public class Door : IDoor
    {
        public event EventHandler<DoorStateEventArgs> OpenDoorEvent;
        public bool IsLocked { get; set; }

        public Door()
        {
            IsLocked = false;

        }


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
           IsLocked = true;

        }

        public void UnlockDoor()
        {
           Console.WriteLine("(Handling) Døren er låst op");
           IsLocked = false;
        }
    }
}
