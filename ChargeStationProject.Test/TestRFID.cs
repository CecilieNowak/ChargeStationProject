using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    [TestFixture]
    class TestRFID
    {


        private IRfidReader _uut;
        private RfidEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new RfidReader();
            _uut.RFIDChangedEvent +=
                (o, args) => { _receivedEventArgs = args; };

        }

        [Test]
        public void ValidateRfidEntryRequestCalledWithNoSubscribtion_ThrowsNothing()
        {
            _uut = new RfidReader();
            Assert.That(() => _uut.ValidateRfidEntryRequest(1234), Throws.Nothing);
        }


        [Test]
        public void ValidateRfidEntryRequestIsCalled_EventIsFired()
        {

            _uut.ValidateRfidEntryRequest(1234);

            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void ValidateRfidEntryRequestIsCalled_CorrectIntRecieved()
        {
            _uut.ValidateRfidEntryRequest(1234);
            Assert.That(_receivedEventArgs.RFID, Is.EqualTo(1234));
        }
    }

}
