using GarageManagementSoftware.Vehicles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GarageManagementSoftware
{
    public class GarageHandler
    {

        public static List<string> errorMessages = new List<string>();
        private Garage<Vehicle> garage;
        private Dictionary<string, Type> vehicleTypes;
        public GarageHandler()
        {
            vehicleTypes = new Dictionary<string, Type>();
            vehicleTypes["Aircraft"] = typeof(Aircraft);
            vehicleTypes["Boat"] = typeof(Boat);
            vehicleTypes["Bus"] = typeof(Bus);
            vehicleTypes["Car"] = typeof(Car);
            vehicleTypes["Motorcycle"] = typeof(Motorcycle);
        }

        public void SetGarage(Garage<Vehicle> newGarage)
        {
            garage = newGarage;
        }

        public bool NewGarage(int capacity)
        {
            garage = new Garage<Vehicle>(capacity);
            return garage != null;
        }

        public int GetGarageCapacity()
        {
            if (garage == null)
                return -1;
            else
                return garage.Capacity;
        }

        public int GetGarageVehicleCount()
        {
            if (garage == null)
                return -1;
            else
                return garage.Count;
        }

        public int GetGarageRemainingSpaces()
        {
            if (garage == null)
                return -1;
            else return GetGarageCapacity() - GetGarageVehicleCount();
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            if (garage == null)
                return false;
            else
            {
                return garage.ParkVehicle(vehicle);
                //var vehicleType = input[0].ToLower();
                //switch (vehicleType)
                //{
                //    case "aircraft":
                //        return ParkAircraft(input);
                //    case "boat":
                //        return ParkBoat(input);
                //    case "bus":
                //        return ParkBus(input);
                //    case "car":
                //        return ParkCar(input);
                //    case "motorcycle":
                //        return ParkMotorcycle(input);
                //    default:
                //        return false;
                //}
            }
        }

        internal bool GetVehicleByRegistrationNumber(string registrationNumber, out Vehicle vehicle)
        {
            vehicle = null;
            if (garage.GetVehicleByRegistrationNumber(registrationNumber, out vehicle))
                return true;
            else return false;
        }

        public static bool CreateAircraft(string[] input, out Aircraft vehicle)
        {
            vehicle = null;
            if (input.Length != 11)
            {
                errorMessages.Add($"To park an aircraft you need to specify 11 values. {input.Length} values were found.");
                return false;
            }
            int numberOfWheels, emptyMass, engineCount;
            AircraftType aircraftType = (AircraftType)Enum.Parse(typeof(AircraftType), input[8]);
            AircraftEngineType aircraftEngineType = (AircraftEngineType)Enum.Parse(typeof(AircraftEngineType), input[9]);
            int.TryParse(input[10], out engineCount);
            if (!int.TryParse(input[3], out numberOfWheels) || !int.TryParse(input[4], out emptyMass) || !int.TryParse(input[10], out engineCount))
            {
                errorMessages.Add("Please make sure to enter numbers where requested.");
                return false;
            }
            vehicle = new Aircraft(input[1], input[2], numberOfWheels, emptyMass, input[5], input[6], input[7], aircraftType, aircraftEngineType, engineCount);
            return true;
        }

        internal void SetCapacity(int v)
        {
            garage.SetCapacity(v);
        }

        private bool ParkCar(string[] input)
        {
            if (input.Length != 6)
            {
                errorMessages.Add($"To park a car you need to specify 6 values. {input.Length} values were found.");
                return false;
            }
            int numberOfWheels, emptyMass;
            CarPropulsionType propulsionType = (CarPropulsionType)Enum.Parse(typeof(CarPropulsionType), input[5]);
            if (!int.TryParse(input[3], out numberOfWheels) || !int.TryParse(input[4], out emptyMass))
            {
                errorMessages.Add("Please make sure to enter numbers where requested.");
                return false;
            }
            var vehicle = new Car(input[1], input[2], numberOfWheels, emptyMass, propulsionType);
            if (garage.ParkVehicle(vehicle))
                return true;
            else
                return false;
        }

        internal List<Vehicle> GetVehicles()
        {
            var vehicles = new List<Vehicle>();
            foreach (var vehicle in garage)
            {
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        private bool ParkMotorcycle(string[] input)
        {
            if (input.Length != 6)
            {
                errorMessages.Add($"To park a motorcycle you need to specify 6 values. {input.Length} values were found.");
                return false;
            }
            int numberOfWheels, emptyMass, cylinderVolume;
            if (!int.TryParse(input[3], out numberOfWheels) || !int.TryParse(input[4], out emptyMass) || !int.TryParse(input[5], out cylinderVolume))
            {
                errorMessages.Add("Please make sure to enter numbers where requested.");
                return false;
            }
            var vehicle = new Motorcycle(input[1], input[2], numberOfWheels, emptyMass, cylinderVolume);
            if (garage.ParkVehicle(vehicle))
                return true;
            else
                return false;
        }

        public Dictionary<string, int> GetVehicleTypeCounts()
        {
            if (garage == null)
                return null;
            else
            {
                return PopulateTypeCountDictionary();
            }
        }

        private Dictionary<string, int> PopulateTypeCountDictionary()
        {
            var vehicleTypeCounts = new Dictionary<string, int>();
            foreach (var vehicle in garage)
            {
                if (TestIsAircraft(vehicle, vehicleTypeCounts)) continue;
                if (TestIsBoat(vehicle, vehicleTypeCounts)) continue;
                if (TestIsBus(vehicle, vehicleTypeCounts)) continue;
                if (TestIsCar(vehicle, vehicleTypeCounts)) continue;
                if (TestIsMotorcycle(vehicle, vehicleTypeCounts)) continue;
            }
            return vehicleTypeCounts;
        }

        private bool TestIsAircraft(Vehicle vehicle, Dictionary<string, int> vehicleTypeCounts)
        {
            var aircraft = vehicle as Aircraft;
            if (aircraft != null)
            {
                IncrementOrAddKey("aircraft", vehicleTypeCounts);
                return true;
            }
            return false;
        }

        private bool TestIsBoat(Vehicle vehicle, Dictionary<string, int> vehicleTypeCounts)
        {
            var boat = vehicle as Boat;
            if (boat != null)
            {
                IncrementOrAddKey("boat", vehicleTypeCounts);
                return true;
            }
            return false;
        }
        private bool TestIsBus(Vehicle vehicle, Dictionary<string, int> vehicleTypeCounts)
        {
            var bus = vehicle as Bus;
            if (bus != null)
            {
                IncrementOrAddKey("bus", vehicleTypeCounts);
                return true;
            }
            return false;
        }
        private bool TestIsCar(Vehicle vehicle, Dictionary<string, int> vehicleTypeCounts)
        {
            var car = vehicle as Car;
            if (car != null)
            {
                IncrementOrAddKey("car", vehicleTypeCounts);
                return true;
            }
            return false;
        }
        private bool TestIsMotorcycle(Vehicle vehicle, Dictionary<string, int> vehicleTypeCounts)
        {
            var motorcycle = vehicle as Motorcycle;
            if (motorcycle != null)
            {
                IncrementOrAddKey("motorcycle", vehicleTypeCounts);
                return true;
            }
            return false;
        }

        private void IncrementOrAddKey(string key, Dictionary<string, int> vehicleTypeCounts)
        {
            if (vehicleTypeCounts.ContainsKey(key))
                vehicleTypeCounts[key]++;
            else
                vehicleTypeCounts.Add(key, 1);
        }

        public string[] ListVehicles()
        {
            if (garage == null)
                return null;
            var strings = new string[garage.Count];
            int i = 0;
            foreach (var vehicle in garage)
            {
                strings[i] = vehicle.ToString();
                i++;
            }
            return strings;
        }

        public bool UnparkVehicle(string registrationNumber) // Needs testing
        {
            if (garage == null)
                return false;
            Vehicle vehicle;
            if (garage.GetVehicleByRegistrationNumber(registrationNumber, out vehicle))
                return garage.UnparkVehicle(vehicle);
            else
                return false;
        }

        internal bool GarageExists()
        {
            return garage != null;
        }

        internal bool LoadGarageFromFile()
        {
            using (StreamReader sr = File.OpenText("garage.txt"))
            {
                var capacity = ReadSecondPartOfLineAsInt(sr);
                var vehicleCount = ReadSecondPartOfLineAsInt(sr);
                for (int i = 0; i < vehicleCount; i++)
                {
                    var typeString = sr.ReadLine();
                    var properties = vehicleTypes[typeString].GetProperties();
                    if (properties.Length == 10)
                    {
                        var dTO = new AircraftDTO();
                        populateDTO(dTO, properties);
                    }
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsAssignableFrom(UI.typeInteger))
                            ReadSecondPartOfLineAsInt(sr);

                    }
                }
                return true;
            }
        }

        private void populateDTO(AircraftDTO dTO, PropertyInfo[] properties)
        {
            
        }

        private static int ReadSecondPartOfLineAsInt(StreamReader sr)
        {
            return int.Parse(sr.ReadLine().Split(":")[1]);
        }

        internal bool SaveGarageToFile()
        {
            using (StreamWriter sw = File.CreateText("garage.txt"))
            {
                try
                {
                    sw.WriteLine($"Garage Capacity:{garage.Capacity}");
                    sw.WriteLine($"Vehicle Count:{garage.Count}");
                    foreach (var vehicle in garage)
                    {
                        sw.WriteLine(vehicle.GetType().Name);
                        foreach (var property in vehicle.GetType().GetProperties())
                        {
                            sw.WriteLine($"{property.Name}:{property.GetValue(vehicle)}");
                        }
                    }
                    return true;
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.Error.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }
}
