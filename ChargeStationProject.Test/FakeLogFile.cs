using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChargeStationProject.Test
{
    public class FakeLogFile : ILogFile
    {
        //private StreamWriter _writer;
        private string logFile = "testFile.txt";

        public int WrittenToFileCount { get; set; } //property injection
        public string TextInFile { get; set; }      //property injection


        public FakeLogFile()
        {
            WrittenToFileCount = 0;
            TextInFile = "";
        }

        public void LogDoorLocked(int id)
        {
            StreamWriter writer;
            
            using (writer = File.AppendText(logFile))
            {
                writer.WriteLine("Skab er låst med RFID {0}", id);
                WrittenToFileCount++;
            }
            writer.Close();
        }

        public void LogDoorUnlocked(int id)
        {
            StreamWriter writer;

            using (writer = File.AppendText(logFile))
            {
                writer.WriteLine("Skab er låst op med RFID {0}", id);
                WrittenToFileCount++;
            }
            writer.Close();
        }

        public void ReadFromFile()
        {
            StreamReader reader = new StreamReader(logFile);

            TextInFile = reader.ReadLine();
        }
    }
}
