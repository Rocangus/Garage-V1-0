using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    public class AircraftDTO
    {
        public string RegistrationNumber { get; set; }
        public string ColorOrLivery { get; set; }
        public int NumberOfLandingGearAssemblies { get; set; }
        public int EmptyMass { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string TypeDesignator { get; set; }
        public AircraftType AircraftType { get; set; }
        public AircraftEngineType EngineType { get; set; }
        public int EngineCount { get; set; }
    }
}