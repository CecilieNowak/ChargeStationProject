using NUnit.Framework;
using NSubstitute;

namespace ChargeStationProject.Test
{
    public class Tests
    {
        private IDisplay _display;
        private IDisplay _displayS;
        private IDoor _door;
        private IRfidReader _RfidReader;
        private StationControl _uut;


        [SetUp]
        public void Setup()
        {

            _display = new DisplayInstructions();
            _displayS = new DisplayStatus();
            _door = Substitute.For<IDoor>();
            _RfidReader = Substitute.For<IRfidReader>();
            _door = Substitute.For<IDoor>();

        }


        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]

        public void RequestEntry_ValidRFID_ReturnTrue()
        {
            _RfidReader.ValidateEntryRequest("valid").Returns(true);
            Assert.That(_uut.RequestEntry("valid"), Is.EqualTo(true));
        }


        [Test]
        public void RequestEntry_InvalidRFID_ReturnFalse()
        {
            _RfidReader.ValidateRfidEntryRequest("invalid").Returns(false);
            Assert.That(_uut.RequestEntry("invalid"), Is.EqualTo(false));
        }

        [Test]
        public void RequestEntry_ValidRFID_NotifyEntryGranted()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry("valid");
            _entryNotification.Received(1).NotifyEntryGranted(Arg.Any<int>());
        }


        [Test]
        public void RequestEntry_InvalidRFID_NotifyEntryDenied()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry("invalid");
            _entryNotification.Received(1).NotifyEntryDenied(Arg.Any<int>());
        }

        [Test]
        public void RequestEntry_validRFID_DoorOpens()
        {
            _RfidReader.ValidateRfidEntryRequest("valid").Returns(true);
            _uut.RequestEntry("valid");
            _door.Received(1).Open();
        }

        [Test]
        public void RequestEntry_validRFID_DoorCloses()
        {
            _RfidReader.ValidateRfidEntryRequest("valid").Returns(true);
            _uut.RequestEntry("valid");
            _door.Received(1).Close();
        }


        [Test]
        public void RequestEntry_invalidRFID_DoorDoesNotOpen()
        {
            _RfidReader.ValidateRfidEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry("not valid");
            _door.DidNotReceive().Open();
        }

    }
}



