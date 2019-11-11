using System;
using GarageManagementSoftware.Vehicles;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware
{
    public class Garage<T> where T : Vehicle
    {
        private Vehicle[] vehicles;
        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public Garage(int capacity) 
        {
            vehicles = new Vehicle[capacity];
            Count = 0;
        }

    }
}
