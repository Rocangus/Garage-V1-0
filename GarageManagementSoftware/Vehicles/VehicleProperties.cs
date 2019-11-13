using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSoftware.Vehicles
{
    public static class VehicleProperties
    {
        private static Dictionary<int, PropertyInfo[]> vehicleTypeProperties = new Dictionary<int, PropertyInfo[]>();

        public static PropertyInfo[] GetProperties(int type)
        {
            PropertyInfo[] infos;

            if (!vehicleTypeProperties.ContainsKey(type))
            {
                vehicleTypeProperties[type] = GetType(type).GetProperties();
            }
            infos = vehicleTypeProperties[type];
            return infos;
        }
        private static Type GetType(int type)
        {
            switch (type)
            {
                case 0:
                    return typeof(Aircraft);
                case 1:
                    return typeof(Boat);
                case 2:
                    return typeof(Bus);
                case 3:
                    return typeof(Car);
                case 4:
                    return typeof(Motorcycle);
                default:
                    throw new ArgumentException($"Argument int type must be 0 <= type <= 4. Actual value: {type}");
            }
        }
    }
}
