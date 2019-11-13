using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GarageManagementSoftware.Vehicles;
using System.Reflection;

namespace GarageManagementSoftware
{
    public class UI
    {
        private GarageHandler handler;
        private string[] vehicleTypes;

        public UI()
        {
            handler = new GarageHandler();
            vehicleTypes = Enum.GetNames(typeof(VehicleTypes));
            MainMenu();
        }

        private void MainMenu()
        {
            PrintWelcomeMessage();
            string input;
            do
            {
                PrintMenuOptions();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Please enter the capacity of the garage.");
                        int capacity = GetInteger(0, 1024);
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
                    case "4":
                        StringBuilder builder = new StringBuilder();
                        builder.Append("Please select one of the following vehicle types: ");
                        var lastElement = vehicleTypes.Length - 1;
                        for (int i = 0; i < lastElement; i++)
                        {
                            builder.Append(i + ": " + vehicleTypes[i] + ", ");
                        }
                        builder.Append(lastElement + ": " + vehicleTypes[lastElement]);
                        Console.WriteLine(builder.ToString());
                        int typeChoice = GetInteger(0, 4);
                        PropertyInfo[] vehicleProperties = VehicleProperties.GetProperties(typeChoice);
                        // ToDo: Figure out how to process the PropertyInfo array to get the right user input
                        break;
                    case "5":
                        Console.WriteLine("Please specify a registration number to unpark:");
                        var registrationNumber = Console.ReadLine();
                        handler.UnparkVehicle(registrationNumber);
                        break;
                    default:
                        break;
                }
            } while (!input.ToLower().StartsWith("q"));
        }

        private int GetInteger(int min, int max)
        {
            int result = 0;
            string input;
            bool done = false;
            while (!done)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out result) && result >= min && result <= max)
                {
                    done = true;
                    break;
                }
                Console.WriteLine($"Please enter an integer, minimum {min}, maximum {max}.");
            }
            return result;
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
