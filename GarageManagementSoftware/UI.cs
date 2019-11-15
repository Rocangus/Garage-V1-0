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
                        CreateGarage();
                        break;
                    case "2":
                        if (GarageExists())
                            ListAllVehicles();
                        break;
                    case "3":
                        if (GarageExists())
                            ListVehicleTypeCounts();
                        break;
                    case "4":
                        if (GarageExists())
                        {
                            if (handler.GetGarageRemainingSpaces() == 0)
                            {
                                Console.WriteLine($"All {handler.GetGarageCapacity()} spaces in the garage are occupied.");
                                break;
                            }
                            ParkVehicle();
                        }
                        break;
                    case "5":
                        if (GarageExists())
                            UnparkVehicle();
                        break;
                    case "6":
                        if (GarageExists())
                            SetGarageCapacity();
                        break;
                    default:
                        break;
                }
            } while (!input.ToLower().StartsWith("q"));
        }

        private void SetGarageCapacity()
        {
            Console.Write("Please specify the new capacity: ");
            var v = GetInteger(0, 1024);
            var vehicleCount = handler.GetGarageVehicleCount();
            if (v < vehicleCount)
            {
                Console.WriteLine($"Cannot set garage capacity to {v} because the garage currently contains {vehicleCount} vehicles.");
                return;
            }
            handler.SetCapacity(v);
        }

        private void ParkVehicle()
        {
            string registrationNumber;
            int typeChoice = GetVehicleTypeChoice();
            string color;
            int emptyMass;
            if (typeChoice == 0)
            {
                ParkAircraft();
            }
            else if (typeChoice == 3)
            {
                ParkCar();
            }
            else
            {
                PropertyInfo[] vehicleProperties = VehicleProperties.GetProperties(typeChoice);
                GetBasicProperties(out registrationNumber, out color, out emptyMass);
                if (vehicleProperties[0].PropertyType.IsAssignableFrom(integer))
                {
                    ParkBusOrMotorcycle(registrationNumber, typeChoice, color, emptyMass);
                }
                if (vehicleProperties[0].PropertyType.IsAssignableFrom(dec))
                {
                    ParkBoat(registrationNumber, color, emptyMass);
                }
            }
        }

        private int GetVehicleTypeChoice()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Please select one of the following vehicle types: ");
            builder.Append(GetTypeString(vehicleTypes));
            Console.WriteLine(builder.ToString());
            int typeChoice = GetInteger(0, 4);
            return typeChoice;
        }

        private void CreateGarage()
        {
            Console.WriteLine("Please enter the capacity of the garage.");
            int capacity = GetInteger(0, 1024);
            handler.NewGarage(capacity);
        }

        private void ListAllVehicles()
        {
            var vehicleStrings = handler.ListVehicles();
            if (vehicleStrings != null)
            {
                foreach (var str in vehicleStrings)
                {
                    Console.WriteLine(str);
                }
            }
        }

        private void ListVehicleTypeCounts()
        {
            var vehicleTypeCounts = handler.GetVehicleTypeCounts();
            if (vehicleTypeCounts != null)
            {
                foreach (var item in vehicleTypeCounts.Keys)
                {
                    Console.WriteLine($"{item}: {vehicleTypeCounts[item]}");
                }
            }
        }

        private void UnparkVehicle()
        {
            string registrationNumber;
            Console.WriteLine("Please specify a registration number to unpark:");
            registrationNumber = Console.ReadLine();
            if (handler.UnparkVehicle(registrationNumber))
                Console.WriteLine($"Vehicle {registrationNumber} unparked.");
            else
                Console.WriteLine($"No vehicle with registration number {registrationNumber} could be found.");
        }

        private void ParkBoat(string registrationNumber, string color, int emptyMass)
        {
            Console.Write("Length in meters: ");
            var length = GetDecimal(0, 2000);
            var boatArguments = new Tuple<string, string, int, decimal>(registrationNumber, color, emptyMass, length);
            Boat boat;
            if (VehicleCreator.CreateBoat(boatArguments, out boat))
            {
                if (handler.ParkVehicle(boat))
                    AnnounceParkingSuccess(registrationNumber);
            }
        }

        private void ParkBusOrMotorcycle(string registrationNumber, int typeChoice, string color, int emptyMass)
        {
            int numberOfWheels = GetNumberOfWheels();
            Console.WriteLine("Specify cylinder volume (motorcycle) or passenger capacity (bus).");
            var passengersOrCylinderVolume = GetInteger(0, 10000);
            var busAndMotorcycleParameters = new BusAndMotorcycleParameters(registrationNumber, color, emptyMass, numberOfWheels, passengersOrCylinderVolume);
            if (typeChoice == 2)
            {
                ParkBus(registrationNumber, busAndMotorcycleParameters);
            }
            if (typeChoice == 4)
            {
                ParkMotorcycle(registrationNumber, busAndMotorcycleParameters);
            }
        }

        private void ParkMotorcycle(string registrationNumber, BusAndMotorcycleParameters busAndMotorcycleParameters)
        {
            Motorcycle motorcycle;
            if (VehicleCreator.CreateMotorcycle(busAndMotorcycleParameters, out motorcycle))
            {
                if (handler.ParkVehicle(motorcycle))
                    AnnounceParkingSuccess(registrationNumber);
            }
        }

        private void ParkBus(string registrationNumber, BusAndMotorcycleParameters busAndMotorcycleParameters)
        {
            Bus bus;
            if (VehicleCreator.CreateBus(busAndMotorcycleParameters, out bus))
            {
                if (handler.ParkVehicle(bus))
                    AnnounceParkingSuccess(registrationNumber);
            }
        }

        private void ParkAircraft()
        {
            AircraftDTO aircraftDTO = new AircraftDTO();
            PopulateAircraftDTO(aircraftDTO);
            Aircraft aircraft;
            if (VehicleCreator.CreateAircraft(aircraftDTO, out aircraft))
            {
                if (handler.ParkVehicle(aircraft))
                    AnnounceParkingSuccess(aircraft.RegistrationNumber);
            }
        }

        private void ParkCar()
        {
            string registrationNumber, color;
            int emptyMass;
            GetBasicProperties(out registrationNumber, out color, out emptyMass);
            int numberOfWheels = GetNumberOfWheels();
            Console.WriteLine(GetTypeString(propulsionTypes));
            int propulsionType = GetInteger(0, 6);
            var carArgs = Tuple.Create(registrationNumber, color, emptyMass, numberOfWheels, (CarPropulsionType)propulsionType);
            Car car;
            if (VehicleCreator.CreateCar(carArgs, out car))
            {
                if (handler.ParkVehicle(car))
                    AnnounceParkingSuccess(registrationNumber);
            }
        }

        private static int GetNumberOfWheels()
        {
            Console.Write("Number of wheels: ");
            var numberOfWheels = GetInteger(0, 20);
            return numberOfWheels;
        }

        private static void AnnounceParkingSuccess(string registrationNumber)
        {
            Console.WriteLine($"Vehicle {registrationNumber} created and parked!");
        }

        private static void GetBasicProperties(out string registrationNumber, out string color, out int emptyMass)
        {
            Console.Write("Registration number: ");
            registrationNumber = Console.ReadLine();
            Console.Write("Color: ");
            color = Console.ReadLine();
            Console.Write("Empty mass in kilograms:");
            emptyMass = GetInteger(0, int.MaxValue);
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

        public static decimal GetDecimal(int min, int max)
        {
            decimal result = 0;
            string input;
            bool done = false;
            while (!done)
            {
                input = Console.ReadLine();
                if (decimal.TryParse(input, out result) && result >= min && result <= max)
                {
                    done = true;
                    break;
                }
                Console.WriteLine($"Please enter a decimal number, minimum {min}, maximum {max}.");
            }
            return result;
        }

        private bool GarageExists()
        {
            if (handler.GarageExists())
            {
                return true;
            }
            Console.WriteLine("No garage found. Please create a garage and try again.");
            return false;
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
