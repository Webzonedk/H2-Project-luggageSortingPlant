using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace luggageSortingPlant
{
    class Gate
    {
        #region Fields
        private bool open;
        private int gateNumber;
        private Luggage[] buffer = new Luggage[MainServer.gateBufferSize];

        #endregion

        #region Properties
        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }
        public Luggage[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }


        #endregion

        #region Constructors
        public Gate()
        {

        }
        public Gate(bool open, int gateNumber)
        {
            this.open = open;
            this.gateNumber = gateNumber;
        }
        #endregion

        #region Methods

        public void Boarding()
        {
            DateTime departure = DateTime.Now;
            int luggageCounter = 0;


            try
            {
                Monitor.Enter(MainServer.flightPlans);//Locking the thread
                Monitor.Enter(MainServer.gateBuffers[GateNumber]);//Locking the thread

                if (MainServer.gateBuffers[GateNumber].Buffer[0] != null)
                {
                    //find flight in flightplan and get departuretime for the luggage in the buffer
                    for (int i = 0; i < MainServer.flightPlans.Length; i++)
                    {
                        if (MainServer.flightPlans[i].FlightNumber == MainServer.gateBuffers[GateNumber].Buffer[0].FlightNumber)
                        {
                            departure = MainServer.flightPlans[i].DepartureTime;//getting the depaturtime to use to open checkin
                            if (((departure - DateTime.Now).TotalSeconds <= MainServer.gateOpenBeforeDeparture) && ((departure - DateTime.Now).TotalSeconds >= MainServer.gateCloseBeforeDeparture))
                            {
                                Open = true;
                            };
                            if ((departure - DateTime.Now).TotalSeconds <= MainServer.gateCloseBeforeDeparture)
                            {
                                Open = false;
                            };
                            i = MainServer.flightPlans.Length;
                        };
                    };


                    if (Open)// If open
                    {
                        //removing luggage from the gate buffer
                        //if (MainServer.gateBuffers[gateNumber].Buffer[0] != null)
                        //{
                        Array.Copy(MainServer.gateBuffers[GateNumber].Buffer, 0, buffer, luggageCounter, 1);//Copy first index from gate buffer to the temp array
                        luggageCounter++;
                        MainServer.outPut.PrintGateCapacity(GateNumber, luggageCounter);
                        MainServer.gateBuffers[GateNumber].Buffer[0] = null;
                        //};
                    };

                    if ((departure - DateTime.Now).TotalSeconds <=0)
                    {
                        Open = false;
                        int i;
                        int flightNumber = 0;
                        for (i = 0; i < buffer.Length; i++)
                        {
                            if (buffer[i] != null)
                            {
                                Array.Copy(buffer, i, MainServer.flightPlanLog, MainServer.logSize - 1, 1);
                                buffer[i] = null;
                                flightNumber = MainServer.flightPlanLog[i].FlightNumber;
                            }
                        }
                            MainServer.outPut.PrintTakeOff(GateNumber, flightNumber, i);

                    }
                }
                else
                {
                    Monitor.Wait(MainServer.flightPlans);//Locking the thread
                    Monitor.Wait(MainServer.gateBuffers[GateNumber]);//Locking the thread
                };
            }
            finally
            {
                Monitor.PulseAll(MainServer.gateBuffers[GateNumber]);//Sending signal to other thread
                Monitor.Exit(MainServer.gateBuffers[GateNumber]);//Release the lock
                Monitor.PulseAll(MainServer.flightPlans);//Sending signal to other thread
                Monitor.Exit(MainServer.flightPlans);//Release the lock

                //int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                //Thread.Sleep(randomSleep);
            };
        }
        #endregion
    }
}
