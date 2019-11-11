using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    // Number of wheels for an aircraft will instead be the number of landing gear assemblies. 
    // A general aviation aircraft often has a nosegear and two main gear for a total of three gear assemblies.
    public class Aircraft : Vehicle
    {
        public AircraftType TypeDescription { get; protected set; }
        public string  TypeDesignator { get; protected set; }
        public AircraftEngineType EngineType { get; protected set; }
        public string Manufacturer { get; protected set; }
        public string Model { get; protected set; }
        public int EngineCount { get; protected set; }

        public Aircraft(string regNumber, string colorOrLivery, int numberOfWheels, int emptyMass, string manufacturer, string model, string typeDesignator, 
                            AircraftType typeDescription, AircraftEngineType engineType, int engineCount) 
                            : base(regNumber, colorOrLivery, numberOfWheels, emptyMass)
        {
            Manufacturer = manufacturer;
            Model = model;
            TypeDesignator = typeDesignator;
            TypeDescription = typeDescription;
            EngineType = engineType;
            EngineCount = engineCount;
        }

        public string GetFullInfoAsString()
        {
            StringBuilder aircraftInformation = new StringBuilder();

            if (TypeDescription == AircraftType.Amphibian)
            {
                aircraftInformation.Append("an amphibian ");
            }
            else
            {
                aircraftInformation.Append($"a {Enum.GetName(typeof(AircraftType), TypeDescription)} ");
            }
            var engineCountString = EngineCount > 1 ? "engines" : "engine";
            aircraftInformation.Append($"with {EngineCount} {engineCountString} of type {Enum.GetName(typeof(AircraftEngineType), EngineType)}");
            return $"an aircraft, {Manufacturer} {Model}, registration {RegistrationNumber}, color {Color}, type designator {TypeDesignator}. It is {aircraftInformation.ToString()} with an empty mass of {EmptyMass}";
        }


    }
}
