using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class CheckInBufferReorderngWorker
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors


        #endregion

        #region Methods
        public void ReorderingCheckInBuffer(int checkInNumber)//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.checkInBuffers[checkInNumber].Buffer);//Locking the thread
                    if (MainServer.checkInBuffers[checkInNumber].Buffer[MainServer.checkInBufferSize - 1] != null)//If the buffer index 0 is empty
                    {
                        for (int i = 1; i < MainServer.checkInBuffers[checkInNumber].Buffer.Length; i++)//Loop through all boxes in the array
                        {
                            MainServer.checkInBuffers[checkInNumber].Buffer[i - 1] = MainServer.checkInBuffers[checkInNumber].Buffer[i];//Move all content oft the indexes one down
                        }
                    }
                    else
                    {
                        Monitor.Wait(MainServer.checkInBuffers[checkInNumber].Buffer);//Setting the thread in waiting state
                    }
                }
                finally
                {
                    Monitor.Pulse(MainServer.checkInBuffers[checkInNumber].Buffer);//Sending signal to other thread
                    Monitor.Exit(MainServer.checkInBuffers[checkInNumber].Buffer);//Release the lock
                }
            }
        }

        #endregion
    }
}
