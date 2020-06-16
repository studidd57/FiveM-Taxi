using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FiveM_Taxi.Client
{
    public class Taxi
    {
        private Vehicle vehicle;
        private Ped driver;

        public Taxi(VehicleHash vehicle, PedHash driver, Vector3 position)
        {
            CreateTaxi(vehicle, driver, position);
        }

        private async void CreateTaxi(VehicleHash vehicle, PedHash driver, Vector3 position)
        {
            // Set up entities
            this.vehicle = await World.CreateVehicle(vehicle, position);
            this.driver = await World.CreatePed(driver, position);
            this.driver.Task.WarpIntoVehicle(this.vehicle, VehicleSeat.Driver);

            // Add object to Taxi Handler
            TaxiHandler.AddTaxiToHandler(this);
        }

        public Vehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }

        public Ped Driver
        {
            get { return Driver; }
            set { Driver = value; }
        }
    }
}
