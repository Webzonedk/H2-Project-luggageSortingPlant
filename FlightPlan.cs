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
        private string name;
        private int flightNumber;
        private string destination;
        private int seats;
        private int gateNumber;
        private DateTime departureTime;




        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { name = value; }
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
                public FlightPlan(string name)
        {
            this.name = name;
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
                if (Manager.flightPlans[Manager.maxPendingFlights] == null)
                {
                    int destinationIndex = Manager.random.Next(0, Manager.destinations.Length);
                    int seats = Manager.random.Next(0, Manager.numberOfSeats.Length);
                    FlightPlan flightPlan = new FlightPlan();
                    flightPlan.FlightNumber++;
                    flightPlan.Destination = Manager.destinations[destinationIndex];
                    flightPlan.Seats = Manager.numberOfSeats[seats];
                    flightPlan.GateNumber = Manager.random.Next(1, Program.manager.AmountOfGates);
                    if (Manager.flightPlans[Manager.maxPendingFlights - 1] == null)
                    {
                        flightPlan.DepartureTime = Program.manager.CurrentTime.AddSeconds(Manager.random.Next(10, 20));
                    }
                    else
                    {
                        flightPlan.DepartureTime = Manager.flightPlans[Manager.maxPendingFlights - 1].DepartureTime.AddSeconds(Manager.random.Next(10, 20));
                    }
                    Manager.flightPlans[Manager.maxPendingFlights] = flightPlan;
                }
            }
        }


        #endregion
    }
}
