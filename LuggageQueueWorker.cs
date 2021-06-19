using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//------------------------------------------------------------------------------------------------------------------
//This class is ment to reordering the luggage array to work as a queue
//------------------------------------------------------------------------------------------------------------------
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

                    for (int i = 0; i < MainServer.luggageBuffer.Length-1; i++)
                    {
                        if (MainServer.luggageBuffer[i]==null)
                        {
                            MainServer.luggageBuffer[i] = MainServer.luggageBuffer[i+1];
                            MainServer.luggageBuffer[i + 1] = null;
                        }
                    }


                    //Old version
                    //if (MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] != null)//Checking if the last index is not empty. if true, do this
                    //{
                    //    if (MainServer.luggageBuffer[0] == null)//if the first index is null
                    //    {
                    //        for (int i = 1; i < MainServer.luggageBuffer.Length; i++)//looping starting from 1 to be able to fill luggage into index 0 and then movin all index one down
                    //        {
                    //            MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    //        }
                    //        MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] = null;
                    //    }
                    //    else
                    //    {
                    //        for (int i = 1; i < MainServer.luggageBuffer.Length; i++)//else looping starting from 1 to be able to fill luggage into index 0 and then movin all index one down
                    //        {
                    //            MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    //        }
                    //        MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] = null;
                    //    }
                    //}


                    //if (MainServer.luggageBuffer[0] == null)
                    //{
                    //    for (int i = 1; i < MainServer.luggageBuffer.Length; i++)
                    //    {
                    //        MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    //    }
                    //        MainServer.luggageBuffer[MainServer.MaxLuggageBuffer - 1] = null;
                    //}
                    //else
                    //{
                    //    Monitor.Wait(MainServer.luggageBuffer);//Setting the thread in waiting state
                    //}
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
