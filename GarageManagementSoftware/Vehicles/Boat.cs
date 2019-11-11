using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class Boat : Vehicle
    {
        public decimal Length { get; private set; }
        public Boat(string registrationNumber, string color, int emptyMass, decimal length) : base(registrationNumber, color, 0, emptyMass)
        {
            Length = length;
        }

        public override string ToString()
        {
            return base.ToString() + $", a boat {Length:0.##} meters long";
        }
    }
}
