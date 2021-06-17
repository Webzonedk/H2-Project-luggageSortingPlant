using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class CheckIn
    {
        #region Fields
        private string workerName;
        private bool open;
        private int checkInNumber;

        #endregion



        #region Properties
        public string WorkerName
        {
            get { return workerName; }
            set { workerName = value; }
        }
        public bool Open
        {
            get { return open; }
            set { open = value; }
        }
        public int CheckInNumber
        {
            get { return checkInNumber; }
            set { checkInNumber = value; }
        }
        #endregion



        #region Constructors
        public CheckIn()
        {

        }
        public CheckIn(string workerName, bool open, int checkInNumber)
        {
            this.workerName = workerName;
            this.open = open;
            this.checkInNumber = checkInNumber;
        }
        #endregion

        #region Methods
 
        public void CheckInLuggage()
        {
            while (true)
            {
                for (int i = 0; i < MainServer.checkInBuffers[i].; i++)
                {

                }
                if (MainServer.checkInBuffers[i] MainServer.flightPlans.)
                {

                }
            }
        }
        #endregion
    }
}
