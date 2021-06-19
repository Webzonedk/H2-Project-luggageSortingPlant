using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class CheckInBuffer
    {
        #region Fields
        private int checkInNumber;



        private Luggage[] buffer = new Luggage[MainServer.checkInBufferSize];

        #endregion



        #region Properties
        public int CheckInNumber
        {
            get { return checkInNumber; }
            set { checkInNumber = value; }
        }
        public Luggage[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }

        #endregion

        #region Constructors
        public CheckInBuffer()
        {

        }
        public CheckInBuffer(int checkInNumber)
        {
            this.checkInNumber = checkInNumber;
        }
        //Instantiating


        #endregion

        #region Methods
        public void ReorderingCheckInBuffer()//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {

                    Monitor.Enter(MainServer.checkInBuffers[CheckInNumber]);//Locking the thread
                    for (int i = 0; i < MainServer.checkInBuffers[CheckInNumber].Buffer.Length - 1; i++)
                    {
                        if (MainServer.checkInBuffers[CheckInNumber].Buffer[i] == null)
                        {
                            MainServer.checkInBuffers[CheckInNumber].Buffer[i] = MainServer.checkInBuffers[CheckInNumber].Buffer[i + 1];
                            MainServer.checkInBuffers[CheckInNumber].Buffer[i + 1] = null;
                        }
                    }


                    ////OLD VERSION
                    //Monitor.Enter(MainServer.checkInBuffers[CheckInNumber]);//Locking the thread

                    //if (MainServer.checkInBuffers[CheckInNumber].Buffer[MainServer.checkInBufferSize - 1] == null)//If the last buffer index is empty
                    //{
                    //    Monitor.Wait(MainServer.checkInBuffers[CheckInNumber]);//Setting the thread in waiting state
                    //}
                    //else
                    //{
                    //    if (MainServer.checkInBuffers[CheckInNumber].Buffer[0] == null)//If the first buffer index is empty
                    //    {

                    //        for (int i = 1; i < MainServer.checkInBuffers[CheckInNumber].Buffer.Length; i++)//Loop through all boxes in the array
                    //        {
                    //            MainServer.checkInBuffers[CheckInNumber].Buffer[i - 1] = MainServer.checkInBuffers[CheckInNumber].Buffer[i];//Move all content oft the indexes one down
                    //        }
                    //        MainServer.checkInBuffers[CheckInNumber].Buffer[MainServer.checkInBufferSize - 1] = null;//Setting the last index to null
                    //        MainServer.outPut.PrintCheckInBufferWorkerOutput(CheckInNumber);
                    //    }
                    //}
                }
                finally
                {
                    Monitor.PulseAll(MainServer.checkInBuffers[CheckInNumber]);//Sending signal to other thread
                    Monitor.Exit(MainServer.checkInBuffers[CheckInNumber]);//Release the lock
                    //int randomSleep = MainServer.random.Next(MainServer.randomSleepMin, MainServer.randomSleepMax);
                    //Thread.Sleep(randomSleep);
                }
            }
        }
        #endregion
    }
}

