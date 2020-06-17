using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;

namespace FiveM_Taxi.Client
{
    class DestinationMenu : BaseScript
    {
        private static Menu DestMenu = new Menu("Destination Menu", "Select your destination.");

        public DestinationMenu()
        {
            MenuController.AddMenu(DestMenu);

            // Create default menu items
            DestMenu.AddMenuItem(new MenuItem("Waypoint", "Set your waypoint as your destination.")
            {
                Enabled = true,
            });

            // Destination Menu events
            DestMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                Taxi currTaxi = TaxiHandler.GetCurrentPlayerTaxi(Game.Player);
                if (currTaxi != null)
                {
                    if (Game.IsWaypointActive)
                    {
                        currTaxi.DriveTo(World.WaypointPosition);
                    }
                    else
                    {
                        _item.Description = "You must set a waypoint first!";
                    }
                }
            };

            DestMenu.OnMenuClose += (_menu) =>
            {
                _menu.GetMenuItems()[0].Description = "Set your waypoint as your destination.";
            };
        }

        public static void ShowDestinationMenu()
        {
            DestMenu.OpenMenu();
        }
    }
}
