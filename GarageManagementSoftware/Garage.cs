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

        public bool GetVehicleByRegistrationNumber(string registrationNumber, out Vehicle vehicle)
        {
            foreach (var item in this)
            {
                if (item.RegistrationNumber.ToLower().Equals(registrationNumber.ToLower()))
                {
                    vehicle = item;
                    return true;
                }
            }
            vehicle = null;
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                var item = (T) vehicles[i];
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void SetCapacity(int v)
        {
            if (v > vehicles.Length)
            {
                IncreaseBackingArraySize(v);
            }
            Capacity = v;
        }

        private void IncreaseBackingArraySize(int v)
        {
            var temp = new Vehicle[v];
            for (int i = 0; i < Count; i++)
            {
                temp[i] = vehicles[i];
            }
            vehicles = temp;
        }
    }
}
