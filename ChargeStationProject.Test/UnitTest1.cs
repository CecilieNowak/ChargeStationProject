using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class Tests
    {
        private FakeDisplayInstructions _display;
        private FakeDisplayStatus _displayS;
        [SetUp]
        public void Setup()
        {
            _display = new FakeDisplayInstructions();
            _displayS = new FakeDisplayStatus();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
    


