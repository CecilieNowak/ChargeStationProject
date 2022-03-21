using System;
using System.Collections.Generic;
using System.Text;
using ChargeStationProject;

namespace ChargeStationClassLibrary
{
   public interface IStationControl
   {
       public StationControl.LadeskabState GetLadeskabState();
       public void SetLadeskabState(StationControl.LadeskabState s);
   }
}
