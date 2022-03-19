    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography.X509Certificates;
    using ChargeStationProject;


    class Program
    {
        
        static void Main(string[] args)
        {

            var door = new Door();
            var charger = new ChargeControl();
            //var stationControl = new StationControl(door, charger); //TODO uncomment
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
                      // rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }

