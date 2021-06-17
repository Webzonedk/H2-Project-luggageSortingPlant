using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luggageSortingPlant
{
    class Gate
    {
        #region Fields
        private string workerName;
        private bool open;
        private int gateNumber;

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

        public int GateNumber
        {
            get { return gateNumber; }
            set { gateNumber = value; }
        }


        #endregion

        #region Constructors
        public Gate()
        {

        }
        public Gate(string workerName, bool open, int gateNumber)
        {
            this.workerName = workerName;
            this.open = open;
            this.gateNumber = gateNumber;
        }
        #endregion

        #region Methods

        public void Boarding()
        {

        }
        #endregion
    }
}
