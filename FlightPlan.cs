using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class FlightPlan
    {
        #region Fields
        private int flightNumber;
        private int seats;
        private int gateNumber;
        private DateTime departureTime;

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

        public FlightPlan(int flightNumber, int seats, int gateNumber, DateTime departureTime)
        {
            this.flightNumber = flightNumber;
            this.seats = seats;
            this.gateNumber = gateNumber;
            this.departureTime = departureTime;
        }


        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion
    }
}
