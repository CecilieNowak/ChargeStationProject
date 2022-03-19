using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using UsbSimulator;

namespace ChargeStationProject.Test
{
    [TestFixture]
    class TestChargeControl
    {
        private IUsbCharger _charger;
        //private IDisplay _display;
        private IChargeControl _uut;

            [SetUp]
            public void Setup()
            {
                _charger = Substitute.For<IUsbCharger>();
                _uut = new ChargeControl(_charger);
            }

            [Test]
            public void StopChargeMethod_StopChargeMethodIsCalled_chargerReceivedMethodCall()
            {
                _uut.stopCharge();

                _charger.Received(1).StopCharge();

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

            //TODO OnCurrentEvent
    }
}
