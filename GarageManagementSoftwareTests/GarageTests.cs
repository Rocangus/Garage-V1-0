using Microsoft.VisualStudio.TestTools.UnitTesting;
using GarageManagementSoftware.Vehicles;
using System;
using System.Collections.Generic;
using GarageManagementSoftware;

namespace GarageManagementSoftware.Tests
{
    [TestClass()]
    public class GarageTests
    {
        [TestMethod()]
        public void GarageCanBeCreatedTest()
        { 
            // Arrange
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            Garage<Vehicle> vehicles1 = new Garage<Vehicle>(10);
            Garage<Vehicle> vehicles2 = new Garage<Vehicle>(25);
            int expected, expected1, expected2;
            expected = 5;
            expected1 = 10;
            expected2 = 25;

            // Act

            // Assert
            Assert.IsNotNull(vehicles);
            Assert.IsNotNull(vehicles1);
            Assert.IsNotNull(vehicles2);
            Assert.AreEqual(expected, vehicles.Capacity);
            Assert.AreEqual(expected1, vehicles1.Capacity);
            Assert.AreEqual(expected2, vehicles2.Capacity);
        }

        [TestMethod]
        public void GarageCanHoldAircraft()
        {
            // Arrange 
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            Aircraft seidy = new Aircraft("SE-IDY", "White", 3, 610, "Piper", "PA-28-161 Warrior II", "PA28", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            Aircraft v22 = new Aircraft("168383", "Grey", 3, 15032, "Bell Boeing", "V-22 Osprey", "V22", AircraftType.Tiltrotor, AircraftEngineType.Turboprop, 2);
            Aircraft n53LH = new Aircraft("N53LH", "White", 3, 521, "Pitts", "Special", "PTS2", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            int count = 0;
            int expectedCount = 3;

            // Act
            vehicles.ParkVehicle(seidy);
            vehicles.ParkVehicle(v22);
            vehicles.ParkVehicle(n53LH);
            foreach (var item in vehicles)
            {
                if (item != null) count++;
            }

            // Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void GarageCanHoldDifferentVehicleTypes()
        {
            // Arrange 
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            Aircraft seidy = new Aircraft("SE-IDY", "White", 3, 610, "Piper", "PA-28-161 Warrior II", "PA28", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            Bus bus = new Bus("ERB321", "Grey", 4, 7350, 15);
            Motorcycle motorcycle = new Motorcycle("XYZ041", "Black", 2, 189, 599);
            Car car = new Car("ABC123", "Red", 4, 1250, CarPropulsionType.Gasoline);
            Boat boat = new Boat("SBR-485214", "Blue", 650, 4);
            int count = 0;
            int expectedCount = 5;

            // Act
            vehicles.ParkVehicle(seidy);
            vehicles.ParkVehicle(bus);
            vehicles.ParkVehicle(motorcycle);
            vehicles.ParkVehicle(car);
            vehicles.ParkVehicle(boat);
            foreach (var item in vehicles)
            {
                if (item != null)
                {
                    count++;
                    Console.WriteLine(item.ToString());
                }
            }

            // Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void UnparkVehiclePerformsCorrectly()
        {
            // Arrange 
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            Aircraft seidy = new Aircraft("SE-IDY", "White", 3, 610, "Piper", "PA-28-161 Warrior II", "PA28", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            Bus bus = new Bus("ERB321", "Grey", 4, 7350, 15);
            Motorcycle motorcycle = new Motorcycle("XYZ041", "Black", 2, 189, 599);
            Car car = new Car("ABC123", "Red", 4, 1250, CarPropulsionType.Gasoline);
            Boat boat = new Boat("SBR-485214", "Blue", 650, 4);
            int count = 0;
            int expectedCount = 4;

            // Act
            vehicles.ParkVehicle(seidy);
            vehicles.ParkVehicle(bus);
            vehicles.ParkVehicle(motorcycle);
            vehicles.ParkVehicle(car);
            vehicles.ParkVehicle(boat);
            vehicles.UnparkVehicle(seidy);
            foreach (var item in vehicles)
            {
                    count++;
                    Console.WriteLine(item.ToString());
            }

            // Assert
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void UnparkVehicleThroughGetByRegistrationNumberPerformsCorrectly()
        {
            // Arrange 
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            Aircraft seidy = new Aircraft("SE-IDY", "White", 3, 610, "Piper", "PA-28-161 Warrior II", "PA28", AircraftType.LandPlane, AircraftEngineType.Piston, 1);
            Bus bus = new Bus("ERB321", "Grey", 4, 7350, 15);
            Motorcycle motorcycle = new Motorcycle("XYZ041", "Black", 2, 189, 599);
            Car car = new Car("ABC123", "Red", 4, 1250, CarPropulsionType.Gasoline);
            Boat boat = new Boat("SBR-485214", "Blue", 650, 4);
            vehicles.ParkVehicle(seidy);
            vehicles.ParkVehicle(bus);
            vehicles.ParkVehicle(motorcycle);
            vehicles.ParkVehicle(car);
            vehicles.ParkVehicle(boat);
            int count = 0;
            int expectedCount = 4;

            // Act
            Vehicle vehicle;
            vehicles.GetVehicleByRegistrationNumber("se-idy", out vehicle);
            vehicles.UnparkVehicle(vehicle);
            foreach (var item in vehicles)
            {
                    count++;
                    Console.WriteLine(item.ToString());
            }

            // Assert
            Assert.AreEqual(expectedCount, count);
        }
    }
}