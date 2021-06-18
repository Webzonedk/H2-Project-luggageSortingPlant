using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class FlightPlanQueueWorker
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void ReorderingFlightPlan()//Reordering the flightplanbuffer to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.flightPlans);//Locking the thread

                    if (MainServer.flightPlans[MainServer.maxPendingFlights - 1] == null)
                    {
                        Monitor.Wait(MainServer.flightPlans);//Setting the thread in waiting state
                    }
                    else
                    {
                        if (MainServer.flightPlans[0] == null)
                        {
                            for (int i = 1; i < MainServer.flightPlans.Length; i++)
                            {

                                MainServer.flightPlans[i - 1] = MainServer.flightPlans[i];
                            }
                        }
                    }
                }
                finally
                {
                    Monitor.Pulse(MainServer.flightPlans);//Sending signal to other thread
                    Monitor.Exit(MainServer.flightPlans);//Release the lock
                }
            }
        }
        #endregion
    }
}
