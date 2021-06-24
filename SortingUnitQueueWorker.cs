﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace luggageSortingPlant
{
    class SortingUnitQueueWorker
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public SortingUnitQueueWorker()
        {

        }
        #endregion

        #region Methods
        public void ReorderingSortingUnitBuffer()//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                    Monitor.Enter(MainServer.sortingUnitBuffer);//Locking the thread
                try
                {

                    for (int i = 0; i < MainServer.sortingUnitBuffer.Length - 1; i++)//Loop through all boxes in the array
                    {
                        if (MainServer.sortingUnitBuffer[i] == null)//If the buffer index 0 is empty
                        {
                            MainServer.sortingUnitBuffer[i] = MainServer.sortingUnitBuffer[i + 1];//Move content of the index one down
                            MainServer.sortingUnitBuffer[i + 1] = null;//Setting the moved index to null
                        }
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
