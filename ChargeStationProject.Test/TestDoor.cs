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
        public void Door_NewlyCreatedObject_IsLockedIsfalse()
        {
            //arrange in setUp
            //act not needed, because the test is based on zero inputs and zero actions
            //Assert
            Assert.That(_uut.IsLocked, Is.False);
        }

        [Test]
        public void DoorOpen_DoorOpenedIsCalled_EventFired()
        {

            _uut.DoorOpen();
            //-= sætter eventet til null
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void EventUnsubscribed_NoMethodIsCalled_EventIsNull()
        {

            //arrange

           

            _uut.OpenDoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };

            _receivedEventArgs = null;

            Assert.That(_receivedEventArgs, Is.Null);
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
        public void DoorLocked_LockDoorIsCalled_IsLockedIsTrue()
        {

            _uut.LockDoor();

            Assert.That(_uut.IsLocked, Is.True);
        }

        [Test]
        public void DoorUnlocked_UnlockDoorIsCalled_IsLockedIsFalse()
        {

            _uut.UnlockDoor();

            Assert.That(_uut.IsLocked, Is.False);
        }
    }
}
