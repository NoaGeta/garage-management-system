namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryHours = 2.6f;
        private const string k_WheelManufacturer = "Default";

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName)
            : base(
                i_ModelName,
                i_LicenseID,
                new ElectricEnergy(0, k_MaxBatteryHours),
                k_WheelManufacturer)
        {
        }
        public override void Recharge(float i_HoursToAdd)
        {
            ((ElectricEnergy)r_EnergySource).Recharge(i_HoursToAdd);
        }

    }
}
