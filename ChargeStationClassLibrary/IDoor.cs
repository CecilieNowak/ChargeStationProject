using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IDoor
    {
        public event EventHandler<DoorIsOpen> DoorIsOpenEvent;
        public void DoorOpen();
        public void DoorClose();
        
    }

    public class DoorIsOpen : EventArgs
    {
        public bool DoorOpenArgs { get; set; }
    }
}
