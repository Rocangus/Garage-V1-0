using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class Bus : Vehicle
    {
        public int PassengerCapacity { get; private set; }
        public Bus(string registrationNumber, string color, int numberOfWheels, int emptyMass, int passengerCapacity) : base(registrationNumber, color, numberOfWheels, emptyMass)
        {
            PassengerCapacity = passengerCapacity;
        }

        public override string ToString()
        {
            return base.ToString() + ", a bus";
        }
    }
}
