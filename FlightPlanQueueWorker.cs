﻿using System;
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
        public FlightPlanQueueWorker()
        {

        }
        #endregion

        #region Methods
        public void ReorderingFlightPlan()//Reordering the flightplanbuffer to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.flightPlans);//Locking the thread


                    for (int i = 0; i < MainServer.flightPlans.Length - 1; i++)
                    {
                        if (MainServer.flightPlans[i] == null)
                        {
                            MainServer.flightPlans[i] = MainServer.flightPlans[i + 1];
                            MainServer.flightPlans[i + 1] = null;
                        }
                    }





                    //OLD version
                    //  Monitor.Enter(MainServer.flightPlans);//Locking the thread
                    //if (MainServer.flightPlans[MainServer.maxPendingFlights - 1] != null)
                    //{
                    //    if (MainServer.flightPlans[0] == null)
                    //    {
                    //        for (int i = 1; i < MainServer.flightPlans.Length; i++)
                    //        {

                    //            MainServer.flightPlans[i - 1] = MainServer.flightPlans[i];
                    //        }
                    //        MainServer.flightPlans[MainServer.maxPendingFlights - 1] = null;
                    //    }
                    //}
                    //else
                    //{
                    //    Monitor.Wait(MainServer.flightPlans);//Setting the thread in waiting state
                    //}
                }
                finally
                {
                    Monitor.PulseAll(MainServer.flightPlans);//Sending signal to other thread
                    Monitor.Exit(MainServer.flightPlans);//Release the lock

                    //int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    //Thread.Sleep(randomSleep);
                }
            }
        }
        #endregion
    }
}
