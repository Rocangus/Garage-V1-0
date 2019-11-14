using System;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware
{
    internal class VehicleCreator
    {
        internal static bool CreateAircraft(AircraftDTO input, out Aircraft vehicle)
        {
            vehicle = null;
            try
            {
                vehicle = new Aircraft(input.RegistrationNumber, input.ColorOrLivery, input.NumberOfLandingGearAssemblies,
                    input.EmptyMass, input.Manufacturer, input.Model, input.TypeDesignator, input.AircraftType, input.EngineType, input.EngineCount);
                return true;
            } catch (NullReferenceException e)
            {
                Console.Error.Write(e.Message);
                return false;
            }

        }
        internal static bool CreateBoat(Tuple<string, string, int, decimal> input, out Boat boat)
        {
            boat = null;
            try
            {
                boat = new Boat(registrationNumber: input.Item1, color: input.Item2, emptyMass: input.Item3, length: input.Item4);
                return true;
            } catch(NullReferenceException e)
            {
                Console.Error.Write(e.Message);
                return false;
            }
        }

        internal static bool CreateBus(BusAndMotorcycleParameters input, out Bus bus)
        {
            bus = null;
            try
            {
                bus = new Bus(input.Item1, input.Item2, input.Item3, input.Item4, input.Item5);
                return true;
            }
            catch (NullReferenceException e)
            {
                Console.Error.Write(e.Message);
                return false;
            }

        }

        internal static bool CreateCar(Tuple<string, string, int, int, CarPropulsionType> carArguments, out Car car)
        {
            car = null;
            try
            {
                car = new Car(carArguments.Item1, carArguments.Item2, carArguments.Item3, carArguments.Item4, carArguments.Item5);
                return true;
            }
            catch (NullReferenceException e)
            {
                Console.Error.Write(e.Message);
                return false;
            }
        }

        internal static bool CreateMotorcycle(BusAndMotorcycleParameters input, out Motorcycle motorcycle)
        {
            motorcycle = null;
            try
            {
                motorcycle = new Motorcycle(input.Item1, input.Item2, input.Item3, input.Item4, input.Item5);
                return true;
            }
            catch (NullReferenceException e)
            {
                Console.Error.Write(e.Message);
                return false;
            }
        }
    }
}