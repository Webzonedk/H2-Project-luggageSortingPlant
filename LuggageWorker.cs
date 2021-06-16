using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bogus;

namespace luggageSortingPlant
{
    class LuggageWorker
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
        public LuggageWorker(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion



        #region Methods
        public void CreateLuggage()
        {
            int luggageCounter = 1;
            int paasengerNumber = 1;
            while (true)
            {
                Monitor.Enter(MainServer.luggageBuffer);//Locking the thread
                for (int i = 0; i < MainServer.luggageBuffer.Length; i++)
                {
                    if (MainServer.luggageBuffer[i] == null)
                    {
                        Luggage luggage = new Luggage();
                        luggage.LuggageNumber = luggageCounter;
                        luggageCounter++;
                        luggage.PassengerNumber = paasengerNumber;
                        paasengerNumber++;
                        Faker passengerName = new Faker();
                        luggage.PassengerName = passengerName.Name.FullName();

                        int randomFlightNumber = MainServer.random.Next(0, MainServer.maxPendingFlights);
                        int countLuggage = 0;
                        for (int j = 0; j < MainServer.luggageBuffer.Length; j++)
                        {
                            if (MainServer.luggageBuffer[j].FlightNumber == MainServer.flightPlans[randomFlightNumber].FlightNumber)
                            {
                                countLuggage++;
                            }
                        }
                        if (countLuggage < MainServer.flightPlans[randomFlightNumber].Seats)
                        {
                            luggage.FlightNumber = randomFlightNumber;
                            MainServer.luggageBuffer[i] = luggage;
                        }
                    }
                }
            }

        }

        #endregion
    }
}
