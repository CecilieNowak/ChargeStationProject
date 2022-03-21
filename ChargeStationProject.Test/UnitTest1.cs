using NUnit.Framework;

namespace ChargeStationProject.Test
{
    public class Tests
    {
        private IDisplay _display;
        [SetUp]
        public void Setup()
        {
            _display = new DisplayInstructions();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase("hej")]
        public void DisplayInstruction_showMessage_resiveIsCorrect(string input)
        {
            _display.showMessage(input);
            Assert.That(_display.SaveMessage, Is.EqualTo(input));

        }
    }
}