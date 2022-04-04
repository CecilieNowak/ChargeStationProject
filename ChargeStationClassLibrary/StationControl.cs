using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargeStationClassLibrary;

namespace ChargeStationProject
{
    public class StationControl 
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
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
        private int oldId;
      
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
            _rfid.RFIDChangedEvent += RfidDetected;

        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(object sender, RfidEventArgs e)
        {

            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected == true) 
                    {
                        oldId = e.RFID;
                        _door.LockDoor();
                        _chargeControl.startCharge();
                        _logFile.LogDoorLocked(e.RFID);
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
                    if (oldId == e.RFID)
                    {
                        _chargeControl.stopCharge();
                        _door.UnlockDoor();
                        _logFile.LogDoorUnlocked(e.RFID);
                        _display.showMessage("Tag din telefon ud af skabet og luk døren"); //tilføjet CBE

                        _state = LadeskabState.Available;
                        //oldId = 0; todo - skal slettes?
                    }
                    else if (e.RFID != oldId)
                    {
                        _display.showMessage("Forkert RFID tag"); //tilføjet CBE
                    }

                    break;
        }
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