namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private const float k_MaxWheelAirPressure = 26f;
        private const float k_MaxFuelAmount = 140f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const string k_WheelManufacturer = "Default";

        private bool m_IsCarryingHazardousMaterials;
        private float m_CargoVolume;

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            float i_CurrentFuelAmount)
            : base(
                i_ModelName,
                i_LicenseNumber,
                new FuelEnergy(
                    i_CurrentFuelAmount,
                    k_MaxFuelAmount,
                    k_FuelType),
                k_NumOfWheels,
                k_MaxWheelAirPressure,
                k_WheelManufacturer)
        {
        }

        public bool IsCarryingHazardousMaterials
        {
            get { return m_IsCarryingHazardousMaterials; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
        }

        public void SetTruckDetails(
            bool i_IsHazardous,
            float i_CargoVolume)
        {
            m_IsCarryingHazardousMaterials = i_IsHazardous;
            m_CargoVolume = i_CargoVolume;
        }

        public override void Refuel(eFuelType i_FuelType, float i_FuelAmount)
        {
            ((FuelEnergy)r_EnergySource).Refuel(i_FuelType, i_FuelAmount);
        }
    }
}
