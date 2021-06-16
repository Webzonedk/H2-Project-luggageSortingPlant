using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    public class FlightPlan
    {
        #region Fields
        private string workerName;
        private int flightNumber;
        private string destination;
        private int seats;
        private int gateNumber;
        private DateTime departureTime;




        #endregion

        #region Properties

        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }

        public int FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public int Seats
        {
            get { return seats; }
            set { seats = value; }
        }
        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }

        public DateTime DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

        public FlightPlan()
        {

        }
        public FlightPlan(string workerName)
        {
            this.workerName = workerName;
        }

        public FlightPlan(int flightNumber, string destination, int seats, int gateNumber, DateTime departureTime)
        {
            this.flightNumber = flightNumber;
            this.destination = destination;
            this.seats = seats;
            this.gateNumber = gateNumber;
            this.departureTime = departureTime;
        }




        #endregion

        #region Constructors

        #endregion

        #region Methods
        //Adding flights if the flightbuffer is not full
        public void AddFlightToFlightPlan()
        {
            while (true)
            {
                if (MainServer.flightPlans[MainServer.maxPendingFlights] == null)
                {
                    int destinationIndex = MainServer.random.Next(0, MainServer.destinations.Length);
                    int seats = MainServer.random.Next(0, MainServer.numberOfSeats.Length);
                    FlightPlan flightPlan = new FlightPlan();
                    flightPlan.FlightNumber++;
                    flightPlan.Destination = MainServer.destinations[destinationIndex];
                    flightPlan.Seats = MainServer.numberOfSeats[seats];
                    flightPlan.GateNumber = MainServer.random.Next(1, MainServer.amountOfGates);
                    if (MainServer.flightPlans[MainServer.maxPendingFlights - 1] == null)
                    {
                        flightPlan.DepartureTime = Program.manager.CurrentTime.AddSeconds(MainServer.random.Next(10, 20));
                    }
                    else
                    {
                        flightPlan.DepartureTime = MainServer.flightPlans[MainServer.maxPendingFlights - 1].DepartureTime.AddSeconds(MainServer.random.Next(10, 20));
                    }
                    MainServer.flightPlans[MainServer.maxPendingFlights] = flightPlan;
                }
            }
        }


        #endregion
    }
}
