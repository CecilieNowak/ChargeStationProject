using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface ILogFile
    {
        public void logDoorLocked(int id);
        public void logDoorUnlocked(int id);
    }
}
