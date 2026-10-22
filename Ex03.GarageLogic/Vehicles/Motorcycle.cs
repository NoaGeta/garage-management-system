namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxWheelAirPressure = 29f;

        protected eLicenseType m_LicenseType;
        protected int m_EngineVolume;

        protected Motorcycle(
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
        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
        }

        public void SetMotorcycleDetails(
            eLicenseType i_LicenseType,
            int i_EngineVolume)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }
    }
}
