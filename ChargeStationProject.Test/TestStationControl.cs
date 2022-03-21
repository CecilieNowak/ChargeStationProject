using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();

            _uut = new StationControl(_door, _display, _chargeControl);

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



    }
}
