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
            Taxi createdTaxi = new Taxi(Game.Player, VehicleHash.Taxi, PedHash.Business01AFY, World.GetNextPositionOnStreet(Game.PlayerPed.Position * 1.25f, true));
            createdTaxi.DriveTo(World.GetNextPositionOnStreet(Game.PlayerPed.Position, true));
            createdTaxi.Vehicle.AttachBlip();
        }

        public static void AddTaxiToHandler(Taxi taxi)
        {
            activeTaxis.Add(taxi);
        }

        [Tick]
        public Task MenuTick()
        {
            foreach (Taxi taxi in activeTaxis)
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    if (Game.PlayerPed.CurrentVehicle.Equals(taxi.Vehicle))
                    {
                        DestinationMenu.ShowDestinationMenu();
                    }
                }
            }

            return Task.FromResult(0);
        }

        [Tick]
        public Task CarControlsTick()
        {
            Taxi playerTaxi = GetPlayerOwnedTaxi(Game.Player);
            if (playerTaxi != null)
            {
                if (Game.PlayerPed.IsInRangeOf(playerTaxi.Vehicle.Position, 5.0f) && !Game.PlayerPed.IsInVehicle())
                {
                    DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to enter the taxi.", false);
                    if (Game.IsControlJustPressed(1, Control.Context))
                    {
                        Game.PlayerPed.Task.EnterVehicle(playerTaxi.Vehicle, VehicleSeat.LeftRear);
                    }
                }
            }
            return Task.FromResult(0);
        }

        public static Taxi GetPlayerOwnedTaxi(Player player)
        {
            foreach (Taxi taxi in activeTaxis)
            {
                if (taxi.Owner.Equals(player))
                {
                    return taxi;
                }
            }
            return null;
        }

        public static Taxi GetCurrentPlayerTaxi(Player player)
        {
            foreach (Taxi taxi in activeTaxis)
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    if (player.Character.CurrentVehicle.Equals(taxi.Vehicle))
                        return taxi;
                }
            }
            return null;
        }
    }
}
