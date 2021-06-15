using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class TheCleaningLady
    {
        public void ReorderingFlightPlan()
        {
            while (true)
            {
                if (Manager.flightPlans[0] == null)
                {
                    for (int i = 1; i < Manager.flightPlans.Length; i++)
                    {
                        Manager.flightPlans[i - 1] = Manager.flightPlans[i];
                    }
                }
            }
        }


        public void ReorderingLuggageBuffer()
        {
            while (true)
            {
                if (Manager.luggageBuffer[0] == null)
                {
                    for (int i = 1; i < Manager.luggageBuffer.Length; i++)
                    {
                        Manager.luggageBuffer[i - 1] = Manager.luggageBuffer[i];
                    }
                }
            }
        }
    }
}
