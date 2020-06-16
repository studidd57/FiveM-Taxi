using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FiveM_Taxi.Client
{
    public class TaxiHandler : BaseScript
    {
        private static List<Taxi> activeTaxis = new List<Taxi>();

        public TaxiHandler()
        {
            // Event handlers for events pertaining to the TaxiHandler
            EventHandlers["FiveM-Taxi:CreateTaxi"] += new Action(CreateTaxi);
        }

        public static void CreateTaxi()
        {
            Taxi createdTaxi = new Taxi(Game.Player, VehicleHash.Taxi, PedHash.Business01AFY, World.GetNextPositionOnStreet(Game.PlayerPed.Position * 1.5f, true));
            createdTaxi.DriveTo(World.GetNextPositionOnStreet(Game.PlayerPed.Position, true));
            createdTaxi.Vehicle.AttachBlip();
        }

        public static void AddTaxiToHandler(Taxi taxi)
        {
            activeTaxis.Add(taxi);
        }

        [Tick]
        public Task OnTick()
        {
            foreach (Taxi taxi in activeTaxis)
            {
                Debug.WriteLine(taxi.Vehicle.LocalizedName);
            }
            return Task.FromResult(0);
        }
    }
}
