using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class OutPut
    {
        #region Fields
 
        #endregion

        #region Properties

        #endregion

        #region Constructors


        #endregion

        #region Methods

        #endregion
        public void PrintFlightPlan(int i)//Method with argument
        {
                    Console.Write("{0,-10}", $"{MainServer.flightPlans[i].FlightNumber}");
                    Console.Write("{0,-35}", $"{MainServer.flightPlans[i].Destination}");
                    Console.Write("{0,7}", $"{MainServer.flightPlans[i].Seats}");
                    Console.Write("{0,10}", $"{MainServer.flightPlans[i].GateNumber}");
                    Console.Write("{0,25}", $"{MainServer.flightPlans[i].DepartureTime}\n");
        }
    }
}
