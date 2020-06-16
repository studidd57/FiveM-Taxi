using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveM_Taxi.Client
{
    enum TaxiStatus
    {
        ENROUTE,    // Taxi is currently driving to it's desired location
        ARRIVED,    // Arrived at the desired location, be it that of the passenger or destination
        OPEN        // Available for service
    }
}
