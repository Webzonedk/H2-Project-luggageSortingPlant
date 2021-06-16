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
                Monitor.Enter(MainServer.luggageBuffer);//Locking the thread
                Luggage luggage = new();
                if (MainServer.luggageBuffer[0]==null)
                {
                    MainServer.cleaningLady.ReorderingLuggageBuffer();
                }
                if (MainServer.luggageBuffer[0]!=null)
                {
                    luggage = MainServer.luggageBuffer[0];
                    MainServer.luggageBuffer[0] = null;
                    MainServer.cleaningLady.ReorderingLuggageBuffer();
                    
                }
                Monitor.Pulse(MainServer.luggageBuffer);//Sending signal to LuggageWorker
                Monitor.Exit(MainServer.luggageBuffer);//Unlocking thread

                if (true)
                {
                    Monitor.Enter(MainServer.checkIns[].CheckInLuggage);//Big error........FIX IT NOW!!!!!!!

                }
            }

        }
        #endregion
    }
}
