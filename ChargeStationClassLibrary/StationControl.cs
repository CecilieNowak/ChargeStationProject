using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeStationClassLibrary;

namespace ChargeStationProject
{
    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen

        };

        private LadeskabState _state;
        private IChargeControl _chargeControl;
        private IDoor _door;
        private IDisplay _display; // CBE tilføjet
        private IRfidReader _rfid;
        private ILogFile _logFile;
      
        // Her mangler constructor
        public StationControl(IDoor door, IDisplay display, IChargeControl chargeControl, ILogFile logFile, IRfidReader rfidReader)
        {
          _state = LadeskabState.Available;

            _door = door;
            _chargeControl = chargeControl;
            _door.OpenDoorEvent += HandleOnOpenDoorEvent;
            _display = display; //CBE tilføjet
            _logFile = logFile;
            _rfid = rfidReader; 

        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected == true) 
                    {
                        _door.LockDoor();
                        _chargeControl.startCharge();
                     

                     _rfid.ValidateRfidEntryRequest(id);

                     _logFile.LogDoorLocked(id);

                    _display.showMessage(
                        "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."); //tilføjet CBE
                    _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.showMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen."); //tilføjet CBE
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (_rfid.ValidateRfidEntryRequest(id) == true)
                    {
                        _chargeControl.stopCharge();
                        _door.UnlockDoor();

                        _logFile.LogDoorUnlocked(id);


                        _display.showMessage("Tag din telefon ud af skabet og luk døren"); //tilføjet CBE
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.showMessage("Forkert RFID tag"); //tilføjet CBE
                    }

                break;
        }
    }

    public LadeskabState GetLadeskabState()
        {
            return _state;
        }


        public void SetLadeskabState(LadeskabState s)
        { 
            _state = s;
        }

        // Her mangler de andre trigger handlere
        private void HandleOnOpenDoorEvent(object sender, DoorStateEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (e.DoorIsOpen == true)
                    {

                        Console.WriteLine("(Handling) Døren er nu åben");
                        
                        _display.showMessage("Tilslut telefon");

                        _chargeControl.Connected = true;

                        _state = LadeskabState.DoorOpen;
                    }

                    break;


                case LadeskabState.DoorOpen:
                    if (e.DoorIsOpen == false)
                    {

                        Console.WriteLine("(Handling) Døren er nu lukket");
                        _display.showMessage("Indlæs RFID");

                        _state = LadeskabState.Available;
                    }

                    break;

                case LadeskabState.Locked:
                    Console.WriteLine("Ladeskabet er optaget!");

                    break;
            }

        }


    }
}