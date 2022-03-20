﻿using System;
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
        private IDisplay _display; // CBE tilføjet

        public bool doorIsOpen { get; set; }

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door,IDisplay display)
        {
            door.DoorIsOpenEvent += HandleOnDoorIsOpenEvent;
            _display = display; //CBE tilføjet
        }

        
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                  // Check for ladeforbindelse
                    if (_charger.Connected)
                    { _door.LockDoor();
                        _charger.StartCharge(); 
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.showMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."); //tilføjet CBE
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
                    if (CheckId(_oldId,id)) //
                    {
                        _charger.StopCharge(); 
                        _door.UnlockDoor(); 
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

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

        // Her mangler de andre trigger handlere
        private void HandleOnDoorIsOpenEvent(object sender, DoorIsOpen e)
        {
            doorIsOpen = e.DoorOpenArgs;

        }

        public void DoorOpened()
        {
            _display.showMessage("Tilslut telefon"); //tilføjet CBE
        }
        public void DoorClosed()
        {
            _display.showMessage("Indlæs RFID"); //tilføjet CBE
        }

        public bool CheckId(int oldId, int id) // //tilføjet CBE
        {
            if (oldId == id)
                return true;

            return false;
        }
        

    }
}
