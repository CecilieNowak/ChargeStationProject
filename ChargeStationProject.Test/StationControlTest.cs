using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    class StationControlTest
    {
        private StationControl _uut;
        private IDoor _door;
        private IChargeControl _chargeControl;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();

            _uut = new StationControl(_door, _display, _chargeControl);

        }

        [Test]
        public void DoorOpened_DoorIsOpenedEvent_LadeSkabStateIsDoorOpen()
        {
            
            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.DoorOpen)); 

        }

        [Test]
        public void DoorClosed_DoorIsOpenedEvent_LadeskabStateIsDoorOpened()
        {

            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = false }); //Her sendes et fake event
            Assert.That(_uut.GetLadeskabState(), Is.EqualTo(StationControl.LadeskabState.Available));

        }

        [Test]
        public void DoorLocked_DoorIsOpenedEvent_LadeskabStateIsDoorLocked()
        {
            //TODO
        }

        [Test]
        public void DoorUnlocked_DoorIsOpenedEvent_LadeskabStateIsDoorOpen()
        {
            //TODO
        }


    }
}
