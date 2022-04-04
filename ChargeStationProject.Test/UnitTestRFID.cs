using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class UnitTestRFID
    {
        private RfidReader _uut;


        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
        }


        //[Test]
        //public void RequestEntry_ValidRFID_ReturnTrue()
        //{
        //    bool rfidEntryRequest = _uut.ValidateRfidEntryRequest(12345678);
        //    Assert.That((rfidEntryRequest), Is.True);
        //}


        //[Test]
        //public void RequestEntry_InvalidRFID_ReturnFalse()
        //{
        //    bool rfidEntryRequest = _uut.ValidateRfidEntryRequest(444);
        //    Assert.That((rfidEntryRequest), Is.False);
        //}



    }
}




    