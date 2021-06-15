using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace luggageSortingPlant
{
    public class Luggage
    {
        #region Fields
        private string workerName;



        private int luggageNumber;
        private int passengerNumber;
        private string passengerName;
        private int flightNumber;
        private DateTime checkInTimeStamp;
        private DateTime sortInTimeStmap;
        private DateTime sortOutTimeStamp;
        private DateTime gateArrivalTimeStamp;


        #endregion



        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }
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
        public DateTime CheckInTimeStamp
        {
            get { return checkInTimeStamp; }
            set { checkInTimeStamp = value; }
        }

        public DateTime SortInTimeStmap
        {
            get { return sortInTimeStmap; }
            set { sortInTimeStmap = value; }
        }

        public DateTime SortOutTimeStamp
        {
            get { return sortOutTimeStamp; }
            set { sortOutTimeStamp = value; }
        }

        public DateTime GateArrivalTimeStamp
        {
            get { return gateArrivalTimeStamp; }
            set { gateArrivalTimeStamp = value; }
        }


        public Luggage()
        {

        }
        public Luggage(string workerName)
        {
            this.workerName = workerName;
        }
        public Luggage(int luggageNumber, int passengerNumber, string passengerName, int flightNumber, DateTime checkInTimeStamp, DateTime sortInTimeStmap, DateTime sortOutTimeStamp, DateTime gateArrivalTimeStamp)
        {
            this.luggageNumber = luggageNumber;
            this.passengerNumber = passengerNumber;
            this.passengerName = passengerName;
            this.flightNumber = flightNumber;
            this.checkInTimeStamp = checkInTimeStamp;
            this.sortInTimeStmap = sortInTimeStmap;
            this.sortOutTimeStamp = sortOutTimeStamp;
            this.gateArrivalTimeStamp = gateArrivalTimeStamp;
        }

        #endregion

        #region Constructors


        #endregion

        #region Methods
        public void CreateLuggage()
        {
            int luggageCounter = 1;
            int paasengerNumber = 1;
            while (true)
                for (int i = 0; i < Manager.luggageBuffer.Length; i++)
                {
                    if (Manager.luggageBuffer[i] == null)
                    {
                        Luggage luggage = new Luggage();
                        luggage.LuggageNumber = luggageCounter;
                        luggageCounter++;
                        luggage.PassengerNumber = paasengerNumber;
                        paasengerNumber++;
                        Faker passengerName = new Faker();
                        luggage.PassengerName = passengerName.Name.FullName();

                        int randomFlightNumber = Manager.random.Next(0, Manager.maxPendingFlights);
                        int countLuggage = 0;
                        for (int j = 0; j < Manager.luggageBuffer.Length; j++)
                        {
                            if (Manager.luggageBuffer[j].FlightNumber == Manager.flightPlans[randomFlightNumber].FlightNumber)
                            {
                                countLuggage++;
                            }
                        }
                        if (countLuggage < Manager.flightPlans[randomFlightNumber].Seats)
                        {
                            luggage.FlightNumber = randomFlightNumber;
                            Manager.luggageBuffer[i] = luggage;
                        }

                    }
                }

        }



        #endregion
    }
}
