﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (MainServer.sortingUnitBuffer[0] == null)//If the buffer index 0 is empty
                {
                    for (int i = 1; i < MainServer.sortingUnitBuffer.Length; i++)//Loop through all boxes in the array
                    {
                        MainServer.sortingUnitBuffer[i - 1] = MainServer.sortingUnitBuffer[i];//Move all content oft the indexes one down
                    }
                }
            }
        }
        #endregion
    }
}
