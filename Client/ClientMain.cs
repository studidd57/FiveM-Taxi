using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace FiveM_Taxi.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            Debug.WriteLine("FiveM-Taxi by Zulfurix loaded!");
        }

        [Tick]
        public Task OnTick()
        {
            return Task.FromResult(0);
        }
    }
}