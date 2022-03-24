using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class TestDisplay
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

        [TestCase("Instruktioner")]
        public void DisplayInstruction_showMessage_resiveIsCorrect(string input)
        {
            _display.showMessage(input);
            Assert.That(_display.SaveMessage, Is.EqualTo(input));
        }

        [TestCase("Status")]
        public void DisplayStatus_showMessage_resiveIsCorrect(string input)
        {
            _displayS.showMessage(input);
            Assert.That(_displayS.SaveMessage, Is.EqualTo(input));
        }
    }
}
    


