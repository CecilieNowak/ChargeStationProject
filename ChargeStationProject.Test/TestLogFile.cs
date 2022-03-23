using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    class TestLogFile
    {
        private FakeLogFile _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new FakeLogFile();
        }

        [Test]
        public void LogDoorLocked_NotCalledYet_WrittenToFileCountIsZero()
        {
            File.Delete(@"testFile.txt");
            Assert.That(_uut.WrittenToFileCount, Is.EqualTo(0));
        }

        [Test]
        public void LogDoorLocked_NotCalledYet_FileIsNotCreated()
        {
            File.Delete(@"testFile.txt");
            Assert.That(@"testFile.txt", Does.Not.Exist);
        }

        [Test]
        public void LogDoorLocked_IsCalledOnce_WrittenToFileIsCountIsOne()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorLocked(20);

            Assert.That(_uut.WrittenToFileCount, Is.EqualTo(1));
        }

        [Test]
        public void LogDoorLocked_IsCalledOnce_FileIsCreated()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorLocked(20);

            Assert.That(@"testFile.txt", Does.Exist);
        }

        [Test]
        public void LogDoorLocked_IsCalledOnce_TheCorrectTextIsWrittenInTheFile()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorLocked(20);
            _uut.ReadFromFile();                

            Assert.That(_uut.TextInFile, Is.EqualTo("Skab er låst med RFID 20"));
        }

        [Test]
        public void LogDoorUnlocked_IsCalledOnce_TheCorrectTextIsWrittenInTheFile()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorUnlocked(20);
            _uut.ReadFromFile();               

            Assert.That(_uut.TextInFile, Is.EqualTo("Skab er låst op med RFID 20"));
        }


        [Test]
        public void LogDoorUnlocked_IsCalledOnce_FileIsCreated()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorUnlocked(20);

            Assert.That(@"testFile.txt", Does.Exist);
        }

        [Test]
        public void LogDoorUnlocked_IsCalledTwice_WrittenToFileIsCountIsTwice()
        {
            File.Delete(@"testFile.txt");
            _uut.LogDoorUnlocked(20);
            _uut.LogDoorUnlocked(20);

            Assert.That(_uut.WrittenToFileCount, Is.EqualTo(2));
        }

    }
}
