using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------------------------------------------------------------------------------------
//This class is sorting the Buffers in the same way as a queue.
//As Mikkel (one of the Teachers), has a complicated relationship with the queues and want us to be able to use arrays

//This CleaningLady could be spread out to multiple classes as the SOLID would recommend. But time is a factor as well
//-----------------------------------------------------------------------------------------------
namespace luggageSortingPlant
{
    class CleaningLady


    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public CleaningLady()
        {

        }
        #endregion

        #region Methods
        public void ReorderingFlightPlan()//Reordering the flightplanbuffer to work as a queue with first in first out.
        {
            while (true)
            {
                if (MainServer.flightPlans[0] == null)
                {
                    for (int i = 1; i < MainServer.flightPlans.Length; i++)
                    {
                        MainServer.flightPlans[i - 1] = MainServer.flightPlans[i];
                    }
                }
            }
        }


        public void ReorderingLuggageBuffer()//Reordering the LuggageBuffer to work as a queue with first in first out.
        {
            while (true)
            {
                if (MainServer.luggageBuffer[0] == null)
                {
                    for (int i = 1; i < MainServer.luggageBuffer.Length; i++)
                    {
                        MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    }
                }
            }
        }

        public void ReorderingCheckInBuffer(int checkInNumber)//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                if (MainServer.checkInBuffers[checkInNumber].Buffer[0] == null)//If the buffer index 0 is empty
                {
                    for (int i = 1; i < MainServer.checkInBuffers[checkInNumber].Buffer.Length; i++)//Loop through all boxes in the array
                    {
                        MainServer.checkInBuffers[checkInNumber].Buffer[i - 1] = MainServer.checkInBuffers[checkInNumber].Buffer[i];//Move all content oft the indexes one down
                    }
                }
            }
        }

        public void ReorderingSortingUnitBuffer()//Reordering the checkin Buffers to work as a queue with first in first out.
        {
            while (true)
            {
                if (MainServer.sortingUnitBuffer.Buffer[0] == null)//If the buffer index 0 is empty
                {
                    for (int i = 1; i < MainServer.sortingUnitBuffer.Buffer.Length; i++)//Loop through all boxes in the array
                    {
                        MainServer.sortingUnitBuffer.Buffer[i - 1] = MainServer.sortingUnitBuffer.Buffer[i];//Move all content oft the indexes one down
                    }
                }
            }
        }

        public void ReorderingGateBuffer(int gateNumber)//Not yet adjusted to fit gatebuffer
        {
            while (true)
            {
                if (MainServer.luggageBuffer[0] == null)
                {
                    for (int i = 1; i < MainServer.luggageBuffer.Length; i++)
                    {
                        MainServer.luggageBuffer[i - 1] = MainServer.luggageBuffer[i];
                    }
                }
            }
        }

        #endregion
    }
}
