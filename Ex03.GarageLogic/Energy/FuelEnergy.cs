using System;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public FuelEnergy(
            float i_CurrentFuelAmount,
            float i_MaxFuelAmount,
            eFuelType i_FuelType)
            : base(i_CurrentFuelAmount, i_MaxFuelAmount)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public void Refuel(eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Incorrect fuel type");
            }

            if (i_FuelToAdd < 0)
            {
                throw new ArgumentException("Fuel amount must be non-negative");
            }

            if (m_CurrentEnergy + i_FuelToAdd > r_MaxEnergy)
            {
                throw new ValueRangeException(
                    0,
                    r_MaxEnergy - m_CurrentEnergy,
                    "Cannot refuel beyond tank capacity");
            }

            m_CurrentEnergy += i_FuelToAdd;
        }
    }
}
