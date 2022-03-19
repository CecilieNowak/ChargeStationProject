using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        public bool DoorIsOpen { get; private set; } //TODO slettes?

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Udkast til constructor
        public StationControl(IDoor door, IChargeControl chargeControl)
        {
            _door = door;
            _door.OpenDoorEvent += HandleOnOpenDoorEvent;
          
        }



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.isConnected() == true) //TODO Property? .connected == true
                    {
                        _door.LockDoor();
                        _charger.startCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }

                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.stopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleOnOpenDoorEvent(object? sender, DoorStateEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
              
                    using (var writer = File.AppendText(logFile))
                    {
                        writer.WriteLine(DateTime.Now + ": Døren er åben");
                    }

                    Console.WriteLine("(Handling) Døren er nu åben");
                    Console.WriteLine("Tilslut telefon");

                    DoorIsOpen = e.DoorIsOpen; // TODO skal slettes?
                    _state= LadeskabState.DoorOpen;

                    break;


                case LadeskabState.DoorOpen:
                    if (e.DoorIsOpen == false)
                    {
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Døren er lukket");
                        }

                        Console.WriteLine("(Handling) Døren er nu lukket");
                        Console.WriteLine("Indlæs telefon");

                        DoorIsOpen = e.DoorIsOpen; //TODO skal slettes?
                    }
                   
                    break;

                case LadeskabState.Locked:
                    Console.WriteLine("Ladeskabet er optaget!");

                    using (var writer = File.AppendText(logFile))
                    {
                        writer.WriteLine(DateTime.Now + ": Ladeskabet er optaget!");
                    }

                    DoorIsOpen = e.DoorIsOpen; //TODO skal slettes?

                    break;


            }
        }
       
    }

}