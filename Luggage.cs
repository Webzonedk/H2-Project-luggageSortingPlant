using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class Luggage
    {
        #region Fields
        private int luggageNumber;
        private int passengerNumber;
        private string passengerName;
        private int flightNumber;
        #endregion



        #region Properties
        public int LuggageNumber
        {
            get { return luggageNumber; }
            set { luggageNumber = value; }
        }
        public int PassengerNumber
        {
            get { return passengerNumber; }
            set { passengerNumber = value; }
        }
        public string PassengerName
        {
            get { return passengerName; }
            set { passengerName = value; }
        }
        public int FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        #endregion

        #region Constructors
        public Luggage(int luggageNumber, int passengerNumber, string passengerName, int flightNumber)
        {
            this.luggageNumber = luggageNumber;
            this.passengerNumber = passengerNumber;
            this.passengerName = passengerName;
            this.flightNumber = flightNumber;
        }

        #endregion

        #region Methods
        public void CreateLuggage()
        {

        }
        #endregion
    }
}
