using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace luggageSortingPlant
{
    class MainEntrance
    {
        #region Fields
        private string workerName;
        #endregion



        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }
        #endregion



        #region Constructors
        public MainEntrance(string workerName)
        {
            this.workerName = workerName;
        }
        #endregion

        #region Methods
        public void SendLuggageToCheckIn()
        {
            while (true)
            {
                Monitor.Enter(Manager.luggageBuffer);
                if (Manager.luggageBuffer[0]!=null)
                {
                    Manager.luggage = Manager.luggageBuffer[0];
                    Manager.luggageBuffer[0] = null;
                    Manager.cleaningLady.ReorderingLuggageBuffer();
                    
                }
            }

        }
        #endregion
    }
}
