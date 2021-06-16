using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void ReorderingFlightPlan()
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


        public void ReorderingLuggageBuffer()
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

        public void ReorderingCheckInBuffer()
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

        public void ReorderingGateBuffer()
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
