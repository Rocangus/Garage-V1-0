﻿using GarageManagementSoftware.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware
{
    public class GarageHandler
    {
        public string ErrorMessage { get; set; }
        private Garage<Vehicle> garage;
        public GarageHandler()
        {
            
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

        public bool ParkVehicle(string[] input)
        {
            if (garage == null)
                return false;
            else
            {
                var vehicleType = input[0].ToLower();
                switch (vehicleType)
                {
                    case "aircraft":
                        return ParkAircraft(input);
                    default:
                        return false;
                }
            }
        }

        private bool ParkAircraft(string[] input)
        {
            {
                if (input.Length != 11)
                {
                    ErrorMessage = $"To add an aircraft you need to specify 11 values. {input.Length} values were found.";
                    return false;
                }
            }
            int numberOfWheels, emptyMass, engineCount;
            AircraftType aircraftType = (AircraftType)Enum.Parse(typeof(AircraftType), input[8]);
            AircraftEngineType aircraftEngineType = (AircraftEngineType)Enum.Parse(typeof(AircraftEngineType), input[9]);
            int.TryParse(input[10], out engineCount);
            if (!int.TryParse(input[3], out numberOfWheels) || !int.TryParse(input[4], out emptyMass) || !int.TryParse(input[10], out engineCount))
            {
                ErrorMessage = "Please make sure to enter numbers where requested.";
                return false;
            }
            var vehicle = new Aircraft(input[1], input[2], numberOfWheels, emptyMass, input[5], input[6], input[7], aircraftType, aircraftEngineType, engineCount);
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
    }
}