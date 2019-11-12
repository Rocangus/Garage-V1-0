using Microsoft.VisualStudio.TestTools.UnitTesting;
using GarageManagementSoftware;
using System;
using System.Collections.Generic;
using System.Text;
using GarageManagementSoftware.Vehicles;

namespace GarageManagementSoftware.Tests
{
    [TestClass()]
    public class GarageHandlerTests
    {
        [TestMethod()]
        public void ListVehicleTypeCountsTest()
        {
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
            var expected = new Dictionary<string, int>();
            var handler = new GarageHandler();
            handler.SetGarage(vehicles);
            expected.Add("aircraft", 1);
            expected.Add("bus", 1);
            expected.Add("motorcycle", 1);
            expected.Add("car", 1);
            expected.Add("boat", 1);

            // Act
            var actual = handler.GetVehicleTypeCounts();

            // Assert
            CollectionAssert.AreEquivalent(actual, expected);

        }

        [TestMethod()]
        public void ParkAircraftTest()
        {
            // Arrange
            string input = "aircraft, SE-IDY, White, 3, 610, Piper, PA-28-161 Warrior II, PA28, 0, 1, 1";
            string[] splitInput = input.Split(", ");
            GarageHandler handler = new GarageHandler();
            handler.NewGarage(1);

            // Act
            handler.ParkVehicle(splitInput);

            // Assert
            Assert.AreEqual(1, handler.GetGarageVehicleCount());
        }

        [TestMethod()]
        public void ParkBoatTest()
        {
            // Arrange
            string input = "boat, SBR-485214, Blue, 650, 4,34";
            string[] splitInput = input.Split(", ");
            GarageHandler handler = new GarageHandler();
            handler.NewGarage(1);

            // Act
            handler.ParkVehicle(splitInput);

            // Assert
            Assert.AreEqual(1, handler.GetGarageVehicleCount());
        }

        [TestMethod()]
        public void ParkBusTest()
        {
            // Arrange
            string input = "bus, ERB321, Grey, 4, 7350, 15";
            string[] splitInput = input.Split(", ");
            GarageHandler handler = new GarageHandler();
            handler.NewGarage(1);

            // Act
            handler.ParkVehicle(splitInput);

            // Assert
            Assert.AreEqual(1, handler.GetGarageVehicleCount());
        }

        [TestMethod()]
        public void ParkCarTest()
        {
            // Arrange
            string input = "car, ABC123, Red, 4, 1250, Gasoline";
            string[] splitInput = input.Split(", ");
            GarageHandler handler = new GarageHandler();
            handler.NewGarage(1);

            // Act
            handler.ParkVehicle(splitInput);

            // Assert
            Assert.AreEqual(1, handler.GetGarageVehicleCount());
        }

        [TestMethod()]
        public void ParkMotorcycleTest()
        {
            // Arrange
            string input = "motorcycle, XYZ041, Black, 2, 189, 599";
            string[] splitInput = input.Split(", ");
            GarageHandler handler = new GarageHandler();
            handler.NewGarage(1);

            // Act
            handler.ParkVehicle(splitInput);

            // Assert
            Assert.AreEqual(1, handler.GetGarageVehicleCount());
        }
    }
}