using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public class Car : Vehicle
    {
        public CarPropulsionType PropulsionType { get; private set; }
        public Car(string registrationNumber, string color, int numberOfWheels, int emptyMass, CarPropulsionType propulsionType):base(registrationNumber, color, numberOfWheels, emptyMass)
        {
            PropulsionType = propulsionType;
        }

        public override string ToString()
        {
            // ToDo: Implement static class instead of enum
            string propulsion = "";
            switch (PropulsionType)
            {
                case CarPropulsionType.Gasoline:
                    propulsion = "gasoline";
                    break;
                case CarPropulsionType.Diesel:
                    propulsion = "diesel";
                    break;
                case CarPropulsionType.GasolineHybrid:
                    propulsion = "gasoline hybrid";
                    break;
                case CarPropulsionType.DieselHybrid:
                    propulsion = "diesel hybrid";
                    break;
                case CarPropulsionType.GasolinePlugInHybrid:
                    propulsion = "gasoline plug-in hybrid";
                    break;
                case CarPropulsionType.DieselPlugInHybrid:
                    propulsion = "diesel plug-in hybrid";
                    break;
                case CarPropulsionType.Electric:
                    propulsion = "electric";
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(propulsion))
            {
                return base.ToString() + ", a car";
            }
            else 
                return base.ToString() + $", a {propulsion} car";
        }
    }
}
