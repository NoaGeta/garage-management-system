using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, GarageVehicle> r_Vehicles;

        public Garage()
        {
            r_Vehicles = new Dictionary<string, GarageVehicle>();
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public void AddNewVehicle(GarageVehicle i_GarageVehicle)
        {
            string licenseNumber = i_GarageVehicle.Vehicle.LicenseNumber;

            if (r_Vehicles.ContainsKey(licenseNumber))
            {
                throw new ArgumentException("Vehicle already exists in garage");
            }

            r_Vehicles.Add(licenseNumber, i_GarageVehicle);
        }

        public void ChangeVehicleStatus(
            string i_LicenseNumber,
            eVehicleStatus i_NewStatus)
        {
            GarageVehicle garageVehicle = getGarageVehicle(i_LicenseNumber);
            garageVehicle.VehicleStatus = i_NewStatus;
        }

        public List<string> GetLicenseNumbers(eVehicleStatus? i_FilterStatus)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (KeyValuePair<string, GarageVehicle> pair in r_Vehicles)
            {
                if (i_FilterStatus == null ||
                   pair.Value.VehicleStatus == i_FilterStatus)
                {
                    licenseNumbers.Add(pair.Key);
                }
            }

            return licenseNumbers;
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            GarageVehicle garageVehicle = getGarageVehicle(i_LicenseNumber);
            garageVehicle.Vehicle.InflateAllWheelsToMax();
        }

        public void RefuelVehicle(
            string i_LicenseNumber,
            eFuelType i_FuelType,
            float i_FuelAmount)
        {
            GarageVehicle garageVehicle = getGarageVehicle(i_LicenseNumber);
            garageVehicle.Vehicle.Refuel(i_FuelType, i_FuelAmount);
        }

        public void RechargeVehicle(
            string i_LicenseNumber,
            float i_HoursToAdd)
        {
            GarageVehicle garageVehicle = getGarageVehicle(i_LicenseNumber);
            garageVehicle.Vehicle.Recharge(i_HoursToAdd);
        }

        public GarageVehicle getGarageVehicle(string i_LicenseNumber)
        {
            if (!r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle does not exist in garage");
            }

            return r_Vehicles[i_LicenseNumber];
        }
        public Vehicle GetVehicle(string i_LicenseNumber)
        {
            if (!r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle does not exist in garage");
            }

            return r_Vehicles[i_LicenseNumber].Vehicle;
        }

    }
}
