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



        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
       // private ILogFile logFile; //TODO uncomment
        private IDisplay _display; // CBE tilføjet
        private RfidReader _rfid;

        public bool doorIsOpen { get; set; }

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IDisplay display, IChargeControl chargeControl)
        {
          _state = LadeskabState.Available;

            _door = door;
            _charger = chargeControl;
            _door.OpenDoorEvent += HandleOnOpenDoorEvent;
            _display = display; //CBE tilføjet
            _rfid = new RfidReader();

        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected == true) 
                    {
                        _door.LockDoor();
                        _charger.startCharge();
                     //   _oldId = id; //TODO CSN skal dette slettes?

                     _rfid.ValidateRfidEntryRequest(id);


                     using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

              //      logFile.LogDoorLocked(id);

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
                    if (_rfid.ValidateRfidEntryRequest(id)==true) // hvem skal lave den her?
                    {
                        _charger.stopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                    _charger.stopCharge();
                    _door.UnlockDoor();

             //       logFile.LogDoorUnlocked(id);


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

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Døren er åben");
                        }

                        Console.WriteLine("(Handling) Døren er nu åben");
                        
                        _display.showMessage("Tilslut telefon"); //TODO display instructions


                        _state = LadeskabState.DoorOpen;
                    }

                    break;


                case LadeskabState.DoorOpen:
                    if (e.DoorIsOpen == false)
                    {
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Døren er lukket");
                        }

                        Console.WriteLine("(Handling) Døren er nu lukket");
                        _display.showMessage("Indlæs RFID");
                        //TODO Display instructions

                        _state = LadeskabState.Available;
                    }

                    break;

                case LadeskabState.Locked:
                    Console.WriteLine("Ladeskabet er optaget!");

                    using (var writer = File.AppendText(logFile))
                    {
                        writer.WriteLine(DateTime.Now + ": Ladeskabet er optaget!");
                    }

                    break;
            }

        }

     /* public bool CheckId(int oldId, int id) // //tilføjet CBE
      {
            if (oldId == id)
                return true;

            return false;
      }
     */ //TODO skal måske slettes?
        

    }
}