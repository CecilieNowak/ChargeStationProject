using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class Tests
    {
        private IDisplay _display;
        private IDisplay _displayS;
        [SetUp]
        public void Setup()
        {
            _display = new DisplayInstructions();
            _displayS = new DisplayStatus();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
    
}

