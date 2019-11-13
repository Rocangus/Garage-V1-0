using System;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    internal class VehicleCreator
    {
        internal static bool CreateAircraft(string[] input, out Aircraft vehicle)
        {
            vehicle = null;
            AircraftType aircraftType = (AircraftType)Enum.Parse(typeof(AircraftType), input[7]);
            AircraftEngineType aircraftEngineType = (AircraftEngineType)Enum.Parse(typeof(AircraftEngineType), input[8]);
            vehicle = new Aircraft(input[0], input[1], int.Parse(input[2]), int.Parse(input[3]), input[4], input[5], input[6], aircraftType, aircraftEngineType, int.Parse(input[9]));
            return true;
        }
    }
}