using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class CheckIn
    {
        #region Fields
        private string workerName;
        private bool open;
        private int checkInNumber;

        #endregion



        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }
        public bool Open
        {
            get { return open; }
            set { open = value; }
        }
        public int CheckInNumber
        {
            get { return checkInNumber; }
            set { checkInNumber = value; }
        }
        #endregion



        #region Constructors
        public CheckIn()
        {

        }
        public CheckIn(string workerName, bool open, int checkInNumber)
        {
            this.workerName = workerName;
            this.open = open;
            this.checkInNumber = checkInNumber;
        }
        #endregion



        #region Methods

        public void CheckInLuggage()
        {
            while (true)
            {


                DateTime departure = DateTime.Now;
                try
                {
                    Monitor.Enter(MainServer.flightPlans);//Locking the thread

                    //find flight in flightplan and get departuretime
                    for (int i = 0; i < MainServer.flightPlans.Length; i++)
                    {
                        if (MainServer.flightPlans[i].FlightNumber == MainServer.checkInBuffers[checkInNumber].Buffer[0].FlightNumber)
                        {
                            departure = MainServer.flightPlans[i].DepartureTime;
                        }
                    }
                }
                finally
                {
                    Monitor.PulseAll(MainServer.flightPlans);//Sending signal to other thread
                    Monitor.Exit(MainServer.flightPlans);//Release the lock
                }

                //Creating an object to contain the luggage
                Luggage luggage = new();

                //removing luggage from the checkIn buffer
                try
                {
                    Monitor.Enter(MainServer.checkInBuffers[CheckInNumber].Buffer);//Locking the thread

                    if ((departure - DateTime.Now).TotalSeconds <= MainServer.checkInOpenBeforeDeparture)
                    {
                        Open = true;
                    }
                    if (Open == true)
                    {

                        if (MainServer.checkInBuffers[CheckInNumber].Buffer[0] != null)
                        {
                            luggage = MainServer.checkInBuffers[CheckInNumber].Buffer[0];
                            luggage.CheckInTimeStamp = DateTime.Now;
                            MainServer.outPut.PrintCheckInArrival(luggage);
                            MainServer.checkInBuffers[CheckInNumber].Buffer[0] = null;

                        }
                        else
                        {
                            Monitor.Wait(MainServer.checkInBuffers[checkInNumber].Buffer);//Setting the thread in waiting state
                        }



                    }
                }
                finally
                {
                    Monitor.PulseAll(MainServer.checkInBuffers[CheckInNumber].Buffer);//Sending signal to other thread
                    Monitor.Exit(MainServer.checkInBuffers[CheckInNumber].Buffer);//Release the lock

                }

                //Adding luggage to SortingBuffer
                try
                {
                    Monitor.Enter(MainServer.sortingUnitBuffer);//Locking the thread
                    if (MainServer.sortingUnitBuffer.Buffer[MainServer.sortBufferSize - 1] == null)
                    {
                        MainServer.sortingUnitBuffer.Buffer[MainServer.sortBufferSize - 1] = luggage;
                        luggage = null;
                        int i;
                        for (i = 0; i < MainServer.sortingUnitBuffer.Buffer.Length;)
                        {
                            if (MainServer.sortingUnitBuffer.Buffer[i] != null)
                            {
                                i++;
                            }
                        }
                        MainServer.outPut.PrintSortingBufferCapacity(i);
                    }
                    else
                    {
                        Monitor.Wait(MainServer.sortingUnitBuffer);//Set thread to wait state
                    }
                }
                finally
                {
                    Monitor.PulseAll(MainServer.sortingUnitBuffer);//Sending signal to other thread
                    Monitor.Exit(MainServer.sortingUnitBuffer);//Release the lock
                }


            }
        }
        #endregion
    }
}
