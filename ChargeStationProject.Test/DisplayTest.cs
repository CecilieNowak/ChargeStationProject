using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ChargeStationProject.Test
{
    [TestFixture]

    class DisplayTest
    {

        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplayInstructions();

        }

        [Test]
        public void ShowMessageIsCalled_PropertySetToMethodParameter_ParameterIsEqualToProperty()
        {
            _uut.showMessage("Test1");

            Assert.That(_uut.SaveMessage, Is.EqualTo("Test1"));
        }

       

    }

    [TestFixture]

    class DisplayStatusTest
    {

        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplayStatus();

        }

        [Test]
        public void ShowMessageIsCalled_PropertySetToMethodParameter_ParameterIsEqualToProperty()
        {
            _uut.showMessage("Test1");

            Assert.That(_uut.SaveMessage, Is.EqualTo("Test1"));
        }



    }
}
