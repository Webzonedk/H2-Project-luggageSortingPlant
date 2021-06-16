using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bogus;
//-----------------------------------------------------------------------------------------
//This class is made to create luggage. It is a thread worker
//-----------------------------------------------------------------------------------------
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


                try
                {
                Monitor.Enter(MainServer.luggageBuffer);//Locking the thread

                    for (int i = 0; i < MainServer.luggageBuffer.Length; i++)
                    {


                        if (MainServer.luggageBuffer[i] == null)
                        {
                            //Added a check to ensure that the randomMax will not exceed the amount og flights in the flightplan
                            int randomMax;
                            for (randomMax = 0; randomMax < MainServer.flightPlans.Length;)
                            {
                                if (MainServer.flightPlans[randomMax] != null)
                                {
                                    randomMax++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            int randomFlightNumber = MainServer.random.Next(0, randomMax);
                            int countLuggage = 0;
                            //if (MainServer.luggageBuffer[0] != null)
                            //{
                            for (int j = 0; j < MainServer.luggageBuffer.Length; j++)
                            {
                                if ((MainServer.luggageBuffer[j] != null) && (MainServer.luggageBuffer[j].FlightNumber == MainServer.flightPlans[randomFlightNumber].FlightNumber))
                                {
                                    countLuggage++;
                                }
                            }

                            //}
                            if ((MainServer.flightPlans[randomFlightNumber] != null) && (countLuggage < MainServer.flightPlans[randomFlightNumber].Seats))
                            {
                                Luggage luggage = new Luggage();
                                luggage.LuggageNumber = luggageCounter;
                                luggageCounter++;
                                luggage.PassengerNumber = paasengerNumber;
                                paasengerNumber++;
                                Faker passengerName = new Faker();
                                luggage.PassengerName = passengerName.Name.FullName();

                                luggage.FlightNumber = randomFlightNumber;
                                MainServer.luggageBuffer[i] = luggage;
                                MainServer.outPut.PrintLuggage(i);
                                i = MainServer.luggageBuffer.Length;
                                
                            }
                        }
                    }
                    Monitor.Pulse(MainServer.luggageBuffer);//Sending signal to other thread
                    Monitor.Exit(MainServer.luggageBuffer);//Release the lock
                }
                catch
                {
                    throw;
                }
            }

        }

        #endregion
    }
}
