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

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();

            _uut = new StationControl(_door, _chargeControl);

        }

        [Test]
        public void DoorOpened_DoorIsOpenedEvent_DoorIsOpenIsTrue()
        {

            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event
            Assert.That(_uut.DoorIsOpen, Is.True); //TODO hvordan skal man ellers gøre?

        }

        public void DoorOpened_DoorIsOpenedEvent_DoorIsOpenIsNotTrue()
        {

            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = false }); //Her sendes et fake event
            Assert.That(_uut.DoorIsOpen, Is.Not.True); //TODO hvordan skal man ellers gøre?

        }

        [Test]
        public void DoorClosed_DoorIsOpenedEvent_DoorIsOpenIsFalse()
        {

            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = false }); //Her sendes et fake event

            Assert.That(_uut.DoorIsOpen, Is.False);

        }


        [Test]
        public void DoorClosed_DoorIsOpenedEvent_DoorIsOpenIsNotFalse()
        {

            _door.OpenDoorEvent += Raise.EventWith(new DoorStateEventArgs { DoorIsOpen = true }); //Her sendes et fake event

            Assert.That(_uut.DoorIsOpen, Is.Not.False);

        }
    }
}
