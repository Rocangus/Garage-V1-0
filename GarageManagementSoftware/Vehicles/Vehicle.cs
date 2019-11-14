using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public abstract class Vehicle
    {
        public string RegistrationNumber { get; protected set; }

        public string Color { get; protected set; }
        public int NumberOfWheels { get; protected set; }
        public int EmptyMass { get; set; }

        public Vehicle(string regnumber, string color, int numberofwheels, int emptymass)
        {
            RegistrationNumber = regnumber;
            Color = color;
            NumberOfWheels = numberofwheels;
            EmptyMass = emptymass;
        }

        /// <summary>
        /// Returns the Registration number as the string representation of the Vehicle.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RegistrationNumber;
        }

        /// <summary>
        /// Equality based on registration number.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Vehicle))
            {
                return false;
            }
            var vehicle = obj as Vehicle;
            return RegistrationNumber.ToLower().Equals(vehicle.RegistrationNumber.ToLower());
        }

        public override int GetHashCode()
        {
            return RegistrationNumber.GetHashCode();
        }
    }
} 
