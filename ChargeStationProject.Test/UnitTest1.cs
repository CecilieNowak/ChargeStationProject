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
