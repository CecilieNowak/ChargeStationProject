using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargeStationClassLibrary;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    [TestFixture, NonParallelizable]
    class TestLogFile
    {
        private LogFile _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new LogFile();
        }

        [Test]
        public void LogDoorLocked_NotCalledYet_FileIsNotCreated()
        {
            //arrange
            File.Delete(@"logFile.txt");

            //assert
            Assert.That(@"logFile.txt", Does.Not.Exist);
        }

        [Test]
        public void LogDoorLocked_IsCalledOnce_FileIsCreated()
        {
            //arrange
            File.Delete(@"logFile.txt");

            //act
            _uut.LogDoorLocked(20);

            //assert
            Assert.That(@"logFile.txt", Does.Exist);
        }

        [Test]
        public void LogDoorLocked_IsCalledOnce_TheCorrectTextIsWrittenInTheFile()
        {
            //arrange
            File.Delete(@"logFile.txt");
            _uut.LogDoorLocked(20);
            StreamReader reader = new StreamReader(@"logFile.txt");

            //act
            string textInFile = reader.ReadLine();

            //assert
            Assert.That(textInFile, Is.EqualTo(DateTime.Today + ": Skab låst med RFID: 20")); 
        }


        [Test]
        public void LogDoorUnlocked_IsCalledOnce_FileIsCreated()
        {
            //arrange
            File.Delete(@"logFile.txt");

            //act
            _uut.LogDoorUnlocked(20);

            //assert
            Assert.That(@"logFile.txt", Does.Exist);
        }

        [Test]
        public void LogDoorUnlocked_IsCalledOnce_TheCorrectTextIsWrittenInTheFile()
        {
            //arrange
            File.Delete(@"logFile.txt");
            _uut.LogDoorUnlocked(20);
            StreamReader reader = new StreamReader(@"logFile.txt");

            //act
            string textInFile = reader.ReadLine();

            //assert
            Assert.That(textInFile, Is.EqualTo(DateTime.Today + ": Skab låst op med RFID: 20")); //hvordan tester jeg det, når der er tid 
        }

    }
}
