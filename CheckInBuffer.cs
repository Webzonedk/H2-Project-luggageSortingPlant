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
        public CheckInBuffer(Luggage[] buffer)
        {
            this.buffer = buffer;
        }

        #endregion

        #region Methods
        public void ReorderingCheckInBuffer()//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.checkInBuffers[checkInNumber]);//Locking the thread
                    if (MainServer.checkInBuffers[checkInNumber].Buffer[MainServer.checkInBufferSize - 1] == null)//If the buffer index 0 is empty
                    {
                        Monitor.Wait(MainServer.checkInBuffers[checkInNumber]);//Setting the thread in waiting state
                    }
                    else
                    {
                        for (int i = 1; i < MainServer.checkInBuffers[checkInNumber].Buffer.Length; i++)//Loop through all boxes in the array
                        {
                            MainServer.checkInBuffers[checkInNumber].Buffer[i - 1] = MainServer.checkInBuffers[checkInNumber].Buffer[i];//Move all content oft the indexes one down
                        }
                            MainServer.outPut.PrintCheckInBufferWorkerOutput(checkInNumber);
                    }
                }
                finally
                {
                    Monitor.Pulse(MainServer.checkInBuffers[checkInNumber]);//Sending signal to other thread
                    Monitor.Exit(MainServer.checkInBuffers[checkInNumber]);//Release the lock
                }
            }
        }
        #endregion
    }
}

