    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography.X509Certificates;
    using ChargeStationClassLibrary;
    using ChargeStationProject;
    using UsbSimulator;


    class Program
    {
        
        static void Main(string[] args)
        {

            IDoor door = new Door();
           IDisplay display = new DisplayInstructions();
           IChargeControl charger = new ChargeControl(new UsbChargerSimulator(),display);
           ILogFile logFile = new LogFile();
           IRfidReader rfidReader = new RfidReader();
;            var stationControl = new StationControl(door, display, charger, logFile,rfidReader); 
            var arg = new DoorStateEventArgs(); //TODO hvor skal dette instantieres?




        bool finish = false;
        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.DoorOpen();
                    break;

                case 'C':
                    door.DoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    stationControl.RfidDetected(id);
                    break;

                default:
                    break;






            }

        } while (!finish);
    }
    }

