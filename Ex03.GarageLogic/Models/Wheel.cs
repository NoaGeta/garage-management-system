using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            if (i_CurrentAirPressure < 0 || i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ValueRangeException(0, i_MaxAirPressure);
            }

            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void Inflate(float i_AirToAdd)
        {
            if (i_AirToAdd < 0)
            {
                throw new ArgumentException("Air to add must be non-negative");
            }

            if (m_CurrentAirPressure + i_AirToAdd > r_MaxAirPressure)
            {
                throw new ValueRangeException(
                    0,
                    r_MaxAirPressure - m_CurrentAirPressure,
                    "Cannot inflate wheel beyond maximum air pressure");
            }

            m_CurrentAirPressure += i_AirToAdd;
        }
    }
}
