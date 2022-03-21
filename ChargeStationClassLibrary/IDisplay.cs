using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IDisplay
    {
        public string SaveMessage { get; set; } 
        public void showMessage(string message);

    }
}
