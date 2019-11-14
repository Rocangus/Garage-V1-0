using System;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    internal class VehicleCreator
    {
        internal static bool CreateAircraft(AircraftDTO input, out Aircraft vehicle)
        {
            vehicle = null;
            vehicle = new Aircraft(input.RegistrationNumber, input.ColorOrLivery, input.NumberOfLandingGearAssemblies,
                input.EmptyMass, input.Manufacturer, input.Model, input.TypeDesignator, input.AircraftType, input.EngineType, input.EngineCount);
            return true;
        }
    }
}