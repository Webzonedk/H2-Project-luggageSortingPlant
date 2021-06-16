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


        #endregion

        #region Constructors
        public CheckIn()
        {

        }
        public CheckIn(string workerName, bool open)
        {
            this.workerName = workerName;
            this.open = open;
        }
        #endregion

        #region Methods
 
        public void CheckInLuggage()
        {

        }
        #endregion
    }
}
