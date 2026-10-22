namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumOfWheels = 5;
        private const float k_MaxWheelAirPressure = 33f;

        protected eCarColor m_Color;
        protected eNumOfDoors m_NumOfDoors;

        protected Car(
            string i_ModelName,
            string i_LicenseNumber,
            EnergySource i_EnergySource,
            string i_WheelManufacturer)
            : base(
                i_ModelName,
                i_LicenseNumber,
                i_EnergySource,
                k_NumOfWheels,
                k_MaxWheelAirPressure,
                i_WheelManufacturer)
        {
        }
        public eCarColor Color
        {
            get { return m_Color; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
        }

        public void SetCarDetails(eCarColor i_Color, eNumOfDoors i_NumOfDoors)
        {
            m_Color = i_Color;
            m_NumOfDoors = i_NumOfDoors;
        }

    }
}
