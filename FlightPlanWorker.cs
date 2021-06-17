using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class FlightPlanWorker
    {
        #region Fields
        private string workerName;

        #endregion

        #region Properties

        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }

        #endregion

        #region Constructors
        public FlightPlanWorker(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion

        #region Methods

        #endregion
        //Adding flights if the flightbuffer is not full
        public void AddFlightToFlightPlan()
        {
            int flightNumberCounter = 0;
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.flightPlans);//Locking the thread

                    for (int i = 0; i < MainServer.flightPlans.Length; i++)
                    {
                        if (MainServer.flightPlans[i] == null)
                        {
                            int destinationIndex = MainServer.random.Next(0, MainServer.destinations.Length);
                            int seats = MainServer.random.Next(0, MainServer.numberOfSeats.Length);
                            FlightPlan flightPlan = new FlightPlan();
                            flightPlan.FlightNumber = flightNumberCounter;
                            flightNumberCounter++;
                            flightPlan.Destination = MainServer.destinations[destinationIndex];
                            flightPlan.Seats = MainServer.numberOfSeats[seats];
                            flightPlan.GateNumber = MainServer.random.Next(1, MainServer.amountOfGates);
                            if (i == 0)
                            {
                                flightPlan.DepartureTime = Program.manager.CurrentTime.AddSeconds(MainServer.random.Next(10, 20));
                            }
                            else
                            {
                                flightPlan.DepartureTime = MainServer.flightPlans[i - 1].DepartureTime.AddSeconds(MainServer.random.Next(10, 20));
                            }
                            MainServer.flightPlans[i] = flightPlan;
                            MainServer.outPut.PrintFlightPlan(i);//Send parameter with the method
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


    }
}
