using System;
using System.IO;
using ChargeStationProject;

namespace ChargeStationClassLibrary
{
    public class LogFile : ILogFile
    {
        private string _logFile = "logfile.txt"; 

        public LogFile()
        { }

        public void LogDoorLocked(int id)
        {
            StreamWriter writer;

            using (writer = File.AppendText(_logFile))
            {
                writer.WriteLine(DateTime.Today + ": Skab låst med RFID: {0}", id);
                writer.Close();
            }

        }

        public void LogDoorUnlocked(int id)
        {
            StreamWriter writer;

            using (writer = File.AppendText(_logFile))
            {
                writer.WriteLine(DateTime.Today + ": Skab låst op med RFID: {0}", id);
                writer.Close();
            }

        }
    }
}
