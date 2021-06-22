﻿using System;
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
        public SortingUnit()
        {

        }
        public SortingUnit(int sortingUnitNumber)
        {
            this.sortingUnitNumber = sortingUnitNumber;
        }

        #endregion

        #region Methods
        public void SortLuggage()
        {
            Luggage[] tempLuggage = new Luggage[1];//To have an object array to keep temp luggage in the mainentrance
            int flightNumber;
            string destination = "";
            int gateNumber = -1;
            while (true)
            {

                //receive luggage from the hall, represented with the Luggagebuffer
                try
                {

                    Monitor.Enter(MainServer.sortingUnitBuffer);//Locking the thread
                    if ((MainServer.sortingUnitBuffer[0] != null) && (tempLuggage[0] == null))
                    {

                        // luggage = MainServer.luggageBuffer[0];
                        Array.Copy(MainServer.sortingUnitBuffer, 0, tempLuggage, 0, 1);//Copy first index from luggagebuffer to the temp array
                        tempLuggage[0].SortInTimeStmap = DateTime.Now;
                        MainServer.sortingUnitBuffer[0] = null;
                        MainServer.outPut.PrintSortingArrival(tempLuggage[0]);
                        flightNumber = tempLuggage[0].FlightNumber;
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
                };


                //Sending the luggage to the right checkins
                if (tempLuggage[0] != null)
                {
                    try
                    {
                        Monitor.Enter(MainServer.flightPlans);//Locking the thread
                        //Getting the gateNumber and flightnumber for the luggage in tempbuffer
                        for (int i = 0; i < MainServer.flightPlans.Length; i++)
                        {
                            if (tempLuggage[0].FlightNumber == MainServer.flightPlans[i].FlightNumber)
                            {
                                gateNumber = MainServer.flightPlans[i].GateNumber;
                                destination = MainServer.flightPlans[i].Destination;
                            };
                        };
                    }
                    finally
                    {
                        Monitor.Pulse(MainServer.flightPlans);//Sending signal to LuggageWorker
                        Monitor.Exit(MainServer.flightPlans);//Unlocking thread
                    };

                    //Moving luggage from tempbuffeer to the gatebuffer
                    try
                    {
                        Monitor.Enter(MainServer.gateBuffers[gateNumber]);//Locking the thread

                        if (MainServer.gateBuffers[gateNumber].Buffer[MainServer.gateBufferSize - 1] == null && gateNumber != -1)
                        {
                            tempLuggage[0].SortOutTimeStamp = DateTime.Now;
                            Array.Copy(tempLuggage, 0, MainServer.gateBuffers[gateNumber].Buffer, MainServer.gateBufferSize - 1, 1);//Copy first index from tempLuggage to the last index in the luggage buffer array
                            MainServer.outPut.PrintSortedToGate(tempLuggage[0], gateNumber);
                            tempLuggage[0] = null;

                        };
                    }
                    finally
                    {
                        Monitor.Pulse(MainServer.gateBuffers[gateNumber]);//Sending signal to LuggageWorker
                        Monitor.Exit(MainServer.gateBuffers[gateNumber]);//Unlocking thread
                        gateNumber = -1;
                        int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                        Thread.Sleep(randomSleep);
                    };

                    //If luggage is too late to the gate and the flight has taken off, then redirict luggage to another gate for that destination
                    if (tempLuggage[0] != null)
                    {
                        try
                        {
                            Monitor.Enter(MainServer.flightPlans);//Locking the thread
                            for (int i = 0; i < MainServer.flightPlans.Length; i++)
                            {
                                if (destination == MainServer.flightPlans[i].Destination)
                                {
                                    gateNumber = MainServer.flightPlans[i].GateNumber;

                                };
                            };
                        }
                        finally
                        {
                            Monitor.Pulse(MainServer.flightPlans);//Sending signal to LuggageWorker
                            Monitor.Exit(MainServer.flightPlans);//Unlocking thread
                        };

                        if (gateNumber != -1 && MainServer.gateBuffers[gateNumber].Buffer[MainServer.gateBufferSize - 1] == null)
                        {
                            try
                            {
                                Monitor.Enter(MainServer.gateBuffers[gateNumber]);//Locking the thread
                                Array.Copy(tempLuggage, 0, MainServer.gateBuffers[gateNumber].Buffer, MainServer.gateBufferSize - 1, 1);//Copy first index from tempLuggage to the last index in the luggage buffer array
                                MainServer.outPut.PrintSortedToGate(tempLuggage[0], gateNumber);
                                tempLuggage[0] = null;
                            }
                            finally
                            {
                                Monitor.Pulse(MainServer.gateBuffers[gateNumber]);//Sending signal to LuggageWorker
                                Monitor.Exit(MainServer.gateBuffers[gateNumber]);//Unlocking thread
                            };
                        };
                    };
                };
            };
        }
        #endregion
    }
}
