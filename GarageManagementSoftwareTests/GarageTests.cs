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
            Garage<Vehicle> vehicles = new Garage<Vehicle>(5);
            
        } 
    }
}