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
        private Vector3 destination;

        private TaxiStatus taxiStatus;
        private Player owner;

        public Taxi(Player owner, VehicleHash vehicle, PedHash driver, Vector3 position)
        {
            this.owner = owner;
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
            taxiStatus = TaxiStatus.OPEN;

        }

        public void DriveTo(Vector3 position)
        {
            this.driver.Task.DriveTo(this.vehicle, position, 10f, 20f, 4);
            this.destination = position;
            taxiStatus = TaxiStatus.ENROUTE;
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

        public TaxiStatus Status
        {
            get { return taxiStatus; }
        }
    }
}
