using System;
using System.IO;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        private static Garage s_Garage = new Garage();

        private enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar = 2,
            FuelMotorcycle = 3,
            ElectricMotorcycle = 4,
            FuelTruck = 5
        }

        public static void Main()
        {
            runGarageSystem();
        }

        private static void runGarageSystem()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                printMenu();
                string userInput = Console.ReadLine();

                try
                {
                    handleUserChoice(userInput, ref isRunning);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (isRunning)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        private static void printMenu()
        {
            Console.WriteLine("=== Garage Management System ===");
            Console.WriteLine("1. Load vehicles from file");
            Console.WriteLine("2. Insert new vehicle");
            Console.WriteLine("3. Display license numbers");
            Console.WriteLine("4. Change vehicle status");
            Console.WriteLine("5. Inflate wheels to max");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Recharge electric vehicle");
            Console.WriteLine("8. Display full vehicle info");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
        }

   
        private static void handleUserChoice(string i_Input, ref bool io_IsRunning)
        {
            switch (i_Input)
            {
                case "1":
                    loadVehiclesFromFile();
                    break;
                case "2":
                    insertVehicle();
                    break;
                case "3":
                    displayLicenseNumbers();
                    break;
                case "4":
                    changeVehicleStatus();
                    break;
                case "5":
                    inflateWheels();
                    break;
                case "6":
                    refuelVehicle();
                    break;
                case "7":
                    rechargeVehicle();
                    break;
                case "8":
                    displayVehicleInfo();
                    break;
                case "0":
                    io_IsRunning = false;
                    break;
                default:
                    throw new ArgumentException("Invalid menu choice");
            }
        }

        // ================= Option 1 =================

        private static void loadVehiclesFromFile()
        {
            Console.Write("Enter full path to vehicles file: ");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist");
            }

            string[] lines = File.ReadAllLines(filePath);
            int loadedCount = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) ||
                   line.StartsWith("*") ||
                   line.StartsWith("THE FORMAT"))
                {
                    continue;
                }

                try
                {
                    loadVehicleFromCsvLine(line);
                    loadedCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load line:");
                    Console.WriteLine(line);
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"{loadedCount} vehicles loaded successfully.");
        }
        private static void loadVehicleFromCsvLine(string i_Line)
        {
            string[] p = i_Line.Split(',');

            if (p.Length < 10)
            {
                throw new FormatException("Invalid file format");
            }

            string vehicleType = p[0];
            string license = p[1];
            string model = p[2];
            float energyPercent = float.Parse(p[3]);
            string wheelManufacturer = p[4];
            float wheelAirPressure = float.Parse(p[5]);
            string ownerName = p[6];
            string ownerPhone = p[7];

            if (s_Garage.IsVehicleInGarage(license))
            {
                s_Garage.ChangeVehicleStatus(license, eVehicleStatus.InRepair);
                return;
            }

            if (vehicleType == "Truck")
            {
                vehicleType = "FuelTruck";
            }

            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, license, model);

            switch (vehicleType)
            {
                case "FuelMotorcycle":
                case "ElectricMotorcycle":
                    ((Motorcycle)vehicle).SetMotorcycleDetails(
                        (eLicenseType)Enum.Parse(typeof(eLicenseType), p[8]),
                        int.Parse(p[9]));
                    break;

                case "FuelCar":
                case "ElectricCar":
                    ((Car)vehicle).SetCarDetails(
                        (eCarColor)Enum.Parse(typeof(eCarColor), p[8]),
                        (eNumOfDoors)Enum.Parse(typeof(eNumOfDoors), p[9]));
                    break;

                case "Truck":
                case "FuelTruck":
                    ((Truck)vehicle).SetTruckDetails(
                        bool.Parse(p[8]),
                        float.Parse(p[9]));
                    break;

                default:
                    throw new ArgumentException("Unknown vehicle type");
            }

            vehicle.LoadEnergyByPercentage(energyPercent);

            vehicle.InflateWheelsTo(wheelAirPressure);

            GarageVehicle garageVehicle =
                new GarageVehicle(vehicle, ownerName, ownerPhone);

            s_Garage.AddNewVehicle(garageVehicle);
        }

        // ================= Option 2 =================

        private static void insertVehicle()
        {
            string license = getString("Enter license number: ");

            if (s_Garage.IsVehicleInGarage(license))
            {
                s_Garage.ChangeVehicleStatus(license, eVehicleStatus.InRepair);
                Console.WriteLine("Vehicle already exists. Status set to InRepair.");
                return;
            }

            eVehicleType type = getEnum<eVehicleType>("Choose vehicle type");

            string model = getString("Enter model name: ");
            string owner = getString("Enter owner name: ");
            string phone = getString("Enter owner phone: ");

            Vehicle vehicle = VehicleCreator.CreateVehicle(type.ToString(), license, model);
            GarageVehicle garageVehicle = new GarageVehicle(vehicle, owner, phone);
            s_Garage.AddNewVehicle(garageVehicle);

            fillVehicleDetails(vehicle, type);

            Console.WriteLine("Vehicle successfully added.");
        }

        private static void fillVehicleDetails(Vehicle i_Vehicle, eVehicleType i_Type)
        {
            switch (i_Type)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    ((Car)i_Vehicle).SetCarDetails(
                        getEnum<eCarColor>("Choose car color"),
                        getEnum<eNumOfDoors>("Choose number of doors"));
                    break;

                case eVehicleType.FuelMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    ((Motorcycle)i_Vehicle).SetMotorcycleDetails(
                        getEnum<eLicenseType>("Choose license type"),
                        getInt("Enter engine volume: "));
                    break;

                case eVehicleType.FuelTruck:
                    ((Truck)i_Vehicle).SetTruckDetails(
                        getBool("Carrying hazardous materials (y/n): "),
                        getFloat("Enter cargo volume: "));
                    break;
            }

            if (i_Type == eVehicleType.FuelCar ||
               i_Type == eVehicleType.FuelMotorcycle ||
               i_Type == eVehicleType.FuelTruck)
            {
                i_Vehicle.Refuel(
                    getEnum<eFuelType>("Choose fuel type"),
                    getFloat("Enter fuel amount: "));
            }
            else
            {
                i_Vehicle.Recharge(getFloat("Enter battery hours: "));
            }

            i_Vehicle.InflateAllWheelsToMax();
        }

        // ================= Option 3 =================

        private static void displayLicenseNumbers()
        {
            Console.WriteLine("Filter by status?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            string input = Console.ReadLine();
            eVehicleStatus? filter = null;

            if (input == "1")
            {
                filter = getEnum<eVehicleStatus>("Choose status");
            }

            var licenses = s_Garage.GetLicenseNumbers(filter);

            if (licenses.Count == 0)
            {
                Console.WriteLine("No vehicles found.");
                return;
            }

            foreach (string license in licenses)
            {
                Console.WriteLine(license);
            }
        }

        // ================= Option 4 =================

        private static void changeVehicleStatus()
        {
            string license = getString("Enter license number: ");
            eVehicleStatus status = getEnum<eVehicleStatus>("Choose new status");
            s_Garage.ChangeVehicleStatus(license, status);
            Console.WriteLine("Status updated.");
        }

        // ================= Option 5 =================

        private static void inflateWheels()
        {
            string license = getString("Enter license number: ");
            s_Garage.InflateWheelsToMax(license);
            Console.WriteLine("Wheels inflated to maximum.");
        }

        // ================= Option 6 =================

        private static void refuelVehicle()
        {
            string license = getString("Enter license number: ");

            try
            {
                eFuelType fuelType = getEnum<eFuelType>("Choose fuel type");
                float amount = getFloat("Enter fuel amount: ");
                s_Garage.RefuelVehicle(license, fuelType, amount);
                Console.WriteLine("Vehicle refueled successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // ================= Option 7 =================

        private static void rechargeVehicle()
        {
            string license = getString("Enter license number: ");

            try
            {
                float hours = getFloat("Enter hours to recharge: ");
                s_Garage.RechargeVehicle(license, hours);
                Console.WriteLine("Vehicle recharged successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // ================= Option 8 =================

        private static void displayVehicleInfo()
        {
            string license = getString("Enter license number: ");

            GarageVehicle garageVehicle = s_Garage.getGarageVehicle(license);
            Vehicle vehicle = garageVehicle.Vehicle;

            Console.WriteLine($"License Number: {vehicle.LicenseNumber}");
            Console.WriteLine($"Model Name: {vehicle.ModelName}");
            Console.WriteLine($"Owner Name: {garageVehicle.OwnerName}");
            Console.WriteLine($"Owner Phone: {garageVehicle.OwnerPhone}");
            Console.WriteLine($"Vehicle Status: {garageVehicle.VehicleStatus}");
            Console.WriteLine($"Energy Percentage: {vehicle.EnergyPercentage}%");

            Console.WriteLine($"Vehicle Type: {vehicle.GetType().Name}");

            if (vehicle is FuelCar || vehicle is FuelMotorcycle || vehicle is FuelTruck)
            {
                Console.WriteLine("Energy Type: Fuel");
            }
            else
            {
                Console.WriteLine("Energy Type: Electric");
            }

            Console.WriteLine("Wheels:");
            foreach (Wheel wheel in vehicle.Wheels)
            {
                Console.WriteLine(
                    $"- Manufacturer: {wheel.ManufacturerName}, Pressure: {wheel.CurrentAirPressure}/{wheel.MaxAirPressure}");
            }

            if (vehicle is Car car)
            {
                Console.WriteLine($"Car Color: {car.Color}");
                Console.WriteLine($"Number of Doors: {car.NumOfDoors}");
            }
            else if (vehicle is Motorcycle motorcycle)
            {
                Console.WriteLine($"License Type: {motorcycle.LicenseType}");
                Console.WriteLine($"Engine Volume: {motorcycle.EngineVolume}");
            }
            else if (vehicle is Truck truck)
            {
                Console.WriteLine($"Carrying Hazardous Materials: {truck.IsCarryingHazardousMaterials}");
                Console.WriteLine($"Cargo Volume: {truck.CargoVolume}");
            }
        }


        // ================= Helpers =================

        private static string getString(string i_Message)
        {
            Console.Write(i_Message);
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be empty");
            }
            return input;
        }

        private static int getInt(string i_Message)
        {
            Console.Write(i_Message);
            return int.Parse(Console.ReadLine());
        }

        private static float getFloat(string i_Message)
        {
            Console.Write(i_Message);
            return float.Parse(Console.ReadLine());
        }

        private static bool getBool(string i_Message)
        {
            Console.Write(i_Message);
            string input = Console.ReadLine().ToLower();
            return input == "y" || input == "yes";
        }

        private static T getEnum<T>(string i_Message) where T : struct
        {
            Console.WriteLine(i_Message + ":");
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{(int)value}. {value}");
            }
            return (T)Enum.ToObject(typeof(T), int.Parse(Console.ReadLine()));
        }
    }
}
