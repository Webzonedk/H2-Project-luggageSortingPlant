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
        #endregion



        #region Properties

        #endregion



        #region Constructors
        public MainEntrance()
        {

        }
        #endregion



        #region Methods
        public void SendLuggageToCheckIn()
        {
            while (true)
            {
                int checkInNumber;
                int flightNumber;
                Luggage luggage;
                Luggage[] tempLuggage = new Luggage[1];//To have an object array to keep temp luggage in the mainentrance

                //receive luggage from the hall, represented with the Luggagebuffer
                try
                {
                    Monitor.Enter(MainServer.luggageBuffer);//Locking the thread

                    if (MainServer.luggageBuffer[0] != null)
                    {

                        luggage = MainServer.luggageBuffer[0];
                        Array.Copy(MainServer.luggageBuffer, 0, tempLuggage, 0, 1);//Copy first index from luggagebuffer to the temp array

                        MainServer.luggageBuffer[0] = null;
                        MainServer.outPut.PrintArrivedToTheAirport(tempLuggage[0]);
                    }
                    else
                    {
                        Monitor.Wait(MainServer.luggageBuffer);//Setting the thread in waiting state
                    };
                }
                finally
                {
                    Monitor.Pulse(MainServer.luggageBuffer);//Sending signal to LuggageWorker
                    Monitor.Exit(MainServer.luggageBuffer);//Unlocking thread
                                                           //int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                                                           //Thread.Sleep(randomSleep);
                };



                // int gateNumber = 0;
                //Sending the luggage to the right checkins
                try
                {
                    //Check if there is already a buffer in use for the specific flight and if thats the case, insert luggae object in that buffer
                    for (int i = 0; i < MainServer.checkInBuffers.Length; i++)//Run throught all the buffers in the array
                    {
                        checkInNumber = i;
                        Monitor.Enter(MainServer.checkInBuffers[checkInNumber]);//Locking the thread
                                                                                //  int index = 0;
                        for (int j = 0; j < MainServer.checkInBuffers[i].Buffer.Length; j++)//loop through the current Buffer
                        {
                            if (MainServer.checkInBuffers[i].Buffer[j] != null)//Count luggage in current buffer and check the flightNumber of the luggae as well
                            {
                                flightNumber = MainServer.checkInBuffers[i].Buffer[j].FlightNumber;//Select the flightNumber in current buffer
                                                                                                   //If buffer is not full and If Luggage flightnumber is = flightNumber in the buffer
                                                                                                   //Then insert the luggage to the last placr in the buffer, and empty the luggage object
                                if ((j < MainServer.checkInBuffers[i].Buffer.Length - 1) && (tempLuggage[0].FlightNumber == flightNumber))
                                {
                                    Array.Copy(tempLuggage, 0, MainServer.checkInBuffers[i].Buffer, 0, 1);//Copy first index from tempLuggage to the checkIn buffer array

                                    // MainServer.checkInBuffers[i].Buffer[MainServer.checkInBufferSize - 1] = tempLuggage[0];
                                    MainServer.outPut.PrintCheckInBufferCapacity(checkInNumber, j);//Printing to console
                                    tempLuggage[0] = null;
                                    j = MainServer.checkInBuffers[i].Buffer.Length;
                                    i = MainServer.checkInBuffers.Length;
                                };


                            };
                        };
                        Monitor.PulseAll(MainServer.checkInBuffers[checkInNumber]);//Sending signal to LuggageWorker
                        Monitor.Exit(MainServer.checkInBuffers[checkInNumber]);//Unlocking thread
                    };
                    //    int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    //    Thread.Sleep(randomSleep);

                    //}
                    //finally
                    //{
                    //}

                    //try
                    //{
                    //If luggage object is still not null after first check, then 
                    if (tempLuggage[0] != null)
                    {
                        try
                        {
                            for (int i = 0; i < MainServer.checkInBuffers.Length; i++)
                            {
                                checkInNumber = i;
                                Monitor.Enter(MainServer.checkInBuffers[checkInNumber]);//Locking the thread

                                int counter = 0;
                                for (int j = 0; j < MainServer.checkInBuffers[i].Buffer.Length; j++)//loop through the current Buffer
                                {
                                    if (MainServer.checkInBuffers[i].Buffer[j] != null)//Count empty spaces in buffer and check the flightNumber of the luggae in the buffer
                                    {
                                        counter++;
                                    };
                                };
                                if (counter == 0)
                                {
                                    Array.Copy(tempLuggage, 0, MainServer.checkInBuffers[i].Buffer, MainServer.checkInBufferSize - 1, 1);//Copy first index from tempLuggage to the last index in the current checkIn buffer array

                                    // MainServer.checkInBuffers[i].Buffer[MainServer.checkInBufferSize - 1] = tempLuggage[0];//Adding the luggage to the buffer
                                    MainServer.outPut.PrintCheckInBufferCapacity(checkInNumber, 1);//Printing to console

                                    tempLuggage[0] = null;
                                };
                                if (tempLuggage[0] == null)
                                {
                                    i = MainServer.checkInBuffers.Length;
                                };
                                Monitor.PulseAll(MainServer.checkInBuffers[checkInNumber]);//Sending signal to LuggageWorker
                                Monitor.Exit(MainServer.checkInBuffers[checkInNumber]);//Unlocking thread
                            };

                        }
                        finally
                        {
                        }
                    };
                }
                finally
                {
                    int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    Thread.Sleep(randomSleep);
                };

            };
        }
        #endregion
    }
}
