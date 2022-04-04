using System;
using System.Collections.Generic;
using System.Text;

namespace ChargeStationProject
{
    public interface IDisplay
    {
        public void showMessage(string message);
        public string SaveMessage { get; set; }

    }
}
