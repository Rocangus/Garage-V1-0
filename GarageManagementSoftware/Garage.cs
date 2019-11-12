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

        public bool UnparkVehicle(Vehicle toUnpark)
        {
            for(int i = 0; i < Count; i++)
            {
                var vehicle = vehicles[i];
                if (vehicle.Equals(toUnpark))
                {
                    vehicles[i] = null;
                    ShiftRemainingItemsLeft(i);
                    Count--;
                    return true;
                }
            }
            return false;
        }

        private void ShiftRemainingItemsLeft(int i)
        {
            while(i+1 < Count)
            {
                if (vehicles[i+1] != null)
                {
                    vehicles[i] = vehicles[i + 1];
                    i++;
                }
                else
                    break;
            }
            if (i < Count) vehicles[i] = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in vehicles)
            {
                if (item != null) 
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
