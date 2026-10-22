using System;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(
            float i_CurrentBatteryHours,
            float i_MaxBatteryHours)
            : base(i_CurrentBatteryHours, i_MaxBatteryHours)
        {
        }

        public void Recharge(float i_HoursToAdd)
        {
            if (i_HoursToAdd < 0)
            {
                throw new ArgumentException("Recharge hours must be non-negative");
            }

            if (m_CurrentEnergy + i_HoursToAdd > r_MaxEnergy)
            {
                throw new ValueRangeException(
                    0,
                    r_MaxEnergy - m_CurrentEnergy,
                    "Cannot recharge beyond maximum battery capacity");
            }

            m_CurrentEnergy += i_HoursToAdd;
        }
    }
}
