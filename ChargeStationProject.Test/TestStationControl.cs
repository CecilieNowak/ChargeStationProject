using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
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

        //---------NEW OBJECT TEST---------//

        [Test]
        public void StationControl_NewlyCreatedObject_LadeSkabStateIsAvailable()
        {
            //arrange


            //TODO what to do - teste om state er available ved instantiering, event er subscribed etc.
           // Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.Available));
        }

        //---------DOOREVENT TEST---------//
     
        [Test]
        public void LadeskabStateIsAvailable_DoorIsOpenedEvent_ConnectedEqualsTrue()
        {
            //arrange - in set-up

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event
            
            //act
            
            Assert.That(_chargeControl.Connected, Is.True); 
            

        }


        [Test]
        public void LadeskabStateIsAvailable_DoorIsClosedEvent_ConnectedEqualsFalse()
        {
            //arrange - in set-up

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = false }); //Her sendes et fake event

            //act

            Assert.That(_chargeControl.Connected, Is.False);


        }

        [Test]
        public void LadeskabStateIsDoorOpened_DoorIsClosedEvent_showMessageIsCalled()
        {
            //arrange
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs() { DoorIsOpen = true }); //Dør ÅBNES her
            

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs() { DoorIsOpen = false }); //Dør LUKKES her

            //assert
            _display.Received(1).showMessage("Indlæs RFID");
        }

        [Test]
        public void LadeskabStateDoorIsOpen_DoorIsOpenedEvent_showMessageIsNotCalled()
        {
            //arrange 
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her ÅBNES døren

            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her forsøges døren åben igen

            //assert
           _display.Received(0).showMessage("Indlæs RFID");

        }

        [Test]
        public void LadeskabStateIsLocked_DoorIsOpenedEvent_showMessageIsCalled()
        {
            //arrange
            _chargeControl.Connected = true; //TODO er dette black box-agtigt? stub
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs() { RFID = 1234 });
            
            //act
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs() { DoorIsOpen = true });

            //assert
            _display.Received(1).showMessage("Ladeskabet er optaget!");
        }


        //---------RFID EVENT TEST---------//

        [Test]
        public void LadeskabStateIsAvailable_RFIDChangedEvent_logFileReceivedMethodCall()
        {
            //arrange - state sættes til available i set up

            _chargeControl.Connected = true;

            //act
           _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });

            //assert
            _door.Received(1).LockDoor();
            _chargeControl.Received(1).startCharge();
            _logFile.Received(1).LogDoorLocked(1234);
            
        }

        [Test]
        public void LadeskabStateIsAvailable_ChargeIsNotConnected_logFileDidNotReceiveMethodCall()
        {
            //arrange - state sættes til available i set up
            _chargeControl.Connected = false;

            //act
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });

            //assert
            
            _door.Received(0).LockDoor();
            _chargeControl.Received(0).startCharge();
            _logFile.Received(0).LogDoorLocked(1234);

            _display.Received(1).showMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        [Test]
        public void LadeskabStateIsDoorOpen_RFIDEventChanged_logFileDidNotReceiveMethodCall()
        {
            //arrange - state sættes til available i set up
         _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); 

            //act
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });

            //assert

            _door.Received(0).LockDoor();
            _chargeControl.Received(0).startCharge();
            _logFile.Received(0).LogDoorLocked(1234);

            _display.Received(0).showMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        [Test]
        public void LadeskabStateIsLocked_RFIDChangedEvent_IDIsCorrected_logFileReceivedMethodCall()
        {
            //arrange
            _chargeControl.Connected = true;
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });

            //act
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });


            //assert
            _logFile.Received(1).LogDoorUnlocked(1234);
            _chargeControl.Received(1).stopCharge();
            _door.Received(1).UnlockDoor();
            _display.Received(1).showMessage("Tag din telefon ud af skabet og luk døren");
        }

        [Test]
        public void LadeskabStateIsLocked_RFIDChangedEvent_IDIsIncorrected_logFileReceivedMethodCall()
        {
            //arrange
            _chargeControl.Connected = true;
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });

            //act
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 5632 });  //Incorrect RFID


            //assert
            _logFile.Received(0).LogDoorUnlocked(1234);
            _chargeControl.Received(0).stopCharge();
            _door.Received(0).UnlockDoor();
            _display.Received(0).showMessage("Tag din telefon ud af skabet og luk døren");

            _display.Received(1).showMessage("Forkert RFID tag");
        }

        [Test]
        public void LadeskabStateIsAvailable_LogFileIsCalledTwice_DoorStateIsWrittenToFileTwice()
        {
            //arrange
            File.Delete(@"logFile.txt");
            //State er sat til available in set up

            //act
            _chargeControl.Connected = true;
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });  //Her låses skabet med ID 5632
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });  //Her låses skabet op 

            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });  //Her låses skabet med ID 1234
            _rfidReader.RFIDChangedEvent += Raise.EventWith(new RfidEventArgs { RFID = 1234 });  //Her låses skabet op 

            //assert
            _logFile.Received(2).LogDoorLocked(1234);
        }

   



    }
}
