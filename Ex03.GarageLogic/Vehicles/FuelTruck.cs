namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck
    {
        public FuelTruck(string i_LicenseID, string i_ModelName)
            : base(i_ModelName, i_LicenseID, 0)
        {
        }
        public override void Refuel(eFuelType i_FuelType, float i_FuelAmount)
        {
            ((FuelEnergy)r_EnergySource).Refuel(i_FuelType, i_FuelAmount);
        }

    }
}
