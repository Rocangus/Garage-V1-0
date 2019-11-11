using System;
using GarageManagementSoftware.Vehicles;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GarageManagementSoftware
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private Vehicle[] vehicles;
        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public Garage(int capacity) 
        {
            vehicles = new Vehicle[capacity];
            Capacity = capacity;
            Count = 0;
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            if(Count<Capacity)
            {
                vehicles[Count] = vehicle;
                Count++;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in vehicles)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
