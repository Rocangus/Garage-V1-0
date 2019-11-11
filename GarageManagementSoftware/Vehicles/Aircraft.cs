using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class Aircraft : Vehicle
    {
        public AircraftType TypeDescription { get; protected set; }
        public string  TypeDesignator { get; protected set; }
        public AircraftEngineType EngineType { get; protected set; }
        public string Manufacturer { get; protected set; }
        public string Model { get; protected set; }

        public Aircraft(string regnumber, string color, int numberofwheels, int emptymass) : base(regnumber, color, numberofwheels, emptymass)
        {

        }
    }
}
