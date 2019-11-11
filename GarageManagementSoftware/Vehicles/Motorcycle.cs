using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; private set; }
        public Motorcycle(string regNumber, string color, int noOfWheels, int emptyMass, int cylinderVolume ) : base(regNumber, color, noOfWheels, emptyMass)
        {
            CylinderVolume = cylinderVolume;
        }

        public override string ToString()
        {
            return base.ToString() + ", a motorcycle";
        }
    }
}
