using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IDisplay _display;
        private ILogFile _logFile;
        private IRfidReader _rfidReader; 

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _logFile = Substitute.For<ILogFile>();
            _rfidReader = Substitute.For<IRfidReader>();

            _uut = new StationControl(_door, _display, _chargeControl, _logFile, _rfidReader);
        }



        [Test]
        public void StationControl_NewlyCreatedObject_LadeSkabStateIsAvailable()
        {
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [TestCase(StationControl.LadeskabState.DoorOpen)]
        [TestCase(StationControl.LadeskabState.Available)]
        [TestCase(StationControl.LadeskabState.Locked)]
        public void SetLadeSkabStateMethod_LadeskabStateIsSet_MethodReturnsState(StationControl.LadeskabState s)
        {
            //act
            _uut.SetLadeskabState(s);

            //assert
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(s));


        }

        

        [Test]
        public void LadeskabStateIsAvailable_DoorIsOpenedEvent_LadeSkabStateIsDoorOpen()
        {
            //arrange - in set-up

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event
            
            //act
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.DoorOpen)); 

        }

        [Test]
        public void LadeskabStateIsDoorOpened_DoorIsClosedEvent_LadeskabStateIsAvailable()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.DoorOpen);

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs() { DoorIsOpen = false }); //Dør LUKKES her

            //assert
            Assert.That(_uut.GetLadeskabState(),Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void LadeskabStateIsLooked_DoorIsOpenedEvent_LadeskabStateIsLocked()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Locked);

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs() { DoorIsOpen = true }); 

            //assert
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void LadeskabStateDoorIsOpen_DoorIsOpenedEvent_LadeskabStateIsNotChanged()
        {
            //arrange 
            _uut.SetLadeskabState(StationControl.LadeskabState.DoorOpen);

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event
            
            //assert
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.DoorOpen));

        }

        [Test]
        public void LadeskabStateIsAvailable_DoorIsClosedEvent_LadeskabStateIsNotChanged()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Available);

          //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = false }); //Her sendes et fake event
            
            //assert
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.Available));

        }

        [Test]
        public void RfidDetected_ChargeIsConnected_logFileReceivedMethodCall()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Available);
            _chargeControl.Connected = true;

            //act
            _uut.RfidDetected(12345678);

            //assert
            _logFile.Received(1).LogDoorLocked(12345678);
        }

        [Test]
        public void RfidDetected_ChargeIsNotConnected_logFileDidNotReceiveMethodCall()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Available);
            _chargeControl.Connected = false;

            //act
            _uut.RfidDetected(12345678);

            //assert
            _logFile.Received(0).LogDoorLocked(12345678);
        }

        [Test]
        public void RfidDetected_ValidRfidEntryRequest_logFileReceivedMethodCall()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Locked);
            _rfidReader.ValidateRfidEntryRequest(12345678);

            //act
            _uut.RfidDetected(12345678);

            //assert
            _logFile.Received(1).LogDoorUnlocked(12345678);
        }

        [Test]
        public void RfidDetected_NotValidRfidEntryRequest_logFileDidNotReceiveMethodCall()
        {
            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Locked);
            _rfidReader.ValidateRfidEntryRequest(12345678);

            //act
            _uut.RfidDetected(20);

            //assert
            _logFile.Received(0).LogDoorUnlocked(12345678);
        }

        [Test]
        public void LadeskabStateIsAvailable_LogFileIsCalledTwice_DoorStateIsWrittenToFileTwice()
        {
            //arrange
            File.Delete(@"logFile.txt");
            _uut.SetLadeskabState(StationControl.LadeskabState.Available);
            _chargeControl.Connected = true;

            //act
            _uut.RfidDetected(12345678);

            //arrange
            _uut.SetLadeskabState(StationControl.LadeskabState.Available);

            //act
            _uut.RfidDetected(12345678);

            //assert
            _logFile.Received(2).LogDoorLocked(12345678);
        }
        [Test]
        public void RequestEntry_validRFID_DoorOpens()
        {
            _uut.SetLadeskabState(StationControl.LadeskabState.Locked);
            _rfidReader.ValidateRfidEntryRequest(12345678);

            _uut.RfidDetected(12345678);

            _door.Received(1).UnlockDoor();
        }
    }
}
