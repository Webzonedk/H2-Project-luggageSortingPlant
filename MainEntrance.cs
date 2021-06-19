using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class MainEntrance
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
        public MainEntrance(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion



        #region Methods
        public void SendLuggageToCheckIn()
        {
            while (true)
            {
                Luggage luggage = new();
                //receive luggage from the hall, represented with the Luggagebuffer
                try
                {
                    Monitor.Enter(MainServer.luggageBuffer);//Locking the thread
                  
                    if (MainServer.luggageBuffer[0] != null)
                    {
                        luggage = MainServer.luggageBuffer[0];
                        MainServer.luggageBuffer[0] = null;
                    MainServer.outPut.PrintArrivedToTheAirport(luggage);
                    }
                    else
                    {
                        Monitor.Wait(MainServer.luggageBuffer);//Setting the thread in waiting state
                    }
                }
                finally
                {
                    Monitor.Pulse(MainServer.luggageBuffer);//Sending signal to LuggageWorker
                    Monitor.Exit(MainServer.luggageBuffer);//Unlocking thread
                    int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    Thread.Sleep(randomSleep);
                }


                int flightNumber = 0;
                int checkInNumber = 0;
                // int gateNumber = 0;
                //Sending the luggage to the right checkins
                try
                {

                    //Check if there is already a buffer in use for the specific flight and if thats the case, insert luggae object in that buffer
                    for (int i = 0; i < MainServer.checkInBuffers.Length; i++)//Run throught all the buffers in the array
                    {
                        checkInNumber = i;
                        Monitor.Enter(MainServer.checkInBuffers[checkInNumber].Buffer);//Locking the thread
                        int j;
                        for (j = 0; j < MainServer.checkInBuffers[i].Buffer.Length;)//loop through the current Buffer
                        {
                            if (MainServer.checkInBuffers[i].Buffer[j] != null)//Count luggage in current buffer and check the flightNumber of the luggae as well
                            {
                                j++;
                                flightNumber = MainServer.checkInBuffers[i].Buffer[j].FlightNumber;//Select the flightNumber in current buffer
                            }
                        }
                        //If buffer is not full and If Luggage flightnumber is = flightNumber in the buffer
                        //Then insert the luggage to the last placr in the buffer, and empty the luggage object
                        if ((j < MainServer.checkInBuffers[i].Buffer.Length - 1) && (luggage.FlightNumber == flightNumber))
                        {
                            MainServer.checkInBuffers[i].Buffer[MainServer.checkInBufferSize - 1] = luggage;
                            MainServer.outPut.PrintCheckInBufferCapacity(checkInNumber, j);//Printing to console
                            luggage = null;
                        }
                    }

                    //If luggage object is still not null after first check, then 
                    if (luggage != null)
                    {
                        for (int i = 0; i < MainServer.checkInBuffers.Length; i++)
                        {
                            checkInNumber = i;
                            Monitor.Enter(MainServer.checkInBuffers[checkInNumber].Buffer);//Locking the thread

                            int k;
                            for (k = 0; k < MainServer.checkInBuffers[i].Buffer.Length;)//loop through the current Buffer
                            {
                                if (MainServer.checkInBuffers[i].Buffer[k] != null)//Count empty spaces in buffer and check the flightNumber of the luggae in the buffer
                                {
                                    k++;
                                }
                            }
                            if (k == 0)
                            {
                                MainServer.checkInBuffers[i].Buffer[MainServer.checkInBufferSize - 1] = luggage;//Adding the luggage to the buffer
                                MainServer.outPut.PrintCheckInBufferCapacity(checkInNumber, 1);//Printing to console

                                luggage = null;
                            }
                        }
                    }
                }
                finally
                {
                    Monitor.PulseAll(MainServer.checkInBuffers[checkInNumber].Buffer);//Sending signal to LuggageWorker
                    Monitor.Exit(MainServer.checkInBuffers[checkInNumber].Buffer);//Unlocking thread
                    int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    Thread.Sleep(randomSleep);
                }
            }
        }
        #endregion
    }
}
