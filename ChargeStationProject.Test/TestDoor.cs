using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    [TestFixture]  

    class TestDoor
    {
        private Door _uut;
        private DoorStateEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new Door();
            _uut.OpenDoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

        }

        [Test]
        public void DoorOpen_DoorOpenedIsCalled_EventFired()
        {

            _uut.DoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void DoorOpen_DoorOpenedIsCalled_CorrectBoolReceived()
        {

            _uut.DoorOpen();
            Assert.That(_receivedEventArgs.DoorIsOpen, Is.EqualTo(true));
        }


        [Test]
        public void DoorClose_DoorCloseIsCalled_EventFired()
        {

            _uut.DoorClose();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void DoorClose_DoorCloseIsCalled_CorrectBoolReceived()
        {

            _uut.DoorClose();
            Assert.That(_receivedEventArgs.DoorIsOpen, Is.EqualTo(false));
        }

        [Test]
        public void DoorLocked_DoorLcokIsCalled_ConsoleOutput()
        {

            _uut.LockDoor();
           // Assert.That();
        }
    }
}
