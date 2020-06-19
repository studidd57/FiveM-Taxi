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
        private const float ARRIVE_RADIUS = 10f;

        private TaxiStatus taxiStatus;
        public Player Owner;

        public Taxi(Player owner, VehicleHash vehicle, PedHash driver, Vector3 position)
        {
            this.Owner = owner;
            CreateTaxi(vehicle, driver, position);
        }

        [Tick]
        private Task StatusTick()
        {
            if (this.vehicle.IsInRangeOf(destination, ARRIVE_RADIUS))
            {
                this.taxiStatus = TaxiStatus.ARRIVED;
            }
            return Task.FromResult(0);
        }

        private async void CreateTaxi(VehicleHash vehicle, PedHash driver, Vector3 position)
        {
            // Set up entities
            this.vehicle = await World.CreateVehicle(vehicle, position);
            this.driver = await World.CreatePed(driver, position);
            this.driver.SetIntoVehicle(this.vehicle, VehicleSeat.Driver);

            // Add object to Taxi Handler
            TaxiHandler.AddTaxiToHandler(this);
            this.taxiStatus = TaxiStatus.OPEN;

        }

        public void DriveTo(Vector3 position)
        {
            this.driver.Task.DriveTo(this.vehicle, position, ARRIVE_RADIUS, 20f, 4);
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
