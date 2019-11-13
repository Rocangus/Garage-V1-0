using Microsoft.VisualStudio.TestTools.UnitTesting;
using GarageManagementSoftware.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles.Tests
{
    [TestClass()]
    public class VehiclePropertiesTests
    {
        [TestMethod()]
        public void GetPropertiesTest()
        {
            // Act
            var aircraft = VehicleProperties.GetProperties(0);
            var boat = VehicleProperties.GetProperties(1);
            var bus = VehicleProperties.GetProperties(2);
            var car = VehicleProperties.GetProperties(3);
            var motorcycle = VehicleProperties.GetProperties(4);
        }
    }
}