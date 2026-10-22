using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        protected readonly List<Wheel> r_Wheels;
        protected readonly EnergySource r_EnergySource;

        protected Vehicle(
            string i_ModelName,
            string i_LicenseNumber,
            EnergySource i_EnergySource,
            int i_NumOfWheels,
            float i_MaxWheelAirPressure,
            string i_WheelManufacturer)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EnergySource = i_EnergySource;
            r_Wheels = new List<Wheel>(i_NumOfWheels);

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                r_Wheels.Add(
                    new Wheel(
                        i_WheelManufacturer,
                        0,
                        i_MaxWheelAirPressure));
            }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public float EnergyPercentage
        {
            get { return r_EnergySource.EnergyPercentage; }
        }
        public float MaxEnergy
        {
            get { return r_EnergySource.MaxEnergy; }
        }
        public IReadOnlyList<Wheel> Wheels
        {
            get { return r_Wheels; }
        }
        public void InflateWheelsTo(float i_AirPressure)
        {
            foreach (Wheel wheel in r_Wheels)
            {
                if (i_AirPressure > wheel.MaxAirPressure)
                {
                    throw new ValueRangeException(
                        0,
                        wheel.MaxAirPressure,
                        "Cannot inflate wheel beyond maximum air pressure");
                }

                float airToAdd = i_AirPressure - wheel.CurrentAirPressure;

                if (airToAdd < 0)
                {
                    throw new ArgumentException("Air pressure cannot be reduced");
                }

                wheel.Inflate(airToAdd);
            }
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.Inflate(airToAdd);
            }
        }
        public virtual void Refuel(eFuelType i_FuelType, float i_FuelAmount)
        {
            throw new ArgumentException("Vehicle does not support refueling");
        }
        public virtual void Recharge(float i_HoursToAdd)
        {
            throw new ArgumentException("Vehicle does not support recharging");
        }
        public void LoadEnergyByPercentage(float i_Percentage)
        {
            if (i_Percentage < 0 || i_Percentage > 100)
            {
                throw new ArgumentException("Energy percentage must be between 0 and 100");
            }

            float absoluteEnergy = r_EnergySource.MaxEnergy * (i_Percentage / 100f);

            if (r_EnergySource is FuelEnergy fuel)
            {
                fuel.Refuel(fuel.FuelType, absoluteEnergy);
            }
            else if (r_EnergySource is ElectricEnergy electric)
            {
                electric.Recharge(absoluteEnergy);
            }
        }
    }
}
