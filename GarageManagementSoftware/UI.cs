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
        private string[] aircraftTypes;
        private string[] aircraftEngineTypes;
        private string[] propulsionTypes;
        private Type str = typeof(string);
        private Type integer = typeof(int);
        private Type dec = typeof(decimal);

        public UI()
        {
            handler = new GarageHandler();
            vehicleTypes = Enum.GetNames(typeof(VehicleTypes));
            aircraftTypes = Enum.GetNames(typeof(AircraftType));
            aircraftEngineTypes = Enum.GetNames(typeof(AircraftEngineType));
            propulsionTypes = Enum.GetNames(typeof(CarPropulsionType));
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
                        builder.Append(GetTypeString(vehicleTypes));
                        Console.WriteLine(builder.ToString());
                        int typeChoice = GetInteger(0, 4);
                        if (typeChoice == 0)
                        {
                            AircraftDTO aircraftDTO = new AircraftDTO();
                            PopulateAircraftDTO(aircraftDTO);
                            Aircraft aircraft;
                            if (VehicleCreator.CreateAircraft(aircraftDTO, out aircraft))
                            {
                                if (handler.ParkVehicle(aircraft))
                                    Console.WriteLine($"Vehicle {aircraft.RegistrationNumber} created and parked!");
                            }
                        }
                        else if (typeChoice == 3)
                        {

                            Console.WriteLine(GetTypeString(propulsionTypes);
                            int propulsionType = GetInteger(0, 6);
                        }
                        else
                        {
                            PropertyInfo[] vehicleProperties = VehicleProperties.GetProperties(typeChoice);
                            Console.WriteLine(vehicleProperties[0].PropertyType.FullName);
                            if (vehicleProperties[0].PropertyType.IsAssignableFrom(str))
                            {
                                Console.WriteLine("Can be assigned as a string.");
                            }
                            if (vehicleProperties[0].PropertyType.IsAssignableFrom(integer))
                            {
                                Console.WriteLine("Can be assigned as an integer.");
                            }
                            if (vehicleProperties[0].PropertyType.IsAssignableFrom(dec))
                            {
                                Console.WriteLine("Can be assigned as a decimal number.");
                            }
                        }
                        // ToDo: Figure out how to process the PropertyInfo array to get the right user input
                        break;
                    case "5":
                        Console.WriteLine("Please specify a registration number to unpark:");
                        var registrationNumber = Console.ReadLine();
                        if (handler.UnparkVehicle(registrationNumber))
                            Console.WriteLine($"Vehicle {registrationNumber} unparked.");
                        else
                            Console.WriteLine($"No vehicle with registration number {registrationNumber} could be found.");
                        break;
                    default:
                        break;
                }
            } while (!input.ToLower().StartsWith("q"));
        }

        private void PopulateAircraftDTO(AircraftDTO aircraftDTO)
        {
            Console.Write("Registration number: ");
            aircraftDTO.RegistrationNumber = Console.ReadLine();
            Console.Write("Color or livery: ");
            aircraftDTO.ColorOrLivery = Console.ReadLine();
            Console.Write("Empty mass in kilograms: ");
            aircraftDTO.EmptyMass = GetInteger(0, int.MaxValue);
            Console.Write("Number of landing gear assemblies: ");
            aircraftDTO.NumberOfLandingGearAssemblies = GetInteger(0, 20);
            Console.Write("Manufacturer: ");
            aircraftDTO.Manufacturer = Console.ReadLine();
            Console.Write("Model: ");
            aircraftDTO.Model = Console.ReadLine();
            Console.Write("Type designator: ");
            aircraftDTO.TypeDesignator = Console.ReadLine();
            Console.WriteLine("Type description: " + GetTypeString(aircraftTypes));
            aircraftDTO.AircraftType = (AircraftType)GetInteger(0, 5);
            Console.WriteLine("Engine type: " + GetTypeString(aircraftEngineTypes));
            aircraftDTO.EngineType = (AircraftEngineType)GetInteger(0, 5);
            Console.Write("Engine count: ");
            aircraftDTO.EngineCount = GetInteger(0, 16);
        }

        public string GetTypeString(string[] typeStrings)
        {

            string v = "";
            var lastElement = typeStrings.Length - 1;
            for (int i = 0; i < lastElement; i++)
            {
                v = string.Concat(v, i + ": " + typeStrings[i] + ", ");
            }
            v = string.Concat(v, lastElement + ": " + typeStrings[lastElement]);
            return v;
        }

        public static int GetInteger(int min, int max)
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
