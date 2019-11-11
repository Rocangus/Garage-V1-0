using System;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            Aircraft aircraft = new Aircraft("SE-IDY", "White", 3, 610, "Piper", "PA-28-161 Warrior II", "PA28", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            Console.WriteLine(aircraft.ToString());
        }
    }
}
