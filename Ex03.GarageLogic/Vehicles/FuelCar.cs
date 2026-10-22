namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelAmount = 47f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const string k_WheelManufacturer = "Default";

        public FuelCar(string i_LicenseID, string i_ModelName)
            : base(
                i_ModelName,
                i_LicenseID,
                new FuelEnergy(0, k_MaxFuelAmount, k_FuelType),
                k_WheelManufacturer)
        {
        }
        public override void Refuel(eFuelType i_FuelType, float i_FuelAmount)
        {
            ((FuelEnergy)r_EnergySource).Refuel(i_FuelType, i_FuelAmount);
        }

    }
}
