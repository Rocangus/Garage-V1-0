﻿using System;
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
    }
}