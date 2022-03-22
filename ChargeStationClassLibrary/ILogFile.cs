using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface ILogFile
    {
        public void LogDoorLocked(int id) {}

        public void LogDoorUnlocked(int id) {}
    }
}
