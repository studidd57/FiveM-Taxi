using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace FiveM_Taxi.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {
            Debug.WriteLine("Hi from FiveM_Taxi.Server!");
        }

        [Command("calltaxi")]
        public void calltaxi()
        {
            TriggerClientEvent("FiveM-Taxi:CreateTaxi");
        }
    }
}