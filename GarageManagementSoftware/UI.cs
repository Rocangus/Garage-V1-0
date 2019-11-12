using System;
using System.Collections.Generic;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    public class UI
    {
        private GarageHandler handler;

        public UI()
        {
            handler = new GarageHandler();
            MainMenu();
        }

        private void MainMenu()
        {
            PrintWelcomeMessage();
            string input;
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Please enter the capacity of the garage.");
                        var capacityString = Console.ReadLine();
                        int capacity;
                        if (int.TryParse(capacityString, out capacity))
                            handler.NewGarage(capacity);
                        break;
                    case "2":
                        var vehicleStrings = handler.ListVehicles();
                        if (vehicleStrings != null)
                        {
                            foreach (var str in vehicleStrings)
                            {
                                Console.WriteLine(str);
                            }
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please specify a registration number to unpark:");

                        break;
                    default:
                        break;
                }
            } while (!input.ToLower().StartsWith("q"));
        }

        private void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the garage management software! Please make a selection...");
        }

        private void PrintMenuOptions()
        {
            Console.WriteLine("1. Create a new garage.");
            Console.WriteLine("2. List all parked vehicles.");
            Console.WriteLine("3. List the count of the different types of vehicles in the garage.");
            Console.WriteLine("4. Park a vehicle in the garage.");
            Console.WriteLine("5. Unpark a vehicle from the garage.");
            Console.WriteLine("6. Set the capacity of the garage.");
            Console.WriteLine("7. List vehicles based on properties (number of wheels etc).");
            Console.WriteLine("8. Search for a vehicle by registration number.");
            Console.WriteLine("Q: Quit the program.");
        }
    }
}
