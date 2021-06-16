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
                try
                {
                    Monitor.Enter(MainServer.luggageBuffer);//Locking the thread
                    if (MainServer.luggageBuffer[0] == null)
                    {
                        MainServer.cleaningLady.ReorderingLuggageBuffer();
                    }
                    if (MainServer.luggageBuffer[0] != null)
                    {
                        luggage = MainServer.luggageBuffer[0];
                        MainServer.luggageBuffer[0] = null;
                        MainServer.cleaningLady.ReorderingLuggageBuffer();

                    }
                    Monitor.Pulse(MainServer.luggageBuffer);//Sending signal to LuggageWorker
                    Monitor.Exit(MainServer.luggageBuffer);//Unlocking thread

                }
                catch
                {


                }

                try
                {
                    int gateNumber = 0;
                    for (int i = 0; i < MainServer.flightPlans.Length; i++)
                    {
                        if (MainServer.flightPlans[i].FlightNumber == luggage.FlightNumber)
                        {
                            gateNumber = MainServer.flightPlans[i].GateNumber;
                        }
                    }
                    Monitor.Enter(MainServer.checkInBuffers[gateNumber].Buffer);//Locking the thread

                    if (MainServer.checkInBuffers[gateNumber].Buffer[MainServer.checkInBufferSize] != null)
                    {
                        MainServer.checkInBuffers[gateNumber].Buffer[MainServer.checkInBufferSize] = luggage;
                        int luggageInBuffer=0;
                        foreach (var item in MainServer.checkInBuffers[gateNumber].Buffer)
                        {
                            if (item!=null)
                            {
                                luggageInBuffer++;
                            }
                        }
                        MainServer.outPut.PrintCheckInBufferCapacity(gateNumber, luggageInBuffer);
                    }
                    Monitor.Pulse(MainServer.checkInBuffers[gateNumber].Buffer);//Sending signal to LuggageWorker
                    Monitor.Exit(MainServer.checkInBuffers[gateNumber].Buffer);//Unlocking thread
                }
                catch
                {
                }

            }

        }
        #endregion
    }
}
