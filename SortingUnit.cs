using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace luggageSortingPlant
{
    class SortingUnit
    {
        #region Fields
        private int sortingUnitNumber;

        #endregion



        #region Properties
        public int SortingUnitNumber
        {
            get { return sortingUnitNumber; }
            set { sortingUnitNumber = value; }
        }

        #endregion

        #region Constructors

        public SortingUnit(int sortingUnitNumber)
        {
            this.sortingUnitNumber = sortingUnitNumber;
        }

        #endregion

        #region Methods
        public void SortLuggage()
        {
            Luggage[] tempLuggage = new Luggage[1];//To have an object array to keep temp luggage in the mainentrance
            while (true)
            {
                try
                {
                    //receive luggage from the hall, represented with the Luggagebuffer
                    try
                    {

                        Monitor.Enter(MainServer.sortingUnitBuffer);//Locking the thread
                        if ((MainServer.luggageBuffer[0] != null) && (tempLuggage[0] == null))
                        {

                            // luggage = MainServer.luggageBuffer[0];
                            Array.Copy(MainServer.luggageBuffer, 0, tempLuggage, 0, 1);//Copy first index from luggagebuffer to the temp array

                            MainServer.luggageBuffer[0] = null;
                            MainServer.outPut.PrintArrivedToTheAirport(tempLuggage[0]);
                        }
                        else
                        {
                            Monitor.Wait(MainServer.sortingUnitBuffer);//Setting the thread in waiting state
                        };
                    }
                    finally
                    {
                        Monitor.Pulse(MainServer.sortingUnitBuffer);//Sending signal to LuggageWorker
                        Monitor.Exit(MainServer.sortingUnitBuffer);//Unlocking thread
                                                               //int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                                                               //Thread.Sleep(randomSleep);
                    };


                }
                finally
                {

                }
            }
        }
        #endregion
    }
}
