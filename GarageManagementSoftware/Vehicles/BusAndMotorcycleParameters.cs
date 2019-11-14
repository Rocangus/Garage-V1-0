using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class BusAndMotorcycleParameters : Tuple<string, string, int, int, int>
    {
        public BusAndMotorcycleParameters(string registrationNumber, string color, int emptyWeight, int numberOfWheels, int passengersOrCylinderVolume) 
            : base(registrationNumber, color, emptyWeight, numberOfWheels, passengersOrCylinderVolume)
        { 
        }
    }
}
