﻿using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class UnitTestRFID
    {
        private IDoor _door;
        private IRfidReader _RfidReader;
        private RfidReader _uut;


        [SetUp]
        public void Setup()
        {

            _door = Substitute.For<IDoor>();
            _RfidReader = Substitute.For<IRfidReader>();
            _door = Substitute.For<IDoor>();
        }


        [Test]
        public void RequestEntry_ValidRFID_ReturnTrue()
        {
            _RfidReader.ValidateEntryRequest(12345678).Returns(true);
            Assert.That(_uut.RfidDetected(12345678), Is.EqualTo(true));
        }


        [Test]
        public void RequestEntry_InvalidRFID_ReturnFalse()
        {
            _RfidReader.ValidateRfidEntryRequest(12345678).Returns(false);
            Assert.That(_uut.RfidDetected(12345678), Is.EqualTo(false));
        }

        [Test]
        public void RequestEntry_ValidRFID_NotifyEntryGranted()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RfidDetected(12345678);
            _entryNotification.Received(1).NotifyEntryGranted(Arg.Any<int>());
        }


        [Test]
        public void RequestEntry_InvalidRFID_NotifyEntryDenied()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RfidDetected(12345678);
            _entryNotification.Received(1).NotifyEntryDenied(Arg.Any<int>());
        }

        [Test]
        public void RequestEntry_validRFID_DoorOpens()
        {
            _RfidReader.ValidateRfidEntryRequest(12345678).Returns(true);
            _uut.RfidDetected(12345678);
            _door.Received(1).Open();
        }

        [Test]
        public void RequestEntry_validRFID_DoorCloses()
        {
            _RfidReader.ValidateRfidEntryRequest(12345678).Returns(true);
            _uut.RfidDetected(12345678);
            _door.Received(1).Close();
        }


        [Test]
        public void RequestEntry_invalidRFID_DoorDoesNotOpen()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RfidDetected(1111111);
            _door.DidNotReceive().Open();
        }

    }
    }   




    