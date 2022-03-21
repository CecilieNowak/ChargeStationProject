using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UsbSimulator;

namespace ChargeStationProject.Test
{
    [TestFixture]
    class TestChargeControl
    {
        private IUsbCharger _charger;
        private IDisplay _display;
        private IChargeControl _uut;

            [SetUp]
            public void Setup()
            {
                _charger = Substitute.For<IUsbCharger>();
                _uut = new ChargeControl(_charger, _display);
            }

        [Test]
        public void ChargeControl_NewlyCreatedObject_ConnectedIsFalse()
        {
            Assert.That(_uut.Connected, Is.False);
        }

        [Test]
        public void ChargeControl_NewlyCreatedObject_IsCompletedIsFalse()
        {
            Assert.That(_uut.IsCompleted, Is.False);
        }

        [Test]
            public void StopChargeMethod_IsCompleteIstrue_chargerReceivedMethodCall()
            {
            //arrange
            _uut.IsCompleted = true;

            //act
                _uut.stopCharge();

                //assert
                _charger.Received(1).StopCharge();

            }

            [Test]
            public void StopChargeMethod_IsCompleteIsFalse_chargerReceivedMethodCall()
            {
                //arrange
                _uut.IsCompleted = false;

                //act
                _uut.stopCharge();

                //assert
                _charger.Received(0).StopCharge();

            }

        [Test]
            public void StartChargeMethod_ConnectedIsTrue_chargerReceivedMethodCall()
            {
                //arrange
                _uut.Connected = true;

                //act
                _uut.startCharge();

                //assert
                _charger.Received(1).StartCharge();

            }

            [Test]
            public void StartChargeMethod_ConnectedIsFalse_chargerDidNotReceiveMethodCall()
            {
                //arrange
                _uut.Connected = false;

                //act
                _uut.startCharge();

                //assert
                _charger.Received(0).StartCharge();

            }

        
            [Test]
        public void OnCurrentValueEvent_CurrentIsIllegal_ExceptionIsThrown()
        {
          
            //act + assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = -200 }));
        }

        [Test]
        public void OnCurrentValueEvent_CurrentIsZero_ConnectedIsFalse()
        {
            //act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });

            //assert
            Assert.That(_uut.Connected, Is.EqualTo(false));
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void OnCurrentValueEvent_CurrentIsHigherThan0AndLessThanOrEqualTo5_IsCompletedIsTrue(double current)
        {
            //act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });

            //assert
            Assert.That(_uut.IsCompleted, Is.EqualTo(true));
        }

        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        public void OnCurrentValueEvent_CurrentIsHigherThan5AndLessThanOrEqualTo500_IsCompletedIsFalse(double current)
        {
            //act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });

            //assert
            Assert.That(_uut.IsCompleted, Is.EqualTo(false));
        }

        [Test]
        public void OnCurrentValueEvent_CurrentIs600_StopChargeIsCalled()
        {
            //act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 600 });

            //assert
            _charger.Received(1).StopCharge();
        }
    }
}
