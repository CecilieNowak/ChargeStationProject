using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IDoor
    {
        event EventHandler<DoorStateEventArgs> OpenDoorEvent;
        public bool IsLocked { get; set; }

        public void DoorOpen();
        public void DoorClose();
        public void LockDoor();
        public void UnlockDoor();

    }

    public class DoorStateEventArgs : EventArgs
    {
        public bool DoorIsOpen { get; set; }    
        
    }


    
}
