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
    }
}