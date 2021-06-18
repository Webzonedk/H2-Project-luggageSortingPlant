using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class LuggageQueueWorker
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LuggageQueueWorker()
        {

        }
        #endregion

        #region Methods

        #endregion
        public void ReorderingLuggageBuffer()//Reordering the LuggageBuffer to work as a queue with first in first out.
        {
            while (true)
            {
                try
                {
                    Monitor.Enter(MainServer.luggageBuffer);//Locking the thread

                    if (MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] != null)
                    {
                        if (MainServer.luggageBuffer[0] == null)
                        {
                            for (int i = 1; i < MainServer.luggageBuffer.Length; i++)
                            {
                                MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                            }
                            MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] = null;
                        }
                    }
                    else
                    {
                        Monitor.Wait(MainServer.luggageBuffer);//Setting the thread in waiting state
                    }
                }
                finally
                {
                    Monitor.PulseAll(MainServer.luggageBuffer);//Sending signal to other thread
                    Monitor.Exit(MainServer.luggageBuffer);//Release the lock
                }
            }
        }
    }
}
