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

        private int flightNumber;
        private string destination;

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        private int seats;
        private int gateNumber;
        private DateTime departureTime;
        private int flightPlanHours;

   


        #endregion

        #region Properties
        public int FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
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
        public int FlightPlanHours
        {
            get { return flightPlanHours; }
            set { flightPlanHours = value; }
        }
        public FlightPlan()
        {

        }

        public FlightPlan(int flightNumber, string destination, int seats, int gateNumber, DateTime departureTime, int flightPlanHours)
        {
            this.flightNumber = flightNumber;
            this.destination = destination;
            this.seats = seats;
            this.gateNumber = gateNumber;
            this.departureTime = departureTime;
            this.flightPlanHours = flightPlanHours;
        }




        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void CreateFlightPlan()
        {
            Faker FlightplanPeriod = new Faker();
            FlightplanPeriod.Date.Between(DateTime.Today, DateTime.Now.AddHours(FlightPlanHours));
          
        }

        #endregion
    }
}
