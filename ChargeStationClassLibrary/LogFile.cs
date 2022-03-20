using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChargeStationProject
{
    public class LogFile : ILogFile
    {
        private StreamWriter writer;
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        //event ?

        //Logging når skab er låst
        public void LogDoorLocked(int id)
        {
            using (writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        //Logging når skab er låst op
        public void LogDoorUnlocked(int id)
        {
            using (writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}
